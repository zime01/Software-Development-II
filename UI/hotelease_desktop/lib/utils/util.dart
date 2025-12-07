class Authorization {
  static String? username;
  static String? password;
  static int? userId;

  static void logout() {
    username = null;
    password = null;
    userId = null;
  }
}
