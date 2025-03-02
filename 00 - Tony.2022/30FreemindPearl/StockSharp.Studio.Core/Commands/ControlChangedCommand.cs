// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.ControlChangedCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using System;

namespace StockSharp.Studio.Core.Commands
{
    public class ControlChangedCommand : BaseStudioCommand
    {
        public ControlChangedCommand( IStudioControl control )
        {
            IStudioControl studioControl = control;
            if ( studioControl == null )
                throw new ArgumentNullException( nameof( control ) );
            Control = studioControl;
        }

        public IStudioControl Control { get; }

        public override StudioCommandDirections PossibleDirection
        {
            get
            {
                return StudioCommandDirections.Top;
            }
        }
    }
}
