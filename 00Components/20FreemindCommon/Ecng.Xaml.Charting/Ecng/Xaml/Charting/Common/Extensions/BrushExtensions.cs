// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Common.Extensions.BrushExtensions
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System.Windows.Media;

namespace fx.Xaml.Charting
{
    internal static class BrushExtensions
    {
        internal static bool IsTransparent( this Brush brush )
        {
            if ( brush.Opacity == 0.0 )
                return true;
            SolidColorBrush solidColorBrush = brush as SolidColorBrush;
            if ( solidColorBrush != null )
                return solidColorBrush.Color.A == ( byte ) 0;
            return false;
        }

        internal static Color ExtractColor( this Brush brush )
        {
            SolidColorBrush solidColorBrush = brush as SolidColorBrush;
            if ( solidColorBrush != null )
                return solidColorBrush.Color;
            LinearGradientBrush linearGradientBrush = brush as LinearGradientBrush;
            if ( linearGradientBrush != null )
                return linearGradientBrush.GradientStops[ 0 ].Color;
            return Color.FromArgb( ( byte ) 0, ( byte ) 0, ( byte ) 0, ( byte ) 0 );
        }
    }
}
