import 'dart:convert';
import 'package:hotelease_mobile/models/room.dart';
import 'package:hotelease_mobile/providers/base_provider.dart';
import 'package:http/http.dart' as http;

class RoomsProvider extends BaseProvider<Room> {
  RoomsProvider() : super("Rooms");

  @override
  Room fromJson(data) {
    return Room.fromJson(data);
  }

  Future<List<Room>> getRoomsByHotelId(int hotelId) async {
    var url = "$baseUrl$endpoint/by-hotel/$hotelId";
    var uri = Uri.parse(url);
    var headers = createHeaders();

    var response = await http.get(uri, headers: headers);

    print("Rooms API status: ${response.statusCode}");
    print("Rooms API body: ${response.body}");

    if (isValidResponse(response)) {
      var data = jsonDecode(response.body) as List;
      return data.map((e) => Room.fromJson(e)).toList();
    } else {
      throw Exception("Error loading rooms for hotel $hotelId");
    }
  }
}
