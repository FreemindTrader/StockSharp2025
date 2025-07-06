using StockSharp.Localization;
using System.ComponentModel.DataAnnotations;

namespace StockSharp.Xaml.Charting
{
    public enum ChartAxisType
    {
        [Display( Name = "Time", ResourceType = typeof( LocalizedStrings ) )] DateTime,
        [Display( Name = "Str1915", ResourceType = typeof( LocalizedStrings ) )] CategoryDateTime,
        [Display( Name = "Str1916", ResourceType = typeof( LocalizedStrings ) )] Numeric,
    }
}
