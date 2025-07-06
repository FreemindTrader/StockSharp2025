//using fx.Algorithm;
//using SciChart.Charting.DrawingTools.TradingAnnotations.Models;
//using fx.Definitions;
//using StockSharp.BusinessEntities;
//using System;
//using System.Collections.Generic; using fx.Collections;
//using System.Windows;
//using System.Windows.Media;

//namespace StockSharp.Xaml.Charting
//{
//    /// <summary>
//    /// Fibbonacci retracement object draws line on the chart at specific fibonacci levels.
//    /// </summary>
//    [Serializable]
//    public class HewFibTargets : IEquatable<HewFibTargets>, IDisposable
//    {
//        private FibonacciType              _fibCalculationType     = FibonacciType.NONE;
//        public FibonacciType FibType
//        {
//            get
//            {
//                return _fibCalculationType;
//            }
//        }


//        private System.Windows.Media.Color _penColor               = Colors.Blue;
//        private int                        _controlPointCount      = 0;
//        

//        private SolidColorBrush            _fibNumberColor         = new SolidColorBrush( Colors.Blue );

//        private SolidColorBrush            _fibStrengthBrushWeak   = new SolidColorBrush( Colors.LightGray );
//        private SolidColorBrush            _fibStrengthBrushMedium = new SolidColorBrush( Colors.Blue );
//        private SolidColorBrush            _fibStrengthBrushStrong = new SolidColorBrush( Colors.Red );        

//        private (DateTime, float)          _startPoint             = default;
//        private (DateTime, float)          _endPoint               = default;
//        private (DateTime, float)          _projectionPoint        = default;

//        private (DateTime, float)          _tonyStartPoint         = default;
//        private (DateTime, float)          _tonyEndPoint           = default;

//        private (DateTime, float)          _tonyStartPoint2         = default;
//        private (DateTime, float)          _tonyEndPoint2           = default;


//        private HewManager                 _hews             = null;

//        private float                      _startingX              = 0;

//        private int                        _startingXIndex         = 0;

//        

//        private int                        _endingXIndex           = 0;

//        private float                      _endingX                = 0;

//        private long                       _owingBarLinuxTime          = -1;

//        private float[ ]                   _fibonaccilevels;

//        private DateTime                   _nextWaveIndex          = DateTime.MinValue;

//        private int                        _selectedLinesCount     = 0;

//        private PooledList<(float, int)>         _calculatedFibLevels1    = new PooledList<(float, int)>( );
//        private PooledList<(float, int)>         _calculatedFibLevels2    = new PooledList<(float, int)>( );

//        private PooledList<int>                  _selectedFibLevels      = new PooledList<int>( );


//        public PooledList<(float, int)> CalculatedFibLevels
//        {
//            get { return _calculatedFibLevels1; }
//        }

//        public PooledList<(float, int)> CalculatedFibLevels2
//        {
//            get { return _calculatedFibLevels2; }
//        }


//        public long OwingBarLinuxTime
//        {
//            get { return _owingBarLinuxTime; }

//            set { _owingBarLinuxTime = value; }
//        }

//        public (DateTime Time, float Value) TonyStartPoint2
//        {
//            get
//            {
//                return _tonyStartPoint2;
//            }
//        }

//        public (DateTime Time, float Value) TonyEndPoint2
//        {
//            get
//            {
//                return _tonyEndPoint2;
//            }
//        }

//        public (DateTime Time, float Value) TonyStartPoint
//        {
//            get
//            {
//                return _tonyStartPoint;
//            }
//        }

//        public (DateTime Time, float Value) TonyEndPoint
//        {
//            get
//            {
//                return _tonyEndPoint;
//            }
//        }

//        public (DateTime Time, float Value) StartPoint
//        {
//            get
//            {
//                return _startPoint;
//            }            
//        }

//        public (DateTime Time, float Value) EndPoint
//        {
//            get
//            {
//                return _endPoint;
//            }
//        }

//        public (DateTime Time, float Value) ProjectionPoint
//        {
//            get
//            {
//                return _projectionPoint;
//            }
//        }


//        public bool IsUptrend
//        {
//            get
//            {
//                return _endPoint.Item2 > _startPoint.Item2;
//            }
//        }


//        public Color PenColor
//        {
//            set
//            {
//                _penColor = value;                
//                _fibNumberColor = new SolidColorBrush( value );                
//            }
//        }

//        private Pen _dashedLinePen = null;
//        private Pen _solidLinePen = null;

//        public bool IsBuilding
//        {
//            get { return _controlPointCount < 2; }
//        }

//        public bool isRetracement
//        {
//            get
//            {
//                return _controlPointCount == 2;
//            }
//        }

//        public bool isProjection
//        {
//            get
//            {
//                return _controlPointCount == 3;
//            }
//        }

//        PooledList<SBar> _targetBars = new PooledList<SBar>();
//        public PooledList<SBar> TargetPoints
//        {
//            get
//            {
//                return _targetBars;
//            }

//            set
//            {
//                _targetBars = value;
//            }
//        }

//        public Security Symbol { get; set; }

//        /// <summary>
//        ///
//        /// </summary>
//        public HewFibTargets( long owingBarTime, Security symbol, string name )
//        {
//            Symbol              = symbol;
//            _owingBarLinuxTime  = owingBarTime;
//            _hews         = AdvancedAnalysisManager.GetCurrentElliottWaveManager( );
//            PenColor            = Colors.DarkGray;
//            _selectedLinesCount = 0;            
//        }




//        #region Tony Implementation

//        public RatioModel[ ] GetTonyLevels( )
//        {
//            return TonyDiscoveryLevels;
//        }


//        public RatioModel[ ] GetFibLevels( )
//        {
//            switch ( this.FibType  )
//            {
//                case FibonacciType.Wave2Retracement:
//                {
//                    return Wave2RetracementLevelsRatio;
//                }

//                case FibonacciType.Wave4Retracement:
//                {
//                    return Wave4RetracementLevelsRatio;
//                }

//                case FibonacciType.ABCWaveBRetracement:
//                {
//                    return ABCWaveBRetracementLevelsRatio;
//                }

//                case FibonacciType.Wave3Projection:
//                {
//                    return Wave3ProjectionLevels;
//                }

//                case FibonacciType.Wave3CProjection:
//                {
//                    return Wave3CProjectionLevels;
//                }

//                case FibonacciType.Wave5Projection:
//                {
//                    return Wave5ProjectionLevels;
//                }

//                case FibonacciType.ABCWaveCProjection:
//                {
//                    return ABCWaveCProjectionLevels;
//                }

//                case FibonacciType.Wave5CProjection:
//                {
//                    return Wave5CProjectionLevels;
//                }

//                case FibonacciType.WaveEFBRetracement:
//                {
//                    return WaveEFBRetracementLevels;
//                }

//                case FibonacciType.WaveTriBRetracement:
//                {
//                    return WaveTriBRetracementLevels;
//                }

//                case FibonacciType.WaveTriCProjection:
//                {
//                    return WaveTriCRetracementLevels;
//                }

//                case FibonacciType.WaveTriDProjection:
//                {
//                    return WaveTriDRetracementLevels;
//                }

//                case FibonacciType.WaveTriEProjection:
//                {
//                    return WaveTriERetracementLevels;
//                }

//                case FibonacciType.FirstXProjection:
//                {
//                    return FirstXProjectionLevels;
//                }

//                case FibonacciType.SecondXProjection:
//                {
//                    return SecondXProjectionLevels;
//                }


//                case FibonacciType.WaveCProjection:
//                {
//                    return ABCWaveCProjectionLevels;
//                }


//            }

//            return null;
//        }


//        public SolidColorBrush GetFibStrengthBrushColor( int strength )
//        {
//            if ( strength < 10 )
//            {
//                return _fibStrengthBrushWeak;
//            }
//            else if ( strength >= 10 && strength < 50 )
//            {
//                return _fibStrengthBrushMedium;
//            }
//            else if ( strength >= 50 && strength < 100 )
//            {
//                return _fibStrengthBrushStrong;
//            }

//            return _fibStrengthBrushWeak;
//        }


//        public void SetTonyExtension(  ( (DateTime, float) startPoint, (DateTime, float) endPoint ) pt , ElliottWaveEnum wave )
//        {
//            _tonyStartPoint       = pt.startPoint;
//            _tonyEndPoint         = pt.endPoint;
//            _controlPointCount    = 2;

//            _fibonaccilevels      = GlobalConstants.TonyDiscoveryLevels;
//            _fibCalculationType   = FibonacciType.TonyProjection;

//            _calculatedFibLevels1 = harmonicEWave.CalculateAcutalFibonacciLevelsAndStrength( _fibCalculationType, _tonyStartPoint.Item2, _tonyEndPoint.Item2, _projectionPoint != default ? _projectionPoint.Item2 : 0 );
//        }

//        public void SetTonyExtension2( ((DateTime, float) startPoint, (DateTime, float) endPoint) pt, ElliottWaveEnum wave )
//        {
//            _tonyStartPoint2      = pt.startPoint;
//            _tonyEndPoint2        = pt.endPoint;
//            _controlPointCount    = 2;

//            _fibonaccilevels      = GlobalConstants.TonyDiscoveryLevels;
//            _fibCalculationType   = FibonacciType.TonyProjection;

//            _calculatedFibLevels2 = harmonicEWave.CalculateAcutalFibonacciLevelsAndStrength( _fibCalculationType, _tonyStartPoint2.Item2, _tonyEndPoint2.Item2, _projectionPoint != default ? _projectionPoint.Item2 : 0 );
//        }

//        public bool HasTonyExtensions
//        {
//            get
//            {
//                return _tonyEndPoint != default && _tonyStartPoint != default;
//            }
//        }

//        public bool HasTonyExtensions2
//        {
//            get
//            {
//                return _tonyEndPoint2 != default && _tonyStartPoint2 != default;
//            }
//        }


//        public void SetFibonacciProjections( ( DateTime, float) startPoint,
//                                             ( DateTime, float) endPoint,
//                                             ( DateTime, float) projectionPoint,
//                                             ElliottWaveEnum wave )
//        {
//            _startPoint        = startPoint;
//            _endPoint          = endPoint;
//            _projectionPoint   = projectionPoint;

//            _controlPointCount = 3;

//            var projection     = _hews.GetFibonacciProjection( wave );

//            if ( projection != null )
//            {
//                _fibonaccilevels    = projection.FibLevels;
//                _fibCalculationType = projection.FibType;
//            }

//            _calculatedFibLevels1 = harmonicEWave.CalculateAcutalFibonacciLevelsAndStrength( _fibCalculationType, _startPoint.Item2, _endPoint.Item2, _projectionPoint != default ? _projectionPoint.Item2 : 0 );
//        }

//        public void SetWaveCProjections( ( DateTime, float) startPoint,
//                                         ( DateTime, float) waveA,
//                                         ( DateTime, float) waveB,
//                                         ElliottWaveEnum wave )
//        {
//            _startPoint         = startPoint;
//            _endPoint           = waveA;
//            _projectionPoint    = waveB;

//            _controlPointCount  = 3;

//            var projection      = _hews.GetWaveCProjection( wave );
//            _fibonaccilevels    = projection.FibLevels;
//            _fibCalculationType = projection.FibType;

//            _calculatedFibLevels1 = harmonicEWave.CalculateAcutalFibonacciLevelsAndStrength( _fibCalculationType, _startPoint.Item2, _endPoint.Item2, _projectionPoint != default ? _projectionPoint.Item2 : 0 );

//        }

//        public void SetFirstXProjections( ( DateTime, float) startPoint,
//                                         ( DateTime, float) waveA,
//                                         ( DateTime, float) waveB
//                                        )
//        {
//            _startPoint         = startPoint;
//            _endPoint           = waveA;
//            _projectionPoint    = waveB;

//            _controlPointCount  = 3;

//            var projection      = _hews.GetFirstXProjection( );
//            _fibonaccilevels    = projection.FibLevels;
//            _fibCalculationType = projection.FibType;

//            _calculatedFibLevels1 = harmonicEWave.CalculateAcutalFibonacciLevelsAndStrength( _fibCalculationType, _startPoint.Item2, _endPoint.Item2, _projectionPoint != default ? _projectionPoint.Item2 : 0 );

//        }

//        public void SetSecondXProjections( (DateTime, float) startPoint,
//                                           (DateTime, float) waveA,
//                                         (DateTime, float) waveB
//                                        )
//        {
//            _startPoint           = startPoint;
//            _endPoint             = waveA;
//            _projectionPoint      = waveB;

//            _controlPointCount    = 3;

//            var projection        = _hews.GetSecondXProjection( );
//            _fibonaccilevels      = projection.FibLevels;
//            _fibCalculationType   = projection.FibType;

//            _calculatedFibLevels1 = harmonicEWave.CalculateAcutalFibonacciLevelsAndStrength( _fibCalculationType, _startPoint.Item2, _endPoint.Item2, _projectionPoint != default ? _projectionPoint.Item2 : 0 );

//        }

//        public void SetFibonacciRetracment( ( DateTime, float) startPoint,
//                                            (DateTime, float) endPoint,
//                                            ElliottWaveEnum wave )
//        {
//            _startPoint        = startPoint;
//            _endPoint          = endPoint;

//            _controlPointCount = 2;

//            var retracement    = _hews.GetFibonacciRetracment( wave );

//            if ( retracement != null )
//            {
//                _fibonaccilevels = retracement.FibLevels;
//                _fibCalculationType = retracement.FibType;
//            }

//            _calculatedFibLevels1 = harmonicEWave.CalculateAcutalFibonacciLevelsAndStrength( _fibCalculationType, _startPoint.Item2, _endPoint.Item2, _projectionPoint != default ? _projectionPoint.Item2 : 0 );
//        }


//        
//        public void SetEndingIndexTime( DateTime barIndex )
//        {
//            _nextWaveIndex = barIndex;
//        }

//        public DateTime GetEndingIndexTime()
//        {
//            return _nextWaveIndex;
//        }


//        public override bool Equals( object obj )
//        {
//            if ( obj is HewFibTargets )
//            {
//                return Equals( ( HewFibTargets )obj );
//            }

//            return base.Equals( obj );
//        }

//        public static bool operator ==( HewFibTargets first, HewFibTargets second )
//        {
//            if ( ( object )first == null )
//            {
//                return ( object )second == null;
//            }

//            return first.Equals( second );
//        }

//        public static bool operator !=( HewFibTargets first, HewFibTargets second )
//        {
//            return !( first == second );
//        }

//        public bool Equals( HewFibTargets other )
//        {
//            if ( ReferenceEquals( null, other ) )
//            {
//                return false;
//            }

//            if ( ReferenceEquals( this, other ) )
//            {
//                return true;
//            }

//            return _controlPointCount.Equals( other._controlPointCount ) && _fibCalculationType.Equals( other._fibCalculationType ) && _startPoint.Equals( other._startPoint ) && _endPoint.Equals( other._endPoint ) && _projectionPoint.Equals( other._projectionPoint ) && _startingX.Equals( other._startingX ) && _startingXIndex.Equals( other._startingXIndex ) && _endingXIndex.Equals( other._endingXIndex ) && _endingX.Equals( other._endingX ) && _owingBarLinuxTime.Equals( other._owingBarLinuxTime ) && Equals( _fibonaccilevels, other._fibonaccilevels ) && _nextWaveIndex.Equals( other._nextWaveIndex ) && _selectedLinesCount.Equals( other._selectedLinesCount ) && Equals( _calculatedFibLevels1, other._calculatedFibLevels1 ) && Equals( _selectedFibLevels, other._selectedFibLevels );
//        }

//        public override int GetHashCode( )
//        {
//            unchecked
//            {
//                int hashCode = 47;
//                hashCode = ( hashCode * 53 ) ^ _penColor.GetHashCode( );
//                hashCode = ( hashCode * 53 ) ^ _controlPointCount.GetHashCode( );
//                hashCode = ( hashCode * 53 ) ^ ( int )_fibCalculationType;
//                if ( _fibNumberColor != null )
//                {
//                    hashCode = ( hashCode * 53 ) ^ _fibNumberColor.GetHashCode( );
//                }

//                hashCode = ( hashCode * 53 ) ^ _startPoint.GetHashCode( );
//                hashCode = ( hashCode * 53 ) ^ _endPoint.GetHashCode( );
//                hashCode = ( hashCode * 53 ) ^ _projectionPoint.GetHashCode( );
//                if ( _hews != null )
//                {
//                    hashCode = ( hashCode * 53 ) ^ _hews.GetHashCode( );
//                }

//                hashCode = ( hashCode * 53 ) ^ _startingX.GetHashCode( );
//                hashCode = ( hashCode * 53 ) ^ _startingXIndex.GetHashCode( );
//                hashCode = ( hashCode * 53 ) ^ _endingXIndex.GetHashCode( );
//                hashCode = ( hashCode * 53 ) ^ _endingX.GetHashCode( );
//                hashCode = ( hashCode * 53 ) ^ _owingBarLinuxTime.GetHashCode( );
//                if ( _fibonaccilevels != null )
//                {
//                    hashCode = ( hashCode * 53 ) ^ _fibonaccilevels.GetHashCode( );
//                }

//                hashCode = ( hashCode * 53 ) ^ _nextWaveIndex.GetHashCode( );
//                hashCode = ( hashCode * 53 ) ^ _selectedLinesCount.GetHashCode( );
//                if ( _calculatedFibLevels1 != null )
//                {
//                    hashCode = ( hashCode * 53 ) ^ _calculatedFibLevels1.GetHashCode( );
//                }

//                if ( _selectedFibLevels != null )
//                {
//                    hashCode = ( hashCode * 53 ) ^ _selectedFibLevels.GetHashCode( );
//                }

//                if ( _dashedLinePen != null )
//                {
//                    hashCode = ( hashCode * 53 ) ^ _dashedLinePen.GetHashCode( );
//                }

//                if ( _solidLinePen != null )
//                {
//                    hashCode = ( hashCode * 53 ) ^ _solidLinePen.GetHashCode( );
//                }

//                return hashCode;
//            }
//        }
//        public void Dispose( )
//        {
//            //_fibNumberColor.Dispose( );
//            //_dashedLinePen.Dispose( );
//            //_solidLinePen.Dispose( );
//            //_fibStrengthBrushWeak.Dispose( );
//            //_fibStrengthBrushMedium.Dispose( );
//            //_fibStrengthBrushStrong.Dispose( );

//            //_fibStrengthPenWeak.Dispose( );
//            //_fibStrengthPenMedium.Dispose( );
//            //_fibStrengthPenStrong.Dispose( );
//        }

//        public static readonly SolidColorBrush BaseColor    = new SolidColorBrush( Color.FromArgb( byte.MaxValue, 119, 119, 135 ) );
//        public static readonly SolidColorBrush Impt0Color   = new SolidColorBrush( Colors.Brown );
//        public static readonly SolidColorBrush Impt5Color   = new SolidColorBrush( Colors.SteelBlue );
//        public static readonly SolidColorBrush Impt10Color  = new SolidColorBrush( Colors.Blue );
//        public static readonly SolidColorBrush Impt20Color  = new SolidColorBrush( Colors.Red );
//        public static readonly SolidColorBrush Impt50Color  = new SolidColorBrush( Colors.Red );
//        public static readonly SolidColorBrush Impt100Color = new SolidColorBrush( Colors.Red );

//        public static readonly RatioModel[ ] WaveEFBRetracementLevels = new RatioModel[ ]
//        {
//            new RatioModel( 0.0902, Impt0Color ),
//            new RatioModel( 0.146 , Impt0Color),
//            new RatioModel( 0.236 , Impt10Color),
//            new RatioModel( 0.333 , Impt10Color),
//            new RatioModel( 0.382 , Impt10Color ),
//            new RatioModel( 0.414 , Impt10Color ),
//            new RatioModel( 0.5   , Impt10Color ),
//            new RatioModel( 0.586 , Impt10Color ),
//            new RatioModel( 0.618 , Impt20Color ),
//            new RatioModel( 0.6666, Impt20Color ),
//            new RatioModel( 0.764 , Impt20Color ),
//            new RatioModel( 0.854 , Impt20Color ),
//            new RatioModel( 0.9002, Impt10Color ),
//            new RatioModel( 0.9440, Impt10Color ),
//            new RatioModel( 0.9540, Impt10Color ),
//            new RatioModel( 0.9860, Impt10Color ),
//            new RatioModel( 1,      Impt10Color ),
//            new RatioModel( 1.092,  Impt10Color ),
//            new RatioModel( 1.1146, Impt10Color ),
//            new RatioModel( 1.236,  Impt10Color ),
//            new RatioModel( 1.333,  Impt10Color ),
//            new RatioModel( 1.382,  Impt10Color ),
//        };

//        public static readonly RatioModel[ ] WaveTriBRetracementLevels = new RatioModel[ ]
//        {
//            new RatioModel( 0.618 , Impt0Color ),
//            new RatioModel( 0.6666, Impt10Color ),
//            new RatioModel( 0.764 , Impt10Color ),
//            new RatioModel( 0.854 , Impt10Color ),
//            new RatioModel( 0.9002, Impt0Color ),
//            new RatioModel( 0.9440, Impt0Color ),
//            new RatioModel( 1,      Impt0Color ),
//            new RatioModel( 1.092,  Impt0Color ),
//            new RatioModel( 1.1146, Impt0Color ),
//            new RatioModel( 1.236,  Impt0Color ),
//            new RatioModel( 1.333,  Impt0Color ),
//            new RatioModel( 1.382,  Impt0Color ),
//        };

//        public static readonly RatioModel[ ] TonyDiscoveryLevels = new RatioModel[ ]
//        {
//            new RatioModel( 1.092 , Impt20Color ),
//            new RatioModel( 1.272,  Impt100Color ),
//            new RatioModel( 1.500 , Impt20Color ),
//            new RatioModel( 1.618 , Impt100Color ),
//            new RatioModel( 2.000,  Impt100Color ),
//            new RatioModel( 2.272,  Impt100Color ),
//            new RatioModel( 2.5,    Impt20Color ),
//            new RatioModel( 2.618,  Impt100Color ),
//            new RatioModel( 3.000,  Impt0Color ),
//            new RatioModel( 3.272,  Impt0Color ),
//            new RatioModel( 3.5,    Impt0Color ),
//            new RatioModel( 3.618,  Impt0Color ),
//            new RatioModel( 4.000,  Impt0Color ),
//            new RatioModel( 4.272,  Impt0Color ),
//            new RatioModel( 4.5,    Impt0Color ),
//            new RatioModel( 4.618,  Impt0Color ),
//            new RatioModel( 5.000,  Impt0Color ),
//            new RatioModel( 5.272,  Impt0Color ),
//            new RatioModel( 5.5,    Impt0Color ),
//            new RatioModel( 5.618,  Impt0Color ),
//            new RatioModel( 6.000,  Impt0Color ),
//            new RatioModel( 6.272,  Impt0Color ),
//            new RatioModel( 6.5,    Impt0Color ),
//            new RatioModel( 6.618,  Impt0Color ),
//            new RatioModel( 7.000,  Impt0Color ),
//            new RatioModel( 7.272,  Impt0Color ),
//            new RatioModel( 7.5,    Impt0Color ),
//            new RatioModel( 7.618,  Impt0Color ),
//            new RatioModel( 8.000,  Impt0Color ),
//            new RatioModel( 8.272,  Impt0Color ),
//            new RatioModel( 8.5,    Impt0Color ),
//            new RatioModel( 8.618,  Impt0Color ),
//            new RatioModel( 9.000,  Impt0Color ),
//            new RatioModel( 9.272,  Impt0Color ),
//            new RatioModel( 9.5,    Impt0Color ),
//            new RatioModel( 9.618,  Impt0Color ),
//        };


//        //public static readonly int [ ] TonyDiscoveryLevelsStrength = new int [ ]
//        //{
//        //                                                        50,
//        //                                                        100,
//        //                                                        50,
//        //                                                        100,
//        //                                                        100,
//        //                                                        100,
//        //                                                        50,
//        //                                                        100,
//        //                                                        10,
//        //                                                        10,
//        //                                                        10,
//        //                                                        10,
//        //};
//        public static readonly RatioModel[ ] WaveTriCRetracementLevels = new RatioModel[ ]
//        {
//            new RatioModel( 0.618 , Impt0Color ),
//            new RatioModel( 0.6666, Impt10Color ),
//            new RatioModel( 0.764 , Impt10Color ),
//            new RatioModel( 0.854 , Impt10Color ),
//            new RatioModel( 0.9002, Impt0Color ),
//            new RatioModel( 0.9440, Impt0Color ),
//            new RatioModel( 1,      Impt0Color ),
//        };

//        public static readonly RatioModel[ ] WaveTriDRetracementLevels = new RatioModel[ ]
//        {
//            new RatioModel( 0.618 , Impt0Color ),
//            new RatioModel( 0.6666, Impt10Color ),
//            new RatioModel( 0.764 , Impt10Color ),
//            new RatioModel( 0.854 , Impt10Color ),
//            new RatioModel( 0.9002, Impt0Color ),
//            new RatioModel( 0.9440, Impt0Color ),
//        };

//        public static readonly RatioModel[ ] WaveTriERetracementLevels = new RatioModel[ ]
//        {
//            new RatioModel( 0.382 , Impt0Color ),
//            new RatioModel( 0.414 , Impt0Color ),
//            new RatioModel( 0.5   , Impt10Color ),
//            new RatioModel( 0.586 , Impt10Color ),
//            new RatioModel( 0.618 , Impt10Color ),
//            new RatioModel( 0.6666, Impt10Color ),
//            new RatioModel( 0.764 , Impt0Color ),
//            new RatioModel( 0.854 , Impt0Color ),

//        };


//        public static readonly RatioModel[ ] Wave2RetracementLevelsRatio = new RatioModel[ 14 ]
//        {
//            new RatioModel( 0.0902, Impt0Color ),
//            new RatioModel( 0.146 , Impt0Color),
//            new RatioModel( 0.236 , Impt10Color),
//            new RatioModel( 0.333 , Impt10Color),
//            new RatioModel( 0.382 , Impt10Color ),
//            new RatioModel( 0.414 , Impt10Color ),
//            new RatioModel( 0.5   , Impt10Color ),
//            new RatioModel( 0.586 , Impt10Color ),
//            new RatioModel( 0.618 , Impt20Color ),
//            new RatioModel( 0.6666, Impt20Color ),
//            new RatioModel( 0.764 , Impt20Color ),
//            new RatioModel( 0.854 , Impt20Color ),
//            new RatioModel( 0.9002, Impt10Color ),
//            new RatioModel( 0.986 , Impt10Color ),

//        };

//        public static readonly RatioModel[ ] Wave4RetracementLevelsRatio = new RatioModel[ ]
//       {
//            new RatioModel( 0.0902, Impt0Color ),
//            new RatioModel( 0.146 , Impt0Color),
//            new RatioModel( 0.236 , Impt10Color),
//            new RatioModel( 0.333 , Impt0Color),
//            new RatioModel( 0.382 , Impt20Color ),
//            new RatioModel( 0.414 , Impt20Color ),
//            new RatioModel( 0.5   , Impt20Color ),
//            new RatioModel( 0.586 , Impt20Color ),
//            new RatioModel( 0.618 , Impt10Color ),
//            new RatioModel( 0.6666, Impt10Color ),
//       };

//        public static readonly RatioModel[ ] Wave3ProjectionLevels = new RatioModel[ ]
//        {
//            new RatioModel( 1.0902, Impt50Color ),
//            new RatioModel( 1.146,  Impt50Color ),
//            new RatioModel( 1.2360, Impt50Color ),
//            new RatioModel( 1.3820, Impt0Color ),
//            new RatioModel( 1.6180, Impt0Color ),
//            new RatioModel( 1.6670, Impt100Color ),
//            new RatioModel( 1.7640, Impt20Color ),
//            new RatioModel( 1.8540, Impt10Color ),
//            new RatioModel( 1.9020, Impt100Color ),
//            new RatioModel( 1.9840, Impt100Color ),
//            new RatioModel( 2.1460, Impt20Color ),
//            new RatioModel( 2.2360, Impt20Color ),
//            new RatioModel( 2.3820, Impt0Color ),
//            new RatioModel( 2.4270, Impt0Color ),
//            new RatioModel( 2.6180, Impt0Color ),
//            new RatioModel( 2.7640, Impt10Color ),
//            new RatioModel( 2.8540, Impt10Color ),
//            new RatioModel( 2.9000, Impt10Color ),
//            new RatioModel( 2.9440, Impt10Color ),
//            new RatioModel( 2.9840, Impt10Color ),
//            new RatioModel( 3.0920, Impt10Color ),
//            new RatioModel( 3.1440, Impt10Color ),
//            new RatioModel( 3.2360, Impt10Color ),
//            new RatioModel( 3.6180, Impt10Color ),
//            new RatioModel( 3.7640, Impt10Color ),
//            new RatioModel( 3.9840, Impt0Color ),
//            new RatioModel( 4.0920, Impt0Color ),
//            new RatioModel( 4.1440, Impt0Color ),
//            new RatioModel( 4.2360, Impt0Color ),
//            new RatioModel( 4.6180, Impt0Color ),
//            new RatioModel( 4.7640, Impt0Color ),
//            new RatioModel( 4.9840, Impt0Color ),
//            new RatioModel( 5.0920, Impt0Color ),
//            new RatioModel( 5.1440, Impt0Color ),
//            new RatioModel( 5.2360, Impt0Color ),
//            new RatioModel( 5.6180, Impt0Color ),
//            new RatioModel( 5.7640, Impt0Color ),
//            new RatioModel( 5.9840, Impt0Color ),
//            new RatioModel( 6.0920, Impt0Color ),
//            new RatioModel( 6.1440, Impt0Color ),
//            new RatioModel( 6.2360, Impt0Color ),
//            new RatioModel( 6.6180, Impt0Color ),
//            new RatioModel( 6.7640, Impt0Color ),
//            new RatioModel( 6.9840, Impt0Color ),
//            new RatioModel( 7.0920, Impt0Color ),
//            new RatioModel( 7.1440, Impt0Color ),
//            new RatioModel( 7.2360, Impt0Color ),
//            new RatioModel( 7.6180, Impt0Color ),
//            new RatioModel( 7.7640, Impt0Color ),
//            new RatioModel( 7.9840, Impt0Color ),
//            new RatioModel( 8.0920, Impt0Color ),
//            new RatioModel( 8.1440, Impt0Color ),
//            new RatioModel( 8.2360, Impt0Color ),
//            new RatioModel( 8.6180, Impt0Color ),
//            new RatioModel( 8.7640, Impt0Color ),
//            new RatioModel( 8.9840, Impt0Color ),
//            new RatioModel( 9.0920, Impt0Color ),
//            new RatioModel( 9.1440, Impt0Color ),
//            new RatioModel( 9.2360, Impt0Color ),
//            new RatioModel( 9.6180, Impt0Color ),
//            new RatioModel( 9.7640, Impt0Color ),
//            new RatioModel( 9.9840, Impt0Color ),
//            
//        };

//        public static readonly RatioModel[ ] FirstXProjectionLevels = new RatioModel[ ]
//        {
//            new RatioModel( 0.764 , Impt0Color ),
//            new RatioModel( 0.9002, Impt0Color ),
//            new RatioModel( 0.9440, Impt0Color ),
//            new RatioModel( 1 ,     Impt10Color ),
//            new RatioModel( 1.056 , Impt10Color ),
//            new RatioModel( 1.0902, Impt10Color ),
//            new RatioModel( 1.146,  Impt10Color ),
//            new RatioModel( 1.2360, Impt10Color ),
//            new RatioModel( 1.3820, Impt10Color ),
//            new RatioModel( 1.5000, Impt10Color ),
//            new RatioModel( 1.6180, Impt10Color ),
//            new RatioModel( 1.6670, Impt10Color ),
//            new RatioModel( 1.7640, Impt0Color ),
//            new RatioModel( 1.8540, Impt0Color ),
//            new RatioModel( 1.9440, Impt0Color ),
//            new RatioModel( 2.2360, Impt10Color ),
//            new RatioModel( 2.3820, Impt10Color ),
//            new RatioModel( 2.6180, Impt10Color ),
//            new RatioModel( 2.7640, Impt0Color ),
//            new RatioModel( 2.8540, Impt0Color ),
//            new RatioModel( 3.618, Impt0Color ),
//        };

//        public static readonly RatioModel[ ] SecondXProjectionLevels = new RatioModel[ ]
//        {
//            new RatioModel( 0.764 , Impt0Color ),
//            new RatioModel( 0.9002, Impt10Color ),
//            new RatioModel( 0.9440, Impt10Color ),
//            new RatioModel( 1 ,     Impt10Color ),
//            new RatioModel( 1.056 , Impt10Color ),
//            new RatioModel( 1.0902, Impt10Color ),
//            new RatioModel( 1.146,  Impt10Color ),
//            new RatioModel( 1.2360, Impt10Color ),
//            new RatioModel( 1.3820, Impt10Color ),
//            new RatioModel( 1.5000, Impt0Color ),
//            new RatioModel( 1.6180, Impt0Color ),

//        };


//        public static readonly RatioModel[ ] Wave3CProjectionLevels = new RatioModel[ ]
//        {
//            new RatioModel( 0.764 , Impt0Color ),
//            new RatioModel( 0.854 , Impt10Color ),
//            new RatioModel( 0.9002, Impt10Color ),
//            new RatioModel( 0.9440, Impt10Color ),
//            new RatioModel( 0.986 , Impt10Color ),
//            new RatioModel( 1 , Impt10Color ),
//            new RatioModel( 1.056 , Impt10Color ),
//            new RatioModel( 1.0902, Impt10Color ),
//            new RatioModel( 1.146,  Impt10Color ),
//            new RatioModel( 1.2360, Impt10Color ),
//            new RatioModel( 1.3820, Impt10Color ),
//            new RatioModel( 1.6180, Impt10Color ),
//            new RatioModel( 1.6670, Impt0Color ),
//            new RatioModel( 1.7640, Impt0Color ),
//            new RatioModel( 1.8540, Impt0Color ),
//            new RatioModel( 1.9440, Impt0Color ),
//            new RatioModel( 2.2360, Impt0Color ),
//            new RatioModel( 2.4270, Impt0Color ),
//            new RatioModel( 2.6180, Impt0Color ),
//            new RatioModel( 2.7640, Impt0Color ),
//        };

//        public static readonly RatioModel[ ] ABCWaveBRetracementLevelsRatio = new RatioModel[ ]
//        {
//            new RatioModel( 0.0902, Impt0Color ),
//            new RatioModel( 0.146 , Impt0Color),
//            new RatioModel( 0.236 , Impt5Color),
//            new RatioModel( 0.333 , Impt0Color),
//            new RatioModel( 0.382 , Impt5Color ),
//            new RatioModel( 0.414 , Impt10Color ),
//            new RatioModel( 0.5   , Impt10Color ),
//            new RatioModel( 0.586 , Impt10Color ),
//            new RatioModel( 0.618 , Impt20Color ),
//            new RatioModel( 0.6666, Impt20Color ),
//            new RatioModel( 0.764 , Impt20Color ),
//            new RatioModel( 0.854 , Impt20Color ),
//            new RatioModel( 0.9002, Impt5Color ),
//            new RatioModel( 0.944, Impt0Color ),
//            new RatioModel( 0.986 , Impt5Color ),
//        };

//        public static readonly RatioModel[ ] Wave5ProjectionLevels = new RatioModel[ ]
//        {
//            new RatioModel( 0.414 , Impt5Color ),
//            new RatioModel( 0.5   , Impt5Color ),
//            new RatioModel( 0.618 , Impt10Color ),
//            new RatioModel( 0.6666, Impt10Color ),
//            new RatioModel( 0.764 , Impt20Color ),
//            new RatioModel( 0.854 , Impt20Color ),
//            new RatioModel( 0.9002, Impt0Color ),
//            new RatioModel( 0.944,  Impt0Color ),
//            new RatioModel( 1,      Impt0Color ),
//            new RatioModel( 1.056,  Impt0Color ),
//        };

//        public static readonly RatioModel[ ] ABCWaveCProjectionLevels = new RatioModel[ ]
//        {
//            new RatioModel( 0.414 , Impt0Color ),
//            new RatioModel( 0.5   , Impt0Color ),
//            new RatioModel( 0.618 , Impt0Color ),
//            new RatioModel( 0.6666, Impt0Color ),
//            new RatioModel( 0.764 , Impt5Color ),
//            new RatioModel( 0.854 , Impt5Color ),
//            new RatioModel( 0.9002, Impt10Color ),
//            new RatioModel( 0.944,  Impt10Color ),
//            new RatioModel( 1,      Impt10Color ),
//            new RatioModel( 1.056,  Impt10Color ),
//            new RatioModel( 1.0902, Impt20Color ),
//            new RatioModel( 1.146,  Impt20Color ),
//            new RatioModel( 1.2360, Impt20Color ),
//            new RatioModel( 1.3820, Impt5Color ),
//            new RatioModel( 1.5,    Impt10Color ),
//            new RatioModel( 1.6180, Impt10Color ),
//            new RatioModel( 1.6670, Impt0Color ),
//            new RatioModel( 1.7640, Impt0Color ),
//            new RatioModel( 1.8540, Impt0Color ),
//            new RatioModel( 1.9440, Impt0Color ),
//            new RatioModel( 2.2360, Impt0Color ),
//            new RatioModel( 2.6180, Impt0Color ),
//            new RatioModel( 2.7640, Impt0Color ),
//            new RatioModel( 2.8540, Impt0Color ),
//            new RatioModel( 3.6180, Impt0Color ),
//        };

//        public static readonly RatioModel[ ] Wave5CProjectionLevels = new RatioModel[ ]
//        {
//            new RatioModel( 0.5   , Impt0Color ),
//            new RatioModel( 0.618 , Impt0Color ),
//            new RatioModel( 0.6666, Impt0Color ),
//            new RatioModel( 0.764 , Impt0Color ),
//            new RatioModel( 0.854 , Impt10Color ),
//            new RatioModel( 0.9002, Impt10Color ),
//            new RatioModel( 0.944,  Impt10Color ),
//            new RatioModel( 0.984,  Impt10Color ),
//            new RatioModel( 1.000,  Impt10Color ),
//            new RatioModel( 1.056,  Impt10Color ),
//            new RatioModel( 1.0902, Impt10Color ),
//            new RatioModel( 1.146,  Impt10Color ),
//            new RatioModel( 1.2360, Impt10Color ),
//            new RatioModel( 1.3820, Impt10Color ),
//            new RatioModel( 1.6180, Impt0Color ),
//            new RatioModel( 1.6670, Impt0Color ),
//            new RatioModel( 1.7640, Impt0Color ),
//            new RatioModel( 1.8540, Impt0Color ),
//            new RatioModel( 1.9440, Impt0Color ),
//            new RatioModel( 2.2360, Impt0Color ),
//            new RatioModel( 2.6180, Impt0Color ),
//            new RatioModel( 2.7640, Impt0Color ),
//        };

//        #endregion

//    }
//}

