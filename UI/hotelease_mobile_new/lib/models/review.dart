import 'package:json_annotation/json_annotation.dart';

part 'review.g.dart';

@JsonSerializable()
class Review {
  int? id;
  int? userId;
  int? hotelId;
  int? reservationId;
  int rating;
  String? comment;
  DateTime? reviewDate;

  Review({
    this.id,
    this.userId,
    this.hotelId,
    this.reservationId,
    required this.rating,
    this.comment,
    this.reviewDate,
  });

  factory Review.fromJson(Map<String, dynamic> json) => _$ReviewFromJson(json);

  Map<String, dynamic> toJson() => _$ReviewToJson(this);
}
