import 'package:flutter/material.dart';
import 'package:hotelease_mobile/providers/assets_provider.dart';
import 'package:hotelease_mobile/providers/hotels_provider.dart';
import 'package:hotelease_mobile/providers/room_availability.dart';
import 'package:hotelease_mobile/providers/rooms.provider.dart';
import 'package:hotelease_mobile/screens/hotels_list_screen.dart';
import 'package:hotelease_mobile/utils/util.dart';
import 'package:provider/provider.dart';

void main() {
  runApp(
    MultiProvider(
      providers: [
        ChangeNotifierProvider(create: (_) => HotelsProvider()),
        ChangeNotifierProvider(create: (_) => AssetsProvider()),
        ChangeNotifierProvider(create: (_) => RoomsProvider()),
        ChangeNotifierProvider(create: (_) => RoomsAvailabilityProvider()),
      ],
      child: const MyApp(),
    ),
  );
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Flutter Demo',
      theme: ThemeData(
        // This is the theme of your application.
        //
        // TRY THIS: Try running your application with "flutter run". You'll see
        // the application has a purple toolbar. Then, without quitting the app,
        // try changing the seedColor in the colorScheme below to Colors.green
        // and then invoke "hot reload" (save your changes or press the "hot
        // reload" button in a Flutter-supported IDE, or press "r" if you used
        // the command line to start the app).
        //
        // Notice that the counter didn't reset back to zero; the application
        // state is not lost during the reload. To reset the state, use hot
        // restart instead.
        //
        // This works for code too, not just values: Most code changes can be
        // tested with just a hot reload.
        colorScheme: ColorScheme.fromSeed(
          seedColor: const Color.fromRGBO(17, 45, 78, 1),
          primary: const Color.fromRGBO(17, 45, 78, 1),
          secondary: const Color.fromRGBO(17, 45, 78, 1),
        ),
      ),
      home: LoginPage(),
    );
  }
}

class LayoutExamples extends StatelessWidget {
  const LayoutExamples({super.key});

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        Container(
          height: 150,
          color: Colors.red,
          child: Center(
            child: Container(
              height: 100,
              color: Colors.blue,
              child: Text("Text"),
            ),
          ),
        ),
        Row(
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          children: [Text("I1"), Text("I2"), Text("I3")],
        ),
        Container(
          height: 150,
          color: Colors.red,
          child: Center(child: Text("Contain")),
        ),
      ],
    );
  }
}

class LoginPage extends StatelessWidget {
  LoginPage({super.key});

  TextEditingController _usernameController = new TextEditingController();
  TextEditingController _passwordController = new TextEditingController();
  late HotelsProvider _hotelsProvider;

  @override
  Widget build(BuildContext context) {
    _hotelsProvider = context.read<HotelsProvider>();
    return Scaffold(
      appBar: AppBar(
        title: Text(
          "Login",
          style: TextStyle(color: const Color.fromRGBO(255, 255, 255, 1)),
        ),
        backgroundColor: const Color.fromRGBO(17, 45, 78, 1),
      ),
      backgroundColor: const Color.fromRGBO(17, 45, 78, 1),
      body: SingleChildScrollView(
        child: Padding(
          padding: const EdgeInsets.all(8.0),
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            crossAxisAlignment: CrossAxisAlignment.center,
            children: [
              //Image.asset("assets/images/fit.png", height: 100, width: 100),
              SizedBox(height: 50),
              Text(
                "🏨HotelEase",
                style: TextStyle(
                  fontSize: 32,
                  color: const Color.fromRGBO(255, 255, 255, 1),
                ),
              ),
              SizedBox(height: 100),
              Container(
                decoration: BoxDecoration(
                  color: const Color.fromRGBO(17, 45, 78, 1),
                ),

                child: Column(
                  children: [
                    TextField(
                      style: TextStyle(
                        color: const Color.fromRGBO(255, 255, 255, 1),
                      ),
                      controller: _usernameController,
                      decoration: InputDecoration(
                        labelText: "Username",
                        labelStyle: TextStyle(
                          color: const Color.fromRGBO(255, 255, 255, 1),
                        ),
                        prefixIcon: Icon(
                          Icons.email,
                          color: const Color.fromRGBO(255, 255, 255, 1),
                        ),
                      ),
                    ),
                    SizedBox(height: 10),
                    TextField(
                      style: TextStyle(
                        color: const Color.fromRGBO(255, 255, 255, 1),
                      ),
                      controller: _passwordController,
                      decoration: InputDecoration(
                        labelText: "Password",
                        labelStyle: TextStyle(
                          color: const Color.fromRGBO(255, 255, 255, 1),
                        ),
                        prefixIcon: Icon(
                          Icons.password,
                          color: const Color.fromRGBO(255, 255, 255, 1),
                        ),
                      ),
                    ),

                    SizedBox(height: 100),
                    Padding(
                      padding: const EdgeInsets.all(8.0),
                      child: Container(
                        decoration: BoxDecoration(
                          color: const Color.fromRGBO(17, 45, 78, 1),
                          border: Border.all(
                            color: const Color.fromRGBO(255, 255, 255, 1),
                          ),
                          borderRadius: BorderRadius.circular(50),
                        ),
                        width: 200,
                        height: 70,
                        child: ElevatedButton(
                          style: ElevatedButton.styleFrom(
                            backgroundColor: const Color.fromRGBO(
                              17,
                              45,
                              78,
                              1,
                            ),
                            foregroundColor: const Color.fromRGBO(
                              255,
                              255,
                              255,
                              1,
                            ),
                          ),
                          onPressed: () async {
                            var username = _usernameController.text;
                            var password = _passwordController.text;

                            print("login creds ${username} ${password}");

                            Authorization.username = username;
                            Authorization.password = password;

                            try {
                              await _hotelsProvider.get();

                              Navigator.of(context).push(
                                MaterialPageRoute(
                                  builder: (context) => HotelsListScreen(),
                                ),
                              );
                            } on Exception catch (e) {
                              showDialog(
                                context: context,
                                builder: (BuildContext context) => AlertDialog(
                                  title: Text("Error"),
                                  content: Text(e.toString()),
                                  actions: [
                                    TextButton(
                                      onPressed: () => Navigator.pop(context),
                                      child: Text("OK"),
                                    ),
                                  ],
                                ),
                              );
                            }
                          },
                          child: Text("Login", style: TextStyle(fontSize: 32)),
                        ),
                      ),
                    ),
                  ],
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}

class MyHomePage extends StatefulWidget {
  const MyHomePage({super.key, required this.title});

  // This widget is the home page of your application. It is stateful, meaning
  // that it has a State object (defined below) that contains fields that affect
  // how it looks.

  // This class is the configuration for the state. It holds the values (in this
  // case the title) provided by the parent (in this case the App widget) and
  // used by the build method of the State. Fields in a Widget subclass are
  // always marked "final".

  final String title;

  @override
  State<MyHomePage> createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  int _counter = 0;

  void _incrementCounter() {
    setState(() {
      // This call to setState tells the Flutter framework that something has
      // changed in this State, which causes it to rerun the build method below
      // so that the display can reflect the updated values. If we changed
      // _counter without calling setState(), then the build method would not be
      // called again, and so nothing would appear to happen.
      _counter++;
    });
  }

  @override
  Widget build(BuildContext context) {
    // This method is rerun every time setState is called, for instance as done
    // by the _incrementCounter method above.
    //
    // The Flutter framework has been optimized to make rerunning build methods
    // fast, so that you can just rebuild anything that needs updating rather
    // than having to individually change instances of widgets.
    return Scaffold(
      appBar: AppBar(
        // TRY THIS: Try changing the color here to a specific color (to
        // Colors.amber, perhaps?) and trigger a hot reload to see the AppBar
        // change color while the other colors stay the same.
        backgroundColor: Theme.of(context).colorScheme.inversePrimary,
        // Here we take the value from the MyHomePage object that was created by
        // the App.build method, and use it to set our appbar title.
        title: Text(widget.title),
      ),
      body: Center(
        // Center is a layout widget. It takes a single child and positions it
        // in the middle of the parent.
        child: Column(
          // Column is also a layout widget. It takes a list of children and
          // arranges them vertically. By default, it sizes itself to fit its
          // children horizontally, and tries to be as tall as its parent.
          //
          // Column has various properties to control how it sizes itself and
          // how it positions its children. Here we use mainAxisAlignment to
          // center the children vertically; the main axis here is the vertical
          // axis because Columns are vertical (the cross axis would be
          // horizontal).
          //
          // TRY THIS: Invoke "debug painting" (choose the "Toggle Debug Paint"
          // action in the IDE, or press "p" in the console), to see the
          // wireframe for each widget.
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            const Text('You have pushed the button this many times:'),
            Text(
              '$_counter',
              style: Theme.of(context).textTheme.headlineMedium,
            ),
          ],
        ),
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: _incrementCounter,
        tooltip: 'Increment',
        child: const Icon(Icons.add),
      ), // This trailing comma makes auto-formatting nicer for build methods.
    );
  }
}
