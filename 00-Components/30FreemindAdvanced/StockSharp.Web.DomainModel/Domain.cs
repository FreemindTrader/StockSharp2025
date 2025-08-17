// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.Domain
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Common;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class Domain : BaseEntity, INameEntity
{
    public string Code { get; set; }

    public string Culture { get; set; }

    public CurrencyTypes Currency { get; set; }

    public int YandexCounter { get; set; }

    public string GoogleCounter { get; set; }

    public int VKCounter { get; set; }

    public string Phone { get; set; }

    public string Messenger { get; set; }

    public FileGroup SiteMap { get; set; }

    public string Pluso { get; set; }

    public Topic Error403 { get; set; }

    public Topic Error404 { get; set; }

    public bool YandexChat { get; set; }

    public File Yml { get; set; }

    public Domain PriceSource { get; set; }

    public string GoogleSearch { get; set; }

    string INameEntity.Name
    {
        get => this.Code;
        set => this.Code = value;
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Code = storage.GetValue<string>("Code", (string)null);
        this.Culture = storage.GetValue<string>("Culture", (string)null);
        this.Currency = storage.GetValue<CurrencyTypes>("Currency", (CurrencyTypes)0);
        this.YandexCounter = storage.GetValue<int>("YandexCounter", 0);
        this.GoogleCounter = storage.GetValue<string>("GoogleCounter", (string)null);
        this.VKCounter = storage.GetValue<int>("VKCounter", 0);
        this.Phone = storage.GetValue<string>("Phone", (string)null);
        this.Messenger = storage.GetValue<string>("Messenger", (string)null);
        this.SiteMap = storage.GetValue<FileGroup>("SiteMap", (FileGroup)null);
        this.Pluso = storage.GetValue<string>("Pluso", (string)null);
        this.Error403 = storage.GetValue<Topic>("Error403", (Topic)null);
        this.Error404 = storage.GetValue<Topic>("Error404", (Topic)null);
        this.YandexChat = storage.GetValue<bool>("YandexChat", false);
        this.Yml = storage.GetValue<File>("Yml", (File)null);
        this.PriceSource = storage.GetValue<Domain>("PriceSource", (Domain)null);
        this.GoogleSearch = storage.GetValue<string>("GoogleSearch", (string)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<string>("Code", this.Code).Set<string>("Culture", this.Culture).Set<CurrencyTypes>("Currency", this.Currency).Set<int>("YandexCounter", this.YandexCounter).Set<string>("GoogleCounter", this.GoogleCounter).Set<int>("VKCounter", this.VKCounter).Set<string>("Phone", this.Phone).Set<string>("Messenger", this.Messenger).Set<FileGroup>("SiteMap", this.SiteMap).Set<string>("Pluso", this.Pluso).Set<Topic>("Error403", this.Error403).Set<Topic>("Error404", this.Error404).Set<bool>("YandexChat", this.YandexChat).Set<File>("Yml", this.Yml).Set<Domain>("PriceSource", this.PriceSource).Set<string>("GoogleSearch", this.GoogleSearch);
    }
}
