using StockSharp.Localization;
using System.ComponentModel.DataAnnotations;

namespace StockSharp.Fix
{
    /// <summary>FIX protocol formats.</summary>
    public enum FixFormats
    {
        /// <summary>Text format.</summary>
        [Display(Name = "Str217", ResourceType = typeof(LocalizedStrings))] Text,
        /// <summary>Binary format (FAST).</summary>
        [Display(Name = "Str1613", ResourceType = typeof(LocalizedStrings))] Binary,
    }
}
