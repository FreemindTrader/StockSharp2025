// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Annotations.AdornerBase
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Windows;
using System.Windows.Controls;
namespace Ecng.Xaml.Charting
{
    internal abstract class AdornerBase : FrameworkElement, IAnnotationAdorner, IReceiveMouseEvents
    {
        private Canvas _parentCanvas;
        private IAnnotation _adornedElement;
        private IServiceContainer _services;

        protected AdornerBase( IAnnotation adornedElement )
        {
            this._adornedElement = adornedElement;
            Panel.SetZIndex( ( UIElement ) this, 100 );
        }

        public IServiceContainer Services
        {
            get
            {
                return this._services;
            }
            set
            {
                if ( this._services != null )
                    this._services.GetService<IMouseManager>().Unsubscribe( ( IReceiveMouseEvents ) this );
                this._services = value;
                if ( this._services == null )
                    return;
                this._services.GetService<IMouseManager>().Subscribe( ( IPublishMouseEvents ) this._adornedElement, ( IReceiveMouseEvents ) this );
            }
        }

        public Canvas ParentCanvas
        {
            get
            {
                return this._parentCanvas;
            }
            set
            {
                if ( this._parentCanvas != null )
                    this.OnDetached();
                this._parentCanvas = value;
                if ( this._parentCanvas == null )
                    return;
                this.OnAttached();
            }
        }

        public string MouseEventGroup
        {
            get; set;
        }

        public IAnnotation AdornedAnnotation
        {
            get
            {
                return this._adornedElement;
            }
        }

        public bool CanReceiveMouseEvents()
        {
            if ( this.IsEnabled )
                return this.ParentCanvas != null;
            return false;
        }

        public virtual void OnAttached()
        {
            this._parentCanvas.Children.Add( ( UIElement ) this );
            this.Initialize();
        }

        public virtual void OnDetached()
        {
            this.Clear();
            this.ParentCanvas.Children.Remove( ( UIElement ) this );
            this.Services = ( IServiceContainer ) null;
        }

        public Point GetPointRelativeToRoot( Point point )
        {
            UIElement adornedAnnotation = this.AdornedAnnotation as UIElement;
            if ( adornedAnnotation == null )
                return point;
            UIElement rootGrid = this.AdornedAnnotation.ParentSurface.RootGrid as UIElement;
            return adornedAnnotation.TranslatePoint( point, rootGrid );
        }

        public virtual void OnModifierDoubleClick( ModifierMouseArgs e )
        {
        }

        public virtual void OnModifierMouseDown( ModifierMouseArgs e )
        {
        }

        public virtual void OnModifierMouseMove( ModifierMouseArgs e )
        {
        }

        public virtual void OnModifierMouseUp( ModifierMouseArgs e )
        {
        }

        public virtual void OnModifierMouseWheel( ModifierMouseArgs e )
        {
        }

        public void OnModifierTouchDown( ModifierTouchManipulationArgs e )
        {
            throw new NotImplementedException();
        }

        public void OnModifierTouchMove( ModifierTouchManipulationArgs e )
        {
            throw new NotImplementedException();
        }

        public void OnModifierTouchUp( ModifierTouchManipulationArgs e )
        {
            throw new NotImplementedException();
        }

        public void OnMasterMouseLeave( ModifierMouseArgs e )
        {
        }

        public new bool IsEnabled
        {
            get
            {
                return this.AdornedAnnotation.IsSelected;
            }
            set
            {
                this.AdornedAnnotation.IsSelected = value;
            }
        }

        public abstract void Initialize();

        public abstract void UpdatePositions();

        public abstract void Clear();
    }
}
