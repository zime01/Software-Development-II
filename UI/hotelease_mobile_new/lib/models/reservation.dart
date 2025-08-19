import 'package:json_annotation/json_annotation.dart';

part 'reservation.g.dart';

@JsonSerializable()
class Reservation {
  int? id;
  int? userId;
  int? roomId;
  DateTime? checkInDate;
  DateTime? checkOutDate;
  double? totalPrice;
  String? status;
  DateTime? createdAt;
  bool? isDeleted;
  DateTime? deletedTime;

  Reservation({
    this.id,
    this.userId,
    this.roomId,
    this.checkInDate,
    this.checkOutDate,
    this.totalPrice,
    this.status,
    this.createdAt,
    this.isDeleted,
    this.deletedTime,
  });

  factory Reservation.fromJson(Map<String, dynamic> json) =>
      _$ReservationFromJson(json);

  Map<String, dynamic> toJson() => _$ReservationToJson(this);
}
