// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.ChartModifiers.TooltipModifier
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Windows;
using System.Windows.Controls;
using Ecng.Xaml.Charting.Utility;
using Ecng.Xaml.Charting.Visuals.RenderableSeries;

namespace Ecng.Xaml.Charting.ChartModifiers
{
    public class TooltipModifier : TooltipModifierBase
    {
        public static readonly DependencyProperty IncludeSeriesProperty = DependencyProperty.RegisterAttached("IncludeSeries", typeof (bool), typeof (TooltipModifier), new PropertyMetadata((object) true));
        public static readonly DependencyProperty TooltipLabelDataContextSelectorProperty = DependencyProperty.Register(nameof (TooltipLabelDataContextSelector), typeof (Func<SeriesInfo, object>), typeof (TooltipModifier), new PropertyMetadata((PropertyChangedCallback) null));
        private const double TooltipXyOffset = 6.0;
        private TemplatableControl _tooltipLabelCache;

        public static bool GetIncludeSeries( DependencyObject obj )
        {
            return ( bool ) obj.GetValue( TooltipModifier.IncludeSeriesProperty );
        }

        public static void SetIncludeSeries( DependencyObject obj, bool value )
        {
            obj.SetValue( TooltipModifier.IncludeSeriesProperty, ( object ) value );
        }

        public Func<SeriesInfo, object> TooltipLabelDataContextSelector
        {
            get
            {
                return ( Func<SeriesInfo, object> ) this.GetValue( TooltipModifier.TooltipLabelDataContextSelectorProperty );
            }
            set
            {
                this.SetValue( TooltipModifier.TooltipLabelDataContextSelectorProperty, ( object ) value );
            }
        }

        public TooltipModifier()
        {
            this.DefaultStyleKey = ( object ) typeof( TooltipModifier );
        }

        protected override void ClearAll()
        {
            this.ClearTooltipOverlay();
        }

        private void ClearTooltipOverlay()
        {
            if ( this._tooltipLabelCache == null || this.ModifierSurface == null )
                return;
            if ( this.ModifierSurface.Children.Contains( ( UIElement ) this._tooltipLabelCache ) )
                this.ModifierSurface.Children.Remove( ( UIElement ) this._tooltipLabelCache );
            this.CurrentPoint = new Point( double.NaN, double.NaN );
        }

        protected override void HandleMasterMouseEvent( Point mousePoint )
        {
            UltrachartDebugLogger.Instance.WriteLine( "TooltipModifier Master MouseMove: {0}, {1}", ( object ) mousePoint.X, ( object ) mousePoint.Y );
            Point hitTestPoint;
            object tooltipDataContext = this.GetTooltipDataContext(mousePoint, out hitTestPoint);
            if ( tooltipDataContext != null && this._tooltipLabelCache != null )
            {
                this._tooltipLabelCache.DataContext = tooltipDataContext;
                this.UpdateTooltipOverlay( mousePoint );
            }
            else
                this.ClearTooltipOverlay();
        }

        protected override void HandleSlaveMouseEvent( Point mousePoint )
        {
        }

        private object GetTooltipDataContext( Point currentPoint, out Point hitTestPoint )
        {
            hitTestPoint = new Point();
            object obj = (object) null;
            foreach ( SeriesInfo seriesInfo in this.GetSeriesInfoAt( currentPoint ) )
            {
                if ( seriesInfo.IsHit && seriesInfo.RenderableSeries.GetIncludeSeries( Modifier.Tooltip ) )
                {
                    hitTestPoint = seriesInfo.XyCoordinate;
                    obj = this.TooltipLabelDataContextSelector != null ? this.TooltipLabelDataContextSelector( seriesInfo ) : ( object ) seriesInfo;
                    break;
                }
            }
            return obj;
        }

        private void UpdateTooltipOverlay( Point mousePoint )
        {
            if ( this._tooltipLabelCache == null )
                return;
            this.PlaceOverlay( ( FrameworkElement ) this._tooltipLabelCache, mousePoint );
            if ( this.ModifierSurface.Children.Contains( ( UIElement ) this._tooltipLabelCache ) )
                return;
            this.ModifierSurface.Children.Add( ( UIElement ) this._tooltipLabelCache );
        }

        private void PlaceOverlay( FrameworkElement overlay, Point mousePoint )
        {
            Rect rect1 = new Rect(0.0, 0.0, this.ModifierSurface.ActualWidth, this.ModifierSurface.ActualHeight);
            double num1 = mousePoint.X + 6.0;
            double num2 = mousePoint.Y + 6.0;
            Rect rect2 = new Rect(num1, num2, overlay.ActualWidth, overlay.ActualHeight);
            if ( rect1.Right < rect2.Right )
                num1 = mousePoint.X - rect2.Width - 6.0;
            if ( rect1.Bottom < rect2.Bottom )
            {
                double num3 = rect2.Bottom - rect1.Bottom;
                double num4 = num2 - num3;
                num2 = num4 < 0.0 ? 0.0 : num4;
            }
            Canvas.SetLeft( ( UIElement ) overlay, num1 );
            Canvas.SetTop( ( UIElement ) overlay, num2 );
        }

        protected override void OnTooltipLabelTemplateChanged()
        {
            this._tooltipLabelCache = this.CreateFromTemplate( this.TooltipLabelTemplate, this.TooltipLabelTemplateSelector, ( object ) this );
        }

        protected override void OnAxisLabelTemplateChanged()
        {
        }
    }
}
