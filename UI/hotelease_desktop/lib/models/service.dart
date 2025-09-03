class Service {
  int? id;
  String name;
  String? description;
  double price;
  int hotelId;

  Service({
    this.id,
    required this.name,
    this.description,
    required this.price,
    required this.hotelId,
  });

  factory Service.fromJson(Map<String, dynamic> json) => Service(
    id: json['id'],
    name: json['name'],
    description: json['description'],
    price: (json['price'] as num).toDouble(),
    hotelId: json['hotelId'],
  );

  Map<String, dynamic> toJson() => {
    "id": id,
    "name": name,
    "description": description,
    "price": price,
    "hotelId": hotelId,
  };
}
