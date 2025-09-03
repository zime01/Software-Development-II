import 'package:json_annotation/json_annotation.dart';

part 'room_type.g.dart';

@JsonSerializable()
class RoomType {
  int? id;
  String? name;
  String? description;
  bool? isDeleted;
  DateTime? deletedTime;

  RoomType({
    this.id,
    this.name,
    this.description,
    this.isDeleted,
    this.deletedTime,
  });

  factory RoomType.fromJson(Map<String, dynamic> json) =>
      _$RoomTypeFromJson(json);

  Map<String, dynamic> toJson() => _$RoomTypeToJson(this);
}
