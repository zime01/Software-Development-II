import 'package:hotelease_mobile_new/providers/base_provider.dart';

class DashboardProvider extends BaseProvider<Map<String, dynamic>> {
  DashboardProvider() : super("dashboard");

  @override
  Map<String, dynamic> fromJson(data) {
    return data; // direktan map jer vraćaš kombinovani JSON
  }

  Future<Map<String, dynamic>> getManagerDashboard(int hotelId) async {
    return await getCustom("dashboard/manager/$hotelId");
  }
}
