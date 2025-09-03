import 'package:flutter/material.dart';
import 'package:hotelease_mobile_new/screens/hotels_manage_screen.dart';
import 'package:hotelease_mobile_new/screens/master_screen.dart';
import 'package:hotelease_mobile_new/screens/room_types_screen.dart';
import 'package:hotelease_mobile_new/screens/users_manage_screen.dart';

class AdminDashboardScreen extends StatelessWidget {
  const AdminDashboardScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return MasterScreen(
      title: "Admin",
      child: ListView(
        children: [
          ListTile(
            leading: const Icon(Icons.people, color: Colors.green),
            title: const Text(
              "Manage Users",
              style: TextStyle(color: Colors.white),
            ),
            onTap: () {
              Navigator.push(
                context,
                MaterialPageRoute(builder: (_) => const UsersManageScreen()),
              );
            },
          ),
          ListTile(
            leading: const Icon(Icons.hotel, color: Colors.blue),
            title: const Text(
              "Manage Hotels",
              style: TextStyle(color: Colors.white),
            ),
            onTap: () {
              Navigator.push(
                context,
                MaterialPageRoute(builder: (_) => const HotelsManageScreen()),
              );
            },
          ),
          ListTile(
            leading: const Icon(Icons.bed, color: Colors.orange),
            title: const Text(
              "Manage Room Types",
              style: TextStyle(color: Colors.white),
            ),
            onTap: () {
              Navigator.push(
                context,
                MaterialPageRoute(builder: (_) => const RoomTypesScreen()),
              );
            },
          ),
        ],
      ),
    );
  }
}
