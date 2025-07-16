// Decompiled with JetBrains decompiler
// Type: #=zAuXtmwo_UFdzWVVSiImlM6CrNZf0Y$AoldqYWswSWHE1DEHxuAfkMq_BMaE9
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

#nullable disable
public abstract class \u0023\u003DzAuXtmwo_UFdzWVVSiImlM6CrNZf0Y\u0024AoldqYWswSWHE1DEHxuAfkMq_BMaE9<T> : 
  \u0023\u003DzjIfS4CbXGFDPWmVOPAZGmiedXllCPFuEn7L1_DWbHW6rxkxNJjBPTR5rC4Mn<T>
  where T : \u0023\u003Dz5B3gvTTfbmLYjDPhZPGfZJtupKNFWXP0_On1YUVI0hqJ\u0024lotV9V57okcKlXHXNUKOsbYO\u0024c\u003D
{
  protected readonly List<Tuple<string, List<T>>> \u0023\u003DzSgM2QmPFjPGL;
  protected readonly List<T> \u0023\u003DzaXL4UJQ\u003D;
  protected int \u0023\u003DzIbVn\u0024T8\u003D;

  protected \u0023\u003DzAuXtmwo_UFdzWVVSiImlM6CrNZf0Y\u0024AoldqYWswSWHE1DEHxuAfkMq_BMaE9()
  {
    this.\u0023\u003DzaXL4UJQ\u003D = new List<T>();
    this.\u0023\u003DzSgM2QmPFjPGL = new List<Tuple<string, List<T>>>();
  }

  public int \u0023\u003DzBRQKPSBN1vq7() => this.\u0023\u003DzaXL4UJQ\u003D.Count;

  public DoubleRange \u0023\u003DzzMId\u0024f67Wftb(
    T _param1,
    IndexRange  _param2)
  {
    double zeroLineY1 = _param1.ZeroLineY;
    double zeroLineY2 = _param1.ZeroLineY;
    int num1 = Math.Max(_param2.Min, 0);
    int num2 = Math.Min(_param2.Max, _param1.get_DataSeries().\u0023\u003DzwQnyySN6xaVC().Count - 1);
    for (int index = num1; index <= num2; ++index)
    {
      Tuple<double, double> tuple = this.\u0023\u003DzeKx7SKdwYOP2(_param1, index);
      if (tuple.Item1 > 0.0 && tuple.Item1 > zeroLineY1)
        zeroLineY1 = tuple.Item1;
      if (tuple.Item1 < 0.0 && tuple.Item1 < zeroLineY2)
        zeroLineY2 = tuple.Item1;
    }
    return \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDlB0n65RlWCGw\u003D\u003D.\u0023\u003DzLc65\u0024pc\u003D((IComparable) zeroLineY2, (IComparable) zeroLineY1).AsDoubleRange();
  }

  public Tuple<double, double> \u0023\u003DzeKx7SKdwYOP2(
    T _param1,
    int _param2,
    bool _param3 = false)
  {
    double num1 = _param1.ZeroLineY;
    double num2 = _param1.ZeroLineY;
    double num3 = 0.0;
    double num4 = 0.0;
    IList<T> source = this.\u0023\u003DzaqOTdcE2Qrcnms5YPQ\u003D\u003D(_param1.get_StackedGroupId());
    bool flag = this.\u0023\u003Dz4nhIf8\u0024OuuPux\u0024xOhCjDsBI\u003D(_param1.get_StackedGroupId());
    Func<T, bool> predicate = \u0023\u003DzAuXtmwo_UFdzWVVSiImlM6CrNZf0Y\u0024AoldqYWswSWHE1DEHxuAfkMq_BMaE9<T>.SomeClass34343383.\u0023\u003Dzbt6wp4trpUeqHhxsrQ\u003D\u003D ?? (\u0023\u003DzAuXtmwo_UFdzWVVSiImlM6CrNZf0Y\u0024AoldqYWswSWHE1DEHxuAfkMq_BMaE9<T>.SomeClass34343383.\u0023\u003Dzbt6wp4trpUeqHhxsrQ\u003D\u003D = new Func<T, bool>(\u0023\u003DzAuXtmwo_UFdzWVVSiImlM6CrNZf0Y\u0024AoldqYWswSWHE1DEHxuAfkMq_BMaE9<T>.SomeClass34343383.SomeMethond0343.\u0023\u003DzLZvPTS5COc90bGo0i3aY9EE\u003D));
    foreach (T zH9Hnkng in source.Where<T>(predicate))
    {
      double num5 = _param3 ? zH9Hnkng.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzSKfyjpipx8dI().\u0023\u003DzPqsSI6C5MOOb()[_param2].ToDouble() : ((IComparable) zH9Hnkng.get_DataSeries().\u0023\u003DzPqsSI6C5MOOb()[_param2]).ToDouble();
      if (!NumberUtil.IsNaN(num5))
      {
        if (num5 >= 0.0)
          num3 += num5;
        else if (num5 < 0.0)
          num4 += num5;
        if ((object) zH9Hnkng == (object) _param1)
        {
          num1 = num5 >= 0.0 ? num3 : num4;
          num1 += _param1.ZeroLineY;
          num2 = num1 - num5;
          if (!flag)
            break;
        }
      }
    }
    if (flag)
    {
      double num6 = num3 - num4;
      num1 = num1 * 100.0 / num6;
      num2 = num2 * 100.0 / num6;
    }
    return new Tuple<double, double>(num1, num2);
  }

  public bool \u0023\u003Dz4nhIf8\u0024OuuPux\u0024xOhCjDsBI\u003D(string _param1)
  {
    return this.\u0023\u003DzaqOTdcE2Qrcnms5YPQ\u003D\u003D(_param1)[0].get_IsOneHundredPercent();
  }

  public IRange \u0023\u003Dz7rfbi2bO7HOZ(
    T _param1,
    Func<T, double> _param2)
  {
    double num1 = 0.0;
    double num2 = 0.0;
    foreach (T zH9Hnkng in this.\u0023\u003DzaqOTdcE2Qrcnms5YPQ\u003D\u003D(_param1.get_StackedGroupId()).Where<T>(\u0023\u003DzAuXtmwo_UFdzWVVSiImlM6CrNZf0Y\u0024AoldqYWswSWHE1DEHxuAfkMq_BMaE9<T>.SomeClass34343383.\u0023\u003DzjJ3vlfxVH92KtuUzew\u003D\u003D ?? (\u0023\u003DzAuXtmwo_UFdzWVVSiImlM6CrNZf0Y\u0024AoldqYWswSWHE1DEHxuAfkMq_BMaE9<T>.SomeClass34343383.\u0023\u003DzjJ3vlfxVH92KtuUzew\u003D\u003D = new Func<T, bool>(\u0023\u003DzAuXtmwo_UFdzWVVSiImlM6CrNZf0Y\u0024AoldqYWswSWHE1DEHxuAfkMq_BMaE9<T>.SomeClass34343383.SomeMethond0343.\u0023\u003Dz9Z5iGcTB1hN7ZnRz61x\u0024KhY\u003D))))
    {
      double num3 = _param2(zH9Hnkng);
      if (!NumberUtil.IsNaN(num3))
      {
        if (num3 > 0.0)
          num1 += num3;
        else
          num2 += num3;
      }
    }
    return \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDlB0n65RlWCGw\u003D\u003D.\u0023\u003DzLc65\u0024pc\u003D((IComparable) num2, (IComparable) num1);
  }

  public abstract void \u0023\u003DzNi0XCnZpx1ge(
    IRenderContext2D _param1);

  public virtual HitTestInfo \u0023\u003Dznnv4eJBaYYey(
    Point _param1,
    HitTestInfo _param2,
    double _param3,
    T _param4)
  {
    if (_param4.get_DataSeries() != null && _param4.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D() != null && !_param2.\u0023\u003DzMeGSfVE\u003D())
    {
      bool flag1 = _param4.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzDoU1CJhSUWFV();
      Point point1 = \u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26h0hvlV4LfsI5sS6QMD39it_JzJq2Auui18\u003D.\u0023\u003Dzop6vn0GowyiR(_param2.\u0023\u003DzxZfJER0dbHuS(), flag1);
      double x = point1.X;
      double num = point1.Y;
      bool flag2 = this.\u0023\u003Dz4nhIf8\u0024OuuPux\u0024xOhCjDsBI\u003D(_param4.get_StackedGroupId());
      if (this.\u0023\u003DzaqOTdcE2Qrcnms5YPQ\u003D\u003D(_param4.get_StackedGroupId()).Count<T>(\u0023\u003DzAuXtmwo_UFdzWVVSiImlM6CrNZf0Y\u0024AoldqYWswSWHE1DEHxuAfkMq_BMaE9<T>.SomeClass34343383.\u0023\u003DzaObIckm5bO9Zm0ifDA\u003D\u003D ?? (\u0023\u003DzAuXtmwo_UFdzWVVSiImlM6CrNZf0Y\u0024AoldqYWswSWHE1DEHxuAfkMq_BMaE9<T>.SomeClass34343383.\u0023\u003DzaObIckm5bO9Zm0ifDA\u003D\u003D = new Func<T, bool>(\u0023\u003DzAuXtmwo_UFdzWVVSiImlM6CrNZf0Y\u0024AoldqYWswSWHE1DEHxuAfkMq_BMaE9<T>.SomeClass34343383.SomeMethond0343.\u0023\u003DzTGuhZLTU3_PDZ4NVc9Rm2zk\u003D))) > 1 | flag2)
      {
        \u0023\u003DzAuXtmwo_UFdzWVVSiImlM6CrNZf0Y\u0024AoldqYWswSWHE1DEHxuAfkMq_BMaE9<T>.\u0023\u003DzHF4K0hz6MwLRPm7JFGVw01g\u003D k0hz6MwLrPm7JfgVw01g = new \u0023\u003DzAuXtmwo_UFdzWVVSiImlM6CrNZf0Y\u0024AoldqYWswSWHE1DEHxuAfkMq_BMaE9<T>.\u0023\u003DzHF4K0hz6MwLRPm7JFGVw01g\u003D();
        k0hz6MwLrPm7JfgVw01g.\u0023\u003Dzay7IytM\u003D = _param2.\u0023\u003DzSkvCFWUKQ7Fw();
        Tuple<double, double> tuple = this.\u0023\u003DzeKx7SKdwYOP2(_param4, k0hz6MwLrPm7JfgVw01g.\u0023\u003Dzay7IytM\u003D);
        num = _param4.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D().\u0023\u003DzhL6gsJw\u003D(tuple.Item1);
        if (flag2)
        {
          IRange abyLt9clZggmJsWhw = this.\u0023\u003Dz7rfbi2bO7HOZ(_param4, new Func<T, double>(k0hz6MwLrPm7JfgVw01g.\u0023\u003DzA7RktUSISABNMw2sUfBVsa8\u003D));
          _param2.\u0023\u003DzMG0PUrAJ2_dYZ38QT2CTbNc\u003D((double) _param2.\u0023\u003Dzd9IAScWutAfJ() / (double) abyLt9clZggmJsWhw.Diff * 100.0);
          _param2.\u0023\u003DzQ9xCEGz0Gl\u0024q((DataSeriesType) 8);
        }
        _param2.\u0023\u003Dz3JT1kQLA9WwW((IComparable) tuple.Item1);
      }
      Point point2 = new Point(x, num);
      _param2.\u0023\u003Dzo2ftAfxjqC04(\u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26h0hvlV4LfsI5sS6QMD39it_JzJq2Auui18\u003D.\u0023\u003Dzop6vn0GowyiR(point2, flag1));
    }
    return _param2;
  }

  public void \u0023\u003DzJoneIt0\u003D(T _param1)
  {
    if (this.\u0023\u003DzaXL4UJQ\u003D.Contains(_param1))
      return;
    this.\u0023\u003DzaXL4UJQ\u003D.Add(_param1);
    string stackedGroupId = _param1.get_StackedGroupId();
    this.\u0023\u003Dz1PPp4iih3ZzT(_param1, stackedGroupId);
  }

  private void \u0023\u003Dz1PPp4iih3ZzT(T _param1, string _param2)
  {
    if (this.\u0023\u003DzmHvvg5w\u003D(_param2))
      this.\u0023\u003DzaqOTdcE2Qrcnms5YPQ\u003D\u003D(_param1.get_StackedGroupId()).Add(_param1);
    else
      this.\u0023\u003DzSgM2QmPFjPGL.Add(new Tuple<string, List<T>>(_param2, new List<T>(1)
      {
        _param1
      }));
  }

  private bool \u0023\u003DzmHvvg5w\u003D(string _param1)
  {
    return this.\u0023\u003DzSgM2QmPFjPGL.Any<Tuple<string, List<T>>>(new Func<Tuple<string, List<T>>, bool>(new \u0023\u003DzAuXtmwo_UFdzWVVSiImlM6CrNZf0Y\u0024AoldqYWswSWHE1DEHxuAfkMq_BMaE9<T>.\u0023\u003Dz4sSy7LmbTiAIt\u0024fc7nJ88\u00244\u003D()
    {
      \u0023\u003Dz1hrVQH8\u003D = _param1
    }.\u0023\u003Dz4AK2Bl06EBCuZNGG\u0024A\u003D\u003D));
  }

  public void \u0023\u003Dz_SCZwjM\u003D(T _param1)
  {
    if (!this.\u0023\u003DzaXL4UJQ\u003D.Contains(_param1))
      return;
    this.\u0023\u003DzaXL4UJQ\u003D.Remove(_param1);
    string stackedGroupId = _param1.get_StackedGroupId();
    this.\u0023\u003Dz_OJS\u0024TFfqHfT(_param1, stackedGroupId);
  }

  private void \u0023\u003Dz_OJS\u0024TFfqHfT(T _param1, string _param2)
  {
    if (!this.\u0023\u003DzmHvvg5w\u003D(_param2))
      return;
    int index = this.\u0023\u003Dzq9AWGAFPmvNn(_param2);
    this.\u0023\u003DzSgM2QmPFjPGL[index].Item2.Remove(_param1);
    if (this.\u0023\u003DzSgM2QmPFjPGL[index].Item2.Count != 0)
      return;
    this.\u0023\u003DzSgM2QmPFjPGL.RemoveAt(index);
  }

  public void \u0023\u003DzZcYnC1z3z2MT(
    T _param1,
    string _param2,
    string _param3)
  {
    if (!this.\u0023\u003DzmHvvg5w\u003D(_param2))
      return;
    this.\u0023\u003Dz_OJS\u0024TFfqHfT(_param1, _param2);
    this.\u0023\u003Dz1PPp4iih3ZzT(_param1, _param3);
  }

  public IList<T> \u0023\u003DzaqOTdcE2Qrcnms5YPQ\u003D\u003D(
    string _param1)
  {
    return (IList<T>) this.\u0023\u003DzSgM2QmPFjPGL[this.\u0023\u003Dzq9AWGAFPmvNn(_param1)].Item2;
  }

  protected int \u0023\u003Dzq9AWGAFPmvNn(string _param1)
  {
    int num = -1;
    foreach (Tuple<string, List<T>> tuple in this.\u0023\u003DzSgM2QmPFjPGL)
    {
      ++num;
      if (tuple.Item1 == _param1)
        break;
    }
    return num;
  }

  public List<T> \u0023\u003DzH11Fih5tNn7B()
  {
    return this.\u0023\u003DzaXL4UJQ\u003D;
  }

  public List<Tuple<string, List<T>>> \u0023\u003DzqvFydN90W9_IRDrgUQ\u003D\u003D()
  {
    return this.\u0023\u003DzSgM2QmPFjPGL;
  }

  public int \u0023\u003Dz8KqszcpQGXCdDZj_BA\u003D\u003D()
  {
    return this.\u0023\u003DzIbVn\u0024T8\u003D;
  }

  private sealed class \u0023\u003Dz4sSy7LmbTiAIt\u0024fc7nJ88\u00244\u003D
  {
    public string \u0023\u003Dz1hrVQH8\u003D;

    public bool \u0023\u003Dz4AK2Bl06EBCuZNGG\u0024A\u003D\u003D(
      Tuple<string, List<T>> _param1)
    {
      return _param1.Item1 == this.\u0023\u003Dz1hrVQH8\u003D;
    }
  }

  [Serializable]
  private sealed class SomeClass34343383
  {
    public static readonly \u0023\u003DzAuXtmwo_UFdzWVVSiImlM6CrNZf0Y\u0024AoldqYWswSWHE1DEHxuAfkMq_BMaE9<T>.SomeClass34343383 SomeMethond0343 = new \u0023\u003DzAuXtmwo_UFdzWVVSiImlM6CrNZf0Y\u0024AoldqYWswSWHE1DEHxuAfkMq_BMaE9<T>.SomeClass34343383();
    public static Func<T, bool> \u0023\u003Dzbt6wp4trpUeqHhxsrQ\u003D\u003D;
    public static Func<T, bool> \u0023\u003DzjJ3vlfxVH92KtuUzew\u003D\u003D;
    public static Func<T, bool> \u0023\u003DzaObIckm5bO9Zm0ifDA\u003D\u003D;

    public bool \u0023\u003DzLZvPTS5COc90bGo0i3aY9EE\u003D(T _param1)
    {
      return _param1.IsVisible;
    }

    public bool \u0023\u003Dz9Z5iGcTB1hN7ZnRz61x\u0024KhY\u003D(T _param1)
    {
      return _param1.IsVisible && _param1.get_DataSeries() != null;
    }

    public bool \u0023\u003DzTGuhZLTU3_PDZ4NVc9Rm2zk\u003D(T _param1)
    {
      return _param1.IsVisible;
    }
  }

  private sealed class \u0023\u003DzHF4K0hz6MwLRPm7JFGVw01g\u003D
  {
    public int \u0023\u003Dzay7IytM\u003D;

    public double \u0023\u003DzA7RktUSISABNMw2sUfBVsa8\u003D(T _param1)
    {
      return ((IComparable) _param1.get_DataSeries().\u0023\u003DzPqsSI6C5MOOb()[this.\u0023\u003Dzay7IytM\u003D]).ToDouble();
    }
  }
}
