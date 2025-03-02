﻿using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using MoreLinq;
using StockSharp.Algo.Indicators;
using StockSharp.Localization;
using System;
using System.Collections;
using System.Collections.Generic; using fx.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace fx.Charting.IndicatorPainters
{
    public abstract class BaseChartIndicatorPainter : BaseVM, ICloneable, IPersistable, IChartIndicatorPainter
    {
        private readonly PooledList< IfxChartElement >  _innerElements    = new PooledList< IfxChartElement >( );
        private readonly IndicatorPainterHelper _indicatorPainter = new IndicatorPainterHelper( );
        private IndicatorUI                     _indicatorElement;

        [Browsable( false )]
        public IndicatorUI Element
        {
            get
            {
                return _indicatorElement;
            }
            private set
            {
                _indicatorElement = value;
            }
        }

        [Browsable( false )]
        public IEnumerable< IfxChartElement > InnerElements
        {
            get
            {
                return _innerElements;
            }
        }

        protected IIndicator Indicator
        {
            get
            {
                return _indicatorPainter.Indicator;
            }
        }

        protected bool IsAttached
        {
            get
            {
                return Element != null;
            }
        }

        private void ChildElementsStartDrawing( )
        {
            var elements = Element.ChildElements.OfType<IDrawableChartElement>( );

            foreach ( var element in elements )
            {
                element.StartDrawing( );
            }
        }

        protected abstract bool OnDraw( );

        public virtual bool Draw( ChartDrawDataEx data )
        {
            if( !IsAttached )
            {
                return false;
            }

            var indicatorDataList = data.GetIndicator( Element );

            if( indicatorDataList != null && !indicatorDataList.IsEmpty( ) )
            {
                _indicatorPainter.Reset( false );

                foreach( var indicatorData in indicatorDataList )
                {
                    _indicatorPainter.UpdateIndicator( indicatorData.Value, indicatorData.Time );
                }

                if ( _indicatorPainter.GetCount( ) > 0 )
                {
                    return OnDraw( );
                }

                return false;
            }
            ChildElementsStartDrawing( );
            return false;
        }

        public virtual void Reset( )
        {
            _indicatorPainter.Reset( true );
        }

        public void OnAttached( IndicatorUI element )
        {
            Element = element;

            foreach ( IfxChartElement chartElement in InnerElements )
            {
                Element.AddChildElement( chartElement, false );
            }
        }

        public void OnDetached( )
        {
            if( Element == null )
            {
                return;
            }

            foreach ( IfxChartElement chartElement in InnerElements )
            {
                Element.RemoveChildElement( chartElement );
            }

            Element = null;
        }

        protected bool DrawValues( IIndicator ind, IfxChartElement element, Func<IIndicatorValue, double> getValue )
        {
            var result = _indicatorPainter.GetIndicatorValueList( ind );

            Func<int, double> indicatorValueFunc = ( i =>
            {
                if ( !_indicatorPainter.IsIndicatorFormed( i ) )
                {
                    return double.NaN;
                }

                return getValue( result?[ i ] );
            } );

            return GetIndicatorValuesAndDraw( element, indicatorValueFunc, null );
        }

        protected bool DrawValues( IIndicator ind, IfxChartElement element )
        {
            var result = _indicatorPainter.GetIndicatorValueList( ind );

            Func<int, double> indicatorValueFunc = ( i => GetIndicatorValueAtIndex( result, i ) );

            return GetIndicatorValuesAndDraw( element, indicatorValueFunc, null );
        }

        protected bool DrawValues( IIndicator ind1, IIndicator ind2, IfxChartElement element )
        {
            var result1 = _indicatorPainter.GetIndicatorValueList( ind1 );
            var result2 = _indicatorPainter.GetIndicatorValueList( ind2 );

            Func<int, double> myFunc1 = ( i => GetIndicatorValueAtIndex( result1, i ) );
            Func<int, double> myFunc2 = ( i => GetIndicatorValueAtIndex( result2, i ) );

            return GetIndicatorValuesAndDraw( element, myFunc1, myFunc2 );
        }

        protected bool DrawValues( IIndicator ind1, IIndicator ind2, IfxChartElement element, Func<double, double, double> op )
        {
            var result1 = _indicatorPainter.GetIndicatorValueList( ind1 );
            var result2 = _indicatorPainter.GetIndicatorValueList( ind2 );

            Func<int, double> indicatorValueFunc = ( i => ( op( GetIndicatorValueAtIndex( result1, i ), GetIndicatorValueAtIndex( result2, i ) ) ) );

            return GetIndicatorValuesAndDraw( element, indicatorValueFunc, null );
        }

        private bool GetIndicatorValuesAndDraw( IfxChartElement chartElement, Func< int, double > myFunc1, Func< int, double > myFunc2 )
        {
            var drawableElement = chartElement as IDrawableChartElement;

            if ( drawableElement == null )
            {
                throw new InvalidOperationException( "invalid chart element" );
            }

            var drawValues = Enumerable.Range( 0, _indicatorPainter.GetCount( ) ) .Select( i =>
                                                                                                {
                                                                                                    DateTime dateTime = _indicatorPainter.DateTimeList[ i ];
                                                                                                    double value1 = myFunc1( i );
                                                                                                    double value2 = myFunc2 != null ? myFunc2( i ) : double.NaN;

                                                                                                    return ChartDrawDataEx.sxTuple< DateTime >.CreateSxTuple( dateTime, value1, value2 );
                                                                                                } 
                                                                                          )
                                                                                    .Cast< ChartDrawDataEx.IDrawValue >( )
                                                                                    .ToEx( _indicatorPainter.GetCount( ) );

            return drawableElement.StartDrawing( drawValues );
        }

        private double GetIndicatorValueAtIndex( ReadOnlyCollection< IIndicatorValue > indicatorValues, int index )
        {
            if( indicatorValues == null || index >= indicatorValues.Count )
            {
                return double.NaN;
            }

            IIndicatorValue iindicatorValue = indicatorValues[ index ];

            if ( iindicatorValue != null && !iindicatorValue.IsEmpty && _indicatorPainter.IsIndicatorFormed( index ) )
            {
                return Decimal.ToDouble( iindicatorValue.GetValue< Decimal >( ) );
            }

            return double.NaN;
        }

        protected void AddChildElement( IfxChartElement element )
        {
            if( InnerElements.Contains( element ) )
            {
                throw new ArgumentException( nameof( element ) );
            }
            _innerElements.Add( element );
        }

        public object Clone( )
        {
            BaseChartIndicatorPainter instance = ( BaseChartIndicatorPainter )Activator.CreateInstance( GetType( ) );
            CopyTo( instance );
            return instance;
        }

        public void CopyTo( object obj )
        {
            if( obj.GetType( ) != GetType( ) )
            {
                throw new InvalidOperationException( "Unexpected type of other painter." );
            }

            BaseChartIndicatorPainter indicatorPainter = ( BaseChartIndicatorPainter )obj;

            if ( _innerElements.Count != indicatorPainter._innerElements.Count )
            {
                throw new InvalidOperationException( "Unexpected number of inner elements on the painter clone." );
            }

            for ( int index = 0; index < _innerElements.Count; ++index )
            {
                ( ( IElementWithXYAxes )_innerElements[ index ] ).Clone( indicatorPainter._innerElements[ index ] );
            }
        }

        public virtual void Load( SettingsStorage storage )
        {
        }

        public virtual void Save( SettingsStorage storage )
        {
        }        

        private sealed class IndicatorPainterHelper
        {
            private readonly PooledDictionary< IIndicator, PooledList< IIndicatorValue > > _indicatorToValueMap = new PooledDictionary< IIndicator, PooledList< IIndicatorValue > >( );
            private readonly PooledList< DateTime >                                  _dateTimeList        = new PooledList< DateTime >( );
            private ReadOnlyCollection< IIndicatorValue >                      _indicatorValueCollection;
            private IIndicator                                                 _indicator;

            public PooledList<DateTime> DateTimeList
            {
                get

                {
                    return _dateTimeList;
                }
            }

            public int GetCount( )
            {
                return DateTimeList.Count;
            }

            public bool IsIndicatorFormed( int index )
            {
                if( index >= DateTimeList.Count )
                {
                    return false;
                }

                var indicatorValueCol = _indicatorValueCollection;

                if ( indicatorValueCol == null )
                {
                    return false;
                }

                return indicatorValueCol[ index ].IsFormed;
            }

            public IIndicator Indicator
            {
                get
                {
                    return _indicator;
                }

                set
                {
                    _indicator = value;
                }
            }

            public void Reset( bool clearAll )
            {
                DateTimeList.Clear( );

                if( clearAll )
                {
                    _indicatorToValueMap.Clear( );
                    Indicator = null;
                    _indicatorValueCollection = null;
                }
                else
                {
                    foreach ( KeyValuePair<IIndicator, PooledList<IIndicatorValue>> pair in _indicatorToValueMap )
                    {
                        pair.Value.Clear( );
                    }

                    if ( GetCount( ) <= 4096 )
                    {
                        return;
                    }

                    foreach ( KeyValuePair<IIndicator, PooledList<IIndicatorValue>> pair in _indicatorToValueMap )
                    {
                        pair.Value.TrimExcess( );
                    }
                }
            }

            private void UpdateIndicatorValue( IIndicator indicator, IIndicatorValue indicatorValue )
            {
                AppendIndicatorValue( indicator, GetCount( ), indicatorValue );

                if( !( indicatorValue is ComplexIndicatorValue complexIndicatorValue ) )
                {
                    return;
                }

                using( IEnumerator< KeyValuePair< IIndicator, IIndicatorValue > > enumerator = complexIndicatorValue.InnerValues.GetEnumerator( ) )
                {
                    while( enumerator.MoveNext( ) )
                    {
                        KeyValuePair< IIndicator, IIndicatorValue > current = enumerator.Current;
                        UpdateIndicatorValue( current.Key, current.Value );
                    }
                }
            }

            public void UpdateIndicator( IIndicatorValue indicator, DateTime datetimeUtc )
            {
                AddIndicator( indicator.Indicator );
                UpdateIndicatorValue( indicator.Indicator, indicator );
                DateTimeList.Add( datetimeUtc );
            }

            private void AppendIndicatorValue( IIndicator indicator, int count, IIndicatorValue newIndicatorValue )
            {
                PooledList< IIndicatorValue > valueList;

                if( !_indicatorToValueMap.TryGetValue( indicator, out valueList ) )
                {
                    _indicatorToValueMap[ indicator ] = valueList = new PooledList< IIndicatorValue >( );
                }

                if( valueList.Count < count )
                {
                    valueList.AddRange( Enumerable.Range( 0, count - valueList.Count ).Select<int, IIndicatorValue>( i => null ) );
                }

                valueList.Add( newIndicatorValue );
            }

            private void AddIndicator( IIndicator addedIndicator )
            {
                if( addedIndicator == Indicator )
                {
                    return;
                }

                if( Indicator != null )
                {
                    throw new InvalidOperationException( LocalizedStrings.NewIndicatorNoReset );
                }

                Indicator = addedIndicator;
                
                PooledList< IIndicatorValue > valueList;

                if( !_indicatorToValueMap.TryGetValue( addedIndicator, out valueList ) )
                {
                    _indicatorToValueMap[ addedIndicator ] = valueList = new PooledList< IIndicatorValue >( );
                }

                _indicatorValueCollection = valueList.AsReadOnly( );
            }

            public ReadOnlyCollection< IIndicatorValue > GetIndicatorValueList( IIndicator indicator )
            {
                return _indicatorToValueMap.TryGetValue( indicator )?.AsReadOnly( );
            }            
        }
    }
}
