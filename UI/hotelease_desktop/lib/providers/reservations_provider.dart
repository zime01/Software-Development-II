import 'package:hotelease_mobile_new/models/reservation.dart';
import 'package:hotelease_mobile_new/models/search_result.dart';
import 'package:hotelease_mobile_new/providers/base_provider.dart';

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
}
