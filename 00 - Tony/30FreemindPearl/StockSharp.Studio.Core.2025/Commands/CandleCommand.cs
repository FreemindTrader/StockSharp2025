// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.CandleCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 287A45E4-79AD-43B4-B98B-3CD330C45DE5
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Core.dll

using StockSharp.Algo.Candles;
using StockSharp.Messages;
using System;

namespace StockSharp.Studio.Core.Commands
{
    public class CandleCommand : BaseStudioCommand
    {
        public CandleCommand( CandleSeries series, ICandleMessage candle )
        {
            CandleSeries candleSeries = series;
            if ( candleSeries == null )
                throw new ArgumentNullException( nameof( series ) );
            this.Series = candleSeries;
            ICandleMessage candleMessage = candle;
            if ( candleMessage == null )
                throw new ArgumentNullException( nameof( candle ) );
            this.Candle = candleMessage;           
        }

        public CandleSeries Series { get; }

        public ICandleMessage Candle { get; }
    }
}
