// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyType
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class StrategyType : BaseEntity, IClientEntity, INameEntity, IProductContentTypeEntity
{
    public Client Client { get; set; }

    public string Name { get; set; }

    public string TypeName { get; set; }

    public string Password { get; set; }

    public ProductContentTypes2 ContentType { get; set; }

    public File Content { get; set; }

    public BaseEntitySet<Strategy> Strategies { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Client = storage.GetValue<Client>("Client", (Client)null);
        this.Name = storage.GetValue<string>("Name", (string)null);
        this.TypeName = storage.GetValue<string>("TypeName", (string)null);
        this.Password = storage.GetValue<string>("Password", (string)null);
        this.ContentType = storage.GetValue<ProductContentTypes2>("ContentType", (ProductContentTypes2)0);
        this.Content = storage.GetValue<File>("Content", (File)null);
        this.Strategies = storage.GetValue<BaseEntitySet<Strategy>>("Strategies", (BaseEntitySet<Strategy>)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<Client>("Client", this.Client).Set<string>("Name", this.Name).Set<string>("TypeName", this.TypeName).Set<string>("Password", this.Password).Set<ProductContentTypes2>("ContentType", this.ContentType).Set<File>("Content", this.Content).Set<BaseEntitySet<Strategy>>("Strategies", this.Strategies);
    }
}
