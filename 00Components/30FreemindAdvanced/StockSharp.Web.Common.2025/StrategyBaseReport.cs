// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Common.StrategyBaseReport
// Assembly: StockSharp.Web.Common, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E38A38B-3071-40E9-9B31-80D08347A76B
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Common.dll

using StockSharp.Web.DomainModel;

namespace StockSharp.Web.Common
{
    public abstract class StrategyBaseReport
    {
        public StrategyLog [ ] Logs { get; set; }

        public StrategyErrorInfo Error { get; set; }
    }
}
