// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.BaseResponse
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public abstract class BaseResponse : IPersistable
    {
        protected BaseResponse( SubscriptionTypes type )
        {
            this.Type = type;
        }

        public SubscriptionTypes Type { get; }

        public long RequestId { get; set; }

        public virtual bool AllowEveryone
        {
            get
            {
                return false;
            }
        }

        public override string ToString()
        {
            return string.Format( "Type={0}, RequestId={1}", ( object ) this.Type, ( object ) this.RequestId );
        }

        public virtual void Load( SettingsStorage storage )
        {
            this.RequestId = ( long ) storage.GetValue<long>( "RequestId", 0L );
        }

        public virtual void Save( SettingsStorage storage )
        {
            storage.Set<long>( "RequestId", this.RequestId );
        }
    }
}
