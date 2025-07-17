// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Visuals.Axes.TimeSpanAxis
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using Ecng.Xaml.Charting.Common.Extensions;
using Ecng.Xaml.Charting.Licensing;
using Ecng.Xaml.Charting.Numerics;
using StockSharp.Xaml.Licensing.Core;

namespace Ecng.Xaml.Charting.Visuals.Axes
{
    [UltrachartLicenseProvider( typeof( AxisUltrachartLicenseProvider ) )]
    public class TimeSpanAxis : TimeSpanAxisBase
    {
        private static readonly List<Type> SupportedTypes = new List<Type>((IEnumerable<Type>) new Type[1]{ typeof (TimeSpan) });

        public TimeSpanAxis()
        {
            this.DefaultStyleKey = ( object ) typeof( TimeSpanAxis );
            this.DefaultLabelProvider = ( ILabelProvider ) new TimeSpanLabelProvider();
            this.SetCurrentValue( AxisBase.TickProviderProperty, ( object ) new TimeSpanTickProvider() );
        }

        public override IRange GetUndefinedRange()
        {
            return ( IRange ) new TimeSpanRange();
        }

        protected override IRange CoerceZeroRange( IRange maximumRange )
        {
            return ( IRange ) this.GetDefaultNonZeroRange().AsDoubleRange();
        }

        public override IRange GetDefaultNonZeroRange()
        {
            return ( IRange ) new TimeSpanRange( TimeSpan.Zero, TimeSpan.FromSeconds( 1.0 ) );
        }

        protected override IRange ToVisibleRange( IComparable min, IComparable max )
        {
            return ( IRange ) new TimeSpanRange( min.ToTimeSpan(), max.ToTimeSpan() );
        }

        protected override IDeltaCalculator GetDeltaCalculator()
        {
            return ( IDeltaCalculator ) TimeSpanDeltaCalculator.Instance;
        }

        protected override IComparable ConvertTickToDataValue( IComparable value )
        {
            return ( IComparable ) value.ToTimeSpan();
        }

        public override bool IsOfValidType( IRange range )
        {
            return range is TimeSpanRange;
        }

        protected override List<Type> GetSupportedTypes()
        {
            return TimeSpanAxis.SupportedTypes;
        }
    }
}
