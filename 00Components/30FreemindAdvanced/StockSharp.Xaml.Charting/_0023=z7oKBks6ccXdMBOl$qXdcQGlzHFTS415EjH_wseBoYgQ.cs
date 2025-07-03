// Decompiled with JetBrains decompiler
// Type: #=z7oKBks6ccXdMBOl$qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using \u002D;
using Ecng.Collections;
using StockSharp.Charting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

#nullable disable
internal sealed class \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR : 
  DependencyObject
{
  
  private static readonly Dictionary<(string, string, object), \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR> \u0023\u003DznABf_Vs\u003D = new Dictionary<(string, string, object), \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR>();
  
  public static readonly DependencyProperty \u0023\u003Dz4JhmNred6k04bHgbHA\u003D\u003D = DependencyProperty.Register("", typeof (IndexRange ), typeof (\u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR), new PropertyMetadata(new PropertyChangedCallback(\u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzoRYIHBGDkf1GAn4szFFBOAk\u003D)));
  
  private IndexRange  \u0023\u003DzrHc3qGwDWR8ZLE0hMQ\u003D\u003D;
  
  public static readonly DependencyProperty \u0023\u003DziUr7YgFYdbrr = DependencyProperty.Register("", typeof (DoubleRange), typeof (\u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR), new PropertyMetadata(new PropertyChangedCallback(\u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DznDXAZ40XZkwF42e7V3ChT6E\u003D)));
  
  private DoubleRange \u0023\u003Dz9wn11RS5FRTj;
  
  public static readonly DependencyProperty \u0023\u003Dz8gZeLle5o5Ez = DependencyProperty.Register("", typeof (\u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26vpI1xWpwwNQJw\u003D\u003D), typeof (\u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR), new PropertyMetadata(new PropertyChangedCallback(\u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzM4Q1JrKOrXUH_pG8e9nkfj8\u003D)));
  
  private \u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26vpI1xWpwwNQJw\u003D\u003D \u0023\u003DzBg6bVfITudAr;
  
  private readonly ChartAxisType \u0023\u003DzjIbS_29QSa7sxFGAlg\u003D\u003D;

  public \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR(
    ChartAxisType _param1,
    int? _param2)
  {
    this.\u0023\u003DzjIbS_29QSa7sxFGAlg\u003D\u003D = _param1;
    this.\u0023\u003DzoMQQ88MEiBDX(_param2);
  }

  public IndexRange  CategoryDateTimeRange
  {
    get => this.\u0023\u003DzrHc3qGwDWR8ZLE0hMQ\u003D\u003D;
    set
    {
      this.SetValue(\u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR.\u0023\u003Dz4JhmNred6k04bHgbHA\u003D\u003D, (object) value);
    }
  }

  public DoubleRange NumericRange
  {
    get => this.\u0023\u003Dz9wn11RS5FRTj;
    set
    {
      this.SetValue(\u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR.\u0023\u003DziUr7YgFYdbrr, (object) value);
    }
  }

  public \u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26vpI1xWpwwNQJw\u003D\u003D DateTimeRange
  {
    get => this.\u0023\u003DzBg6bVfITudAr;
    set
    {
      this.SetValue(\u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR.\u0023\u003Dz8gZeLle5o5Ez, (object) value);
    }
  }

  public ChartAxisType \u0023\u003DzOdcnf0c\u003D()
  {
    return this.\u0023\u003DzjIbS_29QSa7sxFGAlg\u003D\u003D;
  }

  private void \u0023\u003DzoMQQ88MEiBDX(int? _param1)
  {
    if (!_param1.HasValue || _param1.GetValueOrDefault() <= 0)
      _param1 = new int?(50);
    this.CategoryDateTimeRange = new IndexRange (-5, _param1.Value);
    this.NumericRange = new DoubleRange();
  }

  public static void \u0023\u003DzoMQQ88MEiBDX(object _param0)
  {
    \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR.\u0023\u003Dzg5oaV4vdZV8GtEzAmB0rzFQ\u003D v4vdZv8GtEzAmB0rzFq = new \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR.\u0023\u003Dzg5oaV4vdZV8GtEzAmB0rzFQ\u003D()
    {
      \u0023\u003Dz6pqZ7di4NkHd = _param0
    };
    v4vdZv8GtEzAmB0rzFq.\u0023\u003Dzf2JqWCjCPVfE = \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR.\u0023\u003DzI4Rp2x5f98qv(v4vdZv8GtEzAmB0rzFq.\u0023\u003Dz6pqZ7di4NkHd);
    CollectionHelper.ForEach<KeyValuePair<(string, string, object), \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR>>(\u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR.\u0023\u003DznABf_Vs\u003D.Where<KeyValuePair<(string, string, object), \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR>>(new Func<KeyValuePair<(string, string, object), \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR>, bool>(v4vdZv8GtEzAmB0rzFq.\u0023\u003DznhcJw0QjS9iS6EreCve\u0024qxI\u003D)), new Action<KeyValuePair<(string, string, object), \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR>>(v4vdZv8GtEzAmB0rzFq.\u0023\u003DzWv5\u0024RXM3VgWIFlyt9X5W\u0024eE\u003D));
  }

  public static \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR \u0023\u003DzYMTYgq1xYsSy(
    object _param0,
    string _param1,
    string _param2,
    ChartAxisType _param3)
  {
    return CollectionHelper.SafeAdd<(string, string, object), \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR>((IDictionary<(string, string, object), \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR>) \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR.\u0023\u003DznABf_Vs\u003D, (_param1, _param2, _param0), new Func<(string, string, object), \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR>(new \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR.\u0023\u003DzNwXxhkJq9Llz3ah\u0024z2LRQl4\u003D()
    {
      \u0023\u003DzAak5C46IF1W8 = _param3,
      \u0023\u003Dzf2JqWCjCPVfE = \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR.\u0023\u003DzI4Rp2x5f98qv(_param0)
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
  private sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR.\u0023\u003Dz7qOdpi4\u003D();

    internal void \u0023\u003DzoRYIHBGDkf1GAn4szFFBOAk\u003D(
      DependencyObject _param1,
      DependencyPropertyChangedEventArgs _param2)
    {
      ((\u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR) _param1).\u0023\u003DzrHc3qGwDWR8ZLE0hMQ\u003D\u003D = (IndexRange ) _param2.NewValue;
    }

    internal void \u0023\u003DznDXAZ40XZkwF42e7V3ChT6E\u003D(
      DependencyObject _param1,
      DependencyPropertyChangedEventArgs _param2)
    {
      ((\u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR) _param1).\u0023\u003Dz9wn11RS5FRTj = (DoubleRange) _param2.NewValue;
    }

    internal void \u0023\u003DzM4Q1JrKOrXUH_pG8e9nkfj8\u003D(
      DependencyObject _param1,
      DependencyPropertyChangedEventArgs _param2)
    {
      ((\u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR) _param1).\u0023\u003DzBg6bVfITudAr = (\u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26vpI1xWpwwNQJw\u003D\u003D) _param2.NewValue;
    }
  }

  private sealed class \u0023\u003DzNwXxhkJq9Llz3ah\u0024z2LRQl4\u003D
  {
    public ChartAxisType \u0023\u003DzAak5C46IF1W8;
    public int? \u0023\u003Dzf2JqWCjCPVfE;

    internal \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR \u0023\u003DzVyROSASk7JNM_TzPWA\u003D\u003D(
      (string, string, object) _param1)
    {
      return new \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR(this.\u0023\u003DzAak5C46IF1W8, this.\u0023\u003Dzf2JqWCjCPVfE);
    }
  }

  private sealed class \u0023\u003Dzg5oaV4vdZV8GtEzAmB0rzFQ\u003D
  {
    public object \u0023\u003Dz6pqZ7di4NkHd;
    public int? \u0023\u003Dzf2JqWCjCPVfE;

    internal bool \u0023\u003DznhcJw0QjS9iS6EreCve\u0024qxI\u003D(
      KeyValuePair<(string, string, object), \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR> _param1)
    {
      return _param1.Key.Item3 == this.\u0023\u003Dz6pqZ7di4NkHd;
    }

    internal void \u0023\u003DzWv5\u0024RXM3VgWIFlyt9X5W\u0024eE\u003D(
      KeyValuePair<(string, string, object), \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR> _param1)
    {
      _param1.Value.\u0023\u003DzoMQQ88MEiBDX(this.\u0023\u003Dzf2JqWCjCPVfE);
    }
  }
}
