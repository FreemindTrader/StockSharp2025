﻿namespace StockSharp.Algo.Indicators
{
	using System.ComponentModel;
	using StockSharp.Algo.Candles;
    using StockSharp.Algo.Indicators;
    using StockSharp.Localization;

	/// <summary>
	/// Relative Strength Index.
	/// </summary>
	[DisplayName( "HewRSI" )]
	[DescriptionLoc( LocalizedStrings.Str770Key )]
	public class fxHewRSI : LengthIndicator<decimal>
	{
		private readonly SmoothedMovingAverage _gain;
		private readonly SmoothedMovingAverage _loss;
		private bool _isInitialized;
		private decimal _lastHigh;
		private decimal _lastLow;

		/// <summary>
		/// Initializes a new instance of the <see cref="HewRSI"/>.
		/// </summary>
		public fxHewRSI()
		{
			_gain = new SmoothedMovingAverage();
			_loss = new SmoothedMovingAverage();

			Length = 15;
		}

		/// <inheritdoc />
		public override bool IsFormed => _gain.IsFormed;

		/// <inheritdoc />
		public override void Reset()
		{
			_loss.Length = _gain.Length = Length;
			base.Reset();
		}

		/// <inheritdoc />
		protected override IIndicatorValue OnProcess( IIndicatorValue input )
		{
			var currHigh = input.GetValue<Candle>().HighPrice;
			var currLow  = input.GetValue<Candle>().LowPrice;

			if ( !_isInitialized )
			{
				if ( input.IsFinal )
				{
					_lastHigh = currHigh;
					_lastLow = currLow;
					_isInitialized = true;
				}

				return new DecimalIndicatorValue( this );
			}

			var highDiff = currHigh - _lastHigh;
			var lowDiff  = _lastLow - currLow;

			_lastHigh = currHigh;
			_lastLow = currLow;

			decimal gainValue = 0;
			decimal lossValue = 0;


			if ( ( highDiff > 0 ) && ( highDiff > lowDiff ) )
			{
				gainValue = _gain.Process( input.SetValue( this, highDiff ) ).GetValue<decimal>();
			}
			else
			{
				gainValue = _gain.Process( input.SetValue( this, 0 ) ).GetValue<decimal>();
			}

			if ( ( lowDiff > 0 ) && ( lowDiff > highDiff ) )
			{
				lossValue = _loss.Process( input.SetValue( this, lowDiff ) ).GetValue<decimal>();
			}
			else
			{
				lossValue = _loss.Process( input.SetValue( this, 0 ) ).GetValue<decimal>();
			}

			decimal hewRsi = 0;

			if ( gainValue + lossValue != 0 )
			{
				hewRsi = 100 * ( gainValue / ( gainValue + lossValue ) );
			}

			return new DecimalIndicatorValue( this, hewRsi );
		}
	}
}