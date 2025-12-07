import 'package:intl/intl.dart';

final usdFormatter = NumberFormat.currency(
  locale: 'en_US',
  symbol: "\$",
  decimalDigits: 2,
);

String formatPrice(int? value) {
  if (value == null) return "0.00";
  return usdFormatter.format(value.toDouble());
}
