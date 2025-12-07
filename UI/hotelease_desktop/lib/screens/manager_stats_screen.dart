import 'package:flutter/material.dart';
import 'package:hotelease_mobile_new/providers/dashboard_provider.dart';
import 'package:hotelease_mobile_new/screens/master_screen.dart';
import 'package:intl/intl.dart';

class ManagerStatsScreen extends StatefulWidget {
  final int hotelId;
  const ManagerStatsScreen({super.key, required this.hotelId});

  @override
  State<ManagerStatsScreen> createState() => _ManagerStatsScreenState();
}

class _ManagerStatsScreenState extends State<ManagerStatsScreen> {
  Map<String, dynamic>? data;
  bool loading = true;
  String? error;

  @override
  void initState() {
    super.initState();
    fetchDashboard();
  }

  Future<void> fetchDashboard() async {
    try {
      final provider = DashboardProvider();
      final result = await provider.getManagerDashboard(widget.hotelId);
      setState(() {
        data = result;
        loading = false;
      });
    } catch (e) {
      setState(() {
        error = "Error: $e";
        loading = false;
      });
    }
  }

  // Helper za formatiranje datuma
  String _formatDate(String dateString) {
    final dt = DateTime.parse(dateString);
    return DateFormat('dd.MM.yyyy. HH:mm').format(dt);
  }

  // Helper za formatiranje cijena u USD sa 2 decimale
  String _formatPrice(dynamic price) {
    if (price == null) return "\$0.00";
    return "\$${(price as num).toStringAsFixed(2)}";
  }

  @override
  Widget build(BuildContext context) {
    if (loading) return const Center(child: CircularProgressIndicator());
    if (error != null) return Center(child: Text(error!));

    final manager = data!['manager'];
    final dashboard = data!['dashboard'];
    final reservations = dashboard['recentReservations'] ?? [];

    return MasterScreen(
      title: "Manager Dashboard",
      child: SingleChildScrollView(
        padding: const EdgeInsets.all(16),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            // Manager info
            Card(
              color: const Color.fromRGBO(15, 41, 70, 1),
              child: ListTile(
                title: Text(
                  "${manager['firstName']} ${manager['lastName']}",
                  style: TextStyle(color: Colors.white),
                ),
                subtitle: Text(
                  manager['email'],
                  style: TextStyle(color: Colors.grey),
                ),
                trailing: Text(
                  "Username: ${manager['username']}",
                  style: TextStyle(color: Colors.grey),
                ),
              ),
            ),
            const SizedBox(height: 16),

            // Stats section
            Text(
              "📊 Statistics",
              style: TextStyle(color: Colors.white, fontSize: 20),
            ),
            const Divider(),
            _buildStat(
              "Active Bookings",
              dashboard['activeReservations'].toString(),
            ),
            _buildStat("New Users", dashboard['newUsers'].toString()),
            _buildStat(
              "Revenue This Month",
              _formatPrice(dashboard['monthlyRevenue']),
            ),
            _buildStat("Occupancy Rate", "${dashboard['occupancyRate']} %"),

            const SizedBox(height: 24),

            // Recent Reservations section
            Text(
              "📑 Recent Reservations",
              style: TextStyle(color: Colors.white, fontSize: 20),
            ),
            const Divider(),
            if (reservations.isEmpty)
              const Text(
                "No recent reservations.",
                style: TextStyle(color: Colors.white),
              ),
            for (var r in reservations)
              Card(
                color: const Color.fromRGBO(15, 41, 70, 1),
                margin: const EdgeInsets.symmetric(vertical: 8),
                child: ListTile(
                  title: Text(
                    "Reservation #${r['id']} - ${r['status']}",
                    style: TextStyle(color: Colors.white),
                  ),
                  subtitle: Text(
                    "User: ${r['user']['firstName']} ${r['user']['lastName']}\n"
                    "Room: ${r['room']['name']}\n"
                    "Dates: ${_formatDate(r['checkInDate'])} → ${_formatDate(r['checkOutDate'])}",
                    style: TextStyle(color: Colors.grey),
                  ),
                  trailing: Text(
                    _formatPrice(r['totalPrice']),
                    style: TextStyle(color: Colors.grey),
                  ),
                ),
              ),
          ],
        ),
      ),
    );
  }

  Widget _buildStat(String label, String value) {
    return Padding(
      padding: const EdgeInsets.symmetric(vertical: 8),
      child: Row(
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        children: [
          Text(
            label,
            style: const TextStyle(
              fontWeight: FontWeight.w500,
              color: Colors.white,
            ),
          ),
          Text(
            value,
            style: const TextStyle(
              fontWeight: FontWeight.bold,
              color: Colors.white,
            ),
          ),
        ],
      ),
    );
  }
}
