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
        // ![](222205ECFC92E819133AB496E04B784D.png;;;0.01696,0.01566)
        private void Rule5( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            if ( m0 == null )
                return;

            double m0m1Ratio = m0.Over( m1 );

            switch ( m0m1Ratio )
            {
                case double p when ( p < 100 ):
                {
                    Rule5ConditionA( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;

                case double p when ( p >= 100 && p < 161.8 ):
                {
                    Rule5ConditionB( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;

                case double p when ( p >= 161.8 && p <= 261.8 ):
                {
                    Rule5ConditionC( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;

                case double p when ( p > 261.8 ):
                {
                    Rule5ConditionD( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;
            }
        }

        private void Rule5ConditionA( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            // ![](4A9932D2977A9EB4757C4F78FDE6CDBA.png;;;0.02731,0.02765)![](9E3F7D4AB45E39545F4663D26AFC9787.png;;;0.01781,0.01075)
            if ( m1 == null || m2 == null || m3 == null )
                return;

            if ( m2.MonoWavesCount > 3 )
            {
                var first3Pips = m2.First3MonowavesPips( );

                var m2First3Overm1 = m1.Retracement( first3Pips );

                /* -------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *  If the first three monowaves of m2 do not retrace more than 61.8% of m1, 
                 *          a Complex Correction may be unfolding with the 
                 *                  1) first or second monowave (immediately after the end of m1) moving in the opposite direction of m1 representing an x-wave OR 
                 *                  2) m1 contains a "missing" x-wave or 
                 *                  3) "missing" b-wave in its center OR 
                 *                  4) m1 is the 3rd-wave of an 5th-Failure Impulse pattern (Trending or Terminal); place ":5/:s5" at the end of m1. 
                 * 
                 * Add ":F3" to the end of m1. If m1 is retraced at least 25% by the first 3 monowaves. 
                 * 
                 * NOTE: 
                 * if the "missing" x-wave is used, circle the center of m1 and place ":5?" to the left of the circle and "x:c3?" to the right of the circle.                  
                 * If the "missing" b-wave is used, circle the center of m1 and place ":5?" to the left of the circle and "b:F3?" to the right of the circle. 
                 * 
                 * If the b-wave is used, the termination of the Complex Correction will be confirmed immediately before the point where the market violently turns (in the opposite direction of m1) and 
                 * exceeds the 61.8% retracement level of m1.
                 * ------------------------------------------------------------------------------------------------------------------------------------------- */


                if ( m2First3Overm1 <= 61.8 )
                {
                    m1.MainWaveType    = WaveType.ComplexCorrection;
                    m1.WavePattern     |= WavePattern.MissingXWaves | WavePattern.FailureImpulse5th | WavePattern.MissingBWaves;
                    m1.PotentialWave   = ElliottWaveEnum.WaveX;
                    m1.MissingXWavePos = WavePosition.Center;
                    m1.MissingXWaves   = StructureLabelEnum.s5_MAYBE | StructureLabelEnum.x_c3_MAYBE | StructureLabelEnum.b_F3_MAYBE;

                    m1.AddStructureLabel( StructureLabelEnum._5 | StructureLabelEnum.s5 );

                    if ( m2First3Overm1 >= 25 )
                    {
                        m1.AddStructureLabel( StructureLabelEnum.F3 );
                        // 
                        // ![](C4BE057DE6C5F1B9736E8FB900405064.png;;;0.03435,0.03348)


                    }
                }





                /* -------------------------------------------------------------------------------------------------------------------------------------------
                 * // ![](50142B1296C3F75B315D97E945AFE5FE.png;;;0.03574,0.03131) 
                 *  If the first three monowaves of m2 retrace more than 61.8% of m1, 
                 *  m1 may have completed wave-a of a Flat with a complex b-wave OR m1 may have completed wave-3 of a 5th Failure Impulse pattern; 
                 *          place ":F3/:5" at the end of m1 to show these two possibilities respectively.
                 * 
                 * ------------------------------------------------------------------------------------------------------------------------------------------- */

                if ( m2First3Overm1 > 61.8 )
                {                    
                    m1.MainWaveType = WaveType.ComplexCorrection;
                    m1.WavePattern  |= WavePattern.Flat | WavePattern.FailureImpulse5th;

                    m1.AddStructureLabel( StructureLabelEnum.F3 | StructureLabelEnum._5 );
                }
            }
            else
            {
                var m4 = m3.GetNext( );

                if ( m4 == null )
                    return;

                /* -------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *  If m1 (plus one time unit) is completely retraced in the same amount of time (or less) that it took to form and 
                 *  m(-2) and m0 do not share any similar price territory and 
                 *  m2 is larger than m(-2) and 
                 *  m(-2) and m0 are obviously different in price or time or both and 
                 *  when the price lengths of m(-3), m(-1) and m1 are compared, m(-1) is not the shortest, 
                 *  
                 *          a Trending Impulse pattern may have concluded with m1; place ":L5" at the end of m1.
                 * 
                 * ------------------------------------------------------------------------------------------------------------------------------------------- */
                if (
                        m1.Plus1TU_IsCompletelyRetraced_Faster_byM( m2 ) &&
                        m2_.DontOverlap( m0 ) &&
                        m2.IsLongerThan( m2_ ) &&
                        m2_.PriceTimeAlternation( m0 ) &&
                        m1_.IsNotTheShortest( m3_ , m1 )
                   )
                {
                    m1.WavePattern |= WavePattern.TrendingImpulse;
                    m1.AddStructureLabel( StructureLabelEnum.L5 );
                }


                /* -------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *  If m1 (plus one time unit) is completely retraced in the same amount of time (or less) that it took to form and 
                 *  m2 is longer than m(-2) and 
                 *  m(-4) is longer than m(-3), 
                 *  
                 *          a Zigzag or Flat may have concluded with m1; place ":L5" at the end of m1.
                 * 
                 * ------------------------------------------------------------------------------------------------------------------------------------------- */

                var m4_ = m3_.GetPrevious( );
                if ( m4_ == null )
                    return;

                if (
                        m1.Plus1TU_IsCompletelyRetraced_Faster_byM( m2 ) &&                        
                        m2.IsLongerThan( m2_ ) &&
                        m4_.IsLongerThan( m3_ )                         
                   )
                {
                    m1.WavePattern |= WavePattern.ZigZag | WavePattern.Flat;
                    m1.AddStructureLabel( StructureLabelEnum.L5 );
                }


                /* -------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *  If m1 (plus one time unit) is completely retraced in the same amount of time (or less) that it took to form and 
                 *  m2 is longer than m(-2) and 
                 *  m(-3) is longer than m(-2) and 
                 *  m(-4) is shorter than m(-3), 
                 *  
                 *          m1 may be the end of a Standard Elliott pattern which is part of a Complex Correction 
                 *          where m(-2) is an x-wave; 
                 *          place ":L5" at the end of m1 and 
                 *          "x:c3?" at the end of m(-2). 
                 * 
                 * In the above situation, if m(-1) is at least 161.8% of m0, 
                 *          the Standard Correction unfolding is probably a Zigzag. 
                 * 
                 * If m(-1) is at least 100%, but less than 161.8% of m0, the Standard Correction is probably a Flat.
                 * ------------------------------------------------------------------------------------------------------------------------------------------- */

                if (
                        m1.Plus1TU_IsCompletelyRetraced_Faster_byM( m2 ) &&
                        m2.IsLongerThan( m2_ ) &&
                        m3_.IsLongerThan( m2_ ) &&
                        m4_.IsShorterThan( m3_ )
                   )
                {
                    m1.MainWaveType   = WaveType.ComplexCorrection;                                        
                    m2_.PotentialWave = ElliottWaveEnum.WaveX;

                    m1.AddStructureLabel( StructureLabelEnum.L5 );
                    m2_.AddStructureLabel( StructureLabelEnum.x_c3_MAYBE );

                    if ( m1_.IsAtLeastX_OfM( 161.8, m0 ) )
                    {
                        m1.WavePattern |= WavePattern.ZigZag;
                    }

                    if ( m1_.IsBtw_AtLeastX_LessThanY_ofM( 100, 161.8, m0 ) )
                    {
                        m1.WavePattern |= WavePattern.Flat;
                    }
                }

                /* -------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *  If m1 (plus one time unit) is completely retraced in the same amount of time (or less) that it took to form and 
                 *  m2 is smaller than m(-2), 
                 *  
                 *          a Flat or Zigzag may have concluded with m1; 
                 *          place ":L5" at the end of m1.
                 * 
                 * ------------------------------------------------------------------------------------------------------------------------------------------- */
                if (
                       m1.Plus1TU_IsCompletelyRetraced_Faster_byM( m2 ) &&
                       m2.IsShorterThan( m2_ )
                   )
                {
                    m1.WavePattern |= WavePattern.Flat | WavePattern.ZigZag;
                    m1.AddStructureLabel( StructureLabelEnum.L5 );
                }

                /* -------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *  If m1 (plus one time unit) is completely retraced in the same amount of time (or less) that it took to form and 
                 *  m(-2) is smaller than m(-1) and 
                 *  m(-1) is not the shortest when compared to m(-3) and m1 
                 *  and m2 is not retraced more than 61.8% and 
                 *  the market approaches (or exceeds) the beginning of m(-3) within a time period which is 50% or less of that consumed by m(-3) through m1 and 
                 *  the end of m1 is not exceeded for a period which is four times as long as that consumed by m(-3) through m1 and 
                 *  m2-m4's price coverage is at least twice that of m1 and 
                 *  one of m(-1)'s possible Structure labels is ":c3," 
                 *  
                 *          a Terminal Impulse may have completed at the end of m1; 
                 *          add ":L3" to the current Structure list for m1.
                 * 
                 * ------------------------------------------------------------------------------------------------------------------------------------------- */
                if (
                       m1.Plus1TU_IsCompletelyRetraced_Faster_byM( m2 ) &&
                       m2_.IsShorterThan( m1_ ) &&
                       m1_.IsNotTheShortest( m3_, m1 ) &&
                       m2.IsRetraced_NoMoreThanX_byM( 61.8, m3 )
                   )
                {

                    //![](6A96D20B6FE57088D0F6E7891DEFA742.png;;;0.00945,0.01382)![](59B690375BFEE4B6B8C74EB7265F79F4.png;;;0.01072,0.01397)
                    
                    var m3_toM1  = m3_.Combine( m2_, m1_, m0, m1 );
                    var m2Tom4   = m2.Combine( m3, m4 );

                    if ( m2.CloseOrExceedX_TimeLTEy( m3_.Begin, 0.5 * m3_toM1.TimeUnit ) || m2Tom4.CloseOrExceedX_TimeLTEy( m3_.Begin, 0.5 * m3_toM1.TimeUnit ) )
                    {
                        if ( m1.EndNotExceededForXTime( 4 * m3_toM1.TimeUnit ) )
                        {
                            if ( m2Tom4.IsAtLeastX_OfM( 200, m1 ) )
                            {
                                // one of m(-1)'s possible Structure labels is ":c3," 
                                if ( m1_.StructureLabel.HasFlag( StructureLabelEnum.c3 ) )
                                {
                                    // a Terminal Impulse may have completed at the end of m1; 
                                    // *add ":L3" to the current Structure list for m1.

                                    m1.WavePattern |= WavePattern.TerminalImpulse;
                                    m1.AddStructureLabel( StructureLabelEnum.L3 );
                                }
                            }
                        }                                                
                    }
                }




                /* -------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *  If m1 (plus one time unit) is completely retraced in the same amount of time (or less) that it took to form and 
                 *  m3 is between 61.8-100% of m1, 
                 *  
                 *          m1 may be part of an Irregular Failure Flat; 
                 *          place ":F3" at the end of m1.
                 * 
                 * ------------------------------------------------------------------------------------------------------------------------------------------- */

                if (
                       m1.Plus1TU_IsCompletelyRetraced_Faster_byM( m2 ) && m3.IsBtw_XYInclusive_ofM( 61.8, 100, m1 )
                   )
                {

                    m1.WavePattern |= WavePattern.IrregularFailureFlat;
                    m1.AddStructureLabel( StructureLabelEnum.F3 );
                }

                /* -------------------------------------------------------------------------------------------------------------------------------------------
                    * 
                    *  If m1 (plus one time unit) is completely retraced in the same amount of time (or less) that it took to form and 
                    *  m3 is longer than m2 and 
                    *  m0 is at least 161.8% of m2 and 
                    *  m3 is completely retraced in the same amount of time (or less) that it took to form, 
                    *          m1 may be part of an Irregular Flat; place ":F3" at the end of m1.
                    * 
                    * ------------------------------------------------------------------------------------------------------------------------------------------- */

                if ( 
                        m1.Plus1TU_IsCompletelyRetraced_Faster_byM( m2 ) && 
                        m3.IsLongerThan( m2 ) && 
                        m0.IsAtLeastX_OfM( 161.8, m2 ) && 
                        m3.IsCompletelyRetraced_Faster_byM( m4 ) 
                   )
                {
                    m1.WavePattern |= WavePattern.IrregularFailureFlat;
                    m1.AddStructureLabel( StructureLabelEnum.F3 );
                }




                /* -------------------------------------------------------------------------------------------------------------------------------------------
                * 
                *  If m1 is completely retraced slower than it took to form and 
                *  m2 did not retrace more than 61.8% of the distance from the beginning of m(-1) to the end of m1 and 
                *  m3 is shorter than m2,
                *  m1 will be the most extreme price obtained by the market for a period at least twice that of the combined times of m0-m2
                *          
                *          the market may be concluding a Complex Correction in which ; 
                *          place an ":F3" at the end of m1.
                * 
                * ------------------------------------------------------------------------------------------------------------------------------------------- */

                var m1_Tom1 = m1_.Combine( m0, m1 );
                var m0Tom2  = m0.Combine( m1, m2 );

                if ( 
                        m1.IsCompletelyRetraced_Slower_byM( m2 ) && 
                        m1_Tom1.IsRetraced_LtX_byM( 61.8, m2 ) && 
                        m3.IsShorterThan( m2 ) &&
                        m1.IsPeakTroughForAtLeast_XTime( 2 * m0Tom2.TimeUnit )
                   )
                {
                    m1.MainWaveType = WaveType.ComplexCorrection;
                    m1.AddStructureLabel( StructureLabelEnum.F3 );
                }

                /* -------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *  If m1 is completely retraced slower than it took to form and 
                 *  m3 is longer than m2 and 
                 *  m2 is not more than 61.8% of the price distance from the beginning of m(-1) to the end of m1, 
                 *  then the market could be forming a Complex Correction 
                 *  (where m1 would be the end of one corrective phase of the pattern and m2 would probably be the end of an x-wave) 
                 *  or an Expanding Triangle; place ":F3/:c3/:L5" at the end of m1.
                 * 
                 * ------------------------------------------------------------------------------------------------------------------------------------------- */

                if ( 
                        m1.IsCompletelyRetraced_Slower_byM( m2 ) &&
                        m3.IsLongerThan( m2 ) &&
                        m1_Tom1.IsRetraced_LtX_byM( 61.8, m2 )   
                   )
                {
                    m1.MainWaveType  = WaveType.ComplexCorrection;
                    m1.WavePattern   |= WavePattern.AnyComplexCorrection | WavePattern.ExpandingTriangle;
                    m1.PotentialWave = ElliottWaveEnum.WaveC;
                    m2.PotentialWave = ElliottWaveEnum.WaveX;

                    m1.AddStructureLabel( StructureLabelEnum.F3 | StructureLabelEnum.c3 | StructureLabelEnum.L5 );
                }

                /* -------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *  If m1 is completely retraced slower than it took to form and 
                 *  m2 is smaller than m(-2), 
                 *          a Zigzag (which is part of a Contracting Triangle) may have concluded with m1; place ":L5" at the end of m1.
                 * 
                 * ------------------------------------------------------------------------------------------------------------------------------------------- */

                if ( 
                        m1.IsCompletelyRetraced_Slower_byM( m2 ) &&                     
                        m2.IsShorterThan( m2_ ) 
                   )
                {
                    // ![](02945D6E3DAF5B2FB7F02F7E03473675.png;;;0.03299,0.02522)
                    m1.MainWaveType  = WaveType.ComplexCorrection;
                    m1.WavePattern   |= WavePattern.ContractingTriangle;
                    m1.PotentialWave = ElliottWaveEnum.WaveC;

                    m1.AddStructureLabel( StructureLabelEnum.L5 );
                }

                /* -------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *  If m1 is completely retraced slower than it took to form and 
                 *  m(-1) is at least 61.8% of m1 and 
                 *  m3 is smaller than m2 and 
                 *  m3 (plus one time unit) is completely retraced in the same amount of time (or less) that it took to form, 
                 *  
                 *          m1 may be part of a Flat which concludes a larger pattern; 
                 *          place ":F3" at the end of m1.
                 * 
                 * ------------------------------------------------------------------------------------------------------------------------------------------- */



                if ( 
                        m1.IsCompletelyRetraced_Slower_byM( m2 ) && 
                        m1_.IsAtLeastX_OfM( 61.8, m1 ) && 
                        m3.IsShorterThan( m2 ) && 
                        m3.Plus1TU_IsCompletelyRetraced_Faster_byM( m4 ) 
                   )
                {
                    // ![](435827978B9DB1AEC935294883861DEA.png;;;0.02759,0.02240)
                    m1.MainWaveType = WaveType.ComplexCorrection;
                    m1.WavePattern  |= WavePattern.Flat;

                    m1.AddStructureLabel( StructureLabelEnum.F3 );
                }

                /* -------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *  If m3 is longer than m2 and 
                 *  m4 is longer than m3 and 
                 *  m0 is less than 61.8% of m1, 
                 *          m1 may have begun an Expanding Triangle; 
                 *          add "(:F3)" to the Structure list at the end of m1.
                 * 
                 * ------------------------------------------------------------------------------------------------------------------------------------------- */
                if ( 
                        m3.IsLongerThan( m2 ) && 
                        m4.IsLongerThan( m3 ) && 
                        m0.IsLessThanX_OfM( 61.8, m1 ) 
                   )
                {
                    m1.MainWaveType = WaveType.ComplexCorrection;
                    m1.WavePattern  |= WavePattern.ExpandingTriangle;

                    m1.AddStructureLabel( StructureLabelEnum.F3_MAYBE );
                }

                /* -------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *  If m3 is longer than m2 and 
                 *  m4 is longer than m3 and 
                 *  m0 is between 61.8-100% of m1, 
                 *  
                 *          the market may be forming an Expanding Triangle; 
                 *          add "(:c3)" to m1.
                 * 
                 * ------------------------------------------------------------------------------------------------------------------------------------------- */

                if ( 
                        m3.IsLongerThan( m2 ) && 
                        m4.IsLongerThan( m3 ) && 
                        m0.IsBtw_XYInclusive_ofM( 61.8, 100, m1 ) 
                   )
                {
                    m1.MainWaveType = WaveType.ComplexCorrection;
                    m1.WavePattern  |= WavePattern.ExpandingTriangle;

                    m1.AddStructureLabel( StructureLabelEnum.c3_MAYBE );
                }
            }
        }

        private void Rule5ConditionB( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m3 is longer than m2 and is closer to 100% than to 161.8% of m1, 
             *          place ":c3" at the end of m1. 
             *          
             *  If m(-1) is longer than m0 and the ":c3" is used as the preferred Structure choice, 
             *          add "b" in front of ":c3" to get "b:c3." 
             *          This means that m1 is the b-wave of a Flat correction. 
             *          
             *          ![](6CF91FBEFD3905797CC78CBA535047B4.png;;;0.02495,0.01925)
             *          
             *  If m(-1) is less than m0, m1 may be the x-wave of a Complex correction, add "x" in front of the ":c3" instead.
             * 
             * ![](C983C16574DF652FF6EC50B785207646.png;;;0.02968,0.02014)
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if (  m3.IsLongerThan( m2 ) && 
                  m3.IsCloserToX_ThanY_ofZ( 100, 161.8, m1 ) )
            {
                m1.AddStructureLabel( StructureLabelEnum.c3 );

                if ( m1_.IsLongerThan( m0 ) )
                {
                    // 
                    // if ( m1.PreferableStructureIs( StructureLabelEnum.c3 ) )
                    {
                        m1.DropStructureLabel( StructureLabelEnum.c3 );
                        m1.AddStructureLabel( StructureLabelEnum.b_c3 );

                        m1.WavePattern |= WavePattern.Flat;
                        m1.PotentialWave = ElliottWaveEnum.WaveB;
                    }
                }
                else if ( m1_.IsShorterThan( m0 ) )
                {
                    // ![](9CEDFC2827D0B353813F8F5C2F9A2B8F.png;;;0.02122,0.01980)
                    m1.PotentialWave = ElliottWaveEnum.WaveX;

                    m1.DropStructureLabel( StructureLabelEnum.c3 );
                    m1.AddStructureLabel( StructureLabelEnum.x_c3 );
                }
            }

            

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m3 is longer than m2 and m0 is closer to 161.8% of m1 than to 100% of m1, 
             *          place ":F3" at the end of m1. 
             *          
             *  If m(-1) is longer than m0, m2 is probably the end of a Zigzag. 
             *  ![](330233750BD423A7808E5BDA7D7DD675.png;;;0.01761,0.01452)
             *  If m(-1) is shorter than m0, m1 may be an x-wave of a Complex Correction which ends with m4. 
             *  
             *  ![](8DDC85E88300EEA20C95391AC3DC9EEE.png;;;0.01881,0.02177)
             *  To show these two possibilities respectively, add "b:c3" - and "x:c3" to the Structure list at the end of m1.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( 
                    m3.IsLongerThan( m2 ) && 
                    m0.IsCloserToX_ThanY_ofZ( 161.8, 100, m1 ) 
               )
            {
                m1.AddStructureLabel( StructureLabelEnum.F3 );

                if ( m1_.IsLongerThan( m0 ) )
                {
                    m2.WavePattern |= WavePattern.ZigZag;
                    m1.PotentialWave = ElliottWaveEnum.WaveB;
                    m1.AddStructureLabel( StructureLabelEnum.b_c3 );
                }
                else if ( m1_.IsShorterThan( m0 ) )
                {
                    m1.MainWaveType = WaveType.ComplexCorrection;
                    m1.PotentialWave = ElliottWaveEnum.WaveX;
                    m1.AddStructureLabel( StructureLabelEnum.x_c3 );
                }
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m3 is at least 61.8% of m1 and m3 completes without any part of it exceeding the end of m2 and 
             *  m2 is close to 61.8% of m0, 
             *  
             *          m1 may be the first leg of an Irregular Failure Flat: 
             *          add ":F3" to m1's Structure list if it is not already present.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( 
                    m3.IsAtLeastX_OfM( 61.8, m1 ) &&
                    m3.CompleteWithoutExceeding( m2, WavePosition.End ) &&
                    m2.IsApproxX_OfM( 61.8, m0 ) 
               )
            {
                m1.WavePattern |= WavePattern.IrregularFailureFlat;
                m1.AddStructureLabel( StructureLabelEnum.F3 );
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m2 is retraced less than 100% and 
             *  m3 is at least 61.8% of m1 
             *  and m3 completes without any part of it exceeding the end of m2, place ":F3" at the end of m1.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( 
                    m2.IsRetraced_LtX_byM( 100, m3 ) && 
                    m3.IsAtLeastX_OfM( 61.8, m1 ) && 
                    m3.CompleteWithoutExceeding( m2, WavePosition.End ) 
               )
            {
                m1.AddStructureLabel( StructureLabelEnum.F3 );
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m2 is retraced less than 61.8% 
             *  and m1 and m3 share some of the same price range and 
             *  m4 is no more than 261.8% of m2 and 
             *  m2 is not the shortest wave when compared with m0 and m4 and 
             *  
             *  m4 (plus one time unit) is completely retraced faster than it took to form with the market approaching (or exceeding) the beginning of m0 
             *  in 50% of the time (or less) than it took to form m0 through m4, 
             *  
             *  then the market possibly completed a Terminal pattern at m4; 
             *  
             *          place ":c3" at the end of m1 (if ":F3" is one of m1’s current Structure possibilities, 
             *          add brackets "[]" around it to indicate ":c3" is a much better choice).
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            var m4 = m3.GetNext( );
            if ( m4 == null )
                return;

            var m5 = m4.GetNext( );
            if ( m5 == null )
                return;

            var m6 = m5.GetNext( );
            
            var m0Tom4 = m0.Combine( m1, m2, m3, m4 );

            if ( 
                    m2.IsRetraced_LtX_byM( 61.8, m3 ) && 
                    m1.Overlap( m3 ) && 
                    m4.IsNoMoreThanX_OfM( 261.8, m2 ) &&
                    m2.IsNotTheShortest( m0,  m4 ) &&
                    m4.Plus1TU_IsCompletelyRetraced_Faster_byM( m5 )
               )
            {
                
                // ![](9C95407B5EE92668659513E33C21886F.png;;;0.01756,0.01922)

                if ( m5.CloseOrExceedX_TimeLTEy( m0.Begin, 0.5 * m0Tom4.TimeUnit ) )
                {
                    m4.WavePattern |= WavePattern.TerminalPattern;

                    if ( m1.StructureLabel.HasFlag( StructureLabelEnum.F3 ) )
                    {
                        m1.AddStructureLabel( StructureLabelEnum.c3_RARE );
                    }
                    else
                    {
                        m1.AddStructureLabel( StructureLabelEnum.c3 );
                    }
                }
                
                if ( m6 != null )
                {
                    var m7 = m6.GetNext( );
                    if ( m7 != null )
                    {
                        var m5Tom7 = m5.Combine( m6, m7 );

                        if ( m5Tom7.CloseOrExceedX_TimeLTEy( m0.Begin, 0.5 * m0Tom4.TimeUnit ) )
                        {
                            m4.WavePattern |= WavePattern.TerminalPattern;

                            if ( m1.StructureLabel.HasFlag( StructureLabelEnum.F3 ) )
                            {
                                m1.AddStructureLabel( StructureLabelEnum.c3_RARE );
                            }
                            else
                            {
                                m1.AddStructureLabel( StructureLabelEnum.c3 );
                            }
                        }
                    }
                }                                   
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m1 (plus one time unit) is completely retraced in the same amount of time (or less) that it took to form and 
             *  m2 is almost 161.8% of m1 and 
             *  m2 is retraced less than 61.8% and 
             *  m(-1) is at least 61.8% of m0 and 
             *  m(-2) is between 61.8-161.8% of m(-1) and 
             *  m(-3) is between 61.8-161.8% of m(-2) and 
             *  the combined price coverage of m2 through m4 is longer than m0, 
             *  
             *          m1 may have completed a Contracting Triangle; add ":L3" to any existing Structure list.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            var m2Tom4 = m2.Combine( m3, m4 );

            if ( 
                    m1.Plus1TU_IsCompletelyRetraced_Faster_byM( m2 ) && 
                    m2.IsApproxX_OfM( 161.8, m1 ) && 
                    m2.IsRetraced_LtX_byM( 61.8, m3 ) &&
                    m1_.IsAtLeastX_OfM( 61.8, m0 ) && 
                    m2_.IsBtw_XYInclusive_ofM( 61.8, 161.8, m1_ ) && 
                    m3_.IsBtw_XYInclusive_ofM( 61.8, 161.8, m2_ )
               )
            {
                if ( m2Tom4 != null && m2Tom4.IsLongerThan( m0 ) )
                {
                    m1.WavePattern |= WavePattern.ContractingTriangle;
                    m1.AddStructureLabel( StructureLabelEnum.L3 );
                }
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m1 (plus one time unit) is completely retraced in the same amount of time (or less) that it took to form and 
             *  m1 is not more than 161.8% of m(-1) and 
             *  m2 is almost 161.8% of m1 and 
             *  m2 is retraced less than 61.8% and 
             *  the combined price coverage of m2 through m4 is longer than m0, 
             *  
             *          m1 may have completed a Flat pattern; add ":L5" to any existing Structure list.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( 
                    m1.Plus1TU_IsCompletelyRetraced_Faster_byM( m2 ) && 
                    m1.IsNoMoreThanX_OfM( 161.8, m1_ ) && 
                    m2.IsApproxX_OfM( 161.8, m1 ) && 
                    m2.IsRetraced_LtX_byM( 61.8, m3 ) 
               )
            {
                if ( m2Tom4 != null && m2Tom4.IsLongerThan( m0 ) )
                {
                    m1.WavePattern |= WavePattern.Flat;
                    m1.AddStructureLabel( StructureLabelEnum.L5 );
                }
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m(-1) is shorter than m0 and 
             *  m2 (plus one time unit) is completely retraced in the same amount of time it took to form (or less) and 
             *  m0 is not the shortest when compared to m(-2) and m2 and 
             *  the market approaches (or exceeds) the beginning of m(-2) in a period of time 50% (or less) of that consumed by m(-2) through m2, 
             *  
             *          add "(:sL3)" to the Structure list, a Terminal Impulse pattern could have completed at the end of m2.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( 
                    m1_.IsShorterThan( m0 ) && 
                    m2.Plus1TU_IsCompletelyRetraced_Faster_byM( m3 ) && 
                    m0.IsNotTheShortest( m2_, m2 )
               )
            {                                                
                var m2_Tom2 = m2_.Combine( m1_, m0, m1, m2 );

                var m3Tom5  = m3.Combine( m4, m5 );

                if ( m3.CloseOrExceedX_TimeLTEy( m2_.Begin, 0.5 * m2_Tom2.TimeUnit ) || m3Tom5.CloseOrExceedX_TimeLTEy( m2_.Begin, 0.5 * m2_Tom2.TimeUnit ) )
                {
                    m1.AddStructureLabel( StructureLabelEnum.sL3_MAYBE );
                    m2.WavePattern |= WavePattern.TerminalImpulse;
                }                
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
            * 
            *  If none of the above conditions fit your current situation and m1 is a monowave, 
            *  place all Structure Labels at the beginning of this section at the end of m1. 
            *  
            *  If none of the above conditions fit your current situation and m1 is a compacted polywave (or higher) pattern, 
            *  just move on to the section entitled "Implementation of Position Indicators" and 
            *  use surrounding Structure labels to decide the Position Indicator which belongs in front of m1's compacted Structure label.
            * 
            * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( m1.MonoWavesCount == 0 )
            {
                //  {:F3/:c3/:5/:L5/(:L3)}
                m1.AddStructureLabel( StructureLabelEnum.F3 | StructureLabelEnum.c3 | StructureLabelEnum._5 | StructureLabelEnum.L5 | StructureLabelEnum.sL3_LESSLIKELY );
            }

        }

        private void Rule5ConditionC( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            // ![](2ECA405BB294CC1CED7EEA3A2B7FEBCC.png;;;0.02756,0.02861)
            

            var m4 = m3.GetNext( );
            if ( m4 == null )
                return;

            var m5 = m4.GetNext( );
            if ( m5 == null )
                return;

            var m6 = m5.GetNext( );
            if ( m6 == null )
                return;

            bool hasStructureLabel = false;

            var m4tom6 = m4.Combine( m5, m6 );

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m3 is between 61.8-161.8% of m1 (inclusive) and 
             *  m2 is less than 61.8% of m0 and 
             *  m4 is at least 100% of m2 and 
             *  m4 (or m4 through m6) is at least 100% of m0, 
             *  
             *          m1 may be the first leg of an Irregular Flat (either variation) or 
             *          a Running Triangle pattern; place ":F3" at the end of m1.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */
            if ( 
                    m3.IsBtw_XYInclusive_ofM( 61.8, 161.8, m1 ) && 
                    m2.IsLessThanX_OfM( 61.8, m0 ) && 
                    m4.IsAtLeastX_OfM( 100, m2 ) &&
                    ( m4.IsAtLeastX_OfM( 100, m0 ) || m4tom6.IsAtLeastX_OfM( 100, m0 ) )
               )
            {
                m1.WavePattern |= WavePattern.Irregular | WavePattern.RunningTriangle;
                m1.AddStructureLabel( StructureLabelEnum.F3 );
                hasStructureLabel = true;
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m3 is between 101%-161.8% of m2, 
             *  there is the unlikely possibility an Expanding Triangle is forming; place ":c3" at the end of m1.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( m3.IsBtw_XYInclusive_ofM( 101, 161.8, m2 ) )
            {
                m1.WavePattern |= WavePattern.ExpandingTriangle;
                m1.AddStructureLabel( StructureLabelEnum.c3 );
                hasStructureLabel = true;
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m2 (plus one time unit) is completely retraced in the same amount of time (or less) that it took to form 
             *  and m(-1) is shorter than m0 and 
             *  m0, when compared to m(-2) and m2, is not the shortest of the three and 
             *  m(-1) and m1 share some of the same price range and 
             *  the market approaches (or exceeds) the beginning of m(-2) in a period of time which is half (or less) of that consumed by m(-2) through m2, 
             *  
             *          add "(:sL3)" to the Structure list (indicating the possibility of a Terminal pattern completing with m2).
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */
            
            var m2_tom2 = m2_.Combine( m1_, m0, m1, m2 );
            var m3Tom5  = m3.Combine( m4, m5 );

            if ( 
                    m2.Plus1TU_IsCompletelyRetraced_Faster_byM( m3 ) && 
                    m1_.IsShorterThan( m0 ) && 
                    m0.IsNotTheShortest( m2_, m2 )  &&
                    m1_.Overlap( m1 )
               )
            {                               
                if ( m3.CloseOrExceedX_TimeLTEy( m2_.Begin, 0.5 * m2_tom2.TimeUnit ) || m3Tom5.CloseOrExceedX_TimeLTEy( m2_.Begin, 0.5 * m2_tom2.TimeUnit ) )
                {
                    m2.WavePattern |= WavePattern.TerminalPattern;
                    m1.AddStructureLabel( StructureLabelEnum.sL3_LESSLIKELY );
                    hasStructureLabel = true;
                }               
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m2 is retraced less than 61.8% and m1 and m3 share some of the same price range 
             *  and m4 is shorter than m2 
             *  and m4 (plus one time unit) is completely retraced faster than it took to form with the 
             *  market approaching (or exceeding) the beginning of m0 in a period of time which is 50% (or less) of that taken to form m0 through m4, 
             *  
             *          then the market possibly completed a Terminal pattern at m4; place ":c3" at the end of m1.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( m2.IsRetraced_LtX_byM( 61.8, m3 ) && m1.Overlap( m3 ) && m4.IsShorterThan( m2 ) && m4.Plus1TU_IsCompletelyRetraced_Faster_byM( m5 ) )
            {                
                var m0tom4 = m0.Combine( m1, m2, m3, m4 );

                if ( m5.CloseOrExceedX_TimeLTEy( m0.Begin, 0.5 * m0tom4.TimeUnit ) )
                {
                    m4.WavePattern |= WavePattern.TerminalPattern;
                    m1.AddStructureLabel( StructureLabelEnum.c3 );
                    hasStructureLabel = true;
                }

                var m7 = m6.GetNext( );
                if ( m7 == null )
                    return;

                var m5Tom7 = m5.Combine( m6, m7 );

                if ( m5Tom7.CloseOrExceedX_TimeLTEy( m0.Begin, 0.5 * m0tom4.TimeUnit ) )
                {
                    m4.WavePattern |= WavePattern.TerminalPattern;
                    m1.AddStructureLabel( StructureLabelEnum.c3 );
                    hasStructureLabel = true;
                }
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m1 (plus one time unit) is completely retraced in the same amount of time (or less) that it took to form and 
             *  m2 is close to 161.8% of m1 and 
             *  m2 is retraced less than 61.8% 
             *  and m(-1) is at least 61.8% of m0 and 
             *  m(-2) is between 61.8-161.8% of m(-1) and 
             *  m(-3) is between 61.8-161.8% of m(-2) and 
             *  the combined price coverage of m2 through m4 is longer than m0, 
             *  
             *          m1 may have completed a Contracting Triangle; add ":L3" to any existing Structure list.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */
            var m2Tom4 = m2.Combine( m3, m4 );

            if ( m1.Plus1TU_IsCompletelyRetraced_Faster_byM( m2 ) && m2.IsApproxX_OfM( 161.8, m1 ) && m2.IsRetraced_LtX_byM( 61.8, m3 ) )
            {
                if ( m1_.IsAtLeastX_OfM( 61.8, m0 ) && m2_.IsBtw_AtLeastX_LessThanY_ofM( 61.8, 161.8, m1_ ) && m3_.IsBtw_AtLeastX_LessThanY_ofM( 61.8, 161.8, m2_ ) )
                {
                    if ( m2Tom4.IsLongerThan( m0 ) )
                    {
                        m1.WavePattern |= WavePattern.ContractingTriangle;
                        m1.AddStructureLabel( StructureLabelEnum.L3 );
                        hasStructureLabel = true;
                    }
                }
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m1 (plus one time unit) is completely retraced in the same amount of time (or less) that it took to form and 
             *  m1 is very close to 61.8% of m0 but m1 is no more than 161.8% of m(-1) and              
             *  m2 is very close to 161.8% of m1 and 
             *  m2 is retraced less than 61.8% and 
             *  the combined price coverage of m2 through m4 is longer than m0, 
             *  
             *          m1 may have completed a Flat pattern; add ":L5" to any existing Structure list.
             *          
             *  If no Structure label has been placed for m1 during this section, place ":F3" at the end of m1.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( m1.Plus1TU_IsCompletelyRetraced_Faster_byM( m2 ) && m1.IsBtw_AtLeastX_LessThanY_ofM( 61.8, 161.8, m1_ ) && m2.IsApproxX_OfM( 161.8, m1 ) )
            {
                if ( m2.IsRetraced_LtX_byM( 61.8, m3 ) )
                {
                    if ( m2Tom4.IsLongerThan( m0 ) )
                    {
                        m1.WavePattern |= WavePattern.Flat;
                        m1.AddStructureLabel( StructureLabelEnum.L5 );
                        hasStructureLabel = true;
                    }
                }
            }

            if ( !hasStructureLabel )
            {
                m1.AddStructureLabel( StructureLabelEnum.F3 );
            }

        }

        private void Rule5ConditionD( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3 )
        {
            if ( m1 == null || m2 == null || m3 == null )
                return;

            if ( m2.MonoWavesCount > 3 )
            {
                m1.AddStructureLabel( StructureLabelEnum.F3 );
            }

            var m4 = m3.GetNext( );
            if ( m4 == null )
                return;

            var m5 = m4.GetNext( );
            if ( m5 == null )
                return;



            bool hasStructureLabel = false;

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m2 (plus one time unit) is completely retraced in the same amount of time (or less) that it took to form 
             *  and m(-2) is shorter than m0 and 
             *  after m2, the market approaches (or exceeds) the beginning of m(-2) within 50% (or less) of the time taken to form m(-2) through m2; 
             *  
             *          add "(:sL3)" to the Structure list (indicating the outside possibility of a 3rd Extension Terminal pattern completing with m2).
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( 
                    m2.Plus1TU_IsCompletelyRetraced_Faster_byM( m3 ) && 
                    m2_.IsShorterThan( m0 ) 
               )
            {                
                var m2_tom2 = m2_.Combine( m1_, m0, m1, m2 );

                var m3Tom5  = m3.Combine( m4, m5 );

                if ( m3.CloseOrExceedX_TimeLTEy( m2_.Begin, 0.5 * m2_tom2.TimeUnit ) || m3Tom5.CloseOrExceedX_TimeLTEy( m2_.Begin, 0.5 * m2_tom2.TimeUnit ) )
                {
                    m2.WavePattern |= WavePattern.TerminalPattern;
                    m1.AddStructureLabel( StructureLabelEnum.sL3_LESSLIKELY );
                    hasStructureLabel = true;
                }
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * If m2 is retraced less than 61.8% and 
             * m1 and m3 share some of the same price range and 
             * m4 is shorter than m2 and 
             * m4 is completely retraced faster than it took to form with 
             * the market approaching (or exceeding) the beginning of m0 within 50% (or less) of the time it taken to form m0 through m4, 
             * 
             *      then the market possibly completed a Terminal pattern at m4; place ":c3" at the end of m1.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            var m6 = m5.GetNext( );
            if ( m6 == null )
                return;

            if ( 
                    m2.IsRetraced_LtX_byM( 61.8, m3 ) &&
                    m1.Overlap( m3 ) &&
                    m4.IsShorterThan( m2 ) &&
                    m4.IsCompletelyRetraced_Faster_byM( m5 )
               )
            {                                    
                var m0Tom4 = m0.Combine( m1, m2, m3, m4 );

                if ( m5.CloseOrExceedX_TimeLTEy( m0.Begin, 0.5 * m0Tom4.TimeUnit ) )
                {
                    m4.WavePattern |= WavePattern.TerminalPattern;
                    m1.AddStructureLabel( StructureLabelEnum.c3 );
                    hasStructureLabel = true;
                }

                var m7 = m6.GetNext( );
                if ( m7 != null )
                {
                    var m5Tom7 = m5.Combine( m6, m7 );

                    if ( m5Tom7.CloseOrExceedX_TimeLTEy( m0.Begin, 0.5 * m0Tom4.TimeUnit ) )
                    {
                        m4.WavePattern |= WavePattern.TerminalPattern;
                        m1.AddStructureLabel( StructureLabelEnum.c3 );
                        hasStructureLabel = true;
                    }
                }                
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m2 is retraced less than 61.8% and 
             *  m2-m4's combined price coverage is larger and their movement more vertical than m0's and 
             *  m(-1) is at least 61.8% of m0, 
             *  
             *          there is a remote chance m1 completed a Contracting Triangle; add "(:L3)" to m1.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            var m2Tom4 = m2.Combine( m3, m4 );

            if ( 
                    m2.IsRetraced_LtX_byM( 61.8, m3 ) &&
                    m2Tom4.IsLongerAndMoreVerticalThan( m0 ) && 
                    m1_.IsAtLeastX_OfM( 61.8, m0 )
               )
            {
                m1.WavePattern |= WavePattern.ContractingTriangle;
                m1.AddStructureLabel( StructureLabelEnum.L3_LESSLIKELY );
                hasStructureLabel = true;
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m2 is retraced less than 61.8% and 
             *  m2-m4’s combined price coverage is larger and their movement more vertical than m0's and 
             *  m(-1) is approximately equal to m0 in price and 
             *  m1 is equal (or greater) in time to m(-1) and 
             *  m0 takes more time than m(-1) and more time than m1, 
             *  
             *          there is a remote chance m1 completed a "severe" C-Failure Flat; add "[:L5]" to m1.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( 
                    m2.IsRetraced_LtX_byM( 61.8, m3 ) && 
                    m2Tom4.IsLongerAndMoreVerticalThan( m0 ) && 
                    m1_.PriceApproxEqual( m0 ) &&
                    m1.TimeUnit >= m1_.TimeUnit &&
                    m0.TimeUnit > m1_.TimeUnit && 
                    m0.TimeUnit > m1.TimeUnit
               )
            {
                m1.WavePattern |= WavePattern.CFailureFlat;
                m1.AddStructureLabel( StructureLabelEnum.L5_RARE );
                hasStructureLabel = true;
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m3 is between 61.8-100% of m2 and 
             *  m4 is less than 61.8% of m0 and 
             *  m1 takes less time than m0, 
             *  
             *          m1 may be an "x-wave" within a Complex Correction; 
             *          jot that down and place "x:c3" at the end of m1.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( 
                    m3.IsBtw_XYInclusive_ofM( 61.8, 100, m2 ) && 
                    m4.IsLessThanX_OfM( 61.8, m0 ) && 
                    m1.TimeUnit < m0.TimeUnit 
                )
            {
                m1.WavePattern |= WavePattern.AnyComplexCorrection;
                m1.PotentialWave = ElliottWaveEnum.WaveX;
                m1.AddStructureLabel( StructureLabelEnum.x_c3 );
                hasStructureLabel = true;
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m3 is between 61.8-100% of m2 and m4 is 61.8% of m0 (or more), put ":F3" at the end of m1.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( m3.IsBtw_XYInclusive_ofM( 61.8, 100, m2 ) && m4.IsMoreThanX_ofM( 61.8, m0 ) )
            {
                m1.AddStructureLabel( StructureLabelEnum.F3 );
                hasStructureLabel = true;
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m3 is less than 61.8% of m2, place an ":F3/:c3" at the end of m1.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( m3.IsLessThanX_OfM( 61.8, m2 ) )
            {
                m1.AddStructureLabel( StructureLabelEnum.F3 | StructureLabelEnum.c3 );
                hasStructureLabel = true;
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  If m3 is at least 61.8% of m1 but 
             *  less than 100% of m2 and 
             *  m4 is at least as long as m2 and 
             *  m4 (or m4 through m6), without breaking beyond the end of m3, is at least 61.8% of m0, 
             *  
             *      there is a good chance m1 is the first leg of an Irregular Failure Flat; add ":F3" to m1.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( m3.IsAtLeastX_OfM( 61.8, m1 ) && m3.IsLessThanX_OfM( 100, m2 ) && m4.IsAtLeastX_OfM( 100, m2 ) )
            {
                //![](D7512772168A0B917FEA13B96E9B7765.png;;;0.01646,0.01481)
                var m4tom6 = m4.Combine( m5, m6 );

                if ( ( !m4.Break( m3.End ) ) && ( !m4tom6.Break( m3.End ) ) )
                {
                    m1.WavePattern |= WavePattern.IrregularFailureFlat;
                    m1.AddStructureLabel( StructureLabelEnum.F3 );
                    hasStructureLabel = true;
                }
            }

            if ( !hasStructureLabel )
            {
                m1.AddStructureLabel( StructureLabelEnum.F3 );
            }

        }
    }
}
