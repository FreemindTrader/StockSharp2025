using Ecng.ComponentModel;
using StockSharp.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockSharp.Xaml
{
    public class SimpleSecurityInfo : NotifiableObject
    {
        private Decimal? _priceStep;
        private Decimal? _volumeStep;
        private int? _decimals;

        public SimpleSecurityInfo( )
        {
        }

        [Display( Description = "MinPriceStep", GroupName = "General", Name = "PriceStep", Order = 0, ResourceType = typeof( LocalizedStrings ) )]
        public Decimal? PriceStep
        {
            get
            {
                return _priceStep;
            }
            set
            {
                if ( _priceStep == value )
                    return;

                if ( value < 0 )
                    throw new ArgumentOutOfRangeException( nameof( value ) );

                _priceStep = value;
                NotifyChanged( nameof( PriceStep ) );
            }
        }

        [Display( Description = "Str366", GroupName = "General", Name = "VolumeStep", Order = 1, ResourceType = typeof( LocalizedStrings ) )]
        public Decimal? VolumeStep
        {
            get
            {
                return _volumeStep;
            }
            set
            {
                if ( _volumeStep == value )
                    return;

                if ( value < 0 )
                    throw new ArgumentOutOfRangeException( nameof( value ) );

                _volumeStep = value;
                
                NotifyChanged( nameof( VolumeStep ) );
            }
        }

        [Display( Description = "Str548", GroupName = "General", Name = "Decimals", Order = 2, ResourceType = typeof( LocalizedStrings ) )]
        public int? Decimals
        {
            get
            {
                return _decimals;
            }
            set
            {
                if ( _decimals == value )
                    return;

                if ( value < 0 )
                    throw new ArgumentOutOfRangeException( nameof( value ) );

                _decimals = value;

                NotifyChanged( nameof( Decimals ) );
            }
        }
    }
}
