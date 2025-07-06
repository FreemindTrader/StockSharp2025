using StockSharp.Localization;
using System.ComponentModel.DataAnnotations;

namespace StockSharp.Xaml.Charting
{
    public enum DrawStyles
    {
        [Display( Name = "Str1898"   , ResourceType = typeof( LocalizedStrings ) )] Line,
        [Display( Name = "Str1972"   , ResourceType = typeof( LocalizedStrings ) )] NoGapLine,
        [Display( Name = "Str1973"   , ResourceType = typeof( LocalizedStrings ) )] StepLine,
        [Display( Name = "Str1974"   , ResourceType = typeof( LocalizedStrings ) )] Band,
        [Display( Name = "Str1974_2" , ResourceType = typeof( LocalizedStrings ) )] BandOneValue,
        [Display( Name = "Str1975"   , ResourceType = typeof( LocalizedStrings ) )] Dot,
        [Display( Name = "Str1976"   , ResourceType = typeof( LocalizedStrings ) )] Histogram,
        [Display( Name = "Str1977"   , ResourceType = typeof( LocalizedStrings ) )] Bubble,
        [Display( Name = "Str1978"   , ResourceType = typeof( LocalizedStrings ) )] StackedBar,
        [Display( Name = "DashedLine", ResourceType = typeof( LocalizedStrings ) )] DashedLine,
        [Display( Name = "Area"      , ResourceType = typeof( LocalizedStrings ) )] Area,
        [Display( Name = "Sprite"    , ResourceType = typeof( LocalizedStrings ) )] Sprite
    }
}
