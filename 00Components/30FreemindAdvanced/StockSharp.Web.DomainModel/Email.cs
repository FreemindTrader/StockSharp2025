// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.Email
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class Email : BaseEntity, INameEntity
{
    public string FromAddress { get; set; }

    public string FromAlias { get; set; }

    public string ToAddress { get; set; }

    public string ToAlias { get; set; }

    public string Name { get; set; }

    public string Text { get; set; }

    public string Html { get; set; }

    public int ErrorCount { get; set; }

    public BaseEntitySet<File> Files { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.FromAddress = storage.GetValue<string>("FromAddress", (string)null);
        this.FromAlias = storage.GetValue<string>("FromAlias", (string)null);
        this.ToAddress = storage.GetValue<string>("ToAddress", (string)null);
        this.ToAlias = storage.GetValue<string>("ToAlias", (string)null);
        this.Name = storage.GetValue<string>("Name", (string)null);
        this.Text = storage.GetValue<string>("Text", (string)null);
        this.Html = storage.GetValue<string>("Html", (string)null);
        this.ErrorCount = storage.GetValue<int>("ErrorCount", 0);
        this.Files = storage.GetValue<BaseEntitySet<File>>("Files", (BaseEntitySet<File>)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<string>("FromAddress", this.FromAddress).Set<string>("FromAlias", this.FromAlias).Set<string>("ToAddress", this.ToAddress).Set<string>("ToAlias", this.ToAlias).Set<string>("Name", this.Name).Set<string>("Text", this.Text).Set<string>("Html", this.Html).Set<int>("ErrorCount", this.ErrorCount).Set<BaseEntitySet<File>>("Files", this.Files);
    }
}
