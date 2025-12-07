import 'package:flutter/material.dart';
import 'package:hotelease_mobile_new/models/city.dart';
import 'package:hotelease_mobile_new/models/hotel.dart';
import 'package:hotelease_mobile_new/providers/cities_provider.dart';
import 'package:hotelease_mobile_new/providers/hotels_provider.dart';
import 'package:hotelease_mobile_new/screens/master_screen.dart';

class HotelFormScreen extends StatefulWidget {
  final Hotel? hotel;

  const HotelFormScreen({super.key, this.hotel});

  @override
  State<HotelFormScreen> createState() => _HotelFormScreenState();
}

class _HotelFormScreenState extends State<HotelFormScreen> {
  final _formKey = GlobalKey<FormState>();

  final HotelsProvider _hotelProvider = HotelsProvider();
  final CitiesProvider _citiesProvider = CitiesProvider();

  late TextEditingController _nameController;
  late TextEditingController _addressController;
  late TextEditingController _descriptionController;
  late TextEditingController _starRatingController;

  List<City> _cities = [];
  int? _selectedCityId;

  bool _loading = true;

  @override
  void initState() {
    super.initState();

    _nameController = TextEditingController(text: widget.hotel?.name ?? "");
    _addressController = TextEditingController(
      text: widget.hotel?.address ?? "",
    );
    _descriptionController = TextEditingController(
      text: widget.hotel?.description ?? "",
    );
    _starRatingController = TextEditingController(
      text: widget.hotel?.starRating?.toString() ?? "3",
    );

    _loadCities();
  }

  Future<void> _loadCities() async {
    try {
      final result = await _citiesProvider.get();
      _cities = result.result;
      if (widget.hotel?.cityId != null) {
        _selectedCityId = widget.hotel!.cityId;
      }
    } catch (e) {
      print("Error loading cities: $e");
    } finally {
      setState(() {
        _loading = false;
      });
    }
  }

  Future<void> _saveHotel() async {
    if (!_formKey.currentState!.validate()) return;

    if (_selectedCityId == null) {
      ScaffoldMessenger.of(
        context,
      ).showSnackBar(const SnackBar(content: Text("Please select a city")));
      return;
    }

    final payload = {
      "name": _nameController.text,
      "address": _addressController.text,
      "description": _descriptionController.text,
      "cityId": _selectedCityId,
      "countryId": 1,
      "managerId": 1,
      "starRating": int.tryParse(_starRatingController.text) ?? 3,
      "isActive": true,
    };

    if (widget.hotel == null) {
      await _hotelProvider.insert(payload);
    } else {
      await _hotelProvider.update(widget.hotel!.id!, payload);
    }

    Navigator.pop(context, true);
  }

  @override
  Widget build(BuildContext context) {
    return MasterScreen(
      title: widget.hotel == null ? "Add Hotel" : "Edit Hotel",
      child: _loading
          ? const Center(child: CircularProgressIndicator())
          : Padding(
              padding: const EdgeInsets.all(16),
              child: Form(
                key: _formKey,
                child: ListView(
                  children: [
                    TextFormField(
                      controller: _nameController,
                      style: const TextStyle(color: Colors.white),
                      decoration: const InputDecoration(
                        labelText: "Hotel Name",
                        labelStyle: TextStyle(color: Colors.grey),
                      ),
                      validator: (val) => val!.isEmpty ? "Enter name" : null,
                    ),
                    const SizedBox(height: 10),
                    TextFormField(
                      controller: _addressController,
                      style: const TextStyle(color: Colors.white),
                      decoration: const InputDecoration(
                        labelText: "Address",
                        labelStyle: TextStyle(color: Colors.grey),
                      ),
                      validator: (val) => val!.isEmpty ? "Enter address" : null,
                    ),
                    const SizedBox(height: 10),
                    TextFormField(
                      controller: _descriptionController,
                      style: const TextStyle(color: Colors.white),
                      minLines: 3,
                      maxLines: 6,
                      decoration: const InputDecoration(
                        labelText: "Description",
                        labelStyle: TextStyle(color: Colors.grey),
                      ),
                    ),
                    const SizedBox(height: 10),
                    TextFormField(
                      controller: _starRatingController,
                      style: const TextStyle(color: Colors.white),
                      decoration: const InputDecoration(
                        labelText: "Star Rating (1–5)",
                        labelStyle: TextStyle(color: Colors.grey),
                      ),
                      keyboardType: TextInputType.number,
                      validator: (val) {
                        int? num = int.tryParse(val ?? "");
                        if (num == null || num < 1 || num > 5) {
                          return "Enter rating 1–5";
                        }
                        return null;
                      },
                    ),
                    const SizedBox(height: 20),

                    // Dropdown za gradove
                    DropdownButtonFormField<int>(
                      dropdownColor: const Color.fromRGBO(17, 45, 78, 1),
                      style: TextStyle(color: Colors.white),
                      value: _selectedCityId,
                      items: _cities
                          .map(
                            (city) => DropdownMenuItem<int>(
                              value: city.id,
                              child: Text(
                                city.name ?? "",
                                style: const TextStyle(color: Colors.white),
                              ),
                            ),
                          )
                          .toList(),
                      onChanged: (val) {
                        setState(() {
                          _selectedCityId = val;
                        });
                      },
                      decoration: const InputDecoration(
                        labelText: "Select City",
                        labelStyle: TextStyle(color: Colors.grey),
                      ),
                      validator: (val) =>
                          val == null ? "Please select a city" : null,
                    ),
                    const SizedBox(height: 30),

                    ElevatedButton(
                      onPressed: _saveHotel,
                      child: const Text("Save"),
                    ),
                  ],
                ),
              ),
            ),
    );
  }
}
