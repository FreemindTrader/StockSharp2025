// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.GridLinesPanel
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace fx.Xaml.Charting
{
    public class GridLinesPanel : ContentControl, IGridLinesPanel
    {
        private IEventAggregator _eventAggregator;

        public GridLinesPanel()
        {
            this.DefaultStyleKey = ( object ) typeof( GridLinesPanel );
            this.SizeChanged += new SizeChangedEventHandler( this.GridLinesPanelSizeChanged );
        }

        public IEventAggregator EventAggregator
        {
            set
            {
                this._eventAggregator = value;
            }
        }

        public void Clear( XyDirection xyDirection )
        {
            throw new InvalidOperationException( "GridLinesPanel no longer draws gridlines. These are now added to the RenderSurfaceBase instance instead for performance. " );
        }

        public void AddLine( XyDirection xyDirection, Line line )
        {
            throw new InvalidOperationException( "GridLinesPanel no longer draws gridlines. These are now added to the RenderSurfaceBase instance instead for performance. " );
        }

        int IGridLinesPanel.Width
        {
            get
            {
                return ( int ) this.ActualWidth;
            }
        }

        int IGridLinesPanel.Height
        {
            get
            {
                return ( int ) this.ActualHeight;
            }
        }

        public Line GenerateElement( int lineId, XyDirection xyDirection, Style lineStyle )
        {
            throw new InvalidOperationException( "GridLinesPanel no longer draws gridlines. These are now added to the RenderSurfaceBase instance instead for performance. " );
        }

        public void RemoveElementsAfter( XyDirection xyDirection, int index )
        {
            throw new InvalidOperationException( "GridLinesPanel no longer draws gridlines. These are now added to the RenderSurfaceBase instance instead for performance. " );
        }

        private void GridLinesPanelSizeChanged( object sender, SizeChangedEventArgs e )
        {
        }

        [SpecialName]
        Thickness IGridLinesPanel.BorderThickness
        {
            get
            {
                return this.BorderThickness;
            }
        }
    }
}
