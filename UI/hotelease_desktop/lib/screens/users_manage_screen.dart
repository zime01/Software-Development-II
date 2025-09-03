import 'package:flutter/material.dart';
import 'package:hotelease_mobile_new/models/user.dart';
import 'package:hotelease_mobile_new/providers/users_provider.dart';
import 'package:hotelease_mobile_new/screens/master_screen.dart';

class UsersManageScreen extends StatefulWidget {
  const UsersManageScreen({super.key});

  @override
  State<UsersManageScreen> createState() => _UsersManageScreenState();
}

class _UsersManageScreenState extends State<UsersManageScreen> {
  final UsersProvider _provider = UsersProvider();
  List<User> users = [];

  @override
  void initState() {
    super.initState();
    loadUsers();
  }

  Future<void> loadUsers() async {
    var result = await _provider.getAll(includeRoles: true);
    setState(() {
      users = result;
    });
  }

  Future<void> toggleActivation(User user) async {
    await _provider.updateStatus(user.id!, !user.isActive!);
    loadUsers();
  }

  Future<void> changeRole(User user, String newRole) async {
    await _provider.changeRole(user.id!, newRole);
    loadUsers();
  }

  @override
  Widget build(BuildContext context) {
    return MasterScreen(
      title: "Manage users",
      child: SingleChildScrollView(
        scrollDirection: Axis.horizontal,
        child: DataTable(
          columns: const [
            DataColumn(
              label: Text("ID", style: TextStyle(color: Colors.white)),
            ),
            DataColumn(
              label: Text("Username", style: TextStyle(color: Colors.white)),
            ),
            DataColumn(
              label: Text("Email", style: TextStyle(color: Colors.white)),
            ),
            DataColumn(
              label: Text("Role", style: TextStyle(color: Colors.white)),
            ),
            DataColumn(
              label: Text("Active", style: TextStyle(color: Colors.white)),
            ),
            DataColumn(
              label: Text("Actions", style: TextStyle(color: Colors.white)),
            ),
          ],
          rows: users.map((u) {
            return DataRow(
              cells: [
                DataCell(
                  Text(u.id.toString(), style: TextStyle(color: Colors.white)),
                ),
                DataCell(
                  Text(u.username ?? "", style: TextStyle(color: Colors.white)),
                ),
                DataCell(
                  Text(u.email ?? "", style: TextStyle(color: Colors.white)),
                ),
                DataCell(
                  Text(
                    u.roles != null && u.roles!.isNotEmpty
                        ? u.roles!.map((r) => r.name).join(", ")
                        : "No role",
                    style: TextStyle(color: Colors.white),
                  ),
                ),
                DataCell(
                  Text(
                    u.isActive == true ? "Yes" : "No",
                    style: TextStyle(color: Colors.white),
                  ),
                ),
                DataCell(
                  Row(
                    children: [
                      IconButton(
                        icon: Icon(
                          u.isActive == true ? Icons.block : Icons.check,
                          color: u.isActive == true ? Colors.red : Colors.green,
                        ),
                        onPressed: () => toggleActivation(u),
                      ),
                      PopupMenuButton<String>(
                        onSelected: (role) => changeRole(u, role),
                        itemBuilder: (context) => [
                          const PopupMenuItem(
                            value: "user",
                            child: Text("User"),
                          ),
                          const PopupMenuItem(
                            value: "manager",
                            child: Text("Manager"),
                          ),
                          const PopupMenuItem(
                            value: "admin",
                            child: Text("Admin"),
                          ),
                        ],
                        child: const Icon(Icons.edit, color: Colors.white),
                      ),
                    ],
                  ),
                ),
              ],
            );
          }).toList(),
        ),
      ),
    );
  }
}
