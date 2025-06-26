using Ecng.Common;
using SciChart.Charting.Model;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Indicators;
using StockSharp.BusinessEntities;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Bars
{
    public static class BarIndicatorHelper
    {
		/// <summary>
		/// To renew the indicator with candle closing price <see cref="Candle.ClosePrice"/>.
		/// </summary>
		/// <param name="indicator">Indicator.</param>
		/// <param name="bar">Candle.</param>
		/// <returns>The new value of the indicator.</returns>
		public static IIndicatorValue Process( this IIndicator indicator, ref SBar bar )
		{
			return indicator.Process( new CandleIndicatorValue( indicator, ref bar ) );
		}

		public static ICandleMessage ToCandle( this SBar bar )
        {
            var output = new TimeFrameCandleMessage
            {
                TypedArg = bar.SymbolEx.Period,                
            };
            
			output.HighPrice  = ( decimal ) bar.HighPrice;
			output.LowPrice   = ( decimal ) bar.LowPrice;
			output.OpenPrice  = ( decimal ) bar.OpenPrice;
			output.ClosePrice = ( decimal ) bar.ClosePrice;

			return output;
        }
	}

	public class CandleIndicatorValue : SingleIndicatorValue<SBar>
	{
		private readonly Func<SBar, float> _getPart;

		/// <summary>
		/// Initializes a new instance of the <see cref="CandleIndicatorValue"/>.
		/// </summary>
		/// <param name="indicator">Indicator.</param>
		/// <param name="value">Value.</param>
		public CandleIndicatorValue( IIndicator indicator, ref SBar value )
			: this( indicator, ref value, ByClose )
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="CandleIndicatorValue"/>.
        /// </summary>
        /// <param name="indicator">Indicator.</param>
        /// <param name="value">Value.</param>
        /// <param name="getPart">The candle converter, through which its parameter can be got. By default, the <see cref="ByClose"/> is used.</param>
        public CandleIndicatorValue( IIndicator indicator, ref SBar value, Func<SBar, float> getPart )
			: base( indicator, value, value.ServerTime() )
		{			
			_getPart = getPart ?? throw new ArgumentNullException( nameof( getPart ) );

			IsFinal = true;
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="CandleIndicatorValue"/>.
        /// </summary>
        /// <param name="indicator">Indicator.</param>
        /// <param name="time"><see cref="IIndicatorValue.Time"/></param>
        public CandleIndicatorValue( IIndicator indicator, DateTimeOffset time )
            : base( indicator, time )
        {
        }

        /// <summary>
        /// The converter, taking from candle closing price <see cref="Candle.ClosePrice"/>.
        /// </summary>
        public static readonly Func<SBar, float> ByClose = c => c.Close;

		/// <summary>
		/// The converter, taking from candle opening price <see cref="Candle.OpenPrice"/>.
		/// </summary>
		public static readonly Func<SBar, float> ByOpen = c => c.Open;

		/// <summary>
		/// The converter, taking from candle middle of the body (<see cref="Candle.OpenPrice"/> + <see cref="Candle.ClosePrice"/>) / 2.
		/// </summary>
		public static readonly Func<SBar, float> ByMiddle = c => (c.Close + c.Open) / 2;

		/// <inheritdoc />
		//public override bool IsSupport( Type valueType ) => valueType == typeof( decimal ) || base.IsSupport( valueType );

		///// <inheritdoc />
		//public override T GetValue<T>()
		//{
		//	var candle = base.GetValue<SBar>();

		//	if ( typeof( T ) == typeof( decimal ) )
  //          {
		//		return _getPart( candle ).To<T>();
		//	}
		//	else if ( typeof( T ) == typeof( Candle ) )
  //          {
		//		return candle.ToCandle().To<T>();
  //          }

		//	return candle.To<T>();
		//}

		///// <inheritdoc />
		//public override IIndicatorValue SetValue<T>( IIndicator indicator, T value )
		//{
		//	return value is SBar bar
		//			? new CandleIndicatorValue( indicator, ref bar ) { InputValue = this }
		//			: value.IsNull() ? new CandleIndicatorValue( indicator ) : base.SetValue( indicator, value );
		//}
	}
}
