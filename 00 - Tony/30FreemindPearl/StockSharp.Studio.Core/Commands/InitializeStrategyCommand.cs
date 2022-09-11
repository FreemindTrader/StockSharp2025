// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.InitializeStrategyCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using StockSharp.Algo.Strategies;
using System;

namespace StockSharp.Studio.Core.Commands
{
    public class InitializeStrategyCommand : BaseStudioCommand
    {
        public InitializeStrategyCommand( Strategy strategy, DateTime from, DateTime to )
        {
            Strategy = strategy;
            FromDate = from;
            ToDate = to;
        }

        public Strategy Strategy { get; }

        public DateTime FromDate { get; }

        public DateTime ToDate { get; }
    }
}
