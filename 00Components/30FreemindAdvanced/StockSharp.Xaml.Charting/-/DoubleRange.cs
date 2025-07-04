using System;

namespace SciChart.Data.Model;

//
// Summary:
//     Defines a range of type System.Double
public class DoubleRange : SciChart.Data.Model.Range<double>
{
    //
    // Summary:
    //     Returns a new Undefined range
    public static DoubleRange UndefinedRange => new DoubleRange( double.NaN, double.NaN );

    //
    // Summary:
    //     Gets the difference (Max - Min) of this range
    public override double Diff => base.Max - base.Min;

    //
    // Summary:
    //     Gets whether the range is Zero, where Max equals Min
    public override bool IsZero => Math.Abs( Diff ) <= double.Epsilon;

    //
    // Summary:
    //     Initializes a new instance of the SciChart.Data.Model.DoubleRange class.
    public DoubleRange()
    {
    }

    //
    // Summary:
    //     Initializes a new instance of the SciChart.Data.Model.DoubleRange class.
    //
    // Parameters:
    //   min:
    //     The min.
    //
    //   max:
    //     The max.
    public DoubleRange( double min, double max )
        : base( min, max )
    {
    }

    //
    // Summary:
    //     Clones this instance.
    public override object Clone()
    {
        return new DoubleRange( base.Min, base.Max );
    }

    //
    // Summary:
    //     Converts this range to a SciChart.Data.Model.DoubleRange, which are used internally
    //     for calculations
    public override DoubleRange AsDoubleRange()
    {
        return this;
    }

    //
    // Summary:
    //     Sets the Min, Max values on the SciChart.Data.Model.IRange, returning this instance
    //     after modification
    //
    // Parameters:
    //   min:
    //     The new Min value.
    //
    //   max:
    //     The new Max value.
    //
    // Returns:
    //     This instance, after the operation
    public override IRange<double> SetMinMax( double min, double max )
    {
        SetMinMaxInternal( min, max );
        return this;
    }

    //
    // Summary:
    //     Sets the Min, Max values on the SciChart.Data.Model.IRange with a max range to
    //     clip values to, returning this instance after modification
    //
    // Parameters:
    //   min:
    //     The new Min value.
    //
    //   max:
    //     The new Max value.
    //
    //   maxRange:
    //     The max range, which is used to clip values.
    //
    // Returns:
    //     This instance, after the operation
    public override IRange<double> SetMinMax( double min, double max, IRange<double> maxRange )
    {
        base.Min = Math.Max( min, maxRange.Min );
        base.Max = Math.Min( max, maxRange.Max );
        return this;
    }

    public override IRange<double> GrowBy(
    double _param1,
    double _param2)
  {
    double diff = this.Diff;
    double num1 = this.Min - _param1 * (this.IsZero ? this.Min : diff);
    double num2 = this.Max + _param2 * (this.IsZero ? this.Max : diff);
    if (num1 > num2)
      \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSRjrsuvJB3oFJPVFS7w\u003D.\u0023\u003DzMv8ALVs\u003D(ref num1, ref num2);
    if (Math.Abs(num1 - num2) <= double.Epsilon && Math.Abs(num1) <= double.Epsilon)
    {
      num1 = -1.0;
      num2 = 1.0;
    }
    this.Min = num1;
    this.Max = num2;
    return (IRange<double>) this;
  }

  public override IRange<double> \u0023\u003DzJIqIiUw\u003D(
    IRange<double> _param1)
  {
    double max = this.Max;
    double min = this.Min;
    double num1 = Math.Min(this.Max, _param1.Max);
    double num2 = Math.Max(this.Min, _param1.Min);
    if (num2 > _param1.Max)
      num2 = _param1.Min;
    if (num1 < min)
      num1 = _param1.Max;
    if (num2 > num1)
    {
      num2 = _param1.Min;
      num1 = _param1.Max;
    }
    this.SetMinMaxInternal(num2, num1);
    return (IRange<double>) this;
  }
}
