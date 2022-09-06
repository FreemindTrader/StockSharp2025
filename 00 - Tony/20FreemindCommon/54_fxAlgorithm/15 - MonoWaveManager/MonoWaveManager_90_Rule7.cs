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
        private void Rule7( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            // ![](9269F08A8EF53F6E13B5007B2C0C8773.png;;;0.02205,0.01768)
            if ( m0 == null )
                return;

            double m0m1Ratio = m0.Over( m1 );

            switch ( m0m1Ratio )
            {
                case double p when ( p < 100 ):
                {
                    Rule7ConditionA( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;


                case double p when ( p >= 100 && p < 161.8 ):
                {
                    Rule7ConditionB( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;

                case double p when ( p >= 161.8 && p <= 261.8 ):
                {
                    Rule7ConditionC( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;

                case double p when ( p > 261.8 ):
                {
                    Rule7ConditionD( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;
            }
        }

        private void Rule7ConditionA( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            if ( m0 == null || m1 == null || m2 == null )
                return;

            var first3Pips = m2.First3MonowavesPips( );

            if ( m2.MonoWavesCount > 3 )
            {
                var m2First3Overm1 = m1.Retracement( first3Pips );

                if ( m2First3Overm1 <= 61.8 )
                {
                    m1.MainWaveType = WaveType.ComplexCorrection;
                    m1.WavePattern |= WavePattern.MissingXWaves | WavePattern.FailureImpulse5th;
                    m1.PotentialWave = ElliottWaveEnum.WaveX;
                    m1.MissingXWavePos = WavePosition.Center;
                    m1.MissingXWaves = StructureLabelEnum._5_MAYBE | StructureLabelEnum.F3_MAYBE;

                    m1.AddStructureLabel( StructureLabelEnum._5 | StructureLabelEnum.s5 );
                    m2.AddStructureLabel( StructureLabelEnum.x_c3_MAYBE );
                }

                if ( m2First3Overm1 > 61.8 )
                {
                    m1.WavePattern |= WavePattern.Flat | WavePattern.FailureImpulse5th;
                    m1.PotentialWave = ElliottWaveEnum.WaveA;
                    m1.AddStructureLabel( StructureLabelEnum.F3 | StructureLabelEnum._5 );
                }
            }

            if ( m2.MonoWavesCount <= 3 )
            {
                // ![](3F65A51E7945C2CD3083A3BB8C0334A5.png;;;0.03470,0.02307)
                m1.AddStructureLabel( StructureLabelEnum.L5 );
            }

            if ( m2.IsRetraced_LtX_byM( 61.8, m3 ) && m2_.IsShorterThan( m1_ ) && m2_.Overlap( m0 ) )
            {
                m1.WavePattern |= WavePattern.TerminalPattern;
                m1.AddStructureLabel( StructureLabelEnum.L3_LESSLIKELY );
            }
        }

        private void Rule7ConditionB( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            // ![](BAC21FBF948A6265F0FEB76E965CDA0B.png;;;0.04061,0.03237)
            if ( m0 == null || m1 == null || m2 == null )
                return;

            var first3Pips = m3.First3MonowavesPips( );

            if ( m3.MonoWavesCount > 3 )
            {
                var m2First3Overm1 = m1.Retracement( first3Pips );

                if ( m2First3Overm1 <= 61.8 )
                {
                    m2.MainWaveType = WaveType.ComplexCorrection;
                    m2.WavePattern |= WavePattern.MissingBWaves | WavePattern.FailureImpulse5th;
                    m3.PotentialWave = ElliottWaveEnum.WaveX;
                    m2.MissingXWaves = StructureLabelEnum._5_MAYBE | StructureLabelEnum.x_c3_MAYBE | StructureLabelEnum.b_F3_MAYBE;

                    m1.AddStructureLabel( StructureLabelEnum.F3 | StructureLabelEnum.c3 | StructureLabelEnum.L3 | StructureLabelEnum.L5 );

                    var first5MonosPips = m3.FirstNthMonowavesPips( 5 );
                    var m2Length618     = m2.Pips * 0.618;

                    if ( first5MonosPips < m2Length618 )
                    {
                        m1.DropStructureLabel( StructureLabelEnum.F3 );
                        m2.WavePattern &= ~WavePattern.FailureImpulse5th;
                    }

                    /* -------------------------------------------------------------------------------------------------------------------------------------------
                     * 
                     *  To account for all of these possibilities, 
                     *  place "x:c3?" at the end of the first and second monowave (immediately after the end of m2) which are moving in the same direction as m1.
                     *  
                     *  TODO: This one is difficult to code, do I add a new monoWave to the b-Tree                     
                     * ------------------------------------------------------------------------------------------------------------------------------------------- */

                }

                if ( m2First3Overm1 > 61.8 )
                {
                    m1.WavePattern |= WavePattern.Flat | WavePattern.FailureImpulse5th;
                    m1.PotentialWave = ElliottWaveEnum.WaveA;
                    m1.AddStructureLabel( StructureLabelEnum.F3 | StructureLabelEnum._5 );
                }
            }

            if ( m3.MonoWavesCount <= 3 )
            {
                /* -------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *  If m0 is at least 61.8% of m1 and 
                 *  m3 is between 100-261.8% of m2, 
                 *  
                 *          m1 may be part of an Expanding Triangle; 
                 *          place ":c3" at the end of m1. 
                 * 
                 * If m4 is more than 61.8% of m3, add ":F3" to m1's Structure list.
                 * 
                 * ------------------------------------------------------------------------------------------------------------------------------------------- */

                if ( m0.IsAtLeastX_OfM( 61.8, m1 ) && m3.IsBtw_XYInclusive_ofM( 100, 261.8, m2 ) )
                {
                    m1.WavePattern |= WavePattern.ExpandingTriangle;
                    m1.AddStructureLabel( StructureLabelEnum.c3 );
                }

                var m4 = m3.GetNext( );

                if ( m4 == null )
                    return;

                if ( m4.IsMoreThanX_ofM( 61.8, m3 ) )
                {
                    // ![](B360C94C3BF3D97A95568F8CB048BB83.png;;;0.02576,0.02289)
                    m1.AddStructureLabel( StructureLabelEnum.F3 );
                }

                /* -------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *  If m1 is not much more than 61.8% of m0 and 
                 *  m2 is retraced less than 61.8% OR more than 100% in a period of time equal to or less than that taken by m2 and 
                 *  the time it took for m2 to equal the length of m0 was equal to or less than that consumed by m0 and 
                 *  m2's price coverage is more vertical than m0, 
                 *  
                 *          there is a good chance m1 completed a Contracting Triangle or a C-Failure Flat; 
                 *          place ":L3/:L5" at the end of m1 to show these two possibilities (respectively).
                 * 
                 * ------------------------------------------------------------------------------------------------------------------------------------------- */

                if ( 
                        m1.IsNotMuchMoreThanX_ofM( 61.8, m0 ) && 
                        m2.IsRetracedLtX_OrGtY_inLE_ZTime_byM( 61.8, 100, m2.TimeUnit, m3 ) && 
                        m2.TimeToEqualX_LessThan( m0 ) && 
                        m2.IsMoreVerticalThan( m0 )
                   )
                {
                    m1.WavePattern |= WavePattern.ContractingTriangle | WavePattern.CFailureFlat;
                    m1.AddStructureLabel( StructureLabelEnum.L3 | StructureLabelEnum.L5 );
                }

                /* -------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *  If m2 is retraced at least 61.8%, but less than 100%, 
                 *          an Elongated Flat is the most likely pattern which concluded with m2; 
                 *          place ":c3" at the end of m1.
                 * 
                 * ------------------------------------------------------------------------------------------------------------------------------------------- */

                if ( m2.IsRetraced_AtLeastX_NoMoreThanY_byM( 61.8, 100, m3 ) )
                {
                    m2.WavePattern |= WavePattern.ElongatedFlat;
                    m1.AddStructureLabel( StructureLabelEnum.c3 );
                }

                var m5 = m4.GetNext( );
                if ( m5 == null )
                    return;

                /* -------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *  If m2 (plus one time unit) is completely retraced in the same amount of time (or less) that it took to form, 
                 *          a Trending Impulse pattern may have concluded with m2; place ":L5" at the end of m1's Structure list. 
                 *          
                 *  If m(-1) is shorter than m0 and 
                 *  m0, when compared to m(-2) and m2, is not the shortest of the three and 
                 *  the market approaches (or exceeds) the beginning of m(-2) in a period of time 50% or less of that consumed by m(-2) through m2, 
                 *          a 5th Extension Terminal may have completed with m2; 
                 *          add ":sL3" to the list of possibilities at the end of m1.
                 * 
                 * ------------------------------------------------------------------------------------------------------------------------------------------- */
                if ( m2.Plus1TU_IsCompletelyRetraced_Faster_byM( m3 ) )
                {
                    m2.WavePattern |= WavePattern.TrendingImpulse;
                    m1.AddStructureLabel( StructureLabelEnum.L5 );
                    
                    var m2_Tom2 = m2_.Combine( m1_, m0, m1, m2 );
                    var m3Tom5  = m3.Combine( m4, m5 );

                    if ( 
                            m1_.IsShorterThan( m0 ) && 
                            m0.IsNotTheShortest( m2_, m2 ) 
                       ) 
                    {
                        

                        if ( m3.CloseOrExceedX_TimeLTEy( m2_.Begin, 0.5 * m2_Tom2.TimeUnit ) || m3Tom5.CloseOrExceedX_TimeLTEy( m2_.Begin, 0.5 * m2_Tom2.TimeUnit ) )
                        {
                            m1.AddStructureLabel( StructureLabelEnum.sL3_MAYBE );
                            m2.WavePattern |= WavePattern.TerminalImpulse;
                        }
                    }
                }
            }
        }

        private void Rule7ConditionC( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            if ( m0 == null || m1 == null || m2 == null )
                return;

            if ( m1.TimeUnit >= m0.TimeUnit && m1.TimeUnit >= m2.TimeUnit )
            {
                m1.AddStructureLabel( StructureLabelEnum.F3 );
            }

            var m4_ = m3_.GetPrevious( );

            if( m4_ == null )
                return;

            if ( m2.TimeToEqualX_LessThan( m0 ) && m2.IsLongerAndMoreVerticalThan( m0 ) && m4_.IsLongerThan( m2_ ) )
            {
                m1.WavePattern |= WavePattern.ContractingTriangle;
                m1.AddStructureLabel( StructureLabelEnum.L3 );
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If the time it took for m2 to equal the length of m0 was equal to (or less) than that taken by m0 and 
             *  m2's price coverage is larger and more vertical than m0 and 
             *  m(-2) is at least 161.8% of m0 and 
             *  m(-2) is at least 61.8% of m2 and 
             *  one of m(-1)'s Structure possibilities is an ":F3," m1 may have completed an Irregular Failure Flat; place ":L5" at the end of m1.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( m2.TimeToEqualX_LessThan( m0 ) && m2.IsLongerAndMoreVerticalThan( m0 ) && m2_.IsAtLeastX_OfM( 161.8, m0 ) && m2_.IsAtLeastX_OfM( 61.8, m2 ) )
            {
                if ( m1_.StructureLabel.HasFlag( StructureLabelEnum.F3 ) )
                {
                    m1.WavePattern |= WavePattern.IrregularFailureFlat;
                    m1.AddStructureLabel( StructureLabelEnum.L5 );
                }
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m2 (plus one time unit) is completely retraced in the same amount of time (or less) that it took to form and 
             *  the market approaches (or exceeds) the beginning of m(-2) in a period of time 50% (or less) of that consumed by m(-2) through m2 and 
             *  m0 is longer than m(-2), an Expanding Terminal Impulse pattern may have concluded with m2; add ":sL3" to the end of m1.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            var m4 = m3.GetNext( );
            if ( m4 == null )
                return;

            var m5 = m4.GetNext( );
            if ( m5 == null )
                return;

            if ( m2.Plus1TU_IsCompletelyRetraced_Faster_byM( m3 ) )
            {                
                var m2_Tom2 = m2_.Combine( m1_, m0, m1, m2 );

                var m3Tom5  = m3.Combine( m4, m5 );

                if ( m3.CloseOrExceedX_TimeLTEy( m2_.Begin, 0.5 * m2_Tom2.TimeUnit ) || m3Tom5.CloseOrExceedX_TimeLTEy( m2_.Begin, 0.5 * m2_Tom2.TimeUnit ) )
                {
                    if ( m0.IsLongerThan( m2_ ) )
                    {
                        m1.AddStructureLabel( StructureLabelEnum.sL3 );
                        m2.WavePattern |= WavePattern.TerminalImpulse;
                    }
                }
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m1 (plus one time unit) is completely retraced in the same amount of time (or less) that it took to form and 
             *  m2 is at least 161.8% of m0 and m1 breaks through a line drawn across the end of m(-3) and m(-1), 
             *  
             *          a Running Correction could have completed with m1; place :L5 at the end of m1.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( m1.Plus1TU_IsCompletelyRetraced_Faster_byM( m2 ) && m2.IsAtLeastX_OfM( 161.8, m0 ) )
            {
                if ( m1.BreakTrendLineBtwXY( m3_, m1_ ) )
                {
                    m1.WavePattern |= WavePattern.RunningCorrection;
                    m1.AddStructureLabel( StructureLabelEnum.L5 );
                }
            }
        }

        private void Rule7ConditionD( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            if ( m0 == null || m1 == null || m2 == null )
                return;

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m0 (minus one time unit) is less than or equal in time to m1 OR if 
             *  m2 (minus one time unit) is less than or equal in time to m1, and as long as 
             *  m1 is not simultaneously less in time than both m0 and m2, m1 may be part of a Zigzag or Impulse pattern, place ":F3" at the end of m1.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( ( 
                    ( m0.TimeUnit - 1 <= m1.TimeUnit ) || 
                    ( m2.TimeUnit - 1 <= m1.TimeUnit ) ) && 
                    ( m1.TimeUnit > m0.TimeUnit || m1.TimeUnit > m2.TimeUnit ) 
               )
            {
                m1.WavePattern |= WavePattern.ZigZag | WavePattern.TrendingImpulse;
                m1.AddStructureLabel( StructureLabelEnum.F3 );
            }

            var m4 = m3.GetNext( );

            if ( m4 == null )
                return;

            var m5 = m4.GetNext( );

            if ( m5 == null )
                return;

            var m2_Tom0 = m2_.Combine( m1_, m0 );
            var m2_Tom2 = m2_.Combine( m1_, m0, m1, m2 );
            // ![](9E4116B32E0EFD0C75971569310EF9EE.png;;;0.02176,0.02138)
            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m1 takes the same amount of time (or less) as  m0 and/or 
             *  m1 takes the same amount of time (or less) as m2 and 
             *  m(-2) is 161.8% or more of m(-1) and 
             *  m(-1) is shorter than m0 and 
             *  m1 is less than 61.8% of the distance from the beginning of m(-2) through the end of m0 and 
             *  if m3 is longer than m2, then make sure m4 is shorter than m3 and 
             *  if m3 is longer than m2, then make sure that 61.8% of m(-2) through m2 is retraced before the end of m2 is exceeded, 
             *  
             *          THEN m1 may be the x-wave of a Double Zigzag or a Complex correction which begins with a Zigzag; place "x:c3" at the end of m1.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( 
                    m1.TimeUnit < m0.TimeUnit || 
                    m1.TimeUnit < m2.TimeUnit  && 
                    m2_.IsAtLeastX_OfM( 161.8, m1_ ) && 
                    m1_.IsShorterThan( m0 ) &&
                    m1.IsLessThanX_OfM( 61.8, m2_Tom0 ) &&
                    m3.IsLongerThan( m2 ) && 
                    m4.IsShorterThan( m3 )
               )
            {
                var length = m2_Tom2.Pips * 0.618;

                ref var m3BeginBar = ref _historicBarsRepo.GetBarByIndex( m3.BeginBarIndex );

                var m3_618Price = m3BeginBar.PeakTroughValue + length;

                if ( m2.BeginBrokenBy( m3_618Price ) )
                {
                    m1.WavePattern |= WavePattern.DoubleZigZag | WavePattern.AnyComplexCorrection;
                    m1.PotentialWave = ElliottWaveEnum.WaveX;
                    m1.AddStructureLabel( StructureLabelEnum.x_c3 );
                }
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m1 takes the same amount of time (or less) as m0 OR 
             *  m1 takes the same amount of time (or less) as m2 and 
             *  m0 is between 100-161.8% of m(-1) and 
             *  m2 is not more than 161.8% of m0 and 
             *  m4 is at least 38.2% of m2 and 
             *  if m3 is longer than m2, then make sure m4 is shorter than m3, 
             *  
             *          THEN m1 may be the x-wave of a Complex correction which starts with a Flat and 
             *          ends with a Flat or Triangle; put "x:c3" at the end of m1.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( 
                    m1.TimeUnit <= m0.TimeUnit  || 
                    m1.TimeUnit <= m2.TimeUnit && 
                    m0.IsBtw_XYInclusive_ofM( 100, 161.8, m1_ ) && 
                    m2.IsLessThanX_OfM( 161.8, m0 ) &&
                    m4.IsAtLeastX_OfM( 38.2, m2 ) && 
                    m3.IsLongerThan( m2 ) && 
                    m4.IsShorterThan( m3 )
                )
            {
                m1.WavePattern |= WavePattern.Flat | WavePattern.ContractingTriangle;
                m1.PotentialWave = ElliottWaveEnum.WaveX;
                m1.AddStructureLabel( StructureLabelEnum.x_c3 );
            }

            // ![](16E15BE457C553F3BDEC3EF158EF1C4E.png;;;0.03328,0.02788)
            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m1 is less than or equal to m0 in time and/or 
             *  m1 is less than or equal to m2 in time, 
             *          place ":c3" at the end of m1. 
             *          
             *  If m(-1) and m1 are almost equal in price or time or both (or related in either case by 61.8%) and 
             *  m(-1) is shorter than m0 and 
             *  when comparing the price lengths of m(-2), m0 and m2 you find that m0 is not the shortest of the three and 
             *  not one of the three is more than 161.8 of the next smaller, 
             *  
             *          then m1 may be part of a Complex Double Zigzag (which will involve one or two x-waves); 
             *          add an "x" in front of ":c3." 
             *                       
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( 
                    m1.TimeUnit <= m0.TimeUnit || 
                    m1.TimeUnit <= m2.TimeUnit 
               )
            {
                
                m1.AddStructureLabel( StructureLabelEnum.c3 );
            }

            if ( 
                    m1_.PriceTimeApproxEqual( m1 ) || m1_.IsApproxX_PriceTime_OfM( 61.8, m1 ) && 
                    m1_.IsShorterThan( m0 ) &&
                    m0.IsNotTheShortest( m2_, m2 )
               )
            {
                if ( 
                        ( m2_.IsLongerThan( m2 ) && m2_.IsLessThanX_OfM( 161.8, m0 ) && m0.IsLessThanX_OfM( 161.8, m2 ) ) ||
                        ( m2.IsLongerThan( m2_ ) && m2.IsLessThanX_OfM( 161.8, m0 ) && m0.IsLessThanX_OfM( 161.8, m2_ ) )
                    )
                {
                    m1.WavePattern |= WavePattern.DoubleZigZag;
                    m1.DropStructureLabel( StructureLabelEnum.c3 );
                    m1.AddStructureLabel( StructureLabelEnum.x_c3 );
                }                
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             *              
             *  If m0 is not the longest of the three mentioned above, the x-wave is probably at the end of m1, 
             *  but if there are other Structure label possibilities for m1 other than ":c3," 
             *          the x-wave could be at the end of m(-1) or m(3). 
             *          
             *  If m0 is the longest of m(-2), m0 and m2, 
             *          the x-wave may be "missing" in the center of m0, 
             *          mark the center of m0 with a dot and put "x:c3?" to the right of the dot and place ":s5" to the left of the dot; 
             *          m(-2), in this instance, would be the beginning of the pattern and m2 would be the end. 
             *          
             *  If m(-2) through m2 institute a Complex Correction with a "missing" x-wave, 
             *          the market should retrace between 61.8-100% of it before the next wave group (of the same Degree as the Complex Correction) begins. 
             *          
             *  If the "missing" x-wave Complex Collection is retraced less than 61.8% and 
             *  then the market exceeds the end of the Complex Correction, 
             *          either m(-2) through m2 does not make-up such a pattern or the Complex Correction is part of a Terminal Impulse pattern.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( m0.IsNotTheLongest( m2_, m2 ) )
            {
                m1.PotentialWave = ElliottWaveEnum.WaveX;

                if ( m1.StructureLabel != StructureLabelEnum.c3 )
                {
                    m1_.PotentialWave = ElliottWaveEnum.WaveX;
                    m3.PotentialWave = ElliottWaveEnum.WaveX;
                }
            }

            if ( m0.IsLongerThan( m2_ ) && m0.IsLongerThan( m2 ) )
            {
                m0.MissingXWavePos = WavePosition.Center;
                m0.MissingXWaves = StructureLabelEnum.x_c3_MAYBE | StructureLabelEnum.s5;

                var m3Tom5 = m3.Combine( m4, m5 );

                if ( 
                        m2_Tom2.IsRetraced_AtLeastX_NoMoreThanY_byM( 61.8, 100.01, m3 ) ||
                        m2_Tom2.IsRetraced_AtLeastX_NoMoreThanY_byM( 61.8, 100.01, m3Tom5 )
                    )
                {
                    m2_.WavePattern |= WavePattern.AnyComplexCorrection;
                    m1_.WavePattern |= WavePattern.AnyComplexCorrection;
                    m0.WavePattern |= WavePattern.AnyComplexCorrection;
                    m1.WavePattern |= WavePattern.AnyComplexCorrection;
                    m2.WavePattern |= WavePattern.AnyComplexCorrection;
                }
                else
                {
                    m2_.WavePattern |= WavePattern.TerminalImpulse;
                    m1_.WavePattern |= WavePattern.TerminalImpulse;
                    m0.WavePattern |= WavePattern.TerminalImpulse;
                    m1.WavePattern |= WavePattern.TerminalImpulse;
                    m2.WavePattern |= WavePattern.TerminalImpulse;
                }
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m1 (plus one time unit) is completely retraced in the same amount of time (or less) that it took to form and 
             *  m(-1) and m1 are equal in price and time (or related by 61.8% in either case) and 
             *  m2 is at least 161.8% of m0 and 
             *  m1 and m(-1) do not share any similar price range and 
             *  m2 is not retraced faster than it took to form, 
             *          then m1 may have completed a Running Correction; 
             *          place ":L5" at the end of m1. 
             * 
             * If m(-2) is less than 161.8% of m0 and 
             * m2 is retraced less than 61.8% and 
             * the ":L5" is used, m1 simultaneously terminates more than one Elliott pattern, each of consecutively larger degree.
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( 
                    m1.Plus1TU_IsCompletelyRetraced_Faster_byM( m2 ) && 
                    ( m1_.PriceTimeApproxEqual( m1 ) || m1_.IsApproxX_PriceTime_OfM( 61.8, m1 ) ) 
               )
            {
                if ( m2.IsAtLeastX_OfM( 161.8, m0 ) && !m1.Overlap( m1_ ) && m2.IsCompletelyRetraced_Slower_byM( m3 ) )
                {
                    m1.WavePattern |= WavePattern.RunningCorrection;
                    m1.AddStructureLabel( StructureLabelEnum.L5 );
                }

                if ( m2_.IsLessThanX_OfM( 161.8, m0 ) && m2.IsRetraced_LtX_byM( 61.8, m3 ) )
                {
                    if ( m1.StructureLabel.HasFlag( StructureLabelEnum.L5 ) )
                    {
                        m1.WavePattern |= WavePattern.TerminateMultiDegPattern;
                    }
                }
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m2 is retraced less than 61.8% and 
             *  m2's price coverage is larger and more vertical than m0 and 
             *  m(-1) is no more than 161.8% of m0 and 
             *  m(-1) and m1 share some of the same price range and 
             *  at least one of m0's Structure possibilities contains a ":3"(any variation), 
             *  there is a small chance m1 completed a Contracting Triangle; 
             *          place "(:L3)" at the end of m1. 
             * 
             * If m(-1) and m1 are equal (or related by 61.8%) in price or time or both and 
             * m(-1) and m1 share some of the same price range, 
             *          m1 may have completed an Irregular or C-Failure Flat; 
             *          place ":L5" at the end of m1.
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( 
                    m2.IsRetraced_LtX_byM( 61.8, m3 ) && 
                    m2.IsLongerAndMoreVerticalThan( m0 ) && 
                    m1_.IsNoMoreThanX_OfM( 161.8, m0 ) && 
                    m1_.Overlap( m1 ) 
               )
            {
                if ( m0.HasAnyVariationOf3( ) )
                {
                    m1.WavePattern |= WavePattern.ContractingTriangle;
                    m1.AddStructureLabel( StructureLabelEnum.L3_LESSLIKELY );
                }
            }

            if ( m1_.PriceTimeApproxEqual( m1 ) || m1_.IsApproxX_PriceTime_OfM( 61.8, m1 ) )
            {
                if ( m1_.Overlap( m1 ) )
                {
                    m1.WavePattern |= WavePattern.IrregularFailureFlat;
                    m1.AddStructureLabel( StructureLabelEnum.L5 );
                }
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m2 is retraced less than 61.8% and 
             *  m2's price coverage is between 61.8% and 161.8% of m0 and 
             *  m(-1) is shorter than m0 and 
             *  m(-1) is not more than 161.8% of m0, 
             *  
             *          m1 may be an "x-wave" of a Complex Corrective pattern; 
             *          make note of that on the chart next to m1 and 
             *          add "x:c3" to m1's Structure list if it is not already present.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( m2.IsRetraced_LtX_byM( 61.8, m3 ) && m2.IsBtw_XYInclusive_ofM( 61.8, 161.8, m0 ) && m1_.IsShorterThan( m0 ) && m1_.IsLessThanX_OfM( 161.8, m0 ) )
            {
                m1.PotentialWave = ElliottWaveEnum.WaveX;
                m1.AddStructureLabel( StructureLabelEnum.x_c3 );
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m2 (plus one time unit) is completely retraced in the same amount of time (or less) that it took to form and 
             *  m3 is not retraced more than 61.8% and 
             *  m(-1) is shorter than m0 and 
             *  part of m(-1) shares some of the same price range as m1 and 
             *  m0, when compared to m(-2) and m2, is not the shortest of the three and 
             *  m3, in a period of time 50% (or less) of that consumed m(-2) through m2, closely approaches (or exceeds) the beginning of m(-2), 
             *  
             *          a Terminal pattern may have concluded with m2; add ":sL3" to the Structure list at the end of m1.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */
            

            if ( 
                    m2.Plus1TU_IsCompletelyRetraced_Faster_byM( m3 ) && 
                    m3.IsLessThanX_OfM( 61.8, m4 ) && 
                    m1_.IsShorterThan( m0 ) && 
                    m1_.Overlap( m1 ) &&
                    m0.IsNotTheShortest( m2_, m2 ) &&
                    m3.CloseOrExceedX_TimeLTEy( m2_.Begin, 0.5 * m2_Tom2.TimeUnit )
               )
            {
                m1.AddStructureLabel( StructureLabelEnum.sL3_MAYBE );
                m2.WavePattern |= WavePattern.TerminalImpulse;
            }

        }
    }
}
