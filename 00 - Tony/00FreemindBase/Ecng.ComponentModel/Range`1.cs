// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.Range`1
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using Ecng.Common;
using System;
using System.ComponentModel;

namespace Ecng.ComponentModel
{
  [Serializable]
  public class Range<T> : Equatable<Range<T>>, IConvertible, IRange where T : IComparable<T>
  {
    private readonly NullableEx<T> _min = new NullableEx<T>();
    private readonly NullableEx<T> _max = new NullableEx<T>();
    public static readonly T MinValue = default (T);
    public static readonly T MaxValue = default (T);
    private static IOperator<T> _operator;

    public Range()
    {
    }

    public Range(T min, T max)
    {
      this.Init(min, max);
    }

    [Browsable(false)]
    public bool HasMinValue
    {
      get
      {
        return this._min.HasValue;
      }
    }

    [Browsable(false)]
    public bool HasMaxValue
    {
      get
      {
        return this._max.HasValue;
      }
    }

    [Browsable(false)]
    public T Length
    {
      get
      {
        if (!this.HasMinValue || !this.HasMaxValue)
          return Range<T>.MaxValue;
        if (Range<T>._operator == null)
          Range<T>._operator = OperatorRegistry.GetOperator<T>();
        return Range<T>._operator.Subtract(this.Max, this.Min);
      }
    }

    public T Min
    {
      get
      {
        return this._min.Value;
      }
      set
      {
        if (this._max.HasValue)
          Range<T>.ValidateBounds(value, this.Max);
        this._min.Value = value;
      }
    }

    public T Max
    {
      get
      {
        return this._max.Value;
      }
      set
      {
        if (this._min.HasValue)
          Range<T>.ValidateBounds(this.Min, value);
        this._max.Value = value;
      }
    }

    public static explicit operator Range<T>(string str)
    {
      return Range<T>.Parse(str);
    }

    public static Range<T> Parse(string value)
    {
      if (value.IsEmpty())
        throw new ArgumentNullException(nameof (value));
      if (value.Length < 3)
        throw new ArgumentOutOfRangeException(nameof (value));
      value = value.Substring(1, value.Length - 2);
      value = value.Remove("Min:", false);
      string str1 = value.Substring(0, value.IndexOf("Max:") - 1);
      string str2 = value.Substring(value.IndexOf("Max:") + 4);
      Range<T> range = new Range<T>();
      if (!str1.IsEmpty() && str1 != "null")
        range.Min = str1.To<T>();
      if (!str2.IsEmpty() && str2 != "null")
        range.Max = str2.To<T>();
      return range;
    }

    public override int GetHashCode()
    {
      T obj = this.Min;
      int hashCode1 = obj.GetHashCode();
      obj = this.Max;
      int hashCode2 = obj.GetHashCode();
      return hashCode1 ^ hashCode2;
    }

    public override string ToString()
    {
      object[] objArray = new object[2];
      T obj;
      string str1;
      if (!this.HasMinValue)
      {
        str1 = "null";
      }
      else
      {
        obj = this.Min;
        str1 = obj.ToString();
      }
      objArray[0] = (object) str1;
      string str2;
      if (!this.HasMaxValue)
      {
        str2 = "null";
      }
      else
      {
        obj = this.Max;
        str2 = obj.ToString();
      }
      objArray[1] = (object) str2;
      return "{{Min:{0} Max:{1}}}".Put(objArray);
    }

    protected override bool OnEquals(Range<T> other)
    {
      if ((Equatable<NullableEx<T>>) this._min == other._min)
        return (Equatable<NullableEx<T>>) this._max == other._max;
      return false;
    }

    object IRange.Min
    {
      get
      {
        if (!this.HasMinValue)
          return (object) null;
        return (object) this.Min;
      }
      set
      {
        this.Min = (T) value;
      }
    }

    object IRange.Max
    {
      get
      {
        if (!this.HasMaxValue)
          return (object) null;
        return (object) this.Max;
      }
      set
      {
        this.Max = (T) value;
      }
    }

    public override Range<T> Clone()
    {
      return new Range<T>(this.Min, this.Max);
    }

    public bool Contains(Range<T> range)
    {
      if (range == null)
        throw new ArgumentNullException(nameof (range));
      if (this.Contains(range.Min))
        return this.Contains(range.Max);
      return false;
    }

    public Range<T> Intersect(Range<T> range)
    {
      if (range == null)
        throw new ArgumentNullException(nameof (range));
      if (this.Contains(range))
        return range.Clone();
      if (range.Contains(this))
        return this.Clone();
      int num = this.Contains(range.Min) ? 1 : 0;
      bool flag = this.Contains(range.Max);
      if (num != 0)
        return new Range<T>(range.Min, this.Max);
      if (flag)
        return new Range<T>(this.Min, range.Max);
      return (Range<T>) null;
    }

    public Range<T> SubRange(T min, T max)
    {
      if (!this.Contains(min))
        throw new ArgumentException(nameof (min));
      if (!this.Contains(max))
        throw new ArgumentException(nameof (max));
      return new Range<T>(min, max);
    }

    public bool Contains(T value)
    {
      T obj;
      if (this._min.HasValue)
      {
        obj = this.Min;
        if (obj.CompareTo(value) > 0)
          return false;
      }
      if (this._max.HasValue)
      {
        obj = this.Max;
        if (obj.CompareTo(value) < 0)
          return false;
      }
      return true;
    }

    private void Init(T min, T max)
    {
      Range<T>.ValidateBounds(min, max);
      this._min.Value = min;
      this._max.Value = max;
    }

    private static void ValidateBounds(T min, T max)
    {
      if (min.CompareTo(max) > 0)
        throw new ArgumentOutOfRangeException(nameof (min), string.Format("Min value {0} is more than max value {1}.", (object) min, (object) max));
    }

    TypeCode IConvertible.GetTypeCode()
    {
      return TypeCode.Object;
    }

    bool IConvertible.ToBoolean(IFormatProvider provider)
    {
      throw new InvalidCastException();
    }

    char IConvertible.ToChar(IFormatProvider provider)
    {
      throw new InvalidCastException();
    }

    sbyte IConvertible.ToSByte(IFormatProvider provider)
    {
      throw new InvalidCastException();
    }

    byte IConvertible.ToByte(IFormatProvider provider)
    {
      throw new InvalidCastException();
    }

    short IConvertible.ToInt16(IFormatProvider provider)
    {
      throw new InvalidCastException();
    }

    ushort IConvertible.ToUInt16(IFormatProvider provider)
    {
      throw new InvalidCastException();
    }

    int IConvertible.ToInt32(IFormatProvider provider)
    {
      throw new InvalidCastException();
    }

    uint IConvertible.ToUInt32(IFormatProvider provider)
    {
      throw new InvalidCastException();
    }

    long IConvertible.ToInt64(IFormatProvider provider)
    {
      throw new InvalidCastException();
    }

    ulong IConvertible.ToUInt64(IFormatProvider provider)
    {
      throw new InvalidCastException();
    }

    float IConvertible.ToSingle(IFormatProvider provider)
    {
      throw new InvalidCastException();
    }

    double IConvertible.ToDouble(IFormatProvider provider)
    {
      throw new InvalidCastException();
    }

    Decimal IConvertible.ToDecimal(IFormatProvider provider)
    {
      throw new InvalidCastException();
    }

    DateTime IConvertible.ToDateTime(IFormatProvider provider)
    {
      throw new InvalidCastException();
    }

    string IConvertible.ToString(IFormatProvider provider)
    {
      return this.ToString();
    }

    object IConvertible.ToType(Type conversionType, IFormatProvider provider)
    {
      if (conversionType == typeof (string))
        return (object) this.ToString();
      throw new InvalidCastException();
    }
  }
}
