// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.Axes.CategoryDateTimeAxis
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using StockSharp.Xaml.Charting.Common.Extensions;
using StockSharp.Xaml.Charting.Model.DataSeries;
using StockSharp.Xaml.Charting.Numerics;
using StockSharp.Xaml.Charting.Numerics.CoordinateCalculators;
using StockSharp.Xaml.Charting.Numerics.CoordinateProviders;
using StockSharp.Xaml.Charting.Utility;
using StockSharp.Xaml.Charting.Visuals.RenderableSeries;

namespace StockSharp.Xaml.Charting.Visuals.Axes
{

    public class CategoryDateTimeAxis : AxisBase, ICategoryAxis, IAxis, IAxisParams, IHitTestable, ISuspendable, IInvalidatableElement, IDrawable
    {
        public static readonly DependencyProperty BarTimeFrameProperty = DependencyProperty.Register(nameof (BarTimeFrame), typeof (double), typeof (CategoryDateTimeAxis), new PropertyMetadata((object) -1.0));
        public static readonly DependencyProperty SubDayTextFormattingProperty = DependencyProperty.Register(nameof (SubDayTextFormatting), typeof (string), typeof (CategoryDateTimeAxis), new PropertyMetadata((object) null, new PropertyChangedCallback(AxisBase.InvalidateParent)));
        private static readonly List<Type> SupportedTypes = new List<Type>((IEnumerable<Type>) new Type[1]
    {
      typeof (DateTime)
    });
        private double _dataPointWidth = double.NaN;
        private AxisParams _axisParams;

        public CategoryDateTimeAxis()
        {
            this.DefaultStyleKey = ( object ) typeof( CategoryDateTimeAxis );
            this.DefaultLabelProvider = ( ILabelProvider ) new TradeChartAxisLabelProvider();
            this.SetCurrentValue( AxisBase.TickProviderProperty, ( object ) new NumericTickProvider() );
            this.SetCurrentValue( AxisBase.TickCoordinatesProviderProperty, ( object ) new CategoryTickCoordinatesProvider() );
        }

        public string SubDayTextFormatting
        {
            get
            {
                return ( string ) this.GetValue( CategoryDateTimeAxis.SubDayTextFormattingProperty );
            }
            set
            {
                this.SetValue( CategoryDateTimeAxis.SubDayTextFormattingProperty, ( object ) value );
            }
        }

        public double BarTimeFrame
        {
            get
            {
                return ( double ) this.GetValue( CategoryDateTimeAxis.BarTimeFrameProperty );
            }
            set
            {
                this.SetValue( CategoryDateTimeAxis.BarTimeFrameProperty, ( object ) value );
            }
        }

        public int? MinimalZoomConstrain
        {
            get
            {
                return ( int? ) this.GetValue( AxisBase.MinimalZoomConstrainProperty );
            }
            set
            {
                this.SetValue( AxisBase.MinimalZoomConstrainProperty, ( object ) value );
            }
        }

        public override double CurrentDatapointPixelSize
        {
            get
            {
                return this._dataPointWidth;
            }
        }

        public override bool IsCategoryAxis
        {
            get
            {
                return true;
            }
        }

        public override IRange CalculateYRange( RenderPassInfo renderPassInfo )
        {
            throw new InvalidOperationException( "CalculateYRange is only valid on Y-Axis types" );
        }

        public override IRange GetMaximumRange()
        {
            if ( !this.IsXAxis )
            {
                throw new InvalidOperationException( "CategoryDateTimeAxis is only valid as an X-Axis" );
            }

            IRange range = this.VisibleRange == null || !this.VisibleRange.IsDefined ? this.GetDefaultNonZeroRange() : this.VisibleRange;
            IRange dataRange = this.CalculateDataRange();
            if ( dataRange != null )
            {
                range = dataRange.GrowBy( this.GrowBy.Min, this.GrowBy.Max );
                if ( this.VisibleRangeLimit != null )
                {
                    range.ClipTo( this.VisibleRangeLimit, this.VisibleRangeLimitMode );
                }
            }
            return range;
        }

        protected override IRange CalculateDataRange()
        {
            if ( this.ParentSurface == null || this.ParentSurface.RenderableSeries.IsNullOrEmpty<IRenderableSeries>() )
            {
                return ( IRange ) null;
            }

            return ( IRange ) this.GetIndexRange();
        }

        private IndexRange GetIndexRange()
        {
            IndexRange indexRange = (IndexRange) null;
            IRenderableSeries renderableSeries = this.ParentSurface.RenderableSeries.FirstOrDefault<IRenderableSeries>((Func<IRenderableSeries, bool>) (x =>
            {
                if (!(x.XAxisId == this.Id))
                {
                    return false;
                }

                IDataSeries dataSeries = x.DataSeries;
                if (dataSeries == null)
                {
                    return false;
                }

                return dataSeries.HasValues;
            }));
            if ( renderableSeries != null )
            {
                indexRange = new IndexRange( 0, renderableSeries.DataSeries.XValues.Count - 1 );
            }

            return indexRange;
        }

        public override IAxis Clone()
        {
            CategoryDateTimeAxis categoryDateTimeAxis = new CategoryDateTimeAxis();
            if ( this.VisibleRange != null )
            {
                categoryDateTimeAxis.VisibleRange = ( IRange ) this.VisibleRange.Clone();
            }

            if ( this.GrowBy != null )
            {
                categoryDateTimeAxis.GrowBy = ( IRange<double> ) this.GrowBy.Clone();
            }

            categoryDateTimeAxis.BarTimeFrame = -1.0;
            return ( IAxis ) categoryDateTimeAxis;
        }

        protected override void CalculateDelta()
        {
            IAxisDelta deltaFromRange = this.GetDeltaCalculator().GetDeltaFromRange(this.VisibleRange.Min, this.VisibleRange.Max, this.MinorsPerMajor, this.GetMaxAutoTicks());
            this.MajorDelta = deltaFromRange.MajorDelta;
            this.MinorDelta = deltaFromRange.MinorDelta;
        }

        protected override IDeltaCalculator GetDeltaCalculator()
        {
            return ( IDeltaCalculator ) NumericDeltaCalculator.Instance;
        }

        public override double GetCoordinate( IComparable value )
        {
            ICoordinateCalculator<double> coordinateCalculator1 = this.GetCurrentCoordinateCalculator();
            if ( coordinateCalculator1 == null )
            {
                return double.NaN;
            }

            ICategoryCoordinateCalculator coordinateCalculator2 = coordinateCalculator1 as ICategoryCoordinateCalculator;
            if ( coordinateCalculator2 != null && value is DateTime )
            {
                value = ( IComparable ) coordinateCalculator2.TransformDataToIndex( ( DateTime ) value );
            }

            return this._currentCoordinateCalculator.GetCoordinate( value.ToDouble() );
        }

        public override IComparable GetDataValue( double pixelCoordinate )
        {
            if ( this._currentCoordinateCalculator == null )
            {
                return ( IComparable ) int.MaxValue;
            }

            double dataValue = this._currentCoordinateCalculator.GetDataValue(pixelCoordinate);
            ICategoryCoordinateCalculator coordinateCalculator = this.GetCurrentCoordinateCalculator() as ICategoryCoordinateCalculator;
            return ( IComparable ) ( coordinateCalculator != null ? coordinateCalculator.TransformIndexToData( ( int ) dataValue ) : dataValue.ToDateTime() );
        }

        protected override IComparable ConvertTickToDataValue( IComparable value )
        {
            return ( IComparable ) value.ToDateTime();
        }

        public override bool IsOfValidType( IRange range )
        {
            return range is IndexRange;
        }

        public override IRange GetUndefinedRange()
        {
            return ( IRange ) new IndexRange( 0, int.MaxValue );
        }

        public override IRange GetDefaultNonZeroRange()
        {
            return ( IRange ) new IndexRange( 0, 10 );
        }

        public override AxisParams GetAxisParams()
        {
            return this._axisParams;
        }

        public override void OnBeginRenderPass( RenderPassInfo renderPassInfo = default( RenderPassInfo ), IPointSeries firstPointSeries = null )
        {
            this._axisParams = base.GetAxisParams();
            if ( firstPointSeries != null )
            {
                this.ComputeAxisParams( firstPointSeries );
            }
            else
            {
                this._axisParams.IsCategoryAxis = false;
                this._axisParams.CategoryPointSeries = ( IPointSeries ) null;
            }
            base.OnBeginRenderPass( renderPassInfo, firstPointSeries );
        }

        public override void ScrollByDataPoints( int pointAmount, TimeSpan duration )
        {
            IndexRange visibleRange = this.VisibleRange as IndexRange;
            if ( visibleRange == null )
            {
                return;
            }

            IndexRange indexRange = new IndexRange(visibleRange.Min + pointAmount, visibleRange.Max + pointAmount);
            this.TryApplyVisibleRangeLimit( ( IRange ) indexRange );
            this.TrySetOrAnimateVisibleRange( ( IRange ) indexRange, duration );
        }

        private void ComputeAxisParams( IPointSeries firstPointSeries )
        {
            this._axisParams.IsCategoryAxis = true;
            this._axisParams.CategoryPointSeries = firstPointSeries;
            IRange visibleRange = this.VisibleRange;
            int num = visibleRange != null ? Math.Max((int) visibleRange.Min, 0) : 0;
            int max = visibleRange != null ? Math.Max(num, (int) visibleRange.Max) : 0;
            this._axisParams.PointRange = new IndexRange( num, max );
            this._axisParams.DataPointStep = this.GetBarTimeFrame();
            this._dataPointWidth = CategoryDateTimeAxis.ComputeDataPointWidth( ( IndexRange ) visibleRange, this._axisParams.Size );
            this._axisParams.DataPointPixelSize = this._dataPointWidth;
            this._axisParams.Size -= this._dataPointWidth;
        }

        internal double GetBarTimeFrame()
        {
            IList baseXvalues = this._axisParams.BaseXValues;
            double num = (double) TimeSpan.FromSeconds(this.BarTimeFrame).Ticks;
            if ( this.BarTimeFrame <= 0.0 )
            {
                double ticks = (double) TimeSpan.FromSeconds(60.0).Ticks;
                if ( baseXvalues != null && baseXvalues.Count >= 2 )
                {
                    int index = baseXvalues.Count - 1;
                    num = ( ( ( DateTime ) baseXvalues[ index ] ).ToDouble() - ( ( DateTime ) baseXvalues[ 0 ] ).ToDouble() ) / ( double ) index;
                }
                num = num > 0.0 ? num : ticks;
            }
            return num;
        }

        internal static double ComputeDataPointWidth( IndexRange visibleRange, double size )
        {
            if ( visibleRange == null )
            {
                return 1.0;
            }

            int num = visibleRange.Max - visibleRange.Min + 1;
            return size / ( double ) num;
        }

        public DateRange ToDateRange( IndexRange visibleRange )
        {
            Guard.NotNull( ( object ) visibleRange, nameof( visibleRange ) );
            DateRange dateRange = (DateRange) null;
            ICategoryCoordinateCalculator coordinateCalculator = this.GetCurrentCoordinateCalculator() as ICategoryCoordinateCalculator;
            if ( coordinateCalculator != null )
            {
                dateRange = new DateRange( coordinateCalculator.TransformIndexToData( visibleRange.Min ).ToDateTime(), coordinateCalculator.TransformIndexToData( visibleRange.Max ).ToDateTime() );
            }

            return dateRange;
        }

        protected override List<Type> GetSupportedTypes()
        {
            return CategoryDateTimeAxis.SupportedTypes;
        }

        [SpecialName]
        HorizontalAlignment IAxis.HorizontalAlignment
        {
            get
            {
                return this.HorizontalAlignment;
            }

            set
            {
                this.HorizontalAlignment = value;
            }
        }



        [SpecialName]
        VerticalAlignment IAxis.VerticalAlignment
        {
            get
            {
                return this.VerticalAlignment;
            }

            set
            {
                this.VerticalAlignment = value;
            }

        }


        [SpecialName]
        Visibility IAxis.Visibility
        {
            get
            {
                return this.Visibility;
            }

            set
            {
                this.Visibility = value;
            }

        }


        [SpecialName]
        double IHitTestable.ActualWidth
        {
            get
            {
                return this.ActualWidth;
            }
        }

        [SpecialName]
        double IHitTestable.ActualHeight
        {
            get
            {
                return this.ActualHeight;
            }
        }




    }
}
