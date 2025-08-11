import 'package:flutter/material.dart';
import 'package:hotelease_mobile/providers/hotels_provider.dart';
import 'package:hotelease_mobile/screens/hotels_details_screen.dart';
import 'package:hotelease_mobile/screens/master_screen.dart';
import 'package:provider/provider.dart';

class HotelsListScreen extends StatefulWidget {
  const HotelsListScreen({super.key});

  @override
  State<HotelsListScreen> createState() => _HotelsListScreenState();
}

class _HotelsListScreenState extends State<HotelsListScreen> {
  late HotelsProvider _hotelsProvider;
  dynamic result;

  @override
  void didChangeDependencies() {
    // TODO: implement didChangeDependencies
    super.didChangeDependencies();
    _hotelsProvider = context.read<HotelsProvider>();
  }

  @override
  Widget build(BuildContext context) {
    return MasterScreen(
      title: "Hotels List",
      child: Container(
        child: Column(children: [Text("HotelsList"), _buildSearch()]),
      ),
    );
  }

  Widget _buildSearch() {
    return Padding(
      padding: const EdgeInsets.all(8.0),
      child: Row(
        children: [
          Expanded(
            child: TextField(decoration: InputDecoration(labelText: "Name")),
          ),
          Expanded(child: Text("daddada")),
          ElevatedButton(
            onPressed: () async {
              print("Called");
              result = await _hotelsProvider.get();
              print("data ${result['resultList'][0]['name']}");
            },
            child: Text("Search"),
          ),
          ElevatedButton(
            onPressed: () async {
              print("Called");
              var data = await _hotelsProvider.get();
              print("data ${data['resultList'][0]['name']}");
            },
            child: Text("Add"),
          ),
        ],
      ),
    );
  }
}
