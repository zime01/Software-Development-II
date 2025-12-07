import 'package:hotelease_mobile_new/models/asset.dart';
import 'package:hotelease_mobile_new/models/city.dart';
import 'package:hotelease_mobile_new/models/room.dart';
import 'package:json_annotation/json_annotation.dart';

part 'hotel.g.dart';

@JsonSerializable()
class Hotel {
  int? id;
  String? name;
  int? price;
  String? description;
  String? address;

  int? cityId;

  int? starRating;
  List<Asset>? assets;
  List<Room>? rooms;

  bool? wifi;
  bool? parking;
  bool? pool;
  bool? bar;
  bool? fitness;
  bool? spa;
  String? imageUrl;

  int? minPrice;
  int? maxPrice;
  City? city;
  Hotel({
    this.imageUrl,
    this.id,
    this.name,
    this.price,
    this.description,
    this.address,
    this.cityId,

    this.starRating,
    this.assets,
    this.rooms,
    this.bar,
    this.fitness,
    this.parking,
    this.pool,
    this.spa,
    this.wifi,

    this.minPrice,
    this.maxPrice,
    this.city,
  });

  factory Hotel.fromJson(Map<String, dynamic> json) => _$HotelFromJson(json);

  Map<String, dynamic> toJson() => _$HotelToJson(this);
}
