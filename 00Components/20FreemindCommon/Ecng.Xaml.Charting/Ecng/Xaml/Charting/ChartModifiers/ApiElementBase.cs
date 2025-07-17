// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.ApiElementBase
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
namespace fx.Xaml.Charting
{
    public abstract class ApiElementBase : ContentControl, INotifyPropertyChanged
    {
        private ISciChartSurface _parentSurface;

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual ISciChartSurface ParentSurface
        {
            get
            {
                return this._parentSurface;
            }
            set
            {
                this._parentSurface = value;
                this.OnPropertyChanged( nameof( ParentSurface ) );
            }
        }

        public IEnumerable<IAxis> XAxes
        {
            get
            {
                if ( this.ParentSurface == null || this.ParentSurface.XAxes == null )
                {
                    return Enumerable.Empty<IAxis>();
                }

                return ( IEnumerable<IAxis> ) this.ParentSurface.XAxes;
            }
        }

        public IEnumerable<IAxis> YAxes
        {
            get
            {
                if ( this.ParentSurface == null || this.ParentSurface.YAxes == null )
                {
                    return Enumerable.Empty<IAxis>();
                }

                return ( IEnumerable<IAxis> ) this.ParentSurface.YAxes;
            }
        }

        public virtual IAxis YAxis
        {
            get
            {
                ISciChartSurface parentSurface = this.ParentSurface;
                IAxis axis1 = (IAxis) null;
                if ( parentSurface != null )
                {
                    axis1 = parentSurface.YAxis;
                    if ( axis1 == null && !parentSurface.YAxes.IsNullOrEmpty<IAxis>() )
                    {
                        axis1 = parentSurface.YAxes.FirstOrDefault<IAxis>( ( Func<IAxis, bool> ) ( axis => axis.IsPrimaryAxis ) );
                    }
                }
                return axis1;
            }
        }

        public virtual IAxis XAxis
        {
            get
            {
                ISciChartSurface parentSurface = this.ParentSurface;
                IAxis axis1 = (IAxis) null;
                if ( parentSurface != null )
                {
                    axis1 = parentSurface.XAxis;
                    if ( axis1 == null && !parentSurface.XAxes.IsNullOrEmpty<IAxis>() )
                    {
                        axis1 = parentSurface.XAxes.FirstOrDefault<IAxis>( ( Func<IAxis, bool> ) ( axis => axis.IsPrimaryAxis ) );
                    }
                }
                return axis1;
            }
        }

        public virtual IServiceContainer Services
        {
            get; set;
        }

        public IChartModifierSurface ModifierSurface
        {
            get
            {
                if ( this.ParentSurface == null )
                {
                    return ( IChartModifierSurface ) null;
                }

                return this.ParentSurface.ModifierSurface;
            }
        }

        public virtual bool IsAttached
        {
            get; set;
        }

        protected IMainGrid RootGrid
        {
            get
            {
                if ( this.ParentSurface == null )
                {
                    return ( IMainGrid ) null;
                }

                return this.ParentSurface.RootGrid;
            }
        }

        public abstract void OnAttached();

        public abstract void OnDetached();

        public IAxis GetYAxis( string axisName )
        {
            if ( this.ParentSurface == null || this.ParentSurface.YAxes == null )
            {
                return ( IAxis ) null;
            }

            return this.ParentSurface.YAxes.GetAxisById( axisName, false );
        }

        public IAxis GetXAxis( string axisName )
        {
            if ( this.ParentSurface == null || this.ParentSurface.XAxes == null )
            {
                return ( IAxis ) null;
            }

            return this.ParentSurface.XAxes.GetAxisById( axisName, false );
        }

        protected virtual void OnInvalidateParentSurface()
        {
            if ( this.Services == null )
            {
                return;
            }

            this.Services.GetService<IEventAggregator>().Publish<InvalidateUltrachartMessage>( new InvalidateUltrachartMessage( ( object ) this ) );
        }

        protected T GetAndAssertTemplateChild<T>( string childName ) where T : class
        {
            T templateChild = this.GetTemplateChild(childName) as T;
            if ( ( object ) templateChild == null )
            {
                throw new InvalidOperationException( string.Format( "Unable to Apply the Control Template. {0} is missing or of the wrong type", ( object ) childName ) );
            }

            return templateChild;
        }

        protected void OnPropertyChanged( string propertyName )
        {
            // ISSUE: reference to a compiler-generated field
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ( propertyChanged == null )
            {
                return;
            }

            propertyChanged( ( object ) this, new PropertyChangedEventArgs( propertyName ) );
        }
    }
}
