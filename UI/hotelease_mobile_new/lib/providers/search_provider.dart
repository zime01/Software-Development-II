import 'package:flutter/material.dart';

class SearchParams {
  final DateTime checkInDate;
  final DateTime checkOutDate;
  final int guests;
  final List<int> selectedServiceIds;

  SearchParams({
    required this.checkInDate,
    required this.checkOutDate,
    required this.guests,
    this.selectedServiceIds = const [],
  });
}

class SearchProvider with ChangeNotifier {
  SearchParams? _params;

  SearchParams? get params => _params;

  void setParams(SearchParams params) {
    _params = params;
    notifyListeners();
  }

  void clearParams() {
    _params = null;
    notifyListeners();
  }
}
