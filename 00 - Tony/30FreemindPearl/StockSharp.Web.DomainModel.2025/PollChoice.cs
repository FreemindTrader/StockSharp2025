// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.PollChoice
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class PollChoice : BaseEntity
    {
        public string Text { get; set; }

        public Poll Poll { get; set; }

        public BaseEntitySet<PollVote> Votes { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Text = ( string ) storage.GetValue<string>( "Text", null );
            this.Poll = ( Poll ) storage.GetValue<Poll>( "Poll", null );
            this.Votes = ( BaseEntitySet<PollVote> ) storage.GetValue<BaseEntitySet<PollVote>>( "Votes", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<string>( "Text", this.Text ).Set<Poll>( "Poll", this.Poll ).Set<BaseEntitySet<PollVote>>( "Votes", this.Votes );
        }
    }
}
