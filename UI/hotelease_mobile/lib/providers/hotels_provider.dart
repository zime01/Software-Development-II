import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:hotelease_mobile/models/hotel.dart';
import 'package:hotelease_mobile/utils/util.dart';

import 'package:http/http.dart' as http;
import 'package:http/http.dart';

class HotelsProvider with ChangeNotifier {
  static String? _baseUrl;
  String _endpoint = "Hotels";

  HotelsProvider() {
    _baseUrl = const String.fromEnvironment(
      "baseUrl",
      defaultValue: "http://10.0.2.2:5296/api/",
    );
  }

  Future<List<Hotel>> get() async {
    var url = "$_baseUrl$_endpoint";

    var uri = Uri.parse(url);
    var headers = createHeaders();
    var response = await http.get(uri, headers: headers);

    if (isValidResponse(response)) {
      var data = jsonDecode(response.body);

      print("response: ${response.statusCode} ${response.body}");

      print(data);

      var result = data["resultList"].map((e) => Hotel.fromJson(e));

      return data;
    } else {
      throw new Exception("Unknown error");
    }
  }

  bool isValidResponse(Response response) {
    if (response.statusCode < 299) {
      return true;
    } else if (response.statusCode == 401) {
      throw new Exception("Unauthorized");
    } else {
      throw new Exception("Something bad happened please try again");
    }
  }

  Map<String, String> createHeaders() {
    String username = Authorization.username ?? "";
    String password = Authorization.password ?? "";

    String basicAuth =
        "Basic ${base64Encode(utf8.encode('$username:$password'))}";

    var headers = {
      "Content-Type": "application/json",
      "Authorization": basicAuth,
    };

    return headers;
  }
}
