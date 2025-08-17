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
        // ![](5C10D7EB5C4FDD119B41E5FA68B1019A.png;;;0.02284,0.02197)
        private void Rule2( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            if ( m0 == null )
                return;

            double m0m1Ratio = m0.Over( m1 );

            switch ( m0m1Ratio )
            {
                case double p when ( p < 38.2 ):
                {
                    Rule2ConditionA( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;

                case double p when ( p >= 38.2 && p < 61.8 ):
                {
                    Rule2ConditionB( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;


                case double p when ( p >= 61.8 && p < 100 ):
                {
                    Rule2ConditionC( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;

                case double p when ( p >= 100 && p < 161.8 ):
                {
                    Rule2ConditionD( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;

                case double p when ( p >= 161.8 ):
                {
                    Rule2ConditionE( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;
            }
        }


        private void Rule2ConditionA( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            //![](11765DCAFF5E9D89F70C33C01ED431E8.png;;;0.02528,0.02513) ![](F8B1D82B57CB701D0932F99EE6161938.png;;;0.02160,0.02183)

            if ( m1 == null || m2 == null || m3 == null )
                return;

            m1.AddStructureLabel(StructureLabelEnum._5 );

            var m4 = m3.GetNext( );

            if ( m4 == null )
                return;

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m4 does not exceed the end of m0, 
             *  m1 may be completing a Corrective pattern within a Complex formation where 
             *          m2 is an x-wave; 
             *          place ":s5" at the end of m1 and 
             *          "x:c3?" at the end of m2. 
             *          
             *  When comparing m(-1), m1 and m3, if m1 is not the shortest of the three and 
             *  the longest of the three is close to (or greater than) 161.8% of the next longest and 
             *  m3 is retraced at least 61.8%, 
             *          the market may be forming an Impulse pattern with m1 the center phase (wave-3). 
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( !m4.Exceed( m0.End ) )
            {
                // ![](F4765808DA6292198239B5BBD5665E88.png;;;0.02168,0.02303)
                m1.MainWaveType  = WaveType.Correction;
                m1.WavePattern  |= WavePattern.Flat;
                m1.WavePosition  = WavePosition.End;

                m1.WavePattern  |= WavePattern.AnyCorrectivePattern;
                m2.PotentialWave = ElliottWaveEnum.WaveX;

                m1.AddStructureLabel( StructureLabelEnum.s5 );
                m2.AddStructureLabel(StructureLabelEnum.x_c3_MAYBE );
            }

            if ( m1.IsNotTheShortest( m1_, m3 ) )
            {
                double longest           = Math.Max( m1.Pips, Math.Max( m1_.Pips, m3.Pips ) );
                double second            = 0d;
                double first2SecondRatio = 0;

                var a = m1_.Pips;
                var b = m1.Pips;
                var c = m3.Pips;

                if ( a >= b && a >= c )
                {
                    second = ( b >= c ) ? b : c;
                }
                else if ( b >= a && b >= c )
                {
                    second = ( a >= c ) ? a : c;
                }
                // c is the largest number of the three
                else if ( a >= b )
                {
                    // a is the second largest
                    second = a;
                }
                else
                {
                    // B is the second largest
                    second = b;
                }

                first2SecondRatio = longest / second * 100;

                if ( first2SecondRatio > 160 )
                {
                    if ( m3.IsRetraced_AtLeastX_byM( 61.8, m4 ) )
                    {
                        // ![](632AB115BDD10FB1F06BA49D5F38D22D.png;;;0.03324,0.03017)
                        m1.WavePattern |= WavePattern.TrendingImpulse;
                        m1.PotentialWave = ElliottWaveEnum.Wave3;
                    }
                }
            }

            if ( m0.MonoWavesCount > 3 && m0.IsCompletelyRetraced_Faster_byM( m1 ) )
            {
                m0.MainWaveType = WaveType.ImptWavePattern;
                m0.WavePosition = WavePosition.End;
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m0 and m2 are related by 61.8% in price and equal or related (by 61.8%) in time and 
             *  m(-1) is 161.8% or more of m1 and 
             *  m3 (and any additional monowaves required) achieves a price length longer than m(-1) within a time frame equal to or less than m(-1), 
             *  
             *              a Running Correction (any variation) is probably unfolding; 
             *              take note of that fact and add "[:c3]" after the ":5" already at the end of m1. 
             * 
             * 
             * The Running correction most likely started at the beginning of m0 and 
             * concluded at the end of m2. 
             * 
             * When you begin grouping your Structure labels in Chapter 4, work on the Running Correction as the first possibility with the ":c3" either wave-b of a Running Correction or 
             * wave-x of a Double Three Running Correction. 
             * 
             * Read on to be aware of additional circumstances possible at this juncture.
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( m0.IsApproxX_PriceTime_OfM( 61.8, m2 ) && m1_.IsMoreThanX_ofM( 161.8, m1 ) )
            {
                var m5 = m4.GetNext( );
                var m3_m5 = m3.Combine( m4, m5 );

                if ( ( m3.Pips > m1_.Pips && m3.TimeUnit < m1_.TimeUnit ) || ( m3_m5.Pips > m1_.Pips && m3_m5.TimeUnit < m1_.TimeUnit ) )
                {
                    m0.MainWaveType = WaveType.RunningCorrection;
                    m0.WavePattern |= WavePattern.RunningCorrection;

                    m1.MainWaveType = WaveType.RunningCorrection;
                    m1.WavePattern |= WavePattern.RunningCorrection;

                    m2.MainWaveType = WaveType.RunningCorrection;
                    m2.WavePattern |= WavePattern.RunningCorrection;


                    m1.AddStructureLabel(StructureLabelEnum._5 | StructureLabelEnum.c3 );
                    m1.PotentialWave = ElliottWaveEnum.WaveB;
                }
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m0 and m2 are approximately equal in time and 
             *  m3 is less than 161.8% of m1 and 
             *  m(-1) is longer than m0, 
             *          
             *          m1 might be part of a Complex Correction which will necessitate the use of an "x-wave" Progress label. The x-wave will be in one of three places; if m(-2) is shorter than m(-1), the x-wave might be at the end of m0; if m4 is no more than 161.8% of m3, the x-wave may be at the end of m2; or, if m0 is no more than 50% of m1 and m1 is the longest when compared to m(-1) and m3, the x-wave may be hidden from sight (i.e., invisible or missing) in the center of m1. To warn of the three possibilities, take a pencil and place an "x:c3?" at the end of m0, the end of m2 and, if appropriate, the center of m1 (using a circle to mark the center of m1). The "missing" x-wave in the center of m1 is the least likely of the three choices under these circumstances. [The concept of "missing waves" is discussed in Chapter 12, page 12-34.] NOTE: The x-wave can occur in only one of the three places mentioned. If the x-wave concept is used at one point, erase the other two possibilities. These warnings will come in helpful as you group monowaves together in Chapter 4 and as you finalize your interpretations throughout the analysis process. The x-wave possibility makes use of the ":5" Structure label which should already be in place at the end of m1.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( 
                    m0.TimeApproxEqual( m2 ) && 
                    m3.IsLessThanX_OfM( 161.8, m1 ) && 
                    m1_.IsLongerThan( m0 ) 
               )
            {
                m1.MainWaveType = WaveType.ComplexCorrection;
                m1.WavePattern |= WavePattern.AnyCorrectivePattern;

                if ( m2_.IsShorterThan( m1_ ) )
                {
                    m0.MissingXWavePos = WavePosition.End;
                    m0.MissingXWaves |= StructureLabelEnum.x_c3_MAYBE;
                }

                if ( m4.IsLessThanX_OfM( 161.8, m3 ) )
                {
                    m2.MissingXWavePos = WavePosition.End;
                    m2.MissingXWaves |= StructureLabelEnum.x_c3_MAYBE;
                }

                if ( m0.IsLessThanX_OfM( 50, m1 ) && m1.IsLongerThan( m1_ ) && m1.IsLongerThan( m3 ) )
                {
                    m1.MissingXWavePos = WavePosition.Center;
                    m1.MissingXWaves |= StructureLabelEnum.x_c3_MAYBE;
                }
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m(-1) is longer than m0 and 
             *  m0 is shorter than m1 and 
             *  m1, when compared to m(-1) and m3, is not the shortest of the three and 
             *  m3 (plus one time unit) is completely retraced in the same amount of time (or less) that it took to form, 
             *  
             *          m1 may be wave-3 of a Terminal Impulse pattern; place ":c3" at the end of m1.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( 
                    m1_.IsLongerThan( m0 ) && 
                    m0.IsShorterThan( m1 ) && 
                    m1.IsNotTheShortest( m1_, m3 ) && 
                    m3.Plus1TU_IsCompletelyRetraced_Faster_byM( m4 ) 
               )
            {
                m1.WavePattern |= WavePattern.TerminalImpulse;
                m1.PotentialWave = ElliottWaveEnum.Wave3;
            }

        }

        private void Rule2ConditionB( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            if ( m1 == null || m2 == null || m3 == null )
                return;

            m1.AddStructureLabel(StructureLabelEnum._5 );

            var m4 = m3.GetNext( );

            if ( m4 == null )
                return;

            if ( !m4.Exceed( m0.End ) )
            {
                m1.WavePattern |= WavePattern.AnyCorrectivePattern;
                m2.PotentialWave = ElliottWaveEnum.WaveX;
                m1.AddStructureLabel( StructureLabelEnum.s5 );
                m2.AddStructureLabel(StructureLabelEnum.x_c3_MAYBE );
            }

            if ( m0.MonoWavesCount > 3 && m0.IsCompletelyRetraced_Faster_byM( m1 ) )
            {
                m0.MainWaveType = WaveType.ImptWavePattern;
                m0.WavePosition = WavePosition.End;
            }

            if ( m0.IsApproxX_PriceTime_OfM( 61.8, m2 ) && m1_.IsMoreThanX_ofM( 161.8, m1 ) )
            {
                var m5 = m4.GetNext( );
                var m3_m5 = m3.Combine( m4, m5 );

                if ( ( m3.Pips > m1_.Pips && m3.TimeUnit < m1_.TimeUnit ) || ( m3_m5.Pips > m1_.Pips && m3_m5.TimeUnit < m1_.TimeUnit ) )
                {
                    m0.MainWaveType = WaveType.RunningCorrection;
                    m0.WavePattern |= WavePattern.RunningCorrection;

                    m1.MainWaveType = WaveType.RunningCorrection;
                    m1.WavePattern |= WavePattern.RunningCorrection;

                    m2.MainWaveType = WaveType.RunningCorrection;
                    m2.WavePattern |= WavePattern.RunningCorrection;

                    m1.AddStructureLabel(StructureLabelEnum._5 | StructureLabelEnum.c3 );
                    m1.PotentialWave = ElliottWaveEnum.WaveB;
                }
            }

            if ( m0.TimeApproxEqual( m2 ) && m3.IsLessThanX_OfM( 161.8, m1 ) && m3.IsCompletelyRetraced_Faster_byM( m4 ) )
            {
                m1.MainWaveType = WaveType.ComplexCorrection;
                m1.WavePattern |= WavePattern.AnyCorrectivePattern;

                if ( m3.IsLessThanX_OfM( 61.8, m1 ) )
                {
                    m1.MissingXWavePos = WavePosition.Center;
                    m1.MissingXWaves |= StructureLabelEnum.x_c3_MAYBE;
                }
                else
                {
                    m0.MissingXWavePos = WavePosition.End;
                    m0.MissingXWaves |= StructureLabelEnum.x_c3_MAYBE;
                    m1.MissingXWavePos = WavePosition.Center;
                    m1.MissingXWaves |= StructureLabelEnum.x_c3_MAYBE;
                    m2.MissingXWavePos = WavePosition.End;
                    m2.MissingXWaves |= StructureLabelEnum.x_c3_MAYBE;
                }
            }

            if ( m1_.IsLongerThan( m0 ) && m0.IsShorterThan( m1 ) && ( m1.IsLongerThan( m1_ ) || m1.IsLongerThan( m3 ) ) && m3.Plus1TU_IsCompletelyRetraced_Faster_byM( m4 ) )
            {
                m1.WavePattern |= WavePattern.TerminalImpulse;
                m1.PotentialWave = ElliottWaveEnum.Wave3;
            }
        }

        private void Rule2ConditionC( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            if ( m1 == null || m2 == null || m3 == null )
                return;


            // ![](B7E8A9DAF2673BAAEC6AF04EE7CE5D31.png;;;0.04684,0.04109)
            m1.AddStructureLabel(StructureLabelEnum._5 );

            var m4 = m3.GetNext( );

            if ( m4 == null )
                return;

            // ![](632F66D474F4ECA043C54311671E7A38.png;;;0.03972,0.03364)
            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  Under all circumstances, place ":5" at the end of m1. 
             *  
             *  If m4 does not exceed the end of m0, 
             *          1) m1 may complete a Flat pattern within a Complex formation where 
             *                  m2 is an x-wave; place ":s5" at the end of m1 and 
             *                  "x:c3?" at the end of m2. 
             *  Other possibilities could be unfolding, read the rest of this section to make sure no additional possibilities are missed. 
             * 
             * If the ":5" is used as the preferred Structure label, 
             *          1) it may be the end of a Zigzag within a Running or irregular Failure Flat correction OR 
             *          2) part of a Complex correction where wave-x is at the end of m0 or m2 (add "x:c3?" to the end of m0). 
             *                  If considering the Complex scenario, for the 
             *                          a) x-wave to work in the m0 position, 
             *                                  m(-2) must be shorter than m(-1) and 
             *                                  it is extremely likely m(-4) would be larger than m(-3). 
             *                                  
             *                          b) For the x-wave to work in the m2 position, 
             *                                  it is extremely likely m(-2) would be longer than m(-1). 
             *                                  Furthermore, for the x-wave to work in the m2 position, m1 must be at least 38.2% of m(-1) with it preferable m1 be 61.8% or more of m(-1).
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */
            if ( !m4.Exceed( m0.End ) )
            {                
                m1.MainWaveType    = WaveType.Correction;
                m1.WavePattern    |= WavePattern.Flat;
                m1.WavePosition    = WavePosition.End;
                m1.AddStructureLabel( StructureLabelEnum.s5 );

                m2.PotentialWave   = ElliottWaveEnum.WaveX;                
                m2.MissingXWaves   = StructureLabelEnum.x_c3_MAYBE;
                m2.AddStructureLabel( StructureLabelEnum.x_c3_MAYBE );
            }

            var m4_ = m3_.GetPrevious( );

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If considering the Complex scenario, 
             *          for the x-wave to work in the m0 position, 
             *          m(-2) must be shorter than m(-1) and 
             *          it is extremely likely m(-4) would be larger than m(-3). 
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( m1.PreferableStructureIs( StructureLabelEnum._5 ) )
            {
                if ( 
                        m2_.IsShorterThan( m1_ ) &&
                        m4_ != null && 
                        m4_.IsLongerThan( m3_ )
                    )
                {
                    m1.MainWaveType = WaveType.ComplexCorrection;
                    m0.PotentialWave = ElliottWaveEnum.WaveX;                    
                }

                if ( 
                        m2_.IsLongerThan( m1_ ) &&
                        m1.IsMoreThanX_ofM( 38.2, m1_ )
                   )
                {
                    m1.MainWaveType = WaveType.ComplexCorrection;
                    m2.PotentialWave = ElliottWaveEnum.WaveX;                                        
                }
            }

            

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m(-1) is larger than m0, but less than 261.8% of m1 and m3 is shorter than m1 and after m3, 
             *  the market quickly returns to the beginning of m1 (or beyond), a Terminal may have completed with m3; place ":c3" at the end of m1.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( m1_.IsLongerThan( m0 ) && m1_.IsLessThanX_OfM( 261.8, m1 ) && m3.IsShorterThan( m1 ) )
            {
                if ( m3.IsCompletelyRetraced_Faster_byM( m4 ) )
                {
                    m3.WavePattern |= WavePattern.TerminalImpulse;

                    m1.AddStructureLabel( StructureLabelEnum.c3 );
                }
            }

            if ( m0.MonoWavesCount > 3 && m0.IsCompletelyRetraced_Faster_byM( m1 ) )
            {
                m0.MainWaveType = WaveType.ImptWavePattern;
                m0.WavePosition = WavePosition.End;
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m2 (plus one time unit) is completely retraced in the same amount of time (or less) that it took to form and 
             *  m3 is longer and more vertical than m1 and 
             *  m(-1) is not more than 161.8% of m1, 
             *  
             *              a Running Triangle may have terminated with m2; place ":sL3" at the end of m1
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */
            if ( m2.Plus1TU_IsCompletelyRetraced_Faster_byM( m3 ) && m3.IsLongerAndMoreVerticalThan( m1 ) && m1_.IsLessThanX_OfM( 161.8, m1 ) )
            {
                m2.WavePattern |= WavePattern.RunningTriangle;
                m1.AddStructureLabel( StructureLabelEnum.sL3 );
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m3 and m(-1) are both at least 161.8% of m1, 
             *  
             *              an Irregular Failure may have completed with m2; place ":c3" at the end of m1.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */
            if ( m3.IsMoreThanX_ofM( 161.8, m1 ) || m1_.IsMoreThanX_ofM( 161.8, m1 ) )
            {
                m2.WavePattern |= WavePattern.IrregularFailureFlat;
            }
        }

        private void Rule2ConditionD( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            if ( m1 == null || m2 == null || m3 == null )
                return;

            if ( m2.TimeUnit >= m1.TimeUnit || m2.TimeUnit >= m3.TimeUnit )
            {
                m1.AddStructureLabel(StructureLabelEnum._5 );
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m2 (plus one time unit) is completely retraced in the same amount of time (or less) that it took to form and 
             *  m3 is longer and more vertical than m1 and 
             *  m0 and m1 consume a similar amount of time (within a 61.8% tolerance) and 
             *  m2 is at least 61.8% of the time of m0 or m1 and 
             *  m0 is no more than 138.2% of m1, 
             *  
             *          then a severe "C-Failure" Flat may have concluded with m2; place ":c3" at the end of m1.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( m2.Plus1TU_IsCompletelyRetraced_Faster_byM( m3 ) && m3.IsLongerAndMoreVerticalThan( m1 ) && m0.IsWithinX_Time_OfM( 61.8, m1 ) )
            {
                if ( m2.IsAtLeastX_Time_OfX( 61.8, m0 ) || m2.IsAtLeastX_Time_OfX( 61.8, m1 ) && m0.IsLessThanX_OfM( 138.2, m1 ) )
                {
                    m2.WavePattern |= WavePattern.CFailureFlat;
                    m1.AddStructureLabel( StructureLabelEnum.c3 );
                }
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             * If m3 is longer and more vertical than m1 and 
             * either m3 is completely retraced OR m3 is retraced no more than 61.8% and 
             * one of m0's Structure label choices is ":c3" 
             * and m(-3) is longer than m(-2) and 
             * m(-2) or m(-1) is longer than m0, 
             * 
             *          m1 may be the second-to-last leg of a Contracting Triangle; add "(:sL3)" to the Structure list. 
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            var m4 = m3.GetNext( );

            if ( m4 == null )
                return;

            if ( m3.IsLongerAndMoreVerticalThan( m1 ) && m3.IsCompletelyRetraced_byM( m4 ) || m3.IsRetraced_LtX_byM( 61.8, m4 ) )
            {
                if ( m3.StructureLabel.HasFlag( StructureLabelEnum.c3 ) && m3_.IsLongerThan( m2_ ) )
                {
                    if ( m2_.IsLongerThan( m0 ) || m1_.IsLongerThan( m0 ) )
                    {
                        m1.WavePattern |= WavePattern.ContractingTriangle;
                        m1.AddStructureLabel( StructureLabelEnum.sL3 );
                    }
                }
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m3 is shorter than m1 and 
             *  m3 is retraced at least 61.8% and 
             *  m1 takes less time than m0 and m2 takes just as much time (or more) as m1, 
             *  
             *          then m1 is probably part of a Zigzag which concludes with m3; put a ":5" at the end of m1.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( m3.IsShorterThan( m1 ) && m3.IsRetraced_AtLeastX_byM( 61.8, m4 ) )
            {
                if ( m1.TimeUnit < m0.TimeUnit && m2.TimeUnit >= m1.TimeUnit )
                {
                    m1.WavePattern |= WavePattern.ZigZag;
                    m2.WavePattern |= WavePattern.ZigZag;
                    m3.WavePattern |= WavePattern.ZigZag;

                    m1.AddStructureLabel(StructureLabelEnum._5 );
                }
            }

        }

        private void Rule2ConditionE( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            if ( m1 == null || m2 == null || m3 == null )
                return;

            m1.AddStructureLabel(StructureLabelEnum._5 );

            if ( m3.IsShorterThan( m1 ) && m3.IsLessVerticalThan( m1 ) )
            {
                m1.AddStructureLabel(StructureLabelEnum._5 );
            }

            if ( m2.Plus1TU_IsCompletelyRetraced_Faster_byM( m3 ) && m3.IsLongerAndMoreVerticalThan( m1 ) && !m1_.Overlap( m1 ) )
            {
                m2.MainWaveType = WaveType.ComplexCorrection;
                m2.WavePattern |= WavePattern.AnyCorrectivePattern;

                m1.AddStructureLabel( StructureLabelEnum.c3 );

                m0.MissingXWavePos = WavePosition.Center;
                m0.MissingXWaves |= StructureLabelEnum._5 | StructureLabelEnum.x_c3_MAYBE;
            }
        }
    }
}
