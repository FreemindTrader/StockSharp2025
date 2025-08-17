using fx.Collections;
using Ecng.Collections;
using fx.Common;
using fx.Definitions;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using Ecng.Logging;

namespace fx.Algorithm
{
    public enum ChartTypeEnum : byte
    {
        Line,
        Histogram,
        ColoredArea
    }

    /// <summary>
    /// Hosts the indicators calculation results. An indicator typically has one or a few output signalling sets of
    /// ouput values (lines).
    /// </summary>
    [Serializable]
    public class IndicatorResults : BaseLogReceiver, ISerializable
    {
        private SynchronizedDictionary< string, ChartTypeEnum > _resultSetsChartTypes = new SynchronizedDictionary< string, ChartTypeEnum >( );
        private SynchronizedDictionary< string, fxList< double > > _resultSets = new SynchronizedDictionary< string, fxList< double > >( );
        private const int _MaximumValueConstraint = 100000;   // Values above this will cause automatic scale down, since otherwise they tend to overload the rendering engine.        
        private volatile float _dynamicMultiplicator = 1;        // When data is too big, we need to multiply down to fit into float range.

        /// <summary>
        /// Thread safe access to names of output sets.
        /// </summary>
        public PooledList< string > SetsNamesList
        {
            get
            {
                return _resultSets.Keys.ToPooledList();
            }
        }

        /// <summary>
        /// Thread unsafe way of accessing output sets names.
        /// </summary>
        public IEnumerable< string > SetsNamesUnsafe
        {
            get
            {
                return _resultSets.Keys;
            }
        }

        /// <summary>
        /// Values of a set, by index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public IList< double > this[ int index ]
        {
            get
            {
                int i = 0;

                foreach( string name in _resultSets.Keys )
                {
                    if( i == index )
                    {
                        return _resultSets[ name ].AsMutableList( );
                    }
                }

                return null;
            }
        }

        public IList< double > this[ long index ]
        {
            get
            {
                long i = 0;

                foreach( string name in _resultSets.Keys )
                {
                    if( i == index )
                    {
                        return _resultSets[ name ].AsMutableList( );
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Values of a set, by name.
        /// </summary>
        public IList< double > this[ string name ]
        {
            get
            {
                return _resultSets[ name ].AsMutableList( );
            }
        }

        /// <summary>
        /// How many result sets are in this container.
        /// </summary>
        public int SetsCount
        {
            get
            {
                return _resultSets.Count;
            }
        }

        /// <summary>
        /// What is the length of the sets.
        /// </summary>
        public int SetLength
        {
            get
            {
                foreach( var list in _resultSets.Values )
                {
                    return ( int )list.Count;
                }

                return 0;
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public IndicatorResults( Indicator indicator, string[ ] resultSetNames )
        {
            foreach( string name in resultSetNames )
            {
                _resultSets[ name ] = new fxList< double >( );
            }
        }

        public IndicatorResults( Indicator indicator, string[ ] resultSetNames, int count )
        {
            foreach( string name in resultSetNames )
            {
                _resultSets[ name ] = new fxList< double >( count );
            }
        }

        /// <summary>
        /// Deserialization constructor.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public IndicatorResults( SerializationInfo info, StreamingContext context )
        {
            string[ ] names = ( string[ ] )info.GetValue( "resultSetsNames", typeof( string[ ] ) );

            foreach( string name in names )
            {
                _resultSets.Add( name, new fxList< double >( ) );
            }

            _resultSetsChartTypes = ( SynchronizedDictionary< string, ChartTypeEnum > )info.GetValue( "resultSetsChartTypes", typeof( SynchronizedDictionary< string, ChartTypeEnum > ) );
        }

        /// <summary>
        /// Serialization routine.
        /// </summary>
        public void GetObjectData( SerializationInfo info, StreamingContext context )
        {
            info.AddValue( "resultSetsNames", GeneralHelper.EnumerableToArray( _resultSets.Keys ) );
            info.AddValue( "resultSetsChartTypes", _resultSetsChartTypes );
        }

        /// <summary>
        ///
        /// </summary>
        public void Clear( )
        {
            foreach( string resultSet in _resultSets.Keys )
            {
                _resultSets[ resultSet ].Clear( );
            }
        }

        /// <summary>
        /// Clip all results to maximum count.
        /// </summary>
        /// <param name="count"></param>
        public void ClipTo( int count )
        {
            //lock( this )
            //{
            //    foreach( string resultSet in _resultSets.Keys )
            //    {
            //        if ( _resultSets[ resultSet ].Count > count )
            //        {
            //            _resultSets[ resultSet ].RemoveRange( count, _resultSets[ resultSet ].Count - count );
            //        }

            //        _resultSets[ resultSet ].Clear( );
            //    }
            //}
        }

        private bool UpdateDynamicMultiplicator( ref double[ ] values1 )
        {
            foreach( double value in values1 )
            {
                if( ( value * _dynamicMultiplicator ) > _MaximumValueConstraint )
                {
                    _dynamicMultiplicator *= 0.1f;
                    UpdateDynamicMultiplicator( ref values1 );
                    return true;
                }
            }

            return false;
        }

        private bool UpdateDynamicMultiplicator( ref int[ ] values1 )
        {
            foreach( int value in values1 )
            {
                if( ( value * _dynamicMultiplicator ) > _MaximumValueConstraint )
                {
                    _dynamicMultiplicator *= 0.1f;
                    UpdateDynamicMultiplicator( ref values1 );
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// This used to handle results. inputResultPiece stores results, where 0 corresponds to startingIndex; the
        /// length of inputResultPiece may be larger than count.
        /// </summary>
        public bool AddSetValues( string name, int startingIndex, int count, bool overrideExistingValues, double[ ] inputResultPiece )
        {
            if( _resultSets.ContainsKey( name ) == false )
            {
                this.LogError( "SetResultSetValues result set [" + name + "] not found." );
                return false;
            }

            var indicatorfxList = _resultSets[ name ];

            for( long i = indicatorfxList.Count; i < startingIndex; i++ )
            {
                // Only if there are some empty spaces before the start, fill them with no value.
                indicatorfxList.Add( double.NaN );
            }

            var endIndex = startingIndex + count;

            // Get the dataSource from the result it is provided to us.
            for( int i = startingIndex; i < endIndex; i++ )
            {
                if( indicatorfxList.Count <= i )
                {
                    indicatorfxList.Add( inputResultPiece[ i - startingIndex ] );
                }
                else
                {
                    if( overrideExistingValues )
                    {
                        indicatorfxList.Update( i, inputResultPiece[ i - startingIndex ] );
                    }
                }
            }

            return true;
        }

        public bool AddSetValues( string name, int startingIndex, int count, bool overrideExistingValues, Span<double> inputResultPiece )
        {
            if ( _resultSets.ContainsKey( name ) == false )
            {
                this.LogError( "SetResultSetValues result set [" + name + "] not found." );
                return false;
            }

            var indicatorfxList = _resultSets[ name ];

            for ( long i = indicatorfxList.Count; i < startingIndex; i++ )
            {
                // Only if there are some empty spaces before the start, fill them with no value.
                indicatorfxList.Add( double.NaN );
            }

            var endIndex = startingIndex + count;

            // Get the dataSource from the result it is provided to us.
            for ( int i = startingIndex; i < endIndex; i++ )
            {
                if ( indicatorfxList.Count <= i )
                {
                    indicatorfxList.Add( inputResultPiece[ i - startingIndex ] );
                }
                else
                {
                    if ( overrideExistingValues )
                    {
                        indicatorfxList.Update( i, inputResultPiece[ i - startingIndex ] );
                    }
                }
            }

            return true;
        }

        public bool AddSetValues( string name, int startingIndex, int count, bool overrideExistingValues, int[ ] inputResultPiece )
        {
            if( _resultSets.ContainsKey( name ) == false )
            {
                this.LogError( "SetResultSetValues result set [" + name + "] not found." );
                return false;
            }

            var indicatorfxList = _resultSets[ name ];

            for( long i = indicatorfxList.Count; i < startingIndex; i++ )
            {
                // Only if there are some empty spaces before the start, fill them with no value.
                indicatorfxList.Add( double.NaN );
            }

            var endIndex = startingIndex + count;

            // Get the dataSource from the result it is provided to us.
            for( int i = startingIndex; i < endIndex; i++ )
            {
                if( indicatorfxList.Count <= i )
                {
                    indicatorfxList.Add( inputResultPiece[ i - startingIndex ] );
                }
                else
                {
                    if( overrideExistingValues )
                    {
                        throw new NotImplementedException( );
                        //indicatorfxList.Update( i, inputResultPiece[ i - startingIndex ] );
                    }
                }
            }

            if( indicatorfxList.Count > ( endIndex + 1 ) )
            {
                throw new InvalidOperationException( );
            }

            return true;
        }

        /// <summary>
        ///
        /// </summary>
        public double? GetValueSetCurrentValue( int setIndex )
        {
            var set = this[ setIndex ];

            if( set == null || set.Count == 0 )
            {
                return null;
            }

            return set.ElementAt( set.Count - 1 );
        }

        /// <summary>
        ///
        /// </summary>
        public double? GetValueSetCurrentValue( string setName )
        {
            var set = this[ setName ];

            if( set == null )
            {
                return null;
            }

            return set.ElementAt( set.Count - 1 );
        }

        /// <summary>
        ///
        /// </summary>
        public void SetResultSetChartType( string setName, ChartTypeEnum chartType )
        {
            _resultSetsChartTypes[ setName ] = chartType;
        }

        /// <summary>
        /// Obtain the chart type for this result set.
        /// </summary>
        public ChartTypeEnum? GetResultSetChartType( string setName )
        {
            if( _resultSetsChartTypes.ContainsKey( setName ) )
            {
                return _resultSetsChartTypes[ setName ];
            }

            return null;
        }
    }
}