import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:hotelease_mobile_new/models/asset.dart';
import 'package:hotelease_mobile_new/models/hotel.dart';
import 'package:hotelease_mobile_new/models/search_result.dart';
import 'package:hotelease_mobile_new/providers/hotels_provider.dart';
import 'package:hotelease_mobile_new/providers/search_provider.dart';
import 'package:hotelease_mobile_new/screens/hotel_details_screen.dart';
import 'package:hotelease_mobile_new/screens/hotels.dart';
import 'package:hotelease_mobile_new/screens/master_screen.dart';
import 'package:hotelease_mobile_new/utils/USDformat.dart';
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

  DateTime? _dateFrom;
  DateTime? _dateTo;
  int _adults = 1;
  int _children = 0;
  int _rooms = 1;

  String searchQuery = "";
  String? selectedSort;
  double minPrice = 0;
  double maxPrice = 500;
  bool wifi = false;
  bool parking = false;
  bool pool = false;

  List<Map<String, String>> sortOptions = [
    {'label': 'Lowest Price', 'value': 'PriceAsc'},
    {'label': 'Highest Price', 'value': 'PriceDesc'},
    {'label': 'Highest Stars', 'value': 'StarRatingDesc'},
  ];

  @override
  void initState() {
    super.initState();
    _hotelsProvider = context.read<HotelsProvider>();
    _restorePreviousSearch();
  }

  void _restorePreviousSearch() {
    final params = context.read<SearchProvider>().params;
    if (params != null) {
      setState(() {
        _dateFrom = params.checkInDate;
        _dateTo = params.checkOutDate;
        _adults = params.guests;
      });
    }
  }

  Future<void> _selectDateFrom() async {
    final now = DateTime.now();
    final picked = await showDatePicker(
      context: context,
      initialDate: _dateFrom ?? now,
      firstDate: now,
      lastDate: DateTime(now.year + 1),
    );
    if (picked != null) {
      final time = await showTimePicker(
        context: context,
        initialTime: const TimeOfDay(hour: 10, minute: 0),
      );
      if (time != null) {
        setState(() {
          _dateFrom = DateTime(
            picked.year,
            picked.month,
            picked.day,
            time.hour,
            time.minute,
          );
        });
      }
    }
  }

  Future<void> _selectDateTo() async {
    final now = DateTime.now();
    final picked = await showDatePicker(
      context: context,
      initialDate: _dateTo ?? _dateFrom ?? now,
      firstDate: now,
      lastDate: DateTime(now.year + 1),
    );
    if (picked != null) {
      final time = await showTimePicker(
        context: context,
        initialTime: const TimeOfDay(hour: 10, minute: 0),
      );
      if (time != null) {
        setState(() {
          _dateTo = DateTime(
            picked.year,
            picked.month,
            picked.day,
            time.hour,
            time.minute,
          );
        });
      }
    }
  }

  Future<void> _searchAvailableHotels() async {
    if (_dateFrom == null || _dateTo == null) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(
          content: Text("Please select check-in and check-out dates"),
        ),
      );
      return;
    }

    final totalGuests = _adults + _children;

    context.read<SearchProvider>().setParams(
      SearchParams(
        checkInDate: _dateFrom!,
        checkOutDate: _dateTo!,
        guests: totalGuests,
      ),
    );

    try {
      final data = await _hotelsProvider.searchAvailableHotels(
        cityName: searchQuery.isEmpty ? null : searchQuery,
        adults: totalGuests,
        rooms: _rooms,
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
        ScaffoldMessenger.of(
          context,
        ).showSnackBar(const SnackBar(content: Text("No hotels found")));
        return;
      }

      Navigator.push(
        context,
        MaterialPageRoute(builder: (context) => Hotels(result: data)),
      ).then((_) => _restorePreviousSearch());
    } catch (e) {
      ScaffoldMessenger.of(
        context,
      ).showSnackBar(SnackBar(content: Text("Error: ${e.toString()}")));
    }
  }

  Widget _buildHotelImage(String? base64Image) {
    const double imgHeight = 100;

    if (base64Image == null || base64Image.isEmpty) {
      return Image.asset(
        'assets/images/cant_load_image.png',
        height: imgHeight,
        fit: BoxFit.cover,
      );
    }

    try {
      // 1) Decode base64
      final decoded = base64.decode(base64Image);

      // 2) Pokušaj interpretirati kao UTF-8 (možda je URL enkodiran u base64)
      final asString = utf8.decode(decoded, allowMalformed: true);

      // 3) Ako je asString URL → prikaži network image
      if (asString.startsWith("http://") || asString.startsWith("https://")) {
        return Image.network(
          asString,
          height: imgHeight,
          fit: BoxFit.cover,
          errorBuilder: (_, __, ___) => Image.asset(
            'assets/images/cant_load_image.png',
            height: imgHeight,
            fit: BoxFit.cover,
          ),
        );
      }

      // 4) Inače → raw slika (PNG/JPEG)
      return Image.memory(decoded, height: imgHeight, fit: BoxFit.cover);
    } catch (e) {
      print("Hotel image decode error: $e");

      return Image.asset(
        'assets/images/cant_load_image.png',
        height: imgHeight,
        fit: BoxFit.cover,
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    return MasterScreen(
      title: "Hotels List",
      child: SingleChildScrollView(
        child: Container(
          color: Theme.of(context).colorScheme.primary,
          child: Padding(
            padding: const EdgeInsets.all(8.0),
            child: Column(
              children: [
                const SizedBox(height: 20),
                Text(
                  "🏨 HotelEase",
                  style: TextStyle(
                    fontSize: 32,
                    color: Theme.of(context).colorScheme.onPrimary,
                  ),
                ),
                const SizedBox(height: 20),
                _buildFilters(),
                const SizedBox(height: 30),
                _buildDatePickers(),
                const SizedBox(height: 20),
                _buildGuestsAndRooms(),
                const SizedBox(height: 30),
                ElevatedButton.icon(
                  style: ElevatedButton.styleFrom(
                    backgroundColor: Colors.white,
                    padding: const EdgeInsets.symmetric(
                      horizontal: 20,
                      vertical: 12,
                    ),
                  ),
                  onPressed: _searchAvailableHotels,
                  icon: const Icon(
                    Icons.search,
                    color: Color.fromRGBO(17, 45, 78, 1),
                  ),
                  label: const Text(
                    "Search Hotels",
                    style: TextStyle(color: Color.fromRGBO(17, 45, 78, 1)),
                  ),
                ),
                const SizedBox(height: 40),
                _sectionTitle("🌟 Popular Hotels"),
                const SizedBox(height: 10),
                _buildPopularHotels(),
                const SizedBox(height: 40),
                _sectionTitle("✨ Recommended For You"),
                const SizedBox(height: 10),
                _buildRecommendedHotels(),
              ],
            ),
          ),
        ),
      ),
    );
  }

  Widget _sectionTitle(String title) {
    return Align(
      alignment: Alignment.centerLeft,
      child: Text(
        title,
        style: TextStyle(
          color: Theme.of(context).colorScheme.onPrimary,
          fontSize: 26,
          fontWeight: FontWeight.bold,
        ),
      ),
    );
  }

  Widget _buildFilters() {
    return Column(
      children: [
        DropdownButton<String>(
          style: TextStyle(color: Theme.of(context).colorScheme.onPrimary),
          dropdownColor: Theme.of(context).colorScheme.primary,
          hint: const Text('Sort by', style: TextStyle(color: Colors.white)),
          value: selectedSort,
          items: sortOptions.map((option) {
            return DropdownMenuItem(
              value: option['value'],
              child: Text(option['label']!),
            );
          }).toList(),
          onChanged: (value) => setState(() => selectedSort = value),
        ),
        CheckboxListTile(
          title: Text(
            "Free WiFi",
            style: TextStyle(color: Theme.of(context).colorScheme.onPrimary),
          ),
          value: wifi,
          onChanged: (v) => setState(() => wifi = v ?? false),
        ),
        CheckboxListTile(
          title: Text(
            "Parking",
            style: TextStyle(color: Theme.of(context).colorScheme.onPrimary),
          ),
          value: parking,
          onChanged: (v) => setState(() => parking = v ?? false),
        ),
        CheckboxListTile(
          title: Text(
            "Pool",
            style: TextStyle(color: Theme.of(context).colorScheme.onPrimary),
          ),
          value: pool,
          onChanged: (v) => setState(() => pool = v ?? false),
        ),
        Text(
          "Price Range",
          style: TextStyle(color: Theme.of(context).colorScheme.onPrimary),
        ),
        RangeSlider(
          activeColor: Colors.blueAccent,
          inactiveColor: Colors.white24,
          values: RangeValues(minPrice, maxPrice),
          min: 0,
          max: 1000,
          divisions: 20,
          labels: RangeLabels('${minPrice.toInt()}', '${maxPrice.toInt()}'),
          onChanged: (values) => setState(() {
            minPrice = values.start;
            maxPrice = values.end;
          }),
        ),
        const SizedBox(height: 10),
        TextField(
          style: TextStyle(color: Color.fromRGBO(17, 45, 78, 1)),
          decoration: InputDecoration(
            filled: true,
            fillColor: Colors.white,
            hintText: "City...",
            labelStyle: TextStyle(
              color: Theme.of(context).colorScheme.onPrimary,
            ),

            prefixIcon: Icon(
              Icons.location_city,
              color: Theme.of(context).colorScheme.primary,
            ),
            border: OutlineInputBorder(borderRadius: BorderRadius.circular(50)),
          ),
          onChanged: (v) => setState(() => searchQuery = v),
        ),
      ],
    );
  }

  Widget _buildDatePickers() {
    return Column(
      children: [
        GestureDetector(
          onTap: _selectDateFrom,
          child: Text(
            _dateFrom == null ? "Check-in Date" : formatter.format(_dateFrom!),
            style: TextStyle(
              color: Theme.of(context).colorScheme.onPrimary,
              fontSize: 20,
            ),
          ),
        ),
        const SizedBox(height: 10),
        GestureDetector(
          onTap: _selectDateTo,
          child: Text(
            _dateTo == null ? "Check-out Date" : formatter.format(_dateTo!),
            style: TextStyle(
              color: Theme.of(context).colorScheme.onPrimary,
              fontSize: 20,
            ),
          ),
        ),
      ],
    );
  }

  Widget _buildGuestsAndRooms() {
    return Column(
      children: [
        Row(
          mainAxisAlignment: MainAxisAlignment.spaceAround,
          children: [
            _dropdown(
              "Adults",
              _adults,
              (v) => setState(() => _adults = v!),
              startFrom: 1,
            ),
            _dropdown(
              "Children",
              _children,
              (v) => setState(() => _children = v!),
              startFrom: 0,
            ),
          ],
        ),
        const SizedBox(height: 10),
        _dropdown(
          "Rooms",
          _rooms,
          (v) => setState(() => _rooms = v!),
          startFrom: 1,
        ),
      ],
    );
  }

  Widget _dropdown(
    String label,
    int value,
    ValueChanged<int?> onChanged, {
    int startFrom = 1,
  }) {
    return Column(
      children: [
        Text(
          label,
          style: TextStyle(
            color: Theme.of(context).colorScheme.onPrimary,
            fontSize: 20,
          ),
        ),
        DropdownButton<int>(
          dropdownColor: Theme.of(context).colorScheme.primary,
          value: value,
          items: List.generate(10 - startFrom + 1, (i) => i + startFrom)
              .map(
                (num) => DropdownMenuItem<int>(
                  value: num,
                  child: Text(
                    num.toString(),
                    style: TextStyle(
                      color: Theme.of(context).colorScheme.onPrimary,
                    ),
                  ),
                ),
              )
              .toList(),
          onChanged: onChanged,
        ),
      ],
    );
  }

  Widget _buildPopularHotels() {
    return FutureBuilder<List<Hotel>>(
      future: _hotelsProvider.getPopularHotels(top: 5),
      builder: (context, snapshot) {
        if (snapshot.connectionState == ConnectionState.waiting) {
          return CircularProgressIndicator(
            color: Theme.of(context).colorScheme.onPrimary,
          );
        }
        if (!snapshot.hasData || snapshot.data!.isEmpty) {
          return Text(
            "No popular hotels",
            style: TextStyle(color: Theme.of(context).colorScheme.onPrimary),
          );
        }
        return _horizontalHotelList(snapshot.data!);
      },
    );
  }

  Widget _buildRecommendedHotels() {
    return FutureBuilder<List<Hotel>>(
      future: _hotelsProvider.getCollaborativeHotels(1, top: 5),
      builder: (context, snapshot) {
        if (snapshot.connectionState == ConnectionState.waiting) {
          return CircularProgressIndicator(
            color: Theme.of(context).colorScheme.onPrimary,
          );
        }
        if (!snapshot.hasData || snapshot.data!.isEmpty) {
          return Text(
            "No recommendations",
            style: TextStyle(color: Theme.of(context).colorScheme.onPrimary),
          );
        }
        return _horizontalHotelList(snapshot.data!);
      },
    );
  }

  Widget _horizontalHotelList(List<Hotel> hotels) {
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
              if (r.assets != null) assets.addAll(r.assets!);
            }
          }
          return Container(
            width: 160,
            margin: const EdgeInsets.all(8),
            child: GestureDetector(
              onTap: () {
                Navigator.push(
                  context,
                  MaterialPageRoute(
                    builder: (_) => HotelDetailsScreen(
                      id: hotel.id,
                      name: hotel.name,
                      starRating: hotel.starRating,
                      price: hotel.price,
                      description: hotel.description,
                      hotel: hotel,
                    ),
                  ),
                ).then((_) => _restorePreviousSearch());
              },
              child: Column(
                children: [
                  Expanded(
                    child: assets.isNotEmpty
                        ? PageView.builder(
                            itemCount: assets.length,
                            itemBuilder: (context, i) =>
                                _buildHotelImage(assets[i].image),
                          )
                        : Image.asset(
                            'assets/images/cant_load_image.png',
                            fit: BoxFit.cover,
                          ),
                  ),
                  const SizedBox(height: 5),
                  Text(
                    hotel.name ?? "",
                    style: TextStyle(
                      color: Theme.of(context).colorScheme.onPrimary,
                      fontSize: 16,
                    ),
                    overflow: TextOverflow.ellipsis,
                  ),
                  SizedBox(height: 5),
                  Text(
                    hotel.city?.name ?? "Unknown city",
                    style: TextStyle(
                      color: Theme.of(context).colorScheme.onPrimary,
                    ),
                    overflow: TextOverflow.ellipsis,
                  ),
                  SizedBox(height: 5),
                  Text(
                    "${hotel.starRating} ★",
                    style: TextStyle(
                      color: Colors.yellow,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                  SizedBox(height: 5),
                  Text(
                    "${formatPrice(hotel.minPrice)} - ${formatPrice(hotel.maxPrice)} /night",
                    style: TextStyle(
                      color: Theme.of(context).colorScheme.onPrimary,
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
  }
}
