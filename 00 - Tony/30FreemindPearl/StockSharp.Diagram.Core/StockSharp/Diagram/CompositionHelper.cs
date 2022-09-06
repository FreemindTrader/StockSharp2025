// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.CompositionHelper
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using Ecng.Common;
using Ecng.Serialization;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Indicators;
using StockSharp.BusinessEntities;
using StockSharp.Configuration;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockSharp.Diagram
{
    /// <summary>Helpers.</summary>
    public static class CompositionHelper
    {

        private static readonly HashSet<DiagramSocketType> _diagramSocketTypes = new HashSet<DiagramSocketType>( new DiagramSocketType[10] { DiagramSocketType.Any, DiagramSocketType.Bool, DiagramSocketType.Unit, DiagramSocketType.Date, DiagramSocketType.Time, DiagramSocketType.Side, DiagramSocketType.CandleStates, DiagramSocketType.OrderState, DiagramSocketType.Security, DiagramSocketType.Portfolio } );

        private static readonly Type[ ] _type01;

        private static Type[ ] _diagramElements;

        static CompositionHelper()
        {
            StockSharpSection rootSection = Configuration.Extensions.RootSection;
            _type01 = ( rootSection != null ? rootSection.CustomDiagramElements.SafeAdd( new Func<Configuration.DiagramElement, Type>( x => x.Type.To<Type>() ) ) : null ) ?? Array.Empty<Type>();
        }

        /// <summary>Get all diagram elements.</summary>
        /// <returns>All diagram elements.</returns>
        public static IEnumerable<DiagramElement> GetDiagramElements()
        {
            if ( _diagramElements == null )
                _diagramElements = ( ( IEnumerable<Type> )typeof( DiagramElement ).Assembly.GetTypes() )
                    .Where( x => {
                                    if ( !x.IsAbstract && x.IsSubclassOf( typeof( DiagramElement ) ) && x.IsBrowsable() )
                                        return x != typeof( CompositionDiagramElement );
                                    return false;
                                 } )
                    .Concat( _type01 )
                    .OrderBy( x => x.Name )
                    .ToArray();
            
            return ( ( IEnumerable<Type> )_diagramElements )
                                            .Select( x => x.CreateInstance<DiagramElement>() )
                                            .ToArray();
        }

        /// <summary>Determine the specified type is browsable.</summary>
        /// <param name="type">Scheme type.</param>
        /// <returns>Check result.</returns>
        public static bool IsBrowsable( this SchemeTypes type )
        {
            if ( type != SchemeTypes.Regular )
                return type == SchemeTypes.Independent;
            return true;
        }

        /// <summary>To continue and stop at the next element.</summary>
        /// <param name="syncObject">
        ///   <see cref="T:StockSharp.Diagram.DebuggerSyncObject" />
        /// </param>
        public static void ContinueAndWaitOnNext( this DebuggerSyncObject syncObject )
        {
            if ( syncObject == null )
                throw new ArgumentNullException( "syncObject == null" );
            syncObject.SetWaitOnNext();
            syncObject.Continue();
        }

        private static void ToUnit( ref IComparable _param0, ref IComparable _param1 )
        {
            Unit unit1 = _param0 as Unit;
            Unit unit2 = _param1 as Unit;
            if ( unit1 != null )
            {
                if ( !( unit2 == null ) || !( ( object )_param1 ).GetType().IsNumeric() )
                    return;
                _param1 = new Unit( _param1.To<Decimal>() );
            }
            else
            {
                if ( !( unit2 != null ) || !( ( object )_param0 ).GetType().IsNumeric() )
                    return;
                _param0 = new Unit( _param0.To<Decimal>() );
            }
        }

        private static void ToUnit2( ref IComparable _param0, ref IComparable _param1 )
        {
            IIndicatorValue indicatorValue1 = _param0 as IIndicatorValue;
            IIndicatorValue indicatorValue2 = _param1 as IIndicatorValue;
            if ( indicatorValue1 != null && indicatorValue2 != null )
                return;
            try
            {
                if ( indicatorValue1 != null )
                {
                    Decimal num = indicatorValue1.GetValue<Decimal>();
                    Type type = ( ( object )_param1 ).GetType();
                    if ( type.IsNumeric() )
                    {
                        _param0 = num;
                        _param1 = _param1.To<Decimal>();
                    }
                    else if ( type == typeof( Unit ) )
                        _param0 = new Unit( num );
                }
                if ( indicatorValue2 == null )
                    return;
                Decimal num1 = indicatorValue2.GetValue<Decimal>();
                Type type1 = ( ( object )_param0 ).GetType();
                if ( type1.IsNumeric() )
                {
                    _param0 = _param0.To<Decimal>();
                    _param1 = num1;
                }
                else
                {
                    if ( !( type1 == typeof( Unit ) ) )
                        return;
                    _param1 = new Unit( num1 );
                }
            }
            catch ( Exception ex )
            {
            }
        }

        internal static void Convert( ref IComparable _param0, ref IComparable _param1 )
        {
            CompositionHelper.ToUnit( ref _param0, ref _param1 );
            CompositionHelper.ToUnit2( ref _param0, ref _param1 );
        }

        internal static DiagramSocket AddDiagramSocketTypes( this DiagramSocket _param0 )
        {
            _param0.AvailableTypes.Clear();
            _param0.AvailableTypes.Add( DiagramSocketType.Candle );
            _param0.AvailableTypes.Add( DiagramSocketType.IndicatorValue );
            _param0.AvailableTypes.Add( DiagramSocketType.Unit );
            _param0.AvailableTypes.Add( DiagramSocketType.Quote );
            return _param0;
        }

        internal static DiagramSocket Reset( this DiagramSocket _param0 )
        {
            _param0.AvailableTypes.Clear();
            _param0.AvailableTypes.Add( DiagramSocketType.Any );
            return _param0;
        }

        /// <summary>
        /// </summary>
        /// <param name="type">
        /// </param>
        /// <returns>
        /// </returns>
        public static bool IsEditable( this DiagramSocketType type )
        {
            return _diagramSocketTypes.Contains( type );
        }

        /// <summary>
        /// </summary>
        /// <param name="sockets">
        /// </param>
        /// <param name="id">
        /// </param>
        /// <returns>
        /// </returns>
        public static DiagramSocket FindById(
          this IEnumerable<DiagramSocket> sockets,
          string id )
        {
            return sockets.FirstOrDefault( x => x.Id.EqualsIgnoreCase( id ) );
        }

        internal static void CheckParameters<T>(
          this SettingsStorage settings,
          string _param1,
          Action<T> _param2,
          bool _param3 )
          where T : class
        {
            if ( settings == null )
                throw new ArgumentNullException( "settings == null" );

            if ( _param1 == null )
                throw new ArgumentNullException( "_param1 == null" );

            if ( _param2 == null )
                throw new ArgumentNullException( "_param2 == null" );

            T obj = settings.GetValue( _param1, default( T ) );
            if ( obj == null && !_param3 )
                return;
            try
            {
                _param2( obj );
            }
            catch ( Exception ex )
            {
                ex.LogError( null );
            }
        }

        /// <summary>
        /// Convert input value to <see cref="T:StockSharp.Algo.Indicators.IIndicatorValue" />.
        /// </summary>
        /// <param name="indicator">Indicator.</param>
        /// <param name="inputValue">Input value.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Algo.Indicators.IIndicatorValue" />.</returns>
        public static IIndicatorValue ConvertToIIndicatorValue(
          this IIndicator indicator,
          object inputValue )
        {
            if ( indicator == null )
                throw new ArgumentNullException( "indicator == null" );

            if ( inputValue == null )
                throw new ArgumentNullException( "inputValue == null" );

            IIndicatorValue indicatorValue1 = null;
            Candle candle = inputValue as Candle;
            if ( candle == null )
            {
                IIndicatorValue indicatorValue2 = inputValue as IIndicatorValue;
                if ( indicatorValue2 == null )
                {
                    Unit unit = inputValue as Unit;
                    if ( ( object )unit == null )
                    {
                        Tuple<Decimal, Decimal> tuple = inputValue as Tuple<Decimal, Decimal>;
                        if ( tuple == null )
                        {
                            MarketDepth depth = inputValue as MarketDepth;
                            if ( depth != null )
                            {
                                MarketDepthIndicatorValue depthIndicatorValue = new MarketDepthIndicatorValue( indicator, depth );
                                depthIndicatorValue.IsFinal = true;
                                indicatorValue1 = depthIndicatorValue;
                            }
                        }
                        else
                        {
                            PairIndicatorValue<Decimal> pairIndicatorValue = new PairIndicatorValue<Decimal>( indicator, tuple );
                            pairIndicatorValue.IsFinal = true;
                            indicatorValue1 = pairIndicatorValue;
                        }
                    }
                    else
                    {
                        DecimalIndicatorValue decimalIndicatorValue = new DecimalIndicatorValue( indicator, unit.Value );
                        decimalIndicatorValue.IsFinal = true;
                        indicatorValue1 = decimalIndicatorValue;
                    }
                }
                else
                    indicatorValue1 = indicatorValue2;
            }
            else
                indicatorValue1 = new CandleIndicatorValue( indicator, candle );
            if ( indicatorValue1 == null && inputValue.GetType().IsNumeric() )
            {
                DecimalIndicatorValue decimalIndicatorValue = new DecimalIndicatorValue( indicator, inputValue.To<Decimal>() );
                decimalIndicatorValue.IsFinal = true;
                indicatorValue1 = decimalIndicatorValue;
            }
            if ( indicatorValue1 == null )
                throw new ArgumentException( LocalizedStrings.Str3106Params.Put( new object[1] { inputValue.GetType().Name } ) );
            return indicatorValue1;
        }

        /// <summary>Get From socket for the specified link.</summary>
        /// <param name="link">
        ///   <see cref="T:StockSharp.Diagram.ICompositionModelLink" />
        /// </param>
        /// <param name="behavior">
        ///   <see cref="T:StockSharp.Diagram.ICompositionModelBehavior`2" />
        /// </param>
        /// <returns>
        ///   <see cref="T:StockSharp.Diagram.DiagramSocket" />
        /// </returns>
        public static DiagramSocket GetFromSocket<TNode, TLink>(
          this ICompositionModelLink link,
          ICompositionModelBehavior<TNode, TLink> behavior )
          where TNode : ICompositionModelNode
          where TLink : ICompositionModelLink
        {
            if ( !link.IsConnected )
                return null;

            // Tony
            //if ( !link.CheckOnNull<ICompositionModelLink>( nameof( -1260198879 ) ).IsConnected )
            //    return ( DiagramSocket )null;

            TNode nodeByKey = behavior.FindNodeByKey( link.From );
            ref TNode local = ref nodeByKey;
            if ( local == null )
                return null;
            DiagramElement element = local.Element;
            if ( element == null )
                return null;
            IReadOnlyCollection<DiagramSocket> outputSockets = element.OutputSockets;
            if ( outputSockets == null )
                return null;
            return outputSockets.FindById( link.FromPort );
        }

        /// <summary>Get To socket for the specified link.</summary>
        /// <param name="link">
        ///   <see cref="T:StockSharp.Diagram.ICompositionModelLink" />
        /// </param>
        /// <param name="behavior">
        ///   <see cref="T:StockSharp.Diagram.ICompositionModelBehavior`2" />
        /// </param>
        /// <returns>
        ///   <see cref="T:StockSharp.Diagram.DiagramSocket" />
        /// </returns>
        public static DiagramSocket GetToSocket<TNode, TLink>( this ICompositionModelLink link, ICompositionModelBehavior<TNode, TLink> behavior ) where TNode : ICompositionModelNode where TLink : ICompositionModelLink
        {
            if ( !link.IsConnected )
                return null;

            // Tony
            //if ( !link.CheckOnNull<ICompositionModelLink>( nameof( -1260198879 ) ).IsConnected )
            //    return ( DiagramSocket )null;


            TNode nodeByKey = behavior.FindNodeByKey( link.To );
            ref TNode local = ref nodeByKey;
            if ( local == null )
                return null;
            DiagramElement element = local.Element;
            if ( element == null )
                return null;
            IReadOnlyCollection<DiagramSocket> inputSockets = element.InputSockets;
            if ( inputSockets == null )
                return null;
            return inputSockets.FindById( link.ToPort );
        }
        
    }
}
