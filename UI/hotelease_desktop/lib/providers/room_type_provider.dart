import 'dart:convert';
import 'package:hotelease_mobile_new/models/room_type.dart';
import 'package:hotelease_mobile_new/providers/base_provider.dart';
import 'package:http/http.dart' as http;

class RoomTypesProvider extends BaseProvider<RoomType> {
  RoomTypesProvider() : super("RoomTypes");

  @override
  RoomType fromJson(data) {
    return RoomType.fromJson(data);
  }

  Future<RoomType> insertRoomType(Map<String, dynamic> payload) async {
    var uri = Uri.parse("$baseUrl$endpoint");
    var response = await http.post(
      uri,
      headers: createHeaders(),
      body: jsonEncode(payload),
    );

    if (isValidResponse(response)) {
      return fromJson(jsonDecode(response.body));
    } else {
      throw Exception("Failed to insert RoomType: ${response.body}");
    }
  }

  Future<RoomType> updateRoomType(int id, Map<String, dynamic> payload) async {
    var uri = Uri.parse("$baseUrl$endpoint/$id");
    var response = await http.put(
      uri,
      headers: createHeaders(),
      body: jsonEncode(payload),
    );

    if (isValidResponse(response)) {
      return fromJson(jsonDecode(response.body));
    } else {
      throw Exception("Failed to update RoomType: ${response.body}");
    }
  }

  Future<void> deleteRoomType(int id) async {
    var uri = Uri.parse("$baseUrl$endpoint/$id");
    var response = await http.delete(uri, headers: createHeaders());

    if (!isValidResponse(response)) {
      throw Exception("Failed to delete RoomType: ${response.body}");
    }
  }
}
