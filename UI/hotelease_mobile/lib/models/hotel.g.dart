// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'hotel.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Hotel _$HotelFromJson(Map<String, dynamic> json) => Hotel()
  ..Id = (json['Id'] as num?)?.toInt()
  ..name = json['name'] as String?;

Map<String, dynamic> _$HotelToJson(Hotel instance) => <String, dynamic>{
  'Id': instance.Id,
  'name': instance.name,
};
