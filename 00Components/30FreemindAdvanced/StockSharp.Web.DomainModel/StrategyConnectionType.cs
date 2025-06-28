// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyConnectionType
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class StrategyConnectionType : BaseEntity, INameEntity, IDescriptionEntity
{
    public string AdapterType { get; set; }

    public BaseEntitySet<StrategyConnection> Connections { get; set; }

    string INameEntity.Name
    {
        get => this.AdapterType;
        set => this.AdapterType = value;
    }

    string IDescriptionEntity.Description
    {
        get => this.AdapterType;
        set => this.AdapterType = value;
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.AdapterType = storage.GetValue<string>("AdapterType", (string)null);
        this.Connections = storage.GetValue<BaseEntitySet<StrategyConnection>>("Connections", (BaseEntitySet<StrategyConnection>)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<string>("AdapterType", this.AdapterType).Set<BaseEntitySet<StrategyConnection>>("Connections", this.Connections);
    }
}
