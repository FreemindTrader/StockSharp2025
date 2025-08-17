// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.SocialToken
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using Ecng.Net;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class SocialToken : BaseEntity, IClientEntity, IOAuthToken
{
    public Social Social { get; set; }

    public Client Client { get; set; }

    public SocialTokenTypes Type { get; set; }

    public string Value { get; set; }

    public DateTime? Expires { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Social = storage.GetValue<Social>("Social", (Social)null);
        this.Client = storage.GetValue<Client>("Client", (Client)null);
        this.Type = storage.GetValue<SocialTokenTypes>("Type", SocialTokenTypes.Refresh);
        this.Value = storage.GetValue<string>("Value", (string)null);
        this.Expires = storage.GetValue<DateTime?>("Expires", new DateTime?());
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<Social>("Social", this.Social).Set<Client>("Client", this.Client).Set<SocialTokenTypes>("Type", this.Type).Set<string>("Value", this.Value).Set<DateTime?>("Expires", this.Expires);
    }
}
