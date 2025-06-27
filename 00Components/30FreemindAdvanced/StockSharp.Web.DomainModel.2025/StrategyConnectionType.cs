// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyConnectionType
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class StrategyConnectionType : BaseEntity, INameEntity, IDescriptionEntity
    {
        public string AdapterType { get; set; }

        public BaseEntitySet<StrategyConnection> Connections { get; set; }

        string INameEntity.Name
        {
            get
            {
                return this.AdapterType;
            }
            set
            {
                this.AdapterType = value;
            }
        }

        string IDescriptionEntity.Description
        {
            get
            {
                return this.AdapterType;
            }
            set
            {
                this.AdapterType = value;
            }
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.AdapterType = ( string ) storage.GetValue<string>( "AdapterType", null );
            this.Connections = ( BaseEntitySet<StrategyConnection> ) storage.GetValue<BaseEntitySet<StrategyConnection>>( "Connections", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<string>( "AdapterType", this.AdapterType ).Set<BaseEntitySet<StrategyConnection>>( "Connections", this.Connections );
        }
    }
}
