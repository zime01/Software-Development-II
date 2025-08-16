import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:hotelease_mobile/models/search_result.dart';
import 'package:hotelease_mobile/utils/util.dart';
import 'package:http/http.dart' as http;
import 'package:http/http.dart';

class BaseProvider<T> with ChangeNotifier {
  final String baseUrl = const String.fromEnvironment(
    "baseUrl",
    defaultValue: "http://10.0.2.2:5296/api/",
  );

  String endpoint = "";

  BaseProvider(this.endpoint);

  /// API koji vraća SearchResult<T>
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

  /// API koji vraća čistu listu
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

  T fromJson(data) {
    throw Exception("Method not implemented");
  }

  bool isValidResponse(Response response) {
    if (response.statusCode < 299) {
      return true;
    } else if (response.statusCode == 401) {
      throw Exception("Unauthorized");
    } else {
      throw Exception("Something bad happened please try again");
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
