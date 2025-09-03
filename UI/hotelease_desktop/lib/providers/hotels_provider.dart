import 'dart:convert';

import 'package:hotelease_mobile_new/models/hotel.dart';
import 'package:hotelease_mobile_new/models/search_result.dart';
import 'package:hotelease_mobile_new/providers/base_provider.dart';
import 'package:http/http.dart' as http;

class HotelsProvider extends BaseProvider<Hotel> {
  HotelsProvider() : super("Hotels");

  @override
  Hotel fromJson(data) {
    return Hotel.fromJson(data);
  }

  int? _currentHotelId;

  int? get currentHotelId => _currentHotelId;

  void setCurrentHotelId(int id) {
    _currentHotelId = id;
    notifyListeners();
  }

  Future<dynamic> getCustom(String endpoint) async {
    var uri = Uri.parse("$baseUrl$endpoint"); // baseUrl već koristiš
    var response = await http.get(uri, headers: await createHeaders());

    if (response.statusCode < 200 || response.statusCode > 299) {
      throw Exception("Failed: ${response.body}");
    }

    return jsonDecode(response.body);
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

  Future<List<Hotel>> getPopularHotels({int top = 5}) async {
    var response = await http.get(
      Uri.parse("${baseUrl}Hotels/popular?top=$top"),
      headers: createHeaders(),
    );

    if (response.body.isEmpty) return [];

    return (jsonDecode(response.body) as List)
        .map((e) => Hotel.fromJson(e))
        .toList();
  }

  Future<List<Hotel>> getContentBasedHotels(int hotelId, {int top = 5}) async {
    var response = await http.get(
      Uri.parse("${baseUrl}Hotels/$hotelId/content-based?top=$top"),
      headers: createHeaders(),
    );

    if (response.body.isEmpty) return [];

    return (jsonDecode(response.body) as List)
        .map((e) => Hotel.fromJson(e))
        .toList();
  }

  Future<List<Hotel>> getCollaborativeHotels(int userId, {int top = 5}) async {
    var response = await http.get(
      Uri.parse("${baseUrl}Hotels/user/$userId/collaborative?top=$top"),
      headers: createHeaders(),
    );

    if (response.body.isEmpty) return [];

    return (jsonDecode(response.body) as List)
        .map((e) => Hotel.fromJson(e))
        .toList();
  }

  Future<void> delete(int id) async {
    var uri = Uri.parse("$baseUrl$endpoint/$id");
    var response = await http.delete(uri, headers: createHeaders());

    if (!isValidResponse(response)) {
      throw Exception("Failed to delete hotel: ${response.body}");
    }
  }

  Future<Hotel> update(int id, Map<String, dynamic> payload) async {
    var url = "$baseUrl$endpoint/$id";
    var uri = Uri.parse(url);

    var response = await http.put(
      uri,
      headers: createHeaders(),
      body: jsonEncode(payload),
    );

    if (isValidResponse(response)) {
      return fromJson(jsonDecode(response.body));
    } else {
      throw Exception(
        "Update failed: ${response.statusCode} - ${response.body}",
      );
    }
  }
}
