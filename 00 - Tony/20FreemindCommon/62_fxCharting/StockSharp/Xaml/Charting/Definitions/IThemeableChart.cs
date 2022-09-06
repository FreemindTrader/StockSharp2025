using Ecng.Serialization;

namespace fx.Charting
{
    public interface IThemeableChart : IPersistable
    {
        string ChartTheme { get; set; }
    }
}
