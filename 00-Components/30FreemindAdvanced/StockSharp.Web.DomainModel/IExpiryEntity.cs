// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.IExpiryEntity
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;

#nullable disable
namespace StockSharp.Web.DomainModel;

public interface IExpiryEntity
{
    DateTime ExpiryDate { get; set; }
}
