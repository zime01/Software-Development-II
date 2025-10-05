import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_dotenv/flutter_dotenv.dart';
import 'package:flutter_stripe/flutter_stripe.dart';
import 'package:hotelease_mobile_new/providers/assets_provider.dart';
import 'package:hotelease_mobile_new/providers/hotels_provider.dart';
import 'package:hotelease_mobile_new/providers/notifications.provider.dart';
import 'package:hotelease_mobile_new/providers/payments_provider.dart';
import 'package:hotelease_mobile_new/providers/reservations_provider.dart';
import 'package:hotelease_mobile_new/providers/reviews_provider.dart';
import 'package:hotelease_mobile_new/providers/room_availability.dart';
import 'package:hotelease_mobile_new/providers/rooms.provider.dart';
import 'package:hotelease_mobile_new/providers/search_provider.dart';
import 'package:hotelease_mobile_new/providers/services_provider.dart';
import 'package:hotelease_mobile_new/providers/theme_provider.dart';
import 'package:hotelease_mobile_new/providers/users_provider.dart';
import 'package:hotelease_mobile_new/screens/hotels_list_screen.dart';
import 'package:hotelease_mobile_new/screens/register_screen.dart';
import 'package:hotelease_mobile_new/utils/util.dart';
import 'package:provider/provider.dart';

void main() async {
  WidgetsFlutterBinding.ensureInitialized();

  Stripe.publishableKey =
      "pk_test_51QZ8FxKH5av5GhJI2G02GXIaCml8epgY98WnYBZ8lH0s4HR4MwPLnO5wIEpe0z3lR12OPgOvzCtETkO3wH5xsrxo006dxV0Xvt";

  await Stripe.instance.applySettings();

  runApp(
    MultiProvider(
      providers: [
        ChangeNotifierProvider(create: (_) => HotelsProvider()),
        ChangeNotifierProvider(create: (_) => AssetsProvider()),
        ChangeNotifierProvider(create: (_) => RoomsProvider()),
        ChangeNotifierProvider(create: (_) => RoomsAvailabilityProvider()),
        ChangeNotifierProvider(create: (_) => ServicesProvider()),
        ChangeNotifierProvider(create: (_) => NotificationsProvider()),
        ChangeNotifierProvider(create: (_) => ReservationsProvider()),
        ChangeNotifierProvider(create: (_) => UsersProvider()),
        ChangeNotifierProvider(create: (_) => PaymentsProvider()),
        ChangeNotifierProvider(create: (_) => ReviewsProvider()),
        ChangeNotifierProvider(create: (_) => SearchProvider()),
        ChangeNotifierProvider(create: (_) => ThemeProvider()),
      ],
      child: const MyApp(),
    ),
  );
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    final themeProvider = Provider.of<ThemeProvider>(context);

    const primaryColor = Color.fromRGBO(17, 45, 78, 1); //
    const secondaryColor = Color(0xFFBFD7ED);

    return MaterialApp(
      title: 'HotelEase',
      debugShowCheckedModeBanner: false,
      // 🌞 LIGHT MODE
      theme: ThemeData(
        colorScheme: ColorScheme.fromSeed(
          seedColor: secondaryColor,
          brightness: Brightness.light,
          primary: secondaryColor, // svijetla plava dominira
          onPrimary: primaryColor,
          secondary: const Color.fromARGB(
            255,
            64,
            134,
            215,
          ), // tamna za akcente
          background: Colors.white,
          surface: Colors.white,
        ),
        scaffoldBackgroundColor: Colors.white,
        appBarTheme: const AppBarTheme(
          backgroundColor: secondaryColor,
          foregroundColor: Colors.black,
          elevation: 1,
        ),
        textTheme: const TextTheme(
          bodyLarge: TextStyle(color: Colors.black87),
          bodyMedium: TextStyle(color: Colors.black87),
        ),
        useMaterial3: true,
      ),

      // 🌙 DARK MODE
      darkTheme: ThemeData(
        colorScheme: ColorScheme.fromSeed(
          seedColor: primaryColor,
          onPrimary: Colors.white,
          brightness: Brightness.dark,
          primary: primaryColor, // tamno plava dominira
          secondary: Color.fromRGBO(15, 30, 70, 1), // svijetla za akcente
          background: const Color(0xFF0F1E46),
          surface: const Color(0xFF112D4E),
        ),
        scaffoldBackgroundColor: const Color(0xFF0F1E46),
        appBarTheme: const AppBarTheme(
          backgroundColor: primaryColor,
          foregroundColor: Colors.white,
        ),
        textTheme: const TextTheme(
          bodyLarge: TextStyle(color: Colors.white),
          bodyMedium: TextStyle(color: Colors.white),
        ),
        useMaterial3: true,
      ),
      themeMode: themeProvider.isDarkMode ? ThemeMode.dark : ThemeMode.light,
      home: LoginPage(),
    );
  }
}

class LoginPage extends StatefulWidget {
  const LoginPage({super.key});

  @override
  State<LoginPage> createState() => _LoginPageState();
}

class _LoginPageState extends State<LoginPage> {
  final TextEditingController _usernameController = TextEditingController();
  final TextEditingController _passwordController = TextEditingController();

  @override
  void dispose() {
    _usernameController.dispose();
    _passwordController.dispose();
    super.dispose();
  }

  Future<void> _login(BuildContext context) async {
    final hotelsProvider = context.read<HotelsProvider>();
    final usersProvider = context.read<UsersProvider>();

    showDialog(
      context: context,
      barrierDismissible: false,
      builder: (_) => const Center(child: CircularProgressIndicator()),
    );

    try {
      final username = _usernameController.text;
      final password = _passwordController.text;

      print("Login credentials: $username / $password");

      Authorization.username = username;
      Authorization.password = password;

      await hotelsProvider.get();

      final currentUser = await usersProvider.getCurrentUser();
      Authorization.userId = currentUser.id;

      Navigator.of(context).pop(); // Remove loading
      Navigator.of(
        context,
      ).pushReplacement(MaterialPageRoute(builder: (_) => HotelsListScreen()));
    } catch (e) {
      Navigator.of(context).pop(); // Remove loading
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
    final size = MediaQuery.of(context).size;

    return Scaffold(
      appBar: AppBar(
        title: const Text("Login", style: TextStyle(color: Colors.white)),
        backgroundColor: const Color.fromRGBO(17, 45, 78, 1),
      ),
      backgroundColor: const Color.fromRGBO(17, 45, 78, 1),
      body: SingleChildScrollView(
        child: ConstrainedBox(
          constraints: BoxConstraints(minHeight: size.height),
          child: IntrinsicHeight(
            child: Column(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                const SizedBox(height: 50),
                const Text(
                  "🏨 HotelEase",
                  style: TextStyle(fontSize: 32, color: Colors.white),
                ),
                const SizedBox(height: 100),
                Padding(
                  padding: const EdgeInsets.symmetric(horizontal: 16.0),
                  child: Column(
                    children: [
                      TextField(
                        controller: _usernameController,
                        style: const TextStyle(color: Colors.white),
                        decoration: const InputDecoration(
                          labelText: "Username",
                          labelStyle: TextStyle(color: Colors.white),
                          prefixIcon: Icon(Icons.email, color: Colors.white),
                        ),
                      ),
                      const SizedBox(height: 10),
                      TextField(
                        controller: _passwordController,
                        style: const TextStyle(color: Colors.white),
                        obscureText: true,
                        decoration: const InputDecoration(
                          labelText: "Password",
                          labelStyle: TextStyle(color: Colors.white),
                          prefixIcon: Icon(Icons.lock, color: Colors.white),
                        ),
                      ),
                      const SizedBox(height: 50),
                      ElevatedButton(
                        onPressed: () => _login(context),
                        style: ElevatedButton.styleFrom(
                          backgroundColor: const Color(0xFF112D4E),
                          foregroundColor: Colors.white,
                          minimumSize: const Size(200, 70),
                          shape: RoundedRectangleBorder(
                            borderRadius: BorderRadius.circular(50),
                            side: const BorderSide(color: Colors.white),
                          ),
                        ),
                        child: const Text(
                          "Login",
                          style: TextStyle(fontSize: 24),
                        ),
                      ),
                      SizedBox(height: 20),
                      GestureDetector(
                        onTap: () {
                          Navigator.of(context).push(
                            MaterialPageRoute(
                              builder: (context) => RegisterScreen(),
                            ),
                          );
                        },
                        child: Text(
                          "Register now",
                          style: TextStyle(color: Colors.white, fontSize: 20),
                        ),
                      ),
                    ],
                  ),
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }
}
