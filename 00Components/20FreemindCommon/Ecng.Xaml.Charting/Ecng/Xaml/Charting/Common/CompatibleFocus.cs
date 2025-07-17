// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Common.CompatibleFocus
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System.Windows;
using System.Windows.Controls;

namespace StockSharp.Xaml.Charting.Common
{
    public class CompatibleFocus
    {
        public static readonly DependencyProperty IsFocusableProperty = DependencyProperty.RegisterAttached("IsFocusable", typeof (bool), typeof (CompatibleFocus), new PropertyMetadata((object) true, new PropertyChangedCallback(CompatibleFocus.OnIsFocusableChanged)));

        public static bool GetIsFocusable( DependencyObject obj )
        {
            return ( bool ) obj.GetValue( CompatibleFocus.IsFocusableProperty );
        }

        public static void SetIsFocusable( DependencyObject obj, bool value )
        {
            obj.SetValue( CompatibleFocus.IsFocusableProperty, ( object ) value );
        }

        private static void OnIsFocusableChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            Control control = d as Control;
            bool newValue = (bool) e.NewValue;
            if ( control == null )
                return;
            control.Focusable = newValue;
        }
    }
}
