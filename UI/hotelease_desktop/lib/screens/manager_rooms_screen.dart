import 'dart:convert';
import 'dart:io';
import 'dart:typed_data';

import 'package:flutter/material.dart';
import 'package:hotelease_mobile_new/models/asset.dart';
import 'package:hotelease_mobile_new/models/room.dart';
import 'package:hotelease_mobile_new/models/room_type.dart';
import 'package:hotelease_mobile_new/providers/assets_provider.dart';
import 'package:hotelease_mobile_new/providers/room_type_provider.dart';
import 'package:hotelease_mobile_new/providers/rooms.provider.dart';
import 'package:hotelease_mobile_new/screens/master_screen.dart';
import 'package:image_picker/image_picker.dart';
import 'package:provider/provider.dart';

class ManagerRoomsScreen extends StatefulWidget {
  final int hotelId;
  const ManagerRoomsScreen({super.key, required this.hotelId});

  @override
  State<ManagerRoomsScreen> createState() => _ManagerRoomsScreenState();
}

class _ManagerRoomsScreenState extends State<ManagerRoomsScreen> {
  List<Room> rooms = [];
  bool isLoading = true;

  @override
  void initState() {
    super.initState();
    _loadRooms();
  }

  Future<void> _loadRooms() async {
    setState(() => isLoading = true);
    try {
      var provider = context.read<RoomsProvider>();
      var data = await provider.getRoomsByHotelId(widget.hotelId);
      setState(() {
        rooms = data;
        isLoading = false;
      });
      print("Rooms loaded: ${rooms.length}");
    } catch (e, st) {
      setState(() => isLoading = false);
      print("Error loading rooms: $e\n$st");
      ScaffoldMessenger.of(
        context,
      ).showSnackBar(SnackBar(content: Text("Error loading rooms: $e")));
    }
  }

  void _openRoomForm([Room? room]) {
    Navigator.push(
      context,
      MaterialPageRoute(
        builder: (_) => RoomFormScreen(room: room, hotelId: widget.hotelId),
      ),
    ).then((_) => _loadRooms());
  }

  Future<void> _deleteRoom(int roomId) async {
    try {
      await context.read<RoomsProvider>().delete(roomId);
      print("Deleted room id: $roomId");
      _loadRooms();
    } catch (e, st) {
      print("Error deleting room: $e\n$st");
      ScaffoldMessenger.of(
        context,
      ).showSnackBar(SnackBar(content: Text("Error deleting room: $e")));
    }
  }

  Uint8List? tryDecodeBase64(String? base64Str) {
    if (base64Str == null || base64Str.isEmpty) return null;
    try {
      if (base64Str.startsWith('data:image')) {
        base64Str = base64Str.split(',').last;
      }
      return base64Decode(base64Str);
    } catch (e, st) {
      print("Error decoding Base64 image: $e\n$st");
      return null;
    }
  }

  bool _roomHasImage(Room room) =>
      room.assets != null &&
      room.assets!.isNotEmpty &&
      room.assets!.first.image != null &&
      room.assets!.first.image!.isNotEmpty;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: const Color.fromRGBO(17, 45, 78, 1),
      appBar: AppBar(
        foregroundColor: Colors.white,
        title: const Text(
          "Manage Rooms",
          style: TextStyle(color: Colors.white),
        ),
        backgroundColor: const Color.fromRGBO(17, 45, 78, 1),
      ),
      body: isLoading
          ? const Center(child: CircularProgressIndicator())
          : rooms.isEmpty
          ? const Center(
              child: Text(
                "No rooms available",
                style: TextStyle(color: Colors.white),
              ),
            )
          : ListView.builder(
              itemCount: rooms.length,
              itemBuilder: (context, index) {
                final room = rooms[index];
                return Card(
                  color: const Color.fromRGBO(15, 41, 70, 1),
                  child: ListTile(
                    leading: _roomHasImage(room)
                        ? Builder(
                            builder: (_) {
                              final bytes = tryDecodeBase64(
                                room.assets!.first.image,
                              );
                              if (bytes == null) {
                                print("Invalid image for room id ${room.id}");
                                return const Icon(
                                  Icons.bed,
                                  color: Colors.white,
                                );
                              }
                              return Image.memory(
                                bytes,
                                width: 50,
                                height: 50,
                                fit: BoxFit.cover,
                              );
                            },
                          )
                        : const Icon(Icons.bed, color: Colors.white),
                    title: Text(
                      room.name ?? "Room #${room.id}",
                      style: const TextStyle(color: Colors.white),
                    ),
                    subtitle: Text(
                      "Capacity: ${room.capacity ?? '-'} | Price: ${room.pricePerNight ?? '-'} €",
                      style: const TextStyle(color: Colors.grey),
                    ),
                    trailing: Row(
                      mainAxisSize: MainAxisSize.min,
                      children: [
                        IconButton(
                          icon: const Icon(Icons.edit, color: Colors.white),
                          onPressed: () => _openRoomForm(room),
                        ),
                        IconButton(
                          icon: const Icon(Icons.delete, color: Colors.red),
                          onPressed: () => _deleteRoom(room.id!),
                        ),
                      ],
                    ),
                  ),
                );
              },
            ),
      floatingActionButton: Padding(
        padding: const EdgeInsets.only(right: 50.0),
        child: FloatingActionButton(
          onPressed: () => _openRoomForm(),
          child: const Icon(Icons.add),
        ),
      ),
    );
  }
}

// -------- Room Form Screen --------
class RoomFormScreen extends StatefulWidget {
  final Room? room;
  final int hotelId;
  const RoomFormScreen({super.key, this.room, required this.hotelId});

  @override
  State<RoomFormScreen> createState() => _RoomFormScreenState();
}

class _RoomFormScreenState extends State<RoomFormScreen> {
  final _formKey = GlobalKey<FormState>();
  final _nameController = TextEditingController();
  final _capacityController = TextEditingController();
  final _priceController = TextEditingController();
  final _descController = TextEditingController();

  RoomType? _selectedRoomType;
  bool _queenBed = false, _wifi = false, _cityView = false, _ac = false;

  List<RoomType> _roomTypes = [];
  List<Asset> _images = []; // postojeće slike
  List<File> _newImages = []; // nove slike
  List<int> _imagesToDelete = []; // id slike koje user briše

  bool _isSaving = false;
  final ImagePicker _picker = ImagePicker();

  @override
  void initState() {
    super.initState();
    _loadRoomTypes();
    _initRoomValues();
  }

  void _initRoomValues() {
    if (widget.room != null) {
      _nameController.text = widget.room!.name ?? '';
      _capacityController.text = widget.room!.capacity?.toString() ?? '';
      _priceController.text = widget.room!.pricePerNight?.toString() ?? '';
      _descController.text = widget.room!.description ?? '';
      _queenBed = widget.room!.queenBed ?? false;
      _wifi = widget.room!.wiFi ?? false;
      _cityView = widget.room!.cityView ?? false;
      _images = widget.room!.assets ?? [];
    }
  }

  Future<void> _loadRoomTypes() async {
    try {
      var result = await context.read<RoomTypesProvider>().get();
      setState(() {
        _roomTypes = result.result;
        if (_roomTypes.isNotEmpty) {
          _selectedRoomType = widget.room != null
              ? _roomTypes.firstWhere(
                  (rt) => rt.id == widget.room!.roomTypeId,
                  orElse: () => _roomTypes.first,
                )
              : _roomTypes.first;
        }
      });
    } catch (e) {
      print("Error loading room types: $e");
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text("Failed to load room types")),
      );
    }
  }

  Future<void> _pickImage() async {
    try {
      final pickedFile = await _picker.pickImage(source: ImageSource.gallery);
      if (pickedFile != null) {
        final bytes = File(pickedFile.path).readAsBytesSync();
        setState(() {
          _newImages.add(File(pickedFile.path));
          _images.add(
            Asset(image: base64Encode(bytes), fileName: pickedFile.name),
          );
        });
      }
    } catch (e) {
      print("Error picking image: $e");
      ScaffoldMessenger.of(
        context,
      ).showSnackBar(const SnackBar(content: Text("Failed to pick image")));
    }
  }

  Future<void> _saveRoom() async {
    if (!_formKey.currentState!.validate()) return;
    if (_selectedRoomType == null) {
      ScaffoldMessenger.of(
        context,
      ).showSnackBar(const SnackBar(content: Text("Select a room type")));
      return;
    }

    setState(() => _isSaving = true);

    final roomsProvider = context.read<RoomsProvider>();
    final assetsProvider = context.read<AssetsProvider>();

    try {
      Room roomData = Room(
        id: widget.room?.id,
        hotelId: widget.hotelId,
        roomTypeId: _selectedRoomType!.id,
        name: _nameController.text.trim(),
        capacity: int.tryParse(_capacityController.text) ?? 1,
        pricePerNight: double.tryParse(_priceController.text) ?? 0,
        description: _descController.text.trim(),
        queenBed: _queenBed,
        wiFi: _wifi,
        cityView: _cityView,
      );

      Room savedRoom;
      if (widget.room == null) {
        savedRoom = await roomsProvider.addRoom(roomData);
      } else {
        savedRoom = await roomsProvider.editRoom(roomData);
      }

      // --- BRISANJE SLIKA ---
      if (_imagesToDelete.isNotEmpty) {
        await Future.wait(
          _imagesToDelete.map((id) => assetsProvider.deleteAsset(id)),
        );
      }

      // --- DODAVANJE NOVIH SLIKA ---
      if (_newImages.isNotEmpty) {
        final List<Map<String, dynamic>> assetsData = _newImages.map((file) {
          final bytes = file.readAsBytesSync();
          return {
            "RoomId": savedRoom.id,
            "HotelId": widget.hotelId,
            "FileName": file.path.split('/').last,
            "Image": base64Encode(bytes),
            "MimeType": "image/jpeg",
          };
        }).toList();

        await assetsProvider.insertAssets(assetsData);
      }

      Navigator.pop(context, savedRoom);
    } catch (e) {
      print("Error saving room: $e");
      ScaffoldMessenger.of(
        context,
      ).showSnackBar(SnackBar(content: Text("Error saving room: $e")));
    } finally {
      setState(() => _isSaving = false);
    }
  }

  @override
  Widget build(BuildContext context) {
    return MasterScreen(
      child: _roomTypes.isEmpty
          ? const Center(child: CircularProgressIndicator())
          : Padding(
              padding: const EdgeInsets.all(16),
              child: Form(
                key: _formKey,
                child: ListView(
                  children: [
                    DropdownButtonFormField<RoomType>(
                      dropdownColor: const Color.fromRGBO(17, 45, 78, 1),
                      value: _selectedRoomType,
                      decoration: const InputDecoration(
                        labelText: "Room Type",
                        labelStyle: TextStyle(color: Colors.grey),
                      ),
                      items: _roomTypes
                          .map(
                            (rt) => DropdownMenuItem(
                              value: rt,
                              child: Text(
                                rt.name ?? "Unnamed",
                                style: const TextStyle(color: Colors.white),
                              ),
                            ),
                          )
                          .toList(),
                      onChanged: (val) =>
                          setState(() => _selectedRoomType = val),
                    ),
                    const SizedBox(height: 10),
                    _buildTextField(_nameController, "Name"),
                    const SizedBox(height: 10),
                    _buildTextField(
                      _capacityController,
                      "Capacity",
                      isNumber: true,
                    ),
                    const SizedBox(height: 10),
                    _buildTextField(
                      _priceController,
                      "Price per night",
                      isNumber: true,
                    ),
                    const SizedBox(height: 10),
                    _buildTextField(
                      _descController,
                      "Description",
                      maxLines: 3,
                    ),
                    const SizedBox(height: 10),
                    _buildSwitch(
                      "Queen Bed",
                      _queenBed,
                      (v) => setState(() => _queenBed = v),
                    ),
                    _buildSwitch(
                      "WiFi",
                      _wifi,
                      (v) => setState(() => _wifi = v),
                    ),
                    _buildSwitch(
                      "City View",
                      _cityView,
                      (v) => setState(() => _cityView = v),
                    ),

                    const SizedBox(height: 10),
                    _buildImagesPicker(),
                    const SizedBox(height: 20),
                    ElevatedButton(
                      onPressed: _isSaving ? null : _saveRoom,
                      child: _isSaving
                          ? const CircularProgressIndicator(color: Colors.white)
                          : Text(
                              widget.room == null ? "Add Room" : "Save Room",
                            ),
                    ),
                  ],
                ),
              ),
            ),
    );
  }

  TextFormField _buildTextField(
    TextEditingController ctrl,
    String label, {
    bool isNumber = false,
    int maxLines = 1,
  }) {
    return TextFormField(
      controller: ctrl,
      style: const TextStyle(color: Colors.white),
      keyboardType: isNumber ? TextInputType.number : TextInputType.text,
      maxLines: maxLines,
      decoration: InputDecoration(
        labelText: label,
        labelStyle: const TextStyle(color: Colors.grey),
      ),
      validator: (val) {
        if (val == null || val.isEmpty) return "Required";
        if (isNumber && double.tryParse(val) == null) return "Invalid number";
        return null;
      },
    );
  }

  SwitchListTile _buildSwitch(
    String label,
    bool value,
    ValueChanged<bool> onChanged,
  ) {
    return SwitchListTile(
      title: Text(label, style: const TextStyle(color: Colors.white)),
      value: value,
      onChanged: onChanged,
    );
  }

  Widget _buildImagesPicker() {
    return Wrap(
      spacing: 8,
      runSpacing: 8,
      children: [
        ..._images.asMap().entries.map((entry) {
          final img = entry.value;
          final index = entry.key;

          Widget imageWidget;

          try {
            final imageStr = img.image;

            if (imageStr == null || imageStr.isEmpty) {
              imageWidget = Container(
                width: 80,
                height: 80,
                color: Colors.grey,
              );
            } else {
              // 1) Decode base64
              final decoded = base64.decode(imageStr);

              // 2) Probaj pretvoriti u UTF-8 (možda je URL u base64)
              final asString = utf8.decode(decoded, allowMalformed: true);

              // 3) Ako počinje sa http → prikaži kao URL
              if (asString.startsWith("http://") ||
                  asString.startsWith("https://")) {
                imageWidget = Image.network(
                  asString,
                  width: 80,
                  height: 80,
                  fit: BoxFit.cover,
                );
              } else {
                // 4) Inače je raw slika
                imageWidget = Image.memory(
                  decoded,
                  width: 80,
                  height: 80,
                  fit: BoxFit.cover,
                );
              }
            }
          } catch (e) {
            imageWidget = Container(width: 80, height: 80, color: Colors.grey);
          }

          return Stack(
            children: [
              GestureDetector(child: imageWidget),
              Positioned(
                right: 0,
                top: 0,
                child: GestureDetector(
                  onTap: () {
                    setState(() {
                      if (img.id != null) _imagesToDelete.add(img.id!);
                      _newImages.removeWhere(
                        (f) => f.path.split('/').last == img.fileName,
                      );
                      _images.removeAt(index);
                    });
                  },
                  child: const Icon(Icons.close, color: Colors.red),
                ),
              ),
            ],
          );
        }),

        // Add new image button
        GestureDetector(
          onTap: _pickImage,
          child: Container(
            width: 80,
            height: 80,
            color: Colors.grey[700],
            child: const Icon(Icons.add, color: Colors.white),
          ),
        ),
      ],
    );
  }
}
