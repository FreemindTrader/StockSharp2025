using fx.Collections;
using fx.TimePeriod;

using fx.Common;
using fx.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fx.Bars;
using System.Collections.ObjectModel;
using Ecng.Xaml;
using Ecng.ComponentModel;

namespace fx.Algorithm
{
    public class HewPredictionTargets
    {
        FibonacciType          _fibType;
        WaveInfo               _currentWaveInfo;
        SBar                 _startingBar;
        FibLevelsCollection    _fibLevels;
        ElliottWaveEnum        _targetWaveName;

        ( DateTime, float )    _startPoint;
        ( DateTime, float )    _endPoint;

        TimeBlockEx            _timePeriod;
        TimeSpan               _period;
        private ThreadSafeDictionary< ElliottWaveCycle, FibExpRet > _otherfibLevels = new ThreadSafeDictionary< ElliottWaveCycle, FibExpRet >( );

        public HewPredictionTargets( TimeSpan period, ( DateTime, float ) start, ( DateTime, float ) end, ref SBar bar0, FibLevelsCollection fibLevels, ref WaveInfo currWaveInfo, FibonacciType fibType )
        {
            _period      = period;
            _startPoint  = start;
            _endPoint    = end;
            _startingBar = bar0;

            _timePeriod = new TimeBlockEx( _startPoint.Item1, _endPoint.Item1 );

            FibLevels    = fibLevels;
            FibType      = fibType;
        }

        public ElliottWaveEnum TargetWaveName
        {
            get
            {
                return _targetWaveName;
            }
            set
            {
                _targetWaveName = value;
            }
        }

        public FibonacciType FibType
        {
            get
            {
                return _fibType;
            }
            set
            {
                _fibType = value;
            }
        }

        public FibLevelsCollection FibLevels
        {
            get
            {
                return _fibLevels;
            }
            set
            {
                _fibLevels = value;
            }
        }

        public TimeBlockEx TimePeriod
        {
            get
            {               
                return _timePeriod;
            }
        }

        public PooledList< SRlevel > ToSRlevelList( )
        {
            PooledList< SRlevel > output = new PooledList< SRlevel >( );

            foreach( var fibLevel in _fibLevels.FibLevels )
            {
                output.Add( new SRlevel( ref _timePeriod, _period, fibLevel.FibLevel, ( int )fibLevel.FibLevelStrengh, _fibType.To1stSuppRes( ), TargetWaveName.To2ndSuppRes( ), _fibType.To3rdSuppRes( ), fibLevel.FibPrecentage.ToString( ) ) );
            }

            foreach( var fibExpRetPair in _otherfibLevels )
            {
                var fibExpRet = fibExpRetPair.Value;

                if( fibExpRet != null )
                {
                    output.AddRange( fibExpRet.GetSRlevelList( ) );
                }
            }

            var sorted = output.OrderBy( i => i.SRvalue ).ToPooledList( );

            return sorted;
        }

        public ref SBar StartingBar
        {
            get
            {
                return ref _startingBar;
            }
            
        }

        public WaveInfo CurrentWaveInfo
        {
            get
            {
                return _currentWaveInfo;
            }
            set
            {
                _currentWaveInfo = value;
            }
        }

        public void AddHigherPredictionTargets( ElliottWaveCycle higherWaveCycle, FibExpRet higherHew )
        {
            _otherfibLevels.TryAdd( higherWaveCycle, higherHew );
        }

        public void Analyze( )
        {
            var newSRlevels = ToSRlevelList( );

            var aa = ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _startingBar.SymbolEx );

            ObservableCollectionEx< SRlevel > srLevels = null;

            if ( aa != null )
            {
                srLevels = aa.SRLevelsItemSource;
            }
            else
            {
                return;
            }            

            newSRlevels.AddRange( srLevels );

            newSRlevels = newSRlevels.Distinct( ).ToPooledList( );

            var a = newSRlevels.OrderBy( i => i.SRvalue ).ToPooledList( );
            srLevels.Clear( );

            foreach( var b in a )
            {
                srLevels.Add( b );
            }

            //foreach ( var newSRlevel in newSRlevels )
            //{
            //    if ( !srLevels.Contains( newSRlevel ) )
            //        srLevels.Add( newSRlevel );
            //}

            //srLevels.BubbleSort( );
        }        
    }
}
