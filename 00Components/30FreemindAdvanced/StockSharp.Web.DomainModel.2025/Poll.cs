// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.Poll
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public class Poll : BaseEntity, IClientEntity
    {
        public string Question { get; set; }

        public DateTime? Till { get; set; }

        public Client Client { get; set; }

        public BaseEntitySet<PollChoice> Choices { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Question = ( string ) storage.GetValue<string>( "Question", null );
            this.Till = ( DateTime? ) storage.GetValue<DateTime?>( "Till", new DateTime?() );
            this.Client = ( Client ) storage.GetValue<Client>( "Client", null );
            this.Choices = ( BaseEntitySet<PollChoice> ) storage.GetValue<BaseEntitySet<PollChoice>>( "Choices", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<string>( "Question", this.Question ).Set<DateTime?>( "Till", this.Till ).Set<Client>( "Client", this.Client ).Set<BaseEntitySet<PollChoice>>( "Choices", this.Choices );
        }
    }
}
