// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'role.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Role _$RoleFromJson(Map<String, dynamic> json) => Role(
  id: (json['id'] as num?)?.toInt(),
  name: json['name'] as String?,
  isDeleted: json['isDeleted'] as bool?,
  deletedTime: json['deletedTime'] == null
      ? null
      : DateTime.parse(json['deletedTime'] as String),
);

Map<String, dynamic> _$RoleToJson(Role instance) => <String, dynamic>{
  'id': instance.id,
  'name': instance.name,
  'isDeleted': instance.isDeleted,
  'deletedTime': instance.deletedTime?.toIso8601String(),
};
