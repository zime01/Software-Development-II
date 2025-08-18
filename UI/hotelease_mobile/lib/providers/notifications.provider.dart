import 'package:hotelease_mobile/models/notification.dart';
import 'package:hotelease_mobile/providers/base_provider.dart';
import 'package:intl/intl.dart';

class NotificationsProvider extends BaseProvider<Notification> {
  NotificationsProvider() : super("Notifications");

  Future<void> sendReservationCreated({
    required int userId,
    required String email,
    required String hotelName,
    required String? roomName,
    required DateTime checkIn,
    required DateTime checkOut,
  }) async {
    final payload = {
      "userId": userId,
      "message": {
        "type": "email",
        "to": email,
        "subject": "Reservation Confirmed",
        "body":
            "Your reservation has been successfully created.\n\n"
            "Hotel: $hotelName\n"
            "Room: $roomName\n"
            "Check-in: ${DateFormat('dd.MM.yyyy').format(checkIn)}\n"
            "Check-out: ${DateFormat('dd.MM.yyyy').format(checkOut)}",
      },
    };

    await post("Notifications/rabbit-mq", payload);
  }

  @override
  Notification fromJson(data) {
    return Notification.fromJson(data);
  }
}
