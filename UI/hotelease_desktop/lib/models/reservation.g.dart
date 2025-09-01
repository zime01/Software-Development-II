// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'reservation.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Reservation _$ReservationFromJson(Map<String, dynamic> json) => Reservation(
  room: json['room'] == null
      ? null
      : Room.fromJson(json['room'] as Map<String, dynamic>),
  id: (json['id'] as num?)?.toInt(),
  userId: (json['userId'] as num?)?.toInt(),
  roomId: (json['roomId'] as num?)?.toInt(),
  checkInDate: json['checkInDate'] == null
      ? null
      : DateTime.parse(json['checkInDate'] as String),
  checkOutDate: json['checkOutDate'] == null
      ? null
      : DateTime.parse(json['checkOutDate'] as String),
  totalPrice: (json['totalPrice'] as num?)?.toDouble(),
  status: json['status'] as String?,
  createdAt: json['createdAt'] == null
      ? null
      : DateTime.parse(json['createdAt'] as String),
  isDeleted: json['isDeleted'] as bool?,
  deletedTime: json['deletedTime'] == null
      ? null
      : DateTime.parse(json['deletedTime'] as String),
);

Map<String, dynamic> _$ReservationToJson(Reservation instance) =>
    <String, dynamic>{
      'id': instance.id,
      'userId': instance.userId,
      'roomId': instance.roomId,
      'checkInDate': instance.checkInDate?.toIso8601String(),
      'checkOutDate': instance.checkOutDate?.toIso8601String(),
      'totalPrice': instance.totalPrice,
      'status': instance.status,
      'createdAt': instance.createdAt?.toIso8601String(),
      'isDeleted': instance.isDeleted,
      'deletedTime': instance.deletedTime?.toIso8601String(),
      'room': instance.room,
    };
