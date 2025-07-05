using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
using fx.Algorithm;
using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.Visuals.Annotations;
using fx.Definitions;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Indicators;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using fx.Collections;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using fx.Bars;

namespace fx.Charting
{
    public partial class ChartDrawData
    {
        public struct sCandleColor : IDrawValue
        {
            private readonly DateTime _utcTime;
            private readonly Color? _candleColor;

            public sCandleColor( DateTimeOffset _param1, Color? _param2 )
            {
                _utcTime = _param1.UtcDateTime;
                _candleColor = _param2;
            }


            public DateTime UtcTime()
            {
                return _utcTime;
            }

            public Color? Color
            {
                get
                {
                    return _candleColor;
                }
            }
        }
        ///// <summary>Indicator values to draw on chart.</summary>
        ////public struct sIndicator : IDrawValue<sIndicator>
        //public struct sIndicator : IDrawValue
        //{
        //    private readonly DateTime _utcTime;

        //    private readonly IIndicatorValue indicatorValue;

        //    /// <summary>Create instance.</summary>
        //    /// <param name="dto">Value timestamp.</param>
        //    /// <param name="val">Indicator value.</param>
        //    public sIndicator( DateTimeOffset dto, IIndicatorValue val )
        //    {
        //        this = new sIndicator( dto.UtcDateTime, val );
        //    }

        //    public sIndicator( DateTime _param1, IIndicatorValue _param2 )
        //    {
        //        _utcTime = _param1;
        //        indicatorValue = _param2;
        //    }

        //    /// <summary>Value timestamp.</summary>
        //    public DateTime Time
        //    {
        //        get
        //        {
        //            return _utcTime;
        //        }
        //    }

        //    /// <summary>Indicator value.</summary>
        //    public IIndicatorValue Value
        //    {
        //        get
        //        {
        //            return indicatorValue;
        //        }
        //    }
        //}

        //public struct sTrade : IDrawValue<sTrade>


        public struct sxTuple<T> : IDrawValue where T : struct, IComparable
        {
            private readonly T _property;
            private readonly double _valueOne;
            private readonly double _y2;

            private sxTuple( double _param1, double _param2, double _param3 = double.NaN )
            {
                this = new sxTuple<T>( ( T ) ( ValueType ) _param1, _param2, _param3 );
            }

            private sxTuple( DateTime _param1, double _param2, double _param3 = double.NaN )
            {
                this = new sxTuple<T>( ( T ) ( ValueType ) _param1, _param2, _param3 );
            }

            private sxTuple(
              T _param1,
              double _param2,
              double _param3 )
            {
                _property = _param1;
                _valueOne = _param2;
                _y2 = _param3;
            }


            public T GetProperty()
            {
                return _property;
            }


            public double ValueOne()
            {
                return _valueOne;
            }


            public double ValueTwo()
            {
                return _y2;
            }

            public static sxTuple<T> CreateSxTuple<TXOrig>( TXOrig _param0, double _param1, double _param2 )
                where TXOrig : struct, IComparable
            {
                if ( ( ValueType ) _param0 is DateTimeOffset )
                    return new sxTuple<T>( _param0.To<DateTimeOffset>().UtcDateTime, _param1, _param2 );
                if ( ( ValueType ) _param0 is double )
                    return new sxTuple<T>( _param0.To<double>(), _param1, _param2 );
                
                throw new NotSupportedException( "LocalizedStrings.Param.Put( typeof( TXOrig ).Name )" );
            }

            public static sxTuple<T> CreateSxTuple( T _param0 )
            {
                return new sxTuple<T>( _param0, double.NaN, double.NaN );
            }

            public static sxTuple<T> CreateSxTuple( T _param0, double _param1, double _param2 )
            {
                return new sxTuple<T>( _param0, _param1, _param2 );
            }
        }




        public class sAnnotation : IDrawValue, IPersistable
        {

            private bool? _isVisible = new bool? (true);

            private bool? _isEditable;

            private IComparable _x1;

            private IComparable _y1;

            private IComparable _x2;

            private IComparable _y2;

            private Brush _stroke;

            private Brush _fill;

            private Brush _foreground;

            private Thickness? _thickness;

            private bool? _showLabel;

            private LabelPlacement? _labelPlacement;

            private HorizontalAlignment? _horizontalAlignment;

            private VerticalAlignment? _verticalAlignment;

            private AnnotationCoordinateMode? _coordinateMode;

            private string _text;

            /// <summary>Show/hide annotation.</summary>
            public bool? IsVisible
            {
                get
                {
                    return _isVisible;
                }
                set
                {
                    _isVisible = value;
                }
            }

            /// <summary>Whether user can edit annotation.</summary>
            public bool? IsEditable
            {
                get
                {
                    return _isEditable;
                }
                set
                {
                    _isEditable = value;
                }
            }

            /// <summary>
            /// X1 coordinate for annotation drawing.
            /// <see cref="T:System.DateTimeOffset" /> for coordinate mode <see cref="F:StockSharp.Xaml.Charting.Visuals.Annotations.AnnotationCoordinateMode.Absolute" /> or <see cref="F:StockSharp.Xaml.Charting.Visuals.Annotations.AnnotationCoordinateMode.RelativeY" />.
            /// <see cref="T:System.Double" /> otherwise.
            /// </summary>
            public IComparable X1
            {
                get
                {
                    return _x1;
                }
                set
                {
                    _x1 = value;
                }
            }

            /// <summary>
            /// Y1 coordinate for annotation drawing.
            /// <see cref="T:System.Decimal" /> for coordinate mode <see cref="F:StockSharp.Xaml.Charting.Visuals.Annotations.AnnotationCoordinateMode.Absolute" /> or <see cref="F:StockSharp.Xaml.Charting.Visuals.Annotations.AnnotationCoordinateMode.RelativeX" />.
            /// <see cref="T:System.Double" /> otherwise.
            /// </summary>
            public IComparable Y1
            {
                get
                {
                    return _y1;
                }
                set
                {
                    _y1 = value;
                }
            }

            /// <summary>
            /// X2 coordinate for annotation drawing.
            /// <see cref="T:System.DateTimeOffset" /> for coordinate mode <see cref="F:StockSharp.Xaml.Charting.Visuals.Annotations.AnnotationCoordinateMode.Absolute" /> or <see cref="F:StockSharp.Xaml.Charting.Visuals.Annotations.AnnotationCoordinateMode.RelativeY" />.
            /// <see cref="T:System.Double" /> otherwise.
            /// </summary>
            public IComparable X2
            {
                get
                {
                    return _x2;
                }
                set
                {
                    _x2 = value;
                }
            }

            /// <summary>
            /// Y2 coordinate for annotation drawing.
            /// <see cref="T:System.Decimal" /> for coordinate mode <see cref="F:StockSharp.Xaml.Charting.Visuals.Annotations.AnnotationCoordinateMode.Absolute" /> or <see cref="F:StockSharp.Xaml.Charting.Visuals.Annotations.AnnotationCoordinateMode.RelativeX" />.
            /// <see cref="T:System.Double" /> otherwise.
            /// </summary>
            public IComparable Y2
            {
                get
                {
                    return _y2;
                }
                set
                {
                    _y2 = value;
                }
            }

            /// <summary>Brush to draw lines and borders.</summary>
            public Brush Stroke
            {
                get
                {
                    return _stroke;
                }
                set
                {
                    _stroke = value;
                }
            }

            /// <summary>Brush to fill background.</summary>
            public Brush Fill
            {
                get
                {
                    return _fill;
                }
                set
                {
                    _fill = value;
                }
            }

            /// <summary>Brush to fill background.</summary>
            public Brush Foreground
            {
                get
                {
                    return _foreground;
                }
                set
                {
                    _foreground = value;
                }
            }

            /// <summary>Line thickness.</summary>
            public Thickness? Thickness
            {
                get
                {
                    return _thickness;
                }
                set
                {
                    _thickness = value;
                }
            }

            /// <summary>Turn on/off label show for horizontal and vertical lines.</summary>
            public bool? ShowLabel
            {
                get
                {
                    return _showLabel;
                }
                set
                {
                    _showLabel = value;
                }
            }

            /// <summary>Label placement for horizontal and vertical lines.</summary>
            public LabelPlacement? LabelPlacement
            {
                get
                {
                    return _labelPlacement;
                }
                set
                {
                    _labelPlacement = value;
                }
            }

            /// <summary>Alignment for horizontal lines.</summary>
            public HorizontalAlignment? HorizontalAlignment
            {
                get
                {
                    return _horizontalAlignment;
                }
                set
                {
                    _horizontalAlignment = value;
                }
            }

            /// <summary>Alignment for vertical lines.</summary>
            public VerticalAlignment? VerticalAlignment
            {
                get
                {
                    return _verticalAlignment;
                }
                set
                {
                    _verticalAlignment = value;
                }
            }

            /// <summary>
            /// Coordinate mode.
            /// <see cref="F:StockSharp.Xaml.Charting.Visuals.Annotations.AnnotationCoordinateMode.Absolute" /> means <see cref="T:System.DateTimeOffset" /> for X and <see cref="T:System.Decimal" /> price for Y.
            /// <see cref="F:StockSharp.Xaml.Charting.Visuals.Annotations.AnnotationCoordinateMode.Relative" /> means relative to the screen edges: double. 0=top/left, 0.5=center, 1=bottom/right
            /// </summary>
            public AnnotationCoordinateMode? CoordinateMode
            {
                get
                {
                    return _coordinateMode;
                }
                set
                {
                    _coordinateMode = value;
                }
            }

            /// <summary>Text for text annotation.</summary>
            public string Text
            {
                get
                {
                    return _text;
                }
                set
                {
                    _text = value;
                }
            }

            /// <summary>Load settings.</summary>
            /// <param name="storage">Settings storage.</param>
            public void Load( SettingsStorage storage )
            {
                IsVisible = storage.GetValue( "IsVisible", IsVisible );
                IsEditable = storage.GetValue( "IsEditable", IsEditable );
                X1 = storage.GetValue( "X1", X1 );
                Y1 = storage.GetValue( "Y1", Y1 );
                X2 = storage.GetValue( "X2", X2 );
                Y2 = storage.GetValue( "Y2", Y2 );
                Stroke = storage.GetValue<SettingsStorage>( "Stroke", null ).GetBrush();
                Fill = storage.GetValue<SettingsStorage>( "Fill", null ).GetBrush();
                Foreground = storage.GetValue<SettingsStorage>( "Foreground", null ).GetBrush();
                Thickness = storage.GetValue( "Thickness", Thickness );
                ShowLabel = storage.GetValue( "ShowLabel", ShowLabel );
                LabelPlacement = storage.GetValue( "LabelPlacement", LabelPlacement );
                HorizontalAlignment = storage.GetValue( "HorizontalAlignment", HorizontalAlignment );
                VerticalAlignment = storage.GetValue( "VerticalAlignment", VerticalAlignment );
                CoordinateMode = storage.GetValue( "CoordinateMode", CoordinateMode );
                Text = storage.GetValue( "Text", Text );
            }

            /// <summary>Save settings.</summary>
            /// <param name="storage">Settings storage.</param>
            public void Save( SettingsStorage storage )
            {
                storage.SetValue( "IsVisible", IsVisible );
                storage.SetValue( "IsEditable", IsEditable );
                storage.SetValue( "X1", X1 );
                storage.SetValue( "Y1", Y1 );
                storage.SetValue( "X2", X2 );
                storage.SetValue( "Y2", Y2 );
                storage.SetValue( "Stroke", Stroke.SaveBrush() );
                storage.SetValue( "Fill", Fill.SaveBrush() );
                storage.SetValue( "Foreground", Foreground.SaveBrush() );
                storage.SetValue( "Thickness", Thickness );
                storage.SetValue( "ShowLabel", ShowLabel );
                storage.SetValue( "LabelPlacement", LabelPlacement );
                storage.SetValue( "HorizontalAlignment", HorizontalAlignment );
                storage.SetValue( "VerticalAlignment", VerticalAlignment );
                storage.SetValue( "CoordinateMode", CoordinateMode );
                storage.SetValue( "Text", Text );
            }
        }


        public class sCandleEx : IDrawValue
        {
            public fxHistoricBarsRepo BarRepo { get; set; }
            public uint StartIndex  { get; set; } = uint.MaxValue;
            public uint EndIndex    { get; set; } = 0;

            public sCandleEx( )
            {
                BarRepo = null;

                StartIndex = uint.MaxValue;

                EndIndex = 0;
            }

            public sCandleEx( fxHistoricBarsRepo bars, uint begin, uint end )
            {
                BarRepo = bars;

                StartIndex = begin;

                EndIndex = end;
            }

            public void Add( fxHistoricBarsRepo bars, uint index )
            {
                BarRepo = bars;

                if ( index < StartIndex ) StartIndex = index;

                if ( index > EndIndex ) EndIndex = index;

            }

            public bool IsSet
            {
                get
                {
                    return BarRepo != null;
                }
                
            }

            public int Count
            {
                get
                {
                    if ( EndIndex == 0 ) return 0;
                    return (int)( EndIndex - StartIndex + 1 );
                }
            }
        }


        public struct sCandle : IDrawValue
        {
            private readonly DateTime _utcTime;
            private readonly object _candleArg;
            private readonly double _openPrice;
            private readonly double _highPrice;
            private readonly double _lowPrice;
            private readonly double _closePrice;
            private readonly CandlePriceLevel[ ] _candlePriceLevel;
            private readonly double? _priceStep;
            private readonly IPointMetadata  _advancedTAinfo;

            public sCandle(
                              DateTimeOffset barTime,
                              object arg,
                              Decimal open,
                              Decimal high,
                              Decimal low,
                              Decimal close,
                              IEnumerable<CandlePriceLevel> priceLvls,
                              Decimal? priceStep
                         )
            {
                _utcTime          = barTime.UtcDateTime;
                _candleArg        = arg;
                _openPrice        = ( double ) open;
                _highPrice        = ( double ) high;
                _lowPrice         = ( double ) low;
                _closePrice       = ( double ) close;
                _candlePriceLevel = priceLvls != null ? priceLvls.ToArray() : null;
                _priceStep        = priceStep.HasValue ? new double?( ( double ) priceStep.GetValueOrDefault() ) : new double?();
                _advancedTAinfo   = null;
            }

            //_lastBarTime, bar.Candle.Arg, bar.Open, bar.High, bar.Low, bar.Close, bar.Candle.PriceLevels, ( double )bar.Candle.Security.PriceStep, ( IPointMetadata )bar
            public sCandle( ref SBar bar )
            {
                _utcTime          = bar.BarTime;
                _candleArg        = bar.SymbolEx.Period;
                _openPrice        = bar.Open;
                _highPrice        = bar.High;
                _lowPrice         = bar.Low;
                _closePrice       = bar.Close;
                _candlePriceLevel = null;
                _priceStep        = bar.SymbolEx.PriceStep;
                _advancedTAinfo   = bar;
            }

            public sCandle( DateTimeOffset utcTime, object candleArg, double openPrice, double highPrice, double lowPrice, double closePrice, IEnumerable<CandlePriceLevel> priceLvls, double? priceStep, IPointMetadata advancedTAInfo )
            {
                _utcTime          = utcTime.UtcDateTime;
                _candleArg        = candleArg;
                _openPrice        = openPrice;
                _highPrice        = highPrice;
                _lowPrice         = lowPrice;
                _closePrice       = closePrice;
                _candlePriceLevel = priceLvls != null ? priceLvls.ToArray() : null;
                _priceStep        = priceStep;
                _advancedTAinfo   = advancedTAInfo;
            }


            public DateTime UtcTime()
            {
                return _utcTime;
            }

            public IPointMetadata AdvancedTAInfo()
            {
                return _advancedTAinfo;
            }

            public object CandleArg()
            {
                return _candleArg;
            }


            public double OpenPrice()
            {
                return _openPrice;
            }


            public double HighPrice()
            {
                return _highPrice;
            }


            public double LowPrice()
            {
                return _lowPrice;
            }


            public double ClosePrice()
            {
                return _closePrice;
            }


            public CandlePriceLevel[ ] CandlePriceLevel()
            {
                return _candlePriceLevel;
            }


            public double? PriceStep()
            {
                return _priceStep;
            }
        }
    }
}
