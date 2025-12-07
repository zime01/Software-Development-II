import 'package:hotelease_mobile_new/models/city.dart';
import 'package:hotelease_mobile_new/providers/base_provider.dart';

class CitiesProvider extends BaseProvider<City> {
  CitiesProvider() : super("Cities");

  @override
  City fromJson(data) {
    return City.fromJson(data);
  }
}
