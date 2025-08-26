using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockSharp.Algo.Indicators
{
    using System.ComponentModel;
    using System.Security.Cryptography;
    using Ecng.Common;
    using StockSharp.Algo.Indicators;
    using StockSharp.Localization;

    public class HewRsiIndicatorValue : ComplexIndicatorValue<HewRsiComplex>
    {
        public HewRsiIndicatorValue(HewRsiComplex indicator, DateTimeOffset time) : base(indicator, time)
        {
        }
    }

    /// <summary>
    /// Convergence/divergence of moving averages with signal line.
    /// </summary>
    [DisplayName("HewRsiComplex")]

    public class HewRsiComplex : BaseComplexIndicator<HewRsiIndicatorValue>
    {


        public HewRsiComplex()
        {
            AddInner(Rsi = new fxHewRSI());
            AddInner(OverBought = new ConstantLineIndicator(70) { Name = "OverBought" });
            AddInner(OverSold = new ConstantLineIndicator(30) { Name = "OverSold" });
        }

        /// <summary>
		/// Middle line.
		/// </summary>
		[Browsable(false)]
        public LengthIndicator<decimal> Rsi { get; }

        [Browsable(false)]
        public ConstantLineIndicator OverBought { get; }

        /// <summary>
        /// Lower band -.
        /// </summary>
        [Browsable(false)]
        public ConstantLineIndicator OverSold { get; }

        /// <summary>
		/// Period length. By default equal to 1.
		/// </summary>
		public virtual int Length
        {
            get => Rsi.Length;
            set
            {
                Rsi.Length = value;
                Reset();
            }
        }

        protected override HewRsiIndicatorValue CreateValue(DateTimeOffset time)
        {
            var value = new HewRsiIndicatorValue(this, time);
            
            return value;
        }

        protected override IIndicatorValue OnProcess(IIndicatorValue input)
        {
            var overbough = OverBought.Process(input);
            var overSold = OverSold.Process(input);
            var rsi = Rsi.Process(input);

            var value = new HewRsiIndicatorValue(this, input.Time);

            value.InnerValues.Add(OverBought, overbough);
            value.InnerValues.Add(Rsi, rsi);
            value.InnerValues.Add(OverSold, overSold);
            return value;
        }
    }
}
