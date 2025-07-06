using StockSharp.Xaml.Charting;
using System.Collections.Specialized;

internal sealed class XAxisesComboBoxEditSettings : AxisesComboBoxEditSettings
{
    protected override INotifyCollectionChanged GetAxisesArray(
      IChartElement element )
    {
        return element.ChartArea.XAxises as INotifyCollectionChanged;
    }
}
