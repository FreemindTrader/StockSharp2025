// Decompiled with JetBrains decompiler
// Type: -.AxisArea
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Windows.Controls;

#nullable disable
namespace StockSharp.Charting;

public sealed class AxisArea : ItemsControl
{
  public void \u0023\u003DzXp3QEOzPyMYR(object _param1)
  {
    try
    {
      if (_param1 == null || this.Items == null || !this.Items.Contains(_param1))
        return;
      this.Items.Remove(_param1);
    }
    catch
    {
    }
  }
}
