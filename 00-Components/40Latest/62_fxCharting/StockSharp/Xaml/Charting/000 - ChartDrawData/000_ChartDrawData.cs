
using DevExpress.Mvvm.POCO;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
using fx.Bars;
using StockSharp.Algo.Indicators;
using StockSharp.BusinessEntities;
using StockSharp.Charting;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using static DevExpress.XtraPrinting.Export.Pdf.PdfImageCache;

#nullable disable
namespace StockSharp.Xaml.Charting;

/// <summary>Chart drawing data.</summary>
public partial class ChartDrawData : IChartDrawData
{
    /// <summary>Interface which represents all chart draw data types.</summary>
    /// <summary>Indicator values to draw on chart.</summary>

    public interface IDrawValue
    {
    }

    private Dictionary<IChartCandleElement, List<sCandle>> GetCandleMap( )
    {
        return _candleMap ?? ( _candleMap = new Dictionary<IChartCandleElement, List<sCandle>>( ) );
    }

    private Dictionary<IChartCandleElement, List<sCandleColor>> GetCandleColorMap( )
    {
        return _candleColorMap ?? ( _candleColorMap = new Dictionary<IChartCandleElement, List<sCandleColor>>( ) );
    }

    private Dictionary<IChartIndicatorElement, List<IndicatorData>> GetIndicatorMap( )
    {
        return _indicatorMap ?? ( _indicatorMap = new Dictionary<IChartIndicatorElement, List<IndicatorData>>( ) );
    }

    private Dictionary<IChartOrderElement, List<sTrade>> GetOrderMap( )
    {
        return _orderMap ?? ( _orderMap = new Dictionary<IChartOrderElement, List<sTrade>>( ) );
    }

    private Dictionary<IChartTradeElement, List<sTrade>> GetTradeMap( )
    {
        return _tradeMap ?? ( _tradeMap = new Dictionary<IChartTradeElement, List<sTrade>>( ) );
    }

    internal Dictionary<IChartActiveOrdersElement, List<sActiveOrder>> GetActiveOrderMap( )
    {
        return _activeOrdersMap ?? ( _activeOrdersMap = new Dictionary<IChartActiveOrdersElement, List<sActiveOrder>>( ) );
    }

    private Dictionary<IChartLineElement, List<sxTuple<DateTime>>> GetLineTimeMap( )
    {
        return _lineTimeMap ?? ( _lineTimeMap = new Dictionary<IChartLineElement, List<sxTuple<DateTime>>>( ) );
    }

    private Dictionary<IChartLineElement, List<sxTuple<double>>> GetLineValueMap( )
    {
        return _lineValueMap ?? ( _lineValueMap = new Dictionary<IChartLineElement, List<sxTuple<double>>>( ) );
    }

    private Dictionary<IChartBandElement, List<sxTuple<DateTime>>> GetBandTimeMap( )
    {
        return _bandTimeMap ?? ( _bandTimeMap = new Dictionary<IChartBandElement, List<sxTuple<DateTime>>>( ) );
    }

    private Dictionary<IChartBandElement, List<sxTuple<double>>> GetBandValueMap( )
    {
        return _bandValueMap ?? ( _bandValueMap = new Dictionary<IChartBandElement, List<sxTuple<double>>>( ) );
    }

    private Dictionary<IChartAnnotationElement, IAnnotationData> GetAnnotationMap( )
    {
        return _annotationMap ?? ( _annotationMap = new Dictionary<IChartAnnotationElement, IAnnotationData>( ) );
    }



    //internal readonly struct sCandle : IDrawValue
    //{
    //    private readonly DateTime _utcTime;
    //    private readonly DataType _candleArg;
    //    private readonly double _openPrice;
    //    private readonly double _highPrice;

    //    private readonly double _lowPrice;

    //    private readonly double _closePrice;

    //    private readonly CandlePriceLevel[] _candlePriceLevel;

    //    public sCandle( DateTimeOffset _param1, DataType _param2, Decimal _param3, Decimal _param4, Decimal _param5, Decimal _param6, IEnumerable<CandlePriceLevel> _param7 )
    //    {
    //        _utcTime = _param1.UtcDateTime;
    //        _candleArg = _param2 ?? throw new ArgumentNullException( "dataType" );
    //        _openPrice = ( double ) _param3;
    //        _highPrice = ( double ) _param4;
    //        _lowPrice = ( double ) _param5;
    //        _closePrice = ( double ) _param6;
    //        _candlePriceLevel = _param7 != null ? _param7.ToArray<CandlePriceLevel>() : ( CandlePriceLevel[ ] ) null;
    //    }

    //    public DateTime UtcTime()
    //    {
    //        return _utcTime;
    //    }

    //    public DataType CandleDataType() => _candleArg;

    //    public double OpenPrice => _openPrice;

    //    public double HighPrice => _highPrice;

    //    public double LowPrice
    //    {
    //        return _lowPrice;
    //    }

    //    public double Close => _closePrice;

    //    public CandlePriceLevel[ ] CandlePriceLevel
    //    {
    //        return _candlePriceLevel;
    //    }
    //}

    internal readonly struct sCandleColor : IDrawValue
    {

        private readonly DateTime _utcTime;

        private readonly System.Windows.Media.Color? _candleColor;

        public sCandleColor( DateTimeOffset _param1, System.Windows.Media.Color? _param2 )
        {
            _utcTime = _param1.UtcDateTime;
            _candleColor = _param2;
        }

        public DateTime Time
        {
            get
            {
                return _utcTime;
            }
        }

        public System.Windows.Media.Color? Color => _candleColor;
    }

    internal readonly struct sActiveOrder : IDrawValue
    {

        private readonly Order _order;

        private readonly Decimal _balance;

        private readonly OrderStates _orderStates;

        private readonly Decimal _priceStep;

        private readonly bool _autoRemove;

        private readonly bool _isFrozen;

        private readonly bool _isHidden;

        private readonly bool _hasError;

        private readonly Decimal _price;

        public sActiveOrder( Order _param1, Decimal _param2, OrderStates _param3, Decimal _param4, bool _param5, bool _param6, bool _param7, bool _param8, Decimal _param9 )
        {
            Order order = _param1 != null || _param8 ? _param1 : throw new ArgumentException("order can be null only if isError is true");
            if ( order == null )
                order = new Order
                {
                    State = ( OrderStates ) 3,
                    Volume = _param2,
                    Balance = _param2
                };
            _order = order;
            _balance = _param2;
            _orderStates = _param1 == null ? ( OrderStates ) ( object ) 3 : _param3;
            _priceStep = _param4;
            _autoRemove = _param5 || _param1 == null;
            _isFrozen = _param6 || _param1 == null;
            _isHidden = _param7;
            _hasError = _param8;
            _price = _param9;
        }

        public Order Order => _order;


        public Decimal Balance => _balance;


        public OrderStates OrderStates => _orderStates;


        public Decimal PriceStep => _priceStep;


        public bool AutoRemove => _autoRemove;


        public bool IsFrozen => _isFrozen;

        public bool IsHidden => _isHidden;

        public bool IsError => _hasError;

        public Decimal Price => _price;
    }

    public readonly struct IndicatorData : IDrawValue
    {

        private readonly DateTime _utcTime;

        private readonly IIndicatorValue _indicatorValue;

        /// <summary>Create instance.</summary>
        /// <param name="dto">Value timestamp.</param>
        /// <param name="val">Indicator value.</param>
        public IndicatorData( DateTimeOffset dto, IIndicatorValue val )
          : this( dto.UtcDateTime, val )
        {
        }

        internal IndicatorData( DateTime _param1, IIndicatorValue _param2 )
        {
            _utcTime = _param1;
            _indicatorValue = _param2;
        }

        /// <summary>Value timestamp.</summary>
        public DateTime Time => _utcTime;

        /// <summary>Indicator value.</summary>
        public IIndicatorValue Value => _indicatorValue;
    }

    internal sealed class sTrade : IDrawValue
    {

        private string _transactionString;

        private readonly long _transactionId;

        private readonly DateTime _utcTime;

        private readonly Sides _orderSide;

        private readonly double _price;

        private readonly long _volume;

        private readonly string _errorMessage;

        private readonly bool _isOrderFilled;

        public sTrade( DateTimeOffset dto, long transId, string tranStr, Sides side, Decimal price, Decimal volume, string errMsg, bool isOrderFilled )
        {
            _transactionString = transId == 0L ? tranStr : ( string ) null;
            _transactionId     = transId;
            _utcTime           = dto.UtcDateTime;
            _orderSide         = side;
            _price             = ( double ) price;
            _volume            = ( long ) volume;
            _errorMessage      = errMsg;
            _isOrderFilled     = isOrderFilled;
        }

        public DateTime Time
        {
            get
            {
                return _utcTime;
            }
        }

        /// <summary>
        /// Tony Added
        /// </summary>
        public long TradeId
        {
            get
            {
                return _transactionId;
            }
        }

        public string GetTransactionString( )
        {
            return _transactionString ?? ( _transactionString = _transactionId.ToString( ) );
        }

        public Sides OrderSides( ) => _orderSide;

        public double Price => _price;

        public long Volume => _volume;

        public string ErrorMessage( )
        {
            return _errorMessage;
        }

        public bool IsOrderFilled( ) => _isOrderFilled;

        public bool IsError => !StringHelper.IsEmptyOrWhiteSpace( ErrorMessage( ) );

        public override string ToString( )
        {
            return $"{"TransactionData"}: id={GetTransactionString( )}, time={Time} {OrderSides( )} {Volume}@{Volume}";
        }
    }

    public struct sxTuple<T> : IDrawValue where T : struct, IComparable
    {

        private readonly T _property;

        private readonly double _valueOne;

        private readonly double _valueTwo;

        private int _integerValue;

        private sxTuple( double _param1, double _param2, double _param3 )
            : this( ( T ) ( ValueType ) _param1, _param2, _param3 )
        {
        }

        private sxTuple( DateTime _param1, double _param2, double _param3 )
            : this( ( T ) ( ValueType ) _param1, _param2, _param3 )
        {
        }

        private sxTuple( T property, double value1, double value2 )
        {
            _integerValue = 0;
            _property = property;
            _valueOne = value1;
            _valueTwo = value2;
        }

        public T Property( )
        {
            return _property;
        }

        public double ValueOne( )
        {
            return _valueOne;
        }

        public double ValueTwo( )
        {
            return _valueTwo;
        }

        public int GetIntegerValue( ) => _integerValue;

        public void SetIntegerValue( int _param1 )
        {
            _integerValue = _param1;
        }

        public static sxTuple<T> CreateSxTuple<TXOrig>( TXOrig original, double _param1, double _param2 ) where TXOrig : struct, IComparable
        {
            if ( ( ValueType ) original is DateTimeOffset )
                return new sxTuple<T>( Converter.To<DateTimeOffset>( original ).UtcDateTime, _param1, _param2 );

            if ( ( ValueType ) original is double )
                return new sxTuple<T>( Converter.To<double>( ( object ) original ), _param1, _param2 );

            throw new NotSupportedException( StringHelper.Put( LocalizedStrings.UnsupportedType, new object[1]
            {
                 typeof (TXOrig).Name
            } ) );
        }

        public static sxTuple<T> CreateSxTuple( T _param0 )
        {
            return new sxTuple<T>( _param0, double.NaN, double.NaN );
        }

        public static sxTuple<T> CreateSxTuple( T _param0, double _param1, double _param2, int _param3 )
        {
            sxTuple<T> z6MdlWkBsH4 = new sxTuple<T>(_param0, _param1, _param2);
            z6MdlWkBsH4.SetIntegerValue( _param3 );
            return z6MdlWkBsH4;
        }
    }

    public class AnnotationData : IDrawValue, IPersistable, IAnnotationData
    {

        private bool? _isVisible = new bool?(true);

        private bool? _isEditable;

        private IComparable _x1;

        private IComparable _y1;

        private IComparable _x2;

        private IComparable _y2;

        private System.Windows.Media.Brush _strokeBrush;

        private System.Windows.Media.Brush _fillBrush;

        private System.Windows.Media.Brush _foregroundBrush;

        private System.Windows.Thickness? _thickness;

        private bool? _ShowLabel;

        private StockSharp.Charting.LabelPlacement? _LabelPlacement;

        private System.Windows.HorizontalAlignment? _HorizontalAlignment;

        private System.Windows.VerticalAlignment? _VerticalAlignment;

        private StockSharp.Charting.AnnotationCoordinateMode? _CoordinateMode;

        private string _Text;

        /// <inheritdoc />
        public bool? IsVisible
        {
            get => _isVisible;
            set => _isVisible = value;
        }

        /// <inheritdoc />
        public bool? IsEditable
        {
            get => _isEditable;
            set => _isEditable = value;
        }

        /// <inheritdoc />
        public IComparable X1
        {
            get => _x1;
            set => _x1 = value;
        }

        /// <inheritdoc />
        public IComparable Y1
        {
            get => _y1;
            set => _y1 = value;
        }

        /// <inheritdoc />
        public IComparable X2
        {
            get => _x2;
            set => _x2 = value;
        }

        /// <inheritdoc />
        public IComparable Y2
        {
            get => _y2;
            set => _y2 = value;
        }

        /// <summary>Brush to draw lines and borders.</summary>
        public System.Windows.Media.Brush Stroke
        {
            get => _strokeBrush;
            set => _strokeBrush = value;
        }

        /// <summary>Brush to fill background.</summary>
        public System.Windows.Media.Brush Fill
        {
            get => _fillBrush;
            set => _fillBrush = value;
        }

        /// <summary>Brush to fill background.</summary>
        public System.Windows.Media.Brush Foreground
        {
            get => _foregroundBrush;
            set => _foregroundBrush = value;
        }

        /// <summary>Line thickness.</summary>
        public System.Windows.Thickness? Thickness
        {
            get => _thickness;
            set => _thickness = value;
        }

        /// <inheritdoc />
        public bool? ShowLabel
        {
            get => _ShowLabel;
            set => _ShowLabel = value;
        }

        /// <inheritdoc />
        public LabelPlacement? LabelPlacement
        {
            get => _LabelPlacement;
            set => _LabelPlacement = value;
        }

        /// <summary>Alignment for horizontal lines.</summary>
        public System.Windows.HorizontalAlignment? HorizontalAlignment
        {
            get => _HorizontalAlignment;
            set => _HorizontalAlignment = value;
        }

        /// <summary>Alignment for vertical lines.</summary>
        public System.Windows.VerticalAlignment? VerticalAlignment
        {
            get => _VerticalAlignment;
            set => _VerticalAlignment = value;
        }

        /// <inheritdoc />
        public AnnotationCoordinateMode? CoordinateMode
        {
            get => _CoordinateMode;
            set => _CoordinateMode = value;
        }

        /// <inheritdoc />
        public string Text
        {
            get => _Text;
            set => _Text = value;
        }

        Ecng.Drawing.Brush IAnnotationData.Stroke
        {
            get => Stroke.FromWpf( );
            set => Stroke = value.ToWpf( );
        }

        Ecng.Drawing.Brush IAnnotationData.Fill
        {
            get => Fill.FromWpf( );
            set => Fill = value.ToWpf( );
        }

        Ecng.Drawing.Brush IAnnotationData.Foreground
        {
            get => Foreground.FromWpf( );
            set => Foreground = value.ToWpf( );
        }

        Ecng.Drawing.Thickness? IAnnotationData.Thickness
        {
            get
            {                
                return !Thickness.HasValue ? new Ecng.Drawing.Thickness?( ) : new Ecng.Drawing.Thickness?( Thickness.GetValueOrDefault( ).FromWpf( ) );
            }
            set
            {
                Thickness = value.HasValue ? new System.Windows.Thickness?( value.GetValueOrDefault( ).ToWpf( ) ) : new System.Windows.Thickness?( );
            }
        }

        Ecng.Drawing.HorizontalAlignment? IAnnotationData.HorizontalAlignment
        {
            get
            {                
                return !HorizontalAlignment.HasValue ? new Ecng.Drawing.HorizontalAlignment?( ) : new Ecng.Drawing.HorizontalAlignment?( HorizontalAlignment.GetValueOrDefault( ).FromWpf( ) );
            }
            set
            {
                HorizontalAlignment = value.HasValue ? new System.Windows.HorizontalAlignment?( value.GetValueOrDefault( ).ToWpf( ) ) : new System.Windows.HorizontalAlignment?( );
            }
        }

        Ecng.Drawing.VerticalAlignment? IAnnotationData.VerticalAlignment
        {
            get
            {                
                return !VerticalAlignment.HasValue ? new Ecng.Drawing.VerticalAlignment?( ) : new Ecng.Drawing.VerticalAlignment?( VerticalAlignment.GetValueOrDefault( ).FromWpf( ) );
            }
            set
            {
                VerticalAlignment = value.HasValue ? new System.Windows.VerticalAlignment?( value.GetValueOrDefault( ).ToWpf( ) ) : new System.Windows.VerticalAlignment?( );
            }
        }

        internal static IComparable ConvertToUTC( IComparable input )
        {
            return input is DateTime dateTime ? ( IComparable ) TimeHelper.UtcKind( dateTime ) : input;
        }

        internal static IComparable ConvertToUniversalTime( IComparable input )
        {
            switch ( input )
            {
                case DateTimeOffset dto:
                    return ( IComparable ) dto.UtcDateTime;

                case DateTime dt:
                    return ( IComparable ) dt.ToUniversalTime( );

                default:
                    return input;
            }
        }

        /// <summary>Load settings.</summary>
        /// <param name="storage">Settings storage.</param>
        public void Load( SettingsStorage storage )
        {
            IsVisible    = storage.GetValue<bool?>( "IsVisible", IsVisible );
            IsEditable   = storage.GetValue<bool?>( "IsEditable", IsEditable );
            X1           = AnnotationData.ConvertToUTC( storage.GetValue<IComparable>( "X1", X1 ) );
            Y1           = storage.GetValue<IComparable>( "Y1", Y1 );
            X2           = AnnotationData.ConvertToUTC( storage.GetValue<IComparable>( ".X2", X2 ) );
            Y2           = storage.GetValue<IComparable>( "Y2", Y2 );
            var strokeST = storage.GetValue<SettingsStorage>("Stroke", null);
            Stroke       = strokeST != null ? strokeST.GetBrush( ) : null;
            var fillST   = storage.GetValue<SettingsStorage>("Fill", null);
            Fill         = fillST != null ? fillST.GetBrush( ) : null;
            var fgST     = storage.GetValue<SettingsStorage>("Foreground", null);
            Foreground   = fgST != null ? fgST.GetBrush( ) : null;

            try
            {
                var thickST = storage.GetValue<SettingsStorage>("Thickness", null);
                Thickness = thickST != null ? new System.Windows.Thickness?( thickST.CreateThickness( ) ) : new System.Windows.Thickness?( );
            }
            catch
            {
            }

            ShowLabel           = storage.GetValue<bool?>( "ShowLabel", ShowLabel );
            LabelPlacement      = storage.GetValue<LabelPlacement?>( "LabelPlacement", LabelPlacement );
            HorizontalAlignment = storage.GetValue<System.Windows.HorizontalAlignment?>( "HorizontalAlignment", HorizontalAlignment );
            VerticalAlignment   = storage.GetValue<System.Windows.VerticalAlignment?>( "VerticalAlignment", VerticalAlignment );
            CoordinateMode      = storage.GetValue<AnnotationCoordinateMode?>( "CoordinateMode", CoordinateMode );
            Text                = storage.GetValue<string>( "Text", Text );
        }



        /// <summary>Save settings.</summary>
        /// <param name="storage">Settings storage.</param>
        public void Save( SettingsStorage storage )
        {
            storage.SetValue( "IsVisible" , IsVisible                                                 );
            storage.SetValue( "IsEditable", IsEditable                                                );
            storage.SetValue( "X1"        , AnnotationData.ConvertToUniversalTime( X1 ) );
            storage.SetValue( "Y1"        , Y1                                                        );
            storage.SetValue( "X2"        , AnnotationData.ConvertToUniversalTime( X2 ) );
            storage.SetValue( "Y2"        , Y2                                                        );
                        
            var strokeST = Stroke != null ? Stroke.SaveSettings() : null;
            storage.SetValue( "Stroke", strokeST );
                        
            var fillST = Fill != null ? Fill.SaveSettings() : null;
            storage.SetValue( "Fill", fillST );
                        
            var fgST = Foreground != null ? Foreground.SaveSettings() : null;
            storage.SetValue( "Foreground", fgST );

            var thickST = Thickness.HasValue ? Thickness.GetValueOrDefault().SaveSettings() : null;
            storage.SetValue( "Thickness"          , thickST             );
            storage.SetValue( "ShowLabel"          , ShowLabel           );
            storage.SetValue( "LabelPlacement"     , LabelPlacement      );
            storage.SetValue( "HorizontalAlignment", HorizontalAlignment );
            storage.SetValue( "VerticalAlignment"  , VerticalAlignment   );
            storage.SetValue( "CoordinateMode"     , CoordinateMode      );
            storage.SetValue( "Text"               , Text                );
        }
    }

    private Dictionary<IChartCandleElement, List<sCandle>>            _candleMap;

    private Dictionary<IChartCandleElement, List<sCandleColor>>       _candleColorMap;

    private Dictionary<IChartIndicatorElement, List<IndicatorData>>   _indicatorMap;

    private Dictionary<IChartOrderElement, List<sTrade>>              _orderMap;

    private Dictionary<IChartTradeElement, List<sTrade>>              _tradeMap;

    private Dictionary<IChartActiveOrdersElement, List<sActiveOrder>> _activeOrdersMap;

    private Dictionary<IChartLineElement, List<sxTuple<DateTime>>>    _lineTimeMap;

    private Dictionary<IChartLineElement, List<sxTuple<double>>>      _lineValueMap;

    private Dictionary<IChartBandElement, List<sxTuple<DateTime>>>    _bandTimeMap;

    private Dictionary<IChartBandElement, List<sxTuple<double>>>      _bandValueMap;

    private Dictionary<IChartAnnotationElement, IAnnotationData>                    _annotationMap;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Xaml.Charting.ChartDrawDataEx" />.
    /// </summary>
    public ChartDrawData( )
    {
    }

    internal ChartDrawData( IEnumerable<RefPair<DateTimeOffset, IDictionary<IChartElement, object>>> drawDatas )
    {
        if ( drawDatas == null )
            throw new ArgumentNullException( "" );

        foreach ( var data in drawDatas )
        {
            DateTimeOffset barTime = data.First;
            foreach ( KeyValuePair<IChartElement, object> keyValuePair in ( IEnumerable<KeyValuePair<IChartElement, object>> ) data.Second )
            {
                IChartElement ui = keyValuePair.Key;
                object obj = keyValuePair.Value;
                
                switch ( ui )
                {
                    case IChartCandleElement candlestickUI:
                        if ( obj is ICandleMessage icandleMessage )
                        {
                            CollectionHelper.SafeAdd( GetCandleMap( ), candlestickUI ).Add( new sCandle( barTime, icandleMessage.DataType, icandleMessage.OpenPrice, icandleMessage.HighPrice, icandleMessage.LowPrice, icandleMessage.ClosePrice, icandleMessage.PriceLevels ) );
                            continue;
                        }
                        
                        if ( obj is System.Windows.Media.Color color )
                        {
                            CollectionHelper.SafeAdd( GetCandleColorMap( ), candlestickUI ).Add( new sCandleColor( barTime, new System.Windows.Media.Color?( color ) ) );
                            continue;
                        }
                        continue;

                    case IChartIndicatorElement indicatorUI:
                        IIndicatorValue indValue = (IIndicatorValue)obj;
                        CollectionHelper.SafeAdd( GetIndicatorMap( ), indicatorUI ).Add( new IndicatorData( barTime, indValue ) );
                        continue;

                    case IChartOrderElement orderUI:
                        Order order = (Order)obj;
                        CollectionHelper.SafeAdd( GetOrderMap( ), orderUI ).Add( new sTrade( barTime, order.TransactionId, null, order.Side, order.Price, order.Volume, order.State != OrderStates.Failed ? null : LocalizedStrings.Failed, true ) );
                        continue;

                    case IChartTradeElement tradeUI:
                        MyTrade myTrade = (MyTrade)obj;
                        var trade = myTrade.Trade;
                        CollectionHelper.SafeAdd( GetTradeMap( ), tradeUI ).Add( new sTrade( barTime, trade.Id.GetValueOrDefault( ), trade.StringId, myTrade.Order.Side, trade.Price, trade.Volume, null, false ) );
                        continue;

                    case IChartActiveOrdersElement activeOrderUI:
                        CollectionHelper.SafeAdd( GetActiveOrderMap( ), activeOrderUI ).Add( ( sActiveOrder ) obj );
                        continue;

                    default:
                        throw new ArgumentException( StringHelper.Put( LocalizedStrings.UnknownType, ui ) );
                }
            }
        }
    }

    public sealed class ChartDrawDataItem : IChartDrawData.IChartDrawDataItem
    {

        private readonly ChartDrawData _drawData;

        private readonly DateTimeOffset _timeStamp;

        private readonly double _xValue;

        internal ChartDrawDataItem( ChartDrawData drawData, DateTimeOffset dto )
        {
            _drawData = drawData ?? throw new ArgumentNullException( "" );
            _timeStamp = dto;
            _xValue = double.NaN;
        }

        internal ChartDrawDataItem( ChartDrawData _param1, double _param2 )
        {
            _drawData = _param1 ?? throw new ArgumentNullException( "" );
            _xValue = _param2;
        }

        /// <summary>The time stamp of the new data generation.</summary>
        public DateTimeOffset TimeStamp => _timeStamp;

        /// <summary>
        /// Value of X coordinate for <see cref="T:StockSharp.Xaml.Charting.ChartLineElement" />.
        /// </summary>
        public double XValue => _xValue;

        /// <inheritdoc />
        public IChartDrawData.IChartDrawDataItem Add( IChartCandleElement element, System.Drawing.Color? color )
        {
            return Add( element, color.HasValue ? new System.Windows.Media.Color?( color.GetValueOrDefault( ).ToWpf( ) ) : new System.Windows.Media.Color?( ) );
        }

        /// <inheritdoc />
        public IChartDrawData.IChartDrawDataItem Add( IChartCandleElement element, System.Windows.Media.Color? color )
        {
            return GetMap( _drawData.GetCandleColorMap( ), element, new sCandleColor( TimeStamp, color ) );
        }

        /// <inheritdoc />
        public IChartDrawData.IChartDrawDataItem Add( IChartCandleElement element, DataType dataType, SecurityId secId, Decimal openPrice, Decimal highPrice, Decimal lowPrice, Decimal closePrice, CandlePriceLevel[ ] priceLevels, CandleStates _ )
        {
            return GetMap( _drawData.GetCandleMap( ), element, new sCandle( TimeStamp, dataType, openPrice, highPrice, lowPrice, closePrice, ( IEnumerable<CandlePriceLevel> ) priceLevels ) );
        }

        /// <summary>Put candle color data.</summary>
        /// <param name="element">The chart element representing a candle.</param>
        /// <param name="color">Candle draw color.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Xaml.Charting.ChartDrawDataItem" /> instance.</returns>
        public ChartDrawDataItem Add( IChartCandleElement candleUI, fxHistoricBarsRepo barList, uint barIndex )
        {
            _drawData.GetMyCandleMap( ).SafeAdd( candleUI ).Add( barList, barIndex );

            return this;
        }


        /// <summary>Put the indicator data.</summary>
        /// <param name="element">The chart element representing the indicator.</param>
        /// <param name="value">The indicator value.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Xaml.Charting.ChartDrawDataItem" /> instance.</returns>
        public ChartDrawDataItem Add( IChartIndicatorElement indicatorUI, IIndicator indicator, IIndicatorValue value )
        {
            if ( value == null )
                return this;

            var indicatorMap = _drawData.GetMyIndicatorMap();

            if ( indicatorMap.ContainsKey( indicatorUI ) )
            {
                var indicatorList = indicatorMap[ indicatorUI ];
                indicatorList.SetIndicatorValue( TimeStamp, value );
            }
            else
            {
                throw new InvalidCastException( );
            }

            //return Add<ChartIndicatorElement, sIndicator>( , element, new sIndicator( TimeStamp, value ) );

            return this;
        }

        /// <inheritdoc />
        public IChartDrawData.IChartDrawDataItem Add( IChartIndicatorElement element, IIndicatorValue value )
        {
            return value == null ? this : GetMap( _drawData.GetIndicatorMap( ), element, new IndicatorData( TimeStamp, value ) );
        }

        /// <inheritdoc />
        public IChartDrawData.IChartDrawDataItem Add( IChartOrderElement element, long orderId, string orderStringId, Sides side, Decimal price, Decimal volume, string errorMessage = null )
        {
            return GetMap( _drawData.GetOrderMap( ), element, new sTrade( TimeStamp, orderId, orderStringId, side, price, volume, errorMessage, true ) );
        }

        /// <inheritdoc />
        public IChartDrawData.IChartDrawDataItem Add( IChartTradeElement element, long tradeId, string tradeStringId, Sides side, Decimal price, Decimal volume )
        {
            return GetMap( _drawData.GetTradeMap( ), element, new sTrade( TimeStamp, tradeId, tradeStringId, side, price, volume, ( string ) null, false ) );
        }

        /// <inheritdoc />
        public IChartDrawData.IChartDrawDataItem Add( IChartLineElement element, double value1, double value2 = double.NaN )
        {
            return !MathHelper.IsNaN( XValue ) ? GetMap( _drawData.GetLineValueMap( ), element, sxTuple<double>.CreateSxTuple<double>( XValue, value1, value2 ) ) :  GetMap( _drawData.GetLineTimeMap( ), element, sxTuple<DateTime>.CreateSxTuple<DateTimeOffset>( TimeStamp, value1, value2 ) );
        }

        /// <inheritdoc />
        public IChartDrawData.IChartDrawDataItem Add( IChartBandElement element, Decimal value )
        {
            return Add( element, ( double ) value, 0.0 );
        }

        /// <inheritdoc />
        public IChartDrawData.IChartDrawDataItem Add( IChartBandElement element, double value1, double value2 )
        {
            return !MathHelper.IsNaN( XValue ) ? GetMap( _drawData.GetBandValueMap( ), element, sxTuple<double>.CreateSxTuple<double>( XValue, value1, value2 ) ) : GetMap( _drawData.GetBandTimeMap( ), element, sxTuple<DateTime>.CreateSxTuple<DateTimeOffset>( TimeStamp, value1, value2 ) );
        }

        private ChartDrawDataItem GetMap<TElement, TValue>( Dictionary<TElement, List<TValue>> _param1, TElement _param2, TValue _param3 )
        {
            CollectionHelper.SafeAdd( _param1, _param2 ).Add( _param3 );
            return this;
        }
    }

    /// <inheritdoc />
    public IChartDrawData.IChartDrawDataItem Group( DateTimeOffset timeStamp )
    {
        return new ChartDrawDataItem( this, timeStamp );
    }

    /// <inheritdoc />
    public IChartDrawData.IChartDrawDataItem Group( double xValue )
    {
        return new ChartDrawDataItem( this, xValue );
    }

    /// <inheritdoc />
    public IChartDrawData Add( IChartAnnotationElement element, IAnnotationData data )
    {
        var annotMap = GetAnnotationMap();        
        
        annotMap[element] = data ?? throw new ArgumentNullException( "" );

        return ( IChartDrawData ) this;
    }

    /// <inheritdoc />
    public IChartDrawData Add( IChartActiveOrdersElement element, Order order, bool? isFrozen = null, bool autoRemoveFromChart = true, bool isHidden = false, bool? isError = null, Decimal? price = null, Decimal? balance = null, OrderStates? state = null )
    {
        state.GetValueOrDefault( );
        if ( !state.HasValue )
            state = order != null ? order.State : OrderStates.None;

        Decimal valueOrDefault = price.GetValueOrDefault();
        if ( !price.HasValue )
        {
            price = order != null ? order.Price : throw new ArgumentException( "Order is NUll" );
        }
        valueOrDefault = balance.GetValueOrDefault( );
        if ( !balance.HasValue )
            balance = new Decimal?( order != null ? order.Balance : 0M );
        isFrozen.GetValueOrDefault( );
        
        if ( !isFrozen.HasValue )
        {                        
            isFrozen = new bool?( state.GetValueOrDefault( ) == OrderStates.None & state.HasValue || state.GetValueOrDefault( ) == OrderStates.Pending );
        }
        CollectionHelper.SafeAdd( GetActiveOrderMap( ), element ).Add( new sActiveOrder( order, balance.Value, state.Value, ( Decimal? ) order?.Security?.PriceStep ?? 0.01M, autoRemoveFromChart, isFrozen.Value, isHidden, ( isError ?? ( state.GetValueOrDefault( ) == OrderStates.Failed ? true : false ) ) != false, price.Value ) );
        return ( IChartDrawData ) this;
    }

    internal List<sCandle> GetCandleRelatedData( IChartCandleElement candle )
    {        
        return _candleMap == null ? null : CollectionHelper.TryGetValue( _candleMap, candle );
    }

    internal List<sCandleColor> GetCandleColor( IChartCandleElement candleColor )
    {        
        return _candleColorMap == null ? null : CollectionHelper.TryGetValue( _candleColorMap, candleColor );
    }

    internal List<IndicatorData> GetCandleRelatedData( IChartIndicatorElement indicator )
    {        
        return _indicatorMap == null ?  null : CollectionHelper.TryGetValue( _indicatorMap, indicator );
    }

    internal List<sTrade> GetCandleRelatedData( IChartOrderElement order )
    {        
        return _orderMap == null ? null : CollectionHelper.TryGetValue( _orderMap, order );
    }

    internal List<sTrade> GetCandleRelatedData( IChartTradeElement trade )
    {        
        return _tradeMap == null ? null : CollectionHelper.TryGetValue( _tradeMap, trade );
    }

    internal List<sActiveOrder> GetCandleRelatedData( IChartActiveOrdersElement ao )
    {        
        return _activeOrdersMap == null ? null : CollectionHelper.TryGetValue( _activeOrdersMap, ao );
    }

    internal List<sxTuple<DateTime>> GetLineTime( IChartLineElement line )
    {        
        return _lineTimeMap == null ? null : CollectionHelper.TryGetValue( _lineTimeMap, line );
    }

    internal List<sxTuple<double>> GetLineValue( IChartLineElement line )
    {
        
        return _lineValueMap == null ? null : CollectionHelper.TryGetValue( _lineValueMap, line );
    }

    internal List<sxTuple<DateTime>> GetLineTime( IChartBandElement band )
    {
        
        return _bandTimeMap == null ? null : CollectionHelper.TryGetValue( _bandTimeMap, band );
    }

    internal List<sxTuple<double>> GetLineValue( IChartBandElement band )
    {        
        return _bandValueMap == null ? null : CollectionHelper.TryGetValue( _bandValueMap, band );
    }

    internal IEnumerableEx<IDrawValue> GetCandleRelatedData( IChartLineElement line )
    {
        List<sxTuple<DateTime>> source1 = GetLineTime(line);
        if ( source1 != null && source1.Count > 0 )
            return CollectionHelper.ToEx<IDrawValue>( source1.Cast<IDrawValue>( ), source1.Count );
        List<sxTuple<double>> source2 = GetLineValue(line);
        return source2 == null ? null : CollectionHelper.ToEx<IDrawValue>( source2.Cast<IDrawValue>( ), source2.Count );
    }

    internal IEnumerableEx<IDrawValue> GetCandleRelatedData( IChartBandElement band )
    {
        var lineTime = GetLineTime(band);
        if ( lineTime != null && lineTime.Count > 0 )
            return CollectionHelper.ToEx<IDrawValue>( lineTime.Cast<IDrawValue>( ), lineTime.Count );
        
        var lineValue = GetLineValue(band);

        return lineValue == null ? null : CollectionHelper.ToEx<IDrawValue>( lineValue.Cast<IDrawValue>( ), lineValue.Count );
    }

    internal AnnotationData GetAnnotation( IChartAnnotationElement annot )
    {        
        return _annotationMap != null ? ( AnnotationData ) CollectionHelper.TryGetValue( _annotationMap, annot ) : null;
    }

}