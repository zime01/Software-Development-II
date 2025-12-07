import 'dart:convert';

import 'package:hotelease_mobile_new/models/reservation.dart';
import 'package:hotelease_mobile_new/models/search_result.dart';
import 'package:hotelease_mobile_new/providers/base_provider.dart';
import 'package:http/http.dart' as http;

class ReservationsProvider extends BaseProvider<Reservation> {
  ReservationsProvider() : super("Reservations");

  Future<Reservation> createReservation(Map<String, dynamic> payload) async {
    var response = await post("Reservations/insert-reservation", payload);
    return fromJson(response);
  }

  @override
  Reservation fromJson(data) {
    return Reservation.fromJson(data);
  }

  Future<List<Reservation>> getMyReservations() async {
    SearchResult<Reservation> result = await super.get();
    return result.result; // vraća čistu listu
  }

  Future<List<Reservation>> getByDate(DateTime date) async {
    final formatted = date.toIso8601String().split("T").first;
    final url = "${baseUrl}Reservations/by-date?date=$formatted";

    final response = await http.get(Uri.parse(url), headers: createHeaders());

    if (!isValidResponse(response)) {
      throw Exception("Failed to load reservations for date");
    }

    final decoded = jsonDecode(response.body) as List;
    final list = decoded.map((e) => Reservation.fromJson(e)).toList();
    return list;
  }

  // Refresh reservations for a given date (used in pull-to-refresh)
  Future<List<Reservation>> refreshByDate(DateTime date) async {
    return getByDate(date);
  }

  Future<Reservation> updateReservation(Reservation reservation) async {
    final payload = {
      "Id": reservation.id,
      "UserId": reservation.userId,
      "RoomId": reservation.roomId,
      "CheckInDate": reservation.checkInDate?.toIso8601String(),
      "CheckOutDate": reservation.checkOutDate?.toIso8601String(),
      "TotalPrice": reservation.totalPrice,
      "Status": reservation.status,
      "CreatedAt": reservation.createdAt?.toIso8601String(),
      "IsDeleted": reservation.isDeleted,
      "DeletedTime": reservation.deletedTime?.toIso8601String(),
    };

    var url =
        "${baseUrl}Reservations/update-reservation"; // ili update endpoint ako postoji
    var response = await http.put(
      Uri.parse(url),
      headers: createHeaders(),
      body: jsonEncode(payload),
    );

    if (isValidResponse(response)) {
      return Reservation.fromJson(jsonDecode(response.body));
    }

    throw Exception("Failed to update reservation");
  }
}
