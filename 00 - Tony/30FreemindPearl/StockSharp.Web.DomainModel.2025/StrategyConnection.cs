// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyConnection
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class StrategyConnection : BaseEntity, IClientEntity, INameEntity, IDescriptionEntity
    {
        public Client Client { get; set; }

        public string Name { get; set; }

        public StrategyConnectionType Type { get; set; }

        public bool? IsDemo { get; set; }

        public string ApproveError { get; set; }

        public bool? IsApproved { get; set; }

        public KeySecret KeySecret { get; set; }

        public string Address { get; set; }

        public BaseEntitySet<StrategyAccount> Accounts { get; set; }

        string IDescriptionEntity.Description
        {
            get
            {
                return this.Type?.AdapterType;
            }
            set
            {
                this.Type = new StrategyConnectionType()
                {
                    AdapterType = value
                };
            }
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Client = ( Client ) storage.GetValue<Client>( "Client", null );
            this.Name = ( string ) storage.GetValue<string>( "Name", null );
            this.Type = ( StrategyConnectionType ) storage.GetValue<StrategyConnectionType>( "Type", null );
            this.IsDemo = ( bool? ) storage.GetValue<bool?>( "IsDemo", new bool?() );
            this.ApproveError = ( string ) storage.GetValue<string>( "ApproveError", null );
            this.IsApproved = ( bool? ) storage.GetValue<bool?>( "IsApproved", new bool?() );
            this.KeySecret = ( KeySecret ) storage.GetValue<KeySecret>( "KeySecret", null );
            this.Address = ( string ) storage.GetValue<string>( "Address", null );
            this.Accounts = ( BaseEntitySet<StrategyAccount> ) storage.GetValue<BaseEntitySet<StrategyAccount>>( "Accounts", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<Client>( "Client", this.Client ).Set<string>( "Name", this.Name ).Set<StrategyConnectionType>( "Type", this.Type ).Set<bool?>( "IsDemo", this.IsDemo ).Set<string>( "ApproveError", this.ApproveError ).Set<bool?>( "IsApproved", this.IsApproved ).Set<KeySecret>( "KeySecret", this.KeySecret ).Set<string>( "Address", this.Address ).Set<BaseEntitySet<StrategyAccount>>( "Accounts", this.Accounts );
        }
    }
}
