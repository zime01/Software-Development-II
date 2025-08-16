class SearchResult<T> {
  int? count = 0;
  List<T> result = [];

  SearchResult({List<T>? result, this.count}) : result = result ?? [];

  factory SearchResult.fromJson(
    Map<String, dynamic> json,
    T Function(Map<String, dynamic>) fromJsonT,
  ) {
    return SearchResult<T>(
      result: (json['resultList'] as List)
          .map((item) => fromJsonT(item as Map<String, dynamic>))
          .toList(),
      count: json['count'] ?? 0,
    );
  }
}
