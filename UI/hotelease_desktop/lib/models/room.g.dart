// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'room.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Room _$RoomFromJson(Map<String, dynamic> json) => Room(
  hotelName: json['hotelName'] as String?,
  id: (json['id'] as num?)?.toInt(),
  hotelId: (json['hotelId'] as num?)?.toInt(),
  roomTypeId: (json['roomTypeId'] as num?)?.toInt(),
  name: json['name'] as String?,
  capacity: (json['capacity'] as num?)?.toInt(),
  pricePerNight: (json['pricePerNight'] as num?)?.toDouble(),
  isAvailable: json['isAvailable'] as bool?,
  description: json['description'] as String?,
  assets: (json['assets'] as List<dynamic>?)
      ?.map((e) => Asset.fromJson(e as Map<String, dynamic>))
      .toList(),
  ac: json['ac'] as bool?,
  cityView: json['cityView'] as bool?,
  queenBed: json['queenBed'] as bool?,
  wiFi: json['wiFi'] as bool?,
);

Map<String, dynamic> _$RoomToJson(Room instance) => <String, dynamic>{
  'id': instance.id,
  'hotelId': instance.hotelId,
  'roomTypeId': instance.roomTypeId,
  'name': instance.name,
  'capacity': instance.capacity,
  'pricePerNight': instance.pricePerNight,
  'isAvailable': instance.isAvailable,
  'description': instance.description,
  'assets': instance.assets,
  'queenBed': instance.queenBed,
  'wiFi': instance.wiFi,
  'cityView': instance.cityView,
  'ac': instance.ac,
  'hotelName': instance.hotelName,
};
