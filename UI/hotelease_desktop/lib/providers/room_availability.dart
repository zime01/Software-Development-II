import 'package:hotelease_mobile_new/models/room_availability.dart';
import 'package:hotelease_mobile_new/providers/base_provider.dart';

class RoomsAvailabilityProvider extends BaseProvider<RoomAvailability> {
  RoomsAvailabilityProvider() : super("RoomsAvailability");

  @override
  RoomAvailability fromJson(data) => RoomAvailability.fromJson(data);

  Future<List<RoomAvailability>> getAvailabilityRooms(
    int roomId,
    int month,
    int year,
  ) async {
    return await getList(
      "RoomsAvailability/color-coded?roomId=$roomId&month=$month&year=$year",
    );
  }
}
