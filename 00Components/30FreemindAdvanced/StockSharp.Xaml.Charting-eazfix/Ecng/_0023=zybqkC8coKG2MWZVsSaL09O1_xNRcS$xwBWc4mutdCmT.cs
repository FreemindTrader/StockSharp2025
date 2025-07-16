// Decompiled with JetBrains decompiler
// Type: #=zybqkC8coKG2MWZVsSaL09O1_xNRcS$xwBWc4mutdCmTdKj2X3QJ3_KQ=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;

#nullable disable
public sealed class \u0023\u003DzybqkC8coKG2MWZVsSaL09O1_xNRcS\u0024xwBWc4mutdCmTdKj2X3QJ3_KQ\u003D : 
  \u0023\u003Dz6RuI7w1XIc38iQTeDB5TvWfY_h5S2Y_Vli8ECWL1cMw8VzeOSw\u003D\u003D<SciChartSurface>
{
  private static \u0023\u003DzybqkC8coKG2MWZVsSaL09O1_xNRcS\u0024xwBWc4mutdCmTdKj2X3QJ3_KQ\u003D \u0023\u003Dzj9RABVg\u003D;

  private \u0023\u003DzybqkC8coKG2MWZVsSaL09O1_xNRcS\u0024xwBWc4mutdCmTdKj2X3QJ3_KQ\u003D()
  {
    this.\u0023\u003Dz6DunSwc\u003D = new string[5]
    {
      "MaxFrameRate",
      "ClipOverlayAnnotations",
      "ClipUnderlayAnnotations",
      "ClipModifierSurface",
      "ChartTitle"
    };
  }

  public static \u0023\u003DzybqkC8coKG2MWZVsSaL09O1_xNRcS\u0024xwBWc4mutdCmTdKj2X3QJ3_KQ\u003D \u0023\u003DzFvAsfEI\u003D()
  {
    return \u0023\u003DzybqkC8coKG2MWZVsSaL09O1_xNRcS\u0024xwBWc4mutdCmTdKj2X3QJ3_KQ\u003D.\u0023\u003Dzj9RABVg\u003D ?? (\u0023\u003DzybqkC8coKG2MWZVsSaL09O1_xNRcS\u0024xwBWc4mutdCmTdKj2X3QJ3_KQ\u003D.\u0023\u003Dzj9RABVg\u003D = new \u0023\u003DzybqkC8coKG2MWZVsSaL09O1_xNRcS\u0024xwBWc4mutdCmTdKj2X3QJ3_KQ\u003D());
  }

  public override void \u0023\u003Dz7SZ\u0024Lrw\u003D(
    SciChartSurface _param1,
    XmlWriter _param2)
  {
    base.\u0023\u003Dz7SZ\u0024Lrw\u003D(_param1, _param2);
    string theme = dje_zE2RKFGSKSSRHLKHMSTKDLZ3G36L4UQCSJVCT8AU3_ejd.GetTheme((DependencyObject) _param1);
    if (theme != null)
      _param2.WriteAttributeString("Theme", theme);
    string renderSurfaceType = RenderSurfaceBase.GetRenderSurfaceType((UIElement) _param1);
    if (renderSurfaceType != null)
      _param2.WriteAttributeString("RenderSurfaceType", renderSurfaceType);
    \u0023\u003DzybqkC8coKG2MWZVsSaL09O1_xNRcS\u0024xwBWc4mutdCmTdKj2X3QJ3_KQ\u003D.\u0023\u003DzLq2ODtDmwh8XhlieTRcobzhsPRrHR7GqyQ\u003D\u003D((IEnumerable<IRenderableSeries>) _param1.RenderableSeries, _param2);
    \u0023\u003DzybqkC8coKG2MWZVsSaL09O1_xNRcS\u0024xwBWc4mutdCmTdKj2X3QJ3_KQ\u003D.\u0023\u003Dzysp\u0024M4k\u003D<AxisCollection>(_param1.XAxes, "XAxes", _param2);
    \u0023\u003DzybqkC8coKG2MWZVsSaL09O1_xNRcS\u0024xwBWc4mutdCmTdKj2X3QJ3_KQ\u003D.\u0023\u003Dzysp\u0024M4k\u003D<AxisCollection>(_param1.YAxes, "YAxes", _param2);
    \u0023\u003DzybqkC8coKG2MWZVsSaL09O1_xNRcS\u0024xwBWc4mutdCmTdKj2X3QJ3_KQ\u003D.\u0023\u003Dzysp\u0024M4k\u003D<AnnotationCollection>(_param1.Annotations, "Annotations", _param2);
    \u0023\u003DzybqkC8coKG2MWZVsSaL09O1_xNRcS\u0024xwBWc4mutdCmTdKj2X3QJ3_KQ\u003D.\u0023\u003Dzysp\u0024M4k\u003D<ChartModifierBase>((ChartModifierBase) _param1.ChartModifier, "ChartModifier", _param2);
  }

  public override void \u0023\u003Dz4EJs3pc\u003D(
    SciChartSurface _param1,
    XmlReader _param2)
  {
    base.\u0023\u003Dz4EJs3pc\u003D(_param1, _param2);
    string str1 = _param2["Theme"];
    if (str1 != null)
      dje_zE2RKFGSKSSRHLKHMSTKDLZ3G36L4UQCSJVCT8AU3_ejd.SetTheme((DependencyObject) _param1, str1);
    string str2 = _param2["RenderSurfaceType"];
    if (str2 != null)
      RenderSurfaceBase.SetRenderSurfaceType((UIElement) _param1, str2);
    if (!_param2.Read())
      return;
    while (_param2.MoveToContent() == XmlNodeType.Element)
    {
      string localName = _param2.LocalName;
      if (localName == "RenderableSeries")
      {
        ObservableCollection<IRenderableSeries> instance = (ObservableCollection<IRenderableSeries>) Activator.CreateInstance(typeof (ObservableCollection<IRenderableSeries>));
        IEnumerable<IRenderableSeries> s1JolYrWoYpqmQ6ugs = \u0023\u003DzD8wDhZ3givcSnsIhbrLbuMG1x9yc5uc0Gequ88XBvceJ8sddSTrltYg\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003DztbbHmR4\u003D(_param2);
        instance.\u0023\u003Dz6_E5\u0024pE\u003D<IRenderableSeries>(s1JolYrWoYpqmQ6ugs);
        _param1.RenderableSeries = instance;
      }
      else
      {
        IXmlSerializable instance = (IXmlSerializable) Activator.CreateInstance(Type.GetType(_param2["Type"]));
        instance.ReadXml(_param2);
        ((object) _param1).GetType().GetProperty(localName).SetValue((object) _param1, (object) instance, (object[]) null);
      }
      _param2.Read();
    }
  }

  private static void \u0023\u003Dzysp\u0024M4k\u003D<T>(
    T _param0,
    string _param1,
    XmlWriter _param2)
    where T : IXmlSerializable
  {
    if ((object) _param0 == null)
      return;
    _param2.WriteStartElement(_param1);
    _param2.WriteAttributeString("Type", _param0.GetType().\u0023\u003Dzb_Ih6a0\u003D());
    _param0.WriteXml(_param2);
    _param2.WriteEndElement();
  }

  private static void \u0023\u003DzLq2ODtDmwh8XhlieTRcobzhsPRrHR7GqyQ\u003D\u003D(
    IEnumerable<IRenderableSeries> _param0,
    XmlWriter _param1)
  {
    if (_param0 == null)
      return;
    _param1.WriteStartElement("RenderableSeries");
    \u0023\u003DzD8wDhZ3givcSnsIhbrLbuMG1x9yc5uc0Gequ88XBvceJ8sddSTrltYg\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003DzT642HR8\u003D(_param0, _param1);
    _param1.WriteEndElement();
  }
}
