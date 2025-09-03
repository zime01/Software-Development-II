// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'room_type.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

RoomType _$RoomTypeFromJson(Map<String, dynamic> json) => RoomType(
  id: (json['id'] as num?)?.toInt(),
  name: json['name'] as String?,
  description: json['description'] as String?,
  isDeleted: json['isDeleted'] as bool?,
  deletedTime: json['deletedTime'] == null
      ? null
      : DateTime.parse(json['deletedTime'] as String),
);

Map<String, dynamic> _$RoomTypeToJson(RoomType instance) => <String, dynamic>{
  'id': instance.id,
  'name': instance.name,
  'description': instance.description,
  'isDeleted': instance.isDeleted,
  'deletedTime': instance.deletedTime?.toIso8601String(),
};
