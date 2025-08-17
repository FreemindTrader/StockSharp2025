// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Common.SearchPhrase
// Assembly: StockSharp.Web.Common, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 02092862-EA5F-4AA7-B6CA-D0C9A4C8AFDF
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Common.dll

using System;

#nullable disable
namespace StockSharp.Web.Common;

public class SearchPhrase(string value, int index, bool isExact, bool isSpecial)
{
    public string Value { get; } = value ?? throw new ArgumentNullException(nameof(value));

    public int Index { get; } = index;

    public bool IsExact { get; } = isExact;

    public bool IsSpecial { get; } = isSpecial;
}
