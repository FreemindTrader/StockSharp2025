// Decompiled with JetBrains decompiler
// Type: #=zawTMm83sNsuVHdgLsihy4doIUbD$mKpPwQ==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Helpers;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.PropertyGrid;
using StockSharp.Charting;
using StockSharp.Xaml.Charting.Ultrachart;

#nullable disable
internal abstract class \u0023\u003DzawTMm83sNsuVHdgLsihy4doIUbD\u0024mKpPwQ\u003D\u003D : 
  ComboBoxEditSettings
{
  public override IBaseEdit CreateEditor(
    bool _param1,
    IDefaultEditorViewInfo _param2,
    EditorOptimizationMode _param3)
  {
    if (this.ItemsSource != null)
      return base.CreateEditor(_param1, _param2, _param3);
    if (!(_param2 is EditorColumn editorColumn))
      return base.CreateEditor(_param1, _param2, _param3);
    IChartElement chartElement = this.\u0023\u003DzfA7kt3oacVpT(editorColumn.Owner);
    if (chartElement == null)
      return base.CreateEditor(_param1, _param2, _param3);
    this.\u0023\u003DzC4EgRM0UvJOX(chartElement);
    return base.CreateEditor(_param1, _param2, _param3);
  }

  protected abstract void \u0023\u003DzC4EgRM0UvJOX(IChartElement _param1);

  private IChartElement \u0023\u003DzfA7kt3oacVpT(RowData _param1)
  {
    for (; !(_param1.Value is ChartIndicatorElementSettingsObject elementSettingsObject); _param1 = _param1.Parent)
    {
      if (_param1.Value is IChartElement chartElement)
        return chartElement;
      if (_param1.Parent == null)
        return (IChartElement) null;
    }
    return elementSettingsObject.Orig;
  }
}
