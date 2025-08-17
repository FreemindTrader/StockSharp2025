// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.CommandRequestScopes
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;

#nullable disable
namespace StockSharp.Web.DomainModel;

[Flags]
public enum CommandRequestScopes : long
{
    App = 1,
    Hydra = 2,
    Strategy = 4,
    Connection = 8,
}
