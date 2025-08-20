import 'dart:convert';

import 'package:hotelease_mobile_new/models/user.dart';
import 'package:hotelease_mobile_new/providers/base_provider.dart';
import 'package:http/http.dart' as http;

class UsersProvider extends BaseProvider<User> {
  UsersProvider() : super("Users");

  @override
  User fromJson(data) {
    return User.fromJson(data);
  }

  // metoda da dobavi trenutno logovanog usera
  Future<User> getCurrentUser() async {
    var url = "$baseUrl$endpoint/me";
    var uri = Uri.parse(url);
    var headers = createHeaders();

    var response = await http.get(uri, headers: headers);

    if (isValidResponse(response)) {
      var data = jsonDecode(response.body);
      return fromJson(data);
    } else {
      throw Exception(
        "Unknown error: ${response.statusCode} - ${response.body}",
      );
    }
  }

  Future<Map<String, dynamic>> registerUser(
    Map<String, dynamic> payload,
  ) async {
    var url = "$baseUrl$endpoint/register";
    var uri = Uri.parse(url);

    var response = await http.post(
      uri,
      headers: {"Content-Type": "application/json"},
      body: jsonEncode(payload),
    );

    return jsonDecode(response.body);
  }
}
