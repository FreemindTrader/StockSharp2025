// Decompiled with JetBrains decompiler
// Type: #=zK11CXzkQ3m66hjsjmkZfa7gEpyy6EjdRjsrA1yjQSXVyIo1W$w==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
internal abstract class \u0023\u003DzK11CXzkQ3m66hjsjmkZfa7gEpyy6EjdRjsrA1yjQSXVyIo1W\u0024w\u003D\u003D : 
  \u0023\u003Dzx24ajpn1eHsuu_VYWsSDOby\u0024vWkeqZwE94P6zz4sY0BD_b\u0024iDA\u003D\u003D
{
  private static byte[] \u0023\u003DzpQ\u0024NBSHbFmS_;

  internal static bool \u0023\u003DzilOTiYzU6JIQ(
    \u0023\u003Dzr3AyUEt11qAsNGjKm7GKWxmriZN_\u0024I_fB5TLZqozNbfOHxiykg\u003D\u003D _param0,
    IndexRange  _param1,
    int _param2)
  {
    int num1 = _param1.Max - _param1.Min + 1;
    int num2 = 4 * _param2;
    return _param0 != 0 & num1 > num2;
  }

  public abstract \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ \u0023\u003Dzg_KsNhI\u003D(
    \u0023\u003Dzr3AyUEt11qAsNGjKm7GKWxmriZN_\u0024I_fB5TLZqozNbfOHxiykg\u003D\u003D _param1,
    IndexRange  _param2,
    int _param3,
    bool _param4,
    bool _param5,
    IList _param6,
    IList _param7,
    bool? _param8,
    bool? _param9,
    bool? _param10,
    IRange _param11);

  private static void \u0023\u003Dzq3MgExWxza1L<TX>(
    IList<TX> _param0,
    IndexRange  _param1,
    IRange _param2,
    out double _param3,
    out double _param4)
    where TX : IComparable
  {
    _param3 = _param0[_param1.Min].ToDouble();
    _param4 = _param0[_param1.Max].ToDouble();
    if (_param2 == null || _param2 is IndexRange )
      return;
    _param3 = _param2.Min.ToDouble();
    _param4 = _param2.Max.ToDouble();
  }

  private static bool \u0023\u003DzkfeONHfyq5z2Lpu\u00241g\u003D\u003D<TX, TY>(
    IMath<TX> _param0,
    IMath<TY> _param1,
    \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWuPneE7x_PdElYKA_LxaZfcJ<TX> _param2,
    \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWuPneE7x_PdElYKA_LxaZfcJ<TY> _param3,
    int _param4,
    int _param5,
    double _param6,
    out double _param7,
    out double _param8,
    out int _param9)
    where TX : IComparable
    where TY : IComparable
  {
    double d = _param1.ToDouble(_param3[_param4]);
    double num = _param0.ToDouble(_param2[_param4]);
    _param9 = 0;
    _param7 = d;
    _param8 = d;
    bool flag = false;
    for (; num <= _param6; num = _param0.ToDouble(_param2[_param4]))
    {
      if (_param9 == 0)
        flag = double.IsNaN(d);
      else if (flag != double.IsNaN(d))
        return false;
      if (d <= _param7)
        _param7 = d;
      if (d >= _param8)
        _param8 = d;
      ++_param9;
      if (_param9 < _param5)
      {
        ++_param4;
        d = _param1.ToDouble(_param3[_param4]);
      }
      else
        break;
    }
    return true;
  }

  private static unsafe bool \u0023\u003DzkfeONHfyq5z2Lpu\u00241g\u003D\u003D(
    double* _param0,
    double* _param1,
    int _param2,
    double _param3,
    out double _param4,
    out double _param5,
    out int _param6)
  {
    double d = *_param1;
    double num = *_param0;
    _param6 = 0;
    _param4 = d;
    _param5 = d;
    bool flag = false;
    for (; num <= _param3; num = *_param0)
    {
      if (_param6 == 0)
        flag = double.IsNaN(d);
      else if (flag != double.IsNaN(d))
        return false;
      if (d <= _param4)
        _param4 = d;
      if (d >= _param5)
        _param5 = d;
      ++_param6;
      if (_param6 < _param2)
      {
        ++_param0;
        ++_param1;
        d = *_param1;
      }
      else
        break;
    }
    return true;
  }

  protected static \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ \u0023\u003DzndP7N51gjHRMsMhJMjJcSFhKis4Nilo5fw\u003D\u003D<TX, TY>(
    IList<TX> _param0,
    IList<TY> _param1,
    IndexRange  _param2,
    int _param3,
    bool _param4,
    IRange _param5)
    where TX : IComparable
    where TY : IComparable
  {
    double num1;
    double num2;
    \u0023\u003DzK11CXzkQ3m66hjsjmkZfa7gEpyy6EjdRjsrA1yjQSXVyIo1W\u0024w\u003D\u003D.\u0023\u003Dzq3MgExWxza1L<TX>(_param0, _param2, _param5, out num1, out num2);
    int max = _param2.Max;
    int min = _param2.Min;
    double num3 = (num2 - num1) / (double) _param3;
    \u0023\u003DzRYm3Fw8jwwRKksCg00\u00244P09swszfkFaReRy0a_AtDN3C p09swszfkFaReRy0aAtDn3C = new \u0023\u003DzRYm3Fw8jwwRKksCg00\u00244P09swszfkFaReRy0a_AtDN3C(_param3 * 2 + 1);
    \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWuPneE7x_PdElYKA_LxaZfcJ<TY> e7xPdElYkaLxaZfcJ1 = _param1.\u0023\u003Dz1bvQV4SZTWpA<TY>(true);
    \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWuPneE7x_PdElYKA_LxaZfcJ<TX> e7xPdElYkaLxaZfcJ2 = _param0.\u0023\u003Dz1bvQV4SZTWpA<TX>(true);
    bool flag = false;
    if (e7xPdElYkaLxaZfcJ2 != null && e7xPdElYkaLxaZfcJ1 != null)
    {
      \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWuPneE7x_PdElYKA_LxaZfcJ<double> e7xPdElYkaLxaZfcJ3 = e7xPdElYkaLxaZfcJ2 as \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWuPneE7x_PdElYKA_LxaZfcJ<double>;
      \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWuPneE7x_PdElYKA_LxaZfcJ<double> e7xPdElYkaLxaZfcJ4 = e7xPdElYkaLxaZfcJ1 as \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWuPneE7x_PdElYKA_LxaZfcJ<double>;
      if (e7xPdElYkaLxaZfcJ3 != null && e7xPdElYkaLxaZfcJ4 != null)
      {
        \u0023\u003DzK11CXzkQ3m66hjsjmkZfa7gEpyy6EjdRjsrA1yjQSXVyIo1W\u0024w\u003D\u003D.\u0023\u003DzZIhJt9xKqxB\u0024TUA5APdhn7U\u003D(e7xPdElYkaLxaZfcJ3, e7xPdElYkaLxaZfcJ4, p09swszfkFaReRy0aAtDn3C, min, max, num1, num2, _param3, _param4);
        flag = true;
      }
      if (!flag)
      {
        \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWuPneE7x_PdElYKA_LxaZfcJ<double> e7xPdElYkaLxaZfcJ5 = e7xPdElYkaLxaZfcJ2 as \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWuPneE7x_PdElYKA_LxaZfcJ<double>;
        \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWuPneE7x_PdElYKA_LxaZfcJ<float> e7xPdElYkaLxaZfcJ6 = e7xPdElYkaLxaZfcJ1 as \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWuPneE7x_PdElYKA_LxaZfcJ<float>;
        if (e7xPdElYkaLxaZfcJ5 != null && e7xPdElYkaLxaZfcJ6 != null)
        {
          \u0023\u003DzK11CXzkQ3m66hjsjmkZfa7gEpyy6EjdRjsrA1yjQSXVyIo1W\u0024w\u003D\u003D.\u0023\u003DzZIhJt9xKqxB\u0024TUA5APdhn7U\u003D<double, float>(e7xPdElYkaLxaZfcJ5, e7xPdElYkaLxaZfcJ6, p09swszfkFaReRy0aAtDn3C, min, max, num1, num2, _param3, _param4);
          flag = true;
        }
      }
    }
    if (!flag)
      \u0023\u003DzK11CXzkQ3m66hjsjmkZfa7gEpyy6EjdRjsrA1yjQSXVyIo1W\u0024w\u003D\u003D.\u0023\u003DzZIhJt9xKqxB\u0024TUA5APdhn7U\u003D<TX, TY>(e7xPdElYkaLxaZfcJ2, e7xPdElYkaLxaZfcJ1, p09swszfkFaReRy0aAtDn3C, min, max, num1, num2, _param3, _param4);
    p09swszfkFaReRy0aAtDn3C.\u0023\u003DzK_5NMsk\u003D();
    return (\u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ) p09swszfkFaReRy0aAtDn3C;
  }

  private static unsafe void \u0023\u003DzZIhJt9xKqxB\u0024TUA5APdhn7U\u003D(
    \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWuPneE7x_PdElYKA_LxaZfcJ<double> _param0,
    \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWuPneE7x_PdElYKA_LxaZfcJ<double> _param1,
    \u0023\u003DzRYm3Fw8jwwRKksCg00\u00244P09swszfkFaReRy0a_AtDN3C _param2,
    int _param3,
    int _param4,
    double _param5,
    double _param6,
    int _param7,
    bool _param8)
  {
    fixed (double* numPtr1 = &_param0.\u0023\u003DzvsnCYl4\u003D()[_param3])
      fixed (double* numPtr2 = &_param1.\u0023\u003DzvsnCYl4\u003D()[_param3])
      {
        \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSvLP\u0024zDbYxtEhpMKleCtJGtGqo7ZPw\u003D\u003D<double> mkleCtJgtGqo7Zpw1 = _param2.\u0023\u003DzwQnyySN6xaVC();
        \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSvLP\u0024zDbYxtEhpMKleCtJGtGqo7ZPw\u003D\u003D<double> mkleCtJgtGqo7Zpw2 = _param2.\u0023\u003DzPqsSI6C5MOOb();
        double* numPtr3 = numPtr1;
        double* numPtr4 = numPtr2;
        int num1 = _param3;
        bool flag = true;
        double num2 = (_param6 - _param5) / (double) _param7;
        int num3 = 0;
        while (num3 < _param7)
        {
          double num4;
          double num5;
          int num6;
          if (\u0023\u003DzK11CXzkQ3m66hjsjmkZfa7gEpyy6EjdRjsrA1yjQSXVyIo1W\u0024w\u003D\u003D.\u0023\u003DzkfeONHfyq5z2Lpu\u00241g\u003D\u003D(numPtr3, numPtr4, _param4 - num1 + 1, _param5 + num2 * (double) (num3 + 1), out num4, out num5, out num6))
            ++num3;
          double num7 = _param8 ? (double) num1 : *numPtr3;
          if (num6 != 0)
          {
            if (flag)
            {
              mkleCtJgtGqo7Zpw1.Add(num7);
              mkleCtJgtGqo7Zpw2.Add(*numPtr4);
            }
            mkleCtJgtGqo7Zpw1.Add(num7);
            mkleCtJgtGqo7Zpw2.Add(num4);
            mkleCtJgtGqo7Zpw1.Add(num7);
            mkleCtJgtGqo7Zpw2.Add(num5);
          }
          else if (!flag)
          {
            mkleCtJgtGqo7Zpw1.Add(*(numPtr3 - 1));
            mkleCtJgtGqo7Zpw2.Add(*(numPtr4 - 1));
          }
          num1 += num6;
          numPtr3 += num6;
          numPtr4 += num6;
          if (num1 <= _param4)
            flag = num6 == 0;
          else
            break;
        }
        if (num1 <= _param4)
        {
          double num8 = _param8 ? (double) num1 : *numPtr3;
          mkleCtJgtGqo7Zpw1.Add(num8);
          mkleCtJgtGqo7Zpw2.Add(*numPtr4);
        }
      }
  }

  private static void \u0023\u003DzZIhJt9xKqxB\u0024TUA5APdhn7U\u003D<TX, TY>(
    \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWuPneE7x_PdElYKA_LxaZfcJ<TX> _param0,
    \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWuPneE7x_PdElYKA_LxaZfcJ<TY> _param1,
    \u0023\u003DzRYm3Fw8jwwRKksCg00\u00244P09swszfkFaReRy0a_AtDN3C _param2,
    int _param3,
    int _param4,
    double _param5,
    double _param6,
    int _param7,
    bool _param8)
    where TX : IComparable
    where TY : IComparable
  {
    IMath<TX> mijfkOcK7kdYtA2avPae1 = MathHelper.GetMath<TX>();
    IMath<TY> mijfkOcK7kdYtA2avPae2 = MathHelper.GetMath<TY>();
    \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSvLP\u0024zDbYxtEhpMKleCtJGtGqo7ZPw\u003D\u003D<double> mkleCtJgtGqo7Zpw1 = _param2.\u0023\u003DzwQnyySN6xaVC();
    \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSvLP\u0024zDbYxtEhpMKleCtJGtGqo7ZPw\u003D\u003D<double> mkleCtJgtGqo7Zpw2 = _param2.\u0023\u003DzPqsSI6C5MOOb();
    int num1 = _param3;
    bool flag = true;
    double num2 = (_param6 - _param5) / (double) _param7;
    int num3 = 0;
    while (num3 < _param7)
    {
      double num4;
      double num5;
      int num6;
      if (\u0023\u003DzK11CXzkQ3m66hjsjmkZfa7gEpyy6EjdRjsrA1yjQSXVyIo1W\u0024w\u003D\u003D.\u0023\u003DzkfeONHfyq5z2Lpu\u00241g\u003D\u003D<TX, TY>(mijfkOcK7kdYtA2avPae1, mijfkOcK7kdYtA2avPae2, _param0, _param1, num1, _param4 - num1 + 1, _param5 + num2 * (double) (num3 + 1), out num4, out num5, out num6))
        ++num3;
      double num7 = _param8 ? (double) num1 : mijfkOcK7kdYtA2avPae1.ToDouble(_param0[num1]);
      if (num6 != 0)
      {
        if (flag)
        {
          mkleCtJgtGqo7Zpw1.Add(num7);
          mkleCtJgtGqo7Zpw2.Add(mijfkOcK7kdYtA2avPae2.ToDouble(_param1[num1]));
        }
        mkleCtJgtGqo7Zpw1.Add(num7);
        mkleCtJgtGqo7Zpw2.Add(num4);
        mkleCtJgtGqo7Zpw1.Add(num7);
        mkleCtJgtGqo7Zpw2.Add(num5);
      }
      else if (!flag)
      {
        mkleCtJgtGqo7Zpw1.Add(mijfkOcK7kdYtA2avPae1.ToDouble(_param0[num1 - 1]));
        mkleCtJgtGqo7Zpw2.Add(mijfkOcK7kdYtA2avPae2.ToDouble(_param1[num1 - 1]));
      }
      num1 += num6;
      if (num1 <= _param4)
        flag = num6 == 0;
      else
        break;
    }
    if (num1 > _param4)
      return;
    double num8 = _param8 ? (double) num1 : mijfkOcK7kdYtA2avPae1.ToDouble(_param0[num1]);
    mkleCtJgtGqo7Zpw1.Add(num8);
    mkleCtJgtGqo7Zpw2.Add(mijfkOcK7kdYtA2avPae2.ToDouble(_param1[num1]));
  }

  [DllImport("msvcrt.dll", EntryPoint = "memset", CallingConvention = (CallingConvention) 2)]
  private static extern void dje_zFX4HDMPMMWSHFCA_ejd(
    IntPtr dje_z94UNALHN_ejd,
    int dje_z74NFVDWP292AF7Z_ejd,
    int dje_zQXKRAKZG_ejd);

  protected static \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ \u0023\u003DzO_vyIsVdHIlfh82DU1Fga4A\u003D<TX, TY>(
    IList<TX> _param0,
    IList<TY> _param1,
    IndexRange  _param2,
    int _param3,
    bool _param4)
    where TX : IComparable
    where TY : IComparable
  {
    IMath<TX> mijfkOcK7kdYtA2avPae1 = MathHelper.GetMath<TX>();
    IMath<TY> mijfkOcK7kdYtA2avPae2 = MathHelper.GetMath<TY>();
    if (\u0023\u003DzK11CXzkQ3m66hjsjmkZfa7gEpyy6EjdRjsrA1yjQSXVyIo1W\u0024w\u003D\u003D.\u0023\u003DzpQ\u0024NBSHbFmS_ == null)
    {
      \u0023\u003DzK11CXzkQ3m66hjsjmkZfa7gEpyy6EjdRjsrA1yjQSXVyIo1W\u0024w\u003D\u003D.\u0023\u003DzpQ\u0024NBSHbFmS_ = new byte[120000];
    }
    else
    {
      GCHandle gcHandle = GCHandle.Alloc((object) \u0023\u003DzK11CXzkQ3m66hjsjmkZfa7gEpyy6EjdRjsrA1yjQSXVyIo1W\u0024w\u003D\u003D.\u0023\u003DzpQ\u0024NBSHbFmS_, GCHandleType.Pinned);
      \u0023\u003DzK11CXzkQ3m66hjsjmkZfa7gEpyy6EjdRjsrA1yjQSXVyIo1W\u0024w\u003D\u003D.dje_zFX4HDMPMMWSHFCA_ejd(gcHandle.AddrOfPinnedObject(), 0, \u0023\u003DzK11CXzkQ3m66hjsjmkZfa7gEpyy6EjdRjsrA1yjQSXVyIo1W\u0024w\u003D\u003D.\u0023\u003DzpQ\u0024NBSHbFmS_.Length);
      gcHandle.Free();
    }
    TX x1;
    TX x2;
    \u0023\u003DzE2B_RS0KvtqHnw_gRshK2XNMFe_3Evlr8vIKUruTJaum.\u0023\u003Dz\u0024zWmmGTAbDON<TX>((IEnumerable<TX>) _param0, out x1, out x2);
    double num1 = !x2.Equals((object) x1) ? 399.0 / mijfkOcK7kdYtA2avPae1.Subtract(x2, x1).ToDouble() : 0.0;
    TY y1;
    TY y2;
    \u0023\u003DzE2B_RS0KvtqHnw_gRshK2XNMFe_3Evlr8vIKUruTJaum.\u0023\u003Dz\u0024zWmmGTAbDON<TY>((IEnumerable<TY>) _param1, out y1, out y2);
    double num2 = !y2.Equals((object) y1) ? 299.0 / mijfkOcK7kdYtA2avPae2.Subtract(y2, y1).ToDouble() : 0.0;
    \u0023\u003DzRYm3Fw8jwwRKksCg00\u00244P09swszfkFaReRy0a_AtDN3C p09swszfkFaReRy0aAtDn3C = new \u0023\u003DzRYm3Fw8jwwRKksCg00\u00244P09swszfkFaReRy0a_AtDN3C(100);
    TX[] xArray = _param0.\u0023\u003Dz1bvQV4SZTWpA<TX>();
    TY[] yArray = _param1.\u0023\u003Dz1bvQV4SZTWpA<TY>();
    for (int index1 = 0; index1 < _param0.Count; ++index1)
    {
      TX x3 = xArray[index1];
      TY y3 = yArray[index1];
      int index2 = (int) (num1 * mijfkOcK7kdYtA2avPae1.Subtract(x3, x1).ToDouble()) + (int) (num2 * mijfkOcK7kdYtA2avPae2.Subtract(y3, y1).ToDouble()) * 400;
      if (\u0023\u003DzK11CXzkQ3m66hjsjmkZfa7gEpyy6EjdRjsrA1yjQSXVyIo1W\u0024w\u003D\u003D.\u0023\u003DzpQ\u0024NBSHbFmS_[index2] == (byte) 0)
      {
        \u0023\u003DzK11CXzkQ3m66hjsjmkZfa7gEpyy6EjdRjsrA1yjQSXVyIo1W\u0024w\u003D\u003D.\u0023\u003DzpQ\u0024NBSHbFmS_[index2] = (byte) 1;
        p09swszfkFaReRy0aAtDn3C.Add(new \u0023\u003DzPauio66DvxKtWOFEEHOV9eqtPYsuAYR8YjvqTxI\u003D(x3.ToDouble(), y3.ToDouble()));
      }
    }
    p09swszfkFaReRy0aAtDn3C.\u0023\u003DzK_5NMsk\u003D();
    return (\u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ) p09swszfkFaReRy0aAtDn3C;
  }
}
