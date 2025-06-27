using fx.Bars;
using fx.Definitions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace fx.Algorithm
{
    public static class SBarHelper
    {
        public static Color? GetCandleStickPatternColor( this SBar bar, bool border )
        {
            if ( bar.CandlePatterns == TACandle.NONE )
            {
                return null;
            }

            Color patternColor = Color.FromRgb( 1, 104, 179 );

            if ( DataBarHelper.GetCandlestickCount( bar.CandlePatterns ) > 1 )
            {
                patternColor = bar.IsRising ? ( border ? Color.FromRgb( 255, 192, 0 ) : Color.FromRgb( 1, 183, 1 ) ) : ( border ? Color.FromRgb( 255, 192, 0 ) : Color.FromRgb( 255, 0, 0 ) );
                return patternColor;
            }

            switch ( bar.CandlePatterns )
            {
                case TACandle.CdlBreakAwayBear:
                case TACandle.Cdl3LineStrikeBear:
                case TACandle.Cdl3LineStrikeBull:
                case TACandle.CdlRising3Methods:
                case TACandle.CdlFalling3Methods:
                //case TACandle.Cdl3OutsideUp:
                //case TACandle.Cdl3OutsideDown:
                case TACandle.CdlLadderBottom:
                    patternColor = bar.IsRising ? ( border ? Color.FromRgb( 0, 0, 255 ) : Color.FromRgb( 133, 133, 253 ) ) : ( border ? Color.FromRgb( 192, 2, 56 ) : Color.FromRgb( 255, 0, 0 ) );
                    break;

                case TACandle.CdlBreakAwayBull:
                case TACandle.Cdl3WhiteSoldiers:
                    patternColor = bar.IsRising ? ( border ? Color.FromRgb( 0, 0, 255 ) : Color.FromRgb( 133, 133, 253 ) ) : ( border ? Color.FromRgb( 237, 201, 213 ) : Color.FromRgb( 246, 195, 202 ) );
                    break;

                case TACandle.Cdl3BlackCrows:
                    patternColor = bar.IsRising ? ( border ? Color.FromRgb( 200, 218, 251 ) : Color.FromRgb( 227, 236, 253 ) ) : ( border ? Color.FromRgb( 192, 2, 56 ) : Color.FromRgb( 255, 0, 0 ) );
                    break;

                //case TACandle.CdlDownsideTasukiGap:
                //case TACandle.CdlUpsideTasukiGap:
                //case TACandle.CdlMorningDojiStar:
                case TACandle.CdlMorningStar:
                    //case TACandle.CdlMorningStarAverage:
                    //case TACandle.CdlEveningDojiStar:
                    //case TACandle.CdlEveningStar:
                    //case TACandle.CdlEveningStarAverage:
                    //case TACandle.CdlUpsideGap3:
                    //case TACandle.CdlDownSideGap3:
                    patternColor = bar.IsRising ? ( border ? Color.FromRgb( 1, 113, 1 ) : Color.FromRgb( 0, 255, 0 ) ) : ( border ? Color.FromRgb( 192, 2, 56 ) : Color.FromRgb( 255, 0, 0 ) );
                    break;

                case TACandle.CdlDoji:
                case TACandle.CdlDojiStarBear:
                case TACandle.CdlDojiStarBull:
                    patternColor = Color.FromRgb( 234, 0, 255 );
                    break;

                case TACandle.CdlDarkCloudCover:
                case TACandle.CdlEngulfingBear:
                case TACandle.CdlPiercing:
                case TACandle.CdlEngulfingBull:
                //case TACandle.CdlThrusting:
                //case TACandle.CdlKickingBear:
                //case TACandle.CdlKickingBull:
                case TACandle.CdlAdvanceBlock:

                    patternColor = bar.IsRising ? ( border ? Color.FromRgb( 1, 113, 1 ) : Color.FromRgb( 0, 255, 0 ) ) : Color.FromRgb( 0, 0, 0 );
                    break;

                //case TACandle.CdlSpinningTop:
                case TACandle.CdlHangingMan:
                case TACandle.CdlHammer:
                    patternColor = bar.IsRising ? ( border ? Color.FromRgb( 0, 0, 0 ) : Color.FromRgb( 0, 255, 0 ) ) : ( border ? Color.FromRgb( 0, 0, 0 ) : Color.FromRgb( 255, 0, 0 ) );
                    break;

                //case TACandle.CdlInvertedHammerPre:
                //    patternColor = ( border ? Color.FromRgb( 237, 201, 213 ) : Color.FromRgb( 246, 195, 202 ) );
                //    break;

                //case TACandle.CdlStickSandwichBull:
                //    patternColor = this.IsRising ? ( border ? Color.FromRgb( 0, 0, 255 ) : Color.FromRgb( 0, 96, 191 ) ) : ( border ? Color.FromRgb( 192, 2, 56 ) : Color.FromRgb( 0, 96, 191 ) );
                //    break;

                //case TACandle.CdlStickSandwichBear:
                //case TACandle.CdlMatchingLow:
                //    patternColor = this.IsRising ? ( border ? Color.FromRgb( 0, 0, 255 ) : Color.FromRgb( 255, 0, 0 ) ) : ( border ? Color.FromRgb( 192, 2, 56 ) : Color.FromRgb( 255, 0, 0 ) );
                //    break;

                case TACandle.CdlInvertedHammer:
                    patternColor = bar.IsRising ? ( border ? Color.FromRgb( 1, 113, 1 ) : Color.FromRgb( 0, 255, 0 ) ) : ( border ? Color.FromRgb( 192, 2, 56 ) : Color.FromRgb( 255, 0, 0 ) );
                    break;

                //case TACandle.CdlTriStarBear:
                //    patternColor = Color.FromRgb( 255, 0, 0 );
                //    break;

                //case TACandle.CdlTriStarBull:
                //    patternColor = Color.FromRgb( 0, 0, 255 );
                //    break;

                default:
                    patternColor = bar.IsRising ? ( border ? Color.FromRgb( 255, 192, 0 ) : Color.FromRgb( 1, 183, 1 ) ) : ( border ? Color.FromRgb( 255, 192, 0 ) : Color.FromRgb( 255, 0, 0 ) );
                    break;
            }

            return patternColor;
        }

    }
}
