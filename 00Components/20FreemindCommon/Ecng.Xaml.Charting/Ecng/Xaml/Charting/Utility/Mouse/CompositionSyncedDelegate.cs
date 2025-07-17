// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.CompositionSyncedDelegate
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Windows.Media;

namespace fx.Xaml.Charting
{
    internal class CompositionSyncedDelegate
    {
        private readonly Action _operation;

        public CompositionSyncedDelegate( Action operation )
        {
            Guard.NotNull( ( object ) operation, nameof( operation ) );
            this._operation = operation;
            CompositionTarget.Rendering += new EventHandler( this.CompositionTarget_Rendering );
        }

        private void CompositionTarget_Rendering( object sender, EventArgs e )
        {
            CompositionTarget.Rendering -= new EventHandler( this.CompositionTarget_Rendering );
            this._operation();
        }
    }
}
