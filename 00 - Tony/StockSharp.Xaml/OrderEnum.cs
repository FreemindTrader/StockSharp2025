using StockSharp.Localization;
using System.ComponentModel.DataAnnotations;

namespace StockSharp.Xaml
{
    internal enum OrderEnum
    {
        [Display( Name = "Str237", ResourceType = typeof( LocalizedStrings ) )] None,
        [Display( Name = "Str538", ResourceType = typeof( LocalizedStrings ) )] Pending,
        [Display( Name = "Str152", ResourceType = typeof( LocalizedStrings ) )] Failed,
        [Display( Name = "Str238", ResourceType = typeof( LocalizedStrings ) )] Active,
        [Display( Name = "Str1329", ResourceType = typeof( LocalizedStrings ) )] Cancelled,
        [Display( Name = "Str1328", ResourceType = typeof( LocalizedStrings ) )] Matched,
    }
}
