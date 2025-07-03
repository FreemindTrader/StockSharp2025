// Decompiled with JetBrains decompiler
// Type: -.dje_zN29EKU2DQXHTNKQ8X5JHMLEL2WYLBY8R3YWYA24CH8SZCCZ_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using Ecng.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal class dje_zN29EKU2DQXHTNKQ8X5JHMLEL2WYLBY8R3YWYA24CH8SZCCZ_ejd : 
  dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd
{
  
  public static readonly DependencyProperty \u0023\u003DzNRygy3vTBpTh = DependencyProperty.RegisterAttached("", typeof (bool), typeof (dje_zN29EKU2DQXHTNKQ8X5JHMLEL2WYLBY8R3YWYA24CH8SZCCZ_ejd), new PropertyMetadata((object) true));
  
  public static readonly DependencyProperty \u0023\u003DzzEpwYL7p2H9x = DependencyProperty.Register("", typeof (bool), typeof (dje_zN29EKU2DQXHTNKQ8X5JHMLEL2WYLBY8R3YWYA24CH8SZCCZ_ejd), new PropertyMetadata((object) false, (PropertyChangedCallback) null));
  
  private Line \u0023\u003DzuKhG8cvbLb0H;
  
  private Line \u0023\u003Dzqc7rBjeXtKct;
  
  private Ellipse \u0023\u003DzatysOTiD_AK_;
  
  private dje_zWLGYTTT5DUFM55EFRPBBAZMZXPMVBQNX4VDZEWMJLU768RLDDSRVTC6SXVJQ2DSMRBMWZUQJ5VY6CVZ_ejd \u0023\u003DzafnecPyU9x6l;
  
  private ObservableCollection<\u0023\u003DzT6V9cIzTPzymiPsaXC1JFEAP9ly0DLdsgjrQCUaaCm\u0024XPj7JdmPvp0w\u003D> \u0023\u003DzfaDgy7Wexasn = new ObservableCollection<\u0023\u003DzT6V9cIzTPzymiPsaXC1JFEAP9ly0DLdsgjrQCUaaCm\u0024XPj7JdmPvp0w\u003D>();
  
  private \u0023\u003DzT6V9cIzTPzymiPsaXC1JFEAP9ly0DLdsgjrQCUaaCm\u0024XPj7JdmPvp0w\u003D \u0023\u003Dzp00PUT7ntZP8;
  
  private \u0023\u003DzT6V9cIzTPzymiPsaXC1JFEAP9ly0DLdsgjrQCUaaCm\u0024XPj7JdmPvp0w\u003D \u0023\u003DzCIhlnYjUSxNN;

  public dje_zN29EKU2DQXHTNKQ8X5JHMLEL2WYLBY8R3YWYA24CH8SZCCZ_ejd()
  {
    this.DefaultStyleKey = (object) typeof (dje_zN29EKU2DQXHTNKQ8X5JHMLEL2WYLBY8R3YWYA24CH8SZCCZ_ejd);
    this.SetCurrentValue(dje_zW9CS5E2KYALJRMCDFUV9GBWAD6FTS94JN3AD7LFC8K7BTMRPRXTEX_ejd.\u0023\u003DzE7h5hUE7Vu4g, (object) new \u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSv1H1ORLBnhjlg\u003D\u003D());
    this.\u0023\u003DzIxFmGbTNpwO0 = new DelayActionHelper()
    {
      Interval = this.HoverDelay
    };
  }

  public static bool GetIncludeSeries(DependencyObject _param0)
  {
    return (bool) _param0.GetValue(dje_zN29EKU2DQXHTNKQ8X5JHMLEL2WYLBY8R3YWYA24CH8SZCCZ_ejd.\u0023\u003DzNRygy3vTBpTh);
  }

  public static void SetIncludeSeries(DependencyObject _param0, bool _param1)
  {
    _param0.SetValue(dje_zN29EKU2DQXHTNKQ8X5JHMLEL2WYLBY8R3YWYA24CH8SZCCZ_ejd.\u0023\u003DzNRygy3vTBpTh, (object) _param1);
  }

  public ObservableCollection<\u0023\u003DzT6V9cIzTPzymiPsaXC1JFEAP9ly0DLdsgjrQCUaaCm\u0024XPj7JdmPvp0w\u003D> AxisInfo
  {
    get => this.\u0023\u003DzfaDgy7Wexasn;
    set
    {
      this.\u0023\u003DzfaDgy7Wexasn = value;
      this.\u0023\u003Dz15moWio\u003D("");
    }
  }

  public \u0023\u003DzT6V9cIzTPzymiPsaXC1JFEAP9ly0DLdsgjrQCUaaCm\u0024XPj7JdmPvp0w\u003D XAxisInfo
  {
    get => this.\u0023\u003Dzp00PUT7ntZP8;
    set
    {
      this.\u0023\u003Dzp00PUT7ntZP8 = value;
      this.\u0023\u003Dz15moWio\u003D("");
    }
  }

  public \u0023\u003DzT6V9cIzTPzymiPsaXC1JFEAP9ly0DLdsgjrQCUaaCm\u0024XPj7JdmPvp0w\u003D YAxisInfo
  {
    get => this.\u0023\u003DzCIhlnYjUSxNN;
    set
    {
      this.\u0023\u003DzCIhlnYjUSxNN = value;
      this.\u0023\u003Dz15moWio\u003D("");
    }
  }

  public bool ShowTooltip
  {
    get
    {
      return (bool) this.GetValue(dje_zN29EKU2DQXHTNKQ8X5JHMLEL2WYLBY8R3YWYA24CH8SZCCZ_ejd.\u0023\u003DzzEpwYL7p2H9x);
    }
    set
    {
      this.SetValue(dje_zN29EKU2DQXHTNKQ8X5JHMLEL2WYLBY8R3YWYA24CH8SZCCZ_ejd.\u0023\u003DzzEpwYL7p2H9x, (object) value);
    }
  }

  public double HoverDelay
  {
    get
    {
      return (double) this.GetValue(dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.\u0023\u003DzcSEHQMyzEgRH);
    }
    set
    {
      this.SetValue(dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.\u0023\u003DzcSEHQMyzEgRH, (object) value);
    }
  }

  public override void OnAttached()
  {
    base.OnAttached();
    this.\u0023\u003DzeAqKwx8\u003D = new Point(double.NaN, double.NaN);
    this.\u0023\u003DzleRWWIS9Sb_X();
  }

  public override void OnDetached()
  {
    base.OnDetached();
    this.\u0023\u003DzleRWWIS9Sb_X();
  }

  protected override void \u0023\u003DzRhCP\u0024yGKAqZwVyg1vA\u003D\u003D()
  {
    this.\u0023\u003DzleRWWIS9Sb_X();
  }

  protected override void \u0023\u003Dz_wtru8oSZoY9(Point _param1)
  {
    dje_zN29EKU2DQXHTNKQ8X5JHMLEL2WYLBY8R3YWYA24CH8SZCCZ_ejd.\u0023\u003DzM\u0024TlJy0mx0yCUWlqEsh0ZdY\u003D jy0mx0yCuWlqEsh0ZdY = new dje_zN29EKU2DQXHTNKQ8X5JHMLEL2WYLBY8R3YWYA24CH8SZCCZ_ejd.\u0023\u003DzM\u0024TlJy0mx0yCUWlqEsh0ZdY\u003D();
    jy0mx0yCuWlqEsh0ZdY.\u0023\u003DzRRvwDu67s9Rm = this;
    jy0mx0yCuWlqEsh0ZdY.\u0023\u003DzvCn8oRW_i8ct = _param1;
    this.\u0023\u003DzSRU73z_agnZjtPtO2Cu\u0024edM\u003D(jy0mx0yCuWlqEsh0ZdY.\u0023\u003DzvCn8oRW_i8ct);
    if (this.ShowTooltip || this.ShowAxisLabels)
    {
      this.\u0023\u003Dzck1z3ouVxklP(jy0mx0yCuWlqEsh0ZdY.\u0023\u003DzvCn8oRW_i8ct);
      this.\u0023\u003Dz4\u0024ey2dUrFu2c(jy0mx0yCuWlqEsh0ZdY.\u0023\u003DzvCn8oRW_i8ct);
    }
    if (this.ShowTooltip)
    {
      if (this.SeriesData.SeriesInfo.\u0023\u003DzCCMM80zDpO6N<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D>() || !this.\u0023\u003Dz1lxFA46cCdxI())
        this.\u0023\u003Dz0M6Lr1INFIAM();
      else if (this.ShowTooltipOn == dje_zW5MLMFCCFHVWSRGZD77RWP95DJXJ342SYK8Q3LCSNM5MWZXH4Q2GW_ejd.MouseHover)
      {
        this.\u0023\u003Dz0M6Lr1INFIAM();
        this.\u0023\u003DzIxFmGbTNpwO0.Start(new Action(jy0mx0yCuWlqEsh0ZdY.\u0023\u003Dz7Vd1ie04V9seGu9XAXPfhNY\u003D));
      }
      else
        this.\u0023\u003DzcWFGFMbg0iEM(jy0mx0yCuWlqEsh0ZdY.\u0023\u003DzvCn8oRW_i8ct);
    }
    if (!this.ShowAxisLabels)
      return;
    this.\u0023\u003Dzpy\u0024qh0dK5ADs(jy0mx0yCuWlqEsh0ZdY.\u0023\u003DzvCn8oRW_i8ct);
  }

  private void \u0023\u003DzcWFGFMbg0iEM(Point _param1)
  {
    \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D vdj8C0KctI6r27Gg = this.SeriesData.SeriesInfo != null ? this.SeriesData.SeriesInfo.FirstOrDefault<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D>() : (\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D) null;
    Point point = vdj8C0KctI6r27Gg != null ? vdj8C0KctI6r27Gg.XyCoordinate : _param1;
    if (this.\u0023\u003Dzt9d2ExuvJfVV(point))
      this.\u0023\u003DzC4voZ0Pb0Ark(point);
    else
      this.\u0023\u003Dz0M6Lr1INFIAM();
  }

  private void \u0023\u003Dzpy\u0024qh0dK5ADs(Point _param1)
  {
    if (!this.\u0023\u003DzLgNKpMJ5V7kN())
      this.\u0023\u003DzQrcA6OyHGRkq();
    this.\u0023\u003DzqB2OzvmQT2Y9(_param1);
  }

  private void \u0023\u003Dzck1z3ouVxklP(Point _param1)
  {
    this.SeriesData.\u0023\u003DzGPrsWyT8SibF(this.\u0023\u003DzzhlDItrRFv\u0024\u0024(_param1));
  }

  protected override IEnumerable<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D> \u0023\u003DzzhlDItrRFv\u0024\u0024(
    Point _param1)
  {
    return this.\u0023\u003DzzhlDItrRFv\u0024\u0024(new Func<IRenderableSeries, \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D>(new dje_zN29EKU2DQXHTNKQ8X5JHMLEL2WYLBY8R3YWYA24CH8SZCCZ_ejd.\u0023\u003DzIkfJv_ww2IEiAf0VERJZev4\u003D()
    {
      \u0023\u003Dz_hWqBbI\u003D = _param1,
      \u0023\u003DzRRvwDu67s9Rm = this
    }.\u0023\u003DzvJtlk3txQNN91rBG8dcpcj0\u003D));
  }

  protected virtual void \u0023\u003Dz4\u0024ey2dUrFu2c(Point _param1)
  {
    dje_zN29EKU2DQXHTNKQ8X5JHMLEL2WYLBY8R3YWYA24CH8SZCCZ_ejd.\u0023\u003DzE1noKVt89B1lUeA7EDfukJs\u003D kvt89B1lUeA7EdfukJs = new dje_zN29EKU2DQXHTNKQ8X5JHMLEL2WYLBY8R3YWYA24CH8SZCCZ_ejd.\u0023\u003DzE1noKVt89B1lUeA7EDfukJs\u003D();
    kvt89B1lUeA7EdfukJs.\u0023\u003DzRRvwDu67s9Rm = this;
    kvt89B1lUeA7EdfukJs.\u0023\u003DzvCn8oRW_i8ct = _param1;
    IEnumerable<\u0023\u003DzT6V9cIzTPzymiPsaXC1JFEAP9ly0DLdsgjrQCUaaCm\u0024XPj7JdmPvp0w\u003D> source1 = this.YAxes.Select<IAxis, \u0023\u003DzT6V9cIzTPzymiPsaXC1JFEAP9ly0DLdsgjrQCUaaCm\u0024XPj7JdmPvp0w\u003D>(new Func<IAxis, \u0023\u003DzT6V9cIzTPzymiPsaXC1JFEAP9ly0DLdsgjrQCUaaCm\u0024XPj7JdmPvp0w\u003D>(kvt89B1lUeA7EdfukJs.\u0023\u003DzTAW\u0024IBjKDuezcG8tkoBGo1Q\u003D));
    this.YAxisInfo = source1.FirstOrDefault<\u0023\u003DzT6V9cIzTPzymiPsaXC1JFEAP9ly0DLdsgjrQCUaaCm\u0024XPj7JdmPvp0w\u003D>();
    IEnumerable<\u0023\u003DzT6V9cIzTPzymiPsaXC1JFEAP9ly0DLdsgjrQCUaaCm\u0024XPj7JdmPvp0w\u003D> source2 = this.XAxes.Select<IAxis, \u0023\u003DzT6V9cIzTPzymiPsaXC1JFEAP9ly0DLdsgjrQCUaaCm\u0024XPj7JdmPvp0w\u003D>(new Func<IAxis, \u0023\u003DzT6V9cIzTPzymiPsaXC1JFEAP9ly0DLdsgjrQCUaaCm\u0024XPj7JdmPvp0w\u003D>(kvt89B1lUeA7EdfukJs.\u0023\u003DzEhR4Oe23EuYwGjXUPg7SBR8\u003D));
    this.XAxisInfo = source2.FirstOrDefault<\u0023\u003DzT6V9cIzTPzymiPsaXC1JFEAP9ly0DLdsgjrQCUaaCm\u0024XPj7JdmPvp0w\u003D>();
    ObservableCollection<\u0023\u003DzT6V9cIzTPzymiPsaXC1JFEAP9ly0DLdsgjrQCUaaCm\u0024XPj7JdmPvp0w\u003D> observableCollection = new ObservableCollection<\u0023\u003DzT6V9cIzTPzymiPsaXC1JFEAP9ly0DLdsgjrQCUaaCm\u0024XPj7JdmPvp0w\u003D>();
    observableCollection.\u0023\u003Dz6_E5\u0024pE\u003D<\u0023\u003DzT6V9cIzTPzymiPsaXC1JFEAP9ly0DLdsgjrQCUaaCm\u0024XPj7JdmPvp0w\u003D>(source2);
    observableCollection.\u0023\u003Dz6_E5\u0024pE\u003D<\u0023\u003DzT6V9cIzTPzymiPsaXC1JFEAP9ly0DLdsgjrQCUaaCm\u0024XPj7JdmPvp0w\u003D>(source1);
    this.AxisInfo = observableCollection;
  }

  private void \u0023\u003DzSRU73z_agnZjtPtO2Cu\u0024edM\u003D(Point _param1)
  {
    Rect boundsRelativeTo = this.ModifierSurface.GetBoundsRelativeTo((IHitTestable) this.\u0023\u003Dzwc4Gzka23TGB());
    if (this.\u0023\u003DzuKhG8cvbLb0H == null || this.\u0023\u003Dzqc7rBjeXtKct == null)
    {
      Line line1 = new Line();
      line1.Style = this.LineOverlayStyle;
      line1.IsHitTestVisible = false;
      this.\u0023\u003DzuKhG8cvbLb0H = line1;
      Line line2 = new Line();
      line2.Style = this.LineOverlayStyle;
      line2.IsHitTestVisible = false;
      this.\u0023\u003Dzqc7rBjeXtKct = line2;
      if (this.\u0023\u003DzatysOTiD_AK_ == null)
      {
        Ellipse ellipse = new Ellipse();
        ellipse.Fill = this.\u0023\u003Dzqc7rBjeXtKct.Stroke;
        ellipse.Width = 5.0;
        ellipse.Height = 5.0;
        this.\u0023\u003DzatysOTiD_AK_ = ellipse;
      }
      this.ModifierSurface.\u0023\u003DzBDSV99pPo8hY().Add((UIElement) this.\u0023\u003DzuKhG8cvbLb0H);
      this.ModifierSurface.\u0023\u003DzBDSV99pPo8hY().Add((UIElement) this.\u0023\u003Dzqc7rBjeXtKct);
    }
    this.\u0023\u003DzuKhG8cvbLb0H.X1 = 0.0;
    this.\u0023\u003DzuKhG8cvbLb0H.X2 = boundsRelativeTo.Width - 1.0;
    this.\u0023\u003DzuKhG8cvbLb0H.Y1 = _param1.Y;
    this.\u0023\u003DzuKhG8cvbLb0H.Y2 = _param1.Y;
    this.\u0023\u003Dzqc7rBjeXtKct.X1 = _param1.X;
    this.\u0023\u003Dzqc7rBjeXtKct.X2 = _param1.X;
    this.\u0023\u003Dzqc7rBjeXtKct.Y1 = 0.0;
    this.\u0023\u003Dzqc7rBjeXtKct.Y2 = boundsRelativeTo.Height - 1.0;
  }

  protected override void \u0023\u003Dz1z_ZexRGbAiN91rPDA\u003D\u003D(Point _param1)
  {
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("", new object[2]
    {
      (object) _param1.X,
      (object) _param1.Y
    });
    Rect boundsRelativeTo = this.ModifierSurface.GetBoundsRelativeTo((IHitTestable) this.\u0023\u003Dzwc4Gzka23TGB());
    if (this.\u0023\u003Dzqc7rBjeXtKct == null)
    {
      Line line = new Line();
      line.Style = this.LineOverlayStyle;
      line.IsHitTestVisible = false;
      this.\u0023\u003Dzqc7rBjeXtKct = line;
      this.ModifierSurface.\u0023\u003DzBDSV99pPo8hY().Add((UIElement) this.\u0023\u003Dzqc7rBjeXtKct);
    }
    this.\u0023\u003Dzqc7rBjeXtKct.X1 = _param1.X;
    this.\u0023\u003Dzqc7rBjeXtKct.X2 = _param1.X;
    this.\u0023\u003Dzqc7rBjeXtKct.Y1 = 0.0;
    this.\u0023\u003Dzqc7rBjeXtKct.Y2 = boundsRelativeTo.Height - 1.0;
  }

  protected override void \u0023\u003DzleRWWIS9Sb_X()
  {
    if (this.ModifierSurface != null)
    {
      if (this.\u0023\u003DzuKhG8cvbLb0H != null && this.ModifierSurface.\u0023\u003DzBDSV99pPo8hY().Contains((UIElement) this.\u0023\u003DzuKhG8cvbLb0H))
      {
        this.ModifierSurface.\u0023\u003DzBDSV99pPo8hY().Remove((UIElement) this.\u0023\u003DzuKhG8cvbLb0H);
        this.\u0023\u003DzuKhG8cvbLb0H = (Line) null;
      }
      if (this.\u0023\u003Dzqc7rBjeXtKct != null && this.ModifierSurface.\u0023\u003DzBDSV99pPo8hY().Contains((UIElement) this.\u0023\u003Dzqc7rBjeXtKct))
      {
        this.ModifierSurface.\u0023\u003DzBDSV99pPo8hY().Remove((UIElement) this.\u0023\u003Dzqc7rBjeXtKct);
        this.\u0023\u003Dzqc7rBjeXtKct = (Line) null;
      }
    }
    this.\u0023\u003DzALinGkdxMzIo();
    this.\u0023\u003DzeAqKwx8\u003D = new Point(double.NaN, double.NaN);
    this.\u0023\u003DzIxFmGbTNpwO0.Stop();
  }

  private void \u0023\u003DzALinGkdxMzIo()
  {
    this.\u0023\u003Dz0M6Lr1INFIAM();
    this.\u0023\u003DzaHyDUm8F3XtsCygXSA\u003D\u003D();
  }

  private void \u0023\u003Dz0M6Lr1INFIAM()
  {
    if (this.\u0023\u003DzafnecPyU9x6l == null || this.ModifierSurface == null)
      return;
    if (this.ModifierSurface.\u0023\u003DzBDSV99pPo8hY().Contains((UIElement) this.\u0023\u003DzafnecPyU9x6l))
      this.ModifierSurface.\u0023\u003DzBDSV99pPo8hY().Remove((UIElement) this.\u0023\u003DzafnecPyU9x6l);
    if (!this.ModifierSurface.\u0023\u003DzBDSV99pPo8hY().Contains((UIElement) this.\u0023\u003DzatysOTiD_AK_))
      return;
    this.ModifierSurface.\u0023\u003DzBDSV99pPo8hY().Remove((UIElement) this.\u0023\u003DzatysOTiD_AK_);
  }

  private void \u0023\u003DzC4voZ0Pb0Ark(Point _param1)
  {
    if (this.\u0023\u003DzafnecPyU9x6l == null || !this.ShowTooltip)
      return;
    this.\u0023\u003DzHi3KLf0npKDN((FrameworkElement) this.\u0023\u003DzafnecPyU9x6l, _param1);
    Canvas.SetLeft((UIElement) this.\u0023\u003DzatysOTiD_AK_, _param1.X - this.\u0023\u003DzatysOTiD_AK_.ActualWidth * 0.5);
    Canvas.SetTop((UIElement) this.\u0023\u003DzatysOTiD_AK_, _param1.Y - this.\u0023\u003DzatysOTiD_AK_.ActualHeight * 0.5);
    if (!this.ModifierSurface.\u0023\u003DzBDSV99pPo8hY().Contains((UIElement) this.\u0023\u003DzafnecPyU9x6l))
      this.ModifierSurface.\u0023\u003DzBDSV99pPo8hY().Add((UIElement) this.\u0023\u003DzafnecPyU9x6l);
    if (this.ModifierSurface.\u0023\u003DzBDSV99pPo8hY().Contains((UIElement) this.\u0023\u003DzatysOTiD_AK_))
      return;
    this.ModifierSurface.\u0023\u003DzBDSV99pPo8hY().Add((UIElement) this.\u0023\u003DzatysOTiD_AK_);
  }

  private void \u0023\u003DzHi3KLf0npKDN(FrameworkElement _param1, Point _param2)
  {
    Rect rect1 = new Rect(0.0, 0.0, this.ModifierSurface.ActualWidth, this.ModifierSurface.ActualHeight);
    double length = _param2.X + 6.0;
    double num1 = _param2.Y + 6.0;
    Rect rect2 = new Rect(length, num1, _param1.ActualWidth, _param1.ActualHeight);
    if (rect1.Right < rect2.Right)
      length = _param2.X - rect2.Width - 6.0;
    if (rect1.Bottom < rect2.Bottom)
    {
      double num2 = rect2.Bottom - rect1.Bottom;
      double num3 = num1 - num2;
      num1 = num3 < 0.0 ? 0.0 : num3;
    }
    Canvas.SetLeft((UIElement) _param1, length);
    Canvas.SetTop((UIElement) _param1, num1 < 0.0 ? 0.0 : num1);
  }

  protected override void \u0023\u003DzY0Ucom6W\u0024E0ZkvcKcA\u003D\u003D()
  {
    this.\u0023\u003DzafnecPyU9x6l = this.\u0023\u003DzBv1vB\u0024LEKSF4(this.TooltipLabelTemplate, this.TooltipLabelTemplateSelector, (object) this);
  }

  protected override void \u0023\u003Dz9otdS\u0024TJZ4U8t8zXqw\u003D\u003D()
  {
    this.\u0023\u003DzQrcA6OyHGRkq();
  }

  private sealed class \u0023\u003DzE1noKVt89B1lUeA7EDfukJs\u003D
  {
    public dje_zN29EKU2DQXHTNKQ8X5JHMLEL2WYLBY8R3YWYA24CH8SZCCZ_ejd \u0023\u003DzRRvwDu67s9Rm;
    public Point \u0023\u003DzvCn8oRW_i8ct;

    internal \u0023\u003DzT6V9cIzTPzymiPsaXC1JFEAP9ly0DLdsgjrQCUaaCm\u0024XPj7JdmPvp0w\u003D \u0023\u003DzTAW\u0024IBjKDuezcG8tkoBGo1Q\u003D(
      IAxis _param1)
    {
      return this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzU0tYbfdnROi1(_param1, this.\u0023\u003DzvCn8oRW_i8ct);
    }

    internal \u0023\u003DzT6V9cIzTPzymiPsaXC1JFEAP9ly0DLdsgjrQCUaaCm\u0024XPj7JdmPvp0w\u003D \u0023\u003DzEhR4Oe23EuYwGjXUPg7SBR8\u003D(
      IAxis _param1)
    {
      return this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzU0tYbfdnROi1(_param1, this.\u0023\u003DzvCn8oRW_i8ct);
    }
  }

  private sealed class \u0023\u003DzIkfJv_ww2IEiAf0VERJZev4\u003D
  {
    public Point \u0023\u003Dz_hWqBbI\u003D;
    public dje_zN29EKU2DQXHTNKQ8X5JHMLEL2WYLBY8R3YWYA24CH8SZCCZ_ejd \u0023\u003DzRRvwDu67s9Rm;

    internal \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D \u0023\u003DzvJtlk3txQNN91rBG8dcpcj0\u003D(
      IRenderableSeries _param1)
    {
      return _param1.\u0023\u003DznVLFa68vHPHy(this.\u0023\u003Dz_hWqBbI\u003D, this.\u0023\u003DzRRvwDu67s9Rm.UseInterpolation);
    }
  }

  private sealed class \u0023\u003DzM\u0024TlJy0mx0yCUWlqEsh0ZdY\u003D
  {
    public dje_zN29EKU2DQXHTNKQ8X5JHMLEL2WYLBY8R3YWYA24CH8SZCCZ_ejd \u0023\u003DzRRvwDu67s9Rm;
    public Point \u0023\u003DzvCn8oRW_i8ct;

    internal void \u0023\u003Dz7Vd1ie04V9seGu9XAXPfhNY\u003D()
    {
      this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzcWFGFMbg0iEM(this.\u0023\u003DzvCn8oRW_i8ct);
    }
  }
}
