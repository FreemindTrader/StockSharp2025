// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Utility.PolarUtil
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Windows;

namespace Ecng.Xaml.Charting
{
    internal static class PolarUtil
    {
        public static double CalculateViewportRadius( Size viewportSize )
        {
            return PolarUtil.CalculateViewportRadius( viewportSize.Width, viewportSize.Height );
        }

        public static double CalculateViewportRadius( double width, double height )
        {
            return Math.Min( width, height ) / 2.0;
        }

        public static double AngleDistance( ref Point pt1, ref Point pt2 )
        {
            return Math.Abs( pt1.X - pt2.X );
        }

        public static Size CalculatePolarViewportSize( Size size )
        {
            return new Size( 360.0, PolarUtil.CalculateViewportRadius( size ) );
        }
    }
}
