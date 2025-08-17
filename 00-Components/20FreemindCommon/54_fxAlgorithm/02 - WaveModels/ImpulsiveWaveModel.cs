using fx.Bars;
using fx.Collections;
using fx.Common;
using fx.Definitions;
using StockSharp.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Algorithm
{
    //![](9B4595CED784909AC1195C12F1754846.png;;;0.03159,0.02605)
    public partial class ImpulsiveWaveModel : IWaveModel
    {
        private HewManager _hews;
        private fxHistoricBarsRepo _bars;

        HewFibGannTargets _wave2Ret = null;
        HewFibGannTargets _wave3BRet = null;
        HewFibGannTargets _wave3Exp = null;
        HewFibGannTargets _wave4Ret = null;
        HewFibGannTargets _wave5Exp = null;

        private PooledList< FibLevelInfo > _largerTargets = null;
        private PooledList< FibLevelInfo > _unfoldingTargets = null;
        private PooledList< FibLevelInfo > _predictedTargets = null;

        private IWaveModel _unfoldingWave = null;

        public ImpulsiveWaveModel( fxHistoricBarsRepo bars, HewManager hewManager )
        {
            _bars = bars;
            _hews = hewManager;
        }

        
        public PooledList<FibLevelInfo> LargerTargets
        {
            get { return _largerTargets; }
            set
            {
                _largerTargets = value;
            }
        }

        public PooledList<FibLevelInfo> UnfoldingTargets
        {
            get { return _unfoldingTargets; }
            set
            {
                _unfoldingTargets = value;
            }
        }

        public PooledList<FibLevelInfo> PredictedTargets
        {
            get { return _unfoldingTargets; }
            set
            {
                _unfoldingTargets = value;
            }
        }

        public TrendDirection Trend
        {
            get
            {
                if ( Bar0 != SBar.EmptySBar)
                {
                    if( Bar0.IsTrough( ) )
                    {
                        return TrendDirection.Uptrend;
                    }
                    else if ( Bar0.IsPeak() )
                    {
                        return TrendDirection.DownTrend;
                    }
                        
                }

                return TrendDirection.NoTrend;
            }            
        }        

        public void GetFibExtremum( out FibLevelInfo output )
        {
            output = default;

            if ( Trend == TrendDirection.Uptrend )
            {
                double max = double.MinValue;
                foreach ( var level in _largerTargets )
                {
                    if ( level.FibLevel > max )
                    {
                        max = level.FibLevel;
                        output = level;
                    }
                }
            }
            else if ( Trend == TrendDirection.DownTrend )
            {
                double min = double.MaxValue;
                foreach ( var level in _largerTargets )
                {
                    if ( level.FibLevel < min )
                    {
                        min = level.FibLevel;
                        output = level;
                    }
                }
            }
        }

        public Wave3Type Wave3Type { get; set; }

        public FibPercentage Wave2Ret { get; set; }
        public FibPercentage Wave3aProj { get; set; }
        public FibPercentage Wave3cProj { get; set; }

        public FibPercentage Wave2toWave1 { get; set; }

        public FibPercentage Wave3bRet { get; set; }
        public FibPercentage Wave3btoWave1 { get; set; }

        public FibPercentage Wave4Ret { get; set; }
        public FibPercentage Wave4toWave1 { get; set; }

        public FibPercentage Wave5Proj { get; set; }

        public SBar Bar0;

        public SBar Bar1;

        public SBar Bar2;

        public SBar Bar3A;

        public SBar Bar3B;

        public SBar Bar3C;

        public SBar Bar4;

        public SBar Bar5A;

        public SBar Bar5B;

        public SBar Bar5C;

        public void AnalysePreAction( int waveScenarioNo, TimeSpan period, fxHistoricBarsRepo bars, long selectedBarTime, ElliottWaveEnum waveName, ElliottWaveCycle waveDegree )
        {
            long begin        = -1;
            long wave3B       = -1;
            long wave1        = -1;
            long wave2        = -1;
            long wave3A       = -1;
            long wave3C       = -1;
            long wave4        = -1;

            FibLevelInfo wave2Retracement = default;
            FibLevelInfo wave3AtoWave1    = default;
            FibLevelInfo wave3btowave1    = default;
            FibLevelInfo wave3CtoWave1    = default;

            PooledList< FibLevelInfo > potentialLevels = new PooledList< FibLevelInfo >( );


            begin = _hews.FindBeginOfCurrentTrend( waveScenarioNo, period, selectedBarTime, waveDegree );

            if( begin == -1 )
                return;

            switch ( waveName )
            {                
                case ElliottWaveEnum.Wave1:
                case ElliottWaveEnum.Wave1C:
                {
                    // Should not be here, as if we only have wave 1 and nothing else, we can't do any prediction
                    throw new NotImplementedException( );
                }
                

                case ElliottWaveEnum.Wave2:
                {

                }
                break;

                case ElliottWaveEnum.Wave3:
                case ElliottWaveEnum.Wave3C:
                {
                    wave3C       = selectedBarTime;                    
                    wave3B       = _hews.FindPreviousWaveB( waveScenarioNo, period, selectedBarTime, waveDegree );
                    wave3A       = _hews.FindPreviousWaveA( waveScenarioNo, period, selectedBarTime, waveDegree );
                    wave2        = _hews.FindPreviousWave2( waveScenarioNo, period, selectedBarTime, waveDegree );
                    wave1        = _hews.FindPreviousWave1( waveScenarioNo, period, selectedBarTime, waveDegree );                                                            
                }
                break;

                case ElliottWaveEnum.Wave4:
                {
                    wave4        = selectedBarTime;
                    var wave3    = _hews.FindPreviousWave3Btw( waveScenarioNo,period, begin, selectedBarTime, waveDegree );
                    wave3C       = _hews.FindPreviousWaveCBtw( waveScenarioNo, period, begin, selectedBarTime, waveDegree );

                    if ( wave3 != -1 && wave3C == -1 )
                    {
                        wave3C = wave3;
                    }

                    if ( _hews.NoHigherWaveBetween( waveScenarioNo, wave3C, selectedBarTime, waveDegree ) )
                    {
                        wave3B = _hews.FindPreviousWaveBBtw( waveScenarioNo, period, begin, wave3C, waveDegree );
                        wave3A = _hews.FindPreviousWaveABtw( waveScenarioNo, period, begin, wave3C, waveDegree );

                        wave2 = _hews.FindPreviousWave2Btw( waveScenarioNo, period, begin, wave3C, waveDegree );
                        wave1 = _hews.FindPreviousWave1Btw( waveScenarioNo, period, begin, wave3C, waveDegree );
                    }
                }
                break;
                
                case ElliottWaveEnum.WaveA:
                {

                }
                break;

                case ElliottWaveEnum.WaveB:
                {

                }
                break;
                
                case ElliottWaveEnum.WaveC:
                {

                }
                break;
                
            }

            if ( begin > -1 )
            {
                Bar0 = _bars.GetBarByTime( begin );
            }

            if ( wave1 > -1 )
            {
                Bar1 = _bars.GetBarByTime( wave1 );
            }

            if ( wave2 > -1 )
            {
                Bar2 = _bars.GetBarByTime( wave2 );
            }

            if ( wave3A > -1 )
            {
                Bar3A = _bars.GetBarByTime( wave3A );
            }

            if ( wave3B > -1 )
            {
                Bar3B = _bars.GetBarByTime( wave3B );
            }

            if ( wave3C > -1 )
            {
                Bar3C = _bars.GetBarByTime( wave3C );
            }

            if ( wave4 > -1 )
            {
                Bar4 = _bars.GetBarByTime( wave4 );
            }
            
            var symbolex = SymbolHelper.ToSymbolEx( _bars.Security.ToSecurityId(), period );

            if ( wave1 > -1 )
            {
                _wave2Ret = _hews.GetHewFibTargets( waveScenarioNo, symbolex, wave1, ElliottWaveEnum.Wave1, waveDegree );

                if ( _wave2Ret != null && Bar2 != SBar.EmptySBar)
                {                     
                    if ( _wave2Ret.GetClosestFibLevel( Bar2.PeakTroughValue, out wave2Retracement ) )
                    {
                        Wave2Ret = wave2Retracement.FibPrecentage;
                    }
                }
            }

            if ( wave2  > -1 )
            {
                _wave3Exp = _hews.GetHewFibTargets( waveScenarioNo, symbolex, wave2, ElliottWaveEnum.Wave2, waveDegree );

                if ( _wave3Exp != null )
                {
                    if ( Bar3A != SBar.EmptySBar)
                    {                        
                        if ( _wave3Exp.GetClosestFibLevel( Bar3A.PeakTroughValue, out wave3AtoWave1 ) )
                        {
                            Wave3aProj = wave3AtoWave1.FibPrecentage;
                        }
                    }

                    if ( Bar3B != SBar.EmptySBar)
                    {                         
                        if ( _wave3Exp.GetClosestFibLevel( Bar3B.PeakTroughValue, out wave3btowave1 ) )
                        {
                            Wave3btoWave1 = wave3btowave1.FibPrecentage;
                        }
                    }

                    if ( Bar3C != SBar.EmptySBar)
                    {                         
                        if ( _wave3Exp.GetClosestFibLevel( Bar3C.PeakTroughValue, out wave3CtoWave1 ) )
                        {
                            Wave3cProj = wave3CtoWave1.FibPrecentage;
                        }
                    }                    
                }
            }
            
            if ( wave3A > -1 )
            {
                _wave3BRet = _hews.GetHewFibTargets( waveScenarioNo, symbolex, wave3A, ElliottWaveEnum.WaveA, waveDegree );

                if ( _wave3BRet != null && Bar3B != SBar.EmptySBar)
                {                    
                    if ( _wave3BRet.GetClosestFibLevel( Bar3B.PeakTroughValue, out FibLevelInfo wave3BRetValue ) )
                    {
                        Wave3bRet = wave3BRetValue.FibPrecentage;
                    }

                    if ( _wave3Exp != null )
                    {                        
                        if ( _wave3Exp.GetClosestFibLevel( Bar3B.PeakTroughValue, out FibLevelInfo wave3btoWave1Value ) )
                        {
                            Wave3btoWave1 = wave3btoWave1Value.FibPrecentage;
                        }                        
                    }
                }
            }    
            

            if ( wave3C > -1 )
            {
                _wave4Ret = _hews.GetHewFibTargets( waveScenarioNo, symbolex, wave3C, ElliottWaveEnum.Wave3, waveDegree );

                if ( _wave4Ret != null && Bar4 != SBar.EmptySBar)
                {                    
                    if ( _wave4Ret.GetClosestFibLevel( Bar4.PeakTroughValue, out FibLevelInfo wave4RetValue ) )
                    {
                        Wave4Ret = wave4RetValue.FibPrecentage;
                    }

                    if ( _wave3Exp != null )
                    {                        
                        if ( _wave3Exp.GetClosestFibLevel( Bar4.PeakTroughValue, out FibLevelInfo wave4toWave1Value ) )
                        {
                            Wave4toWave1 = wave4toWave1Value.FibPrecentage;
                        }
                    }
                }
            }

            if ( wave4 > -1 )
            {
                _wave5Exp = _hews.GetHewFibTargets( waveScenarioNo, symbolex, wave4, ElliottWaveEnum.Wave4, waveDegree );                
            }
            

            if ( wave2Retracement != default && wave3AtoWave1 != default && wave3btowave1 != default && wave3CtoWave1 != default)
            {
                Wave3Type = GetWave3Type( ref wave2Retracement, ref wave3AtoWave1, ref wave3btowave1, ref wave3CtoWave1 );
            }
            
            
            if ( wave3CtoWave1 != default && Wave3Type == Wave3Type.UNKNOWN )
            {
                Wave3Type = GetWave3TypeByWave3CTarget( ref wave3CtoWave1 );
            }

            if ( wave3AtoWave1 != default && Wave3Type == Wave3Type.UNKNOWN )
            {
                Wave3Type = GetWave3TypeByWave3ATarget( ref wave3CtoWave1 );
            }


            //PredictTargetsBasedOnWave3Model( waveScenarioNo, period, selectedBarTime, waveName, waveDegree, Wave3Type );


        }

        
        
        

        public Wave3Type GetWave3Type( ref FibLevelInfo wave1Retracement, ref FibLevelInfo wave3AtoWave1, ref FibLevelInfo wave3btowave1, ref FibLevelInfo wave3CtoWave1 )
        {
            if ( wave1Retracement == default || wave3AtoWave1 == default || wave3btowave1 == default || wave3CtoWave1 == default)
            {
                return Wave3Type.UNKNOWN;
            }

            switch ( wave3AtoWave1.FibPrecentage )
            {
                case FibPercentage p when ( p >= FibPercentage.Fib_100 && p < FibPercentage.Fib_114_6 ):
                {
                    if ( wave3CtoWave1.FibPrecentage >= FibPercentage.Fib_176_4 && wave3CtoWave1.FibPrecentage < FibPercentage.Fib_200 )
                    {
                        return Wave3Type.Classic;
                    }
                }
                break;
                
                case FibPercentage.Fib_123_6:
                {
                    if ( wave3CtoWave1.FibPrecentage >= FibPercentage.Fib_176_4 && wave3CtoWave1.FibPrecentage < FibPercentage.Fib_200 )
                    {
                        return Wave3Type.Classic;
                    }
                    else if ( wave3CtoWave1.FibPrecentage >= FibPercentage.Fib_214_6 && wave3CtoWave1.FibPrecentage < FibPercentage.Fib_261_8 )
                    {
                        return Wave3Type.Extended;
                    }
                }
                break;

                case FibPercentage p when ( p > FibPercentage.Fib_123_6 && p < FibPercentage.Fib_176_4 ):
                {
                    if ( wave3CtoWave1.FibPrecentage >= FibPercentage.Fib_214_6 && wave3CtoWave1.FibPrecentage < FibPercentage.Fib_261_8 )
                    {
                        return Wave3Type.Extended;
                    }
                }
                break;

                case FibPercentage p when ( p >= FibPercentage.Fib_176_4 ):
                {
                    if ( wave3CtoWave1.FibPrecentage >= FibPercentage.Fib_314_6 )
                    {
                        return Wave3Type.SuperExtended;
                    }
                }
                break;
            }

            return Wave3Type.UNKNOWN;
        }

        public Wave3Type GetWave3TypeByWave3CTarget( ref FibLevelInfo wave3CtoWave1 )
        {
            if ( wave3CtoWave1 == default)
            {
                return Wave3Type.UNKNOWN;
            }

            if ( wave3CtoWave1.FibPrecentage >= FibPercentage.Fib_176_4 && wave3CtoWave1.FibPrecentage < FibPercentage.Fib_200 )
            {
                return Wave3Type.Classic;
            }
            else if ( wave3CtoWave1.FibPrecentage >= FibPercentage.Fib_214_6 && wave3CtoWave1.FibPrecentage < FibPercentage.Fib_261_8 )
            {
                return Wave3Type.Extended;
            }
            else if ( wave3CtoWave1.FibPrecentage >= FibPercentage.Fib_314_6 )
            {
                return Wave3Type.SuperExtended;
            }            

            return Wave3Type.UNKNOWN;
        }

        public Wave3Type GetWave3TypeByWave3ATarget( ref FibLevelInfo wave3AtoWave1 )
        {
            if ( wave3AtoWave1 == default)
            {
                return Wave3Type.UNKNOWN;
            }

            switch ( wave3AtoWave1.FibPrecentage )
            {
                case FibPercentage p when ( p >= FibPercentage.Fib_100 && p < FibPercentage.Fib_114_6 ):
                {
                    return Wave3Type.Classic;
                }                

                case FibPercentage.Fib_123_6:
                {
                    return Wave3Type.Classic;
                }                

                case FibPercentage p when ( p > FibPercentage.Fib_123_6 && p < FibPercentage.Fib_176_4 ):
                {
                    return Wave3Type.Extended;
                }                

                case FibPercentage p when ( p >= FibPercentage.Fib_176_4 ):
                {
                    return Wave3Type.SuperExtended;
                }                
            }

            return Wave3Type.UNKNOWN;
        }

        public void AnalyseUnfoldingAction( int waveScenarioNo, TimeSpan period, fxHistoricBarsRepo bars, long selectedBarTime, ElliottWaveEnum waveName, ElliottWaveCycle waveDegree )
        {
            _unfoldingWave = new CorrectiveWaveModel( _bars, _hews );

            _unfoldingWave.AnalysePreAction( waveScenarioNo, period, bars, selectedBarTime, waveName, waveDegree - GlobalConstants.OneWaveCycle );

            if ( _unfoldingWave.LargerTargets != null && _unfoldingWave.LargerTargets.Count > 0 )
            {
                var diff           = ( double ) _bars.Security.PriceStep.Value;

                _unfoldingTargets = _unfoldingWave.LargerTargets;

                var largerTargets  = _largerTargets;

                foreach ( FibLevelInfo largerTarget in largerTargets )
                {
                    var index = _unfoldingTargets.FindIndex( x => x.WithinCluster( largerTarget, diff * 4  ) );

                    if ( index > -1 )
                    {
                        var selectedLevel = _unfoldingTargets[ index ];

                        selectedLevel.UpdateAll( largerTarget );                        
                    }
                    else
                    {
                        _unfoldingTargets.Add( largerTarget );
                    }
                }

                _predictedTargets = _unfoldingWave.LargerTargets.OrderByDescending( x => x.OverlappedCount ).ToPooledList( );
            }
        }
    }
    
}
