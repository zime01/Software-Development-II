import 'package:flutter/material.dart';
import 'package:hotelease_mobile_new/models/hotel.dart';
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

  @override
  void initState() {
    super.initState();
    loadHotels();
  }

  Future<void> loadHotels() async {
    var result = await _provider.get();
    setState(() {
      hotels = result.result; // direktno, bez .result
    });
  }

  Future<void> deleteHotel(int id) async {
    await _provider.delete(id);
    loadHotels();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: const Color.fromRGBO(17, 45, 78, 1),
      appBar: AppBar(
        foregroundColor: Colors.white,
        backgroundColor: const Color.fromRGBO(17, 45, 78, 1),
        title: const Text(
          "Manage Hotels",
          style: TextStyle(color: Colors.white),
        ),
        actions: [
          IconButton(
            icon: const Icon(Icons.add),
            onPressed: () async {
              // Otvori screen za dodavanje novog hotela
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
          return ListTile(
            title: Text(h.name ?? "", style: TextStyle(color: Colors.white)),
            subtitle: Text(
              h.address ?? "",
              style: TextStyle(color: Colors.white),
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
                  onPressed: () => deleteHotel(h.id!),
                ),
              ],
            ),
          );
        },
      ),
    );
  }
}
