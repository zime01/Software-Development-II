import 'package:flutter/material.dart';
import 'package:hotelease_mobile_new/providers/room_availability.dart';

import 'package:provider/provider.dart';
import 'package:table_calendar/table_calendar.dart';

class CalendarWidget extends StatefulWidget {
  final int roomId;
  final Function(DateTime start, DateTime end) onDateRangeSelected;
  final DateTime? initialCheckInDate;
  final DateTime? initialCheckOutDate;

  const CalendarWidget({
    super.key,
    required this.roomId,
    required this.onDateRangeSelected,
    this.initialCheckInDate,
    this.initialCheckOutDate,
  });

  @override
  State<CalendarWidget> createState() => _CalendarWidgetState();
}

class _CalendarWidgetState extends State<CalendarWidget> {
  late RoomsAvailabilityProvider _availabilityProvider;
  Map<DateTime, int> _availabilityMap = {};
  DateTime _focusedDay = DateTime.now();

  // Range selection
  DateTime? _rangeStart;
  DateTime? _rangeEnd;
  RangeSelectionMode _rangeSelectionMode = RangeSelectionMode.toggledOff;

  @override
  void initState() {
    super.initState();

    _rangeStart = widget.initialCheckInDate;
    _rangeEnd = widget.initialCheckOutDate;
    _rangeSelectionMode = (_rangeStart != null && _rangeEnd != null)
        ? RangeSelectionMode.toggledOn
        : RangeSelectionMode.toggledOff;

    _focusedDay = _rangeStart ?? DateTime.now();
  }

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
    _availabilityProvider = context.read<RoomsAvailabilityProvider>();
    _loadAvailability();
  }

  Future<void> _loadAvailability() async {
    var availability = await _availabilityProvider.getAvailabilityRooms(
      widget.roomId,
      _focusedDay.month,
      _focusedDay.year,
    );

    setState(() {
      _availabilityMap = {
        for (var a in availability)
          DateTime(a.date!.year, a.date!.month, a.date!.day): a.status ?? 0,
      };
    });
  }

  Color _getDayColor(DateTime day) {
    final status = _availabilityMap[DateTime(day.year, day.month, day.day)];
    switch (status) {
      case 0: // available
        return Colors.green.withOpacity(0.7);
      case 1: // booked
        return Colors.red.withOpacity(0.7);
      case 2: // limited
        return Colors.yellow.withOpacity(0.7);
      default:
        return Colors.grey.withOpacity(0.3);
    }
  }

  bool _isDateSelectable(DateTime day) {
    final status = _availabilityMap[DateTime(day.year, day.month, day.day)];
    return status == 0 || status == 2 || status == null;
  }

  bool _isInRange(DateTime day) {
    if (_rangeStart == null || _rangeEnd == null) return false;
    return day.isAfter(_rangeStart!) && day.isBefore(_rangeEnd!);
  }

  void _onDaySelected(DateTime selectedDay, DateTime focusedDay) {
    if (!_isDateSelectable(selectedDay)) return;

    setState(() {
      if (_rangeStart != null &&
          _rangeEnd == null &&
          selectedDay.isAfter(_rangeStart!)) {
        _rangeEnd = selectedDay;
        _rangeSelectionMode = RangeSelectionMode.toggledOn;

        widget.onDateRangeSelected(_rangeStart!, _rangeEnd!);
      } else {
        _rangeStart = selectedDay;
        _rangeEnd = null;
        _rangeSelectionMode = RangeSelectionMode.toggledOn;
      }
      _focusedDay = focusedDay;
    });
  }

  @override
  Widget build(BuildContext context) {
    return TableCalendar(
      firstDay: DateTime.utc(2020, 1, 1),
      lastDay: DateTime.utc(2030, 12, 31),
      focusedDay: _focusedDay,

      rangeStartDay: _rangeStart,
      rangeEndDay: _rangeEnd,
      rangeSelectionMode: _rangeSelectionMode,

      onDaySelected: _onDaySelected,

      onRangeSelected: (start, end, focusedDay) {
        if (start != null && !_isDateSelectable(start)) return;
        if (end != null && !_isDateSelectable(end)) return;

        setState(() {
          _rangeStart = start;
          _rangeEnd = end;
          _focusedDay = focusedDay;
          _rangeSelectionMode = RangeSelectionMode.toggledOn;

          if (_rangeStart != null && _rangeEnd != null) {
            widget.onDateRangeSelected(_rangeStart!, _rangeEnd!);
          }
        });
      },

      onPageChanged: (focusedDay) {
        _focusedDay = focusedDay;
        _loadAvailability();
      },

      calendarBuilders: CalendarBuilders(
        defaultBuilder: (context, day, _) {
          return _buildDayContainer(day, _getDayColor(day));
        },
        selectedBuilder: (context, day, focusedDay) {
          return _buildDayContainer(day, Colors.blue);
        },
        rangeStartBuilder: (context, day, focusedDay) {
          return _buildDayContainer(day, Colors.blue);
        },
        rangeEndBuilder: (context, day, focusedDay) {
          return _buildDayContainer(day, Colors.blue);
        },
        withinRangeBuilder: (context, day, focusedDay) {
          Color baseColor = _getDayColor(day);
          return _buildDayContainer(day, baseColor.withOpacity(0.5));
        },
      ),
    );
  }

  Widget _buildDayContainer(DateTime day, Color color) {
    return Container(
      decoration: BoxDecoration(shape: BoxShape.circle, color: color),
      margin: const EdgeInsets.all(6.0),
      alignment: Alignment.center,
      child: Text('${day.day}', style: const TextStyle(color: Colors.white)),
    );
  }
}
