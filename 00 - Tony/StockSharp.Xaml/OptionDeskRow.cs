using Ecng.Common;
using Ecng.ComponentModel;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StockSharp.Xaml
{
    [DisplayNameLoc( "Str1525" )]
    public class OptionDeskRow : NotifiableObject
    {        
        public OptionDeskRow( Security underlyingAsset, Security call, Security put )
        {
            if ( underlyingAsset == null )
            {
                throw new ArgumentNullException( nameof( underlyingAsset ) );
            }

            if ( call == null && put == null )
            {
                throw new ArgumentException( LocalizedStrings.Str1530 );
            }

            if ( call != null )
            {
                Call = new OptionDeskRow.OptionDeskRowSide( this, call );
            }

            if ( put != null )
            {
                Put = new OptionDeskRow.OptionDeskRowSide( this, put );
            }

            Strike = ( call ?? put ).Strike;
            UnderlyingAsset = underlyingAsset;
        }

        //[Display( Description = "OptionStrikePrice", GroupName = "Options", Name = "Strike", Order = 1, ResourceType = typeof( LocalizedStrings ) )]
        /// <summary>
		/// Option strike price.
		/// </summary>
		[DisplayNameLoc( LocalizedStrings.StrikeKey )]
        [DescriptionLoc( LocalizedStrings.OptionStrikePriceKey )]
        [CategoryLoc( LocalizedStrings.OptionsKey )]
        public Decimal? Strike { get; private set; }
        

        //[Display( Description = "Str1528", GroupName = "Options", Name = "Str1527", Order = 2, ResourceType = typeof( LocalizedStrings ) )]
        [DisplayNameLoc( LocalizedStrings.Str1527Key )]
        [DescriptionLoc( LocalizedStrings.Str1528Key )]
        [CategoryLoc( LocalizedStrings.OptionsKey )]
        public Decimal? PnL
        {
            get
            {
                decimal? pnl = null;

                if ( Call != null )
                    pnl = Call.PnL * ( Call.OpenInterest ?? 0 ).Max( Call.Option.VolumeStep ?? 1 );

                if ( Put != null )
                {
                    var putPnL = Put.PnL * (Put.OpenInterest ?? 0).Max(Put.Option.VolumeStep ?? 1);

                    if ( pnl < putPnL )
                        pnl = putPnL;
                }

                return pnl;
            }
        }

        [Browsable( false )]
        public Decimal MaxImpliedVolatilityBestBid { get; set; }
        
        [Browsable( false )]
        public Decimal MaxImpliedVolatilityBestAsk { get; set; }
        

        [Browsable( false )]
        public Decimal MaxImpliedVolatilityLastTrade { get; set; }

        [Browsable( false )]
        public Decimal MaxHistoricalVolatility { get; set; }

        [Browsable( false )]
        public Decimal MaxPnL { get; set; }


        //[Display( Description = "Str1533", GroupName = "Options", Name = "UnderlyingAsset", Order = 3, ResourceType = typeof( LocalizedStrings ) )]
        //[ReadOnly( true )]

        /// <summary>
		/// Information about the underlying asset.
		/// </summary>
		[DisplayNameLoc( LocalizedStrings.UnderlyingAssetKey )]
        [DescriptionLoc( LocalizedStrings.Str1533Key )]
        [CategoryLoc( LocalizedStrings.OptionsKey )]
        [ReadOnly( true )]
        public Security UnderlyingAsset { get; private set; }
        
        [DisplayName( LocalizedStrings.Str223Key )]
        [DescriptionLoc( LocalizedStrings.Str1534Key )]
        [CategoryLoc( LocalizedStrings.OptionsKey )]
        public OptionDeskRow.OptionDeskRowSide Call { get; }
        

        [DisplayName( LocalizedStrings.Str224Key )]
        [DescriptionLoc( LocalizedStrings.Str1535Key )]
        [CategoryLoc( LocalizedStrings.OptionsKey )]
        public OptionDeskRowSide Put { get; }

        [TypeConverter( typeof( ExpandableObjectConverter ) )]
        public sealed class OptionDeskRowSide
        {
            private readonly OptionDeskRow _desk;
            private readonly Security _security;


            internal OptionDeskRowSide( OptionDeskRow row, Security security )
            {

                if ( row == null )
                {
                    throw new ArgumentNullException( "row" );
                }

                _desk = row;

                if ( security == null )
                {
                    throw new ArgumentNullException( "option" );
                }

                _security = security;
            }

            public OptionDeskRow Row
            {
                get
                {
                    return _desk;
                }
            }

            /// <summary>
			/// The best buy price.
			/// </summary>
            public decimal? BestBidPrice { get; set; }

            /// <summary>
			/// The best buy price.
			/// </summary>
			public decimal? BestAskPrice { get; set; }

            /// <summary>
			/// Volume per session.
			/// </summary>
			public decimal? Volume { get; set; }

            /// <summary>
			/// Theoretical price.
			/// </summary>
			public decimal? TheorPrice { get; set; }

            /// <summary>
			/// Reserved.
			/// </summary>
			[Browsable( false )]
            public decimal MaxOpenInterest { get; set; }

            /// <summary>
			/// Information about the option.
			/// </summary>
			[ReadOnly( true )]
            [DisplayNameLoc( LocalizedStrings.OptionsContractKey )]
            [DescriptionLoc( LocalizedStrings.Str1526Key )]
            public Security Option { get; }

            /// <summary>
			/// Option delta.
			/// </summary>
			[DisplayNameLoc( LocalizedStrings.DeltaKey )]
            [DescriptionLoc( LocalizedStrings.OptionDeltaKey )]
            public decimal? Delta { get; set; }


            /// <summary>
			/// Option gamma.
			/// </summary>
			[DisplayNameLoc( LocalizedStrings.GammaKey )]
            [DescriptionLoc( LocalizedStrings.OptionGammaKey )]
            public decimal? Gamma { get; set; }

            /// <summary>
			/// Option theta.
			/// </summary>
			[DisplayNameLoc( LocalizedStrings.ThetaKey )]
            [DescriptionLoc( LocalizedStrings.OptionThetaKey )]
            public decimal? Theta { get; set; }

            /// <summary>
			/// Option vega.
			/// </summary>
			[DisplayNameLoc( LocalizedStrings.VegaKey )]
            [DescriptionLoc( LocalizedStrings.OptionVegaKey )]
            public decimal? Vega { get; set; }

            /// <summary>
			/// Option rho.
			/// </summary>
			[DisplayNameLoc( LocalizedStrings.RhoKey )]
            [DescriptionLoc( LocalizedStrings.OptionRhoKey )]
            public decimal? Rho { get; set; }

            /// <summary>
			/// Open interest.
			/// </summary>
			[DisplayNameLoc( LocalizedStrings.OIKey )]
            [DescriptionLoc( LocalizedStrings.OpenInterestKey )]
            public decimal? OpenInterest { get; set; }

            /// <summary>
			/// Profitability of an option contract.
			/// </summary>
			[DisplayNameLoc( LocalizedStrings.Str1527Key )]
            [DescriptionLoc( LocalizedStrings.Str1528Key )]
            [CategoryLoc( LocalizedStrings.OptionsKey )]
            public decimal? PnL { get; set; }

            /// <summary>
			/// Reserved.
			/// </summary>
			[Browsable( false )]
            public decimal MaxVolume { get; set; }


            [DisplayNameLoc( LocalizedStrings.Str293Key )]
            [DescriptionLoc( LocalizedStrings.Str1531Key )]
            [CategoryLoc( LocalizedStrings.OptionsKey )]
            public Decimal? ImpliedVolatilityBestBid { get; set; }



            [DisplayNameLoc( LocalizedStrings.Str293Key )]
            [DescriptionLoc( LocalizedStrings.Str1531Key )]
            [CategoryLoc( LocalizedStrings.OptionsKey )]
            public Decimal? ImpliedVolatilityBestAsk { get; set; }


            [DisplayNameLoc( LocalizedStrings.Str293Key )]
            [DescriptionLoc( LocalizedStrings.Str1531Key )]
            [CategoryLoc( LocalizedStrings.OptionsKey )]
            public Decimal? ImpliedVolatilityLastTrade { get; set; }


            [DisplayNameLoc( LocalizedStrings.Str299Key )]
            [DescriptionLoc( LocalizedStrings.Str1532Key )]
            [CategoryLoc( LocalizedStrings.OptionsKey )]
            //[Display( Description = "Str1532", GroupName = "Options", Name = "Str299", Order = 103, ResourceType = typeof( LocalizedStrings ) )]
            public Decimal? HistoricalVolatility { get; set; }


            public int? DaysToExpire
            {
                get
                {
                    DateTimeOffset? expiryDate = Option.ExpiryDate;
                    if ( !expiryDate.HasValue )
                    {
                        return new int?();
                    }

                    int num = (int) (expiryDate.Value - DateTimeOffset.Now).TotalDays;
                    if ( num < 0 )
                    {
                        num = 0;
                    }

                    return new int?( num );
                }
            }

            public override string ToString( )
            {
                return string.Empty;
            }

            public Level1ChangeMessage ToLevel1( )
            {
                var level1Msg = new Level1ChangeMessage();
                level1Msg.SecurityId = Option.ToSecurityId( null );

                var partialResult = (((((((( level1Msg.TryAdd( Level1Fields.BestAskPrice, false ) ).TryAdd( Level1Fields.BestBidPrice, false ) ).TryAdd( Level1Fields.Delta, false ) ).TryAdd( Level1Fields.Gamma, false ) ).TryAdd( Level1Fields.Vega, false ) ).TryAdd( Level1Fields.Theta, false ) ).TryAdd( Level1Fields.Rho, false ) ).TryAdd( Level1Fields.HistoricalVolatility, false ) );                

                Decimal? volatilityLastTrade = ImpliedVolatilityLastTrade;
                Decimal? someVolatilityResult;

                if ( !volatilityLastTrade.HasValue )
                {
                    Decimal? volatilityBestBid = ImpliedVolatilityBestBid;
                    someVolatilityResult = volatilityBestBid.HasValue ? volatilityBestBid : ImpliedVolatilityBestAsk;
                }
                else
                {
                    someVolatilityResult = volatilityLastTrade;
                }

                level1Msg = ( ( ( partialResult.TryAdd( Level1Fields.ImpliedVolatility, someVolatilityResult, false ) ).TryAdd( Level1Fields.OpenInterest, false ) ).TryAdd( Level1Fields.TheorPrice, false ) ).TryAdd( Level1Fields.Volume, false );
                
                return level1Msg;
            }
        }
    }
}
