using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Definitions
{
    public class TACandleViewModel
    {
        public TACandleViewModel( TACandle candle)
        {
            Candle = candle;
        }

        public TACandle Candle { get; set; }

        public string ImageUrl
        {
            get
            {
                switch ( Candle )
                {
                    case TACandle.CdlEngulfingBear:
                        return "../Images/Candles/BearishEngulfing_16x16.png";

                    case TACandle.CdlEngulfingBull:
                        return "../Images/Candles/BullishEngulfing_16x16.png";

                    case TACandle.CdlDarkCloudCover:
                        return "../Images/Candles/DarkcloudCover_16x16.png";

                    case TACandle.CdlEveningStar:
                        return "../Images/Candles/EveningStar_16x16.png";

                    case TACandle.CdlDoji:
                        return "../Images/Candles/GreenDoji_16x16.png";

                    case TACandle.CdlHammer:
                        return "../Images/Candles/GreenHammer_16x16.png";

                    case TACandle.CdlMorningStar:
                        return "../Images/Candles/MorningStar_16x16.png";


                    case TACandle.CdlPiercing:
                        return "../Images/Candles/Piercing_16x16.png";

                    case TACandle.CdlTriStarBear:
                    case TACandle.CdlTriStarBull:
                        return "../Images/Candles/TriStars_16x16.png";

                    case TACandle.CdlShootingStar:
                        return "../Images/Candles/RedShootingStar_16x16.png";

                }


                throw new Exception( "Should not be possible" );
            }
            set
            {
                throw new Exception( "Should not be possible" );
            }
        }
    }
}
