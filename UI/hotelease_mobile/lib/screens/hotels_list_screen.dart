import 'package:flutter/material.dart';
import 'package:hotelease_mobile/models/hotel.dart';
import 'package:hotelease_mobile/models/search_result.dart';
import 'package:hotelease_mobile/providers/hotels_provider.dart';
import 'package:hotelease_mobile/screens/dummy.dart';
import 'package:hotelease_mobile/screens/hotels.dart';
//import 'package:hotelease_mobile/screens/hotels_details_screen.dart';
import 'package:hotelease_mobile/screens/master_screen.dart';
import 'package:intl/intl.dart';
import 'package:provider/provider.dart';

final formatter = DateFormat('d MMM yyyy HH:mm');

class HotelsListScreen extends StatefulWidget {
  const HotelsListScreen({super.key});

  @override
  State<HotelsListScreen> createState() => _HotelsListScreenState();
}

class _HotelsListScreenState extends State<HotelsListScreen> {
  late HotelsProvider _hotelsProvider;
  SearchResult<Hotel>? result = null;
  String searchQuery = "";

  DateTime? _dateFrom;
  DateTime? _dateTo;

  List<int> adults = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
  List<int> children = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
  List<int> rooms = [1, 2, 3, 4, 5, 6, 7];

  int _selectedPass1 = 1;
  int _selectedPass2 = 1;

  int _selectedroom = 1;

  String? selectedSort;

  double minPrice = 0;
  double maxPrice = 500;
  int minStars = 1;
  bool wifi = false;
  bool parking = false;
  bool pool = false;

  List<Map<String, String>> sortOptions = [
    {'label': 'Lowest Price', 'value': 'PriceAsc'},
    {'label': 'Highest Price', 'value': 'PriceDesc'},
    {'label': 'Highest Stars', 'value': 'StarRatingDesc'},
  ];

  void _selectDateFrom() async {
    print("pozvana funkcija za datume");
    final now = DateTime.now();
    // final firstDate = DateTime(now.year - 1, now.month, now.day);
    final firstDate = DateTime(now.year - 1, now.month, now.day);
    final lastDate = DateTime(now.year + 1, now.month, now.day);
    final pickedDate = await showDatePicker(
      context: context,
      initialDate: _dateFrom ?? now,
      firstDate: firstDate,
      lastDate: lastDate,
    );
    if (pickedDate != null) {
      final pickedTime = await showTimePicker(
        context: context,
        initialTime: TimeOfDay(hour: 10, minute: 0),
      );
      if (pickedTime != null) {
        final combined = DateTime(
          pickedDate.year,
          pickedDate.month,
          pickedDate.day,
          pickedTime.hour,
          pickedTime.minute,
        );
        setState(() {
          _dateFrom = combined;
          print("Date picker ${_dateFrom}");
        });
      }
    } else {
      print("Picked date is null");
    }
  }

  void _selectDateTo() async {
    final now = DateTime.now();
    final firstDate = DateTime(now.year - 1, now.month, now.day);
    final lastDate = DateTime(now.year + 1, now.month, now.day);
    final pickedDate = await showDatePicker(
      context: context,
      initialDate: _dateFrom ?? now,
      firstDate: firstDate,
      lastDate: lastDate,
    );
    if (pickedDate != null) {
      final pickedTime = await showTimePicker(
        context: context,
        initialTime: TimeOfDay(hour: 10, minute: 0),
      );
      if (pickedTime != null) {
        final combined = DateTime(
          pickedDate.year,
          pickedDate.month,
          pickedDate.day,
          pickedTime.hour,
          pickedTime.minute,
        );
        setState(() {
          _dateTo = combined;
          print("Date picker ${_dateTo}");
        });
      }
    } else {
      print("Picked date is null");
    }
  }

  Future<void> _searchAvailableHotels() async {
    if (_dateFrom == null || _dateTo == null) {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text("Please select check-in and check-out dates")),
      );
      return;
    }

    try {
      final data = await _hotelsProvider.searchAvailableHotels(
        adults:
            _selectedPass1, // ili možeš sabrati _selectedPass1 + _selectedPass2
        checkIn: _dateFrom!,
        checkOut: _dateTo!,
      );

      if (data.result.isEmpty) {
        ScaffoldMessenger.of(
          context,
        ).showSnackBar(SnackBar(content: Text("No available hotels found")));
      } else {
        Navigator.push(
          context,
          MaterialPageRoute(builder: (context) => Hotels(result: data)),
        );
      }
    } catch (e) {
      ScaffoldMessenger.of(
        context,
      ).showSnackBar(SnackBar(content: Text("Error: ${e.toString()}")));
    }
  }

  @override
  void didChangeDependencies() {
    // TODO: implement didChangeDependencies
    super.didChangeDependencies();
    _hotelsProvider = context.read<HotelsProvider>();
  }

  @override
  Widget build(BuildContext context) {
    return MasterScreen(
      title: "Hotels List",
      child: SingleChildScrollView(
        child: Container(child: Column(children: [_buildSearch()])),
      ),
    );
  }

  Widget _buildSearch() {
    return SingleChildScrollView(
      child: Container(
        decoration: BoxDecoration(color: const Color.fromRGBO(17, 45, 78, 1)),
        child: Padding(
          padding: const EdgeInsets.all(8.0),
          child: SingleChildScrollView(
            child: Column(
              children: [
                SizedBox(height: 20),
                Text(
                  "🏨HotelEase",
                  style: TextStyle(
                    fontSize: 32,
                    color: const Color.fromRGBO(255, 255, 255, 1),
                  ),
                ),
                SizedBox(height: 30),
                // Row(
                //   children: [
                //     Expanded(
                //       child: TextField(
                //         decoration: InputDecoration(labelText: "Name"),
                //       ),
                //     ),
                //     Expanded(child: Text("daddada")),
                //     ElevatedButton(
                //       onPressed: () async {
                //         print("Called");
                //         result = await _hotelsProvider.get();
                //         print("data ${result?.result[0].name}");
                //       },
                //       child: Text("Search"),
                //     ),
                //     ElevatedButton(
                //       onPressed: () async {
                //         print("Called");
                //         var data = await _hotelsProvider.get();
                //         print("data ${data.result[0].name}");
                //       },
                //       child: Text("Add"),
                //     ),
                //   ],
                // ),,
                DropdownButton<String>(
                  style: TextStyle(color: Colors.white),
                  dropdownColor: const Color.fromRGBO(17, 45, 78, 1),
                  hint: Text('Sort by', style: TextStyle(color: Colors.white)),
                  value: selectedSort,
                  items: sortOptions.map((option) {
                    return DropdownMenuItem(
                      value: option['value'],
                      child: Text(option['label']!),
                    );
                  }).toList(),
                  onChanged: (value) {
                    setState(() => selectedSort = value);
                  },
                ),
                CheckboxListTile(
                  title: Text(
                    "Free WiFi",
                    style: TextStyle(color: Colors.white),
                  ),
                  value: wifi,
                  onChanged: (value) => setState(() => wifi = value ?? false),
                ),
                CheckboxListTile(
                  tileColor: const Color.fromRGBO(
                    15,
                    30,
                    70,
                    1,
                  ), // boja kada nije selektovano
                  selectedTileColor: const Color.fromRGBO(15, 30, 70, 1),
                  title: Text("Parking", style: TextStyle(color: Colors.white)),
                  value: parking,
                  onChanged: (value) =>
                      setState(() => parking = value ?? false),
                ),
                CheckboxListTile(
                  title: Text("Pool", style: TextStyle(color: Colors.white)),
                  value: pool,
                  onChanged: (value) => setState(() => pool = value ?? false),
                ),

                Text("Price", style: TextStyle(color: Colors.white)),
                RangeSlider(
                  activeColor: const Color.fromRGBO(15, 30, 70, 1),
                  inactiveColor: Colors.white,
                  values: RangeValues(minPrice, maxPrice),
                  min: 0,
                  max: 1000,
                  divisions: 20,
                  labels: RangeLabels(
                    '${minPrice.toInt()}',
                    '${maxPrice.toInt()}',
                  ),
                  onChanged: (values) {
                    setState(() {
                      minPrice = values.start;
                      maxPrice = values.end;
                    });
                  },
                ),
                Row(
                  children: [
                    Expanded(
                      child: Container(
                        decoration: BoxDecoration(
                          color: Colors.white,
                          borderRadius: BorderRadius.only(
                            bottomLeft: Radius.circular(50),
                            topLeft: Radius.circular(50),
                          ),
                          boxShadow: [
                            BoxShadow(
                              color: Colors.black.withOpacity(0.1),
                              blurRadius: 5,
                              offset: Offset(0, 2),
                            ),
                          ],
                        ),
                        child: Padding(
                          padding: const EdgeInsets.fromLTRB(8, 8, 0, 8),
                          child: TextField(
                            onChanged: (value) {
                              setState(() {
                                searchQuery = value;
                              });
                            },
                            style: TextStyle(
                              color: const Color.fromRGBO(17, 45, 78, 1),
                            ),
                            decoration: InputDecoration(
                              hintText: "City ...",
                              hintStyle: TextStyle(color: Colors.grey),
                              // prefixIcon: Icon(Icons.search, color: Colors.grey),
                              border: InputBorder.none,
                              contentPadding: EdgeInsets.symmetric(
                                vertical: 10,
                              ),
                            ),
                          ),
                        ),
                      ),
                    ),
                    Padding(
                      padding: EdgeInsets.fromLTRB(0, 8, 8, 8),
                      child: Container(
                        height: 67,

                        decoration: BoxDecoration(
                          borderRadius: BorderRadius.only(
                            topRight: Radius.circular(50),
                            bottomRight: Radius.circular(50),
                          ),
                        ),
                        child: ElevatedButton(
                          style: ElevatedButton.styleFrom(
                            backgroundColor: Colors.white,
                            shape: RoundedRectangleBorder(
                              borderRadius: BorderRadius.only(
                                topRight: Radius.circular(50),
                                bottomRight: Radius.circular(50),
                              ),
                            ),
                          ),
                          onPressed: () async {
                            if (_dateFrom == null || _dateTo == null) {
                              ScaffoldMessenger.of(context).showSnackBar(
                                SnackBar(
                                  content: Text(
                                    "Please select check-in and check-out dates",
                                  ),
                                ),
                              );
                              return;
                            }

                            final data = await _hotelsProvider
                                .searchAvailableHotels(
                                  cityName: searchQuery.isEmpty
                                      ? null
                                      : searchQuery,
                                  adults: _selectedPass1 + _selectedPass2,
                                  rooms: _selectedroom,
                                  checkIn: _dateFrom!,
                                  checkOut: _dateTo!,
                                  minPrice: minPrice,
                                  maxPrice: maxPrice,
                                  wifi: wifi,
                                  parking: parking,
                                  pool: pool,
                                  sortBy: selectedSort,
                                );

                            if (data.result.isEmpty) {
                              ScaffoldMessenger.of(context).showSnackBar(
                                SnackBar(content: Text("No hotels found")),
                              );
                              return;
                            }

                            Navigator.push(
                              context,
                              MaterialPageRoute(
                                builder: (context) => Hotels(result: data),
                              ),
                            );
                          },
                          child: Icon(
                            Icons.search,
                            color: const Color.fromRGBO(17, 45, 78, 1),
                            size: 24,
                          ),
                        ),
                      ),
                    ),
                  ],
                ),

                SizedBox(height: 30),

                Center(
                  child: Row(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      GestureDetector(
                        onTap: _selectDateFrom,
                        child: Text(
                          _dateFrom == null
                              ? 'Check in date'
                              : formatter.format(_dateFrom!),
                          style: TextStyle(fontSize: 22, color: Colors.white),
                        ),
                      ),

                      IconButton(
                        onPressed: _selectDateFrom,
                        icon: Icon(Icons.calendar_month),
                        iconSize: 50,
                        color: Colors.white,
                      ),
                    ],
                  ),
                ),

                SizedBox(height: 15),
                Center(
                  child: Row(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      GestureDetector(
                        onTap: _selectDateTo,
                        child: Text(
                          _dateTo == null
                              ? 'Check out date'
                              : formatter.format(_dateTo!),
                          style: TextStyle(fontSize: 22, color: Colors.white),
                        ),
                      ),

                      IconButton(
                        onPressed: _selectDateTo,
                        icon: Icon(Icons.calendar_month),
                        iconSize: 50,
                        color: Colors.white,
                      ),
                    ],
                  ),
                ),
                SizedBox(height: 20),
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceAround,
                  children: [
                    Padding(
                      padding: const EdgeInsets.symmetric(
                        vertical: 10,
                        horizontal: 20,
                      ),
                      child: Row(
                        children: [
                          Text(
                            'Adults',
                            style: TextStyle(fontSize: 22, color: Colors.white),
                          ),
                          SizedBox(width: 20),
                          Container(
                            width: 50,
                            child: DropdownButtonFormField<int>(
                              dropdownColor: Color.fromARGB(255, 11, 13, 26),
                              value: _selectedPass1,
                              items: adults
                                  .map(
                                    (passenger) => DropdownMenuItem<int>(
                                      value: passenger,
                                      child: Text(
                                        passenger.toString(),
                                        style: TextStyle(
                                          color: Colors.white,
                                          fontSize: 18,
                                        ),
                                      ),
                                    ),
                                  )
                                  .toList(),
                              onChanged: (value) {
                                setState(() {
                                  _selectedPass1 = value!;
                                });
                              },
                            ),
                          ),
                        ],
                      ),
                    ),

                    Padding(
                      padding: const EdgeInsets.symmetric(
                        vertical: 10,
                        horizontal: 20,
                      ),
                      child: Row(
                        children: [
                          Text(
                            'Adults',
                            style: TextStyle(fontSize: 22, color: Colors.white),
                          ),
                          SizedBox(width: 20),
                          Container(
                            width: 50,
                            child: DropdownButtonFormField<int>(
                              dropdownColor: Color.fromARGB(255, 11, 13, 26),
                              value: _selectedPass2,
                              items: adults
                                  .map(
                                    (passenger) => DropdownMenuItem<int>(
                                      value: passenger,
                                      child: Text(
                                        passenger.toString(),
                                        style: TextStyle(
                                          color: Colors.white,
                                          fontSize: 18,
                                        ),
                                      ),
                                    ),
                                  )
                                  .toList(),
                              onChanged: (value) {
                                setState(() {
                                  _selectedPass2 = value!;
                                });
                              },
                            ),
                          ),
                        ],
                      ),
                    ),
                  ],
                ),
                SizedBox(height: 20),
                Text(
                  'Adults',
                  style: TextStyle(fontSize: 22, color: Colors.white),
                ),
                SizedBox(width: 20),
                Container(
                  width: 50,
                  child: DropdownButtonFormField<int>(
                    dropdownColor: Color.fromARGB(255, 11, 13, 26),
                    value: _selectedroom,
                    items: rooms
                        .map(
                          (passenger) => DropdownMenuItem<int>(
                            value: passenger,
                            child: Text(
                              passenger.toString(),
                              style: TextStyle(
                                color: Colors.white,
                                fontSize: 18,
                              ),
                            ),
                          ),
                        )
                        .toList(),
                    onChanged: (value) {
                      setState(() {
                        _selectedPass2 = value!;
                      });
                    },
                  ),
                ),
                SizedBox(height: 20),
                Text(
                  "POPULAR DESTINATIONS",
                  style: TextStyle(color: Colors.white, fontSize: 22),
                ),
                SizedBox(height: 30),
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceAround,
                  children: [
                    Container(
                      constraints: BoxConstraints(maxWidth: 150),

                      child: Column(
                        children: [
                          Image.network(
                            "https://encrypted-tbn0.gstatic.com/licensed-image?q=tbn:ANd9GcQ7GXv4aUNbTQ7uZCFs9xHCJEwYFxcDLse2oFWs6hem4lzlpogcVlP-2CQ8Lu0hYPGJSi5mbm36rb5E79A1VQtGbwFj3nKPAbEz29_1JD8",
                          ),
                          Text(
                            "Jajce",
                            style: TextStyle(color: Colors.white, fontSize: 18),
                          ),
                        ],
                      ),
                    ),
                    Container(
                      constraints: BoxConstraints(maxWidth: 150),
                      child: Column(
                        children: [
                          Image.network(
                            "https://lh3.googleusercontent.com/gps-cs-s/AC9h4noCxkjonP6IT0XE5Q8EHu5UBdjmUzEdhdiJDvBz8wc0zD7SJGifwJ9wtkwya3uSdAn8-N6HNCsbwkJCufqpTGUubCYr8GkQf1SdrnvdQM1bMqRfMflrRJ5ihIBzpLrQ8R41NkiKxw=w675-h390-n-k-no",
                          ),
                          Text(
                            "Travnik",
                            style: TextStyle(color: Colors.white, fontSize: 18),
                          ),
                        ],
                      ),
                    ),
                  ],
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }
}
