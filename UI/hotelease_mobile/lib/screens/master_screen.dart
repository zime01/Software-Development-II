import 'package:flutter/material.dart';
import 'package:hotelease_mobile/screens/hotel_details_screen.dart';
import 'package:hotelease_mobile/screens/hotels.dart';
//import 'package:hotelease_mobile/screens/hotels_details_screen.dart';
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
      backgroundColor: const Color.fromRGBO(17, 45, 78, 1),
      drawer: Drawer(
        backgroundColor: const Color.fromRGBO(15, 41, 70, 1),
        shadowColor: const Color.fromRGBO(15, 41, 70, 1),
        child: ListView(
          children: [
            ListTile(
              title: Text("Back", style: TextStyle(color: Colors.white)),
              onTap: () {
                Navigator.of(context).pop();
              },
            ),
            ListTile(
              title: Text(
                "Search hotels",
                style: TextStyle(color: Colors.white),
              ),
              onTap: () {
                Navigator.of(context).push(
                  MaterialPageRoute(builder: (context) => HotelsListScreen()),
                );
              },
            ),
          ],
        ),
      ),
      appBar: AppBar(
        title:
            widget.title_widget ??
            Text(
              widget.title ?? "",
              style: TextStyle(color: const Color.fromRGBO(255, 255, 255, 1)),
            ),
        backgroundColor: const Color.fromRGBO(17, 45, 78, 1),
        iconTheme: IconThemeData(color: const Color.fromRGBO(255, 255, 255, 1)),
        leading: IconButton(
          onPressed: () {
            Navigator.of(context).pop();
          },
          icon: Icon(Icons.arrow_back),
        ),
      ),

      body: widget.child,
    );
  }
}
