using System;
using System.Collections.Generic;
using System.Windows;
using StockSharp.Xaml.Charting.Common.Extensions;
using StockSharp.Xaml.Charting.Visuals.Axes;
using StockSharp.Xaml.Charting.Visuals.RenderableSeries;

namespace StockSharp.Xaml.Charting.ChartModifiers
{
    public abstract class InspectSeriesModifierBase : ChartModifierBase
    {
        public readonly static DependencyProperty UseInterpolationProperty;

        public readonly static DependencyProperty SourceModeProperty;

        public readonly static DependencyProperty SeriesDataProperty;

        protected Point CurrentPoint = new Point(double.NaN, double.NaN);

        private bool _isMaster;

        public ChartDataObject SeriesData
        {
            get
            {
                return ( ChartDataObject ) base.GetValue( InspectSeriesModifierBase.SeriesDataProperty );
            }
            set
            {
                base.SetValue( InspectSeriesModifierBase.SeriesDataProperty, value );
            }
        }

        public StockSharp.Xaml.Charting.ChartModifiers.SourceMode SourceMode
        {
            get
            {
                return ( StockSharp.Xaml.Charting.ChartModifiers.SourceMode ) base.GetValue( InspectSeriesModifierBase.SourceModeProperty );
            }
            set
            {
                base.SetValue( InspectSeriesModifierBase.SourceModeProperty, value );
            }
        }

        public bool UseInterpolation
        {
            get
            {
                return ( bool ) base.GetValue( InspectSeriesModifierBase.UseInterpolationProperty );
            }
            set
            {
                base.SetValue( InspectSeriesModifierBase.UseInterpolationProperty, value );
            }
        }

        static InspectSeriesModifierBase()
        {
            InspectSeriesModifierBase.UseInterpolationProperty = DependencyProperty.Register( "UseInterpolation", typeof( bool ), typeof( InspectSeriesModifierBase ), new PropertyMetadata( false ) );
            InspectSeriesModifierBase.SourceModeProperty = DependencyProperty.Register( "SourceMode", typeof( StockSharp.Xaml.Charting.ChartModifiers.SourceMode ), typeof( InspectSeriesModifierBase ), new PropertyMetadata( ( object ) StockSharp.Xaml.Charting.ChartModifiers.SourceMode.AllVisibleSeries ) );
            InspectSeriesModifierBase.SeriesDataProperty = DependencyProperty.Register( "SeriesData", typeof( ChartDataObject ), typeof( InspectSeriesModifierBase ), new PropertyMetadata( null ) );
        }

        protected InspectSeriesModifierBase()
        {
            base.SetCurrentValue( InspectSeriesModifierBase.SourceModeProperty, StockSharp.Xaml.Charting.ChartModifiers.SourceMode.AllVisibleSeries );
            base.SetCurrentValue( ChartModifierBase.ExecuteOnProperty, StockSharp.Xaml.Charting.ChartModifiers.ExecuteOn.MouseMove );
        }

        private bool CheckSeriesMode( IRenderableSeries series )
        {

            if ( this.SourceMode == StockSharp.Xaml.Charting.ChartModifiers.SourceMode.AllSeries || series.IsVisible && this.SourceMode == StockSharp.Xaml.Charting.ChartModifiers.SourceMode.AllVisibleSeries || series.IsSelected && this.SourceMode == StockSharp.Xaml.Charting.ChartModifiers.SourceMode.SelectedSeries )
            {
                return true;
            }
            if ( series.IsSelected )
            {
                return false;
            }
            return this.SourceMode == StockSharp.Xaml.Charting.ChartModifiers.SourceMode.UnselectedSeries;
        }

        protected abstract void ClearAll();

        protected virtual IEnumerable<SeriesInfo> GetSeriesInfoAt( Point point )
        {
            return this.GetSeriesInfoAt( ( IRenderableSeries renderSeries ) => renderSeries.HitTest( point, this.UseInterpolation ) );
        }

        protected virtual IEnumerable<SeriesInfo> GetSeriesInfoAt( Point point, double hitTestRadius )
        {
            return this.GetSeriesInfoAt( ( IRenderableSeries renderSeries ) => renderSeries.HitTest( point, hitTestRadius, this.UseInterpolation ) );
        }

        protected IEnumerable<SeriesInfo> GetSeriesInfoAt( Func<IRenderableSeries, HitTestInfo> hitTestMethod )
        {
            InspectSeriesModifierBase inspectSeriesModifierBase = null;
            if ( inspectSeriesModifierBase.ParentSurface != null && !inspectSeriesModifierBase.ParentSurface.RenderableSeries.IsNullOrEmpty<IRenderableSeries>() )
            {
                foreach ( IRenderableSeries renderableSeries in inspectSeriesModifierBase.ParentSurface.RenderableSeries )
                {
                    if ( !inspectSeriesModifierBase.IsSeriesValid( renderableSeries ) )
                    {
                        continue;
                    }
                    HitTestInfo hitTestInfo = hitTestMethod(renderableSeries);
                    if ( !inspectSeriesModifierBase.IsHitPointValid( hitTestInfo ) )
                    {
                        continue;
                    }
                    yield return renderableSeries.GetSeriesInfo( hitTestInfo );
                }
            }
        }

        protected abstract void HandleMasterMouseEvent( Point mousePoint );

        protected void HandleMouseEvent( ModifierMouseArgs e )
        {
            bool flag = false;
            if ( this.IsInteractionEnabled( e ) )

            {
                Point pointRelativeTo = base.GetPointRelativeTo(e.MousePoint, base.ModifierSurface);
                this.CurrentPoint = pointRelativeTo;
                this._isMaster = e.IsMaster;
                flag = this.HandleMouseEvent( this.CurrentPoint );
            }
            e.Handled = flag;
        }

        private bool HandleMouseEvent( Point relativeToModifierSurface )
        {
            bool flag = this.IsEnabledAt(relativeToModifierSurface);
            if ( !flag )
            {
                this.ClearAll();
                return flag;
            }
            if ( this._isMaster )




            {
                this.HandleMasterMouseEvent( relativeToModifierSurface );
                return flag;
            }
            this.HandleSlaveMouseEvent( relativeToModifierSurface );
            return flag;
        }

        protected abstract void HandleSlaveMouseEvent( Point mousePoint );

        protected AxisInfo HitTestAxis( IAxis axis, Point atPoint )
        {
            AxisInfo axisInfo = axis.HitTest(atPoint);
            if ( axisInfo != null )
            {
                axisInfo.IsMasterChartAxis = this._isMaster;
            }
            return axisInfo;
        }

        protected virtual bool IsEnabledAt( Point point )
        {
            bool flag = (!point.X.IsDefined() || !point.Y.IsDefined() || point.X < 0 ? false : point.X <= base.ModifierSurface.ActualWidth);
            if ( this._isMaster )
            {
                flag = flag & ( point.Y < 0 ? false : point.Y <= base.ModifierSurface.ActualHeight );
            }
            return flag;
        }

        protected virtual bool IsHitPointValid( HitTestInfo hitTestInfo )
        {
            if ( hitTestInfo.IsEmpty() )
            {
                return false;
            }
            return hitTestInfo.IsHit;
        }

        private bool IsInteractionEnabled( ModifierMouseArgs e )
        {
            if ( base.ModifierSurface == null || !base.IsEnabled )
            {
                return false;
            }
            return base.MatchesExecuteOn( e.MouseButtons, base.ExecuteOn );
        }

        protected virtual bool IsSeriesValid( IRenderableSeries series )
        {
            if ( series == null || !this.CheckSeriesMode( series ) )
            {
                return false;
            }
            return series.DataSeries != null;
        }

        public override void OnModifierMouseMove( ModifierMouseArgs e )
        {
            base.OnModifierMouseMove( e );
            this.HandleMouseEvent( e );
            e.Handled = false;
        }

        protected override void OnParentSurfaceMouseLeave()
        {
            double num = double.NaN;
            double num1 = num;
            this.CurrentPoint.Y = num;
            this.CurrentPoint.X = num1;
            this.ClearAll();
        }

        public override void OnParentSurfaceRendered( UltrachartRenderedMessage e )
        {
            base.OnParentSurfaceRendered( e );
            if ( base.IsEnabled )
            {
                this.HandleMouseEvent( this.CurrentPoint );
            }
        }
    }
}
