using SciChart.Charting.Visuals.RenderableSeries;
using StockSharp.Charting;
using StockSharp.Xaml.Charting;
using System;
using System.Globalization;
using System.Windows.Data;

public enum OhlcLineDrawMode
{
    Open,
    High,
    Low,
    Close,
}
internal sealed class DrawStylesToModeConverter : IValueConverter
{
    object IValueConverter.Convert(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture )
    {
        switch ( ( ChartCandleDrawStyles )value )
        {
            case ChartCandleDrawStyles.LineOpen:
                return OhlcLineDrawMode.Open;
            case ChartCandleDrawStyles.LineHigh:
                return OhlcLineDrawMode.High;
            case ChartCandleDrawStyles.LineLow:
                return OhlcLineDrawMode.Low;
            default:
                return OhlcLineDrawMode.Close;
        }
    }

    object IValueConverter.ConvertBack(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture )
    {
        switch ( ( OhlcLineDrawMode )value )
        {
            case OhlcLineDrawMode.Open:
                return ChartCandleDrawStyles.LineOpen;
            case OhlcLineDrawMode.High:
                return ChartCandleDrawStyles.LineHigh;
            case OhlcLineDrawMode.Low:
                return ChartCandleDrawStyles.LineLow;
            default:
                return ChartCandleDrawStyles.LineClose;
        }
    }
}
