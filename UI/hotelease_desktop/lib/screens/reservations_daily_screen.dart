import 'package:flutter/material.dart';
import 'package:hotelease_mobile_new/models/reservation.dart';
import 'package:hotelease_mobile_new/providers/reservations_provider.dart';
import 'package:hotelease_mobile_new/screens/master_screen.dart';
import 'package:hotelease_mobile_new/screens/reservations_edit_screen.dart';
import 'package:provider/provider.dart';
import 'package:intl/intl.dart';

class ReservationsDailyScreen extends StatefulWidget {
  const ReservationsDailyScreen({super.key});

  @override
  State<ReservationsDailyScreen> createState() =>
      _ReservationsDailyScreenState();
}

class _ReservationsDailyScreenState extends State<ReservationsDailyScreen> {
  DateTime selectedDate = DateTime.now();
  late Future<List<Reservation>> _futureReservations;
  final Color primaryColor = const Color.fromRGBO(17, 45, 78, 1);
  late ReservationsProvider _reservationsProvider;

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
    _reservationsProvider = Provider.of<ReservationsProvider>(context);
    _loadReservations();
  }

  void _loadReservations() {
    setState(() {
      _futureReservations = _reservationsProvider.getByDate(selectedDate);
    });
  }

  Future<void> _refresh() async {
    setState(() {
      _futureReservations = _reservationsProvider.refreshByDate(selectedDate);
    });
    await _futureReservations;
  }

  void _changeDate(DateTime newDate) {
    setState(() {
      selectedDate = newDate;
      _loadReservations();
    });
  }

  void _pickDate() async {
    DateTime? picked = await showDatePicker(
      context: context,
      initialDate: selectedDate,
      firstDate: DateTime(2020),
      lastDate: DateTime(2030),
    );
    if (picked != null && picked != selectedDate) {
      _changeDate(picked);
    }
  }

  void _incrementDay() =>
      _changeDate(selectedDate.add(const Duration(days: 1)));
  void _decrementDay() =>
      _changeDate(selectedDate.subtract(const Duration(days: 1)));

  String _formattedDate(DateTime date) => DateFormat('dd.MM.yyyy').format(date);

  Color _statusColor(String? status) {
    switch (status) {
      case "Confirmed":
        return Colors.green;
      case "Pending":
        return Colors.orange;
      case "Cancelled":
        return Colors.red;
      default:
        return Colors.grey;
    }
  }

  @override
  Widget build(BuildContext context) {
    return MasterScreen(
      title: "Daily Reservations",
      child: Column(
        children: [
          // Date picker + strelice
          Padding(
            padding: const EdgeInsets.all(12.0),
            child: Container(
              padding: const EdgeInsets.symmetric(vertical: 12, horizontal: 16),
              decoration: BoxDecoration(
                color: const Color.fromRGBO(15, 41, 70, 1),
                borderRadius: BorderRadius.circular(10),
              ),
              child: Row(
                children: [
                  IconButton(
                    icon: const Icon(Icons.chevron_left, size: 28),
                    color: Colors.white,
                    onPressed: _decrementDay,
                  ),
                  Expanded(
                    child: InkWell(
                      onTap: _pickDate,
                      child: Center(
                        child: Text(
                          _formattedDate(selectedDate),
                          style: TextStyle(
                            fontSize: 18,
                            fontWeight: FontWeight.bold,
                            color: Colors.white,
                          ),
                        ),
                      ),
                    ),
                  ),
                  IconButton(
                    icon: const Icon(Icons.chevron_right, size: 28),
                    color: Colors.white,
                    onPressed: _incrementDay,
                  ),
                ],
              ),
            ),
          ),

          // Lista rezervacija sa pull-to-refresh
          Expanded(
            child: RefreshIndicator(
              onRefresh: _refresh,
              child: FutureBuilder<List<Reservation>>(
                future: _futureReservations,
                builder: (context, snapshot) {
                  if (snapshot.connectionState == ConnectionState.waiting) {
                    return const Center(child: CircularProgressIndicator());
                  }

                  if (snapshot.hasError) {
                    return Center(
                      child: SingleChildScrollView(
                        physics: const AlwaysScrollableScrollPhysics(),
                        child: Column(
                          mainAxisSize: MainAxisSize.min,
                          children: [
                            const Icon(
                              Icons.error_outline,
                              size: 48,
                              color: Colors.red,
                            ),
                            const SizedBox(height: 12),
                            const Text(
                              "Error loading reservations",
                              style: TextStyle(fontSize: 16, color: Colors.red),
                            ),
                            const SizedBox(height: 8),
                            Text(
                              snapshot.error.toString(),
                              style: const TextStyle(
                                fontSize: 12,
                                color: Colors.grey,
                              ),
                              textAlign: TextAlign.center,
                            ),
                            const SizedBox(height: 12),
                            ElevatedButton(
                              onPressed: _refresh,
                              child: const Text("Try again"),
                            ),
                          ],
                        ),
                      ),
                    );
                  }

                  final list = snapshot.data ?? [];
                  if (list.isEmpty) {
                    return Center(
                      child: SingleChildScrollView(
                        physics: const AlwaysScrollableScrollPhysics(),
                        child: Column(
                          mainAxisSize: MainAxisSize.min,
                          children: [
                            const Icon(
                              Icons.calendar_today,
                              size: 64,
                              color: Colors.white,
                            ),
                            const SizedBox(height: 16),
                            const Text(
                              "No reservations for this date.",
                              style: TextStyle(
                                fontSize: 16,
                                color: Colors.white,
                              ),
                            ),
                          ],
                        ),
                      ),
                    );
                  }

                  return ListView.builder(
                    physics: const AlwaysScrollableScrollPhysics(),
                    itemCount: list.length,
                    itemBuilder: (_, i) {
                      final r = list[i];
                      return Card(
                        color: const Color.fromRGBO(15, 41, 70, 1),
                        margin: const EdgeInsets.symmetric(
                          horizontal: 12,
                          vertical: 6,
                        ),
                        elevation: 2,
                        child: ListTile(
                          onTap: () {
                            Navigator.push(
                              context,
                              MaterialPageRoute(
                                builder: (_) =>
                                    ReservationEditScreen(reservation: r),
                              ),
                            ).then((_) => _refresh());
                          },
                          leading: CircleAvatar(
                            backgroundColor: _statusColor(r.status),
                            child: Text(
                              "${r.user?.firstName?.substring(0, 1) ?? ''}${r.user?.lastName?.substring(0, 1) ?? ''}",
                              style: const TextStyle(
                                color: Colors.white,
                                fontWeight: FontWeight.bold,
                              ),
                            ),
                          ),
                          title: Text(
                            "${r.user?.firstName ?? ''} ${r.user?.lastName ?? ''}",
                            style: const TextStyle(
                              fontWeight: FontWeight.bold,
                              color: Colors.white,
                            ),
                          ),
                          subtitle: Column(
                            crossAxisAlignment: CrossAxisAlignment.start,
                            children: [
                              const SizedBox(height: 4),
                              Text(
                                "${r.room?.hotel?.name ?? 'Unknown Hotel'} • Room: ${r.room?.name ?? 'N/A'}",
                                style: TextStyle(
                                  fontSize: 14,
                                  color: Colors.white,
                                ),
                              ),
                              if (r.checkInDate != null &&
                                  r.checkOutDate != null)
                                Text(
                                  "Check-in: ${DateFormat('dd.MM.yyyy. HH:mm').format(r.checkInDate!)} | Check-out: ${DateFormat('dd.MM.yyyy. HH:mm').format(r.checkOutDate!)}",
                                  style: TextStyle(
                                    fontSize: 12,
                                    color: Colors.white,
                                  ),
                                ),
                            ],
                          ),
                          trailing: Container(
                            padding: const EdgeInsets.symmetric(
                              horizontal: 10,
                              vertical: 4,
                            ),
                            decoration: BoxDecoration(
                              color: _statusColor(r.status),
                              borderRadius: BorderRadius.circular(8),
                            ),
                            child: Text(
                              r.status ?? "Unknown",
                              style: const TextStyle(
                                color: Colors.white,
                                fontWeight: FontWeight.bold,
                                fontSize: 12,
                              ),
                            ),
                          ),
                        ),
                      );
                    },
                  );
                },
              ),
            ),
          ),
        ],
      ),
    );
  }
}
