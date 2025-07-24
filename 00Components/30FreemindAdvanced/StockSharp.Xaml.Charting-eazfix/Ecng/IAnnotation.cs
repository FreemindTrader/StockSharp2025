//// Decompiled with JetBrains decompiler
//// Type: #=zV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k=
//// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
//// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

//using \u002D;
//using System;
//using System.Collections.Generic;
//using System.Collections.Specialized;
//using System.Windows;
//using System.Xml.Serialization;

//#nullable disable
//public interface IAnnotation : 
//  IXmlSerializable,
//  IPublishMouseEvents,
//  IHitTestable
//{
//  event EventHandler<EventArgs> DragStarted;

//  void add_DragStarted(
//    EventHandler<EventArgs> _param1);

//  void remove_DragStarted(
//    EventHandler<EventArgs> _param1);

//  void add_DragEnded(
//    EventHandler<EventArgs> _param1);

//  event EventHandler<EventArgs> DragEnded;

//  void remove_DragEnded(
//    EventHandler<EventArgs> _param1);

//  event EventHandler<AnnotationDragDeltaEventArgs> DragDelta;

//  void add_DragDelta(
//    EventHandler<AnnotationDragDeltaEventArgs> _param1);

//  void remove_DragDelta(
//    EventHandler<AnnotationDragDeltaEventArgs> _param1);

//  event EventHandler Selected;

//  void add_Selected(EventHandler _param1);

//  void remove_Selected(EventHandler _param1);

//  void add_Unselected(EventHandler _param1);

//  event EventHandler Unselected;

//  void remove_Unselected(EventHandler _param1);

//  string get_XAxisId();

//  string XAxisId { get; set; }

//  void set_XAxisId(string _param1);

//  string get_YAxisId();

//  string YAxisId { get; set; }

//  void set_YAxisId(string _param1);

//  bool IsAttached { get; set; }

//  bool get_IsSelected();

//  bool IsSelected { get; set; }

//  void set_IsSelected(bool _param1);

//  bool IsEditable { get; set; }

//  bool get_IsEditable();

//  void set_IsEditable(bool _param1);

//  bool IsHidden { get; set; }

//  bool get_IsHidden();

//  void set_IsHidden(bool _param1);

//  IAxis YAxis { get; }

//  IEnumerable<IAxis> YAxes { get; }

//  IAxis XAxis { get; }

//  IEnumerable<IAxis> XAxes { get; }

//  IServiceContainer Services { get; set; }

//  IComparable get_X1();

//  IComparable X1 { get; set; }

//  void set_X1(IComparable _param1);

//  IComparable Y1 { get; set; }

//  IComparable get_Y1();

//  void set_Y1(IComparable _param1);

//  IComparable get_X2();

//  IComparable X2 { get; set; }

//  void set_X2(IComparable _param1);

//  IComparable Y2 { get; set; }

//  IComparable get_Y2();

//  void set_Y2(IComparable _param1);

//  ISciChartSurface ParentSurface { get; set; }

//  XyDirection DragDirections { get; set; }

//  XyDirection get_DragDirections();

//  void set_DragDirections(
//    XyDirection _param1);

//  XyDirection get_ResizeDirections();

//  XyDirection ResizeDirections { get; set; }

//  void set_ResizeDirections(
//    XyDirection _param1);

//  bool get_IsResizable();

//  bool IsResizable { get; }

//  object \u0023\u003Dzo5cal7RkFjo\u0024();

//  void \u0023\u003Dzr3bTJ1tY5M3J(object _param1);

//  bool CaptureMouse();

//  void ReleaseMouseCapture();

//  void Update(
//    ICoordinateCalculator<<double> _param1,
//    ICoordinateCalculator<<double> _param2);

//  void OnDetached();

//  void OnAttached();

//  void Hide();

//  void Show();

//  void MoveAnnotation(double _param1, double _param2);

//  void SetBasePoint(Point _param1, int _param2);

//  Point[] GetBasePoints();

//  bool Refresh();

//  void StartDrag(bool _param1);

//  void EndDrag();

//  void Drag(double _param1, double _param2);

//  void OnXAxesCollectionChanged(object _param1, NotifyCollectionChangedEventArgs _param2);

//  void OnYAxesCollectionChanged(object _param1, NotifyCollectionChangedEventArgs _param2);

//  bool CanMultiSelect(
//    IAnnotation[] _param1);
//}
