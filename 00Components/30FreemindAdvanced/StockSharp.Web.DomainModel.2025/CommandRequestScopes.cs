// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.CommandRequestScopes
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using System;

namespace StockSharp.Web.DomainModel
{
    [Flags]
    public enum CommandRequestScopes : long
    {
        App = 1,
        Hydra = 2,
        Strategy = 4,
        Connection = 8,
    }
}
