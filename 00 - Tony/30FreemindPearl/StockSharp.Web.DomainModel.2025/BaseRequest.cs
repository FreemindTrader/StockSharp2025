// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.BaseRequest
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public abstract class BaseRequest : IPersistable
    {
        protected BaseRequest( SubscriptionTypes type )
        {
            Type = type;
        }

        public bool IsSubscribe { get; set; }

        public long Id { get; set; }

        public SubscriptionTypes Type { get; set; }

        public virtual bool AllowClient
        {
            get
            {
                return false;
            }
        }

        public override string ToString()
        {
            return string.Format( "Type={0}, Id={1}", ( object ) this.Type, ( object ) this.Id );
        }

        public virtual void Load( SettingsStorage storage )
        {
            this.IsSubscribe = ( bool ) storage.GetValue<bool>( "IsSubscribe", false );
            this.Id = ( long ) storage.GetValue<long>( "Id", 0L );
            this.Type = ( SubscriptionTypes ) storage.GetValue<SubscriptionTypes>( "Type", 0 );
        }

        public virtual void Save( SettingsStorage storage )
        {
            storage.Set<bool>( "IsSubscribe", ( this.IsSubscribe ) ).Set<long>( "Id", this.Id ).Set<SubscriptionTypes>( "Type", this.Type );
        }
    }
}
