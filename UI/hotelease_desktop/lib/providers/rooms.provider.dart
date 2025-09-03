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

  // Dobavljanje svih soba
  Future<List<Room>> getRooms() async {
    var result = await get(); // ovo vraća SearchResult<Room>
    return result.result;
  }

  // Insert soba
  Future<Room> addRoom(Room room) async {
    var json = await super.insert(room.toJson());
    return Room.fromJson(json);
  }

  Future<Room> editRoom(Room room) async {
    if (room.id == null) {
      throw Exception("Room ID is required for update");
    }
    return await super.update(room.id!, room.toJson());
  }
}
