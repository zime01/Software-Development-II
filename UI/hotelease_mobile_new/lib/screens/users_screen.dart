import 'package:flutter/material.dart';
import 'package:hotelease_mobile_new/models/user.dart';
import 'package:hotelease_mobile_new/models/reservation.dart';
import 'package:hotelease_mobile_new/providers/rooms.provider.dart';
import 'package:hotelease_mobile_new/providers/users_provider.dart';
import 'package:hotelease_mobile_new/providers/reservations_provider.dart';
import 'package:hotelease_mobile_new/utils/util.dart';

class UsersScreen extends StatefulWidget {
  const UsersScreen({super.key});

  @override
  State<UsersScreen> createState() => _UsersScreenState();
}

class _UsersScreenState extends State<UsersScreen>
    with SingleTickerProviderStateMixin {
  final RoomsProvider _roomsProvider = RoomsProvider();
  late TabController _tabController;
  User? _currentUser;
  List<Reservation> _reservations = [];

  final _formKey = GlobalKey<FormState>();
  late TextEditingController _firstName;
  late TextEditingController _lastName;
  late TextEditingController _phone;

  final _oldPass = TextEditingController();
  final _newPass = TextEditingController();
  final _confirmPass = TextEditingController();

  @override
  void initState() {
    super.initState();
    _tabController = TabController(length: 3, vsync: this);
    _loadUser();
    _loadReservations();
  }

  Future<void> _loadUser() async {
    try {
      var user = await UsersProvider().getCurrentUser();
      setState(() {
        _currentUser = user;
        _firstName = TextEditingController(text: user.firstName ?? "");
        _lastName = TextEditingController(text: user.lastName ?? "");
        _phone = TextEditingController(text: user.phoneNumber ?? "");
      });
    } catch (e, s) {
      debugPrint("Error while loading user: $e");
      debugPrint("$s");
    }
  }

  Future<void> _loadReservations() async {
    try {
      var resList = await ReservationsProvider().getMyReservations();

      // učitaj sobe paralelno
      await Future.wait(
        resList.map((res) async {
          if (res.roomId != null) {
            res.room = await _roomsProvider.getRoomById(res.roomId!);
          }
        }),
      );

      // tek sad ažuriraj UI sa potpunim podacima
      setState(() {
        _reservations = resList;
      });
    } catch (e, s) {
      debugPrint("Error with reservation $e");
      debugPrint("$s");
    }
  }

  Future<void> _updateProfile() async {
    if (_formKey.currentState!.validate()) {
      await UsersProvider().update(_currentUser!.id!, {
        "firstName": _firstName.text,
        "lastName": _lastName.text,
        "phoneNumber": _phone.text,
      });
      ScaffoldMessenger.of(
        context,
      ).showSnackBar(const SnackBar(content: Text("Profile updated ✅")));
    }
  }

  Future<void> _changePassword() async {
    if (_newPass.text != _confirmPass.text) {
      showDialog(
        context: context,
        builder: (context) => AlertDialog(
          title: Text("Error"),
          content: Text("New and confirm password must be same"),
          actions: [
            TextButton(
              onPressed: () => Navigator.pop(context),
              child: Text("OK"),
            ),
          ],
        ),
      );
      return;
    }

    try {
      await UsersProvider().update(_currentUser!.id!, {
        "firstName": _currentUser!.firstName,
        "lastName": _currentUser!.lastName,
        "phoneNumber": _currentUser!.phoneNumber,
        "oldPassword": _oldPass.text,
        "password": _newPass.text,
        "confirmPassword": _confirmPass.text,
      });

      showDialog(
        context: context,
        builder: (context) => AlertDialog(
          title: Text("Success"),
          content: Text("Succesfully changed password"),
          actions: [
            TextButton(
              onPressed: () => Navigator.pop(context),
              child: Text("OK"),
            ),
          ],
        ),
      );
    } catch (e) {
      showDialog(
        context: context,
        builder: (context) => AlertDialog(
          title: Text("Error"),
          content: Text("Failed to change password: $e"),
          actions: [
            TextButton(
              onPressed: () => Navigator.pop(context),
              child: Text("OK"),
            ),
          ],
        ),
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    if (_currentUser == null) {
      return const Center(child: CircularProgressIndicator());
    }

    return Scaffold(
      appBar: AppBar(
        iconTheme: IconThemeData(color: Colors.white),
        backgroundColor: const Color.fromRGBO(17, 45, 78, 1),
        title: const Text(
          "User Profile",
          style: TextStyle(color: Colors.white),
        ),
        bottom: TabBar(
          labelColor: Colors.white,
          unselectedLabelColor: Colors.white,
          controller: _tabController,
          tabs: const [
            Tab(text: "Profile"),
            Tab(text: "Password"),
            Tab(text: "Reservations"),
          ],
        ),
      ),
      body: Container(
        decoration: BoxDecoration(color: const Color.fromRGBO(17, 45, 78, 1)),
        child: TabBarView(
          controller: _tabController,
          children: [
            // 📌 1. Profil
            Padding(
              padding: const EdgeInsets.all(16),
              child: Form(
                key: _formKey,
                child: Column(
                  children: [
                    TextFormField(
                      style: TextStyle(color: Colors.white),
                      controller: _firstName,
                      decoration: const InputDecoration(
                        labelText: "First Name",
                        labelStyle: TextStyle(color: Colors.grey),
                      ),
                      validator: (v) => v!.isEmpty ? "Enter FirstName" : null,
                    ),
                    TextFormField(
                      style: TextStyle(color: Colors.white),
                      controller: _lastName,
                      decoration: const InputDecoration(
                        labelText: "Last Name",
                        labelStyle: TextStyle(color: Colors.grey),
                      ),
                      validator: (v) => v!.isEmpty ? "Enter Last Name" : null,
                    ),
                    TextFormField(
                      style: TextStyle(color: Colors.white),
                      controller: _phone,
                      decoration: const InputDecoration(
                        labelText: "Phone Number",
                        labelStyle: TextStyle(color: Colors.grey),
                      ),
                    ),
                    const SizedBox(height: 20),
                    ElevatedButton(
                      onPressed: _updateProfile,
                      child: const Text("Save"),
                    ),
                  ],
                ),
              ),
            ),

            // 📌 2. Promjena lozinke
            Padding(
              padding: const EdgeInsets.all(16),
              child: Column(
                children: [
                  TextField(
                    style: TextStyle(color: Colors.white),
                    controller: _oldPass,
                    decoration: const InputDecoration(
                      labelText: "Old Password",
                      labelStyle: TextStyle(color: Colors.grey),
                    ),
                    obscureText: true,
                  ),
                  TextField(
                    style: TextStyle(color: Colors.white),
                    controller: _newPass,
                    decoration: const InputDecoration(
                      labelText: "New Password",
                      labelStyle: TextStyle(color: Colors.grey),
                    ),
                    obscureText: true,
                  ),
                  TextField(
                    style: TextStyle(color: Colors.white),
                    controller: _confirmPass,
                    decoration: const InputDecoration(
                      labelText: "Confirm Password",
                      labelStyle: TextStyle(color: Colors.grey),
                    ),
                    obscureText: true,
                  ),
                  const SizedBox(height: 20),
                  ElevatedButton(
                    onPressed: _changePassword,
                    child: const Text("Change Password"),
                  ),
                ],
              ),
            ),

            // 📌 3. Rezervacije
            _reservations.isEmpty
                ? const Center(
                    child: Text(
                      "No Reservations",
                      style: TextStyle(color: Colors.white),
                    ),
                  )
                : Container(
                    decoration: BoxDecoration(
                      color: const Color.fromRGBO(17, 45, 78, 1),
                    ),
                    child: ListView.builder(
                      itemCount: _reservations.length,
                      itemBuilder: (context, index) {
                        var res = _reservations[index];
                        return Card(
                          color: const Color.fromRGBO(15, 25, 70, 1),
                          margin: const EdgeInsets.all(8),
                          child: ListTile(
                            title: Text(
                              res.room?.hotelName ?? "Hotel",
                              style: TextStyle(color: Colors.white),
                            ),
                            subtitle: Text(
                              "${res.checkInDate} - ${res.checkOutDate}",
                              style: TextStyle(color: Colors.white),
                            ),
                            trailing: Text(
                              res.status ?? "",
                              style: TextStyle(color: Colors.white),
                            ),
                            onTap: () {},
                          ),
                        );
                      },
                    ),
                  ),
          ],
        ),
      ),
    );
  }
}
