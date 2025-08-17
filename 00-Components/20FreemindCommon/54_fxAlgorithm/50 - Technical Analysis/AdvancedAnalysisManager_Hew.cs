using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using fx.Definitions;
using System.ComponentModel;
using DevExpress.Mvvm;

using fx.Common;
using System.Collections.ObjectModel;
using StockSharp.BusinessEntities;
using fx.Collections;

namespace fx.Algorithm
{
    /// <summary>
    /// Class contains general helper functionality for financial classes and operation.
    /// </summary>
    public sealed partial class AdvancedAnalysisManager
    {
        public bool WaveImportanceCalculationAddDone( )
        {
            foreach ( KeyValuePair<TimeSpan, WorkFlowStatus> pair in _waveImportanceCalcStatus )
            {
                if ( pair.Value != WorkFlowStatus.DoneWork )
                {
                    return false;
                }
            }

            return true;
        }

        public bool InitializeWaveImportanceCalculationStatus( )
        {            
            var _waveImportanceCalcStatus = new ThreadSafeDictionary< TimeSpan, WorkFlowStatus >( );

            var supportedTF = SupportedTimeSpan;

            foreach( var tf in supportedTF )
            {
                if( tf != TimeSpan.FromSeconds( 1 ) )
                {
                    _waveImportanceCalcStatus.Add( tf, WorkFlowStatus.NotStarted );
                }
            }            

            return true;
        }

        public WorkFlowStatus GetWaveImportanceCalculationStatus( TimeSpan period )
        {
            WorkFlowStatus output = WorkFlowStatus.NotStarted;

            if ( _waveImportanceCalcStatus.TryGetValue( period, out output ) )
            {
                return output;
            }

            return WorkFlowStatus.ErrorInWork;
        }

        public bool SetWaveImportanceCalculationStatus( TimeSpan period, WorkFlowStatus newStatus )
        {            
            if( _waveImportanceCalcStatus.TryAddOrReplace( period, newStatus ) )
            {
                return true;
            }

            return false;
        }
    }
}
