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

  Future<Map<String, dynamic>?> updateStripeStatus({
    required int paymentId,
    required String newStatus,
    String? providerPaymentId,
  }) async {
    final body = {
      "paymentId": paymentId,
      "newStatus": newStatus, // npr. "succeeded" ili "failed"
      if (providerPaymentId != null) "providerPaymentId": providerPaymentId,
    };

    final rsp = await post("Payments/update-status", body);

    if (rsp == null) return null;
    return rsp as Map<String, dynamic>;
  }
}
