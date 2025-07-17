// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Numerics.LinearRegression
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Collections;
using Ecng.Xaml.Charting.Common.Extensions;

namespace Ecng.Xaml.Charting.Numerics
{
    internal class LinearRegression
    {
        private readonly double _gradient;
        private readonly double _intercept;

        public LinearRegression( IList values )
        {
            double num1 = 0.0;
            double num2 = 0.0;
            int count = values.Count;
            for ( int index = 0 ; index < count ; ++index )
            {
                num1 += ( double ) index;
                num2 += ( ( IComparable ) values[ index ] ).ToDouble();
            }
            double num3 = num1 / (double) values.Count;
            double num4 = num2 / (double) values.Count;
            double num5 = 0.0;
            double num6 = 0.0;
            for ( int index = 0 ; index < count ; ++index )
            {
                num5 += ( ( double ) index - num3 ) * ( ( ( IComparable ) values[ index ] ).ToDouble() - num4 );
                num6 += Math.Pow( ( double ) index - num3, 2.0 );
            }
            this._gradient = num5 / num6;
            this._intercept = num4 - this.Gradient * num3;
        }

        public double GetYValue( double x )
        {
            return this._gradient * x + this._intercept;
        }

        public double Intercept
        {
            get
            {
                return this._intercept;
            }
        }

        public double Gradient
        {
            get
            {
                return this._gradient;
            }
        }
    }
}
