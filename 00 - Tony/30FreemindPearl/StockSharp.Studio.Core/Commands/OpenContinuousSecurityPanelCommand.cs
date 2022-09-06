// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.OpenContinuousSecurityPanelCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using StockSharp.Algo;
using System;

namespace StockSharp.Studio.Core.Commands
{
    public class OpenContinuousSecurityPanelCommand : BaseStudioCommand
    {
        public ContinuousSecurity Security { get; }

        public OpenContinuousSecurityPanelCommand( ContinuousSecurity security )
        {
            ContinuousSecurity continuousSecurity = security;
            if ( continuousSecurity == null )
                throw new ArgumentNullException( nameof( security ) );
            this.Security = continuousSecurity;
        }
    }
}
