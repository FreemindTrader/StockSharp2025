// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Common.AttachedProperties.FreezeHelper
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System.Windows;

namespace Ecng.Xaml.Charting
{
    internal class FreezeHelper
    {
        public static readonly DependencyProperty FreezeProperty = DependencyProperty.RegisterAttached("Freeze", typeof (bool), typeof (FreezeHelper), new PropertyMetadata((object) false, new PropertyChangedCallback(FreezeHelper.OnFreezePropertyChanged)));

        private static void OnFreezePropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            Freezable freezable = d as Freezable;
            if ( freezable == null || ( !true.Equals( e.NewValue ) || !freezable.CanFreeze ) )
                return;
            freezable.Freeze();
        }

        public static void SetFreeze( DependencyObject element, bool value )
        {
            element.SetValue( FreezeHelper.FreezeProperty, ( object ) value );
        }

        public static bool GetFreeze( DependencyObject element )
        {
            return ( bool ) element.GetValue( FreezeHelper.FreezeProperty );
        }
    }
}
