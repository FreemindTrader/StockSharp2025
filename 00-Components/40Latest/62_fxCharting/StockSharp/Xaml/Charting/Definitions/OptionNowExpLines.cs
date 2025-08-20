using StockSharp.Xaml.Charting;

internal sealed class OptionNowExpLines
{
    private ChartLineElement chartLineElement_0;
    private ChartLineElement chartLineElement_1;

    public ChartLineElement GetNowChartLine( )
    {
        return chartLineElement_0;
    }

    public void Now( ChartLineElement chartLineElement_2 )
    {
        chartLineElement_0 = chartLineElement_2;
    }

    public ChartLineElement GetExpirationChartLine( )
    {
        return chartLineElement_1;
    }

    public void Expiration( ChartLineElement chartLineElement_2 )
    {
        chartLineElement_1 = chartLineElement_2;
    }
}
