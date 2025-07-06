using Ecng.Serialization;

namespace StockSharp.Xaml.Charting
{
    public interface IThemeableChart : IPersistable
    {
        string ChartTheme { get; set; }
    }
}
