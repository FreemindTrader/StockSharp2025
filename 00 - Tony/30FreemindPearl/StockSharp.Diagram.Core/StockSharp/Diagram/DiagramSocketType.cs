using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using StockSharp.Algo.Indicators;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;

namespace StockSharp.Diagram
{
    /// <summary>Connection type.</summary>
    public class DiagramSocketType : Equatable<DiagramSocketType>, INotifyPropertyChanged, IPersistable
    {
        
        private static readonly CachedSynchronizedDictionary<string, DiagramSocketType> _diagramSocketTypes = new CachedSynchronizedDictionary<string, DiagramSocketType>();
        /// <summary>Unknown data type.</summary>
        public static readonly DiagramSocketType Any = RegisterType<object>( LocalizedStrings.AnyData, Color.Black );
        /// <summary>Security.</summary>
        public static readonly DiagramSocketType Security = RegisterType<Security>( LocalizedStrings.Security, Color.DarkGreen );
        /// <summary>Market depth.</summary>
        public static readonly DiagramSocketType MarketDepth = RegisterType<MarketDepth>( LocalizedStrings.MarketDepth, Color.DarkCyan );
        /// <summary>Quote.</summary>
        public static readonly DiagramSocketType Quote = RegisterType<QuoteChange>( LocalizedStrings.Str273, Color.Cyan );
        /// <summary>Candle.</summary>
        public static readonly DiagramSocketType Candle = RegisterType<Algo.Candles.Candle>( LocalizedStrings.Candles, Color.OrangeRed );
        /// <summary>Indicator value.</summary>
        public static readonly DiagramSocketType IndicatorValue = RegisterType<IIndicatorValue>( LocalizedStrings.IndicatorValue, Color.DarkGoldenrod );
        /// <summary>Order.</summary>
        public static readonly DiagramSocketType Order = RegisterType<Order>( LocalizedStrings.Str504, Color.Olive );
        /// <summary>Order fail.</summary>
        public static readonly DiagramSocketType OrderFail = RegisterType<OrderFail>( LocalizedStrings.XamlStr182, Color.PaleVioletRed );
        /// <summary>Own trade.</summary>
        public static readonly DiagramSocketType MyTrade = RegisterType<MyTrade>( LocalizedStrings.OwnTrade, Color.DarkOliveGreen );
        /// <summary>Flag.</summary>
        public static readonly DiagramSocketType Bool = RegisterType<bool>( LocalizedStrings.Flag, Color.DodgerBlue );
        /// <summary>Numeric value.</summary>
        public static readonly DiagramSocketType Unit = RegisterType<Unit>( LocalizedStrings.NumericValue, Color.MediumSeaGreen );
        /// <summary>Comparable values.</summary>
        public static readonly DiagramSocketType Comparable = RegisterType<IComparable>( LocalizedStrings.Comparison, Color.DarkSlateBlue );
        /// <summary>Portfolio.</summary>
        public static readonly DiagramSocketType Portfolio = RegisterType<Portfolio>( LocalizedStrings.Portfolio, Color.Brown );
        /// <summary>Options.</summary>
        public static readonly DiagramSocketType Options = RegisterType<IEnumerable<Security>>( LocalizedStrings.Options, Color.DeepPink );
        /// <summary>Side.</summary>
        public static readonly DiagramSocketType Side = RegisterType<Sides>( LocalizedStrings.Side, Color.Beige );
        /// <summary>Candle state.</summary>
        public static readonly DiagramSocketType CandleStates = RegisterType<CandleStates>( LocalizedStrings.CandleState, Color.OrangeRed );
        /// <summary>Trade.</summary>
        public static readonly DiagramSocketType Trade = RegisterType<Trade>( LocalizedStrings.Str506, Color.DarkKhaki );
        /// <summary>Strategy.</summary>
        public static readonly DiagramSocketType Strategy = RegisterType<Algo.Strategies.Strategy>( LocalizedStrings.Strategy, Color.DarkBlue );
        /// <summary>Connector.</summary>
        public static readonly DiagramSocketType Connector = RegisterType<IConnector>( LocalizedStrings.Connector, Color.YellowGreen );
        /// <summary>Strategy.</summary>
        public static readonly DiagramSocketType Date = RegisterType<DateTimeOffset>( LocalizedStrings.Date, Color.Chocolate );
        /// <summary>Connector.</summary>
        public static readonly DiagramSocketType Time = RegisterType<TimeSpan>( LocalizedStrings.Time, Color.Coral );
        /// <summary>Position.</summary>
        public static readonly DiagramSocketType Position = RegisterType<Position>( LocalizedStrings.Str862, Color.SaddleBrown );
        /// <summary>Order state.</summary>
        public static readonly DiagramSocketType OrderState = RegisterType<OrderStates>( LocalizedStrings.OrderState, Color.Chartreuse );
        
        private string _name = string.Empty;
        
        private Type _type = typeof( object );
        
        private Color _color = Color.Black;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.DiagramSocketType" />.
        /// </summary>
        public DiagramSocketType()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.DiagramSocketType" />.
        /// </summary>
        /// <param name="type">Data type.</param>
        /// <param name="name">The name of the connection type.</param>
        /// <param name="color">The connection color.</param>
        public DiagramSocketType( Type type, string name, Color color )
        {
            this.Type = type;
            this.Name = ( name );
            this.Color = ( color );
        }

        /// <summary>The name of the connection type.</summary>
        public string Name
        {
            get
            {
                return this._name;
            }

            set
            {
                string str = value;
                if ( str == null )
                    throw new ArgumentNullException( "Name Value == null " );
                this._name = str;
                this.OnPropertyChanged( nameof( Name ) );
            }
        }



        /// <summary>Connection type.</summary>
        public Type Type
        {
            get
            {
                return this._type;
            }
            set
            {
                Type type = value;
                if ( ( object )type == null )
                    throw new ArgumentNullException( "type == null " );
                this._type = type;
                this.OnPropertyChanged( nameof( Type ) );
            }
        }

        /// <summary>The connection color.</summary>
        public Color Color
        {
            get
            {
                return this._color;
            }

            set
            {
                this._color = value;
                this.OnPropertyChanged( nameof( Color ) );
            }
        }



        /// <summary>All available connection types for elements.</summary>
        public static IEnumerable<DiagramSocketType> AllTypes
        {
            get
            {
                return ( IEnumerable<DiagramSocketType> )_diagramSocketTypes.CachedValues;
            }
        }

        /// <summary>To register the connection type.</summary>
        /// <typeparam name="T">Data type.</typeparam>
        /// <param name="name">The name of the connection type.</param>
        /// <param name="color">The connection color.</param>
        /// <returns>Connection type.</returns>
        public static DiagramSocketType RegisterType<T>( string name, Color color )
        {
            DiagramSocketType.DiagramSocketTypeHolder<T> holder = new DiagramSocketType.DiagramSocketTypeHolder<T>();
            holder._myString = name;
            holder._myColor = color;
            if ( holder._myString == null )
                throw new ArgumentNullException( "name == null" );
            return _diagramSocketTypes.SafeAdd<string, DiagramSocketType>( holder._myString, new Func<string, DiagramSocketType>( holder.Function1 ) );
        }

        /// <summary>The connection properties value change event.</summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>To call the connection property value change event.</summary>
        /// <param name="propertyName">Property name.</param>
        protected virtual void OnPropertyChanged( string propertyName )
        {
            PropertyChangedEventHandler zaDtyDxg = this.PropertyChanged;
            if ( zaDtyDxg == null )
                return;
            zaDtyDxg.Invoke( ( object )this, propertyName );
        }

        /// <summary>
        /// Create a copy of <see cref="T:StockSharp.Diagram.DiagramSocketType" />.
        /// </summary>
        /// <returns>Copy.</returns>
        public override DiagramSocketType Clone()
        {
            return new DiagramSocketType( this.Type, this.Name, this.Color );
        }

        /// <summary>
        /// Compare <see cref="T:StockSharp.Diagram.DiagramSocketType" /> on the equivalence.
        /// </summary>
        /// <param name="other">Another value with which to compare.</param>
        /// <returns>
        /// <see langword="true" />, if the specified object is equal to the current object, otherwise, <see langword="false" />.</returns>
        protected override bool OnEquals( DiagramSocketType other )
        {
            return this.Type == other.Type;
        }

        /// <summary>Load settings.</summary>
        /// <param name="storage">Settings storage.</param>
        public void Load( SettingsStorage storage )
        {
            this.Type = storage.GetValue<string>( nameof( Type ), ( string )null ).To<Type>();
            this.Name = ( storage.GetValue<string>( nameof( Name ), ( string )null ) );
            this.Color = ( storage.GetValue<Color>( nameof( Color ), new Color() ) );
        }

        /// <summary>Save settings.</summary>
        /// <param name="storage">Settings storage.</param>
        public void Save( SettingsStorage storage )
        {
            storage.SetValue<string>( nameof( Type ), this.Type.GetTypeName( false ) );
            storage.SetValue<string>( nameof( Name ), this.Name );
            storage.SetValue<Color>( nameof( Color ), this.Color );
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return this.Name;
        }

        /// <summary>
        /// Get <see cref="T:StockSharp.Diagram.DiagramSocketType" /> for <see cref="T:System.Type" />.
        /// </summary>
        /// <param name="parameterType">Type.</param>
        /// <returns>Diagram socket type.</returns>
        public static DiagramSocketType GetSocketType( Type parameterType )
        {
            if ( parameterType == ( Type )null )
                throw new ArgumentNullException( "parameterType == (Type) null" );
            if ( parameterType == typeof( bool ) )
                return Bool;
            if ( typeof( StockSharp.BusinessEntities.Security ).IsAssignableFrom( parameterType ) )
                return Security;
            if ( typeof( StockSharp.Algo.Candles.Candle ).IsAssignableFrom( parameterType ) )
                return Candle;
            if ( typeof( IIndicatorValue ).IsAssignableFrom( parameterType ) )
                return IndicatorValue;
            if ( parameterType == typeof( StockSharp.BusinessEntities.MarketDepth ) )
                return MarketDepth;
            if ( typeof( StockSharp.BusinessEntities.Quote ).IsAssignableFrom( parameterType ) || typeof( QuoteChange ).IsAssignableFrom( parameterType ) )
                return Quote;
            if ( parameterType == typeof( StockSharp.BusinessEntities.MyTrade ) )
                return MyTrade;
            if ( parameterType == typeof( StockSharp.BusinessEntities.Order ) )
                return Order;
            if ( parameterType == typeof( StockSharp.BusinessEntities.Portfolio ) )
                return Portfolio;
            if ( parameterType == typeof( StockSharp.BusinessEntities.Position ) )
                return Position;
            if ( parameterType == typeof( DateTimeOffset ) )
                return Date;
            if ( parameterType == typeof( TimeSpan ) )
                return Time;
            if ( parameterType == typeof( StockSharp.Messages.Unit ) || parameterType.IsNumeric() && !parameterType.IsEnum() || parameterType.IsNullable() && parameterType.GetUnderlyingType().IsNumeric() && !parameterType.GetUnderlyingType().IsEnum() )
                return Unit;
            if ( typeof( IEnumerable<StockSharp.BusinessEntities.Security> ).IsAssignableFrom( parameterType ) )
                return Options;
            return Any;
        }

        private sealed class DiagramSocketTypeHolder<T>
        {
            public string _myString;
            public Color _myColor;

            internal DiagramSocketType Function1(
              string _param1 )
            {
                return new DiagramSocketType( typeof( T ), this._myString, this._myColor );
            }
        }
    }
}
