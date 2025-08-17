// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.Social
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class Social : BaseEntity, INameEntity, IPictureEntity
{
    public string Url { get; set; }

    public string Code { get; set; }

    public string Name { get; set; }

    public string LoginScope { get; set; }

    public string PostScope { get; set; }

    public SocialUrl AuthReal { get; set; }

    public SocialUrl AuthDemo { get; set; }

    public string AuthHandler { get; set; }

    public int CharsLimit { get; set; }

    public File Picture { get; set; }

    public Domain AuthDomainOnly { get; set; }

    public KeySecret Auth { get; set; }

    public KeySecret Blog { get; set; }

    public KeySecret AccessToken { get; set; }

    public string BlogHandler { get; set; }

    public SocialFlags Flags { get; set; }

    public bool Logging { get; set; }

    public BaseEntitySet<Client> Clients { get; set; }

    public BaseEntitySet<ClientSocial> ClientSocials { get; set; }

    public BaseEntitySet<Client> Tokens { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Url = storage.GetValue<string>("Url", (string)null);
        this.Code = storage.GetValue<string>("Code", (string)null);
        this.Name = storage.GetValue<string>("Name", (string)null);
        this.LoginScope = storage.GetValue<string>("LoginScope", (string)null);
        this.PostScope = storage.GetValue<string>("PostScope", (string)null);
        this.AuthReal = storage.GetValue<SocialUrl>("AuthReal", (SocialUrl)null);
        this.AuthDemo = storage.GetValue<SocialUrl>("AuthDemo", (SocialUrl)null);
        this.AuthHandler = storage.GetValue<string>("AuthHandler", (string)null);
        this.CharsLimit = storage.GetValue<int>("CharsLimit", 0);
        this.Picture = storage.GetValue<File>("Picture", (File)null);
        this.AuthDomainOnly = storage.GetValue<Domain>("AuthDomainOnly", (Domain)null);
        this.Auth = storage.GetValue<KeySecret>("Auth", (KeySecret)null);
        this.Blog = storage.GetValue<KeySecret>("Blog", (KeySecret)null);
        this.AccessToken = storage.GetValue<KeySecret>("AccessToken", (KeySecret)null);
        this.BlogHandler = storage.GetValue<string>("BlogHandler", (string)null);
        this.Flags = storage.GetValue<SocialFlags>("Flags", (SocialFlags)0);
        this.Logging = storage.GetValue<bool>("Logging", false);
        this.Clients = storage.GetValue<BaseEntitySet<Client>>("Clients", (BaseEntitySet<Client>)null);
        this.ClientSocials = storage.GetValue<BaseEntitySet<ClientSocial>>("ClientSocials", (BaseEntitySet<ClientSocial>)null);
        this.Tokens = storage.GetValue<BaseEntitySet<Client>>("Tokens", (BaseEntitySet<Client>)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<string>("Url", this.Url).Set<string>("Code", this.Code).Set<string>("Name", this.Name).Set<string>("LoginScope", this.LoginScope).Set<string>("PostScope", this.PostScope).Set<SocialUrl>("AuthReal", this.AuthReal).Set<SocialUrl>("AuthDemo", this.AuthDemo).Set<string>("AuthHandler", this.AuthHandler).Set<int>("CharsLimit", this.CharsLimit).Set<File>("Picture", this.Picture).Set<Domain>("AuthDomainOnly", this.AuthDomainOnly).Set<KeySecret>("Auth", this.Auth).Set<KeySecret>("Blog", this.Blog).Set<KeySecret>("AccessToken", this.AccessToken).Set<string>("BlogHandler", this.BlogHandler).Set<SocialFlags>("Flags", this.Flags).Set<bool>("Logging", this.Logging).Set<BaseEntitySet<Client>>("Clients", this.Clients).Set<BaseEntitySet<ClientSocial>>("ClientSocials", this.ClientSocials).Set<BaseEntitySet<Client>>("Tokens", this.Tokens);
    }
}
