import 'package:hotelease_mobile/models/service.dart';
import 'package:hotelease_mobile/providers/base_provider.dart';

class ServicesProvider extends BaseProvider<Service> {
  ServicesProvider() : super("Services");

  @override
  Service fromJson(data) => Service.fromJson(data);

  Future<List<Service>> getServicesByHotel(int hotelId) async {
    return await getList("Services/by-hotel/$hotelId");
  }
}
