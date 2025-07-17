// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.FrameworkVisibilityManager
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System.Windows;

namespace fx.Xaml.Charting
{
    public class FrameworkVisibilityManager : FrameworkElement
    {
        public static readonly DependencyProperty VisibleInProperty = DependencyProperty.RegisterAttached("VisibleIn", typeof (FrameworkVisibility), typeof (FrameworkVisibilityManager), new PropertyMetadata((object) FrameworkVisibility.All, new PropertyChangedCallback(FrameworkVisibilityManager.OnVisibleInPropertyChanged)));

        public static void SetVisibleIn( DependencyObject element, FrameworkVisibility visibleIn )
        {
            element.SetValue( FrameworkVisibilityManager.VisibleInProperty, ( object ) visibleIn );
        }

        public static FrameworkVisibility GetVisibleIn( DependencyObject element )
        {
            return ( FrameworkVisibility ) element.GetValue( FrameworkVisibilityManager.VisibleInProperty );
        }

        private static void OnVisibleInPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            Visibility visibility = (FrameworkVisibility) e.NewValue == FrameworkVisibility.Silverlight ? Visibility.Collapsed : Visibility.Visible;
            ( d as UIElement ).Visibility = visibility;
        }
    }
}
