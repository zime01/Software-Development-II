import 'package:flutter/material.dart';
import 'package:hotelease_mobile_new/providers/room_type_provider.dart';
import 'package:hotelease_mobile_new/models/room_type.dart';
import 'package:hotelease_mobile_new/screens/master_screen.dart';

class RoomTypesScreen extends StatefulWidget {
  const RoomTypesScreen({super.key});

  @override
  State<RoomTypesScreen> createState() => _RoomTypesScreenState();
}

class _RoomTypesScreenState extends State<RoomTypesScreen> {
  final RoomTypesProvider _provider = RoomTypesProvider();
  List<RoomType> roomTypes = [];
  final TextEditingController _nameController = TextEditingController();

  @override
  void initState() {
    super.initState();
    loadRoomTypes();
  }

  Future<void> loadRoomTypes() async {
    var result = await _provider.get();
    setState(() {
      roomTypes = result.result;
    });
  }

  Future<void> addRoomType() async {
    if (_nameController.text.isEmpty) return;

    await _provider.insertRoomType({"name": _nameController.text});
    _nameController.clear();
    loadRoomTypes();
  }

  @override
  Widget build(BuildContext context) {
    return MasterScreen(
      child: Column(
        children: [
          Padding(
            padding: const EdgeInsets.all(12),
            child: Row(
              children: [
                Expanded(
                  child: TextField(
                    style: TextStyle(color: Colors.white),
                    controller: _nameController,
                    decoration: const InputDecoration(
                      labelText: "Room Type Name",
                      labelStyle: TextStyle(color: Colors.white),
                      border: OutlineInputBorder(),
                    ),
                  ),
                ),
                const SizedBox(width: 10),
                ElevatedButton(
                  onPressed: addRoomType,
                  child: const Text("Add"),
                ),
              ],
            ),
          ),
          Expanded(
            child: ListView.builder(
              itemCount: roomTypes.length,
              itemBuilder: (context, index) {
                final rt = roomTypes[index];
                return ListTile(
                  title: Text(rt.name!, style: TextStyle(color: Colors.white)),
                );
              },
            ),
          ),
        ],
      ),
    );
  }
}
