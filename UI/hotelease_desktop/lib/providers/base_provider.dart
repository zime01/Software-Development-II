import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:hotelease_mobile_new/models/search_result.dart';
import 'package:hotelease_mobile_new/utils/util.dart';

import 'package:http/http.dart' as http;
import 'package:http/http.dart';

class BaseProvider<T> with ChangeNotifier {
  final String baseUrl = const String.fromEnvironment(
    "baseUrl",
    //defaultValue: "http://localhost:5296/api/",
    defaultValue: "http://192.168.0.13:5000/api/",
  );

  String endpoint = "";

  BaseProvider(this.endpoint);

  Future<SearchResult<T>> get({dynamic filter}) async {
    var url = "$baseUrl$endpoint";

    if (filter != null) {
      var queryString = getQueryString(filter);
      url = "$url?$queryString";
    }

    var uri = Uri.parse(url);
    var headers = createHeaders();
    var response = await http.get(uri, headers: headers);

    if (isValidResponse(response)) {
      var data = jsonDecode(response.body);

      SearchResult<T> result = SearchResult();
      result.count = data["count"];
      for (var item in data['resultList']) {
        result.result.add(fromJson(item));
      }
      return result;
    } else {
      throw Exception("Unknown error");
    }
  }

  Future<List<T>> getList(String endpointPath) async {
    var uri = Uri.parse("$baseUrl$endpointPath");
    var headers = createHeaders();
    var response = await http.get(uri, headers: headers);

    if (isValidResponse(response)) {
      var data = jsonDecode(response.body) as List;
      return data.map((e) => fromJson(e)).toList();
    } else {
      throw Exception("Unknown error");
    }
  }

  Future<dynamic> post(String endpoint, dynamic payload) async {
    var url = Uri.parse("$baseUrl$endpoint");
    var response = await http.post(
      url,
      headers: createHeaders(),
      body: jsonEncode(payload),
    );

    if (response.statusCode >= 200 && response.statusCode < 300) {
      if (response.body.isEmpty) {
        return null; // ✅ spriječava FormatException
      }
      return jsonDecode(response.body);
    } else {
      throw Exception("Error: ${response.statusCode} ${response.reasonPhrase}");
    }
  }

  T fromJson(data) {
    throw Exception("Method not implemented");
  }

  Future<dynamic> getCustom(String endpoint) async {
    var uri = Uri.parse("$baseUrl$endpoint");
    var response = await http.get(uri, headers: createHeaders());

    if (response.statusCode < 200 || response.statusCode > 299) {
      throw Exception("Greška pri GET custom: ${response.body}");
    }

    return jsonDecode(response.body);
  }

  Future<dynamic> getById(int id) async {
    var uri = Uri.parse("$baseUrl$endpoint/details/$id");
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

  Future<dynamic> insert(dynamic request) async {
    var uri = Uri.parse("$baseUrl$endpoint");
    var response = await http.post(
      uri,
      headers: createHeaders(),
      body: jsonEncode(request),
    );

    if (response.statusCode < 200 || response.statusCode > 299) {
      throw Exception("Greška pri POST: ${response.body}");
    }

    if (response.body.isEmpty) {
      return null;
    }

    return jsonDecode(response.body);
  }

  bool isValidResponse(Response response) {
    if (response.statusCode >= 200 && response.statusCode <= 299) {
      return true;
    } else if (response.statusCode == 401) {
      throw Exception("Unauthorized: ${response.body}");
    } else {
      throw Exception("Error: ${response.statusCode} ${response.body}");
    }
  }

  Map<String, String> createHeaders() {
    String username = Authorization.username ?? "";
    String password = Authorization.password ?? "";

    String basicAuth =
        "Basic ${base64Encode(utf8.encode('$username:$password'))}";

    return {"Content-Type": "application/json", "Authorization": basicAuth};
  }

  String getQueryString(
    Map params, {
    String prefix = '&',
    bool inRecursion = false,
  }) {
    String query = '';
    params.forEach((key, value) {
      if (inRecursion) {
        if (key is int) {
          key = '[$key]';
        } else {
          key = '.$key';
        }
      }
      if (value is String || value is int || value is double || value is bool) {
        var encoded = value is String ? Uri.encodeComponent(value) : value;
        query += '$prefix$key=$encoded';
      } else if (value is DateTime) {
        query += '$prefix$key=${value.toIso8601String()}';
      } else if (value is List || value is Map) {
        if (value is List) value = value.asMap();
        value.forEach((k, v) {
          query += getQueryString(
            {k: v},
            prefix: '$prefix$key',
            inRecursion: true,
          );
        });
      }
    });
    return query;
  }
}
