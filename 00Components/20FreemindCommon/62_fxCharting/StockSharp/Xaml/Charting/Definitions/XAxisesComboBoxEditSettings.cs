using fx.Charting;
using System.Collections.Specialized;

internal sealed class XAxisesComboBoxEditSettings : AxisesComboBoxEditSettings
{
    protected override INotifyCollectionChanged GetAxisesArray(
      IfxChartElement element )
    {
        return element.ChartArea.XAxises as INotifyCollectionChanged;
    }
}
