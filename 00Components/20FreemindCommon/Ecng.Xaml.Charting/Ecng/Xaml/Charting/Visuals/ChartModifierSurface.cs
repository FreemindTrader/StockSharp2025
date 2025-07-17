// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.ChartModifierSurface
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Windows.Media;
namespace Ecng.Xaml.Charting
{
    [ContentProperty( "Children" )]
    public class ChartModifierSurface : ContentControl, IChartModifierSurface, IHitTestable
    {
        public new static readonly DependencyProperty ClipToBoundsProperty = DependencyProperty.Register(nameof (ClipToBounds), typeof (bool), typeof (ChartModifierSurface), new PropertyMetadata((object) false, new PropertyChangedCallback(ChartModifierSurface.OnClipToBoundsPropertyChanged)));
        private readonly Canvas _modifierCanvas = new Canvas();
        private readonly ObservableCollection<UIElement> _children;

        public ChartModifierSurface()
        {
            this._children = new ObservableCollection<UIElement>();
            this._children.CollectionChanged += new NotifyCollectionChangedEventHandler( this.ChildrenCollectionChanged );
            this.Content = ( object ) this._modifierCanvas;
            this.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            this.VerticalContentAlignment = VerticalAlignment.Stretch;
        }

        public new bool ClipToBounds
        {
            get
            {
                return ( bool ) this.GetValue( ChartModifierSurface.ClipToBoundsProperty );
            }
            set
            {
                this.SetValue( ChartModifierSurface.ClipToBoundsProperty, ( object ) value );
            }
        }

        public ObservableCollection<UIElement> Children
        {
            get
            {
                return this._children;
            }
        }

        public void Clear()
        {
            this.Children.Clear();
        }

        public Point TranslatePoint( Point point, IHitTestable relativeTo )
        {
            return ElementExtensions.TranslatePoint( this, point, relativeTo );
        }

        public bool IsPointWithinBounds( Point point )
        {
            return HitTestableExtensions.IsPointWithinBounds( this, point );
        }

        public Rect GetBoundsRelativeTo( IHitTestable relativeTo )
        {
            Visual visual = relativeTo as Visual;
            if ( visual == null )
            {
                return Rect.Empty;
            }

            return this.TransformToVisual( visual ).TransformBounds( LayoutInformation.GetLayoutSlot( ( FrameworkElement ) this ) );
        }

        private void ChildrenCollectionChanged( object sender, NotifyCollectionChangedEventArgs e )
        {
            if ( e.Action == NotifyCollectionChangedAction.Reset )
            {
                this._modifierCanvas.Children.Clear();
                this._children.ForEachDo<UIElement>( ( Action<UIElement> ) ( x => this._modifierCanvas.Children.Add( x ) ) );
            }
            if ( e.NewItems != null )
            {
                e.NewItems.Cast<UIElement>().ForEachDo<UIElement>( ( Action<UIElement> ) ( x => this._modifierCanvas.Children.Add( x ) ) );
            }

            if ( e.OldItems == null )
            {
                return;
            }

            e.OldItems.Cast<UIElement>().ForEachDo<UIElement>( ( Action<UIElement> ) ( x => this._modifierCanvas.Children.Remove( x ) ) );
        }

        private static void OnClipToBoundsPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ClipToBoundsHelper.SetClipToBounds( ( DependencyObject ) ( ( ChartModifierSurface ) d )._modifierCanvas, ( bool ) e.NewValue );
        }

        internal Canvas ModifierCanvas
        {
            get
            {
                return this._modifierCanvas;
            }
        }



        [SpecialName]
        double IHitTestable.ActualWidth
        {
            get
            {
                return this.ActualWidth;
            }

        }

        [SpecialName]
        double IHitTestable.ActualHeight
        {
            get
            {
                return this.ActualHeight;
            }
        }


    }
}
