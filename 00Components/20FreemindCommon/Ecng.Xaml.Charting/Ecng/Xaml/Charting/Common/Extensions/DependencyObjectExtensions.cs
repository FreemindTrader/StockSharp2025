// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Common.Extensions.DependencyObjectExtensions
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System.Windows;
using System.Windows.Media;

namespace Ecng.Xaml.Charting
{
    public static class DependencyObjectExtensions
    {
        public static T FindVisualChild<T>( this DependencyObject parent ) where T : DependencyObject
        {
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for ( int childIndex = 0 ; childIndex < childrenCount ; ++childIndex )
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, childIndex);
                if ( child != null && typeof( T ).IsAssignableFrom( child.GetType() ) )
                    return ( T ) child;
                DependencyObject visualChild = (DependencyObject) child.FindVisualChild<T>();
                if ( visualChild != null )
                    return ( T ) visualChild;
            }
            return default( T );
        }
    }
}
