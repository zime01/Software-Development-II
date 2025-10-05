import 'package:flutter/material.dart';
import 'package:flutter_stripe/flutter_stripe.dart';
import 'package:hotelease_mobile_new/providers/payments_provider.dart';
import 'package:hotelease_mobile_new/screens/master_screen.dart';
import 'package:provider/provider.dart';

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
      // 1. Kreiraj PaymentIntent na backendu
      final intent = await context
          .read<PaymentsProvider>()
          .createStripePaymentIntent(
            widget.reservationId,
            widget.amount,
            currency: widget.currency,
          );

      if (intent == null || intent.clientSecret.isEmpty) {
        throw Exception("Greška: server nije vratio clientSecret.");
      }

      // 2. Inicijaliziraj PaymentSheet
      await Stripe.instance.initPaymentSheet(
        paymentSheetParameters: SetupPaymentSheetParameters(
          paymentIntentClientSecret: intent.clientSecret,
          style: ThemeMode.dark,
          merchantDisplayName: "HotelEase",
          appearance: const PaymentSheetAppearance(
            colors: PaymentSheetAppearanceColors(),
          ),
        ),
      );

      // 3. Prikaži PaymentSheet
      await Stripe.instance.presentPaymentSheet();

      // 4. Update status na backendu
      await context.read<PaymentsProvider>().updateStripeStatus(
        paymentId: intent
            .paymentId, // ovo moraš da dodaš u PaymentIntentResult model da ti backend vrati
        newStatus: "succeeded",
        providerPaymentId: intent.paymentIntentId,
      );

      if (!mounted) return;
      ScaffoldMessenger.of(
        context,
      ).showSnackBar(const SnackBar(content: Text("✅ Plaćanje uspješno!")));
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
      child: loading
          ? const Center(child: CircularProgressIndicator())
          : Padding(
              padding: const EdgeInsets.all(24),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.stretch,
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  Row(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      Text(
                        "Reservation: ",
                        textAlign: TextAlign.center,
                        style: TextStyle(
                          fontSize: 18,
                          fontWeight: FontWeight.bold,
                          color: Theme.of(context).colorScheme.onPrimary,
                        ),
                      ),
                      Text(
                        "#${widget.reservationId}",
                        style: TextStyle(
                          fontSize: 20,
                          fontWeight: FontWeight.bold,
                          color: Theme.of(context).colorScheme.onPrimary,
                        ),
                      ),
                    ],
                  ),
                  const SizedBox(height: 16),
                  Row(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      Text(
                        "Amount: ",
                        textAlign: TextAlign.center,
                        style: TextStyle(
                          fontSize: 18,
                          color: Theme.of(context).colorScheme.onPrimary,
                        ),
                      ),
                      Text(
                        "${widget.amount.toStringAsFixed(2)} ${widget.currency.toUpperCase()}",
                        style: TextStyle(
                          fontSize: 20,
                          fontWeight: FontWeight.bold,
                          color: Theme.of(context).colorScheme.onPrimary,
                        ),
                      ),
                    ],
                  ),
                  const SizedBox(height: 40),
                  ElevatedButton(
                    onPressed: _startPayment,
                    style: ElevatedButton.styleFrom(
                      backgroundColor: const Color(0xFF112D4E),
                      foregroundColor: Colors.white,
                      padding: const EdgeInsets.symmetric(vertical: 18),
                      shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(12),
                        side: const BorderSide(color: Colors.white),
                      ),
                    ),
                    child: const Text(
                      "Plati sada",
                      style: TextStyle(fontSize: 20),
                    ),
                  ),
                  if (error != null) ...[
                    const SizedBox(height: 20),
                    Text(
                      "Greška: $error",
                      textAlign: TextAlign.center,
                      style: const TextStyle(
                        color: Colors.redAccent,
                        fontSize: 14,
                      ),
                    ),
                  ],
                ],
              ),
            ),
    );
  }
}
