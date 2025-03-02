// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Common.SearchPhrase
// Assembly: StockSharp.Web.Common, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E38A38B-3071-40E9-9B31-80D08347A76B
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Common.dll

using System;

namespace StockSharp.Web.Common
{
    public class SearchPhrase
    {
        public SearchPhrase( string value, int index, bool isExact, bool isSpecial )
        {
            string str = value;
            if ( str == null )
                throw new ArgumentNullException( nameof( value ) );
            this.Value = str;
            this.Index = index;
            this.IsExact = isExact;
            this.IsSpecial = isSpecial;            
        }

        public string Value { get; }

        public int Index { get; }

        public bool IsExact { get; }

        public bool IsSpecial { get; }
    }
}
