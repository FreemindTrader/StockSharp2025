﻿// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.MarketDepthCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using StockSharp.Algo;
using StockSharp.BusinessEntities;
using System;

namespace StockSharp.Studio.Core.Commands
{
    public class MarketDepthCommand : EntityCommand<MarketDepth>
    {
        public Guid? SourceId { get; set; }

        public MarketDepthCommand( Subscription subscription, MarketDepth depth )
          : base( subscription, depth )
        {
        }
    }
}
