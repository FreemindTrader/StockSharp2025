// Decompiled with JetBrains decompiler
// Type: #=zFgfHSvJTVKiBUeYgwcNjybqFg$G_l0RuWYSZ33g=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

#nullable enable
public static class \u0023\u003DzFgfHSvJTVKiBUeYgwcNjybqFg\u0024G_l0RuWYSZ33g\u003D
{
  public static 
  #nullable disable
  UIElement \u0023\u003DzjgOr4ajGBpa0(
    this UIElementCollection _param0,
    Predicate<UIElement> _param1)
  {
    foreach (object obj in _param0)
    {
      if (_param1((UIElement) obj))
        return (UIElement) obj;
    }
    return (UIElement) null;
  }

  public static DoubleRange \u0023\u003Dz1j51DAw\u003D(
    this IList<double> _param0)
  {
    double num1;
    double num2;
    \u0023\u003DzE2B_RS0KvtqHnw_gRshK2XNMFe_3Evlr8vIKUruTJaum.\u0023\u003Dz\u0024zWmmGTAbDON<double>(_param0 is \u0023\u003DzESFnl\u0024cpXbmAnH3LhxCsqSxgZhPFILnvQBugXV_GEszyTZ24Jw\u003D\u003D<double> ? (IEnumerable<double>) ((\u0023\u003DzESFnl\u0024cpXbmAnH3LhxCsqSxgZhPFILnvQBugXV_GEszyTZ24Jw\u003D\u003D<double>) _param0).\u0023\u003Dz3TQv53iUYmxB() : (IEnumerable<double>) _param0.\u0023\u003Dz1bvQV4SZTWpA<double>(), out num1, out num2);
    return new DoubleRange(num1, num2);
  }

  public static double[] \u0023\u003DzvczFDIa7rqI9<T>(this IList<T> _param0)
  {
    return _param0.\u0023\u003Dz1bvQV4SZTWpA<T>() as double[];
  }

  public static \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWuPneE7x_PdElYKA_LxaZfcJ<T> \u0023\u003Dz1bvQV4SZTWpA<T>(
    this IList<T> _param0,
    IndexRange  _param1,
    bool _param2)
  {
    int num = _param1.Max - _param1.Min + 1;
    switch (_param0)
    {
      case \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWuPneE7x_PdElYKA_LxaZfcJ<T> e7xPdElYkaLxaZfcJ:
        return new \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWuPneE7x_PdElYKA_LxaZfcJ<T>(e7xPdElYkaLxaZfcJ.\u0023\u003DzvsnCYl4\u003D(), _param1.Min + e7xPdElYkaLxaZfcJ.\u0023\u003DzOhT86Emh4umk(), num);
      case \u0023\u003Dzboj3ckhISv7k6koCkTeIfzSujzHmXzYCLKUgdFUczis\u0024<T> hmXzYclkUgdFuczis:
        return hmXzYclkUgdFuczis.\u0023\u003Dz1bvQV4SZTWpA(_param1.Min, num);
      case T[] objArray:
        return new \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWuPneE7x_PdElYKA_LxaZfcJ<T>(objArray, _param1.Min, num);
      case List<T> _:
        return new \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWuPneE7x_PdElYKA_LxaZfcJ<T>((T[]) typeof (List<T>).GetField("_items", BindingFlags.Instance | BindingFlags.NonPublic).GetValue((object) _param0), _param1.Min, num);
      default:
        return _param2 ? new \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWuPneE7x_PdElYKA_LxaZfcJ<T>(_param0.ToArray<T>(), _param1.Min, num) : (\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWuPneE7x_PdElYKA_LxaZfcJ<T>) null;
    }
  }

  public static \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWuPneE7x_PdElYKA_LxaZfcJ<T> \u0023\u003Dz1bvQV4SZTWpA<T>(
    this IList<T> _param0,
    bool _param1)
  {
    switch (_param0)
    {
      case \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWuPneE7x_PdElYKA_LxaZfcJ<T> e7xPdElYkaLxaZfcJ:
        return e7xPdElYkaLxaZfcJ;
      case \u0023\u003Dzboj3ckhISv7k6koCkTeIfzSujzHmXzYCLKUgdFUczis\u0024<T> hmXzYclkUgdFuczis:
        return hmXzYclkUgdFuczis.\u0023\u003Dz1bvQV4SZTWpA(0, hmXzYclkUgdFuczis.Count);
      case T[] objArray:
        return new \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWuPneE7x_PdElYKA_LxaZfcJ<T>(objArray);
      case List<T> _:
        return new \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWuPneE7x_PdElYKA_LxaZfcJ<T>((T[]) typeof (List<T>).GetField("_items", BindingFlags.Instance | BindingFlags.NonPublic).GetValue((object) _param0));
      default:
        return _param1 ? new \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWuPneE7x_PdElYKA_LxaZfcJ<T>(_param0.ToArray<T>()) : (\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWuPneE7x_PdElYKA_LxaZfcJ<T>) null;
    }
  }

  public static T[] \u0023\u003Dz1bvQV4SZTWpA<T>(this IList<T> _param0)
  {
    switch (_param0)
    {
      case \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqI7mlakqGSWX9govxQ\u0024cni0A<T> i7mlakqGswX9govxQCni0A:
        return i7mlakqGswX9govxQCni0A.\u0023\u003DzCQWqAOc\u003D();
      case T[] objArray:
        return objArray;
      case AbstractList<T> kcpLekFdw1PpSgxGl:
        return kcpLekFdw1PpSgxGl.\u0023\u003DzRr4AYdnHaTxa();
      case List<T> _:
        return (T[]) typeof (List<T>).GetField("_items", BindingFlags.Instance | BindingFlags.NonPublic).GetValue((object) _param0);
      case \u0023\u003DzESFnl\u0024cpXbmAnH3LhxCsqSxgZhPFILnvQBugXV_GEszyTZ24Jw\u003D\u003D<T> qbugXvGeszyTz24Jw:
        return qbugXvGeszyTz24Jw.\u0023\u003DzSWlLd4k\u003D();
      default:
        return _param0.ToArray<T>();
    }
  }

  public static bool \u0023\u003DzfFZjWlZiH0nd(this IList _param0)
  {
    return _param0 == null || _param0.Count == 0;
  }

  public static bool \u0023\u003DzCCMM80zDpO6N<T>(this IEnumerable<T> _param0)
  {
    return _param0 == null || !_param0.Any<T>();
  }

  public static bool \u0023\u003DzMeGSfVE\u003D<T>(this IEnumerable<T> _param0)
  {
    return !_param0.Any<T>();
  }

  public static void \u0023\u003Dz30RSSSygABj_<T>(this IEnumerable _param0, Action<T> _param1)
  {
    if (_param0 == null)
      return;
    ((IEnumerable<T>) _param0.OfType<IRenderableSeries>()).\u0023\u003Dz30RSSSygABj_<T>(_param1);
  }

  public static void \u0023\u003Dz30RSSSygABj_<T>(this IEnumerable<T> _param0, Action<T> _param1)
  {
    if (_param0 == null)
      return;
    \u0023\u003DzITX8mZ2jbGEtwuB21HaSb94StZu7BSE7Sw\u003D\u003D.\u0023\u003DzVDzEWto\u003D((object) _param1, "operation");
    foreach (T obj in _param0)
      _param1(obj);
  }

  public static void \u0023\u003DzmFyFyI4\u003D<T>(this IList<T> _param0, Predicate<T> _param1)
  {
    for (int index = 0; index < _param0.Count; ++index)
    {
      if (_param1(_param0[index]))
        _param0.RemoveAt(index--);
    }
  }

  public static void \u0023\u003Dzfhu56zPTf8It<T>(this IList<T> _param0, T _param1)
  {
    if (_param0.Contains(_param1))
      return;
    _param0.Add(_param1);
  }

  public static int \u0023\u003DzFH1yjjY\u003D<T>(
    this IList<T> _param0,
    bool _param1,
    IComparable _param2,
    \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D _param3)
    where T : IComparable
  {
    return ((IList) _param0).\u0023\u003DzFH1yjjY\u003D(_param1, _param2, _param3);
  }

  public static int \u0023\u003Dzk3Bi7DnEdNPh<T>(
    this IList<T> _param0,
    T _param1,
    IComparer<T> _param2)
  {
    if (_param0 == null)
      throw new ArgumentNullException("list");
    if (_param2 == null)
      _param2 = (IComparer<T>) Comparer<T>.Default;
    int num1 = 0;
    int num2 = _param0.Count - 1;
    while (num1 <= num2)
    {
      int index = num1 + (num2 - num1) / 2;
      int num3 = _param2.Compare(_param1, _param0[index]);
      if (num3 == 0)
        return index;
      if (num3 < 0)
        num2 = index - 1;
      else
        num1 = index + 1;
    }
    return ~num1;
  }

  public static int \u0023\u003DzFH1yjjY\u003D(
    this IList _param0,
    bool _param1,
    IComparable _param2,
    \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D _param3)
  {
    if (_param0 == null)
      throw new ArgumentNullException("list");
    if (_param2 == null)
      throw new ArgumentNullException("value");
    Comparer<IComparable> comparer = Comparer<IComparable>.Default;
    if (_param1)
      return _param0.\u0023\u003Dzfidg2fLfXmXy(_param2, (IComparer<IComparable>) comparer, _param3);
    if (_param3 == (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 0)
      return _param0.IndexOf((object) _param2);
    throw new NotImplementedException($"Unsorted data occurs in the collection. The only allowed SearchMode is {(\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 0} when FindIndex() is called on an unsorted collection, but {_param3} was passed in.");
  }

  private static int \u0023\u003Dzfidg2fLfXmXy(
    this IList _param0,
    IComparable _param1,
    IComparer<IComparable> _param2,
    \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D _param3)
  {
    if (_param0.Count == 0)
      return -1;
    if (_param2.Compare(_param1, (IComparable) _param0[0]) < 0)
      return _param3 != (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 0 ? 0 : -1;
    if (_param2.Compare(_param1, (IComparable) _param0[0]) == 0)
      return 0;
    if (_param2.Compare(_param1, (IComparable) _param0[_param0.Count - 1]) == 0)
      return _param0.Count - 1;
    if (_param2.Compare(_param1, (IComparable) _param0[_param0.Count - 1]) > 0)
      return _param3 != (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 0 ? _param0.Count - 1 : -1;
    int num1 = 0;
    int num2 = _param0.Count - 1;
    while (num1 <= num2)
    {
      int index = (num1 + num2) / 2;
      int num3 = _param2.Compare(_param1, (IComparable) _param0[index]);
      if (num3 == 0)
        return index;
      if (num3 < 0)
        num2 = index - 1;
      else
        num1 = index + 1;
    }
    if (_param3 == (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 0)
      return -1;
    if (_param3 == (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 1)
      return \u0023\u003DzFgfHSvJTVKiBUeYgwcNjybqFg\u0024G_l0RuWYSZ33g\u003D.\u0023\u003DzSU8kFJZ102Nc(_param0, num1, num2, _param1);
    int num4 = (num1 + num2) / 2;
    return _param3 != (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 2 ? num4 + 1 : num4;
  }

  public static int \u0023\u003Dzfidg2fLfXmXy<TX>(
    this TX[] _param0,
    int _param1,
    TX _param2,
    \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D _param3,
    IMath<TX> _param4)
    where TX : IComparable
  {
    if (_param1 == 0)
      return -1;
    ref TX local1 = ref _param2;
    TX x;
    if ((object) default (TX) == null)
    {
      x = local1;
      local1 = ref x;
    }
    // ISSUE: variable of a boxed type
    __Boxed<TX> local2 = (object) _param0[0];
    if (local1.CompareTo((object) local2) < 0)
      return _param3 != (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 0 ? 0 : -1;
    ref TX local3 = ref _param2;
    x = default (TX);
    if ((object) x == null)
    {
      x = local3;
      local3 = ref x;
    }
    // ISSUE: variable of a boxed type
    __Boxed<TX> local4 = (object) _param0[0];
    if (local3.CompareTo((object) local4) == 0)
      return 0;
    ref TX local5 = ref _param2;
    x = default (TX);
    if ((object) x == null)
    {
      x = local5;
      local5 = ref x;
    }
    // ISSUE: variable of a boxed type
    __Boxed<TX> local6 = (object) _param0[_param1 - 1];
    if (local5.CompareTo((object) local6) == 0)
      return _param1 - 1;
    ref TX local7 = ref _param2;
    x = default (TX);
    if ((object) x == null)
    {
      x = local7;
      local7 = ref x;
    }
    // ISSUE: variable of a boxed type
    __Boxed<TX> local8 = (object) _param0[_param1 - 1];
    if (local7.CompareTo((object) local8) > 0)
      return _param3 != (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 0 ? _param1 - 1 : -1;
    int num1 = 0;
    int num2 = _param1 - 1;
    while (num1 <= num2)
    {
      int index = (num1 + num2) / 2;
      ref TX local9 = ref _param2;
      x = default (TX);
      if ((object) x == null)
      {
        x = local9;
        local9 = ref x;
      }
      // ISSUE: variable of a boxed type
      __Boxed<TX> local10 = (object) _param0[index];
      int num3 = local9.CompareTo((object) local10);
      if (num3 == 0)
        return index;
      if (num3 < 0)
        num2 = index - 1;
      else
        num1 = index + 1;
    }
    if (_param3 == (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 0)
      return -1;
    if (_param3 == (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 1)
      return \u0023\u003DzFgfHSvJTVKiBUeYgwcNjybqFg\u0024G_l0RuWYSZ33g\u003D.\u0023\u003DzSU8kFJZ102Nc<TX>(_param0, _param1, num1, num2, _param2, _param4);
    int num4 = (num1 + num2) / 2;
    return _param3 != (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 2 ? num4 + 1 : num4;
  }

  private static int \u0023\u003DzSU8kFJZ102Nc(
    IList _param0,
    int _param1,
    int _param2,
    IComparable _param3)
  {
    if (_param1 > _param2)
    {
      int num = _param2;
      _param2 = _param1;
      _param1 = num;
    }
    _param2 = NumberUtil.Constrain(_param2, 0, _param0.Count - 1);
    _param1 = NumberUtil.Constrain(_param1, 0, _param0.Count - 1);
    double num1 = ((IComparable) _param0[_param1]).ToDouble();
    double num2 = ((IComparable) _param0[_param2]).ToDouble();
    double num3 = _param3.ToDouble();
    return num3 - num1 < num2 - num3 ? _param1 : _param2;
  }

  private static int \u0023\u003DzSU8kFJZ102Nc<TX>(
    TX[] _param0,
    int _param1,
    int _param2,
    int _param3,
    TX _param4,
    IMath<TX> _param5)
    where TX : IComparable
  {
    if (_param2 > _param3)
    {
      int num = _param3;
      _param3 = _param2;
      _param2 = num;
    }
    _param3 = NumberUtil.Constrain(_param3, 0, _param1 - 1);
    _param2 = NumberUtil.Constrain(_param2, 0, _param1 - 1);
    TX x1 = _param0[_param2];
    TX x2 = _param0[_param3];
    TX x3 = _param4;
    return _param5.Subtract(x3, x1).CompareTo((object) _param5.Subtract(x2, x3)) < 0 ? _param2 : _param3;
  }

  public static void \u0023\u003Dz6_E5\u0024pE\u003D<T>(
    this ObservableCollection<T> _param0,
    IEnumerable<T> _param1)
  {
    foreach (T obj in _param1)
      _param0.Add(obj);
  }

  public static IEnumerable<IEnumerable<Tuple<Point, Point>>> \u0023\u003Dz4tVt5y5Hyap1kzcfrQ\u003D\u003D(
    this IEnumerable<Tuple<Point, Point>> _param0)
  {
    return (IEnumerable<IEnumerable<Tuple<Point, Point>>>) new \u0023\u003DzFgfHSvJTVKiBUeYgwcNjybqFg\u0024G_l0RuWYSZ33g\u003D.\u0023\u003Dz\u0024gu2ELXOMdDr0DdMZBFIY6iwkWU5(-2)
    {
      \u0023\u003DziwMdY_txwoDC = _param0
    };
  }

  private static bool \u0023\u003Dz8vUQEQweU6cO6OJIlg\u003D\u003D(
    IEnumerator<Tuple<Point, Point>> _param0)
  {
    if (!double.IsNaN(_param0.Current.Item1.X) && !double.IsNaN(_param0.Current.Item1.Y))
      return true;
    while (_param0.MoveNext())
    {
      if (!double.IsNaN(_param0.Current.Item1.X) && !double.IsNaN(_param0.Current.Item1.Y))
        return true;
    }
    return false;
  }

  private static IEnumerable<Tuple<Point, Point>> \u0023\u003DzZiM76vJudvEb(
    IEnumerator<Tuple<Point, Point>> _param0)
  {
    return (IEnumerable<Tuple<Point, Point>>) new \u0023\u003DzFgfHSvJTVKiBUeYgwcNjybqFg\u0024G_l0RuWYSZ33g\u003D.\u0023\u003DzGOma7xbMB971rH6q7VbpjKc\u003D(-2)
    {
      \u0023\u003DzUG8TIoNNA_H_ = _param0
    };
  }

  public static IEnumerable<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D> \u0023\u003DzcxxcgCCxILAWoUgLzQ\u003D\u003D(
    this IEnumerable<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D> _param0)
  {
    return (IEnumerable<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D>) new \u0023\u003DzFgfHSvJTVKiBUeYgwcNjybqFg\u0024G_l0RuWYSZ33g\u003D.\u0023\u003DzHEaN982cjGylL4EpsyeBIP4\u003D(-2)
    {
      \u0023\u003DzkJE8qXgCdeO6 = _param0
    };
  }

  public static T? \u0023\u003DzAAksTMXIKE7d<T>(this IEnumerable<T> _param0) where T : struct, IComparable
  {
    return !_param0.Any<T>() ? new T?() : new T?(_param0.Max<T>());
  }

  public static T? \u0023\u003Dz98VKYrYGa2Pn<T>(this IEnumerable<T> _param0) where T : struct, IComparable
  {
    return !_param0.Any<T>() ? new T?() : new T?(_param0.Min<T>());
  }

  private sealed class \u0023\u003Dz\u0024gu2ELXOMdDr0DdMZBFIY6iwkWU5 : 
    IDisposable,
    IEnumerable,
    IEnumerator,
    IEnumerable<IEnumerable<Tuple<Point, Point>>>,
    IEnumerator<IEnumerable<Tuple<Point, Point>>>
  {
    
    private int \u0023\u003Dz4fzyEZ1SsHYa;
    
    private IEnumerable<Tuple<Point, Point>> \u0023\u003Dzaev1bhaFFIDX;
    
    private int \u0023\u003DzFd7NMxipJEwB36N0OA\u003D\u003D;
    
    private IEnumerable<Tuple<Point, Point>> \u0023\u003DzGB9Q4U0\u003D;
    
    public IEnumerable<Tuple<Point, Point>> \u0023\u003DziwMdY_txwoDC;
    
    private IEnumerator<Tuple<Point, Point>> \u0023\u003DzQ4y6uPO8vKMGT9eMZA\u003D\u003D;

    [DebuggerHidden]
    public \u0023\u003Dz\u0024gu2ELXOMdDr0DdMZBFIY6iwkWU5(int _param1)
    {
      this.\u0023\u003Dz4fzyEZ1SsHYa = _param1;
      this.\u0023\u003DzFd7NMxipJEwB36N0OA\u003D\u003D = Environment.CurrentManagedThreadId;
    }

    [DebuggerHidden]
    void IDisposable.Dispose()
    {
    }

    bool IEnumerator.MoveNext()
    {
      switch (this.\u0023\u003Dz4fzyEZ1SsHYa)
      {
        case 0:
          this.\u0023\u003Dz4fzyEZ1SsHYa = -1;
          this.\u0023\u003DzQ4y6uPO8vKMGT9eMZA\u003D\u003D = this.\u0023\u003DzGB9Q4U0\u003D.GetEnumerator();
          if (!this.\u0023\u003DzQ4y6uPO8vKMGT9eMZA\u003D\u003D.MoveNext())
            goto label_7;
          break;
        case 1:
          this.\u0023\u003Dz4fzyEZ1SsHYa = -1;
          if (!this.\u0023\u003DzQ4y6uPO8vKMGT9eMZA\u003D\u003D.MoveNext())
            return false;
          break;
        default:
          return false;
      }
      if (\u0023\u003DzFgfHSvJTVKiBUeYgwcNjybqFg\u0024G_l0RuWYSZ33g\u003D.\u0023\u003Dz8vUQEQweU6cO6OJIlg\u003D\u003D(this.\u0023\u003DzQ4y6uPO8vKMGT9eMZA\u003D\u003D))
      {
        this.\u0023\u003Dzaev1bhaFFIDX = \u0023\u003DzFgfHSvJTVKiBUeYgwcNjybqFg\u0024G_l0RuWYSZ33g\u003D.\u0023\u003DzZiM76vJudvEb(this.\u0023\u003DzQ4y6uPO8vKMGT9eMZA\u003D\u003D);
        this.\u0023\u003Dz4fzyEZ1SsHYa = 1;
        return true;
      }
label_7:
      return false;
    }

    [DebuggerHidden]
    IEnumerable<Tuple<Point, Point>> IEnumerator<IEnumerable<Tuple<Point, Point>>>.\u0023\u003Dqa_8nKR_u8eeTxOkd331Xb6IwStE8NmJlOgTZ2bRVHIFZkR\u0024Swr69rO8CMw8GX5F6b8RK\u0024syTW5x8pQBogo7YZtVbJy7lewq0n4006n\u0024D3YMmQJzjbyxuunrMkkB3TDBA6cx8KhUDMqmK0y1VLBF6QKiGsqsSe2njVRUjncpivu\u002430GgR0mhTpSL_otNAlAey9qB_dQXpcv6IXjwaAVYkwg\u003D\u003D()
    {
      return this.\u0023\u003Dzaev1bhaFFIDX;
    }

    [DebuggerHidden]
    void IEnumerator.\u0023\u003Dz__yDkd4DQlAhNe9vxQ\u003D\u003D()
    {
      throw new NotSupportedException();
    }

    [DebuggerHidden]
    object IEnumerator.\u0023\u003DzmTTmbxIPszmU9qSLsSEbSqM\u003D()
    {
      return (object) this.\u0023\u003Dzaev1bhaFFIDX;
    }

    [DebuggerHidden]
    #nullable enable
    IEnumerator<
    #nullable disable
    IEnumerable<Tuple<Point, Point>>> IEnumerable<IEnumerable<Tuple<Point, Point>>>.\u0023\u003DqbrYisYiOdYdVwF4adSdE9StcqGM7dvZ4F0Tpf1elSXSIwGEIUgD_kn\u0024EqrOKgsCppqfMADX4Ch9i46Q45iqmy5sRFmymUO0ZupLkz7AhmCJd3F73IL6\u0024jGCE7Pq7rsUFgcaZGS5OmKMhZvSSzGKXk6yGPI3AGEoqAOXcsAFlu4y7o24WnTcbLiV0e87p_bL2HiaAXUnElWczTCQXVfs_Mg\u003D\u003D()
    {
      \u0023\u003DzFgfHSvJTVKiBUeYgwcNjybqFg\u0024G_l0RuWYSZ33g\u003D.\u0023\u003Dz\u0024gu2ELXOMdDr0DdMZBFIY6iwkWU5 dr0DdMzbfiY6iwkWu5;
      if (this.\u0023\u003Dz4fzyEZ1SsHYa == -2 && this.\u0023\u003DzFd7NMxipJEwB36N0OA\u003D\u003D == Environment.CurrentManagedThreadId)
      {
        this.\u0023\u003Dz4fzyEZ1SsHYa = 0;
        dr0DdMzbfiY6iwkWu5 = this;
      }
      else
        dr0DdMzbfiY6iwkWu5 = new \u0023\u003DzFgfHSvJTVKiBUeYgwcNjybqFg\u0024G_l0RuWYSZ33g\u003D.\u0023\u003Dz\u0024gu2ELXOMdDr0DdMZBFIY6iwkWU5(0);
      dr0DdMzbfiY6iwkWu5.\u0023\u003DzGB9Q4U0\u003D = this.\u0023\u003DziwMdY_txwoDC;
      return (IEnumerator<IEnumerable<Tuple<Point, Point>>>) dr0DdMzbfiY6iwkWu5;
    }

    [DebuggerHidden]
    #nullable enable
    IEnumerator IEnumerable.\u0023\u003DzSV_TZe7ftMh2SQ86i417Nok\u003D()
    {
      return (IEnumerator) this.\u0023\u003DqbrYisYiOdYdVwF4adSdE9StcqGM7dvZ4F0Tpf1elSXSIwGEIUgD_kn\u0024EqrOKgsCppqfMADX4Ch9i46Q45iqmy5sRFmymUO0ZupLkz7AhmCJd3F73IL6\u0024jGCE7Pq7rsUFgcaZGS5OmKMhZvSSzGKXk6yGPI3AGEoqAOXcsAFlu4y7o24WnTcbLiV0e87p_bL2HiaAXUnElWczTCQXVfs_Mg\u003D\u003D();
    }
  }

  private sealed class \u0023\u003DzGOma7xbMB971rH6q7VbpjKc\u003D : 
    IDisposable,
    IEnumerable,
    IEnumerator,
    IEnumerable<
    #nullable disable
    Tuple<Point, Point>>,
    IEnumerator<Tuple<Point, Point>>
  {
    
    private int \u0023\u003Dz4fzyEZ1SsHYa;
    
    private Tuple<Point, Point> \u0023\u003Dzaev1bhaFFIDX;
    
    private int \u0023\u003DzFd7NMxipJEwB36N0OA\u003D\u003D;
    
    private IEnumerator<Tuple<Point, Point>> \u0023\u003DzDQL0BA0\u003D;
    
    public IEnumerator<Tuple<Point, Point>> \u0023\u003DzUG8TIoNNA_H_;

    [DebuggerHidden]
    public \u0023\u003DzGOma7xbMB971rH6q7VbpjKc\u003D(int _param1)
    {
      this.\u0023\u003Dz4fzyEZ1SsHYa = _param1;
      this.\u0023\u003DzFd7NMxipJEwB36N0OA\u003D\u003D = Environment.CurrentManagedThreadId;
    }

    [DebuggerHidden]
    void IDisposable.Dispose()
    {
    }

    bool IEnumerator.MoveNext()
    {
      switch (this.\u0023\u003Dz4fzyEZ1SsHYa)
      {
        case 0:
          this.\u0023\u003Dz4fzyEZ1SsHYa = -1;
          if (!double.IsNaN(this.\u0023\u003DzDQL0BA0\u003D.Current.Item1.X) && !double.IsNaN(this.\u0023\u003DzDQL0BA0\u003D.Current.Item1.Y))
          {
            this.\u0023\u003Dzaev1bhaFFIDX = this.\u0023\u003DzDQL0BA0\u003D.Current;
            this.\u0023\u003Dz4fzyEZ1SsHYa = 1;
            return true;
          }
          goto label_8;
        case 1:
          this.\u0023\u003Dz4fzyEZ1SsHYa = -1;
          break;
        case 2:
          this.\u0023\u003Dz4fzyEZ1SsHYa = -1;
          break;
        default:
          return false;
      }
      if (this.\u0023\u003DzDQL0BA0\u003D.MoveNext() && !double.IsNaN(this.\u0023\u003DzDQL0BA0\u003D.Current.Item1.X) && !double.IsNaN(this.\u0023\u003DzDQL0BA0\u003D.Current.Item1.Y))
      {
        this.\u0023\u003Dzaev1bhaFFIDX = this.\u0023\u003DzDQL0BA0\u003D.Current;
        this.\u0023\u003Dz4fzyEZ1SsHYa = 2;
        return true;
      }
label_8:
      return false;
    }

    [DebuggerHidden]
    Tuple<Point, Point> IEnumerator<Tuple<Point, Point>>.\u0023\u003DqzmvT3SLBreOhtuTH2jio0LRqa7OreBHKBUqRs94Q8O_4f0D7G0OVXa1024pNFfW6\u0024MjRHwilvOewtq2Dw9o7gBZpmopSlFNuiOxpbcX_jFscvg9bOnScacPbNkocWLt25Z5UvGEdbku6ACEPbhAUuA\u003D\u003D()
    {
      return this.\u0023\u003Dzaev1bhaFFIDX;
    }

    [DebuggerHidden]
    void IEnumerator.\u0023\u003Dz__yDkd4DQlAhNe9vxQ\u003D\u003D()
    {
      throw new NotSupportedException();
    }

    [DebuggerHidden]
    object IEnumerator.\u0023\u003DzmTTmbxIPszmU9qSLsSEbSqM\u003D()
    {
      return (object) this.\u0023\u003Dzaev1bhaFFIDX;
    }

    [DebuggerHidden]
    #nullable enable
    IEnumerator<
    #nullable disable
    Tuple<Point, Point>> IEnumerable<Tuple<Point, Point>>.\u0023\u003Dqa_8nKR_u8eeTxOkd331Xb6IwStE8NmJlOgTZ2bRVHIH1JABDf0IqbxVW9sQ9fpZ9ie52JATSFTkkrLrBBEfDWpGyCleNSgtcwlCJu75IvIdZgqm9_Cs3RCYq2yXCujFeZ_lqX2F4W1u4v4rVgzg\u00241w\u003D\u003D()
    {
      \u0023\u003DzFgfHSvJTVKiBUeYgwcNjybqFg\u0024G_l0RuWYSZ33g\u003D.\u0023\u003DzGOma7xbMB971rH6q7VbpjKc\u003D mb971rH6q7VbpjKc;
      if (this.\u0023\u003Dz4fzyEZ1SsHYa == -2 && this.\u0023\u003DzFd7NMxipJEwB36N0OA\u003D\u003D == Environment.CurrentManagedThreadId)
      {
        this.\u0023\u003Dz4fzyEZ1SsHYa = 0;
        mb971rH6q7VbpjKc = this;
      }
      else
        mb971rH6q7VbpjKc = new \u0023\u003DzFgfHSvJTVKiBUeYgwcNjybqFg\u0024G_l0RuWYSZ33g\u003D.\u0023\u003DzGOma7xbMB971rH6q7VbpjKc\u003D(0);
      mb971rH6q7VbpjKc.\u0023\u003DzDQL0BA0\u003D = this.\u0023\u003DzUG8TIoNNA_H_;
      return (IEnumerator<Tuple<Point, Point>>) mb971rH6q7VbpjKc;
    }

    [DebuggerHidden]
    #nullable enable
    IEnumerator IEnumerable.\u0023\u003DzSV_TZe7ftMh2SQ86i417Nok\u003D()
    {
      return (IEnumerator) this.\u0023\u003Dqa_8nKR_u8eeTxOkd331Xb6IwStE8NmJlOgTZ2bRVHIH1JABDf0IqbxVW9sQ9fpZ9ie52JATSFTkkrLrBBEfDWpGyCleNSgtcwlCJu75IvIdZgqm9_Cs3RCYq2yXCujFeZ_lqX2F4W1u4v4rVgzg\u00241w\u003D\u003D();
    }
  }

  private sealed class \u0023\u003DzHEaN982cjGylL4EpsyeBIP4\u003D : 
    IDisposable,
    IEnumerable,
    IEnumerator,
    IEnumerable<
    #nullable disable
    \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D>,
    IEnumerator<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D>
  {
    
    private int \u0023\u003Dz4fzyEZ1SsHYa;
    
    private \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D \u0023\u003Dzaev1bhaFFIDX;
    
    private int \u0023\u003DzFd7NMxipJEwB36N0OA\u003D\u003D;
    
    private IEnumerable<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D> \u0023\u003Dzw\u0024Dv7kw\u003D;
    
    public IEnumerable<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D> \u0023\u003DzkJE8qXgCdeO6;
    
    private IEnumerator<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D> \u0023\u003DzezsfKEm_Vb7b\u0024HR11w\u003D\u003D;

    [DebuggerHidden]
    public \u0023\u003DzHEaN982cjGylL4EpsyeBIP4\u003D(int _param1)
    {
      this.\u0023\u003Dz4fzyEZ1SsHYa = _param1;
      this.\u0023\u003DzFd7NMxipJEwB36N0OA\u003D\u003D = Environment.CurrentManagedThreadId;
    }

    [DebuggerHidden]
    void IDisposable.Dispose()
    {
    }

    bool IEnumerator.MoveNext()
    {
      switch (this.\u0023\u003Dz4fzyEZ1SsHYa)
      {
        case 0:
          this.\u0023\u003Dz4fzyEZ1SsHYa = -1;
          this.\u0023\u003DzezsfKEm_Vb7b\u0024HR11w\u003D\u003D = this.\u0023\u003Dzw\u0024Dv7kw\u003D.GetEnumerator();
          goto label_8;
        case 1:
          this.\u0023\u003Dz4fzyEZ1SsHYa = -1;
          break;
        case 2:
          this.\u0023\u003Dz4fzyEZ1SsHYa = -1;
          goto label_8;
        default:
          return false;
      }
label_6:
      this.\u0023\u003Dzaev1bhaFFIDX = this.\u0023\u003DzezsfKEm_Vb7b\u0024HR11w\u003D\u003D.Current;
      this.\u0023\u003Dz4fzyEZ1SsHYa = 2;
      return true;
label_8:
      if (!this.\u0023\u003DzezsfKEm_Vb7b\u0024HR11w\u003D\u003D.MoveNext())
        return false;
      if (this.\u0023\u003DzezsfKEm_Vb7b\u0024HR11w\u003D\u003D.Current is \u0023\u003DzEJoJjwSelM_K3zbmiw1OA_rAdtq\u00247psDWA\u003D\u003D current)
      {
        \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D zldchDrVsrVyHh6WyiGy1 = new \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D();
        zldchDrVsrVyHh6WyiGy1.\u0023\u003DzBswzhzuQHrrX(current.Y1Value);
        zldchDrVsrVyHh6WyiGy1.\u0023\u003Dz3JT1kQLA9WwW(current.YValue);
        zldchDrVsrVyHh6WyiGy1.\u0023\u003Dzo2ftAfxjqC04(current.Xy1Coordinate);
        zldchDrVsrVyHh6WyiGy1.\u0023\u003Dz2Iv\u0024sxQuGDBR(current.XValue);
        zldchDrVsrVyHh6WyiGy1.\u0023\u003DzV4wgjRUOXtRf(current.DataSeriesIndex);
        zldchDrVsrVyHh6WyiGy1.\u0023\u003DzQ9xCEGz0Gl\u0024q(current.DataSeriesType);
        zldchDrVsrVyHh6WyiGy1.\u0023\u003Dzn3o1RS9wuET8(current.IsHit);
        \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D zldchDrVsrVyHh6WyiGy2 = zldchDrVsrVyHh6WyiGy1;
        this.\u0023\u003Dzaev1bhaFFIDX = (\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D) new \u0023\u003DzEJoJjwSelM_K3zbmiw1OA_rAdtq\u00247psDWA\u003D\u003D(current.RenderableSeries, zldchDrVsrVyHh6WyiGy2)
        {
          IsFirstSeries = true
        };
        this.\u0023\u003Dz4fzyEZ1SsHYa = 1;
        return true;
      }
      goto label_6;
    }

    [DebuggerHidden]
    \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D IEnumerator<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D>.\u0023\u003Dz4\u0024vcRAdnkf8XAMQ6U6I6aJAVdQshEFF3YrEuKf9hCSePAnpiCKyv8pQ\u003D()
    {
      return this.\u0023\u003Dzaev1bhaFFIDX;
    }

    [DebuggerHidden]
    void IEnumerator.\u0023\u003Dz__yDkd4DQlAhNe9vxQ\u003D\u003D()
    {
      throw new NotSupportedException();
    }

    [DebuggerHidden]
    object IEnumerator.\u0023\u003DzmTTmbxIPszmU9qSLsSEbSqM\u003D()
    {
      return (object) this.\u0023\u003Dzaev1bhaFFIDX;
    }

    [DebuggerHidden]
    #nullable enable
    IEnumerator<
    #nullable disable
    \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D> IEnumerable<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D>.\u0023\u003DzdCr_FD\u0024knmYtmaB5oybNnJEENtOXJw7Q1xWzDzSUVGdOwwA2iYK9I4g\u003D()
    {
      \u0023\u003DzFgfHSvJTVKiBUeYgwcNjybqFg\u0024G_l0RuWYSZ33g\u003D.\u0023\u003DzHEaN982cjGylL4EpsyeBIP4\u003D n982cjGylL4EpsyeBiP4;
      if (this.\u0023\u003Dz4fzyEZ1SsHYa == -2 && this.\u0023\u003DzFd7NMxipJEwB36N0OA\u003D\u003D == Environment.CurrentManagedThreadId)
      {
        this.\u0023\u003Dz4fzyEZ1SsHYa = 0;
        n982cjGylL4EpsyeBiP4 = this;
      }
      else
        n982cjGylL4EpsyeBiP4 = new \u0023\u003DzFgfHSvJTVKiBUeYgwcNjybqFg\u0024G_l0RuWYSZ33g\u003D.\u0023\u003DzHEaN982cjGylL4EpsyeBIP4\u003D(0);
      n982cjGylL4EpsyeBiP4.\u0023\u003Dzw\u0024Dv7kw\u003D = this.\u0023\u003DzkJE8qXgCdeO6;
      return (IEnumerator<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D>) n982cjGylL4EpsyeBiP4;
    }

    [DebuggerHidden]
    #nullable enable
    IEnumerator IEnumerable.\u0023\u003DzSV_TZe7ftMh2SQ86i417Nok\u003D()
    {
      return (IEnumerator) this.\u0023\u003DzdCr_FD\u0024knmYtmaB5oybNnJEENtOXJw7Q1xWzDzSUVGdOwwA2iYK9I4g\u003D();
    }
  }
}
