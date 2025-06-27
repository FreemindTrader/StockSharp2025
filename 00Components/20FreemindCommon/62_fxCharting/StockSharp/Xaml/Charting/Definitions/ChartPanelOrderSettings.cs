using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Configuration;
using Ecng.Serialization;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using System;
using System.ComponentModel.DataAnnotations;

namespace fx.Charting
{
    public class ChartPanelOrderSettings : NotifiableObject, IPersistable
    {
        private Decimal valueOne = Decimal.One;
        private Security security_0;
        private Portfolio portfolio_0;

        [Display( Description = "Str1996", GroupName = "General", Name = "Security", Order = 1, ResourceType = typeof( LocalizedStrings ) )]
        public Security Security
        {
            get
            {
                return security_0;
            }
            set
            {
                if( security_0 == value )
                {
                    return;
                }
                security_0 = value;
                NotifyChanged( nameof( Security ) );
            }
        }

        [Display( Description = "Str1997", GroupName = "General", Name = "Portfolio", Order = 2, ResourceType = typeof( LocalizedStrings ) )]
        public Portfolio Portfolio
        {
            get
            {
                return portfolio_0;
            }
            set
            {
                if( portfolio_0 == value )
                {
                    return;
                }
                portfolio_0 = value;
                NotifyChanged( nameof( Portfolio ) );
            }
        }

        [Display( Description = "OrderVolume", GroupName = "General", Name = "Volume", Order = 3, ResourceType = typeof( LocalizedStrings ) )]
        public Decimal Volume
        {
            get
            {
                return valueOne;
            }
            set
            {
                if( value <= Decimal.Zero )
                {
                    throw new ArgumentOutOfRangeException( );
                }
                valueOne = value;
                NotifyChanged( nameof( Volume ) );
            }
        }

        public void Load( SettingsStorage storage )
        {
            ISecurityProvider securityProvider = ServicesRegistry.TrySecurityProvider;
            if( securityProvider != null )
            {
                string str = storage.GetValue( "Security", ( string )null );
                if( !str.IsEmpty( ) )
                {
                    Security = securityProvider.LookupById( str );
                }
            }
            IPortfolioProvider portfolioProvider = ServicesRegistry.TryPortfolioProvider;
            if ( portfolioProvider != null )
            {
                string str = storage.GetValue( "Portfolio", ( string )null );
                if( !str.IsEmpty( ) )
                {
                    Portfolio = portfolioProvider.LookupByPortfolioName( str );
                }
            }
            Decimal one = storage.GetValue( "Volume", Decimal.One );
            if( one <= Decimal.Zero )
            {
                one = Decimal.One;
            }
            Volume = one;
        }

        public void Save( SettingsStorage storage )
        {
            storage.SetValue( "Security", Security?.Id );
            storage.SetValue( "Portfolio", Portfolio?.Name );
            storage.SetValue( "Volume", Volume );
        }
    }
}
