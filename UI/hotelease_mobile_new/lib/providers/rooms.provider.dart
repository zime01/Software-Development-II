import 'package:hotelease_mobile_new/models/room.dart';
import 'package:hotelease_mobile_new/providers/base_provider.dart';

class RoomsProvider extends BaseProvider<Room> {
  RoomsProvider() : super("Rooms");

  @override
  Room fromJson(data) => Room.fromJson(data);

  Future<List<Room>> getRoomsByHotelId(int hotelId) async {
    return await getList("Rooms/by-hotel/$hotelId");
  }

  Future<Room?> getRoomById(int id) async {
    return await getById(id);
  }
}
