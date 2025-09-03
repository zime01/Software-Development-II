import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:hotelease_mobile_new/models/service.dart';
import 'package:hotelease_mobile_new/providers/services_provider.dart';
import 'package:hotelease_mobile_new/screens/master_screen.dart';

class ServiceFormScreen extends StatefulWidget {
  final int hotelId;
  final Service? service;

  const ServiceFormScreen({super.key, required this.hotelId, this.service});

  @override
  State<ServiceFormScreen> createState() => _ServiceFormScreenState();
}

class _ServiceFormScreenState extends State<ServiceFormScreen> {
  final _formKey = GlobalKey<FormState>();
  late TextEditingController _nameController;
  late TextEditingController _descController;
  late TextEditingController _priceController;

  @override
  void initState() {
    super.initState();
    _nameController = TextEditingController(text: widget.service?.name ?? "");
    _descController = TextEditingController(
      text: widget.service?.description ?? "",
    );
    _priceController = TextEditingController(
      text: widget.service?.price.toString() ?? "",
    );
  }

  @override
  void dispose() {
    _nameController.dispose();
    _descController.dispose();
    _priceController.dispose();
    super.dispose();
  }

  Future<void> _save() async {
    if (!_formKey.currentState!.validate()) return;

    var provider = context.read<ServicesProvider>();
    var service = Service(
      id: widget.service?.id,
      name: _nameController.text,
      description: _descController.text,
      price: double.tryParse(_priceController.text) ?? 0,
      hotelId: widget.hotelId,
    );

    if (widget.service == null) {
      await provider.insert(service.toJson());
    } else {
      await provider.update(service.id!, service.toJson());
    }

    Navigator.pop(context);
  }

  @override
  Widget build(BuildContext context) {
    return MasterScreen(
      title: widget.service == null ? "Add Service" : "Edit Service",
      child: Padding(
        padding: const EdgeInsets.all(16),
        child: Form(
          key: _formKey,
          child: Column(
            children: [
              TextFormField(
                style: TextStyle(color: Colors.white),
                controller: _nameController,
                decoration: const InputDecoration(
                  labelText: "Name",
                  labelStyle: TextStyle(color: Colors.grey),
                ),
                validator: (v) => v == null || v.isEmpty ? "Enter name" : null,
              ),
              TextFormField(
                style: TextStyle(color: Colors.white),
                controller: _descController,
                decoration: const InputDecoration(
                  labelText: "Description",
                  labelStyle: TextStyle(color: Colors.grey),
                ),
              ),
              TextFormField(
                style: TextStyle(color: Colors.white),
                controller: _priceController,
                decoration: const InputDecoration(
                  labelText: "Price \$",
                  labelStyle: TextStyle(color: Colors.grey),
                ),
                keyboardType: TextInputType.number,
                validator: (v) => v == null || v.isEmpty ? "Enter price" : null,
              ),
              const SizedBox(height: 20),
              ElevatedButton(onPressed: _save, child: const Text("Spremi")),
            ],
          ),
        ),
      ),
    );
  }
}
