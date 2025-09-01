import 'package:hotelease_mobile_new/models/review.dart';
import 'package:hotelease_mobile_new/providers/base_provider.dart';

class ReviewsProvider extends BaseProvider<Review> {
  ReviewsProvider() : super("Reviews");

  @override
  Review fromJson(data) {
    return Review.fromJson(data);
  }

  Future<List<Review>> getByHotelId(int hotelId) async {
    var response = await getCustom("Reviews/by-hotel/$hotelId");
    return (response as List).map((e) => Review.fromJson(e)).toList();
  }

  Future<Review?> addReview(Review review) async {
    var response = await insert(review.toJson());
    return Review.fromJson(response);
  }
}
