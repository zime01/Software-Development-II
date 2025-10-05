import 'dart:async';
import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:hotelease_mobile_new/models/asset.dart';
import 'package:hotelease_mobile_new/models/hotel.dart';
import 'package:hotelease_mobile_new/models/search_result.dart';
import 'package:hotelease_mobile_new/providers/assets_provider.dart';
import 'package:hotelease_mobile_new/providers/hotels_provider.dart';
import 'package:hotelease_mobile_new/providers/reviews_provider.dart';
import 'package:hotelease_mobile_new/screens/hotel_details_screen.dart';
import 'package:hotelease_mobile_new/screens/master_screen.dart';

import 'package:provider/provider.dart';

class Hotels extends StatefulWidget {
  final SearchResult<Hotel>? result;

  const Hotels({super.key, this.result});

  @override
  State<Hotels> createState() => _HotelsState();
}

class _HotelsState extends State<Hotels> {
  late AssetsProvider _assetsProvider;
  late HotelsProvider _hotelsProvider;
  late ReviewsProvider _reviewsProvider;
  final Map<int, double> _hotelRatings = {};

  final Map<int, List<Asset>> _hotelAssets = {};
  final Map<int, PageController> _pageControllers = {};
  final Map<int, Timer> _timers = {};

  String? selectedSort;
  String? selectedFilter;

  @override
  void initState() {
    super.initState();
    Future.microtask(() => _loadAllHotelImages());
  }

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
    _assetsProvider = context.read<AssetsProvider>();
    _hotelsProvider = context.read<HotelsProvider>();
    _reviewsProvider = context.read<ReviewsProvider>();
  }

  @override
  void dispose() {
    for (var controller in _pageControllers.values) {
      controller.dispose();
    }
    for (var timer in _timers.values) {
      timer.cancel();
    }
    super.dispose();
  }

  Future<void> _loadHotels() async {
    var result = await _hotelsProvider.searchAvailableHotels(
      sortBy: selectedSort,
      wifi: selectedFilter == "wifi" ? true : null,
      parking: selectedFilter == "parking" ? true : null,
      pool: selectedFilter == "pool" ? true : null,
      bar: selectedFilter == "bar" ? true : null,
      fitness: selectedFilter == "fitness" ? true : null,
      spa: selectedFilter == "spa" ? true : null,
    );
    setState(() {
      widget.result?.result.clear();
      widget.result?.result.addAll(result.result);
      _loadAllHotelImages();
    });
  }

  Widget buildStarRating(int? rating) {
    return Row(
      children: List.generate(5, (index) {
        return Icon(
          index < (rating ?? 0) ? Icons.star : Icons.star_border,
          color: Colors.white,
          size: 20,
        );
      }),
    );
  }

  Future<void> _loadAllHotelImages() async {
    if (widget.result?.result == null) return;

    await Future.wait(
      widget.result!.result.map((hotel) async {
        final reviews = await _reviewsProvider.getByHotelId(hotel.id ?? 0);

        if (reviews.isNotEmpty) {
          final avg =
              reviews.map((r) => r.rating).reduce((a, b) => a + b) /
              reviews.length;
          _hotelRatings[hotel.id ?? 0] = avg;
        } else {
          _hotelRatings[hotel.id ?? 0] = 0;
        }
        try {
          final assets = await _assetsProvider.getAssetsByHotelId(
            hotel.id ?? 0,
          );
          _hotelAssets[hotel.id ?? 0] = assets.result;

          final controller = PageController();
          _pageControllers[hotel.id ?? 0] = controller;

          if (assets.result.isNotEmpty) {
            // Pokrećemo timer tek nakon što je PageView izgrađen
            WidgetsBinding.instance.addPostFrameCallback((_) {
              _timers[hotel.id ?? 0] = Timer.periodic(
                const Duration(seconds: 3),
                (timer) {
                  if (!mounted) {
                    timer.cancel();
                    return;
                  }
                  if (controller.hasClients) {
                    int currentPage = controller.page?.toInt() ?? 0;
                    int nextPage = currentPage + 1;
                    if (nextPage >= assets.result.length) nextPage = 0;
                    controller.animateToPage(
                      nextPage,
                      duration: const Duration(milliseconds: 300),
                      curve: Curves.easeInOut,
                    );
                  }
                },
              );
            });
          }
        } catch (_) {
          _hotelAssets[hotel.id ?? 0] = [];
        }
      }),
    );

    if (mounted) setState(() {});
  }

  Widget buildHotelImages(int hotelId, List<Asset> assets) {
    if (assets.isEmpty) {
      return Image.asset(
        'assets/images/cant_load_image.png',
        width: double.infinity,
        height: 200,
        fit: BoxFit.cover,
      );
    }

    final controller = _pageControllers[hotelId]!;

    return SizedBox(
      width: double.infinity,
      height: 200,
      child: PageView.builder(
        controller: controller,
        itemCount: assets.length,
        itemBuilder: (context, index) {
          final imageString = assets[index].image;
          if (imageString == null || imageString.isEmpty) {
            return Image.asset(
              'assets/images/cant_load_image.png',
              fit: BoxFit.cover,
            );
          }
          try {
            final decodedUrl = utf8.decode(base64.decode(imageString));
            if (decodedUrl.startsWith('http')) {
              return Image.network(
                decodedUrl,
                fit: BoxFit.cover,
                errorBuilder: (_, __, ___) => Image.asset(
                  'assets/images/cant_load_image.png',
                  fit: BoxFit.cover,
                ),
              );
            }
          } catch (_) {}
          return Image.asset(
            'assets/images/cant_load_image.png',
            fit: BoxFit.cover,
          );
        },
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    final hotels = widget.result?.result ?? [];

    return MasterScreen(
      title: "List of Hotels",
      child: Column(
        children: [
          Padding(
            padding: const EdgeInsets.all(8.0),
            child: Row(
              children: [
                Expanded(
                  child: DropdownButtonFormField<String>(
                    style: TextStyle(color: Colors.white),
                    focusColor: const Color.fromRGBO(15, 30, 70, 1),
                    dropdownColor: Theme.of(context).colorScheme.primary,
                    value: selectedSort,
                    decoration: InputDecoration(
                      labelText: "Sort by",
                      labelStyle: TextStyle(color: Colors.white),
                      // filled: true,
                      // fillColor: Colors.white,
                    ),
                    items: const [
                      DropdownMenuItem(
                        value: "price_asc",
                        child: Text("Price: Low to High"),
                      ),
                      DropdownMenuItem(
                        value: "price_desc",
                        child: Text("Price: High to Low"),
                      ),
                      DropdownMenuItem(
                        value: "rating",
                        child: Text("Star Rating"),
                      ),
                    ],
                    onChanged: (val) {
                      setState(() => selectedSort = val);
                      _loadHotels();
                    },
                  ),
                ),
                const SizedBox(width: 10),
                Expanded(
                  child: DropdownButtonFormField<String>(
                    dropdownColor: Theme.of(context).colorScheme.primary,
                    value: selectedFilter,
                    decoration: InputDecoration(
                      labelText: "Filter by",
                      labelStyle: TextStyle(color: Colors.white),
                      // filled: true,
                      // fillColor: Colors.white,
                    ),
                    items: const [
                      DropdownMenuItem(
                        value: "wifi",
                        child: Text(
                          "WiFi",
                          style: TextStyle(color: Colors.white),
                        ),
                      ),
                      DropdownMenuItem(
                        value: "parking",
                        child: Text(
                          "Parking",
                          style: TextStyle(color: Colors.white),
                        ),
                      ),
                      DropdownMenuItem(
                        value: "pool",
                        child: Text(
                          "Pool",
                          style: TextStyle(color: Colors.white),
                        ),
                      ),
                      DropdownMenuItem(
                        value: "bar",
                        child: Text(
                          "Bar",
                          style: TextStyle(color: Colors.white),
                        ),
                      ),
                      DropdownMenuItem(
                        value: "fitness",
                        child: Text(
                          "Fitness",
                          style: TextStyle(color: Colors.white),
                        ),
                      ),
                      DropdownMenuItem(
                        value: "spa",
                        child: Text(
                          "Spa",
                          style: TextStyle(color: Colors.white),
                        ),
                      ),
                    ],
                    onChanged: (val) {
                      setState(() => selectedFilter = val);
                      _loadHotels();
                    },
                  ),
                ),
              ],
            ),
          ),
          Expanded(
            child: ListView.builder(
              itemCount: hotels.length,
              itemBuilder: (context, index) {
                final hotel = hotels[index];
                final assets = _hotelAssets[hotel.id ?? 0] ?? [];

                return Card(
                  margin: const EdgeInsets.symmetric(
                    horizontal: 12,
                    vertical: 8,
                  ),
                  color: Theme.of(context).colorScheme.secondary,
                  child: InkWell(
                    onTap: () {
                      Navigator.of(context).push(
                        MaterialPageRoute(
                          builder: (_) => HotelDetailsScreen(
                            name: hotel.name,
                            starRating: hotel.starRating,
                            price: hotel.price,
                            description: hotel.description,
                            id: hotel.id,
                            hotel: hotel,
                          ),
                        ),
                      );
                    },
                    child: Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        ClipRRect(
                          borderRadius: const BorderRadius.vertical(
                            top: Radius.circular(8),
                          ),
                          child: buildHotelImages(hotel.id ?? 0, assets),
                        ),
                        Padding(
                          padding: const EdgeInsets.all(8.0),
                          child: Column(
                            crossAxisAlignment: CrossAxisAlignment.start,
                            children: [
                              Row(
                                mainAxisAlignment:
                                    MainAxisAlignment.spaceBetween,
                                children: [
                                  Text(
                                    hotel.name ?? "Unknown Hotel",
                                    style: TextStyle(
                                      color: Theme.of(
                                        context,
                                      ).colorScheme.onPrimary,
                                      fontWeight: FontWeight.bold,
                                      fontSize: 20,
                                    ),
                                  ),
                                  buildStarRating(hotel.starRating),
                                ],
                              ),
                              const SizedBox(height: 8),
                              Row(
                                children: [
                                  if (hotel.wifi == true)
                                    const Icon(
                                      Icons.wifi,
                                      color: Colors.green,
                                      size: 20,
                                    ),
                                  if (hotel.parking == true)
                                    const Icon(
                                      Icons.local_parking,
                                      color: Colors.blue,
                                      size: 20,
                                    ),
                                  if (hotel.pool == true)
                                    const Icon(
                                      Icons.pool,
                                      color: Colors.cyan,
                                      size: 20,
                                    ),
                                  if (hotel.bar == true)
                                    const Icon(
                                      Icons.local_bar,
                                      color: Colors.purple,
                                      size: 20,
                                    ),
                                  if (hotel.fitness == true)
                                    const Icon(
                                      Icons.fitness_center,
                                      color: Colors.red,
                                      size: 20,
                                    ),
                                  if (hotel.spa == true)
                                    const Icon(
                                      Icons.spa,
                                      color: Colors.orange,
                                      size: 20,
                                    ),
                                ],
                              ),
                              const SizedBox(height: 12),
                              Row(
                                children: [
                                  const Icon(
                                    Icons.location_on,
                                    color: Colors.red,
                                    size: 20,
                                  ),
                                  Text(
                                    "${hotel.address ?? "Unknown City"}",
                                    style: TextStyle(
                                      color: Theme.of(
                                        context,
                                      ).colorScheme.onPrimary,
                                    ),
                                  ),
                                ],
                              ),
                              const SizedBox(height: 12),
                              if (_hotelRatings.containsKey(hotel.id))
                                Row(
                                  children: [
                                    Text(
                                      "Reviews",
                                      style: TextStyle(
                                        color: Theme.of(
                                          context,
                                        ).colorScheme.onPrimary,
                                      ),
                                    ),
                                    const Icon(
                                      Icons.star,
                                      color: Colors.amber,
                                      size: 18,
                                    ),
                                    const SizedBox(width: 4),
                                    Text(
                                      "${_hotelRatings[hotel.id]!.toStringAsFixed(1)}/5",
                                      style: TextStyle(
                                        color: Theme.of(
                                          context,
                                        ).colorScheme.onPrimary,
                                      ),
                                    ),
                                  ],
                                ),
                              const SizedBox(height: 12),
                              Center(
                                child: Row(
                                  mainAxisAlignment: MainAxisAlignment.center,
                                  children: [
                                    Text(
                                      "${hotel.price?.toStringAsFixed(2)} \$",
                                      style: TextStyle(
                                        fontSize: 24,
                                        fontWeight: FontWeight.bold,
                                        color: Theme.of(
                                          context,
                                        ).colorScheme.onPrimary,
                                      ),
                                    ),
                                    Text(
                                      "/night",
                                      style: TextStyle(
                                        fontSize: 22,
                                        color: Theme.of(
                                          context,
                                        ).colorScheme.onPrimary,
                                      ),
                                    ),
                                  ],
                                ),
                              ),
                            ],
                          ),
                        ),
                      ],
                    ),
                  ),
                );
              },
            ),
          ),
        ],
      ),
    );
  }
}
