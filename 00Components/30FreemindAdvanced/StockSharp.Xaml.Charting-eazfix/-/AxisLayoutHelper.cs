using StockSharp.Xaml.Charting.Common.Extensions;
using StockSharp.Xaml.Charting.Visuals.Axes;
using System;
using System.Windows;
using System.Windows.Controls;

namespace StockSharp.Xaml.Charting.Common
{
    public class AxisLayoutHelper
    {
        public static readonly DependencyProperty AxisAlignmentProperty = DependencyProperty.RegisterAttached("AxisAlignment", typeof (AxisAlignment), typeof (AxisLayoutHelper), new PropertyMetadata((object) AxisAlignment.Default, new PropertyChangedCallback(AxisLayoutHelper.OnAxisAlignmentChanged)));
        public static readonly DependencyProperty IsInsideItemProperty = DependencyProperty.RegisterAttached("IsInsideItem", typeof (bool), typeof (AxisLayoutHelper), new PropertyMetadata((object) false, (PropertyChangedCallback) ((d, e) => AxisLayoutHelper.OnAxisAlignmentChanged(((FrameworkElement) d).Parent, e))));
        public static readonly DependencyProperty IsOutsideItemProperty = DependencyProperty.RegisterAttached("IsOutsideItem", typeof (bool), typeof (AxisLayoutHelper), new PropertyMetadata((object) false, (PropertyChangedCallback) ((d, e) => AxisLayoutHelper.OnAxisAlignmentChanged(((FrameworkElement) d).Parent, e))));

        public static AxisAlignment GetAxisAlignment( DependencyObject obj )
        {
            return ( AxisAlignment ) obj.GetValue( AxisLayoutHelper.AxisAlignmentProperty );
        }

        public static void SetAxisAlignment( DependencyObject obj, AxisAlignment value )
        {
            obj.SetValue( AxisLayoutHelper.AxisAlignmentProperty, ( object ) value );
        }

        public static bool GetIsInsideItem( DependencyObject obj )
        {
            return ( bool ) obj.GetValue( AxisLayoutHelper.IsInsideItemProperty );
        }

        public static void SetIsInsideItem( DependencyObject obj, bool value )
        {
            obj.SetValue( AxisLayoutHelper.IsInsideItemProperty, ( object ) value );
        }

        public static bool GetIsOutsideItem( DependencyObject obj )
        {
            return ( bool ) obj.GetValue( AxisLayoutHelper.IsOutsideItemProperty );
        }

        public static void SetIsOutsideItem( DependencyObject obj, bool value )
        {
            obj.SetValue( AxisLayoutHelper.IsOutsideItemProperty, ( object ) value );
        }

        private static void OnAxisAlignmentChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            StackPanel panel = d as StackPanel;
            if ( panel == null )
                return;
            AxisLayoutHelper.UpdateItemsOrder( panel );
        }

        internal static void UpdateItemsOrder( StackPanel panel )
        {
            if ( panel.FlowDirection == FlowDirection.RightToLeft )
                return;
            AxisAlignment axisAlignment = (AxisAlignment) panel.GetValue(AxisLayoutHelper.AxisAlignmentProperty);
            bool flag = axisAlignment == AxisAlignment.Bottom || axisAlignment == AxisAlignment.Top;
            panel.Orientation = flag ? Orientation.Vertical : Orientation.Horizontal;
            FrameworkElement frameworkElement1 = (FrameworkElement) panel.Children.SingleOrDefault((Predicate<UIElement>) (elem => (bool) elem.GetValue(AxisLayoutHelper.IsInsideItemProperty)));
            FrameworkElement frameworkElement2 = (FrameworkElement) panel.Children.SingleOrDefault((Predicate<UIElement>) (elem => (bool) elem.GetValue(AxisLayoutHelper.IsOutsideItemProperty)));
            int num = axisAlignment == AxisAlignment.Left ? 1 : (axisAlignment == AxisAlignment.Top ? 1 : 0);
            panel.SafeRemoveChild( ( object ) frameworkElement1 );
            panel.SafeRemoveChild( ( object ) frameworkElement2 );
            if ( num != 0 )
            {
                panel.SafeAddChild( ( object ) frameworkElement2, 0 );
                panel.SafeAddChild( ( object ) frameworkElement1, -1 );
            }
            else
            {
                panel.SafeAddChild( ( object ) frameworkElement1, 0 );
                panel.SafeAddChild( ( object ) frameworkElement2, -1 );
            }
        }
    }
}
