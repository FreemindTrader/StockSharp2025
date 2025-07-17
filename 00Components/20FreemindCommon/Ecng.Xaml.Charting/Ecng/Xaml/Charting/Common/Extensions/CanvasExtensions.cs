// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Common.Extensions.CanvasExtensions
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Windows;
using System.Windows.Controls;

namespace Ecng.Xaml.Charting.Common.Extensions
{
    public static class CanvasExtensions
    {
        internal static void RemoveWhere( this UIElementCollection uiElementCollection, Func<UIElement, bool> predicate )
        {
            for ( int index = uiElementCollection.Count - 1 ; index >= 0 ; --index )
            {
                if ( predicate( uiElementCollection[ index ] ) )
                    uiElementCollection.RemoveAt( index );
            }
        }

        internal static UIElement FirstOrDefault( this UIElementCollection uiElementCollection, Func<UIElement, bool> predicate )
        {
            for ( int index = 0 ; index < uiElementCollection.Count ; ++index )
            {
                if ( predicate( uiElementCollection[ index ] ) )
                    return uiElementCollection[ index ];
            }
            return ( UIElement ) null;
        }
    }
}
