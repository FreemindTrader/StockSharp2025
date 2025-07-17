// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.NumericAxis
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: B:\00 - Programming\StockSharp\References\fx.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using StockSharp.Xaml.Licensing.Core;

namespace fx.Xaml.Charting
{
    [UltrachartLicenseProvider( typeof( AxisUltrachartLicenseProvider ) )]
    public class NumericAxis : AxisBase
    {
        private static readonly List<Type> SupportedTypes = new List<Type>((IEnumerable<Type>) new Type[10]
        {
          typeof (int),
          typeof (double),
          typeof (Decimal),
          typeof (long),
          typeof (float),
          typeof (short),
          typeof (byte),
          typeof (uint),
          typeof (ushort),
          typeof (sbyte)
        });

        public static readonly DependencyProperty ScientificNotationProperty = DependencyProperty.Register(nameof (ScientificNotation), typeof (ScientificNotation), typeof (NumericAxis), new PropertyMetadata((object) ScientificNotation.None, new PropertyChangedCallback(AxisBase.InvalidateParent)));
        private AxisParams _axisParams;

        public NumericAxis()
        {
            DefaultStyleKey = ( object ) typeof( NumericAxis );
            DefaultLabelProvider = ( ILabelProvider ) new NumericLabelProvider();
            SetCurrentValue( AxisBase.TickProviderProperty, ( object ) new NumericTickProvider() );
        }

        public new double MajorDelta
        {
            get
            {
                return ( double ) GetValue( AxisBase.MajorDeltaProperty );
            }
            set
            {
                SetValue( AxisBase.MajorDeltaProperty, ( object ) value );
            }
        }

        public new double MinorDelta
        {
            get
            {
                return ( double ) GetValue( AxisBase.MinorDeltaProperty );
            }
            set
            {
                SetValue( AxisBase.MinorDeltaProperty, ( object ) value );
            }
        }

        public new double? MinimalZoomConstrain
        {
            get
            {
                return ( double? ) GetValue( AxisBase.MinimalZoomConstrainProperty );
            }
            set
            {
                SetValue( AxisBase.MinimalZoomConstrainProperty, ( object ) value );
            }
        }

        public ScientificNotation ScientificNotation
        {
            get
            {
                return ( ScientificNotation ) GetValue( NumericAxis.ScientificNotationProperty );
            }
            set
            {
                SetValue( NumericAxis.ScientificNotationProperty, ( object ) value );
            }
        }

        protected override IDeltaCalculator GetDeltaCalculator()
        {
            return ( IDeltaCalculator ) NumericDeltaCalculator.Instance;
        }

        protected override void CalculateDelta()
        {
            IRange<double> visibleRange = (IRange<double>) VisibleRange;
            if ( !AutoTicks )
                return;
            uint maxAutoTicks = GetMaxAutoTicks();
            IAxisDelta<double> deltaFromRange = (IAxisDelta<double>) GetDeltaCalculator().GetDeltaFromRange((IComparable) visibleRange.Min, (IComparable) visibleRange.Max, MinorsPerMajor, maxAutoTicks);
            MajorDelta = deltaFromRange.MajorDelta;
            MinorDelta = deltaFromRange.MinorDelta;
            UltrachartDebugLogger.Instance.WriteLine( "CalculateDelta: Min={0}, Max={1}, Major={2}, Minor={3}, MaxAutoTicks={4}", ( object ) visibleRange.Min, ( object ) visibleRange.Max, ( object ) deltaFromRange.MajorDelta, ( object ) deltaFromRange.MinorDelta, ( object ) maxAutoTicks );
        }

        public override IRange CalculateYRange( RenderPassInfo renderPassInfo )
        {
            if ( IsXAxis )
                throw new InvalidOperationException( "CalculateYRange is only valid on Y-Axis types" );
            double num1 = double.MinValue;
            double num2 = double.MaxValue;
            Dictionary<string, DoubleRange> dictionary = new Dictionary<string, DoubleRange>();
            int length = renderPassInfo.PointSeries.Length;
            for ( int index = 0 ; index < length ; ++index )
            {
                IRenderableSeries renderableSeries1 = renderPassInfo.RenderableSeries[index];
                IPointSeries pointSeries = renderPassInfo.PointSeries[index];
                if ( renderableSeries1 != null && pointSeries != null && !( renderableSeries1.YAxisId != Id ) )
                {
                    IDataSeries dataSeries = renderPassInfo.DataSeries[index];
                    IndexRange indicesRange = renderPassInfo.IndicesRanges[index];
                    DoubleRange doubleRange1 = dataSeries.DataSeriesType != DataSeriesType.Ohlc || indicesRange.Diff.CompareTo(1000) >= 0 ? pointSeries.GetYRange() : dataSeries.GetWindowedYRange(new IndexRange(indicesRange.Min, indicesRange.Max)).AsDoubleRange();
                    string key = string.Empty;
                    if ( renderableSeries1 is IStackedRenderableSeries )
                    {
                        IStackedRenderableSeries renderableSeries2 = renderableSeries1 as IStackedRenderableSeries;
                        key = renderableSeries2.StackedGroupId;
                        doubleRange1 = ( DoubleRange ) renderableSeries2.GetYRange( ( IRange ) renderPassInfo.IndicesRanges[ index ], IsLogarithmicAxis );
                        if ( dictionary.ContainsKey( key ) )
                        {
                            DoubleRange doubleRange2 = dictionary[renderableSeries2.StackedGroupId];
                            dictionary[ renderableSeries2.StackedGroupId ] = ( DoubleRange ) doubleRange1.Union( ( IRange<double> ) doubleRange2 );
                        }
                    }
                    else
                    {
                        num2 = num2 < doubleRange1.Min ? num2 : doubleRange1.Min;
                        num1 = num1 > doubleRange1.Max ? num1 : doubleRange1.Max;
                    }
                    if ( !dictionary.ContainsKey( key ) )
                        dictionary.Add( key, doubleRange1 );
                }
            }
            foreach ( KeyValuePair<string, DoubleRange> keyValuePair in dictionary )
            {
                num2 = num2 < keyValuePair.Value.Min ? num2 : keyValuePair.Value.Min;
                num1 = num1 > keyValuePair.Value.Max ? num1 : keyValuePair.Value.Max;
            }
            IRange range = RangeFactory.NewRange((IComparable) num2, (IComparable) num1);
            double logBase = IsLogarithmicAxis ? ((ILogarithmicAxis) this).LogarithmicBase : 0.0;
            if ( GrowBy == null )
                return range;
            return range.GrowBy( GrowBy.Min, GrowBy.Max, IsLogarithmicAxis, logBase );
        }

        public override IRange GetUndefinedRange()
        {
            return ( IRange ) new DoubleRange( double.NaN, double.NaN );
        }

        public override IRange GetDefaultNonZeroRange()
        {
            return ( IRange ) new DoubleRange( 0.0, 10.0 );
        }

        public override IAxis Clone()
        {
            IAxis instance = (IAxis) Activator.CreateInstance(GetType());
            if ( VisibleRange != null )
                instance.VisibleRange = ( IRange ) VisibleRange.Clone();
            if ( GrowBy != null )
                instance.GrowBy = ( IRange<double> ) GrowBy.Clone();
            return instance;
        }

        public override bool IsOfValidType( IRange range )
        {
            return range is DoubleRange;
        }

        protected override List<Type> GetSupportedTypes()
        {
            return NumericAxis.SupportedTypes;
        }

        protected override void OnVisibleRangeChanged( VisibleRangeChangedEventArgs args )
        {
            _axisParams = base.GetAxisParams();
            base.OnVisibleRangeChanged( args );
        }

        public override AxisParams GetAxisParams()
        {
            return _axisParams;
        }

        public override void OnBeginRenderPass( RenderPassInfo renderPassInfo = default( RenderPassInfo ), IPointSeries firstPointSeries = null )
        {
            _axisParams = base.GetAxisParams();
            TimeframeSegmentRenderableSeries renderableSeries = renderPassInfo.RenderableSeries.OfType<TimeframeSegmentRenderableSeries>().FirstOrDefault<TimeframeSegmentRenderableSeries>();
            if ( firstPointSeries != null && renderableSeries != null )
            {
                double priceScale = renderableSeries.PriceScale;
                IRange<double> visibleRange = (IRange<double>) VisibleRange;
                if ( priceScale > 0.0 )
                {
                    _axisParams.DataPointStep = priceScale;
                    _axisParams.DataPointPixelSize = renderPassInfo.ViewportSize.Height / ( ( visibleRange.Max - visibleRange.Min ) / priceScale );
                }
            }
            base.OnBeginRenderPass( renderPassInfo, firstPointSeries );
        }

        public override double CurrentDatapointPixelSize
        {
            get
            {
                return _axisParams.DataPointPixelSize;
            }
        }

        protected override IRange CoerceZeroRange( IRange maximumRange )
        {
            DoubleRange doubleRange = maximumRange as DoubleRange;
            if ( _axisParams.DataPointStep <= 0.0 || doubleRange == null )
                return base.CoerceZeroRange( maximumRange );
            return ( IRange ) new DoubleRange( doubleRange.Min - _axisParams.DataPointStep, doubleRange.Min + _axisParams.DataPointStep );
        }

        public override IComparable GetDataValue( double pixelCoordinate )
        {
            IComparable dataValue = base.GetDataValue(pixelCoordinate);
            if ( _axisParams.DataPointStep <= 0.0 || !( dataValue is double ) )
                return dataValue;
            double num = (double) dataValue;
            if ( num.IsNaN() )
                return ( IComparable ) num;
            return ( IComparable ) num.Round( _axisParams.DataPointStep );
        }
    }
}
