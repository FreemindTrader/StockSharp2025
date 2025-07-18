// Decompiled with JetBrains decompiler
// Type: #=z7oKBks6ccXdMBOl$qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using Ecng.Collections;
using StockSharp.Charting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

#nullable disable
public sealed class VisbleRangeDp : 
  DependencyObject
{
  
  private static readonly Dictionary<(string, string, object), VisbleRangeDp> \u0023\u003DznABf_Vs\u003D = new Dictionary<(string, string, object), VisbleRangeDp>();
  
  public static readonly DependencyProperty \u0023\u003Dz4JhmNred6k04bHgbHA\u003D\u003D = DependencyProperty.Register(nameof (CategoryDateTimeRange), typeof (IndexRange ), typeof (VisbleRangeDp), new PropertyMetadata(new PropertyChangedCallback(VisbleRangeDp.SomeClass34343383.SomeMethond0343.\u0023\u003DzoRYIHBGDkf1GAn4szFFBOAk\u003D)));
  
  private IndexRange  \u0023\u003DzrHc3qGwDWR8ZLE0hMQ\u003D\u003D;
  
  public static readonly DependencyProperty \u0023\u003DziUr7YgFYdbrr = DependencyProperty.Register(nameof (NumericRange), typeof (DoubleRange), typeof (VisbleRangeDp), new PropertyMetadata(new PropertyChangedCallback(VisbleRangeDp.SomeClass34343383.SomeMethond0343.\u0023\u003DznDXAZ40XZkwF42e7V3ChT6E\u003D)));
  
  private DoubleRange \u0023\u003Dz9wn11RS5FRTj;
  
  public static readonly DependencyProperty \u0023\u003Dz8gZeLle5o5Ez = DependencyProperty.Register(nameof (DateTimeRange), typeof (\u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26vpI1xWpwwNQJw\u003D\u003D), typeof (VisbleRangeDp), new PropertyMetadata(new PropertyChangedCallback(VisbleRangeDp.SomeClass34343383.SomeMethond0343.\u0023\u003DzM4Q1JrKOrXUH_pG8e9nkfj8\u003D)));
  
  private \u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26vpI1xWpwwNQJw\u003D\u003D \u0023\u003DzBg6bVfITudAr;
  
  private readonly ChartAxisType \u0023\u003DzjIbS_29QSa7sxFGAlg\u003D\u003D;

  public VisbleRangeDp(
    ChartAxisType _param1,
    int? _param2)
  {
    this.\u0023\u003DzjIbS_29QSa7sxFGAlg\u003D\u003D = _param1;
    this.InitRangeDepProperty(_param2);
  }

  public IndexRange  CategoryDateTimeRange
  {
    get => this.\u0023\u003DzrHc3qGwDWR8ZLE0hMQ\u003D\u003D;
    set
    {
      this.SetValue(VisbleRangeDp.\u0023\u003Dz4JhmNred6k04bHgbHA\u003D\u003D, (object) value);
    }
  }

  public DoubleRange NumericRange
  {
    get => this.\u0023\u003Dz9wn11RS5FRTj;
    set
    {
      this.SetValue(VisbleRangeDp.\u0023\u003DziUr7YgFYdbrr, (object) value);
    }
  }

  public \u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26vpI1xWpwwNQJw\u003D\u003D DateTimeRange
  {
    get => this.\u0023\u003DzBg6bVfITudAr;
    set
    {
      this.SetValue(VisbleRangeDp.\u0023\u003Dz8gZeLle5o5Ez, (object) value);
    }
  }

  public ChartAxisType GetAxisType()
  {
    return this.\u0023\u003DzjIbS_29QSa7sxFGAlg\u003D\u003D;
  }

  private void InitRangeDepProperty(int? _param1)
  {
    if (!_param1.HasValue || _param1.GetValueOrDefault() <= 0)
      _param1 = new int?(50);
    this.CategoryDateTimeRange = new IndexRange (-5, _param1.Value);
    this.NumericRange = new DoubleRange();
  }

  public static void InitRangeDepProperty(object _param0)
  {
    VisbleRangeDp.SomeClass6409 v4vdZv8GtEzAmB0rzFq = new VisbleRangeDp.SomeClass6409()
    {
      \u0023\u003Dz6pqZ7di4NkHd = _param0
    };
    v4vdZv8GtEzAmB0rzFq.\u0023\u003Dzf2JqWCjCPVfE = VisbleRangeDp.\u0023\u003DzI4Rp2x5f98qv(v4vdZv8GtEzAmB0rzFq.\u0023\u003Dz6pqZ7di4NkHd);
    CollectionHelper.ForEach<KeyValuePair<(string, string, object), VisbleRangeDp>>(VisbleRangeDp.\u0023\u003DznABf_Vs\u003D.Where<KeyValuePair<(string, string, object), VisbleRangeDp>>(new Func<KeyValuePair<(string, string, object), VisbleRangeDp>, bool>(v4vdZv8GtEzAmB0rzFq.\u0023\u003DznhcJw0QjS9iS6EreCve\u0024qxI\u003D)), new Action<KeyValuePair<(string, string, object), VisbleRangeDp>>(v4vdZv8GtEzAmB0rzFq.\u0023\u003DzWv5\u0024RXM3VgWIFlyt9X5W\u0024eE\u003D));
  }

  public static VisbleRangeDp \u0023\u003DzYMTYgq1xYsSy(
    object _param0,
    string _param1,
    string _param2,
    ChartAxisType _param3)
  {
    return CollectionHelper.SafeAdd<(string, string, object), VisbleRangeDp>((IDictionary<(string, string, object), VisbleRangeDp>) VisbleRangeDp.\u0023\u003DznABf_Vs\u003D, (_param1, _param2, _param0), new Func<(string, string, object), VisbleRangeDp>(new VisbleRangeDp.SomeClass398()
    {
      \u0023\u003DzAak5C46IF1W8 = _param3,
      \u0023\u003Dzf2JqWCjCPVfE = VisbleRangeDp.\u0023\u003DzI4Rp2x5f98qv(_param0)
    }.\u0023\u003DzVyROSASk7JNM_TzPWA\u003D\u003D));
  }

  private static int? \u0023\u003DzI4Rp2x5f98qv(object _param0)
  {
    switch (_param0)
    {
      case ScichartSurfaceMVVM tdnKj06Uu87Wzk09Wj:
        return new int?(tdnKj06Uu87Wzk09Wj.MinimumRange);
      case ChartViewModel zgziIlo367a8J0vVw6:
        return new int?(zgziIlo367a8J0vVw6.MinimumRange);
      default:
        return new int?();
    }
  }

  [Serializable]
  private sealed class SomeClass34343383
  {
    public static readonly VisbleRangeDp.SomeClass34343383 SomeMethond0343 = new VisbleRangeDp.SomeClass34343383();

    public void \u0023\u003DzoRYIHBGDkf1GAn4szFFBOAk\u003D(
      DependencyObject _param1,
      DependencyPropertyChangedEventArgs _param2)
    {
      ((VisbleRangeDp) _param1).\u0023\u003DzrHc3qGwDWR8ZLE0hMQ\u003D\u003D = (IndexRange ) _param2.NewValue;
    }

    public void \u0023\u003DznDXAZ40XZkwF42e7V3ChT6E\u003D(
      DependencyObject _param1,
      DependencyPropertyChangedEventArgs _param2)
    {
      ((VisbleRangeDp) _param1).\u0023\u003Dz9wn11RS5FRTj = (DoubleRange) _param2.NewValue;
    }

    public void \u0023\u003DzM4Q1JrKOrXUH_pG8e9nkfj8\u003D(
      DependencyObject _param1,
      DependencyPropertyChangedEventArgs _param2)
    {
      ((VisbleRangeDp) _param1).\u0023\u003DzBg6bVfITudAr = (\u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26vpI1xWpwwNQJw\u003D\u003D) _param2.NewValue;
    }
  }

  private sealed class SomeClass398
  {
    public ChartAxisType \u0023\u003DzAak5C46IF1W8;
    public int? \u0023\u003Dzf2JqWCjCPVfE;

    public VisbleRangeDp \u0023\u003DzVyROSASk7JNM_TzPWA\u003D\u003D(
      (string, string, object) _param1)
    {
      return new VisbleRangeDp(this.\u0023\u003DzAak5C46IF1W8, this.\u0023\u003Dzf2JqWCjCPVfE);
    }
  }

  private sealed class SomeClass6409
  {
    public object \u0023\u003Dz6pqZ7di4NkHd;
    public int? \u0023\u003Dzf2JqWCjCPVfE;

    public bool \u0023\u003DznhcJw0QjS9iS6EreCve\u0024qxI\u003D(
      KeyValuePair<(string, string, object), VisbleRangeDp> _param1)
    {
      return _param1.Key.Item3 == this.\u0023\u003Dz6pqZ7di4NkHd;
    }

    public void \u0023\u003DzWv5\u0024RXM3VgWIFlyt9X5W\u0024eE\u003D(
      KeyValuePair<(string, string, object), VisbleRangeDp> _param1)
    {
      _param1.Value.InitRangeDepProperty(this.\u0023\u003Dzf2JqWCjCPVfE);
    }
  }
}
