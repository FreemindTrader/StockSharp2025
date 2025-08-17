// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyConnection
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class StrategyConnection : BaseEntity, IClientEntity, INameEntity, IDescriptionEntity
{
    public Client Client { get; set; }

    public string Name { get; set; }

    public StrategyConnectionType Type { get; set; }

    public bool? IsDemo { get; set; }

    public string ApproveError { get; set; }

    public bool? IsApproved { get; set; }

    public KeySecret KeySecret { get; set; }

    public string Address { get; set; }

    public BaseEntitySet<StrategyAccount> Accounts { get; set; }

    string IDescriptionEntity.Description
    {
        get => this.Type?.AdapterType;
        set
        {
            this.Type = new StrategyConnectionType()
            {
                AdapterType = value
            };
        }
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Client = storage.GetValue<Client>("Client", (Client)null);
        this.Name = storage.GetValue<string>("Name", (string)null);
        this.Type = storage.GetValue<StrategyConnectionType>("Type", (StrategyConnectionType)null);
        this.IsDemo = storage.GetValue<bool?>("IsDemo", new bool?());
        this.ApproveError = storage.GetValue<string>("ApproveError", (string)null);
        this.IsApproved = storage.GetValue<bool?>("IsApproved", new bool?());
        this.KeySecret = storage.GetValue<KeySecret>("KeySecret", (KeySecret)null);
        this.Address = storage.GetValue<string>("Address", (string)null);
        this.Accounts = storage.GetValue<BaseEntitySet<StrategyAccount>>("Accounts", (BaseEntitySet<StrategyAccount>)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<Client>("Client", this.Client).Set<string>("Name", this.Name).Set<StrategyConnectionType>("Type", this.Type).Set<bool?>("IsDemo", this.IsDemo).Set<string>("ApproveError", this.ApproveError).Set<bool?>("IsApproved", this.IsApproved).Set<KeySecret>("KeySecret", this.KeySecret).Set<string>("Address", this.Address).Set<BaseEntitySet<StrategyAccount>>("Accounts", this.Accounts);
    }
}
