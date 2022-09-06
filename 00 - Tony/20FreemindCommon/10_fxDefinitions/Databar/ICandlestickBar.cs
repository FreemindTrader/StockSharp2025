using System;
using System.Collections.Generic;
using System.Linq;
using StockSharp.Algo.Candles;

namespace fx.Definitions
{    
    public interface ICandleBar
    {
        WholeCandleRangeF WholeCandle { get; }

        UpperShadowRangeF UpperShadow { get; }
        LowerShadowRangeF LowerShadow { get; }

        RealBodyRangeF RealBody { get; }

        float Open { get; }
        float Close { get; }
        float High { get; }
        float Low { get; }

        int Volume { get; }
    }

    public interface ICandlestickBar : ICandleBar
    {
        Candle BarCandle { get;  }
        bool   IsDojiCandle { get; }
        bool   IsBlackCandle                        { get; }
        bool   IsWhiteCandle                        { get; }

        

        

        

        double UpperShadowLength                    { get; }
        double UpperShadowLengthAsPip               { get; }
        double LowerShadowLength                    { get; }
        double LowerShadowLengthAsPip               { get; }
        double RealBodyLength                       { get; }
        double RealBodyAsPip                        { get; }
        double CandleLength                         { get; }
        double CandleLengthAsPip                    { get; }     
        
        float PointSize { get; }

        int    CandleColor                          { get; }
                
        TACandle? CandlePatterns { get; set; }
        
        bool    HasCandleStickPattern               { get; }
       
        string CandleStickPatten { get; }
        

        void ClearPattern( );

    }

    public enum CandleStrength
    {
        UNKNOWN   = 0,
        Level1    = 1,
        Level2    = 2,
        Level3    = 3,
        Level4    = 4,
        Level5    = 5,
        Level6    = 6,
        Level7    = 7,
        Level8    = 8,
        Level9    = 9,
        Level10   = 10,
        Level11   = 11,
        Level12   = 12,

    }


    [Flags]
    public enum TACandle 
    {
        NONE                  = 0,
        CdlMorningStar        = 1 << 1,
        CdlDoji               = 1 << 2,
        CdlEveningStar        = 1 << 3,
        CdlBreakAwayBull      = 1 << 4,
        CdlBreakAwayBear      = 1 << 5,
        CdlAdvanceBlock       = 1 << 6,
        CdlPiercing           = 1 << 7,
        CdlDarkCloudCover     = 1 << 8,
        CdlEngulfingBull      = 1 << 9,
        CdlEngulfingBear      = 1 << 10,

        Cdl3BlackCrows        = 1 << 11,
        Cdl3WhiteSoldiers     = 1 << 12,

        CdlDojiStarBear       = 1 << 13,
        CdlDojiStarBull       = 1 << 14,
        CdlHammer             = 1 << 15,
        CdlHangingMan         = 1 << 16,
        CdlShootingStar       = 1 << 17,
        CdlInvertedHammer     = 1 << 18,
        
        CdlRising3Methods     = 1 << 19,
        CdlFalling3Methods    = 1 << 20,
        CdlLadderBottom       = 1 << 21,
        Cdl3LineStrikeBear    = 1 << 22,
        Cdl3LineStrikeBull    = 1 << 23,
        CdlMeetingLinesBear   = 1 << 24,

        CdlTriStarBear        = 1 << 25,
        CdlTriStarBull        = 1 << 26,
    }

    public enum CandlePatternEnum
    {
        None                    = 0,
            
        Cdl3LineStrikeBear      = 1,
        Cdl3LineStrikeBull      = 2,
        Cdl3BlackCrows          = 3,            
        CdlEveningStar          = 4,
        CdlUpsideTasukiGap      = 5,
        CdlInvertedHammer       = 6,
        CdlMatchingLow          = 7,
        CdlAbandonedBabyBull    = 8,
        Cdl2BlackGapping        = 9,
        CdlBreakAwayBear        =10,
        CdlMorningStar          =11,
        CdlPiercing             =12,
        CdlStickSandwichBull    =13,
        CdlThrusting            =14,
        CdlMeetingLinesBear     =15,            
        Cdl3StarsInSouth        =16,                       // Reversal Bull Market        
        Cdl3WhiteSoldiers       =17,
        CdlIndentical3Crows     =18,
        CdlEngulfingBear        =19,    
        CdlMorningDojiStar      =20,
        Cdl3OutsideUp           =21,        
        CdlBeltHoldBull         =22,
        CdlBeltHoldBear         =23,
        CdlEveningDojiStar      =24,        
        CdlAbandonedBabyBear    =25,
        Cdl3OutsideDown         =26,
        CdlMatHold              =27,                             // Bull Market Continuations
        CdlDeliberation         =28,
        CdlConcealBabySwallow   =29,
        CdlRising3Methods       =30,
        CdlSeparatingLinesBull  =31,
        CdlFalling3Methods      =32,
        CdlDojiStarBear         =33,
        CdlLastEngulfingTop     =34,   
        CdlSideSideWhiteLines   =35,    
        CdlLastEngulfingBottom  =36,
        CdlAdvanceBlock         =37,
        CdlDojiStarBull         =38,            
        CdlSeparatingLinesBear  =39,
        CdlUpsideGap3           =40,                              
        CdlBreakAwayBull        =41,
        CdlKickingBear          =42,
        CdlKickingBull          =43,                              // Bear Continuation
        Cdl13NewPriceLinesBear  =44,                              // Bear Continuation
        Cdl13NewPriceLinesBull  =45,
        CdlTriStarBull          =46,
        CdlTriStarBear          =47,
        CdlDarkCloudCover       =48,
        CdlEngulfingBull        =49,
        CdlMorningStarAverage   =50,
        CdlDownSideGap3         =51,            
        Cdl3Inside              =52,
        CdlDownsideTasukiGap    =53,
        CdlHammer               =54,
        CdlCloseingMarubozu     =55,
        CdlCounterAttack        =56,
        CdlDragonFlyDoji        =57,
        CdlGraveStoneDoji       =58,
        CdlHangingMan           =59,
        CdlHarami               =60,
        CdlHaramiCross          =61,
        CdlHighWave             =62,
        CdlHikakake             =63,
        CdlHikakakeMod          =64,
        CdlHomingPigeon         =65,
        CdlIdentical3Crows      =66,
        CdlKickingByLength      =67,
        CdlLadderBottom         =68,
        CdlLongLeggedDoji       =69,
        CdlEveningStarAverage   =70,
        CdlMarubozu             =71,
        CdlOnNeck               =72,
        CdlRickShawman          =73,
        CdlShootingStar         =74,
        CdlShortLine            =75,
        CdlSpinningTop          =76,
        CdlStalleddPattern      =77,
        CdlTakuri               =78,
        CdlTriStar              =79,
        CdlUnique3River         =80,
        CdlUpsideGap2Crows      =81,
        CdlInvertedHammerPre    =82,
        CdlStickSandwichBear    =83,
        CdlDoji                 =99                        
    }
}
