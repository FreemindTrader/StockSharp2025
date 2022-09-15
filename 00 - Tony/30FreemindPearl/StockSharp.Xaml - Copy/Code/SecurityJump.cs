using Ecng.ComponentModel;
using StockSharp.BusinessEntities;
using System;

namespace StockSharp.Xaml
{
    public sealed class SecurityJump : NotifiableObject
    {
        private Security _security;

        /// <summary>
        /// Security.
        /// </summary>
        public Security Security
        {
            get { return _security; }
            set
            {
                if ( Security == value )
                    return;

                _security = value;
                this.NotifyChanged( nameof( Security ) );

                if( _security == null || ! _security.ExpiryDate.HasValue )
                    return;

                Date = _security.ExpiryDate.Value.UtcDateTime;
            }
        }

        private DateTime _date;

        /// <summary>
        /// Rollover date.
        /// </summary>
        public DateTime Date
        {
            get { return _date; }
            set
            {
                if ( Date == value )
                    return;

                _date = value;

                this.NotifyChanged( nameof( Date ) );
            }
        }        
    }

}


