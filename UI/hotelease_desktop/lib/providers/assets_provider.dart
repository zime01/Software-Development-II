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
    return Asset.fromJson(data);
  }

  // -------------------------------
  // GET ASSETS BY HOTEL ID
  // -------------------------------
  Future<SearchResult<Asset>> getAssetsByHotelId(int hotelId) async {
    var url = "$baseUrl$endpoint/ByHotel/$hotelId";
    return _fetchAssets(url);
  }

  // -------------------------------
  // GET ASSETS BY ROOM ID
  // -------------------------------
  Future<SearchResult<Asset>> getAssetsByRoomId(int roomId) async {
    var url = "$baseUrl$endpoint/ByRoom/$roomId";
    return _fetchAssets(url);
  }

  // -------------------------------
  // REUSABLE FETCH
  // -------------------------------
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

  // -------------------------------
  // INSERT ASSET
  // -------------------------------
  Future<dynamic> insertAsset(Map<String, dynamic> data) async {
    var url = "$baseUrl$endpoint";
    var uri = Uri.parse(url);

    var headers = createHeaders();
    var body = jsonEncode(data);

    var response = await http.post(uri, headers: headers, body: body);

    if (isValidResponse(response)) {
      return jsonDecode(response.body);
    } else {
      throw Exception("Failed to upload asset");
    }
  }

  // INSERT MULTIPLE ASSETS
  Future<List<dynamic>> insertAssets(List<Map<String, dynamic>> assets) async {
    var url = "$baseUrl$endpoint/insert-assets";
    var uri = Uri.parse(url);

    var headers = createHeaders();
    var body = jsonEncode(assets);

    var response = await http.post(uri, headers: headers, body: body);

    if (isValidResponse(response)) {
      return jsonDecode(response.body);
    } else {
      throw Exception("Failed to upload assets");
    }
  }

  // -------------------------------
  // DELETE ASSET (optional)
  // -------------------------------
  Future<void> deleteAsset(int id) async {
    var url = "$baseUrl$endpoint/$id";
    var uri = Uri.parse(url);

    var headers = createHeaders();
    var response = await http.delete(uri, headers: headers);

    if (!isValidResponse(response)) {
      throw Exception("Failed to delete asset");
    }
  }

  // -------------------------------
  // HELPERS
  // -------------------------------
  bool isValidResponse(Response response) {
    if (response.statusCode < 299) return true;

    if (response.statusCode == 401) {
      throw Exception("Unauthorized");
    }
    throw Exception("Something bad happened. Try again.");
  }

  Map<String, String> createHeaders() {
    String username = Authorization.username ?? "";
    String password = Authorization.password ?? "";

    String basicAuth =
        "Basic ${base64Encode(utf8.encode('$username:$password'))}";

    return {"Content-Type": "application/json", "Authorization": basicAuth};
  }
}
