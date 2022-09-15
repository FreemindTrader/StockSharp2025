// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.TimeZoneEditor
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using DevExpress.Xpf.Editors;
using System;

namespace Ecng.Xaml
{
  /// <summary>
  /// </summary>
  public class TimeZoneEditor : ComboBoxEdit
  {
    /// <summary>
    /// </summary>
    public TimeZoneEditor()
    {
      base.\u002Ector();
      ((LookUpEditBase) this).set_ItemsSource((object) TimeZoneInfo.GetSystemTimeZones());
      ((LookUpEditBase) this).set_DisplayMember(nameof(2127278644));
      ((ButtonEdit) this).set_IsTextEditable(new bool?(false));
      ((ButtonEdit) this).AddClearButton((object) null);
    }

    /// <summary>
    /// </summary>
    public TimeZoneInfo TimeZone
    {
      get
      {
        return (TimeZoneInfo) ((BaseEdit) this).get_EditValue();
      }
      set
      {
        ((BaseEdit) this).set_EditValue((object) value);
      }
    }
  }
}
