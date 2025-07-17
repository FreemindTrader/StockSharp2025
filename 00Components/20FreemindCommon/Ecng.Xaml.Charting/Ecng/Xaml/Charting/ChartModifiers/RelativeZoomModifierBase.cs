// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.RelativeZoomModifierBase
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Windows;
namespace Ecng.Xaml.Charting
{
    public abstract class RelativeZoomModifierBase : ChartModifierBase
    {
        public static readonly DependencyProperty XyDirectionProperty = DependencyProperty.Register(nameof (XyDirection), typeof (XyDirection), typeof (RelativeZoomModifierBase), new PropertyMetadata((object) XyDirection.XYDirection));
        private double _growFactor;

        public XyDirection XyDirection
        {
            get
            {
                return ( XyDirection ) this.GetValue( RelativeZoomModifierBase.XyDirectionProperty );
            }
            set
            {
                this.SetValue( RelativeZoomModifierBase.XyDirectionProperty, ( object ) value );
            }
        }

        public double GrowFactor
        {
            get
            {
                return this._growFactor;
            }
            set
            {
                Guard.Assert( ( IComparable ) value, nameof( GrowFactor ) ).IsGreaterThan( ( IComparable ) 0.0 );
                this._growFactor = value;
            }
        }

        protected virtual void PerformZoom( Point mousePoint, double xValue, double yValue )
        {
            if ( this.XyDirection == XyDirection.YDirection || this.XyDirection == XyDirection.XYDirection )
                this.PerformZoomBy( this.GrowFactor * yValue, mousePoint, this.YAxes, "Growing YRange: {0}" );
            if ( this.XyDirection != XyDirection.XDirection && this.XyDirection != XyDirection.XYDirection )
                return;
            this.PerformZoomBy( this.GrowFactor * xValue, mousePoint, this.XAxes, "Growing XRange: {0}" );
        }

        private void PerformZoomBy( double fraction, Point mousePoint, IEnumerable<IAxis> axisCollection, string logMessage )
        {
            foreach ( IAxis axis in axisCollection )
                this.GrowBy( mousePoint, axis, fraction );
            UltrachartDebugLogger.Instance.WriteLine( logMessage, ( object ) fraction );
        }

        protected void GrowBy( Point mousePoint, IAxis axis, double fraction )
        {
            double axisDimension = this.GetAxisDimension(axis);
            double num = axis.IsHorizontalAxis ? mousePoint.X : axisDimension - mousePoint.Y;
            double minFraction = num / axisDimension * fraction;
            double maxFraction = (1.0 - num / axisDimension) * fraction;
            bool flag = axis.IsHorizontalAxis && !axis.IsXAxis || !axis.IsHorizontalAxis && axis.IsXAxis;
            if ( flag && !axis.FlipCoordinates || !flag && axis.FlipCoordinates )
                NumberUtil.Swap( ref minFraction, ref maxFraction );
            axis.ZoomBy( minFraction, maxFraction );
        }

        private double GetAxisDimension( IAxis axis )
        {
            double num = axis.IsHorizontalAxis ? axis.Width : axis.Height;
            UltrachartSurface parentSurface = axis.ParentSurface as UltrachartSurface;
            if ( axis.Visibility == Visibility.Collapsed && parentSurface != null )
                num = axis.IsHorizontalAxis ? parentSurface.ActualWidth : parentSurface.ActualHeight;
            return num;
        }
    }
}
