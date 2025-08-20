using StockSharp.Localization;
using System.ComponentModel.DataAnnotations;

namespace StockSharp.Xaml.Charting
{
    public enum ChartAxisAlignment
    {
        [Display( Name = "Str1910", ResourceType = typeof( LocalizedStrings ) )] Default,
        [Display( Name = "Str1911", ResourceType = typeof( LocalizedStrings ) )] Right,
        [Display( Name = "Str1912", ResourceType = typeof( LocalizedStrings ) )] Left,
        [Display( Name = "Str1913", ResourceType = typeof( LocalizedStrings ) )] Top,
        [Display( Name = "Str1914", ResourceType = typeof( LocalizedStrings ) )] Bottom,
    }
}
