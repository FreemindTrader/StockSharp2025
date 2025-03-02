// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ClientSocial
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.ComponentModel;
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class ClientSocial : BaseEntity, IClientEntity, INameEntity, ITelegramChannel
    {
        public Client Client { get; set; }

        public Social Social { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public Domain DomainOnly { get; set; }

        public string Url { get; set; }

        public long? MessageCount { get; set; }

        public bool? IsApproved { get; set; }

        public string AI { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Client = ( Client ) storage.GetValue<Client>( "Client", null );
            this.Social = ( Social ) storage.GetValue<Social>( "Social", null );
            this.Name = ( string ) storage.GetValue<string>( "Name", null );
            this.Code = ( string ) storage.GetValue<string>( "Code", null );
            this.DomainOnly = ( Domain ) storage.GetValue<Domain>( "DomainOnly", null );
            this.Url = ( string ) storage.GetValue<string>( "Url", null );
            this.MessageCount = ( long? ) storage.GetValue<long?>( "MessageCount", new long?() );
            this.IsApproved = ( bool? ) storage.GetValue<bool?>( "IsApproved", new bool?() );
            this.AI = ( string ) storage.GetValue<string>( "AI", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<Client>( "Client", this.Client ).Set<Social>( "Social", this.Social ).Set<string>( "Name", this.Name ).Set<string>( "Code", this.Code ).Set<Domain>( "DomainOnly", this.DomainOnly ).Set<string>( "Url", this.Url ).Set<long?>( "MessageCount", this.MessageCount ).Set<bool?>( "IsApproved", this.IsApproved ).Set<string>( "AI", this.AI );
        }
    }
}
