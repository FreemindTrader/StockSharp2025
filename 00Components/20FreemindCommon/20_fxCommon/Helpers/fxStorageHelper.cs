using Ecng.Collections;
using StockSharp.Algo.Storages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Common
{
    public static class fxStorageHelper
    {
        public static DateTime? GetToTime( this IMarketDataStorage storage )
        {
            var lastDate =  storage.Dates.LastOr( );

            if ( lastDate.HasValue )
            {
                var meta = storage.GetMetaInfo( lastDate.Value );

                if ( meta != null )
                {
                    return meta.LastTime;
                }                
            }
            
            return null;
        }

        public static DateTime? GetFromTime( this IMarketDataStorage storage )
        {
            var firstDate = storage.Dates.FirstOr( );

            if ( firstDate.HasValue )
            {
                var meta = storage.GetMetaInfo( firstDate.Value );

                if ( meta != null )
                {
                    return meta.FirstTime;
                }
            }

            return null;
        }
    }
}
