import 'dart:async';
import 'package:hotelease_mobile_new/models/payment.dart';
import 'package:hotelease_mobile_new/providers/base_provider.dart';

class PaymentsProvider extends BaseProvider<Payment> {
  PaymentsProvider() : super("Payments");
  Future<Map<String, dynamic>?> createCheckoutSession(
    int reservationId,
    double amount, {
    String currency = "usd",
  }) async {
    final rsp = await post("Payments/stripe-checkout-session", {
      "reservationId": reservationId,
      "currency": currency,
      "overrideAmount": amount,
    });
    if (rsp == null) return null;
    return rsp as Map<String, dynamic>;
  }

  Future<String?> checkStatus(int paymentId) async {
    final rsp = await getCustom("Payments/status/$paymentId");
    if (rsp == null) return null;
    return rsp["status"] as String?;
  }

  Future<String?> checkStatusByReservation(int reservationId) async {
    final rsp = await getCustom("Payments/by-reservation/$reservationId");
    if (rsp == null) return null;
    if (rsp is List && rsp.isNotEmpty) {
      final p = rsp.first;
      return p["status"] as String?;
    }
    return null;
  }

  @override
  Payment fromJson(data) {
    return Payment.fromJson(data);
  }
}
