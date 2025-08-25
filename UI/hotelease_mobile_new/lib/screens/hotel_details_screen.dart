import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:hotelease_mobile_new/models/asset.dart';
import 'package:hotelease_mobile_new/models/hotel.dart';
import 'package:hotelease_mobile_new/models/review.dart';
import 'package:hotelease_mobile_new/models/room.dart';
import 'package:hotelease_mobile_new/providers/assets_provider.dart';
import 'package:hotelease_mobile_new/providers/hotels_provider.dart';
import 'package:hotelease_mobile_new/providers/reviews_provider.dart';
import 'package:hotelease_mobile_new/providers/rooms.provider.dart';
import 'package:hotelease_mobile_new/screens/master_screen.dart';
import 'package:hotelease_mobile_new/screens/reservations_screen.dart';
import 'package:hotelease_mobile_new/widgets/calendar_widget.dart';

import 'package:provider/provider.dart';

class HotelDetailsScreen extends StatefulWidget {
  final int? id;
  final String? name;
  final int? starRating;
  final int? price;
  final String? description;

  const HotelDetailsScreen({
    super.key,
    this.id,
    this.name,
    this.starRating,
    this.price,
    this.description,
  });

  @override
  State<HotelDetailsScreen> createState() => _HotelDetailsScreenState();
}

class _HotelDetailsScreenState extends State<HotelDetailsScreen> {
  final Map<int, PageController> _pageControllers = {};
  final Map<int, List<Asset>> _hotelAssets = {};

  late HotelsProvider _hotelsProvider;

  late AssetsProvider _assetsProvider;
  late RoomsProvider _roomsProvider;

  late ReviewsProvider _reviewsProvider;
  List<Review> _reviews = [];
  bool _loadingReviews = true;

  int _rating = 5;

  bool _loadingAssets = true;
  bool _loadingRooms = true;

  List<Room> _rooms = [];

  @override
  void initState() {
    super.initState();
    _pageControllers[widget.id ?? 0] = PageController();
    _hotelAssets[widget.id ?? 0] = [];
  }

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
    _assetsProvider = context.read<AssetsProvider>();
    _roomsProvider = context.read<RoomsProvider>();

    _reviewsProvider = context.read<ReviewsProvider>();

    _hotelsProvider = context.read<HotelsProvider>();

    if (_loadingAssets) _loadHotelAssets();
    if (_loadingRooms) _loadRooms();
    if (_loadingReviews) _loadReviews();
  }

  Future<void> _loadReviews() async {
    if (widget.id == null) return;
    try {
      final result = await _reviewsProvider.getByHotelId(widget.id!);
      setState(() {
        _reviews = result;
        _loadingReviews = false;
      });
    } catch (e) {
      print("Greška kod učitavanja reviews: $e");
      setState(() => _loadingReviews = false);
    }
  }

  Future<void> _loadHotelAssets() async {
    if (widget.id == null) return;
    try {
      final result = await _assetsProvider.getAssetsByHotelId(widget.id!);
      setState(() {
        _hotelAssets[widget.id!] = result.result;
        _loadingAssets = false;
      });
    } catch (e) {
      print("Greška kod učitavanja slika: $e");
      setState(() => _loadingAssets = false);
    }
  }

  Future<void> _loadRooms() async {
    if (widget.id == null) return;
    try {
      final data = await _roomsProvider.getRoomsByHotelId(widget.id!);
      setState(() {
        _rooms = data;
        _loadingRooms = false;
      });
    } catch (e) {
      print("Greška kod učitavanja soba: $e");
      setState(() => _loadingRooms = false);
    }
  }

  Widget buildStarRating(int? rating) {
    return Row(
      children: List.generate(5, (index) {
        if (rating != null && index < rating) {
          return const Icon(Icons.star, color: Colors.white, size: 20);
        } else {
          return const Icon(Icons.star_border, color: Colors.white, size: 20);
        }
      }),
    );
  }

  Widget buildHotelImages(int hotelId, List<Asset> assets) {
    if (assets.isEmpty) {
      return Image.asset(
        'assets/images/cant_load_image.png',
        width: double.infinity,
        height: 200,
        fit: BoxFit.cover,
      );
    }

    final controller = _pageControllers[hotelId]!;

    return SizedBox(
      width: double.infinity,
      height: 200,
      child: PageView.builder(
        controller: controller,
        itemCount: assets.length,
        itemBuilder: (context, index) {
          final imageString = assets[index].image;
          if (imageString == null || imageString.isEmpty) {
            return Image.asset(
              'assets/images/cant_load_image.png',
              fit: BoxFit.cover,
            );
          }

          try {
            final decodedUrl = utf8.decode(base64.decode(imageString));
            return Image.network(
              decodedUrl,
              fit: BoxFit.cover,
              errorBuilder: (_, __, ___) => Image.asset(
                'assets/images/cant_load_image.png',
                fit: BoxFit.cover,
              ),
            );
          } catch (e) {
            return Image.asset(
              'assets/images/cant_load_image.png',
              fit: BoxFit.cover,
            );
          }
        },
      ),
    );
  }

  Widget buildRoomsSection(List<Room> rooms) {
    if (rooms.isEmpty) {
      return const Text(
        "No rooms available",
        style: TextStyle(color: Colors.white70),
      );
    }

    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        const SizedBox(height: 20),
        const Text(
          "Rooms",
          style: TextStyle(
            fontSize: 22,
            fontWeight: FontWeight.bold,
            color: Colors.white,
          ),
        ),
        const SizedBox(height: 10),
        ...rooms.map((room) {
          return Card(
            color: const Color.fromRGBO(15, 30, 70, 1),
            margin: const EdgeInsets.symmetric(vertical: 8),
            child: Padding(
              padding: const EdgeInsets.all(8.0),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Center(
                    child: Text(
                      room.name ?? "Unknown Room",
                      style: const TextStyle(
                        color: Colors.white,
                        fontSize: 18,
                        fontWeight: FontWeight.bold,
                      ),
                    ),
                  ),
                  const SizedBox(height: 4),
                  Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: [
                      Text(
                        "Capacity: ${room.capacity ?? '-'}",
                        style: const TextStyle(color: Colors.white70),
                      ),
                      Row(
                        children: [
                          if (room.wiFi == true)
                            const Icon(
                              Icons.wifi,
                              color: Colors.green,
                              size: 20,
                            ),
                          if (room.ac == true)
                            const Icon(
                              Icons.ac_unit_rounded,
                              color: Colors.blue,
                              size: 20,
                            ),
                          if (room.queenBed == true)
                            const Icon(
                              Icons.bed_outlined,
                              color: Colors.cyan,
                              size: 20,
                            ),
                          if (room.cityView == true)
                            const Icon(
                              Icons.nature,
                              color: Colors.purple,
                              size: 20,
                            ),
                        ],
                      ),
                      Row(
                        children: [
                          Text(
                            "\$${room.pricePerNight}",
                            style: const TextStyle(
                              fontSize: 24,
                              fontWeight: FontWeight.bold,
                              color: Colors.white,
                            ),
                          ),
                          const Text(
                            "/night",
                            style: TextStyle(color: Colors.white, fontSize: 16),
                          ),
                        ],
                      ),
                    ],
                  ),
                  const SizedBox(height: 10),
                  Center(
                    child: ElevatedButton(
                      style: ElevatedButton.styleFrom(
                        backgroundColor: const Color.fromRGBO(15, 30, 70, 1),
                        side: const BorderSide(color: Colors.white),
                        shape: RoundedRectangleBorder(
                          borderRadius: BorderRadius.circular(30),
                        ),
                      ),
                      onPressed: () {
                        showModalBottomSheet(
                          context: context,
                          isScrollControlled: true,
                          backgroundColor: Colors.white,
                          builder: (_) {
                            return SizedBox(
                              height: MediaQuery.of(context).size.height * 0.6,
                              child: CalendarWidget(
                                roomId: room.id!,
                                onDateRangeSelected: (checkIn, checkOut) {
                                  print(
                                    "hotelId: $widget.id; checkInDate: $checkIn; checkOutDate: $checkOut",
                                  );
                                  Navigator.pop(context);
                                  Navigator.push(
                                    context,
                                    MaterialPageRoute(
                                      builder: (_) => ReservationScreen(
                                        hotelName: widget.name,
                                        hotelId: widget.id!,
                                        room: room,
                                        checkInDate: checkIn,
                                        checkOutDate: checkOut,
                                      ),
                                    ),
                                  );
                                },
                              ),
                            );
                          },
                        );
                      },
                      child: const Text(
                        "Book now",
                        style: TextStyle(color: Colors.white, fontSize: 18),
                      ),
                    ),
                  ),
                ],
              ),
            ),
          );
        }),
      ],
    );
  }

  Widget buildReviewsSection(List<Review> reviews) {
    if (reviews.isEmpty) {
      return const Text(
        "No reviews yet",
        style: TextStyle(color: Colors.white70),
      );
    }

    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        const SizedBox(height: 20),
        const Text(
          "Reviews",
          style: TextStyle(
            fontSize: 22,
            fontWeight: FontWeight.bold,
            color: Colors.white,
          ),
        ),
        const SizedBox(height: 10),
        ...reviews.map((review) {
          return Card(
            color: const Color.fromRGBO(15, 30, 70, 1),
            margin: const EdgeInsets.symmetric(vertical: 6),
            child: ListTile(
              title: Text(
                review.comment ?? "No comment",
                style: const TextStyle(color: Colors.white),
              ),
              subtitle: Row(
                children: List.generate(5, (index) {
                  if (index < review.rating) {
                    return const Icon(
                      Icons.star,
                      color: Colors.yellow,
                      size: 16,
                    );
                  } else {
                    return const Icon(
                      Icons.star_border,
                      color: Colors.white,
                      size: 16,
                    );
                  }
                }),
              ),
              trailing: Text(
                review.reviewDate != null
                    ? "${review.reviewDate!.day}.${review.reviewDate!.month}.${review.reviewDate!.year}"
                    : "",
                style: const TextStyle(color: Colors.white70, fontSize: 12),
              ),
            ),
          );
        }),
      ],
    );
  }

  Widget buildAddReviewSection() {
    final _commentController = TextEditingController();

    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        const Text(
          "Leave a Review",
          style: TextStyle(color: Colors.white, fontSize: 20),
        ),
        const SizedBox(height: 10),
        Row(
          children: List.generate(5, (index) {
            return IconButton(
              icon: Icon(
                index < _rating ? Icons.star : Icons.star_border,
                color: Colors.yellow,
              ),
              onPressed: () {
                setState(() {
                  _rating = index + 1;
                });
              },
            );
          }),
        ),
        TextField(
          controller: _commentController,
          decoration: const InputDecoration(
            hintText: "Write your comment...",
            hintStyle: TextStyle(color: Colors.white54),
            enabledBorder: UnderlineInputBorder(
              borderSide: BorderSide(color: Colors.white70),
            ),
          ),
          style: const TextStyle(color: Colors.white),
        ),
        const SizedBox(height: 10),
        ElevatedButton(
          style: ElevatedButton.styleFrom(
            backgroundColor: const Color.fromRGBO(15, 30, 70, 1),
            side: const BorderSide(color: Colors.white),
          ),
          onPressed: () async {
            if (widget.id == null) return;
            final newReview = Review(
              userId: 1, // TODO: uzmi iz login session
              hotelId: widget.id!,
              reservationId: 1, // TODO: uzmi pravu rezervaciju
              rating: _rating,
              comment: _commentController.text,
              reviewDate: DateTime.now(),
            );
            await _reviewsProvider.addReview(newReview);
            _loadReviews();
          },
          child: const Text(
            "Submit Review",
            style: TextStyle(color: Colors.white),
          ),
        ),
      ],
    );
  }

  Widget _buildHotelImage(String? base64Image) {
    if (base64Image == null || base64Image.isEmpty) {
      return Image.asset(
        'assets/images/cant_load_image.png',
        height: 100,
        fit: BoxFit.cover,
      );
    }

    try {
      // Backend ti šalje BASE64 ENCODED URL
      final decodedUrl = utf8.decode(base64.decode(base64Image));
      return Image.network(
        decodedUrl,
        height: 100,
        fit: BoxFit.cover,
        errorBuilder: (_, __, ___) => Image.asset(
          'assets/images/cant_load_image.png',
          height: 100,
          fit: BoxFit.cover,
        ),
      );
    } catch (e) {
      return Image.asset(
        'assets/images/cant_load_image.png',
        height: 100,
        fit: BoxFit.cover,
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    final assets = _hotelAssets[widget.id ?? 0] ?? [];

    return MasterScreen(
      title: "Hotel details",
      child: SingleChildScrollView(
        child: Column(
          children: [
            const SizedBox(height: 30),
            ClipRRect(
              borderRadius: const BorderRadius.vertical(
                top: Radius.circular(8),
              ),
              child: buildHotelImages(widget.id ?? 0, assets),
            ),
            const SizedBox(height: 10),
            Text(
              widget.name ?? "",
              style: const TextStyle(color: Colors.white, fontSize: 26),
            ),
            const SizedBox(height: 10),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceAround,
              children: [
                buildStarRating(widget.starRating),
                Row(
                  children: [
                    Text(
                      "\$${widget.price}",
                      style: const TextStyle(
                        fontSize: 24,
                        fontWeight: FontWeight.bold,
                        color: Colors.white,
                      ),
                    ),
                    const Text(
                      "/night",
                      style: TextStyle(fontSize: 22, color: Colors.white),
                    ),
                  ],
                ),
              ],
            ),
            const SizedBox(height: 20),
            Text(
              widget.description ?? "",
              style: const TextStyle(color: Colors.white),
            ),
            const SizedBox(height: 20),
            const Text(
              "AMENITIES",
              style: TextStyle(color: Colors.white, fontSize: 22),
            ),
            const SizedBox(height: 20),
            if (_loadingRooms)
              const CircularProgressIndicator()
            else
              buildRoomsSection(_rooms),
            const SizedBox(height: 20),

            if (_loadingReviews)
              const CircularProgressIndicator()
            else
              buildReviewsSection(_reviews),

            const SizedBox(height: 20),
            buildAddReviewSection(),
            const SizedBox(height: 20),
            SizedBox(height: 20),
            Text(
              "Similar Hotels",
              style: TextStyle(color: Colors.white, fontSize: 22),
            ),
            SizedBox(height: 15),
            FutureBuilder<List<Hotel>>(
              future: _hotelsProvider.getContentBasedHotels(widget.id!, top: 5),
              builder: (context, snapshot) {
                if (snapshot.connectionState == ConnectionState.waiting) {
                  return CircularProgressIndicator(color: Colors.white);
                }
                if (!snapshot.hasData || snapshot.data!.isEmpty) {
                  return Text(
                    "No similar hotels",
                    style: TextStyle(color: Colors.white),
                  );
                }

                final hotels = snapshot.data!;
                return SizedBox(
                  height: 220,
                  child: ListView.builder(
                    scrollDirection: Axis.horizontal,
                    itemCount: hotels.length,
                    itemBuilder: (context, index) {
                      final hotel = hotels[index];
                      final List<Asset> assets = [];
                      if (hotel.rooms != null) {
                        for (var r in hotel.rooms!) {
                          if (r.assets != null) {
                            assets.addAll(r.assets!);
                          }
                        }
                      }

                      return GestureDetector(
                        onTap: () {
                          Navigator.push(
                            context,
                            MaterialPageRoute(
                              builder: (_) => HotelDetailsScreen(
                                id: hotel.id,
                                name: hotel.name,
                                starRating: hotel.starRating,
                                price: hotel.price,
                                description: hotel.description,
                              ),
                            ),
                          );
                        },
                        child: Container(
                          width: 160,
                          margin: EdgeInsets.all(8),
                          child: Column(
                            children: [
                              Expanded(
                                child: assets != null && assets.isNotEmpty
                                    ? PageView.builder(
                                        itemCount: assets.length,
                                        itemBuilder: (context, i) {
                                          return _buildHotelImage(
                                            assets[i].image,
                                          );
                                        },
                                      )
                                    : Image.asset(
                                        'assets/images/cant_load_image.png',
                                        fit: BoxFit.cover,
                                      ),
                              ),
                              SizedBox(height: 5),
                              Text(
                                hotel.name ?? "",
                                style: TextStyle(color: Colors.white),
                                overflow: TextOverflow.ellipsis,
                              ),
                              Text(
                                "${hotel.starRating} ★",
                                style: TextStyle(color: Colors.yellow),
                              ),
                            ],
                          ),
                        ),
                      );
                    },
                  ),
                );
              },
            ),
          ],
        ),
      ),
    );
  }
}
