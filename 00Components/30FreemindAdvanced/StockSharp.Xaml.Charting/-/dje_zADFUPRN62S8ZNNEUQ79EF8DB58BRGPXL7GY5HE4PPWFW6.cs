// Decompiled with JetBrains decompiler
// Type: -.dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Editors.Helpers;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Xaml;
using StockSharp.BusinessEntities;
using StockSharp.Charting;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Xaml;
using StockSharp.Xaml.Charting;
using StockSharp.Xaml.Charting.Ultrachart;
using StockSharp.Xaml.Charting.Visuals.Annotations;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

#nullable enable
namespace StockSharp.Xaml.Charting;

internal sealed class dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd : 
  dje_zZTHFGMBWG8W4RABP3CZXW8NL5AQ7G4LTKKFALWWSGS7J5ARQQP83C_ejd,
  IComponentConnector
{
  
  private readonly 
  #nullable disable
  ChartArea \u0023\u003DzeckSod0\u003D;
  
  private readonly \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh5kehUw\u0024c\u0024fhZpDlpYA\u003D \u0023\u003DzfS3q6Qc\u003D;
  
  private readonly PairSet<AnnotationBase, ChartAnnotation> \u0023\u003DzCqbxaY8TFdGX = new PairSet<AnnotationBase, ChartAnnotation>();
  
  private readonly HashSet<AnnotationBase> \u0023\u003Dz58j\u0024CeQi5Bfr = new HashSet<AnnotationBase>();
  
  private RulerAnnotation \u0023\u003Dz8zsB7zNofSMS;
  
  private bool \u0023\u003DzmHijKtV8bKDY;
  
  private dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCBCF2R6BFXD35AJMNZAG8VPWQ3UE6ZFKM53T5BWGTPBKV9Z_ejd \u0023\u003DzUdabyQU\u003D;
  
  public static readonly DependencyProperty \u0023\u003DzEaRzkw2Bl16I = DependencyProperty.Register(XXX.SSS(-539330433), typeof (ChartAnnotationTypes), typeof (dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd), new PropertyMetadata((object) ChartAnnotationTypes.None, new PropertyChangedCallback(dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd.\u0023\u003DzLJLGN_0FzU1B)));
  
  private bool \u0023\u003DzQGCmQMjHdLKS;

  public dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd(
    ChartArea _param1,
    \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh5kehUw\u0024c\u0024fhZpDlpYA\u003D _param2)
  {
    this.InitializeComponent();
    this.\u0023\u003DzeckSod0\u003D = _param1 ?? throw new ArgumentNullException(XXX.SSS(-539330159));
    this.\u0023\u003DzfS3q6Qc\u003D = _param2 ?? throw new ArgumentNullException(XXX.SSS(-539330454));
  }

  private Chart \u0023\u003DzGuZ8w82B3fMJ() => this.\u0023\u003DzeckSod0\u003D.Chart as Chart;

  private dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCBCF2R6BFXD35AJMNZAG8VPWQ3UE6ZFKM53T5BWGTPBKV9Z_ejd \u0023\u003DzqRVOnAc\u003D()
  {
    return this.\u0023\u003DzUdabyQU\u003D ?? (this.\u0023\u003DzUdabyQU\u003D = new dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCBCF2R6BFXD35AJMNZAG8VPWQ3UE6ZFKM53T5BWGTPBKV9Z_ejd());
  }

  private static void \u0023\u003DzLJLGN_0FzU1B(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    ((dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd) _param0).\u0023\u003DzNJ7MG_c\u0024k1K0((ChartAnnotationTypes) _param1.NewValue);
  }

  public ChartAnnotationTypes UserAnnotationType
  {
    get
    {
      return (ChartAnnotationTypes) this.GetValue(dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd.\u0023\u003DzEaRzkw2Bl16I);
    }
    set
    {
      this.SetValue(dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd.\u0023\u003DzEaRzkw2Bl16I, (object) value);
    }
  }

  private void \u0023\u003Dz_I\u0024qdC0\u003D()
  {
    if (this.\u0023\u003Dz8zsB7zNofSMS == null)
      return;
    this.ParentSurface.get_Annotations().Remove((\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D) this.\u0023\u003Dz8zsB7zNofSMS);
    this.\u0023\u003Dz8zsB7zNofSMS = (RulerAnnotation) null;
  }

  protected override AnnotationBase \u0023\u003DzWj46Xvc\u003D(Type _param1, Style _param2)
  {
    if (_param1 != typeof (RulerAnnotation))
      return base.\u0023\u003DzWj46Xvc\u003D(_param1, _param2);
    this.\u0023\u003Dz_I\u0024qdC0\u003D();
    IChartCandleElement element = ((IEnumerable) this.\u0023\u003DzeckSod0\u003D.Elements).OfType<IChartCandleElement>().FirstOrDefault<IChartCandleElement>();
    double num = (double) ((Decimal?) ((SecurityMessage) (element == null ? (SubscriptionBase<Subscription>) null : (SubscriptionBase<Subscription>) ((Chart) this.\u0023\u003DzeckSod0\u003D.Chart).TryGetSubscription((IChartElement) element))?.MarketData).PriceStep ?? 0.01M);
    RulerAnnotation rulerAnnotation1 = new RulerAnnotation();
    rulerAnnotation1.YAxisId = this.YAxisId;
    rulerAnnotation1.XAxisId = this.XAxisId;
    rulerAnnotation1.PriceStep = num;
    rulerAnnotation1.RemoveOnClick = true;
    RulerAnnotation rulerAnnotation2 = rulerAnnotation1;
    this.\u0023\u003Dz8zsB7zNofSMS = rulerAnnotation1;
    RulerAnnotation rulerAnnotation3 = rulerAnnotation2;
    this.ParentSurface.get_Annotations().Add((\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D) rulerAnnotation3);
    return (AnnotationBase) rulerAnnotation3;
  }

  public override void \u0023\u003DzsXEfcKpqchyX(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
    if (MathHelper.IsNaN((double) this.YAxis.\u0023\u003DzACwLhyc\u003D(_param1.\u0023\u003DztkyOk5amPcz3().Y)))
      this.UserAnnotationType = ChartAnnotationTypes.None;
    else
      base.\u0023\u003DzsXEfcKpqchyX(_param1);
  }

  private void \u0023\u003DzNJ7MG_c\u0024k1K0(ChartAnnotationTypes _param1)
  {
    if (_param1 == ChartAnnotationTypes.None)
    {
      CollectionHelper.ForEach<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>((IEnumerable<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>) this.\u0023\u003DzfS3q6Qc\u003D, dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dzm5iuiBtfSUf6PnApUQ\u003D\u003D ?? (dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dzm5iuiBtfSUf6PnApUQ\u003D\u003D = new Action<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>(dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dzf_OX3zxNZIoqduZEY_a_\u0024gQ\u003D)));
      this.AnnotationType = (Type) null;
      this.IsEnabled = false;
    }
    else
    {
      Type type = _param1.\u0023\u003Dz6wKFLhaRAAzr();
      string key = type.Name + XXX.SSS(-539433382);
      if (this.Resources.Contains((object) key))
        this.AnnotationStyle = (Style) this.Resources[(object) key];
      this.AnnotationType = type;
      this.IsEnabled = true;
    }
  }

  private void dje_zW48GVEHV99U6ZJQ_ejd(object _param1, EventArgs _param2)
  {
    AnnotationBase annotation = (AnnotationBase) this.Annotation;
    ChartAnnotationTypes userAnnotationType = this.UserAnnotationType;
    this.UserAnnotationType = ChartAnnotationTypes.None;
    this.AnnotationType = (Type) null;
    this.IsEnabled = false;
    if (annotation == null)
      return;
    bool flag = !(annotation is RulerAnnotation);
    this.\u0023\u003DzbSFwQBfxHeQf(annotation, flag);
    this.\u0023\u003DzIehZCHK_gy_6(annotation);
    ChartAnnotation chartAnnotation = new ChartAnnotation()
    {
      Type = userAnnotationType
    };
    ((KeyedCollection<AnnotationBase, ChartAnnotation>) this.\u0023\u003DzCqbxaY8TFdGX)[annotation] = chartAnnotation;
    ((ICollection<IChartElement>) this.\u0023\u003DzeckSod0\u003D.Elements).Add((IChartElement) chartAnnotation);
    Chart chart = this.\u0023\u003DzGuZ8w82B3fMJ();
    if (chart != null)
    {
      chart.\u0023\u003Dz49m\u0024QLWwKQs9(chartAnnotation);
      chart.\u0023\u003Dz5mEkRaZSEt9m(chartAnnotation, this.\u0023\u003DzCVYc\u0024mw923hM(annotation));
    }
    if (!annotation.IsSelected)
      return;
    Keyboard.Focus((IInputElement) annotation);
    this.\u0023\u003DzGuZ8w82B3fMJ()?.\u0023\u003DzSZqzgFQySfHr(chartAnnotation, this.\u0023\u003DzCVYc\u0024mw923hM(annotation));
  }

  private bool \u0023\u003DztnHdtisTy1nl(AnnotationBase _param1)
  {
    return this.\u0023\u003Dz58j\u0024CeQi5Bfr.Contains(_param1);
  }

  private void \u0023\u003DzbSFwQBfxHeQf(AnnotationBase _param1, bool _param2)
  {
    dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd.\u0023\u003DzNwXxhkJq9Llz3ah\u0024z2LRQl4\u003D jq9Llz3ahZ2LrQl4 = new dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd.\u0023\u003DzNwXxhkJq9Llz3ah\u0024z2LRQl4\u003D();
    jq9Llz3ahZ2LrQl4.\u0023\u003DzRRvwDu67s9Rm = this;
    jq9Llz3ahZ2LrQl4.\u0023\u003DzLXiKo\u0024A\u003D = _param1;
    if (_param2 == this.\u0023\u003DztnHdtisTy1nl(jq9Llz3ahZ2LrQl4.\u0023\u003DzLXiKo\u0024A\u003D))
      return;
    if (_param2)
      this.\u0023\u003Dz58j\u0024CeQi5Bfr.Add(jq9Llz3ahZ2LrQl4.\u0023\u003DzLXiKo\u0024A\u003D);
    else
      this.\u0023\u003Dz58j\u0024CeQi5Bfr.Remove(jq9Llz3ahZ2LrQl4.\u0023\u003DzLXiKo\u0024A\u003D);
    jq9Llz3ahZ2LrQl4.\u0023\u003DzLXiKo\u0024A\u003D.IsEditable = _param2;
    jq9Llz3ahZ2LrQl4.\u0023\u003DzLXiKo\u0024A\u003D.CanEditText = _param2;
    jq9Llz3ahZ2LrQl4.\u0023\u003DzLXiKo\u0024A\u003D.FocusVisualStyle = (Style) null;
    jq9Llz3ahZ2LrQl4.\u0023\u003DztKpRxYMDxV\u00243 = new DelegateCommand<AnnotationBase>(new Action<AnnotationBase>(jq9Llz3ahZ2LrQl4.\u0023\u003DqkJAvLS57TQDltohwMlvUc8ToXY9\u0024wVsEDT1RmCTmQAl1TTvH5MP818enEgRePrx8));
    PopupMenu popupMenu1 = new PopupMenu();
    CommonBarItemCollection items1 = popupMenu1.Items;
    BarButtonItem barButtonItem1 = new BarButtonItem();
    barButtonItem1.Glyph = ThemedIconsExtension.GetImage(XXX.SSS(-539328527));
    barButtonItem1.Content = (object) (LocalizedStrings.Properties + XXX.SSS(-539330490));
    barButtonItem1.Command = (ICommand) new DelegateCommand<AnnotationBase>(new Action<AnnotationBase>(jq9Llz3ahZ2LrQl4.\u0023\u003Dzn4dNgi0mxbbCP\u0024DWxprQswg\u003D));
    barButtonItem1.CommandParameter = (object) jq9Llz3ahZ2LrQl4.\u0023\u003DzLXiKo\u0024A\u003D;
    items1.Add((IBarItem) barButtonItem1);
    CommonBarItemCollection items2 = popupMenu1.Items;
    BarButtonItem barButtonItem2 = new BarButtonItem();
    barButtonItem2.Glyph = ThemedIconsExtension.GetImage(XXX.SSS(-539328570));
    barButtonItem2.Content = (object) LocalizedStrings.Delete;
    barButtonItem2.Command = (ICommand) jq9Llz3ahZ2LrQl4.\u0023\u003DztKpRxYMDxV\u00243;
    barButtonItem2.CommandParameter = (object) jq9Llz3ahZ2LrQl4.\u0023\u003DzLXiKo\u0024A\u003D;
    items2.Add((IBarItem) barButtonItem2);
    PopupMenu popupMenu2 = popupMenu1;
    if (_param2)
      BarManager.SetDXContextMenu((UIElement) jq9Llz3ahZ2LrQl4.\u0023\u003DzLXiKo\u0024A\u003D, (IPopupControl) popupMenu2);
    if (_param2)
    {
      jq9Llz3ahZ2LrQl4.\u0023\u003DzLXiKo\u0024A\u003D.KeyDown += new KeyEventHandler(jq9Llz3ahZ2LrQl4.\u0023\u003Dq7mcVfPvAB3Y4duXTm_bwIHboyLUu0\u0024G63ba4grB5MsPr1uOUJsYns2NOLvS5EdHu_qwqq\u0024dzh7uvWZ_cRJz2Qw\u003D\u003D);
      jq9Llz3ahZ2LrQl4.\u0023\u003DzLXiKo\u0024A\u003D.Selected += new EventHandler(jq9Llz3ahZ2LrQl4.\u0023\u003DqsO0aV2_u3gwC4_hwIR4BeABhYV_7m2A\u0024ZOJFQhD2VppSu5lNDzXhb76mKBs5qy0DljWs2YahWqjBYAlTloZS5Q\u003D\u003D);
      jq9Llz3ahZ2LrQl4.\u0023\u003DzLXiKo\u0024A\u003D.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(jq9Llz3ahZ2LrQl4.\u0023\u003DqdGbJy3AROhfZcOB8qLyu\u0024jJUN5YXrNmxgUQMxhoWOUyOvmpRAlpHEMxwLY7VT_bNf7h7A1Gpy0sisqLkbC4SE1lJB8L2p27M0jUbIyr1qsE\u003D);
      jq9Llz3ahZ2LrQl4.\u0023\u003DzLXiKo\u0024A\u003D.Unselected += new EventHandler(jq9Llz3ahZ2LrQl4.\u0023\u003DqwGFSEwECiuHXXjEFGHe8QY\u00247phLKFc6Uur2mC93l2Vt3u_d_e1\u00246awwFvq85QGOeUttMmP5CScJ0f9g25CtXrQ\u003D\u003D);
      jq9Llz3ahZ2LrQl4.\u0023\u003DzLXiKo\u0024A\u003D.DragStarted += new EventHandler<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8EceYoYimX3HGDNfbrY\u003D>(jq9Llz3ahZ2LrQl4.\u0023\u003DqyB2RaLDacQfKp3hzEOo9jtCYmWI0NuQIdJ1OucO7P2ZS1UNCupBPv2q788ONhBifj7\u00242eFsTAo3xcpoGsx2qiA\u003D\u003D);
      jq9Llz3ahZ2LrQl4.\u0023\u003DzLXiKo\u0024A\u003D.DragEnded += new EventHandler<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8EceYoYimX3HGDNfbrY\u003D>(jq9Llz3ahZ2LrQl4.\u0023\u003DqxZFddEyfLMrUR0cfuiIOvt2\u0024\u0024WepGdv1wxLS58FfD260\u0024X298XW8T6e3WebsLZDqwhKx85FubKoFI64m1eByJA\u003D\u003D);
    }
    else
    {
      jq9Llz3ahZ2LrQl4.\u0023\u003DzLXiKo\u0024A\u003D.KeyDown -= new KeyEventHandler(jq9Llz3ahZ2LrQl4.\u0023\u003Dq7mcVfPvAB3Y4duXTm_bwIHboyLUu0\u0024G63ba4grB5MsPr1uOUJsYns2NOLvS5EdHu_qwqq\u0024dzh7uvWZ_cRJz2Qw\u003D\u003D);
      jq9Llz3ahZ2LrQl4.\u0023\u003DzLXiKo\u0024A\u003D.Selected -= new EventHandler(jq9Llz3ahZ2LrQl4.\u0023\u003DqsO0aV2_u3gwC4_hwIR4BeABhYV_7m2A\u0024ZOJFQhD2VppSu5lNDzXhb76mKBs5qy0DljWs2YahWqjBYAlTloZS5Q\u003D\u003D);
      jq9Llz3ahZ2LrQl4.\u0023\u003DzLXiKo\u0024A\u003D.PreviewMouseLeftButtonDown -= new MouseButtonEventHandler(jq9Llz3ahZ2LrQl4.\u0023\u003DqdGbJy3AROhfZcOB8qLyu\u0024jJUN5YXrNmxgUQMxhoWOUyOvmpRAlpHEMxwLY7VT_bNf7h7A1Gpy0sisqLkbC4SE1lJB8L2p27M0jUbIyr1qsE\u003D);
      jq9Llz3ahZ2LrQl4.\u0023\u003DzLXiKo\u0024A\u003D.Unselected -= new EventHandler(jq9Llz3ahZ2LrQl4.\u0023\u003DqwGFSEwECiuHXXjEFGHe8QY\u00247phLKFc6Uur2mC93l2Vt3u_d_e1\u00246awwFvq85QGOeUttMmP5CScJ0f9g25CtXrQ\u003D\u003D);
      jq9Llz3ahZ2LrQl4.\u0023\u003DzLXiKo\u0024A\u003D.DragStarted -= new EventHandler<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8EceYoYimX3HGDNfbrY\u003D>(jq9Llz3ahZ2LrQl4.\u0023\u003DqyB2RaLDacQfKp3hzEOo9jtCYmWI0NuQIdJ1OucO7P2ZS1UNCupBPv2q788ONhBifj7\u00242eFsTAo3xcpoGsx2qiA\u003D\u003D);
      jq9Llz3ahZ2LrQl4.\u0023\u003DzLXiKo\u0024A\u003D.DragEnded -= new EventHandler<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8EceYoYimX3HGDNfbrY\u003D>(jq9Llz3ahZ2LrQl4.\u0023\u003DqxZFddEyfLMrUR0cfuiIOvt2\u0024\u0024WepGdv1wxLS58FfD260\u0024X298XW8T6e3WebsLZDqwhKx85FubKoFI64m1eByJA\u003D\u003D);
    }
  }

  private void \u0023\u003DzIehZCHK_gy_6(AnnotationBase _param1)
  {
    dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd.\u0023\u003DzpcA\u0024d1dDoDCwiev7trI4NY0\u003D doDcwiev7trI4Ny0 = new dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd.\u0023\u003DzpcA\u0024d1dDoDCwiev7trI4NY0\u003D();
    doDcwiev7trI4Ny0.\u0023\u003DzRRvwDu67s9Rm = this;
    doDcwiev7trI4Ny0.\u0023\u003Dz2vouRgM\u003D = _param1;
    if (this.\u0023\u003DzGuZ8w82B3fMJ() == null)
      return;
    doDcwiev7trI4Ny0.\u0023\u003Dz2vouRgM\u003D.Selected += new EventHandler(doDcwiev7trI4Ny0.\u0023\u003DzO2Calw5PtJlYUGt7JwufXn8\u003D);
    doDcwiev7trI4Ny0.\u0023\u003Dz2vouRgM\u003D.Unselected += new EventHandler(doDcwiev7trI4Ny0.\u0023\u003DzMAzDDfbdHsJSDAK_mit5cEE\u003D);
    List<DependencyProperty> dependencyPropertyList = new List<DependencyProperty>()
    {
      AnnotationBase.IsHiddenProperty,
      AnnotationBase.IsEditableProperty,
      AnnotationBase.X1Property,
      AnnotationBase.X2Property,
      AnnotationBase.Y1Property,
      AnnotationBase.Y2Property,
      AnnotationBase.CoordinateModeProperty
    };
    if (doDcwiev7trI4Ny0.\u0023\u003Dz2vouRgM\u003D is LineAnnotationBase)
    {
      dependencyPropertyList.Add(LineAnnotationBase.StrokeThicknessProperty);
      dependencyPropertyList.Add(LineAnnotationBase.StrokeProperty);
    }
    if (!(doDcwiev7trI4Ny0.\u0023\u003Dz2vouRgM\u003D is HorizontalLineAnnotation))
    {
      if (!(doDcwiev7trI4Ny0.\u0023\u003Dz2vouRgM\u003D is VerticalLineAnnotation))
      {
        if (!(doDcwiev7trI4Ny0.\u0023\u003Dz2vouRgM\u003D is BoxAnnotation))
        {
          if (!(doDcwiev7trI4Ny0.\u0023\u003Dz2vouRgM\u003D is TextAnnotation))
          {
            if (doDcwiev7trI4Ny0.\u0023\u003Dz2vouRgM\u003D is RulerAnnotation)
              dependencyPropertyList.Add(Control.BackgroundProperty);
          }
          else
          {
            dependencyPropertyList.Add(TextAnnotation.TextProperty);
            dependencyPropertyList.Add(Control.BackgroundProperty);
            dependencyPropertyList.Add(Control.BorderBrushProperty);
            dependencyPropertyList.Add(Control.BorderThicknessProperty);
          }
        }
        else
        {
          dependencyPropertyList.Add(Control.BackgroundProperty);
          dependencyPropertyList.Add(Control.BorderBrushProperty);
          dependencyPropertyList.Add(Control.BorderThicknessProperty);
        }
      }
      else
        dependencyPropertyList.Add(FrameworkElement.VerticalAlignmentProperty);
    }
    else
      dependencyPropertyList.Add(FrameworkElement.HorizontalAlignmentProperty);
    dependencyPropertyList.ForEach(new Action<DependencyProperty>(doDcwiev7trI4Ny0.\u0023\u003Dz2YagnOQq\u0024fEh5b7gci8gwU8\u003D));
  }

  private ChartDrawData.AnnotationData \u0023\u003DzCVYc\u0024mw923hM(AnnotationBase _param1)
  {
    dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd.\u0023\u003DzN3EMs6Vm6DExIKYZZOKCa\u0024w\u003D vm6DexIkyzzokCaW;
    vm6DexIkyzzokCaW.\u0023\u003Dz2vouRgM\u003D = _param1;
    ChartDrawData.AnnotationData annotationData = new ChartDrawData.AnnotationData();
    vm6DexIkyzzokCaW.\u0023\u003DzFlkZpfJp6G9R = vm6DexIkyzzokCaW.\u0023\u003Dz2vouRgM\u003D.XAxis?.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D();
    annotationData.IsVisible = new bool?(!vm6DexIkyzzokCaW.\u0023\u003Dz2vouRgM\u003D.IsHidden);
    annotationData.IsEditable = new bool?(this.\u0023\u003DztnHdtisTy1nl(vm6DexIkyzzokCaW.\u0023\u003Dz2vouRgM\u003D));
    annotationData.CoordinateMode = new AnnotationCoordinateMode?(vm6DexIkyzzokCaW.\u0023\u003Dz2vouRgM\u003D.CoordinateMode);
    annotationData.X1 = dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd.\u0023\u003DqilGCdp9aFjNd5SLe7I3UCUsoNPZA2A4LqGx2E87OFI3Oof40izFV2yt8T4S4jCDk(vm6DexIkyzzokCaW.\u0023\u003Dz2vouRgM\u003D.X1, ref vm6DexIkyzzokCaW);
    annotationData.X2 = dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd.\u0023\u003DqilGCdp9aFjNd5SLe7I3UCUsoNPZA2A4LqGx2E87OFI3Oof40izFV2yt8T4S4jCDk(vm6DexIkyzzokCaW.\u0023\u003Dz2vouRgM\u003D.X2, ref vm6DexIkyzzokCaW);
    annotationData.Y1 = dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd.\u0023\u003DqTAamYC40X7Nd2JjKg0gRJnEFqikmgfX0UtSubtvET0wvj813KCUIgmYrc9ybxFYK(vm6DexIkyzzokCaW.\u0023\u003Dz2vouRgM\u003D.Y1, ref vm6DexIkyzzokCaW);
    annotationData.Y2 = dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd.\u0023\u003DqTAamYC40X7Nd2JjKg0gRJnEFqikmgfX0UtSubtvET0wvj813KCUIgmYrc9ybxFYK(vm6DexIkyzzokCaW.\u0023\u003Dz2vouRgM\u003D.Y2, ref vm6DexIkyzzokCaW);
    if (vm6DexIkyzzokCaW.\u0023\u003Dz2vouRgM\u003D is LineAnnotationBase z2vouRgM1)
    {
      annotationData.Stroke = z2vouRgM1.Stroke;
      annotationData.Thickness = new Thickness?(new Thickness(z2vouRgM1.StrokeThickness));
    }
    if (!(vm6DexIkyzzokCaW.\u0023\u003Dz2vouRgM\u003D is HorizontalLineAnnotation z2vouRgM5))
    {
      if (!(vm6DexIkyzzokCaW.\u0023\u003Dz2vouRgM\u003D is VerticalLineAnnotation z2vouRgM4))
      {
        if (!(vm6DexIkyzzokCaW.\u0023\u003Dz2vouRgM\u003D is BoxAnnotation z2vouRgM3))
        {
          if (vm6DexIkyzzokCaW.\u0023\u003Dz2vouRgM\u003D is TextAnnotation z2vouRgM2)
          {
            annotationData.Foreground = z2vouRgM2.Foreground;
            annotationData.Text = z2vouRgM2.Text;
            annotationData.Fill = z2vouRgM2.Background;
            annotationData.Stroke = z2vouRgM2.BorderBrush;
            annotationData.Thickness = new Thickness?(z2vouRgM2.BorderThickness);
          }
        }
        else
        {
          annotationData.Fill = z2vouRgM3.Background;
          annotationData.Stroke = z2vouRgM3.BorderBrush;
          annotationData.Thickness = new Thickness?(z2vouRgM3.BorderThickness);
        }
      }
      else
      {
        annotationData.VerticalAlignment = new VerticalAlignment?(z2vouRgM4.VerticalAlignment);
        annotationData.LabelPlacement = new LabelPlacement?(z2vouRgM4.LabelPlacement);
        annotationData.ShowLabel = new bool?(z2vouRgM4.ShowLabel);
      }
    }
    else
    {
      annotationData.HorizontalAlignment = new HorizontalAlignment?(z2vouRgM5.HorizontalAlignment);
      annotationData.LabelPlacement = new LabelPlacement?(z2vouRgM5.LabelPlacement);
      annotationData.ShowLabel = new bool?(z2vouRgM5.ShowLabel);
    }
    return annotationData;
  }

  public void \u0023\u003Dz\u0024abmkXc\u003D(ChartAnnotation _param1)
  {
    AnnotationBase annotationBase;
    if (!this.\u0023\u003DzCqbxaY8TFdGX.TryGetKey(_param1, ref annotationBase))
      return;
    this.\u0023\u003DzfS3q6Qc\u003D.Remove((\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D) annotationBase);
    ((KeyedCollection<AnnotationBase, ChartAnnotation>) this.\u0023\u003DzCqbxaY8TFdGX).Remove(annotationBase);
    ((ICollection<IChartElement>) this.\u0023\u003DzeckSod0\u003D.Elements).Remove((IChartElement) _param1);
    this.\u0023\u003DzGuZ8w82B3fMJ()?.\u0023\u003DzXartur54T48t(_param1);
  }

  public void \u0023\u003DzjgUUUJE\u003D(
    ChartAnnotation _param1,
    ChartDrawData.AnnotationData _param2)
  {
    dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd.\u0023\u003DzIcUXbzVqd1QHu2nAW1nzwT0\u003D vqd1Qhu2nAw1nzwT0;
    bool? nullable;
    if (!this.\u0023\u003DzCqbxaY8TFdGX.TryGetKey(_param1, ref vqd1Qhu2nAw1nzwT0.\u0023\u003Dz2vouRgM\u003D))
    {
      Type type = _param1.Type.\u0023\u003Dz6wKFLhaRAAzr();
      vqd1Qhu2nAw1nzwT0.\u0023\u003Dz2vouRgM\u003D = (AnnotationBase) Activator.CreateInstance(type);
      vqd1Qhu2nAw1nzwT0.\u0023\u003Dz2vouRgM\u003D.XAxisId = _param1.XAxisId;
      vqd1Qhu2nAw1nzwT0.\u0023\u003Dz2vouRgM\u003D.YAxisId = _param1.YAxisId;
      vqd1Qhu2nAw1nzwT0.\u0023\u003Dz2vouRgM\u003D.IsHidden = false;
      AnnotationBase z2vouRgM = vqd1Qhu2nAw1nzwT0.\u0023\u003Dz2vouRgM\u003D;
      nullable = _param2.IsEditable;
      int num = !(!nullable.GetValueOrDefault() & nullable.HasValue) ? 1 : 0;
      this.\u0023\u003DzbSFwQBfxHeQf(z2vouRgM, num != 0);
      this.\u0023\u003DzIehZCHK_gy_6(vqd1Qhu2nAw1nzwT0.\u0023\u003Dz2vouRgM\u003D);
      ((KeyedCollection<AnnotationBase, ChartAnnotation>) this.\u0023\u003DzCqbxaY8TFdGX)[vqd1Qhu2nAw1nzwT0.\u0023\u003Dz2vouRgM\u003D] = _param1;
      this.\u0023\u003DzfS3q6Qc\u003D.Add((\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D) vqd1Qhu2nAw1nzwT0.\u0023\u003Dz2vouRgM\u003D);
      this.\u0023\u003DzGuZ8w82B3fMJ()?.\u0023\u003Dz49m\u0024QLWwKQs9(_param1);
    }
    vqd1Qhu2nAw1nzwT0.\u0023\u003DzFlkZpfJp6G9R = vqd1Qhu2nAw1nzwT0.\u0023\u003Dz2vouRgM\u003D.XAxis?.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D();
    try
    {
      this.\u0023\u003DzmHijKtV8bKDY = true;
      nullable = _param2.IsVisible;
      if (nullable.HasValue)
      {
        AnnotationBase z2vouRgM = vqd1Qhu2nAw1nzwT0.\u0023\u003Dz2vouRgM\u003D;
        nullable = _param2.IsVisible;
        int num = !nullable.Value ? 1 : 0;
        z2vouRgM.IsHidden = num != 0;
      }
      nullable = _param2.IsEditable;
      if (nullable.HasValue)
      {
        AnnotationBase z2vouRgM = vqd1Qhu2nAw1nzwT0.\u0023\u003Dz2vouRgM\u003D;
        nullable = _param2.IsEditable;
        int num = nullable.Value ? 1 : 0;
        this.\u0023\u003DzbSFwQBfxHeQf(z2vouRgM, num != 0);
      }
      if (_param2.CoordinateMode.HasValue)
        vqd1Qhu2nAw1nzwT0.\u0023\u003Dz2vouRgM\u003D.CoordinateMode = _param2.CoordinateMode.Value;
      IComparable comparable1 = dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd.\u0023\u003DqB_tciyqtNGJB3PpHZflMM\u0024N9EO6XlBaG688iDCFvTDc\u003D(_param2.X1, ref vqd1Qhu2nAw1nzwT0);
      IComparable comparable2 = dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd.\u0023\u003DqB_tciyqtNGJB3PpHZflMM\u0024N9EO6XlBaG688iDCFvTDc\u003D(_param2.X2, ref vqd1Qhu2nAw1nzwT0);
      IComparable comparable3 = dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd.\u0023\u003Dq8f\u0024Kf3mr1qpotJDNtCA37\u0024_mt5h9RLbGp_SzHkzBWCc\u003D(_param2.Y1);
      IComparable comparable4 = dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd.\u0023\u003Dq8f\u0024Kf3mr1qpotJDNtCA37\u0024_mt5h9RLbGp_SzHkzBWCc\u003D(_param2.Y2);
      if (comparable1 != null)
        vqd1Qhu2nAw1nzwT0.\u0023\u003Dz2vouRgM\u003D.X1 = comparable1;
      if (comparable2 != null)
        vqd1Qhu2nAw1nzwT0.\u0023\u003Dz2vouRgM\u003D.X2 = comparable2;
      if (comparable3 != null)
        vqd1Qhu2nAw1nzwT0.\u0023\u003Dz2vouRgM\u003D.Y1 = comparable3;
      if (comparable4 != null)
        vqd1Qhu2nAw1nzwT0.\u0023\u003Dz2vouRgM\u003D.Y2 = comparable4;
      if (vqd1Qhu2nAw1nzwT0.\u0023\u003Dz2vouRgM\u003D is LineAnnotationBase z2vouRgM1)
      {
        if (_param2.Stroke != null)
          z2vouRgM1.Stroke = _param2.Stroke;
        if (_param2.Thickness.HasValue)
          z2vouRgM1.StrokeThickness = _param2.Thickness.Value.Left;
      }
      if (!(vqd1Qhu2nAw1nzwT0.\u0023\u003Dz2vouRgM\u003D is HorizontalLineAnnotation z2vouRgM6))
      {
        if (!(vqd1Qhu2nAw1nzwT0.\u0023\u003Dz2vouRgM\u003D is VerticalLineAnnotation z2vouRgM5))
        {
          if (!(vqd1Qhu2nAw1nzwT0.\u0023\u003Dz2vouRgM\u003D is BoxAnnotation z2vouRgM4))
          {
            if (!(vqd1Qhu2nAw1nzwT0.\u0023\u003Dz2vouRgM\u003D is TextAnnotation z2vouRgM3))
            {
              if (!(vqd1Qhu2nAw1nzwT0.\u0023\u003Dz2vouRgM\u003D is RulerAnnotation z2vouRgM2) || _param2.Fill == null)
                return;
              Brush brush = _param2.Fill is SolidColorBrush fill ? (Brush) new SolidColorBrush(fill.Color.ToTransparent((byte) 50)) : _param2.Fill;
              z2vouRgM2.Background = brush;
            }
            else
            {
              if (_param2.Foreground != null)
                z2vouRgM3.Foreground = _param2.Foreground;
              if (_param2.Text != null)
                z2vouRgM3.Text = _param2.Text;
              if (_param2.Fill != null)
                z2vouRgM3.Background = _param2.Fill;
              if (_param2.Stroke != null)
                z2vouRgM3.BorderBrush = _param2.Stroke;
              if (!_param2.Thickness.HasValue)
                return;
              z2vouRgM3.BorderThickness = _param2.Thickness.Value;
            }
          }
          else
          {
            if (_param2.Fill != null)
              z2vouRgM4.Background = _param2.Fill;
            if (_param2.Stroke != null)
              z2vouRgM4.BorderBrush = _param2.Stroke;
            if (!_param2.Thickness.HasValue)
              return;
            z2vouRgM4.BorderThickness = _param2.Thickness.Value;
          }
        }
        else
        {
          if (_param2.VerticalAlignment.HasValue)
            z2vouRgM5.VerticalAlignment = _param2.VerticalAlignment.Value;
          if (_param2.LabelPlacement.HasValue)
            z2vouRgM5.LabelPlacement = _param2.LabelPlacement.Value;
          nullable = _param2.ShowLabel;
          if (!nullable.HasValue)
            return;
          VerticalLineAnnotation verticalLineAnnotation = z2vouRgM5;
          nullable = _param2.ShowLabel;
          int num = nullable.Value ? 1 : 0;
          verticalLineAnnotation.ShowLabel = num != 0;
        }
      }
      else
      {
        if (_param2.HorizontalAlignment.HasValue)
          z2vouRgM6.HorizontalAlignment = _param2.HorizontalAlignment.Value;
        if (_param2.LabelPlacement.HasValue)
          z2vouRgM6.LabelPlacement = _param2.LabelPlacement.Value;
        nullable = _param2.ShowLabel;
        if (!nullable.HasValue)
          return;
        HorizontalLineAnnotation horizontalLineAnnotation = z2vouRgM6;
        nullable = _param2.ShowLabel;
        int num = nullable.Value ? 1 : 0;
        horizontalLineAnnotation.ShowLabel = num != 0;
      }
    }
    finally
    {
      this.\u0023\u003DzmHijKtV8bKDY = false;
      this.\u0023\u003DzGuZ8w82B3fMJ()?.\u0023\u003Dz5mEkRaZSEt9m(_param1, this.\u0023\u003DzCVYc\u0024mw923hM(vqd1Qhu2nAw1nzwT0.\u0023\u003Dz2vouRgM\u003D));
    }
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  public void InitializeComponent()
  {
    if (this.\u0023\u003DzQGCmQMjHdLKS)
      return;
    this.\u0023\u003DzQGCmQMjHdLKS = true;
    Application.LoadComponent((object) this, new Uri(XXX.SSS(-539330471), UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  internal Delegate \u0023\u003DzciIj4U627yBM(Type _param1, string _param2)
  {
    return Delegate.CreateDelegate(_param1, (object) this, _param2);
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.\u0023\u003DzuNHLeGEnMjz9FDFZ6wymuXfyw_Iz(int _param1, object _param2)
  {
    this.\u0023\u003DzQGCmQMjHdLKS = true;
  }

  internal static IComparable \u0023\u003DqilGCdp9aFjNd5SLe7I3UCUsoNPZA2A4LqGx2E87OFI3Oof40izFV2yt8T4S4jCDk(
    IComparable _param0,
    ref dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd.\u0023\u003DzN3EMs6Vm6DExIKYZZOKCa\u0024w\u003D _param1)
  {
    switch (_param0)
    {
      case null:
        return (IComparable) null;
      case int num1:
        if (!(_param1.\u0023\u003DzFlkZpfJp6G9R is \u0023\u003Dz5hVyTN88kBn45NAfOxK7MCQZNrLpjKlS2Qc8bb5_oiHXVWVmbJi\u0024\u0024q9i0M\u0024xI7QB9c1V6c0\u003D zFlkZpfJp6G9R))
          throw new InvalidOperationException(StringHelper.Put(LocalizedStrings.UnexpectedCoordTypeParams, new object[1]
          {
            (object) XXX.SSS(-539330305)
          }));
        return (IComparable) new DateTimeOffset(zFlkZpfJp6G9R.\u0023\u003DzWZQlXHuDrnKc(num1), TimeSpan.Zero);
      case DateTime dateTime:
        return (IComparable) new DateTimeOffset(dateTime, TimeSpan.Zero);
      case double num2:
        return (IComparable) num2;
      default:
        throw new InvalidOperationException(StringHelper.Put(LocalizedStrings.UnexpectedCoordTypeParams, new object[1]
        {
          (object) _param0.GetType().Name
        }));
    }
  }

  internal static IComparable \u0023\u003DqTAamYC40X7Nd2JjKg0gRJnEFqikmgfX0UtSubtvET0wvj813KCUIgmYrc9ybxFYK(
    IComparable _param0,
    ref dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd.\u0023\u003DzN3EMs6Vm6DExIKYZZOKCa\u0024w\u003D _param1)
  {
    if (_param0 == null)
      return (IComparable) null;
    if (_param0 is double num)
      return _param1.\u0023\u003Dz2vouRgM\u003D.CoordinateMode == AnnotationCoordinateMode.Relative || _param1.\u0023\u003Dz2vouRgM\u003D.CoordinateMode == AnnotationCoordinateMode.RelativeY ? (IComparable) num : (IComparable) Converter.To<Decimal>((object) num);
    throw new InvalidOperationException(StringHelper.Put(LocalizedStrings.UnexpectedCoordTypeParams, new object[1]
    {
      (object) _param0.GetType().Name
    }));
  }

  internal static IComparable \u0023\u003DqB_tciyqtNGJB3PpHZflMM\u0024N9EO6XlBaG688iDCFvTDc\u003D(
    IComparable _param0,
    ref dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd.\u0023\u003DzIcUXbzVqd1QHu2nAW1nzwT0\u003D _param1)
  {
    if (_param0 == null)
      return (IComparable) null;
    if (_param0 is DateTimeOffset dateTimeOffset)
      return (IComparable) dateTimeOffset.UtcDateTime;
    if (_param0 is DateTime dateTime)
      return (IComparable) dateTime;
    if (_param1.\u0023\u003DzFlkZpfJp6G9R is \u0023\u003Dz5hVyTN88kBn45NAfOxK7MCQZNrLpjKlS2Qc8bb5_oiHXVWVmbJi\u0024\u0024q9i0M\u0024xI7QB9c1V6c0\u003D && (_param1.\u0023\u003Dz2vouRgM\u003D.CoordinateMode == AnnotationCoordinateMode.Absolute || _param1.\u0023\u003Dz2vouRgM\u003D.CoordinateMode == AnnotationCoordinateMode.RelativeY))
      throw new InvalidOperationException(StringHelper.Put(LocalizedStrings.UnexpectedCoordTypeParams, new object[1]
      {
        (object) _param0.GetType().Name
      }));
    switch (_param0)
    {
      case Decimal num1:
        return (IComparable) (double) num1;
      case double num2:
        return (IComparable) num2;
      default:
        throw new InvalidOperationException(StringHelper.Put(LocalizedStrings.UnexpectedCoordTypeParams, new object[1]
        {
          (object) _param0.GetType().Name
        }));
    }
  }

  internal static IComparable \u0023\u003Dq8f\u0024Kf3mr1qpotJDNtCA37\u0024_mt5h9RLbGp_SzHkzBWCc\u003D(
    IComparable _param0)
  {
    switch (_param0)
    {
      case null:
        return (IComparable) null;
      case Decimal num1:
        return (IComparable) (double) num1;
      case double num2:
        return (IComparable) num2;
      default:
        throw new InvalidOperationException(StringHelper.Put(LocalizedStrings.UnexpectedCoordTypeParams, new object[1]
        {
          (object) _param0.GetType().Name
        }));
    }
  }

  [Serializable]
  private new sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd.\u0023\u003Dz7qOdpi4\u003D();
    public static Action<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D> \u0023\u003Dzm5iuiBtfSUf6PnApUQ\u003D\u003D;
    public static Action<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D> \u0023\u003Dz\u0024KPevpYcSl7cnlctrA\u003D\u003D;

    internal void \u0023\u003Dzf_OX3zxNZIoqduZEY_a_\u0024gQ\u003D(
      \u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D _param1)
    {
      _param1.set_IsEditable(true);
    }

    internal void \u0023\u003DzvGJdbcfbzF5NLww3X__o_JGpcRO1(
      \u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D _param1)
    {
      _param1.set_IsSelected(false);
    }
  }

  [StructLayout(LayoutKind.Auto)]
  private struct \u0023\u003DzIcUXbzVqd1QHu2nAW1nzwT0\u003D
  {
    
    public \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> \u0023\u003DzFlkZpfJp6G9R;
    
    public AnnotationBase \u0023\u003Dz2vouRgM\u003D;
  }

  [StructLayout(LayoutKind.Auto)]
  private struct \u0023\u003DzN3EMs6Vm6DExIKYZZOKCa\u0024w\u003D
  {
    
    public \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> \u0023\u003DzFlkZpfJp6G9R;
    
    public AnnotationBase \u0023\u003Dz2vouRgM\u003D;
  }

  private sealed class \u0023\u003DzNwXxhkJq9Llz3ah\u0024z2LRQl4\u003D
  {
    public dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd \u0023\u003DzRRvwDu67s9Rm;
    public AnnotationBase \u0023\u003DzLXiKo\u0024A\u003D;
    public DelegateCommand<AnnotationBase> \u0023\u003DztKpRxYMDxV\u00243;

    internal void \u0023\u003DqkJAvLS57TQDltohwMlvUc8ToXY9\u0024wVsEDT1RmCTmQAl1TTvH5MP818enEgRePrx8(
      AnnotationBase _param1)
    {
      this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzfS3q6Qc\u003D.Remove((\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D) _param1);
      ChartAnnotation chartAnnotation;
      if (!((KeyedCollection<AnnotationBase, ChartAnnotation>) this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzCqbxaY8TFdGX).TryGetValue(_param1, ref chartAnnotation))
        return;
      ((KeyedCollection<AnnotationBase, ChartAnnotation>) this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzCqbxaY8TFdGX).Remove(_param1);
      ((ICollection<IChartElement>) this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzeckSod0\u003D.Elements).Remove((IChartElement) chartAnnotation);
      this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzGuZ8w82B3fMJ()?.\u0023\u003DzXartur54T48t(chartAnnotation);
    }

    internal void \u0023\u003Dzn4dNgi0mxbbCP\u0024DWxprQswg\u003D(AnnotationBase _param1)
    {
      dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd.\u0023\u003DzckhZYWd3zPHu0dS2ZQhuzuE\u003D wd3zPhu0dS2ZqhuzuE = new dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd.\u0023\u003DzckhZYWd3zPHu0dS2ZQhuzuE\u003D();
      wd3zPhu0dS2ZqhuzuE.\u0023\u003Dz2vouRgM\u003D = _param1;
      dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCBCF2R6BFXD35AJMNZAG8VPWQ3UE6ZFKM53T5BWGTPBKV9Z_ejd m53T5BwgtpbkV9ZEjd = this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzqRVOnAc\u003D();
      m53T5BwgtpbkV9ZEjd.IsOpen = false;
      CollectionHelper.ForEach<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>(this.\u0023\u003DzRRvwDu67s9Rm.ParentSurface.get_Annotations().Where<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>(new Func<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D, bool>(wd3zPhu0dS2ZqhuzuE.\u0023\u003DzJdTk1tU_hZdMZEOQ7Sdo5JQ\u003D)), dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz\u0024KPevpYcSl7cnlctrA\u003D\u003D ?? (dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz\u0024KPevpYcSl7cnlctrA\u003D\u003D = new Action<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>(dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzvGJdbcfbzF5NLww3X__o_JGpcRO1)));
      wd3zPhu0dS2ZqhuzuE.\u0023\u003Dz2vouRgM\u003D.IsSelected = true;
      m53T5BwgtpbkV9ZEjd.PlacementTarget = (UIElement) wd3zPhu0dS2ZqhuzuE.\u0023\u003Dz2vouRgM\u003D;
      m53T5BwgtpbkV9ZEjd.IsOpen = true;
    }

    internal void \u0023\u003Dq7mcVfPvAB3Y4duXTm_bwIHboyLUu0\u0024G63ba4grB5MsPr1uOUJsYns2NOLvS5EdHu_qwqq\u0024dzh7uvWZ_cRJz2Qw\u003D\u003D(
      object _param1,
      KeyEventArgs _param2)
    {
      if (!this.\u0023\u003DzLXiKo\u0024A\u003D.IsSelected || Keyboard.Modifiers != null)
        return;
      if (_param2.Key == 32 /*0x20*/)
      {
        this.\u0023\u003DztKpRxYMDxV\u00243.TryExecute((object) this.\u0023\u003DzLXiKo\u0024A\u003D);
      }
      else
      {
        if (_param2.Key != 13 || !(this.\u0023\u003DzLXiKo\u0024A\u003D is TextAnnotation zLxiKoA))
          return;
        zLxiKoA.RemoveFocusFromInputTextArea();
      }
    }

    internal void \u0023\u003DqsO0aV2_u3gwC4_hwIR4BeABhYV_7m2A\u0024ZOJFQhD2VppSu5lNDzXhb76mKBs5qy0DljWs2YahWqjBYAlTloZS5Q\u003D\u003D(
      object _param1,
      EventArgs _param2)
    {
      Keyboard.Focus((IInputElement) this.\u0023\u003DzLXiKo\u0024A\u003D);
    }

    internal void \u0023\u003DqdGbJy3AROhfZcOB8qLyu\u0024jJUN5YXrNmxgUQMxhoWOUyOvmpRAlpHEMxwLY7VT_bNf7h7A1Gpy0sisqLkbC4SE1lJB8L2p27M0jUbIyr1qsE\u003D(
      object _param1,
      MouseButtonEventArgs _param2)
    {
      Keyboard.Focus((IInputElement) this.\u0023\u003DzLXiKo\u0024A\u003D);
    }

    internal void \u0023\u003DqwGFSEwECiuHXXjEFGHe8QY\u00247phLKFc6Uur2mC93l2Vt3u_d_e1\u00246awwFvq85QGOeUttMmP5CScJ0f9g25CtXrQ\u003D\u003D(
      object _param1,
      EventArgs _param2)
    {
      this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzqRVOnAc\u003D().IsOpen = false;
    }

    internal void \u0023\u003DqyB2RaLDacQfKp3hzEOo9jtCYmWI0NuQIdJ1OucO7P2ZS1UNCupBPv2q788ONhBifj7\u00242eFsTAo3xcpoGsx2qiA\u003D\u003D(
      object _param1,
      \u0023\u003DzYH1zUE63H2wnu5PkgVdj8EceYoYimX3HGDNfbrY\u003D _param2)
    {
      this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzmHijKtV8bKDY = true;
    }

    internal void \u0023\u003DqxZFddEyfLMrUR0cfuiIOvt2\u0024\u0024WepGdv1wxLS58FfD260\u0024X298XW8T6e3WebsLZDqwhKx85FubKoFI64m1eByJA\u003D\u003D(
      object _param1,
      EventArgs _param2)
    {
      AnnotationBase annotationBase = (AnnotationBase) _param1;
      try
      {
        dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCBCF2R6BFXD35AJMNZAG8VPWQ3UE6ZFKM53T5BWGTPBKV9Z_ejd m53T5BwgtpbkV9ZEjd = this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzqRVOnAc\u003D();
        if (!m53T5BwgtpbkV9ZEjd.IsOpen)
          return;
        m53T5BwgtpbkV9ZEjd.IsOpen = false;
        m53T5BwgtpbkV9ZEjd.IsOpen = true;
      }
      finally
      {
        this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzmHijKtV8bKDY = false;
        ChartAnnotation chartAnnotation;
        if (((KeyedCollection<AnnotationBase, ChartAnnotation>) this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzCqbxaY8TFdGX).TryGetValue(annotationBase, ref chartAnnotation))
          this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzGuZ8w82B3fMJ().\u0023\u003Dz5mEkRaZSEt9m(chartAnnotation, this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzCVYc\u0024mw923hM(annotationBase));
      }
    }
  }

  private sealed class \u0023\u003DzckhZYWd3zPHu0dS2ZQhuzuE\u003D
  {
    public AnnotationBase \u0023\u003Dz2vouRgM\u003D;

    internal bool \u0023\u003DzJdTk1tU_hZdMZEOQ7Sdo5JQ\u003D(
      \u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D _param1)
    {
      return _param1 != this.\u0023\u003Dz2vouRgM\u003D;
    }
  }

  private sealed class \u0023\u003DzpcA\u0024d1dDoDCwiev7trI4NY0\u003D
  {
    public dje_zADFUPRN62S8ZNNEUQ79EF8DB58BRGPXL7GY5HE4PPWFW6CDHKBAB6HP6FKG7AAUPM52GHWGQ_ejd \u0023\u003DzRRvwDu67s9Rm;
    public AnnotationBase \u0023\u003Dz2vouRgM\u003D;
    public Action<DependencyPropertyChangedEventArgs> \u0023\u003DzDg_APFfs\u0024qGS;

    internal void \u0023\u003DzO2Calw5PtJlYUGt7JwufXn8\u003D(
    #nullable enable
    object? _param1, EventArgs _param2)
    {
      AnnotationBase annotationBase = (AnnotationBase) _param1;
      ChartAnnotation chartAnnotation = CollectionHelper.TryGetValue<AnnotationBase, ChartAnnotation>((IDictionary<AnnotationBase, ChartAnnotation>) this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzCqbxaY8TFdGX, annotationBase);
      this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzGuZ8w82B3fMJ()?.\u0023\u003DzSZqzgFQySfHr(chartAnnotation, Equatable<ChartAnnotation>.op_Equality((Equatable<ChartAnnotation>) chartAnnotation, (ChartAnnotation) null) ? (ChartDrawData.AnnotationData) null : this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzCVYc\u0024mw923hM(annotationBase));
    }

    internal void \u0023\u003DzMAzDDfbdHsJSDAK_mit5cEE\u003D(object? _param1, EventArgs _param2)
    {
      this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzGuZ8w82B3fMJ()?.\u0023\u003DzSZqzgFQySfHr((ChartAnnotation) null, (ChartDrawData.AnnotationData) null);
    }

    internal void \u0023\u003Dz2YagnOQq\u0024fEh5b7gci8gwU8\u003D(
    #nullable disable
    DependencyProperty _param1)
    {
      this.\u0023\u003Dz2vouRgM\u003D.\u0023\u003DzCZHTVdsqw3mR(_param1, this.\u0023\u003DzDg_APFfs\u0024qGS ?? (this.\u0023\u003DzDg_APFfs\u0024qGS = new Action<DependencyPropertyChangedEventArgs>(this.\u0023\u003DzBSmoA83lp78GLXOjbXNge4A\u003D)));
    }

    internal void \u0023\u003DzBSmoA83lp78GLXOjbXNge4A\u003D(
      DependencyPropertyChangedEventArgs _param1)
    {
      ChartAnnotation chartAnnotation;
      if (!((KeyedCollection<AnnotationBase, ChartAnnotation>) this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzCqbxaY8TFdGX).TryGetValue(this.\u0023\u003Dz2vouRgM\u003D, ref chartAnnotation) || this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzmHijKtV8bKDY)
        return;
      this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzGuZ8w82B3fMJ().\u0023\u003Dz5mEkRaZSEt9m(chartAnnotation, this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzCVYc\u0024mw923hM(this.\u0023\u003Dz2vouRgM\u003D));
    }
  }
}
