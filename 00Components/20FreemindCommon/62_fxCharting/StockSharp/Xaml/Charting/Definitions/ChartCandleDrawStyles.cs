using StockSharp.Localization;
using System.ComponentModel.DataAnnotations;

namespace StockSharp.Xaml.Charting
{
    public enum ChartCandleDrawStyles
    {
        [Display( Name = "CandleStick", ResourceType = typeof( LocalizedStrings ) )] CandleStick,
        [Display( Name = "Bars", ResourceType = typeof( LocalizedStrings ) )] Ohlc,
        [Display( Name = "LineOpen", ResourceType = typeof( LocalizedStrings ) )] LineOpen,
        [Display( Name = "LineHigh", ResourceType = typeof( LocalizedStrings ) )] LineHigh,
        [Display( Name = "LineLow", ResourceType = typeof( LocalizedStrings ) )] LineLow,
        [Display( Name = "LineClose", ResourceType = typeof( LocalizedStrings ) )] LineClose,
        [Display( Name = "BoxChart", ResourceType = typeof( LocalizedStrings ) )] BoxVolume,
        [Display( Name = "ClusterProfile", ResourceType = typeof( LocalizedStrings ) )] ClusterProfile,
        [Display( Name = "Area", ResourceType = typeof( LocalizedStrings ) )] Area,
        [Display( Name = "PnFCandle", ResourceType = typeof( LocalizedStrings ) )] PnF,
    }
}
