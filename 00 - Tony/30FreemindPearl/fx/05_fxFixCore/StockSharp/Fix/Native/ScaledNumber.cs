using System;
using System.Globalization;

namespace StockSharp.Fix.Native
{
    /// <summary>
    /// Scaled numbers, like floating point numbers are represented as a mantissa and an exponent.
    /// </summary>
    public struct ScaledNumber
    {
        /// <summary>Mantissa.</summary>
        public readonly long Mantissa;
        /// <summary>Exponent.</summary>
        public readonly int Exponent;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Fix.Native.ScaledNumber" />.
        /// </summary>
        /// <param name="exponent">Exponent.</param>
        /// <param name="mantissa">Mantissa.</param>
        public ScaledNumber(int exponent, long mantissa)
        {
            this.Exponent = exponent;
            this.Mantissa = mantissa;
        }

        /// <summary>
        /// The numerical value is obtained by multiplying the mantissa with the base-10 power of the exponent.
        /// </summary>
        public double AsDouble => (double)this.Mantissa * Math.Pow(10.0, (double)this.Exponent);

        /// <summary>
        /// The numerical value is obtained by multiplying the mantissa with the base-10 power of the exponent.
        /// </summary>
        public decimal AsDecimal => new decimal(this.AsDouble);

        /// <inheritdoc />
        public override string ToString() => this.Exponent != 0 ? this.AsDouble.ToString((IFormatProvider)CultureInfo.InvariantCulture) : this.Mantissa.ToString();

        /// <summary>
        /// Add the two objects <see cref="T:StockSharp.Fix.Native.ScaledNumber" />.
        /// </summary>
        /// <param name="n1">First object <see cref="T:StockSharp.Fix.Native.ScaledNumber" />.</param>
        /// <param name="n2">Second object <see cref="T:StockSharp.Fix.Native.ScaledNumber" />.</param>
        /// <returns>The result of addition.</returns>
        public static ScaledNumber operator +(ScaledNumber n1, ScaledNumber n2) => new ScaledNumber(n1.Exponent + n2.Exponent, n1.Mantissa + n2.Mantissa);
    }
}
