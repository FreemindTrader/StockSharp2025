// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Numerics.CoordinateProviders.PolarTickCoordinatesProvider
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using Ecng.Xaml.Charting.Numerics.TickCoordinateProviders;

namespace Ecng.Xaml.Charting.Numerics.CoordinateProviders
{
    public class PolarTickCoordinatesProvider : DefaultTickCoordinatesProvider
    {
        protected override bool IsInBounds( double coord )
        {
            bool flag;
            if ( this.ParentAxis.IsXAxis )
            {
                int num1 = 0;
                int num2 = 360;
                flag = ( ( coord <= ( double ) num1 ? ( false ? 1 : 0 ) : ( coord < ( double ) num2 ? 1 : 0 ) ) | ( this.ParentAxis.FlipCoordinates ? ( coord <= ( double ) num2 ? 1 : 0 ) : ( coord >= ( double ) num1 ? 1 : 0 ) ) ) != 0;
            }
            else
                flag = base.IsInBounds( coord );
            return flag;
        }
    }
}
