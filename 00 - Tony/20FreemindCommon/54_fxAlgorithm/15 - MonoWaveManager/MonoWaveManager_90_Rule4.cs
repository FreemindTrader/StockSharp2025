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
        //![](D6574B6F5757740F0F6F9FA8023F7307.png;;;0.02564,0.02204)
        private void Rule4( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            if ( m0 == null )
                return;

            double m0m1Ratio = m0.Over( m1 );

            switch ( m0m1Ratio )
            {
                case double p when ( p < 38.2 ):
                {
                    Rule4ConditionA( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;

                case double p when ( p >= 38.2 && p < 100 ):
                {
                    Rule4ConditionB( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;

                case double p when ( p >= 100 && p < 161.8 ):
                {
                    Rule4ConditionC( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;

                case double p when ( p >= 161.8 && p <= 261.8 ):
                {
                    Rule4ConditionD( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;

                case double p when ( p > 261.8 ):
                {
                    Rule4ConditionE( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;
            }
        }

        private void Rule4ConditionA( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            if ( m1 == null || m2 == null || m3 == null )
                return;

            double m3m2Ration = m3.Over( m2 );

            switch ( m3m2Ration )
            {
                case double p when ( p >= 100 && p < 161.8 ):
                {
                    Rule4ConditionACategoriy1( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;

                case double p when ( p >= 161.8 && p <= 261.8 ):
                {
                    Rule4ConditionACategoriy2( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;

                case double p when ( p > 261.8 ):
                {
                    Rule4ConditionACategoriy3( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;
            }
        }

        private void Rule4ConditionACategoriy2( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            if ( m1 == null || m2 == null || m3 == null )
                return;

            if ( m1_.IsMoreThanX_ofM( 261.8, m1 ) )
            {
                m1.AddStructureLabel( StructureLabelEnum.F3 );
            }

            var m4 = m3.GetNext( );

            if ( m4 == null )
                return;

            if ( m4.IsLongerThan( m3 ) )
            {
                m1.AddStructureLabel( StructureLabelEnum.F3 );
            }

            if ( m4.IsRetraced_LtX_byM( 100, m4 ) )
            {
                m1.AddStructureLabel( StructureLabelEnum.s5 );
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m1 is retraced no more than 70% and 
             *  m1 is from 101-161.8% of m(-1) and 
             *  none of the price range covered by m2 is shared by m0 and 
             *  m(-2) is longer than m(-1), 
             *          m1 may conclude the 3rd wave of a Trending 5th Extension Impulse pattern. 
             *          
             *  If m1 is between 161.8-261.8% of m(-1), it becomes more likely that 
             *          m1 is the end of a Zigzag inside of a Complex Correction with 
             *          m2 the end of an "x-wave". 
             * 
             * But, the 3rd wave idea is still possible as long as you identify the 3rd wave as part of a Double Extension Impulse pattern (see diagram; Chapter 12, page 12) with the 5th wave the longest. If m1 is more than 261.8% of m(-1), the Complex corrective scenario is the only proper choice.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( m1.IsRetraced_LtX_byM( 70, m2 ) && !m2.Overlap( m0 ) && m2_.IsLongerThan( m1_ ) )
            {
                if ( m1.IsBtw_XYInclusive_ofM( 101, 161.8, m1_ ) )
                {
                    m1.MainWaveType = WaveType.Impulsive5Waves;
                    m1.WavePattern |= WavePattern.TrendingImpulse;
                    m1.PotentialWave = ElliottWaveEnum.Wave3;
                }

                if ( m1.IsBtw_XYExclusive_ofM( 161.8, 261.8, m1_ ) )
                {
                    m1.MainWaveType = WaveType.ComplexCorrection;
                    m1.WavePattern |= WavePattern.DoubleZigZag;
                    m1.PotentialWave = ElliottWaveEnum.WaveC;
                    m2.PotentialWave = ElliottWaveEnum.WaveX;
                }

                if ( m1.IsMoreThanX_ofM( 261.8, m1_ ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.s5 );
                }
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m1 is retraced no more than 70% and m1 is at least 100, 
             *  but less than 161.8% of m(-1) and 
             *  some of the price range covered by m2 is shared by m0 and 
             *  m(-2) is longer than m(-1), then 
             *          m1 may conclude the 3rd wave of a Terminal 5th Extension Impulse pattern. 
             *          
             *  If m1 is between 161.8-261.8% (inclusive) of m(-1), 
             *          it becomes more likely m1 is the end of a Zigzag inside of a Complex Correction with 
             *          m2 the end of an "x-wave". 
             *          
             *  But, the 3rd wave idea is still possible as long as you identify the 3rd wave as part of a Double Extension Terminal Impulse pattern 
             *  with the 5th wave the longest. 
             *  
             *  If m1 is more than 261.8% of m(-1), the Complex Corrective scenario is the only proper choice.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( m1.IsRetraced_LtX_byM( 70, m2 ) && m2.Overlap( m0 ) && m2_.IsLongerThan( m1_ ) )
            {
                if ( m1.IsBtw_AtLeastX_LessThanY_ofM( 100, 161.8, m1_ ) )
                {
                    m1.WavePattern |= WavePattern.TerminalImpulse;
                    m1.PotentialWave = ElliottWaveEnum.Wave3;
                }

                if ( m1.IsBtw_XYInclusive_ofM( 161.8, 261.8, m1_ ) )
                {
                    m1.MainWaveType = WaveType.ComplexCorrection;
                    m1.WavePattern |= WavePattern.DoubleZigZag;
                    m1.PotentialWave = ElliottWaveEnum.WaveC;
                    m2.PotentialWave = ElliottWaveEnum.WaveX;
                }

                if ( m1.IsMoreThanX_ofM( 261.8, m1_ ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.s5 );
                }
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  3.	If m1 is retraced no more than 70% and m1 is smaller than m(-1), m1 can only be part of a Zigzag pattern.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */
            if ( m1.IsRetraced_LtX_byM( 70, m2 ) && m1.IsShorterThan( m1_ ) )
            {
                m1.MainWaveType = WaveType.Correction;
                m1.WavePattern |= WavePattern.ZigZag;
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  4.	If m1 is retraced more than 70%, it is most likely m1 terminated a Zigzag. But, 
             *  
             *  if the price ranges of m0 and m2 share some common territory and 
             *  m3 is retraced faster than it took to form, m1 may be the 3rd wave of a 5th Extension Terminal Impulse pattern.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( m1.IsRetraced_AtLeastX_byM( 70, m2 ) )
            {
                m1.MainWaveType = WaveType.Correction;
                m1.WavePattern |= WavePattern.ZigZag;
                m1.PotentialWave = ElliottWaveEnum.WaveC;

                if ( m0.Overlap( m2 ) && m3.IsCompletelyRetraced_Faster_byM( m4 ) )
                {
                    m1.MainWaveType = WaveType.TerminalImpulse;
                    m1.WavePattern |= WavePattern.TerminalImpulse;
                    m1.PotentialWave = ElliottWaveEnum.Wave3;
                }
            }
        }

        private void Rule4ConditionACategoriy3( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            if ( m1 == null || m2 == null || m3 == null )
                return;

            if ( m1_.IsMoreThanX_ofM( 261.8, m1 ) )
            {
                m1.AddStructureLabel( StructureLabelEnum.F3 );
            }

            var m4 = m3.GetNext( );

            if ( m4 != null )
            {
                if ( m3.IsCompletelyRetraced_byM( m4 ) )
                {
                    m1.AddStructureLabel( StructureLabelEnum.F3 );
                }

                if ( m3.IsRetraced_LtX_byM( 100, m4 ) )
                {
                    m1.AddStructureLabel( StructureLabelEnum.s5 );
                }
            }


        }

        private void Rule4ConditionACategoriy1( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            if ( m1 == null || m2 == null || m3 == null )
                return;

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m3 (plus one time unit) is completely retraced slower than it took to form, 
             *          m1 should be the first leg of a correction which follows an x-wave (m0) or 
             *          the end of a corrective phase which is part of a larger Standard or Non-Standard formation; 
             *                  place ":F3/:s5" at the end of m1. 
             *                  
             *  If ":F3" is chosen, m1 is wave-a of a Flat correction. 
             *  If ":s5" is appropriate, m1 is the end of a Zigzag pattern. 
             *  
             *  If m1 is less than 61.8% of m(-1), drop ":s5" from the Structure list. 
             *  
             *  If m0 simultaneously takes less time than m(-1) and m1, drop ":s5" from m1's list.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */
            if ( m3.Plus1TU_IsCompletelyRetraced_Slower_byM( m2 ) )
            {
                m1.MainWaveType = WaveType.ComplexCorrection;
                m1.WavePattern |= WavePattern.DoubleZigZag | WavePattern.MoreThanOneEWPatternEnd;
                m1.PotentialWave = ElliottWaveEnum.WaveA;
                m1.AddStructureLabel( StructureLabelEnum.F3 | StructureLabelEnum.s5 );

                if ( m1.PreferableStructureIs( StructureLabelEnum.F3 ) )
                {
                    m1.WavePattern |= WavePattern.Flat;
                    m1.PotentialWave = ElliottWaveEnum.WaveA;
                }

                if ( m1.PreferableStructureIs( StructureLabelEnum.s5 ) )
                {
                    m1.WavePattern |= WavePattern.ZigZag;
                    m1.PotentialWave = ElliottWaveEnum.WaveC;
                }

                if ( m1.IsLessThanX_OfM( 61.8, m1_ ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.s5 );
                }
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m3 is completely retraced in the same amount of time (or less) that it took to form, 
             *          there is virtually no chance m1 completed an Elliott pattern; 
             *                  place only ":F3/:c3" at the end of m1. 
             * 
             *  If m1 is retraced no more than 70% and 
             *  none of m2's price range is shared by m0 and 
             *  m3 is almost 161.8% of m1 and m0 takes more time than m(-1) or 
             *  m0 takes more time than m1, 
             *          add ":s5" to the Structure list. 
             *          
             *  If none of m2's price range is shared by m0, 
             *          drop ":c3" as a choice. 
             *  
             *  If ":F3" is used, 
             *                  m1 would be wave-a of a correction within a larger complex formation; 
             *                  m0 would be an x-wave.
             *                  
             *  If ":c3" is still a possibility, m1 may be part of an Expanding Triangle or a Terminal Impulse pattern. 
             *  If ":s5" is a possibility, it would be wave-3 of a 5th Extension Impulse pattern.
             *                   
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            var m4 = m3.GetNext( );

            if ( m4 != null )
            {
                if ( m3.IsCompletelyRetraced_Faster_byM( m4 ) )
                {
                    m1.AddStructureLabel( StructureLabelEnum.F3 | StructureLabelEnum.c3 );

                    if ( m1.IsRetraced_LtX_byM( 70, m2 ) && !m2.Overlap( m0 ) && m3.IsAtMostX_OfM( 161.8, m1 ) )
                    {
                        if ( m0.TimeUnit > m1_.TimeUnit || m0.TimeUnit > m1.TimeUnit )
                        {
                            m1.AddStructureLabel( StructureLabelEnum.s5 );
                        }
                    }

                    if ( !m2.Overlap( m0 ) )
                    {
                        m1.DropStructureLabel( StructureLabelEnum.c3 );
                    }

                    if ( m1.PreferableStructureIs( StructureLabelEnum.F3 ) )
                    {
                        m1.WavePattern |= WavePattern.AnyComplexCorrection;
                        m1.PotentialWave = ElliottWaveEnum.WaveA;
                        m0.PotentialWave = ElliottWaveEnum.WaveX;
                    }

                    if ( m1.PreferableStructureIs( StructureLabelEnum.c3 ) )
                    {
                        m1.WavePattern |= WavePattern.TerminalImpulse | WavePattern.ExpandingTriangle;
                    }

                    if ( m1.PreferableStructureIs( StructureLabelEnum.s5 ) )
                    {
                        m1.WavePattern |= WavePattern.TrendingImpulse;
                        m1.PotentialWave = ElliottWaveEnum.Wave3;
                    }
                }

                /* -------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *  If m3 is retraced less than 100%, 
                 *          place ":F3/:s5" at the end of m1. 
                 * 
                 * If m2 is composed of more than three monowaves and 
                 * m2 (plus one time unit) is completely retraced faster than it took to form and 
                 * m2 takes more time than m1 and 
                 * m2 breaks a line draw across the end of m(-2) and m0 faster than m1 took to form, 
                 *          then m1 may be the end of a Zigzag contained within an Irregular or Running Correction; 
                 *          add ":L5" to the Structure list at the end of m1. 
                 *          The ":L5" in this case was justified based on the two stages of confirmation possible when dealing with polywave patterns (see Chapter 6 for further details). 
                 *          
                 * If m0 simultaneously takes less time than m(-1) and m1, drop ":s5" from list. If ":F3" is chosen, m1 is wave-a of a Flat or Triangle correction. If ":s5" is appropriate, m1 is the end of a Zigzag pattern.
                 * ------------------------------------------------------------------------------------------------------------------------------------------- */

                if ( m3.IsRetraced_LtX_byM( 100, m4 ) )
                {
                    m1.AddStructureLabel( StructureLabelEnum.F3 | StructureLabelEnum.s5 );

                    if ( m2.MonoWavesCount > 3 && m2.Plus1TU_IsCompletelyRetraced_Faster_byM( m3 ) )
                    {
                        if ( m2.TimeUnit > m1.TimeUnit && m2.BreakTrendLineBtwXYInLETimeToZ( m2_, m0, m1 ) )
                        {
                            m1.MainWaveType = WaveType.ComplexCorrection;
                            m1.WavePattern |= WavePattern.Irregular | WavePattern.RunningCorrection;
                            m1.PotentialWave = ElliottWaveEnum.WaveC;
                            m1.AddStructureLabel( StructureLabelEnum.L5 );
                        }
                    }

                    if ( m0.TimeUnit < m1_.TimeUnit && m0.TimeUnit < m1.TimeUnit )
                    {
                        m1.DropStructureLabel( StructureLabelEnum.s5 );
                    }

                    if ( m1.PreferableStructureIs( StructureLabelEnum.F3 ) )
                    {
                        m1.WavePattern |= WavePattern.Flat | WavePattern.ContractingTriangle;
                        m1.PotentialWave = ElliottWaveEnum.WaveA;
                    }

                    if ( m1.PreferableStructureIs( StructureLabelEnum.s5 ) )
                    {
                        m1.WavePattern |= WavePattern.ZigZag;
                        m1.PotentialWave = ElliottWaveEnum.WaveC;
                    }
                }
            }
        }

        private void Rule4ConditionB( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            // ![](BB32B9D30C486EFAEF0DD0909CE145EC.png;;;0.03289,0.03401)
            // ![](BB54421326C4F701F8EF37326AE6C29C.png;;;0.03698,0.02078)

            double m3m2Ration = m3.Over( m2 );

            switch ( m3m2Ration )
            {
                case double p when ( p >= 100 && p < 161.8 ):
                {
                    Rule4ConditionBCategory1( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;

                case double p when ( p >= 161.8 && p <= 261.8 ):
                {
                    Rule4ConditionBCategory2( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;

                case double p when ( p > 261.8 ):
                {
                    Rule4ConditionBCategory3( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;
            }

        }

        private void Rule4ConditionBCategory3( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            if ( m1 == null || m2 == null || m3 == null )
                return;

            var m4 = m3.GetNext( );
            if ( m4 == null ) return;

            var m5 = m4.GetNext( );
            if ( m5 == null ) return;

            var m6 = m5.GetNext( );
            if ( m6 == null ) return;

            bool noneApply = true;

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m(-1) is more than 261.8% of m1, there is virtually no chance m1 is the end of any Elliott formation; 
             *          place only ":c3/(:F3)" at the end of m1. 
             *          
             *  If the end of m1 is exceeded during the formation of m2, add an "x" in front of ":c3."
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( m1_.IsMoreThanX_ofM( 261.8, m1 ) )
            {
                // ![](EE82FB24EC0E6E06CAF428C968E3912B.png;;;0.03421,0.03366)
                m1.AddStructureLabel( StructureLabelEnum.F3_LESSLIKELY | StructureLabelEnum.c3 );

                if ( m1.EndExceededBy( m2 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.c3 );
                    m1.AddStructureLabel( StructureLabelEnum.x_c3 );
                }

                noneApply = false;
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m(-1) is at least 161.8% of m1 and 
             *  m0 is retraced slower than it took to form and 
             *  m1 takes 161.8% of the time of m0 (or more), it is almost certain 
             *  
             *          m0-m2 form an Irregular Failure Flat, 
             *          place ":c3/(:F3)" at the end of m1. 
             * 
             * If the end of m1 is exceeded during the formation of m2, add an "x" in front of ":c3."
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( 
                    m1_.IsAtLeastX_OfM( 161.8, m1 ) && 
                    m0.IsCompletelyRetraced_Slower_byM( m1 ) && 
                    m1.IsAtLeastX_Time_OfX( 161.8, m0 ) 
               )
            {
                m0.MainWaveType = WaveType.Correction;
                m0.WavePattern |= WavePattern.IrregularFailureFlat;
                m1.WavePattern |= WavePattern.IrregularFailureFlat;
                m2.WavePattern |= WavePattern.IrregularFailureFlat;
                noneApply = false;

                m1.AddStructureLabel( StructureLabelEnum.F3_LESSLIKELY | StructureLabelEnum.c3 );
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m1 is the longest when compared to m(-1) and m(-3) and 
             *  m2 breaks a trendline drawn across the low of m(-2) and m0 in a period of time equal to or less than that taken by m1, 
             *  
             *          m1 may be the 5th wave of a 5th Extension pattern; add [:L5] to the end of m1.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( m1.IsTheLongest( m1_, m3_ ) && m2.BreakTrendLineBtwXYInLETimeToZ( m2_, m0, m1 ) )
            {
                m1.MainWaveType = WaveType.Impulsive5Waves;
                m1.WavePattern |= WavePattern.TrendingImpulse;
                m1.WavePosition = WavePosition.End;
                m1.PotentialWave = ElliottWaveEnum.Wave5;
                noneApply = false;

                m1.AddStructureLabel( StructureLabelEnum.L5_RARE );
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m3 is retraced less than 61.8%, it is extremely unlikely m1 is the beginning of any Elliott pattern, 
             *          place only ":F3/:c3/(:s5)" at the end of m1. 
             * 
             *  If ":F3" is used, an Elongated Flat begins with the start of m1. 
             *  
             *  If m1 is the longest when it is compared to m(-1) and m(-3) and 
             *  m2 breaks a trendline drawn across the low of m(-2) and m0 in a period of time equal to or less than that taken by m1, 
             *  
             *          m1 may be the 5th wave of a 5th Extension pattern; add [:L5] to the end of m1. 
             *          
             * If m0 simultaneously takes less time than m(-1) and m1, drop ":s5" from list. If the end of m1 is exceeded during the formation of m2, add an "x" in front of ":c3."
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( m3.IsRetraced_LtX_byM( 61.8, m4 ) )
            {
                // ![](07C82C454FFB4C7C00EF3635ED5BF5EC.png;;;0.03458,0.02384)
                m1.AddStructureLabel( StructureLabelEnum.F3 | StructureLabelEnum.c3 | StructureLabelEnum.s5_LESSLIKELY );

                if ( m1.PreferableStructureIs( StructureLabelEnum.F3 ) )
                {
                    m1.WavePattern |= WavePattern.ElongatedFlat;
                    m2.WavePattern |= WavePattern.ElongatedFlat;
                }

                if ( 
                        m1.IsTheLongest( m1_, m3_ ) &&
                        m2.BreakTrendLineBtwXYInLETimeToZ( m2_, m0, m1 )
                   )
                {
                    m1.WavePattern |= WavePattern.TrendingImpulse;
                    m1.PotentialWave = ElliottWaveEnum.Wave5;
                    m1.AddStructureLabel( StructureLabelEnum.L5_RARE );
                }

                if ( 
                        m0.TimeUnit < m1_.TimeUnit && 
                        m0.TimeUnit < m1.TimeUnit 
                   ) 
                {
                    m1.DropStructureLabel( StructureLabelEnum.s5 );
                }

                noneApply = false;
            }

            if ( noneApply )
            {
                m1.AddStructureLabel( StructureLabelEnum.F3 | StructureLabelEnum.c3 | StructureLabelEnum.sL3 | StructureLabelEnum.s5 );

                if ( m1.IsTheLongest( m1_, m3_ ) && m2.BreakTrendLineBtwXYInLETimeToZ( m2_, m0, m1 ) )
                {
                    m1.MainWaveType = WaveType.Impulsive5Waves;
                    m1.WavePattern |= WavePattern.TrendingImpulse;
                    m1.WavePosition = WavePosition.End;
                    m1.PotentialWave = ElliottWaveEnum.Wave5;

                    m1.AddStructureLabel( StructureLabelEnum.L5_RARE );
                }

                if ( m1.IsLessThanX_OfM( 61.8, m1_ ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.s5 );
                }

                if ( m3.IsRetraced_LtX_byM( 61.8, m4 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.F3 );
                }

                if ( m0.TimeUnit < m1_.TimeUnit && m0.TimeUnit < m1.TimeUnit )
                {
                    m1.DropStructureLabel( StructureLabelEnum.s5 );
                }
            }
        }

        private void Rule4ConditionBCategory2( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            // ![](2EF9A9C9AC7E0FB073561661CD30A88E.png;;;0.03542,0.02508)
            if ( m1 == null || m2 == null || m3 == null )
                return;

            var m4 = m3.GetNext( );
            if ( m4 == null )
                return;

            var m5 = m4.GetNext( );
            if ( m5 == null )
                return;

            var m6 = m5.GetNext( );

            bool noneApply = true;

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m(-1) is more than 261.8% of m1, there is virtually no chance m1 is the end of any Elliott formation; 
             *  place only ":F3/:c3" at the end of m1. 
             *  
             *  If the end of m1 is exceeded during the formation of m2, add an "x" in front of ":c3".
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( m1_.IsMoreThanX_ofM( 261.8, m1 ) )
            {
                m1.AddStructureLabel( StructureLabelEnum.F3 | StructureLabelEnum.c3 );
                noneApply = false;

                if ( m1.EndExceededBy( m2 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.c3 );
                    m1.AddStructureLabel( StructureLabelEnum.x_c3 );
                }
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m1 is the longest wave when compared to m(-1) and m(-3) and 
             *  m2 (plus one time unit) breaks a trendline drawn across the low of m(-2) and m0 in a period of time equal to or less than that taken by m1, 
             *  
             *  m1 may be the 5th wave of a 5th Extension pattern; add [:L5] to the end of m1.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( m1.IsTheLongest( m1_, m3_ ) && m2.BreakTrendLineBtwXYInLETimeToZ( m2_, m0, m1 ) )
            {
                m1.MainWaveType = WaveType.Impulsive5Waves;
                m1.WavePattern |= WavePattern.TrendingImpulse;
                m1.WavePosition = WavePosition.End;
                m1.PotentialWave = ElliottWaveEnum.Wave5;

                m1.AddStructureLabel( StructureLabelEnum.L5_RARE );
                noneApply = false;
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             * If m3 is retraced less than 61.8%, it is extremely unlikely m1 is the beginning of any Elliott pattern, 
             *          place only ":c3/(:sL3)/(:s5)" at the end of m1. 
             * If the end of m1 is exceeded during the formation of m2, 
                        * add an "x" in front of ":c3." If m3 through m5 do not achieve a price distance of 161.8% (or more) of m1, drop ":sL3" from the list. If m0 (plus one time unit) simultaneously takes less time than m(-1) and m1, drop ":s5" from list. If m2 is completely retraced slower than it took to form, drop ":sL3" from the list NOTE: if ":sL3" is used, the Triangle (which concludes with m2) is Non-Limiting. 
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( m3.IsRetraced_LtX_byM( 61.8, m4 ) )
            {
                m1.AddStructureLabel( StructureLabelEnum.c3 | StructureLabelEnum.sL3_LESSLIKELY | StructureLabelEnum.s5_LESSLIKELY );

                if ( m5 != null )
                {
                    var m3Tom5 = m3.Combine( m4, m5 );

                    if ( m3Tom5.IsLessThanX_OfM( 161.8, m1 ) )
                    {
                        m1.DropStructureLabel( StructureLabelEnum.s5 );
                    }

                    if ( m2.IsCompletelyRetraced_Slower_byM( m3 ) )
                    {
                        m1.DropStructureLabel( StructureLabelEnum.sL3 );
                    }
                }

                noneApply = false;
            }


            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If neither of the conditions above applies, place ":F3/:c3/:sL3/:s5" at the end of m1. 
             *  If the end of m1 is exceeded during the formation of m2, add "x:c3" to the Structure list. 
             *  If m1 is the longest when compared to m(-1) and m(-3) and 
             *  m2 breaks a trendline drawn across the low of m(-2) and m0 in a period of time equal to or less than that taken by m1, 
             *  
             *          m1 may be the 5th wave of a 5th Extension pattern; add [:L5] to the end of m1. 
             * 
             *  When comparing m(-1), m1 and m3, if m1 is the shortest of the three and 
             *  m3 (plus one time unit) is completely retraced faster than it took to form, 
             *          drop ":c3" as a possibility. 
             *  
             *  If m1 is less than 61.8% of m(-1), drop ":s5" from the list. 
             *  
             *  If m3 is retraced less than 61.8%, drop ":F3" as a possibility. 
             *  
             *  If m0 simultaneously takes less time than m(-1) and m1, drop ":s5" from the list. 
             *  
             *  If m2 (plus one time unit) is retraced slower than it took to form, drop ":sL3" from the list.
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( noneApply )
            {
                m1.AddStructureLabel( StructureLabelEnum.F3 | StructureLabelEnum.c3 | StructureLabelEnum.sL3 | StructureLabelEnum.s5 );

                if ( 
                     m1.IsTheShortest( m1_,  m3_ ) && 
                     m3.Plus1TU_IsCompletelyRetraced_Faster_byM( m4 ) 
                   )
                {
                    m1.DropStructureLabel( StructureLabelEnum.c3 );
                }

                if ( m1.IsLessThanX_OfM( 61.8, m1_ ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.s5 );
                }

                if ( m3.IsRetraced_LtX_byM( 61.8, m4 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.F3 );
                }

                if ( m0.TimeUnit < m1_.TimeUnit && m0.TimeUnit < m1.TimeUnit )
                {
                    m1.DropStructureLabel( StructureLabelEnum.s5 );
                }

                if ( m2.Plus1TU_IsCompletelyRetraced_Slower_byM( m3 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.sL3 );
                }
            }
        }

        private void Rule4ConditionBCategory1( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            if ( m1 == null || m2 == null || m3 == null )
                return;

            var m4 = m3.GetNext( );

            if ( m4 == null )
                return;

            var m5 = m4.GetNext( );

            if ( m5 == null )
                return;

            var m6 = m5.GetNext( );

            if ( m6 == null )
                return;

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m3 (plus one time unit) is completely retraced in the same amount of time (or less) that it took to form, 
             *  there is virtually no chance m1 completed an Elliott pattern; 
             *          place only ":F3/:c3" at the end of m1. 
             *          
             *  If later it is found that ":c3" is the preferred choice, m1 may be part of a Terminal Impulse pattern. 
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( m3.Plus1TU_IsCompletelyRetraced_Faster_byM( m4 ) )
            {
                m1.AddStructureLabel( StructureLabelEnum.F3 | StructureLabelEnum.c3 );

                if ( m1.PreferableStructureIs( StructureLabelEnum.c3 ) )
                {
                    m1.WavePattern |= WavePattern.TerminalImpulse;

                    var m3BrokenLevel   = m3.End;
                    var m0BrokenLevel   = m0.End;
                    bool breakM3First   = false;

                    var testingMonowave = m4;

                    /* -------------------------------------------------------------------------------------------------------------------------------------------
                     * 
                     *  If the end of m3 is exceeded before the end of m0 and 
                     *  m1 is the longest when it is compared to m(-1) and m(-3) and 
                     *  m2 breaks a trendline drawn across the low of m(-2) and m0 in a period of time equal to or less of that taken by m1,                        
                     *          m1 may be the 5th wave of a 5th Extension pattern; add [:L5] to the end of m1.
                     * 
                     * ------------------------------------------------------------------------------------------------------------------------------------------- */

                    do
                    {
                        if ( testingMonowave.Break( m3BrokenLevel ) )
                        {
                            breakM3First = true;
                            break;
                        }

                        if ( testingMonowave.Break( m0BrokenLevel ) )
                        {
                            breakM3First = false;
                            break;
                        }

                        testingMonowave = testingMonowave.GetNext( );

                    } while ( testingMonowave != null );

                    if ( breakM3First && m1.IsLongerThan( m1_ ) && m1.IsLongerThan( m3_ ) )
                    {
                        if ( m2.BreakTrendLineBtwXYInLETimeToZ( m2_, m0, m1 ) )
                        {
                            m1.MainWaveType = WaveType.Impulsive5Waves;
                            m1.WavePattern |= WavePattern.TrendingImpulse;
                            m1.WavePosition = WavePosition.End;
                            m1.PotentialWave = ElliottWaveEnum.Wave5;

                            m1.AddStructureLabel( StructureLabelEnum.L5_RARE );
                        }
                    }
                }
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m3 is completely retraced slower than it took to form; place ":F3/:c3/:s5" at the end of m1. 
             *  If the end of m1 is exceeded during the formation of m2, add an "x" in front of ":c3." 
             *  
             *  If the end of m3 is exceeded before the end of m0 and m1 is the longest when compared to m(-1) and m(-3) and m2 breaks a trendline drawn across the low of m(-2) and m0 in a period of time equal to or less than that taken by m1, m1 may be the 5th wave of a 5th Extension pattern; add [:L5] to m1's Structure list. If m1 is less than 61.8% of m(-1), drop ":s5" from the list. If m(-1) is 161.8% (or more) of m1 and m3 is retraced less than 61.8%, drop ":F3" as a possibility. If m0 (plus one time unit) simultaneously takes less time than m(-1) and m1, drop ":s5" from list.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */
            if ( m3.IsCompletelyRetraced_Slower_byM( m4 ) )
            {
                m1.AddStructureLabel( StructureLabelEnum.F3 | StructureLabelEnum.c3 | StructureLabelEnum.s5 );

                var m3BrokenLevel   = m3.End;
                var m0BrokenLevel   = m0.End;
                bool breakM3First   = false;

                var testingMonowave = m4;

                /* -------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *  If the end of m3 is exceeded before the end of m0 and 
                 *  m1 is the longest when it is compared to m(-1) and m(-3) and 
                 *  m2 breaks a trendline drawn across the low of m(-2) and m0 in a period of time equal to or less of that taken by m1,                        
                 *          m1 may be the 5th wave of a 5th Extension pattern; add [:L5] to the end of m1.
                 * 
                 * ------------------------------------------------------------------------------------------------------------------------------------------- */

                do
                {
                    if ( testingMonowave.Break( m3BrokenLevel ) )
                    {
                        breakM3First = true;
                        break;
                    }

                    if ( testingMonowave.Break( m0BrokenLevel ) )
                    {
                        breakM3First = false;
                        break;
                    }

                    testingMonowave = testingMonowave.GetNext( );

                } while ( testingMonowave != null );

                if ( breakM3First && m1.IsLongerThan( m1_ ) && m1.IsLongerThan( m3_ ) )
                {
                    if ( m2.BreakTrendLineBtwXYInLETimeToZ( m2_, m0, m1 ) )
                    {
                        m1.MainWaveType = WaveType.Impulsive5Waves;
                        m1.WavePattern |= WavePattern.TrendingImpulse;
                        m1.WavePosition = WavePosition.End;
                        m1.PotentialWave = ElliottWaveEnum.Wave5;

                        m1.AddStructureLabel( StructureLabelEnum.L5_RARE );
                    }
                }

                if ( m1.IsLessThanX_OfM( 61.8, m1_ ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.s5 );
                }

                if ( m1_.IsAtLeastX_OfM( 161.8, m1 ) && m3.IsRetraced_LtX_byM( 61.8, m4 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.F3 );
                }

                if ( ( m0.TimeUnit + 1 < m1_.TimeUnit ) && ( m0.TimeUnit + 1 < m1.TimeUnit ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.s5 );
                }
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m3 is retraced less than 100%, it is extremely unlikely m1 is the beginning of any Elliott pattern; 
             *          place ":c3/:s5" at the end of m1. 
             *          
             *  If the end of m1 is exceeded during the formation of m2, add an "x" in front of ":c3," 
             *  
             *  If m2 is composed of more than three monowaves and 
             *  m2 is completely retraced faster than it took to form and 
             *  m2 takes more time than m1 and 
             *  m(-1) is at least 161.8% of m0 and 
             *  m2 breaks a line which is draw across the end of m(-2) and m0 faster than m1 took to form, 
             *          then m1 may be the end of a Zigzag contained within an Irregular or Running Correction; 
             *                  add ":L5" to the Structure list at the end of m1. 
             *                  The ":L5" in this case was justified based on the two stages of confirmation possible when dealing with polywave patterns (see Chapter 6 for further details). 
             *                  
             *  If m0 (plus one time unit) takes less time than m(-1) and less time than m1, drop ":s5" from list. 
             *  
             *  If m(-2) is longer than m(-1) and ":c3" in the list is not preceded by an "x", drop ":c3" from the list. 
             *  
             *  If m5 is not completely retraced as fast as it took to form, drop ":c3" from the list. 
             *  
             *  If ":c3" is still a possibility, m1 may be the x-wave of a Complex Correction; add "x:c3?" as an additional Structure label to m1's list.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( m3.IsRetraced_LtX_byM( 100, m4 ) )
            {
                // ![](245C8BE2E865CE118AF884BF96F5298F.png;;;0.02070,0.02012)
                m1.AddStructureLabel( StructureLabelEnum.c3 | StructureLabelEnum.s5 );

                if ( m2.MonoWavesCount > 3 && m2.IsCompletelyRetraced_Faster_byM( m3 ) && m2.TimeUnit > m1.TimeUnit && m1_.IsAtLeastX_OfM( 161.8, m0 ) )
                {
                    if ( m2.BreakTrendLineBtwXYInLETimeToZ( m2_, m0, m1 ) )
                    {
                        m1.WavePattern |= WavePattern.Irregular | WavePattern.RunningCorrection;
                        m1.WavePosition = WavePosition.End;
                        m1.AddStructureLabel( StructureLabelEnum.L5 );
                    }

                    if ( ( m0.TimeUnit + 1 < m1_.TimeUnit ) && ( m0.TimeUnit + 1 < m1.TimeUnit ) )
                    {
                        m1.DropStructureLabel( StructureLabelEnum.s5 );
                    }

                    if ( m2_.IsLongerThan( m1_ ) )
                    {
                        m1.DropStructureLabel( StructureLabelEnum.c3 );
                    }



                    if ( m5 != null && m6 != null )
                    {
                        if ( m5.IsCompletelyRetraced_Slower_byM( m6 ) )
                        {
                            m1.DropStructureLabel( StructureLabelEnum.c3 );
                        }
                    }

                    if ( m1.StructureLabel.HasFlag( StructureLabelEnum.c3 ) )
                    {
                        m1.WavePattern |= WavePattern.AnyComplexCorrection;
                        m1.PotentialWave = ElliottWaveEnum.WaveX;
                        m1.AddStructureLabel( StructureLabelEnum.x_c3_MAYBE );
                    }
                }
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m3 is retraced less than 61.8%, it is extremely unlikely m1 is the beginning of any Elliott pattern; 
             *          place ":c3/:sL3/:s5" at the end of m1. 
             *          
             *  If the end of m1 is exceeded during the formation of m2, add an "x" in front of ":c3." 
             *  
             *  If m1 is the longest when compared to m(-1) and m(-3) and m2 breaks a trendline drawn across the low of m(-2) and m0 in a period of time equal to or less than that taken by m1, 
             *          m1 may be the 5th wave of a 5th Extension pattern; add [:L5] to the end of m1. 
             *          
             *  If m3 through m5 do not achieve a price distance of 161.8% of m1 (or more), drop ":sL3" from the list. 
             *  
             *  If m2 (plus one time unit) is not completely retraced in the same amount of time that it took to form (or less), drop ":sL3" from the list. 
             *  
             *  If m0 (plus one time unit) simultaneously takes less time than m(-1) and m1, drop ":s5" from list. 
             *  
             *  If m(-2) is longer (in price) than m(-1) and ":c3" is not preceded by an "x," drop ":c3" from the list. 
             *  
             *  If ":c3" is still a possibility, m1 may be the x-wave of a Complex Correction; add "x:c3?" as an additional Structure label to m1's list.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( m3.IsRetraced_LtX_byM( 61.8, m4 ) )
            {
                m1.AddStructureLabel( StructureLabelEnum.c3 | StructureLabelEnum.sL3 | StructureLabelEnum.s5 );

                if ( m1.IsLongerThan( m1_ ) && m1.IsLongerThan( m3_ ) )
                {
                    if ( m2.BreakTrendLineBtwXYInLETimeToZ( m2_, m0, m1 ) )
                    {
                        m1.WavePattern |= WavePattern.Irregular | WavePattern.RunningCorrection;
                        m1.WavePosition = WavePosition.End;
                        m1.AddStructureLabel( StructureLabelEnum.L5_RARE );
                    }
                }

                var m3Tom5 = m3.Combine( m4, m5 );

                if ( m3Tom5.IsLessThanX_OfM( 161.8, m1 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.sL3 );
                }

                if ( !m2.IsCompletelyRetraced_Faster_byM( m3 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.sL3 );
                }

                if ( ( m0.TimeUnit + 1 < m1_.TimeUnit ) && ( m0.TimeUnit + 1 < m1.TimeUnit ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.s5 );
                }

                if ( m2_.IsLongerThan( m1_ ) && m1.StructureLabel.HasFlag( StructureLabelEnum.c3 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.c3 );
                }

                if ( m1.StructureLabel.HasFlag( StructureLabelEnum.c3 ) )
                {
                    m1.WavePattern |= WavePattern.AnyComplexCorrection;
                    m1.PotentialWave = ElliottWaveEnum.WaveX;
                    m1.AddStructureLabel( StructureLabelEnum.x_c3_MAYBE );
                }
            }
        }

        private void Rule4ConditionC( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            if ( m1 == null || m2 == null || m3 == null )
                return;

            double m3m2Ration = m3.Over( m2 );

            switch ( m3m2Ration )
            {
                case double p when ( p >= 100 && p < 161.8 ):
                {
                    Rule4ConditionCCategory1( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;

                case double p when ( p >= 161.8 && p <= 261.8 ):
                {
                    Rule4ConditionCCategory2( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;

                case double p when ( p > 261.8 ):
                {
                    Rule4ConditionCCategory3( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;
            }
        }

        private void Rule4ConditionCCategory1( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            if ( m1 == null || m2 == null || m3 == null )
                return;

            var m4 = m3.GetNext( );

            if ( m4 == null )
                return;

            m1.AddStructureLabel( StructureLabelEnum.F3 | StructureLabelEnum.c3 );
        }
        private void Rule4ConditionCCategory2( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            if ( m1 == null || m2 == null || m3 == null )
                return;

            var m4 = m3.GetNext( );

            if ( m4 == null )
                return;

            if ( m2.Plus1TU_IsCompletelyRetraced_Faster_byM( m3 ) && m3.IsMoreThanX_ofM( 161.8, m1 ) )
            {
                m1.MainWaveType = WaveType.Correction;
                m1.WavePattern |= WavePattern.CFailureFlat | WavePattern.ContractingTriangle;
                m1.WavePosition = WavePosition.Center;
                m1.AddStructureLabel( StructureLabelEnum.c3 | StructureLabelEnum.F3_LESSLIKELY );

                if ( m1.PreferableStructureIs( StructureLabelEnum.F3_LESSLIKELY ) )
                {
                    m1.WavePattern |= WavePattern.ElongatedFlat;
                }
            }
            else
            {
                m1.AddStructureLabel( StructureLabelEnum.F3 | StructureLabelEnum.c3 | StructureLabelEnum.x_c3 );
            }

        }

        private void Rule4ConditionCCategory3( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            if ( m1 == null || m2 == null || m3 == null )
                return;

            var m4 = m3.GetNext( );

            if ( m2.Plus1TU_IsCompletelyRetraced_Faster_byM( m3 ) && m3.IsMoreThanX_ofM( 161.8, m1 ) )
            {
                m1.MainWaveType = WaveType.Correction;
                m1.WavePattern |= WavePattern.CFailureFlat | WavePattern.ContractingTriangle;
                m1.WavePosition = WavePosition.Center;
                m1.AddStructureLabel( StructureLabelEnum.c3 | StructureLabelEnum.F3_RARE );

                if ( m4 == null )
                    return;

                if ( m3.IsRetraced_AtLeastX_byM( 61.8, m4 ) )
                {
                    m1.DropStructureLabel( StructureLabelEnum.c3 );
                }
            }
        }

        private void Rule4ConditionD( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            if ( m1 == null || m2 == null || m3 == null )
                return;

            double m3m2Ration = m3.Over( m2 );

            switch ( m3m2Ration )
            {
                case double p when ( p >= 100 && p < 261.8 ):
                {
                    Rule4ConditionDCategory12( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;


                case double p when ( p > 261.8 ):
                {
                    Rule4ConditionDCategory3( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;
            }
        }

        private void Rule4ConditionDCategory12( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            if ( m1 == null || m2 == null || m3 == null )
                return;

            var m4 = m3.GetNext( );

            if ( m4 == null )
                return;

            var m5 = m4.GetNext( );

            if ( m5 == null )
                return;

            var m3Tom5 = m3.Combine( m4, m5 );

            if ( m2.Plus1TU_IsCompletelyRetraced_Faster_byM( m3 ) && m3.IsRetraced_NoMoreThanX_byM( 61.8, m4 ) )
            {
                if ( m3.IsMoreThanX_ofM( 161.8, m1 ) || m3Tom5.IsMoreThanX_ofM( 161.8, m1 ) )
                {
                    m1.MainWaveType = WaveType.ComplexCorrection;
                    m1.AddStructureLabel( StructureLabelEnum.F3 | StructureLabelEnum.c3_RARE );

                    m0.MissingXWavePos = WavePosition.Center;
                    m0.MissingXWaves = StructureLabelEnum.x_c3_MAYBE | StructureLabelEnum.c3_MAYBE;
                    m0.AddStructureLabel( StructureLabelEnum.F3_MAYBE );
                }
            }

            if ( m2.IsCompletelyRetraced_Slower_byM( m3 ) )
            {
                /*
                 * If m2 is completely retraced slower than it took to form, a Flat or Triangle is probably involved; 
                 * place ":F3/:c3" at the end of m1. If the end of m1 is exceeded during the formation of m2, add an "x" in front of ":c3."                                                   
                 */

                m1.WavePattern |= WavePattern.Flat | WavePattern.ContractingTriangle;
                m1.AddStructureLabel( StructureLabelEnum.F3 | StructureLabelEnum.c3 );
            }

            if ( m3.Plus1TU_IsCompletelyRetraced_Faster_byM( m4 ) )
            {
                m1.AddStructureLabel(StructureLabelEnum.F3 );
            }

            if ( m3.IsRetraced_AtLeastX_NoMoreThanY_byM( 61.8, 100, m4 ) )
            {
                m1.AddStructureLabel(StructureLabelEnum.F3 );
            }

            if ( m3.IsRetraced_LtX_byM( 61.8, m4 ) )
            {
                m1.AddStructureLabel( StructureLabelEnum.F3 );

                var m6 = m5.GetNext( );

                if ( m6 != null )
                {
                    if ( m1.IsLongerThan( m5 ) || m3.IsLongerThan( m5 ) && m5.Plus1TU_IsCompletelyRetraced_Faster_byM( m6 ) )
                    {
                        m1.WavePattern |= WavePattern.TerminalImpulse;
                        m1.AddStructureLabel( StructureLabelEnum.F3 );
                    }

                    if ( m5.IsLongerThan( m1 ) && m5.IsLongerThan( m3 ) )
                    {
                        m1.WavePattern |= WavePattern.DoubleFlat;
                        m4.PotentialWave = ElliottWaveEnum.WaveX;
                        m1.AddStructureLabel( StructureLabelEnum.F3 );
                    }
                }

            }
        }

        private void Rule4ConditionDCategory3( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            if ( m1 == null || m2 == null || m3 == null )
                return;

            var m4 = m3.GetNext( );

            if ( m3.TimeUnit <= m1.TimeUnit && m2.Plus1TU_IsCompletelyRetraced_Faster_byM( m3 ) )
            {
                m0.MissingXWavePos = WavePosition.Center;
                m1.AddStructureLabel( StructureLabelEnum.c3 );
            }

            if ( m3.IsRetraced_AtLeastX_byM( 61.8, m4 ) )
            {
                m1.WavePattern |= WavePattern.Flat;
                m1.AddStructureLabel( StructureLabelEnum.F3 );
            }

            if ( m3.TimeUnit > m1.TimeUnit )
            {
                m1.AddStructureLabel( StructureLabelEnum.c3 | StructureLabelEnum.F3 );
            }
        }

        private void Rule4ConditionE( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            if ( m1 == null || m2 == null || m3 == null )
                return;

            double m3m2Ration = m3.Over( m2 );

            switch ( m3m2Ration )
            {
                case double p when ( p >= 100 && p < 261.8 ):
                {
                    Rule4ConditionECategory12( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;


                case double p when ( p > 261.8 ):
                {
                    Rule4ConditionECategory3( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;
            }
        }

        private void Rule4ConditionECategory12( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            if ( m1 == null || m2 == null || m3 == null )
                return;

            var m4 = m3.GetNext( );

            if ( m4 == null )
                return;

            var m5 = m4.GetNext( );

            if ( m5 == null )
                return;

            if ( m3.IsCompletelyRetraced_Faster_byM( m4 ) )
            {
                m1.AddStructureLabel(StructureLabelEnum.F3 );
            }

            if ( m3.IsLessThanX_OfM( 161.8, m2 ) && !m3.IsCompletelyRetraced_byM( m4 ) && m4.IsCompletelyRetraced_Faster_byM( m5 ) )
            {
                m1.MainWaveType = WaveType.ComplexCorrection;
                m1.PotentialWave = ElliottWaveEnum.WaveX;
                m1.AddStructureLabel( StructureLabelEnum.x_c3 );

                if ( m1_.IsMoreThanX_ofM( 61.8, m0 ) )
                {
                    m0.MissingXWavePos = WavePosition.Center;
                }
            }

            var m3tom5 = m3.Combine( m4, m5 );

            if ( m2.Plus1TU_IsCompletelyRetraced_Faster_byM( m3 ) && !m1_.IsMoreThanX_ofM( 61.8, m0 ) && m3.IsRetraced_NoMoreThanX_byM( 61.8, m4 ) )
            {
                if ( m3.AchieveSameDistanceInLessTime( m1 ) || m3tom5.AchieveSameDistanceInLessTime( m1 ) )
                {
                    m1.MainWaveType = WaveType.ComplexCorrection;
                    m0.MissingXWavePos = WavePosition.Center;
                    m0.MissingXWaves = StructureLabelEnum.x_c3_MAYBE | StructureLabelEnum.s5;
                    m1.PotentialWave = ElliottWaveEnum.WaveX;

                    m1.AddStructureLabel( StructureLabelEnum.F3 | StructureLabelEnum.c3_RARE );
                }
            }

            if ( m2.IsCompletelyRetraced_Slower_byM( m3 ) && !m1_.IsMoreThanX_ofM( 61.8, m0 ) && m3.IsRetraced_NoMoreThanX_byM( 61.8, m4 ) )
            {
                if ( m3tom5.IsMoreThanX_LessEqTime_ofM( 161.8, m1 ) )
                {
                    m1.MainWaveType = WaveType.ComplexCorrection;
                    m1.AddStructureLabel( StructureLabelEnum.F3 | StructureLabelEnum.c3_RARE );
                    m0.MissingXWavePos = WavePosition.Center;
                    m0.MissingXWaves = StructureLabelEnum.x_c3_MAYBE | StructureLabelEnum.s5;
                }
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m2 is retraced slower than it took to form, a Flat or Triangle is probably involved; place an ":F3" at the end of m1.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( m2.IsCompletelyRetraced_Faster_byM( m3 ) )
            {
                m1.MainWaveType = WaveType.ComplexCorrection;
                m1.WavePattern |= WavePattern.ContractingTriangle | WavePattern.Flat;
                m1.AddStructureLabel( StructureLabelEnum.F3 );
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m0 is a polywave (or a monowave with a suspected "missing" wave toward its center), m1 may be an x-wave of a Complex Correction; 
             *  add "x:c3" to any existing Structure labels at the end of m1
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( m0.MonoWavesCount > 3 )
            {
                m1.MainWaveType = WaveType.ComplexCorrection;
                m1.PotentialWave = ElliottWaveEnum.WaveX;
                m1.AddStructureLabel( StructureLabelEnum.x_c3_MAYBE );
            }

            if ( m1_.IsNoMoreThanX_OfM( 61.8, m0 ) )
            {
                m1.MainWaveType = WaveType.ComplexCorrection;
                m1.PotentialWave = ElliottWaveEnum.WaveX;
                m1.AddStructureLabel( StructureLabelEnum.x_c3 );
            }
        }

        private void Rule4ConditionECategory3( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            if ( m1 == null || m2 == null || m3 == null )
                return;

            var m4 = m3.GetNext( );
            if ( m4 == null )
                return;

            if ( m3.TimeUnit <= m1.TimeUnit && m2.Plus1TU_IsCompletelyRetraced_Faster_byM( m4 ) )
            {
                m0.MissingXWavePos = WavePosition.Center;
                m1.AddStructureLabel( StructureLabelEnum.x_c3 );
            }

            var m5 = m4.GetNext( );
            if ( m5 == null )
                return;

            var m3tom5 = m3.Combine( m4, m5 );

            if ( !m3tom5.Exceed( m0.Begin ) && m3.IsRetraced_AtLeastX_byM( 61.8, m4 ) )
            {
                m1.MainWaveType = WaveType.Correction;
                m1.WavePattern |= WavePattern.ElongatedFlat;

                m1.AddStructureLabel( StructureLabelEnum.F3 );
            }
        }
    }
}
