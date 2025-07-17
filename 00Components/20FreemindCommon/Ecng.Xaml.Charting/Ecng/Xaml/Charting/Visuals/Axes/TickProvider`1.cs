// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.Axes.TickProvider`1
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Linq;
using StockSharp.Xaml.Charting.Common.Extensions;

namespace StockSharp.Xaml.Charting.Visuals.Axes
{
    public abstract class TickProvider<T> : ITickProvider<T>, ITickProvider where T : IComparable
    {
        public IAxis ParentAxis
        {
            get; protected set;
        }

        public virtual void Init( IAxis axis )
        {
            this.ParentAxis = axis;
        }

        double[ ] ITickProvider.GetMajorTicks( IAxisParams axis )
        {
            return this.ConvertTicks( this.GetMajorTicks( axis ) );
        }

        double[ ] ITickProvider.GetMinorTicks( IAxisParams axis )
        {
            return this.ConvertTicks( this.GetMinorTicks( axis ) );
        }

        protected virtual double[ ] ConvertTicks( T[ ] ticks )
        {
            return ( ( IEnumerable<T> ) ticks ).Select<T, double>( ( Func<T, double> ) ( x => x.ToDouble() ) ).ToArray<double>();
        }

        public abstract T[ ] GetMajorTicks( IAxisParams axis );

        public abstract T[ ] GetMinorTicks( IAxisParams axis );
    }
}
