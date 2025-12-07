import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:hotelease_mobile_new/models/asset.dart';
import 'package:hotelease_mobile_new/models/search_result.dart';
import 'package:hotelease_mobile_new/providers/base_provider.dart';
import 'package:hotelease_mobile_new/utils/util.dart';

import 'package:http/http.dart' as http;
import 'package:http/http.dart';

class AssetsProvider extends BaseProvider<Asset> {
  AssetsProvider() : super("Assets");

  @override
  Asset fromJson(data) {
    return Asset.fromJson(data); // koristi model Asset
  }

  Future<SearchResult<Asset>> getAssetsByHotelId(int hotelId) async {
    var url = "$baseUrl$endpoint/ByHotel/$hotelId";
    var uri = Uri.parse(url);
    var headers = createHeaders();

    var response = await http.get(uri, headers: headers);

    if (isValidResponse(response)) {
      var data = jsonDecode(response.body) as Map<String, dynamic>;

      var assetsList = data['resultList'] as List<dynamic>;

      SearchResult<Asset> result = SearchResult<Asset>();
      result.result = assetsList.map((e) => fromJson(e)).toList();
      result.count = result.result.length;

      return result;
    } else {
      throw Exception("Error loading assets for hotel $hotelId");
    }
  }

  Future<SearchResult<Asset>> getAssetsByRoomId(int roomId) async {
    var url = "$baseUrl$endpoint/ByRoom/$roomId";
    return _fetchAssets(url);
  }

  Future<SearchResult<Asset>> _fetchAssets(String url) async {
    var uri = Uri.parse(url);
    var headers = createHeaders();

    var response = await http.get(uri, headers: headers);

    if (isValidResponse(response)) {
      var data = jsonDecode(response.body) as Map<String, dynamic>;
      var assetsList = data['resultList'] as List<dynamic>;

      SearchResult<Asset> result = SearchResult<Asset>();
      result.result = assetsList.map((e) => fromJson(e)).toList();
      result.count = result.result.length;

      return result;
    } else {
      throw Exception("Error loading assets");
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
