// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Common.StrategyBaseReport
// Assembly: StockSharp.Web.Common, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 02092862-EA5F-4AA7-B6CA-D0C9A4C8AFDF
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Common.dll

using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Common;

public abstract class StrategyBaseReport
{
    public StrategyLog[] Logs { get; set; }

    public StrategyErrorInfo Error { get; set; }
}
