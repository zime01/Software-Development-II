import 'package:flutter/material.dart';
import 'package:hotelease_mobile_new/models/reservation.dart';
import 'package:hotelease_mobile_new/providers/reservations_provider.dart';
import 'package:intl/intl.dart';
import 'package:provider/provider.dart';

class ReservationEditScreen extends StatefulWidget {
  final Reservation reservation;

  const ReservationEditScreen({super.key, required this.reservation});

  @override
  State<ReservationEditScreen> createState() => _ReservationEditScreenState();
}

class _ReservationEditScreenState extends State<ReservationEditScreen> {
  late TextEditingController totalController;
  late DateTime checkIn;
  late DateTime checkOut;
  String? status;

  final Color primaryColor = const Color.fromRGBO(17, 45, 78, 1);
  final Color cardColor = const Color.fromRGBO(34, 62, 92, 1);

  final List<String> statusOptions = ["Confirmed", "Pending", "Cancelled"];

  @override
  void initState() {
    super.initState();
    checkIn = widget.reservation.checkInDate!;
    checkOut = widget.reservation.checkOutDate!;
    totalController = TextEditingController(
      text: widget.reservation.totalPrice?.toStringAsFixed(2),
    );
    status = widget.reservation.status;
  }

  @override
  void dispose() {
    totalController.dispose();
    super.dispose();
  }

  Future<void> _pickDate({required bool isCheckIn}) async {
    DateTime initial = isCheckIn ? checkIn : checkOut;
    DateTime? picked = await showDatePicker(
      context: context,
      initialDate: initial,
      firstDate: DateTime(2020),
      lastDate: DateTime(2030),
    );
    if (picked != null) {
      TimeOfDay? time = await showTimePicker(
        context: context,
        initialTime: TimeOfDay.fromDateTime(initial),
      );
      if (time != null) {
        setState(() {
          DateTime combined = DateTime(
            picked.year,
            picked.month,
            picked.day,
            time.hour,
            time.minute,
          );
          if (isCheckIn) {
            checkIn = combined;
          } else {
            checkOut = combined;
          }
        });
      }
    }
  }

  String _formatDateTime(DateTime dt) {
    return DateFormat('dd.MM.yyyy. HH:mm').format(dt);
  }

  @override
  Widget build(BuildContext context) {
    final user = widget.reservation.user;
    final room = widget.reservation.room;
    final payments = widget.reservation.payments ?? [];
    final reservationsProvider = Provider.of<ReservationsProvider>(context);

    return Scaffold(
      appBar: AppBar(
        title: const Text("Edit Reservation"),
        backgroundColor: primaryColor,
        foregroundColor: Colors.white,
      ),
      body: SingleChildScrollView(
        padding: const EdgeInsets.all(16),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            _sectionTitle("User Info"),
            _infoTile(
              "Name",
              "${user?.firstName ?? ''} ${user?.lastName ?? ''}",
            ),
            _infoTile("Email", user?.email),
            _infoTile("User ID", user?.id?.toString()),

            const SizedBox(height: 16),

            _sectionTitle("Room Info"),
            _infoTile("Room", room?.name),

            _infoTile(
              "Price per Night",
              "\$${room?.pricePerNight?.toStringAsFixed(2)}",
            ),

            const SizedBox(height: 16),

            _sectionTitle("Reservation"),
            _editableDateTile(
              "Check-in",
              checkIn,
              () => _pickDate(isCheckIn: true),
            ),
            _editableDateTile(
              "Check-out",
              checkOut,
              () => _pickDate(isCheckIn: false),
            ),
            _editableTextTile("Total (\$)", totalController),
            _dropdownTile("Status", statusOptions, status, (val) {
              setState(() {
                status = val;
              });
            }),

            const SizedBox(height: 16),

            ElevatedButton(
              onPressed: () async {
                try {
                  double total = double.tryParse(totalController.text) ?? 0.0;

                  widget.reservation.checkInDate = checkIn;
                  widget.reservation.checkOutDate = checkOut;
                  widget.reservation.totalPrice = total;
                  widget.reservation.status = status;

                  await reservationsProvider.updateReservation(
                    widget.reservation,
                  );

                  ScaffoldMessenger.of(context).showSnackBar(
                    const SnackBar(
                      content: Text("Reservation updated successfully"),
                    ),
                  );
                  Navigator.pop(context);
                } catch (e) {
                  ScaffoldMessenger.of(context).showSnackBar(
                    SnackBar(content: Text("Error updating reservation: $e")),
                  );
                }
              },
              child: const Text(
                "Save Changes",
                style: TextStyle(color: Colors.white),
              ),
              style: ElevatedButton.styleFrom(backgroundColor: primaryColor),
            ),

            const SizedBox(height: 16),

            _sectionTitle("Payments"),
            if (payments.isEmpty)
              const Text(
                "No payments recorded.",
                style: TextStyle(color: Colors.white),
              ),
            if (payments.isNotEmpty)
              Column(
                children: payments.map((p) {
                  return Card(
                    color: cardColor,
                    child: ListTile(
                      title: Text(
                        "Amount: \$${p.amount!.toStringAsFixed(2)}",
                        style: const TextStyle(color: Colors.white),
                      ),
                      subtitle: Text(
                        "Date: ${_formatDateTime(p.createdAt!)}",
                        style: const TextStyle(color: Colors.white70),
                      ),
                    ),
                  );
                }).toList(),
              ),
          ],
        ),
      ),
      backgroundColor: primaryColor,
    );
  }

  Widget _sectionTitle(String text) {
    return Padding(
      padding: const EdgeInsets.only(bottom: 6),
      child: Text(
        text,
        style: const TextStyle(
          fontSize: 20,
          fontWeight: FontWeight.bold,
          color: Colors.white,
        ),
      ),
    );
  }

  Widget _infoTile(String label, String? value) {
    return Container(
      padding: const EdgeInsets.all(12),
      margin: const EdgeInsets.only(bottom: 8),
      decoration: BoxDecoration(
        color: cardColor,
        borderRadius: BorderRadius.circular(10),
      ),
      child: Row(
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        children: [
          Text(
            label,
            style: const TextStyle(
              fontWeight: FontWeight.bold,
              color: Colors.white,
            ),
          ),
          Text(value ?? "-", style: const TextStyle(color: Colors.white)),
        ],
      ),
    );
  }

  Widget _editableTextTile(String label, TextEditingController controller) {
    return Container(
      padding: const EdgeInsets.all(12),
      margin: const EdgeInsets.only(bottom: 8),
      decoration: BoxDecoration(
        color: cardColor,
        borderRadius: BorderRadius.circular(10),
      ),
      child: Row(
        children: [
          Expanded(
            child: Text(
              label,
              style: const TextStyle(
                fontWeight: FontWeight.bold,
                color: Colors.white,
              ),
            ),
          ),
          SizedBox(
            width: 100,
            child: TextField(
              controller: controller,
              keyboardType: TextInputType.numberWithOptions(decimal: true),
              textAlign: TextAlign.right,
              style: const TextStyle(color: Colors.white),
              decoration: const InputDecoration(border: InputBorder.none),
            ),
          ),
        ],
      ),
    );
  }

  Widget _editableDateTile(String label, DateTime date, VoidCallback onTap) {
    return InkWell(
      onTap: onTap,
      child: Container(
        padding: const EdgeInsets.all(12),
        margin: const EdgeInsets.only(bottom: 8),
        decoration: BoxDecoration(
          color: cardColor,
          borderRadius: BorderRadius.circular(10),
        ),
        child: Row(
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          children: [
            Text(
              label,
              style: const TextStyle(
                fontWeight: FontWeight.bold,
                color: Colors.white,
              ),
            ),
            Text(
              _formatDateTime(date),
              style: const TextStyle(color: Colors.white),
            ),
          ],
        ),
      ),
    );
  }

  Widget _dropdownTile(
    String label,
    List<String> options,
    String? value,
    ValueChanged<String?> onChanged,
  ) {
    return Container(
      padding: const EdgeInsets.all(12),
      margin: const EdgeInsets.only(bottom: 8),
      decoration: BoxDecoration(
        color: cardColor,
        borderRadius: BorderRadius.circular(10),
      ),
      child: Row(
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        children: [
          Text(
            label,
            style: const TextStyle(
              fontWeight: FontWeight.bold,
              color: Colors.white,
            ),
          ),
          DropdownButton<String>(
            dropdownColor: cardColor,
            value: value,
            items: options
                .map(
                  (e) => DropdownMenuItem(
                    value: e,
                    child: Text(e, style: const TextStyle(color: Colors.white)),
                  ),
                )
                .toList(),
            onChanged: onChanged,
          ),
        ],
      ),
    );
  }
}
