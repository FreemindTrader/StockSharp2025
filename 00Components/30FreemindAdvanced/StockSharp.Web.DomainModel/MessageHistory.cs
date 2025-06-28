// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.MessageHistory
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class MessageHistory : BaseEntity, IMessageEntity, IDescriptionEntity
{
    public Message Message { get; set; }

    public string Text { get; set; }

    string IDescriptionEntity.Description
    {
        get => this.Text;
        set => this.Text = value;
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Message = storage.GetValue<Message>("Message", (Message)null);
        this.Text = storage.GetValue<string>("Text", (string)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<Message>("Message", this.Message).Set<string>("Text", this.Text);
    }
}
