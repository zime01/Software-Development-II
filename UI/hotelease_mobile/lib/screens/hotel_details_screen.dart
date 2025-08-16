import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:hotelease_mobile/models/asset.dart';
import 'package:hotelease_mobile/models/room.dart';
import 'package:hotelease_mobile/providers/assets_provider.dart';
import 'package:hotelease_mobile/providers/rooms.provider.dart';
import 'package:hotelease_mobile/screens/master_screen.dart';
import 'package:hotelease_mobile/widgets/calendar_widget.dart';
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

  late AssetsProvider _assetsProvider;
  late RoomsProvider _roomsProvider;

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

    if (_loadingAssets) _loadHotelAssets();
    if (_loadingRooms) _loadRooms();
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
                              child: CalendarWidget(roomId: room.id!),
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
          ],
        ),
      ),
    );
  }
}
