// Decompiled with JetBrains decompiler
// Type: -.dje_zDDJ3D37GQNGTHWK82PDGKZ3UWXHCH9YWML4RSUBC_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using StockSharp.Localization;
using StockSharp.Xaml.Charting;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal sealed class dje_zDDJ3D37GQNGTHWK82PDGKZ3UWXHCH9YWML4RSUBC_ejd : 
  \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D
{
  
  private ChartDrawData.\u0023\u003DzU3TaXFs\u003D \u0023\u003DzSgRFDbs\u003D;
  
  private readonly Lazy<dje_zDDJ3D37GQNGTHWK82PDGKZ3UWXHCH9YWML4RSUBC_ejd> \u0023\u003Dzu24j_FM\u003D;
  
  private string \u0023\u003DzJz0NEpw\u003D;
  
  private string \u0023\u003Dz7CXThCs\u003D;
  
  private string \u0023\u003DzHhOSPlQ\u003D;
  
  private string \u0023\u003Dzi\u0024Pgiec\u003D;

  public dje_zDDJ3D37GQNGTHWK82PDGKZ3UWXHCH9YWML4RSUBC_ejd(
    IRenderableSeries _param1,
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D _param2)
    : base(_param1, _param2)
  {
    this.\u0023\u003DzSgRFDbs\u003D = _param2.Transaction;
    this.\u0023\u003Dzu24j_FM\u003D = new Lazy<dje_zDDJ3D37GQNGTHWK82PDGKZ3UWXHCH9YWML4RSUBC_ejd>(new Func<dje_zDDJ3D37GQNGTHWK82PDGKZ3UWXHCH9YWML4RSUBC_ejd>(this.\u0023\u003DzRc0chjE\u003D));
  }

  public string Header => this.\u0023\u003Dzu24j_FM\u003D.Value.\u0023\u003DzJz0NEpw\u003D;

  public string Action => this.\u0023\u003Dzu24j_FM\u003D.Value.\u0023\u003Dz7CXThCs\u003D;

  public string Time => this.\u0023\u003Dzu24j_FM\u003D.Value.\u0023\u003DzHhOSPlQ\u003D;

  public string Error => this.\u0023\u003Dzu24j_FM\u003D.Value.\u0023\u003Dzi\u0024Pgiec\u003D;

  public ChartDrawData.\u0023\u003DzU3TaXFs\u003D Transaction
  {
    get => this.\u0023\u003DzSgRFDbs\u003D;
    set
    {
      this.\u0023\u003DzwGPLgl8\u003D<ChartDrawData.\u0023\u003DzU3TaXFs\u003D>(ref this.\u0023\u003DzSgRFDbs\u003D, value, XXX.SSS(-539329115));
    }
  }

  private dje_zDDJ3D37GQNGTHWK82PDGKZ3UWXHCH9YWML4RSUBC_ejd \u0023\u003DzRc0chjE\u003D()
  {
    this.\u0023\u003DzJz0NEpw\u003D = this.\u0023\u003DzSgRFDbs\u003D.\u0023\u003DzHE7yEzyy7k7_() ? LocalizedStrings.Order : LocalizedStrings.Trade;
    string str = this.\u0023\u003DzSgRFDbs\u003D.\u0023\u003DzUYTxG_Bgl8ih() == null ? LocalizedStrings.Buy2 : LocalizedStrings.Sell2;
    DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 3);
    interpolatedStringHandler.AppendFormatted(str);
    interpolatedStringHandler.AppendLiteral(XXX.SSS(-539432316));
    interpolatedStringHandler.AppendFormatted<long>(this.\u0023\u003DzSgRFDbs\u003D.Volume);
    interpolatedStringHandler.AppendLiteral(XXX.SSS(-539427391));
    interpolatedStringHandler.AppendFormatted<double>(this.\u0023\u003DzSgRFDbs\u003D.\u0023\u003DzbH5YDNBwpnry());
    this.\u0023\u003Dz7CXThCs\u003D = interpolatedStringHandler.ToStringAndClear();
    interpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 3);
    interpolatedStringHandler.AppendFormatted(LocalizedStrings.Time);
    interpolatedStringHandler.AppendLiteral(XXX.SSS(-539329097));
    interpolatedStringHandler.AppendFormatted<DateTime>(this.\u0023\u003DzSgRFDbs\u003D.\u0023\u003Dzg86amuQ\u003D(), XXX.SSS(-539329138));
    interpolatedStringHandler.AppendLiteral(XXX.SSS(-539427378));
    interpolatedStringHandler.AppendFormatted<DateTime>(this.\u0023\u003DzSgRFDbs\u003D.\u0023\u003Dzg86amuQ\u003D(), XXX.SSS(-539433036));
    this.\u0023\u003DzHhOSPlQ\u003D = interpolatedStringHandler.ToStringAndClear();
    if (this.\u0023\u003DzSgRFDbs\u003D.IsError)
      this.\u0023\u003Dzi\u0024Pgiec\u003D = LocalizedStrings.Error + XXX.SSS(-539329097) + this.\u0023\u003DzSgRFDbs\u003D.\u0023\u003Dzj4eGSep8GqT3();
    return this;
  }

  public override void \u0023\u003DzCadMMgc\u003D(
    \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param1)
  {
    base.\u0023\u003DzCadMMgc\u003D(_param1);
    this.Transaction = ((dje_zDDJ3D37GQNGTHWK82PDGKZ3UWXHCH9YWML4RSUBC_ejd) _param1).Transaction;
  }
}
