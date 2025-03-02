using fx.Collections;
using System;
using System.Collections.Generic;


namespace fx.Definitions
{
    
    /// <summary>
    /// Interface defines what values should a databar represents
    /// </summary>
    public interface ITechnicalBar
    {        
        bool       HasSignal              { get; }       
        
        bool HasDivergence { get; }

        bool HasWaveRotation { get; }

        bool HasGannSquare { get; }
        bool HasPivotRelation { get; }

        bool HasStructureLabel { get; }        

        TASignal  TechnicalAnalysisSignal { get; set; }

        

        void RemoveSignals( TASignal signal1, TASignal signal2 );

        void RemoveSignals( TASignal signal );

        void ClearSignal( );


        int        TechnicalAnalysisSignalCount( ref PooledList< TASignal > signalList );               
        string     GetBarSignalString( );

        string BarSignal { get;  }                       
    }

    public enum MacdSignal
    {
        NONE = 0,
        HighestPoint_MacdUptrend,
        LowestPoint_MacdDowntrend,
        HigherHigh_MacdDowntrend,
        LowerLow_MacdUptrend
    }

    
}