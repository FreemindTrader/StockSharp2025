// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.PollChoice
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class PollChoice : BaseEntity
{
    public string Text { get; set; }

    public Poll Poll { get; set; }

    public BaseEntitySet<PollVote> Votes { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Text = storage.GetValue<string>("Text", (string)null);
        this.Poll = storage.GetValue<Poll>("Poll", (Poll)null);
        this.Votes = storage.GetValue<BaseEntitySet<PollVote>>("Votes", (BaseEntitySet<PollVote>)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<string>("Text", this.Text).Set<Poll>("Poll", this.Poll).Set<BaseEntitySet<PollVote>>("Votes", this.Votes);
    }
}
