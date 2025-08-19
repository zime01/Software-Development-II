import 'package:hotelease_mobile_new/models/payment.dart';
import 'package:hotelease_mobile_new/models/payment_intent_result.dart';
import 'package:hotelease_mobile_new/providers/base_provider.dart';

class PaymentsProvider extends BaseProvider<Payment> {
  PaymentsProvider() : super("Payments");

  Future<PaymentIntentResult?> createStripePaymentIntent(
    int reservationId,
    double amount, {
    String currency = "usd",
  }) async {
    // Add "Payments/" before the endpoint
    final rsp = await post("Payments/stripe-create-intent", {
      "reservationId": reservationId,
      "amount": amount,
      "currency": currency,
    });

    if (rsp == null) return null;
    return PaymentIntentResult.fromJson(rsp);
  }

  Future<Map<String, dynamic>?> updateStripeStatus(
    String paymentIntentId,
  ) async {
    // Add "Payments/" before the endpoint
    return await post("Payments/stripe-update-status/$paymentIntentId", {});
  }
}
