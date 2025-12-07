import 'dart:async';
import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:hotelease_mobile_new/models/asset.dart';
import 'package:hotelease_mobile_new/models/hotel.dart';
import 'package:hotelease_mobile_new/models/room.dart';
import 'package:hotelease_mobile_new/providers/hotels_provider.dart';
import 'package:hotelease_mobile_new/screens/hotel_form_screen.dart';

class HotelsManageScreen extends StatefulWidget {
  const HotelsManageScreen({super.key});

  @override
  State<HotelsManageScreen> createState() => _HotelsManageScreenState();
}

class _HotelsManageScreenState extends State<HotelsManageScreen> {
  final HotelsProvider _provider = HotelsProvider();
  List<Hotel> hotels = [];

  final Map<int, List<Asset>> _hotelAssets = {};
  final Map<int, PageController> _pageControllers = {};
  final Map<int, Timer> _timers = {};

  @override
  void initState() {
    super.initState();
    loadHotels();
  }

  @override
  void dispose() {
    for (var controller in _pageControllers.values) controller.dispose();
    for (var timer in _timers.values) timer.cancel();
    super.dispose();
  }

  Future<void> loadHotels() async {
    var result = await _provider.get(filter: {"isRoomsIcluded": true});
    hotels = result.result;

    print("🔹 Loaded ${hotels.length} hotels from API.");

    await Future.wait(
      hotels.map((hotel) async {
        final id = hotel.id ?? 0;

        // Spoj sve assets iz soba hotela
        final List<Asset> assets = [];
        if (hotel.rooms != null) {
          for (Room room in hotel.rooms!) {
            if (room.assets != null) {
              assets.addAll(room.assets!);
            }
          }
        }

        _hotelAssets[id] = assets;
        print("  Hotel '${hotel.name}' has ${assets.length} assets.");

        final controller = PageController();
        _pageControllers[id] = controller;

        if (assets.isNotEmpty) {
          _timers[id]?.cancel();
          _timers[id] = Timer.periodic(const Duration(seconds: 3), (_) {
            if (!mounted) return;
            if (controller.hasClients) {
              int current = controller.page?.toInt() ?? 0;
              int next = (current + 1) % assets.length;
              controller.animateToPage(
                next,
                duration: const Duration(milliseconds: 300),
                curve: Curves.easeInOut,
              );
            }
          });

          for (var asset in assets) {
            if (asset.image != null && asset.image!.length > 20) {
              print(
                "    Asset '${asset.fileName}' image preview: ${asset.image!.substring(0, 20)}",
              );
            } else {
              print("    Asset '${asset.fileName}' image is NULL or too short");
            }
          }
        }
      }),
    );

    setState(() {});
  }

  Future<void> deleteHotel(int id) async {
    await _provider.delete(id);
    loadHotels();
  }

  Widget _placeholder() {
    return Container(
      width: 60,
      height: 60,
      decoration: BoxDecoration(
        color: Colors.grey.shade700,
        borderRadius: BorderRadius.circular(8),
      ),
      child: const Icon(Icons.image_not_supported, color: Colors.white70),
    );
  }

  Widget hotelImage(String? raw, {double width = 60, double height = 60}) {
    if (raw == null || raw.isEmpty) return _placeholder();

    try {
      // 1) Pokušaj dekodirati base64
      final decoded = base64.decode(raw);

      // 2) Pretvori u UTF-8 string da provjeriš da li je URL
      final asString = utf8.decode(decoded, allowMalformed: true);

      // 3) Ako počinje sa http/https → base64-url
      if (asString.startsWith("http://") || asString.startsWith("https://")) {
        return Image.network(
          asString,
          width: width,
          height: height,
          fit: BoxFit.cover,
          errorBuilder: (_, __, ___) => _placeholder(),
        );
      }

      // 4) Inače → raw slika (JPEG/PNG)
      return Image.memory(
        decoded,
        width: width,
        height: height,
        fit: BoxFit.cover,
      );
    } catch (e) {
      // 5) Ako nije base64, provjeri da li je raw URL
      if (raw.startsWith("http://") || raw.startsWith("https://")) {
        return Image.network(
          raw,
          width: width,
          height: height,
          fit: BoxFit.cover,
          errorBuilder: (_, __, ___) => _placeholder(),
        );
      }

      // 6) Inače fallback
      return _placeholder();
    }
  }

  Widget buildHotelImages(int hotelId, List<Asset> assets) {
    if (assets.isEmpty) return _placeholder();
    final controller = _pageControllers[hotelId]!;
    return SizedBox(
      width: 60,
      height: 60,
      child: PageView.builder(
        controller: controller,
        itemCount: assets.length,
        itemBuilder: (context, index) {
          return hotelImage(assets[index].image);
        },
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: const Color.fromRGBO(17, 45, 78, 1),
      appBar: AppBar(
        iconTheme: const IconThemeData(color: Colors.white),
        title: const Text(
          "Manage Hotels",
          style: TextStyle(color: Colors.white),
        ),
        backgroundColor: const Color.fromRGBO(17, 45, 78, 1),
        actions: [
          IconButton(
            icon: const Icon(Icons.add),
            onPressed: () async {
              final result = await Navigator.push(
                context,
                MaterialPageRoute(builder: (_) => const HotelFormScreen()),
              );
              if (result == true) loadHotels();
            },
          ),
        ],
      ),
      body: ListView.builder(
        itemCount: hotels.length,
        itemBuilder: (context, index) {
          final h = hotels[index];
          final id = h.id ?? 0;
          final assets = _hotelAssets[id] ?? [];

          return ListTile(
            leading: buildHotelImages(id, assets),
            title: Text(
              h.name ?? "",
              style: const TextStyle(color: Colors.white),
            ),
            subtitle: Text(
              h.address ?? "",
              style: const TextStyle(color: Colors.white70),
            ),
            trailing: Row(
              mainAxisSize: MainAxisSize.min,
              children: [
                IconButton(
                  icon: const Icon(Icons.edit, color: Colors.blue),
                  onPressed: () async {
                    final result = await Navigator.push(
                      context,
                      MaterialPageRoute(
                        builder: (_) => HotelFormScreen(hotel: h),
                      ),
                    );
                    if (result == true) loadHotels();
                  },
                ),
                IconButton(
                  icon: const Icon(Icons.delete, color: Colors.red),
                  onPressed: () => deleteHotel(id),
                ),
              ],
            ),
          );
        },
      ),
    );
  }
}
