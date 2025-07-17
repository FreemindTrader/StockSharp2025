// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Themes.PolarAxisPanel
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System.Windows;
using System.Windows.Controls;
namespace fx.Xaml.Charting
{
    public class PolarAxisPanel : AxisPanel
    {
        private PolarPanel _polarPanel;
        private double _radius;
        private TickCoordinates _lastDrawnTickCoordinates;
        private float _lastDrawnOffset;
        private PolarCartesianTransformationHelper _transformationHelper;

        protected override Size MeasureOverride( Size availableSize )
        {
            this.AddTickLabels( this.AddLabels );
            this._polarPanel = this.Children[ 0 ] as PolarPanel;
            this._polarPanel.Measure( availableSize );
            foreach ( UIElement child in this._polarPanel.Children )
            {
                Image image = child as Image;
                if ( image != null )
                {
                    this._axisImage = image;
                    this._axisImage.SizeChanged -= new SizeChangedEventHandler( this.OnAxisImageSizeChanged );
                    this._axisImage.SizeChanged += new SizeChangedEventHandler( this.OnAxisImageSizeChanged );
                }
                Grid grid = child as Grid;
                if ( grid != null )
                    this._labelsContainer = grid;
            }
            return this._polarPanel.DesiredSize;
        }

        private void OnAxisImageSizeChanged( object sender, SizeChangedEventArgs sizeChangedEventArgs )
        {
            this.DrawTicks( this._lastDrawnTickCoordinates, this._lastDrawnOffset );
        }

        protected override Size ArrangeOverride( Size finalSize )
        {
            this._polarPanel.Arrange( new Rect( new Point( 0.0, 0.0 ), finalSize ) );
            return finalSize;
        }

        public override void DrawTicks( TickCoordinates tickCoords, float offset )
        {
            base.DrawTicks( tickCoords, offset );
            this._lastDrawnTickCoordinates = tickCoords;
            this._lastDrawnOffset = offset;
        }

        protected override void DrawTicks( IRenderContext2D renderContext, Style tickStyle, double tickSize, float[ ] tickCoords, float offset )
        {
            this.LineToStyle.Style = tickStyle;
            ThemeManager.SetTheme( ( DependencyObject ) this.LineToStyle, ThemeManager.GetTheme( ( DependencyObject ) this ) );
            using ( IPen2D styledPen = renderContext.GetStyledPen( this.LineToStyle, true ) )
            {
                foreach ( float tickCoord in tickCoords )
                    this.DrawTick( renderContext, styledPen, tickCoord, tickSize );
            }
        }

        private void DrawTick( IRenderContext2D renderContext, IPen2D tickPen, float coord, double tickSize )
        {
            double r = this._radius - this.MajorTickSize;
            Point cartesian1 = this._transformationHelper.ToCartesian((double) coord, r);
            Point cartesian2 = this._transformationHelper.ToCartesian((double) coord, r + tickSize);
            renderContext.DrawLine( tickPen, cartesian1, cartesian2 );
        }

        protected override Size GetRenderContextSize()
        {
            Size renderSize = this.AxisImage.RenderSize;
            this._transformationHelper = new PolarCartesianTransformationHelper( renderSize.Width, renderSize.Height );
            this._radius = PolarUtil.CalculateViewportRadius( renderSize );
            return renderSize;
        }
    }
}
