import 'package:flutter/material.dart';
import 'package:hotelease_mobile_new/screens/manager_room_availability_screen.dart';
import 'package:hotelease_mobile_new/screens/manager_rooms_screen.dart';
import 'package:hotelease_mobile_new/screens/manager_room_types_screen.dart';
import 'package:hotelease_mobile_new/screens/manager_reservations_screen.dart';
import 'package:hotelease_mobile_new/screens/master_screen.dart';

class ManagerDashboardScreen extends StatelessWidget {
  const ManagerDashboardScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return MasterScreen(
      title: "Manager Dashboard",
      child: ListView(
        children: [
          // ListTile(
          //   leading: const Icon(Icons.meeting_room, color: Colors.white),
          //   title: const Text(
          //     "Manage Rooms",
          //     style: TextStyle(color: Colors.white),
          //   ),
          //   onTap: () => Navigator.push(
          //     context,
          //     MaterialPageRoute(
          //       builder: (_) => const ManagerRoomsScreen(hotelId: null ?? 0),
          //     ),
          //   ),
          // ),
          ListTile(
            leading: const Icon(Icons.category, color: Colors.white),
            title: const Text(
              "Manage Room Types",
              style: TextStyle(color: Colors.white),
            ),
            onTap: () => Navigator.push(
              context,
              MaterialPageRoute(builder: (_) => const ManagerRoomTypesScreen()),
            ),
          ),
          ListTile(
            leading: const Icon(Icons.event, color: Colors.white),
            title: const Text(
              "Room Availability",
              style: TextStyle(color: Colors.white),
            ),
            onTap: () => Navigator.push(
              context,
              MaterialPageRoute(builder: (_) => const RoomAvailabilityScreen()),
            ),
          ),
          ListTile(
            leading: const Icon(Icons.book_online, color: Colors.white),
            title: const Text(
              "Reservations",
              style: TextStyle(color: Colors.white),
            ),
            onTap: () => Navigator.push(
              context,
              MaterialPageRoute(
                builder: (_) => const ManagerReservationsScreen(),
              ),
            ),
          ),
        ],
      ),
    );
  }
}
