// Decompiled with JetBrains decompiler
// Type: -.dje_zRKH78WALVVKAZZ9387XQ6M2LLV693MRMFGTBVYBK6FFUKL3LRZ78R_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal sealed class dje_zRKH78WALVVKAZZ9387XQ6M2LLV693MRMFGTBVYBK6FFUKL3LRZ78R_ejd : 
  IValueConverter
{
  public object Convert(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    Orientation orientation = (Orientation) _param1;
    return (object) new RotateTransform()
    {
      Angle = (orientation == Orientation.Horizontal ? 0.0 : -90.0)
    };
  }

  public object ConvertBack(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    throw new NotImplementedException();
  }
}
