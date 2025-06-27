// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ShortUrl
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public class ShortUrl : BaseEntity, IClientEntity, IExpiryEntity
    {
        public Client Client { get; set; }

        public string Origin { get; set; }

        public string Short { get; set; }

        public DateTime ExpiryDate { get; set; } = DateTime.MaxValue;

        public BaseEntitySet<ShortUrlVisit> Visits { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Client = ( Client ) storage.GetValue<Client>( "Client", null );
            this.Origin = ( string ) storage.GetValue<string>( "Origin", null );
            this.Short = ( string ) storage.GetValue<string>( "Short", null );
            this.ExpiryDate = ( DateTime ) storage.GetValue<DateTime>( "ExpiryDate", new DateTime() );
            this.Visits = ( BaseEntitySet<ShortUrlVisit> ) storage.GetValue<BaseEntitySet<ShortUrlVisit>>( "Visits", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<Client>( "Client", this.Client ).Set<string>( "Origin", this.Origin ).Set<string>( "Short", this.Short ).Set<DateTime>( "ExpiryDate", this.ExpiryDate ).Set<BaseEntitySet<ShortUrlVisit>>( "Visits", this.Visits );
        }
    }
}
