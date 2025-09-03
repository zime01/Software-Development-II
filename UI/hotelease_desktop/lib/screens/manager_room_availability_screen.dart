import 'package:flutter/material.dart';
import 'package:hotelease_mobile_new/models/room.dart';
import 'package:hotelease_mobile_new/models/room_availability.dart';
import 'package:hotelease_mobile_new/providers/room_availability.dart';
import 'package:hotelease_mobile_new/providers/rooms.provider.dart';
import 'package:hotelease_mobile_new/screens/master_screen.dart';
import 'package:provider/provider.dart';

class RoomAvailabilityScreen extends StatefulWidget {
  const RoomAvailabilityScreen({super.key});

  @override
  State<RoomAvailabilityScreen> createState() => _RoomAvailabilityScreenState();
}

class _RoomAvailabilityScreenState extends State<RoomAvailabilityScreen> {
  Room? _selectedRoom;
  DateTime _selectedMonth = DateTime.now();

  List<Room> _rooms = [];
  List<RoomAvailability> _availability = [];
  bool _isLoading = false;

  @override
  void initState() {
    super.initState();
    _loadRooms();
  }

  Future<void> _loadRooms() async {
    var provider = context.read<RoomsProvider>();
    var rooms = await provider.getRooms();
    setState(() {
      _rooms = rooms;
      if (rooms.isNotEmpty) {
        _selectedRoom = rooms.first;
      }
    });
    if (_selectedRoom != null) {
      _loadAvailability();
    }
  }

  Future<void> _loadAvailability() async {
    if (_selectedRoom == null) return;

    setState(() => _isLoading = true);

    var provider = context.read<RoomsAvailabilityProvider>();
    var data = await provider.getAvailabilityRooms(
      _selectedRoom!.id!,
      _selectedMonth.month,
      _selectedMonth.year,
    );

    setState(() {
      _availability = data;
      _isLoading = false;
    });
  }

  Color _statusColor(int status) {
    switch (status) {
      case 1:
        return Colors.red; // booked
      case 2:
        return Colors.orange; // limited
      default:
        return Colors.green; // available
    }
  }

  @override
  Widget build(BuildContext context) {
    return MasterScreen(
      title: "Room Availability",
      child: Padding(
        padding: const EdgeInsets.all(16),
        child: Column(
          children: [
            DropdownButton<Room>(
              dropdownColor: const Color.fromRGBO(17, 45, 78, 1),
              value: _selectedRoom,
              items: _rooms
                  .map(
                    (r) => DropdownMenuItem(
                      value: r,
                      child: Text(
                        r.name ?? "Room #${r.id}",
                        style: TextStyle(color: Colors.white),
                      ),
                    ),
                  )
                  .toList(),
              onChanged: (room) {
                setState(() => _selectedRoom = room);
                _loadAvailability();
              },
            ),
            const SizedBox(height: 12),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                Text(
                  "${_selectedMonth.month}/${_selectedMonth.year}",
                  style: const TextStyle(fontSize: 18, color: Colors.white),
                ),
                IconButton(
                  icon: const Icon(Icons.calendar_month, color: Colors.white),
                  onPressed: () async {
                    final picked = await showDatePicker(
                      context: context,
                      initialDate: DateTime(
                        _selectedMonth.year,
                        _selectedMonth.month,
                        1,
                      ),
                      firstDate: DateTime(2020),
                      lastDate: DateTime(2030),
                      selectableDayPredicate: (d) => d.day == 1,
                    );
                    if (picked != null) {
                      setState(() => _selectedMonth = picked);
                      _loadAvailability();
                    }
                  },
                ),
              ],
            ),
            const SizedBox(height: 20),
            _isLoading
                ? const Center(child: CircularProgressIndicator())
                : Expanded(
                    child: GridView.builder(
                      gridDelegate:
                          const SliverGridDelegateWithFixedCrossAxisCount(
                            crossAxisCount: 7,
                            crossAxisSpacing: 4,
                            mainAxisSpacing: 4,
                          ),
                      itemCount: _availability.length,
                      itemBuilder: (context, index) {
                        final day = _availability[index];
                        return Container(
                          decoration: BoxDecoration(
                            color: _statusColor(day.status!),
                            borderRadius: BorderRadius.circular(6),
                          ),
                          child: Center(
                            child: Text(
                              "${day.date?.day}",
                              style: const TextStyle(color: Colors.white),
                            ),
                          ),
                        );
                      },
                    ),
                  ),
          ],
        ),
      ),
    );
  }
}
