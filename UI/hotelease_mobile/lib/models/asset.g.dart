// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'asset.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Asset _$AssetFromJson(Map<String, dynamic> json) => Asset(
  id: (json['id'] as num?)?.toInt(),
  fileName: json['fileName'] as String?,
  image: json['image'] as String?,
  imageThumb: json['imageThumb'] as String?,
  mimeType: json['mimeType'] as String?,
  createdAt: json['createdAt'] == null
      ? null
      : DateTime.parse(json['createdAt'] as String),
  hotelId: (json['hotelId'] as num?)?.toInt(),
  roomId: (json['roomId'] as num?)?.toInt(),
);

Map<String, dynamic> _$AssetToJson(Asset instance) => <String, dynamic>{
  'id': instance.id,
  'fileName': instance.fileName,
  'image': instance.image,
  'imageThumb': instance.imageThumb,
  'mimeType': instance.mimeType,
  'createdAt': instance.createdAt?.toIso8601String(),
  'hotelId': instance.hotelId,
  'roomId': instance.roomId,
};
