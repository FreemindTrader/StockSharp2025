namespace StockSharp.FxConnectFXCM
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.Runtime.Serialization;

	using Ecng.Collections;

	using StockSharp.Localization;
	using StockSharp.Messages;
    public enum FxcmContingencyTypes
    {
        NA = 0,
        Oco = 1,
        Oto = 2,
        Els = 3,
        Otoco = 4,
    }
    public enum FxcmExtendedOrderTypes
	{
		/// <summary>Market Open order.</summary>
		MarketOpen,
		/// <summary>Market Open Range order.</summary>
		MarketOpenRange,
		/// <summary>True Market Close order.</summary>
		TrueMarketClose,
		/// <summary>Market Close order.</summary>
		MarketClose,
		/// <summary>Market Close Range order.</summary>
		MarketCloseRange,
		/// <summary>Stop Entry order.</summary>
		StopEntry,
		/// <summary>Limit Entry order.</summary>
		LimitEntry,
		/// <summary>Range Entry order.</summary>
		RangeEntry,
		/// <summary>Range Trailing Entry order.</summary>
		RangeTrailingEntry,
		/// <summary>Limit order.</summary>
		Limit,
		/// <summary>Stop order.</summary>
		Stop,
		/// <summary>Open Limit order.</summary>
		OpenLimit,
		/// <summary>Close Limit order.</summary>
		CloseLimit,
	}

	/// <summary>
	/// <see cref="Fxcm"/> order condition.
	/// </summary>
	[Serializable]
	[DataContract]
	[DisplayNameLoc(LocalizedStrings.Str2264Key, LocalizedStrings.FxcmKey)]
	public class FxcmOrderCondition : OrderCondition
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="FxcmOrderCondition"/>.
		/// </summary>
		public FxcmOrderCondition()
		{
		}

		/// <summary>
		/// Activation price, when reached an order will be placed.
		/// </summary>
		[DataMember]
		[Display( Description = "Str2425", GroupName = "Str225", Name = "Str2424", Order = 0, ResourceType = typeof( LocalizedStrings ) )]
		public FxcmExtendedOrderTypes? ExtendedType
		{
			get
			{
				return ( FxcmExtendedOrderTypes? ) Parameters.TryGetValue( nameof( ExtendedType ) );
			}
			set
			{
				Parameters[ nameof( ExtendedType ) ] = value;
			}
		}

		public decimal? StopLoss
		{
			get
			{
				return ( decimal? ) Parameters.TryGetValue( nameof( StopLoss ) );
			}
			set
			{
				Parameters[ nameof( StopLoss ) ] = value;
			}
		}

		public int? TrailStep
		{
			get
			{
				return ( int? ) Parameters.TryGetValue( nameof( TrailStep ) );
			}
			set
			{
				Parameters[ nameof( TrailStep ) ] = value;
			}
		}

		public decimal? TakeProfit
		{
			get
			{
				return ( decimal? ) Parameters.TryGetValue( nameof( TakeProfit ) );
			}
			set
			{
				Parameters[ nameof( TakeProfit ) ] = value;
			}
		}

		public bool? IsInPips
		{
			get
			{
				return ( bool? ) Parameters.TryGetValue( nameof( IsInPips ) );
			}
			set
			{
				Parameters[ nameof( IsInPips ) ] = value;
			}
		}

        public Decimal? RateMin
        {
            get
            {
                return ( Decimal? ) Parameters.TryGetValue( nameof( RateMin ) );
            }
            set
            {
                Parameters[ nameof( RateMin ) ] = value;
            }
        }

        public Decimal? RateMax
        {
            get
            {
                return ( Decimal? ) Parameters.TryGetValue( nameof( RateMax ) );
            }
            set
            {
                Parameters[ nameof( RateMax ) ] = value;
            }
        }

        public Decimal? AtMarket
        {
            get
            {
                return ( Decimal? ) Parameters.TryGetValue( nameof( AtMarket ) );
            }
            set
            {
                Parameters[ nameof( AtMarket ) ] = value;
            }
        }

        public FxcmContingencyTypes? ContingencyType
        {
            get
            {
                return ( FxcmContingencyTypes? ) Parameters.TryGetValue( nameof( ContingencyType ) );
            }
            set
            {
                Parameters[ nameof( ContingencyType ) ] = value;
            }
        }

        public string ContingentOrderId
        {
            get
            {
                return ( string ) Parameters.TryGetValue( nameof( ContingentOrderId ) );
            }
            set
            {
                Parameters[ nameof( ContingentOrderId ) ] = value;
            }
        }

        public string PegType
        {
            get
            {
                return ( string ) Parameters.TryGetValue( nameof( PegType ) );
            }
            set
            {
                Parameters[ nameof( PegType ) ] = value;
            }
        }

        public Decimal? PegOffset
        {
            get
            {
                return ( Decimal? ) Parameters.TryGetValue( nameof( PegOffset ) );
            }
            set
            {
                Parameters[ nameof( PegOffset ) ] = value;
            }
        }

        public Decimal? PegOffsetMin
        {
            get
            {
                return ( Decimal? ) Parameters.TryGetValue( nameof( PegOffsetMin ) );
            }
            set
            {
                Parameters[ nameof( PegOffsetMin ) ] = value;
            }
        }

        public Decimal? PegOffsetMax
        {
            get
            {
                return ( Decimal? ) Parameters.TryGetValue( nameof( PegOffsetMax ) );
            }
            set
            {
                Parameters[ nameof( PegOffsetMax ) ] = value;
            }
        }

        public Decimal? TrailRate
        {
            get
            {
                return ( Decimal? ) Parameters.TryGetValue( nameof( TrailRate ) );
            }
            set
            {
                Parameters[ nameof( TrailRate ) ] = value;
            }
        }
        

        public string Parties
        {
            get
            {
                return ( string ) Parameters.TryGetValue( nameof( Parties ) );
            }
            set
            {
                Parameters[ nameof( Parties ) ] = value;
            }
        }
    }
}
