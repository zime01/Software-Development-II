// providers/hotels_provider.dart
import 'package:hotelease_mobile/models/hotel.dart';
import 'package:hotelease_mobile/models/search_result.dart';
import 'package:hotelease_mobile/providers/base_provider.dart';

class HotelsProvider extends BaseProvider<Hotel> {
  HotelsProvider() : super("Hotels");

  @override
  Hotel fromJson(data) {
    return Hotel.fromJson(data);
  }

  Future<SearchResult<Hotel>> searchAvailableHotels({
    String? cityName,
    int? adults,
    int? rooms,
    DateTime? checkIn,
    DateTime? checkOut,
    double? minPrice,
    double? maxPrice,
    bool? wifi,
    bool? parking,
    bool? pool,
    bool? bar,
    bool? fitness,
    bool? spa,
    String? sortBy,
  }) async {
    var queryParams = Map<String, String>();

    if (cityName != null && cityName.isNotEmpty)
      queryParams['CityName'] = cityName;
    if (adults != null) queryParams['Adults'] = adults.toString();
    if (rooms != null) queryParams['Rooms'] = rooms.toString();
    if (checkIn != null) queryParams['CheckIn'] = checkIn.toIso8601String();
    if (checkOut != null) queryParams['CheckOut'] = checkOut.toIso8601String();
    if (minPrice != null) queryParams['MinPrice'] = minPrice.toString();
    if (maxPrice != null) queryParams['MaxPrice'] = maxPrice.toString();
    if (wifi != null) queryParams['WiFi'] = wifi.toString();
    if (parking != null) queryParams['Parking'] = parking.toString();
    if (pool != null) queryParams['Pool'] = pool.toString();
    if (sortBy != null) queryParams['SortBy'] = sortBy;

    var response = await get(filter: queryParams);
    return response;
  }
}
