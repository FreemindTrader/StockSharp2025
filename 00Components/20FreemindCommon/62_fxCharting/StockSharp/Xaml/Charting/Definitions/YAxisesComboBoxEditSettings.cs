using StockSharp.Xaml.Charting;
using System.Collections.Specialized;

internal sealed class YAxisesComboBoxEditSettings : AxisesComboBoxEditSettings
{
    protected override INotifyCollectionChanged GetAxisesArray(
      IChartElement element )
    {
        return element.ChartArea.YAxises as INotifyCollectionChanged;
    }
}
