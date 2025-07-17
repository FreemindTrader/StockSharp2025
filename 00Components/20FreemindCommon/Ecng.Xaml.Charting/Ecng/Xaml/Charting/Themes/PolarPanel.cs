// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Themes.PolarPanel
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\StockSharp.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using StockSharp.Xaml.Charting.Common.Extensions;

namespace StockSharp.Xaml.Charting.Themes
{
    public class PolarPanel : Panel
    {
        public static readonly DependencyProperty StretchToSizeProperty               = DependencyProperty.Register(nameof (StretchToSize), typeof (bool), typeof (PolarPanel), new PropertyMetadata((object) false, new PropertyChangedCallback(PolarPanel.InvalidateMeasure)));
        public static readonly DependencyProperty IsReversedOrderProperty             = DependencyProperty.Register(nameof (IsReversedOrder), typeof (bool), typeof (PolarPanel), new PropertyMetadata((object) false, new PropertyChangedCallback(PolarPanel.InvalidateMeasure)));
        public static readonly DependencyProperty MinimimalItemSizeProperty           = DependencyProperty.Register(nameof (MinimimalItemSize), typeof (double), typeof (PolarPanel), new PropertyMetadata((object) 0.0));
        public static readonly DependencyProperty ShouldCopyThicknessToParentProperty = DependencyProperty.RegisterAttached("ShouldCopyThicknessToParent", typeof (bool), typeof (PolarPanel), new PropertyMetadata((object) false));
        public static readonly DependencyProperty ThicknessProperty                   = DependencyProperty.RegisterAttached("Thickness", typeof (double), typeof (PolarPanel), new PropertyMetadata((object) 0.0, new PropertyChangedCallback(PolarPanel.OnThicknessPropertyChanged)));

        private const double MinPolarAreaSize = 50.0;

        private Size      _desiredSize;
        private UIElement _itemsParent;
        private bool      _isLoaded;

        public PolarPanel()
        {
            Loaded += ( RoutedEventHandler ) ( ( sender, args ) =>
                                                {
                                                    UpdateItemsParent();
                                                    _isLoaded = true;
                                                } );

            Unloaded += ( RoutedEventHandler ) ( ( sender, args ) => _itemsParent = ( UIElement ) null );
        }

        private void UpdateItemsParent()
        {
            if ( IsItemsHost )
            {
                _itemsParent = ( UIElement ) this.FindVisualParent<ItemsControl>();
            }
            else
            {
                _itemsParent = ( UIElement ) this;
            }

        }

        public bool StretchToSize
        {
            get
            {
                return ( bool ) GetValue( PolarPanel.StretchToSizeProperty );
            }
            set
            {
                SetValue( PolarPanel.StretchToSizeProperty, ( object ) value );
            }
        }

        public bool IsReversedOrder
        {
            get
            {
                return ( bool ) GetValue( PolarPanel.IsReversedOrderProperty );
            }
            set
            {
                SetValue( PolarPanel.IsReversedOrderProperty, ( object ) value );
            }
        }

        public double MinimimalItemSize
        {
            get
            {
                return ( double ) GetValue( PolarPanel.MinimimalItemSizeProperty );
            }
            set
            {
                SetValue( PolarPanel.MinimimalItemSizeProperty, ( object ) value );
            }
        }

        protected override Size MeasureOverride( Size availableSize )
        {
            bool flag             = !StretchToSize;
            _desiredSize = base.MeasureOverride( availableSize );
            UIElement[] array     = GetElements().ToArray<UIElement>();
            double panelThickness = ((IEnumerable<UIElement>) array).Select<UIElement, double>(new Func<UIElement, double>(PolarPanel.GetThickness)).Sum();
            Size centerSize       = GetCenterSize(availableSize.Width, availableSize.Height, panelThickness);

            foreach ( UIElement element in array )
            {
                double thickness = PolarPanel.GetThickness(element);
                centerSize.Width += 2.0 * thickness;
                centerSize.Height += 2.0 * thickness;
                if ( flag )
                    element.Measure( _desiredSize );
            }

            if ( centerSize.Width.IsRealNumber() && centerSize.Height.IsRealNumber() )
            {
                _desiredSize.Width = Math.Max( centerSize.Width, _desiredSize.Width );
                _desiredSize.Height = Math.Max( centerSize.Height, _desiredSize.Height );
            }

            if ( !flag )
            {
                foreach ( UIElement uiElement in array )
                    uiElement.Measure( _desiredSize );
            }
            return _desiredSize;
        }

        private IEnumerable<UIElement> GetElements()
        {
            IEnumerable<UIElement> source = Children.OfType<UIElement>().Where<UIElement>((Func<UIElement, bool>) (x => x.IsVisible()));
            if ( !IsReversedOrder )
                return source;
            return source.Reverse<UIElement>();
        }

        private Size GetCenterSize( double width, double height, double panelThickness )
        {
            if ( !StretchToSize )
                width = height = Math.Min( width, height );

            width -= panelThickness * 2.0;
            height -= panelThickness * 2.0;

            if ( !StretchToSize )
            {
                width = Math.Max( width, 50.0 );
                height = Math.Max( height, 50.0 );
            }
            return new Size( width, height );
        }

        protected override Size ArrangeOverride( Size finalSize )
        {
            UIElement[] array     = GetElements().ToArray<UIElement>();
            double panelThickness = ((IEnumerable<UIElement>) array).Select<UIElement, double>(new Func<UIElement, double>(PolarPanel.GetThickness)).Sum();
            Size centerSize       = GetCenterSize(finalSize.Width, finalSize.Height, panelThickness);
            Point point           = new Point(finalSize.Width / 2.0, finalSize.Height / 2.0);

            foreach ( UIElement element in array )
            {
                double thickness = PolarPanel.GetThickness(element);
                centerSize.Width += 2.0 * thickness;
                centerSize.Height += 2.0 * thickness;
                element.Arrange( new Rect( new Point( point.X - centerSize.Width / 2.0, point.Y - centerSize.Height / 2.0 ), centerSize ) );
            }

            TryUpdateItemsParentThickness( panelThickness );
            return finalSize;
        }

        private void TryUpdateItemsParentThickness( double panelThickness )
        {
            if ( !_isLoaded )
                UpdateItemsParent();
            if ( _itemsParent == null )
                return;
            PolarPanel.SetThickness( _itemsParent, panelThickness );
        }

        public static void SetThickness( UIElement element, double value )
        {
            element.SetValue( PolarPanel.ThicknessProperty, ( object ) value );
        }

        public static double GetThickness( UIElement element )
        {
            return ( double ) element.GetValue( PolarPanel.ThicknessProperty );
        }

        public static void SetShouldCopyThicknessToParent( UIElement element, bool value )
        {
            element.SetValue( PolarPanel.ShouldCopyThicknessToParentProperty, ( object ) value );
        }

        public static bool GetShouldCopyThicknessToParent( UIElement element )
        {
            return ( bool ) element.GetValue( PolarPanel.ShouldCopyThicknessToParentProperty );
        }

        private static void InvalidateMeasure( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( d as PolarPanel )?.InvalidateMeasure();
        }

        private static void OnThicknessPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            UIElement element = d as UIElement;
            if ( element == null )
                return;
            ( VisualTreeHelper.GetParent( ( DependencyObject ) element ) as PolarPanel )?.InvalidateMeasure();
            PolarPanel.TrySetThicknessOnParent( element, ( double ) e.NewValue );
        }

        private static void TrySetThicknessOnParent( UIElement element, double value )
        {
            if ( !PolarPanel.GetShouldCopyThicknessToParent( element ) )
                return;
            ( VisualTreeHelper.GetParent( ( DependencyObject ) element ) as UIElement )?.SetValue( PolarPanel.ThicknessProperty, ( object ) value );
        }
    }
}
