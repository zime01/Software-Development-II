import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:hotelease_mobile_new/models/asset.dart';
import 'package:hotelease_mobile_new/models/hotel.dart';
import 'package:hotelease_mobile_new/models/search_result.dart';
import 'package:hotelease_mobile_new/providers/hotels_provider.dart';
import 'package:hotelease_mobile_new/screens/hotel_details_screen.dart';
import 'package:hotelease_mobile_new/screens/hotels.dart';
import 'package:hotelease_mobile_new/screens/master_screen.dart';

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

  late Future<List<Hotel>> _recommendedHotelsFuture;
  late Future<List<Hotel>> _popularHotelsFuture;

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

  @override
  void didChangeDependencies() {
    // TODO: implement didChangeDependencies
    super.didChangeDependencies();
    _hotelsProvider = context.read<HotelsProvider>();
  }

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

  final usdFormatter = NumberFormat.currency(
    locale: 'en_US',
    symbol: "\$",
    decimalDigits: 2,
  );

  String formatPrice(int? value) {
    if (value == null) return "0.00";
    return usdFormatter.format(value.toDouble());
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

  Widget _buildHotelImage(String? base64Image) {
    if (base64Image == null || base64Image.isEmpty) {
      return Image.asset(
        'assets/images/cant_load_image.png',
        height: 100,
        fit: BoxFit.cover,
      );
    }

    try {
      // Backend ti šalje BASE64 ENCODED URL
      final decodedUrl = utf8.decode(base64.decode(base64Image));
      return Image.network(
        decodedUrl,
        height: 100,
        fit: BoxFit.cover,
        errorBuilder: (_, __, ___) => Image.asset(
          'assets/images/cant_load_image.png',
          height: 100,
          fit: BoxFit.cover,
        ),
      );
    } catch (e) {
      return Image.asset(
        'assets/images/cant_load_image.png',
        height: 100,
        fit: BoxFit.cover,
      );
    }
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
                            'Children',
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
                  'Rooms',
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
                SizedBox(height: 20),
                Text(
                  "Popular Hotels",
                  style: TextStyle(color: Colors.white, fontSize: 22),
                ),
                SizedBox(height: 15),
                FutureBuilder<List<Hotel>>(
                  future: _hotelsProvider.getPopularHotels(top: 5),
                  builder: (context, snapshot) {
                    if (snapshot.connectionState == ConnectionState.waiting) {
                      return CircularProgressIndicator(color: Colors.white);
                    }
                    if (!snapshot.hasData || snapshot.data!.isEmpty) {
                      return Text(
                        "No popular hotels",
                        style: TextStyle(color: Colors.white),
                      );
                    }

                    final hotels = snapshot.data!;
                    return SizedBox(
                      height: 220,
                      child: ListView.builder(
                        scrollDirection: Axis.horizontal,
                        itemCount: hotels.length,
                        itemBuilder: (context, index) {
                          final hotel = hotels[index];
                          final List<Asset> assets = [];
                          if (hotel.rooms != null) {
                            for (var r in hotel.rooms!) {
                              if (r.assets != null) {
                                assets.addAll(r.assets!);
                              }
                            }
                          }

                          return Container(
                            width: 160,
                            margin: EdgeInsets.all(8),
                            child: GestureDetector(
                              onTap: () {
                                Navigator.push(
                                  context,
                                  MaterialPageRoute(
                                    builder: (_) => HotelDetailsScreen(
                                      id: hotel.id!,
                                      name: hotel.name,
                                      starRating: hotel.starRating,
                                      price: hotel.price,
                                      description: hotel.description,
                                    ),
                                  ),
                                );
                              },
                              child: Column(
                                children: [
                                  Expanded(
                                    child: assets != null && assets.isNotEmpty
                                        ? PageView.builder(
                                            itemCount: assets.length,
                                            itemBuilder: (context, i) {
                                              return _buildHotelImage(
                                                assets[i].image,
                                              );
                                            },
                                          )
                                        : Image.asset(
                                            'assets/images/cant_load_image.png',
                                            fit: BoxFit.cover,
                                          ),
                                  ),
                                  SizedBox(height: 5),
                                  Text(
                                    hotel.name ?? "",
                                    style: TextStyle(
                                      color: Colors.white,
                                      fontSize: 16,
                                    ),
                                    overflow: TextOverflow.ellipsis,
                                  ),
                                  SizedBox(height: 5),
                                  Text(
                                    hotel.city?.name ?? "Unknown city",
                                    style: TextStyle(color: Colors.white),
                                    overflow: TextOverflow.ellipsis,
                                  ),
                                  SizedBox(height: 5),
                                  Text(
                                    "${hotel.starRating} ★",
                                    style: TextStyle(color: Colors.yellow),
                                  ),
                                  SizedBox(height: 5),
                                  Text(
                                    "${formatPrice(hotel.minPrice)} - ${formatPrice(hotel.maxPrice)} /night",
                                    style: TextStyle(
                                      color: Colors.white70,
                                      fontSize: 12,
                                    ),
                                  ),
                                ],
                              ),
                            ),
                          );
                        },
                      ),
                    );
                  },
                ),
                Text(
                  "Hotels Recommended For You",
                  style: TextStyle(color: Colors.white, fontSize: 22),
                ),
                SizedBox(height: 15),
                FutureBuilder<List<Hotel>>(
                  future: _hotelsProvider.getCollaborativeHotels(1, top: 5),
                  builder: (context, snapshot) {
                    if (snapshot.connectionState == ConnectionState.waiting) {
                      return CircularProgressIndicator(color: Colors.white);
                    }
                    if (!snapshot.hasData || snapshot.data!.isEmpty) {
                      return Text(
                        "No popular hotels",
                        style: TextStyle(color: Colors.white),
                      );
                    }

                    final hotels = snapshot.data!;
                    return SizedBox(
                      height: 220,
                      child: ListView.builder(
                        scrollDirection: Axis.horizontal,
                        itemCount: hotels.length,
                        itemBuilder: (context, index) {
                          final hotel = hotels[index];
                          final List<Asset> assets = [];
                          if (hotel.rooms != null) {
                            for (var r in hotel.rooms!) {
                              if (r.assets != null) {
                                assets.addAll(r.assets!);
                              }
                            }
                          }

                          return Container(
                            width: 160,
                            margin: EdgeInsets.all(8),
                            child: GestureDetector(
                              onTap: () {
                                Navigator.push(
                                  context,
                                  MaterialPageRoute(
                                    builder: (_) => HotelDetailsScreen(
                                      id: hotel.id!,
                                      name: hotel.name,
                                      starRating: hotel.starRating,
                                      price: hotel.price,
                                      description: hotel.description,
                                    ),
                                  ),
                                );
                              },
                              child: Column(
                                children: [
                                  Expanded(
                                    child: assets != null && assets.isNotEmpty
                                        ? PageView.builder(
                                            itemCount: assets.length,
                                            itemBuilder: (context, i) {
                                              return _buildHotelImage(
                                                assets[i].image,
                                              );
                                            },
                                          )
                                        : Image.asset(
                                            'assets/images/cant_load_image.png',
                                            fit: BoxFit.cover,
                                          ),
                                  ),
                                  SizedBox(height: 5),
                                  Text(
                                    hotel.name ?? "",
                                    style: TextStyle(color: Colors.white),
                                    overflow: TextOverflow.ellipsis,
                                  ),
                                  SizedBox(height: 5),
                                  Text(
                                    hotel.city?.name ?? "Unknown city",
                                    style: TextStyle(color: Colors.white),
                                    overflow: TextOverflow.ellipsis,
                                  ),
                                  SizedBox(height: 5),
                                  Text(
                                    "${hotel.starRating} ★",
                                    style: TextStyle(color: Colors.yellow),
                                  ),
                                  SizedBox(height: 5),
                                  Text(
                                    "${formatPrice(hotel.minPrice)} - ${formatPrice(hotel.maxPrice)} /night",
                                    style: TextStyle(
                                      color: Colors.white70,
                                      fontSize: 12,
                                    ),
                                  ),
                                ],
                              ),
                            ),
                          );
                        },
                      ),
                    );
                  },
                ),

                SizedBox(height: 30),
              ],
            ),
          ),
        ),
      ),
    );
  }
}
