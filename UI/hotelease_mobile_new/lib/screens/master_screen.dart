import 'package:flutter/material.dart';
import 'package:hotelease_mobile_new/main.dart';
import 'package:hotelease_mobile_new/providers/theme_provider.dart';
import 'package:hotelease_mobile_new/screens/hotels_list_screen.dart';
import 'package:hotelease_mobile_new/screens/users_screen.dart';
import 'package:provider/provider.dart';
import '../utils/util.dart';

class MasterScreen extends StatefulWidget {
  final Widget? child;
  final String? title;
  final Widget? title_widget;

  const MasterScreen({this.child, this.title, this.title_widget, super.key});

  @override
  State<MasterScreen> createState() => _MasterScreenState();
}

class _MasterScreenState extends State<MasterScreen> {
  void _logout(BuildContext context) {
    Authorization.username = "";
    Authorization.password = "";
    Authorization.userId = null;

    Navigator.of(context).pushAndRemoveUntil(
      MaterialPageRoute(builder: (_) => const LoginPage()),
      (route) => false,
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Theme.of(context).colorScheme.primary,
      drawer: Drawer(
        backgroundColor: const Color.fromRGBO(15, 41, 70, 1),
        child: ListView(
          children: [
            ListTile(
              leading: const Icon(Icons.arrow_back, color: Colors.white),
              title: const Text("Back", style: TextStyle(color: Colors.white)),
              onTap: () => Navigator.of(context).pop(),
            ),
            ListTile(
              leading: const Icon(Icons.home, color: Colors.white),
              title: const Text("Home", style: TextStyle(color: Colors.white)),
              onTap: () => Navigator.of(context).pushReplacement(
                MaterialPageRoute(builder: (_) => HotelsListScreen()),
              ),
            ),
            ListTile(
              leading: const Icon(Icons.search, color: Colors.white),
              title: const Text(
                "Search hotels",
                style: TextStyle(color: Colors.white),
              ),
              onTap: () => Navigator.of(
                context,
              ).push(MaterialPageRoute(builder: (_) => HotelsListScreen())),
            ),
            ListTile(
              leading: const Icon(Icons.verified_user, color: Colors.white),
              title: const Text("User", style: TextStyle(color: Colors.white)),
              onTap: () => Navigator.of(
                context,
              ).push(MaterialPageRoute(builder: (_) => UsersScreen())),
            ),
            const Divider(color: Colors.white70),
            ListTile(
              leading: const Icon(Icons.logout, color: Colors.white),
              title: const Text(
                "Logout",
                style: TextStyle(color: Colors.white),
              ),
              onTap: () => _logout(context),
            ),
          ],
        ),
      ),
      appBar: AppBar(
        automaticallyImplyLeading: false,
        title: Row(
          children: [
            widget.title_widget ??
                Text(
                  widget.title ?? "",
                  style: TextStyle(
                    color: Theme.of(context).colorScheme.onPrimary,
                  ),
                ),
            const Spacer(),
            Switch(
              value: Provider.of<ThemeProvider>(context).isDarkMode,
              onChanged: (val) => Provider.of<ThemeProvider>(
                context,
                listen: false,
              ).toggleTheme(val),
            ),
          ],
        ),
        backgroundColor: Theme.of(context).colorScheme.primary,
        iconTheme: IconThemeData(
          color: Theme.of(context).colorScheme.onPrimary,
        ),
        leading: Navigator.canPop(context)
            ? IconButton(
                icon: const Icon(Icons.arrow_back),
                onPressed: () => Navigator.of(context).pop(),
              )
            : null,
        actions: [
          Builder(
            builder: (context) => IconButton(
              icon: const Icon(Icons.menu),
              onPressed: () => Scaffold.of(context).openDrawer(),
            ),
          ),
        ],
      ),
      body: widget.child,
    );
  }
}
