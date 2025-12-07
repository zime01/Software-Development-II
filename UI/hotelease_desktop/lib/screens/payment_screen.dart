import 'dart:async';
import 'package:flutter/material.dart';
import 'package:hotelease_mobile_new/providers/payments_provider.dart';
import 'package:uni_links/uni_links.dart';
import 'package:url_launcher/url_launcher.dart';

class PaymentScreen extends StatefulWidget {
  final int reservationId;
  final double amount;
  final String currency;

  const PaymentScreen({
    super.key,
    required this.reservationId,
    required this.amount,
    required this.currency,
  });

  @override
  State<PaymentScreen> createState() => _PaymentScreenState();
}

class _PaymentScreenState extends State<PaymentScreen> {
  StreamSubscription? _sub;
  bool isPaid = false;

  Timer? _timer;
  int? _paymentId;

  @override
  void initState() {
    super.initState();
    checkExistingPayment();
  }

  Future<void> checkExistingPayment() async {
    final provider = PaymentsProvider();

    // API GET /Payments/status/{reservationId}
    final status = await provider.checkStatusByReservation(
      widget.reservationId,
    );

    if (status == "Succeeded") {
      setState(() => isPaid = true);
    }
  }

  Future<void> startPayment() async {
    final provider = PaymentsProvider();
    final session = await provider.createCheckoutSession(
      widget.reservationId,
      widget.amount,
    );

    if (session == null) return;

    final url = session["url"];
    _paymentId = session["paymentId"];

    // Otvori Stripe Checkout u browseru
    await launchUrl(Uri.parse(url), mode: LaunchMode.externalApplication);

    // START POLLING
    _timer = Timer.periodic(Duration(seconds: 2), (timer) async {
      if (_paymentId == null) return;

      final status = await provider.checkStatus(_paymentId!);

      if (status == "Succeeded") {
        timer.cancel();
        setState(() => isPaid = true);

        if (mounted) {
          ScaffoldMessenger.of(context).showSnackBar(
            const SnackBar(
              content: Text("Uspješno plaćeno!"),
              backgroundColor: Colors.green,
            ),
          );
          Navigator.pop(context);
        }
      }
    });
  }

  @override
  void dispose() {
    _sub?.cancel();
    super.dispose();
    _timer?.cancel();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text("Payment")),
      body: Center(
        child: isPaid
            ? Text("Already paid. Thank you!")
            : ElevatedButton(
                onPressed: startPayment,
                child: Text("Pay now ${widget.amount} ${widget.currency}"),
              ),
      ),
    );
  }
}
