// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.SocialToken
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public class SocialToken : BaseEntity, IClientEntity, Ecng.Net.IOAuthToken
    {
        public Social Social { get; set; }

        public Client Client { get; set; }

        public SocialTokenTypes Type { get; set; }

        public string Value { get; set; }

        public DateTime? Expires { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Social = ( Social ) storage.GetValue<Social>( "Social", null );
            this.Client = ( Client ) storage.GetValue<Client>( "Client", null );
            this.Type = ( SocialTokenTypes ) storage.GetValue<SocialTokenTypes>( "Type", 0 );
            this.Value = ( string ) storage.GetValue<string>( "Value", null );
            this.Expires = ( DateTime? ) storage.GetValue<DateTime?>( "Expires", new DateTime?() );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<Social>( "Social", this.Social ).Set<Client>( "Client", this.Client ).Set<SocialTokenTypes>( "Type", this.Type ).Set<string>( "Value", this.Value ).Set<DateTime?>( "Expires", this.Expires );
        }
    }
}
