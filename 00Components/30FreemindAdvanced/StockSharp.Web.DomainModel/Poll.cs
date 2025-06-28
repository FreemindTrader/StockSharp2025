// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.Poll
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class Poll : BaseEntity, IClientEntity
{
    public string Question { get; set; }

    public DateTime? Till { get; set; }

    public Client Client { get; set; }

    public BaseEntitySet<PollChoice> Choices { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Question = storage.GetValue<string>("Question", (string)null);
        this.Till = storage.GetValue<DateTime?>("Till", new DateTime?());
        this.Client = storage.GetValue<Client>("Client", (Client)null);
        this.Choices = storage.GetValue<BaseEntitySet<PollChoice>>("Choices", (BaseEntitySet<PollChoice>)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<string>("Question", this.Question).Set<DateTime?>("Till", this.Till).Set<Client>("Client", this.Client).Set<BaseEntitySet<PollChoice>>("Choices", this.Choices);
    }
}
