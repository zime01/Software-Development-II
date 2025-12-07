import 'package:json_annotation/json_annotation.dart';

part 'payment.g.dart';

@JsonSerializable()
class Payment {
  final int? id;
  final int? reservationId;
  final String? provider;
  final String? providerPaymentId;
  final double? amount;
  final String? currency;
  final String? status;
  final DateTime? createdAt;
  final DateTime? updatedAt;

  Payment({
    required this.id,
    required this.reservationId,
    required this.provider,
    required this.providerPaymentId,
    required this.amount,
    required this.currency,
    required this.status,
    required this.createdAt,
    this.updatedAt,
  });

  factory Payment.fromJson(Map<String, dynamic> json) =>
      _$PaymentFromJson(json);
  Map<String, dynamic> toJson() => _$PaymentToJson(this);
}
