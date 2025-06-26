using fx.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace fx.Definitions
{
    public enum BBANDSStates
    {
        SuperStrongSell = -30,
        StrongSell      = -20,
        Sell            = -10,
        Default         = 0,
        Buy             = 10,
        StrongBuy       = 20,
        SuperStrongBuy  = 30,
    }
    

    /// <summary>
    /// This extension method will provide a string representation for the different timeframes in forex readable format    
    /// </summary>
    public static class MyExtensionHelper
    {
        public static bool IsBetterThan( this ElliottWaveEnum newWave, ElliottWaveEnum oldWave )
        {
            switch ( newWave )
            {
                case ElliottWaveEnum.Wave5C:
                case ElliottWaveEnum.Wave3C:
                case ElliottWaveEnum.Wave1C:
                    {
                        if ( oldWave == ElliottWaveEnum.WaveC )
                        {
                            return true;
                        }
                    }
                    break;

            }

            return false;
        }

        public static bool isBuy( this BBANDSStates signal )
        {
            return ( ( signal == BBANDSStates.Buy ) || ( signal == BBANDSStates.StrongBuy ) || ( signal == BBANDSStates.SuperStrongBuy ) );
        }

        public static bool isSell( this BBANDSStates signal )
        {
            return ( ( signal == BBANDSStates.Sell ) || ( signal == BBANDSStates.StrongSell ) || ( signal == BBANDSStates.SuperStrongSell ) );
        }

        public static int ToColor( this BBANDSStates bbState )
        {
            int color = 13;

            switch ( bbState )
            {
                case BBANDSStates.SuperStrongSell:
                    color = 10;
                    break;
                case BBANDSStates.StrongSell:
                    color = 8;
                    break;
                case BBANDSStates.Sell:
                    color = 6;
                    break;
                case BBANDSStates.Default:
                    color = 13;
                    break;

                case BBANDSStates.Buy:
                    color = 5;
                    break;
                case BBANDSStates.StrongBuy:
                    color = 3;
                    break;
                case BBANDSStates.SuperStrongBuy:
                    color = 1;
                    break;
            }

            return color;
        }
        public static string ToReadable( this TimeSpan input )
        {
            if ( input == TimeSpan.FromDays( 30 ) )
            {
                return "Monthly";
            }
            else if ( input == TimeSpan.FromDays( 7 ) )
            {
                return "Weekly";
            }
            else if ( input == TimeSpan.FromDays( 1 ) )
            {
                return "Daily";
            }
            else if ( input == TimeSpan.FromHours( 8 ) )
            {
                return "8 Hr";
            }
            else if ( input == TimeSpan.FromHours( 6 ) )
            {
                return "6 Hr";
            }
            else if ( input == TimeSpan.FromHours( 4 ) )
            {
                return "4 Hr";
            }
            else if ( input == TimeSpan.FromHours( 3 ) )
            {
                return "3 Hr";
            }
            else if ( input == TimeSpan.FromHours( 2 ) )
            {
                return "2 Hr";
            }
            else if ( input == TimeSpan.FromHours( 1 ) )
            {
                return "1 Hr";
            }
            else if ( input == TimeSpan.FromMinutes( 60 ) )
            {
                return "60 Min";
            }
            else if ( input == TimeSpan.FromMinutes( 30 ) )
            {
                return "30 Min";
            }
            else if ( input == TimeSpan.FromMinutes( 15 ) )
            {
                return "15 Min";
            }
            else if ( input == TimeSpan.FromMinutes( 5 ) )
            {
                return "5 Min";
            }
            else if ( input == TimeSpan.FromMinutes( 4 ) )
            {
                return "4 Min";
            }
            else if ( input == TimeSpan.FromMinutes( 1 ) )
            {
                return "1 Min";
            }
            else if ( input == TimeSpan.FromSeconds( 1 ) )
            {
                return "1 Sec";
            }
            else if ( input == TimeSpan.FromTicks( 1 ) )
            {
                return "Tick";
            }

            return ( "NAN" );
        }

        public static string ToShortForm( this TimeSpan period )
        {
            string sPeriodId = "t1";

            if ( period.Days == 30 )
            {
                sPeriodId = "M1";
            }
            else if ( period.Days == 7 )
            {
                sPeriodId = "W1";
            }
            else if ( period.Days == 1 )
            {
                sPeriodId = "D1";
            }
            else if ( period.Hours == 1 )
            {
                sPeriodId = "H1";
            }
            else if ( period.Hours == 2 )
            {
                sPeriodId = "H2";
            }
            else if ( period.Hours == 3 )
            {
                sPeriodId = "H3";
            }
            else if ( period.Hours == 4 )
            {
                sPeriodId = "H4";
            }
            else if ( period.Hours == 6 )
            {
                sPeriodId = "H6";
            }
            else if ( period.Hours == 8 )
            {
                sPeriodId = "H8";
            }
            else if ( period.Minutes == 1 )
            {
                sPeriodId = "m1";
            }
            else if ( period.Minutes == 4 )
            {
                sPeriodId = "m4";
            }
            else if ( period.Minutes == 5 )
            {
                sPeriodId = "m5";
            }
            else if ( period.Minutes == 15 )
            {
                sPeriodId = "m15";
            }
            else if ( period.Minutes == 30 )
            {
                sPeriodId = "m30";
            }
            else if ( period.Seconds == 1 )
            {
                sPeriodId = "s1";
            }
            else if ( period.Ticks == 1 )
            {
                sPeriodId = "t1";
            }

            return sPeriodId;
        }

        public static bool IsPivotEnum( this SR3rdType input )
        {
            if ( input > SR3rdType.NONE && input <= SR3rdType.MDN )
                return true;

            return false;
        }

        public static bool IsMovingAvg( this SR3rdType input )
        {
            if ( input > SR3rdType.SMA50 && input <= SR3rdType.SMA233 )
                return true;

            return false;
        }

        public static bool IsSupport( this SR3rdType input )
        {
            if ( input.IsPivotEnum( ) )
            {
                if ( input == SR3rdType.S1 || input == SR3rdType.S2 || input == SR3rdType.S3 )
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsHalfSupport( this SR3rdType input )
        {
            if ( input.IsPivotEnum( ) )
            {
                if ( input == SR3rdType.M0 || input == SR3rdType.M1 || input == SR3rdType.M2 )
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsResistance( this SR3rdType input )
        {
            if ( input.IsPivotEnum( ) )
            {
                if ( input == SR3rdType.R1 || input == SR3rdType.R2 || input == SR3rdType.R3 )
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsHalfResistance( this SR3rdType input )
        {
            if ( input.IsPivotEnum( ) )
            {
                if ( input == SR3rdType.M3 || input == SR3rdType.M4 || input == SR3rdType.M5 )
                {
                    return true;
                }
            }

            return false;
        }


        public static bool IsWaveEnum( this SR3rdType input )
        {
            if ( input >= SR3rdType.WaveCProjection && input <= SR3rdType.SecondXProjection )
                return true;

            return false;
        }

        public static SR1stType Sr1st( this TimeSpan input )
        {
            if ( input == TimeSpan.FromDays( 1 ) )
            {
                return SR1stType.DailyPP;
            }
            else if ( input == TimeSpan.FromDays( 7 ) )
            {
                return SR1stType.WeekPP;
            }
            else if ( input == TimeSpan.FromDays( 30 ) )
            {
                return SR1stType.MonthPP;
            }

            return SR1stType.Unknown;
        }

        public static SR2ndType SmaToSr2nd( this TimeSpan input )
        {
            if ( input == TimeSpan.FromMinutes( 1 ) )
            {
                return SR2ndType.SMA01;
            }
            else if ( input == TimeSpan.FromMinutes( 5 ) )
            {
                return SR2ndType.SMA05;
            }
            else if ( input == TimeSpan.FromMinutes( 15 ) )
            {
                return SR2ndType.SMA15;
            }
            else if ( input == TimeSpan.FromMinutes( 30 ) )
            {
                return SR2ndType.SMA30;
            }
            else if ( input == TimeSpan.FromHours( 1 ) )
            {
                return SR2ndType.SMA1H;
            }
            else if ( input == TimeSpan.FromHours( 2 ) )
            {
                return SR2ndType.SMA2H;
            }
            else if ( input == TimeSpan.FromHours( 4 ) )
            {
                return SR2ndType.SMA4H;
            }
            else if ( input == TimeSpan.FromDays( 1 ) )
            {
                return SR2ndType.SMA1D;
            }
            else if ( input == TimeSpan.FromDays( 7 ) )
            {
                return SR2ndType.SMA7D;
            }
            else if ( input == TimeSpan.FromDays( 30 ) )
            {
                return SR2ndType.SMA1M;
            }

            return SR2ndType.NONE;
        }

        public static SR2ndType Sr2nd( this SR3rdType input )
        {
            if( input.IsPivotEnum( ) )
            {
                if( input == SR3rdType.PIVOT )
                {
                    return SR2ndType.Mean;
                }
                else if( input.IsSupport( ) )
                {
                    return SR2ndType.Support;
                }
                else if( input.IsResistance( ) )
                {
                    return SR2ndType.Resistance;
                }
                else if( input.IsHalfSupport( ) )
                {
                    return SR2ndType.HalfSupport;
                }
                else if( input.IsHalfResistance( ) )
                {
                    return SR2ndType.HalfResistance;
                }
                else if( input == SR3rdType.MDN )
                {
                    return SR2ndType.DirectionNo;
                }
            }
            

            return SR2ndType.NONE;
        }

        public static BarPeriod ToBarPeriod( this TimeSpan period )
        {
            BarPeriod sPeriodId = BarPeriod.t1;

            if ( period.Days == 30 )
            {
                sPeriodId = BarPeriod.M1;
            }
            else if ( period.Days == 7 )
            {
                sPeriodId = BarPeriod.W1;
            }
            else if ( period.Days == 1 )
            {
                sPeriodId = BarPeriod.D1;
            }
            else if ( period.Hours == 1 )
            {
                sPeriodId = BarPeriod.H1;
            }
            else if ( period.Hours == 2 )
            {
                sPeriodId = BarPeriod.H2;
            }
            else if ( period.Hours == 3 )
            {
                sPeriodId = BarPeriod.H3;
            }
            else if ( period.Hours == 4 )
            {
                sPeriodId = BarPeriod.H4;
            }
            else if ( period.Minutes == 1 )
            {
                sPeriodId = BarPeriod.m1;
            }
            else if ( period.Minutes == 4 )
            {
                sPeriodId = BarPeriod.m4;
            }
            else if ( period.Minutes == 5 )
            {
                sPeriodId = BarPeriod.m5;
            }
            else if ( period.Minutes == 15 )
            {
                sPeriodId = BarPeriod.m15;
            }
            else if ( period.Minutes == 30 )
            {
                sPeriodId = BarPeriod.m30;
            }
            else if ( period.Ticks == 1 )
            {
                sPeriodId = BarPeriod.t1;
            }

            return sPeriodId;
        }

        public static TimeSpan ToTimeSpan( this BarPeriod period )
        {
            TimeSpan output = TimeSpan.FromTicks( 1 );
            if ( period == BarPeriod.t1 )
            {
                output = TimeSpan.FromTicks( 1 );
            }
            else if ( period == BarPeriod.m1 )
            {
                output = TimeSpan.FromMinutes( 1 );
            }
            else if ( period == BarPeriod.m4 )
            {
                output = TimeSpan.FromMinutes( 4 );
            }
            else if ( period == BarPeriod.m5 )
            {
                output = TimeSpan.FromMinutes( 5 );
            }
            else if ( period == BarPeriod.m15 )
            {
                output = TimeSpan.FromMinutes( 15 );
            }
            else if ( period == BarPeriod.m30 )
            {
                output = TimeSpan.FromMinutes( 30 );
            }
            else if ( period == BarPeriod.H1 )
            {
                output = TimeSpan.FromHours( 1 );
            }
            else if ( period == BarPeriod.H2 )
            {
                output = TimeSpan.FromHours( 2 );
            }
            else if ( period == BarPeriod.H3 )
            {
                output = TimeSpan.FromHours( 3 );
            }
            else if ( period == BarPeriod.H4 )
            {
                output = TimeSpan.FromHours( 4 );
            }
            else if ( period == BarPeriod.H6 )
            {
                output = TimeSpan.FromHours( 6 );
            }
            else if ( period == BarPeriod.H8 )
            {
                output = TimeSpan.FromHours( 8 );
            }
            else if ( period == BarPeriod.D1 )
            {
                output = TimeSpan.FromDays( 1 );
            }
            else if ( period == BarPeriod.W1 )
            {
                output = TimeSpan.FromDays( 7 );
            }
            else if ( period == BarPeriod.M1 )
            {
                output = TimeSpan.FromDays( 30 );
            }

            return output;
        }
    }    
}