// Decompiled with JetBrains decompiler
// Type: #=zBQI7r_wiRKcCdd7zOgb7xGkBRjz2cANt6prK2mzvoXV0
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System;
using System.Xml;

#nullable disable
public sealed class \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xGkBRjz2cANt6prK2mzvoXV0 : 
  \u0023\u003Dz6RuI7w1XIc38iQTeDB5TvWfY_h5S2Y_Vli8ECWL1cMw8VzeOSw\u003D\u003D<dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd>
{
  private static \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xGkBRjz2cANt6prK2mzvoXV0 \u0023\u003Dzj9RABVg\u003D;

  private \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xGkBRjz2cANt6prK2mzvoXV0()
  {
    this.\u0023\u003Dz6DunSwc\u003D = new string[16 /*0x10*/]
    {
      "AutoRange",
      "AutoTicks",
      "AxisAlignment",
      "DrawMajorBands",
      "DrawMajorTicks",
      "DrawLabels",
      "DrawMinorTicks",
      "DrawMajorGridLines",
      "DrawMinorGridLines",
      "AxisTitle",
      "VisibleRange",
      "Id",
      "GrowBy",
      "MinorsPerMajor",
      "MaxAutoTicks",
      "FlipCoordinates"
    };
  }

  public static \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xGkBRjz2cANt6prK2mzvoXV0 \u0023\u003DzFvAsfEI\u003D()
  {
    return \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xGkBRjz2cANt6prK2mzvoXV0.\u0023\u003Dzj9RABVg\u003D ?? (\u0023\u003DzBQI7r_wiRKcCdd7zOgb7xGkBRjz2cANt6prK2mzvoXV0.\u0023\u003Dzj9RABVg\u003D = new \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xGkBRjz2cANt6prK2mzvoXV0());
  }

  public override void \u0023\u003Dz4EJs3pc\u003D(
    dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd _param1,
    XmlReader _param2)
  {
    base.\u0023\u003Dz4EJs3pc\u003D(_param1, _param2);
    string typeName = _param2["DeltaType"];
    if (typeName == null)
      return;
    Type type = Type.GetType(typeName);
    _param1.MajorDelta = (IComparable) _param2.\u0023\u003Dzm2nn9hA\u003D("MajorDelta", type);
    _param1.MinorDelta = (IComparable) _param2.\u0023\u003Dzm2nn9hA\u003D("MinorDelta", type);
  }

  public override void \u0023\u003Dz7SZ\u0024Lrw\u003D(
    dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd _param1,
    XmlWriter _param2)
  {
    base.\u0023\u003Dz7SZ\u0024Lrw\u003D(_param1, _param2);
    dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd axF9ZgQ7NbH9KsEjd = _param1;
    if (axF9ZgQ7NbH9KsEjd == null || axF9ZgQ7NbH9KsEjd.AutoTicks || _param1.MinorDelta == null || _param1.MajorDelta == null)
      return;
    Type type = _param1.MajorDelta.GetType();
    _param2.WriteAttributeString("DeltaType", type.\u0023\u003Dzb_Ih6a0\u003D());
    _param2.\u0023\u003DzVjDFK7Q\u003D((object) _param1, "MinorDelta");
    _param2.\u0023\u003DzVjDFK7Q\u003D((object) _param1, "MajorDelta");
  }
}
