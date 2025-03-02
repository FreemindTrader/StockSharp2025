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
        // ![](52946EBD29C31AFF3E7F8E8FE6B34824.png;;;0.02466,0.02216)
        private void Rule1( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            if ( m0 == null )
                return;

            double m0m1Ratio = m0.Over( m1 );

            switch ( m0m1Ratio )
            {
                case double p when ( p < 61.8 ):
                {
                    Rule1ConditionA( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;

                case double p when ( p >= 61.8 && p < 100 ):
                {
                    Rule1ConditionB( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;


                case double p when ( p >= 100 && p <= 161.8 ):
                {
                    Rule1ConditionC( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;

                case double p when ( p > 161.8 ):
                {
                    Rule1ConditionD( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;
            }
        }

        /* -------------------------------------------------------------------------------------------------------------------------------------------
         * 
         *  this Condition is saying that MonoWaveGroup m0 and MonoWaveGroup m2 are not overlapping.
         * 
         * ![](F4D024B5F8A035E5E1E32E47B8661254.png;;;0.03906,0.03541)![](E441CB6584715C637EC455BF76EFFA91.png;;;0.04024,0.03433)
         * ------------------------------------------------------------------------------------------------------------------------------------------- */

        private void Rule1ConditionA( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            if ( m1 == null || m2 == null || m3 == null )
            {
                return;
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  m2 takes the same amount of time (or more) as m1 OR 
             *  m2 takes the same amount of time (or more) as m3,
             * 
             *          Place a ":5" at the end of m1 if (Tony: Somehow here, I think the correction can be shorter in time than the impulsive wave up. Will work on those later in the code. )
             *  
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if (
                    m2.TimeUnit >= m1.TimeUnit ||
                    m2.TimeUnit >= m3.TimeUnit
               )
            {
                m1.AddStructureLabel( StructureLabelEnum._5 );
            }

            
            if ( m1_ == null )
                return;

            var m4 = m3.GetNext( );

            if ( m4 == null )
                return;

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             * Place ":s5" at the end of m1 (m1 may complete a Flat pattern within a Complex formation where m2 is an x-wave (x:c3)) if 
             *      •	the length of m(-1) is between 100-161.8% (inclusive) of m0 and 
             *      •	m0 is very close to 61.8% of m1 and      
             *      •	m4 does not exceed the end of m0,;. 
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */
            if (
                    m1_.IsBtw_XYInclusive_ofM( 100, 161.8, m0 ) &&
                    m0.IsApproxX_OfM( 61.8, m1 ) &&
                    !m4.Exceed( m0.End )
               )
            {
                m1.MainWaveType = WaveType.ComplexCorrection;
                m1.WavePattern |= WavePattern.Flat;
                m1.WavePosition = WavePosition.End;
                m1.AddStructureLabel( StructureLabelEnum.s5 );

                m2.PotentialWave = ElliottWaveEnum.WaveX;
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m0 is composed of more than three monowaves and 
             *  m1 retraces all of m0 in the same amount of time (or less) that m0 took to form, 
             *  
             *          m0 is probably the end of an important Elliott pattern; note on chart.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( 
                    m0.MonoWavesCount > 3 && 
                    m0.IsCompletelyRetraced_Faster_byM( m1 ) )
            {
                m0.MainWaveType = WaveType.ImptWavePattern;
                m0.WavePosition = WavePosition.End;
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m0 and m2 are approximately equal in price and time (or related by 61.8% in either case) and 
             *  m(-1) is 161.8% (or more) of m1 and 
             *  m3 (or m3 through m5) achieves a price length equal to or greater than m(-1) within a time frame equal to or less than that of m(-1), 
             *          a Running Correction (any variation) is probably unfolding; take note of that fact and 
             *          add "[:c3]" after the ":5" already at the end of m1. 
             * 
             * If the Running correction is a simple variation, it most likely 
             *          started at the beginning of m0 and 
             *          concluded at the end of m2 with 
             *          m1 the "b-wave" of the correction. 
             *
             * For the Running Correction to be of the complex Double Three variety, 
             * m(-2) must be shorter than m(-1); 
             *          in that case, the formation probably started with m(-2) and 
             *          concluded with m4 making m1 the "x-wave" of the formation (x;c3). 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */
            var m5 = m4.GetNext( );
            if ( m5 == null )
                return;

            var m3Tom5 = m3.Combine( m4, m5 );

            var priceTimeApproxEqualOrRelatedBy61_8 = ( m0.PriceTimeApproxEqual( m2 ) || m0.IsApproxX_PriceTime_OfM( 61.8, m2 ) );

            if (   priceTimeApproxEqualOrRelatedBy61_8 &&
                   m1_.IsMoreThanX_ofM( 161.8, m1 ) &&
                   (
                        m3.AchieveSameDistanceInEqualOrLessTime( m1_ ) ||
                        m3Tom5.AchieveSameDistanceInEqualOrLessTime( m1_ )
                   )
               )
            {
                if ( m2_.IsShorterThan( m1_ ) )
                {
                    m2_.WavePattern |= WavePattern.DoubleThree;
                    m1_.WavePattern |= WavePattern.DoubleThree;
                    m0.WavePattern |= WavePattern.DoubleThree;
                    m1.WavePattern |= WavePattern.DoubleThree;
                    m1.PotentialWave = ElliottWaveEnum.WaveX;
                    m2.WavePattern |= WavePattern.DoubleThree;
                    m3.WavePattern |= WavePattern.DoubleThree;
                    m4.WavePattern |= WavePattern.DoubleThree;

                    m1.AddStructureLabel( StructureLabelEnum._5 | StructureLabelEnum.x_c3_LESSLIKELY );
                }
                else
                {
                    m0.WavePattern |= WavePattern.RunningFlat;
                    m1.WavePattern |= WavePattern.RunningFlat;
                    m1.PotentialWave = ElliottWaveEnum.WaveB;
                    m2.WavePattern |= WavePattern.RunningFlat;
                    m1.AddStructureLabel( StructureLabelEnum._5 | StructureLabelEnum.c3_RARE );
                }
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m0 and m2 are approximately equal in price and time (or related by 61.8% in either case) and 
             *  m(-1) is less than 161.8% of m1 and 
             *  m(-1) is larger than m0 and m3 or 
             *  m5 is 161.8% of m1 (or more), 
             *  
             *          a Running Correction (any variation), which concludes more than one pattern, might be under formation; note that fact and 
             *          add ":c3" after the ":5" already at the end of m1. 
             *          
             * If m(-2) is longer than m(-1), 
             *          go back to what is currently m(-1) and add ":sL3" to its Structure list. 
             * 
             * If the Running correction is a simple variation, it most likely started at the beginning of m1 and concluded at the end of m2 with m1 the "b wave" of the correction. 
             * 
             * For the Running Correction to be of the complex Double Three variety, 
             * m(-2) must be shorter than m(-1) and 
             * m3 must not be more than 161.8% of m1; 
             * under those specific conditions, the formation probably started with m(-2) and concluded with m4 making m1 the "x-wave" of the formation (add an "x" in front of the ":c3").
             * ------------------------------------------------------------------------------------------------------------------------------------------- */


            if (    priceTimeApproxEqualOrRelatedBy61_8 &&
                    m1_.IsLessThanX_OfM( 168.1, m1 ) &&
                    ( m1_.IsLongerThan( m0, m3 ) || m5.IsMoreThanX_ofM( 161.8, m1 ) )
               )
            {
                m1.AddStructureLabel( StructureLabelEnum._5 | StructureLabelEnum.c3 );

                if ( m2_.IsLongerThan( m1_ ) )
                {
                    m1_.AddStructureLabel( StructureLabelEnum.sL3 );

                    m1.MainWaveType = WaveType.RunningCorrection;
                    m1.WavePattern |= WavePattern.RunningCorrection;
                    m1.PotentialWave = ElliottWaveEnum.WaveB;

                    m2.MainWaveType = WaveType.RunningCorrection;
                    m2.WavePattern |= WavePattern.RunningCorrection;
                }

                if ( m2_.IsShorterThan( m1_ ) &&
                     m3.IsLessThanX_OfM( 161.8, m1 )
                   )
                {
                    m2_.WavePattern |= WavePattern.DoubleThree;
                    m1_.WavePattern |= WavePattern.DoubleThree;
                    m0.WavePattern |= WavePattern.DoubleThree;
                    m1.WavePattern |= WavePattern.DoubleThree;
                    m1.PotentialWave = ElliottWaveEnum.WaveX;
                    m2.WavePattern |= WavePattern.DoubleThree;
                    m3.WavePattern |= WavePattern.DoubleThree;
                    m4.WavePattern |= WavePattern.DoubleThree;

                    m1.AddStructureLabel( StructureLabelEnum._5 | StructureLabelEnum.x_c3 );
                }
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m0 and m2 are approximately equal in price and time (or related by 61.8% in either case) and 
             *  m3 is less than 161.8% of m1 and 
             *  m3 (plus one time unit) is completely retraced in the same amount of time (or less) that it took to form, 
             *  
             *          m1 may be part of a Complex Correction which will necessitate the use of an "x-wave" Progress label. 
             *          
             *          The x-wave would be in one of two places; the end of m0 or hidden from sight (i.e., invisible or "missing") in the center of m1. 
             *          This concept of "missing waves" is discussed in Chapter 32, page 12-34. 
             *          To warn of these two possibilities, take a pencil and place an "x:c3?", at the end of m0. 
             *          Also, circle the center of m1 and, to the right of the circle, place "x:c3?," to the left place ":s5." 
             * 
             * If m(-2) is longer than m(-1), the x-wave is not at the end of m0, drop that as a possibility. 
             * If the length of m3 is less than 61.8% of m1, the probabilities increase dramatically that the x-wave is hidden in the center of m1. These warnings will come in helpful as you group monowaves in Chapter 4 and as you finalize your interpretation throughout the analysis process. If the x-wave is used, the previously placed ":5" Structure label applies.
             * ------------------------------------------------------------------------------------------------------------------------------------------- */
            if (    priceTimeApproxEqualOrRelatedBy61_8 &&
                    m3.IsLessThanX_OfM( 161.8, m1 ) && 
                    m3.Plus1TU_IsCompletelyRetraced_Faster_byM( m4 ) 
               )
            {
                m1.MainWaveType = WaveType.ComplexCorrection;
                m1.WavePattern |= WavePattern.AnyCorrectivePattern;

                if ( m2_.IsLongerThan( m1_ ) )
                {
                    m1.MissingXWavePos = WavePosition.Center;
                    m1.MissingXWaves = StructureLabelEnum.s5 | StructureLabelEnum.x_c3_MAYBE;
                }
                else
                {
                    m0.MissingXWavePos = WavePosition.End;
                    m0.MissingXWaves = StructureLabelEnum.x_c3_MAYBE;
                    m1.MissingXWavePos = WavePosition.Center;
                    m1.MissingXWaves = StructureLabelEnum.s5 | StructureLabelEnum.x_c3_MAYBE;
                }

                if ( m3.IsLessThanX_OfM( 61.8, m1 ) )
                {
                    m1.MissingXWavePos = WavePosition.Center;
                    m1.MissingXWaves = StructureLabelEnum.s5 | StructureLabelEnum.x_c3_MAYBE;
                }
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m0 and m2 are obviously different from each other in price or time or both and 
             *  m0 and m2 do not share any similar price range and 
             *  m1, when compared to m(-1) and m3, is not the shortest of the three, 
             *  
             *          m1 may be part of a larger Trending Impulse pattern; if so, the previously placed ":5" is used.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( ! priceTimeApproxEqualOrRelatedBy61_8 && 
                   m0.DontOverlap( m2 )  &&
                   m1.IsNotTheShortest( m1_ , m3 )
               )
            {
                    
                // ![](B851A65382DD092DFE9EB72ADE54BFB9.png;;;0.04343,0.03332)
                m1.WavePattern |= WavePattern.TrendingImpulse;
                m1.AddStructureLabel(StructureLabelEnum._5 );
                    
            }
            
        }

        private void Rule1ConditionB( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            // ![](393AFD755D5DBD18BE7560A39C7EFED7.png;;;0.02755,0.01869) ![](1367497F1E9FEDDC70CFB2B60BB9A534.png;;;0.02147,0.01855)
            if ( m1 == null || m2 == null || m3 == null )
            {
                return;
            }

            m1.AddStructureLabel(StructureLabelEnum._5 );

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If the length of m(-1) is between 100-161.8% (inclusive) of m0 and 
             *  m4 does not exceed the end of m0, m1 may complete a Flat pattern within a Complex formation where m2 is an x-wave; 
             *          place ":s5" at the end of m1 and 
             *          "x:c3?" at the end of m2. 
             * 
             * Additional Structure labels may be needed if certain behavior is exhibited by the price action. Read the below descriptions to decide if any additional Structure labels should be added.
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            var m4 = m3.GetNext( );
            if( m4 == null )
                return;

            if ( 
                    m1_.IsBtw_XYInclusive_ofM( 100, 161.8, m0 ) &&
                    !m4.Exceed( m0.End )
               )
            {
                m1.MainWaveType = WaveType.Correction;
                m1.WavePattern |= WavePattern.Flat;
                m1.WavePosition = WavePosition.End;


                m2.PotentialWave = ElliottWaveEnum.WaveX;
                m1.AddStructureLabel( StructureLabelEnum.s5 );
                m2.AddStructureLabel( StructureLabelEnum.x_c3_MAYBE );
            }

            if ( m0.MonoWavesCount > 3 && m0.IsCompletelyRetraced_byM_Minus1TU( m1 ) )
            {
                m0.MainWaveType = WaveType.ImptWavePattern;
                m0.WavePosition = WavePosition.End;
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If part of m2's price range is shared by m0 and 
             *  m3 is longer and more vertical than m1 during a time span equal to (or less than) m1 and 
             *  m(-1) is longer than m1, add ":sL3" to m1's Structure list.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( m2.Overlap( m0 ) && m3.IsLongerAndMoreVerticalThan( m1 ) )
            {
                if ( m1_.IsLongerThan( m1 ) )
                {
                    m1.AddStructureLabel( StructureLabelEnum.sL3 );
                }
            }

            var m1Tom3    = m1.Combine( m2, m3 );
            var m5        = m4.GetNext( );
            if ( m5 == null ) return;
            var m6        = m5.GetNext( );
            if ( m6 == null ) return;
            var m4Tom6    = m4.Combine( m5, m6 );

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If part of m2's price range is shared by m0 and 
             *  m3 is longer and more vertical than m1 during a time span equal to (or less than) m1 and 
             *  m(-1) is shorter than m1 and 
             *  m0 and m2 are obviously different in price or time or both and 
             *  m4 (or m4 through m6) returns to the beginning of m1 in a time period 50% of that consumed by m1 through m3, a 5th Extension Terminal pattern may have completed with m3; add ":c3" to m1's Structure list.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */
            if ( 
                    m2.Overlap( m0 ) && 
                    m3.IsLongerAndMoreVerticalThan( m1 ) &&
                    m1_.IsShorterThan( m1 ) &&
                    m0.PriceTimeAlternation( m2 )
               )
            {
                if ( 
                        m4.ReturnToX_TimeLTEy( m1.Begin, 0.5 * m1Tom3.TimeUnit ) ||
                        m4Tom6.ReturnToX_TimeLTEy( m1.Begin, 0.5 * m1Tom3.TimeUnit )
                    )
                {
                    m1.AddStructureLabel( StructureLabelEnum.c3 );
                    m3.WavePattern |= WavePattern.TerminalPattern;
                    m3.WavePosition = WavePosition.End;
                }
            }
        }

        private void Rule1ConditionC( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            if ( m2 == null || m1 == null || m0 == null )
                return;

            // Place a ":5" at the end of m1
            m1.AddStructureLabel(StructureLabelEnum._5 );

            if ( m0.IsApproxEqual_X_OfM( 10, m1 ) )
            {
                if ( m0.IsApproxX_PriceTime_OfM( 61.8, m1 ) )
                {
                    if ( m3 != null )
                    {
                        if ( m3.IsLongerAndMoreVerticalThan( m2 ) )
                        {
                            if ( m2.TimeUnit >= m0.TimeUnit || m2.TimeUnit >= m1.TimeUnit )
                            {
                                if ( m2.IsApproxX_OfM( 38.2, m1 ) )
                                {
                                    if ( m0.StructureLabel.HasFlag( StructureLabelEnum.F3 ) )
                                    {
                                        m0.AddStructureLabel( StructureLabelEnum.c3 );
                                    }
                                }
                            }
                        }

                        if ( m3.IsLongerAndMoreVerticalThan( m1 ) )
                        {
                            var m4 = m3.GetNext( );

                            if ( m4 != null )
                            {
                                // m3 is completely retraced OR m3 is retraced no more than 61.8 %
                                if ( m3.IsCompletelyRetraced_byM( m4 ) || m3.IsRetraced_LtX_byM( 61.8, m4 ) )
                                {
                                    if ( m2.IsApproxX_OfM( 38.2, m1 ) )
                                    {
                                        if ( m0.StructureLabel.HasFlag( StructureLabelEnum.c3 ) )
                                        {
                                            if ( m3_.IsLongerThan( m2_ ) && ( m2_.IsLongerThan( m0 ) || m1_.IsLongerThan( m0 ) ) )
                                            {
                                                // m1 may be the second-to-last leg of a Contracting Triangle; add "(:sL3)" to the Structure list
                                                m1.WavePattern |= WavePattern.ContractingTriangle;
                                                m1.AddStructureLabel(StructureLabelEnum.sL3 | StructureLabelEnum._5 );
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
            }
        }

        private void Rule1ConditionD( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            if ( m2 == null || m1 == null || m0 == null )
                return;

            // Place a ":5" at the end of m1
            m1.AddStructureLabel(StructureLabelEnum._5 );
        }
    }
}
