
using DevExpress.Mvvm.POCO;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
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
public class ChartDrawData : IChartDrawData
{
    /// <summary>Interface which represents all chart draw data types.</summary>
    /// <summary>Indicator values to draw on chart.</summary>

    public interface IDrawValue
    {
    }

    private Dictionary<IChartCandleElement, List<ChartDrawData.sCandle>> GetCandleMap()
    {
        return _candleMap ?? ( _candleMap = new Dictionary<IChartCandleElement, List<ChartDrawData.sCandle>>() );
    }

    private Dictionary<IChartCandleElement, List<ChartDrawData.sCandleColor>> GetCandleColorMap()
    {
        return _candleColorMap ?? ( _candleColorMap = new Dictionary<IChartCandleElement, List<ChartDrawData.sCandleColor>>() );
    }

    private Dictionary<IChartIndicatorElement, List<ChartDrawData.IndicatorData>> GetIndicatorMap()
    {
        return _indicatorMap ?? ( _indicatorMap = new Dictionary<IChartIndicatorElement, List<ChartDrawData.IndicatorData>>() );
    }

    private Dictionary<IChartOrderElement, List<ChartDrawData.sTrade>> GetOrderMap()
    {
        return _orderMap ?? ( _orderMap = new Dictionary<IChartOrderElement, List<ChartDrawData.sTrade>>() );
    }

    private Dictionary<IChartTradeElement, List<ChartDrawData.sTrade>> GetTradeMap()
    {
        return _tradeMap ?? ( _tradeMap = new Dictionary<IChartTradeElement, List<ChartDrawData.sTrade>>() );
    }

    private Dictionary<IChartActiveOrdersElement, List<ChartDrawData.sActiveOrder>> GetActiveOrderMap()
    {
        return _activeOrdersMap ?? ( _activeOrdersMap = new Dictionary<IChartActiveOrdersElement, List<ChartDrawData.sActiveOrder>>() );
    }

    private Dictionary<IChartLineElement, List<ChartDrawData.sxTuple<DateTime>>> GetLineTimeMap()
    {
        return _lineTimeMap ?? ( _lineTimeMap = new Dictionary<IChartLineElement, List<ChartDrawData.sxTuple<DateTime>>>() );
    }

    private Dictionary<IChartLineElement, List<ChartDrawData.sxTuple<double>>> GetLineValueMap()
    {
        return _lineValueMap ?? ( _lineValueMap = new Dictionary<IChartLineElement, List<ChartDrawData.sxTuple<double>>>() );
    }

    private Dictionary<IChartBandElement, List<ChartDrawData.sxTuple<DateTime>>> GetBandTimeMap()
    {
        return _bandTimeMap ?? ( _bandTimeMap = new Dictionary<IChartBandElement, List<ChartDrawData.sxTuple<DateTime>>>() );
    }

    private Dictionary<IChartBandElement, List<ChartDrawData.sxTuple<double>>> GetBandValueMap()
    {
        return _bandValueMap ?? ( _bandValueMap = new Dictionary<IChartBandElement, List<ChartDrawData.sxTuple<double>>>() );
    }

    private Dictionary<IChartAnnotationElement, IAnnotationData> GetAnnotationMap()
    {
        return _annotationMap ?? ( _annotationMap = new Dictionary<IChartAnnotationElement, IAnnotationData>() );
    }



    internal readonly struct sCandle : ChartDrawData.IDrawValue
    {
        private readonly DateTime _utcTime;
        private readonly DataType _candleArg;
        private readonly double _openPrice;
        private readonly double _highPrice;

        private readonly double _lowPrice;

        private readonly double _closePrice;

        private readonly CandlePriceLevel[] _candlePriceLevel;

        public sCandle( DateTimeOffset _param1, DataType _param2, Decimal _param3, Decimal _param4, Decimal _param5, Decimal _param6, IEnumerable<CandlePriceLevel> _param7 )
        {
            _utcTime = _param1.UtcDateTime;
            _candleArg = _param2 ?? throw new ArgumentNullException( "dataType" );
            _openPrice = ( double ) _param3;
            _highPrice = ( double ) _param4;
            _lowPrice = ( double ) _param5;
            _closePrice = ( double ) _param6;
            _candlePriceLevel = _param7 != null ? _param7.ToArray<CandlePriceLevel>() : ( CandlePriceLevel[ ] ) null;
        }

        public DateTime UtcTime()
        {
            return _utcTime;
        }

        public DataType CandleDataType() => _candleArg;

        public double OpenPrice() => _openPrice;

        public double HighPrice() => _highPrice;

        public double LowPrice()
        {
            return _lowPrice;
        }

        public double Close => _closePrice;

        public CandlePriceLevel[ ] CandlePriceLevel()
        {
            return _candlePriceLevel;
        }
    }

    internal readonly struct sCandleColor :
    ChartDrawData.IDrawValue
    {

        private readonly DateTime _utcTime;

        private readonly System.Windows.Media.Color? _candleColor;

        public sCandleColor( DateTimeOffset _param1, System.Windows.Media.Color? _param2 )
        {
            _utcTime = _param1.UtcDateTime;
            _candleColor = _param2;
        }

        public DateTime UtcTime()
        {
            return _utcTime;
        }

        public System.Windows.Media.Color? Color => _candleColor;
    }

    internal readonly struct sActiveOrder : ChartDrawData.IDrawValue
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
                order = new Order()
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

        public Order Order()
        {
            return _order;
        }

        public Decimal Balance()
        {
            return _balance;
        }

        public OrderStates OrderStates()
        {
            return _orderStates;
        }

        public Decimal PriceStep()
        {
            return _priceStep;
        }

        public bool AutoRemoveFromChart()
        {
            return _autoRemove;
        }

        public bool IsFrozen() => _isFrozen;

        public bool IsHidden() => _isHidden;

        public bool IsError => _hasError;

        public Decimal Price() => _price;
    }

    public readonly struct IndicatorData : ChartDrawData.IDrawValue
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

    internal sealed class sTrade : ChartDrawData.IDrawValue
    {

        private string _transactionString;

        private readonly long _transactionId;

        private readonly DateTime _utcTime;

        private readonly Sides _orderSide;

        private readonly double _price;

        private readonly long _volume;

        private readonly string _errorMessage;

        private readonly bool _isOrderFilled;

        public sTrade( DateTimeOffset _param1, long _param2, string _param3, Sides _param4, Decimal _param5, Decimal _param6, string _param7, bool _param8 )
        {
            _transactionString = _param2 == 0L ? _param3 : ( string ) null;
            _transactionId = _param2;
            _utcTime = _param1.UtcDateTime;
            _orderSide = _param4;
            _price = ( double ) _param5;
            _volume = ( long ) _param6;
            _errorMessage = _param7;
            _isOrderFilled = _param8;
        }

        public DateTime UtcTime()
        {
            return _utcTime;
        }

        public string GetTransactionString()
        {
            return _transactionString ?? ( _transactionString = _transactionId.ToString() );
        }

        public Sides OrderSides() => _orderSide;

        public double Price() => _price;

        public long Volume => _volume;

        public string ErrorMessage()
        {
            return _errorMessage;
        }

        public bool IsOrderFilled() => _isOrderFilled;

        public bool IsError => !StringHelper.IsEmptyOrWhiteSpace( ErrorMessage() );

        public override string ToString()
        {
            return $"{"TransactionData"}: id={GetTransactionString()}, time={UtcTime()} {OrderSides()} {Volume}@{Volume}";
        }
    }

    public struct sxTuple<T> : ChartDrawData.IDrawValue where T : struct, IComparable
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

        private sxTuple(
            T _param1,
            double _param2,
            double _param3 )
        {
            _integerValue = 0;
            _property = _param1;
            _valueOne = _param2;
            _valueTwo = _param3;
        }

        public T Property()
        {
            return _property;
        }

        public double ValueOne()
        {
            return _valueOne;
        }

        public double ValueTwo()
        {
            return _valueTwo;
        }

        public int GetIntegerValue() => _integerValue;

        public void SetIntegerValue( int _param1 )
        {
            _integerValue = _param1;
        }

        public static ChartDrawData.sxTuple<T> CreateSxTuple<TXOrig>( TXOrig _param0, double _param1, double _param2 ) where TXOrig : struct, IComparable
        {
            if ( ( ValueType ) _param0 is DateTimeOffset )
                return new ChartDrawData.sxTuple<T>( Converter.To<DateTimeOffset>( _param0 ).UtcDateTime, _param1, _param2 );
            if ( ( ValueType ) _param0 is double )
                return new ChartDrawData.sxTuple<T>( Converter.To<double>( ( object ) _param0 ), _param1, _param2 );
            throw new NotSupportedException( StringHelper.Put( LocalizedStrings.UnsupportedType, new object[ 1 ]
            {
                 typeof (TXOrig).Name
            } ) );
        }

        public static ChartDrawData.sxTuple<T> CreateSxTuple( T _param0 )
        {
            return new ChartDrawData.sxTuple<T>( _param0, double.NaN, double.NaN );
        }

        public static ChartDrawData.sxTuple<T> CreateSxTuple( T _param0, double _param1, double _param2, int _param3 )
        {
            ChartDrawData.sxTuple<T> z6MdlWkBsH4 = new ChartDrawData.sxTuple<T>(_param0, _param1, _param2);
            z6MdlWkBsH4.SetIntegerValue( _param3 );
            return z6MdlWkBsH4;
        }
    }

    public class AnnotationData : ChartDrawData.IDrawValue, IPersistable, IAnnotationData
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

        private LabelPlacement? _LabelPlacement;

        private System.Windows.HorizontalAlignment? _HorizontalAlignment;

        private System.Windows.VerticalAlignment? _VerticalAlignment;

        private AnnotationCoordinateMode? _CoordinateMode;

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
            get => Stroke.FromWpf();
            set => Stroke = value.ToWpf();
        }

        Ecng.Drawing.Brush IAnnotationData.Fill
        {
            get => Fill.FromWpf();
            set => Fill = value.ToWpf();
        }

        Ecng.Drawing.Brush IAnnotationData.Foreground
        {
            get => Foreground.FromWpf();
            set => Foreground = value.ToWpf();
        }

        Ecng.Drawing.Thickness? IAnnotationData.Thickness
        {
            get
            {
                System.Windows.Thickness? thickness = Thickness;
                ref System.Windows.Thickness? local = ref thickness;
                return !local.HasValue ? new Ecng.Drawing.Thickness?() : new Ecng.Drawing.Thickness?( local.GetValueOrDefault().FromWpf() );
            }
            set
            {
                Thickness = value.HasValue ? new System.Windows.Thickness?( value.GetValueOrDefault().ToWpf() ) : new System.Windows.Thickness?();
            }
        }

        Ecng.Drawing.HorizontalAlignment? IAnnotationData.HorizontalAlignment
        {
            get
            {
                System.Windows.HorizontalAlignment? horizontalAlignment = HorizontalAlignment;
                ref System.Windows.HorizontalAlignment? local = ref horizontalAlignment;
                return !local.HasValue ? new Ecng.Drawing.HorizontalAlignment?() : new Ecng.Drawing.HorizontalAlignment?( local.GetValueOrDefault().FromWpf() );
            }
            set
            {
                HorizontalAlignment = value.HasValue ? new System.Windows.HorizontalAlignment?( value.GetValueOrDefault().ToWpf() ) : new System.Windows.HorizontalAlignment?();
            }
        }

        Ecng.Drawing.VerticalAlignment? IAnnotationData.VerticalAlignment
        {
            get
            {
                System.Windows.VerticalAlignment? verticalAlignment = VerticalAlignment;
                ref System.Windows.VerticalAlignment? local = ref verticalAlignment;
                return !local.HasValue ? new Ecng.Drawing.VerticalAlignment?() : new Ecng.Drawing.VerticalAlignment?( local.GetValueOrDefault().FromWpf() );
            }
            set
            {
                VerticalAlignment = value.HasValue ? new System.Windows.VerticalAlignment?( value.GetValueOrDefault().ToWpf() ) : new System.Windows.VerticalAlignment?();
            }
        }

        internal static IComparable ConvertToUTC( IComparable _param0 )
        {
            return _param0 is DateTime dateTime ? ( IComparable ) TimeHelper.UtcKind( dateTime ) : _param0;
        }

        internal static IComparable ConvertToUniversalTime( IComparable _param0 )
        {
            switch ( _param0 )
            {
                case DateTimeOffset dateTimeOffset:
                    return ( IComparable ) dateTimeOffset.UtcDateTime;
                case DateTime dateTime:
                    return ( IComparable ) dateTime.ToUniversalTime();
                default:
                    return _param0;
            }
        }

        /// <summary>Load settings.</summary>
        /// <param name="storage">Settings storage.</param>
        public void Load( SettingsStorage storage )
        {
            IsVisible = storage.GetValue<bool?>( "IsVisible", IsVisible );
            IsEditable = storage.GetValue<bool?>( "IsEditable", IsEditable );
            X1 = ChartDrawData.AnnotationData.ConvertToUTC( storage.GetValue<IComparable>( "X1", X1 ) );
            Y1 = storage.GetValue<IComparable>( "Y1", Y1 );
            X2 = ChartDrawData.AnnotationData.ConvertToUTC( storage.GetValue<IComparable>( ".X2", X2 ) );
            Y2 = storage.GetValue<IComparable>( "Y2", Y2 );
            SettingsStorage settingsStorage1 = storage.GetValue<SettingsStorage>("Stroke", (SettingsStorage)null);
            Stroke = settingsStorage1 != null ? settingsStorage1.GetBrush() : ( System.Windows.Media.Brush ) null;
            SettingsStorage settingsStorage2 = storage.GetValue<SettingsStorage>("Fill", (SettingsStorage)null);
            Fill = settingsStorage2 != null ? settingsStorage2.GetBrush() : ( System.Windows.Media.Brush ) null;
            SettingsStorage settingsStorage3 = storage.GetValue<SettingsStorage>("Foreground", (SettingsStorage)null);
            Foreground = settingsStorage3 != null ? settingsStorage3.GetBrush() : ( System.Windows.Media.Brush ) null;
            try
            {
                SettingsStorage settingsStorage4 = storage.GetValue<SettingsStorage>("Thickness", (SettingsStorage)null);
                Thickness = settingsStorage4 != null ? new System.Windows.Thickness?( settingsStorage4.CreateThickness() ) : new System.Windows.Thickness?();
            }
            catch
            {
            }
            ShowLabel = storage.GetValue<bool?>( "ShowLabel", ShowLabel );
            LabelPlacement = storage.GetValue<LabelPlacement?>( "LabelPlacement", LabelPlacement );
            HorizontalAlignment = storage.GetValue<System.Windows.HorizontalAlignment?>( "HorizontalAlignment", HorizontalAlignment );
            VerticalAlignment = storage.GetValue<System.Windows.VerticalAlignment?>( "VerticalAlignment", VerticalAlignment );
            CoordinateMode = storage.GetValue<AnnotationCoordinateMode?>( "CoordinateMode", CoordinateMode );
            Text = storage.GetValue<string>( "Text", Text );
        }



        /// <summary>Save settings.</summary>
        /// <param name="storage">Settings storage.</param>
        public void Save( SettingsStorage storage )
        {
            storage.SetValue<bool?>( "IsVisible", IsVisible );
            storage.SetValue<bool?>( "IsEditable", IsEditable );
            storage.SetValue<IComparable>( "X1", ChartDrawData.AnnotationData.ConvertToUniversalTime( X1 ) );
            storage.SetValue<IComparable>( "Y1", Y1 );
            storage.SetValue<IComparable>( "X2", ChartDrawData.AnnotationData.ConvertToUniversalTime( X2 ) );
            storage.SetValue<IComparable>( "Y2", Y2 );
            SettingsStorage settingsStorage1 = storage;

            System.Windows.Media.Brush stroke = Stroke;
            SettingsStorage settingsStorage2 = stroke != null ? stroke.SaveSettings() : (SettingsStorage)null;
            settingsStorage1.SetValue<SettingsStorage>( "Stroke", settingsStorage2 );
            SettingsStorage settingsStorage3 = storage;

            System.Windows.Media.Brush fill = Fill;
            SettingsStorage settingsStorage4 = fill != null ? fill.SaveSettings() : (SettingsStorage)null;
            settingsStorage3.SetValue<SettingsStorage>( "Fill", settingsStorage4 );
            SettingsStorage settingsStorage5 = storage;

            System.Windows.Media.Brush foreground = Foreground;
            SettingsStorage settingsStorage6 = foreground != null ? foreground.SaveSettings() : (SettingsStorage)null;
            settingsStorage5.SetValue<SettingsStorage>( "Foreground", settingsStorage6 );
            SettingsStorage settingsStorage7 = storage;

            
            SettingsStorage settingsStorage8 = Thickness.HasValue ? Thickness.GetValueOrDefault().SaveSettings() : (SettingsStorage)null;
            settingsStorage7.SetValue<SettingsStorage>( "Thickness", settingsStorage8 );
            storage.SetValue<bool?>( "ShowLabel", ShowLabel );
            storage.SetValue<LabelPlacement?>( "LabelPlacement", LabelPlacement );
            storage.SetValue<System.Windows.HorizontalAlignment?>( "HorizontalAlignment", HorizontalAlignment );
            storage.SetValue<System.Windows.VerticalAlignment?>( "VerticalAlignment", VerticalAlignment );
            storage.SetValue<AnnotationCoordinateMode?>( "CoordinateMode", CoordinateMode );
            storage.SetValue<string>( "Text", Text );
        }
    }

    private Dictionary<IChartCandleElement, List<ChartDrawData.sCandle>> _candleMap;

    private Dictionary<IChartCandleElement, List<ChartDrawData.sCandleColor>> _candleColorMap;

    private Dictionary<IChartIndicatorElement, List<ChartDrawData.IndicatorData>> _indicatorMap;

    private Dictionary<IChartOrderElement, List<ChartDrawData.sTrade>> _orderMap;

    private Dictionary<IChartTradeElement, List<ChartDrawData.sTrade>> _tradeMap;

    private Dictionary<IChartActiveOrdersElement, List<ChartDrawData.sActiveOrder>> _activeOrdersMap;

    private Dictionary<IChartLineElement, List<ChartDrawData.sxTuple<DateTime>>> _lineTimeMap;

    private Dictionary<IChartLineElement, List<ChartDrawData.sxTuple<double>>> _lineValueMap;

    private Dictionary<IChartBandElement, List<ChartDrawData.sxTuple<DateTime>>> _bandTimeMap;

    private Dictionary<IChartBandElement, List<ChartDrawData.sxTuple<double>>> _bandValueMap;

    private Dictionary<IChartAnnotationElement, IAnnotationData> _annotationMap;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Xaml.Charting.ChartDrawData" />.
    /// </summary>
    public ChartDrawData()
    {
    }

    internal ChartDrawData( IEnumerable<RefPair<DateTimeOffset, IDictionary<IChartElement, object>>> _param1 )
    {
        if ( _param1 == null )
            throw new ArgumentNullException( "" );
        foreach ( RefPair<DateTimeOffset, IDictionary<IChartElement, object>> refPair in _param1 )
        {
            DateTimeOffset first = refPair.First;
            foreach ( KeyValuePair<IChartElement, object> keyValuePair in ( IEnumerable<KeyValuePair<IChartElement, object>> ) refPair.Second )
            {
                IChartElement key = keyValuePair.Key;
                object obj = keyValuePair.Value;
                switch ( key )
                {
                    case IChartCandleElement chartCandleElement:
                        if ( obj is ICandleMessage icandleMessage )
                        {
                            CollectionHelper.SafeAdd<IChartCandleElement, List<ChartDrawData.sCandle>>( ( IDictionary<IChartCandleElement, List<ChartDrawData.sCandle>> ) GetCandleMap(), chartCandleElement ).Add( new ChartDrawData.sCandle( first, icandleMessage.DataType, icandleMessage.OpenPrice, icandleMessage.HighPrice, icandleMessage.LowPrice, icandleMessage.ClosePrice, icandleMessage.PriceLevels ) );
                            continue;
                        }
                        if ( obj is System.Windows.Media.Color color )
                        {
                            CollectionHelper.SafeAdd<IChartCandleElement, List<ChartDrawData.sCandleColor>>( ( IDictionary<IChartCandleElement, List<ChartDrawData.sCandleColor>> ) GetCandleColorMap(), chartCandleElement ).Add( new ChartDrawData.sCandleColor( first, new System.Windows.Media.Color?( color ) ) );
                            continue;
                        }
                        continue;
                    case IChartIndicatorElement indicatorElement:
                        IIndicatorValue val = (IIndicatorValue)obj;
                        CollectionHelper.SafeAdd<IChartIndicatorElement, List<ChartDrawData.IndicatorData>>( ( IDictionary<IChartIndicatorElement, List<ChartDrawData.IndicatorData>> ) GetIndicatorMap(), indicatorElement ).Add( new ChartDrawData.IndicatorData( first, val ) );
                        continue;
                    case IChartOrderElement chartOrderElement:
                        Order order = (Order)obj;
                        CollectionHelper.SafeAdd<IChartOrderElement, List<ChartDrawData.sTrade>>( ( IDictionary<IChartOrderElement, List<ChartDrawData.sTrade>> ) GetOrderMap(), chartOrderElement ).Add( new ChartDrawData.sTrade( first, order.TransactionId, ( string ) null, order.Side, order.Price, order.Volume, order.State != OrderStates.Failed ? ( string ) null : LocalizedStrings.Failed, true ) );
                        continue;
                    case IChartTradeElement chartTradeElement:
                        MyTrade myTrade = (MyTrade)obj;
                        var trade = myTrade.Trade;
                        CollectionHelper.SafeAdd<IChartTradeElement, List<ChartDrawData.sTrade>>( ( IDictionary<IChartTradeElement, List<ChartDrawData.sTrade>> ) GetTradeMap(), chartTradeElement ).Add( new ChartDrawData.sTrade( first, trade.Id.GetValueOrDefault(), trade.StringId, myTrade.Order.Side, trade.Price, trade.Volume, ( string ) null, false ) );
                        continue;
                    case IChartActiveOrdersElement activeOrdersElement:
                        CollectionHelper.SafeAdd<IChartActiveOrdersElement, List<ChartDrawData.sActiveOrder>>( ( IDictionary<IChartActiveOrdersElement, List<ChartDrawData.sActiveOrder>> ) GetActiveOrderMap(), activeOrdersElement ).Add( ( ChartDrawData.sActiveOrder ) obj );
                        continue;
                    default:
                        throw new ArgumentException( StringHelper.Put( LocalizedStrings.UnknownType, new object[ 1 ]
                        {
              (object) key
                        } ) );
                }
            }
        }
    }

    public sealed class ChartDrawDataItem : IChartDrawData.IChartDrawDataItem
    {

        private readonly ChartDrawData _drawData;

        private readonly DateTimeOffset _timeStamp;

        private readonly double _xValue;

        internal ChartDrawDataItem( ChartDrawData _param1, DateTimeOffset _param2 )
        {
            _drawData = _param1 ?? throw new ArgumentNullException( "" );
            _timeStamp = _param2;
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
            return Add( element, color.HasValue ? new System.Windows.Media.Color?( color.GetValueOrDefault().ToWpf() ) : new System.Windows.Media.Color?() );
        }

        /// <inheritdoc />
        public IChartDrawData.IChartDrawDataItem Add( IChartCandleElement element, System.Windows.Media.Color? color )
        {
            return ( IChartDrawData.IChartDrawDataItem ) GetMap<IChartCandleElement, ChartDrawData.sCandleColor>( _drawData.GetCandleColorMap(), element, new ChartDrawData.sCandleColor( TimeStamp, color ) );
        }

        /// <inheritdoc />
        public IChartDrawData.IChartDrawDataItem Add( IChartCandleElement element, DataType dataType, SecurityId secId, Decimal openPrice, Decimal highPrice, Decimal lowPrice, Decimal closePrice, CandlePriceLevel[ ] priceLevels, CandleStates _ )
        {
            return ( IChartDrawData.IChartDrawDataItem ) GetMap<IChartCandleElement, ChartDrawData.sCandle>( _drawData.GetCandleMap(), element, new ChartDrawData.sCandle( TimeStamp, dataType, openPrice, highPrice, lowPrice, closePrice, ( IEnumerable<CandlePriceLevel> ) priceLevels ) );
        }

        /// <inheritdoc />
        public IChartDrawData.IChartDrawDataItem Add( IChartIndicatorElement element, IIndicatorValue value )
        {
            return value == null ? ( IChartDrawData.IChartDrawDataItem ) this : ( IChartDrawData.IChartDrawDataItem ) GetMap<IChartIndicatorElement, ChartDrawData.IndicatorData>( _drawData.GetIndicatorMap(), element, new ChartDrawData.IndicatorData( TimeStamp, value ) );
        }

        /// <inheritdoc />
        public IChartDrawData.IChartDrawDataItem Add( IChartOrderElement element, long orderId, string orderStringId, Sides side, Decimal price, Decimal volume, string errorMessage = null )
        {
            return ( IChartDrawData.IChartDrawDataItem ) GetMap<IChartOrderElement, ChartDrawData.sTrade>( _drawData.GetOrderMap(), element, new ChartDrawData.sTrade( TimeStamp, orderId, orderStringId, side, price, volume, errorMessage, true ) );
        }

        /// <inheritdoc />
        public IChartDrawData.IChartDrawDataItem Add( IChartTradeElement element, long tradeId, string tradeStringId, Sides side, Decimal price, Decimal volume )
        {
            return ( IChartDrawData.IChartDrawDataItem ) GetMap<IChartTradeElement, ChartDrawData.sTrade>( _drawData.GetTradeMap(), element, new ChartDrawData.sTrade( TimeStamp, tradeId, tradeStringId, side, price, volume, ( string ) null, false ) );
        }

        /// <inheritdoc />
        public IChartDrawData.IChartDrawDataItem Add( IChartLineElement element, double value1, double value2 = double.NaN )
        {
            return !MathHelper.IsNaN( XValue ) ? ( IChartDrawData.IChartDrawDataItem ) GetMap<IChartLineElement, ChartDrawData.sxTuple<double>>( _drawData.GetLineValueMap(), element, ChartDrawData.sxTuple<double>.CreateSxTuple<double>( XValue, value1, value2 ) ) : ( IChartDrawData.IChartDrawDataItem ) GetMap<IChartLineElement, ChartDrawData.sxTuple<DateTime>>( _drawData.GetLineTimeMap(), element, ChartDrawData.sxTuple<DateTime>.CreateSxTuple<DateTimeOffset>( TimeStamp, value1, value2 ) );
        }

        /// <inheritdoc />
        public IChartDrawData.IChartDrawDataItem Add( IChartBandElement element, Decimal value )
        {
            return Add( element, ( double ) value, 0.0 );
        }

        /// <inheritdoc />
        public IChartDrawData.IChartDrawDataItem Add( IChartBandElement element, double value1, double value2 )
        {
            return !MathHelper.IsNaN( XValue ) ? ( IChartDrawData.IChartDrawDataItem ) GetMap<IChartBandElement, ChartDrawData.sxTuple<double>>( _drawData.GetBandValueMap(), element, ChartDrawData.sxTuple<double>.CreateSxTuple<double>( XValue, value1, value2 ) ) : ( IChartDrawData.IChartDrawDataItem ) GetMap<IChartBandElement, ChartDrawData.sxTuple<DateTime>>( _drawData.GetBandTimeMap(), element, ChartDrawData.sxTuple<DateTime>.CreateSxTuple<DateTimeOffset>( TimeStamp, value1, value2 ) );
        }

        private ChartDrawData.ChartDrawDataItem GetMap<TElement, TValue>( Dictionary<TElement, List<TValue>> _param1, TElement _param2, TValue _param3 )
        {
            CollectionHelper.SafeAdd<TElement, List<TValue>>( ( IDictionary<TElement, List<TValue>> ) _param1, _param2 ).Add( _param3 );
            return this;
        }
    }

    /// <inheritdoc />
    public IChartDrawData.IChartDrawDataItem Group( DateTimeOffset timeStamp )
    {
        return ( IChartDrawData.IChartDrawDataItem ) new ChartDrawData.ChartDrawDataItem( this, timeStamp );
    }

    /// <inheritdoc />
    public IChartDrawData.IChartDrawDataItem Group( double xValue )
    {
        return ( IChartDrawData.IChartDrawDataItem ) new ChartDrawData.ChartDrawDataItem( this, xValue );
    }

    /// <inheritdoc />
    public IChartDrawData Add( IChartAnnotationElement element, IAnnotationData data )
    {
        Dictionary<IChartAnnotationElement, IAnnotationData> dictionary = GetAnnotationMap();
        IChartAnnotationElement key = element;
        dictionary[ key ] = data ?? throw new ArgumentNullException( "" );
        return ( IChartDrawData ) this;
    }

    /// <inheritdoc />
    public IChartDrawData Add( IChartActiveOrdersElement element, Order order, bool? isFrozen = null, bool autoRemoveFromChart = true, bool isHidden = false, bool? isError = null, Decimal? price = null, Decimal? balance = null, OrderStates? state = null )
    {
        state.GetValueOrDefault();
        if ( !state.HasValue )
            state = order != null ? order.State : OrderStates.None;

        Decimal valueOrDefault = price.GetValueOrDefault();
        if ( !price.HasValue )
        {
            price = order != null ? order.Price : throw new ArgumentException( "Order is NUll" );
        }
        valueOrDefault = balance.GetValueOrDefault();
        if ( !balance.HasValue )
            balance = new Decimal?( order != null ? order.Balance : 0M );
        isFrozen.GetValueOrDefault();
        if ( !isFrozen.HasValue )
        {
            OrderStates? nullable = state;
            OrderStates orderStates = OrderStates.None;
            isFrozen = new bool?( nullable.GetValueOrDefault() == orderStates & nullable.HasValue || state.GetValueOrDefault() == OrderStates.Pending );
        }
        CollectionHelper.SafeAdd<IChartActiveOrdersElement, List<ChartDrawData.sActiveOrder>>( ( IDictionary<IChartActiveOrdersElement, List<ChartDrawData.sActiveOrder>> ) GetActiveOrderMap(), element ).Add( new ChartDrawData.sActiveOrder( order, balance.Value, state.Value, ( Decimal? ) order?.Security?.PriceStep ?? 0.01M, autoRemoveFromChart, isFrozen.Value, isHidden, ( isError ?? ( state.GetValueOrDefault() == OrderStates.Failed ? true : false ) ) != false, price.Value ) );
        return ( IChartDrawData ) this;
    }

    internal List<ChartDrawData.sCandle> GetCandleRelatedData(
      IChartCandleElement _param1 )
    {
        Dictionary<IChartCandleElement, List<ChartDrawData.sCandle>> a4BPs40sQtxmCo6Ew = _candleMap;
        return a4BPs40sQtxmCo6Ew == null ? ( List<ChartDrawData.sCandle> ) null : CollectionHelper.TryGetValue<IChartCandleElement, List<ChartDrawData.sCandle>>( ( IDictionary<IChartCandleElement, List<ChartDrawData.sCandle>> ) a4BPs40sQtxmCo6Ew, _param1 );
    }

    internal List<ChartDrawData.sCandleColor> GetCandleColor(
      IChartCandleElement _param1 )
    {
        Dictionary<IChartCandleElement, List<ChartDrawData.sCandleColor>> z3mmVbunXgsFqZ9EtJw = _candleColorMap;
        return z3mmVbunXgsFqZ9EtJw == null ? ( List<ChartDrawData.sCandleColor> ) null : CollectionHelper.TryGetValue<IChartCandleElement, List<ChartDrawData.sCandleColor>>( ( IDictionary<IChartCandleElement, List<ChartDrawData.sCandleColor>> ) z3mmVbunXgsFqZ9EtJw, _param1 );
    }

    internal List<ChartDrawData.IndicatorData> GetCandleRelatedData(
      IChartIndicatorElement _param1 )
    {
        Dictionary<IChartIndicatorElement, List<ChartDrawData.IndicatorData>> ireF1JupHymGa845Q = _indicatorMap;
        return ireF1JupHymGa845Q == null ? ( List<ChartDrawData.IndicatorData> ) null : CollectionHelper.TryGetValue<IChartIndicatorElement, List<ChartDrawData.IndicatorData>>( ( IDictionary<IChartIndicatorElement, List<ChartDrawData.IndicatorData>> ) ireF1JupHymGa845Q, _param1 );
    }

    internal List<ChartDrawData.sTrade> GetCandleRelatedData(
      IChartOrderElement _param1 )
    {
        Dictionary<IChartOrderElement, List<ChartDrawData.sTrade>> zFl6oOk7l7AmA = _orderMap;
        return zFl6oOk7l7AmA == null ? ( List<ChartDrawData.sTrade> ) null : CollectionHelper.TryGetValue<IChartOrderElement, List<ChartDrawData.sTrade>>( ( IDictionary<IChartOrderElement, List<ChartDrawData.sTrade>> ) zFl6oOk7l7AmA, _param1 );
    }

    internal List<ChartDrawData.sTrade> GetCandleRelatedData(
      IChartTradeElement _param1 )
    {
        Dictionary<IChartTradeElement, List<ChartDrawData.sTrade>> zW6DkSpcRysL = _tradeMap;
        return zW6DkSpcRysL == null ? ( List<ChartDrawData.sTrade> ) null : CollectionHelper.TryGetValue<IChartTradeElement, List<ChartDrawData.sTrade>>( ( IDictionary<IChartTradeElement, List<ChartDrawData.sTrade>> ) zW6DkSpcRysL, _param1 );
    }

    internal List<ChartDrawData.sActiveOrder> GetCandleRelatedData(
      IChartActiveOrdersElement _param1 )
    {
        Dictionary<IChartActiveOrdersElement, List<ChartDrawData.sActiveOrder>> z4fpZvTfoaMgNvfE8Ng = _activeOrdersMap;
        return z4fpZvTfoaMgNvfE8Ng == null ? ( List<ChartDrawData.sActiveOrder> ) null : CollectionHelper.TryGetValue<IChartActiveOrdersElement, List<ChartDrawData.sActiveOrder>>( ( IDictionary<IChartActiveOrdersElement, List<ChartDrawData.sActiveOrder>> ) z4fpZvTfoaMgNvfE8Ng, _param1 );
    }

    internal List<ChartDrawData.sxTuple<DateTime>> GetLineTime(
    IChartLineElement _param1 )
    {
        Dictionary<IChartLineElement, List<ChartDrawData.sxTuple<DateTime>>> z6XWSk2oHkW = _lineTimeMap;
        return z6XWSk2oHkW == null ? ( List<ChartDrawData.sxTuple<DateTime>> ) null : CollectionHelper.TryGetValue<IChartLineElement, List<ChartDrawData.sxTuple<DateTime>>>( ( IDictionary<IChartLineElement, List<ChartDrawData.sxTuple<DateTime>>> ) z6XWSk2oHkW, _param1 );
    }

    internal List<ChartDrawData.sxTuple<double>> GetLineValue(
      IChartLineElement _param1 )
    {
        Dictionary<IChartLineElement, List<ChartDrawData.sxTuple<double>>> zPfyjopyhj6ky = _lineValueMap;
        return zPfyjopyhj6ky == null ? ( List<ChartDrawData.sxTuple<double>> ) null : CollectionHelper.TryGetValue<IChartLineElement, List<ChartDrawData.sxTuple<double>>>( ( IDictionary<IChartLineElement, List<ChartDrawData.sxTuple<double>>> ) zPfyjopyhj6ky, _param1 );
    }

    internal List<ChartDrawData.sxTuple<DateTime>> GetLineTime(
      IChartBandElement _param1 )
    {
        Dictionary<IChartBandElement, List<ChartDrawData.sxTuple<DateTime>>> hrT3IuXyJaPuaHpg = _bandTimeMap;
        return hrT3IuXyJaPuaHpg == null ? ( List<ChartDrawData.sxTuple<DateTime>> ) null : CollectionHelper.TryGetValue<IChartBandElement, List<ChartDrawData.sxTuple<DateTime>>>( ( IDictionary<IChartBandElement, List<ChartDrawData.sxTuple<DateTime>>> ) hrT3IuXyJaPuaHpg, _param1 );
    }

    internal List<ChartDrawData.sxTuple<double>> GetLineValue(
          IChartBandElement _param1 )
    {
        Dictionary<IChartBandElement, List<ChartDrawData.sxTuple<double>>> kpa3YtJh2v6Rs3Joq = _bandValueMap;
        return kpa3YtJh2v6Rs3Joq == null ? ( List<ChartDrawData.sxTuple<double>> ) null : CollectionHelper.TryGetValue<IChartBandElement, List<ChartDrawData.sxTuple<double>>>( ( IDictionary<IChartBandElement, List<ChartDrawData.sxTuple<double>>> ) kpa3YtJh2v6Rs3Joq, _param1 );
    }

    internal IEnumerableEx<ChartDrawData.IDrawValue> GetCandleRelatedData( IChartLineElement _param1 )
    {
        List<ChartDrawData.sxTuple<DateTime>> source1 = GetLineTime(_param1);
        if ( source1 != null && source1.Count > 0 )
            return CollectionHelper.ToEx<ChartDrawData.IDrawValue>( source1.Cast<ChartDrawData.IDrawValue>(), source1.Count );
        List<ChartDrawData.sxTuple<double>> source2 = GetLineValue(_param1);
        return source2 == null ? ( IEnumerableEx<ChartDrawData.IDrawValue> ) null : CollectionHelper.ToEx<ChartDrawData.IDrawValue>( source2.Cast<ChartDrawData.IDrawValue>(), source2.Count );
    }

    internal IEnumerableEx<ChartDrawData.IDrawValue> GetCandleRelatedData(
      IChartBandElement _param1 )
    {
        List<ChartDrawData.sxTuple<DateTime>> source1 = GetLineTime(_param1);
        if ( source1 != null && source1.Count > 0 )
            return CollectionHelper.ToEx<ChartDrawData.IDrawValue>( source1.Cast<ChartDrawData.IDrawValue>(), source1.Count );
        List<ChartDrawData.sxTuple<double>> source2 = GetLineValue(_param1);
        return source2 == null ? ( IEnumerableEx<ChartDrawData.IDrawValue> ) null : CollectionHelper.ToEx<ChartDrawData.IDrawValue>( source2.Cast<ChartDrawData.IDrawValue>(), source2.Count );
    }

    internal ChartDrawData.AnnotationData GetAnnotation( IChartAnnotationElement _param1 )
    {
        Dictionary<IChartAnnotationElement, IAnnotationData> zfS3q6Qc = _annotationMap;
        return zfS3q6Qc != null ? ( ChartDrawData.AnnotationData ) CollectionHelper.TryGetValue<IChartAnnotationElement, IAnnotationData>( ( IDictionary<IChartAnnotationElement, IAnnotationData> ) zfS3q6Qc, _param1 ) : ( ChartDrawData.AnnotationData ) null;
    }

}