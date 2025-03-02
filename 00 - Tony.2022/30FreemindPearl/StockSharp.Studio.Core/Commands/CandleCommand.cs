// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.CandleCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using StockSharp.Algo.Candles;
using System;

namespace StockSharp.Studio.Core.Commands
{
    public class CandleCommand : BaseStudioCommand
    {
        public CandleSeries Series { get; }

        public Candle Candle { get; }

        public CandleCommand( CandleSeries series, Candle candle )
        {
            CandleSeries candleSeries = series;
            if ( candleSeries == null )
                throw new ArgumentNullException( nameof( series ) );
            Series = candleSeries;
            Candle candle1 = candle;
            if ( candle1 == null )
                throw new ArgumentNullException( nameof( candle ) );
            Candle = candle1;
        }
    }
}
