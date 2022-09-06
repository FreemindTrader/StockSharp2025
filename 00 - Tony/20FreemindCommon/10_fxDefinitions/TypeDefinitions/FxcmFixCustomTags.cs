using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    public static class FxcmTags
    {
        /*/*
        <value description ="FXCM_NEWS_REQUEST" enum="U50"/>
        <value description ="FXCM_NEWS_TOPIC_REQUEST" enum="U51"/>
        <value description ="FXCM_REQUEST_REJECT" enum="U52"/>
        <value description ="FXCM_NEWS_TOPIC_RESPONSE" enum="U53"/>
        */
        public const int PegPriceType                      = 1094;
        public const int ContingencyType                   = 1385;
        public const int FXCMSymID                         = 9000;
        public const int FXCMSymPrecision                  = 9001;
        public const int FXCMSymPointSize                  = 9002;
        public const int FXCMSymInterestBuy                = 9003;
        public const int FXCMSymInterestSell               = 9004;
        public const int FXCMSymSortOrder                  = 9005;
        public const int FXCMSymMarginRatio                = 9006;
        public const int FXCMTimingInterval                = 9011;

        enum FXCMTimingIntervalEnum
        {
            SPOT   = 0,
            MIN1   = 1,
            MIN5   = 2,
            MIN15  = 3,
            MIN30  = 4,
            HOUR1  = 5,
            DAY1   = 6,
            WEEK1  = 7,
            MONTH1 = 8
        }


        public const int FXCMStartDate                     = 9012;
        public const int FXCMStartTime                     = 9013;
        public const int FXCMEndDate                       = 9014;
        public const int FXCMEndTime                       = 9015;
        public const int FXCMNoParam                       = 9016;
        public const int FXCMParamName                     = 9017;
        public const int FXCMParamValue                    = 9018;
        public const int FXCMServerTimeZone                = 9019;
        public const int FXCMContinuousFlag                = 9020;

        enum FXCMContinuousFlagEnum
        {
            SINGLE          = 0,
            PART_OF_PACKAGE = 1,
            END_OF_PACKAGE  = 2
        }

        public const int FXCMNoSnapshot                    = 9021;
        public const int FXCMPageID                        = 9022;
        public const int FXCMPageviewID                    = 9023;
        public const int FXCMPageviewLifetime              = 9024;
        public const int FXCMRequestRejectReason           = 9025;

        enum FXCMRequestRejectReasonEnum
        {
            UNKNOWN                   = 0,
            GENERIC                   = 1,
            DATA_NOT_FOUND            = 2,
            TRADING_SESSION_NOT_FOUND = 3,
            OTHER                     = 4

        }
        public const int FXCMPageIDNo                      = 9026;
        public const int FXCMClientExtra                   = 9027;
        public const int FXCMCommandID                     = 9028;
        public const int FXCMErrorDetails                  = 9029;

        public const int FXCMServerTimeZoneName            = 9030;
        public const int FXCMStarFXCMSessionManagerIDtTime = 9031;
        public const int FXCMTopicID1                      = 9032;
        public const int FXCMTopicID2                      = 9033;
        public const int FXCMTopicID3                      = 9034;
        public const int FXCMTopicID4                      = 9035;
        public const int FXCMEMBMSG                        = 9036;
        public const int FXCMTopicID5                      = 9037;
        public const int FXCMUsedMargin                    = 9038;

        public const int FXCMMsgID                         = 9039;
        public const int FXCMPosInterest                   = 9040;
        public const int FXCMPosID                         = 9041;
        public const int FXCMPosOpenTime                   = 9042;
        public const int FXCMCloseSettlPrice               = 9043;
        public const int FXCMPosCloseTime                  = 9044;
        public const int FXCMMarginCall                    = 9045;
        public const int FXCMUsedMargin3                   = 9046;
        public const int FXCMCashDaily                     = 9047;

        public const int FXCMCloseClOrdID                  = 9048;
        public const int FXCMCloseSecondaryClOrdID         = 9049;
        public const int FXCMOrdType                       = 9050;
        public const int FXCMOrdStatus                     = 9051;
        public const int FXCMClosePnl                      = 9052;



        public const int FXCMPosCommission                 = 9053;

        public const int FXCMCloseOrderID                  = 9054;

        public const int FXCMNoAddon                       = 9055;

        public const int FXCMNAddOnText                    = 9056;

        public const int FXCMDBAlias                       = 9057;

        public const int FXCMEntityCode                    = 9058;

        public const int FXCMResponseEncoding              = 9059;

        public const int FXCMMaxNoResults                  = 9060;

        public const int FXCMPegFluctuatePts               = 9061;

        public const int FXCMSubscriptionStatus            = 9076;

        public const int FXCMPosIDRef                      = 9078;

        public const int FXCMContingencyID                 = 9079;

        public const int FXCMProductID                     = 9080;

        public const int FXCMCondDistStop                  = 9090;

        public const int FXCMCondDistLimit                 = 9091;

        public const int FXCMCondDistEntryStop             = 9092;

        public const int FXCMCondDistEntryLimit            = 9093;

        public const int FXCMMaxQuantity                   = 9094;

        public const int FXCMMinQuantity                   = 9095;

        public const int FXCMTradingStatus                 = 9096;






        /*
<value description                                  ="OPEN" enum="O"/>

<value description                                  ="PHONE_OPEN" enum="OF"/>

<value description                                  ="OPEN_MARKET" enum="OM"/>

<value description                                  ="OPEN_LIMIT" enum="OL"/>
    
    */

    }
}
