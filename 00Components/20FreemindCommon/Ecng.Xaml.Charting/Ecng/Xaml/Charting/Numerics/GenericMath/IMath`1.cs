// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Numerics.GenericMath.IMath`1
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

namespace fx.Xaml.Charting
{
    public interface IMath<T>
    {
        T MinValue
        {
            get;
        }

        T MaxValue
        {
            get;
        }

        T ZeroValue
        {
            get;
        }

        T Max( T a, T b );

        T Min( T a, T b );

        T MinGreaterThan( T floor, T a, T b );

        bool IsNaN( T value );

        T Subtract( T a, T b );

        T Abs( T a );

        double ToDouble( T value );

        T Mult( T lhs, T rhs );

        T Mult( T lhs, double rhs );

        T Add( T lhs, T rhs );

        T Inc( ref T value );

        T Dec( ref T value );
    }
}
