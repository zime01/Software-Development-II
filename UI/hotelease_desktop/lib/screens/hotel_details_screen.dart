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
import 'package:hotelease_mobile_new/screens/manager_rooms_screen.dart';
import 'package:hotelease_mobile_new/screens/manager_services_screen.dart';
import 'package:hotelease_mobile_new/screens/manager_stats_screen.dart';
import 'package:hotelease_mobile_new/screens/master_screen.dart';
import 'package:provider/provider.dart';
import '../utils/USDformat.dart';

class HotelDetailsScreen extends StatefulWidget {
  final Hotel? hotel;

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
    this.hotel,
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

  List<Room> _rooms = [];
  List<Review> _reviews = [];
  int _rating = 5;
  bool _loadingRooms = true;
  bool _loadingReviews = true;
  bool _loadingAssets = true;

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

  Future<void> _loadRooms() async {
    if (widget.id == null) return;
    try {
      final data = await _roomsProvider.getRoomsByHotelId(widget.id!);
      setState(() {
        _rooms = data;
        _loadingRooms = false;
      });
    } catch (e) {
      print("Error loading rooms: $e");
      setState(() => _loadingRooms = false);
    }
  }

  Future<void> _loadReviews() async {
    if (widget.id == null) return;
    try {
      final data = await _reviewsProvider.getByHotelId(widget.id!);
      setState(() {
        _reviews = data;
        _loadingReviews = false;
      });
    } catch (e) {
      print("Error loading reviews: $e");
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
      print("Error loading hotel images: $e");
      setState(() => _loadingAssets = false);
    }
  }

  void _openRoomForm([Room? room]) {
    Navigator.push(
      context,
      MaterialPageRoute(
        builder: (_) => RoomFormScreen(room: room, hotelId: widget.id!),
      ),
    ).then((_) => _loadRooms());
  }

  Future<void> _deleteRoom(int roomId) async {
    try {
      await _roomsProvider.delete(roomId);
      _loadRooms();
    } catch (e) {
      ScaffoldMessenger.of(
        context,
      ).showSnackBar(SnackBar(content: Text("Error deleting room: $e")));
    }
  }

  void _showRoomImagesModal(Room room) async {
    List<Asset> roomAssets = [];
    try {
      final result = await _assetsProvider.getAssetsByRoomId(room.id!);
      roomAssets = result.result;
    } catch (e) {
      print("Error loading room images: $e");
    }

    if (roomAssets.isEmpty) roomAssets = [];

    showDialog(
      context: context,
      builder: (_) => Dialog(
        backgroundColor: Colors.transparent,
        child: SizedBox(
          height: 300,
          child: PageView.builder(
            itemCount: roomAssets.length,
            itemBuilder: (context, index) {
              final imageStr = roomAssets[index].image;

              if (imageStr == null || imageStr.isEmpty) {
                return Image.asset(
                  'assets/images/cant_load_image.png',
                  fit: BoxFit.cover,
                );
              }

              try {
                // 1) Decode base64
                final decoded = base64.decode(imageStr);

                // 2) Pokušaj pretvoriti u UTF-8 string (možda je URL)
                final asString = utf8.decode(decoded, allowMalformed: true);

                // 3) Ako počinje sa http → to je base64-url
                if (asString.startsWith("http://") ||
                    asString.startsWith("https://")) {
                  return Image.network(asString, fit: BoxFit.cover);
                }

                // 4) Inače → to je raw slika (JPEG/PNG)
                return Image.memory(decoded, fit: BoxFit.cover);
              } catch (e) {
                print("Image decode error: $e");
                return Image.asset(
                  'assets/images/cant_load_image.png',
                  fit: BoxFit.cover,
                );
              }
            },
          ),
        ),
      ),
    );
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
          final imageStr = assets[index].image;

          if (imageStr == null || imageStr.isEmpty) {
            return Image.asset(
              'assets/images/cant_load_image.png',
              fit: BoxFit.cover,
            );
          }

          try {
            // 1) Decode base64 string → bytes
            final decoded = base64.decode(imageStr);

            // 2) Pokušaj pretvoriti bytes → string (možda URL)
            final asString = utf8.decode(decoded, allowMalformed: true);

            // 3) Ako počinje s http → tretiramo kao URL
            if (asString.startsWith("http://") ||
                asString.startsWith("https://")) {
              return Image.network(
                asString,
                fit: BoxFit.cover,
                errorBuilder: (_, __, ___) => Image.asset(
                  'assets/images/cant_load_image.png',
                  fit: BoxFit.cover,
                ),
              );
            }

            // 4) Inače, radi se o raw slici (JPEG/PNG base64)
            return Image.memory(
              decoded,
              fit: BoxFit.cover,
              errorBuilder: (_, __, ___) => Image.asset(
                'assets/images/cant_load_image.png',
                fit: BoxFit.cover,
              ),
            );
          } catch (e) {
            print("Image decode error: $e");
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
    if (rooms.isEmpty)
      return const Text(
        "No rooms available",
        style: TextStyle(color: Colors.white70),
      );

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
          return GestureDetector(
            onTap: () => _showRoomImagesModal(room),
            child: Card(
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
                        Text(
                          "${usdFormatter.format(room.pricePerNight!.toDouble())}/night",
                          style: const TextStyle(
                            fontSize: 16,
                            fontWeight: FontWeight.bold,
                            color: Colors.white,
                          ),
                        ),
                      ],
                    ),
                    const SizedBox(height: 10),
                    Row(
                      mainAxisAlignment: MainAxisAlignment.center,
                      children: [
                        ElevatedButton(
                          onPressed: () => _openRoomForm(room),
                          child: const Text("Edit"),
                        ),
                        const SizedBox(width: 10),
                        ElevatedButton(
                          style: ElevatedButton.styleFrom(
                            backgroundColor: Colors.red,
                          ),
                          onPressed: () => _deleteRoom(room.id!),
                          child: const Text("Delete"),
                        ),
                      ],
                    ),
                  ],
                ),
              ),
            ),
          );
        }),
        const SizedBox(height: 10),
        Center(
          child: ElevatedButton(
            onPressed: () => _openRoomForm(),
            child: const Text("Add New Room"),
          ),
        ),
      ],
    );
  }

  Widget buildReviewsSection(List<Review> reviews) {
    if (reviews.isEmpty)
      return const Text(
        "No reviews yet",
        style: TextStyle(color: Colors.white70),
      );

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
                  if (index < review.rating)
                    return const Icon(
                      Icons.star,
                      color: Colors.yellow,
                      size: 16,
                    );
                  return const Icon(
                    Icons.star_border,
                    color: Colors.white,
                    size: 16,
                  );
                }),
              ),
              trailing: Text(
                review.reviewDate != null
                    ? "${review.reviewDate!.day}.${review.reviewDate!.month}.${review.reviewDate!.year}."
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
        Text(
          "Leave a Review",
          style: TextStyle(
            color: Theme.of(context).colorScheme.onPrimary,
            fontSize: 20,
          ),
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
          decoration: InputDecoration(
            hintText: "Write your comment...",
            hintStyle: TextStyle(
              color: Theme.of(context).colorScheme.onPrimary,
            ),
            enabledBorder: UnderlineInputBorder(
              borderSide: BorderSide(
                color: Theme.of(context).colorScheme.onPrimary,
              ),
            ),
          ),
          style: const TextStyle(color: Colors.white),
        ),
        const SizedBox(height: 10),
        ElevatedButton(
          style: ElevatedButton.styleFrom(
            backgroundColor: Theme.of(context).colorScheme.secondary,
            side: BorderSide(color: Theme.of(context).colorScheme.onPrimary),
          ),
          onPressed: () async {
            if (widget.id == null) return;
            final newReview = Review(
              userId: 1,
              hotelId: widget.id!,
              reservationId: 1,
              rating: _rating,
              comment: _commentController.text,
              reviewDate: DateTime.now(),
            );
            await _reviewsProvider.addReview(newReview);
            _loadReviews();
            _commentController.clear();
            setState(() {
              _rating = 5;
            });
          },
          child: Text(
            "Submit Review",
            style: TextStyle(color: Theme.of(context).colorScheme.onPrimary),
          ),
        ),
      ],
    );
  }

  Widget buildHotelAmenities(Hotel hotel) {
    return Padding(
      padding: const EdgeInsets.symmetric(vertical: 12.0),
      child: Wrap(
        spacing: 20,
        runSpacing: 12,
        children: [
          if (hotel.wifi == true)
            Column(
              mainAxisSize: MainAxisSize.min,
              children: [
                Icon(Icons.wifi, color: Colors.green, size: 28),
                Text(
                  "WiFi",
                  style: TextStyle(
                    color: Theme.of(context).colorScheme.onPrimary,
                    fontSize: 12,
                  ),
                ),
              ],
            ),
          if (hotel.pool == true)
            Column(
              mainAxisSize: MainAxisSize.min,
              children: [
                Icon(Icons.pool, color: Colors.cyan, size: 28),
                Text(
                  "Pool",
                  style: TextStyle(
                    color: Theme.of(context).colorScheme.onPrimary,
                    fontSize: 12,
                  ),
                ),
              ],
            ),
          if (hotel.parking == true)
            Column(
              mainAxisSize: MainAxisSize.min,
              children: [
                Icon(Icons.local_parking, color: Colors.blue, size: 28),
                Text(
                  "Parking",
                  style: TextStyle(
                    color: Theme.of(context).colorScheme.onPrimary,
                    fontSize: 12,
                  ),
                ),
              ],
            ),
          if (hotel.bar == true)
            Column(
              mainAxisSize: MainAxisSize.min,
              children: [
                Icon(Icons.local_bar, color: Colors.purple, size: 28),
                Text(
                  "Bar",
                  style: TextStyle(
                    color: Theme.of(context).colorScheme.onPrimary,
                    fontSize: 12,
                  ),
                ),
              ],
            ),
          if (hotel.fitness == true)
            Column(
              mainAxisSize: MainAxisSize.min,
              children: [
                Icon(Icons.fitness_center, color: Colors.orange, size: 28),
                Text(
                  "Fitness",
                  style: TextStyle(
                    color: Theme.of(context).colorScheme.onPrimary,
                    fontSize: 12,
                  ),
                ),
              ],
            ),
          if (hotel.spa == true)
            Column(
              mainAxisSize: MainAxisSize.min,
              children: [
                Icon(Icons.spa, color: Colors.pink, size: 28),
                Text(
                  "Spa",
                  style: TextStyle(
                    color: Theme.of(context).colorScheme.onPrimary,
                    fontSize: 12,
                  ),
                ),
              ],
            ),
        ],
      ),
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

  Widget buildManagerActions(int hotelId) {
    return Row(
      mainAxisAlignment: MainAxisAlignment.spaceEvenly,
      children: [
        // SERVICES
        GestureDetector(
          onTap: () {
            Navigator.push(
              context,
              MaterialPageRoute(
                builder: (_) => ManagerServicesScreen(hotelId: hotelId),
              ),
            );
          },
          child: Column(
            children: const [
              Icon(Icons.room_service, color: Colors.white, size: 32),
              SizedBox(height: 6),
              Text("Services", style: TextStyle(color: Colors.white)),
            ],
          ),
        ),

        // STATS
        GestureDetector(
          onTap: () {
            Navigator.push(
              context,
              MaterialPageRoute(
                builder: (_) => ManagerStatsScreen(hotelId: hotelId),
              ),
            );
          },
          child: Column(
            children: const [
              Icon(Icons.bar_chart, color: Colors.white, size: 32),
              SizedBox(height: 6),
              Text("Stats", style: TextStyle(color: Colors.white)),
            ],
          ),
        ),
      ],
    );
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
                      "${usdFormatter.format(widget.price?.toDouble() ?? 0)}",
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
            if (widget.hotel != null) buildHotelAmenities(widget.hotel!),
            const SizedBox(height: 20),

            buildManagerActions(widget.id!),

            const SizedBox(height: 20),
            const SizedBox(height: 20),
            _loadingRooms
                ? const CircularProgressIndicator()
                : buildRoomsSection(_rooms),

            const SizedBox(height: 20),
            _loadingReviews
                ? const CircularProgressIndicator()
                : buildReviewsSection(_reviews),
            const SizedBox(height: 20),
            buildAddReviewSection(),
            const SizedBox(height: 20),
            const SizedBox(height: 20),
            Text(
              "Similar Hotels",
              style: TextStyle(
                color: Theme.of(context).colorScheme.onPrimary,
                fontSize: 22,
              ),
            ),
            const SizedBox(height: 15),
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
                                style: TextStyle(
                                  color: Colors.white,
                                  fontSize: 16,
                                ),
                                overflow: TextOverflow.ellipsis,
                              ),
                              SizedBox(height: 5),
                              Text(
                                hotel.city?.name ?? "Unknown city",
                                style: TextStyle(color: Colors.white),
                                overflow: TextOverflow.ellipsis,
                              ),
                              SizedBox(height: 5),
                              Text(
                                "${hotel.starRating} ★",
                                style: TextStyle(color: Colors.yellow),
                              ),
                              SizedBox(height: 5),
                              Text(
                                "${formatPrice(hotel.minPrice)} - ${formatPrice(hotel.maxPrice)} /night",
                                style: TextStyle(
                                  color: Colors.white70,
                                  fontSize: 12,
                                ),
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
