using fx.Collections;

using fx.Definitions;
using System;
using System.Collections.Generic;
using System.Text;
using fx.Bars;
using fx.Common;

namespace fx.Algorithm
{
    public class FibLevelsCollection
    {
        private FibKey _fibKey = null;
        private PooledList< FibLevelInfo > _fibLevels = new PooledList< FibLevelInfo >( );
        private DictionarySlim<FibLevelInfo, PooledList<MatchedSRinfo>> _majorSL = new DictionarySlim<FibLevelInfo, PooledList<MatchedSRinfo>>();
       

        public FibLevelsCollection( FibonacciType fibType, ( DateTime, float ) startPoint, ( DateTime, float ) endPoint, ( DateTime, float ) projectionPoint )
        {
            _fibKey = new FibKey( fibType, startPoint, endPoint, projectionPoint );

            CalcProjection( fibType, startPoint.Item2, endPoint.Item2, projectionPoint.Item2 );
        }

        public FibLevelsCollection( FibonacciType fibType, ( DateTime, float ) startPoint, ( DateTime, float ) endPoint )
        {
            _fibKey = new FibKey( fibType, startPoint, endPoint );

            CalcRetracement( fibType, startPoint.Item2, endPoint.Item2 );
        }

        public FibKey Key
        {
            get
            {
                return _fibKey;
            }
        }

        public float StartPoint
        {
            get
            {
                if( _fibKey != null )
                {
                    return _fibKey.Start.Item2;
                }

                return -1;
            }

            //set
            //{
            //    _startPoint = value;
            //}
        }

        public float EndPoint
        {
            get
            {
                if( _fibKey != null )
                {
                    return _fibKey.End.Item2;
                }

                return -1;
            }

            //get { return _endPoint; }
            //set
            //{
            //    _endPoint = value;
            //}
        }

        public float ProjectionPoint
        {
            get
            {
                if( _fibKey != null && _fibKey.Projection != default )
                {
                    return _fibKey.Projection.Item2;
                }

                return -1;
            }

            //get { return _projectionPoint; }
            //set
            //{
            //    _projectionPoint = value;
            //}
        }

        public FibonacciType FibType
        {
            get
            {
                if( _fibKey != null )
                {
                    return _fibKey.FibType;
                }

                return FibonacciType.NONE;
            }

            //get { return _fibType; }
            //set
            //{
            //    _fibType = value;
            //}
        }

        public PooledList< FibLevelInfo > FibLevels
        {
            get
            {
                return _fibLevels;
            }
        }

        public void CalcRetracement( 
                                    FibonacciType fibCalculationType,
                                    float startPoint,
                                    float endPoint )
        {
            int i = 0;

            switch( fibCalculationType )
            {
                case FibonacciType.Wave2Retracement:
                {
                    for( i = 0; i < GlobalConstants.Wave2RetracementLevels.Length; i++ )
                    {
                        var fibValue = endPoint + ( ( startPoint - endPoint ) * GlobalConstants.Wave2RetracementLevels[ i ] / 100 );
                        var info = new FibLevelInfo( GlobalConstants.Wave2RetracementFibLevelType[ i ], fibValue, GlobalConstants.Wave2RetracementStrength[ i ] );

                        _fibLevels.Add( info );                        
                    }
                }
                    break;

                case FibonacciType.TonyRetracement:
                {
                    for ( i = 0; i < GlobalConstants.Wave4RetracementLevels.Length; i++ )
                    {
                        for ( i = 0; i < GlobalConstants.Wave4RetracementLevels.Length; i++ )
                        {
                            var fibValue = endPoint + ( ( startPoint - endPoint ) * GlobalConstants.Wave4RetracementLevels[ i ] / 100 );
                            var info = new FibLevelInfo( GlobalConstants.Wave4RetracementFibLevelType[ i ], fibValue, GlobalConstants.Wave4RetracementStrength[ i ] );

                            _fibLevels.Add( info );
                        }
                    }
                }
                break;

                case FibonacciType.Wave4Retracement:
                {
                    for( i = 0; i < GlobalConstants.Wave4RetracementLevels.Length; i++ )
                    {
                        var fibValue = endPoint + ( ( startPoint - endPoint ) * GlobalConstants.Wave4RetracementLevels[ i ] / 100 );
                        var info = new FibLevelInfo( GlobalConstants.Wave4RetracementFibLevelType[ i ], fibValue, GlobalConstants.Wave4RetracementStrength[ i ] );

                        _fibLevels.Add( info );                        
                    }
                }
                    break;

                case FibonacciType.ABCWaveBRetracement:
                {
                    for( i = 0; i < GlobalConstants.ABCWaveBRetracementLevels.Length; i++ )
                    {
                        var fibValue = endPoint + ( ( startPoint - endPoint ) * GlobalConstants.ABCWaveBRetracementLevels[ i ] / 100 );
                        var info = new FibLevelInfo( GlobalConstants.ABCWaveBRetracementFibLevelType[ i ], fibValue, GlobalConstants.ABCWaveBRetracementStrength[ i ] );

                        _fibLevels.Add( info );                        
                    }
                }
                    break;

                case FibonacciType.WaveEFBRetracement:
                {
                    for( i = 0; i < GlobalConstants.WaveEFBRetracementLevels.Length; i++ )
                    {
                        var fibValue = endPoint + ( ( startPoint - endPoint ) * GlobalConstants.WaveEFBRetracementLevels[ i ] / 100 );
                        var info = new FibLevelInfo( GlobalConstants.WaveEFBRetracementFibLevelType[ i ], fibValue, GlobalConstants.WaveEFBRetracementStrength[ i ] );

                        _fibLevels.Add( info );                        
                    }
                }
                    break;

                case FibonacciType.WaveTriBRetracement:
                {
                    for( i = 0; i < GlobalConstants.WaveTriBRetracementLevels.Length; i++ )
                    {
                        var fibValue = endPoint + ( ( startPoint - endPoint ) * GlobalConstants.WaveTriBRetracementLevels[ i ] / 100 );
                        var info = new FibLevelInfo( GlobalConstants.WaveTriBRetracementFibLevelType[ i ], fibValue, GlobalConstants.WaveTriBRetracementStrength[ i ] );

                        _fibLevels.Add( info );                        
                    }
                }
                break;

                case FibonacciType.TonyProjection:
                {
                    for ( i = 0; i < GlobalConstants.TonyDiscoveryLevels.Length; i++ )
                    {
                        var fibValue = endPoint + ( ( startPoint - endPoint ) * GlobalConstants.TonyDiscoveryLevels [ i ] / 100 );
                        var info = new FibLevelInfo( GlobalConstants.TonyDiscoveryLevelType[ i ], fibValue, GlobalConstants.TonyDiscoveryLevelsStrength[ i ] );

                        _fibLevels.Add( info );                        
                    }
                }
                break;

                
            }
        }

        public void CalcProjection( FibonacciType fibCalculationType,
                                    float startPoint,
                                    float endPoint,
                                    float projectionPoint )
        {
            int i = 0;

            switch( fibCalculationType )
            {
                case FibonacciType.WaveCProjection:
                {
                    for( i = 0; i < GlobalConstants.Wave3CProjectionLevels.Length; i++ )
                    {
                        var fibValue = projectionPoint + ( ( endPoint - startPoint ) * GlobalConstants.Wave3CProjectionLevels[ i ] / 100 );
                        var info = new FibLevelInfo( GlobalConstants.Wave3CProjectionFibLevelType[ i ], fibValue, GlobalConstants.Wave3CProjectionStrength[ i ] );

                        _fibLevels.Add( info );                        
                    }
                }
                    break;

                case FibonacciType.Wave3All:
                {
                    for( i = 0; i < GlobalConstants.Wave3AllProjectionLevels.Length; i++ )
                    {
                        var fibValue = projectionPoint + ( ( endPoint - startPoint ) * GlobalConstants.Wave3AllProjectionLevels[ i ] / 100 );
                        var info = new FibLevelInfo( GlobalConstants.Wave3AllFibLevelType[i], fibValue, GlobalConstants.Wave3AllProjectionStrength[i] );

                        _fibLevels.Add( info );                        
                    }
                }
                    break;

                case FibonacciType.CompactWave3:
                {
                    for ( i = 0; i < Wave3Compressed.CompressWave3.Length; i++ )
                    {
                        var fibValue = projectionPoint + ( ( endPoint - startPoint ) * Wave3Compressed.CompressWave3[i].Value / 100 );
                        var info = new FibLevelInfo( Wave3Compressed.CompressWave3[i].Percentage, ( float )fibValue, Wave3Compressed.CompressWave3[i].FibStrength );

                        _fibLevels.Add( info );                        
                    }                    
                }
                break;

                


                case FibonacciType.Wave3ClassicProjection:
                {
                    for ( i = 0; i < Wave3Classic.ClassicWave3.Length; i++ )
                    {
                        var fibValue = projectionPoint + ( ( endPoint - startPoint ) * Wave3Classic.ClassicWave3[i].Value / 100 );
                        var info = new FibLevelInfo( Wave3Classic.ClassicWave3[i].Percentage, (float) fibValue, Wave3Classic.ClassicWave3[i].FibStrength );

                        _fibLevels.Add( info );
                    }
                }
                break;

                case FibonacciType.Wave3Extended:
                {
                    for ( i = 0; i < Wave3Extended.ExtendedWave3.Length; i++ )
                    {
                        var fibValue = projectionPoint + ( ( endPoint - startPoint ) * Wave3Extended.ExtendedWave3[i].Value / 100 );
                        var info = new FibLevelInfo( Wave3Extended.ExtendedWave3[i].Percentage, ( float )fibValue, Wave3Extended.ExtendedWave3[i].FibStrength );

                        _fibLevels.Add( info );
                    }
                }
                break;

                case FibonacciType.Wave3SuperExtended:
                {
                    for ( i = 0; i < Wave3SuperExtended.SuperExtendedWave3.Length; i++ )
                    {
                        var fibValue = projectionPoint + ( ( endPoint - startPoint ) * Wave3SuperExtended.SuperExtendedWave3[i].Value / 100 );
                        var info = new FibLevelInfo( Wave3SuperExtended.SuperExtendedWave3[i].Percentage, ( float )fibValue, Wave3SuperExtended.SuperExtendedWave3[i].FibStrength );

                        _fibLevels.Add( info );
                    }
                }
                break;

                case FibonacciType.Wave3CProjection:
                {
                    for( i = 0; i < GlobalConstants.Wave3CProjectionLevels.Length; i++ )
                    {
                        var fibValue = projectionPoint + ( ( endPoint - startPoint ) * GlobalConstants.Wave3CProjectionLevels[ i ] / 100 );
                        var info = new FibLevelInfo( GlobalConstants.Wave3CProjectionFibLevelType[ i ], fibValue, GlobalConstants.Wave3CProjectionStrength[ i ] );

                        _fibLevels.Add( info );                        
                    }
                }
                    break;

                case FibonacciType.Wave5Projection:
                {
                    for( i = 0; i < GlobalConstants.Wave5ProjectionLevels.Length; i++ )
                    {
                        var fibValue = projectionPoint + ( ( endPoint - startPoint ) * GlobalConstants.Wave5ProjectionLevels[ i ] / 100 );
                        var info = new FibLevelInfo( GlobalConstants.Wave5ProjectionFibLevelType[ i ], fibValue, GlobalConstants.Wave5ProjectionStrength[ i ] );

                        _fibLevels.Add( info );                        
                    }
                }
                    break;

                case FibonacciType.Wave5CProjection:
                {
                    for( i = 0; i < GlobalConstants.Wave5CProjectionLevels.Length; i++ )
                    {
                        var fibValue = projectionPoint + ( ( endPoint - startPoint ) * GlobalConstants.Wave5CProjectionLevels[ i ] / 100 );
                        var info = new FibLevelInfo( GlobalConstants.Wave5CProjectionFibLevelType[ i ], fibValue, GlobalConstants.Wave5CProjectionStrength[ i ] );

                        _fibLevels.Add( info );                        
                    }
                }
                    break;

                case FibonacciType.ABCWaveCProjection:
                {
                    for( i = 0; i < GlobalConstants.ABCWaveCProjectionLevels.Length; i++ )
                    {
                        var fibValue = projectionPoint + ( ( endPoint - startPoint ) * GlobalConstants.ABCWaveCProjectionLevels[ i ] / 100 );
                        var info = new FibLevelInfo( GlobalConstants.ABCWaveCProjectionFibLevelType[ i ], fibValue, GlobalConstants.ABCWaveCProjectionStrength[ i ] );

                        _fibLevels.Add( info );                        
                    }
                }
                    break;

                case FibonacciType.WaveTriCProjection:
                {
                    for( i = 0; i < GlobalConstants.WaveTriCRetracementLevels.Length; i++ )
                    {
                        var fibValue = endPoint + ( ( startPoint - endPoint ) * GlobalConstants.WaveTriCRetracementLevels[ i ] / 100 );
                        var info = new FibLevelInfo( GlobalConstants.WaveTriCRetracementFibLevelType[ i ], fibValue, GlobalConstants.WaveTriCRetracementStrength[ i ] );

                        _fibLevels.Add( info );                        
                    }
                }
                    break;

                case FibonacciType.WaveTriDProjection:
                {
                    for( i = 0; i < GlobalConstants.WaveTriDRetracementLevels.Length; i++ )
                    {
                        var fibValue = endPoint + ( ( startPoint - endPoint ) * GlobalConstants.WaveTriDRetracementLevels[ i ] / 100 );
                        var info = new FibLevelInfo( GlobalConstants.WaveTriDRetracementFibLevelType[ i ], fibValue, GlobalConstants.WaveTriDRetracementStrength[ i ] );

                        _fibLevels.Add( info );                        
                    }
                }
                    break;

                case FibonacciType.WaveTriEProjection:
                {
                    for( i = 0; i < GlobalConstants.WaveTriERetracementLevels.Length; i++ )
                    {
                        var fibValue = endPoint + ( ( startPoint - endPoint ) * GlobalConstants.WaveTriERetracementLevels[ i ] / 100 );
                        var info = new FibLevelInfo( GlobalConstants.WaveTriERetracementFibLevelType[ i ], fibValue, GlobalConstants.WaveTriERetracementStrength[ i ] );

                        _fibLevels.Add( info );                        
                    }
                }
                    break;
            }
        }

        public void AddMatchedSRinfo( ref FibLevelInfo closestRet, MatchedSRinfo SRLinesRet )
        {
            if ( _majorSL.TryGetValue( closestRet, out PooledList<MatchedSRinfo> value  ) )
            {
                value.Add( SRLinesRet );
            }
            else
            {
                var temp = new PooledList<MatchedSRinfo>();
                temp.Add( SRLinesRet );
                _majorSL.GetOrAddValueRef( closestRet ) = temp;
            }            
        }

        public bool GetClosestFibLevel( double lineValue, out FibLevelInfo closestLine )
        {
            double minDiff = double.MaxValue;
            closestLine = default;

            if ( _fibLevels.Count == 0 )
                return false;

            bool found = false;

            foreach ( var level in _fibLevels )
            {
                var fibLvlY = level.FibLevel;

                if ( fibLvlY == 0 )
                    continue;

                if ( Math.Abs( fibLvlY - lineValue ) < minDiff )
                {
                    closestLine = level;
                    minDiff = Math.Abs( fibLvlY - lineValue );

                    found = true;
                }
            }

            return found;
        }

        public bool GetClosestFibLevel( float lineValue, out FibLevelInfo closestLine )
        {
            float minDiff = float.MaxValue;
            closestLine = default;

            if ( _fibLevels.Count == 0 )
                return false;

            bool found = false;

            foreach ( var level in _fibLevels )
            {
                var fibLvlY = level.FibLevel;

                if ( fibLvlY == 0 )
                    continue;

                if ( Math.Abs( fibLvlY - lineValue ) < minDiff )
                {
                    closestLine = level;
                    minDiff = Math.Abs( fibLvlY - lineValue );

                    found = true;
                }
            }

            return found;
        }
    }
}
