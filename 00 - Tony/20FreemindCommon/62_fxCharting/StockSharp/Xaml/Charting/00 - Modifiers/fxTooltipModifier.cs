using DevExpress.Mvvm;
using fx.Algorithm;
using SciChart.Charting.ChartModifiers;
using SciChart.Charting.Common.Extensions;
using SciChart.Charting.Model.ChartData;
using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.Visuals.Axes;
using SciChart.Charting.Visuals.RenderableSeries;
using SciChart.Core.Extensions;
using SciChart.Core.Utility.Mouse;
using SciChart.Data.Model;
using fx.Definitions;
using fx.Charting.HewFibonacci;
using System;
using System.Collections.Generic;
using fx.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using fx.Bars;
using System.Windows.Threading;
using fx.Charting.ATony;

namespace fx.Charting
{
    public class fxTooltipModifier : TooltipModifier
    {
        protected override void HandleMasterMouseEvent( Point mousePoint )
        {
            Point hitTestPoint;

            object tooltipDataContext = this.GetTooltipDataContext( mousePoint, out hitTestPoint );
            
            if ( tooltipDataContext != null && this._tooltipLabelCache != null )
            {
                this._tooltipLabelCache.DataContext = tooltipDataContext;
                this.UpdateTooltipOverlay( mousePoint );
            }
            else
                this.ClearTooltipOverlay();
        }

        private void UpdateTooltipOverlay( Point mousePoint )
        {
            if ( this._tooltipLabelCache == null )
                return;
            this.PlaceOverlay( ( FrameworkElement )this._tooltipLabelCache, mousePoint );
            if ( this.ModifierSurface.Children.Contains( ( UIElement )this._tooltipLabelCache ) )
                return;
            this.ModifierSurface.Children.Add( ( UIElement )this._tooltipLabelCache );
        }


        private void ClearTooltipOverlay()
        {
            if ( this._tooltipLabelCache == null || this.ModifierSurface == null )
                return;
            if ( this.ModifierSurface.Children.Contains( ( UIElement )this._tooltipLabelCache ) )
                this.ModifierSurface.Children.Remove( ( UIElement )this._tooltipLabelCache );
            this.CurrentPoint = new Point( double.NaN, double.NaN );
        }

        private object GetTooltipDataContext( Point currentPoint, out Point hitTestPoint )
        {
            hitTestPoint = new Point();
            object obj = ( object )null;
            
            foreach ( SeriesInfo si in this.GetSeriesInfoAt( currentPoint ) )
            {                
                if ( si.IsHit && TooltipModifier.GetIncludeSeries( ( DependencyObject )si.RenderableSeries ) )
                {
                    hitTestPoint = si.XyCoordinate;
                    obj = this.TooltipLabelDataContextSelector != null ? this.TooltipLabelDataContextSelector( si ) : si;
                    break;
                }
            }
            return obj;
        }
    }        
}
