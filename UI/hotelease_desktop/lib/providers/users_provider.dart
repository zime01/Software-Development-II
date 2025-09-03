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

  // 🔹 nova metoda da dobavi sve korisnike sa IncludeRoles
  Future<List<User>> getAll({bool includeRoles = true}) async {
    var uri = Uri.parse("$baseUrl$endpoint?IncludeRoles=$includeRoles");
    var response = await http.get(uri, headers: createHeaders());

    if (isValidResponse(response)) {
      var data = jsonDecode(response.body);
      // Ako je API paged result
      if (data is Map && data.containsKey("resultList")) {
        return (data["resultList"] as List).map((x) => fromJson(x)).toList();
      }
      return (data as List).map((x) => fromJson(x)).toList();
    } else {
      throw Exception(
        "Failed to fetch users: ${response.statusCode} - ${response.body}",
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

  Future<User> update(int id, Map<String, dynamic> payload) async {
    // 🔹 makni prazna i null polja da ne šalješ "" password
    payload.removeWhere((key, value) => value == null || value == "");

    var url = "$baseUrl$endpoint/change_password/$id";
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

  Future<void> updateStatus(int id, bool isActive) async {
    var uri = Uri.parse(
      "$baseUrl$endpoint/$id/change-status?isActive=$isActive",
    );

    var response = await http.put(uri, headers: createHeaders());

    if (!isValidResponse(response)) {
      throw Exception("Failed to update status: ${response.body}");
    }
  }

  Future<void> changeRole(int id, String role) async {
    var uri = Uri.parse("$baseUrl$endpoint/$id/change-role?role=$role");

    var response = await http.put(uri, headers: createHeaders());

    if (!isValidResponse(response)) {
      throw Exception("Failed to change role: ${response.body}");
    }
  }
}
