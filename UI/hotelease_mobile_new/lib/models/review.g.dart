// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'review.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Review _$ReviewFromJson(Map<String, dynamic> json) => Review(
  id: (json['id'] as num?)?.toInt(),
  userId: (json['userId'] as num?)?.toInt(),
  hotelId: (json['hotelId'] as num?)?.toInt(),
  reservationId: (json['reservationId'] as num?)?.toInt(),
  rating: (json['rating'] as num).toInt(),
  comment: json['comment'] as String?,
  reviewDate: json['reviewDate'] == null
      ? null
      : DateTime.parse(json['reviewDate'] as String),
);

Map<String, dynamic> _$ReviewToJson(Review instance) => <String, dynamic>{
  'id': instance.id,
  'userId': instance.userId,
  'hotelId': instance.hotelId,
  'reservationId': instance.reservationId,
  'rating': instance.rating,
  'comment': instance.comment,
  'reviewDate': instance.reviewDate?.toIso8601String(),
};
