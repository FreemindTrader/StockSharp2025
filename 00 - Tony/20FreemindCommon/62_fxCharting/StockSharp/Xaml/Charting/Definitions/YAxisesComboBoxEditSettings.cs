using fx.Charting;
using System.Collections.Specialized;

internal sealed class YAxisesComboBoxEditSettings : AxisesComboBoxEditSettings
{
    protected override INotifyCollectionChanged GetAxisesArray(
      IfxChartElement element )
    {
        return element.ChartArea.YAxises as INotifyCollectionChanged;
    }
}
