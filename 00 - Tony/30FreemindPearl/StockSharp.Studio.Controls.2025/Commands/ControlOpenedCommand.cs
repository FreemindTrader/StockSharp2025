// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.Commands.ControlOpenedCommand
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

using StockSharp.Studio.Core.Commands;
using System;

namespace StockSharp.Studio.Controls.Commands
{
    public class ControlOpenedCommand : BaseStudioCommand
    {
        public IStudioControl Control { get; }

        public ControlOpenedCommand( IStudioControl control )
        {
            IStudioControl studioControl = control;
            if ( studioControl == null )
                throw new ArgumentNullException( nameof( control ) );
            this.Control = studioControl;
        }
    }
}
