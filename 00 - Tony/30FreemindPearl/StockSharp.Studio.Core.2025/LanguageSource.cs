// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.LanguageSource
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 287A45E4-79AD-43B4-B98B-3CD330C45DE5
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Core.dll

using Ecng.ComponentModel;
using StockSharp.Localization;
using System.Collections.Generic;

namespace StockSharp.Studio.Core
{
    public class LanguageSource : ItemsSourceBase<string>
    {
        protected override IEnumerable<string> GetValues()
        {
            return LocalizedStrings.LangCodes;
        }

        protected override string GetName( string value )
        {
            return LocalizedStrings.GetString( "LanguageName", value );
        }

        public LanguageSource()
        {
            
        }
    }
}
