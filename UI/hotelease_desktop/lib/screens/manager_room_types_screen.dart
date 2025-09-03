import 'package:flutter/material.dart';
import 'package:hotelease_mobile_new/providers/room_type_provider.dart';
import 'package:provider/provider.dart';
import 'package:hotelease_mobile_new/models/room_type.dart';

class ManagerRoomTypesScreen extends StatefulWidget {
  const ManagerRoomTypesScreen({super.key});

  @override
  State<ManagerRoomTypesScreen> createState() => _ManagerRoomTypesScreenState();
}

class _ManagerRoomTypesScreenState extends State<ManagerRoomTypesScreen> {
  List<RoomType> roomTypes = [];
  bool isLoading = true;

  @override
  void initState() {
    super.initState();
    _loadRoomTypes();
  }

  Future<void> _loadRoomTypes() async {
    var provider = context.read<RoomTypesProvider>();
    var data = await provider.get(); // vraća SearchResult
    setState(() {
      roomTypes = data.result;
      isLoading = false;
    });
  }

  void _openForm({RoomType? roomType}) {
    final nameController = TextEditingController(text: roomType?.name ?? "");
    final descController = TextEditingController(
      text: roomType?.description ?? "",
    );

    showDialog(
      context: context,
      builder: (_) => AlertDialog(
        title: Text(roomType == null ? "Add Room Type" : "Edit Room Type"),
        content: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            TextField(
              controller: nameController,
              decoration: const InputDecoration(labelText: "Name"),
            ),
            TextField(
              controller: descController,
              decoration: const InputDecoration(labelText: "Description"),
            ),
          ],
        ),
        actions: [
          TextButton(
            onPressed: () => Navigator.pop(context),
            child: const Text("Cancel"),
          ),
          ElevatedButton(
            onPressed: () async {
              var provider = context.read<RoomTypesProvider>();
              var payload = {
                "name": nameController.text,
                "description": descController.text,
              };

              if (roomType == null) {
                await provider.insertRoomType(payload);
              } else {
                await provider.updateRoomType(roomType.id!, payload);
              }

              Navigator.pop(context);
              _loadRoomTypes();
            },
            child: const Text("Save"),
          ),
        ],
      ),
    );
  }

  Future<void> _deleteRoomType(int id) async {
    var provider = context.read<RoomTypesProvider>();
    await provider.deleteRoomType(id);
    _loadRoomTypes();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: const Color.fromRGBO(17, 45, 78, 1),
      appBar: AppBar(
        foregroundColor: Colors.white,
        title: const Text(
          "Manage Room Types",
          style: TextStyle(color: Colors.white),
        ),
        backgroundColor: const Color.fromRGBO(17, 45, 78, 1),
      ),
      body: isLoading
          ? const Center(child: CircularProgressIndicator())
          : ListView.builder(
              itemCount: roomTypes.length,
              itemBuilder: (context, index) {
                final rt = roomTypes[index];
                return ListTile(
                  leading: const Icon(Icons.category, color: Colors.white),
                  title: Text(
                    rt.name ?? "",
                    style: TextStyle(color: Colors.white),
                  ),
                  subtitle: Text(
                    rt.description ?? "",
                    style: TextStyle(color: Colors.grey),
                  ),
                  trailing: Row(
                    mainAxisSize: MainAxisSize.min,
                    children: [
                      IconButton(
                        icon: const Icon(Icons.edit, color: Colors.white),
                        onPressed: () => _openForm(roomType: rt),
                      ),
                      IconButton(
                        icon: const Icon(Icons.delete, color: Colors.red),
                        onPressed: () => _deleteRoomType(rt.id!),
                      ),
                    ],
                  ),
                );
              },
            ),
      floatingActionButton: FloatingActionButton(
        onPressed: () => _openForm(),
        child: const Icon(Icons.add),
      ),
    );
  }
}
