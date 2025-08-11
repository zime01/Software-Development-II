import 'package:flutter/material.dart';
import 'package:hotelease_mobile/screens/master_screen.dart';

class HotelsDetailsScreen extends StatefulWidget {
  const HotelsDetailsScreen({super.key});

  @override
  State<HotelsDetailsScreen> createState() => _HotelsDetailsScreenState();
}

class _HotelsDetailsScreenState extends State<HotelsDetailsScreen> {
  @override
  Widget build(BuildContext context) {
    return MasterScreen(
      title: "Hotel details",
      child: Column(
        children: [
          Text("Hoteldetails"),
          ElevatedButton(
            onPressed: () {
              Navigator.of(context).pop();
            },
            child: Text("back"),
          ),
        ],
      ),
    );
  }
}
