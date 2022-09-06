using DevExpress.Mvvm;
using DevExpress.Xpf.Docking;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using fx.Charting;
using fx.Algorithm;
using fx.Indicators;
using fx.Definitions;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fx.Bars;

namespace FreemindAITrade.ViewModels
{
    public interface IChartTabViewModel
    {
        int WaveScenarioNumber { get; set; }
        FreemindIndicator FreemindIndicator { get;  }
        PivotPointsCustom PivotPointIndicator { get; }
        ChartExViewModel ChartVM { get;  }
        long SelectedCandleBarTime { get; set; }
        string TargetName { get; set; }

        bool NeedToCenterOnBar { get; set; }
        bool IsActive { get; set; }

        long CenterViewOnThisBarTime { get; set; }

        fxHistoricBarsRepo DatabarRepo { get; }

        void OnChartTabSelected( );
        void Refresh( );
        

        void CenterViewOnTime( DateTime selectedBarTime );

        void CenterViewOnIndexNow(  DateTime selectedBarTime );

        void StartChartDrawingTimerThread( );

        void StopTimerThread( );

        void SetQuickOrderPanel( Security security );

        TimeSpan ResponsibleTF { get; set; }

        event Action<IChartTabViewModel > TabActivated;
        event Action<IChartTabViewModel> CandlesLoadedEvent;
        event Action<IChartTabViewModel>  DoneDownloadBarsEvent;

        void UnRegisterCommandsAndEvents( );

        void AddWaveOneToChart( int waveScenarioNo, ElliottWaveCycle waveCycle, ElliottWaveEnum elliottWave );

        void AddWaveTwoToChart( int waveScenarioNo, ElliottWaveCycle waveCycle, ElliottWaveEnum elliottWave );

        void AddWaveToChart( int waveScenarioNo, ElliottWaveCycle waveCycle, ElliottWaveEnum elliottWave );

        void AddWaveFourToChart( int waveScenarioNo, ElliottWaveCycle waveCycle );

        void AddWaveAToChart( int waveScenarioNo, ElliottWaveCycle waveCycle, ElliottWaveEnum elliottWave );

        void AddWaveBToChart( int waveScenarioNo, ElliottWaveCycle waveCycle, ElliottWaveEnum elliottWave );

        void AddWaveCToChart( int waveScenarioNo, ElliottWaveCycle waveCycle, ElliottWaveEnum elliottWave );

        void CycleUpSelectedBar( int waveScenarioNo );

        void CycleDownSelectedBar( int waveScenarioNo );

        void RemoveWavesFromManagerAndBar( int waveScenarioNo, TimeSpan period );

        void BranchWaves();

        void MarkPrimary();

        void AnalyzeWaveTarget( );            

        void LockWavesInDB( );

        void SwitchToTimeFrameX( TimeSpan reponsible );        

        void CopyBarTimeToClipboard( );

        void FindAndLoadDatabarsLowerTimeFrame( int waveScenarioNo );

        void FindAndLoadDatabarsHigherTimeFrame( int waveScenarioNo );

        void StartSimulation( );
        bool StepByStep { get; set; }

        void ReloadCandles();

        void Step01_ExecuteAddChartArea( );
        
        void Step01_ExecuteAddIndicatorArea( );

        void Step3_LoadCandlesFromLocalStorage_NonVisual();

        void AnalysisWave( );
    }
}
