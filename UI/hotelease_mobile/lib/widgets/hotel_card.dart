import 'package:flutter/material.dart';
import 'package:hotelease_mobile/utils/utils.dart';

class HotelCard extends StatefulWidget {
  String? imageUrl;
  String? name;
  int? stars;
  double? price;

  HotelCard({this.imageUrl, super.key, this.name, this.stars, this.price});

  @override
  State<HotelCard> createState() => _HotelCardState();
}

class _HotelCardState extends State<HotelCard> {
  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: EdgeInsetsGeometry.all(8),
      child: Column(
        children: [
          Container(
            width: 100,
            height: 100,
            child: widget.imageUrl != null
                ? Image.network(widget.imageUrl!, fit: BoxFit.cover)
                : Image.asset("assets/no_image.png"), // default slika
          ),
          Text(widget.name ?? "", style: TextStyle(color: Colors.white)),
        ],
      ),
    );
  }
}
