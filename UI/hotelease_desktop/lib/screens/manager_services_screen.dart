import 'package:flutter/material.dart';
import 'package:hotelease_mobile_new/screens/service_form_screen.dart';
import 'package:provider/provider.dart';
import 'package:hotelease_mobile_new/models/service.dart';
import 'package:hotelease_mobile_new/providers/services_provider.dart';

class ManagerServicesScreen extends StatefulWidget {
  final int hotelId;

  const ManagerServicesScreen({super.key, required this.hotelId});

  @override
  State<ManagerServicesScreen> createState() => _ManagerServicesScreenState();
}

class _ManagerServicesScreenState extends State<ManagerServicesScreen> {
  List<Service> _services = [];
  bool _loading = true;

  @override
  void initState() {
    super.initState();
    _loadServices();
  }

  Future<void> _loadServices() async {
    try {
      var provider = context.read<ServicesProvider>();
      var data = await provider.getServicesByHotel(widget.hotelId);
      setState(() {
        _services = data;
        _loading = false;
      });
    } catch (e) {
      setState(() => _loading = false);
      ScaffoldMessenger.of(
        context,
      ).showSnackBar(SnackBar(content: Text("Greška pri učitavanju: $e")));
    }
  }

  void _openForm([Service? service]) {
    Navigator.push(
      context,
      MaterialPageRoute(
        builder: (_) =>
            ServiceFormScreen(hotelId: widget.hotelId, service: service),
      ),
    ).then((_) => _loadServices());
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: const Color.fromRGBO(17, 45, 78, 1),
      appBar: AppBar(
        title: const Text("Additional Services"),
        backgroundColor: const Color.fromRGBO(17, 45, 78, 1),
        foregroundColor: Colors.white,
      ),
      body: _loading
          ? const Center(child: CircularProgressIndicator())
          : _services.isEmpty
          ? const Center(child: Text("No services available"))
          : ListView.builder(
              itemCount: _services.length,
              itemBuilder: (context, index) {
                final s = _services[index];
                return Card(
                  color: const Color.fromRGBO(15, 41, 70, 1),
                  child: ListTile(
                    title: Text(s.name, style: TextStyle(color: Colors.white)),
                    subtitle: Text(
                      "${s.description ?? ''}\n\$ ${s.price.toStringAsFixed(2)}",
                      style: TextStyle(color: Colors.grey),
                    ),
                    isThreeLine: true,
                    trailing: Row(
                      mainAxisSize: MainAxisSize.min,
                      children: [
                        IconButton(
                          icon: const Icon(Icons.edit, color: Colors.white),
                          onPressed: () => _openForm(s),
                        ),
                        IconButton(
                          icon: const Icon(Icons.delete, color: Colors.red),
                          onPressed: () async {
                            try {
                              var provider = context.read<ServicesProvider>();
                              await provider.delete(s.id!);
                              _loadServices();
                            } catch (e) {
                              ScaffoldMessenger.of(context).showSnackBar(
                                SnackBar(
                                  content: Text("Greška pri brisanju: $e"),
                                ),
                              );
                            }
                          },
                        ),
                      ],
                    ),
                  ),
                );
              },
            ),
      floatingActionButton: FloatingActionButton(
        onPressed: () => _openForm(),
        child: const Icon(Icons.add),
      ),
    );
  }
}
