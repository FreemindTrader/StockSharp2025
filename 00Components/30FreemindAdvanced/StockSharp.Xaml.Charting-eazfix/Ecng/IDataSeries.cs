// Decompiled with JetBrains decompiler
// Type: #=zbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections;

#nullable disable
public interface IDataSeries : 
  ISuspendable
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

  ISciChartSurface get_ParentSurface();

  ISciChartSurface ParentSurface { get; set; }

  void set_ParentSurface(
    ISciChartSurface _param1);

  [Obsolete("IsAttached is obsolete because there is no DataSeriesSet now")]
  bool IsAttached { get; }

  bool get_IsAttached();

  IRange get_XRange();

  IRange XRange { get; }

  IRange YRange { get; }

  IRange get_YRange();

  DataSeriesType DataSeriesType { get; }

  DataSeriesType get_DataSeriesType();

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

  IndexRange  GetIndicesRange(
    IRange _param1);

  IPointSeries ToPointSeries(
    ResamplingMode _param1,
    IndexRange  _param2,
    int _param3,
    bool _param4,
    bool? _param5,
    IRange _param6,
    IPointResamplerFactory _param7,
    object _param8);

  IPointSeries \u0023\u003DzqBr63KVJqTvw(
    IList _param1,
    ResamplingMode _param2,
    IndexRange  _param3,
    int _param4,
    bool _param5);

  IRange \u0023\u003DzIvBsiY\u0024C5tRlcFKGo7\u002430Ac\u003D(
    IRange _param1);

  IRange GetWindowedYRange(
    IndexRange  _param1);

  IRange \u0023\u003DzIvBsiY\u0024C5tRlcFKGo7\u002430Ac\u003D(
    IndexRange  _param1,
    bool _param2);

  IRange \u0023\u003DzIvBsiY\u0024C5tRlcFKGo7\u002430Ac\u003D(
    IRange _param1,
    bool _param2);

  int \u0023\u003DzFH1yjjY\u003D(
    IComparable _param1,
    \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D _param2);

  HitTestInfo \u0023\u003DzDKPxuEruV71w(
    int _param1);

  void InvalidateParentSurface(
    RangeMode _param1);

  int FindClosestPoint(IComparable _param1, IComparable _param2, double _param3, double _param4);

  int FindClosestLine(
    IComparable _param1,
    IComparable _param2,
    double _param3,
    double _param4,
    \u0023\u003DzV9O5tWduWosGLvu_87Zf5KIXjvA0HjqD6negDKigZjec_mB\u0024hq2WcZE\u003D _param5);

  void OnBeginRenderPass();
}
