// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ClientBalance
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Common;
using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public class ClientBalance : BaseEntity, IClientEntity
    {
        public Client Client { get; set; }

        public CurrencyTypes Currency { get; set; }

        public Decimal Amount { get; set; }

        public BaseEntitySet<ClientBalanceHistory> History { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Client = ( Client ) storage.GetValue<Client>( "Client", null );
            this.Currency = ( CurrencyTypes ) storage.GetValue<CurrencyTypes>( "Currency", 0 );
            this.Amount = ( Decimal ) storage.GetValue<Decimal>( "Amount", Decimal.Zero );
            this.History = ( BaseEntitySet<ClientBalanceHistory> ) storage.GetValue<BaseEntitySet<ClientBalanceHistory>>( "History", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<Client>( "Client", this.Client ).Set<CurrencyTypes>( "Currency", this.Currency ).Set<Decimal>( "Amount", this.Amount ).Set<BaseEntitySet<ClientBalanceHistory>>( "History", this.History );
        }
    }
}
