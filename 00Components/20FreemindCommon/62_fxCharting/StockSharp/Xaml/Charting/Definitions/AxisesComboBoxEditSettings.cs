using Ecng.Common;
using fx.Charting;
using System.Collections.Specialized;

internal abstract class AxisesComboBoxEditSettings : AxisesComboBoxEditSettingsBase
{
    protected override void SetItemSource( IfxChartElement element )
    {
        DisplayMember = "Title";
        ValueMember = "Id";
        ItemsSource = element == null || !( element.ChartArea != null ) ?   ( new ChartAxis[ 0 ] ) : ( object )GetAxisesArray( element );
    }

    protected abstract INotifyCollectionChanged GetAxisesArray(
      IfxChartElement element );
}
