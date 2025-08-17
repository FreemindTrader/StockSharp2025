// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.TempSalt
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class TempSalt : BaseEntity
{
    public byte[] Salt { get; set; }

    public byte[] IV { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Salt = storage.GetValue<byte[]>("Salt", (byte[])null);
        this.IV = storage.GetValue<byte[]>("IV", (byte[])null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<byte[]>("Salt", this.Salt).Set<byte[]>("IV", this.IV);
    }
}
