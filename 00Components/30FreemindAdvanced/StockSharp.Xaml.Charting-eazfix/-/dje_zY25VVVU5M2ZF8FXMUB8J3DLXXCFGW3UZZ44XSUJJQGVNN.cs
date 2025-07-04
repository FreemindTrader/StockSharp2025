// Decompiled with JetBrains decompiler
// Type: -.dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace \u002D;

internal abstract class dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd : 
  dje_zW9CS5E2KYALJRMCDFUV9GBWAD6FTS94JN3AD7LFC8K7BTMRPRXTEX_ejd
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzEhUv091lUjuk = DependencyProperty.Register(nameof (ShowTooltipOn), typeof (dje_zW5MLMFCCFHVWSRGZD77RWP95DJXJ342SYK8Q3LCSNM5MWZXH4Q2GW_ejd), typeof (dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd), new PropertyMetadata((object) dje_zW5MLMFCCFHVWSRGZD77RWP95DJXJ342SYK8Q3LCSNM5MWZXH4Q2GW_ejd.MouseOver));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzefJvv3CZ5BIx = DependencyProperty.Register(nameof (LineOverlayStyle), typeof (Style), typeof (dje_zW9CS5E2KYALJRMCDFUV9GBWAD6FTS94JN3AD7LFC8K7BTMRPRXTEX_ejd), new PropertyMetadata((PropertyChangedCallback) null));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003Dzdz6MkOePVs4t = DependencyProperty.Register(nameof (AxisLabelTemplate), typeof (ControlTemplate), typeof (dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd), new PropertyMetadata(new PropertyChangedCallback(dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.\u0023\u003DzbVg\u0024qRIz823IPqWt\u0024A\u003D\u003D)));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzCuzcJq\u0024VLiWR = DependencyProperty.Register(nameof (ShowAxisLabels), typeof (bool), typeof (dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd), new PropertyMetadata((object) true, (PropertyChangedCallback) null));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzwbBEIdM\u0024iIcr = DependencyProperty.Register(nameof (TooltipLabelTemplate), typeof (ControlTemplate), typeof (dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd), new PropertyMetadata(new PropertyChangedCallback(dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.\u0023\u003Dzwur\u0024mqt3kPJHpkPRjQ\u003D\u003D)));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DztS0SV\u0024h_UR9naBBpPQ\u003D\u003D = DependencyProperty.Register(nameof (AxisLabelTemplateSelector), typeof (\u0023\u003DzAfUZ1hld3Aj4_oK9JVqPo8HI_6JOBYhgDQ\u003D\u003D), typeof (dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd), new PropertyMetadata(new PropertyChangedCallback(dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.\u0023\u003DzbVg\u0024qRIz823IPqWt\u0024A\u003D\u003D)));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzvNytNs_0PVAQNrNseA\u003D\u003D = DependencyProperty.Register(nameof (TooltipLabelTemplateSelector), typeof (\u0023\u003DzAfUZ1hld3Aj4_oK9JVqPo8HI_6JOBYhgDQ\u003D\u003D), typeof (dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd), new PropertyMetadata(new PropertyChangedCallback(dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.\u0023\u003Dzwur\u0024mqt3kPJHpkPRjQ\u003D\u003D)));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003Dz6XQECdaYmU1_GuzwGA\u003D\u003D = DependencyProperty.Register(nameof (DefaultAxisLabelTemplateSelector), typeof (\u0023\u003DzAfUZ1hld3Aj4_oK9JVqPo8HI_6JOBYhgDQ\u003D\u003D), typeof (dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd), new PropertyMetadata((object) null));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzClmcjSK4Qv_TawDQNPNUv5c\u003D = DependencyProperty.Register(nameof (DefaultAxisLabelTemplateSelectorStyle), typeof (Style), typeof (dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.\u0023\u003DzbxLNaq5ZjEofA\u0024XB3WbNaIs\u003D)));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003Dz5dHn3Ygiw7f44dBolw\u003D\u003D = DependencyProperty.Register(nameof (DefaultTooltipLabelTemplateSelector), typeof (\u0023\u003DzAfUZ1hld3Aj4_oK9JVqPo8HI_6JOBYhgDQ\u003D\u003D), typeof (dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd), new PropertyMetadata((object) null));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003Dz3WEQAsiyFGgPCmpeo4Tp7eU\u003D = DependencyProperty.Register(nameof (DefaultTooltipLabelTemplateSelectorStyle), typeof (Style), typeof (dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.\u0023\u003Dz8WfI5kqsN6kmTe6TXhhzlCo\u003D)));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzcSEHQMyzEgRH = DependencyProperty.Register("HoverDelay", typeof (double), typeof (dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd), new PropertyMetadata((object) 500.0, new PropertyChangedCallback(dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.\u0023\u003DzesUKUhGoyfNgYFzSJw\u003D\u003D)));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private IEnumerable<Tuple<IAxis, FrameworkElement>> \u0023\u003DzJ9TR0V6N30XqYK5Fsg\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private IEnumerable<Tuple<IAxis, FrameworkElement>> \u0023\u003Dzlpam1C6zshu\u0024hyb5gg\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  internal DelayActionHelper \u0023\u003DzIxFmGbTNpwO0;

  protected dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd()
  {
    dje_zBZFYG9UE9Q9VXV3SUTH6CF44QELPK8MFBBGBUCLK8X5KCN2_ejd mfbbgbuclK8X5KcN2Ejd = new dje_zBZFYG9UE9Q9VXV3SUTH6CF44QELPK8MFBBGBUCLK8X5KCN2_ejd();
    mfbbgbuclK8X5KcN2Ejd.Style = this.DefaultAxisLabelTemplateSelectorStyle;
    this.DefaultAxisLabelTemplateSelector = (\u0023\u003DzAfUZ1hld3Aj4_oK9JVqPo8HI_6JOBYhgDQ\u003D\u003D) mfbbgbuclK8X5KcN2Ejd;
    dje_zX3N6Q9TLRHQ6K6VPWM4NP8NG2Z86U66DVP76ECY4XKZ29FA_ejd p76EcY4XkZ29FaEjd = new dje_zX3N6Q9TLRHQ6K6VPWM4NP8NG2Z86U66DVP76ECY4XKZ29FA_ejd();
    p76EcY4XkZ29FaEjd.Style = this.DefaultTooltipLabelTemplateSelectorStyle;
    this.DefaultTooltipLabelTemplateSelector = (\u0023\u003DzAfUZ1hld3Aj4_oK9JVqPo8HI_6JOBYhgDQ\u003D\u003D) p76EcY4XkZ29FaEjd;
  }

  public dje_zW5MLMFCCFHVWSRGZD77RWP95DJXJ342SYK8Q3LCSNM5MWZXH4Q2GW_ejd ShowTooltipOn
  {
    get
    {
      return (dje_zW5MLMFCCFHVWSRGZD77RWP95DJXJ342SYK8Q3LCSNM5MWZXH4Q2GW_ejd) this.GetValue(dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.\u0023\u003DzEhUv091lUjuk);
    }
    set
    {
      this.SetValue(dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.\u0023\u003DzEhUv091lUjuk, (object) value);
    }
  }

  public Style LineOverlayStyle
  {
    get
    {
      return (Style) this.GetValue(dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.\u0023\u003DzefJvv3CZ5BIx);
    }
    set
    {
      this.SetValue(dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.\u0023\u003DzefJvv3CZ5BIx, (object) value);
    }
  }

  public ControlTemplate AxisLabelTemplate
  {
    get
    {
      return (ControlTemplate) this.GetValue(dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.\u0023\u003Dzdz6MkOePVs4t);
    }
    set
    {
      this.SetValue(dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.\u0023\u003Dzdz6MkOePVs4t, (object) value);
    }
  }

  public ControlTemplate TooltipLabelTemplate
  {
    get
    {
      return (ControlTemplate) this.GetValue(dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.\u0023\u003DzwbBEIdM\u0024iIcr);
    }
    set
    {
      this.SetValue(dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.\u0023\u003DzwbBEIdM\u0024iIcr, (object) value);
    }
  }

  public bool ShowAxisLabels
  {
    get
    {
      return (bool) this.GetValue(dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.\u0023\u003DzCuzcJq\u0024VLiWR);
    }
    set
    {
      this.SetValue(dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.\u0023\u003DzCuzcJq\u0024VLiWR, (object) value);
    }
  }

  public \u0023\u003DzAfUZ1hld3Aj4_oK9JVqPo8HI_6JOBYhgDQ\u003D\u003D AxisLabelTemplateSelector
  {
    get
    {
      return (\u0023\u003DzAfUZ1hld3Aj4_oK9JVqPo8HI_6JOBYhgDQ\u003D\u003D) this.GetValue(dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.\u0023\u003DztS0SV\u0024h_UR9naBBpPQ\u003D\u003D);
    }
    set
    {
      this.SetValue(dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.\u0023\u003DztS0SV\u0024h_UR9naBBpPQ\u003D\u003D, (object) value);
    }
  }

  public \u0023\u003DzAfUZ1hld3Aj4_oK9JVqPo8HI_6JOBYhgDQ\u003D\u003D TooltipLabelTemplateSelector
  {
    get
    {
      return (\u0023\u003DzAfUZ1hld3Aj4_oK9JVqPo8HI_6JOBYhgDQ\u003D\u003D) this.GetValue(dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.\u0023\u003DzvNytNs_0PVAQNrNseA\u003D\u003D);
    }
    set
    {
      this.SetValue(dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.\u0023\u003DzvNytNs_0PVAQNrNseA\u003D\u003D, (object) value);
    }
  }

  public \u0023\u003DzAfUZ1hld3Aj4_oK9JVqPo8HI_6JOBYhgDQ\u003D\u003D DefaultAxisLabelTemplateSelector
  {
    get
    {
      return (\u0023\u003DzAfUZ1hld3Aj4_oK9JVqPo8HI_6JOBYhgDQ\u003D\u003D) this.GetValue(dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.\u0023\u003Dz6XQECdaYmU1_GuzwGA\u003D\u003D);
    }
    protected set
    {
      this.SetValue(dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.\u0023\u003Dz6XQECdaYmU1_GuzwGA\u003D\u003D, (object) value);
    }
  }

  public Style DefaultAxisLabelTemplateSelectorStyle
  {
    get
    {
      return (Style) this.GetValue(dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.\u0023\u003DzClmcjSK4Qv_TawDQNPNUv5c\u003D);
    }
    set
    {
      this.SetValue(dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.\u0023\u003DzClmcjSK4Qv_TawDQNPNUv5c\u003D, (object) value);
    }
  }

  public \u0023\u003DzAfUZ1hld3Aj4_oK9JVqPo8HI_6JOBYhgDQ\u003D\u003D DefaultTooltipLabelTemplateSelector
  {
    get
    {
      return (\u0023\u003DzAfUZ1hld3Aj4_oK9JVqPo8HI_6JOBYhgDQ\u003D\u003D) this.GetValue(dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.\u0023\u003Dz5dHn3Ygiw7f44dBolw\u003D\u003D);
    }
    protected set
    {
      this.SetValue(dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.\u0023\u003Dz5dHn3Ygiw7f44dBolw\u003D\u003D, (object) value);
    }
  }

  public Style DefaultTooltipLabelTemplateSelectorStyle
  {
    get
    {
      return (Style) this.GetValue(dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.\u0023\u003Dz3WEQAsiyFGgPCmpeo4Tp7eU\u003D);
    }
    set
    {
      this.SetValue(dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.\u0023\u003Dz3WEQAsiyFGgPCmpeo4Tp7eU\u003D, (object) value);
    }
  }

  public override void \u0023\u003DzsXEfcKpqchyX(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
    base.\u0023\u003DzsXEfcKpqchyX(_param1);
    this.\u0023\u003DzzleyEI9UNcP_(_param1);
  }

  public override void \u0023\u003DzU3pYs4rYVmOS(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
    base.\u0023\u003DzU3pYs4rYVmOS(_param1);
    this.\u0023\u003DzzleyEI9UNcP_(_param1);
  }

  protected override void \u0023\u003DzCM2UQyuakisf() => this.\u0023\u003DzleRWWIS9Sb_X();

  private void \u0023\u003DzzleyEI9UNcP_(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
    if (this.ShowTooltipOn != dje_zW5MLMFCCFHVWSRGZD77RWP95DJXJ342SYK8Q3LCSNM5MWZXH4Q2GW_ejd.MouseLeftButtonDown && this.ShowTooltipOn != dje_zW5MLMFCCFHVWSRGZD77RWP95DJXJ342SYK8Q3LCSNM5MWZXH4Q2GW_ejd.MouseMiddleButtonDown && this.ShowTooltipOn != dje_zW5MLMFCCFHVWSRGZD77RWP95DJXJ342SYK8Q3LCSNM5MWZXH4Q2GW_ejd.MouseRightButtonDown)
      return;
    \u0023\u003DzAF1f\u0024KZyh1dFR1SFJ2ERzu9w\u0024DbQvIH5WWg9Ebw\u003D erzu9wDbQvIh5Wwg9Ebw = _param1.\u0023\u003DzwuSh61ofE2mr();
    _param1.\u0023\u003DzhWBaoH4TqLHj((\u0023\u003DzAF1f\u0024KZyh1dFR1SFJ2ERzu9w\u0024DbQvIH5WWg9Ebw\u003D) 0);
    this.\u0023\u003DzebZge1miA2O0(_param1);
    _param1.\u0023\u003DzhWBaoH4TqLHj(erzu9wDbQvIh5Wwg9Ebw);
    _param1.\u0023\u003DzBHH5KNloEXNR(false);
  }

  protected bool \u0023\u003Dz1lxFA46cCdxI()
  {
    if (this.ShowTooltipOn == dje_zW5MLMFCCFHVWSRGZD77RWP95DJXJ342SYK8Q3LCSNM5MWZXH4Q2GW_ejd.Always || this.ShowTooltipOn == dje_zW5MLMFCCFHVWSRGZD77RWP95DJXJ342SYK8Q3LCSNM5MWZXH4Q2GW_ejd.MouseHover || this.ShowTooltipOn == dje_zW5MLMFCCFHVWSRGZD77RWP95DJXJ342SYK8Q3LCSNM5MWZXH4Q2GW_ejd.MouseLeftButtonDown && this.IsMouseLeftButtonDown || this.ShowTooltipOn == dje_zW5MLMFCCFHVWSRGZD77RWP95DJXJ342SYK8Q3LCSNM5MWZXH4Q2GW_ejd.MouseMiddleButtonDown && this.IsMouseMiddleButtonDown)
      return true;
    return this.ShowTooltipOn == dje_zW5MLMFCCFHVWSRGZD77RWP95DJXJ342SYK8Q3LCSNM5MWZXH4Q2GW_ejd.MouseRightButtonDown && this.IsMouseRightButtonDown;
  }

  protected override bool \u0023\u003DzD5SquRN7M_9c(
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D _param1)
  {
    if (_param1.\u0023\u003DzMeGSfVE\u003D())
      return false;
    return _param1.\u0023\u003Dzmh1LiTa467ce() || _param1.\u0023\u003DzxIOIxNIOU4djmPFSiA\u003D\u003D();
  }

  protected override void \u0023\u003DzMQsNWQqev3ol8vdG1w\u003D\u003D(
    object _param1,
    NotifyCollectionChangedEventArgs _param2)
  {
    this.\u0023\u003DzhoHxkSVR\u0024FsTBj1bDA\u003D\u003D();
  }

  protected override void \u0023\u003DzBwMZf3Z18v_xRgVWSw\u003D\u003D(
    object _param1,
    NotifyCollectionChangedEventArgs _param2)
  {
    this.\u0023\u003Dz\u0024W78w9bAp8kl9UxrOA\u003D\u003D();
  }

  protected bool \u0023\u003DzLgNKpMJ5V7kN()
  {
    return this.\u0023\u003Dzlpam1C6zshu\u0024hyb5gg\u003D\u003D != null && this.\u0023\u003Dzlpam1C6zshu\u0024hyb5gg\u003D\u003D.Count<Tuple<IAxis, FrameworkElement>>() == this.YAxes.Count<IAxis>() && this.\u0023\u003DzJ9TR0V6N30XqYK5Fsg\u003D\u003D != null && this.\u0023\u003DzJ9TR0V6N30XqYK5Fsg\u003D\u003D.Count<Tuple<IAxis, FrameworkElement>>() == this.XAxes.Count<IAxis>();
  }

  protected void \u0023\u003DzqB2OzvmQT2Y9(Point _param1)
  {
    foreach (IAxis xax in this.XAxes)
    {
      dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.\u0023\u003Dzf1B6wwNJbrqhpNKVkrNPKgU\u003D njbrqhpNkVkrNpKgU = new dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.\u0023\u003Dzf1B6wwNJbrqhpNKVkrNPKgU\u003D();
      njbrqhpNkVkrNpKgU.\u0023\u003Dz4bf8Oyc\u003D = xax;
      FrameworkElement frameworkElement = this.\u0023\u003DzJ9TR0V6N30XqYK5Fsg\u003D\u003D.Where<Tuple<IAxis, FrameworkElement>>(new Func<Tuple<IAxis, FrameworkElement>, bool>(njbrqhpNkVkrNpKgU.\u0023\u003DzftM36zmTSR2NGmsCOruf4V4\u003D)).Select<Tuple<IAxis, FrameworkElement>, FrameworkElement>(dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.SomeClass34343383.\u0023\u003Dz5BIbVz_RRJEtvtlEuw\u003D\u003D ?? (dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.SomeClass34343383.\u0023\u003Dz5BIbVz_RRJEtvtlEuw\u003D\u003D = new Func<Tuple<IAxis, FrameworkElement>, FrameworkElement>(dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.SomeClass34343383.SomeMethond0343.\u0023\u003Dz\u0024J5BAtSL9mu4zONFt126S5i5D4WR))).FirstOrDefault<FrameworkElement>();
      this.\u0023\u003Dz0jC4zrgOWDNx(_param1, njbrqhpNkVkrNpKgU.\u0023\u003Dz4bf8Oyc\u003D, frameworkElement);
    }
    foreach (IAxis yax in this.YAxes)
    {
      dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.\u0023\u003DzPZHe8kTpPoA\u0024JhpeVw6ares\u003D tpPoAJhpeVw6ares = new dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.\u0023\u003DzPZHe8kTpPoA\u0024JhpeVw6ares\u003D();
      tpPoAJhpeVw6ares.\u0023\u003DzS7JsfCE\u003D = yax;
      FrameworkElement frameworkElement = this.\u0023\u003Dzlpam1C6zshu\u0024hyb5gg\u003D\u003D.Where<Tuple<IAxis, FrameworkElement>>(new Func<Tuple<IAxis, FrameworkElement>, bool>(tpPoAJhpeVw6ares.\u0023\u003DzFOzwFNi9b6vX6jZHz0MsrcI\u003D)).Select<Tuple<IAxis, FrameworkElement>, FrameworkElement>(dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.SomeClass34343383.\u0023\u003DzQo1YXL32S5xfzg99Jw\u003D\u003D ?? (dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.SomeClass34343383.\u0023\u003DzQo1YXL32S5xfzg99Jw\u003D\u003D = new Func<Tuple<IAxis, FrameworkElement>, FrameworkElement>(dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.SomeClass34343383.SomeMethond0343.\u0023\u003DzddQllHN_1gETercjTb3EPMwlFP4J))).FirstOrDefault<FrameworkElement>();
      this.\u0023\u003Dz0jC4zrgOWDNx(_param1, tpPoAJhpeVw6ares.\u0023\u003DzS7JsfCE\u003D, frameworkElement);
    }
  }

  private void \u0023\u003Dz0jC4zrgOWDNx(
    Point _param1,
    IAxis _param2,
    FrameworkElement _param3)
  {
    if (_param3 == null || _param2 == null)
      return;
    \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk modifierAxisCanvas = _param2.get_ModifierAxisCanvas();
    bool isPolarAxis = _param2.get_IsPolarAxis();
    \u0023\u003DzUJpBz2W8IzAtBIqVtQXHB99xo8DgCb_3ha_wTIg\u003D b99xo8DgCb3haWTig = this.Services.\u0023\u003Dz2VqWonc\u003D<\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSXWkz2jl56XJoPdfqB4\u003D>().\u0023\u003DzhGnS3f5TTzO8();
    Point point = isPolarAxis ? b99xo8DgCb3haWTig.\u0023\u003Dz8miGAzg\u003D(_param1) : this.ParentSurface.\u0023\u003DzBgWxEdRxHdEh().TranslatePoint(_param1, (IHitTestable) _param2);
    if (dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.\u0023\u003DzxGhbraO0gg9\u0024(point, _param2))
    {
      _param3.DataContext = (object) this.\u0023\u003DzU0tYbfdnROi1(_param2, _param1);
      if (isPolarAxis)
      {
        this.\u0023\u003Dzt\u0024k5cmo8VKnKuCBfsg\u003D\u003D(_param3, point);
      }
      else
      {
        _param2.\u0023\u003DzXOlF_vImljp4(_param3, point);
        _param2.\u0023\u003Dz07\u0024lhqBRmUuR(_param3, point);
      }
      modifierAxisCanvas.\u0023\u003DzH0osWQkV_Y8_((object) _param3, -1);
    }
    else
      modifierAxisCanvas.\u0023\u003DziYdJ\u00246cCiBha((object) _param3);
  }

  private static bool \u0023\u003DzxGhbraO0gg9\u0024(
    Point _param0,
    IAxis _param1)
  {
    double num1;
    double num2;
    if (_param1.IsHorizontalAxis)
    {
      num1 = _param0.X;
      num2 = _param1.ActualWidth;
    }
    else
    {
      num1 = _param0.Y;
      num2 = _param1.ActualHeight;
    }
    return num1 <= num2 && num1 >= 0.0;
  }

  private void \u0023\u003Dzt\u0024k5cmo8VKnKuCBfsg\u003D\u003D(
    FrameworkElement _param1,
    Point _param2)
  {
    Point point = _param2;
    _param1.SetValue(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.\u0023\u003DzasJeVgQ\u003D, (object) 0.0);
    _param1.SetValue(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.\u0023\u003DzHEgPKfijwe68, (object) point.X);
  }

  protected void \u0023\u003DzaHyDUm8F3XtsCygXSA\u003D\u003D()
  {
    this.\u0023\u003DzdwXUIyhDFvIMX\u0024julBrBi\u0024c\u003D(this.\u0023\u003DzJ9TR0V6N30XqYK5Fsg\u003D\u003D);
    this.\u0023\u003DzdwXUIyhDFvIMX\u0024julBrBi\u0024c\u003D(this.\u0023\u003Dzlpam1C6zshu\u0024hyb5gg\u003D\u003D);
  }

  private void \u0023\u003DzdwXUIyhDFvIMX\u0024julBrBi\u0024c\u003D(
    IEnumerable<Tuple<IAxis, FrameworkElement>> _param1)
  {
    if (_param1 == null || this.ParentSurface == null)
      return;
    foreach (Tuple<IAxis, FrameworkElement> tuple in _param1)
      tuple.Item1.get_ModifierAxisCanvas().\u0023\u003DziYdJ\u00246cCiBha((object) tuple.Item2);
  }

  protected void \u0023\u003DzQrcA6OyHGRkq()
  {
    this.\u0023\u003DzhoHxkSVR\u0024FsTBj1bDA\u003D\u003D();
    this.\u0023\u003Dz\u0024W78w9bAp8kl9UxrOA\u003D\u003D();
  }

  private void \u0023\u003DzhoHxkSVR\u0024FsTBj1bDA\u003D\u003D()
  {
    this.\u0023\u003DzdwXUIyhDFvIMX\u0024julBrBi\u0024c\u003D(this.\u0023\u003DzJ9TR0V6N30XqYK5Fsg\u003D\u003D);
    this.\u0023\u003DzJ9TR0V6N30XqYK5Fsg\u003D\u003D = this.\u0023\u003DzZy0RKGi5V\u0024pH(this.XAxes, this.AxisLabelTemplate);
  }

  private void \u0023\u003Dz\u0024W78w9bAp8kl9UxrOA\u003D\u003D()
  {
    this.\u0023\u003DzdwXUIyhDFvIMX\u0024julBrBi\u0024c\u003D(this.\u0023\u003Dzlpam1C6zshu\u0024hyb5gg\u003D\u003D);
    this.\u0023\u003Dzlpam1C6zshu\u0024hyb5gg\u003D\u003D = this.\u0023\u003DzZy0RKGi5V\u0024pH(this.YAxes, this.AxisLabelTemplate);
  }

  protected IEnumerable<Tuple<IAxis, FrameworkElement>> \u0023\u003DzZy0RKGi5V\u0024pH(
    IEnumerable<IAxis> _param1,
    ControlTemplate _param2)
  {
    dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.\u0023\u003DzaWz8dVeBY\u0024tX1maSKMAhb14\u003D veByTX1maSkmAhb14 = new dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.\u0023\u003DzaWz8dVeBY\u0024tX1maSKMAhb14\u003D();
    veByTX1maSkmAhb14.\u0023\u003DzRRvwDu67s9Rm = this;
    veByTX1maSkmAhb14.\u0023\u003DzcyfV7AfhGRGy = _param2;
    IEnumerable<Tuple<IAxis, FrameworkElement>> tuples = (IEnumerable<Tuple<IAxis, FrameworkElement>>) null;
    if (_param1 != null)
      tuples = (IEnumerable<Tuple<IAxis, FrameworkElement>>) _param1.Select<IAxis, Tuple<IAxis, FrameworkElement>>(new Func<IAxis, Tuple<IAxis, FrameworkElement>>(veByTX1maSkmAhb14.\u0023\u003Dz3skGe7kidRzYfBsijg\u003D\u003D)).ToArray<Tuple<IAxis, FrameworkElement>>();
    return tuples;
  }

  protected dje_zWLGYTTT5DUFM55EFRPBBAZMZXPMVBQNX4VDZEWMJLU768RLDDSRVTC6SXVJQ2DSMRBMWZUQJ5VY6CVZ_ejd \u0023\u003DzBv1vB\u0024LEKSF4(
    ControlTemplate _param1,
    \u0023\u003DzAfUZ1hld3Aj4_oK9JVqPo8HI_6JOBYhgDQ\u003D\u003D _param2,
    object _param3)
  {
    dje_zWLGYTTT5DUFM55EFRPBBAZMZXPMVBQNX4VDZEWMJLU768RLDDSRVTC6SXVJQ2DSMRBMWZUQJ5VY6CVZ_ejd dsmrbmwzuqJ5Vy6CvzEjd = (dje_zWLGYTTT5DUFM55EFRPBBAZMZXPMVBQNX4VDZEWMJLU768RLDDSRVTC6SXVJQ2DSMRBMWZUQJ5VY6CVZ_ejd) null;
    if (_param1 != null)
    {
      dje_z5Q63TZN6Q7Z55DU6ZVEKXJV8USEH64ZT7ZSGP6B98ECYG24PF5CNJED6UPTHJB96FA_ejd cnjeD6UpthjB96FaEjd = new dje_z5Q63TZN6Q7Z55DU6ZVEKXJV8USEH64ZT7ZSGP6B98ECYG24PF5CNJED6UPTHJB96FA_ejd();
      cnjeD6UpthjB96FaEjd.Template = _param1;
      cnjeD6UpthjB96FaEjd.DataContext = _param3;
      cnjeD6UpthjB96FaEjd.Selector = _param2;
      dsmrbmwzuqJ5Vy6CvzEjd = (dje_zWLGYTTT5DUFM55EFRPBBAZMZXPMVBQNX4VDZEWMJLU768RLDDSRVTC6SXVJQ2DSMRBMWZUQJ5VY6CVZ_ejd) cnjeD6UpthjB96FaEjd;
    }
    return dsmrbmwzuqJ5Vy6CvzEjd;
  }

  protected abstract void \u0023\u003DzY0Ucom6W\u0024E0ZkvcKcA\u003D\u003D();

  protected abstract void \u0023\u003Dz9otdS\u0024TJZ4U8t8zXqw\u003D\u003D();

  private static void \u0023\u003DzbVg\u0024qRIz823IPqWt\u0024A\u003D\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    ((dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd) _param0)?.\u0023\u003Dz9otdS\u0024TJZ4U8t8zXqw\u003D\u003D();
  }

  private static void \u0023\u003Dzwur\u0024mqt3kPJHpkPRjQ\u003D\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    ((dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd) _param0)?.\u0023\u003DzY0Ucom6W\u0024E0ZkvcKcA\u003D\u003D();
  }

  private static void \u0023\u003DzbxLNaq5ZjEofA\u0024XB3WbNaIs\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd z44XsujjqgvnnD2Ejd))
      return;
    ((FrameworkElement) z44XsujjqgvnnD2Ejd.DefaultAxisLabelTemplateSelector).Style = (Style) _param1.NewValue;
  }

  private static void \u0023\u003Dz8WfI5kqsN6kmTe6TXhhzlCo\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd z44XsujjqgvnnD2Ejd))
      return;
    ((FrameworkElement) z44XsujjqgvnnD2Ejd.DefaultTooltipLabelTemplateSelector).Style = (Style) _param1.NewValue;
  }

  private static void \u0023\u003DzesUKUhGoyfNgYFzSJw\u003D\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd z44XsujjqgvnnD2Ejd))
      return;
    DelayActionHelper zIxFmGbTnpwO0 = z44XsujjqgvnnD2Ejd.\u0023\u003DzIxFmGbTNpwO0;
    if (zIxFmGbTnpwO0 == null)
      return;
    zIxFmGbTnpwO0.Interval = (double) _param1.NewValue;
  }

  [Serializable]
  private new sealed class SomeClass34343383
  {
    public static readonly dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.SomeClass34343383 SomeMethond0343 = new dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.SomeClass34343383();
    public static Func<Tuple<IAxis, FrameworkElement>, FrameworkElement> \u0023\u003Dz5BIbVz_RRJEtvtlEuw\u003D\u003D;
    public static Func<Tuple<IAxis, FrameworkElement>, FrameworkElement> \u0023\u003DzQo1YXL32S5xfzg99Jw\u003D\u003D;

    internal FrameworkElement \u0023\u003Dz\u0024J5BAtSL9mu4zONFt126S5i5D4WR(
      Tuple<IAxis, FrameworkElement> _param1)
    {
      return _param1.Item2;
    }

    internal FrameworkElement \u0023\u003DzddQllHN_1gETercjTb3EPMwlFP4J(
      Tuple<IAxis, FrameworkElement> _param1)
    {
      return _param1.Item2;
    }
  }

  private sealed class \u0023\u003DzPZHe8kTpPoA\u0024JhpeVw6ares\u003D
  {
    public IAxis \u0023\u003DzS7JsfCE\u003D;

    internal bool \u0023\u003DzFOzwFNi9b6vX6jZHz0MsrcI\u003D(
      Tuple<IAxis, FrameworkElement> _param1)
    {
      return _param1.Item1 == this.\u0023\u003DzS7JsfCE\u003D;
    }
  }

  private sealed class \u0023\u003DzaWz8dVeBY\u0024tX1maSKMAhb14\u003D
  {
    public dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd \u0023\u003DzRRvwDu67s9Rm;
    public ControlTemplate \u0023\u003DzcyfV7AfhGRGy;

    internal Tuple<IAxis, FrameworkElement> \u0023\u003Dz3skGe7kidRzYfBsijg\u003D\u003D(
      IAxis _param1)
    {
      return new Tuple<IAxis, FrameworkElement>(_param1, (FrameworkElement) this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzBv1vB\u0024LEKSF4(this.\u0023\u003DzcyfV7AfhGRGy, this.\u0023\u003DzRRvwDu67s9Rm.AxisLabelTemplateSelector, (object) null));
    }
  }

  private sealed class \u0023\u003Dzf1B6wwNJbrqhpNKVkrNPKgU\u003D
  {
    public IAxis \u0023\u003Dz4bf8Oyc\u003D;

    internal bool \u0023\u003DzftM36zmTSR2NGmsCOruf4V4\u003D(
      Tuple<IAxis, FrameworkElement> _param1)
    {
      return _param1.Item1 == this.\u0023\u003Dz4bf8Oyc\u003D;
    }
  }
}
