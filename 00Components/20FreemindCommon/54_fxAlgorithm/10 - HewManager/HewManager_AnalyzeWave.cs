using System;
using fx.Database.Common.DataModel;
using fx.Database.ForexDatabarsDataModel;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System.Linq;
using fx.Definitions;
using fx.Database;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Collections;
using fx.Definitions.Collections;
using StockSharp.BusinessEntities;
using Ecng.Common;
using fx.TimePeriod;
using fx.Collections;
using fx.Bars;
using System.Threading.Tasks;
using fx.Common;

namespace fx.Algorithm
{
    public partial class HewManager
    {
        public void WhereAreWeInWavesCycle( int waveScenarioNo, PooledList<WavePointInfo> allWaves, fxHistoricBarsRepo bars, TimeSpan period, BTreeDictionary<long, WavePointImportance> waveImportance )
        {
            if ( allWaves.Count == 0 )
                return;

            var hews          = GetElliottWavesDictionary( period );
            var descending    = allWaves.OrderByDescending( x => x.BarTime ).ToList();
            var count         = descending.Count;
            var lastWave      = descending[ 0 ];
            var brokenWaves   = new PooledList< DbElliottWave >( );
            var futureTargets = new PooledList< DbElliottWave >( );

            switch ( lastWave.WaveInfo.WaveName )
            {
                case ElliottWaveEnum.WaveA:
                {
                    // Here we are in the middle of Wave B.
                    if ( count > 1 )
                    {
                        var wave024X = descending[ 1 ];
                        var waveA    = lastWave;

                        ref SBar barA = ref bars.GetBarByTime( waveA.BarTime );
                        ref SBar bar024x = ref bars.GetBarByTime( wave024X.BarTime );
                        

                        if ( barA == SBar.EmptySBar || bar024x == SBar.EmptySBar )
                        {
                            break;
                        }

                        var barATime = barA.LinuxTime;
                        var bar024xTime = bar024x.LinuxTime;


                        bool isUp       = ( barA.High > bar024x.High ) && ( barA.Low  > bar024x.Low  );
                        bool isDown     = ( barA.Low  < bar024x.Low  ) && ( barA.High < bar024x.High );
                        var TopButtom   = isUp ? WaveLabelPosition.BOTTOM : WaveLabelPosition.TOP;

                        var oneDegLower = lastWave.WaveInfo.WaveCycle - GlobalConstants.OneWaveCycle;

                        var inBtw       = hews.Where( x => x.Key > bar024xTime && x.Key < barATime && x.Value.WaveLabelPosition == TopButtom ).OrderByDescending( x => x.Key ).ToList();

                        foreach ( var pair in inBtw )
                        {
                            if ( hews.TryGetValue( pair.Key, out DbElliottWave target ) )
                            {
                                ref var hew = ref target.GetWaveFromScenario( waveScenarioNo );
                                var wave = hew.GetHewPointInfoAtCycle( oneDegLower );

                                if ( wave.HasValue )
                                {
                                    var waveName = wave.Value.WaveName;

                                    switch ( waveName )
                                    {
                                        case ElliottWaveEnum.Wave2:
                                        {
                                            var smWave2 = target.BeginTimeUTC;                                            
                                            var info    = GetCorrectionInfo(waveScenarioNo, bars, period, smWave2.ToLinuxTime( ), ElliottWaveEnum.Wave4, oneDegLower );

                                            ref SBar smBar2 = ref bars.GetBarByTime( smWave2 );

                                            if ( smBar2 != SBar.EmptySBar )
                                            {
                                                if ( isDown && !isUp )
                                                {
                                                    if ( bars.WasBrokenUpWard( ref smBar2 ) )
                                                    {
                                                        brokenWaves.Add( target );
                                                    }
                                                    else
                                                    {
                                                        futureTargets.Add( target );
                                                    }
                                                }
                                                else if ( isUp && !isDown )
                                                {
                                                    if ( bars.WasBrokenDownward( ref smBar2 ) )
                                                    {
                                                        brokenWaves.Add( target );
                                                    }
                                                    else
                                                    {
                                                        futureTargets.Add( target );
                                                    }
                                                }
                                            }

                                        }
                                        break;

                                        case ElliottWaveEnum.Wave4:
                                        {
                                            var smWave4 = target.BeginTimeUTC;                                            

                                            var info    = GetCorrectionInfo( waveScenarioNo,bars, period, smWave4.ToLinuxTime( ), ElliottWaveEnum.Wave4, oneDegLower );

                                            ref SBar smBar4 = ref bars.GetBarByTime( smWave4 );

                                            if ( smBar4 != SBar.EmptySBar )                                                
                                            {
                                                if ( isDown && !isUp )
                                                {
                                                    if ( bars.WasBrokenUpWard( ref smBar4 ) )
                                                    {
                                                        brokenWaves.Add( target );
                                                    }
                                                    else
                                                    {
                                                        futureTargets.Add( target );
                                                    }
                                                }
                                                else if ( isUp && !isDown )
                                                {
                                                    if ( bars.WasBrokenDownward( ref smBar4 ) )
                                                    {
                                                        brokenWaves.Add( target );
                                                    }
                                                    else
                                                    {
                                                        futureTargets.Add( target );
                                                    }
                                                }
                                            }
                                        }
                                        break;

                                        case ElliottWaveEnum.WaveB:
                                        {
                                            var smWaveB    = target.BeginTimeUTC;
                                            
                                            var bContainer = WaveBOfWhat( waveScenarioNo, smWaveB.ToLinuxTime(), oneDegLower );
                                            var info       = GetCorrectionInfo( waveScenarioNo, bars, period, smWaveB.ToLinuxTime( ), ElliottWaveEnum.WaveB, oneDegLower );

                                            ref SBar smBarB = ref bars.GetBarByTime( smWaveB );

                                            if ( smBarB != SBar.EmptySBar )                                            
                                            {
                                                if ( isDown && !isUp )
                                                {
                                                    if ( bars.WasBrokenUpWard( ref smBarB ) )
                                                    {


                                                        brokenWaves.Add( target );
                                                    }
                                                    else
                                                    {
                                                        futureTargets.Add( target );
                                                    }
                                                }
                                                else if ( isUp && !isDown )
                                                {
                                                    if ( bars.WasBrokenDownward( ref smBarB ) )
                                                    {
                                                        brokenWaves.Add( target );
                                                    }
                                                    else
                                                    {
                                                        futureTargets.Add( target );
                                                    }
                                                }
                                            }
                                        }
                                        break;

                                        default:
                                        throw new NotImplementedException( );
                                    }
                                }
                            }
                        }


                    }
                }
                break;

                case ElliottWaveEnum.WaveC:
                {
                    if ( count > 2 )
                    {
                        var waveB = descending[ 1 ];
                        var waveA = descending[ 2 ];

                        if ( waveA.WaveInfo.WaveName == ElliottWaveEnum.WaveA && waveB.WaveInfo.WaveName == ElliottWaveEnum.WaveB )
                        {

                            // Here we have a A-B-C, the counter wave, should be either a series of ABCs or impulsive Waves.
                            //![](F84EC1A715B7F472CD60D785814C3DE8.png;;;0.02215,0.01973)

                            ref SBar barC = ref bars.GetBarByTime( lastWave.BarTime );
                            ref SBar barB = ref bars.GetBarByTime( waveB.BarTime );

                            if ( barB == SBar.EmptySBar && barC == SBar.EmptySBar ) return;

                            var barBTime = barB.LinuxTime;
                            var barCTime = barC.LinuxTime;

                            bool isUp       = ( barC.High > barB.High ) && ( barC.Low  > barB.Low  );
                            bool isDown     = ( barC.Low  < barB.Low  ) && ( barC.High < barB.High );
                            var TopButtom   = isUp ? WaveLabelPosition.BOTTOM : WaveLabelPosition.TOP;

                            var oneDegLower = lastWave.WaveInfo.WaveCycle - GlobalConstants.OneWaveCycle;

                            var inBtw       = hews.Where( x => x.Key > barBTime && x.Key < barCTime && x.Value.WaveLabelPosition == TopButtom ).OrderByDescending( x => x.Key ).ToList();

                            foreach ( var pair in inBtw )
                            {
                                if ( hews.TryGetValue( pair.Key, out DbElliottWave target ) )
                                {
                                    ref var hew = ref target.GetWaveFromScenario( waveScenarioNo );

                                    var wave = hew.GetHewPointInfoAtCycle( oneDegLower );

                                    if ( wave.HasValue )
                                    {
                                        var waveName = wave.Value.WaveName;

                                        switch ( waveName )
                                        {
                                            case ElliottWaveEnum.Wave2:
                                            {
                                                var smWave2 = target.BeginTimeUTC;

                                                ref SBar smBar2 = ref bars.GetBarByTime( smWave2 );

                                                if ( smBar2 != SBar.EmptySBar )                                                    
                                                {
                                                    if ( isDown && !isUp )
                                                    {
                                                        if ( bars.WasBrokenUpWard( ref smBar2 ) )
                                                        {
                                                            brokenWaves.Add( target );
                                                        }
                                                        else
                                                        {
                                                            futureTargets.Add( target );
                                                        }
                                                    }
                                                    else if ( isUp && !isDown )
                                                    {
                                                        if ( bars.WasBrokenDownward( ref smBar2 ) )
                                                        {
                                                            brokenWaves.Add( target );
                                                        }
                                                        else
                                                        {
                                                            futureTargets.Add( target );
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                            case ElliottWaveEnum.Wave4:
                                            {
                                                var smWave4 = target.BeginTimeUTC;

                                                ref SBar smBar4 = ref bars.GetBarByTime( smWave4 );

                                                if ( smBar4 != SBar.EmptySBar )                                                    
                                                {
                                                    if ( isDown && !isUp )
                                                    {
                                                        if ( bars.WasBrokenUpWard( ref smBar4 ) )
                                                        {
                                                            brokenWaves.Add( target );
                                                        }
                                                        else
                                                        {
                                                            futureTargets.Add( target );
                                                        }
                                                    }
                                                    else if ( isUp && !isDown )
                                                    {
                                                        if ( bars.WasBrokenDownward( ref smBar4 ) )
                                                        {
                                                            brokenWaves.Add( target );
                                                        }
                                                        else
                                                        {
                                                            futureTargets.Add( target );
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                            case ElliottWaveEnum.WaveB:
                                            {
                                                var smWaveB    = target.BeginTimeUTC;                                                
                                                var bContainer = WaveBOfWhat( waveScenarioNo, smWaveB.ToLinuxTime(), oneDegLower );

                                                ref SBar smBarB = ref bars.GetBarByTime( smWaveB );

                                                if ( smBarB != SBar.EmptySBar )                                                    
                                                {
                                                    if ( isDown && !isUp )
                                                    {
                                                        if ( bars.WasBrokenUpWard( ref smBarB ) )
                                                        {
                                                            var info = GetCorrectionInfo( waveScenarioNo, bars, period, smWaveB.ToLinuxTime( ), ElliottWaveEnum.WaveB, oneDegLower );

                                                            brokenWaves.Add( target );
                                                        }
                                                        else
                                                        {
                                                            futureTargets.Add( target );
                                                        }
                                                    }
                                                    else if ( isUp && !isDown )
                                                    {
                                                        if ( bars.WasBrokenDownward( ref smBarB ) )
                                                        {
                                                            brokenWaves.Add( target );
                                                        }
                                                        else
                                                        {
                                                            futureTargets.Add( target );
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                            default:
                                            throw new NotImplementedException( );
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                break;

            }
        }



        public PooledList<WavePointInfo> GetWavesOfDegree( int waveScenarioNo, TimeSpan period, ElliottWaveCycle cycle, fxHistoricBarsRepo bars )
        {
            PooledList<WavePointInfo> output = new PooledList<WavePointInfo>();

            var hews = GetElliottWavesDictionary(period);

            if ( hews.Count > 0 )
            {
                foreach ( var eWave in hews )
                {
                    var outputWave = hews[eWave.Key];

                    ref var hew = ref outputWave.GetWaveFromScenario( waveScenarioNo );                    

                    var waveInfo = hew.GetHewPointInfoAtCycleOrHigherFirst(cycle);

                    if ( waveInfo.HasValue )
                    {
                        output.Add( new WavePointInfo( eWave.Key, waveInfo.Value ) );
                    }
                }
            }

            return output;
        }


        private CorrectiveWavesInfo GetCorrectionInfo( int waveScenarioNo, fxHistoricBarsRepo bars, TimeSpan period, long waveTime, ElliottWaveEnum waveName, ElliottWaveCycle waveCycle )
        {
            CorrectiveWavesInfo output = null;
            var hews = GetElliottWavesDictionary(period);
            var oneCycleLower = waveCycle - GlobalConstants.OneWaveCycle;

            if ( hews.Count > 0 )
            {
                var previousWaves = hews.GetElementsRightOf( waveTime );

                foreach ( var wave in previousWaves )
                {
                    var hew      = wave.Value.GetWaveFromScenario( waveScenarioNo );
                    var preNames = hew.GetWavesAtCycle( waveCycle );
                    var corrType = WavePattern.UNKNOWN;

                    if ( preNames.Count > 0 )
                    {
                        var prevName = preNames[ 0 ];

                        if ( prevName.IsNextWave( waveName ) )
                        {
                            output           = new CorrectiveWavesInfo( prevName, wave.Key.FromLinuxTime( ), waveName, waveTime.FromLinuxTime( ), waveCycle );
                            var innerWaves   = hews.Where( x => x.Key > wave.Key && x.Key < waveTime ).OrderBy( x => x.Key ).ToList();
                            var lastWaveName = prevName;
                            var lastWaveTime = wave.Key.FromLinuxTime( );

                            int xCount = 0;

                            if ( innerWaves.Count > 0 )
                            {
                                foreach ( var innerWave in innerWaves )
                                {
                                    var innerWaveValue = innerWave.Value.GetWaveFromScenario( waveScenarioNo ).GetFirstHighestWaveInfo();

                                    if ( innerWaveValue.HasValue )
                                    {
                                        var currWaveName  = innerWaveValue.Value.WaveName;
                                        var currWaveTime  = innerWave.Value.WaveDate.Value.UtcDateTime;
                                        var currWaveCycle = innerWaveValue.Value.WaveCycle;
                                        var innerCorrect  = new CorrectiveWavesInfo( lastWaveName, lastWaveTime, currWaveName, currWaveTime, currWaveCycle );

                                        output.AddInnerWaves( innerCorrect );

                                        switch ( currWaveName )
                                        {
                                            case ElliottWaveEnum.WaveTA:
                                            case ElliottWaveEnum.WaveTB:
                                            case ElliottWaveEnum.WaveTC:
                                            case ElliottWaveEnum.WaveTD:
                                            case ElliottWaveEnum.WaveTE:
                                            {
                                                corrType = WavePattern.IrregularTriangle;
                                            }
                                            break;

                                            case ElliottWaveEnum.WaveEFA:
                                            case ElliottWaveEnum.WaveEFB:
                                            case ElliottWaveEnum.WaveEFC:
                                            {
                                                corrType = WavePattern.ExpandedFlat;
                                            }
                                            break;

                                            case ElliottWaveEnum.WaveA:
                                            case ElliottWaveEnum.WaveB:
                                            case ElliottWaveEnum.WaveC:
                                            {
                                                corrType = WavePattern.ZigZag;
                                            }
                                            break;

                                            case ElliottWaveEnum.WaveW:
                                            case ElliottWaveEnum.WaveX:
                                            case ElliottWaveEnum.WaveY:
                                            case ElliottWaveEnum.WaveZ:
                                            {
                                                xCount++;

                                                if ( xCount == 1 )
                                                {
                                                    corrType = WavePattern.DoubleZigZag;
                                                }
                                                else if ( xCount == 2 )
                                                {
                                                    corrType = WavePattern.TripleThree;
                                                }
                                            }
                                            break;
                                        }

                                        lastWaveName = currWaveName;
                                        lastWaveTime = currWaveTime;
                                    }
                                }

                            }
                            else
                            {
                                corrType = DetectWaveFormation( bars, period, wave.Key, waveTime );
                            }

                            output.WavePattern |= corrType;
                        }
                    }
                }
            }

            return output;
        }
        public WavePattern DetectWaveFormation( fxHistoricBarsRepo bars, TimeSpan period, long corrBegin, long corrEnd )
        {
            var output = WavePattern.UNKNOWN;

            ref SBar beginBar = ref bars.GetBarByTime( corrBegin );
            
            if ( beginBar == SBar.EmptySBar )
            {
                return WavePattern.UNKNOWN;
            }
                
            ref SBar endBar = ref bars.GetBarByTime( corrEnd );
            
            if ( endBar == SBar.EmptySBar )
            {
                return WavePattern.UNKNOWN;
            }
          
            bool isUp = ( endBar.High > beginBar.High ) && ( endBar.Low  > beginBar.Low  );

            if ( isUp )
            {
                var result = isZigZagUp( bars, period, corrBegin, corrEnd );
                if ( result.HasValue && result.Value == true )
                {
                    output = GetZigZagTypeUp( bars, period, corrBegin, corrEnd );
                }
            }
            else
            {
                var result = isZigZagDown( bars, period, corrBegin, corrEnd );
                if ( result.HasValue && result.Value == true )
                {
                    output = GetZigZagTypeDown( bars, period, corrBegin, corrEnd );
                }

                result = isExpandedFlatDown( bars, period, corrBegin, corrEnd );
            }

            return output;
        }


        public HewFibGannTargets GetRetracementTargets( int waveScenarioNo, Security symbol, TimeSpan period, long startTime, long endTime, ElliottWaveCycle currentWaveDegree )
        {
            HewFibGannTargets fibTarget = null;

            var symbolex = SymbolHelper.ToSymbolEx( symbol.ToSecurityId(), period );

            fibTarget = new HewFibGannTargets( startTime, symbolex, startTime.ToString( ), _bars.Period.Value );

            var points = GetWavePoints( waveScenarioNo, _bars, _currentActiveTimeFrame, startTime, endTime, currentWaveDegree );

            if ( points != default )
            {
                fibTarget.SetFibonacciRetracment( points.Item1, points.Item2, ElliottWaveEnum.Wave2 );
            }

            return fibTarget;
        }

        public HewFibGannTargets GetRetracementTargets( Security symbol, TimeSpan period, ((DateTime, float) start, (DateTime, float) end) points )
        {
            HewFibGannTargets fibTarget = null;

            var symbolex = SymbolHelper.ToSymbolEx( symbol.ToSecurityId(), period );

            fibTarget = new HewFibGannTargets( points.start.Item1.ToLinuxTime( ), symbolex, points.start.Item1.ToString( ), _bars.Period.Value );

            if ( points != default )
            {
                fibTarget.SetFibonacciRetracment( points.Item1, points.Item2, ElliottWaveEnum.Wave2 );
            }

            return fibTarget;
        }

        public HewFibGannTargets GetTonyProjectionTarget( Security symbol, TimeSpan period, ((DateTime, float) start, (DateTime, float) end) points )
        {
            HewFibGannTargets fibTarget = null;

            var symbolex = SymbolHelper.ToSymbolEx( symbol.ToSecurityId(), period );

            fibTarget = new HewFibGannTargets( points.start.Item1.ToLinuxTime( ), symbolex, points.start.Item1.ToString( ), _bars.Period.Value );

            if ( points != default )
            {
                fibTarget.SetTonyExtension( points, ElliottWaveEnum.Wave4 );
            }

            return fibTarget;
        }

        public HewFibGannTargets GetHewFibTargets( int waveScenarioNo, SymbolEx symbol, TimeSpan period, long barTime, ElliottWaveEnum currentWave, ElliottWaveCycle currentWaveDegree )
        {
            HewFibGannTargets fibTarget = null;

            if ( barTime == -1 )
                return null;

            var nextWave = GetNextWaveOfSameDegreeExcept24( waveScenarioNo, period, barTime, currentWave, currentWaveDegree );

            switch ( currentWave )
            {
                #region Retracement Levels
                // When we select Wave 1, wave 3, and Wave 5, we are looking at the retracement levels
                case ElliottWaveEnum.Wave1:
                case ElliottWaveEnum.Wave1C:
                {
                    var lastWaveStartEnd = GetPoints_Wave0_Wave1C( waveScenarioNo, period, barTime, currentWaveDegree );
                    fibTarget            = new HewFibGannTargets( barTime, symbol, barTime.ToString(), period );
                    var points           = GetImportanceWavePointsForWave2( waveScenarioNo, period, barTime, currentWaveDegree );

                    fibTarget.TargetPoints = points;

                    if ( lastWaveStartEnd != default )
                    {
                        fibTarget.SetFibonacciRetracment( lastWaveStartEnd.Item1, lastWaveStartEnd.Item2, ElliottWaveEnum.Wave2 );

                        if ( nextWave != null )
                        {
                            var nextWaveNameInfo = nextWave.GetWaveFromScenario( waveScenarioNo ).GetHewPointInfoAtCycle( currentWaveDegree );

                            if ( nextWaveNameInfo != null )
                            {
                                if ( nextWave.HasOwingBar() )
                                {
                                    fibTarget.SetEndingIndexTime( nextWave.GetMarginDateTime() );
                                }
                            }
                        }
                    }
                }
                break;

                case ElliottWaveEnum.Wave3:
                case ElliottWaveEnum.Wave3C:
                {
                    var lastWaveStartEnd = GetPoints_Wave2_Wave3C( waveScenarioNo, period, barTime, currentWaveDegree );
                    var wave3bto3C       = GetPoints_Wave3B_Wave3C( waveScenarioNo, period, barTime, currentWaveDegree );

                    ( ( DateTime, float ) wave4, ( DateTime, float ) wave3 )  pt4_3  = GetPoints_smallWave4_Wave3( waveScenarioNo, period, barTime, currentWaveDegree );
                    (( DateTime, float ) wave4B, ( DateTime, float ) wave3 ) pt4B_3 = GetPoints_smallWave4B_Wave3( waveScenarioNo, period, barTime, currentWaveDegree );

                    fibTarget = new HewFibGannTargets( barTime, symbol, barTime.ToString(), _bars.Period.Value );

                    if ( pt4B_3 != default )
                    {
                        fibTarget.SetTonyExtension( pt4B_3, ElliottWaveEnum.Wave3 );
                    }

                    if ( pt4_3 != default )
                    {
                        fibTarget.SetTonyExtension2( pt4_3, ElliottWaveEnum.Wave3 );
                    }

                    if ( wave3bto3C != default )
                    {
                        fibTarget.SetTonyRetracement( wave3bto3C, ElliottWaveEnum.Wave2 );
                    }

                    var points = GetImportanceWavePointsForWave4( waveScenarioNo, period, barTime, currentWaveDegree );

                    fibTarget.TargetPoints = points;

                    if ( lastWaveStartEnd != default )
                    {
                        fibTarget.SetFibonacciRetracment( lastWaveStartEnd.Item1, lastWaveStartEnd.Item2, ElliottWaveEnum.Wave4 );

                        if ( nextWave != null )
                        {
                            var nextWaveNameInfo = nextWave.GetWaveFromScenario( waveScenarioNo ).GetHewPointInfoAtCycle( currentWaveDegree );

                            if ( nextWaveNameInfo != null )
                            {
                                if ( nextWave.HasOwingBar() )
                                {
                                    fibTarget.SetEndingIndexTime( nextWave.GetMarginDateTime() );
                                }
                            }
                        }
                    }
                }
                break;

                case ElliottWaveEnum.Wave5:
                case ElliottWaveEnum.Wave5C:
                {
                    var allWaves = GetAllWavesDescending( waveScenarioNo, period, barTime );
                    fibTarget = new HewFibGannTargets( barTime, symbol, barTime.ToString(), _bars.Period.Value );

                    foreach ( var wave in allWaves )
                    {
                        var wave5Degree = wave.WaveCycle;

                        var lastWaveStartEnd = GetPoints_Wave0_Wave5C( waveScenarioNo, period, barTime, wave5Degree );
                        (( DateTime, float ) wave4, ( DateTime, float ) waveA )  pt4_A  = GetPoints_smallWave4_WaveA( waveScenarioNo, period, barTime, wave5Degree );


                        if ( pt4_A != default && !fibTarget.HasTonyExtensions )
                        {
                            fibTarget.SetTonyExtension( pt4_A, ElliottWaveEnum.WaveA );
                        }
                        else if ( pt4_A != default && !fibTarget.HasTonyExtensions2 )
                        {
                            fibTarget.SetTonyExtension2( pt4_A, ElliottWaveEnum.WaveA );
                        }

                        if ( lastWaveStartEnd != default && !fibTarget.HasRetracement )
                        {
                            fibTarget.SetFibonacciRetracment( lastWaveStartEnd.Item1, lastWaveStartEnd.Item2, currentWave );


                        }
                        else if ( lastWaveStartEnd != default && !fibTarget.HasTonyRetracement )
                        {
                            fibTarget.SetTonyRetracement( lastWaveStartEnd, currentWave );


                        }
                    }

                    fibTarget.RetracementOrProjection = RetraceProjectionType.Retracement;
                }
                break;

                case ElliottWaveEnum.WaveA:
                {
                    var lastWaveStartEnd = GetWaveABeginEndPoints( waveScenarioNo, period, barTime, currentWaveDegree );
                    (( DateTime, float ) wave4, ( DateTime, float ) waveA )  pt4_A  = GetPoints_smallWave4_WaveA( waveScenarioNo, period, barTime, currentWaveDegree );
                    (( DateTime, float ) wave4B, ( DateTime, float ) waveA ) pt4B_A = GetPoints_smallWave4B_WaveA( waveScenarioNo, period, barTime, currentWaveDegree );

                    fibTarget = new HewFibGannTargets( barTime, symbol, barTime.ToString(), _bars.Period.Value );

                    if ( pt4B_A != default )
                    {
                        fibTarget.SetTonyExtension( pt4B_A, ElliottWaveEnum.WaveA );
                    }

                    if ( pt4_A != default )
                    {
                        fibTarget.SetTonyExtension2( pt4_A, ElliottWaveEnum.WaveA );
                    }

                    var points = GetImportanceWavePointsForWaveB( waveScenarioNo, period, barTime, currentWaveDegree );
                    fibTarget.TargetPoints = points;

                    if ( lastWaveStartEnd != default )
                    {
                        fibTarget.SetFibonacciRetracment( lastWaveStartEnd.Item1, lastWaveStartEnd.Item2, ElliottWaveEnum.WaveB );

                        if ( nextWave != null )
                        {
                            var nextWaveNameInfo = nextWave.GetWaveFromScenario( waveScenarioNo ).GetHewPointInfoAtCycle( currentWaveDegree );

                            if ( nextWaveNameInfo != null )
                            {
                                if ( nextWave.HasOwingBar() )
                                {
                                    fibTarget.SetEndingIndexTime( nextWave.GetMarginDateTime() );
                                }
                            }
                        }
                    }
                }
                break;

                case ElliottWaveEnum.WaveEFA:
                {
                    var beginningWave = FindBeginningWaveOfCurrentExpandedFlat(waveScenarioNo, period, barTime, currentWaveDegree );

                    if ( beginningWave != -1 )
                    {
                        var bars = GetDatabarsRepository( period );
                        var points = GetWavePoints( waveScenarioNo, bars, period, beginningWave, barTime, currentWaveDegree );

                        fibTarget = new HewFibGannTargets( barTime, symbol, barTime.ToString(), period );

                        if ( points != default )
                        {
                            fibTarget.SetFibonacciRetracment( points.Item1, points.Item2, ElliottWaveEnum.WaveEFB );

                            if ( nextWave != null )
                            {
                                var nextWaveNameInfo = nextWave.GetWaveFromScenario( waveScenarioNo ).GetHewPointInfoAtCycle( currentWaveDegree );

                                if ( nextWaveNameInfo != null )
                                {
                                    if ( nextWave.HasOwingBar() )
                                    {
                                        fibTarget.SetEndingIndexTime( nextWave.GetMarginDateTime() );
                                    }
                                }
                            }
                        }
                    }
                }
                break;


                case ElliottWaveEnum.WaveTA:
                {
                    var beginningWave = FindBeginningWaveOfCurrentTriangle(waveScenarioNo, period, barTime, currentWaveDegree );

                    if ( beginningWave != -1 )
                    {
                        var bars = GetDatabarsRepository( period );
                        var points = GetWavePoints( waveScenarioNo, bars, period, beginningWave, barTime, currentWaveDegree );

                        fibTarget = new HewFibGannTargets( barTime, symbol, barTime.ToString(), period );

                        if ( points != default )
                        {
                            fibTarget.SetFibonacciRetracment( points.Item1, points.Item2, ElliottWaveEnum.WaveTB );

                            if ( nextWave != null )
                            {
                                var nextWaveNameInfo = nextWave.GetWaveFromScenario( waveScenarioNo ).GetHewPointInfoAtCycle( currentWaveDegree );

                                if ( nextWaveNameInfo != null )
                                {
                                    if ( nextWave.HasOwingBar() )
                                    {
                                        fibTarget.SetEndingIndexTime( nextWave.GetMarginDateTime() );
                                    }
                                }
                            }
                        }
                    }
                }
                break;

                case ElliottWaveEnum.WaveTB:
                {
                    var beginningWave = FindWaveAOfTriangle(waveScenarioNo, period, barTime, currentWaveDegree );

                    if ( beginningWave != -1 )
                    {
                        var bars = GetDatabarsRepository( period );
                        var points = GetWavePoints( waveScenarioNo, bars, period, beginningWave, barTime, currentWaveDegree );

                        fibTarget = new HewFibGannTargets( barTime, symbol, barTime.ToString(), period );

                        if ( points != default )
                        {
                            fibTarget.SetFibonacciRetracment( points.Item1, points.Item2, ElliottWaveEnum.WaveTC );

                            if ( nextWave != null )
                            {
                                var nextWaveNameInfo = nextWave.GetWaveFromScenario( waveScenarioNo ).GetHewPointInfoAtCycle( currentWaveDegree );

                                if ( nextWaveNameInfo != null )
                                {
                                    if ( nextWave.HasOwingBar() )
                                    {
                                        fibTarget.SetEndingIndexTime( nextWave.GetMarginDateTime() );
                                    }
                                }
                            }
                        }
                    }
                }
                break;

                case ElliottWaveEnum.WaveTC:
                {
                    var beginningWave = FindWaveBOfTriangle(waveScenarioNo, period, barTime, currentWaveDegree );

                    if ( beginningWave != -1 )
                    {
                        var bars = GetDatabarsRepository( period );
                        var points = GetWavePoints( waveScenarioNo, bars, period, beginningWave, barTime, currentWaveDegree );

                        fibTarget = new HewFibGannTargets( barTime, symbol, barTime.ToString(), period );

                        if ( points != default )
                        {
                            fibTarget.SetFibonacciRetracment( points.Item1, points.Item2, ElliottWaveEnum.WaveTD );

                            if ( nextWave != null )
                            {
                                var nextWaveNameInfo = nextWave.GetWaveFromScenario( waveScenarioNo ).GetHewPointInfoAtCycle( currentWaveDegree );

                                if ( nextWaveNameInfo != null )
                                {
                                    if ( nextWave.HasOwingBar() )
                                    {
                                        fibTarget.SetEndingIndexTime( nextWave.GetMarginDateTime() );
                                    }
                                }
                            }
                        }
                    }
                }
                break;


                case ElliottWaveEnum.WaveTD:
                {
                    var beginningWave = FindWaveCOfTriangle( waveScenarioNo, period, barTime, currentWaveDegree );

                    if ( beginningWave != -1 )
                    {
                        var bars = GetDatabarsRepository( period );
                        var points = GetWavePoints(waveScenarioNo, bars, period, beginningWave, barTime, currentWaveDegree );

                        fibTarget = new HewFibGannTargets( barTime, symbol, barTime.ToString(), period );

                        if ( points != default )
                        {
                            fibTarget.SetFibonacciRetracment( points.Item1, points.Item2, ElliottWaveEnum.WaveTE );

                            if ( nextWave != null )
                            {
                                var nextWaveNameInfo = nextWave.GetWaveFromScenario( waveScenarioNo ).GetHewPointInfoAtCycle( currentWaveDegree );

                                if ( nextWaveNameInfo != null )
                                {
                                    if ( nextWave.HasOwingBar() )
                                    {
                                        fibTarget.SetEndingIndexTime( nextWave.GetMarginDateTime() );
                                    }
                                }
                            }
                        }
                    }
                }
                break;

                #endregion

                #region Projection Levels

                // When we select Wave 2, wave 4, we are looking at the projections levels

                case ElliottWaveEnum.WaveX:
                {
                    var prevWYTime       = FindPreviousWaveWY( waveScenarioNo, period, barTime, currentWaveDegree );
                    var bars             = GetDatabarsRepository( period );
                    var projectionPoint  = GetCurrentPoint( waveScenarioNo, bars, period, barTime );

                    fibTarget            = new HewFibGannTargets( barTime, symbol, barTime.ToString(), period );

                    var hews             = GetElliottWavesDictionary( period );

                    if ( -1 != prevWYTime )
                    {
                        var waveDbWY = hews[ prevWYTime ];

                        var waveWY = waveDbWY.GetWaveFromScenario( waveScenarioNo ).GetFirstHighestWaveInfo( );

                        switch ( waveWY.Value.WaveName )
                        {
                            case ElliottWaveEnum.WaveW:
                            {
                                var wave0W = GetPoints_Wave0_WaveW_ForWaveX( waveScenarioNo, period, prevWYTime, currentWaveDegree );

                                if ( wave0W != default )
                                {
                                    fibTarget.SetFirstXProjections( wave0W.Item1, wave0W.Item2, projectionPoint );
                                }
                            }
                            break;

                            case ElliottWaveEnum.WaveY:
                            {
                                var waveXY = GetPoints_WaveX_WaveY_ForWaveX( waveScenarioNo, period, prevWYTime, currentWaveDegree );

                                if ( waveXY != default )
                                {
                                    fibTarget.SetFirstXProjections( waveXY.Item1, waveXY.Item2, projectionPoint );
                                }
                            }
                            break;
                        }
                    }
                    else
                    {
                        var lastWaveStartEnd = GetPoints_WaveA_WaveC_ForWaveX(waveScenarioNo, period, barTime, currentWaveDegree );



                        if ( lastWaveStartEnd != default && projectionPoint != default )
                        {
                            fibTarget.SetFirstXProjections( lastWaveStartEnd.Item1, lastWaveStartEnd.Item2, projectionPoint );
                        }
                    }
                }
                break;

                case ElliottWaveEnum.Wave2:
                {
                    (( DateTime, float ) wave0, ( DateTime, float ) wave1C ) pts0_1C = GetPoints_Wave0_Wave1C_ForWave2( waveScenarioNo, period, barTime, currentWaveDegree );
                    (( DateTime, float ) wave1c, ( DateTime, float ) wave2 ) pt1C_2  = GetPoints_Wave1C_Wave12( waveScenarioNo, period, barTime, currentWaveDegree );

                    fibTarget = new HewFibGannTargets( barTime, symbol, barTime.ToString(), period );
                    fibTarget.SetTonyExtension( pt1C_2, ElliottWaveEnum.Wave2 );

                    var bars = GetDatabarsRepository( period );
                    var wave2              = GetCurrentPoint(waveScenarioNo, bars, period, barTime );
                    var points             = GetImportanceWavePointsForWave3( waveScenarioNo, period, barTime, currentWaveDegree );
                    fibTarget.TargetPoints = points;

                    if ( pts0_1C != default && wave2 != default )
                    {
                        fibTarget.SetFibonacciProjections( pts0_1C.wave0, pts0_1C.wave1C, wave2, ElliottWaveEnum.Wave3 );

                        if ( nextWave != null )
                        {
                            var nextWaveNameInfo = nextWave.GetWaveFromScenario( waveScenarioNo ).GetHewPointInfoAtCycle( currentWaveDegree );

                            if ( nextWaveNameInfo != null )
                            {
                                if ( nextWave.HasOwingBar() )
                                {
                                    fibTarget.SetEndingIndexTime( nextWave.GetMarginDateTime() );
                                }
                            }
                        }

                        fibTarget.Analyse3rdWave();
                    }
                }
                break;

                case ElliottWaveEnum.Wave4:
                {
                    var lastWaveStartEnd    = GetPoints_Wave0_Wave3C_ForWave4( waveScenarioNo, period, barTime, currentWaveDegree );
                    var wave3C_wave4        = GetPoints_Wave3C_Wave4( waveScenarioNo, period, barTime, currentWaveDegree );
                    fibTarget               = new HewFibGannTargets( barTime, symbol, barTime.ToString(), period );
                    var bars                = GetDatabarsRepository( period );
                    var projectionPoint     = GetCurrentPoint(waveScenarioNo, bars, period,  barTime );

                    fibTarget.SetTonyExtension( wave3C_wave4, ElliottWaveEnum.Wave4 );

                    if ( lastWaveStartEnd != default && projectionPoint != default )
                    {
                        fibTarget.SetFibonacciProjections( lastWaveStartEnd.Item1, lastWaveStartEnd.Item2, projectionPoint, ElliottWaveEnum.Wave5 );

                        if ( nextWave != null )
                        {
                            var nextWaveNameInfo = nextWave.GetWaveFromScenario( waveScenarioNo ).GetHewPointInfoAtCycle( currentWaveDegree );

                            if ( nextWaveNameInfo != null )
                            {
                                if ( nextWave.HasOwingBar() )
                                {
                                    fibTarget.SetEndingIndexTime( nextWave.GetMarginDateTime() );
                                }
                            }
                        }
                    }
                }
                break;

                // The highlight bar is the Wave B, it can be wave B of different Wave, like Wave 1, Wave 3, Wave 5 or some other correction wave.

                case ElliottWaveEnum.WaveB:
                {
                    var lastWaveStartEnd    = GetPoints_Wave024X_WaveA( waveScenarioNo, period, barTime, currentWaveDegree );
                    var waveA_waveB         = GetPoints_WaveA_WaveB( waveScenarioNo, period, barTime, currentWaveDegree );
                    var waveBContainingWave = WaveBOfWhat( waveScenarioNo, period, barTime, currentWaveDegree );
                    var bars = GetDatabarsRepository( period );
                    var projectionPoint     = GetCurrentPoint(waveScenarioNo, bars, period, barTime );
                    var points              = GetImportanceWavePointsForWaveC( waveScenarioNo, period, barTime, currentWaveDegree );

                    fibTarget = new HewFibGannTargets( barTime, symbol, barTime.ToString(), _bars.Period.Value );
                    fibTarget.SetTonyExtension( waveA_waveB, ElliottWaveEnum.WaveB );
                    fibTarget.TargetPoints = points;

                    if ( lastWaveStartEnd != default && projectionPoint != default && waveBContainingWave != ElliottWaveEnum.NONE )
                    {
                        fibTarget.SetWaveCProjections( lastWaveStartEnd.Item1, lastWaveStartEnd.Item2, projectionPoint, waveBContainingWave );

                        if ( nextWave != null )
                        {
                            var nextWaveNameInfo = nextWave.GetWaveFromScenario( waveScenarioNo ).GetHewPointInfoAtCycle( currentWaveDegree );

                            if ( nextWaveNameInfo != null )
                            {
                                if ( nextWave.HasOwingBar() )
                                {
                                    fibTarget.SetEndingIndexTime( nextWave.GetMarginDateTime() );
                                }
                            }
                        }
                    }
                }
                break;

                case ElliottWaveEnum.WaveC:
                {
                    var lastWaveStartEnd = GetPoints_Wave0X_WaveC( waveScenarioNo, period, barTime, currentWaveDegree );

                    fibTarget = new HewFibGannTargets( barTime, symbol, barTime.ToString(), period );

                    if ( lastWaveStartEnd != default )
                    {
                        fibTarget.SetFibonacciRetracment( lastWaveStartEnd.Item1, lastWaveStartEnd.Item2, ElliottWaveEnum.WaveB );

                        if ( nextWave != null )
                        {
                            var nextWaveNameInfo = nextWave.GetWaveFromScenario( waveScenarioNo ).GetHewPointInfoAtCycle( currentWaveDegree );

                            if ( nextWaveNameInfo != null )
                            {
                                if ( nextWave.HasOwingBar() )
                                {
                                    fibTarget.SetEndingIndexTime( nextWave.GetMarginDateTime() );
                                }
                            }
                        }
                    }
                }
                break;

                case ElliottWaveEnum.WaveEFB:
                {
                    ((DateTime, float) EFA, (DateTime, float) EFB) wavePointEFA = GetPoints_WaveEFA( waveScenarioNo, period, barTime, currentWaveDegree );
                    ((DateTime, float) wave0, (DateTime, float) EFA ) beginningWave = GetPoints_BeginningOfExpandedFlat( waveScenarioNo, period, barTime, currentWaveDegree );


                    fibTarget = new HewFibGannTargets( barTime, symbol, barTime.ToString(), period );

                    var bars = GetDatabarsRepository( period );
                    var projectionPoint     = GetCurrentPoint(waveScenarioNo, bars, period, barTime );
                    

                    if ( wavePointEFA != default && projectionPoint != default && beginningWave != default )
                    {
                        fibTarget.SetWaveCProjections( beginningWave.wave0, wavePointEFA.EFA, projectionPoint, ElliottWaveEnum.WaveEFC );

                        if ( nextWave != null )
                        {
                            var nextWaveNameInfo = nextWave.GetWaveFromScenario( waveScenarioNo ).GetHewPointInfoAtCycle( currentWaveDegree );

                            if ( nextWaveNameInfo != null )
                            {
                                if ( nextWave.HasOwingBar() )
                                {
                                    fibTarget.SetEndingIndexTime( nextWave.GetMarginDateTime() );
                                }
                            }
                        }
                    }
                }
                break;
                #endregion
            }

            //if ( fibTarget  != null && fibTarget.isValid() )
            //{
            //    _waveFibTargets.Add( barTime, fibTarget );
            //}


            return fibTarget;
        }


        public HewFibGannTargets GetHewFibTargets( int waveScenarioNo, SymbolEx symbol, long barTime, ElliottWaveEnum currentWave, ElliottWaveCycle currentWaveDegree )
        {
            HewFibGannTargets fibTarget = null;

            if ( barTime == -1 )
                return null;

            var nextWave = GetNextWaveOfSameDegreeExcept24( waveScenarioNo, barTime, currentWave, currentWaveDegree );

            switch ( currentWave )
            {
                #region Retracement Levels
                // When we select Wave 1, wave 3, and Wave 5, we are looking at the retracement levels
                case ElliottWaveEnum.Wave1:
                case ElliottWaveEnum.Wave1C:
                {
                    var lastWaveStartEnd   = GetPoints_Wave0_Wave1C( waveScenarioNo, barTime, currentWaveDegree );
                    fibTarget              = new HewFibGannTargets( barTime, symbol, barTime.ToString( ), _bars.Period.Value );
                    var points             = GetImportanceWavePointsForWave2( waveScenarioNo, barTime, currentWaveDegree );

                    fibTarget.TargetPoints = points;

                    if ( lastWaveStartEnd != default )
                    {
                        fibTarget.SetFibonacciRetracment( lastWaveStartEnd.Item1, lastWaveStartEnd.Item2, ElliottWaveEnum.Wave2 );

                        if ( nextWave != null )
                        {
                            var nextWaveNameInfo = nextWave.GetWaveFromScenario( waveScenarioNo ).GetHewPointInfoAtCycle( currentWaveDegree );

                            if ( nextWaveNameInfo != null )
                            {
                                if ( nextWave.HasOwingBar() )
                                {                                    
                                    fibTarget.SetEndingIndexTime( nextWave.GetMarginDateTime() );
                                }
                            }
                        }
                    }
                }
                break;

                case ElliottWaveEnum.Wave3:
                case ElliottWaveEnum.Wave3C:
                {
                    var lastWaveStartEnd = GetPoints_Wave2_Wave3C( waveScenarioNo, barTime, currentWaveDegree );
                    var wave3bto3C       = GetPoints_Wave3B_Wave3C( waveScenarioNo, barTime, currentWaveDegree );

                    ( ( DateTime, float ) wave4, ( DateTime, float ) wave3 )  pt4_3  = GetPoints_smallWave4_Wave3( waveScenarioNo, barTime, currentWaveDegree );
                    (( DateTime, float ) wave4B, ( DateTime, float ) wave3 ) pt4B_3 = GetPoints_smallWave4B_Wave3( waveScenarioNo, barTime, currentWaveDegree );

                    fibTarget = new HewFibGannTargets( barTime, symbol, barTime.ToString( ), _bars.Period.Value );

                    if ( pt4B_3 != default )
                    {
                        fibTarget.SetTonyExtension( pt4B_3, ElliottWaveEnum.Wave3 );
                    }

                    if ( pt4_3 != default )
                    {
                        fibTarget.SetTonyExtension2( pt4_3, ElliottWaveEnum.Wave3 );
                    }

                    if ( wave3bto3C != default )
                    {
                        fibTarget.SetTonyRetracement( wave3bto3C, ElliottWaveEnum.Wave2 );
                    }

                    var points = GetImportanceWavePointsForWave4( waveScenarioNo, barTime, currentWaveDegree );

                    fibTarget.TargetPoints = points;

                    if ( lastWaveStartEnd != default )
                    {
                        fibTarget.SetFibonacciRetracment( lastWaveStartEnd.Item1, lastWaveStartEnd.Item2, ElliottWaveEnum.Wave4 );

                        if ( nextWave != null )
                        {
                            var nextWaveNameInfo = nextWave.GetWaveFromScenario( waveScenarioNo ).GetHewPointInfoAtCycle( currentWaveDegree );

                            if ( nextWaveNameInfo != null )
                            {
                                if ( nextWave.HasOwingBar() )
                                {
                                    fibTarget.SetEndingIndexTime( nextWave.GetMarginDateTime() );
                                }                                
                            }
                        }
                    }
                }
                break;

                case ElliottWaveEnum.Wave5:
                case ElliottWaveEnum.Wave5C:
                {
                    var allWaves = GetAllWavesDescending( waveScenarioNo, barTime );
                    fibTarget = new HewFibGannTargets( barTime, symbol, barTime.ToString(), _bars.Period.Value );

                    foreach ( var wave in allWaves )
                    {
                        var wave5Degree = wave.WaveCycle;

                        var lastWaveStartEnd = GetPoints_Wave0_Wave5C( waveScenarioNo, barTime, wave5Degree );
                        (( DateTime, float ) wave4, ( DateTime, float ) waveA )  pt4_A  = GetPoints_smallWave4_WaveA( waveScenarioNo, barTime, wave5Degree );


                        if ( pt4_A != default && !fibTarget.HasTonyExtensions )
                        {
                            fibTarget.SetTonyExtension( pt4_A, ElliottWaveEnum.WaveA );
                        }
                        else if ( pt4_A != default && !fibTarget.HasTonyExtensions2 )
                        {
                            fibTarget.SetTonyExtension2( pt4_A, ElliottWaveEnum.WaveA );
                        }

                        if ( lastWaveStartEnd != default && ! fibTarget.HasRetracement )
                        {
                            fibTarget.SetFibonacciRetracment( lastWaveStartEnd.Item1, lastWaveStartEnd.Item2, currentWave );

                            
                        }
                        else if ( lastWaveStartEnd != default && !fibTarget.HasTonyRetracement )
                        {
                            fibTarget.SetTonyRetracement( lastWaveStartEnd, currentWave );

                            
                        }
                    }

                    fibTarget.RetracementOrProjection = RetraceProjectionType.Retracement;
                }
                break;

                case ElliottWaveEnum.WaveA:
                {
                    var lastWaveStartEnd = GetWaveABeginEndPoints( waveScenarioNo, barTime, currentWaveDegree );
                    (( DateTime, float ) wave4, ( DateTime, float ) waveA )  pt4_A  = GetPoints_smallWave4_WaveA( waveScenarioNo, barTime, currentWaveDegree );
                    (( DateTime, float ) wave4B, ( DateTime, float ) waveA ) pt4B_A = GetPoints_smallWave4B_WaveA( waveScenarioNo, barTime, currentWaveDegree );

                    fibTarget = new HewFibGannTargets( barTime, symbol, barTime.ToString( ), _bars.Period.Value );

                    if ( pt4B_A != default )
                    {
                        fibTarget.SetTonyExtension( pt4B_A, ElliottWaveEnum.WaveA );
                    }

                    if ( pt4_A != default )
                    {
                        fibTarget.SetTonyExtension2( pt4_A, ElliottWaveEnum.WaveA );
                    }

                    var points = GetImportanceWavePointsForWaveB( waveScenarioNo, barTime, currentWaveDegree );
                    fibTarget.TargetPoints = points;

                    if ( lastWaveStartEnd != default )
                    {
                        fibTarget.SetFibonacciRetracment( lastWaveStartEnd.Item1, lastWaveStartEnd.Item2, ElliottWaveEnum.WaveB );

                        if ( nextWave != null )
                        {
                            var nextWaveNameInfo = nextWave.GetWaveFromScenario( waveScenarioNo ).GetHewPointInfoAtCycle( currentWaveDegree );

                            if ( nextWaveNameInfo != null )
                            {
                                if ( nextWave.HasOwingBar() )
                                {
                                    fibTarget.SetEndingIndexTime( nextWave.GetMarginDateTime() );
                                }                                
                            }
                        }
                    }
                }
                break;

                case ElliottWaveEnum.WaveEFA:
                {
                    var beginningWave = FindBeginningWaveOfCurrentExpandedFlat(waveScenarioNo, barTime, currentWaveDegree );

                    if ( beginningWave != -1 )
                    {
                        var points = GetWavePoints( waveScenarioNo, beginningWave, barTime, currentWaveDegree );

                        fibTarget = new HewFibGannTargets( barTime, symbol, barTime.ToString( ), _bars.Period.Value );

                        if ( points != default )
                        {
                            fibTarget.SetFibonacciRetracment( points.Item1, points.Item2, ElliottWaveEnum.WaveEFB );

                            if ( nextWave != null )
                            {
                                var nextWaveNameInfo = nextWave.GetWaveFromScenario( waveScenarioNo ).GetHewPointInfoAtCycle( currentWaveDegree );
                                
                                if ( nextWaveNameInfo != null )
                                {
                                    if ( nextWave.HasOwingBar() )
                                    {
                                        fibTarget.SetEndingIndexTime( nextWave.GetMarginDateTime() );
                                    }
                                }
                            }
                        }
                    }
                }
                break;


                case ElliottWaveEnum.WaveTA:
                {
                    var beginningWave = FindBeginningWaveOfCurrentTriangle(waveScenarioNo, barTime, currentWaveDegree );

                    if ( beginningWave != -1 )
                    {
                        var points = GetWavePoints( waveScenarioNo, beginningWave, barTime, currentWaveDegree );

                        fibTarget = new HewFibGannTargets( barTime, symbol, barTime.ToString( ), _bars.Period.Value );

                        if ( points != default )
                        {
                            fibTarget.SetFibonacciRetracment( points.Item1, points.Item2, ElliottWaveEnum.WaveTB );

                            if ( nextWave != null )
                            {
                                var nextWaveNameInfo = nextWave.GetWaveFromScenario( waveScenarioNo ).GetHewPointInfoAtCycle( currentWaveDegree );

                                if ( nextWaveNameInfo != null )
                                {
                                    if ( nextWave.HasOwingBar() )
                                    {
                                        fibTarget.SetEndingIndexTime( nextWave.GetMarginDateTime() );
                                    }
                                }
                            }
                        }
                    }
                }
                break;

                case ElliottWaveEnum.WaveTB:
                {
                    var beginningWave = FindWaveAOfTriangle(waveScenarioNo, barTime, currentWaveDegree );

                    if ( beginningWave != -1 )
                    {
                        var points = GetWavePoints( waveScenarioNo, beginningWave, barTime, currentWaveDegree );

                        fibTarget = new HewFibGannTargets( barTime, symbol, barTime.ToString( ), _bars.Period.Value );

                        if ( points != default )
                        {
                            fibTarget.SetFibonacciRetracment( points.Item1, points.Item2, ElliottWaveEnum.WaveTC );

                            if ( nextWave != null )
                            {
                                var nextWaveNameInfo = nextWave.GetWaveFromScenario( waveScenarioNo ).GetHewPointInfoAtCycle( currentWaveDegree );

                                if ( nextWaveNameInfo != null )
                                {
                                    if ( nextWave.HasOwingBar() )
                                    {
                                        fibTarget.SetEndingIndexTime( nextWave.GetMarginDateTime() );
                                    }
                                }
                            }
                        }
                    }
                }
                break;

                case ElliottWaveEnum.WaveTC:
                {
                    var beginningWave = FindWaveBOfTriangle(waveScenarioNo, barTime, currentWaveDegree );

                    if ( beginningWave != -1 )
                    {
                        var points = GetWavePoints( waveScenarioNo, beginningWave, barTime, currentWaveDegree );

                        fibTarget = new HewFibGannTargets( barTime, symbol, barTime.ToString( ), _bars.Period.Value );

                        if ( points != default )
                        {
                            fibTarget.SetFibonacciRetracment( points.Item1, points.Item2, ElliottWaveEnum.WaveTD );

                            if ( nextWave != null )
                            {
                                var nextWaveNameInfo = nextWave.GetWaveFromScenario( waveScenarioNo ).GetHewPointInfoAtCycle( currentWaveDegree );

                                if ( nextWaveNameInfo != null )
                                {
                                    if ( nextWave.HasOwingBar() )
                                    {
                                        fibTarget.SetEndingIndexTime( nextWave.GetMarginDateTime() );
                                    }
                                }
                            }
                        }
                    }
                }
                break;


                case ElliottWaveEnum.WaveTD:
                {
                    var beginningWave = FindWaveCOfTriangle( waveScenarioNo,barTime, currentWaveDegree );

                    if ( beginningWave != -1 )
                    {
                        var points = GetWavePoints(waveScenarioNo,  beginningWave, barTime, currentWaveDegree );

                        fibTarget = new HewFibGannTargets( barTime, symbol, barTime.ToString( ), _bars.Period.Value );

                        if ( points != default )
                        {
                            fibTarget.SetFibonacciRetracment( points.Item1, points.Item2, ElliottWaveEnum.WaveTE );

                            if ( nextWave != null )
                            {
                                var nextWaveNameInfo = nextWave.GetWaveFromScenario( waveScenarioNo ).GetHewPointInfoAtCycle( currentWaveDegree );

                                if ( nextWaveNameInfo != null )
                                {
                                    if ( nextWave.HasOwingBar() )
                                    {
                                        fibTarget.SetEndingIndexTime( nextWave.GetMarginDateTime() );
                                    }
                                }
                            }
                        }
                    }
                }
                break;

                #endregion

                #region Projection Levels

                // When we select Wave 2, wave 4, we are looking at the projections levels

                case ElliottWaveEnum.WaveX:
                {
                    var prevWYTime = FindPreviousWaveWY(waveScenarioNo, barTime, currentWaveDegree );

                    var projectionPoint  = GetCurrentPoint( waveScenarioNo, barTime );

                    fibTarget = new HewFibGannTargets( barTime, symbol, barTime.ToString( ), _bars.Period.Value );

                    if ( -1 != prevWYTime )
                    {
                        var waveDbWY = _hews[ prevWYTime ];

                        var waveWY = waveDbWY.GetWaveFromScenario( waveScenarioNo ).GetFirstHighestWaveInfo( );

                        switch ( waveWY.Value.WaveName )
                        {
                            case ElliottWaveEnum.WaveW:
                            {
                                var wave0W = GetPoints_Wave0_WaveW_ForWaveX( waveScenarioNo, prevWYTime, currentWaveDegree );

                                if ( wave0W != default )
                                {
                                    fibTarget.SetFirstXProjections( wave0W.Item1, wave0W.Item2, projectionPoint );
                                }
                            }
                            break;

                            case ElliottWaveEnum.WaveY:
                            {
                                var waveXY = GetPoints_WaveX_WaveY_ForWaveX( waveScenarioNo, prevWYTime, currentWaveDegree );

                                if ( waveXY != default )
                                {
                                    fibTarget.SetFirstXProjections( waveXY.Item1, waveXY.Item2, projectionPoint );
                                }
                            }
                            break;
                        }
                    }
                    else
                    {
                        var lastWaveStartEnd = GetPoints_WaveA_WaveC_ForWaveX(waveScenarioNo,  barTime, currentWaveDegree );



                        if ( lastWaveStartEnd != default && projectionPoint != default )
                        {
                            fibTarget.SetFirstXProjections( lastWaveStartEnd.Item1, lastWaveStartEnd.Item2, projectionPoint );
                        }
                    }
                }
                break;

                case ElliottWaveEnum.Wave2:
                {
                    (( DateTime, float ) wave0, ( DateTime, float ) wave1C ) pts0_1C = GetPoints_Wave0_Wave1C_ForWave2( waveScenarioNo, barTime, currentWaveDegree );
                    (( DateTime, float ) wave1c, ( DateTime, float ) wave2 ) pt1C_2  = GetPoints_Wave1C_Wave12( waveScenarioNo, barTime, currentWaveDegree );

                    fibTarget = new HewFibGannTargets( barTime, symbol, barTime.ToString( ), _bars.Period.Value );
                    fibTarget.SetTonyExtension( pt1C_2, ElliottWaveEnum.Wave2 );
                    var wave2              = GetCurrentPoint(waveScenarioNo,  barTime );
                    var points             = GetImportanceWavePointsForWave3( waveScenarioNo, barTime, currentWaveDegree );
                    fibTarget.TargetPoints = points;

                    if ( pts0_1C != default && wave2 != default )
                    {
                        fibTarget.SetFibonacciProjections( pts0_1C.wave0, pts0_1C.wave1C, wave2, ElliottWaveEnum.Wave3 );

                        if ( nextWave != null )
                        {
                            var nextWaveNameInfo = nextWave.GetWaveFromScenario( waveScenarioNo ).GetHewPointInfoAtCycle( currentWaveDegree );

                            if ( nextWaveNameInfo != null )
                            {
                                if ( nextWave.HasOwingBar() )
                                {
                                    fibTarget.SetEndingIndexTime( nextWave.GetMarginDateTime() );
                                }
                            }
                        }

                        fibTarget.Analyse3rdWave( );
                    }
                }
                break;

                case ElliottWaveEnum.Wave4:
                {
                    var lastWaveStartEnd    = GetPoints_Wave0_Wave3C_ForWave4( waveScenarioNo, barTime, currentWaveDegree );
                    var wave3C_wave4        = GetPoints_Wave3C_Wave4( waveScenarioNo, barTime, currentWaveDegree );
                    fibTarget = new HewFibGannTargets( barTime, symbol, barTime.ToString( ), _bars.Period.Value );
                    var projectionPoint     = GetCurrentPoint(waveScenarioNo,  barTime );

                    fibTarget.SetTonyExtension( wave3C_wave4, ElliottWaveEnum.Wave4 );

                    if ( lastWaveStartEnd != default && projectionPoint != default )
                    {
                        fibTarget.SetFibonacciProjections( lastWaveStartEnd.Item1, lastWaveStartEnd.Item2, projectionPoint, ElliottWaveEnum.Wave5 );

                        if ( nextWave != null )
                        {
                            var nextWaveNameInfo = nextWave.GetWaveFromScenario( waveScenarioNo ).GetHewPointInfoAtCycle( currentWaveDegree );

                            if ( nextWaveNameInfo != null )
                            {
                                if ( nextWave.HasOwingBar() )
                                {
                                    fibTarget.SetEndingIndexTime( nextWave.GetMarginDateTime() );
                                }
                            }
                        }
                    }
                }
                break;

                // The highlight bar is the Wave B, it can be wave B of different Wave, like Wave 1, Wave 3, Wave 5 or some other correction wave.

                case ElliottWaveEnum.WaveB:
                {
                    var lastWaveStartEnd    = GetPoints_Wave024X_WaveA( waveScenarioNo, barTime, currentWaveDegree );
                    var waveA_waveB         = GetPoints_WaveA_WaveB( waveScenarioNo, barTime, currentWaveDegree );
                    var waveBContainingWave = WaveBOfWhat( waveScenarioNo, barTime, currentWaveDegree );
                    var projectionPoint     = GetCurrentPoint(waveScenarioNo,  barTime );
                    var points              = GetImportanceWavePointsForWaveC( waveScenarioNo, barTime, currentWaveDegree );

                    fibTarget = new HewFibGannTargets( barTime, symbol, barTime.ToString( ), _bars.Period.Value );
                    fibTarget.SetTonyExtension( waveA_waveB, ElliottWaveEnum.WaveB );
                    fibTarget.TargetPoints = points;

                    if ( lastWaveStartEnd != default && projectionPoint != default && waveBContainingWave != ElliottWaveEnum.NONE )
                    {
                        fibTarget.SetWaveCProjections( lastWaveStartEnd.Item1, lastWaveStartEnd.Item2, projectionPoint, waveBContainingWave );

                        if ( nextWave != null )
                        {
                            var nextWaveNameInfo = nextWave.GetWaveFromScenario( waveScenarioNo ).GetHewPointInfoAtCycle( currentWaveDegree );

                            if ( nextWaveNameInfo != null )
                            {
                                if ( nextWave.HasOwingBar() )
                                {
                                    fibTarget.SetEndingIndexTime( nextWave.GetMarginDateTime() );
                                }
                            }
                        }
                    }
                }
                break;

                case ElliottWaveEnum.WaveC:
                {
                    var lastWaveStartEnd = GetPoints_Wave0X_WaveC( waveScenarioNo, barTime, currentWaveDegree );

                    fibTarget = new HewFibGannTargets( barTime, symbol, barTime.ToString( ), _bars.Period.Value );

                    if ( lastWaveStartEnd != default )
                    {
                        fibTarget.SetFibonacciRetracment( lastWaveStartEnd.Item1, lastWaveStartEnd.Item2, ElliottWaveEnum.WaveB );

                        if ( nextWave != null )
                        {
                            var nextWaveNameInfo = nextWave.GetWaveFromScenario( waveScenarioNo ).GetHewPointInfoAtCycle( currentWaveDegree );

                            if ( nextWaveNameInfo != null )
                            {
                                if ( nextWave.HasOwingBar() )
                                {
                                    fibTarget.SetEndingIndexTime( nextWave.GetMarginDateTime() );
                                }
                            }
                        }
                    }
                }
                break;

                case ElliottWaveEnum.WaveEFB:
                {
                    ((DateTime, float) EFA, (DateTime, float) EFB) wavePointEFA = GetPoints_WaveEFA( waveScenarioNo, barTime, currentWaveDegree );
                    ((DateTime, float) wave0, (DateTime, float) EFA ) beginningWave = GetPoints_BeginningOfExpandedFlat( waveScenarioNo, barTime, currentWaveDegree );


                    fibTarget = new HewFibGannTargets( barTime, symbol, barTime.ToString( ), _bars.Period.Value );
                    var projectionPoint = GetCurrentPoint( waveScenarioNo, barTime );


                    if ( wavePointEFA != default && projectionPoint != default && beginningWave != default )
                    {
                        fibTarget.SetWaveCProjections( beginningWave.wave0, wavePointEFA.EFA, projectionPoint, ElliottWaveEnum.WaveEFC );

                        if ( nextWave != null )
                        {
                            var nextWaveNameInfo = nextWave.GetWaveFromScenario( waveScenarioNo ).GetHewPointInfoAtCycle( currentWaveDegree );

                            if ( nextWaveNameInfo != null )
                            {
                                if ( nextWave.HasOwingBar() )
                                {
                                    fibTarget.SetEndingIndexTime( nextWave.GetMarginDateTime() );
                                }
                            }
                        }
                    }
                }
                break;
                #endregion
            }

            //if ( fibTarget  != null && fibTarget.isValid() )
            //{
            //    _waveFibTargets.Add( barTime, fibTarget );
            //}


            return fibTarget;
        }

        public PooledList<WavePointInfo> GetWavesOfHighestDegree( int waveScenarioNo, TimeSpan period )
        {
            var output = new PooledList< WavePointInfo >( );
            var hews   = GetElliottWavesDictionary( period );

            ElliottWaveCycle highestCycle = ElliottWaveCycle.UNKNOWN;

            if ( hews.Count > 0 )
            {
                foreach ( var eWave in hews )
                {
                    ref var hew = ref eWave.Value.GetWaveFromScenario( waveScenarioNo );

                    var highestWave = hew.GetLastHighestWaveDegree( );

                    if ( highestWave.HasValue )
                    {
                        if ( highestWave.Value.WaveCycle > highestCycle )
                        {
                            highestCycle = highestWave.Value.WaveCycle;
                        }
                    }
                }
            }

            if ( highestCycle != ElliottWaveCycle.UNKNOWN )
            {
                foreach ( var eWave in hews )
                {
                    ref var hew = ref eWave.Value.GetWaveFromScenario( waveScenarioNo );

                    var lastWave = hew.GetHewPointInfoAtCycleOrHigherLast( highestCycle );

                    if ( lastWave != null )
                    {
                        output.Add( new WavePointInfo( eWave.Key, lastWave.Value ) );
                    }
                }
            }

            return output;
        }

        public void AnalyseWaveTargetsFromHere( PooledList<WavePointInfo> highestWaves )
        {
            long latestTime = -1;
            WavePointInfo? latestWave = null;

            foreach ( WavePointInfo wave in highestWaves )
            {
                if ( wave.LinuxTime > -1 )
                {
                    latestTime = wave.LinuxTime;
                    latestWave = wave;
                }
            }

            if ( latestWave.HasValue )
            {
                var startingPt = latestWave.Value;
                var waveTime   = startingPt.BarTime;
                var waveName   = startingPt.WaveInfo.WaveName;
                var waveCycle  = startingPt.WaveInfo.WaveCycle;

                var nextWave  = harmonicEWave.GetNextWave( waveName );

                switch ( nextWave )
                {
                    case ElliottWaveEnum.NONE:
                    {

                    }
                    break;

                    case ElliottWaveEnum.Wave1:
                    {

                    }
                    break;
                    case ElliottWaveEnum.Wave1C:
                    {

                    }
                    break;
                    case ElliottWaveEnum.Wave2:
                    {

                    }
                    break;
                    case ElliottWaveEnum.Wave3:
                    {

                    }
                    break;

                    case ElliottWaveEnum.Wave3C:
                    {

                    }
                    break;

                    case ElliottWaveEnum.Wave4:
                    {

                    }
                    break;

                    case ElliottWaveEnum.Wave5:
                    {
                        //GetWave4RoadMap( )
                    }
                    break;

                    case ElliottWaveEnum.Wave5C:
                    {

                    }
                    break;

                    case ElliottWaveEnum.WaveTA:
                    {

                    }
                    break;

                    case ElliottWaveEnum.WaveTB:
                    {

                    }
                    break;

                    case ElliottWaveEnum.WaveTC:
                    {

                    }
                    break;

                    case ElliottWaveEnum.WaveTD:
                    {

                    }
                    break;

                    case ElliottWaveEnum.WaveTE:
                    {

                    }
                    break;

                    case ElliottWaveEnum.WaveEFA:
                    {

                    }
                    break;

                    case ElliottWaveEnum.WaveEFB:
                    {

                    }
                    break;

                    case ElliottWaveEnum.WaveEFC:
                    {

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

                    case ElliottWaveEnum.WaveX:
                    {

                    }
                    break;
                }

            }



        }


        /* -------------------------------------------------------------------------------------------------------------------------------------------
         * 
         *  I will do wave target prediction based on Hew theory and Pivot Pionts targets.
         * 
         * ------------------------------------------------------------------------------------------------------------------------------------------- */

        public void AnalyzeWaveTarget( int waveScenarioNo, TimeSpan period, fxHistoricBarsRepo bars, long selectedBarTime )
        {
            var aa = ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( bars.Security );

            if ( aa == null )
            {
                return;
            }

            var bar = bars.GetBarByTime( selectedBarTime );

            ref var hew = ref bar.GetWaveFromScenario( waveScenarioNo );

            if ( hew == AdvBarInfo.EmptyHew )
            {
                return;
            }

            var highestWave = hew.GetFirstHighestWaveInfo( );

            if ( highestWave.HasValue )
            {
                var waveKey = new WaveModelKey( bars.Security, period, waveScenarioNo, selectedBarTime, -1, highestWave.Value.WaveCycle );

                WavePredictionModel waveModel = null;

                if ( aa.GetOrCreateWaveModel( waveKey, bars, this, out waveModel ) )
                {
                    waveModel.BuildWaveModel();
                }
            }

            







        }

        
        public PooledList<DbElliottWave> GetAllWavesOfDegreeAfter( int waveScenarioNo, TimeSpan period, long rawBarTime, ElliottWaveEnum waveName, ElliottWaveCycle waveDegree )
        {
            var output = new PooledList< DbElliottWave >( );

            var hews = GetElliottWavesDictionary( period );

            if ( hews.Count > 0 )
            {
                var nextWaves = hews.GetElementsLeftOf( rawBarTime );

                foreach ( var wave in nextWaves )
                {
                    var hew = wave.Value.GetWaveFromScenario( waveScenarioNo );

                    PooledList< ElliottWaveEnum > lastWave = hew.GetWavesAtCycle( waveDegree );

                    if ( lastWave.Count > 0 )
                    {
                        output.Add( wave.Value );
                    }
                }
            }

            return output;
        }

        public PooledList<DbElliottWave> GetAllWavesAfter( int waveScenarioNo, TimeSpan period, long rawBarTime )
        {
            var output = new PooledList< DbElliottWave >( );

            var hews = GetElliottWavesDictionary( period );

            if ( hews.Count > 0 )
            {
                var nextWaves = hews.GetElementsLeftOf( rawBarTime );

                foreach ( var wave in nextWaves )
                {
                    ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                    if ( hew != AdvBarInfo.EmptyHew )
                    {
                        output.Add( wave.Value );
                    }
                }
            }

            return output;
        }

        bool _ew1MinDone = false;
        bool _ew1HrDone = false;
        bool _ewDailyDone = false;

        public void RestoreElliottWaves( SBarList barList, TimeSpan period, int waveScenarioNo )
        {
            var hews = GetElliottWavesDictionary( period );
            var bars = GetDatabarsRepository( period );

            List< ( int, DbElliottWave )> wavePrices = new List<(int, DbElliottWave)>();

            foreach ( var eWave in hews )
            {
                ref SBar waveBar = ref barList.RestoreWave( eWave.Key, ref eWave.Value.GetWaveFromScenario( 1 ), ref eWave.Value.GetWaveFromScenario( 2 ), ref eWave.Value.GetWaveFromScenario( 3 ), ref eWave.Value.GetWaveFromScenario( 4 ) );

                if ( waveBar != SBar.EmptySBar )
                {
                    eWave.Value.AttachOwningBar( null, ref waveBar );

                    wavePrices.Add( ( waveBar.Index, eWave.Value ) );                    
                }
            }

            foreach( var wavePrice in wavePrices )
            {
                var firstWave = wavePrice.Item2.GetWaveFromScenario( 1 ).GetFirstHighestWaveInfo( );

                if ( firstWave.HasValue )
                {
                    WavePriceTimeInfo output1 = default;

                    if ( CalculateWavePriceTimeInfo( 1, bars, period, wavePrice.Item2.StartDate, firstWave.Value.WaveName, firstWave.Value.WaveCycle, firstWave.Value.LabelPosition, ref output1 ) )
                    {
                        ref SBar sbar = ref barList[ wavePrice.Item1 ];
                        sbar.MainPriceTimeInfo = output1;
                    }
                }

                firstWave = wavePrice.Item2.GetWaveFromScenario( 2 ).GetFirstHighestWaveInfo();

                if ( firstWave.HasValue )
                {
                    WavePriceTimeInfo output1 = default;

                    if ( CalculateWavePriceTimeInfo( 2, bars, period, wavePrice.Item2.StartDate, firstWave.Value.WaveName, firstWave.Value.WaveCycle, firstWave.Value.LabelPosition, ref output1 ) )
                    {
                        ref SBar sbar = ref barList[ wavePrice.Item1 ];
                        sbar.Atl01PriceTimeInfo = output1;
                    }
                }

                firstWave = wavePrice.Item2.GetWaveFromScenario( 3 ).GetFirstHighestWaveInfo();

                if ( firstWave.HasValue )
                {
                    WavePriceTimeInfo output1 = default;

                    if ( CalculateWavePriceTimeInfo( 3, bars, period, wavePrice.Item2.StartDate, firstWave.Value.WaveName, firstWave.Value.WaveCycle, firstWave.Value.LabelPosition, ref output1 ) )
                    {
                        ref SBar sbar = ref barList[ wavePrice.Item1 ];
                        sbar.Atl02PriceTimeInfo = output1;
                    }
                }

                firstWave = wavePrice.Item2.GetWaveFromScenario( 4 ).GetFirstHighestWaveInfo();

                if ( firstWave.HasValue )
                {
                    WavePriceTimeInfo output1 = default;

                    if ( CalculateWavePriceTimeInfo( 4, bars, period, wavePrice.Item2.StartDate, firstWave.Value.WaveName, firstWave.Value.WaveCycle, firstWave.Value.LabelPosition, ref output1 ) )
                    {
                        ref SBar sbar = ref barList[ wavePrice.Item1 ];
                        sbar.Atl03PriceTimeInfo = output1;
                    }
                }
            }

            if ( period == TimeSpan.FromMinutes( 1 ) )
            {
                _ew1MinDone = true;
            }
            else if ( period == TimeSpan.FromHours( 1 ) )
            {
                _ew1HrDone = true;
            }
            else if ( period == TimeSpan.FromDays( 1 ) )
            {
                _ewDailyDone = true;
            }

            if ( _ew1HrDone && _ew1MinDone && _ewDailyDone )
            {
                TaskBuildElliottWaves( );
            }

        }

        protected Task TaskBuildElliottWaves( )
        {            
            Task detectTask  = new Task(
                                            () =>
                                            {
                                                BuildElliottWaves( TimeSpan.FromDays( 1 ) );

                                            }, GeneralHelper.GlobalExitToken( )
                                        );

            detectTask.Start();

            return detectTask;
        }

    }
}
