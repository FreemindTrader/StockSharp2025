// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Utility.Mouse.RenderSynchronizedMouseMove
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Windows.Input;
using System.Windows.Media;

namespace StockSharp.Xaml.Charting.Utility.Mouse
{
    internal class RenderSynchronizedMouseMove : IDisposable
    {
        private bool _isWaiting;
        private readonly IPublishMouseEvents _publisher;

        internal event MouseEventHandler SynchronizedMouseMove;

        internal RenderSynchronizedMouseMove( IPublishMouseEvents publisher )
        {
            this._publisher = publisher;
            publisher.MouseMove += new MouseEventHandler( this.PublisherMouseMove );
        }

        public void Dispose()
        {
            this._publisher.MouseMove -= new MouseEventHandler( this.PublisherMouseMove );
            // ISSUE: reference to a compiler-generated field
            this.SynchronizedMouseMove = ( MouseEventHandler ) null;
        }

        private void PublisherMouseMove( object sender, MouseEventArgs e )
        {
            if ( this._isWaiting )
                return;
            this.OnSynchronizedMouseMove( e );
            this._isWaiting = true;
            CompositionTarget.Rendering += new EventHandler( this.CompositionTargetRendering );
        }

        private void CompositionTargetRendering( object sender, EventArgs e )
        {
            this._isWaiting = false;
            CompositionTarget.Rendering -= new EventHandler( this.CompositionTargetRendering );
        }

        internal void OnSynchronizedMouseMove( MouseEventArgs args )
        {
            // ISSUE: reference to a compiler-generated field
            MouseEventHandler synchronizedMouseMove = this.SynchronizedMouseMove;
            if ( synchronizedMouseMove == null )
                return;
            synchronizedMouseMove( ( object ) this._publisher, args );
        }
    }
}
