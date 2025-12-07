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

  Future<void> _showErrorPopup(String message) async {
    await showDialog(
      context: context,
      builder: (_) => AlertDialog(
        title: const Text(
          "Payment Error",
          style: TextStyle(fontWeight: FontWeight.bold),
        ),
        content: Text(message),
        actions: [
          TextButton(
            onPressed: () => Navigator.pop(context),
            child: const Text("OK"),
          ),
        ],
      ),
    );
  }

  Future<void> _startPayment() async {
    setState(() => loading = true);

    try {
      // 1. Create payment intent
      final intent = await context
          .read<PaymentsProvider>()
          .createStripePaymentIntent(
            widget.reservationId,
            widget.amount,
            currency: widget.currency,
          );

      if (intent == null || intent.clientSecret.isEmpty) {
        throw Exception("Server did not return a valid Stripe client secret.");
      }

      // 2. Init payment sheet
      await Stripe.instance.initPaymentSheet(
        paymentSheetParameters: SetupPaymentSheetParameters(
          paymentIntentClientSecret: intent.clientSecret,
          merchantDisplayName: "HotelEase",
          style: ThemeMode.dark,
        ),
      );

      // 3. Present payment sheet
      await Stripe.instance.presentPaymentSheet();

      // 4. Update backend status
      await context.read<PaymentsProvider>().updateStripeStatus(
        paymentId: intent.paymentId,
        newStatus: "succeeded",
        providerPaymentId: intent.paymentIntentId,
      );

      if (!mounted) return;

      ScaffoldMessenger.of(
        context,
      ).showSnackBar(const SnackBar(content: Text("Payment successful!")));

      Navigator.pop(context, true);
    } catch (e, st) {
      debugPrint("❌ Payment error: $e\n$st");

      String userMessage = "An unexpected error occurred during payment.";

      if (e is StripeException) {
        if (e.error.code == FailureCode.Canceled) {
          userMessage = "Payment was cancelled. You can try again.";
        } else {
          userMessage = "Payment failed. Please check your card and try again.";
        }
      } else if (e.toString().toLowerCase().contains("network")) {
        userMessage = "No internet connection. Please try again.";
      }

      await _showErrorPopup(userMessage);
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
                      "Pay now",
                      style: TextStyle(fontSize: 20),
                    ),
                  ),
                ],
              ),
            ),
    );
  }
}
