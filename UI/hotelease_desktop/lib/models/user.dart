import 'package:hotelease_mobile_new/models/role.dart';
import 'package:json_annotation/json_annotation.dart';

part 'user.g.dart';

@JsonSerializable()
class User {
  int? id;
  String? firstName;
  String? lastName;
  String? email;
  String? username;
  String? phoneNumber;
  bool? isActive;
  DateTime? createdAt;
  DateTime? lastLoginAt;
  List<Role>? roles;
  User({
    required this.id,
    required this.username,
    required this.email,
    this.createdAt,
    this.firstName,
    this.isActive,
    this.lastLoginAt,
    this.lastName,
    this.phoneNumber,
  });

  factory User.fromJson(Map<String, dynamic> json) => _$UserFromJson(json);
  Map<String, dynamic> toJson() => _$UserToJson(this);
}
