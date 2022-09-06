// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.RequestBindSource
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using System;

namespace StockSharp.Studio.Core.Commands
{
    public class RequestBindSource : BaseStudioCommand
    {
        public IStudioControl Control { get; }

        public RequestBindSource( IStudioControl control )
        {
            IStudioControl studioControl = control;
            if ( studioControl == null )
                throw new ArgumentNullException( nameof( control ) );
            this.Control = studioControl;
        }
    }
}
