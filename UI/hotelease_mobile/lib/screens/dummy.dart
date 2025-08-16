import 'package:flutter/material.dart';
import 'package:hotelease_mobile/models/hotel.dart';
import 'package:hotelease_mobile/models/search_result.dart';

class Dummy extends StatelessWidget {
  final SearchResult<Hotel>? searchResult;
  Dummy({super.key, this.searchResult});

  @override
  Widget build(BuildContext context) {
    final hotels = searchResult?.result ?? [];

    return Scaffold(
      appBar: AppBar(title: Text("Search Results")),
      body: ListView.builder(
        itemCount: hotels.length,
        itemBuilder: (context, index) {
          final hotel = hotels[index];

          return ListTile(
            title: Text(hotel.name ?? "No name"),
            subtitle: Text(hotel.address ?? "No city"),
          );
        },
      ),
    );
  }
}
