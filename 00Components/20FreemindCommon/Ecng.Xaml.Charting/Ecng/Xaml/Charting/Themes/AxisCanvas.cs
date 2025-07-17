// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Themes.AxisCanvas
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
namespace Ecng.Xaml.Charting
{
    public class AxisCanvas : Panel, ISuspendable
    {
        public new static readonly DependencyProperty ClipToBoundsProperty    = DependencyProperty.Register(nameof (ClipToBounds), typeof (bool), typeof (AxisCanvas), new PropertyMetadata((object) false, new PropertyChangedCallback(AxisCanvas.OnClipToBoundsChanged)));
        public static readonly DependencyProperty SizeWidthToContentProperty  = DependencyProperty.Register(nameof (SizeWidthToContent), typeof (bool), typeof (AxisCanvas), new PropertyMetadata((object) false, new PropertyChangedCallback(AxisCanvas.OnRenderablePropertyChanged)));
        public static readonly DependencyProperty SizeHeightToContentProperty = DependencyProperty.Register(nameof (SizeHeightToContent), typeof (bool), typeof (AxisCanvas), new PropertyMetadata((object) false, new PropertyChangedCallback(AxisCanvas.OnRenderablePropertyChanged)));
        public static readonly DependencyProperty LeftProperty                = DependencyProperty.RegisterAttached("Left", typeof (double), typeof (AxisCanvas), new PropertyMetadata((object) double.NaN, new PropertyChangedCallback(AxisCanvas.OnRenderablePropertyChanged)));
        public static readonly DependencyProperty RightProperty               = DependencyProperty.RegisterAttached("Right", typeof (double), typeof (AxisCanvas), new PropertyMetadata((object) double.NaN, new PropertyChangedCallback(AxisCanvas.OnRenderablePropertyChanged)));
        public static readonly DependencyProperty TopProperty                 = DependencyProperty.RegisterAttached("Top", typeof (double), typeof (AxisCanvas), new PropertyMetadata((object) double.NaN, new PropertyChangedCallback(AxisCanvas.OnRenderablePropertyChanged)));
        public static readonly DependencyProperty BottomProperty              = DependencyProperty.RegisterAttached("Bottom", typeof (double), typeof (AxisCanvas), new PropertyMetadata((object) double.NaN, new PropertyChangedCallback(AxisCanvas.OnRenderablePropertyChanged)));
        public static readonly DependencyProperty CenterLeftProperty          = DependencyProperty.RegisterAttached("CenterLeft", typeof (double), typeof (AxisCanvas), new PropertyMetadata((object) double.NaN, new PropertyChangedCallback(AxisCanvas.OnRenderablePropertyChanged)));
        public static readonly DependencyProperty CenterRightProperty         = DependencyProperty.RegisterAttached("CenterRight", typeof (double), typeof (AxisCanvas), new PropertyMetadata((object) double.NaN, new PropertyChangedCallback(AxisCanvas.OnRenderablePropertyChanged)));
        public static readonly DependencyProperty CenterTopProperty           = DependencyProperty.RegisterAttached("CenterTop", typeof (double), typeof (AxisCanvas), new PropertyMetadata((object) double.NaN, new PropertyChangedCallback(AxisCanvas.OnRenderablePropertyChanged)));
        public static readonly DependencyProperty CenterBottomProperty        = DependencyProperty.RegisterAttached("CenterBottom", typeof (double), typeof (AxisCanvas), new PropertyMetadata((object) double.NaN, new PropertyChangedCallback(AxisCanvas.OnRenderablePropertyChanged)));

        public static double GetLeft( UIElement element )
        {
            return ( double ) element.GetValue( AxisCanvas.LeftProperty );
        }

        public static void SetLeft( UIElement element, double value )
        {
            element.SetValue( AxisCanvas.LeftProperty, ( object ) value );
        }

        public static double GetRight( UIElement element )
        {
            return ( double ) element.GetValue( AxisCanvas.RightProperty );
        }

        public static void SetRight( UIElement element, double value )
        {
            element.SetValue( AxisCanvas.RightProperty, ( object ) value );
        }

        public static double GetTop( UIElement element )
        {
            return ( double ) element.GetValue( AxisCanvas.TopProperty );
        }

        public static void SetTop( UIElement element, double value )
        {
            element.SetValue( AxisCanvas.TopProperty, ( object ) value );
        }

        public static double GetBottom( UIElement element )
        {
            return ( double ) element.GetValue( AxisCanvas.BottomProperty );
        }

        public static void SetBottom( UIElement element, double value )
        {
            element.SetValue( AxisCanvas.BottomProperty, ( object ) value );
        }

        public static double GetCenterLeft( UIElement element )
        {
            return ( double ) element.GetValue( AxisCanvas.CenterLeftProperty );
        }

        public static void SetCenterLeft( UIElement element, double value )
        {
            element.SetValue( AxisCanvas.CenterLeftProperty, ( object ) value );
        }

        public static double GetCenterRight( UIElement element )
        {
            return ( double ) element.GetValue( AxisCanvas.CenterRightProperty );
        }

        public static void SetCenterRight( UIElement element, double value )
        {
            element.SetValue( AxisCanvas.CenterRightProperty, ( object ) value );
        }

        public static double GetCenterTop( UIElement element )
        {
            return ( double ) element.GetValue( AxisCanvas.CenterTopProperty );
        }

        public static void SetCenterTop( UIElement element, double value )
        {
            element.SetValue( AxisCanvas.CenterTopProperty, ( object ) value );
        }

        public static double GetCenterBottom( UIElement element )
        {
            return ( double ) element.GetValue( AxisCanvas.CenterBottomProperty );
        }

        public static void SetCenterBottom( UIElement element, double value )
        {
            element.SetValue( AxisCanvas.CenterBottomProperty, ( object ) value );
        }

        public bool SizeWidthToContent
        {
            get
            {
                return ( bool ) this.GetValue( AxisCanvas.SizeWidthToContentProperty );
            }
            set
            {
                this.SetValue( AxisCanvas.SizeWidthToContentProperty, ( object ) value );
            }
        }

        public bool SizeHeightToContent
        {
            get
            {
                return ( bool ) this.GetValue( AxisCanvas.SizeHeightToContentProperty );
            }
            set
            {
                this.SetValue( AxisCanvas.SizeHeightToContentProperty, ( object ) value );
            }
        }

        public new bool ClipToBounds
        {
            get
            {
                return ( bool ) this.GetValue( AxisCanvas.ClipToBoundsProperty );
            }
            set
            {
                this.SetValue( AxisCanvas.ClipToBoundsProperty, ( object ) value );
            }
        }

        protected override Geometry GetLayoutClip( Size layoutSlotSize )
        {
            if ( !this.ClipToBounds )
                return ( Geometry ) null;
            return base.GetLayoutClip( layoutSlotSize );
        }

        private void Invalidate()
        {
            if ( this.IsSuspended )
                return;
            if ( this.SizeHeightToContent || this.SizeWidthToContent )
                this.InvalidateMeasure();
            else
                this.InvalidateArrange();
        }

        protected override Size MeasureOverride( Size constraint )
        {
            Size availableSize = new Size(double.PositiveInfinity, double.PositiveInfinity);
            foreach ( UIElement child in this.Children )
                child.Measure( availableSize );
            double maxWidth = 0.0;
            double maxHeight = 0.0;
            if ( this.SizeHeightToContent || this.SizeWidthToContent )
            {
                UIElement[] array = this.Children.OfType<UIElement>().ToArray<UIElement>();
                if ( this.SizeWidthToContent && !( ( IEnumerable<UIElement> ) array ).IsNullOrEmpty<UIElement>() )
                {
                    maxWidth = ( ( IEnumerable<UIElement> ) array ).Where<UIElement>( ( Func<UIElement, bool> ) ( child => !AxisCanvas.GetLeft( child ).IsNaN() ) ).Select<UIElement, double>( ( Func<UIElement, double> ) ( child => AxisCanvas.GetLeft( child ) + child.DesiredSize.Width ) ).Concat<double>( ( ( IEnumerable<UIElement> ) array ).Where<UIElement>( ( Func<UIElement, bool> ) ( child => !AxisCanvas.GetCenterLeft( child ).IsNaN() ) ).Select<UIElement, double>( ( Func<UIElement, double> ) ( child => AxisCanvas.GetCenterLeft( child ) + child.DesiredSize.Width / 2.0 ) ) ).MaxOrNullable<double>() ?? ( ( IEnumerable<UIElement> ) array ).Max<UIElement>( ( Func<UIElement, double> ) ( child => child.DesiredSize.Width ) );
                    double num = ((IEnumerable<UIElement>) array).Where<UIElement>((Func<UIElement, bool>) (child => !AxisCanvas.GetRight(child).IsNaN())).Select<UIElement, double>((Func<UIElement, double>) (child => maxWidth - AxisCanvas.GetRight(child) - child.DesiredSize.Width)).Concat<double>(((IEnumerable<UIElement>) array).Where<UIElement>((Func<UIElement, bool>) (child => !AxisCanvas.GetCenterRight(child).IsNaN())).Select<UIElement, double>((Func<UIElement, double>) (child => maxWidth - AxisCanvas.GetCenterRight(child) - child.DesiredSize.Width / 2.0))).MinOrNullable<double>() ?? 0.0;
                    if ( num < 0.0 )
                        maxWidth += Math.Abs( num );
                }
                if ( this.SizeHeightToContent && !( ( IEnumerable<UIElement> ) array ).IsNullOrEmpty<UIElement>() )
                {
                    maxHeight = ( ( IEnumerable<UIElement> ) array ).Where<UIElement>( ( Func<UIElement, bool> ) ( child => !AxisCanvas.GetTop( child ).IsNaN() ) ).Select<UIElement, double>( ( Func<UIElement, double> ) ( child => AxisCanvas.GetTop( child ) + child.DesiredSize.Height ) ).Concat<double>( ( ( IEnumerable<UIElement> ) array ).Where<UIElement>( ( Func<UIElement, bool> ) ( child => !AxisCanvas.GetCenterTop( child ).IsNaN() ) ).Select<UIElement, double>( ( Func<UIElement, double> ) ( child => AxisCanvas.GetCenterTop( child ) + child.DesiredSize.Height / 2.0 ) ) ).MaxOrNullable<double>() ?? ( ( IEnumerable<UIElement> ) array ).Max<UIElement>( ( Func<UIElement, double> ) ( child => child.DesiredSize.Height ) );
                    double num = ((IEnumerable<UIElement>) array).Where<UIElement>((Func<UIElement, bool>) (child => !AxisCanvas.GetBottom(child).IsNaN())).Select<UIElement, double>((Func<UIElement, double>) (child => maxHeight - AxisCanvas.GetBottom(child) - child.DesiredSize.Height)).Concat<double>(((IEnumerable<UIElement>) array).Where<UIElement>((Func<UIElement, bool>) (child => !AxisCanvas.GetCenterBottom(child).IsNaN())).Select<UIElement, double>((Func<UIElement, double>) (child => maxHeight - AxisCanvas.GetCenterBottom(child) - child.DesiredSize.Height / 2.0))).MinOrNullable<double>() ?? 0.0;
                    if ( num < 0.0 )
                        maxHeight += Math.Abs( num );
                }
            }
            availableSize = new Size( Math.Max( maxWidth, 0.0 ), Math.Max( maxHeight, 0.0 ) );
            return availableSize;
        }

        protected override Size ArrangeOverride( Size arrangeSize )
        {
            foreach ( UIElement child in this.Children )
            {
                Rect arrangedRect = this.GetArrangedRect(arrangeSize, child);
                child.Arrange( arrangedRect );
            }
            return arrangeSize;
        }

        protected virtual Rect GetArrangedRect( Size arrangeSize, UIElement element )
        {
            double x = 0.0;
            double y = 0.0;
            double left = AxisCanvas.GetLeft(element);
            double centerLeft = AxisCanvas.GetCenterLeft(element);
            double num1 = element.DesiredSize.Width / 2.0;
            if ( !left.IsNaN() )
                x = left;
            else if ( !centerLeft.IsNaN() )
            {
                x = centerLeft - num1;
            }
            else
            {
                double right = AxisCanvas.GetRight(element);
                if ( !right.IsNaN() )
                {
                    x = arrangeSize.Width - element.DesiredSize.Width - right;
                }
                else
                {
                    double centerRight = AxisCanvas.GetCenterRight(element);
                    if ( !centerRight.IsNaN() )
                        x = arrangeSize.Width - num1 - centerRight;
                }
            }
            double top = AxisCanvas.GetTop(element);
            double centerTop = AxisCanvas.GetCenterTop(element);
            Size desiredSize = element.DesiredSize;
            double num2 = desiredSize.Height / 2.0;
            if ( !top.IsNaN() )
                y = top;
            else if ( !centerTop.IsNaN() )
            {
                y = centerTop - num2;
            }
            else
            {
                double bottom = AxisCanvas.GetBottom(element);
                if ( !bottom.IsNaN() )
                {
                    double height1 = arrangeSize.Height;
                    desiredSize = element.DesiredSize;
                    double height2 = desiredSize.Height;
                    y = height1 - height2 - bottom;
                }
                else
                {
                    double centerBottom = AxisCanvas.GetCenterBottom(element);
                    if ( !centerBottom.IsNaN() )
                        y = arrangeSize.Height - num2 - centerBottom;
                }
            }
            return this.AdjustArrangedRectPosition( new Rect( new Point( x, y ), element.DesiredSize ), arrangeSize );
        }

        protected virtual Rect AdjustArrangedRectPosition( Rect arrangedRect, Size arrangeSize )
        {
            return arrangedRect;
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
            return ( IUpdateSuspender ) new UpdateSuspender( ( ISuspendable ) this )
            {
                ResumeTargetOnDispose = false
            };
        }

        public void DecrementSuspend()
        {
        }

        public void ResumeUpdates( IUpdateSuspender suspender )
        {
            if ( !suspender.ResumeTargetOnDispose )
                return;
            this.Invalidate();
        }

        private static void OnClipToBoundsChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
        }

        private static void OnRenderablePropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( ( ( FrameworkElement ) d ).Parent as AxisCanvas )?.Invalidate();
        }
    }
}
