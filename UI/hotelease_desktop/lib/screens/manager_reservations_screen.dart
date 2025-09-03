import 'package:flutter/material.dart';
import 'package:hotelease_mobile_new/screens/master_screen.dart';
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
                        "Check-in: ${res.checkInDate}\nCheck-out: ${res.checkOutDate}",
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

class ReservationDetailsScreen extends StatelessWidget {
  final Reservation reservation;
  const ReservationDetailsScreen({super.key, required this.reservation});

  @override
  Widget build(BuildContext context) {
    return MasterScreen(
      title: "Reservations",
      child: Padding(
        padding: const EdgeInsets.all(16),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Text(
              "Check-in: ${reservation.checkInDate}",
              style: const TextStyle(fontSize: 16, color: Colors.white),
            ),
            Text(
              "Check-out: ${reservation.checkOutDate}",
              style: const TextStyle(fontSize: 16, color: Colors.white),
            ),
            const SizedBox(height: 16),
            Text(
              "Room ID: ${reservation.roomId ?? "-"}",
              style: TextStyle(color: Colors.white),
            ),
            Text(
              "User ID: ${reservation.userId ?? "-"}",
              style: TextStyle(color: Colors.white),
            ),
          ],
        ),
      ),
    );
  }
}
