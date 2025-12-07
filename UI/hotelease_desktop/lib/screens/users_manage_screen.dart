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

  bool? _activeFilter;
  String? _roleFilter;

  @override
  void initState() {
    super.initState();
    loadUsers();
  }

  Future<void> loadUsers() async {
    var result = await _provider.getAll(
      includeRoles: true,
      isActive: _activeFilter,
      role: _roleFilter,
    );
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
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.center,
        children: [
          // 🔹 FILTER ROW
          Padding(
            padding: const EdgeInsets.symmetric(vertical: 10.0, horizontal: 8),
            child: Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                DropdownButton<bool?>(
                  value: _activeFilter,
                  hint: const Text(
                    "Active filter",
                    style: TextStyle(color: Colors.white),
                  ),
                  items: const [
                    DropdownMenuItem(value: null, child: Text("All")),
                    DropdownMenuItem(value: true, child: Text("Active")),
                    DropdownMenuItem(value: false, child: Text("Inactive")),
                  ],
                  onChanged: (value) {
                    setState(() => _activeFilter = value);
                    loadUsers();
                  },
                  dropdownColor: Colors.grey[900],
                  style: const TextStyle(color: Colors.white),
                ),
                const SizedBox(width: 30),
                DropdownButton<String?>(
                  value: _roleFilter,
                  hint: const Text(
                    "Role filter",
                    style: TextStyle(color: Colors.white),
                  ),
                  items: const [
                    DropdownMenuItem(value: null, child: Text("All")),
                    DropdownMenuItem(value: "user", child: Text("User")),
                    DropdownMenuItem(value: "manager", child: Text("Manager")),
                    DropdownMenuItem(value: "admin", child: Text("Admin")),
                  ],
                  onChanged: (value) {
                    setState(() => _roleFilter = value);
                    loadUsers();
                  },
                  dropdownColor: Colors.grey[900],
                  style: const TextStyle(color: Colors.white),
                ),
              ],
            ),
          ),

          // 🔹 USERS TABLE
          Expanded(
            child: SingleChildScrollView(
              scrollDirection: Axis.vertical, // vertical scroll
              child: SingleChildScrollView(
                scrollDirection: Axis.horizontal, // horizontal scroll
                child: DataTable(
                  columnSpacing: 30,
                  headingRowHeight: 56,
                  dataRowHeight: 60,
                  columns: const [
                    DataColumn(
                      label: Center(
                        child: Text(
                          "ID",
                          style: TextStyle(color: Colors.white),
                        ),
                      ),
                    ),
                    DataColumn(
                      label: Center(
                        child: Text(
                          "Username",
                          style: TextStyle(color: Colors.white),
                        ),
                      ),
                    ),
                    DataColumn(
                      label: Center(
                        child: Text(
                          "Email",
                          style: TextStyle(color: Colors.white),
                        ),
                      ),
                    ),
                    DataColumn(
                      label: Center(
                        child: Text(
                          "Role",
                          style: TextStyle(color: Colors.white),
                        ),
                      ),
                    ),
                    DataColumn(
                      label: Center(
                        child: Text(
                          "Active",
                          style: TextStyle(color: Colors.white),
                        ),
                      ),
                    ),
                    DataColumn(
                      label: Center(
                        child: Text(
                          "Actions",
                          style: TextStyle(color: Colors.white),
                        ),
                      ),
                    ),
                  ],
                  rows: users.map((u) {
                    return DataRow(
                      cells: [
                        DataCell(
                          Center(
                            child: Text(
                              u.id.toString(),
                              style: const TextStyle(color: Colors.white),
                            ),
                          ),
                        ),
                        DataCell(
                          Center(
                            child: Text(
                              u.username ?? "",
                              style: const TextStyle(color: Colors.white),
                            ),
                          ),
                        ),
                        DataCell(
                          Center(
                            child: Text(
                              u.email ?? "",
                              style: const TextStyle(color: Colors.white),
                            ),
                          ),
                        ),
                        DataCell(
                          Center(
                            child: Text(
                              u.roles != null && u.roles!.isNotEmpty
                                  ? u.roles!.map((r) => r.name).join(", ")
                                  : "No role",
                              style: const TextStyle(color: Colors.white),
                            ),
                          ),
                        ),
                        DataCell(
                          Center(
                            child: Row(
                              mainAxisSize: MainAxisSize.min,
                              children: [
                                Text(
                                  u.isActive == true ? "Yes" : "No",
                                  style: const TextStyle(color: Colors.white),
                                ),
                                IconButton(
                                  icon: Icon(
                                    u.isActive == true
                                        ? Icons.toggle_on
                                        : Icons.toggle_off,
                                    size: 32,
                                    color: u.isActive == true
                                        ? Colors.green
                                        : Colors.red,
                                  ),
                                  onPressed: () => toggleActivation(u),
                                ),
                              ],
                            ),
                          ),
                        ),
                        DataCell(
                          Center(
                            child: PopupMenuButton<String>(
                              onSelected: (role) => changeRole(u, role),
                              itemBuilder: (context) => const [
                                PopupMenuItem(
                                  value: "user",
                                  child: Text("Assign as User"),
                                ),
                                PopupMenuItem(
                                  value: "manager",
                                  child: Text("Assign as Manager"),
                                ),
                                PopupMenuItem(
                                  value: "admin",
                                  child: Text("Assign as Admin"),
                                ),
                              ],
                              child: const Icon(
                                Icons.edit,
                                color: Colors.white,
                              ),
                            ),
                          ),
                        ),
                      ],
                    );
                  }).toList(),
                ),
              ),
            ),
          ),
        ],
      ),
    );
  }
}
