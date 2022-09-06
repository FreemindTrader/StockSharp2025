
using StockSharp.Localization;
using System.ComponentModel.DataAnnotations;

namespace StockSharp.Diagram
{
    /// <summary>Scheme types.</summary>
    public enum SchemeTypes
    {
        /// <summary>Regular scheme.</summary>
        [Display( Name = "Str1629", ResourceType = typeof( LocalizedStrings ) )] Regular,
        /// <summary>Encrypted scheme.</summary>
        [Display( Name = "IsEncrypted", ResourceType = typeof( LocalizedStrings ) )] Encrypted,
        /// <summary>Independent scheme.</summary>
        [Display( Name = "IsIndependent", ResourceType = typeof( LocalizedStrings ) )] Independent,
    }
}
