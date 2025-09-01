import 'package:hotelease_mobile_new/models/payment.dart';
import 'package:hotelease_mobile_new/models/payment_intent_result.dart';
import 'package:hotelease_mobile_new/providers/base_provider.dart';

class PaymentsProvider extends BaseProvider<Payment> {
  PaymentsProvider() : super("Payments");

  Future<String?> createCheckoutSession(
    int reservationId,
    double amount, {
    String currency = "usd",
  }) async {
    final rsp = await post("Payments/stripe-checkout-session", {
      "reservationId": reservationId,
      "currency": currency,
      "overrideAmount": amount,
    });

    if (rsp == null || rsp['url'] == null) return null;
    return rsp['url'];
  }

  /// Update statusa plaćanja na backendu
  Future<Map<String, dynamic>?> updateStripeStatus(
    String paymentIntentId,
  ) async {
    return await post("Payments/stripe-update-status/$paymentIntentId", {});
  }

  @override
  Payment fromJson(data) {
    return Payment.fromJson(data);
  }
}
