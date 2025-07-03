// Decompiled with JetBrains decompiler
// Type: #=zeOTgfMmJN9ezcFvs39Ju8q$wkROgPo2o_c9nq8U=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System.Collections.ObjectModel;
using System.Windows;

#nullable disable
internal interface \u0023\u003DzeOTgfMmJN9ezcFvs39Ju8q\u0024wkROgPo2o_c9nq8U\u003D : 
  IHitTestable
{
  bool ClipToBounds { get; set; }

  bool get_ClipToBounds();

  void set_ClipToBounds(bool _param1);

  ObservableCollection<UIElement> \u0023\u003DzBDSV99pPo8hY();

  void \u0023\u003DzUf222sU\u003D();

  bool CaptureMouse();

  void ReleaseMouseCapture();
}
