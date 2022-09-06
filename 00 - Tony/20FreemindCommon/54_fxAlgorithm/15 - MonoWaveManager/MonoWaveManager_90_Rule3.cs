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
        // ![](3A598C472AC851439D0132DAC28B49D1.png;;;0.02386,0.02076)
        private void Rule3( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            if ( m0 == null )
                return;

            double m0m1Ratio = m0.Over( m1 );

            switch ( m0m1Ratio )
            {
                case double p when ( p < 38.2 ):
                {
                    Rule3ConditionA( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;

                case double p when ( p >= 38.2 && p < 61.8 ):
                {
                    Rule3ConditionB( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;


                case double p when ( p >= 61.8 && p < 100 ):
                {
                    Rule3ConditionC( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;

                case double p when ( p >= 100 && p < 161.8 ):
                {
                    Rule3ConditionD( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;

                case double p when ( p >= 161.8 && p <= 261.8 ):
                {
                    Rule3ConditionE( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;

                case double p when ( p > 261.8 ):
                {
                    Rule3ConditionF( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;
            }
        }

        private void Rule3ConditionA( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            if ( m1 == null || m2 == null || m3 == null )
                return;

            if ( m3.IsMoreThanX_ofM( 261.8, m1 ) )
            {
                m1.MainWaveType = WaveType.RunningCorrection | WaveType.ComplexCorrection;
                m1.WavePattern |= WavePattern.RunningCorrection | WavePattern.ZigZag;
                m1.WavePosition = WavePosition.Center;
                m1.AddStructureLabel( StructureLabelEnum.c3 | StructureLabelEnum.s5_LESSLIKELY );

                /* -------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *  If m1 is the longest when compared to m(-1) and m(-3) and 
                 *  m2 breaks a trend line drawn across the low of m(-2)and m0 in a period of time equal to or less than that taken by m1, 
                 *  
                 *      m1 may be the 5th wave of a 5th Extension pattern; add [:L5] to the end of m1. 
                 * 
                 * ------------------------------------------------------------------------------------------------------------------------------------------- */

                if ( m1.IsTheLongest( m1_, m3_ ) )
                {
                    // ![](3666128D80973E36E04D5BF80086C0B0.png;;;0.01876,0.01636)

                    if ( m2.BreakTrendLineBtwXYInLETimeToZ( m2_, m0, m1 ) )
                    {
                        m1.MainWaveType = WaveType.Impulsive5Waves;
                        m1.WavePattern |= WavePattern.TrendingImpulse;
                        m1.WavePosition = WavePosition.End;
                        m1.AddStructureLabel( StructureLabelEnum.L5_RARE );
                        m1.PotentialWave = ElliottWaveEnum.Wave5;
                    }
                }

                /* -------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *  If m(-1) is more than 161.8% of m1, drop ":s5" from the list. 
                 *  If m(-1) is less than 61.8% of m3, more than one Elliott pattern (each of a larger magnitude) may have completed at m2.
                 * 
                 * ------------------------------------------------------------------------------------------------------------------------------------------- */
                if ( m1_.IsMoreThanX_ofM( 161.8, m1 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.s5 );
                }

                if ( m1_.IsLessThanX_OfM( 61.8, m3 ) )
                {
                    m2.WavePattern |= WavePattern.MoreThanOneEWPatternEnd;
                }
            }


            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m3 is between 161.8 and 261.8% (inclusive) of m1, 
             *          m1 may be the center portion of an Impulse pattern with a 5th wave Extension, 
             *          the center portion of a Running Correction, 
             *          or the first leg of an Elliott pattern within a Complex Correction; 
             *          
             *          place ":s5/:c3/:F3" at the end of m1 to list these three possibilities in their respective order.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */
            if ( m3.IsBtw_XYInclusive_ofM( 161.8, 261.8, m1 ) )
            {
                m1.WavePattern |= WavePattern.TrendingImpulse | WavePattern.RunningCorrection | WavePattern.AnyComplexCorrection;

                m1.AddStructureLabel( StructureLabelEnum.s5 | StructureLabelEnum.c3 | StructureLabelEnum.F3 );

                /* -------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *  If m1 is the longest when compared to m(-1) and m(-3) and 
                 *  m2 breaks a trendline drawn across the low of m(-2) and m0 in a period of time equal to or less than that taken by m1, 
                 *  
                 *      m1 may be the 5th wave of a 5th Extension pattern; add [:L5] to the end of m1. 
                 * 
                 * ------------------------------------------------------------------------------------------------------------------------------------------- */
                if ( m1.IsTheLongest( m1_, m3_ ) )
                {
                    if ( m2.BreakTrendLineBtwXYInLETimeToZ( m2_, m0, m1 ) )
                    {
                        m1.MainWaveType = WaveType.Impulsive5Waves;
                        m1.WavePattern |= WavePattern.TrendingImpulse;
                        m1.WavePosition = WavePosition.End;
                        m1.AddStructureLabel( StructureLabelEnum.L5_RARE );
                        m1.PotentialWave = ElliottWaveEnum.Wave5;
                    }
                }

                /* -------------------------------------------------------------------------------------------------------------------------------------------
                * 
                *  If m(-1) is longer than m3, 
                *          drop ":c3" from the Structure list. 
                *          
                *  If m(-1) is longer than m1, 
                *          the ":s5" (if used for m1) could only be the c-wave of a Zigzag within a Complex Correction; 
                *          m2 would be an x-wave which is then most likely followed by wave-a of a Contracting Triangle.
                * 
                * ------------------------------------------------------------------------------------------------------------------------------------------- */
                if ( m1_.IsLongerThan( m3 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.c3 );
                }

                if ( m1_.IsLongerThan( m1 ) )
                {
                    if ( m1_.PreferableStructureIs( StructureLabelEnum.s5 ) )
                    {
                        m1.MainWaveType = WaveType.ComplexCorrection;
                        m1.WavePattern |= WavePattern.ZigZag;
                        m1.PotentialWave = ElliottWaveEnum.WaveC;
                        m1.WavePosition = WavePosition.End;
                        m1.AddStructureLabel( StructureLabelEnum.s5 );

                        m2.PotentialWave = ElliottWaveEnum.WaveX;
                    }
                }

            }






            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m3 is at least 100, but less than 161.8% of m1, m1 may be the 
             *          first leg of a Standard Elliott pattern within a Complex Correction, 
             *          wave-3 of an Impulse pattern with a 5th-wave Extension or the 
             *          c-wave of a Zigzag within an ongoing Complex Correction; 
             *          
             *          place ":F3/:5/:s5" at the end of m1 to show these three possibilities respectively. 
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            var m4 = m3.GetNext( );

            if ( m4 == null )
                return;

            if ( m3.IsAtLeastX_LessThanY_ofM( 100, 161.8, m1 ) )
            {
                m1.WavePattern |= WavePattern.TrendingImpulse | WavePattern.ZigZag | WavePattern.AnyComplexCorrection;

                m1.AddStructureLabel( StructureLabelEnum.F3 | StructureLabelEnum._5 | StructureLabelEnum.s5 );

                /* -------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *  If m1 is the longest when compared to m(-1) and m(-3) and 
                 *  m2 breaks a trendline drawn across the low of m(-2) and m0 in a period of time equal to or less than that taken by m1, 
                 *  
                 *      m1 may be the 5th wave of a 5th Extension pattern; add [:L5] to the end of m1. 
                 * 
                 * ------------------------------------------------------------------------------------------------------------------------------------------- */
                if ( m1.IsTheLongest( m1_, m3_ ) )
                {
                    if ( m2.BreakTrendLineBtwXYInLETimeToZ( m2_, m0, m1 ) )
                    {
                        m1.MainWaveType = WaveType.Impulsive5Waves;
                        m1.WavePattern |= WavePattern.TrendingImpulse;
                        m1.WavePosition = WavePosition.End;
                        m1.AddStructureLabel( StructureLabelEnum.L5_RARE );
                        m1.PotentialWave = ElliottWaveEnum.Wave5;
                    }
                }

                if ( m4.IsShorterThan( m3 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.F3 );
                }

                if ( m0.TimeUnit < m1_.TimeUnit && m0.TimeUnit < m1.TimeUnit )
                {
                    m1.DropStructureLabel( StructureLabelEnum.s5 );
                }

                if ( m1_.IsLongerThan( m1 ) )
                {
                    if ( m1.PreferableStructureIs( StructureLabelEnum.s5 ) )
                    {
                        m1.MainWaveType = WaveType.ComplexCorrection;
                        m1.WavePattern |= WavePattern.ZigZag;
                        m1.PotentialWave = ElliottWaveEnum.WaveC;

                        m2.PotentialWave = ElliottWaveEnum.WaveX;
                    }
                }
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m3 is shorter than m1 and 
             *  m3 is completely retraced faster than it took to form, 
             *          an Impulsive or Complex Corrective pattern may have concluded with m3; 
             *          place ":5/:F3" at the end of m1. 
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */
            if ( m3.IsShorterThan( m1 ) && m3.IsCompletelyRetraced_Faster_byM( m4 ) )
            {
                m3.WavePattern |= WavePattern.TrendingImpulse | WavePattern.AnyComplexCorrection;
                m1.AddStructureLabel( StructureLabelEnum._5 | StructureLabelEnum.F3 );

                /* -------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *  If m1 is the longest when compared to m(-1) and m(-3) and 
                 *  m2 breaks a trendline drawn across the low of m(-2) and m0 in a period of time equal to or less than that taken by m1, 
                 *  
                 *      m1 may be the 5th wave of a 5th Extension pattern; add [:L5] to the end of m1. 
                 * 
                 * ------------------------------------------------------------------------------------------------------------------------------------------- */
                if ( m1.IsTheLongest( m1_, m3_ ) )
                {
                    if ( m2.BreakTrendLineBtwXYInLETimeToZ( m2_, m0, m1 ) )
                    {
                        m1.MainWaveType = WaveType.Impulsive5Waves;
                        m1.WavePattern |= WavePattern.TrendingImpulse;
                        m1.WavePosition = WavePosition.End;
                        m1.AddStructureLabel( StructureLabelEnum.L5_RARE );
                        m1.PotentialWave = ElliottWaveEnum.Wave5;
                    }
                }
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m3 is shorter than m1 and m3 is completely retraced slower than it took to form, 
             *          m1 concludes a Zigzag which is part of a Complex Corrective pattern; place ":s5" at the end of m1.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( m3.IsShorterThan( m1 ) && m3.IsCompletelyRetraced_Slower_byM( m4 ) )
            {
                m1.MainWaveType = WaveType.ComplexCorrection;
                m1.WavePattern |= WavePattern.ZigZag;
                m1.AddStructureLabel( StructureLabelEnum.s5 );

                /* -------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *  If m1 is the longest when compared to m(-1) and m(-3) and 
                 *  m2 breaks a trendline drawn across the low of m(-2) and m0 in a period of time equal to or less than that taken by m1, 
                 *  
                 *      m1 may be the 5th wave of a 5th Extension pattern; add [:L5] to the end of m1. 
                 * 
                 * ------------------------------------------------------------------------------------------------------------------------------------------- */
                if ( m1.IsTheLongest( m1_, m3_ ) )
                {
                    if ( m2.BreakTrendLineBtwXYInLETimeToZ( m2_, m0, m1 ) )
                    {
                        m1.MainWaveType = WaveType.Impulsive5Waves;
                        m1.WavePattern |= WavePattern.TrendingImpulse;
                        m1.WavePosition = WavePosition.End;
                        m1.AddStructureLabel( StructureLabelEnum.L5_RARE );
                        m1.PotentialWave = ElliottWaveEnum.Wave5;
                    }
                }
            }


            if ( m3.IsShorterThan( m1 ) && m4.IsShorterThan( m3 ) )
            {
                m1.MainWaveType = WaveType.ComplexCorrection | WaveType.TerminalImpulse;
                m1.WavePattern |= WavePattern.ZigZag | WavePattern.TerminalImpulse;
                m1.AddStructureLabel( StructureLabelEnum.s5 | StructureLabelEnum.F3 );

                /* -------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *  If m1 is the longest when compared to m(-1) and m(-3) and 
                 *  m2 breaks a trendline drawn across the low of m(-2) and m0 in a period of time equal to or less than that taken by m1, 
                 *  
                 *      m1 may be the 5th wave of a 5th Extension pattern; add [:L5] to the end of m1. 
                 * 
                 * ------------------------------------------------------------------------------------------------------------------------------------------- */
                if ( m1.IsTheLongest( m1_, m3_ ) )
                {
                    if ( m2.BreakTrendLineBtwXYInLETimeToZ( m2_, m0, m1 ) )
                    {
                        m1.MainWaveType = WaveType.Impulsive5Waves;
                        m1.WavePattern |= WavePattern.TrendingImpulse;
                        m1.WavePosition = WavePosition.End;
                        m1.AddStructureLabel( StructureLabelEnum.L5_RARE );
                        m1.PotentialWave = ElliottWaveEnum.Wave5;
                    }
                }

                var m5 = m4.GetNext( );

                if ( m5 != null )
                {
                    if ( m5.IsLongerThan( m3 ) )
                    {
                        m1.DropStructureLabel( StructureLabelEnum.F3 );
                    }
                }
            }
        }

        private void Rule3ConditionB( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            if ( m1 == null || m2 == null || m3 == null )
                return;

            if ( m3.IsMoreThanX_ofM( 261.8, m1 ) )
            {
                m1.MainWaveType = WaveType.ComplexCorrection;
                m1.WavePattern |= WavePattern.IrregularFailureFlat | WavePattern.ZigZag;
                m1.AddStructureLabel( StructureLabelEnum.s5_LESSLIKELY | StructureLabelEnum.c3 );

                if ( m1_.IsMoreThanX_ofM( 161.8, m1 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.s5 );
                }

                if ( m1_.IsLessThanX_OfM( 61.8, m1 ) )
                {
                    m2.WavePattern |= WavePattern.MoreThanOneEWPatternEnd;
                }
            }


            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m3 is between 161.8 and 261.8% (inclusive) of m1, 
             *      m1 may be the center portion of an Irregular Failure, 
             *      the c-wave of a Zigzag within a Complex Correction or 
             *      the center portion of a 5th Extension Terminal Impulse pattern; place ":c3/:s5" at the end of m1.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( m3.IsBtw_XYInclusive_ofM( 161.8, 261.8, m1 ) )
            {
                m1.WavePattern |= WavePattern.IrregularFailureFlat | WavePattern.AnyComplexCorrection | WavePattern.TerminalImpulse;
                m1.PotentialWave = ElliottWaveEnum.WaveC;
                m1.WavePosition = WavePosition.Center;
                m1.AddStructureLabel( StructureLabelEnum.s5 | StructureLabelEnum.c3 );

                if ( m1_.IsLongerThan( m1 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.c3 );
                }

            }

            var m4 = m3.GetNext( );

            if ( m4 == null )
                return;

            if ( m3.IsAtLeastX_LessThanY_ofM( 100, 161.8, m1 ) )
            {
                m1.WavePattern |= WavePattern.ZigZag | WavePattern.TrendingImpulse;

                m1.AddStructureLabel( StructureLabelEnum.c3 | StructureLabelEnum._5 | StructureLabelEnum.s5 );

                /* -------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *  If m(-1) is longer than m1, drop ":c3" from the list of possibilities.
                 * 
                 * ------------------------------------------------------------------------------------------------------------------------------------------- */
                if ( m1.IsLongerThan( m1_ ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.c3 );
                }

                if ( m1_.IsLongerThan( m1 ) )
                {
                    if ( m1.StructureLabel.HasFlag( StructureLabelEnum.s5 ) )
                    {
                        m1.MainWaveType = WaveType.ComplexCorrection;
                        m1.WavePattern |= WavePattern.ZigZag;
                        m1.PotentialWave = ElliottWaveEnum.WaveC;
                        m2.AddStructureLabel( StructureLabelEnum.x_c3_MAYBE );
                    }
                }

                if ( m4.IsShorterThan( m3 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum._5 );
                }

                if ( m3.Plus1TU_IsCompletelyRetraced_Faster_byM( m4 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.s5 );
                }
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m3 is shorter than m1 and m3 (plus one time unit) is completely retraced faster than it took to form, 
             *          a Complex Correction may have concluded with m3; 
             *          place ":5" at the end of m1. 
             *                                    
             * ------------------------------------------------------------------------------------------------------------------------------------------- */
            if ( m3.IsShorterThan( m1 ) && m3.Plus1TU_IsCompletelyRetraced_Faster_byM( m4 ) )
            {
                m3.MainWaveType = WaveType.ComplexCorrection;
                m3.WavePattern |= WavePattern.AnyComplexCorrection;
                m1.AddStructureLabel( StructureLabelEnum._5 );

                /* -------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *  If m4 returns to the beginning of m(-1) within a period of time equal to 50% (or less) of that consumed by m(-1) through m3 and 
                 *  m(-1) is not more than 261.8% of m1, m1 may be part of a Terminal Impulse pattern; add ":c3" to m1.
                 * 
                 * ------------------------------------------------------------------------------------------------------------------------------------------- */

                //![](CFE8B4DD09EFE274B50B983D18318705.png;;;0.02028,0.01531)

                
                var m1_Tom3 = m1_.Combine( m0, m1, m2, m3 );

                if ( m1_Tom3.IsCompletelyRetraced_byM( m4 ) )
                {
                    if ( m1_Tom3.TimeUnit * 0.5 >= m4.TimeUnit )
                    {
                        if ( m1_.IsLessThanX_OfM( 261.8, m1 ) )
                        {
                            m1.MainWaveType = WaveType.TerminalImpulse;
                            m1.WavePattern |= WavePattern.TerminalImpulse;
                            m1.AddStructureLabel( StructureLabelEnum.c3 );
                        }
                    }
                }
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m3 is shorter than m1 and m3 is completely retraced slower than it took to form, 
             *          m1 concludes a Zigzag which is part of a Complex Corrective pattern; place ":s5" at the end of m1.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */
            if ( m3.IsShorterThan( m1 ) && m3.IsCompletelyRetraced_byM( m4 ) )
            {
                m1.MainWaveType = WaveType.ComplexCorrection;
                m1.WavePattern |= WavePattern.ZigZag;
                m1.AddStructureLabel( StructureLabelEnum.s5 );
            }


            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m3 is shorter than m1 and m4 is shorter than m3, 
             *          m1 may have completed a Zigzag which is part of a Complex Correction or is part of a Terminal Impulse pattern; 
             *          place ":s5/:F3" at the end of m1 to show these two possibilities, respectively. 
             *          
             *  If m5 is longer than m3, drop ":F3" as a possible Structure label. 
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */
            if ( m3.IsShorterThan( m1 ) && m4.IsShorterThan( m3 ) )
            {
                m1.MainWaveType = WaveType.ComplexCorrection | WaveType.TerminalImpulse;
                m1.WavePattern |= WavePattern.ZigZag | WavePattern.TerminalImpulse;
                m1.AddStructureLabel( StructureLabelEnum.s5 | StructureLabelEnum.F3 );

                var m5 = m4.GetNext( );

                if ( m5 != null )
                {
                    if ( m5.IsLongerThan( m3 ) )
                    {
                        m1.DropStructureLabel( StructureLabelEnum.F3 );
                    }
                }
            }



        }

        private void Rule3ConditionC( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            if ( m1 == null || m2 == null || m3 == null )
                return;

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m3 is more than 261.8% of m1, m2 probably completed an Irregular Failure Flat or a Non-Limiting Triangle; 
             *      place ":c3/sL3" at the end of m1. 
             * 
             * If m(-1) is more than 161.8% of m1, 
             *      drop ":sL3" from the list of Structure possibilities. 
             * 
             * If m(-1) is not more than 161.8% of m1 and 
             * m(-2) is at least 61.8% of m(-1),
             *      drop ":c3" from the Structure list.
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( m3.IsMoreThanX_ofM( 261.8, m1 ) )
            {
                m1.MainWaveType = WaveType.ComplexCorrection;
                m1.WavePattern |= WavePattern.IrregularFailureFlat | WavePattern.ExpandingTriangle;
                m1.AddStructureLabel( StructureLabelEnum.sL3 | StructureLabelEnum.c3 );

                if ( m1_.IsMoreThanX_ofM( 161.8, m1 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.sL3 );
                }

                if ( m1_.IsLessThanX_OfM( 161.8, m1 ) && m2_.IsAtLeastX_OfM( 61.8, m1_ ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.c3 );
                }
            }

            var m4 = m3.GetNext( );

            if ( m4 == null )
                return;

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m3 is between 161.8% and 261.8% (inclusive) of m1, 
             *          m1 may be the center portion of an Irregular Failure Flat, 
             *          the second-to-last leg of a Contracting Triangle or 
             *          part of a Complex Correction; 
             *              place ":F3/:c3/:sL3/:s5" at the end of m1. 
             *              
             *  If m3 (plus one time unit) is completely retraced faster than it took to form, drop ":s5" from the above list.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */
            if ( m3.IsAtLeastX_LessThanY_ofM( 161.8, 261.8, m1 ) )
            {
                m1.WavePattern |= WavePattern.IrregularFailureFlat | WavePattern.ContractingTriangle | WavePattern.AnyComplexCorrection;
                m1.PotentialWave = ElliottWaveEnum.WaveC;
                m1.WavePosition = WavePosition.Center;
                m1.AddStructureLabel( StructureLabelEnum.sL3 | StructureLabelEnum.s5 | StructureLabelEnum.c3 | StructureLabelEnum.F3 );

                if ( m3.Plus1TU_IsCompletelyRetraced_Faster_byM( m4 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.s5 );
                }

                if ( m1_.IsMoreThanX_ofM( 161.8, m1 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.sL3 );
                }

                if ( m1_.IsLessThanX_OfM( 161.8, m1 ) && m1.IsRetraced_AtLeastX_byM( 61.8, m0 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.c3 );
                }

                if ( m4.IsShorterThan( m3 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.F3 );
                }
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m3 is at least 100%, but less than 161.8% of m1, 
             *          m1 may be the center portion of an Irregular Failure Flat, 
             *          the second-to-last leg of a Contracting Triangle, 
             *          the center leg of a 5th Extension Terminal pattern or 
             *          one of the legs of a Complex Correction; 
             *          
             *              place ":F3/:c3/:sL3/:s5" at the end of m1. 
             *              
             *  If m4 is shorter than m3, drop ":F3" from the list and forget about the Terminal scenario. If m3 (plus one time unit) is completely retraced faster than it took to form, drop ":s5" from the above list. If m(-1) is more than 161.8% of m1, drop ":sL3" from the above list. If m(-1) is not more than 161.8% of m1 and m(-1) is retraced at least 61.8%, drop ":c3" from the above list.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( m3.IsAtLeastX_LessThanY_ofM( 100, 161.8, m1 ) )
            {
                m1.WavePattern |= WavePattern.IrregularFailureFlat | WavePattern.ContractingTriangle | WavePattern.TerminalImpulse | WavePattern.AnyComplexCorrection;
                m1.AddStructureLabel( StructureLabelEnum.F3 | StructureLabelEnum.c3 | StructureLabelEnum.sL3 | StructureLabelEnum.s5 );

                if ( m4.IsShorterThan( m3 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.F3 );
                    m1.WavePattern &= ~WavePattern.TerminalImpulse;
                }

                if ( m3.Plus1TU_IsCompletelyRetraced_Faster_byM( m4 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.s5 );
                }

                if ( m1_.IsMoreThanX_ofM( 161.8, m1 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.sL3 );
                }

                if ( m1_.IsLessThanX_OfM( 161.8, m1 ) && m1_.IsRetraced_AtLeastX_byM( 61.8, m0 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.c3 );
                }
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m3 is shorter than m1 and m3 (plus one time unit) is completely retraced faster than it took to form, 
             *          a Terminal Impulse or Complex Correction may have concluded with m3; place ":c3/:F3" at the end of m1. 
             *          
             *          If m(-1) is less than 138.2% or more than 261.8% of m1, the ":c3" becomes very improbable; place brackets around it – "[:c3]."
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( m3.IsShorterThan( m1 ) && m3.Plus1TU_IsCompletelyRetraced_Faster_byM( m4 ) )
            {
                // ![](25965DA003E64B03C98FE079CFACDCFD.png;;;0.03301,0.02636)
                m3.MainWaveType = WaveType.TerminalImpulse | WaveType.ComplexCorrection;
                m3.WavePattern |= WavePattern.TerminalImpulse | WavePattern.AnyComplexCorrection;
                m1.AddStructureLabel( StructureLabelEnum.c3 | StructureLabelEnum.F3 );

                if ( m1_.IsLessThanX_OfM( 138.2, m1 ) || m1_.IsMoreThanX_ofM( 261.8, m1 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.c3 );
                    m1.AddStructureLabel( StructureLabelEnum.c3_RARE );
                }
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m3 is shorter than m1 and m3 (plus one time unit) is completely retraced slower than it took to form, 
             *          m1 may be wave-a of a Zigzag or 
             *          the c-wave of a Zigzag within a Complex Correction; 
             *          place ":F3/(:s5)" at the end of m1. 
             *          
             *  If m5 (plus one time unit) is completely retraced by m4 faster than m4 took to form, drop "(:s5)" from the list.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            var m5 = m4.GetNext( );

            if ( m3.IsShorterThan( m1 ) && m3.Plus1TU_IsCompletelyRetraced_Slower_byM( m4 ) )
            {
                m1.MainWaveType = WaveType.Correction | WaveType.ComplexCorrection;
                m1.WavePattern |= WavePattern.ZigZag | WavePattern.AnyComplexCorrection;
                m1.PotentialWave = ElliottWaveEnum.WaveA;
                m1.AddStructureLabel( StructureLabelEnum.F3 | StructureLabelEnum.s5_LESSLIKELY );

                if ( m5 != null )
                {
                    if ( m4.Plus1TU_IsCompletelyRetraced_Faster_byM( m5 ) )
                    {
                        m1.DropStructureLabel( StructureLabelEnum.s5 );
                    }
                }
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m3 is shorter than m1 and m4 is shorter than m3, 
             *      m1 may be the last leg of a Zigzag or 
             *      Flat within a Complex Correction, 
             *      one of the center legs of a Running Contracting Triangle OR 
             *      the first leg of a Terminal Impulse pattern; 
             *              place ":s5/x3/(:F3)" at the end of m1. 
             *              
             *  If m5 is longer than m3, drop "(:F3)" from the Structure list. If m(-1) is longer than 261.8% of m1, drop ":s5" from the list.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( m3.IsShorterThan( m1 ) && m4.IsShorterThan( m3 ) )
            {
                m1.MainWaveType = WaveType.Correction | WaveType.ComplexCorrection;
                m1.WavePattern |= WavePattern.ZigZag | WavePattern.Flat | WavePattern.ContractingTriangle | WavePattern.TerminalImpulse;
                m1.AddStructureLabel( StructureLabelEnum.F3_LESSLIKELY | StructureLabelEnum.s5 | StructureLabelEnum.x_c3 );

                if ( m5.IsLongerThan( m3 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.F3_LESSLIKELY );
                }

                if ( m1_.IsMoreThanX_ofM( 261.8, m1 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.s5 );
                }
            }
        }

        private void Rule3ConditionD( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            if ( m1 == null || m2 == null || m3 == null )
                return;

            if ( m3.IsMoreThanX_ofM( 261.8, m2 ) )
            {
                m1.MainWaveType = WaveType.ComplexCorrection;
                m1.WavePattern |= WavePattern.ZigZag | WavePattern.CFailureFlat | WavePattern.ContractingTriangle;
                m1.AddStructureLabel( StructureLabelEnum._5 | StructureLabelEnum.c3 | StructureLabelEnum.sL3_LESSLIKELY );

                if ( m1_.IsLessThanX_OfM( 61.8, m0 ) || m1_.IsMoreThanX_ofM( 161.8, m0 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.sL3_LESSLIKELY );
                }

                if ( m2.IsCompletelyRetraced_Slower_byM( m3 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.sL3_LESSLIKELY );
                    m1.DropStructureLabel( StructureLabelEnum.c3 );
                }

                if ( m3.IsMoreThanX_ofM( 161.8, m1 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum._5 );
                }
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m3 is between 161.8 and 261.8% (inclusive) of m2, 
             *          m1 may be the center section of a C-Failure Flat, 
             *          the second-to last leg of a Contracting Triangle or 
             *          the first leg of a Zigzag; 
             *                  place ":c3/:sL3/:5" at the end of m1 to show these three possibilities respectively. 
             *                  
             *  If m(-1) is less than 61.8% of m0 OR more than 161.8% of m0, then check to see if m1 is less than 38.2% of m(-3) through m0; 
             *          if so, drop ":sL3" from the list; 
             *          
             *  if m1 is more than 38.2%, but less than 61.8% of m(-3) through m(-1), place parentheses around ":sL3" to show that, though it is possible, it is not the preferred choice. If m(-1) falls between 61.8-161.8% of m0, drop ":c3" from the list. If m4 is less than 61.8% of m0, place parentheses around ":5" to indicate its lower probability.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            var m4 = m3.GetNext( );

            if ( m3.IsBtw_XYInclusive_ofM( 161.8, 261.8, m2 ) )
            {
                m1.MainWaveType = WaveType.ComplexCorrection;
                m1.WavePattern |= WavePattern.ZigZag | WavePattern.CFailureFlat | WavePattern.ContractingTriangle;
                m1.AddStructureLabel( StructureLabelEnum._5 | StructureLabelEnum.c3 | StructureLabelEnum.sL3 );

                if ( m1_.IsLessThanX_OfM( 61.8, m0 ) || m1_.IsMoreThanX_ofM( 161.8, m0 ) )
                {
                    var m3_Tom1_ = m3_.Combine( m2_, m1_ );

                    if ( m1.IsLessThanX_OfM( 38.2, m3_Tom1_ ) )
                    {
                        m1.DropStructureLabel( StructureLabelEnum.sL3 );
                    }

                    if ( m1.IsBtw_XYExclusive_ofM( 38.2, 61.8, m3_Tom1_ ) )
                    {
                        m1.DropStructureLabel( StructureLabelEnum.sL3 );
                        m1.AddStructureLabel( StructureLabelEnum.sL3_LESSLIKELY );
                    }
                }

                if ( m1_.IsBtw_XYInclusive_ofM( 61.8, 161.8, m0 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.c3 );
                }

                if ( m4 != null )
                {
                    if ( m4.IsLessThanX_OfM( 61.8, m0 ) )
                    {
                        m1.DropStructureLabel( StructureLabelEnum._5 );
                        m1.AddStructureLabel( StructureLabelEnum._5_LESSLIKELY );
                    }
                }
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m3 is at least 100, but less than 161.8% of m2, 
             *          m1 is probably the first leg of a Zigzag, but it may be in a Triangle; place ":5/(:c3)/[:F3]" at the end of m1. If m4 is larger than m3, drop "(:c3)" and "[:F3]" from the list of possibilities. If m4 is shorter than m3 and m5 retraces m4 faster than m4 took to form and m5 is equal to (or longer) and more vertical than m1, drop ":5" from the list.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( m3.IsAtLeastX_LessThanY_ofM( 100, 161.8, m2 ) )
            {
                m1.MainWaveType = WaveType.ComplexCorrection;
                m1.WavePattern |= WavePattern.ZigZag | WavePattern.ContractingTriangle;
                m1.AddStructureLabel( StructureLabelEnum._5 | StructureLabelEnum.c3_LESSLIKELY | StructureLabelEnum.F3_RARE );

                if ( m4.IsLongerThan( m3 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.c3_LESSLIKELY );
                    m1.DropStructureLabel( StructureLabelEnum.F3_RARE );
                }

                var m5 = m4.GetNext( );

                if ( m5 != null )
                {
                    if ( m4.IsShorterThan( m3 ) && m4.IsCompletelyRetraced_Faster_byM( m5 ) && m5.IsLongerAndMoreVerticalThan( m1 ) )
                    {
                        m1.DropStructureLabel( StructureLabelEnum._5 );
                    }
                }
            }
        }

        private void Rule3ConditionE( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            if ( m1 == null || m2 == null || m3 == null )
                return;

            if ( m3.IsMoreThanX_ofM( 261.8, m2 ) )
            {
                m1.MainWaveType = WaveType.ComplexCorrection;
                m1.WavePattern |= WavePattern.ZigZag | WavePattern.CFailureFlat | WavePattern.ContractingTriangle;
                m1.AddStructureLabel( StructureLabelEnum._5 | StructureLabelEnum.c3 | StructureLabelEnum.sL3_LESSLIKELY );

                if ( m1_.IsLessThanX_OfM( 61.8, m0 ) || m1_.IsMoreThanX_ofM( 161.8, m0 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.sL3_MAYBE );
                }

                if ( m2.IsCompletelyRetraced_Slower_byM( m3 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.sL3_MAYBE );
                    m1.DropStructureLabel( StructureLabelEnum.c3 );
                }

                if ( m3.IsMoreThanX_ofM( 161.8, m1 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum._5 );
                }
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m3 is between 161.8 and 261.8% (inclusive) of m2, 
             *          m1 may be the first leg of a Zigzag or 
             *          the center section of a C-Failure Flat which concludes a Complex Correction (with a "missing" x-wave in the middle of m0); 
             *                  place ";5/;c3" at the end of m1 and put a dot in the middle of m0 with "x:c3?" to the right and ":s5?" to the left of the dot. 
             * 
             * If m2 is retraced slower than it took to form, drop ":c3" from the list. If m3 is more than 161.8% of m1, drop ":5" from the list.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            var m4 = m3.GetNext( );

            if ( m3.IsBtw_XYInclusive_ofM( 161.8, 261.8, m2 ) )
            {
                m1.MainWaveType = WaveType.ComplexCorrection;
                m1.WavePattern |= WavePattern.ZigZag | WavePattern.CFailureFlat;
                m1.AddStructureLabel( StructureLabelEnum._5 | StructureLabelEnum.c3 );
                m0.AddStructureLabel( StructureLabelEnum.x_c3_MAYBE | StructureLabelEnum.s5_MAYBE );

                if ( m2.IsCompletelyRetraced_Slower_byM( m3 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.c3 );
                }

                if ( m3.IsMoreThanX_ofM( 161.8, m1 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum._5 );
                }
            }

            if ( m3.IsAtLeastX_LessThanY_ofM( 100, 161.8, m2 ) )
            {
                m1.MainWaveType = WaveType.ComplexCorrection;
                m1.WavePattern |= WavePattern.ZigZag | WavePattern.ContractingTriangle;
                m1.AddStructureLabel( StructureLabelEnum._5 | StructureLabelEnum.F3_LESSLIKELY );

                if ( m4.MonoWavesCount == 0 && m4.IsLongerThan( m3 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.F3_LESSLIKELY );
                }
            }
        }

        private void Rule3ConditionF( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            if ( m1 == null || m2 == null || m3 == null )
                return;

            if ( m3.IsMoreThanX_ofM( 261.8, m2 ) )
            {
                m1.MainWaveType = WaveType.ComplexCorrection;
                m1.WavePattern |= WavePattern.ZigZag | WavePattern.CFailureFlat;
                m1.AddStructureLabel( StructureLabelEnum._5 | StructureLabelEnum.c3_LESSLIKELY );

                if ( m2.IsCompletelyRetraced_Slower_byM( m3 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.c3_LESSLIKELY );
                }

                if ( m3.IsMoreThanX_ofM( 161.8, m1 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum._5 );
                }

                if ( m1.PreferableStructureIs( StructureLabelEnum.c3_LESSLIKELY ) )
                {
                    if ( !m1.Overlap( m1_ ) )
                    {
                        m0.AddStructureLabel( StructureLabelEnum.x_c3_MAYBE | StructureLabelEnum.s5_MAYBE );
                    }
                }
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m3 is between 161.8 and 261.8% (inclusive) of m2, 
             *          m1 may be the first leg of a Zigzag or the 
             *          center section of a C-Failure Flat which concludes a Complex Correction (with a "missing" x-wave in the middle of m0); 
             *                  place ":5/(:c3)" at the end of m1. 
             *                  
             *  If m3 is longer than m2, drop "(:c3)" from the list. 
             *  If m2 is retraced slower than it took to form, drop "(:c3)" from the list. 
             *  If m3 is more than 161.8% of m1, drop ":5" from the list. 
             *  
             *  If the "(:c3)" Structure label is used for m1 and m(-1) shares no similar price territory with m1, 
             *          mark the middle of m0 with a dot and place "x:c3?" to the right of it and 
             *          ":s5" to the left of the dot to represent m0’s "missing" x-wave possibility.
             *          
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            var m4 = m3.GetNext( );

            if ( m3.IsBtw_XYInclusive_ofM( 161.8, 261.8, m2 ) )
            {
                m1.MainWaveType = WaveType.ComplexCorrection;
                m1.WavePattern |= WavePattern.ZigZag | WavePattern.CFailureFlat;
                m1.AddStructureLabel( StructureLabelEnum._5 | StructureLabelEnum.c3_LESSLIKELY );

                if ( m3.IsLongerThan( m2 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.c3_LESSLIKELY );
                }

                if ( m2.IsCompletelyRetraced_Slower_byM( m3 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.c3_LESSLIKELY );
                }

                if ( m3.IsMoreThanX_ofM( 161.8, m1 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum._5 );
                }
            }

            if ( m3.IsAtLeastX_LessThanY_ofM( 100, 161.8, m2 ) )
            {
                m1.MainWaveType = WaveType.ComplexCorrection;
                m1.WavePattern |= WavePattern.ZigZag | WavePattern.ContractingTriangle;
                m1.AddStructureLabel( StructureLabelEnum._5 | StructureLabelEnum.F3_LESSLIKELY );

                if ( m4.MonoWavesCount == 0 && m4.IsLongerThan( m3 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.F3_LESSLIKELY );
                }
            }
        }
    }
}
