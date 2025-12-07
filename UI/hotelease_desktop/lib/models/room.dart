import 'package:hotelease_mobile_new/models/hotel.dart';
import 'package:json_annotation/json_annotation.dart';
import 'asset.dart';

part 'room.g.dart';

@JsonSerializable()
class Room {
  int? id;
  int? hotelId;
  int? roomTypeId;
  String? name;
  int? capacity;
  double? pricePerNight;
  bool? isAvailable;
  String? description;
  List<Asset>? assets;

  Hotel? hotel;

  bool? queenBed;
  bool? wiFi;
  bool? cityView;
  bool? AC;

  String? hotelName;
  Room({
    this.hotelName,
    this.id,
    this.hotelId,
    this.roomTypeId,
    this.name,
    this.capacity,
    this.pricePerNight,
    this.isAvailable,
    this.description,
    this.assets,
    this.AC,
    this.cityView,
    this.queenBed,
    this.wiFi,
  });

  factory Room.fromJson(Map<String, dynamic> json) => _$RoomFromJson(json);
  Map<String, dynamic> toJson() => _$RoomToJson(this);
}
