import 'package:hotelease_mobile_new/models/payment.dart';
import 'package:hotelease_mobile_new/models/room.dart';
import 'package:hotelease_mobile_new/models/user.dart';
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
  Room? room;

  User? user;
  List<Payment>? payments;

  Reservation({
    this.room,
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
    this.user,
  });

  factory Reservation.fromJson(Map<String, dynamic> json) =>
      _$ReservationFromJson(json);

  Map<String, dynamic> toJson() => _$ReservationToJson(this);
}
