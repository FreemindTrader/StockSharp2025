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
        private PooledList< fxFibLevelCluster > _fibLevels = new PooledList< fxFibLevelCluster >( );
        private DictionarySlim<fxFibLevelCluster, PooledList<MatchedSRinfo>> _majorSL = new DictionarySlim<fxFibLevelCluster, PooledList<MatchedSRinfo>>();
       

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

        public FibLevelsCollection( FibonacciType fibType, double startPoint, double endPoint )
        {
            

            CalcRetracement( fibType, (float) startPoint, (float) endPoint );
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

        public PooledList< fxFibLevelCluster > FibLevels
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
                    for( i = 0; i < WaveFibConstants.Wave2RetracementLevels.Length; i++ )
                    {
                        var fibValue = endPoint + ( ( startPoint - endPoint ) * WaveFibConstants.Wave2RetracementLevels[ i ].FibValue / 100 );
                        var info = new fxFibLevelCluster( WaveFibConstants.Wave2RetracementLevels[ i ], (float) fibValue );

                        _fibLevels.Add( info );                        
                    }
                }
                    break;

                case FibonacciType.TonyRetracement:
                {
                    for ( i = 0; i < WaveFibConstants.Wave4RetracementLevels.Length; i++ )
                    {
                        for ( i = 0; i < WaveFibConstants.Wave4RetracementLevels.Length; i++ )
                        {
                            var fibValue = endPoint + ( ( startPoint - endPoint ) * WaveFibConstants.Wave4RetracementLevels[ i ].FibValue / 100 );
                            var info = new fxFibLevelCluster( WaveFibConstants.Wave4RetracementLevels[ i ], (float) fibValue );

                            _fibLevels.Add( info );
                        }
                    }
                }
                break;

                case FibonacciType.Wave4Retracement:
                {
                    for( i = 0; i < WaveFibConstants.Wave4RetracementLevels.Length; i++ )
                    {
                        var fibValue = endPoint + ( ( startPoint - endPoint ) * WaveFibConstants.Wave4RetracementLevels[ i ].FibValue / 100 );
                        var info = new fxFibLevelCluster( WaveFibConstants.Wave4RetracementLevels[ i ], (float) fibValue );

                        _fibLevels.Add( info );                        
                    }
                }
                    break;

                case FibonacciType.ABCWaveBRetracement:
                {
                    for( i = 0; i < WaveFibConstants.ABCWaveBRetracementLevels.Length; i++ )
                    {
                        var fibValue = endPoint + ( ( startPoint - endPoint ) * WaveFibConstants.ABCWaveBRetracementLevels[ i ].FibValue / 100 );
                        var info = new fxFibLevelCluster( WaveFibConstants.ABCWaveBRetracementLevels[ i ], (float) fibValue );

                        _fibLevels.Add( info );                        
                    }
                }
                    break;

                case FibonacciType.Wave3BRetracement:
                {
                    for ( i = 0; i < WaveFibConstants.Wave3BRetracementLevels.Length; i++ )
                    {
                        var fibValue = endPoint + ( ( startPoint - endPoint ) * WaveFibConstants.Wave3BRetracementLevels[i].FibValue / 100 );
                        var info = new fxFibLevelCluster( WaveFibConstants.Wave3BRetracementLevels[i], ( float )fibValue );

                        _fibLevels.Add( info );
                    }
                }
                break;

                case FibonacciType.Wave5BRetracement:
                {
                    for ( i = 0; i < WaveFibConstants.Wave5BRetracementLevels.Length; i++ )
                    {
                        var fibValue = endPoint + ( ( startPoint - endPoint ) * WaveFibConstants.Wave5BRetracementLevels[i].FibValue / 100 );
                        var info = new fxFibLevelCluster( WaveFibConstants.Wave5BRetracementLevels[i], ( float )fibValue );

                        _fibLevels.Add( info );
                    }
                }
                break;

                case FibonacciType.WaveEFBRetracement:
                {
                    for( i = 0; i < WaveFibConstants.WaveEFBRetracementLevels.Length; i++ )
                    {
                        var fibValue = endPoint + ( ( startPoint - endPoint ) * WaveFibConstants.WaveEFBRetracementLevels[ i ].FibValue / 100 );
                        var info = new fxFibLevelCluster( WaveFibConstants.WaveEFBRetracementLevels[ i ], (float) fibValue );

                        _fibLevels.Add( info );                        
                    }
                }
                    break;

                case FibonacciType.WaveTriBRetracement:
                {
                    for( i = 0; i < WaveFibConstants.WaveTriBRetracementLevels.Length; i++ )
                    {
                        var fibValue = endPoint + ( ( startPoint - endPoint ) * WaveFibConstants.WaveTriBRetracementLevels[ i ].FibValue / 100 );
                        var info = new fxFibLevelCluster( WaveFibConstants.WaveTriBRetracementLevels[ i ], (float) fibValue );

                        _fibLevels.Add( info );                        
                    }
                }
                break;

                case FibonacciType.WaveXRetracement:
                {
                    for ( i = 0; i < WaveFibConstants.WaveXRetracementLevels.Length; i++ )
                    {
                        var fibValue = endPoint + ( ( startPoint - endPoint ) * WaveFibConstants.WaveXRetracementLevels[i].FibValue / 100 );
                        var info = new fxFibLevelCluster( WaveFibConstants.WaveXRetracementLevels[i], ( float )fibValue );

                        _fibLevels.Add( info );
                    }
                }
                break;

                case FibonacciType.TonyProjection:
                {
                    for ( i = 0; i < WaveFibConstants.TonyDiscoveryLevels.Length; i++ )
                    {
                        var fibValue = endPoint + ( ( startPoint - endPoint ) * WaveFibConstants.TonyDiscoveryLevels [ i ].FibValue / 100 );
                        var info = new fxFibLevelCluster( WaveFibConstants.TonyDiscoveryLevels[ i ], (float) fibValue  );

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
                    for( i = 0; i < WaveFibConstants.ABCWaveCProjectionFibLevels.Length; i++ )
                    {
                        var fibValue = projectionPoint + ( ( endPoint - startPoint ) * WaveFibConstants.ABCWaveCProjectionFibLevels[ i ].FibValue / 100 );
                        var info = new fxFibLevelCluster( WaveFibConstants.ABCWaveCProjectionFibLevels[ i ], (float) fibValue  );

                        _fibLevels.Add( info );                        
                    }
                }
                    break;

                case FibonacciType.Wave3All:
                {
                    for( i = 0; i < WaveFibConstants.AllWave3.Length; i++ )
                    {
                        var fibValue = projectionPoint + ( ( endPoint - startPoint ) * WaveFibConstants.AllWave3[ i ].FibValue / 100 );
                        var info = new fxFibLevelCluster( WaveFibConstants.AllWave3[i], (float) fibValue  );

                        _fibLevels.Add( info );                        
                    }
                }
                    break;

                case FibonacciType.Wave3Compressed:
                {
                    for ( i = 0; i < WaveFibConstants.CompressWave3.Length; i++ )
                    {
                        var fibValue = projectionPoint + ( ( endPoint - startPoint ) * WaveFibConstants.CompressWave3[i].FibValue / 100 );
                        var info = new fxFibLevelCluster( WaveFibConstants.CompressWave3[i], ( float )fibValue  );

                        _fibLevels.Add( info );                        
                    }                    
                }
                break;

                


                case FibonacciType.Wave3ClassicProjection:
                {
                    for ( i = 0; i < WaveFibConstants.ClassicWave3.Length; i++ )
                    {
                        var fibValue = projectionPoint + ( ( endPoint - startPoint ) * WaveFibConstants.ClassicWave3[i].FibValue / 100 );
                        var info = new fxFibLevelCluster( WaveFibConstants.ClassicWave3[i], (float) fibValue  );

                        _fibLevels.Add( info );
                    }
                }
                break;

                case FibonacciType.Wave3Extended:
                {
                    for ( i = 0; i < WaveFibConstants.ExtendedWave3.Length; i++ )
                    {
                        var fibValue = projectionPoint + ( ( endPoint - startPoint ) * WaveFibConstants.ExtendedWave3[i].FibValue / 100 );
                        var info = new fxFibLevelCluster( WaveFibConstants.ExtendedWave3[i], ( float )fibValue  );

                        _fibLevels.Add( info );
                    }
                }
                break;

                case FibonacciType.Wave3SuperExtended:
                {
                    for ( i = 0; i < WaveFibConstants.SuperExtendedWave3.Length; i++ )
                    {
                        var fibValue = projectionPoint + ( ( endPoint - startPoint ) * WaveFibConstants.SuperExtendedWave3[i].FibValue / 100 );
                        var info = new fxFibLevelCluster( WaveFibConstants.SuperExtendedWave3[i], ( float )fibValue  );

                        _fibLevels.Add( info );
                    }
                }
                break;

                case FibonacciType.Wave3CProjection:
                {
                    for( i = 0; i < WaveFibConstants.Wave3CProjectionLevels.Length; i++ )
                    {
                        var fibValue = projectionPoint + ( ( endPoint - startPoint ) * WaveFibConstants.Wave3CProjectionLevels[ i ].FibValue / 100 );
                        var info = new fxFibLevelCluster( WaveFibConstants.Wave3CProjectionLevels[ i ], (float) fibValue  );

                        _fibLevels.Add( info );                        
                    }
                }
                    break;

                case FibonacciType.Wave5Projection:
                {
                    for( i = 0; i < WaveFibConstants.Wave5ProjectionFibLevels.Length; i++ )
                    {
                        var fibValue = projectionPoint + ( ( endPoint - startPoint ) * WaveFibConstants.Wave5ProjectionFibLevels[ i ].FibValue / 100 );
                        var info = new fxFibLevelCluster( WaveFibConstants.Wave5ProjectionFibLevels[ i ], (float) fibValue );

                        _fibLevels.Add( info );                        
                    }
                }
                    break;

                case FibonacciType.Wave5CProjection:
                {
                    for( i = 0; i < WaveFibConstants.Wave5CProjectionFibLevels.Length; i++ )
                    {
                        var fibValue = projectionPoint + ( ( endPoint - startPoint ) * WaveFibConstants.Wave5CProjectionFibLevels[ i ].FibValue / 100 );
                        var info = new fxFibLevelCluster( WaveFibConstants.Wave5CProjectionFibLevels[ i ], (float) fibValue  );

                        _fibLevels.Add( info );                        
                    }
                }
                    break;

                case FibonacciType.ABCWaveCProjection:
                {
                    for( i = 0; i < WaveFibConstants.ABCWaveCProjectionFibLevels.Length; i++ )
                    {
                        var fibValue = projectionPoint + ( ( endPoint - startPoint ) * WaveFibConstants.ABCWaveCProjectionFibLevels[ i ].FibValue / 100 );
                        var info = new fxFibLevelCluster( WaveFibConstants.ABCWaveCProjectionFibLevels[ i ], (float) fibValue  );

                        _fibLevels.Add( info );                        
                    }
                }
                    break;

                case FibonacciType.WaveTriCProjection:
                {
                    for( i = 0; i < WaveFibConstants.WaveTriCRetracementLevels.Length; i++ )
                    {
                        var fibValue = endPoint + ( ( startPoint - endPoint ) * WaveFibConstants.WaveTriCRetracementLevels[ i ].FibValue / 100 );
                        var info = new fxFibLevelCluster( WaveFibConstants.WaveTriCRetracementLevels[ i ], (float) fibValue  );

                        _fibLevels.Add( info );                        
                    }
                }
                    break;

                case FibonacciType.WaveTriDProjection:
                {
                    for( i = 0; i < WaveFibConstants.WaveTriDRetracementLevels.Length; i++ )
                    {
                        var fibValue = endPoint + ( ( startPoint - endPoint ) * WaveFibConstants.WaveTriDRetracementLevels[ i ].FibValue / 100 );
                        var info = new fxFibLevelCluster( WaveFibConstants.WaveTriDRetracementLevels[ i ], (float) fibValue  );

                        _fibLevels.Add( info );                        
                    }
                }
                    break;

                case FibonacciType.WaveTriEProjection:
                {
                    for( i = 0; i < WaveFibConstants.WaveTriERetracementLevels.Length; i++ )
                    {
                        var fibValue = endPoint + ( ( startPoint - endPoint ) * WaveFibConstants.WaveTriERetracementLevels[ i ].FibValue / 100 );
                        var info = new fxFibLevelCluster( WaveFibConstants.WaveTriERetracementLevels[ i ], (float) fibValue  );

                        _fibLevels.Add( info );                        
                    }
                }
                    break;
            }
        }

        public void AddMatchedSRinfo( ref fxFibLevelCluster closestRet, MatchedSRinfo SRLinesRet )
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

        public bool GetClosestFibLevel( double lineValue, out fxFibLevelCluster closestLine )
        {
            double minDiff = double.MaxValue;
            closestLine = default;

            if ( _fibLevels.Count == 0 )
                return false;

            bool found = false;

            foreach ( var level in _fibLevels )
            {
                var fibLvlY = level.FibValue;

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

        public bool GetClosestFibLevel( float lineValue, out fxFibLevelCluster closestLine )
        {
            float minDiff = float.MaxValue;
            closestLine = default;

            if ( _fibLevels.Count == 0 )
                return false;

            bool found = false;

            foreach ( var level in _fibLevels )
            {
                var fibLvlY = level.FibValue;

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
