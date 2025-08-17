using Ecng.ComponentModel;
using StockSharp.Localization;
using System.Collections.Generic;
#nullable disable
namespace StockSharp.Studio.Core;

public class LanguageSource : ItemsSourceBase<string>
{
    protected override IEnumerable<string> GetValues() => LocalizedStrings.LangCodes;

    protected override string GetName(string value)
    {
        return LocalizedStrings.GetString("LanguageName", value);
    }
}
