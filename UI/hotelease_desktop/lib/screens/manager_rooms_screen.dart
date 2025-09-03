import 'package:flutter/material.dart';
import 'package:hotelease_mobile_new/models/hotel.dart';
import 'package:hotelease_mobile_new/models/room.dart';
import 'package:hotelease_mobile_new/models/room_type.dart';
import 'package:hotelease_mobile_new/providers/hotels_provider.dart';
import 'package:hotelease_mobile_new/providers/room_type_provider.dart';
import 'package:hotelease_mobile_new/providers/rooms.provider.dart';
import 'package:hotelease_mobile_new/screens/master_screen.dart';
import 'package:provider/provider.dart';

class ManagerRoomsScreen extends StatefulWidget {
  const ManagerRoomsScreen({super.key});

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
    try {
      var provider = context.read<RoomsProvider>();
      var data = await provider.getRooms();

      setState(() {
        rooms = data;
        isLoading = false;
      });
    } catch (e) {
      setState(() {
        isLoading = false;
      });
      ScaffoldMessenger.of(
        context,
      ).showSnackBar(SnackBar(content: Text("Error loading rooms: $e")));
    }
  }

  void _openRoomForm([Room? room]) {
    Navigator.push(
      context,
      MaterialPageRoute(builder: (_) => RoomFormScreen(room: room)),
    ).then((_) => _loadRooms());
  }

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
          ? const Center(child: Text("No rooms available"))
          : ListView.builder(
              itemCount: rooms.length,
              itemBuilder: (context, index) {
                final room = rooms[index];
                return Card(
                  color: const Color.fromRGBO(15, 41, 70, 1),
                  child: ListTile(
                    leading: const Icon(Icons.bed, color: Colors.white),
                    title: Text(
                      "Room #${room.id}",
                      style: TextStyle(color: Colors.white),
                    ),
                    subtitle: Text(
                      "Type: ${room.name} | Price: ${room.pricePerNight} €",
                      style: TextStyle(color: Colors.grey),
                    ),
                    trailing: IconButton(
                      icon: const Icon(Icons.edit, color: Colors.white),
                      onPressed: () => _openRoomForm(room),
                    ),
                  ),
                );
              },
            ),
      floatingActionButton: FloatingActionButton(
        onPressed: () => _openRoomForm(),
        child: const Icon(Icons.add),
      ),
    );
  }
}

class RoomFormScreen extends StatefulWidget {
  final Room? room;

  const RoomFormScreen({super.key, this.room});

  @override
  State<RoomFormScreen> createState() => _RoomFormScreenState();
}

class _RoomFormScreenState extends State<RoomFormScreen> {
  final _formKey = GlobalKey<FormState>();
  final _nameController = TextEditingController();
  final _capacityController = TextEditingController();
  final _priceController = TextEditingController();
  final _descController = TextEditingController();

  Hotel? _selectedHotel;
  RoomType? _selectedRoomType;
  bool _queenBed = false;
  bool _wifi = false;
  bool _cityView = false;
  bool _ac = false;

  List<Hotel> _hotels = [];
  List<RoomType> _roomTypes = [];

  @override
  void initState() {
    super.initState();
    _loadData();
    if (widget.room != null) {
      _nameController.text = widget.room!.name ?? '';
      _capacityController.text = widget.room!.capacity.toString();
      _priceController.text = widget.room!.pricePerNight.toString();
      _descController.text = widget.room!.description ?? '';
      _queenBed = widget.room!.queenBed ?? false;
      _wifi = widget.room!.wiFi ?? false;
      _cityView = widget.room!.cityView ?? false;
      _ac = widget.room!.ac ?? false;
    }
  }

  Future<void> _loadData() async {
    var hotelsResult = await context.read<HotelsProvider>().get();
    var roomTypesResult = await context.read<RoomTypesProvider>().get();

    setState(() {
      _hotels = hotelsResult.result;
      _roomTypes = roomTypesResult.result;

      if (widget.room != null) {
        _selectedHotel = _hotels.firstWhere(
          (h) => h.id == widget.room!.hotelId,
        );
        _selectedRoomType = _roomTypes.firstWhere(
          (rt) => rt.id == widget.room!.roomTypeId,
        );
      }
    });
  }

  Future<void> _saveRoom() async {
    if (_formKey.currentState?.validate() != true) return;
    if (_selectedHotel == null || _selectedRoomType == null) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text("Please select hotel and room type")),
      );
      return;
    }

    var provider = context.read<RoomsProvider>();
    var room = Room(
      id: widget.room?.id,
      hotelId: _selectedHotel!.id,
      roomTypeId: _selectedRoomType!.id,
      name: _nameController.text,
      capacity: int.tryParse(_capacityController.text) ?? 0,
      pricePerNight: double.tryParse(_priceController.text) ?? 0,
      description: _descController.text,
      queenBed: _queenBed,
      wiFi: _wifi,
      cityView: _cityView,
      ac: _ac,
    );

    if (widget.room == null) {
      await provider.insert(room);
    } else {
      await provider.addRoom(room);
    }

    Navigator.pop(context);
  }

  @override
  Widget build(BuildContext context) {
    return MasterScreen(
      child: _hotels.isEmpty || _roomTypes.isEmpty
          ? const Center(child: CircularProgressIndicator())
          : Padding(
              padding: const EdgeInsets.all(16.0),
              child: Form(
                key: _formKey,
                child: ListView(
                  children: [
                    DropdownButtonFormField<Hotel>(
                      dropdownColor: const Color.fromRGBO(17, 45, 78, 1),
                      value: _selectedHotel,
                      decoration: const InputDecoration(
                        labelText: "Hotel",
                        labelStyle: TextStyle(color: Colors.grey),
                      ),
                      items: _hotels
                          .map(
                            (h) => DropdownMenuItem(
                              value: h,
                              child: Text(
                                h.name ?? "Unnamed",
                                style: TextStyle(color: Colors.white),
                              ),
                            ),
                          )
                          .toList(),
                      onChanged: (val) => setState(() => _selectedHotel = val),
                      validator: (val) =>
                          val == null ? "Please select hotel" : null,
                    ),
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
                                style: TextStyle(color: Colors.white),
                              ),
                            ),
                          )
                          .toList(),
                      onChanged: (val) =>
                          setState(() => _selectedRoomType = val),
                      validator: (val) =>
                          val == null ? "Please select room type" : null,
                    ),
                    TextFormField(
                      style: TextStyle(color: Colors.white),
                      controller: _nameController,
                      decoration: const InputDecoration(
                        labelText: "Name",
                        labelStyle: TextStyle(color: Colors.grey),
                      ),
                      validator: (val) =>
                          val == null || val.isEmpty ? "Required" : null,
                    ),
                    TextFormField(
                      style: TextStyle(color: Colors.white),
                      controller: _capacityController,
                      decoration: const InputDecoration(
                        labelText: "Capacity",
                        labelStyle: TextStyle(color: Colors.grey),
                      ),
                      keyboardType: TextInputType.number,
                    ),
                    TextFormField(
                      style: TextStyle(color: Colors.white),
                      controller: _priceController,
                      decoration: const InputDecoration(
                        labelText: "Price per night",
                        labelStyle: TextStyle(color: Colors.grey),
                      ),
                      keyboardType: TextInputType.number,
                    ),
                    TextFormField(
                      style: TextStyle(color: Colors.white),
                      controller: _descController,
                      decoration: const InputDecoration(
                        labelText: "Description",
                        labelStyle: TextStyle(color: Colors.grey),
                      ),
                      maxLines: 3,
                    ),
                    SwitchListTile(
                      title: const Text(
                        "Queen Bed",
                        style: TextStyle(color: Colors.white),
                      ),
                      value: _queenBed,
                      onChanged: (v) => setState(() => _queenBed = v),
                    ),
                    SwitchListTile(
                      title: const Text(
                        "WiFi",
                        style: TextStyle(color: Colors.white),
                      ),
                      value: _wifi,
                      onChanged: (v) => setState(() => _wifi = v),
                    ),
                    SwitchListTile(
                      title: const Text(
                        "City View",
                        style: TextStyle(color: Colors.white),
                      ),
                      value: _cityView,
                      onChanged: (v) => setState(() => _cityView = v),
                    ),
                    SwitchListTile(
                      title: const Text(
                        "Air Conditioning",
                        style: TextStyle(color: Colors.white),
                      ),
                      value: _ac,
                      onChanged: (v) => setState(() => _ac = v),
                    ),
                    ElevatedButton(
                      onPressed: _saveRoom,
                      child: Text(widget.room == null ? "Add Room" : "Save"),
                    ),
                  ],
                ),
              ),
            ),
    );
  }
}
