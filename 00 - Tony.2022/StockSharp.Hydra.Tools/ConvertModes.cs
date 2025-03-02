using StockSharp.Localization;
using System;
using System.ComponentModel.DataAnnotations;

namespace StockSharp.Hydra.Tools
{
    [Obsolete]
    internal enum ConvertModes
    {
        [Display( Name = "Str3773", ResourceType = typeof( LocalizedStrings ) )] OrderLogToTicks,
        [Display( Name = "Str3774", ResourceType = typeof( LocalizedStrings ) )] OrderLogToOrderBooks,
        [Display( Name = "Str3775", ResourceType = typeof( LocalizedStrings ) )] OrderLogToCandles,
        [Display( Name = "Str3776", ResourceType = typeof( LocalizedStrings ) )] TicksToCandles,
        [Display( Name = "Str3777", ResourceType = typeof( LocalizedStrings ) )] OrderBooksToCandles,
        [Display( Name = "Level1ToTicks", ResourceType = typeof( LocalizedStrings ) )] Level1ToTicks,
        [Display( Name = "Level1ToCandles", ResourceType = typeof( LocalizedStrings ) )] Level1ToCandles,
        [Display( Name = "Level1ToOrderBooks", ResourceType = typeof( LocalizedStrings ) )] Level1ToOrderBooks,
    }
}
