// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ClientBalanceHistory
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public class ClientBalanceHistory : BaseEntity
    {
        public Payment Payment { get; set; }

        public ClientBalance Balance { get; set; }

        public Decimal Diff { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Payment = ( Payment ) storage.GetValue<Payment>( "Payment", null );
            this.Balance = ( ClientBalance ) storage.GetValue<ClientBalance>( "Balance", null );
            this.Diff = ( Decimal ) storage.GetValue<Decimal>( "Diff", Decimal.Zero );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<Payment>( "Payment", this.Payment ).Set<ClientBalance>( "Balance", this.Balance ).Set<Decimal>( "Diff", this.Diff );
        }
    }
}
