using fx.Bars;
using fx.Collections;
using fx.Common;
using fx.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Algorithm
{
    public interface IFactal
    {
        WaveType MainWaveType { get; set; }
        long BeginTime { get; set; }
        long EndTime { get; set; }

        bool HasChildren { get; set; }
        PooledList<FibLevelInfo> PredictedTargets { get; }
    }

    public interface I3Waves : IFactal
    {
        long Wave0Time { get; set; }
        long WaveA1Time { get; set; }
        long WaveB1Time { get; set; }
        long WaveC1Time { get; set; }
        long WaveA2Time { get; set; }
        long WaveB2Time { get; set; }
        long WaveC2Time { get; set; }
        long WaveA3Time { get; set; }
        long WaveB3Time { get; set; }
        long WaveC3Time { get; set; }

        IFactal SubWaveA1 { get; set; }
        IFactal SubWaveB1 { get; set; }
        IFactal SubWaveC1 { get; set; }
        IFactal SubWaveA2 { get; set; }
        IFactal SubWaveB2 { get; set; }
        IFactal SubWaveC2 { get; set; }
        IFactal SubWaveA3 { get; set; }
        IFactal SubWaveB3 { get; set; }
        IFactal SubWaveC3 { get; set; }
    }

    public interface I5Waves : IFactal
    {
        long Wave0Time { get; set; }
        long Wave1ATime { get; set; }
        long Wave1BTime { get; set; }
        long Wave1CTime { get; set; }
        long Wave2Time { get; set; }
        long Wave3ATime { get; set; }
        long Wave3BTime { get; set; }
        long Wave3CTime { get; set; }
        long Wave4Time { get; set; }
        long Wave5ATime { get; set; }
        long Wave5BTime { get; set; }
        long Wave5CTime { get; set; }

        IFactal SubWave1A { get; set; }
        IFactal SubWave1B { get; set; }
        IFactal SubWave1C { get; set; }
        IFactal SubWave2 { get; set; }
        IFactal SubWave3A { get; set; }
        IFactal SubWave3B { get; set; }
        IFactal SubWave3C { get; set; }
        IFactal SubWave4 { get; set; }
        IFactal SubWave5A { get; set; }
        IFactal SubWave5B { get; set; }
        IFactal SubWave5C { get; set; }
    }

    public interface IWave
    {
        KeyValuePair<long, WavePointImportance> BeginPt          { get; set; }
        DateTime                                BeginDateTime    { get; }
        int                                     BeginBarIndex    { get;  }
        long                                    BeginTime        { get; }
        double                                  Begin            { get; }



        KeyValuePair<long, WavePointImportance> EndPt            { get; set; }
        DateTime                                EndTime          { get; }
        int                                     EndBarIndex      { get; }
        double                                  End              { get; }

        TrendDirection Direction                                 { get; set; }

        RangeEx<double> Range { get; }

        WaveType MainWaveType                                    { get; set; }
        double Pips                                              { get; }
        TimeSpan Duration                                        { get; set; }

        int WaveImportanceNumber                                 { get; }

        bool Is3Wave( );

        bool IsImpulsiveWave( );

        double Highest                                           { get; }
        double Lowest                                            { get; }

        int TimeUnit                                             { get;  }

        WavePattern WavePattern { get; set; }
        ElliottWaveEnum PotentialWave { get; set; }

        StructureLabelEnum StructureLabel { get; set; }

        double Over( IWave other );

        double DividedBy( IWave other );

        #region Retracement 
        bool IsCompletelyRetraced_byM( IWave other );
        bool IsCompletelyRetraced_Slower_byM( IWave other );

        bool IsCompletelyRetraced_Faster_byM( IWave other );
        bool IsCompletelyRetraced_LT_Ytime_byM( int timeUnit, IWave other );
        bool IsCompletelyRetraced_GT_Ytime_byM( int timeUnit, IWave other );

        bool IsCompletelyRetraced_byM_Minus1TU( IWave other );

        bool IsRetraced_LtX_byM( double percent, IWave other );
        bool IsRetraced_LtX_EqZtime_byM( double percent, int timeUnit, IWave other );
        bool IsRetracedLtX_OrGtY_inLETime_byM( double lowerBound, double upperBound, IWave other );
        bool IsRetracedLtX_OrGtY_inLE_ZTime_byM( double lowerBound, double upperBound, int timeUnit, IWave other );
        bool IsRetraced_NoMoreThanX_byM( double percent, IWave other );
        bool IsRetraced_AtLeastX_byM( double percent, IWave other );
        bool IsRetraced_AtLeastX_PriceTime_byM( double percent, IWave other );
        bool IsRetraced_AtLeastX_NoMoreThanY_byM( double lowerBound, double upperBound, IWave other );

        bool Plus1TU_IsCompletelyRetraced_Faster_byM( IWave other );
        bool Plus1TU_IsCompletelyRetraced_Slower_byM( IWave other );
        bool Minus1TU_IsCompletelyRetraced_byM( IWave other );


        #endregion


        #region Length

        bool IsLongerThan( IWave other );

        bool IsLongerThan( IWave first, IWave second );

        bool IsTheLongest( IWave first, IWave second );

        bool IsNotTheShortest( IWave first, IWave second );

        bool IsNotTheLongest( IWave first, IWave second );

        bool IsTheShortest( IWave first, IWave second );

        bool IsShorterThan( IWave other );

        bool IsMoreThanX_ofM( double percentage, IWave other );

        bool IsNotMuchMoreThanX_ofM( double percentage, IWave other );

        bool IsMoreThanX_LessEqTime_ofM( double percentage, IWave other );

        bool IsAtLeastX_LessThanY_ofM( double lowerbound, double upperBound, IWave other );

        bool IsBtw_XYInclusive_ofM( double lowerbound, double upperBound, IWave other );

        bool IsBtw_AtLeastX_LessThanY_ofM( double lowerbound, double upperBound, IWave other );

        bool IsBtw_XYExclusive_ofM( double lowerbound, double upperBound, IWave other );
        bool AchieveSameDistanceInLessTime( IWave other );

        bool AchieveSameDistanceInEqualOrLessTime( IWave other );

        bool IsApproxX_OfM( double percentage, IWave other );

        bool IsApproxEqual_X_OfM( double pctAllowance, IWave m2 );

        bool IsApproxX_PriceTime_OfM( double percentage, IWave m2 );
        bool IsAtLeastX_OfM( double percentage, IWave other );

        bool IsAtLeastX_PriceTime_OfM( double percentage, IWave other );

        bool IsAtLeastX_Time_OfX( double percentage, IWave other );
        bool IsAtMostX_OfM( double percentage, IWave other );

        bool IsWithinX_Time_OfM( double percentage, IWave other );

        bool IsLessThanX_OfM( double percentage, IWave other );

        bool IsNoMoreThanX_OfM( double percentage, IWave other );

        bool IsMoreVerticalThan( IWave other );

        bool IsLessVerticalThan( IWave other );

        bool IsLongerAndMoreVerticalThan( IWave other );

        bool Overlap( IWave other );

        bool DontOverlap( IWave other );

        bool IsCloserToX_ThanY_ofZ( double lowerBound, double higherBound, IWave other );



        #endregion


        bool EndExceededBy( IWave other );

        bool BeginBrokenBy( IWave other );

        bool BeginBrokenBy( double brokenLevel );

        int TimeTakenToBreak( double testLevel );

        int TimeTakenbyMToBreakBegin( IWave m4 );

        bool CompleteWithoutExceeding( IWave m2, WavePosition pos );

        BrokenLevel BrokenBy( double high, double low );

        double Retracement( double otherRange );

        void AddStructureLabel( StructureLabelEnum input );

        void DropStructureLabel( StructureLabelEnum input );


        bool EndNotExceededForXTime( int fourtime_m3_Tom1 );

        bool IsPeakTroughForAtLeast_XTime( int timeUnit );

        bool TimeToEqualX_LessThan( IWave other );

        bool CloseOrExceedX_TimeLTEy( double price, double timeTaken );

        bool ReturnToX_TimeLTEy( double price, double timeTaken );

        bool Break( double m3BrokenLevel );

        bool HasAnyVariationOf3( );

        string GetStructureLabelString( );

        bool Exceed( double otherEndValue );

        double PricePercenatage( IWave m2 );

        double TimePercenatage( IWave m2 );

        bool PriceApproxEqual( IWave m2 );

        bool TimeApproxEqual( IWave m2 );

        bool PriceTimeApproxEqual( IWave m2 );
        bool TimeApproxEqual( IWave m2, double pctAllowance );

        WavePosition MissingXWavePos { get; set; }

        StructureLabelEnum MissingXWaves { get; set; }

        WavePosition WavePosition { get; set; }
    }
}
