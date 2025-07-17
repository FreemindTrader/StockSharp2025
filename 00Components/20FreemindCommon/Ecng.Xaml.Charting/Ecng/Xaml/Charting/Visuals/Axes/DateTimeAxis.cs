// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.DateTimeAxis
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: B:\00 - Programming\StockSharp\References\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using StockSharp.Xaml.Licensing.Core;

namespace Ecng.Xaml.Charting
{
    [UltrachartLicenseProvider( typeof( AxisUltrachartLicenseProvider ) )]
    public class DateTimeAxis : TimeSpanAxisBase
    {
        private static readonly List<Type> SupportedTypes = new List<Type>((IEnumerable<Type>) new Type[] { typeof (DateTime) });
        public static readonly DependencyProperty SubDayTextFormattingProperty = DependencyProperty.Register(nameof (SubDayTextFormatting), typeof (string), typeof (DateTimeAxis), new PropertyMetadata((object) null, new PropertyChangedCallback(AxisBase.InvalidateParent)));

        public DateTimeAxis()
        {
            DefaultStyleKey = ( object ) typeof( DateTimeAxis );
            DefaultLabelProvider = ( ILabelProvider ) new DateTimeLabelProvider();
            SetCurrentValue( AxisBase.TickProviderProperty, ( object ) new DateTimeTickProvider() );
        }

        public string SubDayTextFormatting
        {
            get
            {
                return ( string ) GetValue( DateTimeAxis.SubDayTextFormattingProperty );
            }
            set
            {
                SetValue( DateTimeAxis.SubDayTextFormattingProperty, ( object ) value );
            }
        }

        public override IRange CalculateYRange( RenderPassInfo renderPassInfo )
        {
            if ( IsXAxis )
            {
                throw new InvalidOperationException( "CalculateYRange is only valid on Y-Axis types" );
            }

            double min = DateTime.MinValue.ToDouble();
            double max = DateTime.MaxValue.ToDouble();

            int length = renderPassInfo.PointSeries.Length;
            for ( int index = 0 ; index < length ; ++index )
            {
                IRenderableSeries renderableSeries = renderPassInfo.RenderableSeries[index];
                IPointSeries pointSeries = renderPassInfo.PointSeries[index];
                if ( renderableSeries != null && pointSeries != null && !( renderableSeries.YAxisId != Id ) )
                {
                    DoubleRange yrange = pointSeries.GetYRange();
                    max = max < yrange.Min ? max : yrange.Min;
                    min = min > yrange.Max ? min : yrange.Max;
                }
            }
            return ( IRange ) new DateRange( new DateTime( Math.Min( ( long ) max, DateTime.MaxValue.Ticks ) ), new DateTime( Math.Max( ( long ) min, DateTime.MinValue.Ticks ) ) ).GrowBy( GrowBy.Min, GrowBy.Max );
        }

        public override IRange GetMaximumRange()
        {
            IRange maximumRange = base.GetMaximumRange();
            return ( IRange ) new DateRange( maximumRange.Min.ToDateTime(), maximumRange.Max.ToDateTime() );
        }

        protected override IRange CoerceZeroRange( IRange maximumRange )
        {
            long ticks = TimeSpan.FromDays(1.0).Ticks;
            return RangeFactory.NewRange( ( IComparable ) ( maximumRange.Min.ToDouble() - ( double ) ticks ), ( IComparable ) ( maximumRange.Max.ToDouble() + ( double ) ticks ) );
        }

        public override void AssertDataType( Type dataType )
        {
            if ( !DateTimeAxis.SupportedTypes.Contains( dataType ) )
            {
                throw new InvalidOperationException( string.Format( "DateTimeAxis does not support the type {0}. Supported types include {1}", ( object ) dataType, ( object ) string.Join( ", ", DateTimeAxis.SupportedTypes.Select<Type, string>( ( Func<Type, string> ) ( x => x.Name ) ).ToArray<string>() ) ) );
            }
        }

        public override IRange GetUndefinedRange()
        {
            return ( IRange ) new DateRange();
        }

        public override IRange GetDefaultNonZeroRange()
        {
            DateTime date = DateTime.UtcNow.Date;
            return ( IRange ) new DateRange( date.AddDays( -1.0 ), date.AddDays( 1.0 ) );
        }

        protected override IRange ToVisibleRange( IComparable min, IComparable max )
        {
            return ( IRange ) new DateRange( min.ToDateTime(), max.ToDateTime() );
        }

        protected override IDeltaCalculator GetDeltaCalculator()
        {
            return ( IDeltaCalculator ) DateTimeDeltaCalculator.Instance;
        }

        protected override IComparable ConvertTickToDataValue( IComparable value )
        {
            return ( IComparable ) value.ToDateTime();
        }

        public override bool IsOfValidType( IRange range )
        {
            return range is DateRange;
        }

        protected override List<Type> GetSupportedTypes()
        {
            return DateTimeAxis.SupportedTypes;
        }
    }
}
