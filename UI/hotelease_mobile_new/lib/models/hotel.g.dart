// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'hotel.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Hotel _$HotelFromJson(Map<String, dynamic> json) => Hotel(
  imageUrl: json['imageUrl'] as String?,
  id: (json['id'] as num?)?.toInt(),
  name: json['name'] as String?,
  price: (json['price'] as num?)?.toInt(),
  description: json['description'] as String?,
  address: json['address'] as String?,
  starRating: (json['starRating'] as num?)?.toInt(),
  assets: (json['assets'] as List<dynamic>?)
      ?.map((e) => Asset.fromJson(e as Map<String, dynamic>))
      .toList(),
  rooms: (json['rooms'] as List<dynamic>?)
      ?.map((e) => Room.fromJson(e as Map<String, dynamic>))
      .toList(),
  bar: json['bar'] as bool?,
  fitness: json['fitness'] as bool?,
  parking: json['parking'] as bool?,
  pool: json['pool'] as bool?,
  spa: json['spa'] as bool?,
  wifi: json['wifi'] as bool?,
);

Map<String, dynamic> _$HotelToJson(Hotel instance) => <String, dynamic>{
  'id': instance.id,
  'name': instance.name,
  'price': instance.price,
  'description': instance.description,
  'address': instance.address,
  'starRating': instance.starRating,
  'assets': instance.assets,
  'rooms': instance.rooms,
  'wifi': instance.wifi,
  'parking': instance.parking,
  'pool': instance.pool,
  'bar': instance.bar,
  'fitness': instance.fitness,
  'spa': instance.spa,
  'imageUrl': instance.imageUrl,
};
