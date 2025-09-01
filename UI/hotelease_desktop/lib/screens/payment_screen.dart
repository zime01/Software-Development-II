import 'package:flutter/material.dart';
import 'package:hotelease_mobile_new/providers/payments_provider.dart';
import 'package:hotelease_mobile_new/screens/master_screen.dart';
import 'package:url_launcher/url_launcher.dart';

class PaymentScreen extends StatelessWidget {
  final int reservationId;
  final double amount;
  final String currency;

  const PaymentScreen({
    super.key,
    required this.reservationId,
    required this.amount,
    required this.currency,
  });

  Future<void> _startPayment(BuildContext context) async {
    final provider = PaymentsProvider();

    try {
      // 1️⃣ Kreiraj Checkout session i dobavi URL
      final url = await provider.createCheckoutSession(
        reservationId,
        amount,
        currency: currency,
      );
      if (url == null) throw Exception("Backend nije vratio URL!");

      // 2️⃣ Otvori browser
      final uri = Uri.parse(url);
      if (!await launchUrl(uri, mode: LaunchMode.externalApplication)) {
        throw Exception("Ne mogu otvoriti browser na ovom uređaju");
      }

      // 3️⃣ Obavijesti korisnika
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(
          content: Text(
            "Plaćanje pokrenuto! Nakon završetka, email potvrda će biti poslana.",
          ),
        ),
      );
    } catch (e) {
      ScaffoldMessenger.of(
        context,
      ).showSnackBar(SnackBar(content: Text("Greška: $e")));
    }
  }

  @override
  Widget build(BuildContext context) {
    return MasterScreen(
      title: "Payment",
      child: Center(
        child: ElevatedButton(
          onPressed: () => _startPayment(context),
          child: const Text("Plati sada"),
        ),
      ),
    );
  }
}
