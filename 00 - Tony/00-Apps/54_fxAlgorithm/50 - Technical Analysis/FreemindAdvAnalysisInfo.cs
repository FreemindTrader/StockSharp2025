using fx.Collections;

using fx.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fx.Bars;

namespace fx.Algorithm
{
    public class FreemindAdvAnalysisInfo
    {
        private ThreadSafeDictionary< TADivergence, DivergenceInfo >                              _divergenceInfo = new ThreadSafeDictionary< TADivergence, DivergenceInfo >( );
        private ThreadSafeDictionary< TaWaveRotation, WaveRotationInfo >                          _waveRotationTimeInfo = new ThreadSafeDictionary< TaWaveRotation, WaveRotationInfo >( );
        private ThreadSafeDictionary< TaGannPriceTimeType, GannPriceTimeInfo >                    _gannPriceTimeInfo = new ThreadSafeDictionary< TaGannPriceTimeType, GannPriceTimeInfo >( );
        private ThreadSafeDictionary< Tuple< TaGannPriceTimeType, int, int >, TaGannPriceTimeType > _gannCache = new ThreadSafeDictionary< Tuple< TaGannPriceTimeType, int, int >, TaGannPriceTimeType >( );
        private PooledList< MatchedSRinfo >                                                           _matchedPivotLevels = new PooledList< MatchedSRinfo >( );

        private StructureLabelEnum _structureLabel;

        //private TACandle? _candleStickPatterns;
        private TABottomingSignal? _bottomingSignals;
        private TAToppingSignal? _toppingSignals;

        

        public StructureLabelEnum StructureLabel
        {
            get
            {
                return _structureLabel;
            }
            set
            {
                _structureLabel = value;
            }
        }

        public FreemindAdvAnalysisInfo(  )
        {
            
        }

        public void AddDivergenceInfo( DivergenceInfo info )
        {
            if( !_divergenceInfo.ContainsKey( info.Divergence ) )
            {
                _divergenceInfo.Add( info.Divergence, info );
            }
        }

        public void AddSignalInfo( TABottomingSignal info )
        {
            var temp = _bottomingSignals;

            if( temp.HasValue )
            {
                temp = temp | info;
            }
            else
            {
                temp = info;
            }

            _bottomingSignals = temp;
        }

        public void AddSignalInfo( TAToppingSignal info )
        {
            var temp = _toppingSignals;

            if( temp.HasValue )
            {
                temp = temp | info;
            }
            else
            {
                temp = info;
            }

            _toppingSignals = temp;
        }

        public void AddPivotRelationship( PooledList< MatchedSRinfo > info )
        {
            foreach( MatchedSRinfo matchedPivotInfo in info )
            {
                if( _matchedPivotLevels.FindIndex( x => x.Equals( matchedPivotInfo ) ) == -1 )
                {
                    _matchedPivotLevels.Add( matchedPivotInfo );
                }
            }
        }

        public PooledList< DivergenceInfo > GetDivergenceInfo( )
        {
            var output = new PooledList< DivergenceInfo >( );

            foreach( KeyValuePair< TADivergence, DivergenceInfo > tADivergence in _divergenceInfo )
            {
                output.Add( tADivergence.Value );
            }

            output.Sort( );

            return output;
        }

        public PooledList< DivergenceInfo > GetDivergenceInfoDescending( )
        {
            var output = new PooledList< DivergenceInfo >( );

            foreach( KeyValuePair< TADivergence, DivergenceInfo > tADivergence in _divergenceInfo )
            {
                output.Add( tADivergence.Value );
            }

            output.Sort( );
            output.Reverse( );

            return output;
        }

        public PooledList< MatchedSRinfo > GetPivotRelations( )
        {
            return _matchedPivotLevels;
        }

        public void AddWaveImportantTimeInfo( WaveRotationInfo info )
        {
            if( !_waveRotationTimeInfo.ContainsKey( info.GannPriceTimeType ) )
            {
                _waveRotationTimeInfo.Add( info.GannPriceTimeType, info );
            }
        }

        public void AddGannPriceTimeInfo( GannPriceTimeInfo info )
        {
            if( !_gannPriceTimeInfo.ContainsKey( info.GannPriceTimeType ) )
            {
                _gannPriceTimeInfo.Add( info.GannPriceTimeType, info );

                var search = new Tuple< TaGannPriceTimeType, int, int >( info.GetGeneralType( ), info.BeginBarIndex, info.ParentBarIndex );

                _gannCache.Add( search, info.GannPriceTimeType );
            }
        }

        public bool CheckAndAddGannPriceTimeInfo( GannPriceTimeInfo info )
        {
            var generalType = info.GetGeneralType( );

            var search = new Tuple< TaGannPriceTimeType, int, int >( generalType, info.BeginBarIndex, info.ParentBarIndex );

            if( _gannCache.ContainsKey( search ) )
            {
                var priceTimeType = _gannCache[ search ];

                if( priceTimeType != info.GannPriceTimeType )
                {
                    if( priceTimeType > info.GannPriceTimeType )
                    {
                        if( ( priceTimeType - info.GannPriceTimeType ) <= 2 )
                        {
                            return false;
                        }
                    }
                    else if( priceTimeType < info.GannPriceTimeType )
                    {
                        if( Math.Abs( info.GannPriceTimeType - priceTimeType ) <= 2 )
                        {
                            _gannPriceTimeInfo[ info.GannPriceTimeType ] = info;
                            _gannCache[ search ] = info.GannPriceTimeType;

                            return true;
                        }
                    }
                }
            }
            else if( !_gannPriceTimeInfo.ContainsKey( info.GannPriceTimeType ) )
            {
                _gannPriceTimeInfo.Add( info.GannPriceTimeType, info );

                search = new Tuple< TaGannPriceTimeType, int, int >( info.GetGeneralType( ), info.BeginBarIndex, info.ParentBarIndex );

                _gannCache.Add( search, info.GannPriceTimeType );

                return true;
            }

            return false;
        }

        public PooledList< WaveRotationInfo > GetWaveImportantTimeInfo( )
        {
            var output = new PooledList< WaveRotationInfo >( );

            foreach( var waves in _waveRotationTimeInfo )
            {
                output.Add( waves.Value );
            }

            return output;
        }

        public PooledList< GannPriceTimeInfo > GetGannPriceTimeInfo( )
        {
            var output = new PooledList< GannPriceTimeInfo >( );

            foreach( var waves in _gannPriceTimeInfo )
            {
                output.Add( waves.Value );
            }

            return output;
        }

        public PooledList< TAToppingSignal > GetToppingSignals( )
        {
            var output = new PooledList< TAToppingSignal >( );

            if( _toppingSignals.HasValue )
            {
                if( _toppingSignals.Value.HasFlag( TAToppingSignal.MACD_CROSS_DOWN ) )
                {
                    output.Add( TAToppingSignal.MACD_CROSS_DOWN );
                }

                if( _toppingSignals.Value.HasFlag( TAToppingSignal.WAVE_PEAK ) )
                {
                    output.Add( TAToppingSignal.WAVE_PEAK );
                }

                if( _toppingSignals.Value.HasFlag( TAToppingSignal.GANN_PEAK ) )
                {
                    output.Add( TAToppingSignal.GANN_PEAK );
                }

                if( _toppingSignals.Value.HasFlag( TAToppingSignal.ExitOverBought ) )
                {
                    output.Add( TAToppingSignal.ExitOverBought );
                }

                if( _toppingSignals.Value.HasFlag( TAToppingSignal.OscillatorCrossDown ) )
                {
                    output.Add( TAToppingSignal.OscillatorCrossDown );
                }

                if( _toppingSignals.Value.HasFlag( TAToppingSignal.OscNegativeDivergence ) )
                {
                    output.Add( TAToppingSignal.OscNegativeDivergence );
                }

                if( _toppingSignals.Value.HasFlag( TAToppingSignal.MAXIMUM_MACD ) )
                {
                    output.Add( TAToppingSignal.MAXIMUM_MACD );
                }

                if( _toppingSignals.Value.HasFlag( TAToppingSignal.ComasTurnDown ) )
                {
                    output.Add( TAToppingSignal.ComasTurnDown );
                }

                if( _toppingSignals.Value.HasFlag( TAToppingSignal.ComasCrossDown ) )
                {
                    output.Add( TAToppingSignal.ComasCrossDown );
                }

                if( _toppingSignals.Value.HasFlag( TAToppingSignal.OscillatorSmoothDown ) )
                {
                    output.Add( TAToppingSignal.OscillatorSmoothDown );
                }
            }

            return output;
        }

        public PooledList< TABottomingSignal > GetBottomingSignals( )
        {
            var output = new PooledList< TABottomingSignal >( );

            if( _bottomingSignals.HasValue )
            {
                if( _bottomingSignals.Value.HasFlag( TABottomingSignal.MACD_CROSS_UP ) )
                {
                    output.Add( TABottomingSignal.MACD_CROSS_UP );
                }

                if( _bottomingSignals.Value.HasFlag( TABottomingSignal.WAVE_TROUGH ) )
                {
                    output.Add( TABottomingSignal.WAVE_TROUGH );
                }

                if( _bottomingSignals.Value.HasFlag( TABottomingSignal.GANN_TROUGH ) )
                {
                    output.Add( TABottomingSignal.GANN_TROUGH );
                }

                if( _bottomingSignals.Value.HasFlag( TABottomingSignal.ExitOverSold ) )
                {
                    output.Add( TABottomingSignal.ExitOverSold );
                }

                if( _bottomingSignals.Value.HasFlag( TABottomingSignal.OscillatorCrossUp ) )
                {
                    output.Add( TABottomingSignal.OscillatorCrossUp );
                }

                if( _bottomingSignals.Value.HasFlag( TABottomingSignal.OscPositiveDivergence ) )
                {
                    output.Add( TABottomingSignal.OscPositiveDivergence );
                }

                if( _bottomingSignals.Value.HasFlag( TABottomingSignal.MINIMUM_MACD ) )
                {
                    output.Add( TABottomingSignal.MINIMUM_MACD );
                }

                if( _bottomingSignals.Value.HasFlag( TABottomingSignal.ComasTurnUp ) )
                {
                    output.Add( TABottomingSignal.ComasTurnUp );
                }

                if( _bottomingSignals.Value.HasFlag( TABottomingSignal.ComasCrossUp ) )
                {
                    output.Add( TABottomingSignal.ComasCrossUp );
                }

                if( _bottomingSignals.Value.HasFlag( TABottomingSignal.OscillatorSmoothUp ) )
                {
                    output.Add( TABottomingSignal.OscillatorSmoothUp );
                }
            }

            return output;
        }
    }
}
