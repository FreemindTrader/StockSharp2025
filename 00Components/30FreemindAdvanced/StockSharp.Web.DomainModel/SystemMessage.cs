// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.SystemMessage
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class SystemMessage : Message
{
    public string Source { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Source = storage.GetValue<string>("Source", (string)null);
        this.ExpiryDate = storage.GetValue<DateTime?>("ExpiryDate", new DateTime?());
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<string>("Source", this.Source).Set<DateTime?>("ExpiryDate", this.ExpiryDate);
    }
}
