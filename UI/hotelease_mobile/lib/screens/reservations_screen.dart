import 'package:flutter/material.dart';
import 'package:hotelease_mobile/models/room.dart';
import 'package:hotelease_mobile/models/service.dart';
import 'package:hotelease_mobile/providers/services_provider.dart';
import 'package:hotelease_mobile/screens/master_screen.dart';
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
  int guests = 0;
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
            Padding(
              padding: const EdgeInsets.all(8.0),
              child: Text(
                "${widget.hotelName}",
                style: TextStyle(
                  fontSize: 24,
                  fontWeight: FontWeight.bold,
                  color: Colors.white,
                ),
              ),
            ),
            Padding(
              padding: const EdgeInsets.all(8.0),
              child: Text(
                "${widget.room.name}",
                style: const TextStyle(fontSize: 20, color: Colors.white),
              ),
            ),
            Padding(
              padding: const EdgeInsets.all(5),
              child: Text(
                "Capacity: ${widget.room.capacity}",
                style: TextStyle(color: Colors.white),
              ),
            ),
            Padding(
              padding: const EdgeInsets.all(4),
              child: Text(
                "Price per night: \$${widget.room.pricePerNight}",
                style: TextStyle(color: Colors.white),
              ),
            ),
            const Divider(),
            Padding(
              padding: const EdgeInsets.all(4),
              child: Text(
                "Dates: ${formattedCheckIn} → ${formattedCheckOut}",
                style: TextStyle(color: Colors.white),
              ),
            ),
            Padding(
              padding: const EdgeInsets.all(4),
              child: Text(
                "Nights: $nights",
                style: TextStyle(color: Colors.white),
              ),
            ),
            const Divider(),
            Row(
              children: [
                Padding(
                  padding: const EdgeInsets.all(4),
                  child: const Text(
                    "Guests: ",
                    style: TextStyle(color: Colors.white),
                  ),
                ),
                IconButton(
                  onPressed: () {
                    if (guests > 1) setState(() => guests--);
                  },
                  icon: const Icon(Icons.remove),
                ),
                Padding(
                  padding: const EdgeInsets.all(4),
                  child: Text("$guests", style: TextStyle(color: Colors.white)),
                ),
                IconButton(
                  onPressed: () => setState(() => guests++),
                  icon: const Icon(Icons.add),
                ),
              ],
            ),
            const Divider(),
            Padding(
              padding: const EdgeInsets.all(4),
              child: const Text(
                "Additional Services",
                style: TextStyle(color: Colors.white),
              ),
            ),
            ...availableServices.map((s) {
              bool selected = selectedServices.contains(s);
              return CheckboxListTile(
                title: Padding(
                  padding: const EdgeInsets.all(4),
                  child: Text(
                    "${s.name} (\$${s.price})",
                    style: TextStyle(color: Colors.white),
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
            Padding(
              padding: const EdgeInsets.all(4),
              child: Text(
                "Room cost: \$${roomCost.toStringAsFixed(2)}",
                style: TextStyle(color: Colors.white),
              ),
            ),
            Padding(
              padding: const EdgeInsets.all(4),
              child: Text(
                "Services: \$${servicesCost.toStringAsFixed(2)}",
                style: TextStyle(color: Colors.white),
              ),
            ),
            Padding(
              padding: const EdgeInsets.all(4),
              child: Text(
                "Total: \$${totalCost.toStringAsFixed(2)}",
                style: const TextStyle(
                  fontSize: 20,
                  fontWeight: FontWeight.bold,
                  color: Colors.white,
                ),
              ),
            ),
            const SizedBox(height: 20),
            // ElevatedButton(
            //   onPressed: () async {
            //     var reservationPayload = {
            //       "roomId": widget.room.id,
            //       "checkInDate": widget.checkInDate.toIso8601String(),
            //       "checkOutDate": widget.checkOutDate.toIso8601String(),
            //       "guests": guests,
            //       "services": selectedServices.map((s) => s.id).toList(),
            //     };

            //     try {
            //       await context.read<ReservationsProvider>().createReservation(
            //         reservationPayload,
            //       );

            //       // poziv RabbitMQ API-ja za slanje notifikacije
            //       await context
            //           .read<NotificationsProvider>()
            //           .sendReservationCreated();

            //       Navigator.push(
            //         context,
            //         MaterialPageRoute(
            //           builder: (_) =>
            //               PaymentScreen(reservationPayload: reservationPayload),
            //         ),
            //       );
            //     } catch (e) {
            //       ScaffoldMessenger.of(context).showSnackBar(
            //         SnackBar(content: Text("Greška pri rezervaciji: $e")),
            //       );
            //     }
            //   },
            //   child: const Text("Proceed to Payment"),
            // ),
          ],
        ),
      ),
    );
  }
}
