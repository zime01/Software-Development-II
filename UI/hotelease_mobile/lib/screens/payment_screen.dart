import 'package:flutter/material.dart';
import 'package:flutter_stripe/flutter_stripe.dart';
import 'package:hotelease_mobile/screens/master_screen.dart';
import 'package:provider/provider.dart';
import 'package:hotelease_mobile/providers/payments_provider.dart';
import 'package:hotelease_mobile/models/payment_intent_result.dart';

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
  bool loading = false;
  String? error;

  Future<void> _startPayment() async {
    setState(() {
      loading = true;
      error = null;
    });

    try {
      // 1) Kreiraj PaymentIntent na backendu
      final PaymentIntentResult? intent = await context
          .read<PaymentsProvider>()
          .createStripePaymentIntent(
            widget.reservationId,
            widget.amount,
            currency: widget.currency,
          );

      if (intent == null ||
          intent.clientSecret.isEmpty ||
          intent.paymentIntentId.isEmpty) {
        throw Exception("Neispravan odgovor sa servera (clientSecret/PI id).");
      }

      // 2) Potvrdi karticu kroz Stripe SDK
      await Stripe.instance.confirmPayment(
        paymentIntentClientSecret: intent.clientSecret,
        data: const PaymentMethodParams.card(
          paymentMethodData: PaymentMethodData(),
        ),
      );

      // 3) Javi backendu da ažurira status preko PaymentIntent Id
      await context.read<PaymentsProvider>().updateStripeStatus(
        intent.paymentIntentId,
      );

      if (!mounted) return;
      ScaffoldMessenger.of(
        context,
      ).showSnackBar(const SnackBar(content: Text("Plaćanje uspješno!")));
      Navigator.pop(context, true);
    } catch (e, st) {
      debugPrint("❌ Payment error: $e\n$st");
      setState(() => error = e.toString());
      if (mounted) {
        ScaffoldMessenger.of(
          context,
        ).showSnackBar(SnackBar(content: Text("Greška pri plaćanju: $e")));
      }
    } finally {
      if (mounted) setState(() => loading = false);
    }
  }

  @override
  Widget build(BuildContext context) {
    return MasterScreen(
      child: SingleChildScrollView(
        padding: const EdgeInsets.all(16),
        child: loading
            ? const Center(child: CircularProgressIndicator())
            : Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Text(
                    "Reservation #${widget.reservationId}",
                    style: const TextStyle(
                      fontSize: 18,
                      fontWeight: FontWeight.bold,
                      color: Colors.white,
                    ),
                  ),
                  const SizedBox(height: 12),
                  Text(
                    "Amount: ${widget.amount.toStringAsFixed(2)} ${widget.currency}",
                    style: const TextStyle(color: Colors.white, fontSize: 16),
                  ),
                  const SizedBox(height: 20),

                  // Stripe CardField za unos podataka
                  CardField(
                    decoration: InputDecoration(
                      hintText: 'Card details',
                      hintStyle: TextStyle(color: Colors.white70),
                      border: OutlineInputBorder(),
                      filled: true,
                      fillColor: Colors.grey[900]!,
                      contentPadding: EdgeInsets.symmetric(
                        vertical: 20,
                      ), // Dodajte padding za bolji izgled
                    ),
                    style: TextStyle(color: Colors.white, fontSize: 18),
                    onCardChanged: (card) {
                      debugPrint(
                        'Card changed: ${card?.complete}',
                      ); // Dodajte ? za null safety
                    },
                  ),

                  const SizedBox(height: 28),
                  SizedBox(
                    width: double.infinity,
                    child: ElevatedButton(
                      onPressed: _startPayment,
                      style: ElevatedButton.styleFrom(
                        backgroundColor: const Color.fromRGBO(17, 45, 78, 1),
                        padding: const EdgeInsets.symmetric(vertical: 16),
                        textStyle: const TextStyle(fontSize: 18),
                      ),
                      child: const Text(
                        "Plati sada",
                        style: TextStyle(color: Colors.white),
                      ),
                    ),
                  ),

                  if (error != null) ...[
                    const SizedBox(height: 12),
                    Text(
                      "Greška: $error",
                      style: const TextStyle(color: Colors.red, fontSize: 14),
                    ),
                  ],
                ],
              ),
      ),
    );
  }
}
