// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.Draft
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class Draft : BaseEntity, IClientEntity, INameEntity, IDescriptionEntity
{
    public Client Client { get; set; }

    public string PageId { get; set; }

    public string Name { get; set; }

    public string Text { get; set; }

    public string Tags { get; set; }

    public BaseEntitySet<File> Attachments { get; set; }

    string IDescriptionEntity.Description
    {
        get => this.Text;
        set => this.Text = value;
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Client = storage.GetValue<Client>("Client", (Client)null);
        this.PageId = storage.GetValue<string>("PageId", (string)null);
        this.Name = storage.GetValue<string>("Name", (string)null);
        this.Text = storage.GetValue<string>("Text", (string)null);
        this.Tags = storage.GetValue<string>("Tags", (string)null);
        this.Attachments = storage.GetValue<BaseEntitySet<File>>("Attachments", (BaseEntitySet<File>)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<Client>("Client", this.Client).Set<string>("PageId", this.PageId).Set<string>("Name", this.Name).Set<string>("Text", this.Text).Set<string>("Tags", this.Tags).Set<BaseEntitySet<File>>("Attachments", this.Attachments);
    }
}
