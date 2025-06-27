namespace StockSharp.Algo.Indicators
{
	using System.ComponentModel;
	using StockSharp.Algo.Candles;
	using StockSharp.Algo.Indicators;
	using StockSharp.Localization;

	/// <summary>
	/// Relative Strength Index.
	/// </summary>
	[DisplayName( "ConstantLineIndicator" )]
	
	public class ConstantLineIndicator : LengthIndicator<decimal>
	{
		private decimal _constLine;
		
		/// <summary>
		/// Initializes a new instance of the <see cref="ConstantLineIndicator"/>.
		/// </summary>
		public ConstantLineIndicator( decimal constLine )
		{
			_constLine = constLine;
		}

		/// <inheritdoc />
		//public override bool IsFormed => true;

		/// <inheritdoc />
		public override void Reset()
		{
			base.Reset();
		}

		/// <inheritdoc />
		protected override IIndicatorValue OnProcess( IIndicatorValue input )
		{
			return new DecimalIndicatorValue( this, _constLine, input.Time );
		}
	}
}

