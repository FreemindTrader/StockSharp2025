using fx.Definitions;
using StockSharp.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Algorithm
{
    public partial class MonoWaveManager
    {
        // ![](68B6C35124F2D9F941E15F30D020727D.png;;;0.03185,0.03301)
        private void Rule6( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            if ( m0 == null )
                return;

            double m0m1Ratio = m0.Over( m1 );

            switch ( m0m1Ratio )
            {
                case double p when ( p < 100 ):
                {
                    Rule6ConditionA( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;

                case double p when ( p >= 100 && p < 161.8 ):
                {
                    Rule6ConditionB( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;

                case double p when ( p >= 161.8 && p <= 261.8 ):
                {
                    Rule6ConditionC( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;

                case double p when ( p > 261.8 ):
                {
                    Rule6ConditionD( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;
            }
        }

        private void Rule6ConditionA( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            
            if ( m1 == null || m2 == null || m3 == null )
                return;

            var first3Pips = m2.First3MonowavesPips( );

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If the first three monowaves of m2 do not retrace more than 61.8% of m1, 
             *  a Complex Correction may be unfolding with the first or second monowave (immediately after the end of m1), 
             *          moving in the opposite direction of m1, representing the x-wave OR 
             *          m1 contains a "missing" x-wave in its center OR 
             *          m1 is the 3rd-wave of a 5th-Failure Impulse pattern (Trending or Terminal); 
             *          
             *                  place ":5/:s5" at the end of m1. 
             * 
             * NOTE: if the "missing" x-wave is used, circle the center of m1 and 
             *          place ":5?" to the left of the circle and 
             *          ":F3?" to the right of the circle, 
             *          the end of the Complex Correction will be confirmed immediately before the point when the market violently turns 
             *                  (in the opposite direction of m1) and exceeds the 61.8% retracement level of m1.
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( m2.MonoWavesCount > 3 )
            {
                var m2First3Overm1 = m1.Retracement( first3Pips );

                if ( m2First3Overm1 <= 61.8 )
                {
                    m1.MainWaveType = WaveType.ComplexCorrection;
                    m1.WavePattern |= WavePattern.MissingXWaves | WavePattern.FailureImpulse5th;
                    m1.PotentialWave = ElliottWaveEnum.WaveX;
                    m1.MissingXWavePos = WavePosition.Center;
                    m1.MissingXWaves = StructureLabelEnum.s5_MAYBE | StructureLabelEnum.F3_MAYBE;

                    m1.AddStructureLabel( StructureLabelEnum._5 | StructureLabelEnum.s5 );
                }

                /* -------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *  If the first three monowaves of m2 retrace more than 61.8% of m1, 
                 *          m1 may have completed wave-a of a Flat with a complex b-wave OR 
                 *          m1 may have completed wave-3 of a 5th Failure Impulse pattern; 
                 *          
                 *                  place ":F3/:5" at the end of m1 to show these two possibilities respectively.
                 * 
                 * ------------------------------------------------------------------------------------------------------------------------------------------- */

                if ( m2First3Overm1 > 61.8 )
                {
                    m1.WavePattern |= WavePattern.Flat | WavePattern.FailureImpulse5th;
                    m1.PotentialWave = ElliottWaveEnum.WaveA;
                    m1.AddStructureLabel( StructureLabelEnum.F3 | StructureLabelEnum._5 );
                }
            }

            if ( m2.MonoWavesCount <= 3 )
            {
                /* -------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *  If m2 is retraced less than 61.8% by m3, 
                 *          place ":L5" at the end of m1. 
                 * 
                 * If m0 and m(-2) share some of the same price territory, 
                 *          add ":L3" to the list.
                 *          
                 * ------------------------------------------------------------------------------------------------------------------------------------------- */

                if ( m2.IsRetraced_LtX_byM( 61.8, m3 ) )
                {
                    // ![](5A63B73801EA7E080F64A3E652BF47B2.png;;;0.03590,0.03100)
                    m1.AddStructureLabel( StructureLabelEnum.L5 );

                    if ( m0.Overlap( m2_ ) )
                    {
                        if ( m1.Is3Wave() )
                        {
                            m1.DropStructureLabel( StructureLabelEnum.L5 );
                        }

                        m1.AddStructureLabel( StructureLabelEnum.L3 );
                    }
                }

                if ( m2.IsRetraced_AtLeastX_byM( 61.8, m3 ) )
                {
                    // ![](A5F62D827B78E845AB31ECC94E31C4C0.png;;;0.02773,0.02045)
                    m1.AddStructureLabel( StructureLabelEnum.L5 );
                }

                /* -------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *  If m1 (plus one time unit) is completely retraced in the same amount of time (or less) that it took to form, 
                 *  a Trending Impulse pattern may have completed with m1; place ":L5H at the end of m1.
                 * 
                 * ------------------------------------------------------------------------------------------------------------------------------------------- */

                if ( m1.Plus1TU_IsCompletelyRetraced_Faster_byM( m2 ) )
                {
                    m1.WavePattern |= WavePattern.TrendingImpulse;
                    m1.AddStructureLabel( StructureLabelEnum.L5 );
                }

                /* -------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *  If m1 (plus one time unit) is completely retraced in the same amount of time (or less) that it took to form and 
                 *  m3 is shorter than m2 and 
                 *  m2 (or m2 through m4) approaches (or exceeds) the beginning of m(-3) within a time frame which is 50% (or less) of that taken by m(-3) through m1 and 
                 *  m0 and m(-2) share some of the same price territory, 
                 *  
                 *          then m1 may have completed a Terminal Impulse pattern; 
                 *          add "(:L3)" to the Structure list at the end of m1. 
                 * 
                 * If m3 is between 61.8-100% (exclusive) of m2 and ":L3" is used as the preferred Structure label, 
                 * 
                 *          m2 is probably an x-wave OR the Terminal pattern which completed with m1 is within a larger Triangle; 
                 *          place "x:c3?" at the end of m2.
                 * 
                 * ------------------------------------------------------------------------------------------------------------------------------------------- */

                if ( 
                        m1.Plus1TU_IsCompletelyRetraced_Faster_byM( m2 ) && 
                        m3.IsShorterThan( m2 ) && 
                        m0.Overlap( m2_ ) 
                    )
                {                    
                    var m3_tom1  = m3_.Combine( m2_, m1_ , m0, m1 );

                    if ( m2.CloseOrExceedX_TimeLTEy( m3_.Begin, 0.5 * m3_tom1.TimeUnit ) )
                    {
                        m1.WavePattern |= WavePattern.TerminalImpulse;
                        m1.AddStructureLabel( StructureLabelEnum.L3_LESSLIKELY );
                    }

                    var m4     = m3.GetNext( );
                    var m2Tom4 = m2.Combine( m3, m4 );

                    if ( m2Tom4.CloseOrExceedX_TimeLTEy( m3_.Begin, 0.5 * m3_tom1.TimeUnit ) )
                    {
                        m1.WavePattern |= WavePattern.TerminalImpulse;
                        m1.AddStructureLabel( StructureLabelEnum.L3_LESSLIKELY );
                    }

                    if ( m3.IsBtw_XYInclusive_ofM( 61.8, 100, m2 ) )
                    {
                        if ( m1.PreferableStructureIs( StructureLabelEnum.L3 ) )
                        {
                            m1.WavePattern |= WavePattern.ContractingTriangle;
                            m2.PotentialWave = ElliottWaveEnum.WaveX;
                            m2.AddStructureLabel( StructureLabelEnum.x_c3_MAYBE );
                        }
                    }
                }

                /* -------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *  If m1 is completely retraced slower than it took to form and 
                 *  m2 does not exceed the end of m(-2) and 
                 *  m(-1) is at least 61.8% of m1 and 
                 *  m(-2) is shorter than m(-1), 
                 *  
                 *          m1 may be wave-a of a Flat pattern which concludes a Complex Correction and 
                 *          m0 is the x-wave of the pattern; 
                 *          place ":F3" at the end of m1 and 
                 *          "x:c3?" at the end of m0.
                 * 
                 * ------------------------------------------------------------------------------------------------------------------------------------------- */

                if ( m1.IsCompletelyRetraced_Slower_byM( m2 ) && !m2.Exceed( m2_.End ) && m1_.IsAtLeastX_OfM( 61.8, m1 ) && m2_.IsShorterThan( m1_ ) )
                {
                    m1.PotentialWave = ElliottWaveEnum.WaveA;
                    m1.WavePattern |= WavePattern.Flat;
                    m0.PotentialWave = ElliottWaveEnum.WaveX;
                    m1.AddStructureLabel( StructureLabelEnum.F3 );
                    m0.AddStructureLabel( StructureLabelEnum.x_c3_MAYBE );
                }
            }
        }

        private void Rule6ConditionB( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            // ![](34DFFFAE744E9D5B54A1C403C9843228.png;;;0.03336,0.02356)
            if ( m1 == null || m2 == null || m3 == null )
                return;

            var first3Pips = m3.First3MonowavesPips( );

            if ( m3.MonoWavesCount > 3 )
            {
                var m2First3Overm1 = m1.Retracement( first3Pips );

                if ( m2First3Overm1 <= 61.8 )
                {
                    m2.MainWaveType = WaveType.ComplexCorrection;
                    m2.WavePattern |= WavePattern.MissingXWaves | WavePattern.FailureImpulse5th;
                    m2.PotentialWave = ElliottWaveEnum.WaveX;
                    m2.MissingXWavePos = WavePosition.Center;
                    m1.MissingXWaves = StructureLabelEnum.s5_MAYBE | StructureLabelEnum.F3_MAYBE;

                    m1.AddStructureLabel( StructureLabelEnum._5 | StructureLabelEnum.s5 );
                }

                if ( m2First3Overm1 > 61.8 )
                {
                    m2.WavePattern |= WavePattern.Flat;
                    m2.PotentialWave = ElliottWaveEnum.WaveA;

                    m1.WavePattern |= WavePattern.FailureImpulse5th;
                    m1.PotentialWave = ElliottWaveEnum.Wave3;

                    m1.AddStructureLabel( StructureLabelEnum.F3 | StructureLabelEnum._5 );
                }
            }

            if ( m3.MonoWavesCount <= 3 )
            {
                /* -------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *  If m1 is less than or equal to m0 in time or 
                 *  m1 is less than or equal to m2 in time and 
                 *  m(-2) is shorter than m(-1), 
                 *  
                 *          m1 may be the x-wave of a Complex Correction; place "x:c3" at the end of m1.
                 * 
                 * ------------------------------------------------------------------------------------------------------------------------------------------- */

                if ( 
                        ( m1.TimeUnit <= m0.TimeUnit || m1.TimeUnit <= m2.TimeUnit ) && 
                          m2_.IsShorterThan( m1_ ) 
                   )
                {
                    m1.WavePattern |= WavePattern.AnyComplexCorrection;
                    m1.PotentialWave = ElliottWaveEnum.WaveX;

                    m1.AddStructureLabel( StructureLabelEnum.x_c3 );
                }

                /* -------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *  If m1 takes the same amount of time (or more) as m0 or m1 takes the same amount of time (or more) as m2 and m0 is close to 161.8% of m1, m1 may be part of a Zigzag or Impulse pattern, place ":F3" at the end of m1.
                 * 
                 * ------------------------------------------------------------------------------------------------------------------------------------------- */

                if ( 
                        ( m1.TimeUnit >= m0.TimeUnit || m1.TimeUnit >= m2.TimeUnit ) && 
                          m0.IsApproxX_OfM( 161.8, m1 ) 
                   )
                {
                    m1.WavePattern |= WavePattern.ZigZag | WavePattern.TrendingImpulse;
                    m1.AddStructureLabel( StructureLabelEnum.F3 );
                }

                /* -------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *  If m1 (plus one time unit) is completely retraced by m2 in the same amount of time (or less) that m1 took to form and 
                 *  m2 is retraced less than 61.8% OR more than 100% in a period of time equal to (or less) than that taken by m2 and 
                 *  m(-1) is at least 61.8% of m1 in price and time and 
                 *  if m1 is a Compacted pattern, make sure no part of m1 moved beyond the beginning of m1 during m1's formation, 
                 *  
                 *          then there is a chance m1 completed the C-wave of a Flat; place ":L5" at the end of m1 to show this possibility.
                 * 
                 * ------------------------------------------------------------------------------------------------------------------------------------------- */

                if ( 
                        m1.Plus1TU_IsCompletelyRetraced_Faster_byM( m2 ) && 
                        m2.IsRetracedLtX_OrGtY_inLETime_byM( 61.8, 100, m3 ) && 
                        m1_.IsAtLeastX_PriceTime_OfM( 61.8, m1 ) 
                   )
                {
                    m1.WavePattern |= WavePattern.Flat;
                    m1.PotentialWave = ElliottWaveEnum.WaveC;
                    m1.AddStructureLabel( StructureLabelEnum.L5 );
                }

                /* -------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *  If m1 (plus one time unit) is completely retraced by m2 in the same amount of time m1 took to form (or less) and 
                 *  m2 is retraced less than 61.8% and 
                 *  m(-1) is at least 61.8% of m0 in price and time, 
                 *  
                 *          there is a chance m1 completed a Contracting Triangle or 
                 *          one of several Flat variations [depending on the length of m(-1)]; 
                 *          place ":L3/:L5" at the end of m1 to show these two possibilities (respectively). 
                 * 
                 * If m1 is a polywave and part of m1 exceeds m1's beginning, drop ":L5" as a possibility.
                 * 
                 * ------------------------------------------------------------------------------------------------------------------------------------------- */

                if ( 
                        m1.Plus1TU_IsCompletelyRetraced_Faster_byM( m2 ) && 
                        m2.IsRetraced_LtX_byM( 61.8, m3 ) && 
                        m1_.IsAtLeastX_PriceTime_OfM( 61.8, m0 ) 
                   )
                {
                    m1.WavePattern |= WavePattern.ContractingTriangle | WavePattern.Flat;
                    m1.AddStructureLabel( StructureLabelEnum.L3 | StructureLabelEnum.L5 );
                }

                /* -------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *  If m1 is completely retraced slower than it took to form and 
                 *  m2 is composed of three or more monowaves and 
                 *  m2 is longer than both m(-1) and m0, 
                 *  
                 *          then m1 may be one of the middle legs of a Triangle; place ":c3" at the end of m1
                 * 
                 * ------------------------------------------------------------------------------------------------------------------------------------------- */

                if ( 
                        m1.IsCompletelyRetraced_Slower_byM( m2 ) && 
                        m2.MonoWavesCount >= 3 && 
                        m2.IsLongerThan( m1_, m0 ) 
                   )
                {
                    m1.WavePattern |= WavePattern.ContractingTriangle;
                    m1.AddStructureLabel( StructureLabelEnum.c3 );
                }

                /* -------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *  If m2 (plus one time unit) is completely retraced in the same amount of time (or less) that it took to form and 
                 *  m0, when compared to m(-2) and m2, is not the shortest of the three and 
                 *  the market approaches (or exceeds) the beginning of m(-2) in a period of time 50% (or less) of that taken by m(-2) through m2, 
                 *  
                 *          a Terminal pattern may have concluded with m2; 
                 *          add ":sL3" to any current Structure list present at the end of m1.
                 * 
                 * ------------------------------------------------------------------------------------------------------------------------------------------- */

                var m4 = m3.GetNext( );
                if ( m4 == null )
                    return;

                var m5 = m4.GetNext( );
                if ( m5 == null )
                    return;
                
                var m2_tom2 = m2_.Combine( m1_, m0, m1, m2 );
                var m3Tom5  = m3.Combine( m4, m5 );

                if ( 
                        m2.Plus1TU_IsCompletelyRetraced_Faster_byM( m3 ) && 
                        m0.IsNotTheShortest( m2_,  m2 ) 
                   ) 
                {
                
                    if ( m3.CloseOrExceedX_TimeLTEy( m2_.Begin, 0.5 * m2_tom2.TimeUnit ) || m3Tom5.CloseOrExceedX_TimeLTEy( m2_.Begin, 0.5 * m2_tom2.TimeUnit ) )
                    {
                        m2.WavePattern |= WavePattern.TerminalPattern;
                        m1.AddStructureLabel( StructureLabelEnum.sL3 );
                    }
                }

                /* -------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *  If m3 is between 101%-161.8% of m2, an Expanding Triangle may be forming; 
                 *  
                 *  if there is an ":F3" in m1's Structure list, add brackets around it to indicate ":c3" is the better choice.
                 * 
                 * ------------------------------------------------------------------------------------------------------------------------------------------- */

                if ( m3.IsBtw_XYInclusive_ofM( 101, 161.8, m2 ) )
                {
                    m2.WavePattern |= WavePattern.ExpandingTriangle;

                    if ( m1.StructureLabel.HasFlag( StructureLabelEnum.F3 ) )
                    {
                        m1.DropStructureLabel( StructureLabelEnum.F3 );
                        m1.AddStructureLabel( StructureLabelEnum.F3_LESSLIKELY );
                    }
                }
            }
        }

        private void Rule6ConditionC( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            // ![](92AF93273C67DDFABC1A65027D016CC6.png;;;0.02274,0.02319)
            m1.AddStructureLabel( StructureLabelEnum.F3 );

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m1 (plus one time unit) is completely retraced in the same amount of time (or less) that it took to form and 
             *  m2 is retraced less than 61.8% and during a period equal to the time of m0, 
             *  m2 exceeded the length of m0 and 
             *  m(-1) is between 61.8-161.8% of m0 and 
             *  m2's price coverage is larger and its movement more vertical than that exhibited by m0, 
             *  
             *          m1 may have completed a Contracting Triangle or 
             *          a severe C-Failure Flat; 
             *          place ":L3/(:L5)" at the end of m1 to show these two possibilities (respectively).
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( m1.Plus1TU_IsCompletelyRetraced_Faster_byM( m2 ) && m2.IsRetraced_LtX_EqZtime_byM( 61.8, m0.TimeUnit, m3 ) && m2.IsLongerThan( m0 ) && m1_.IsBtw_XYInclusive_ofM( 61.8, 161.8, m0 ) )
            {
                if ( m2.IsLongerAndMoreVerticalThan( m0 ) )
                {
                    m1.WavePattern |= WavePattern.ContractingTriangle | WavePattern.CFailureFlat;
                    m1.AddStructureLabel( StructureLabelEnum.L3 | StructureLabelEnum.L5_LESSLIKELY );
                }
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m2 (plus one time unit) is completely retraced in the same amount of time (or less) that it took to form and 
             *  m(-1) and m1 share some of the same price range and 
             *  m0, when compared to m(-2) and m2, is not the shortest of the three and 
             *  the market approaches (or exceeds) the beginning of m(-2) in a period of time 50% (or less) of that taken by m(-2) through m2, 
             *  
             *          then a Terminal pattern may have concluded with m2; add ":sL3" to the end of m1.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            var m4 = m3.GetNext( );

            if ( m4 == null )
                return;

            var m5 = m4.GetNext( );
            if ( m5 == null )
                return;
            
            var m2_tom2 = m2_.Combine( m1_, m0, m1, m2 );
            var m3Tom5  = m3.Combine( m4, m5 );

            if ( 
                    m2.Plus1TU_IsCompletelyRetraced_Faster_byM( m3 ) && 
                    m1_.Overlap( m1 ) && 
                    m0.IsNotTheShortest( m2_, m2 ) 
               ) 
            {
            
                if ( m3.CloseOrExceedX_TimeLTEy( m2_.Begin, 0.5 * m2_tom2.TimeUnit ) || m3Tom5.CloseOrExceedX_TimeLTEy( m2_.Begin, 0.5 * m2_tom2.TimeUnit ) )
                {
                    m2.WavePattern |= WavePattern.TerminalPattern;
                    m1.AddStructureLabel( StructureLabelEnum.sL3 );
                }
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m3 is between 101%-161.8% of m2, there is the unlikely possibility that an Expanding Triangle is forming; add "(:c3)" to the Structure list.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( m3.IsBtw_XYInclusive_ofM( 101, 161.8, m2 ) )
            {
                m1.WavePattern |= WavePattern.ExpandingTriangle;
                m1.AddStructureLabel( StructureLabelEnum.c3_LESSLIKELY );
            }

        }

        private void Rule6ConditionD( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            if ( m0 == null || m1 == null || m2 == null )
                return;

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m0 (minus one time unit) is less than or equal to m1 in time OR 
             *  if m2 (minus one time unit) is less than or equal to m1 in time, 
             *  and as long as m1 is not simultaneously less in time than both m0 and m2, 
             *  
             *          m1 is either the first phase of a larger correction or the 
             *          completion of a correction within a Zigzag or Impulse pattern; 
             *          place ":F3" at the end of m1.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( ( ( m0.TimeUnit - 1 ) <= m1.TimeUnit ) || ( ( m2.TimeUnit - 1 ) <= m1.TimeUnit ) )
            {
                if ( ( m1.TimeUnit > m0.TimeUnit ) || ( m1.TimeUnit > m2.TimeUnit ) )
                {
                    m1.WavePattern |= WavePattern.AnyCorrectivePattern | WavePattern.ZigZag | WavePattern.TrendingImpulse;
                    m1.AddStructureLabel( StructureLabelEnum.F3 );
                }
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m2 is retraced less than 61.8% and 
             *  the combined time of m2 through m4 is equal to or less than that consumed by m0 and 
             *  m2-m4's combined price coverage is larger and their movement more vertical than that exhibited by m0, 
             *  
             *          there is a small chance that m1 completed a Contracting Triangle or a severe C-Failure Flat; 
             *          place "(:L3)/[:L5]" at the end of m1 to show these two possibilities (respectively).
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            var m4 = m3.GetNext( );
            if ( m4 == null )
                return;

            var m5 = m4.GetNext( );
            if ( m5 == null )
                return;

            if ( m2.IsRetraced_LtX_byM( 61.8, m3 ) )
            {
                var m2Tom4 = m2.Combine( m3, m4 );

                if ( m2Tom4.IsLongerAndMoreVerticalThan( m0 ) )
                {
                    m1.WavePattern |= WavePattern.ContractingTriangle | WavePattern.CFailureFlat;
                    m1.AddStructureLabel( StructureLabelEnum.L3_LESSLIKELY | StructureLabelEnum.L5_RARE );
                }
            }


            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m2 (plus one time unit) is completely retraced in the same amount of time (or less) that it took to form and 
             *  m(-1) and m1 share some of the same price range and 
             *  m0, when compared to m(-2) and m2, is not the shortest of the three and 
             *  the market approaches (or exceeds) the beginning of m(-2) in a period of time 50% (or less) of that taken by m(-2) through m2, 
             *  
             *          then a Terminal pattern may have concluded with m2; add ":sL3" to the end of m1.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */            
            var m2_tom2 = m2_.Combine( m1_, m0, m1, m2 );
            var m3Tom5  = m3.Combine( m4, m5 );

            if ( 
                    m2.Plus1TU_IsCompletelyRetraced_Faster_byM( m3 ) && 
                    m1_.Overlap( m1 ) && 
                    m0.IsNotTheShortest( m2_, m2 ) 
               ) 
            {                
                if ( m3.CloseOrExceedX_TimeLTEy( m2_.Begin, 0.5 * m2_tom2.TimeUnit ) || m3Tom5.CloseOrExceedX_TimeLTEy( m2_.Begin, 0.5 * m2_tom2.TimeUnit ) )
                {
                    m2.WavePattern |= WavePattern.TerminalPattern;
                    m1.AddStructureLabel( StructureLabelEnum.sL3 );
                }
            }


        }
    }
}
