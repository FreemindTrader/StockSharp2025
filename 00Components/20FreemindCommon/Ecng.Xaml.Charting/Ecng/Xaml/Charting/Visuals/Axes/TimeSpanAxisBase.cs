//// Decompiled with JetBrains decompiler
//// Type: Ecng.Xaml.Charting.TimeSpanAxisBase
//// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
//// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
//// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

//using Ecng.Xaml.Charting;//using Ecng.Xaml.Charting;//using Ecng.Xaml.Charting;//using Ecng.Xaml.Charting;//using Ecng.Xaml.Charting;//using System;
//using System.Runtime.CompilerServices;
//using System.Windows;

//namespace Ecng.Xaml.Charting//{
//    public abstract class TimeSpanAxisBase : AxisBase, IAxis, IAxisParams, IHitTestable, ISuspendable, IInvalidatableElement, IDrawable
//    {
//        IComparable IAxisParams.MinorDelta
//        {
//            get
//            {
//                return ( IComparable ) MinorDelta;
//            }
//            set
//            {
//                MinorDelta = ( TimeSpan ) value;
//            }
//        }

//        IComparable IAxisParams.MajorDelta
//        {
//            get
//            {
//                return ( IComparable ) MajorDelta;
//            }
//            set
//            {
//                MajorDelta = ( TimeSpan ) value;
//            }
//        }

//        public TimeSpan MajorDelta
//        {
//            get
//            {
//                return ( TimeSpan ) GetValue( AxisBase.MajorDeltaProperty );
//            }
//            set
//            {
//                SetValue( AxisBase.MajorDeltaProperty, ( object ) value );
//            }
//        }

//        public TimeSpan MinorDelta
//        {
//            get
//            {
//                return ( TimeSpan ) GetValue( AxisBase.MinorDeltaProperty );
//            }
//            set
//            {
//                SetValue( AxisBase.MinorDeltaProperty, ( object ) value );
//            }
//        }

//        public TimeSpan? MinimalZoomConstrain
//        {
//            get
//            {
//                return ( TimeSpan? ) GetValue( AxisBase.MinimalZoomConstrainProperty );
//            }
//            set
//            {
//                SetValue( AxisBase.MinimalZoomConstrainProperty, ( object ) value );
//            }
//        }

//        public override IRange CalculateYRange( RenderPassInfo renderPassInfo )
//        {
//            if ( IsXAxis )
//            {
//                throw new InvalidOperationException( "CalculateYRange is only valid on Y-Axis types" );
//            }

//            double num1 = TimeSpan.MinValue.ToDouble();
//            double num2 = TimeSpan.MaxValue.ToDouble();
//            int length = renderPassInfo.PointSeries.Length;
//            for ( int index = 0; index < length; ++index )
//            {
//                IRenderableSeries renderableSeries = renderPassInfo.RenderableSeries[index];
//                IPointSeries pointSeries = renderPassInfo.PointSeries[index];
//                if ( renderableSeries != null && pointSeries != null && !( renderableSeries.YAxisId != Id ) )
//                {
//                    DoubleRange yrange = pointSeries.GetYRange();
//                    num2 = num2 < yrange.Min ? num2 : yrange.Min;
//                    num1 = num1 > yrange.Max ? num1 : yrange.Max;
//                }
//            }
//            return ToVisibleRange( ( IComparable ) num2, ( IComparable ) num1 ).GrowBy( GrowBy.Min, GrowBy.Max );
//        }

//        protected abstract IRange ToVisibleRange( IComparable min, IComparable max );

//        public override IRange GetMaximumRange( )
//        {
//            IRange maximumRange = base.GetMaximumRange();
//            return ToVisibleRange( maximumRange.Min, maximumRange.Max );
//        }

//        protected override IRange CalculateDataRange( )
//        {
//            IRange range = base.CalculateDataRange();
//            if ( range != null )
//            {
//                range = ToVisibleRange( range.Min, range.Max );
//            }

//            return range;
//        }

//        protected override void CalculateDelta( )
//        {
//            if ( !AutoTicks )
//            {
//                return;
//            }

//            TimeSpanDelta deltaFromRange = ((IDateDeltaCalculator) GetDeltaCalculator()).GetDeltaFromRange(VisibleRange.Min, VisibleRange.Max, MinorsPerMajor, GetMaxAutoTicks());
//            MajorDelta = deltaFromRange.MajorDelta;
//            MinorDelta = deltaFromRange.MinorDelta;
//            UltrachartDebugLogger.Instance.WriteLine( "CalculateDelta: Major={0}, Minor={1}", ( object ) deltaFromRange.MajorDelta, ( object ) deltaFromRange.MinorDelta );
//        }

//        public override IComparable GetDataValue( double pixelCoordinate )
//        {
//            return ConvertTickToDataValue( base.GetDataValue( pixelCoordinate ) );
//        }

//        public override IAxis Clone( )
//        {
//            TimeSpanAxisBase instance = (TimeSpanAxisBase) Activator.CreateInstance(GetType());
//            if ( VisibleRange != null )
//            {
//                instance.VisibleRange = ( IRange ) VisibleRange.Clone();
//            }

//            if ( GrowBy != null )
//            {
//                instance.GrowBy = ( IRange<double> ) GrowBy.Clone();
//            }

//            return ( IAxis ) instance;
//        }


//    }
//}

// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.TimeSpanAxisBase
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: B:\00 - Programming\StockSharp\References\Ecng.Xaml.Charting.dll

using System;
using System.Runtime.CompilerServices;
using System.Windows;
namespace Ecng.Xaml.Charting
{
    public abstract class TimeSpanAxisBase : AxisBase, IAxis, IAxisParams, IHitTestable, ISuspendable, IInvalidatableElement, IDrawable
    {
        IComparable IAxisParams.MinorDelta
        {
            get
            {
                return ( IComparable ) MinorDelta;
            }
            set
            {
                MinorDelta = ( TimeSpan ) value;
            }
        }

        IComparable IAxisParams.MajorDelta
        {
            get
            {
                return ( IComparable ) MajorDelta;
            }
            set
            {
                MajorDelta = ( TimeSpan ) value;
            }
        }

        public new TimeSpan MajorDelta
        {
            get
            {
                return ( TimeSpan ) GetValue( AxisBase.MajorDeltaProperty );
            }
            set
            {
                SetValue( AxisBase.MajorDeltaProperty, ( object ) value );
            }
        }

        public new TimeSpan MinorDelta
        {
            get
            {
                return ( TimeSpan ) GetValue( AxisBase.MinorDeltaProperty );
            }
            set
            {
                SetValue( AxisBase.MinorDeltaProperty, ( object ) value );
            }
        }

        public new TimeSpan? MinimalZoomConstrain
        {
            get
            {
                return ( TimeSpan? ) GetValue( AxisBase.MinimalZoomConstrainProperty );
            }
            set
            {
                SetValue( AxisBase.MinimalZoomConstrainProperty, ( object ) value );
            }
        }

        public override IRange CalculateYRange( RenderPassInfo renderPassInfo )
        {
            if ( IsXAxis )
            {
                throw new InvalidOperationException( "CalculateYRange is only valid on Y-Axis types" );
            }

            double num1 = TimeSpan.MinValue.ToDouble();
            double num2 = TimeSpan.MaxValue.ToDouble();
            int length = renderPassInfo.PointSeries.Length;
            for ( int index = 0 ; index < length ; ++index )
            {
                IRenderableSeries renderableSeries = renderPassInfo.RenderableSeries[index];
                IPointSeries pointSeries = renderPassInfo.PointSeries[index];
                if ( renderableSeries != null && pointSeries != null && !( renderableSeries.YAxisId != Id ) )
                {
                    DoubleRange yrange = pointSeries.GetYRange();
                    num2 = num2 < yrange.Min ? num2 : yrange.Min;
                    num1 = num1 > yrange.Max ? num1 : yrange.Max;
                }
            }
            return ToVisibleRange( ( IComparable ) num2, ( IComparable ) num1 ).GrowBy( GrowBy.Min, GrowBy.Max );
        }

        protected abstract IRange ToVisibleRange( IComparable min, IComparable max );

        public override IRange GetMaximumRange()
        {
            IRange maximumRange = base.GetMaximumRange();
            return ToVisibleRange( maximumRange.Min, maximumRange.Max );
        }

        protected override IRange CalculateDataRange()
        {
            IRange range = base.CalculateDataRange();
            if ( range != null )
            {
                range = ToVisibleRange( range.Min, range.Max );
            }

            return range;
        }

        protected override void CalculateDelta()
        {
            if ( !AutoTicks )
            {
                return;
            }

            IDateDeltaCalculator deltaCalculator = (IDateDeltaCalculator) GetDeltaCalculator();
            uint maxAutoTicks = GetMaxAutoTicks();
            IComparable min = VisibleRange.Min;
            IComparable max = VisibleRange.Max;
            int minorsPerMajor = MinorsPerMajor;
            int num = (int) maxAutoTicks;
            TimeSpanDelta deltaFromRange = deltaCalculator.GetDeltaFromRange(min, max, minorsPerMajor, (uint) num);
            MajorDelta = deltaFromRange.MajorDelta;
            MinorDelta = deltaFromRange.MinorDelta;
            UltrachartDebugLogger.Instance.WriteLine( "CalculateDelta: Major={0}, Minor={1}", ( object ) deltaFromRange.MajorDelta, ( object ) deltaFromRange.MinorDelta );
        }

        public override IComparable GetDataValue( double pixelCoordinate )
        {
            return ConvertTickToDataValue( base.GetDataValue( pixelCoordinate ) );
        }

        public override IAxis Clone()
        {
            TimeSpanAxisBase instance = (TimeSpanAxisBase) Activator.CreateInstance(GetType());
            if ( VisibleRange != null )
            {
                instance.VisibleRange = ( IRange ) VisibleRange.Clone();
            }

            if ( GrowBy != null )
            {
                instance.GrowBy = ( IRange<double> ) GrowBy.Clone();
            }

            return ( IAxis ) instance;
        }

        [SpecialName]
        HorizontalAlignment IAxis.HorizontalAlignment
        {
            get
            {
                return HorizontalAlignment;
            }

            set
            {
                HorizontalAlignment = value;
            }
        }



        [SpecialName]
        VerticalAlignment IAxis.VerticalAlignment
        {
            get
            {
                return VerticalAlignment;
            }

            set
            {
                VerticalAlignment = value;
            }

        }


        [SpecialName]
        Visibility IAxis.Visibility
        {
            get
            {
                return Visibility;
            }

            set
            {
                Visibility = value;
            }
        }


        [SpecialName]
        double IHitTestable.ActualWidth
        {
            get
            {
                return ActualWidth;
            }


        }

        [SpecialName]
        double IHitTestable.ActualHeight
        {
            get
            {
                return ActualHeight;
            }
        }
    }
}

