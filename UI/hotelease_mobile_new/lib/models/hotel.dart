import 'package:json_annotation/json_annotation.dart';

part 'hotel.g.dart';

@JsonSerializable()
class Hotel {
  int? id;
  String? name;
  int? price;
  String? description;
  String? address;

  int? starRating;
  List<dynamic>? assets;
  List<dynamic>? rooms;

  bool? wifi;
  bool? parking;
  bool? pool;
  bool? bar;
  bool? fitness;
  bool? spa;
  Hotel({
    this.id,
    this.name,
    this.price,
    this.description,
    this.address,

    this.starRating,
    this.assets,
    this.rooms,
    this.bar,
    this.fitness,
    this.parking,
    this.pool,
    this.spa,
    this.wifi,
  });

  factory Hotel.fromJson(Map<String, dynamic> json) => _$HotelFromJson(json);

  Map<String, dynamic> toJson() => _$HotelToJson(this);
}
