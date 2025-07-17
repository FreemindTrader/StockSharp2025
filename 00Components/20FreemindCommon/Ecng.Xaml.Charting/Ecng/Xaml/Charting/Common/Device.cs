// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Common.Device
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System.Windows;

namespace fx.Xaml.Charting
{
    public class Device : FrameworkElement
    {
        public new static readonly DependencyProperty SnapsToDevicePixelsProperty = DependencyProperty.RegisterAttached("SnapsToDevicePixels", typeof (bool), typeof (Device), new PropertyMetadata((object) false, new PropertyChangedCallback(Device.OnSnapToDevicePixelsPropertyChanged)));

        public static void SetSnapsToDevicePixels( DependencyObject element, bool snapToDevicePixels )
        {
            element.SetValue( Device.SnapsToDevicePixelsProperty, ( object ) snapToDevicePixels );
        }

        public static bool GetSnapsToDevicePixels( DependencyObject element )
        {
            return ( bool ) element.GetValue( Device.SnapsToDevicePixelsProperty );
        }

        private static void OnSnapToDevicePixelsPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            FrameworkElement frameworkElement = (FrameworkElement) d;
            bool newValue = (bool) e.NewValue;
            frameworkElement.SetCurrentValue( FrameworkElement.UseLayoutRoundingProperty, ( object ) newValue );
            frameworkElement.SetCurrentValue( Device.SnapsToDevicePixelsProperty, ( object ) newValue );
        }
    }
}
