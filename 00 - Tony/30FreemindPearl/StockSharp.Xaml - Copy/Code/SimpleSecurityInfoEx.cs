using Ecng.ComponentModel;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockSharp.Xaml
{
    public sealed class SimpleSecurityInfoEx : SimpleSecurityInfo
    {
        private Unit _volumeLevel = new Unit();
        private bool _isOpenInterest;

        [Display( Description = "OpenInterest", GroupName = "ContinuousSecurity", Name = "OI", Order = 1, ResourceType = typeof( LocalizedStrings ) )]
        public bool IsOpenInterest
        {
            get
            {
                return this._isOpenInterest;
            }
            set
            {
                this._isOpenInterest = value;
            }
        }

        [Display( Description = "VolumeTrigger", GroupName = "ContinuousSecurity", Name = "Volume", Order = 0, ResourceType = typeof( LocalizedStrings ) )]
        public Unit VolumeLevel
        {
            get
            {
                return this._volumeLevel;
            }
            set
            {
                Unit unit = value;
                if ( unit == null )
                {
                    throw new ArgumentNullException( nameof( value ) );
                }

                this._volumeLevel = unit;
            }
        }
    }
}

