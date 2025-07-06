using StockSharp.Xaml.Charting;

internal sealed class OptionNowExpLines
{
    private LineUI chartLineElement_0;
    private LineUI chartLineElement_1;

    public LineUI GetNowChartLine( )
    {
        return chartLineElement_0;
    }

    public void Now( LineUI chartLineElement_2 )
    {
        chartLineElement_0 = chartLineElement_2;
    }

    public LineUI GetExpirationChartLine( )
    {
        return chartLineElement_1;
    }

    public void Expiration( LineUI chartLineElement_2 )
    {
        chartLineElement_1 = chartLineElement_2;
    }
}
