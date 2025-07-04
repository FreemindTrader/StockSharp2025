// Decompiled with JetBrains decompiler
// Type: #=zNCoz_cr7eiA6K6bzw3PTSRjrsuvJB3oFJPVFS7w=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Common;
using System;

namespace StockSharp.Xaml.Charting;
public static class NumberUtil
{
    private static readonly Decimal DecimalEpsilon = 0.0000000000000000000000000001M;

    public static int GetDecimalLength( this Decimal _param0 )
    {
        MathHelper.DecimalInfo decimalInfo = MathHelper.GetDecimalInfo(_param0);
        int effectiveScale = ((MathHelper.DecimalInfo)  decimalInfo).EffectiveScale;
        if ( effectiveScale > 0 )
            ++effectiveScale;
        return MathHelper.GetDigitCount( ( long ) _param0 ) + effectiveScale;
    }

    public static bool DoubleEquals( this double _param0, double _param1 )
    {
        return Math.Abs( _param0 - _param1 ) < 1E-15;
    }

    public static double Round( this double _param0, double _param1 )
    {
        return Math.Round( _param0 / _param1 ) * _param1;
    }

    public static double NormalizePrice( this double _param0, double _param1 )
    {
        return Math.Round( _param0 / _param1 ) * _param1;
    }

    public static float Round( this float _param0, float _param1 )
    {
        return ( float ) Math.Round( ( double ) _param0 / ( double ) _param1 ) * _param1;
    }

    public static double RoundUp( double _param0, double _param1 )
    {
        return Math.Ceiling( _param0 / _param1 ) * _param1;
    }

    public static double RoundDown( double _param0, double _param1 )
    {
        return Math.Floor( _param0 / _param1 ) * _param1;
    }

    internal static bool IsDivisibleBy( double _param0, double _param1 )
    {
        _param0 = Math.Round( _param0, 15 );
        if ( Math.Abs( _param1 ) < 1E-15 )
            return false;
        double a = Math.Abs(_param0 / _param1);
        double num = 1E-15 * a;
        return Math.Abs( a - Math.Round( a ) ) <= num;
    }

    internal static bool IsDivisibleBy( Decimal _param0, Decimal _param1 )
    {
        if ( Math.Abs( _param1 - 0M ) < NumberUtil.DecimalEpsilon )
            return false;
        Decimal d = Math.Abs(_param0 / _param1);
        Decimal num = NumberUtil.DecimalEpsilon * d;
        return Math.Abs( d - Math.Round( d ) ) <= num;
    }

    internal static Decimal RoundUp( Decimal _param0, Decimal _param1 )
    {
        return Decimal.Ceiling( _param0 / _param1 ) * _param1;
    }

    public static void Swap( ref int _param0, ref int _param1 )
    {
        int num = _param1;
        _param1 = _param0;
        _param0 = num;
    }

    public static void Swap( ref long _param0, ref long _param1 )
    {
        long num = _param1;
        _param1 = _param0;
        _param0 = num;
    }

    public static void Swap( ref double _param0, ref double _param1 )
    {
        double num = _param1;
        _param1 = _param0;
        _param0 = num;
    }

    public static void Swap( ref float _param0, ref float _param1 )
    {
        float num = _param1;
        _param1 = _param0;
        _param0 = num;
    }

    internal static void SortedSwap(
      ref double _param0,
      ref double _param1,
      ref double _param2,
      ref double _param3 )
    {
        if ( _param0 <= _param1 )
            return;
        double num1 = _param0;
        _param0 = _param1;
        _param1 = num1;
        double num2 = _param2;
        _param2 = _param3;
        _param3 = num2;
    }

    public static int Constrain( int _param0, int _param1, int _param2 )
    {
        if ( _param0 < _param1 )
            return _param1;
        return _param0 <= _param2 ? _param0 : _param2;
    }

    public static long Constrain( long _param0, long _param1, long _param2 )
    {
        if ( _param0 < _param1 )
            return _param1;
        return _param0 <= _param2 ? _param0 : _param2;
    }

    public static double Constrain( double _param0, double _param1, double _param2 )
    {
        if ( _param0 < _param1 )
            return _param1;
        return _param0 <= _param2 ? _param0 : _param2;
    }

    public static bool IsPowerOf( double _param0, double _param1, double _param2 )
    {
        return Math.Abs( NumberUtil.RoundUpPower( _param0, _param1, _param2 ) - _param0 ) <= double.Epsilon;
    }

    internal static double RoundUpPower( double _param0, double _param1, double _param2 )
    {
        bool flag = Math.Sign(_param0) == -1;
        double a = Math.Round(Math.Log(Math.Abs(_param0), _param2) / Math.Log(Math.Abs(_param1), _param2), 5);
        double num1 = Math.Ceiling(a);
        if ( Math.Abs( num1 - a ) < double.Epsilon )
            return _param0;
        double y = flag ? num1 - 1.0 : num1;
        double num2 = Math.Pow(_param1, y);
        return !flag ? num2 : -num2;
    }

    internal static double RoundDownPower( double _param0, double _param1, double _param2 )
    {
        bool flag = Math.Sign(_param0) == -1;
        double d = Math.Round(Math.Log(Math.Abs(_param0), _param2) / Math.Log(Math.Abs(_param1), _param2), 5);
        double num1 = Math.Floor(d);
        if ( Math.Abs( num1 - d ) < double.Epsilon )
            return _param0;
        double y = flag ? num1 - 1.0 : num1;
        double num2 = Math.Pow(_param1, y);
        return !flag ? num2 : -num2;
    }

    internal static bool IsIntegerType( Type _param0 )
    {
        bool flag = false;
        switch ( Type.GetTypeCode( _param0 ) )
        {
            case TypeCode.SByte:
            case TypeCode.Byte:
            case TypeCode.Int16:
            case TypeCode.UInt16:
            case TypeCode.Int32:
            case TypeCode.UInt32:
            case TypeCode.Int64:
            case TypeCode.UInt64:
                flag = true;
                break;
        }
        return flag;
    }

    public static bool IsNaN( double _param0 ) => _param0 != _param0;
}
