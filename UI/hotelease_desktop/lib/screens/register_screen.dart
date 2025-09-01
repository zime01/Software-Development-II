import 'package:flutter/material.dart';
import 'package:hotelease_mobile_new/main.dart';
import 'package:hotelease_mobile_new/providers/notifications.provider.dart';
import 'package:hotelease_mobile_new/providers/users_provider.dart';
import 'package:hotelease_mobile_new/screens/master_screen.dart';
import 'package:provider/provider.dart';

class RegisterScreen extends StatefulWidget {
  const RegisterScreen({super.key});

  @override
  State<RegisterScreen> createState() => _RegisterScreenState();
}

class _RegisterScreenState extends State<RegisterScreen> {
  final _firstNameController = TextEditingController();
  final _lastNameController = TextEditingController();
  final _emailController = TextEditingController();
  final _usernameController = TextEditingController();
  final _passwordController = TextEditingController();
  final _confirmPasswordController = TextEditingController();

  Future<void> _register(BuildContext context) async {
    final usersProvider = context.read<UsersProvider>();
    final notificationsProvider = context.read<NotificationsProvider>();

    showDialog(
      context: context,
      barrierDismissible: false,
      builder: (_) => const Center(child: CircularProgressIndicator()),
    );

    try {
      final payload = {
        "firstName": _firstNameController.text,
        "lastName": _lastNameController.text,
        "email": _emailController.text,
        "username": _usernameController.text,
        "password": _passwordController.text,
        "confirmPassword": _confirmPasswordController.text,
        "isActive": true,
        "createdAt": DateTime.now().toIso8601String(),
      };
      final result = await usersProvider.registerUser(payload);

      Navigator.of(context).pop(); // zatvori loading

      if (result["success"] == true) {
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(
            content: Text(result["message"]),
            backgroundColor: Colors.green,
          ),
        );

        Navigator.of(context).pushAndRemoveUntil(
          MaterialPageRoute(builder: (_) => MyApp()),
          (route) => false,
        );
      } else {
        showDialog(
          context: context,
          builder: (_) => AlertDialog(
            title: const Text("Error"),
            content: Text(result["message"]),
            actions: [
              TextButton(
                onPressed: () => Navigator.pop(context),
                child: const Text("OK"),
              ),
            ],
          ),
        );
      }
    } catch (e) {
      Navigator.of(context).pop(); // zatvori loading
      showDialog(
        context: context,
        builder: (_) => AlertDialog(
          title: const Text("Error"),
          content: Text(e.toString()),
          actions: [
            TextButton(
              onPressed: () => Navigator.pop(context),
              child: const Text("OK"),
            ),
          ],
        ),
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    return MasterScreen(
      title: "Registration",
      child: SingleChildScrollView(
        child: Padding(
          padding: const EdgeInsets.all(8.0),
          child: Column(
            children: [
              _buildTextField(_firstNameController, "First name"),
              _buildTextField(_lastNameController, "Last name"),
              _buildTextField(_emailController, "Email"),
              _buildTextField(_usernameController, "Username"),
              _buildTextField(_passwordController, "Password", obscure: true),
              _buildTextField(
                _confirmPasswordController,
                "Confirm password",
                obscure: true,
              ),
              const SizedBox(height: 30),
              ElevatedButton(
                onPressed: () => _register(context),
                style: ElevatedButton.styleFrom(
                  backgroundColor: const Color(0xFF112D4E),
                  foregroundColor: Colors.white,
                  minimumSize: const Size(200, 70),
                  shape: RoundedRectangleBorder(
                    borderRadius: BorderRadius.circular(50),
                    side: const BorderSide(color: Colors.white),
                  ),
                ),
                child: const Text("Register", style: TextStyle(fontSize: 24)),
              ),
            ],
          ),
        ),
      ),
    );
  }

  Widget _buildTextField(
    TextEditingController controller,
    String label, {
    bool obscure = false,
  }) {
    return Padding(
      padding: const EdgeInsets.all(8.0),
      child: TextField(
        controller: controller,
        style: const TextStyle(color: Colors.white),
        obscureText: obscure,
        decoration: InputDecoration(
          labelText: label,
          labelStyle: const TextStyle(
            color: Color.fromRGBO(255, 255, 255, 0.6),
          ),
        ),
      ),
    );
  }
}
