using fx.Collections;
using DevExpress.Mvvm;

using fx.Common;
using fx.Definitions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using fx.Bars;
using System.Runtime.CompilerServices;

namespace fx.Algorithm
{
    public interface IStructureLabel
    {
        void SetStructureLabel( ref SBar bar, StructureLabelEnum structLabel );
    }

    public partial class PeriodXTaManager : BindableBase, IStructureLabel, IPeriodXTaManager
    {
        public double AverageDailyRange
        {
            get
            {
                return _averageDailyRange;
            }
            set
            {
                SetValue( ref _averageDailyRange, value );

                RaisePropertyChanged( nameof( AverageDailyRange ) );
            }
        }

        public double TodayRange
        {
            get
            {
                return _todayRange;
            }
            set
            {
                SetValue( ref _todayRange, value );

                RaisePropertyChanged( nameof( TodayRange ) );
            }
        }

        public int TodayRangeToAvgPercentage
        {
            get
            {
                return _todayRangeToAvgPercentage;
            }

            set
            {
                int newValue = 0;

                if( _averageDailyRange > 0 )
                {
                    newValue = ( int )( _todayRange / _averageDailyRange * 100 );

                    if( newValue > 100 )
                    {
                        newValue = 100;
                    }
                }

                if( _todayRangeToAvgPercentage != newValue && newValue > 0 )
                {
                    _todayRangeToAvgPercentage = newValue;

                    RaisePropertyChanged( nameof( TodayRangeToAvgPercentage ) );
                }
            }
        }

        public string BarSpeedString
        {
            get
            {
                string output = "";

                switch( _barSpeeding )
                {
                    case BarSpeed.Invalid:
                        output = "Bar % Meter";
                        break;
                    case BarSpeed.Stopped:
                        output = "Stopped";
                        break;
                    case BarSpeed.SpeedingUp:
                        output = "<color=255,0,0>SpeedingUp";
                        break;
                    case BarSpeed.Steady:
                        output = "<color=0,0,255>Steady";
                        break;
                    case BarSpeed.SlowingDown:
                        output = "<color=0,255,0>Slowing Down";
                        break;
                }
                return output;
            }
        }

        BarSpeed _barSpeeding;

        public BarSpeed BarSpeeding
        {
            get
            {
                return _barSpeeding;
            }
            set
            {
                SetValue( ref _barSpeeding, value );
            }
        }

        public PooledList< DivergenceInfo >    LatestDivergenceInfo
        {
            get;
            set;
        }

        public TABottomingSignal       LatestBottomingSignal
        {
            get;
            set;
        }

        public TAToppingSignal         LatestToppingSignal
        {
            get;
            set;
        }

        public PooledList< WaveRotationInfo >  LatestWaveImportantTimeInfo
        {
            get;
            set;
        }

        public PooledList< GannPriceTimeInfo > LatestGannPriceTimeInfo
        {
            get;
            set;
        }

        public TimeSpan Period
        {
            get;
            set;
        }

        public string Symbol
        {
            get;
            set;
        }

        public PeriodXTaManager( string symbol, TimeSpan timePeriod )
        {
            Symbol = symbol;
            Period = timePeriod;
        }

        
        public void AddBottomingSignal( ref SBar bar, TABottomingSignal info )
        {
            FreemindAdvAnalysisInfo ta = null;

            if( _technicalAnalysisInfo.TryGetValue( bar.LinuxTime, out ta ) )
            {
                ta.AddSignalInfo( info );
            }
            else
            {
                var newTA = new FreemindAdvAnalysisInfo( );

                newTA.AddSignalInfo( info );

                _technicalAnalysisInfo.Add( bar.LinuxTime, newTA );
            }

            if( bar.BarIndex > _latestBottomSignalIndex )
            {
                _latestBottomSignalIndex = bar.BarIndex;
                LatestBottomingSignal = info;
            }
        }

        public void AddToppingSignal( ref SBar bar, TAToppingSignal info )
        {
            FreemindAdvAnalysisInfo ta = null;

            if( _technicalAnalysisInfo.TryGetValue( bar.LinuxTime, out ta ) )
            {
                ta.AddSignalInfo( info );
            }
            else
            {
                var newTA = new FreemindAdvAnalysisInfo( );

                newTA.AddSignalInfo( info );

                _technicalAnalysisInfo.Add( bar.LinuxTime, newTA );
            }

            if( bar.BarIndex > _latestBottomSignalIndex )
            {
                _latestBottomSignalIndex = bar.BarIndex;
                LatestToppingSignal = info;
            }
        }

        public void AddDivergenceInfo( ref SBar bar, DivergenceInfo info )
        {
            FreemindAdvAnalysisInfo ta = null;

            if( _technicalAnalysisInfo.TryGetValue( bar.LinuxTime, out ta ) )
            {
                ta.AddDivergenceInfo( info );
            }
            else
            {
                var newTA = new FreemindAdvAnalysisInfo( );

                newTA.AddDivergenceInfo( info );

                _technicalAnalysisInfo.Add( bar.LinuxTime, newTA );
            }

            if( bar.BarIndex > _latestDivergenceIndex )
            {
                if ( bar.BarIndex == ( _latestDivergenceIndex + 1 ) )
                {
                    //throw new InvalidProgramException( );
                }

                _latestDivergenceIndex = bar.BarIndex;
                LatestDivergenceInfo = new PooledList< DivergenceInfo >( );
                LatestDivergenceInfo.Add( info );
            }
        }

        public PooledList< DivergenceInfo > GetDivergenceInfo( ref SBar bar )
        {
            FreemindAdvAnalysisInfo ta = null;

            if( _technicalAnalysisInfo.TryGetValue( bar.LinuxTime, out ta ) )
            {
                if( ta != null )
                {
                    var div = ta.GetDivergenceInfo( );

                    return div;
                }
            }

            return null;
        }

        //public PooledList<DivergenceInfo> GetDivergenceInfo( SBar bar )
        //{
        //    FreemindAdvAnalysisInfo ta = null;

        //    if ( _technicalAnalysisInfo.TryGetValue( bar.LinuxTime, out ta ) )
        //    {
        //        if ( ta != null )
        //        {
        //            var div = ta.GetDivergenceInfo( );

        //            return div;
        //        }
        //    }

        //    return null;
        //}

        public void SetStructureLabel( ref SBar bar, StructureLabelEnum structLabel )
        {
            FreemindAdvAnalysisInfo ta = null;

            if ( bar.Index == 89656 )
            {

            }

            if ( _technicalAnalysisInfo.TryGetValue( bar.LinuxTime, out ta ) )
            {
                ta.StructureLabel = structLabel;
            }
            else
            {
                var newTA = new FreemindAdvAnalysisInfo( );

                newTA.StructureLabel = structLabel;

                _technicalAnalysisInfo.Add( bar.LinuxTime, newTA );
            }

            bar.HasStructureLabel = true;
        }


        public StructureLabelEnum GetStructureLabel( ref SBar bar )
        {
            FreemindAdvAnalysisInfo info = null;

            if( _technicalAnalysisInfo.TryGetValue( bar.LinuxTime, out info ) )
            {
                var structLabel = info.StructureLabel;

                return structLabel;
            }

            return StructureLabelEnum.NONE;
        }

        public PooledList< DivergenceInfo > GetDivergenceInfoForDrawing( ref  SBar bar )
        {
            FreemindAdvAnalysisInfo ta = null;

            if( _technicalAnalysisInfo.TryGetValue( bar.LinuxTime, out ta ) )
            {
                if( ta != null )
                {
                    var div = ta.GetDivergenceInfoDescending( );

                    return div;
                }
            }

            return null;
        }

        public PooledList< MatchedSRinfo > GetPivotReltations( ref SBar bar )
        {
            FreemindAdvAnalysisInfo ta = null;

            if( _technicalAnalysisInfo.TryGetValue( bar.LinuxTime, out ta ) )
            {
                if( ta != null )
                {
                    var relations = ta.GetPivotRelations( );

                    return relations;
                }
            }

            return null;
        }

        //public PooledList<MatchedSRinfo> GetPivotReltations( SBar bar )
        //{
        //    FreemindAdvAnalysisInfo ta = null;

        //    if ( _technicalAnalysisInfo.TryGetValue( bar.LinuxTime, out ta ) )
        //    {
        //        if ( ta != null )
        //        {
        //            var relations = ta.GetPivotRelations( );

        //            return relations;
        //        }
        //    }

        //    return null;
        //}

        public PooledList< WaveRotationInfo > GetWaveImportantTimeInfo( ref SBar bar )
        {
            FreemindAdvAnalysisInfo wave = null;

            if( _technicalAnalysisInfo.TryGetValue( bar.LinuxTime, out wave ) )
            {
                if( wave != null )
                {
                    var rot = wave.GetWaveImportantTimeInfo( );

                    return rot;
                }
            }

            return null;
        }

        //public PooledList<WaveRotationInfo> GetWaveImportantTimeInfo( SBar bar )
        //{
        //    FreemindAdvAnalysisInfo wave = null;

        //    if ( _technicalAnalysisInfo.TryGetValue( bar.LinuxTime, out wave ) )
        //    {
        //        if ( wave != null )
        //        {
        //            var rot = wave.GetWaveImportantTimeInfo( );

        //            return rot;
        //        }
        //    }

        //    return null;
        //}

        //public PooledList<WaveRotationInfo> GetWaveImportantTimeInfo( ref SBar bar )
        //{
        //    FreemindAdvAnalysisInfo wave = null;

        //    if ( _technicalAnalysisInfo.TryGetValue( bar.LinuxTime, out wave ) )
        //    {
        //        if ( wave != null )
        //        {
        //            var rot = wave.GetWaveImportantTimeInfo( );

        //            return rot;
        //        }
        //    }

        //    return null;
        //}

        public PooledList< GannPriceTimeInfo > GetGannPriceTimeInfo( ref SBar bar )
        {
            FreemindAdvAnalysisInfo wave = null;

            if( _technicalAnalysisInfo.TryGetValue( bar.LinuxTime, out wave ) )
            {
                if( wave != null )
                {
                    var rot = wave.GetGannPriceTimeInfo( );

                    return rot;
                }
            }

            return null;
        }

        

        public PooledList< TAToppingSignal > GetToppingSignalsInfo( ref SBar bar )
        {
            FreemindAdvAnalysisInfo info = null;

            if( _technicalAnalysisInfo.TryGetValue( bar.LinuxTime, out info ) )
            {
                if( info != null )
                {
                    var rot = info.GetToppingSignals( );

                    return rot;
                }
            }

            return null;
        }

        public PooledList< TABottomingSignal > GetBottomingSignalsInfo( ref SBar bar )
        {
            FreemindAdvAnalysisInfo info = null;

            if( _technicalAnalysisInfo.TryGetValue( bar.LinuxTime, out info ) )
            {
                if( info != null )
                {
                    var rot = info.GetBottomingSignals( );

                    return rot;
                }
            }

            return null;
        }

        public void AddZigZagInfo( long startAddingTime, ThreadSafeDictionary< long, TASignal > zigZagInfo )
        {
            if( _zigZagInfo.ContainsKey( startAddingTime ) )
            {
                Collections.RemoveManyDictionaryDelegate< long, TASignal > removeDelegate = delegate( long x, TASignal signal )
                {
                    if( x > startAddingTime )
                    {
                        return true;
                    }

                    return false;
                };

                _zigZagInfo.RemoveMany( removeDelegate );
            }

            if( zigZagInfo == null )
            {
                throw new ArgumentNullException( "Collection is null" );
            }

            foreach ( KeyValuePair<long, TASignal> zzInfo in zigZagInfo )
            {
                _zigZagInfo.TryAdd( zzInfo.Key, zzInfo.Value );
            }
            

            foreach( var zigZag in zigZagInfo )
            {
                if( zigZag.Value == TASignal.WAVE_PEAK )
                {
                    AddWaveTopSignal( zigZag.Key, TAToppingSignal.WAVE_PEAK );
                }
                else
                {
                    AddWaveBottomSignal( zigZag.Key, TABottomingSignal.WAVE_TROUGH );
                }
            }
        }

        public void AddZigZagInfo( long startAddingTime, IEnumerable< KeyValuePair<long, TASignal> > zigZagInfo )
        {
            if ( _zigZagInfo.ContainsKey( startAddingTime ) )
            {
                Collections.RemoveManyDictionaryDelegate< long, TASignal > removeDelegate = delegate( long x, TASignal signal )
                {
                    if( x > startAddingTime )
                    {
                        return true;
                    }

                    return false;
                };

                _zigZagInfo.RemoveMany( removeDelegate );
            }

            if ( zigZagInfo == null )
            {
                throw new ArgumentNullException( "Collection is null" );
            }

            foreach ( KeyValuePair<long, TASignal> zzInfo in zigZagInfo )
            {
                _zigZagInfo.TryAdd( zzInfo.Key, zzInfo.Value );
            }


            foreach ( var zigZag in zigZagInfo )
            {
                if ( zigZag.Value == TASignal.WAVE_PEAK )
                {
                    AddWaveTopSignal( zigZag.Key, TAToppingSignal.WAVE_PEAK );
                }
                else
                {
                    AddWaveBottomSignal( zigZag.Key, TABottomingSignal.WAVE_TROUGH );
                }
            }
        }

        public void AddWaveTopSignal( long barTime, TAToppingSignal info )
        {
            FreemindAdvAnalysisInfo ta = null;

            if( _technicalAnalysisInfo.TryGetValue( barTime, out ta ) )
            {
                ta.AddSignalInfo( info );
            }
            else
            {
                var newTA = new FreemindAdvAnalysisInfo( );

                newTA.AddSignalInfo( info );

                _technicalAnalysisInfo.Add( barTime, newTA );
            }

            LatestToppingSignal = info;
        }

        public void AddWaveBottomSignal( long barTime, TABottomingSignal info )
        {
            FreemindAdvAnalysisInfo ta = null;

            if( _technicalAnalysisInfo.TryGetValue( barTime, out ta ) )
            {
                ta.AddSignalInfo( info );
            }
            else
            {
                var newTA = new FreemindAdvAnalysisInfo( );

                newTA.AddSignalInfo( info );

                _technicalAnalysisInfo.Add( barTime, newTA );
            }

            LatestBottomingSignal = info;
        }

        public ThreadSafeDictionary< long, TASignal > ZigZagResult
        {
            get
            {
                return _zigZagInfo;
            }
        }

        public void AddWaveImportantTimeInfo( ref SBar bar, WaveRotationInfo info )
        {
            FreemindAdvAnalysisInfo ta = null;

            if( _technicalAnalysisInfo.TryGetValue( bar.LinuxTime, out ta ) )
            {
                ta.AddWaveImportantTimeInfo( info );
            }
            else
            {
                var newTA = new FreemindAdvAnalysisInfo( );

                newTA.AddWaveImportantTimeInfo( info );

                _technicalAnalysisInfo.Add( bar.LinuxTime, newTA );
            }

            bar.AddSignal( TASignal.HAS_TIME_ROTATION );

            if( bar.BarIndex > _latestWaveRotationIndex )
            {
                _latestWaveRotationIndex = bar.BarIndex;

                LatestWaveImportantTimeInfo = new PooledList< WaveRotationInfo >( );
                LatestWaveImportantTimeInfo.Add( info );
            }

            if( bar.BarPeriod == TimeSpan.FromDays( 1 ) && bar.BarIndex == 0 )
            {
            }
        }

        public bool TryAddGannPriceTimeInfo( ref SBar bar, GannPriceTimeInfo info )
        {
            bool added = false;
            FreemindAdvAnalysisInfo ta = null;

            if( _technicalAnalysisInfo.TryGetValue( bar.LinuxTime, out ta ) )
            {
                added = ta.CheckAndAddGannPriceTimeInfo( info );

                if( added )
                {
                    bar.AddSignal( TASignal.HAS_GANN_SQUARE );
                }
            }
            else
            {
                var newTA = new FreemindAdvAnalysisInfo( );

                newTA.AddGannPriceTimeInfo( info );

                _technicalAnalysisInfo.Add( bar.LinuxTime, newTA );

                bar.AddSignal( TASignal.HAS_GANN_SQUARE );

                added = true;
            }

            if( added )
            {
                if( bar.BarIndex > _lastestGannPriceTimeIndex )
                {
                    _lastestGannPriceTimeIndex = bar.BarIndex;

                    LatestGannPriceTimeInfo = new PooledList< GannPriceTimeInfo >( );
                    LatestGannPriceTimeInfo.Add( info );
                }
            }

            return added;
        }

        public void AddPivotRelationship( ref SBar bar, PooledList< MatchedSRinfo > info )
        {
            FreemindAdvAnalysisInfo ta = null;

            if( _technicalAnalysisInfo.TryGetValue( bar.LinuxTime, out ta ) )
            {
                ta.AddPivotRelationship( info );
            }
            else
            {
                var newTA = new FreemindAdvAnalysisInfo( );

                newTA.AddPivotRelationship( info );

                _technicalAnalysisInfo.Add( bar.LinuxTime, newTA );
            }

            bar.AddSignal( TASignal.HAS_PIVOT_RELATION );
        }


        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public bool IsLatestSignalBar( int index )
        {
            if ( index == _latestTopSignalIndex )
            {
                return true;
            }

            if ( index == _latestBottomSignalIndex )
            {
                return true;
            }

            if ( index == _latestDivergenceIndex )
            {
                return true;
            }

            return false;
        }
    }

    
}
