using StockSharp.Localization;
using System.ComponentModel.DataAnnotations;

namespace StockSharp.Server.Core
{
    /// <summary>Types of authorization.</summary>
    public enum AuthorizationModes
    {
        /// <summary>Anonymous.</summary>
        [Display( Name = "Anonymous", ResourceType = typeof( LocalizedStrings ) )] Anonymous,
        /// <summary>Windows authorization.</summary>
        [Display( Name = "Windows", ResourceType = typeof( LocalizedStrings ) )] Windows,
        /// <summary>Custom.</summary>
        [Display( Name = "Custom", ResourceType = typeof( LocalizedStrings ) )] Custom,
        /// <summary>StockSharp.</summary>
        [Display( Name = "StockSharp", ResourceType = typeof( LocalizedStrings ) )] Community,
    }
}
