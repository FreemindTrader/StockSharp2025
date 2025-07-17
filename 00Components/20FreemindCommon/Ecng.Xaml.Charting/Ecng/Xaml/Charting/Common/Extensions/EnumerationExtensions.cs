// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Common.Extensions.EnumerationExtensions
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

namespace Ecng.Xaml.Charting
{
    internal static class EnumerationExtensions
    {
        internal static bool IsTop( this LabelPlacement placement )
        {
            if ( placement != LabelPlacement.Top && placement != LabelPlacement.TopLeft )
                return placement == LabelPlacement.TopRight;
            return true;
        }

        internal static bool IsBottom( this LabelPlacement placement )
        {
            if ( placement != LabelPlacement.Bottom && placement != LabelPlacement.BottomLeft )
                return placement == LabelPlacement.BottomRight;
            return true;
        }

        internal static bool IsRight( this LabelPlacement placement )
        {
            if ( placement != LabelPlacement.Right && placement != LabelPlacement.TopRight )
                return placement == LabelPlacement.BottomRight;
            return true;
        }

        internal static bool IsLeft( this LabelPlacement placement )
        {
            if ( placement != LabelPlacement.Left && placement != LabelPlacement.TopLeft )
                return placement == LabelPlacement.BottomLeft;
            return true;
        }

        internal static bool IsAxis( this LabelPlacement placement )
        {
            return placement == LabelPlacement.Axis;
        }
    }
}
