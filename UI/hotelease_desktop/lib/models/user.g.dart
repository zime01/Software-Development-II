// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'user.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

User _$UserFromJson(Map<String, dynamic> json) =>
    User(
        id: (json['id'] as num?)?.toInt(),
        username: json['username'] as String?,
        email: json['email'] as String?,
        createdAt: json['createdAt'] == null
            ? null
            : DateTime.parse(json['createdAt'] as String),
        firstName: json['firstName'] as String?,
        isActive: json['isActive'] as bool?,
        lastLoginAt: json['lastLoginAt'] == null
            ? null
            : DateTime.parse(json['lastLoginAt'] as String),
        lastName: json['lastName'] as String?,
        phoneNumber: json['phoneNumber'] as String?,
      )
      ..roles = (json['roles'] as List<dynamic>?)
          ?.map((e) => Role.fromJson(e as Map<String, dynamic>))
          .toList();

Map<String, dynamic> _$UserToJson(User instance) => <String, dynamic>{
  'id': instance.id,
  'firstName': instance.firstName,
  'lastName': instance.lastName,
  'email': instance.email,
  'username': instance.username,
  'phoneNumber': instance.phoneNumber,
  'isActive': instance.isActive,
  'createdAt': instance.createdAt?.toIso8601String(),
  'lastLoginAt': instance.lastLoginAt?.toIso8601String(),
  'roles': instance.roles,
};
