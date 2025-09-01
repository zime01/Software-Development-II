class PaymentIntentResult {
  final String clientSecret;
  final String paymentIntentId;
  final int paymentId;

  PaymentIntentResult({
    required this.clientSecret,
    required this.paymentIntentId,
    required this.paymentId,
  });

  factory PaymentIntentResult.fromJson(Map<String, dynamic> json) {
    return PaymentIntentResult(
      clientSecret: json['clientSecret'] ?? "",
      paymentIntentId: json['paymentIntentId'] ?? "",
      paymentId: (json['paymentId'] as num?)?.toInt() ?? 0,
    );
  }
}
