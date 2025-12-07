import 'package:json_annotation/json_annotation.dart';

part 'asset.g.dart';

@JsonSerializable()
class Asset {
  int? id;
  String? fileName;
  String? image;
  String? imageThumb;
  String? mimeType;
  DateTime? createdAt;
  int? hotelId;
  int? roomId;

  Asset({
    this.id,
    this.fileName,
    this.image,
    this.imageThumb,
    this.mimeType,
    this.createdAt,
    this.hotelId,
    this.roomId,
  });

  factory Asset.fromJson(Map<String, dynamic> json) => _$AssetFromJson(json);

  Map<String, dynamic> toJson({bool includeId = true}) {
    final map = {
      "fileName": fileName,
      "image": image,
      "imageThumb": imageThumb,
      "mimeType": mimeType,
      "hotelId": hotelId,
      "roomId": roomId,
      // ne šalji createdAt
    };
    if (includeId && id != null && id != 0) {
      map["id"] = id;
    }
    return map;
  }
}
