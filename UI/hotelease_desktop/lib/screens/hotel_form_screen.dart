import 'package:flutter/material.dart';
import 'package:hotelease_mobile_new/models/hotel.dart';
import 'package:hotelease_mobile_new/providers/hotels_provider.dart';

class HotelFormScreen extends StatefulWidget {
  final Hotel? hotel; // ako je null => dodavanje, ako nije => edit

  const HotelFormScreen({super.key, this.hotel});

  @override
  State<HotelFormScreen> createState() => _HotelFormScreenState();
}

class _HotelFormScreenState extends State<HotelFormScreen> {
  final HotelsProvider _provider = HotelsProvider();
  final _formKey = GlobalKey<FormState>();

  late TextEditingController _nameController;
  late TextEditingController _addressController;
  late TextEditingController _descriptionController;
  late TextEditingController _starRatingController;

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
  }

  Future<void> saveHotel() async {
    if (!_formKey.currentState!.validate()) return;

    final payload = {
      "name": _nameController.text,
      "address": _addressController.text,
      "description": _descriptionController.text,
      "cityId": 1, // TODO: zamijeni sa pravim odabirom grada
      "countryId": 1,
      "managerId": 1,
      "starRating": int.tryParse(_starRatingController.text) ?? 3,
      "isActive": true,
    };

    if (widget.hotel == null) {
      await _provider.insert(payload);
    } else {
      await _provider.update(widget.hotel!.id!, payload);
    }

    Navigator.pop(
      context,
      true,
    ); // vraća signal parent ekranu da refresha listu
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(widget.hotel == null ? "Add Hotel" : "Edit Hotel"),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16),
        child: Form(
          key: _formKey,
          child: ListView(
            children: [
              TextFormField(
                controller: _nameController,
                decoration: const InputDecoration(labelText: "Hotel Name"),
                validator: (val) => val!.isEmpty ? "Enter name" : null,
              ),
              TextFormField(
                controller: _addressController,
                decoration: const InputDecoration(labelText: "Address"),
                validator: (val) => val!.isEmpty ? "Enter address" : null,
              ),
              TextFormField(
                controller: _descriptionController,
                decoration: const InputDecoration(labelText: "Description"),
              ),
              TextFormField(
                controller: _starRatingController,
                decoration: const InputDecoration(labelText: "Star Rating"),
                keyboardType: TextInputType.number,
              ),
              const SizedBox(height: 20),
              ElevatedButton(onPressed: saveHotel, child: const Text("Save")),
            ],
          ),
        ),
      ),
    );
  }
}
