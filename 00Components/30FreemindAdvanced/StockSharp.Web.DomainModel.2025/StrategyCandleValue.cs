// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyCandleValue
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;
using StockSharp.Messages;
using System;

namespace StockSharp.Web.DomainModel
{
    public class StrategyCandleValue : IPersistable
    {
        public int Index { get; set; }

        public Decimal Open { get; set; }

        public Decimal High { get; set; }

        public Decimal Low { get; set; }

        public Decimal Close { get; set; }

        public Decimal Volume { get; set; }

        public CandlePriceLevel [ ] Levels { get; set; }

        public int? Color { get; set; }

        public virtual void Load( SettingsStorage storage )
        {
            this.Index = ( int ) storage.GetValue<int>( "Index", 0 );
            this.Open = ( Decimal ) storage.GetValue<Decimal>( "Open", Decimal.Zero );
            this.High = ( Decimal ) storage.GetValue<Decimal>( "High", Decimal.Zero );
            this.Low = ( Decimal ) storage.GetValue<Decimal>( "Low", Decimal.Zero );
            this.Close = ( Decimal ) storage.GetValue<Decimal>( "Close", Decimal.Zero );
            this.Volume = ( Decimal ) storage.GetValue<Decimal>( "Volume", Decimal.Zero );
            this.Levels = ( CandlePriceLevel [ ] ) storage.GetValue<CandlePriceLevel [ ]>( "Levels", null );
            this.Color = ( int? ) storage.GetValue<int?>( "Color", new int?() );
        }

        public virtual void Save( SettingsStorage storage )
        {
            storage.Set<int>( "Index", this.Index ).Set<Decimal>( "Open", this.Open ).Set<Decimal>( "High", this.High ).Set<Decimal>( "Low", this.Low ).Set<Decimal>( "Close", this.Close ).Set<Decimal>( "Volume", this.Volume ).Set<CandlePriceLevel [ ]>( "Levels", this.Levels ).Set<int?>( "Color", this.Color );
        }
    }
}
