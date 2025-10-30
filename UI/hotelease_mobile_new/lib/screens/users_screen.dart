import 'dart:convert';
import 'dart:math';

import 'package:flutter/material.dart';
import 'package:hotelease_mobile_new/models/user.dart';
import 'package:hotelease_mobile_new/models/reservation.dart';
import 'package:hotelease_mobile_new/providers/rooms.provider.dart';
import 'package:hotelease_mobile_new/providers/users_provider.dart';
import 'package:hotelease_mobile_new/providers/reservations_provider.dart';
import 'package:intl/intl.dart';

class UsersScreen extends StatefulWidget {
  const UsersScreen({super.key});

  @override
  State<UsersScreen> createState() => _UsersScreenState();
}

class _UsersScreenState extends State<UsersScreen>
    with SingleTickerProviderStateMixin {
  final RoomsProvider _roomsProvider = RoomsProvider();
  late TabController _tabController;
  User? _currentUser;
  List<Reservation> _reservations = [];

  final _formKey = GlobalKey<FormState>();
  late TextEditingController _firstName;
  late TextEditingController _lastName;
  late TextEditingController _phone;

  final _oldPass = TextEditingController();
  final _newPass = TextEditingController();
  final _confirmPass = TextEditingController();

  String _formatDate(DateTime? date) {
    if (date == null) return "-";
    return DateFormat('dd.MM.yyyy – HH:mm').format(date);
  }

  @override
  void initState() {
    super.initState();
    _tabController = TabController(length: 3, vsync: this);
    _loadUser();
    _loadReservations();
  }

  Future<void> _loadUser() async {
    try {
      var user = await UsersProvider().getCurrentUser();
      setState(() {
        _currentUser = user;
        _firstName = TextEditingController(text: user.firstName ?? "");
        _lastName = TextEditingController(text: user.lastName ?? "");
        _phone = TextEditingController(text: user.phoneNumber ?? "");
      });
    } catch (e, s) {
      debugPrint("Error while loading user: $e");
      debugPrint("$s");
    }
  }

  Future<void> _loadReservations() async {
    try {
      var resList = await ReservationsProvider().getMyReservations();

      await Future.wait(
        resList.map((res) async {
          if (res.roomId != null) {
            res.room = await _roomsProvider.getRoomById(res.roomId!);
          }
        }),
      );

      setState(() {
        _reservations = resList;
      });
    } catch (e, s) {
      debugPrint("Error with reservation $e");
      debugPrint("$s");
    }
  }

  Future<void> _updateProfile() async {
    if (_formKey.currentState!.validate()) {
      await UsersProvider().update(_currentUser!.id!, {
        "firstName": _firstName.text,
        "lastName": _lastName.text,
        "phoneNumber": _phone.text,
      });
      ScaffoldMessenger.of(
        context,
      ).showSnackBar(const SnackBar(content: Text("Profile updated ✅")));
    }
  }

  Future<void> _cancelReservation(int reservationId) async {
    try {
      await ReservationsProvider().cancelReservation(reservationId);
      ScaffoldMessenger.of(
        context,
      ).showSnackBar(const SnackBar(content: Text("Reservation canceled ✅")));
      _loadReservations(); // 🔹 refresh odmah
    } catch (e) {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text("Error canceling reservation: $e")),
      );
    }
  }

  void _showReservationDetails(Reservation res) {
    debugPrint("=== RESERVATION DEBUG INFO ===");
    debugPrint("Room: ${res.room?.toJson()}");
    debugPrint("Assets count: ${res.room?.assets?.length ?? 0}");

    if (res.room?.assets?.isNotEmpty == true) {
      final asset = res.room!.assets!.first;
      debugPrint("First asset: ${asset.toJson()}");

      final imageData = asset.image!;
      debugPrint("Image data length: ${imageData.length}");
      debugPrint(
        "Image data first 50 chars: ${imageData.substring(0, min(50, imageData.length))}",
      );

      try {
        final decodedUrl = utf8.decode(base64.decode(imageData));
        debugPrint("Decoded URL: $decodedUrl");
        debugPrint("Decoded URL length: ${decodedUrl.length}");

        // Probaj s imageThumb ako image ne radi
        if (asset.imageThumb != null) {
          final decodedThumbUrl = utf8.decode(base64.decode(asset.imageThumb!));
          debugPrint("Thumb URL: $decodedThumbUrl");
        }
      } catch (e) {
        debugPrint("Base64 decode error: $e");
      }
    }

    debugPrint("=== END DEBUG INFO ===");
    showModalBottomSheet(
      context: context,
      isScrollControlled: true,
      backgroundColor: Theme.of(context).colorScheme.secondary,
      shape: const RoundedRectangleBorder(
        borderRadius: BorderRadius.vertical(top: Radius.circular(20)),
      ),
      builder: (context) => DraggableScrollableSheet(
        expand: false,
        initialChildSize: 0.8,
        minChildSize: 0.5,
        maxChildSize: 0.95,
        builder: (_, controller) => SingleChildScrollView(
          controller: controller,
          child: Padding(
            padding: const EdgeInsets.all(16),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                const SizedBox(height: 16),
                Center(
                  child: Text(
                    res.room?.hotelName ?? "Hotel",
                    style: TextStyle(
                      color: Theme.of(context).colorScheme.onPrimary,
                      fontSize: 24,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                ),
                const SizedBox(height: 8),
                Center(
                  child: Text(
                    res.room?.name ?? "-",
                    style: TextStyle(
                      color: Theme.of(context).colorScheme.onPrimary,
                      fontSize: 18,
                    ),
                  ),
                ),
                Divider(
                  color: Theme.of(context).colorScheme.onPrimary,
                  height: 30,
                ),

                // 🔹 Informacije o rezervaciji
                Text(
                  "Reservation Details",
                  style: TextStyle(
                    color: Theme.of(context).colorScheme.onPrimary,
                    fontSize: 18,
                    fontWeight: FontWeight.bold,
                  ),
                ),
                const SizedBox(height: 8),
                _detailRow(
                  Icons.calendar_today,
                  "Check-in:",
                  _formatDate(res.checkInDate),
                ),
                _detailRow(
                  Icons.calendar_today_outlined,
                  "Check-out:",
                  _formatDate(res.checkOutDate),
                ),

                _detailRow(
                  Icons.confirmation_number,
                  "Status:",
                  res.status ?? "-",
                ),

                Divider(
                  color: Theme.of(context).colorScheme.onPrimary,
                  height: 30,
                ),

                // 🔹 Informacije o plaćanju
                Text(
                  "Payment Info",
                  style: TextStyle(
                    color: Theme.of(context).colorScheme.onPrimary,
                    fontSize: 18,
                    fontWeight: FontWeight.bold,
                  ),
                ),
                const SizedBox(height: 8),
                _detailRow(
                  Icons.attach_money,
                  "Price:",
                  "${res.totalPrice?.toStringAsFixed(2) ?? '-'} €",
                ),

                const SizedBox(height: 25),
                if (res.status?.toLowerCase() != "canceled")
                  Center(
                    child: ElevatedButton.icon(
                      onPressed: () {
                        Navigator.pop(context);
                        _cancelReservation(res.id!);
                      },
                      icon: Icon(
                        Icons.cancel,
                        color: Theme.of(context).colorScheme.onPrimary,
                      ),
                      label: const Text("Cancel Reservation"),
                      style: ElevatedButton.styleFrom(
                        backgroundColor: Colors.redAccent,
                        padding: const EdgeInsets.symmetric(
                          horizontal: 24,
                          vertical: 12,
                        ),
                        shape: RoundedRectangleBorder(
                          borderRadius: BorderRadius.circular(25),
                        ),
                      ),
                    ),
                  ),
              ],
            ),
          ),
        ),
      ),
    );
  }

  Widget _detailRow(IconData icon, String label, String value) {
    return Padding(
      padding: const EdgeInsets.symmetric(vertical: 6),
      child: Row(
        children: [
          Icon(icon, color: Theme.of(context).colorScheme.onPrimary, size: 20),
          const SizedBox(width: 10),
          Text(
            label,
            style: TextStyle(color: Theme.of(context).colorScheme.onPrimary),
          ),
          const SizedBox(width: 5),
          Expanded(
            child: Text(
              value,
              style: TextStyle(
                color: Theme.of(context).colorScheme.onPrimary,
                fontWeight: FontWeight.w500,
              ),
              textAlign: TextAlign.right,
            ),
          ),
        ],
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    if (_currentUser == null) {
      return const Center(child: CircularProgressIndicator());
    }

    return Scaffold(
      appBar: AppBar(
        iconTheme: IconThemeData(
          color: Theme.of(context).colorScheme.onPrimary,
        ),
        backgroundColor: Theme.of(context).colorScheme.primary,
        title: Text(
          "User Profile",
          style: TextStyle(color: Theme.of(context).colorScheme.onPrimary),
        ),
        bottom: TabBar(
          labelColor: Theme.of(context).colorScheme.onPrimary,
          unselectedLabelColor: Theme.of(context).colorScheme.onPrimary,
          controller: _tabController,
          tabs: const [
            Tab(text: "Profile"),
            Tab(text: "Password"),
            Tab(text: "Reservations"),
          ],
        ),
      ),
      body: Container(
        decoration: BoxDecoration(color: Theme.of(context).colorScheme.primary),
        child: TabBarView(
          controller: _tabController,
          children: [
            // 1️⃣ Profile
            Padding(
              padding: const EdgeInsets.all(16),
              child: Form(
                key: _formKey,
                child: Column(
                  children: [
                    TextFormField(
                      style: TextStyle(
                        color: Theme.of(context).colorScheme.onPrimary,
                      ),
                      controller: _firstName,
                      decoration: const InputDecoration(
                        labelText: "First Name",
                        labelStyle: TextStyle(color: Colors.grey),
                      ),
                      validator: (v) => v!.isEmpty ? "Enter First Name" : null,
                    ),
                    TextFormField(
                      style: TextStyle(
                        color: Theme.of(context).colorScheme.onPrimary,
                      ),
                      controller: _lastName,
                      decoration: const InputDecoration(
                        labelText: "Last Name",
                        labelStyle: TextStyle(color: Colors.grey),
                      ),
                      validator: (v) => v!.isEmpty ? "Enter Last Name" : null,
                    ),
                    TextFormField(
                      style: TextStyle(
                        color: Theme.of(context).colorScheme.onPrimary,
                      ),
                      controller: _phone,
                      decoration: const InputDecoration(
                        labelText: "Phone Number",
                        labelStyle: TextStyle(color: Colors.grey),
                      ),
                    ),
                    const SizedBox(height: 20),
                    ElevatedButton(
                      onPressed: _updateProfile,
                      child: Text(
                        "Save",
                        style: TextStyle(
                          color: Theme.of(context).colorScheme.onPrimary,
                        ),
                      ),
                    ),
                  ],
                ),
              ),
            ),

            // 2️⃣ Password change
            Padding(
              padding: const EdgeInsets.all(16),
              child: Column(
                children: [
                  TextField(
                    style: TextStyle(
                      color: Theme.of(context).colorScheme.onPrimary,
                    ),
                    controller: _oldPass,
                    decoration: const InputDecoration(
                      labelText: "Old Password",
                      labelStyle: TextStyle(color: Colors.grey),
                    ),
                    obscureText: true,
                  ),
                  TextField(
                    style: TextStyle(
                      color: Theme.of(context).colorScheme.onPrimary,
                    ),
                    controller: _newPass,
                    decoration: const InputDecoration(
                      labelText: "New Password",
                      labelStyle: TextStyle(color: Colors.grey),
                    ),
                    obscureText: true,
                  ),
                  TextField(
                    style: TextStyle(
                      color: Theme.of(context).colorScheme.onPrimary,
                    ),
                    controller: _confirmPass,
                    decoration: const InputDecoration(
                      labelText: "Confirm Password",
                      labelStyle: TextStyle(color: Colors.grey),
                    ),
                    obscureText: true,
                  ),
                  const SizedBox(height: 20),
                  ElevatedButton(
                    onPressed: () {},
                    child: Text(
                      "Change Password",
                      style: TextStyle(
                        color: Theme.of(context).colorScheme.onPrimary,
                      ),
                    ),
                  ),
                ],
              ),
            ),

            // 3️⃣ Reservations
            _reservations.isEmpty
                ? Center(
                    child: Text(
                      "No Reservations",
                      style: TextStyle(
                        color: Theme.of(context).colorScheme.onPrimary,
                      ),
                    ),
                  )
                : ListView.builder(
                    itemCount: _reservations.length,
                    itemBuilder: (context, index) {
                      var res = _reservations[index];
                      return Card(
                        color: Theme.of(context).colorScheme.secondary,
                        margin: const EdgeInsets.all(8),
                        child: ListTile(
                          title: Text(
                            res.room?.hotelName ?? "Hotel",
                            style: TextStyle(
                              color: Theme.of(context).colorScheme.onPrimary,
                            ),
                          ),
                          subtitle: Text(
                            "${res.checkInDate} - ${res.checkOutDate}",
                            style: TextStyle(
                              color: Theme.of(context).colorScheme.onPrimary,
                            ),
                          ),
                          trailing: Row(
                            mainAxisSize: MainAxisSize.min,
                            children: [
                              Text(
                                res.status ?? "",
                                style: TextStyle(
                                  color: res.status?.toLowerCase() == "canceled"
                                      ? Colors.red
                                      : Colors.greenAccent,
                                ),
                              ),
                              Icon(
                                Icons.info_outline,
                                color: Theme.of(context).colorScheme.onPrimary,
                              ),
                            ],
                          ),
                          onTap: () => _showReservationDetails(res),
                        ),
                      );
                    },
                  ),
          ],
        ),
      ),
    );
  }
}
