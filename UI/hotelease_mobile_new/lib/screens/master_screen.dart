import 'package:flutter/material.dart';
import 'package:hotelease_mobile_new/screens/hotels_list_screen.dart';
import 'package:hotelease_mobile_new/screens/users_screen.dart';

class MasterScreen extends StatefulWidget {
  final Widget? child;
  final String? title;
  final Widget? title_widget;

  const MasterScreen({this.child, this.title, this.title_widget, super.key});

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
              title: const Text("Back", style: TextStyle(color: Colors.white)),
              onTap: () {
                Navigator.of(context).pop();
              },
            ),
            ListTile(
              title: const Text(
                "Search hotels",
                style: TextStyle(color: Colors.white),
              ),
              onTap: () {
                Navigator.of(context).push(
                  MaterialPageRoute(builder: (context) => HotelsListScreen()),
                );
              },
            ),
            ListTile(
              title: const Text("User", style: TextStyle(color: Colors.white)),
              onTap: () {
                Navigator.of(
                  context,
                ).push(MaterialPageRoute(builder: (context) => UsersScreen()));
              },
            ),
          ],
        ),
      ),

      appBar: AppBar(
        automaticallyImplyLeading: false, // sprječava dupli hamburger
        title:
            widget.title_widget ??
            Text(
              widget.title ?? "",
              style: const TextStyle(color: Colors.white),
            ),
        backgroundColor: const Color.fromRGBO(17, 45, 78, 1),
        iconTheme: const IconThemeData(color: Colors.white),

        // back dugme samo ako se može vratiti
        leading: Navigator.canPop(context)
            ? IconButton(
                icon: const Icon(Icons.arrow_back),
                onPressed: () => Navigator.of(context).pop(),
              )
            : null,

        // hamburger uvijek desno
        actions: [
          Builder(
            builder: (context) => IconButton(
              icon: const Icon(Icons.menu),
              onPressed: () {
                Scaffold.of(context).openDrawer();
              },
            ),
          ),
        ],
      ),

      body: widget.child,
    );
  }
}
