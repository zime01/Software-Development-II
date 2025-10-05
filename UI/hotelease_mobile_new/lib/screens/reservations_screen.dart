import 'package:flutter/material.dart';
import 'package:hotelease_mobile_new/models/room.dart';
import 'package:hotelease_mobile_new/models/service.dart';
import 'package:hotelease_mobile_new/providers/notifications.provider.dart';
import 'package:hotelease_mobile_new/providers/reservations_provider.dart';
import 'package:hotelease_mobile_new/providers/services_provider.dart';
import 'package:hotelease_mobile_new/providers/users_provider.dart';
import 'package:hotelease_mobile_new/screens/master_screen.dart';
import 'package:hotelease_mobile_new/screens/payment_screen.dart';

import 'package:intl/intl.dart';
import 'package:provider/provider.dart';

class ReservationScreen extends StatefulWidget {
  final int hotelId;
  final String? hotelName;
  final Room room;
  final DateTime checkInDate;
  final DateTime checkOutDate;

  const ReservationScreen({
    super.key,
    required this.hotelId,
    required this.room,
    required this.checkInDate,
    required this.checkOutDate,
    required this.hotelName,
  });

  @override
  State<ReservationScreen> createState() => _ReservationScreenState();
}

class _ReservationScreenState extends State<ReservationScreen> {
  int guests = 1;
  List<Service> availableServices = [];
  List<Service> selectedServices = [];

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
    _loadServices();
  }

  Future<void> _loadServices() async {
    var provider = context.read<ServicesProvider>();
    var result = await provider.getServicesByHotel(widget.hotelId);
    setState(() {
      availableServices = result;
    });
  }

  double get roomCost =>
      widget.room.pricePerNight! *
      widget.checkOutDate.difference(widget.checkInDate).inDays;

  double get servicesCost =>
      selectedServices.fold(0, (sum, s) => sum + s.price);

  double get totalCost => roomCost + servicesCost;

  Widget _buildInfoRow(IconData icon, String label, String value) {
    return Padding(
      padding: const EdgeInsets.symmetric(vertical: 4.0),
      child: Row(
        children: [
          Icon(
            icon,
            color: Theme.of(context).colorScheme.onPrimary,
            size: 20,
          ), // ikonica
          const SizedBox(width: 8),
          Expanded(
            child: Text(
              label,
              style: TextStyle(color: Theme.of(context).colorScheme.onPrimary),
            ),
          ),
          Text(
            value,
            style: TextStyle(
              fontWeight: FontWeight.bold,
              color: Theme.of(context).colorScheme.onPrimary,
            ),
          ),
        ],
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    String formattedCheckIn = DateFormat(
      'dd.MM.yyyy',
    ).format(widget.checkInDate);
    String formattedCheckOut = DateFormat(
      'dd.MM.yyyy',
    ).format(widget.checkOutDate);
    int nights = widget.checkOutDate.difference(widget.checkInDate).inDays;

    return MasterScreen(
      child: Padding(
        padding: const EdgeInsets.all(16.0),
        child: ListView(
          children: [
            Text(
              "${widget.hotelName}",
              style: TextStyle(
                fontSize: 24,
                fontWeight: FontWeight.bold,
                color: Theme.of(context).colorScheme.onPrimary,
              ),
            ),
            const SizedBox(height: 8),
            Text(
              "${widget.room.name}",
              style: TextStyle(
                fontSize: 20,
                color: Theme.of(context).colorScheme.onPrimary,
              ),
            ),
            const Divider(),
            _buildInfoRow(Icons.bed, "Capacity", "${widget.room.capacity}"),
            _buildInfoRow(
              Icons.attach_money,
              "Price per night",
              "\$${widget.room.pricePerNight!.toStringAsFixed(2)}",
            ),
            _buildInfoRow(
              Icons.calendar_today,
              "Dates",
              "$formattedCheckIn → $formattedCheckOut",
            ),
            _buildInfoRow(Icons.night_shelter, "Nights", "$nights"),
            const Divider(),
            Row(
              children: [
                Icon(
                  Icons.group,
                  color: Theme.of(context).colorScheme.onPrimary,
                ),
                const SizedBox(width: 8),
                Text(
                  "Guests: ",
                  style: TextStyle(
                    color: Theme.of(context).colorScheme.onPrimary,
                  ),
                ),
                IconButton(
                  onPressed: () {
                    if (guests > 1) setState(() => guests--);
                  },
                  icon: const Icon(Icons.remove),
                ),
                Text(
                  "$guests",
                  style: TextStyle(
                    color: Theme.of(context).colorScheme.onPrimary,
                  ),
                ),
                IconButton(
                  onPressed: () => setState(() => guests++),
                  icon: const Icon(Icons.add),
                ),
              ],
            ),
            const Divider(),
            Text(
              "Additional Services",
              style: TextStyle(color: Theme.of(context).colorScheme.onPrimary),
            ),
            ...availableServices.map((s) {
              bool selected = selectedServices.contains(s);
              return CheckboxListTile(
                title: Text(
                  "${s.name} (\$${s.price})",
                  style: TextStyle(
                    color: Theme.of(context).colorScheme.onPrimary,
                  ),
                ),
                value: selected,
                onChanged: (val) {
                  setState(() {
                    if (val == true) {
                      selectedServices.add(s);
                    } else {
                      selectedServices.remove(s);
                    }
                  });
                },
              );
            }),
            const Divider(),
            _buildInfoRow(
              Icons.meeting_room,
              "Room cost",
              "\$${roomCost.toStringAsFixed(2)}",
            ),
            _buildInfoRow(
              Icons.miscellaneous_services,
              "Services",
              "\$${servicesCost.toStringAsFixed(2)}",
            ),
            _buildInfoRow(
              Icons.payments,
              "Total",
              "\$${totalCost.toStringAsFixed(2)}",
            ),
            const SizedBox(height: 40),
            Container(
              decoration: BoxDecoration(
                border: Border.all(
                  color: Theme.of(context).colorScheme.onPrimary,
                ),
                borderRadius: BorderRadius.all(Radius.circular(30)),
              ),
              child: ElevatedButton(
                onPressed: () async {
                  try {
                    final currentUser = await UsersProvider().getCurrentUser();

                    final reservationPayload = {
                      "userId": currentUser.id,
                      "roomId": widget.room.id,
                      "checkInDate": widget.checkInDate.toIso8601String(),
                      "checkOutDate": widget.checkOutDate.toIso8601String(),
                      "totalPrice": totalCost,
                      "status": "Pending",
                    };

                    // Kreiraj rezervaciju
                    var createdReservation = await context
                        .read<ReservationsProvider>()
                        .createReservation(reservationPayload);

                    // Pošalji RabbitMQ event
                    await context
                        .read<NotificationsProvider>()
                        .sendReservationCreated(
                          userId: currentUser.id!,
                          email: currentUser.email!,
                          hotelName: widget.hotelName ?? "Hotel",
                          roomName: widget.room.name,
                          checkIn: widget.checkInDate,
                          checkOut: widget.checkOutDate,
                        );

                    // Navigacija na PaymentScreen
                    Navigator.push(
                      context,
                      MaterialPageRoute(
                        builder: (_) => PaymentScreen(
                          reservationId: createdReservation.id ?? 0,
                          amount: totalCost,
                          currency: "USD",
                        ),
                      ),
                    );
                  } catch (e, stackTrace) {
                    print("Error: $e");
                    print(stackTrace);
                    ScaffoldMessenger.of(context).showSnackBar(
                      SnackBar(content: Text("Error while reservating $e")),
                    );
                  }
                },
                style: ElevatedButton.styleFrom(
                  backgroundColor: Theme.of(context).colorScheme.secondary,
                  padding: const EdgeInsets.symmetric(vertical: 16),
                  textStyle: const TextStyle(fontSize: 18),
                ),
                child: Text(
                  "Continue to payment",
                  style: TextStyle(
                    color: Theme.of(context).colorScheme.onPrimary,
                  ),
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
