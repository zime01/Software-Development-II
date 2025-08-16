import 'dart:convert';
import 'package:hotelease_mobile/models/room_availability.dart';
import 'package:hotelease_mobile/providers/base_provider.dart';
import 'package:http/http.dart' as http;

class RoomsAvailabilityProvider extends BaseProvider<RoomAvailability> {
  RoomsAvailabilityProvider() : super("RoomsAvailability");

  @override
  RoomAvailability fromJson(data) {
    return RoomAvailability.fromJson(data);
  }

  Future<List<RoomAvailability>> getAvailabilityRooms(
    int roomId,
    int month,
    int year,
  ) async {
    var url = Uri.parse(
      "${baseUrl}RoomsAvailability/color-coded?roomId=$roomId&month=$month&year=$year",
    );

    var response = await http.get(url, headers: createHeaders());
    if (response.statusCode >= 200 && response.statusCode < 300) {
      var data = jsonDecode(response.body) as List;
      return data.map((e) => RoomAvailability.fromJson(e)).toList();
    } else {
      throw Exception(
        "Failed to load room availability. Status: ${response.statusCode}, Body: ${response.body}",
      );
    }
  }
}
