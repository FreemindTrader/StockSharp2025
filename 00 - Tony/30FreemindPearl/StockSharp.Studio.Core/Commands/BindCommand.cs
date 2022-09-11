// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.BindCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using StockSharp.Algo.Strategies;
using System;

namespace StockSharp.Studio.Core.Commands
{
    public class BindCommand : BaseStudioCommand
    {
        public Strategy Source { get; }

        public IStudioControl Control { get; }

        public bool IsInteractive { get; set; }

        public BindCommand( Strategy source, IStudioControl control )
        {
            Strategy strategy = source;
            if ( strategy == null )
                throw new ArgumentNullException( nameof( source ) );
            Source = strategy;
            Control = control;
        }

        public bool CheckControl( IStudioControl control )
        {
            if ( Control != null )
                return Control == control;
            return true;
        }
    }
}
