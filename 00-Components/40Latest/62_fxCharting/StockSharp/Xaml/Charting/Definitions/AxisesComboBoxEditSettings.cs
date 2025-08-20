using Ecng.Common;
using StockSharp.Xaml.Charting;
using StockSharp.Charting;
using System.Collections.Specialized;

internal abstract class AxisesComboBoxEditSettings : AxisesComboBoxEditSettingsBase
{
    protected override void SetItemSource( IChartElement element )
    {
        DisplayMember = "Title";
        ValueMember = "Id";
        ItemsSource = element == null || !( element.ChartArea != null ) ?   ( new ChartAxis[ 0 ] ) : ( object )GetAxisesArray( element );
    }

    protected abstract INotifyCollectionChanged GetAxisesArray(
      IChartElement element );
}
