﻿using fx.Collections;
using fx.TimePeriod;
using fx.Common;
using fx.Definitions;
using fx.DefinitionsWnd;
using StockSharp.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fx.Bars;

namespace fx.Algorithm
{
    public enum RetraceProjectionType : byte
    {
        NONE = 0,
        Retracement = 1,
        Projection = 2,
        TonyProjection = 3,
        TonyRetracement = 4
    }

    public class HewFibGannTargets : IEquatable<HewFibGannTargets>
    {
        bool _showTonyExtension2;
        bool _showTonyExtensions;
        private RetraceProjectionType _retracementOrProjection;
        private FibonacciType _fibCalculationType = FibonacciType.NONE;

        public FibonacciType FibType
        {
            get
            {
                return _fibCalculationType;
            }
        }


        public RetraceProjectionType RetracementOrProjection
        {
            get { return _retracementOrProjection; }
            set
            {
                _retracementOrProjection = value;
            }
        }


        public TimeSpan Period { get; set; }

        public fxFibLevel[ ] GetTonyLevels()
        {
            return WaveFibConstants.TonyDiscoveryLevels;
            //if ( _tonyRetracementLevels != null )
            //{
            //    return _tonyRetracementLevels.FibLevels;
            //}
            //else if ( _tonyFirstLevels != null )
            //{
            //    return _tonyFirstLevels.FibLevels;
            //}
            //else if ( _tonySecondLevels != null )
            //{
            //    return _tonySecondLevels.FibLevels;
            //}

            //return null;
        }

        public fxFibLevel[ ] GetNewFibLevels()
        {
            switch ( FibType )
            {
                case FibonacciType.Wave3ClassicProjection:
                {
                    return WaveFibConstants.ClassicWave3;
                }

                case FibonacciType.Wave3Extended:
                {
                    return WaveFibConstants.ExtendedWave3;
                }

                case FibonacciType.Wave3SuperExtended:
                {
                    return WaveFibConstants.SuperExtendedWave3;
                }

                //case FibonacciType.Wave3All:
                //{
                //    return fxFibRatioConstants.Wave3AllFibLevels;
                //}


                case FibonacciType.Wave3Compressed:
                {
                    return WaveFibConstants.CompressWave3;
                }

                //case FibonacciType.TonyProjection:
                //{
                //    return fxFibRatioConstants.TonyDiscoveryFibLevels;
                //}
            }

            return null;
        }

        public fxFibLevel[ ] GetFibLevels()
        {
            switch ( FibType )
            {
                case FibonacciType.TonyRetracement:
                case FibonacciType.Wave2Retracement:
                {
                    return WaveFibConstants.Wave2RetracementLevels;
                }


                case FibonacciType.Wave4Retracement:
                {
                    return WaveFibConstants.Wave4RetracementLevels;
                }

                case FibonacciType.ABCWaveBRetracement:
                {
                    return WaveFibConstants.ABCWaveBRetracementFibLevels;
                }

                case FibonacciType.Wave3ClassicProjection:
                {
                    return WaveFibConstants.ClassicWave3;
                }

                case FibonacciType.Wave3Extended:
                {
                    return WaveFibConstants.ExtendedWave3;
                }

                case FibonacciType.Wave3SuperExtended:
                {
                    return WaveFibConstants.SuperExtendedWave3;
                }

                case FibonacciType.Wave3All:
                {
                    return WaveFibConstants.AllWave3;
                }

                case FibonacciType.Wave3CProjection:
                {
                    return WaveFibConstants.Wave3CProjectionLevels;
                }

                case FibonacciType.Wave5Projection:
                {
                    return WaveFibConstants.Wave5ProjectionFibLevels;
                }

                case FibonacciType.ABCWaveCProjection:
                {
                    return WaveFibConstants.ABCWaveCProjectionFibLevels;
                }

                case FibonacciType.Wave5CProjection:
                {
                    return WaveFibConstants.Wave5CProjectionFibLevels;
                }

                case FibonacciType.WaveEFBRetracement:
                {
                    return WaveFibConstants.WaveEFBRetracementLevels;
                }

                case FibonacciType.WaveTriBRetracement:
                {
                    return WaveFibConstants.WaveTriBRetracementLevels;
                }

                case FibonacciType.WaveTriCProjection:
                {
                    return WaveFibConstants.WaveTriCRetracementLevels;
                }

                case FibonacciType.WaveTriDProjection:
                {
                    return WaveFibConstants.WaveTriDRetracementLevels;
                }

                case FibonacciType.WaveTriEProjection:
                {
                    return WaveFibConstants.WaveTriERetracementLevels;
                }

                case FibonacciType.FirstXProjection:
                {
                    return WaveFibConstants.FirstXProjectionLevels;
                }

                case FibonacciType.SecondXProjection:
                {
                    return WaveFibConstants.FirstXProjectionLevels;
                }


                case FibonacciType.WaveCProjection:
                {
                    return WaveFibConstants.ABCWaveCProjectionFibLevels;
                }

                case FibonacciType.Wave3Compressed:
                {
                    return WaveFibConstants.CompressWave3;
                }

                case FibonacciType.WaveXRetracement:
                {
                    return WaveFibConstants.WaveXRetracementLevels;
                }

                case FibonacciType.Wave3BRetracement:
                {
                    return WaveFibConstants.Wave3BRetracementLevels;
                }

                case FibonacciType.Wave5BRetracement:
                {
                    return WaveFibConstants.Wave5BRetracementLevels;
                }

                //case FibonacciType.TonyProjection:
                //{
                //    return WaveFibConstants.TonyDiscoveryFibLevels;
                //}
            }

            return null;
        }

        public static PooledList<fxFibLevelCluster> GetFibLevels( FibonacciType fibType, double starting, double ending )
        {
            var temp = new FibLevelsCollection( fibType, starting, ending );

            return temp.FibLevels;
        }



        private HewManager _hews = null;

        private float _startingX = 0;

        private int _startingXIndex = 0;

        private int _endingXIndex = 0;

        private float _endingX = 0;

        private long _owingBarLinuxTime = -1;


        private DateTime _nextWaveIndex = DateTime.MinValue;

        private int _selectedLinesCount = 0;

        private PooledList<int> _selectedFibLevels = new PooledList<int>();

        private FibLevelsCollection _regularRetraceProjectionLevels = null;
        private FibLevelsCollection _tonyFirstLevels = null;
        private FibLevelsCollection _tonySecondLevels = null;
        private FibLevelsCollection _tonyRetracementLevels = null;

        private GannLevelsCollection _gannLevelsCollection = null;

        public FibLevelsCollection RegularRetraceProjectionLevels
        {
            get { return _regularRetraceProjectionLevels; }
        }

        public FibLevelsCollection TonySecondLevels
        {
            get { return _tonySecondLevels; }
        }

        public FibLevelsCollection TonyFirstLevels
        {
            get { return _tonyFirstLevels; }
        }

        public FibLevelsCollection TonyRetracementLevels
        {
            get { return _tonyRetracementLevels; }
        }

        public long OwingBarLinuxTime
        {
            get { return _owingBarLinuxTime; }

            set { _owingBarLinuxTime = value; }
        }


        private (DateTime, float) _tonyStartPoint2 = default;
        public (DateTime Time, float Value) TonyStartPoint2
        {
            get
            {
                return _tonyStartPoint2;
            }
        }

        private (DateTime, float) _tonyEndPoint2 = default;
        public (DateTime Time, float Value) TonyEndPoint2
        {
            get
            {
                return _tonyEndPoint2;
            }
        }

        private (DateTime, float) _tonyStartPoint = default;
        public (DateTime Time, float Value) TonyStartPoint
        {
            get
            {
                return _tonyStartPoint;
            }
        }


        private (DateTime, float) _tonyEndPoint = default;
        public (DateTime Time, float Value) TonyEndPoint
        {
            get
            {
                return _tonyEndPoint;
            }
        }

        private (DateTime, float) _tonyRetracementStartPoint = default;
        public (DateTime Time, float Value) TonyRetracementStartPoint
        {
            get
            {
                return _tonyRetracementStartPoint;
            }
        }


        private (DateTime, float) _tonyRetracementEndPoint = default;
        public (DateTime Time, float Value) TonyRetracementEndPoint
        {
            get
            {
                return _tonyRetracementEndPoint;
            }
        }

        private (DateTime, float) _startPoint = default;

        public (DateTime Time, float Value) StartPoint
        {
            get
            {
                return _startPoint;
            }
        }

        private (DateTime, float) _endPoint = default;
        public (DateTime Time, float Value) EndPoint
        {
            get
            {
                return _endPoint;
            }
        }

        private (DateTime, float) _projectionPoint = default;
        public (DateTime Time, float Value) ProjectionPoint
        {
            get
            {
                return _projectionPoint;
            }
        }

        public bool isRetracement
        {
            get
            {
                return RetracementOrProjection == RetraceProjectionType.Retracement;
            }
        }

        public bool isProjection
        {
            get
            {
                return RetracementOrProjection == RetraceProjectionType.Projection;
            }
        }

        public bool IsUptrend
        {
            get
            {
                return _endPoint.Item2 > _startPoint.Item2;
            }
        }

        PooledList<SBar> _targetBars = new PooledList<SBar>();
        public PooledList<SBar> TargetPoints
        {
            get
            {
                return _targetBars;
            }

            set
            {
                _targetBars = value;
            }
        }

        public SymbolEx Symbol { get; set; }



        public HewFibGannTargets( long owingBarTime, SymbolEx symbol, string name, TimeSpan period )
        {
            Symbol = symbol;
            _owingBarLinuxTime = owingBarTime;

            var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( symbol );

            if ( aa != null )
            {
                _hews = ( HewManager )aa.HewManager;
            }

            _selectedLinesCount = 0;
            Period = period;
        }

        public void SetTonyRetracement( ((DateTime, float) startPoint, (DateTime, float) endPoint) pt, ElliottWaveEnum wave )
        {
            _tonyRetracementStartPoint = pt.startPoint;
            _tonyRetracementEndPoint = pt.endPoint;

            _fibCalculationType = FibonacciType.TonyRetracement;

            RetracementOrProjection = RetraceProjectionType.Retracement;

            _tonyRetracementLevels = new FibLevelsCollection( _fibCalculationType, _tonyRetracementStartPoint, _tonyRetracementEndPoint );
        }




        public void SetTonyExtension( ((DateTime, float) startPoint, (DateTime, float) endPoint) pt, ElliottWaveEnum wave )
        {
            if ( wave == ElliottWaveEnum.Wave2 )
            {
                return;
            }


            _tonyStartPoint = pt.startPoint;
            _tonyEndPoint = pt.endPoint;

            _fibCalculationType = FibonacciType.TonyProjection;

            RetracementOrProjection = RetraceProjectionType.TonyProjection;

            _tonyFirstLevels = new FibLevelsCollection( _fibCalculationType, _tonyStartPoint, _tonyEndPoint );
        }

        public void SetTonyExtension2( ((DateTime, float) startPoint, (DateTime, float) endPoint) pt, ElliottWaveEnum wave )
        {
            if ( wave == ElliottWaveEnum.Wave2 )
            {
                return;
            }

            _tonyStartPoint2 = pt.startPoint;
            _tonyEndPoint2 = pt.endPoint;


            _fibCalculationType = FibonacciType.TonyProjection;

            RetracementOrProjection = RetraceProjectionType.TonyProjection;

            _tonySecondLevels = new FibLevelsCollection( _fibCalculationType, _tonyStartPoint2, _tonyEndPoint2 );
        }

        public bool HasRetracement
        {
            get
            {
                return _startPoint != default && _endPoint != default;
            }
        }

        public bool HasTonyRetracement
        {
            get
            {
                return _tonyRetracementStartPoint != default && _tonyRetracementEndPoint != default;
            }
        }

        public bool HasTonyExtensions
        {
            get
            {
                return _tonyEndPoint != default && _tonyStartPoint != default;
            }
        }


        public bool ShowTonyExtensions
        {
            get => _showTonyExtensions;
            set => _showTonyExtensions = value;
        }


        public bool HasTonyExtensions2
        {
            get
            {
                return _tonyEndPoint2 != default && _tonyStartPoint2 != default;
            }
        }


        public bool ShowTonyExtension2
        {
            get => _showTonyExtension2;
            set => _showTonyExtension2 = value;
        }



        public void SetFibonacciProjections( (DateTime, float) startPoint,
                                             (DateTime, float) endPoint,
                                             (DateTime, float) projectionPoint,
                                             ElliottWaveEnum wave )
        {
            _startPoint = startPoint;
            _endPoint = endPoint;
            _projectionPoint = projectionPoint;

            RetracementOrProjection = RetraceProjectionType.Projection;

            _fibCalculationType = _hews.GetFibonacciProjection( wave );

            if ( _projectionPoint != default )
            {
                TrendDirection direction = endPoint.Item2 > startPoint.Item2 ? TrendDirection.Uptrend : TrendDirection.DownTrend;

                _gannLevelsCollection = new GannLevelsCollection( _projectionPoint.Item2, _projectionPoint.Item1.ToLinuxTime(), direction, 5 );
            }



            _regularRetraceProjectionLevels = new FibLevelsCollection( _fibCalculationType, _startPoint, _endPoint, _projectionPoint );
        }

        public void SetWave3Projections( (DateTime, float) startPoint,
                                             (DateTime, float) endPoint,
                                             (DateTime, float) projectionPoint,
                                             FibonacciType waveType )
        {
            _startPoint = startPoint;
            _endPoint = endPoint;
            _projectionPoint = projectionPoint;

            RetracementOrProjection = RetraceProjectionType.Projection;

            _fibCalculationType = waveType;

            if ( _projectionPoint != default )
            {
                TrendDirection direction = endPoint.Item2 > startPoint.Item2 ? TrendDirection.Uptrend : TrendDirection.DownTrend;

                _gannLevelsCollection = new GannLevelsCollection( _projectionPoint.Item2, _projectionPoint.Item1.ToLinuxTime(), direction, 5 );
            }



            _regularRetraceProjectionLevels = new FibLevelsCollection( _fibCalculationType, _startPoint, _endPoint, _projectionPoint );
        }

        public void SetWaveCProjections( (DateTime, float) startPoint,
                                         (DateTime, float) waveA,
                                         (DateTime, float) waveB,
                                         ElliottWaveEnum wave )
        {
            _startPoint = startPoint;
            _endPoint = waveA;
            _projectionPoint = waveB;

            RetracementOrProjection = RetraceProjectionType.Projection;

            if ( _projectionPoint != default )
            {
                TrendDirection direction = _endPoint.Item2 > _startPoint.Item2 ? TrendDirection.Uptrend : TrendDirection.DownTrend;

                _gannLevelsCollection = new GannLevelsCollection( _projectionPoint.Item2, _projectionPoint.Item1.ToLinuxTime(), direction, 5 );
            }

            _fibCalculationType = _hews.GetWaveCProjectionFibType( wave );

            _regularRetraceProjectionLevels = new FibLevelsCollection( _fibCalculationType, _startPoint, _endPoint, _projectionPoint );

        }

        public void SetFirstXProjections( (DateTime, float) startPoint,
                                          (DateTime, float) waveA,
                                          (DateTime, float) waveB
                                        )
        {
            _startPoint = startPoint;
            _endPoint = waveA;
            _projectionPoint = waveB;

            if ( _projectionPoint != default )
            {
                TrendDirection direction = _endPoint.Item2 > _startPoint.Item2 ? TrendDirection.Uptrend : TrendDirection.DownTrend;

                _gannLevelsCollection = new GannLevelsCollection( _projectionPoint.Item2, _projectionPoint.Item1.ToLinuxTime(), direction, 5 );
            }

            RetracementOrProjection = RetraceProjectionType.Projection;

            _fibCalculationType = FibonacciType.FirstXProjection;

            _regularRetraceProjectionLevels = new FibLevelsCollection( _fibCalculationType, _startPoint, _endPoint, _projectionPoint );

        }

        public void SetSecondXProjections( (DateTime, float) startPoint,
                                           (DateTime, float) waveA,
                                         (DateTime, float) waveB
                                        )
        {
            _startPoint = startPoint;
            _endPoint = waveA;
            _projectionPoint = waveB;

            if ( _projectionPoint != default )
            {
                TrendDirection direction = _endPoint.Item2 > _startPoint.Item2 ? TrendDirection.Uptrend : TrendDirection.DownTrend;

                _gannLevelsCollection = new GannLevelsCollection( _projectionPoint.Item2, _projectionPoint.Item1.ToLinuxTime(), direction, 5 );
            }

            RetracementOrProjection = RetraceProjectionType.Projection;

            _fibCalculationType = FibonacciType.SecondXProjection;

            _regularRetraceProjectionLevels = new FibLevelsCollection( _fibCalculationType, _startPoint, _endPoint, _projectionPoint );

        }

        public void SetFibonacciRetracment( (DateTime, float) startPoint,
                                            (DateTime, float) endPoint,
                                            ElliottWaveEnum wave )
        {
            _startPoint = startPoint;
            _endPoint = endPoint;

            RetracementOrProjection = RetraceProjectionType.Retracement;

            _fibCalculationType = _hews.GetFibonacciRetracment( wave );

            TrendDirection direction = endPoint.Item2 > startPoint.Item2 ? TrendDirection.DownTrend : TrendDirection.Uptrend;

            _regularRetraceProjectionLevels = new FibLevelsCollection( _fibCalculationType, _startPoint, _endPoint );

            _gannLevelsCollection = new GannLevelsCollection( endPoint.Item2, endPoint.Item1.ToLinuxTime(), direction, 5 );
        }

        public PooledList<SRlevel> GetFibonacciSRLevels()
        {
            return GetFibonacciSRLevels( FibType );
        }


        public PooledList<SRlevel> GetFibonacciSRLevels( FibonacciType fibType )
        {
            var output = new PooledList<SRlevel>( 20 );

            int i = 0;

            switch ( fibType )
            {
                case FibonacciType.WaveCProjection:
                {
                    for ( i = 0; i < WaveFibConstants.ABCWaveCProjectionFibLevels.Length; i++ )
                    {
                        var level = ProjectionPoint.Value + ( ( EndPoint.Value - StartPoint.Value ) * WaveFibConstants.ABCWaveCProjectionFibLevels[i].FibValue / 100 );
                        var strength = WaveFibConstants.ABCWaveCProjectionFibLevels[i].FibStrength;
                        var tb = new TimeBlockEx( StartPoint.Time, EndPoint.Time );
                        var lvl = new SRlevel( ref tb, TimeSpan.FromMinutes( 1 ), level, strength, SR3rdType.WaveCProjection );

                        output.Add( lvl );
                    }
                }
                break;

                case FibonacciType.Wave2Retracement:
                {
                    for ( i = 0; i < WaveFibConstants.Wave2RetracementLevels.Length; i++ )
                    {
                        var level = EndPoint.Value + ( ( StartPoint.Value - EndPoint.Value ) * WaveFibConstants.Wave2RetracementLevels[i].FibValue / 100 );
                        var strength = WaveFibConstants.Wave2RetracementLevels[i].FibStrength;
                        var tb = new TimeBlockEx( StartPoint.Time, EndPoint.Time );
                        var lvl = new SRlevel( ref tb, TimeSpan.FromMinutes( 1 ), level, strength, SR3rdType.Wave2Retracement );

                        output.Add( lvl );
                    }
                }
                break;

                case FibonacciType.FirstXProjection:
                {
                    for ( i = 0; i < WaveFibConstants.FirstXProjectionLevels.Length; i++ )
                    {
                        var level = ProjectionPoint.Value + ( ( EndPoint.Value - StartPoint.Value ) * WaveFibConstants.FirstXProjectionLevels[i].FibValue / 100 );
                        var strength = WaveFibConstants.FirstXProjectionLevels[i].FibStrength;
                        var tb = new TimeBlockEx( StartPoint.Time, ProjectionPoint.Time );
                        var lvl = new SRlevel( ref tb, TimeSpan.FromMinutes( 1 ), level, strength, SR3rdType.FirstXProjection );

                        output.Add( lvl );
                    }
                }
                break;

                case FibonacciType.SecondXProjection:
                {
                    for ( i = 0; i < WaveFibConstants.FirstXProjectionLevels.Length; i++ )
                    {
                        var level = ProjectionPoint.Value + ( ( EndPoint.Value - StartPoint.Value ) * WaveFibConstants.FirstXProjectionLevels[i].FibValue / 100 );
                        var strength = WaveFibConstants.FirstXProjectionLevels[i].FibStrength;
                        var tb = new TimeBlockEx( StartPoint.Time, ProjectionPoint.Time );
                        var lvl = new SRlevel( ref tb, TimeSpan.FromMinutes( 1 ), level, strength, SR3rdType.SecondXProjection );

                        output.Add( lvl );
                    }
                }
                break;

                case FibonacciType.Wave3ClassicProjection:
                {
                    for ( i = 0; i < WaveFibConstants.ClassicWave3.Length; i++ )
                    {
                        var level = ProjectionPoint.Value + ( ( EndPoint.Value - StartPoint.Value ) * WaveFibConstants.ClassicWave3[i].FibValue / 100 );
                        var strength = WaveFibConstants.ClassicWave3[i].FibStrength;
                        var tb = new TimeBlockEx( StartPoint.Time, ProjectionPoint.Time );
                        var lvl = new SRlevel( ref tb, TimeSpan.FromMinutes( 1 ), level, strength, SR3rdType.Wave3ClassicProjection );

                        output.Add( lvl );
                    }
                }
                break;

                case FibonacciType.Wave3Compressed:
                {
                    for ( i = 0; i < WaveFibConstants.CompressWave3.Length; i++ )
                    {
                        var level = ProjectionPoint.Value + ( ( EndPoint.Value - StartPoint.Value ) * WaveFibConstants.CompressWave3[i].FibValue / 100 );
                        var strength = WaveFibConstants.CompressWave3[i].FibStrength;
                        var tb = new TimeBlockEx( StartPoint.Time, ProjectionPoint.Time );
                        var lvl = new SRlevel( ref tb, TimeSpan.FromMinutes( 1 ), level, strength, SR3rdType.Wave3ClassicProjection );

                        output.Add( lvl );
                    }
                }
                break;

                case FibonacciType.Wave3Extended:
                {
                    for ( i = 0; i < WaveFibConstants.ExtendedWave3.Length; i++ )
                    {
                        var level = ProjectionPoint.Value + ( ( EndPoint.Value - StartPoint.Value ) * WaveFibConstants.ExtendedWave3[i].FibValue / 100 );
                        var strength = WaveFibConstants.ExtendedWave3[i].FibStrength;
                        var tb = new TimeBlockEx( StartPoint.Time, ProjectionPoint.Time );
                        var lvl = new SRlevel( ref tb, TimeSpan.FromMinutes( 1 ), level, strength, SR3rdType.Wave3ClassicProjection );

                        output.Add( lvl );
                    }
                }
                break;

                case FibonacciType.Wave3SuperExtended:
                {
                    for ( i = 0; i < WaveFibConstants.SuperExtendedWave3.Length; i++ )
                    {
                        var level = ProjectionPoint.Value + ( ( EndPoint.Value - StartPoint.Value ) * WaveFibConstants.SuperExtendedWave3[i].FibValue / 100 );
                        var strength = WaveFibConstants.SuperExtendedWave3[i].FibStrength;
                        var tb = new TimeBlockEx( StartPoint.Time, ProjectionPoint.Time );
                        var lvl = new SRlevel( ref tb, TimeSpan.FromMinutes( 1 ), level, strength, SR3rdType.Wave3ClassicProjection );

                        output.Add( lvl );
                    }
                }
                break;

                case FibonacciType.Wave3All:
                {
                    for ( i = 0; i < WaveFibConstants.AllWave3.Length; i++ )
                    {
                        var level = ProjectionPoint.Value + ( ( EndPoint.Value - StartPoint.Value ) * WaveFibConstants.AllWave3[i].FibValue / 100 );
                        var strength = WaveFibConstants.AllWave3[i].FibStrength;
                        var tb = new TimeBlockEx( StartPoint.Time, ProjectionPoint.Time );
                        var lvl = new SRlevel( ref tb, TimeSpan.FromMinutes( 1 ), level, strength, SR3rdType.Wave3ClassicProjection );

                        output.Add( lvl );
                    }
                }
                break;

                case FibonacciType.Wave3CProjection:
                {
                    for ( i = 0; i < WaveFibConstants.Wave3CProjectionLevels.Length; i++ )
                    {
                        var level = ProjectionPoint.Value + ( ( EndPoint.Value - StartPoint.Value ) * WaveFibConstants.Wave3CProjectionLevels[i].FibValue / 100 );
                        var strength = WaveFibConstants.Wave3CProjectionLevels[i].FibStrength;
                        var tb = new TimeBlockEx( StartPoint.Time, ProjectionPoint.Time );
                        var lvl = new SRlevel( ref tb, TimeSpan.FromMinutes( 1 ), level, strength, SR3rdType.Wave3CProjection );

                        output.Add( lvl );
                    }
                }
                break;

                case FibonacciType.TonyRetracement:
                case FibonacciType.Wave4Retracement:
                {
                    for ( i = 0; i < WaveFibConstants.Wave4RetracementLevels.Length; i++ )
                    {
                        var level = EndPoint.Value + ( ( StartPoint.Value - EndPoint.Value ) * WaveFibConstants.Wave4RetracementLevels[i].FibValue / 100 );
                        var strength = WaveFibConstants.Wave4RetracementLevels[i].FibStrength;
                        var tb = new TimeBlockEx( StartPoint.Time, EndPoint.Time );
                        var lvl = new SRlevel( ref tb, TimeSpan.FromMinutes( 1 ), level, strength, SR3rdType.Wave4Retracement );

                        output.Add( lvl );
                    }
                }
                break;

                case FibonacciType.Wave5Projection:
                {
                    for ( i = 0; i < WaveFibConstants.Wave5ProjectionFibLevels.Length; i++ )
                    {
                        var level = ProjectionPoint.Value + ( ( EndPoint.Value - StartPoint.Value ) * WaveFibConstants.Wave5ProjectionFibLevels[i].FibValue / 100 );
                        var strength = WaveFibConstants.Wave5ProjectionFibLevels[i].FibStrength;
                        var tb = new TimeBlockEx( StartPoint.Time, ProjectionPoint.Time );
                        var lvl = new SRlevel( ref tb, TimeSpan.FromMinutes( 1 ), level, strength, SR3rdType.Wave5Projection );

                        output.Add( lvl );
                    }
                }
                break;

                case FibonacciType.Wave5CProjection:
                {
                    for ( i = 0; i < WaveFibConstants.Wave5ProjectionFibLevels.Length; i++ )
                    {
                        var level = ProjectionPoint.Value + ( ( EndPoint.Value - StartPoint.Value ) * WaveFibConstants.Wave5ProjectionFibLevels[i].FibValue / 100 );
                        var strength = WaveFibConstants.Wave5ProjectionFibLevels[i].FibStrength;
                        var tb = new TimeBlockEx( StartPoint.Time, ProjectionPoint.Time );
                        var lvl = new SRlevel( ref tb, TimeSpan.FromMinutes( 1 ), level, strength, SR3rdType.Wave5CProjection );

                        output.Add( lvl );
                    }
                }
                break;

                case FibonacciType.ABCWaveCProjection:
                {
                    for ( i = 0; i < WaveFibConstants.ABCWaveCProjectionFibLevels.Length; i++ )
                    {
                        var level = ProjectionPoint.Value + ( ( EndPoint.Value - StartPoint.Value ) * WaveFibConstants.ABCWaveCProjectionFibLevels[i].FibValue / 100 );
                        var strength = WaveFibConstants.ABCWaveCProjectionFibLevels[i].FibStrength;
                        var tb = new TimeBlockEx( StartPoint.Time, ProjectionPoint.Time );
                        var lvl = new SRlevel( ref tb, TimeSpan.FromMinutes( 1 ), level, strength, SR3rdType.ABCWaveCProjection );

                        output.Add( lvl );
                    }
                }
                break;

                case FibonacciType.ABCWaveBRetracement:
                {
                    for ( i = 0; i < WaveFibConstants.ABCWaveBRetracementLevels.Length; i++ )
                    {
                        var level = EndPoint.Value + ( ( StartPoint.Value - EndPoint.Value ) * WaveFibConstants.ABCWaveBRetracementLevels[i].FibValue / 100 );
                        var strength = WaveFibConstants.ABCWaveBRetracementLevels[i].FibStrength;
                        var tb = new TimeBlockEx( StartPoint.Time, EndPoint.Time );
                        var lvl = new SRlevel( ref tb, TimeSpan.FromMinutes( 1 ), level, strength, SR3rdType.ABCWaveBRetracement );

                        output.Add( lvl );
                    }
                }
                break;

                case FibonacciType.Wave3BRetracement:
                {
                    for ( i = 0; i < WaveFibConstants.Wave3BRetracementLevels.Length; i++ )
                    {
                        var level = EndPoint.Value + ( ( StartPoint.Value - EndPoint.Value ) * WaveFibConstants.Wave3BRetracementLevels[i].FibValue / 100 );
                        var strength = WaveFibConstants.Wave3BRetracementLevels[i].FibStrength;
                        var tb = new TimeBlockEx( StartPoint.Time, EndPoint.Time );
                        var lvl = new SRlevel( ref tb, TimeSpan.FromMinutes( 1 ), level, strength, SR3rdType.ABCWaveBRetracement );

                        output.Add( lvl );
                    }
                }
                break;

                case FibonacciType.Wave5BRetracement:
                {
                    for ( i = 0; i < WaveFibConstants.Wave5BRetracementLevels.Length; i++ )
                    {
                        var level = EndPoint.Value + ( ( StartPoint.Value - EndPoint.Value ) * WaveFibConstants.Wave5BRetracementLevels[i].FibValue / 100 );
                        var strength = WaveFibConstants.Wave5BRetracementLevels[i].FibStrength;
                        var tb = new TimeBlockEx( StartPoint.Time, EndPoint.Time );
                        var lvl = new SRlevel( ref tb, TimeSpan.FromMinutes( 1 ), level, strength, SR3rdType.ABCWaveBRetracement );

                        output.Add( lvl );
                    }
                }
                break;

                case FibonacciType.WaveEFBRetracement:
                {
                    for ( i = 0; i < WaveFibConstants.WaveEFBRetracementLevels.Length; i++ )
                    {
                        var level = EndPoint.Value + ( ( StartPoint.Value - EndPoint.Value ) * WaveFibConstants.WaveEFBRetracementLevels[i].FibValue / 100 );
                        var strength = WaveFibConstants.WaveEFBRetracementLevels[i].FibStrength;
                        var tb = new TimeBlockEx( StartPoint.Time, EndPoint.Time );
                        var lvl = new SRlevel( ref tb, TimeSpan.FromMinutes( 1 ), level, strength, SR3rdType.WaveEFBRetracement );

                        output.Add( lvl );
                    }
                }
                break;

                case FibonacciType.WaveTriBRetracement:
                {
                    for ( i = 0; i < WaveFibConstants.WaveTriBRetracementLevels.Length; i++ )
                    {
                        var level = EndPoint.Value + ( ( StartPoint.Value - EndPoint.Value ) * WaveFibConstants.WaveTriBRetracementLevels[i].FibValue / 100 );
                        var strength = WaveFibConstants.WaveTriBRetracementLevels[i].FibStrength;
                        var tb = new TimeBlockEx( StartPoint.Time, EndPoint.Time );
                        var lvl = new SRlevel( ref tb, TimeSpan.FromMinutes( 1 ), level, strength, SR3rdType.WaveTriBRetracement );

                        output.Add( lvl );
                    }
                }
                break;

                case FibonacciType.WaveTriCProjection:
                {
                    for ( i = 0; i < WaveFibConstants.WaveTriCRetracementLevels.Length; i++ )
                    {
                        var level = EndPoint.Value + ( ( StartPoint.Value - EndPoint.Value ) * WaveFibConstants.WaveTriCRetracementLevels[i].FibValue / 100 );
                        var strength = WaveFibConstants.WaveTriCRetracementLevels[i].FibStrength;
                        var tb = new TimeBlockEx( StartPoint.Time, EndPoint.Time );
                        var lvl = new SRlevel( ref tb, TimeSpan.FromMinutes( 1 ), level, strength, SR3rdType.WaveTriCProjection );

                        output.Add( lvl );
                    }
                }
                break;

                case FibonacciType.WaveTriDProjection:
                {
                    for ( i = 0; i < WaveFibConstants.WaveTriDRetracementLevels.Length; i++ )
                    {
                        var level = EndPoint.Value + ( ( StartPoint.Value - EndPoint.Value ) * WaveFibConstants.WaveTriDRetracementLevels[i].FibValue / 100 );
                        var strength = WaveFibConstants.WaveTriDRetracementLevels[i].FibStrength;
                        var tb = new TimeBlockEx( StartPoint.Time, EndPoint.Time );
                        var lvl = new SRlevel( ref tb, TimeSpan.FromMinutes( 1 ), level, strength, SR3rdType.WaveTriDProjection );

                        output.Add( lvl );
                    }
                }
                break;

                case FibonacciType.WaveTriEProjection:
                {
                    for ( i = 0; i < WaveFibConstants.WaveTriERetracementLevels.Length; i++ )
                    {
                        var level = EndPoint.Value + ( ( StartPoint.Value - EndPoint.Value ) * WaveFibConstants.WaveTriERetracementLevels[i].FibValue / 100 );
                        var strength = WaveFibConstants.WaveTriERetracementLevels[i].FibStrength;
                        var tb = new TimeBlockEx( StartPoint.Time, EndPoint.Time );
                        var lvl = new SRlevel( ref tb, TimeSpan.FromMinutes( 1 ), level, strength, SR3rdType.WaveTriEProjection );

                        output.Add( lvl );
                    }
                }
                break;

                case FibonacciType.TonyProjection:
                {
                    for ( i = 0; i < WaveFibConstants.TonyDiscoveryLevels.Length; i++ )
                    {
                        var level = EndPoint.Value + ( ( StartPoint.Value - EndPoint.Value ) * WaveFibConstants.TonyDiscoveryLevels[i].FibValue / 100 );
                        var strength = WaveFibConstants.TonyDiscoveryLevels[i].FibStrength;

                        var tb = new TimeBlockEx( StartPoint.Time, EndPoint.Time );
                        var lvl = new SRlevel( ref tb, TimeSpan.FromMinutes( 1 ), level, strength, SR3rdType.TonyProjection );

                        output.Add( lvl );
                    }
                }
                break;
            }

            return output;
        }


        public void SetEndingIndexTime( DateTime barIndex )
        {
            _nextWaveIndex = barIndex;
        }

        public DateTime GetEndingIndexTime()
        {
            return _nextWaveIndex;
        }



        public bool isValid()
        {
            if ( RetracementOrProjection != RetraceProjectionType.NONE )
            {
                if ( RetracementOrProjection == RetraceProjectionType.Retracement )
                {
                    if ( _startPoint != default && _endPoint != default )
                    {
                        if ( _endPoint.Item1 > _startPoint.Item1 && _endPoint.Item2 > 0 && _startPoint.Item2 > 0 )
                        {
                            return true;
                        }
                    }
                }
                else if ( RetracementOrProjection == RetraceProjectionType.Projection )
                {
                    if ( _startPoint != default && _endPoint != default && _projectionPoint != default )
                    {
                        if ( _endPoint.Item1 > _startPoint.Item1 && _endPoint.Item2 > 0 && _startPoint.Item2 > 0 )
                        {
                            return true;
                        }
                    }
                }
                else if ( RetracementOrProjection == RetraceProjectionType.TonyProjection )
                {
                    if ( _tonyStartPoint != default && _tonyEndPoint != default )
                    {
                        if ( _tonyEndPoint.Item1 > _tonyStartPoint.Item1 && _tonyEndPoint.Item2 > 0 && _tonyStartPoint.Item2 > 0 )
                        {
                            return true;
                        }
                    }

                    if ( _tonyStartPoint2 != default && _tonyEndPoint2 != default )
                    {
                        if ( _tonyEndPoint2.Item1 > _tonyStartPoint2.Item1 && _tonyEndPoint2.Item2 > 0 && _tonyStartPoint2.Item2 > 0 )
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }


        /* -------------------------------------------------------------------------------------------------------------------------------------------
        * 
        *  In this function, I want to analyse What will be a highly likely target those wave importances that have been created  
        *  I will match with the three model of Wave 3 to see which one fit the most closely
        * 
        * ------------------------------------------------------------------------------------------------------------------------------------------- 
        */
        public void Analyse3rdWave()
        {
            foreach ( SBar bar in TargetPoints )
            {
                if ( bar.IsWavePeak() )
                {
                    GetClosestFibLevel( bar.High, out fxFibLevelCluster closestLine );
                }
                else if ( bar.IsWaveTrough() )
                {
                    GetClosestFibLevel( bar.Low, out fxFibLevelCluster closestLine );
                }
            }
        }

        public bool GetClosestFibLevel( double lineValue, out fxFibLevelCluster closestLine )
        {
            double minDiff = double.MaxValue;
            closestLine = default;

            if ( RegularRetraceProjectionLevels == null )
                return false;

            bool found = false;

            foreach ( var level in RegularRetraceProjectionLevels.FibLevels )
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


        public PooledList<fxFibLevelCluster> GetAllFibLevelsAbove( double lineValue, out double highestLevel )
        {
            var output = new PooledList<fxFibLevelCluster>();

            highestLevel = double.MinValue;

            foreach ( var level in RegularRetraceProjectionLevels.FibLevels )
            {
                var fibLvlY = level.FibValue;

                if ( fibLvlY == 0 )
                    continue;

                if ( fibLvlY >= lineValue )
                {
                    if ( fibLvlY > highestLevel )
                    {
                        highestLevel = fibLvlY;
                    }

                    output.Add( level );
                }
            }

            if ( HasTonyRetracement )
            {
                foreach ( var level in TonyRetracementLevels.FibLevels )
                {
                    var fibLvlY = level.FibValue;

                    if ( fibLvlY == 0 )
                        continue;

                    if ( fibLvlY >= lineValue )
                    {
                        if ( fibLvlY > highestLevel )
                        {
                            highestLevel = fibLvlY;
                        }

                        output.Add( level );
                    }
                }
            }

            return output;
        }




        public PooledList<fxFibLevelCluster> GetAllFibLevelsBelow( double lineValue, out double lowestLevel )
        {
            lowestLevel = double.MaxValue;

            var output = new PooledList<fxFibLevelCluster>();

            foreach ( var level in RegularRetraceProjectionLevels.FibLevels )
            {
                var fibLvlY = level.FibValue;

                if ( fibLvlY == 0 )
                    continue;

                if ( fibLvlY < lineValue )
                {
                    if ( fibLvlY < lowestLevel )
                    {
                        lowestLevel = fibLvlY;
                    }

                    output.Add( level );
                }
            }

            if ( HasTonyRetracement )
            {
                foreach ( var level in TonyRetracementLevels.FibLevels )
                {
                    var fibLvlY = level.FibValue;

                    if ( fibLvlY == 0 )
                        continue;

                    if ( fibLvlY < lineValue )
                    {
                        if ( fibLvlY < lowestLevel )
                        {
                            lowestLevel = fibLvlY;
                        }

                        output.Add( level );
                    }
                }
            }

            return output;
        }
        public PooledList<fxFibLevelCluster> GetAllFibLevelsLT( FibPercentage fibPercentage )
        {
            var output = new PooledList<fxFibLevelCluster>();

            foreach ( var level in RegularRetraceProjectionLevels.FibLevels )
            {
                var fibPer = level.FibPrecentage;

                if ( fibPer == 0 )
                    continue;

                if ( fibPer < fibPercentage )
                {
                    output.Add( level );
                }
            }

            return output;
        }

        public PooledList<fxFibLevelCluster> GetAllFibLevelsLE( FibPercentage fibPercentage, double pipsDiff )
        {
            var output = new PooledList<fxFibLevelCluster>();

            foreach ( var level in RegularRetraceProjectionLevels.FibLevels )
            {
                var fibPer = level.FibPrecentage;

                if ( fibPer == 0 )
                    continue;

                if ( fibPer <= fibPercentage )
                {
                    output.Add( level );
                }
            }

            if ( HasTonyRetracement )
            {
                foreach ( var fibLevelInfo in TonyRetracementLevels.FibLevels )
                {
                    var index = output.FindIndex( x => Math.Abs( x.FibValue - fibLevelInfo.FibValue ) < pipsDiff );

                    if ( index > -1 )
                    {
                        var selectedLevel = output[index];

                        selectedLevel.UpdateAll( fibLevelInfo );
                    }
                    else
                    {
                        if ( fibLevelInfo.FibPrecentage <= fibPercentage )
                        {
                            output.Add( fibLevelInfo );
                        }
                    }
                }
            }

            return output;
        }

        public PooledList<fxFibLevelCluster> GetFibLevelsBtw( FibPercentage lowerPct, FibPercentage higherPct )
        {
            var output = new PooledList<fxFibLevelCluster>();

            foreach ( var level in RegularRetraceProjectionLevels.FibLevels )
            {
                if ( level.FibPrecentage >= lowerPct && level.FibPrecentage <= higherPct )
                {
                    output.Add( level );
                }
            }

            return output;
        }

        public PooledList<fxFibLevelCluster> TonyGetFibLevelsBtw( FibPercentage lowerPct, FibPercentage higherPct, double pipsDiff )
        {
            PooledList<fxFibLevelCluster> output = GetFibLevelsBtw( lowerPct, higherPct );

            if ( HasTonyRetracement )
            {
                foreach ( var fibLevelInfo in TonyRetracementLevels.FibLevels )
                {
                    var index = output.FindIndex( x => Math.Abs( x.FibValue - fibLevelInfo.FibValue ) < pipsDiff );

                    if ( index > -1 )
                    {
                        var selectedLevel = output[index];

                        selectedLevel.UpdateAll( fibLevelInfo );
                    }
                    else
                    {
                        if ( fibLevelInfo.FibPrecentage >= lowerPct && fibLevelInfo.FibPrecentage <= higherPct )
                        {
                            output.Add( fibLevelInfo );
                        }
                    }
                }
            }

            return output;
        }

        public PooledList<fxFibLevelCluster> GetAllFibLevelsGT( FibPercentage fibPercentage )
        {
            var output = new PooledList<fxFibLevelCluster>();

            foreach ( var level in RegularRetraceProjectionLevels.FibLevels )
            {
                var fibPer = level.FibPrecentage;

                if ( fibPer == 0 )
                    continue;

                if ( fibPer > fibPercentage )
                {
                    output.Add( level );
                }
            }

            return output;
        }

        public PooledList<fxFibLevelCluster> GetAllFibLevelsGE( FibPercentage fibPercentage )
        {
            var output = new PooledList<fxFibLevelCluster>();

            foreach ( var level in RegularRetraceProjectionLevels.FibLevels )
            {
                var fibPer = level.FibPrecentage;

                if ( fibPer == 0 )
                    continue;

                if ( fibPer >= fibPercentage )
                {
                    output.Add( level );
                }
            }

            return output;
        }

        public override bool Equals( object obj )
        {
            if ( obj is HewFibGannTargets )
            {
                return Equals( ( HewFibGannTargets )obj );
            }

            return base.Equals( obj );
        }

        public static bool operator ==( HewFibGannTargets first, HewFibGannTargets second )
        {
            if ( ( object )first == null )
            {
                return ( object )second == null;
            }

            return first.Equals( second );
        }

        public static bool operator !=( HewFibGannTargets first, HewFibGannTargets second )
        {
            return !( first == second );
        }

        public bool Equals( HewFibGannTargets other )
        {
            if ( ReferenceEquals( null, other ) )
            {
                return false;
            }

            if ( ReferenceEquals( this, other ) )
            {
                return true;
            }

            return _retracementOrProjection.Equals( other._retracementOrProjection ) && _fibCalculationType.Equals( other._fibCalculationType ) && Equals( _hews, other._hews ) && _startingX.Equals( other._startingX ) && _startingXIndex.Equals( other._startingXIndex ) && _endingXIndex.Equals( other._endingXIndex ) && _endingX.Equals( other._endingX ) && _owingBarLinuxTime.Equals( other._owingBarLinuxTime ) && _nextWaveIndex.Equals( other._nextWaveIndex ) && _selectedLinesCount.Equals( other._selectedLinesCount ) && Equals( _selectedFibLevels, other._selectedFibLevels ) && Equals( _regularRetraceProjectionLevels, other._regularRetraceProjectionLevels ) && Equals( _tonyFirstLevels, other._tonyFirstLevels ) && Equals( _tonySecondLevels, other._tonySecondLevels ) && Equals( _tonyRetracementLevels, other._tonyRetracementLevels ) && Equals( _gannLevelsCollection, other._gannLevelsCollection ) && _tonyStartPoint2.Equals( other._tonyStartPoint2 ) && _tonyEndPoint2.Equals( other._tonyEndPoint2 ) && _tonyStartPoint.Equals( other._tonyStartPoint ) && _tonyEndPoint.Equals( other._tonyEndPoint ) && _tonyRetracementStartPoint.Equals( other._tonyRetracementStartPoint ) && _tonyRetracementEndPoint.Equals( other._tonyRetracementEndPoint ) && _startPoint.Equals( other._startPoint ) && _endPoint.Equals( other._endPoint ) && _projectionPoint.Equals( other._projectionPoint ) && Equals( _targetBars, other._targetBars ) && Period.Equals( other.Period ) && isRetracement.Equals( other.isRetracement ) && isProjection.Equals( other.isProjection ) && Symbol.Equals( other.Symbol );
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 47;
                hashCode = ( hashCode * 53 ) ^ ( int )_retracementOrProjection;
                hashCode = ( hashCode * 53 ) ^ ( int )_fibCalculationType;
                if ( _hews != null )
                {
                    hashCode = ( hashCode * 53 ) ^ EqualityComparer<HewManager>.Default.GetHashCode( _hews );
                }

                hashCode = ( hashCode * 53 ) ^ _startingX.GetHashCode();
                hashCode = ( hashCode * 53 ) ^ _startingXIndex.GetHashCode();
                hashCode = ( hashCode * 53 ) ^ _endingXIndex.GetHashCode();
                hashCode = ( hashCode * 53 ) ^ _endingX.GetHashCode();
                hashCode = ( hashCode * 53 ) ^ _owingBarLinuxTime.GetHashCode();


                hashCode = ( hashCode * 53 ) ^ _nextWaveIndex.GetHashCode();
                hashCode = ( hashCode * 53 ) ^ _selectedLinesCount.GetHashCode();
                if ( _selectedFibLevels != null )
                {
                    hashCode = ( hashCode * 53 ) ^ EqualityComparer<PooledList<int>>.Default.GetHashCode( _selectedFibLevels );
                }

                if ( _regularRetraceProjectionLevels != null )
                {
                    hashCode = ( hashCode * 53 ) ^ EqualityComparer<FibLevelsCollection>.Default.GetHashCode( _regularRetraceProjectionLevels );
                }

                if ( _tonyFirstLevels != null )
                {
                    hashCode = ( hashCode * 53 ) ^ EqualityComparer<FibLevelsCollection>.Default.GetHashCode( _tonyFirstLevels );
                }

                if ( _tonySecondLevels != null )
                {
                    hashCode = ( hashCode * 53 ) ^ EqualityComparer<FibLevelsCollection>.Default.GetHashCode( _tonySecondLevels );
                }

                if ( _tonyRetracementLevels != null )
                {
                    hashCode = ( hashCode * 53 ) ^ EqualityComparer<FibLevelsCollection>.Default.GetHashCode( _tonyRetracementLevels );
                }

                if ( _gannLevelsCollection != null )
                {
                    hashCode = ( hashCode * 53 ) ^ EqualityComparer<GannLevelsCollection>.Default.GetHashCode( _gannLevelsCollection );
                }

                hashCode = ( hashCode * 53 ) ^ EqualityComparer<(DateTime, float)>.Default.GetHashCode( _tonyStartPoint2 );
                hashCode = ( hashCode * 53 ) ^ EqualityComparer<(DateTime, float)>.Default.GetHashCode( _tonyEndPoint2 );
                hashCode = ( hashCode * 53 ) ^ EqualityComparer<(DateTime, float)>.Default.GetHashCode( _tonyStartPoint );
                hashCode = ( hashCode * 53 ) ^ EqualityComparer<(DateTime, float)>.Default.GetHashCode( _tonyEndPoint );
                hashCode = ( hashCode * 53 ) ^ EqualityComparer<(DateTime, float)>.Default.GetHashCode( _tonyRetracementStartPoint );
                hashCode = ( hashCode * 53 ) ^ EqualityComparer<(DateTime, float)>.Default.GetHashCode( _tonyRetracementEndPoint );
                hashCode = ( hashCode * 53 ) ^ EqualityComparer<(DateTime, float)>.Default.GetHashCode( _startPoint );
                hashCode = ( hashCode * 53 ) ^ EqualityComparer<(DateTime, float)>.Default.GetHashCode( _endPoint );
                hashCode = ( hashCode * 53 ) ^ EqualityComparer<(DateTime, float)>.Default.GetHashCode( _projectionPoint );
                if ( _targetBars != null )
                {
                    hashCode = ( hashCode * 53 ) ^ EqualityComparer<PooledList<SBar>>.Default.GetHashCode( _targetBars );
                }

                hashCode = ( hashCode * 53 ) ^ EqualityComparer<TimeSpan>.Default.GetHashCode( Period );
                hashCode = ( hashCode * 53 ) ^ isRetracement.GetHashCode();
                hashCode = ( hashCode * 53 ) ^ isProjection.GetHashCode();
                hashCode = ( hashCode * 53 ) ^ EqualityComparer<SymbolEx>.Default.GetHashCode( Symbol );
                return hashCode;
            }
        }
    }
}
