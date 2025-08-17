using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    
    public interface IOffer
    {
        /// <summary>
        /// Gets the instrument name
        /// </summary>
        string Instrument { get; }

        /// <summary>
        /// Gets the date/time (in UTC time zone) when the offer was updated the last time
        /// </summary>
        DateTime LastUpdate { get; }

        /// <summary>
        /// Gets the latest offer bid price
        /// </summary>
        double Bid { get; }

        /// <summary>
        /// Gets the latest offer ask price
        /// </summary>
        double Ask { get; }

        /// <summary>
        /// Gets the offer accumulated last minute volume
        /// </summary>
        int MinuteVolume { get; }

        /// <summary>
        /// Gets the number of significant digits after decimal point
        /// </summary>
        int Digits { get; }

        /// <summary>
        /// Makes a copy of the offer
        /// </summary>
        /// <returns></returns>
        IOffer Clone( );

    }
}
