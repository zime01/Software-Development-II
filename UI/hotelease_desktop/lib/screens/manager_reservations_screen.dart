import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:hotelease_mobile_new/models/hotel.dart';
import 'package:hotelease_mobile_new/models/user.dart';
import 'package:hotelease_mobile_new/providers/hotels_provider.dart';
import 'package:hotelease_mobile_new/providers/users_provider.dart';
import 'package:hotelease_mobile_new/screens/master_screen.dart';
import 'package:hotelease_mobile_new/utils/DateTimeFormat.dart';
import 'package:http/http.dart' as http;
import 'package:intl/intl.dart';
import 'package:provider/provider.dart';
import 'package:hotelease_mobile_new/providers/reservations_provider.dart';
import 'package:hotelease_mobile_new/models/reservation.dart';

class ManagerReservationsScreen extends StatefulWidget {
  const ManagerReservationsScreen({super.key});

  @override
  State<ManagerReservationsScreen> createState() =>
      _ManagerReservationsScreenState();
}

class _ManagerReservationsScreenState extends State<ManagerReservationsScreen> {
  List<Reservation> reservations = [];
  bool isLoading = true;

  @override
  void initState() {
    super.initState();
    _loadReservations();
  }

  Future<void> _loadReservations() async {
    try {
      var provider = context.read<ReservationsProvider>();
      var data = await provider.getMyReservations();

      setState(() {
        reservations = data;
        isLoading = false;
      });
    } catch (e) {
      setState(() {
        isLoading = false;
      });
      ScaffoldMessenger.of(
        context,
      ).showSnackBar(SnackBar(content: Text("Error loading reservations: $e")));
    }
  }

  @override
  Widget build(BuildContext context) {
    return MasterScreen(
      title: "Reservations",
      child: isLoading
          ? const Center(child: CircularProgressIndicator())
          : reservations.isEmpty
          ? const Center(child: Text("No reservations found"))
          : RefreshIndicator(
              onRefresh: _loadReservations,
              child: ListView.builder(
                itemCount: reservations.length,
                itemBuilder: (context, index) {
                  final res = reservations[index];
                  return Card(
                    color: const Color.fromRGBO(15, 41, 70, 1),
                    margin: const EdgeInsets.symmetric(
                      horizontal: 12,
                      vertical: 6,
                    ),
                    child: ListTile(
                      leading: const Icon(
                        Icons.book_online,
                        color: Colors.white,
                      ),
                      title: Text(
                        "Reservation #${res.id}",
                        style: TextStyle(color: Colors.white),
                      ),
                      subtitle: Text(
                        "Check-in: ${DateFormat('dd.MM.yyyy HH:mm').format(res.checkInDate!)}\nCheck-out: ${DateFormat('dd.MM.yyyy HH:mm').format(res.checkOutDate!)}",
                        style: TextStyle(color: Colors.grey),
                      ),
                      trailing: const Icon(
                        Icons.arrow_forward_ios,
                        color: Colors.white,
                      ),
                      onTap: () {
                        Navigator.push(
                          context,
                          MaterialPageRoute(
                            builder: (_) =>
                                ReservationDetailsScreen(reservation: res),
                          ),
                        );
                      },
                    ),
                  );
                },
              ),
            ),
    );
  }
}

class ReservationDetailsScreen extends StatefulWidget {
  final Reservation reservation;

  const ReservationDetailsScreen({super.key, required this.reservation});

  @override
  State<ReservationDetailsScreen> createState() =>
      _ReservationDetailsScreenState();
}

class _ReservationDetailsScreenState extends State<ReservationDetailsScreen> {
  User? user;
  Hotel? hotel;
  bool isLoading = true;

  final usersProvider = UsersProvider();
  final hotelsProvider = HotelsProvider();

  @override
  void initState() {
    super.initState();
    _loadData();
  }

  Future<void> _loadData() async {
    try {
      // -------------------- User --------------------
      if (widget.reservation.userId != null) {
        try {
          // Pravi URL bez /details/
          final uri = Uri.parse(
            "${usersProvider.baseUrl}Users/${widget.reservation.userId}",
          );
          final response = await http.get(
            uri,
            headers: usersProvider.createHeaders(),
          );
          if (response.statusCode >= 200 && response.statusCode < 300) {
            final fetchedUser = User.fromJson(jsonDecode(response.body));
            setState(() {
              user = fetchedUser;
            });
            print("User loaded: ${user!.firstName} ${user!.lastName}");
          } else {
            print(
              "Failed to load user: ${response.statusCode} - ${response.body}",
            );
          }
        } catch (e) {
          print("Failed to load user: $e");
        }
      } else {
        print("No userId in reservation");
      }

      // -------------------- Hotel --------------------
      final hotelId = widget.reservation.room?.hotelId;
      if (hotelId != null && hotelId > 0) {
        try {
          // Pravi URL bez /details/
          final uri = Uri.parse("${hotelsProvider.baseUrl}Hotels/$hotelId");
          final response = await http.get(
            uri,
            headers: await hotelsProvider.createHeaders(),
          );
          if (response.statusCode >= 200 && response.statusCode < 300) {
            final fetchedHotel = Hotel.fromJson(jsonDecode(response.body));
            setState(() {
              hotel = fetchedHotel;
            });
            print("Hotel loaded: ${hotel!.name}");
          } else {
            print(
              "Failed to load hotel: ${response.statusCode} - ${response.body}",
            );
          }
        } catch (e) {
          print("Failed to load hotel: $e");
        }
      } else {
        print("No hotelId in reservation.room");
      }
    } finally {
      setState(() {
        isLoading = false;
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    if (isLoading) {
      return const Scaffold(body: Center(child: CircularProgressIndicator()));
    }

    final room = widget.reservation.room;

    return MasterScreen(
      title: "Reservation Details",
      child: SingleChildScrollView(
        padding: const EdgeInsets.all(16),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            // ------------------ REZERVACIJA ------------------
            _sectionTitle("Reservation Info"),
            _infoRow("Check-in", _formatDate(widget.reservation.checkInDate)),
            _infoRow("Check-out", _formatDate(widget.reservation.checkOutDate)),
            _infoRow(
              "Total Price",
              "${widget.reservation.totalPrice?.toStringAsFixed(2) ?? "-"} €",
            ),
            _infoRow("Status", widget.reservation.status ?? "Unknown"),
            const SizedBox(height: 20),

            // ------------------ GOST ------------------
            if (user != null) ...[
              _sectionTitle("Guest"),
              _infoRow("User ID", user!.id.toString()),
              _infoRow("Name", "${user!.firstName} ${user!.lastName}"),
              _infoRow("Email", user!.email ?? "-"),
              const SizedBox(height: 20),
            ],

            // ------------------ HOTEL ------------------
            if (hotel != null) ...[
              _sectionTitle("Hotel"),
              _infoRow("Name", hotel!.name ?? "-"),
              _infoRow("Address", hotel!.address ?? "-"),
              const SizedBox(height: 20),
            ],

            // ------------------ SOBA ------------------
            if (room != null) ...[
              _sectionTitle("Room"),
              _infoRow("Room Name", room.name ?? "-"),
              _infoRow("Capacity", room.capacity?.toString() ?? "-"),
              _infoRow("Price per Night", "${room.pricePerNight ?? "-"} €"),
              const SizedBox(height: 20),
            ],
          ],
        ),
      ),
    );
  }

  // ------------------ Helpers ------------------
  Widget _sectionTitle(String title) {
    return Text(
      title,
      style: const TextStyle(
        color: Colors.white,
        fontSize: 20,
        fontWeight: FontWeight.bold,
      ),
    );
  }

  Widget _infoRow(String label, String value) {
    return Padding(
      padding: const EdgeInsets.symmetric(vertical: 4),
      child: Row(
        children: [
          Text(
            "$label: ",
            style: const TextStyle(
              color: Colors.white70,
              fontWeight: FontWeight.bold,
              fontSize: 16,
            ),
          ),
          Expanded(
            child: Text(
              value,
              style: const TextStyle(color: Colors.white, fontSize: 16),
            ),
          ),
        ],
      ),
    );
  }

  String _formatDate(DateTime? d) {
    if (d == null) return "-";
    return DateFormat('dd.MM.yyyy HH:mm').format(d);
  }
}
