// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Numerics.GenericMath.GenericMathFactory
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;

namespace Ecng.Xaml.Charting.Numerics.GenericMath
{
    internal static class GenericMathFactory
    {
        private static readonly FloatMath floatMath               = new FloatMath();
        private static readonly DoubleMath doubleMath             = new DoubleMath();
        private static readonly Int32Math int32Math               = new Int32Math();
        private static readonly Int64Math int64Math               = new Int64Math();
        private static readonly DateTimeMath dateTimeMath         = new DateTimeMath();
        private static readonly TimeSpanMath timeSpanMath         = new TimeSpanMath();
        private static readonly DecimalMath decimalMath           = new DecimalMath();
        private static readonly UShortMath uShortMath             = new UShortMath();
        private static readonly ShortMath shortMath               = new ShortMath();
        private static readonly Uint32Math uint32Math             = new Uint32Math();
        private static readonly Uint64Math uint64Math             = new Uint64Math();
        private static readonly SbyteMath sbyteMath               = new SbyteMath();
        private static readonly ByteMath byteMath                 = new ByteMath();
        private static readonly IDictionary<Type, object> mathMap = (IDictionary<Type, object>) new Dictionary<Type, object>()
                                                                        {
                                                                          {
                                                                            typeof (double),
                                                                            (object) GenericMathFactory.doubleMath
                                                                          },
                                                                          {
                                                                            typeof (DateTime),
                                                                            (object) GenericMathFactory.dateTimeMath
                                                                          },
                                                                          {
                                                                            typeof (float),
                                                                            (object) GenericMathFactory.floatMath
                                                                          },
                                                                          {
                                                                            typeof (int),
                                                                            (object) GenericMathFactory.int32Math
                                                                          },
                                                                          {
                                                                            typeof (long),
                                                                            (object) GenericMathFactory.int64Math
                                                                          },
                                                                          {
                                                                            typeof (Decimal),
                                                                            (object) GenericMathFactory.decimalMath
                                                                          },
                                                                          {
                                                                            typeof (TimeSpan),
                                                                            (object) GenericMathFactory.timeSpanMath
                                                                          },
                                                                          {
                                                                            typeof (short),
                                                                            (object) GenericMathFactory.shortMath
                                                                          },
                                                                          {
                                                                            typeof (ushort),
                                                                            (object) GenericMathFactory.uShortMath
                                                                          },
                                                                          {
                                                                            typeof (sbyte),
                                                                            (object) GenericMathFactory.sbyteMath
                                                                          },
                                                                          {
                                                                            typeof (ulong),
                                                                            (object) GenericMathFactory.uint64Math
                                                                          },
                                                                          {
                                                                            typeof (uint),
                                                                            (object) GenericMathFactory.uint32Math
                                                                          },
                                                                          {
                                                                            typeof (byte),
                                                                            (object) GenericMathFactory.byteMath
                                                                          }
                                                                        };

        internal static IMath<T> New<T>()
        {
            IMath<T> math = GenericMathFactory.TryNew<T>();
            if ( math != null )
            {
                return math;
            }

            throw new NotSupportedException( "GenericMath does not support Type " + ( object ) typeof( T ) );
        }

        internal static IMath<T> TryNew<T>()
        {
            Type key = typeof (T);
            object obj;
            if ( GenericMathFactory.mathMap.TryGetValue( key, out obj ) )
            {
                return ( IMath<T> ) obj;
            }

            return ( IMath<T> ) null;
        }
    }
}
