// Decompiled with JetBrains decompiler
// Type: #=zpWMIzYBzoypE5Wwh$gRH6ek_dynWMOFzgH4RlW$$B0lB
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

#nullable disable
internal interface IAxis : 
  IDrawable,
  IAxisParams,
  ISuspendable,
  IInvalidatableElement,
  IHitTestable
{
  void \u0023\u003Dzf1TnIHLmqeNf(
    EventHandler<\u0023\u003Dz9Cv\u0024UX3L5m_6hX1ogAvN6swsMiTQ4vauzZKCwXA\u003D> _param1);

  void \u0023\u003DzO6DAydbqJOaS(
    EventHandler<\u0023\u003Dz9Cv\u0024UX3L5m_6hX1ogAvN6swsMiTQ4vauzZKCwXA\u003D> _param1);

  void \u0023\u003DzF_\u0024wky5\u0024qiYa(EventHandler<EventArgs> _param1);

  void \u0023\u003DzwG_uRQ_EmTwc(EventHandler<EventArgs> _param1);

  string Id { get; set; }

  string Id;

  void set_Id(string _param1);

  bool get_AutoTicks();

  bool AutoTicks { get; set; }

  void set_AutoTicks(bool _param1);

  ITickProvider TickProvider { get; set; }

  ITickProvider TickProvider;

  void set_TickProvider(
    ITickProvider _param1);

  [TypeConverter(typeof (StringToDoubleRangeTypeConverter))]
  IRange VisibleRangeLimit { get; set; }

  IRange get_AnimatedVisibleRange();

  void set_AnimatedVisibleRange(
    IRange _param1);

  IRange get_DataRange();

  IRange DataRange { get; }

  IServiceContainer get_Services();

  IServiceContainer Services { get; set; }

  void set_Services(
    IServiceContainer _param1);

  ISciChartSurface ParentSurface { get; set; }

  ISciChartSurface get_ParentSurface();

  void set_ParentSurface(
    ISciChartSurface _param1);

  Orientation Orientation { get; set; }

  Orientation get_Orientation();

  void set_Orientation(Orientation _param1);

  Brush get_MajorLineStroke();

  [Obsolete("MajorLineStroke is obsolete, please use MajorTickLineStyle instead", true)]
  Brush MajorLineStroke { get; set; }

  void set_MajorLineStroke(Brush _param1);

  [Obsolete("MinorLineStroke is obsolete, please use MajorTickLineStyle instead", true)]
  Brush MinorLineStroke { get; set; }

  Brush get_MinorLineStroke();

  void set_MinorLineStroke(Brush _param1);

  Style get_MajorTickLineStyle();

  Style MajorTickLineStyle { get; set; }

  void set_MajorTickLineStyle(Style _param1);

  Style get_MinorTickLineStyle();

  Style MinorTickLineStyle { get; set; }

  void set_MinorTickLineStyle(Style _param1);

  Style MajorGridLineStyle { get; set; }

  Style get_MajorGridLineStyle();

  void set_MajorGridLineStyle(Style _param1);

  Style MinorGridLineStyle { get; set; }

  Style get_MinorGridLineStyle();

  void set_MinorGridLineStyle(Style _param1);

  AutoRange get_AutoRange();

  AutoRange AutoRange { get; set; }

  void set_AutoRange(
    AutoRange _param1);

  string get_TextFormatting();

  string TextFormatting { get; set; }

  void set_TextFormatting(string _param1);

  string CursorTextFormatting { get; set; }

  string get_CursorTextFormatting();

  void set_CursorTextFormatting(string _param1);

  \u0023\u003DzkAKUJrbqM7JEiA1NxV8i_U1qeTmG05tjnxhrXf80OTVH get_LabelProvider();

  \u0023\u003DzkAKUJrbqM7JEiA1NxV8i_U1qeTmG05tjnxhrXf80OTVH LabelProvider { get; set; }

  void set_LabelProvider(
    \u0023\u003DzkAKUJrbqM7JEiA1NxV8i_U1qeTmG05tjnxhrXf80OTVH _param1);

  bool \u0023\u003DzFrVmckt\u0024NpG6();

  void \u0023\u003Dz\u0024mpHDOeBCMkH(bool _param1);

  bool IsHorizontalAxis { get; }

  bool get_IsStaticAxis();

  bool IsStaticAxis { get; set; }

  void set_IsStaticAxis(bool _param1);

  bool get_FlipCoordinates();

  bool FlipCoordinates { get; set; }

  void set_FlipCoordinates(bool _param1);

  bool get_HasValidVisibleRange();

  bool HasValidVisibleRange { get; }

  bool HasDefaultVisibleRange { get; }

  bool get_HasDefaultVisibleRange();

  string get_AxisTitle();

  string AxisTitle { get; set; }

  void set_AxisTitle(string _param1);

  Brush TickTextBrush { get; set; }

  Brush get_TickTextBrush();

  void set_TickTextBrush(Brush _param1);

  bool AutoAlignVisibleRange { get; set; }

  bool get_AutoAlignVisibleRange();

  void set_AutoAlignVisibleRange(bool _param1);

  bool DrawMinorTicks { get; set; }

  bool get_DrawMinorTicks();

  void set_DrawMinorTicks(bool _param1);

  bool get_DrawMajorTicks();

  bool DrawMajorTicks { get; set; }

  void set_DrawMajorTicks(bool _param1);

  bool get_DrawMajorGridLines();

  bool DrawMajorGridLines { get; set; }

  void set_DrawMajorGridLines(bool _param1);

  bool DrawMinorGridLines { get; set; }

  bool get_DrawMinorGridLines();

  void set_DrawMinorGridLines(bool _param1);

  HorizontalAlignment HorizontalAlignment { get; set; }

  HorizontalAlignment get_HorizontalAlignment();

  void set_HorizontalAlignment(HorizontalAlignment _param1);

  VerticalAlignment get_VerticalAlignment();

  VerticalAlignment VerticalAlignment { get; set; }

  void set_VerticalAlignment(VerticalAlignment _param1);

  \u0023\u003DzzKBN5TXUMNIGpWrDrUMXSdQaIc96_52nMJQUgeTEfBP3 get_AxisMode();

  [Obsolete("IAxis.AxisMode is obsolete, please use NumericAxis or LogarithmicNumericAxis instead")]
  \u0023\u003DzzKBN5TXUMNIGpWrDrUMXSdQaIc96_52nMJQUgeTEfBP3 AxisMode { get; set; }

  void set_AxisMode(
    \u0023\u003DzzKBN5TXUMNIGpWrDrUMXSdQaIc96_52nMJQUgeTEfBP3 _param1);

  AxisAlignment get_AxisAlignment();

  AxisAlignment AxisAlignment { get; set; }

  void set_AxisAlignment(
    AxisAlignment _param1);

  bool IsCategoryAxis { get; }

  bool get_IsCategoryAxis();

  bool IsLogarithmicAxis { get; }

  bool get_IsLogarithmicAxis();

  bool IsPolarAxis { get; }

  bool get_IsPolarAxis();

  bool IsCenterAxis { get; set; }

  bool get_IsCenterAxis();

  void set_IsCenterAxis(bool _param1);

  bool get_IsPrimaryAxis();

  bool IsPrimaryAxis { get; set; }

  void set_IsPrimaryAxis(bool _param1);

  \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk get_ModifierAxisCanvas();

  \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk ModifierAxisCanvas { get; }

  Visibility get_Visibility();

  Visibility Visibility { get; set; }

  void set_Visibility(Visibility _param1);

  bool IsAxisFlipped { get; }

  bool get_IsAxisFlipped();

  IRange get_VisibleRangeLimit();

  [TypeConverter(typeof (StringToDoubleRangeTypeConverter))]
  IRange \u0023\u003Dz\u00241ESPLeztLMW { get; set; }

  void set_VisibleRangeLimit(
    IRange _param1);

  \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQBfb77y0xl0\u00246w\u003D\u003D get_VisibleRangeLimitMode();

  \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQBfb77y0xl0\u00246w\u003D\u003D VisibleRangeLimitMode { get; set; }

  void set_VisibleRangeLimitMode(
    \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQBfb77y0xl0\u00246w\u003D\u003D _param1);

  IComparable get_MinimalZoomConstrain();

  IComparable MinimalZoomConstrain { get; set; }

  void set_MinimalZoomConstrain(IComparable _param1);

  bool get_IsLabelCullingEnabled();

  bool IsLabelCullingEnabled { get; set; }

  void set_IsLabelCullingEnabled(bool _param1);

  \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> \u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D();

  \u0023\u003DzFDK4fEILkMRswIjIg1\u0024y3MZjK6kEswW_XYNXMkMl\u0024H7TxZaHyXLiZ9wXJZ_c \u0023\u003DzLwX32hiDT0l2MBUaLIQGQLie6ie0();

  bool CaptureMouse();

  void ReleaseMouseCapture();

  void \u0023\u003DzqFIyyIbnwGLq(Cursor _param1);

  \u0023\u003DzT6V9cIzTPzymiPsaXC1JFEAP9ly0DLdsgjrQCUaaCm\u0024XPj7JdmPvp0w\u003D \u0023\u003DzjuB\u0024Pa8\u003D(
    Point _param1);

  \u0023\u003Dz59_koqr2EQdapDcFKycZuFSiStoJv1Yg1g\u003D\u003D \u0023\u003Dz53g0bO2haOY4();

  IRange \u0023\u003DzzMId\u0024f67Wftb(
    \u0023\u003DzdDznHH56iLab0VjufJI3RvrDHJH0\u0024iDtfw\u003D\u003D _param1);

  IRange \u0023\u003DzIvBsiY\u0024C5tRlcFKGo7\u002430Ac\u003D(
    IDictionary<string, IRange> _param1);

  double \u0023\u003DzhL6gsJw\u003D(IComparable _param1);

  IComparable GetDataValue(double _param1);

  double \u0023\u003Dz4wEfDhMr\u0024V6c();

  void \u0023\u003Dzs15X3Ar32F1\u0024(
    \u0023\u003DzdDznHH56iLab0VjufJI3RvrDHJH0\u0024iDtfw\u003D\u003D _param1,
    \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ _param2);

  void \u0023\u003DzquLnA5Y\u003D(
    double _param1,
    ClipMode _param2);

  void \u0023\u003DzquLnA5Y\u003D(
    double _param1,
    ClipMode _param2,
    TimeSpan _param3);

  void \u0023\u003Dz\u00248pSPh2nSp0Q(int _param1);

  void \u0023\u003Dz\u00248pSPh2nSp0Q(int _param1, TimeSpan _param2);

  void \u0023\u003DzQdR08KQ\u003D(double _param1, double _param2);

  void \u0023\u003DzQdR08KQ\u003D(double _param1, double _param2, TimeSpan _param3);

  void \u0023\u003Dz40HnRQM\u003D(double _param1, double _param2);

  void \u0023\u003Dz40HnRQM\u003D(double _param1, double _param2, TimeSpan _param3);

  void \u0023\u003DzSYETXFE\u003D(
    IRange _param1,
    double _param2);

  void \u0023\u003DzCIPDlIJQeLiZ(
    IRange _param1,
    double _param2,
    IRange _param3);

  void \u0023\u003DzQ4klw1orSVl\u0024(Type _param1);

  string \u0023\u003DzRDs3D1Q\u003D(IComparable _param1, string _param2);

  string \u0023\u003DzRDs3D1Q\u003D(IComparable _param1);

  string \u0023\u003DzRQVMnjXxoCTF(IComparable _param1, bool _param2);

  bool \u0023\u003Dz2OKbyRBzRCBL(
    IRange _param1);

  IAxis \u0023\u003DzQ8SgRgQ\u003D();

  void \u0023\u003DzwrnVUenT8f7v7FlPviBwd40\u003D(
    IRange _param1,
    TimeSpan _param2);

  void \u0023\u003DzpTR8\u0024ECbZOHX();

  void Clear();

  IRange \u0023\u003DzspbjXJnVtbB\u0024();

  IRange \u0023\u003Dz8dMR0vhnuqhVVjJNjQ\u003D\u003D();

  double CurrentDatapointPixelSize { get; }

  double get_CurrentDatapointPixelSize();
}
