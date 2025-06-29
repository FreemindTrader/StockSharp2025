// Decompiled with JetBrains decompiler
// Type: #=zbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Collections;

#nullable disable
internal interface \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D : 
  \u0023\u003DzExPUKZPbT0fb9dlf_qOoa7Fo_o9lZIelo\u0024_m4wTHwP6Ifze3\u0024A\u003D\u003D
{
  event EventHandler<\u0023\u003Dz5hVyTN88kBn45NAfOxK7MD4fbuZgSG4uWo9Ll25GzP7X> DataSeriesChanged;

  void add_DataSeriesChanged(
    EventHandler<\u0023\u003Dz5hVyTN88kBn45NAfOxK7MD4fbuZgSG4uWo9Ll25GzP7X> _param1);

  void remove_DataSeriesChanged(
    EventHandler<\u0023\u003Dz5hVyTN88kBn45NAfOxK7MD4fbuZgSG4uWo9Ll25GzP7X> _param1);

  Type get_XType();

  Type XType { get; }

  Type get_YType();

  Type YType { get; }

  \u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D get_ParentSurface();

  \u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D ParentSurface { get; set; }

  void set_ParentSurface(
    \u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D _param1);

  [Obsolete("IsAttached is obsolete because there is no DataSeriesSet now")]
  bool IsAttached { get; }

  bool get_IsAttached();

  \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D get_XRange();

  \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D XRange { get; }

  \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D YRange { get; }

  \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D get_YRange();

  \u0023\u003DzzKBN5TXUMNIGpWrDrUMXSW1J0DiEBQ7p1fR0bYE\u003D DataSeriesType { get; }

  \u0023\u003DzzKBN5TXUMNIGpWrDrUMXSW1J0DiEBQ7p1fR0bYE\u003D get_DataSeriesType();

  IList \u0023\u003DzwQnyySN6xaVC();

  IList \u0023\u003DzPqsSI6C5MOOb();

  IComparable get_LatestYValue();

  IComparable LatestYValue { get; }

  string SeriesName { get; set; }

  string get_SeriesName();

  void set_SeriesName(string _param1);

  IComparable get_YMin();

  IComparable YMin { get; }

  IComparable get_YMax();

  IComparable YMax { get; }

  IComparable get_XMin();

  IComparable XMin { get; }

  IComparable XMax { get; }

  IComparable get_XMax();

  bool get_IsFifo();

  bool IsFifo { get; }

  int? FifoCapacity { get; set; }

  int? get_FifoCapacity();

  void set_FifoCapacity(int? _param1);

  bool HasValues { get; }

  bool get_HasValues();

  bool IsSecondary { get; }

  bool get_IsSecondary();

  int get_Count();

  int Count { get; }

  bool IsSorted { get; }

  bool get_IsSorted();

  object get_SyncRoot();

  object SyncRoot { get; }

  bool AcceptsUnsortedData { get; set; }

  bool get_AcceptsUnsortedData();

  void set_AcceptsUnsortedData(bool _param1);

  void Clear();

  \u0023\u003DzR2x48Sho4AxfV9DSAxG8OQ2rGx6KyfAREQ\u003D\u003D GetIndicesRange(
    \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D _param1);

  \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ ToPointSeries(
    \u0023\u003Dzr3AyUEt11qAsNGjKm7GKWxmriZN_\u0024I_fB5TLZqozNbfOHxiykg\u003D\u003D _param1,
    \u0023\u003DzR2x48Sho4AxfV9DSAxG8OQ2rGx6KyfAREQ\u003D\u003D _param2,
    int _param3,
    bool _param4,
    bool? _param5,
    \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D _param6,
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6S86EZeND1KSf7Q5ckAbN6LxyEWNToOUjo1\u00243K\u00241Ho2jpA\u003D\u003D _param7,
    object _param8);

  \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ \u0023\u003DzqBr63KVJqTvw(
    IList _param1,
    \u0023\u003Dzr3AyUEt11qAsNGjKm7GKWxmriZN_\u0024I_fB5TLZqozNbfOHxiykg\u003D\u003D _param2,
    \u0023\u003DzR2x48Sho4AxfV9DSAxG8OQ2rGx6KyfAREQ\u003D\u003D _param3,
    int _param4,
    bool _param5);

  \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D \u0023\u003DzIvBsiY\u0024C5tRlcFKGo7\u002430Ac\u003D(
    \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D _param1);

  \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D GetWindowedYRange(
    \u0023\u003DzR2x48Sho4AxfV9DSAxG8OQ2rGx6KyfAREQ\u003D\u003D _param1);

  \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D \u0023\u003DzIvBsiY\u0024C5tRlcFKGo7\u002430Ac\u003D(
    \u0023\u003DzR2x48Sho4AxfV9DSAxG8OQ2rGx6KyfAREQ\u003D\u003D _param1,
    bool _param2);

  \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D \u0023\u003DzIvBsiY\u0024C5tRlcFKGo7\u002430Ac\u003D(
    \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D _param1,
    bool _param2);

  int \u0023\u003DzFH1yjjY\u003D(
    IComparable _param1,
    \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D _param2);

  \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D \u0023\u003DzDKPxuEruV71w(
    int _param1);

  void InvalidateParentSurface(
    \u0023\u003DzkAKUJrbqM7JEiA1NxV8i_f7seBjAlONQGY47Zh8\u003D _param1);

  int FindClosestPoint(IComparable _param1, IComparable _param2, double _param3, double _param4);

  int FindClosestLine(
    IComparable _param1,
    IComparable _param2,
    double _param3,
    double _param4,
    \u0023\u003DzV9O5tWduWosGLvu_87Zf5KIXjvA0HjqD6negDKigZjec_mB\u0024hq2WcZE\u003D _param5);

  void OnBeginRenderPass();
}
