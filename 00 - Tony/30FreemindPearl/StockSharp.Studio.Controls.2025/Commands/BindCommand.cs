// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.Commands.BindCommand
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

using StockSharp.Studio.Core.Commands;
using System;

namespace StockSharp.Studio.Controls.Commands
{
    public class BindCommand : BaseStudioCommand
    {
        public IStrategyBinder Binder { get; }

        public IStudioControl Control { get; }

        public bool IsInteractive { get; set; }

        public BindCommand( IStrategyBinder binder, IStudioControl control )
        {
            IStrategyBinder strategyBinder = binder;
            if ( strategyBinder == null )
                throw new ArgumentNullException( nameof( binder ) );
            this.Binder = strategyBinder;
            this.Control = control;
        }

        public bool CheckControl( IStudioControl control )
        {
            if ( this.Control != null )
                return this.Control == control;
            return true;
        }
    }
}
