// Decompiled with JetBrains decompiler
// Type: #=zdPAQRlt3VWWvvKbSPLZ0IZuSESVgU8LW8DvId9tdE7eLQoPdEDqa2l4=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using \u002D;
using Ecng.Collections;
using Ecng.Xaml;
using Ecng.Xaml.Converters;
using StockSharp.Xaml.Charting;
using StockSharp.Xaml.Charting.Visuals.Annotations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Threading;

#nullable disable
internal abstract class UIBaseVM :
  ChartBaseViewModel
{
    
    private readonly Dictionary<IRenderableSeries, AxisMarkerAnnotation> _renderseries2AxisMarker = new Dictionary<IRenderableSeries, AxisMarkerAnnotation>();
    
    private ParentVM _parentChartViewModel;

    protected ParentVM GetParentVM()
    {
        return this._parentChartViewModel;
    }

    private void SetParentVM(
      ParentVM _param1)
    {
        this._parentChartViewModel = _param1;
    }

    public abstract IDrawableChartElement Element { get; }

    public IfxChartElement RootElem
    {
        get => this.GetParentVM().ChartElement;
    }

    protected IScichartSurfaceVM \u0023\u003Dz\u00246aIVrHDxlRJ()
    {
        return this.GetParentVM().Pane;
    }

    protected bool \u0023\u003Dz5OzJ0EHhtC8P() => this.GetParentVM().IsDisposed;

    private static Dispatcher \u0023\u003Dz7tn0Xgbe83AE()
    {
        return GuiDispatcher.GlobalDispatcher.Dispatcher;
    }

    protected static bool \u0023\u003Dz03PnGbpCXkrj()
    {
        return UIBaseVM.\u0023\u003Dz7tn0Xgbe83AE().CheckAccess();
    }

    protected abstract void \u0023\u003DzowR7R4A\u003D();

  protected virtual void \u0023\u003DzY0x9JtY\u003D()
  {
  }

protected abstract void \u0023\u003DzXfak0jM\u003D();

public abstract bool \u0023\u003DzjgUUUJE\u003D(IEnumerableEx<ChartDrawData.IDrawValue> _param1);

public void \u0023\u003DzYI36Ggg\u003D()
  {
    this.\u0023\u003DzowR7R4A\u003D();
this.\u0023\u003Dz4EoFHUaZg4JL(new Action(this.\u0023\u003DzAVykYn0F15D5Ztnzfw\u003D\u003D), true);
  }

  public virtual void \u0023\u003Dz\u0024abmkXc\u003D()
  {
    this.\u0023\u003Dz4EoFHUaZg4JL(new Action(this.\u0023\u003Dzl5v19PTUv87upojg6w\u003D\u003D), true);
  }

  protected virtual void \u0023\u003DzfrhXX9MCW\u0024SC(
    IfxChartElement _param1,
    string _param2,
    object _param3)
  {
  }

  protected virtual void \u0023\u003Dz3u1qwgvgJlZC(
    IfxChartElement _param1,
    string _param2)
  {
}

public void \u0023\u003Dzfd2adzY\u003D(
    ParentVM _param1)
  {
    if (this.GetParentVM() != null)
      throw new InvalidOperationException("");
this.SetParentVM(_param1);
this.\u0023\u003DzY0x9JtY\u003D();
this.\u0023\u003DzYI36Ggg\u003D();
  }

  public virtual void \u0023\u003DzoK7PFLI\u003D()
  {
    foreach (KeyValuePair<IRenderableSeries, AxisMarkerAnnotation> keyValuePair in this._renderseries2AxisMarker)
      keyValuePair.Value.Y1 = keyValuePair.Key.get_DataSeries()?.get_LatestYValue();
  }

  public virtual void \u0023\u003DzKy5smiO3gHXp()
  {
}

protected void \u0023\u003DzAgVixDQ6Vc2r()
  {
    foreach (KeyValuePair<IRenderableSeries, AxisMarkerAnnotation> keyValuePair in this._renderseries2AxisMarker)
    {
        keyValuePair.Value.IsHidden = true;
        BindingOperations.ClearAllBindings((DependencyObject)keyValuePair.Value);
    }
    this.\u0023\u003Dz\u00246aIVrHDxlRJ().\u0023\u003DzwxpBjD0\u003D(this.RootElem, (object)null);
    this._renderseries2AxisMarker.Clear();
}

protected void \u0023\u003Dz7GhHTEkMkDYT(
  IRenderableSeries _param1,
  IfxChartElement _param2,
  string _param3,
  string _param4)
  {
    AxisMarkerAnnotation markerAnnotation1 = new AxisMarkerAnnotation();
    markerAnnotation1.FontSize = 11.0;
    markerAnnotation1.Foreground = (Brush)Brushes.White;
    AxisMarkerAnnotation markerAnnotation2 = markerAnnotation1;
    this._renderseries2AxisMarker[_param1] = markerAnnotation2;
    this.\u0023\u003Dz\u00246aIVrHDxlRJ().\u0023\u003DzIeZhoes\u003D(this.RootElem, (\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D) markerAnnotation2, (object)markerAnnotation2);
    markerAnnotation2.SetBindings(AnnotationBase.XAxisIdProperty, (object)_param2, "");
    markerAnnotation2.SetBindings(AnnotationBase.YAxisIdProperty, (object)_param2, "");
    AxisMarkerAnnotation markerAnnotation3 = markerAnnotation2;
    DependencyProperty isHiddenProperty = AnnotationBase.IsHiddenProperty;
    BoolAnyConverter conv = new BoolAnyConverter();
    conv.Value = false;
    Binding[] bindingArray = new Binding[2]
    {
      new Binding(_param3) { Source = (object) _param2 },
      new Binding()
      {
        Source = (object) _param1,
        Path = new PropertyPath((object) dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003Dz8b6MQAIE8UZn)
      }
    };
    markerAnnotation3.SetMultiBinding(isHiddenProperty, (IMultiValueConverter)conv, bindingArray);
    if (_param4 != null)
    {
        markerAnnotation2.SetBindings(Control.BackgroundProperty, (object)_param2, _param4, converter: (IValueConverter)new ColorToBrushConverter());
        markerAnnotation2.SetBindings(Control.BorderBrushProperty, (object)_param2, _param4, converter: (IValueConverter)new ColorToBrushConverter());
    }
    else
        markerAnnotation2.Background = markerAnnotation2.BorderBrush = (Brush)Brushes.Gray;
}

protected void \u0023\u003DzY_lPK_VP\u0024B7_(Action _param1, bool _param2)
  {
    this.\u0023\u003DztwYEX\u0024c\u003D(new Action<Action>(((XamlHelper)UIBaseVM.\u0023\u003Dz7tn0Xgbe83AE()).GuiAsync), _param1, _param2);
  }

  protected void \u0023\u003Dz4EoFHUaZg4JL(Action _param1, bool _param2)
  {
    this.\u0023\u003DztwYEX\u0024c\u003D(new Action<Action>(((XamlHelper)UIBaseVM.\u0023\u003Dz7tn0Xgbe83AE()).GuiSync), _param1, _param2);
}

private void \u0023\u003DztwYEX\u0024c\u003D(
    Action<Action> _param1,
  Action _param2,
    bool _param3)
  {
    UIBaseVM.\u0023\u003DzlcqYEuvfbi2ga1Q3dva__4g\u003D yeuvfbi2ga1Q3dva4g = new UIBaseVM.\u0023\u003DzlcqYEuvfbi2ga1Q3dva__4g\u003D();
yeuvfbi2ga1Q3dva4g.\u0023\u003DzRRvwDu67s9Rm = this;
yeuvfbi2ga1Q3dva4g.\u0023\u003Dz07PQx44\u003D = _param2;
    Action action = _param3 ? new Action(yeuvfbi2ga1Q3dva4g.\u0023\u003Dz60l\u0024Ihha9C_kL2icwg\u003D\u003D) : yeuvfbi2ga1Q3dva4g.\u0023\u003Dz07PQx44\u003D;
    if (UIBaseVM.\u0023\u003Dz03PnGbpCXkrj())
      action();
    else
      _param1(action);
  }

  private void \u0023\u003DzAVykYn0F15D5Ztnzfw\u003D\u003D()
  {
    CollectionHelper.ForEach<KeyValuePair<IRenderableSeries, AxisMarkerAnnotation>>((IEnumerable<KeyValuePair<IRenderableSeries, AxisMarkerAnnotation>>) this._renderseries2AxisMarker, UIBaseVM.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzyKR7f5A3QPvTwxZODA\u003D\u003D ?? (UIBaseVM.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzyKR7f5A3QPvTwxZODA\u003D\u003D = new Action<KeyValuePair<IRenderableSeries, AxisMarkerAnnotation>>(UIBaseVM.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzrUT4mYySZlfIfzql4Q\u003D\u003D)));
  }

  private void \u0023\u003Dzl5v19PTUv87upojg6w\u003D\u003D()
  {
    this.\u0023\u003DzYI36Ggg\u003D();
    this.\u0023\u003DzXfak0jM\u003D();
  }

  [Serializable]
  private new sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly UIBaseVM.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new UIBaseVM.\u0023\u003Dz7qOdpi4\u003D();
    public static Action<KeyValuePair<IRenderableSeries, AxisMarkerAnnotation>> \u0023\u003DzyKR7f5A3QPvTwxZODA\u003D\u003D;

    internal void \u0023\u003DzrUT4mYySZlfIfzql4Q\u003D\u003D(
      KeyValuePair<IRenderableSeries, AxisMarkerAnnotation> _param1)
    {
      _param1.Value.Y1 = (IComparable) null;
    }
  }

  private sealed class \u0023\u003DzlcqYEuvfbi2ga1Q3dva__4g\u003D
  {
    public UIBaseVM \u0023\u003DzRRvwDu67s9Rm;
    public Action \u0023\u003Dz07PQx44\u003D;

    internal void \u0023\u003Dz60l\u0024Ihha9C_kL2icwg\u003D\u003D()
    {
      if (this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003Dz5OzJ0EHhtC8P())
        return;
      this.\u0023\u003Dz07PQx44\u003D();
    }
  }
}
