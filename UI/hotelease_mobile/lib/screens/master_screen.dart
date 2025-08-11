import 'package:flutter/material.dart';
import 'package:hotelease_mobile/screens/hotels_details_screen.dart';
import 'package:hotelease_mobile/screens/hotels_list_screen.dart';

class MasterScreen extends StatefulWidget {
  Widget? child;
  String? title;
  Widget? title_widget;

  MasterScreen({this.child, this.title, this.title_widget, super.key});

  @override
  State<MasterScreen> createState() => _MasterScreenState();
}

class _MasterScreenState extends State<MasterScreen> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      drawer: Drawer(
        child: ListView(
          children: [
            ListTile(
              title: Text("Back"),
              onTap: () {
                Navigator.of(context).pop();
              },
            ),
            ListTile(
              title: Text("Hotels"),
              onTap: () {
                Navigator.of(context).push(
                  MaterialPageRoute(builder: (context) => HotelsListScreen()),
                );
              },
            ),
            ListTile(
              title: Text("Details"),
              onTap: () {
                Navigator.of(context).push(
                  MaterialPageRoute(
                    builder: (context) => HotelsDetailsScreen(),
                  ),
                );
              },
            ),
          ],
        ),
      ),
      appBar: AppBar(title: widget.title_widget ?? Text(widget.title ?? "")),
      body: widget.child,
    );
  }
}
