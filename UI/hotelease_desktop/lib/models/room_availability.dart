class RoomAvailability {
  int? roomId;
  DateTime? date;
  int? status; // 0 = available, 1 = booked, 2 = limited

  RoomAvailability({this.roomId, this.date, this.status});

  factory RoomAvailability.fromJson(Map<String, dynamic> json) {
    return RoomAvailability(
      roomId: json['roomId'] as int?,
      date: DateTime.parse(json['date']),
      status: json['status'] as int?,
    );
  }
}
