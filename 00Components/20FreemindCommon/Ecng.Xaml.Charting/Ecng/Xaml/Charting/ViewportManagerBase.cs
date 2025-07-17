// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.ViewportManagerBase
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Windows;
using System.Windows.Threading;
using Ecng.Xaml.Charting.Model.DataSeries;
using Ecng.Xaml.Charting.Utility;
using Ecng.Xaml.Charting.Visuals;
using Ecng.Xaml.Charting.Visuals.Axes;

namespace Ecng.Xaml.Charting
{
    public abstract class ViewportManagerBase : IViewportManager, IUltrachartController, ISuspendable, IInvalidatableElement
    {
        private ISciChartSurface _scs;
        private IServiceContainer _services;

        public IServiceContainer Services
        {
            get
            {
                return this._services;
            }
            set
            {
                this._services = value;
            }
        }

        public bool IsAttached
        {
            get; private set;
        }

        public virtual void AttachUltrachartSurface( ISciChartSurface scs )
        {
            this._scs = scs;
            this._services = this._scs.Services;
            this.IsAttached = true;
        }

        public virtual void DetachUltrachartSurface()
        {
            this._scs = ( ISciChartSurface ) null;
            this._services = ( IServiceContainer ) null;
            this.IsAttached = false;
        }

        public bool IsSuspended
        {
            get
            {
                return UpdateSuspender.GetIsSuspended( ( ISuspendable ) this );
            }
        }

        public IUpdateSuspender SuspendUpdates()
        {
            return ( IUpdateSuspender ) new UpdateSuspender( ( ISuspendable ) this );
        }

        public void ResumeUpdates( IUpdateSuspender suspender )
        {
        }

        public void DecrementSuspend()
        {
        }

        public void InvalidateElement()
        {
            if ( !this.IsAttached )
                return;
            this.SafeInvoke( ( Action ) ( () => this._scs.InvalidateElement() ) );
        }

        private void SafeInvoke( Action action )
        {
            this._services.GetService<IDispatcherFacade>().BeginInvokeIfRequired( action, DispatcherPriority.ApplicationIdle );
        }

        public void ZoomExtents()
        {
            if ( !this.IsAttached )
                return;
            this.SafeInvoke( ( Action ) ( () => this._scs.ZoomExtents() ) );
        }

        public void AnimateZoomExtents( TimeSpan duration )
        {
            if ( !this.IsAttached )
                return;
            this.SafeInvoke( ( Action ) ( () => this._scs.AnimateZoomExtents( duration ) ) );
        }

        public void ZoomExtentsY()
        {
            if ( !this.IsAttached )
                return;
            this.SafeInvoke( ( Action ) ( () => this._scs.ZoomExtentsY() ) );
        }

        public void AnimateZoomExtentsY( TimeSpan duration )
        {
            if ( !this.IsAttached )
                return;
            this.SafeInvoke( ( Action ) ( () => this._scs.AnimateZoomExtentsY( duration ) ) );
        }

        public void ZoomExtentsX()
        {
            if ( !this.IsAttached )
                return;
            this.SafeInvoke( ( Action ) ( () => this._scs.ZoomExtentsX() ) );
        }

        public void AnimateZoomExtentsX( TimeSpan duration )
        {
            if ( !this.IsAttached )
                return;
            this.SafeInvoke( ( Action ) ( () => this._scs.AnimateZoomExtentsX( duration ) ) );
        }

        public virtual IRange CalculateAutoRange( IAxis axis )
        {
            if ( axis.AutoRange == AutoRange.Always || axis.AutoRange == AutoRange.Once )
            {
                IRange maximumRange = axis.GetMaximumRange();
                if ( maximumRange != null && maximumRange.IsDefined )
                    return maximumRange;
            }
            return axis.VisibleRange;
        }

        public IRange CalculateNewXAxisRange( IAxis xAxis )
        {
            if ( !this.IsSuspended )
                return this.OnCalculateNewXRange( xAxis );
            return xAxis.VisibleRange;
        }

        public IRange CalculateNewYAxisRange( IAxis yAxis, RenderPassInfo renderPassInfo )
        {
            if ( !this.IsSuspended )
                return this.OnCalculateNewYRange( yAxis, renderPassInfo );
            return yAxis.VisibleRange;
        }

        protected abstract IRange OnCalculateNewXRange( IAxis xAxis );

        protected abstract IRange OnCalculateNewYRange( IAxis yAxis, RenderPassInfo renderPassInfo );

        public virtual void OnVisibleRangeChanged( IAxis axis )
        {
        }

        public virtual void OnParentSurfaceRendered( ISciChartSurface ultraChartSurface )
        {
        }

        public void InvalidateParentSurface( RangeMode rangeMode )
        {
            if ( !this.IsAttached )
                return;
            IEventAggregator service = this.Services.GetService<IEventAggregator>();
            switch ( rangeMode )
            {
                case RangeMode.None:
                    service.Publish<InvalidateUltrachartMessage>( new InvalidateUltrachartMessage( ( object ) this ) );
                    break;
                case RangeMode.ZoomToFit:
                    service.Publish<ZoomExtentsMessage>( new ZoomExtentsMessage( ( object ) this ) );
                    break;
                case RangeMode.ZoomToFitY:
                    service.Publish<ZoomExtentsMessage>( new ZoomExtentsMessage( ( object ) this, true ) );
                    break;
            }
        }

        protected void OnInvalidateParentSurface( DependencyPropertyChangedEventArgs e )
        {
            if ( !this.IsAttached )
                return;
            this.Services.GetService<IEventAggregator>().Publish<InvalidateUltrachartMessage>( new InvalidateUltrachartMessage( ( object ) this ) );
        }
    }
}
