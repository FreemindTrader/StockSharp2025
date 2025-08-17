// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.AccessToken
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class AccessToken : BaseEntity, IClientEntity, IExpiryEntity
{
    public Client Client { get; set; }

    public DateTime ExpiryDate { get; set; } = DateTime.MaxValue;

    public AccessTokenScopes Scope { get; set; }

    public string Text { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Client = storage.GetValue<Client>("Client", (Client)null);
        this.ExpiryDate = storage.GetValue<DateTime>("ExpiryDate", new DateTime());
        this.Scope = storage.GetValue<AccessTokenScopes>("Scope", AccessTokenScopes.All);
        this.Text = storage.GetValue<string>("Text", (string)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<Client>("Client", this.Client).Set<DateTime>("ExpiryDate", this.ExpiryDate).Set<AccessTokenScopes>("Scope", this.Scope).Set<string>("Text", this.Text);
    }
}
