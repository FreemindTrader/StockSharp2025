using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockSharp.Xaml.Charting;
internal class ComparableUtil
{
    private static readonly IDictionary<Type, IComparable> _zeroValues = (IDictionary<Type, IComparable>)new Dictionary<Type, IComparable>()
    {
      {
        typeof (long),
        (IComparable) 0L
      },
      {
        typeof (int),
        (IComparable) 0
      },
      {
        typeof (short),
        (IComparable) (short) 0
      },
      {
        typeof (sbyte),
        (IComparable) (sbyte) 0
      },
      {
        typeof (ulong),
        (IComparable) 0UL
      },
      {
        typeof (uint),
        (IComparable) 0U
      },
      {
        typeof (ushort),
        (IComparable) (ushort) 0
      },
      {
        typeof (byte),
        (IComparable) (byte) 0
      },
      {
        typeof (Decimal),
        (IComparable) Decimal.Zero
      },
      {
        typeof (double),
        (IComparable) 0.0
      },
      {
        typeof (float),
        (IComparable) 0.0f
      },
      {
        typeof (DateTime),
        (IComparable) new DateTime(0L)
      }
    };
    private static readonly IDictionary<Type, IComparable> _maxValues = (IDictionary<Type, IComparable>)new Dictionary<Type, IComparable>()
    {
      {
        typeof (long),
        (IComparable) long.MaxValue
      },
      {
        typeof (int),
        (IComparable) int.MaxValue
      },
      {
        typeof (short),
        (IComparable) short.MaxValue
      },
      {
        typeof (sbyte),
        (IComparable) sbyte.MaxValue
      },
      {
        typeof (ulong),
        (IComparable) ulong.MaxValue
      },
      {
        typeof (uint),
        (IComparable) uint.MaxValue
      },
      {
        typeof (ushort),
        (IComparable) ushort.MaxValue
      },
      {
        typeof (byte),
        (IComparable) byte.MaxValue
      },
      {
        typeof (Decimal),
        (IComparable) new Decimal(-1, -1, -1, false, (byte) 0)
      },
      {
        typeof (double),
        (IComparable) double.MaxValue
      },
      {
        typeof (float),
        (IComparable) float.MaxValue
      },
      {
        typeof (DateTime),
        (IComparable) DateTime.MaxValue
      }
    };
    private static readonly IDictionary<Type, IComparable> _minValues = (IDictionary<Type, IComparable>)new Dictionary<Type, IComparable>()
    {
      {
        typeof (long),
        (IComparable) long.MinValue
      },
      {
        typeof (int),
        (IComparable) int.MinValue
      },
      {
        typeof (short),
        (IComparable) short.MinValue
      },
      {
        typeof (sbyte),
        (IComparable) sbyte.MinValue
      },
      {
        typeof (ulong),
        (IComparable) 0UL
      },
      {
        typeof (uint),
        (IComparable) 0U
      },
      {
        typeof (ushort),
        (IComparable) (ushort) 0
      },
      {
        typeof (byte),
        (IComparable) (byte) 0
      },
      {
        typeof (Decimal),
        (IComparable) new Decimal(-1, -1, -1, true, (byte) 0)
      },
      {
        typeof (double),
        (IComparable) double.MinValue
      },
      {
        typeof (float),
        (IComparable) float.MinValue
      },
      {
        typeof (DateTime),
        (IComparable) DateTime.MinValue
      }
    };
    private static readonly IDictionary<Type, Func<IComparable, bool>> _isDefinedDelegates = (IDictionary<Type, Func<IComparable, bool>>)new Dictionary<Type, Func<IComparable, bool>>()
    {
      {
        typeof (long),
        (Func<IComparable, bool>) (c => ComparableUtil.IsDefined((long) c))
      },
      {
        typeof (int),
        (Func<IComparable, bool>) (c => ComparableUtil.IsDefined((int) c))
      },
      {
        typeof (short),
        (Func<IComparable, bool>) (c => true)
      },
      {
        typeof (sbyte),
        (Func<IComparable, bool>) (c => true)
      },
      {
        typeof (ulong),
        (Func<IComparable, bool>) (c => true)
      },
      {
        typeof (uint),
        (Func<IComparable, bool>) (c => true)
      },
      {
        typeof (ushort),
        (Func<IComparable, bool>) (c => true)
      },
      {
        typeof (byte),
        (Func<IComparable, bool>) (c => true)
      },
      {
        typeof (Decimal),
        (Func<IComparable, bool>) (c => ComparableUtil.IsDefined((Decimal) c))
      },
      {
        typeof (double),
        (Func<IComparable, bool>) (c => ComparableUtil.IsDefined((double) c))
      },
      {
        typeof (float),
        (Func<IComparable, bool>) (c => ComparableUtil.IsDefined((float) c))
      },
      {
        typeof (DateTime),
        (Func<IComparable, bool>) (c => ComparableUtil.IsDefined((DateTime) c))
      },
      {
        typeof (TimeSpan),
        (Func<IComparable, bool>) (c => ComparableUtil.IsDefined((TimeSpan) c))
      }
    };

    internal static TComparable MaxValue<TComparable>() where TComparable : IComparable
    {
        Type key = typeof(TComparable);
        if ( ComparableUtil._maxValues.ContainsKey(key) )
        {
            return (TComparable)ComparableUtil._maxValues[key];
        }

        throw new InvalidOperationException(string.Format("Cannot get the MaxValue of Type {0}", (object)key));
    }

    internal static TComparable Zero<TComparable>()
    {
        Type key = typeof(TComparable);
        if ( ComparableUtil._maxValues.ContainsKey(key) )
        {
            return (TComparable)ComparableUtil._zeroValues[key];
        }

        throw new InvalidOperationException(string.Format("Cannot get the Zero Value of Type {0}", (object)key));
    }

    public static IComparable MinValue(Type comparableType)
    {
        if ( ComparableUtil._minValues.ContainsKey(comparableType) )
        {
            return ComparableUtil._minValues[comparableType];
        }

        throw new InvalidOperationException(string.Format("Cannot get the MinValue of Type {0}", (object)comparableType));
    }

    internal static TComparable MinValue<TComparable>() where TComparable : IComparable
    {
        Type key = typeof(TComparable);
        if ( ComparableUtil._minValues.ContainsKey(key) )
        {
            return (TComparable)ComparableUtil._minValues[key];
        }

        throw new InvalidOperationException(string.Format("Cannot get the MinValue of Type {0}", (object)key));
    }

    internal static bool IsValidComparableType<TComparable>() where TComparable : IComparable
    {
        return ComparableUtil.IsValidComparableType(typeof(TComparable));
    }

    internal static bool IsValidComparableType(Type comparableType)
    {
        return ComparableUtil._minValues.ContainsKey(comparableType);
    }

    internal static TComparable Max<TComparable>(TComparable first, TComparable second) where TComparable : IComparable
    {
        if ( first.CompareTo((object)second) <= 0 )
        {
            return second;
        }

        return first;
    }

    internal static TComparable Min<TComparable>(TComparable first, TComparable second) where TComparable : IComparable
    {
        if ( second.CompareTo((object)first) <= 0 )
        {
            return second;
        }

        return first;
    }

    internal static TComparable Max<TComparable>(TComparable a, TComparable b, TComparable c, TComparable d) where TComparable : IComparable
    {
        return ComparableUtil.Max<TComparable>(a, ComparableUtil.Max<TComparable>(b, ComparableUtil.Max<TComparable>(c, d)));
    }

    internal static TComparable Min<TComparable>(TComparable a, TComparable b, TComparable c, TComparable d) where TComparable : IComparable
    {
        return ComparableUtil.Min<TComparable>(a, ComparableUtil.Min<TComparable>(b, ComparableUtil.Min<TComparable>(c, d)));
    }

    internal static bool IsDefined(IComparable comparable)
    {
        if ( comparable is double )
        {
            return ComparableUtil.IsDefined((double)comparable);
        }

        if ( comparable is DateTime )
        {
            return ComparableUtil.IsDefined((DateTime)comparable);
        }

        Type type = comparable.GetType();
        if ( ComparableUtil._isDefinedDelegates.ContainsKey(type) )
        {
            return ComparableUtil._isDefinedDelegates[type](comparable);
        }

        throw new InvalidOperationException(string.Format("The Type {0} is not a valid Comparable Type", (object)comparable));
    }

    internal static double ToDouble(DateTime value)
    {
        return (double)value.Ticks;
    }

    internal static double ToDouble(IComparable comparable)
    {
        if ( comparable is DateTime )
        {
            return (double)( (DateTime)comparable ).Ticks;
        }

        if ( comparable is TimeSpan )
        {
            return (double)( (TimeSpan)comparable ).Ticks;
        }

        return Convert.ToDouble((object)comparable, (IFormatProvider)CultureInfo.InvariantCulture);
    }

    private static bool IsDefined(short arg)
    {
        return true;
    }

    private static bool IsDefined(sbyte arg)
    {
        return true;
    }

    private static bool IsDefined(ulong arg)
    {
        return true;
    }

    private static bool IsDefined(uint arg)
    {
        return true;
    }

    private static bool IsDefined(ushort arg)
    {
        return true;
    }

    private static bool IsDefined(byte arg)
    {
        return true;
    }

    private static bool IsDefined(long arg)
    {
        if ( arg != long.MinValue )
        {
            return arg != long.MaxValue;
        }

        return false;
    }

    private static bool IsDefined(int arg)
    {
        if ( arg != int.MinValue )
        {
            return arg != int.MaxValue;
        }

        return false;
    }

    private static bool IsDefined(Decimal arg)
    {
        if ( arg != new Decimal(-1, -1, -1, true, (byte)0) )
        {
            return arg != new Decimal(-1, -1, -1, false, (byte)0);
        }

        return false;
    }

    private static bool IsDefined(double arg)
    {
        if ( arg != double.MinValue && arg != double.MaxValue && !double.IsInfinity(arg) )
        {
            return !double.IsNaN(arg);
        }

        return false;
    }

    private static bool IsDefined(float arg)
    {
        if ( (double)arg != -3.40282346638529E+38 && (double)arg != 3.40282346638529E+38 )
        {
            return !float.IsNaN(arg);
        }

        return false;
    }

    private static bool IsDefined(DateTime arg)
    {
        return arg != DateTime.MaxValue;
    }

    private static bool IsDefined(TimeSpan arg)
    {
        return arg != TimeSpan.MaxValue;
    }

    private static bool CanConvertToUInt64(double value)
    {
        if ( ComparableUtil.IsDefined(value) && value >= 0.0 )
        {
            return value <= 1.84467440737096E+19;
        }

        return false;
    }

    private static bool CanConvertToUInt32(double value)
    {
        if ( ComparableUtil.IsDefined(value) && value >= 0.0 )
        {
            return value <= (double)uint.MaxValue;
        }

        return false;
    }

    private static bool CanConvertToUInt16(double value)
    {
        if ( ComparableUtil.IsDefined(value) && value >= 0.0 )
        {
            return value <= (double)ushort.MaxValue;
        }

        return false;
    }

    private static bool CanConvertToSByte(double value)
    {
        if ( ComparableUtil.IsDefined(value) && value >= (double)sbyte.MinValue )
        {
            return value <= (double)sbyte.MaxValue;
        }

        return false;
    }

    private static bool CanConvertToByte(double value)
    {
        if ( ComparableUtil.IsDefined(value) && value >= 0.0 )
        {
            return value <= (double)byte.MaxValue;
        }

        return false;
    }

    private static bool CanConvertToInt64(double value)
    {
        if ( ComparableUtil.IsDefined(value) && value > (double)long.MinValue )
        {
            return value < (double)long.MaxValue;
        }

        return false;
    }

    private static bool CanConvertToInt32(double value)
    {
        if ( ComparableUtil.IsDefined(value) && value >= (double)int.MinValue )
        {
            return value <= (double)int.MaxValue;
        }

        return false;
    }

    private static bool CanConvertToInt16(double value)
    {
        if ( ComparableUtil.IsDefined(value) && value >= (double)short.MinValue )
        {
            return value <= (double)short.MaxValue;
        }

        return false;
    }

    private static bool CanConvertToDecimal(double value)
    {
        if ( ComparableUtil.IsDefined(value) && value >= -7.92281625142643E+28 )
        {
            return value <= 7.92281625142643E+28;
        }

        return false;
    }

    private static bool CanConvertToSingle(double value)
    {
        if ( ComparableUtil.IsDefined(value) && value >= -3.40282346638529E+38 )
        {
            return value <= 3.40282346638529E+38;
        }

        return false;
    }

    private static bool CanConvertToDateTime(double value)
    {
        if ( ComparableUtil.CanConvertToInt64(value) )
        {
            return value >= 0.0;
        }

        return false;
    }

    public static DateTime DateTimeFromDouble(double rawDataValue)
    {
        if ( ComparableUtil.IsDefined(rawDataValue) )
        {
            return new DateTime((long)rawDataValue);
        }

        return DateTime.MaxValue;
    }

    public static IComparable FromDouble(double rawDataValue, Type type)
    {
        IComparable comparable = (IComparable)null;
        if ( ComparableUtil.CanChangeType(rawDataValue, type) )
        {
            comparable = !( type == typeof(DateTime) ) ? ( !( type == typeof(TimeSpan) ) ? (IComparable)Convert.ChangeType((object)rawDataValue, type, (IFormatProvider)CultureInfo.InvariantCulture) : (IComparable)new TimeSpan((long)rawDataValue) ) : (IComparable)new DateTime((long)rawDataValue);
        }

        return comparable;
    }

    internal static bool CanChangeType(double value, Type conversionType)
    {
        bool flag = false;
        switch ( Type.GetTypeCode(conversionType) )
        {
            case TypeCode.SByte:
                flag = ComparableUtil.CanConvertToSByte(value);
                break;
            case TypeCode.Byte:
                flag = ComparableUtil.CanConvertToByte(value);
                break;
            case TypeCode.Int16:
                flag = ComparableUtil.CanConvertToInt16(value);
                break;
            case TypeCode.UInt16:
                flag = ComparableUtil.CanConvertToUInt16(value);
                break;
            case TypeCode.Int32:
                flag = ComparableUtil.CanConvertToInt32(value);
                break;
            case TypeCode.UInt32:
                flag = ComparableUtil.CanConvertToUInt32(value);
                break;
            case TypeCode.Int64:
                flag = ComparableUtil.CanConvertToInt64(value);
                break;
            case TypeCode.UInt64:
                flag = ComparableUtil.CanConvertToUInt64(value);
                break;
            case TypeCode.Single:
                flag = ComparableUtil.CanConvertToSingle(value);
                break;
            case TypeCode.Double:
                flag = true;
                break;
            case TypeCode.Decimal:
                flag = ComparableUtil.CanConvertToDecimal(value);
                break;
            case TypeCode.DateTime:
                flag = ComparableUtil.CanConvertToDateTime(value);
                break;
            default:
                if ( conversionType == typeof(TimeSpan) )
                {
                    flag = ComparableUtil.CanConvertToInt64(value);
                    break;
                }
                break;
        }
        return flag;
    }

    public static IComparable Subtract(IComparable yValue, IComparable yValue2)
    {
        throw new NotImplementedException();
    }
}