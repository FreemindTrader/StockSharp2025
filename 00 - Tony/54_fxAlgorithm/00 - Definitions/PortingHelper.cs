using fx.Definitions;
using StockSharp.BusinessEntities;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Algorithm
{
    static class PortingHelper
    {
        public static BarPeriod ToBarPeriod( this TimeSpan period )
        {
            if( period == TimeSpan.FromTicks( 1 ) )
            {
                return BarPeriod.t1;
            }
            else if( period == TimeSpan.FromMinutes( 1 ) )
            {
                return BarPeriod.m1;
            }
            else if( period == TimeSpan.FromMinutes( 4 ) )
            {
                return BarPeriod.m4;
            }
            else if( period == TimeSpan.FromMinutes( 5 ) )
            {
                return BarPeriod.m5;
            }
            else if( period == TimeSpan.FromMinutes( 15 ) )
            {
                return BarPeriod.m15;
            }
            else if( period == TimeSpan.FromMinutes( 30 ) )
            {
                return BarPeriod.m30;
            }
            else if( period == TimeSpan.FromHours( 1 ) )
            {
                return BarPeriod.H1;
            }
            else if( period == TimeSpan.FromHours( 2 ) )
            {
                return BarPeriod.H2;
            }
            else if( period == TimeSpan.FromHours( 3 ) )
            {
                return BarPeriod.H3;
            }
            else if( period == TimeSpan.FromHours( 4 ) )
            {
                return BarPeriod.H4;
            }
            else if( period == TimeSpan.FromHours( 6 ) )
            {
                return BarPeriod.H6;
            }
            else if( period == TimeSpan.FromHours( 8 ) )
            {
                return BarPeriod.H8;
            }
            else if( period == TimeSpan.FromDays( 1 ) )
            {
                return BarPeriod.D1;
            }
            else if( period == TimeSpan.FromDays( 7 ) )
            {
                return BarPeriod.W1;
            }
            else if( period == TimeSpan.FromDays( 30 ) )
            {
                return BarPeriod.M1;
            }

            return BarPeriod.NA;
        }
    }
}
