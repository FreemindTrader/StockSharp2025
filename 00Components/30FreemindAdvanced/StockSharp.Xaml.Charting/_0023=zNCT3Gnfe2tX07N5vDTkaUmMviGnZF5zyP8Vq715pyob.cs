// Decompiled with JetBrains decompiler
// Type: #=zNCT3Gnfe2tX07N5vDTkaUmMviGnZF5zyP8Vq715pyobvSG_F30ddnEdMvAIP_dliVQ==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using \u002D;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Core;
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Xaml.Converters;
using StockSharp.Charting;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Xaml.Charting;
using StockSharp.Xaml.Charting.Model.DataSeries.SegmentDataSeries;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

#nullable disable
internal sealed class \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUmMviGnZF5zyP8Vq715pyobvSG_F30ddnEdMvAIP_dliVQ\u003D\u003D(
  ChartCandleElement _param1) : 
  \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xAm_Ca5jQWV7qTLlcbbCsg9V98qcX3BV8d1LwxB\u0024<ChartCandleElement>(_param1),
  \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh1gj3_fBHIvbLIG5Htg5ScQRmCkwmAANyPA\u003D
{
  
  private readonly \u0023\u003DzUib3SzczDtLU7txM4YiSeKmXoPo_JGajS5kum66ISFwjrEPTng\u003D\u003D<DateTime, double> \u0023\u003DzbqAj1u4yWHJPaYS4vgV_85Q\u003D;
  
  private readonly \u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IV6_IiM70jMp5uwjMXR4ajr_<DateTime, double> \u0023\u003DzVtuwsFJcSHit;
  
  private readonly SynchronizedList<\u0023\u003DzGf68ilGq59TJ0aVKr0K_9Ur9WO7TFzBBD24ufNSokcCRpZmI_iRRi1f09FSCgNU6tg\u003D\u003D> \u0023\u003DzSi1nzfdLoJWYKewDlw\u003D\u003D;
  
  private \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEf8BMO_ZOBlhDA\u003D\u003D \u0023\u003DzKj7nvWQ\u003D;
  
  private TimeframeSegmentDataSeries \u0023\u003DzQ73Ei9NuGdXX;
  
  private DateTime \u0023\u003DzTqpoRUfBxm2O;
  
  private bool? \u0023\u003DzuuCm_xeHWuMy;
  
  private \u0023\u003DzR2x48Sho4AxfV9DSAxG8OQ2rGx6KyfAREQ\u003D\u003D \u0023\u003DzUuMx9G25\u0024d0f;
  
  private \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy \u0023\u003DzCKkTLgMyXhNP;
  
  private \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy \u0023\u003DzfR8j2PM1RBui;
  
  private \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy \u0023\u003DzjAnzC1Gk9\u0024VP;
  
  private \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy \u0023\u003DzKCOaMdzTEc8\u0024;
  
  private \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy \u0023\u003DzdaM3_c5kMuSK;
  
  private \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy \u0023\u003DzWbY5DVo2YLuB;
  
  private Decimal? \u0023\u003DzfILpmP5JFeem;
  
  private Func<DateTimeOffset, bool, bool, Color?> \u0023\u003DzVnKR\u0024HeQJSKTkReJlfF5mAk\u003D;
  
  private readonly SynchronizedDictionary<DateTime, Color> \u0023\u003DzK1tfXeY7PPNb;
  
  private readonly \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUmMviGnZF5zyP8Vq715pyobvSG_F30ddnEdMvAIP_dliVQ\u003D\u003D.\u0023\u003Dzqyx1Pxv3orWX \u0023\u003Dznrzap3Ru8O5U = new \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUmMviGnZF5zyP8Vq715pyobvSG_F30ddnEdMvAIP_dliVQ\u003D\u003D.\u0023\u003Dzqyx1Pxv3orWX(_param1);
  
  private bool \u0023\u003DzZ8C5xEdop1TJ;

  public \u0023\u003DzUib3SzczDtLU7txM4YiSeKmXoPo_JGajS5kum66ISFwjrEPTng\u003D\u003D<DateTime, double> OhlcSeries
  {
    get => this.\u0023\u003DzbqAj1u4yWHJPaYS4vgV_85Q\u003D;
  }

  public TimeSpan? CandlesTimeframe => this.OhlcSeries.Timeframe;

  private \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D \u0023\u003Dz8bZd_lILC\u0024Y\u0024()
  {
    if (this.\u0023\u003DzeaszzAAoBOY9().DrawStyle.IsVolumeProfileChart())
      return (\u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D) this.\u0023\u003DzQ73Ei9NuGdXX;
    return this.\u0023\u003DzeaszzAAoBOY9().DrawStyle != ChartCandleDrawStyles.Area ? (\u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D) this.OhlcSeries : (\u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D) this.\u0023\u003DzVtuwsFJcSHit;
  }

  public void \u0023\u003DznX2LIYg\u003D(
    \u0023\u003DzGf68ilGq59TJ0aVKr0K_9Ur9WO7TFzBBD24ufNSokcCRpZmI_iRRi1f09FSCgNU6tg\u003D\u003D _param1)
  {
    if (((BaseCollection<\u0023\u003DzGf68ilGq59TJ0aVKr0K_9Ur9WO7TFzBBD24ufNSokcCRpZmI_iRRi1f09FSCgNU6tg\u003D\u003D, List<\u0023\u003DzGf68ilGq59TJ0aVKr0K_9Ur9WO7TFzBBD24ufNSokcCRpZmI_iRRi1f09FSCgNU6tg\u003D\u003D>>) this.\u0023\u003DzSi1nzfdLoJWYKewDlw\u003D\u003D).Contains(_param1))
      return;
    ((BaseCollection<\u0023\u003DzGf68ilGq59TJ0aVKr0K_9Ur9WO7TFzBBD24ufNSokcCRpZmI_iRRi1f09FSCgNU6tg\u003D\u003D, List<\u0023\u003DzGf68ilGq59TJ0aVKr0K_9Ur9WO7TFzBBD24ufNSokcCRpZmI_iRRi1f09FSCgNU6tg\u003D\u003D>>) this.\u0023\u003DzSi1nzfdLoJWYKewDlw\u003D\u003D).Add(_param1);
  }

  public void \u0023\u003DziWBXfUI\u003D(
    \u0023\u003DzGf68ilGq59TJ0aVKr0K_9Ur9WO7TFzBBD24ufNSokcCRpZmI_iRRi1f09FSCgNU6tg\u003D\u003D _param1)
  {
    ((BaseCollection<\u0023\u003DzGf68ilGq59TJ0aVKr0K_9Ur9WO7TFzBBD24ufNSokcCRpZmI_iRRi1f09FSCgNU6tg\u003D\u003D, List<\u0023\u003DzGf68ilGq59TJ0aVKr0K_9Ur9WO7TFzBBD24ufNSokcCRpZmI_iRRi1f09FSCgNU6tg\u003D\u003D>>) this.\u0023\u003DzSi1nzfdLoJWYKewDlw\u003D\u003D).Remove(_param1);
  }

  protected override void \u0023\u003DzY0x9JtY\u003D()
  {
    base.\u0023\u003DzY0x9JtY\u003D();
    string[] strArray = new string[2]
    {
      XXX.SSS(-539433258),
      XXX.SSS(-539433270)
    };
    this.GetParentVM().\u0023\u003Dzfc4TzKM\u003D(this.\u0023\u003DzCKkTLgMyXhNP = new \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy(XXX.SSS(-539329735), (INotifyPropertyChanged) this.\u0023\u003DzeaszzAAoBOY9(), new Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, Color>(this.\u0023\u003Dqo12wao\u0024ehitMzQgoNEdrsjKxeleFvrK0RDpR\u0024y51Ghs\u003D), \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUmMviGnZF5zyP8Vq715pyobvSG_F30ddnEdMvAIP_dliVQ\u003D\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzU9srAoETJDIIA3EbGw\u003D\u003D ?? (\u0023\u003DzNCT3Gnfe2tX07N5vDTkaUmMviGnZF5zyP8Vq715pyobvSG_F30ddnEdMvAIP_dliVQ\u003D\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzU9srAoETJDIIA3EbGw\u003D\u003D = new Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, string>(\u0023\u003DzNCT3Gnfe2tX07N5vDTkaUmMviGnZF5zyP8Vq715pyobvSG_F30ddnEdMvAIP_dliVQ\u003D\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DziVMtGB3eskawtnyDEBGppGQ\u003D)), strArray));
    this.GetParentVM().\u0023\u003Dzfc4TzKM\u003D(this.\u0023\u003DzfR8j2PM1RBui = new \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy(XXX.SSS(-539329743), (INotifyPropertyChanged) this.\u0023\u003DzeaszzAAoBOY9(), new Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, Color>(this.\u0023\u003Dqo12wao\u0024ehitMzQgoNEdrsjKxeleFvrK0RDpR\u0024y51Ghs\u003D), \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUmMviGnZF5zyP8Vq715pyobvSG_F30ddnEdMvAIP_dliVQ\u003D\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dzv7x3aq5xjFNkgWHCPg\u003D\u003D ?? (\u0023\u003DzNCT3Gnfe2tX07N5vDTkaUmMviGnZF5zyP8Vq715pyobvSG_F30ddnEdMvAIP_dliVQ\u003D\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dzv7x3aq5xjFNkgWHCPg\u003D\u003D = new Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, string>(\u0023\u003DzNCT3Gnfe2tX07N5vDTkaUmMviGnZF5zyP8Vq715pyobvSG_F30ddnEdMvAIP_dliVQ\u003D\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzyboOJPrkbYbHrtK_jYxCKz0\u003D)), strArray));
    this.GetParentVM().\u0023\u003Dzfc4TzKM\u003D(this.\u0023\u003DzjAnzC1Gk9\u0024VP = new \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy(XXX.SSS(-539329783), (INotifyPropertyChanged) this.\u0023\u003DzeaszzAAoBOY9(), new Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, Color>(this.\u0023\u003Dqo12wao\u0024ehitMzQgoNEdrsjKxeleFvrK0RDpR\u0024y51Ghs\u003D), \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUmMviGnZF5zyP8Vq715pyobvSG_F30ddnEdMvAIP_dliVQ\u003D\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz3QgfRT\u0024GTbbdeFCZLg\u003D\u003D ?? (\u0023\u003DzNCT3Gnfe2tX07N5vDTkaUmMviGnZF5zyP8Vq715pyobvSG_F30ddnEdMvAIP_dliVQ\u003D\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz3QgfRT\u0024GTbbdeFCZLg\u003D\u003D = new Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, string>(\u0023\u003DzNCT3Gnfe2tX07N5vDTkaUmMviGnZF5zyP8Vq715pyobvSG_F30ddnEdMvAIP_dliVQ\u003D\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dz_F9udS\u0024bvXmu7p7kNM2l1tM\u003D)), strArray));
    this.GetParentVM().\u0023\u003Dzfc4TzKM\u003D(this.\u0023\u003DzKCOaMdzTEc8\u0024 = new \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy(XXX.SSS(-539329791), (INotifyPropertyChanged) this.\u0023\u003DzeaszzAAoBOY9(), new Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, Color>(this.\u0023\u003Dqo12wao\u0024ehitMzQgoNEdrsjKxeleFvrK0RDpR\u0024y51Ghs\u003D), \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUmMviGnZF5zyP8Vq715pyobvSG_F30ddnEdMvAIP_dliVQ\u003D\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz3XWQ58Tacl3uxVgGiw\u003D\u003D ?? (\u0023\u003DzNCT3Gnfe2tX07N5vDTkaUmMviGnZF5zyP8Vq715pyobvSG_F30ddnEdMvAIP_dliVQ\u003D\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz3XWQ58Tacl3uxVgGiw\u003D\u003D = new Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, string>(\u0023\u003DzNCT3Gnfe2tX07N5vDTkaUmMviGnZF5zyP8Vq715pyobvSG_F30ddnEdMvAIP_dliVQ\u003D\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dz_2ZdaKSEm\u0024dowilMRXDN0r4\u003D)), strArray));
    this.GetParentVM().\u0023\u003Dzfc4TzKM\u003D(this.\u0023\u003DzdaM3_c5kMuSK = new \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy(XXX.SSS(-539443181), (INotifyPropertyChanged) this.\u0023\u003DzeaszzAAoBOY9(), new Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, Color>(this.\u0023\u003Dqo12wao\u0024ehitMzQgoNEdrsjKxeleFvrK0RDpR\u0024y51Ghs\u003D), new Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, string>(this.\u0023\u003DqTIcmF95twR2vB7L3I\u0024trXELaSG5nNQSTgo5LhClfL2U\u003D), strArray));
    this.GetParentVM().\u0023\u003Dzfc4TzKM\u003D(this.\u0023\u003DzWbY5DVo2YLuB = new \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy(XXX.SSS(-539329767), (INotifyPropertyChanged) this.\u0023\u003DzeaszzAAoBOY9(), new Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, Color>(this.\u0023\u003Dqo12wao\u0024ehitMzQgoNEdrsjKxeleFvrK0RDpR\u0024y51Ghs\u003D), \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUmMviGnZF5zyP8Vq715pyobvSG_F30ddnEdMvAIP_dliVQ\u003D\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DztKkeF19DCI_S9dyd\u0024A\u003D\u003D ?? (\u0023\u003DzNCT3Gnfe2tX07N5vDTkaUmMviGnZF5zyP8Vq715pyobvSG_F30ddnEdMvAIP_dliVQ\u003D\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DztKkeF19DCI_S9dyd\u0024A\u003D\u003D = new Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, string>(\u0023\u003DzNCT3Gnfe2tX07N5vDTkaUmMviGnZF5zyP8Vq715pyobvSG_F30ddnEdMvAIP_dliVQ\u003D\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dzu0cWRPhAEXPa2F76HfbtuFw\u003D)), strArray));
    this.\u0023\u003Dzk_r\u0024wtNtUKwJ();
    \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xAm_Ca5jQWV7qTLlcbbCsg9V98qcX3BV8d1LwxB\u0024<ChartCandleElement>.\u0023\u003Dz9tL3mkpMz5PJ<ChartCandleDrawStyles>((IfxChartElement) this.\u0023\u003DzeaszzAAoBOY9(), XXX.SSS(-539433247), new ChartCandleDrawStyles[10]
    {
      ChartCandleDrawStyles.CandleStick,
      ChartCandleDrawStyles.Ohlc,
      ChartCandleDrawStyles.PnF,
      ChartCandleDrawStyles.LineOpen,
      ChartCandleDrawStyles.LineHigh,
      ChartCandleDrawStyles.LineLow,
      ChartCandleDrawStyles.LineClose,
      ChartCandleDrawStyles.BoxVolume,
      ChartCandleDrawStyles.ClusterProfile,
      ChartCandleDrawStyles.Area
    });
  }

  private void \u0023\u003Dzk_r\u0024wtNtUKwJ()
  {
    if (!UIBaseVM.\u0023\u003Dz03PnGbpCXkrj())
    {
      this.\u0023\u003Dz4EoFHUaZg4JL(new Action(this.\u0023\u003Dzk_r\u0024wtNtUKwJ), true);
    }
    else
    {
      this.\u0023\u003Dz2wIbFnXzsEoFauM8VA\u003D\u003D();
      if (this.\u0023\u003Dz8bZd_lILC\u0024Y\u0024() == null)
      {
        this.\u0023\u003Dz_JPQrgU\u003D();
      }
      else
      {
        if (this.\u0023\u003DzKj7nvWQ\u003D != null)
        {
          Type type1 = this.\u0023\u003Dzq1K_twB2qI4CAVGTXkCEBpY\u003D();
          Type type2 = this.\u0023\u003DzKj7nvWQ\u003D.RenderSeries.GetType();
          if (this.\u0023\u003DzKj7nvWQ\u003D.DataSeries == this.\u0023\u003Dz8bZd_lILC\u0024Y\u0024() && type1 == type2)
            return;
          BindingOperations.ClearAllBindings((DependencyObject) this.\u0023\u003DzKj7nvWQ\u003D.RenderSeries);
        }
        if (this.\u0023\u003DzKj7nvWQ\u003D == null || this.\u0023\u003DzKj7nvWQ\u003D.DataSeries != this.\u0023\u003Dz8bZd_lILC\u0024Y\u0024())
        {
          this.\u0023\u003Dz_JPQrgU\u003D();
          this.\u0023\u003DztzYeKaM\u003D();
        }
        else
        {
          this.\u0023\u003DzKj7nvWQ\u003D.RenderSeries = (IRenderableSeries) this.\u0023\u003Dzj4cwTqTBSZ3fAaZzTX46uig\u003D();
          this.\u0023\u003DzAgVixDQ6Vc2r();
          this.\u0023\u003Dz7GhHTEkMkDYT(this.\u0023\u003DzKj7nvWQ\u003D.RenderSeries, (IfxChartElement) this.\u0023\u003DzeaszzAAoBOY9(), XXX.SSS(-539434714), (string) null);
        }
      }
    }
  }

  private void \u0023\u003DzywI2pEkeRxof()
  {
    if (this.\u0023\u003DzQ73Ei9NuGdXX != null)
      return;
    this.\u0023\u003Dzk_r\u0024wtNtUKwJ();
  }

  private void \u0023\u003DzuqKjoe_5lTdYuAkGZw\u003D\u003D(object _param1)
  {
    if (this.\u0023\u003DzfILpmP5JFeem.HasValue || !(_param1 is PnFArg pnFarg))
      return;
    this.\u0023\u003DzfILpmP5JFeem = new Decimal?(pnFarg.BoxSize.Type == 1 ? pnFarg.BoxSize.Value : Unit.op_Explicit(pnFarg.BoxSize));
    if (!(this.\u0023\u003DzKj7nvWQ\u003D.RenderSeries is dje_z2F7KFUZRTFPWT6JP9QD62893JPNDH7HDZ2FZKZQYFCVV32D7MT596XGT8S3KNWNR57LVXU8TJABWGARB6RDEX_ejd renderSeries))
      return;
    renderSeries.XOBoxSize = (double) this.\u0023\u003DzfILpmP5JFeem.Value;
  }

  private void \u0023\u003Dz2wIbFnXzsEoFauM8VA\u003D\u003D()
  {
    TimeSpan? candlesTimeframe = this.CandlesTimeframe;
    TimeframeSegmentDataSeries zQ73Ei9NuGdXx = this.\u0023\u003DzQ73Ei9NuGdXX;
    TimeframeSegmentDataSeries segmentDataSeries;
    if (zQ73Ei9NuGdXx != null)
    {
      TimeSpan? timeframe = zQ73Ei9NuGdXx.Timeframe;
      TimeSpan? nullable = candlesTimeframe;
      if ((timeframe.HasValue == nullable.HasValue ? (timeframe.HasValue ? (timeframe.GetValueOrDefault() == nullable.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
      {
        segmentDataSeries = zQ73Ei9NuGdXx;
        goto label_4;
      }
    }
    segmentDataSeries = new TimeframeSegmentDataSeries(candlesTimeframe, this.\u0023\u003DzeaszzAAoBOY9());
label_4:
    this.\u0023\u003DzQ73Ei9NuGdXX = segmentDataSeries;
  }

  private Type \u0023\u003Dzq1K_twB2qI4CAVGTXkCEBpY\u003D()
  {
    switch (this.\u0023\u003DzeaszzAAoBOY9().DrawStyle)
    {
      case ChartCandleDrawStyles.CandleStick:
        return typeof (dje_z8KP94MAVAC8XVLJD2A4JR8Q5NFBLA2X8EB75XQ5CG2QAS7W8ZPHQMTXMAE3XE28M8857AH3KGB5ERBMZUQ2EEMGNBDTCXDA2A3_ejd);
      case ChartCandleDrawStyles.Ohlc:
        return typeof (dje_zXLPLNSHM7QU5GA2T5S4C4SHVF6L8PVAPMBCTY5WFUGN8USCF94BEQYVU9VJCT4CGFWU4FYRZXRPAXM3VUD4LQ_ejd);
      case ChartCandleDrawStyles.LineOpen:
      case ChartCandleDrawStyles.LineHigh:
      case ChartCandleDrawStyles.LineLow:
      case ChartCandleDrawStyles.LineClose:
        return typeof (dje_zN8RD3UL4Q5RJJYR8DXFMVFNTLQD93DU4393K4ENMLPSZDUWE6QXAWS9WCUYMARFXHNJL76MQ47Z7V297V2U22_ejd);
      case ChartCandleDrawStyles.BoxVolume:
        return typeof (dje_zPAYV36PKF8VT83W7TYA6GZS9WQ7A9GC3RX935K47MM57JXW4GNVYASU299URDR9BJTW9S4P5GLA76LMRLQ4YZ_ejd);
      case ChartCandleDrawStyles.ClusterProfile:
        return typeof (dje_zMFW7VEH9YQSML9Y7R42FYSK685GBSP6YDHCWWGNXQE6V7BBN8HWC7XLY67P47NZHA3X42ZPH5UUQPRZRVLJWD_ejd);
      case ChartCandleDrawStyles.Area:
        return typeof (dje_zKEMJWHZWSE279KQB6EDEEPQJWFZTHVK3RNB3SECQBVFS2MZJVYVEKR2UZXXBEHEVGBXXDD3GV8492CURMVVPXNR5WLZA_ejd);
      case ChartCandleDrawStyles.PnF:
        return typeof (dje_z2F7KFUZRTFPWT6JP9QD62893JPNDH7HDZ2FZKZQYFCVV32D7MT596XGT8S3KNWNR57LVXU8TJABWGARB6RDEX_ejd);
      default:
        throw new ArgumentOutOfRangeException();
    }
  }

  protected override void \u0023\u003Dz3u1qwgvgJlZC(
    IfxChartElement _param1,
    string _param2)
  {
    base.\u0023\u003Dz3u1qwgvgJlZC(_param1, _param2);
    if (!(_param2 == XXX.SSS(-539433247)))
      return;
    this.\u0023\u003Dzk_r\u0024wtNtUKwJ();
  }

  private dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd \u0023\u003Dzj4cwTqTBSZ3fAaZzTX46uig\u003D()
  {
    dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd target;
    switch (this.\u0023\u003DzeaszzAAoBOY9().DrawStyle)
    {
      case ChartCandleDrawStyles.CandleStick:
        target = (dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd) this.\u0023\u003Dzj4cwTqTBSZ3fAaZzTX46uig\u003D<dje_z8KP94MAVAC8XVLJD2A4JR8Q5NFBLA2X8EB75XQ5CG2QAS7W8ZPHQMTXMAE3XE28M8857AH3KGB5ERBMZUQ2EEMGNBDTCXDA2A3_ejd>(new \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy[4]
        {
          this.\u0023\u003DzCKkTLgMyXhNP,
          this.\u0023\u003DzfR8j2PM1RBui,
          this.\u0023\u003DzjAnzC1Gk9\u0024VP,
          this.\u0023\u003DzKCOaMdzTEc8\u0024
        });
        target.SetBindings(dje_z8KP94MAVAC8XVLJD2A4JR8Q5NFBLA2X8EB75XQ5CG2QAS7W8ZPHQMTXMAE3XE28M8857AH3KGB5ERBMZUQ2EEMGNBDTCXDA2A3_ejd.\u0023\u003DzQE5RIB4g32gf, (object) this.\u0023\u003DzeaszzAAoBOY9(), XXX.SSS(-539433258), converter: (IValueConverter) new Ecng.Xaml.Converters.ColorToBrushConverter());
        target.SetBindings(dje_z8KP94MAVAC8XVLJD2A4JR8Q5NFBLA2X8EB75XQ5CG2QAS7W8ZPHQMTXMAE3XE28M8857AH3KGB5ERBMZUQ2EEMGNBDTCXDA2A3_ejd.\u0023\u003DzzR4yyf\u0024wfFYI, (object) this.\u0023\u003DzeaszzAAoBOY9(), XXX.SSS(-539433270), converter: (IValueConverter) new Ecng.Xaml.Converters.ColorToBrushConverter());
        target.SetBindings(dje_z8KP94MAVAC8XVLJD2A4JR8Q5NFBLA2X8EB75XQ5CG2QAS7W8ZPHQMTXMAE3XE28M8857AH3KGB5ERBMZUQ2EEMGNBDTCXDA2A3_ejd.\u0023\u003DzofZZEllOaqFCpmhtlA\u003D\u003D, (object) this.\u0023\u003DzeaszzAAoBOY9(), XXX.SSS(-539433330));
        target.SetBindings(dje_z8KP94MAVAC8XVLJD2A4JR8Q5NFBLA2X8EB75XQ5CG2QAS7W8ZPHQMTXMAE3XE28M8857AH3KGB5ERBMZUQ2EEMGNBDTCXDA2A3_ejd.\u0023\u003DzGlIIfIPiLV_pixpxdA\u003D\u003D, (object) this.\u0023\u003DzeaszzAAoBOY9(), XXX.SSS(-539433312));
        target.SetBindings(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzTe_gV3cWjEp7, (object) this.\u0023\u003DzeaszzAAoBOY9(), XXX.SSS(-539434641));
        target.PaletteProvider = (\u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh1gj3_fBHIvbLIG5Htg5ScQRmCkwmAANyPA\u003D) this;
        break;
      case ChartCandleDrawStyles.Ohlc:
        target = (dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd) this.\u0023\u003Dzj4cwTqTBSZ3fAaZzTX46uig\u003D<dje_zXLPLNSHM7QU5GA2T5S4C4SHVF6L8PVAPMBCTY5WFUGN8USCF94BEQYVU9VJCT4CGFWU4FYRZXRPAXM3VUD4LQ_ejd>(new \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy[4]
        {
          this.\u0023\u003DzCKkTLgMyXhNP,
          this.\u0023\u003DzfR8j2PM1RBui,
          this.\u0023\u003DzjAnzC1Gk9\u0024VP,
          this.\u0023\u003DzKCOaMdzTEc8\u0024
        });
        target.SetBindings(dje_zXLPLNSHM7QU5GA2T5S4C4SHVF6L8PVAPMBCTY5WFUGN8USCF94BEQYVU9VJCT4CGFWU4FYRZXRPAXM3VUD4LQ_ejd.\u0023\u003DzofZZEllOaqFCpmhtlA\u003D\u003D, (object) this.\u0023\u003DzeaszzAAoBOY9(), XXX.SSS(-539433330));
        target.SetBindings(dje_zXLPLNSHM7QU5GA2T5S4C4SHVF6L8PVAPMBCTY5WFUGN8USCF94BEQYVU9VJCT4CGFWU4FYRZXRPAXM3VUD4LQ_ejd.\u0023\u003DzGlIIfIPiLV_pixpxdA\u003D\u003D, (object) this.\u0023\u003DzeaszzAAoBOY9(), XXX.SSS(-539433312));
        target.SetBindings(dje_zXLPLNSHM7QU5GA2T5S4C4SHVF6L8PVAPMBCTY5WFUGN8USCF94BEQYVU9VJCT4CGFWU4FYRZXRPAXM3VUD4LQ_ejd.\u0023\u003DzVvc2lVdKTrj8, (object) this.\u0023\u003DzeaszzAAoBOY9(), XXX.SSS(-539434641), converter: (IValueConverter) new \u0023\u003Dz5CbAZMXp7dgzzBe\u0024G3xhiiYRKe0897RDjLr\u0024L9wcxjXImUKaPnpxZj0\u003D());
        BindingOperations.ClearBinding((DependencyObject) target, dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzTe_gV3cWjEp7);
        target.PaletteProvider = (\u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh1gj3_fBHIvbLIG5Htg5ScQRmCkwmAANyPA\u003D) this;
        break;
      case ChartCandleDrawStyles.LineOpen:
      case ChartCandleDrawStyles.LineHigh:
      case ChartCandleDrawStyles.LineLow:
      case ChartCandleDrawStyles.LineClose:
        target = (dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd) this.\u0023\u003Dzj4cwTqTBSZ3fAaZzTX46uig\u003D<dje_zN8RD3UL4Q5RJJYR8DXFMVFNTLQD93DU4393K4ENMLPSZDUWE6QXAWS9WCUYMARFXHNJL76MQ47Z7V297V2U22_ejd>(new \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy[1]
        {
          this.\u0023\u003DzdaM3_c5kMuSK
        });
        target.SetBindings(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzIcVMwZBBZ1n3, (object) this.\u0023\u003Dznrzap3Ru8O5U, XXX.SSS(-539434682));
        target.SetBindings(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzTe_gV3cWjEp7, (object) this.\u0023\u003DzeaszzAAoBOY9(), XXX.SSS(-539434641));
        target.SetBindings(dje_zN8RD3UL4Q5RJJYR8DXFMVFNTLQD93DU4393K4ENMLPSZDUWE6QXAWS9WCUYMARFXHNJL76MQ47Z7V297V2U22_ejd.\u0023\u003DzGQjlrvOq17qko2MPPw\u003D\u003D, (object) this.\u0023\u003DzeaszzAAoBOY9(), XXX.SSS(-539433247), converter: (IValueConverter) new \u0023\u003DzdkTsoRIhz16dAJ0Ha_QZUm_WprFxHE3GMkMYTmM5Gv6xPE1lfu86yUSurnkA());
        break;
      case ChartCandleDrawStyles.BoxVolume:
        target = (dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd) this.\u0023\u003Dzj4cwTqTBSZ3fAaZzTX46uig\u003D<dje_zPAYV36PKF8VT83W7TYA6GZS9WQ7A9GC3RX935K47MM57JXW4GNVYASU299URDR9BJTW9S4P5GLA76LMRLQ4YZ_ejd>(new \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy[1]
        {
          this.\u0023\u003DzWbY5DVo2YLuB
        });
        target.SetBindings(dje_zPAYV36PKF8VT83W7TYA6GZS9WQ7A9GC3RX935K47MM57JXW4GNVYASU299URDR9BJTW9S4P5GLA76LMRLQ4YZ_ejd.\u0023\u003Dz1cQ4wVMzHjSWdOxgiAXT\u0024gw\u003D, (object) this.\u0023\u003Dznrzap3Ru8O5U, XXX.SSS(-539434590));
        target.SetBindings(dje_zPAYV36PKF8VT83W7TYA6GZS9WQ7A9GC3RX935K47MM57JXW4GNVYASU299URDR9BJTW9S4P5GLA76LMRLQ4YZ_ejd.\u0023\u003DzFin1pB0TmtTClsb5xicohbw\u003D, (object) this.\u0023\u003Dznrzap3Ru8O5U, XXX.SSS(-539434616));
        target.SetBindings(dje_zPAYV36PKF8VT83W7TYA6GZS9WQ7A9GC3RX935K47MM57JXW4GNVYASU299URDR9BJTW9S4P5GLA76LMRLQ4YZ_ejd.\u0023\u003Dzu_UBGSutECvla_Z4FBIjQVw\u003D, (object) this.\u0023\u003Dznrzap3Ru8O5U, XXX.SSS(-539434899));
        target.SetBindings(dje_zPAYV36PKF8VT83W7TYA6GZS9WQ7A9GC3RX935K47MM57JXW4GNVYASU299URDR9BJTW9S4P5GLA76LMRLQ4YZ_ejd.\u0023\u003DzJmV2YqfjqzBN, (object) this.\u0023\u003Dznrzap3Ru8O5U, XXX.SSS(-539434542));
        target.SetBindings(dje_zPAYV36PKF8VT83W7TYA6GZS9WQ7A9GC3RX935K47MM57JXW4GNVYASU299URDR9BJTW9S4P5GLA76LMRLQ4YZ_ejd.\u0023\u003Dz\u0024pEkT\u0024GyBsLd, (object) this.\u0023\u003Dznrzap3Ru8O5U, XXX.SSS(-539434885));
        target.SetBindings(dje_zPAYV36PKF8VT83W7TYA6GZS9WQ7A9GC3RX935K47MM57JXW4GNVYASU299URDR9BJTW9S4P5GLA76LMRLQ4YZ_ejd.\u0023\u003DzeOkHtL891mol_E40\u0024w\u003D\u003D, (object) this.\u0023\u003Dznrzap3Ru8O5U, XXX.SSS(-539434938));
        target.SetBindings(dje_zPAYV36PKF8VT83W7TYA6GZS9WQ7A9GC3RX935K47MM57JXW4GNVYASU299URDR9BJTW9S4P5GLA76LMRLQ4YZ_ejd.\u0023\u003DzY\u0024hEpvHX6SG5MENm\u0024w\u003D\u003D, (object) this.\u0023\u003DzeaszzAAoBOY9(), XXX.SSS(-539434524), BindingMode.OneWay, (IValueConverter) new \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUmMviGnZF5zyP8Vq715pyobvSG_F30ddnEdMvAIP_dliVQ\u003D\u003D.\u0023\u003DzBaQAMfOA0Xrd(this.CandlesTimeframe));
        target.SetBindings(dje_zPAYV36PKF8VT83W7TYA6GZS9WQ7A9GC3RX935K47MM57JXW4GNVYASU299URDR9BJTW9S4P5GLA76LMRLQ4YZ_ejd.\u0023\u003DzsXfDaxv8JwjzCERgkg\u003D\u003D, (object) this.\u0023\u003DzeaszzAAoBOY9(), XXX.SSS(-539434551), BindingMode.OneWay, (IValueConverter) new \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUmMviGnZF5zyP8Vq715pyobvSG_F30ddnEdMvAIP_dliVQ\u003D\u003D.\u0023\u003DzBaQAMfOA0Xrd(this.CandlesTimeframe));
        break;
      case ChartCandleDrawStyles.ClusterProfile:
        target = (dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd) this.\u0023\u003Dzj4cwTqTBSZ3fAaZzTX46uig\u003D<dje_zMFW7VEH9YQSML9Y7R42FYSK685GBSP6YDHCWWGNXQE6V7BBN8HWC7XLY67P47NZHA3X42ZPH5UUQPRZRVLJWD_ejd>(new \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy[1]
        {
          this.\u0023\u003DzWbY5DVo2YLuB
        });
        target.SetBindings(dje_zMFW7VEH9YQSML9Y7R42FYSK685GBSP6YDHCWWGNXQE6V7BBN8HWC7XLY67P47NZHA3X42ZPH5UUQPRZRVLJWD_ejd.\u0023\u003Dz3vhKFHvmUfSR, (object) this.\u0023\u003Dznrzap3Ru8O5U, XXX.SSS(-539434959));
        target.SetBindings(dje_zMFW7VEH9YQSML9Y7R42FYSK685GBSP6YDHCWWGNXQE6V7BBN8HWC7XLY67P47NZHA3X42ZPH5UUQPRZRVLJWD_ejd.\u0023\u003DzTpSTo8U\u003D, (object) this.\u0023\u003Dznrzap3Ru8O5U, XXX.SSS(-539434968));
        target.SetBindings(dje_zMFW7VEH9YQSML9Y7R42FYSK685GBSP6YDHCWWGNXQE6V7BBN8HWC7XLY67P47NZHA3X42ZPH5UUQPRZRVLJWD_ejd.\u0023\u003DzFLdFQ9M\u003D, (object) this.\u0023\u003Dznrzap3Ru8O5U, XXX.SSS(-539434991));
        target.SetBindings(dje_zMFW7VEH9YQSML9Y7R42FYSK685GBSP6YDHCWWGNXQE6V7BBN8HWC7XLY67P47NZHA3X42ZPH5UUQPRZRVLJWD_ejd.\u0023\u003DzpELAaZtMaBgQ, (object) this.\u0023\u003Dznrzap3Ru8O5U, XXX.SSS(-539434754));
        target.SetBindings(dje_zMFW7VEH9YQSML9Y7R42FYSK685GBSP6YDHCWWGNXQE6V7BBN8HWC7XLY67P47NZHA3X42ZPH5UUQPRZRVLJWD_ejd.\u0023\u003DzcL2qfCOpQZK5, (object) this.\u0023\u003Dznrzap3Ru8O5U, XXX.SSS(-539434805));
        break;
      case ChartCandleDrawStyles.Area:
        target = (dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd) this.\u0023\u003Dzj4cwTqTBSZ3fAaZzTX46uig\u003D<dje_zKEMJWHZWSE279KQB6EDEEPQJWFZTHVK3RNB3SECQBVFS2MZJVYVEKR2UZXXBEHEVGBXXDD3GV8492CURMVVPXNR5WLZA_ejd>(new \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy[1]
        {
          this.\u0023\u003DzdaM3_c5kMuSK
        });
        target.SeriesColor = Colors.Transparent;
        target.SetBindings(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzIcVMwZBBZ1n3, (object) this.\u0023\u003Dznrzap3Ru8O5U, XXX.SSS(-539434682));
        target.SetBindings(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzTe_gV3cWjEp7, (object) this.\u0023\u003DzeaszzAAoBOY9(), XXX.SSS(-539434641));
        target.SetBindings(dje_z69W49PU7FS9ADZCYETBA2JNAH33HZA6JARXFFS3478TNDWQCS56A84JEUPSV3BARVVHU59UKY87PYJ97Q4K7W5RSK8AA_ejd.\u0023\u003DzXc9apgJiH9mm, (object) this.\u0023\u003Dznrzap3Ru8O5U, XXX.SSS(-539434666), converter: (IValueConverter) new Ecng.Xaml.Converters.ColorToBrushConverter());
        break;
      case ChartCandleDrawStyles.PnF:
        target = (dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd) this.\u0023\u003Dzj4cwTqTBSZ3fAaZzTX46uig\u003D<dje_z2F7KFUZRTFPWT6JP9QD62893JPNDH7HDZ2FZKZQYFCVV32D7MT596XGT8S3KNWNR57LVXU8TJABWGARB6RDEX_ejd>(new \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy[4]
        {
          this.\u0023\u003DzCKkTLgMyXhNP,
          this.\u0023\u003DzfR8j2PM1RBui,
          this.\u0023\u003DzjAnzC1Gk9\u0024VP,
          this.\u0023\u003DzKCOaMdzTEc8\u0024
        });
        target.SetBindings(dje_zXLPLNSHM7QU5GA2T5S4C4SHVF6L8PVAPMBCTY5WFUGN8USCF94BEQYVU9VJCT4CGFWU4FYRZXRPAXM3VUD4LQ_ejd.\u0023\u003DzofZZEllOaqFCpmhtlA\u003D\u003D, (object) this.\u0023\u003DzeaszzAAoBOY9(), XXX.SSS(-539433330));
        target.SetBindings(dje_zXLPLNSHM7QU5GA2T5S4C4SHVF6L8PVAPMBCTY5WFUGN8USCF94BEQYVU9VJCT4CGFWU4FYRZXRPAXM3VUD4LQ_ejd.\u0023\u003DzGlIIfIPiLV_pixpxdA\u003D\u003D, (object) this.\u0023\u003DzeaszzAAoBOY9(), XXX.SSS(-539433312));
        target.SetBindings(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzTe_gV3cWjEp7, (object) this.\u0023\u003DzeaszzAAoBOY9(), XXX.SSS(-539434641));
        Decimal? zfIlpmP5Jfeem = this.\u0023\u003DzfILpmP5JFeem;
        if (zfIlpmP5Jfeem.HasValue)
        {
          Decimal valueOrDefault = zfIlpmP5Jfeem.GetValueOrDefault();
          ((dje_z2F7KFUZRTFPWT6JP9QD62893JPNDH7HDZ2FZKZQYFCVV32D7MT596XGT8S3KNWNR57LVXU8TJABWGARB6RDEX_ejd) target).XOBoxSize = (double) valueOrDefault;
        }
        target.PaletteProvider = (\u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh1gj3_fBHIvbLIG5Htg5ScQRmCkwmAANyPA\u003D) this;
        break;
      default:
        throw new InvalidOperationException(StringHelper.Put(LocalizedStrings.UnsupportedType, new object[1]
        {
          (object) this.\u0023\u003DzeaszzAAoBOY9().DrawStyle
        }));
    }
    if (this.\u0023\u003DzeaszzAAoBOY9().DrawStyle.IsVolumeProfileChart())
    {
      target.SetBindings(Control.FontFamilyProperty, (object) this.\u0023\u003DzeaszzAAoBOY9(), XXX.SSS(-539434227), converter: (IValueConverter) new FontFamilyValueConverter());
      target.SetBindings(Control.FontSizeProperty, (object) this.\u0023\u003DzeaszzAAoBOY9(), XXX.SSS(-539434223), converter: (IValueConverter) new TypeCastConverter());
      target.SetBindings(Control.FontWeightProperty, (object) this.\u0023\u003DzeaszzAAoBOY9(), XXX.SSS(-539434010));
      target.SetBindings(\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003DzclgWzqVX9aMz0ymbjg\u003D\u003D, (object) this.\u0023\u003DzeaszzAAoBOY9(), XXX.SSS(-539434746));
      target.SetBindings(\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003DzQertKBH63fdmC8AJyRsykJc\u003D, (object) this.\u0023\u003DzeaszzAAoBOY9(), XXX.SSS(-539434799));
      target.SetBindings(\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003Dzi0KcPWeppPsdH7enWLwUIiA\u003D, (object) this.\u0023\u003DzeaszzAAoBOY9(), XXX.SSS(-539434827));
      target.SetBindings(\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003DzItT23YVSDC4D2EO\u0024iA\u003D\u003D, (object) this.\u0023\u003DzeaszzAAoBOY9(), XXX.SSS(-539434856));
      target.SetBindings(\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003DzkZL83tvq\u0024ktX, (object) this.\u0023\u003Dznrzap3Ru8O5U, XXX.SSS(-539434124), BindingMode.OneWay, (IValueConverter) new Ecng.Xaml.Converters.ColorToBrushConverter());
      target.SetBindings(\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003DzagbwXpWOZg7bLqSU7A\u003D\u003D, (object) this.\u0023\u003Dznrzap3Ru8O5U, XXX.SSS(-539434152));
      target.SetBindings(\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003Dz6m31HqKWmW3sf6kpzl00drQ\u003D, (object) this.\u0023\u003DzeaszzAAoBOY9(), XXX.SSS(-539433999));
      target.SetBindings(\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003DzsiwadMWumB4q, (object) this.\u0023\u003Dznrzap3Ru8O5U, XXX.SSS(-539433667));
      target.SetBindings(\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003DzyQkELvBo\u002432H, (object) this.\u0023\u003Dznrzap3Ru8O5U, XXX.SSS(-539433497));
      target.SetBindings(\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003Dzd0DbwufxB45U, (object) this.\u0023\u003Dznrzap3Ru8O5U, XXX.SSS(-539432639));
      target.SetBindings(\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003DzMymlj1BxH8MY, (object) this.\u0023\u003Dznrzap3Ru8O5U, XXX.SSS(-539432591));
    }
    return target;
  }

  protected override void \u0023\u003DzXfak0jM\u003D()
  {
    this.\u0023\u003Dz_JPQrgU\u003D();
    this.\u0023\u003Dznrzap3Ru8O5U.Dispose();
  }

  protected override void \u0023\u003DzowR7R4A\u003D()
  {
    this.OhlcSeries.Clear();
    this.OhlcSeries.Timeframe = new TimeSpan?();
    this.\u0023\u003DzVtuwsFJcSHit.Clear();
    this.\u0023\u003DzQ73Ei9NuGdXX = (TimeframeSegmentDataSeries) null;
    this.\u0023\u003DzTqpoRUfBxm2O = new DateTime();
    this.\u0023\u003DzfILpmP5JFeem = new Decimal?();
    this.\u0023\u003DzK1tfXeY7PPNb.Clear();
    this.\u0023\u003Dz4EoFHUaZg4JL(new Action(this.\u0023\u003Dzp5J6G301TJ9o31R2wgChW2E\u003D), true);
  }

  private void \u0023\u003DztzYeKaM\u003D()
  {
    this.\u0023\u003DzKj7nvWQ\u003D = new \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEf8BMO_ZOBlhDA\u003D\u003D(this.\u0023\u003Dz8bZd_lILC\u0024Y\u0024(), (IRenderableSeries) this.\u0023\u003Dzj4cwTqTBSZ3fAaZzTX46uig\u003D());
    this.\u0023\u003Dz\u00246aIVrHDxlRJ().\u0023\u003DzBE5I4io\u003D(this.RootElem, (\u0023\u003DziARJyOecclYiJO5UbZqQJ_KOh9jO5RUUcFIAqLc\u003D) this.\u0023\u003DzKj7nvWQ\u003D);
    this.\u0023\u003DzAgVixDQ6Vc2r();
    this.\u0023\u003Dz7GhHTEkMkDYT(this.\u0023\u003DzKj7nvWQ\u003D.RenderSeries, (IfxChartElement) this.\u0023\u003DzeaszzAAoBOY9(), XXX.SSS(-539434714), (string) null);
  }

  private void \u0023\u003Dz_JPQrgU\u003D()
  {
    if (this.\u0023\u003DzKj7nvWQ\u003D == null)
      return;
    this.\u0023\u003Dz\u00246aIVrHDxlRJ().\u0023\u003Dzwh_e_TheVZKh(this.RootElem);
    this.\u0023\u003DzKj7nvWQ\u003D = (\u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEf8BMO_ZOBlhDA\u003D\u003D) null;
  }

  public override void \u0023\u003DzKy5smiO3gHXp()
  {
    base.\u0023\u003DzKy5smiO3gHXp();
    \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR g72ZksY7iW1Jk3iR = this.\u0023\u003Dz\u00246aIVrHDxlRJ().\u0023\u003DzOALCA8UxYpqEXXXKxQ\u003D\u003D(this.\u0023\u003DzeaszzAAoBOY9().XAxisId);
    if (g72ZksY7iW1Jk3iR == null)
      return;
    \u0023\u003DzR2x48Sho4AxfV9DSAxG8OQ2rGx6KyfAREQ\u003D\u003D categoryDateTimeRange = g72ZksY7iW1Jk3iR.CategoryDateTimeRange;
    int count = this.OhlcSeries.Count;
    bool flag1 = count > 0 && categoryDateTimeRange.IsDefined && !this.\u0023\u003Dz\u00246aIVrHDxlRJ().Chart.IsAutoRange;
    bool flag2 = flag1 && this.\u0023\u003Dz\u00246aIVrHDxlRJ().Chart.IsAutoScroll;
    if (!this.\u0023\u003DzZ8C5xEdop1TJ & flag1)
    {
      this.\u0023\u003DzZ8C5xEdop1TJ = true;
      if (!flag2)
      {
        \u0023\u003DzR2x48Sho4AxfV9DSAxG8OQ2rGx6KyfAREQ\u003D\u003D g8Oq2rGx6KyfAreq = new \u0023\u003DzR2x48Sho4AxfV9DSAxG8OQ2rGx6KyfAREQ\u003D\u003D(0, categoryDateTimeRange.Max - categoryDateTimeRange.Min);
        g72ZksY7iW1Jk3iR.CategoryDateTimeRange = g8Oq2rGx6KyfAreq;
      }
    }
    if (!flag2)
    {
      this.\u0023\u003DzuuCm_xeHWuMy = new bool?();
      this.\u0023\u003DzUuMx9G25\u0024d0f = (\u0023\u003DzR2x48Sho4AxfV9DSAxG8OQ2rGx6KyfAREQ\u003D\u003D) null;
    }
    else
    {
      int num1;
      if (this.\u0023\u003DzuuCm_xeHWuMy.HasValue && (!this.\u0023\u003DzuuCm_xeHWuMy.GetValueOrDefault() || this.\u0023\u003DzUuMx9G25\u0024d0f != categoryDateTimeRange))
      {
        bool? zuuCmXeHwuMy = this.\u0023\u003DzuuCm_xeHWuMy;
        num1 = !(!zuuCmXeHwuMy.GetValueOrDefault() & zuuCmXeHwuMy.HasValue) ? 0 : (categoryDateTimeRange.Max >= count ? 1 : 0);
      }
      else
        num1 = 1;
      bool flag3 = num1 != 0;
      int num2 = count;
      if (flag3 && (categoryDateTimeRange.Max < num2 || !this.\u0023\u003DzuuCm_xeHWuMy.HasValue))
      {
        int num3 = categoryDateTimeRange.Max - categoryDateTimeRange.Min + 1;
        \u0023\u003DzR2x48Sho4AxfV9DSAxG8OQ2rGx6KyfAREQ\u003D\u003D g8Oq2rGx6KyfAreq = new \u0023\u003DzR2x48Sho4AxfV9DSAxG8OQ2rGx6KyfAREQ\u003D\u003D(num2 - num3 + 1, num2);
        g72ZksY7iW1Jk3iR.CategoryDateTimeRange = g8Oq2rGx6KyfAreq;
        this.\u0023\u003DzUuMx9G25\u0024d0f = g8Oq2rGx6KyfAreq;
      }
      else
        this.\u0023\u003DzUuMx9G25\u0024d0f = categoryDateTimeRange;
      this.\u0023\u003DzuuCm_xeHWuMy = new bool?(flag3);
    }
  }

  public override bool \u0023\u003DzjgUUUJE\u003D(IEnumerableEx<ChartDrawData.IDrawValue> _param1)
  {
    switch (_param1 != null ? ((IEnumerable<ChartDrawData.IDrawValue>) _param1).FirstOrDefault<ChartDrawData.IDrawValue>() : (ChartDrawData.IDrawValue) null)
    {
      case null:
        return false;
      case ChartDrawData.sCandle _:
        return this.\u0023\u003DzjgUUUJE\u003D(CollectionHelper.ToEx<ChartDrawData.sCandle>(((IEnumerable) _param1).Cast<ChartDrawData.sCandle>(), ((IEnumerableEx) _param1).Count));
      case ChartDrawData.sCandleColor _:
        return this.\u0023\u003DzjgUUUJE\u003D(CollectionHelper.ToEx<ChartDrawData.sCandleColor>(((IEnumerable) _param1).Cast<ChartDrawData.sCandleColor>(), ((IEnumerableEx) _param1).Count));
      default:
        throw new ArgumentOutOfRangeException(XXX.SSS(-539329773));
    }
  }

  private bool \u0023\u003DzjgUUUJE\u003D(
    IEnumerableEx<ChartDrawData.sCandleColor> _param1)
  {
    if (CollectionHelper.IsEmpty<ChartDrawData.sCandleColor>((IEnumerable<ChartDrawData.sCandleColor>) _param1))
      return false;
    foreach (ChartDrawData.sCandleColor zs3gDb01RWCzVlS5w in (IEnumerable<ChartDrawData.sCandleColor>) _param1)
    {
      Color? color1 = zs3gDb01RWCzVlS5w.Color;
      if (color1.HasValue)
      {
        SynchronizedDictionary<DateTime, Color> zK1tfXeY7PpNb = this.\u0023\u003DzK1tfXeY7PPNb;
        DateTime dateTime = zs3gDb01RWCzVlS5w.\u0023\u003Dzg86amuQ\u003D();
        color1 = zs3gDb01RWCzVlS5w.Color;
        Color color2 = color1.Value;
        zK1tfXeY7PpNb[dateTime] = color2;
      }
      else
        this.\u0023\u003DzK1tfXeY7PPNb.Remove(zs3gDb01RWCzVlS5w.\u0023\u003Dzg86amuQ\u003D());
    }
    this.\u0023\u003DzKj7nvWQ\u003D?.RenderSeries.\u0023\u003Dzu\u0024P3XgkcE7BC()?.\u0023\u003Dz2VqWonc\u003D<\u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D>()?.\u0023\u003Dz5q8i9C4\u003D();
    return true;
  }

  private bool \u0023\u003DzjgUUUJE\u003D(
    IEnumerableEx<ChartDrawData.sCandle> _param1)
  {
    if (this.\u0023\u003DzVnKR\u0024HeQJSKTkReJlfF5mAk\u003D != this.\u0023\u003DzeaszzAAoBOY9().Colorer)
    {
      this.\u0023\u003DzVnKR\u0024HeQJSKTkReJlfF5mAk\u003D = this.\u0023\u003DzeaszzAAoBOY9().Colorer;
      this.\u0023\u003DzKj7nvWQ\u003D?.RenderSeries.\u0023\u003Dzu\u0024P3XgkcE7BC()?.\u0023\u003Dz2VqWonc\u003D<\u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D>()?.\u0023\u003Dz5q8i9C4\u003D();
    }
    if (_param1 == null || CollectionHelper.IsEmpty<ChartDrawData.sCandle>((IEnumerable<ChartDrawData.sCandle>) _param1))
      return false;
    int count = ((IEnumerableEx) _param1).Count;
    DateTime dateTime = this.\u0023\u003DzTqpoRUfBxm2O;
    int index = -1;
    bool flag = false;
    DateTime[] array1 = new DateTime[count];
    double[] array2 = new double[count];
    double[] array3 = new double[count];
    double[] array4 = new double[count];
    double[] array5 = new double[count];
    foreach (ChartDrawData.sCandle zbzWrwPExZ6TzuVkEg in (IEnumerable<ChartDrawData.sCandle>) _param1)
    {
      object obj = zbzWrwPExZ6TzuVkEg.\u0023\u003DzdR0PhFO4Br84().Arg;
      if (obj is TimeSpan timeSpan)
        this.OhlcSeries.Timeframe = new TimeSpan?(timeSpan);
      this.\u0023\u003DzuqKjoe_5lTdYuAkGZw\u003D\u003D(obj);
      this.\u0023\u003DzywI2pEkeRxof();
      switch (zbzWrwPExZ6TzuVkEg.\u0023\u003Dzg86amuQ\u003D().CompareTo(dateTime))
      {
        case -1:
          throw new InvalidOperationException(StringHelper.Put(LocalizedStrings.CannotChangeCandleValue, new object[2]
          {
            (object) zbzWrwPExZ6TzuVkEg.\u0023\u003Dzg86amuQ\u003D(),
            (object) dateTime
          }));
        case 0:
          flag = true;
          this.OhlcSeries.\u0023\u003DzFkV86a8\u003D(zbzWrwPExZ6TzuVkEg.\u0023\u003Dzg86amuQ\u003D(), zbzWrwPExZ6TzuVkEg.\u0023\u003DzGze4a8XU7KvB(), zbzWrwPExZ6TzuVkEg.\u0023\u003DzolXXlhDBER_c(), zbzWrwPExZ6TzuVkEg.\u0023\u003DzchuwVU\u00245sIH8(), zbzWrwPExZ6TzuVkEg.Close);
          this.\u0023\u003DzVtuwsFJcSHit.\u0023\u003DzFkV86a8\u003D(zbzWrwPExZ6TzuVkEg.\u0023\u003Dzg86amuQ\u003D(), zbzWrwPExZ6TzuVkEg.Close);
          if (zbzWrwPExZ6TzuVkEg.\u0023\u003Dzeu8tE1P9bfD8() != null && this.\u0023\u003DzQ73Ei9NuGdXX != null)
          {
            foreach (CandlePriceLevel level in zbzWrwPExZ6TzuVkEg.\u0023\u003Dzeu8tE1P9bfD8())
              this.\u0023\u003DzQ73Ei9NuGdXX.Update(zbzWrwPExZ6TzuVkEg.\u0023\u003Dzg86amuQ\u003D(), (double) ((CandlePriceLevel) ref level).Price, level);
          }
          --count;
          break;
        default:
          ++index;
          array1[index] = zbzWrwPExZ6TzuVkEg.\u0023\u003Dzg86amuQ\u003D();
          array2[index] = zbzWrwPExZ6TzuVkEg.\u0023\u003DzGze4a8XU7KvB();
          array3[index] = zbzWrwPExZ6TzuVkEg.\u0023\u003DzolXXlhDBER_c();
          array4[index] = zbzWrwPExZ6TzuVkEg.\u0023\u003DzchuwVU\u00245sIH8();
          array5[index] = zbzWrwPExZ6TzuVkEg.Close;
          if (zbzWrwPExZ6TzuVkEg.\u0023\u003Dzeu8tE1P9bfD8() != null && this.\u0023\u003DzQ73Ei9NuGdXX != null)
          {
            foreach (CandlePriceLevel level in zbzWrwPExZ6TzuVkEg.\u0023\u003Dzeu8tE1P9bfD8())
              this.\u0023\u003DzQ73Ei9NuGdXX.Append(zbzWrwPExZ6TzuVkEg.\u0023\u003Dzg86amuQ\u003D(), (double) ((CandlePriceLevel) ref level).Price, level);
            break;
          }
          break;
      }
      dateTime = zbzWrwPExZ6TzuVkEg.\u0023\u003Dzg86amuQ\u003D();
    }
    if (count == 0)
      return flag;
    Array.Resize<DateTime>(ref array1, count);
    Array.Resize<double>(ref array2, count);
    Array.Resize<double>(ref array3, count);
    Array.Resize<double>(ref array4, count);
    Array.Resize<double>(ref array5, count);
    this.OhlcSeries.\u0023\u003Dznc8esWY\u003D((IEnumerable<DateTime>) array1, (IEnumerable<double>) array2, (IEnumerable<double>) array3, (IEnumerable<double>) array4, (IEnumerable<double>) array5);
    this.\u0023\u003DzVtuwsFJcSHit.\u0023\u003Dznc8esWY\u003D((IEnumerable<DateTime>) array1, (IEnumerable<double>) array5);
    this.\u0023\u003DzTqpoRUfBxm2O = dateTime;
    return true;
  }

  Color? \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh1gj3_fBHIvbLIG5Htg5ScQRmCkwmAANyPA\u003D.\u0023\u003DzbcX\u0024ot\u0024Zhy6wUdB9J7NC3yzxavQxmzggPmZ8V62OI0Kr0hq2Km30eq101sCk(
    IRenderableSeries _param1,
    double _param2,
    double _param3)
  {
    return new Color?();
  }

  Color? \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh1gj3_fBHIvbLIG5Htg5ScQRmCkwmAANyPA\u003D.\u0023\u003DzaP7vgnwtOd1ghQwnj\u00248jG1HXnj3oIOVYNqLIS92dT0MqcEWOD7IYv5ohC5gQ(
    IRenderableSeries _param1,
    double _param2,
    double _param3,
    double _param4)
  {
    return new Color?();
  }

  Color? \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh1gj3_fBHIvbLIG5Htg5ScQRmCkwmAANyPA\u003D.\u0023\u003DzaP7vgnwtOd1ghQwnj\u00248jG1HXnj3oIOVYNqLIS92dT0MqcEWOD7IYv5ohC5gQ(
    IRenderableSeries _param1,
    double _param2,
    double _param3,
    double _param4,
    double _param5,
    double _param6)
  {
    int index = (int) _param2;
    DateTime xvalue = this.OhlcSeries.XValues[index];
    foreach (\u0023\u003DzGf68ilGq59TJ0aVKr0K_9Ur9WO7TFzBBD24ufNSokcCRpZmI_iRRi1f09FSCgNU6tg\u003D\u003D rri1f09FsCgNu6tg in (BaseCollection<\u0023\u003DzGf68ilGq59TJ0aVKr0K_9Ur9WO7TFzBBD24ufNSokcCRpZmI_iRRi1f09FSCgNU6tg\u003D\u003D, List<\u0023\u003DzGf68ilGq59TJ0aVKr0K_9Ur9WO7TFzBBD24ufNSokcCRpZmI_iRRi1f09FSCgNU6tg\u003D\u003D>>) this.\u0023\u003DzSi1nzfdLoJWYKewDlw\u003D\u003D)
    {
      Color? nullable = rri1f09FsCgNu6tg.\u0023\u003Dzj4w_lAs\u003D(xvalue, _param6 > _param3);
      if (nullable.HasValue)
        return nullable;
    }
    Color color;
    if (this.\u0023\u003DzK1tfXeY7PPNb.TryGetValue(xvalue, ref color))
      return new Color?(color);
    Func<DateTimeOffset, bool, bool, Color?> qjskTkReJlfF5mAk = this.\u0023\u003DzVnKR\u0024HeQJSKTkReJlfF5mAk\u003D;
    return qjskTkReJlfF5mAk == null ? new Color?() : qjskTkReJlfF5mAk(TimeHelper.ToDateTimeOffset(xvalue, TimeZoneInfo.Utc), _param6 >= _param3, index == this.OhlcSeries.Count - 1);
  }

  internal static bool \u0023\u003DqUITHEHars91eFjENoL1ls59z\u0024GzJCXEyQkVMXaxFgmo\u003D(
    \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param0)
  {
    return _param0 is \u0023\u003Dz3HkNAtjftY7KLZeVO1e0c8c41pWQbDKntdB13Yg\u003D vo1e0c8c41pWqbDkntdB13Yg && vo1e0c8c41pWqbDkntdB13Yg.CloseValue > vo1e0c8c41pWqbDkntdB13Yg.OpenValue;
  }

  private Color \u0023\u003Dqo12wao\u0024ehitMzQgoNEdrsjKxeleFvrK0RDpR\u0024y51Ghs\u003D(
    \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param1)
  {
    Color color;
    if (this.\u0023\u003DzK1tfXeY7PPNb.TryGetValue(this.OhlcSeries.XValues[_param1.DataSeriesIndex], ref color))
      return color;
    return \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUmMviGnZF5zyP8Vq715pyobvSG_F30ddnEdMvAIP_dliVQ\u003D\u003D.\u0023\u003DqUITHEHars91eFjENoL1ls59z\u0024GzJCXEyQkVMXaxFgmo\u003D(_param1) ? (!this.\u0023\u003Dznrzap3Ru8O5U.IsDark ? Colors.Green : Colors.LimeGreen) : (!this.\u0023\u003Dznrzap3Ru8O5U.IsDark ? Colors.Red : Colors.OrangeRed);
  }

  private string \u0023\u003DqTIcmF95twR2vB7L3I\u0024trXELaSG5nNQSTgo5LhClfL2U\u003D(
    \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param1)
  {
    if (!(_param1 is \u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciTIXSve_YM8UPQ\u003D\u003D uyciTixSveYm8Upq))
      return (string) null;
    switch (this.\u0023\u003DzeaszzAAoBOY9().DrawStyle)
    {
      case ChartCandleDrawStyles.LineOpen:
        this.\u0023\u003DzdaM3_c5kMuSK.Name = XXX.SSS(-539329735);
        return uyciTixSveYm8Upq.FormattedYValue;
      case ChartCandleDrawStyles.LineHigh:
        this.\u0023\u003DzdaM3_c5kMuSK.Name = XXX.SSS(-539329743);
        return uyciTixSveYm8Upq.FormattedYValue;
      case ChartCandleDrawStyles.LineLow:
        this.\u0023\u003DzdaM3_c5kMuSK.Name = XXX.SSS(-539329783);
        return uyciTixSveYm8Upq.FormattedYValue;
      case ChartCandleDrawStyles.LineClose:
        this.\u0023\u003DzdaM3_c5kMuSK.Name = XXX.SSS(-539329791);
        return uyciTixSveYm8Upq.FormattedYValue;
      case ChartCandleDrawStyles.Area:
        this.\u0023\u003DzdaM3_c5kMuSK.Name = XXX.SSS(-539329791);
        return uyciTixSveYm8Upq.FormattedYValue;
      default:
        this.\u0023\u003DzdaM3_c5kMuSK.Value = (string) null;
        return (string) null;
    }
  }

  private void \u0023\u003Dzp5J6G301TJ9o31R2wgChW2E\u003D()
  {
    this.\u0023\u003DzAgVixDQ6Vc2r();
    this.\u0023\u003Dz_JPQrgU\u003D();
    this.\u0023\u003DztzYeKaM\u003D();
  }

  [Serializable]
  private new sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUmMviGnZF5zyP8Vq715pyobvSG_F30ddnEdMvAIP_dliVQ\u003D\u003D.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUmMviGnZF5zyP8Vq715pyobvSG_F30ddnEdMvAIP_dliVQ\u003D\u003D.\u0023\u003Dz7qOdpi4\u003D();
    public static Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, string> \u0023\u003DzU9srAoETJDIIA3EbGw\u003D\u003D;
    public static Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, string> \u0023\u003Dzv7x3aq5xjFNkgWHCPg\u003D\u003D;
    public static Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, string> \u0023\u003Dz3QgfRT\u0024GTbbdeFCZLg\u003D\u003D;
    public static Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, string> \u0023\u003Dz3XWQ58Tacl3uxVgGiw\u003D\u003D;
    public static Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, string> \u0023\u003DztKkeF19DCI_S9dyd\u0024A\u003D\u003D;

    internal string \u0023\u003DziVMtGB3eskawtnyDEBGppGQ\u003D(
      \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param1)
    {
      return !(_param1 is \u0023\u003Dz3HkNAtjftY7KLZeVO1e0c8c41pWQbDKntdB13Yg\u003D vo1e0c8c41pWqbDkntdB13Yg) ? (string) null : vo1e0c8c41pWqbDkntdB13Yg.FormattedOpenValue;
    }

    internal string \u0023\u003DzyboOJPrkbYbHrtK_jYxCKz0\u003D(
      \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param1)
    {
      return !(_param1 is \u0023\u003Dz3HkNAtjftY7KLZeVO1e0c8c41pWQbDKntdB13Yg\u003D vo1e0c8c41pWqbDkntdB13Yg) ? (string) null : vo1e0c8c41pWqbDkntdB13Yg.FormattedHighValue;
    }

    internal string \u0023\u003Dz_F9udS\u0024bvXmu7p7kNM2l1tM\u003D(
      \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param1)
    {
      return !(_param1 is \u0023\u003Dz3HkNAtjftY7KLZeVO1e0c8c41pWQbDKntdB13Yg\u003D vo1e0c8c41pWqbDkntdB13Yg) ? (string) null : vo1e0c8c41pWqbDkntdB13Yg.FormattedLowValue;
    }

    internal string \u0023\u003Dz_2ZdaKSEm\u0024dowilMRXDN0r4\u003D(
      \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param1)
    {
      return !(_param1 is \u0023\u003Dz3HkNAtjftY7KLZeVO1e0c8c41pWQbDKntdB13Yg\u003D vo1e0c8c41pWqbDkntdB13Yg) ? (string) null : vo1e0c8c41pWqbDkntdB13Yg.FormattedCloseValue;
    }

    internal string \u0023\u003Dzu0cWRPhAEXPa2F76HfbtuFw\u003D(
      \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param1)
    {
      if (!(_param1 is \u0023\u003DzGULZ_B3lGVEDiq9xPbVQjsPdCs3fSNYVEdhm_bS76Lhc cs3fSnyvEdhmBS76Lhc))
        return string.Empty;
      string str = string.Empty;
      DefaultInterpolatedStringHandler interpolatedStringHandler;
      if (cs3fSnyvEdhmBS76Lhc.XValue != null)
      {
        interpolatedStringHandler = new DefaultInterpolatedStringHandler(11, 4);
        interpolatedStringHandler.AppendLiteral(XXX.SSS(-539329675));
        interpolatedStringHandler.AppendFormatted(cs3fSnyvEdhmBS76Lhc.FormattedOpenValue);
        interpolatedStringHandler.AppendLiteral(XXX.SSS(-539329716));
        interpolatedStringHandler.AppendFormatted(cs3fSnyvEdhmBS76Lhc.FormattedHighValue);
        interpolatedStringHandler.AppendLiteral(XXX.SSS(-539329722));
        interpolatedStringHandler.AppendFormatted(cs3fSnyvEdhmBS76Lhc.FormattedLowValue);
        interpolatedStringHandler.AppendLiteral(XXX.SSS(-539329704));
        interpolatedStringHandler.AppendFormatted(cs3fSnyvEdhmBS76Lhc.FormattedCloseValue);
        str = interpolatedStringHandler.ToStringAndClear();
      }
      interpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
      interpolatedStringHandler.AppendFormatted(str);
      interpolatedStringHandler.AppendLiteral(XXX.SSS(-539329710));
      interpolatedStringHandler.AppendFormatted<CandlePriceLevel>(cs3fSnyvEdhmBS76Lhc.Level);
      return interpolatedStringHandler.ToStringAndClear();
    }
  }

  private sealed class \u0023\u003DzBaQAMfOA0Xrd(TimeSpan? _param1) : IValueConverter
  {
    
    private readonly TimeSpan? \u0023\u003DzIijA5WEGKapy9Yb82g\u003D\u003D = _param1;

    object IValueConverter.\u0023\u003DzM9yoqEmGoL\u0024Vcrr_ku1EGJc\u003D(
      object _param1,
      Type _param2,
      object _param3,
      CultureInfo _param4)
    {
      return (object) (!this.\u0023\u003DzIijA5WEGKapy9Yb82g\u003D\u003D.HasValue || !(_param1 is int num) ? new TimeSpan?() : new TimeSpan?(TimeSpan.FromTicks((long) num * this.\u0023\u003DzIijA5WEGKapy9Yb82g\u003D\u003D.Value.Ticks)));
    }

    object IValueConverter.\u0023\u003Dz7t96kV0doysI1t8U28R3TqlcxXQz(
      object _param1,
      Type _param2,
      object _param3,
      CultureInfo _param4)
    {
      throw new NotSupportedException();
    }
  }

  private sealed class \u0023\u003Dzqyx1Pxv3orWX : NotifiableObject, IDisposable
  {
    
    private readonly ChartCandleElement \u0023\u003DzpojClAU\u003D;
    
    private \u0023\u003DzjIfS4CbXGFDPWmVOPAZGmt9VmM5IkN_VTVwybkk\u003D \u0023\u003DziWrrroE\u003D;
    
    private bool \u0023\u003DzfEd754vMvBRgdJ4LHw\u003D\u003D;

    public \u0023\u003Dzqyx1Pxv3orWX(ChartCandleElement _param1)
    {
      this.\u0023\u003DzpojClAU\u003D = _param1 ?? throw new ArgumentNullException(XXX.SSS(-539329756));
      this.\u0023\u003DzpojClAU\u003D.PropertyChanged += new PropertyChangedEventHandler(this.\u0023\u003Dz15moWio\u003D);
      this.\u0023\u003DzO\u0024GBeNt4gKS5();
      ThemeManager.ApplicationThemeChanged += new ThemeChangedRoutedEventHandler(this.\u0023\u003Dzb3YxwRaSuvYM);
    }

    public void Dispose()
    {
      this.\u0023\u003DzpojClAU\u003D.PropertyChanged -= new PropertyChangedEventHandler(this.\u0023\u003Dz15moWio\u003D);
      ThemeManager.ApplicationThemeChanged -= new ThemeChangedRoutedEventHandler(this.\u0023\u003Dzb3YxwRaSuvYM);
      GC.SuppressFinalize((object) this);
    }

    private void \u0023\u003Dz15moWio\u003D(object _param1, PropertyChangedEventArgs _param2)
    {
      this.NotifyChanged(_param2.PropertyName);
    }

    private void \u0023\u003Dzb3YxwRaSuvYM(
      DependencyObject _param1,
      ThemeChangedRoutedEventArgs _param2)
    {
      this.\u0023\u003DzO\u0024GBeNt4gKS5();
      this.NotifyChanged(XXX.SSS(-539434682));
      this.NotifyChanged(XXX.SSS(-539434666));
      this.NotifyChanged(XXX.SSS(-539434542));
      this.NotifyChanged(XXX.SSS(-539434590));
      this.NotifyChanged(XXX.SSS(-539434616));
      this.NotifyChanged(XXX.SSS(-539434899));
      this.NotifyChanged(XXX.SSS(-539434885));
      this.NotifyChanged(XXX.SSS(-539434938));
      this.NotifyChanged(XXX.SSS(-539434968));
      this.NotifyChanged(XXX.SSS(-539434959));
      this.NotifyChanged(XXX.SSS(-539434991));
      this.NotifyChanged(XXX.SSS(-539434754));
      this.NotifyChanged(XXX.SSS(-539434805));
      this.NotifyChanged(XXX.SSS(-539434124));
      this.NotifyChanged(XXX.SSS(-539434152));
      this.NotifyChanged(XXX.SSS(-539433667));
      this.NotifyChanged(XXX.SSS(-539433497));
      this.NotifyChanged(XXX.SSS(-539432639));
      this.NotifyChanged(XXX.SSS(-539432591));
    }

    private void \u0023\u003DzO\u0024GBeNt4gKS5()
    {
      this.IsDark = ChartHelper.CurrChartTheme() == XXX.SSS(-539427003);
      this.\u0023\u003DziWrrroE\u003D = ThemeManager.\u0023\u003DzrILtKW7bADnV(ChartHelper.CurrChartTheme());
    }

    public bool IsDark
    {
      get => this.\u0023\u003DzfEd754vMvBRgdJ4LHw\u003D\u003D;
      private set => this.\u0023\u003DzfEd754vMvBRgdJ4LHw\u003D\u003D = value;
    }

    public Color LineColor
    {
      get
      {
        Color? lineColor = this.\u0023\u003DzpojClAU\u003D.LineColor;
        if (lineColor.HasValue)
          return lineColor.GetValueOrDefault();
        return !this.IsDark ? Colors.Blue : Colors.Orange;
      }
      set => this.\u0023\u003DzpojClAU\u003D.LineColor = new Color?(value);
    }

    public Color AreaColor
    {
      get
      {
        Color? areaColor = this.\u0023\u003DzpojClAU\u003D.AreaColor;
        if (areaColor.HasValue)
          return areaColor.GetValueOrDefault();
        return !this.IsDark ? Colors.Blue : Colors.Orange;
      }
      set => this.\u0023\u003DzpojClAU\u003D.AreaColor = new Color?(value);
    }

    public Color FontColor
    {
      get
      {
        Color? fontColor = this.\u0023\u003DzpojClAU\u003D.FontColor;
        if (fontColor.HasValue)
          return fontColor.GetValueOrDefault();
        return !this.IsDark ? Colors.Black : Colors.White;
      }
      set => this.\u0023\u003DzpojClAU\u003D.FontColor = new Color?(value);
    }

    public Color Timeframe2Color
    {
      get
      {
        return this.\u0023\u003DzpojClAU\u003D.Timeframe2Color ?? this.\u0023\u003DziWrrroE\u003D.get_BoxVolumeTimeframe2Color();
      }
      set => this.\u0023\u003DzpojClAU\u003D.Timeframe2Color = new Color?(value);
    }

    public Color Timeframe2FrameColor
    {
      get
      {
        return this.\u0023\u003DzpojClAU\u003D.Timeframe2FrameColor ?? this.\u0023\u003DziWrrroE\u003D.get_BoxVolumeTimeframe2FrameColor();
      }
      set => this.\u0023\u003DzpojClAU\u003D.Timeframe2FrameColor = new Color?(value);
    }

    public Color Timeframe3Color
    {
      get
      {
        return this.\u0023\u003DzpojClAU\u003D.Timeframe3Color ?? this.\u0023\u003DziWrrroE\u003D.get_BoxVolumeTimeframe3Color();
      }
      set => this.\u0023\u003DzpojClAU\u003D.Timeframe3Color = new Color?(value);
    }

    public Color MaxVolumeColor
    {
      get
      {
        return this.\u0023\u003DzpojClAU\u003D.MaxVolumeColor ?? this.\u0023\u003DziWrrroE\u003D.get_BoxVolumeHighVolColor();
      }
      set => this.\u0023\u003DzpojClAU\u003D.MaxVolumeColor = new Color?(value);
    }

    public Color MaxVolumeBackground
    {
      get => this.\u0023\u003DzpojClAU\u003D.MaxVolumeBackground ?? Colors.LightBlue;
      set => this.\u0023\u003DzpojClAU\u003D.MaxVolumeBackground = new Color?(value);
    }

    public Color ClusterLineColor
    {
      get
      {
        return this.\u0023\u003DzpojClAU\u003D.ClusterLineColor ?? this.\u0023\u003DziWrrroE\u003D.get_ClusterProfileLineColor();
      }
      set => this.\u0023\u003DzpojClAU\u003D.ClusterLineColor = new Color?(value);
    }

    public Color ClusterSeparatorLineColor
    {
      get
      {
        return this.\u0023\u003DzpojClAU\u003D.ClusterSeparatorLineColor ?? this.\u0023\u003DziWrrroE\u003D.get_ClusterProfileSeparatorLineColor();
      }
      set => this.\u0023\u003DzpojClAU\u003D.ClusterSeparatorLineColor = new Color?(value);
    }

    public Color ClusterTextColor
    {
      get
      {
        return this.\u0023\u003DzpojClAU\u003D.ClusterTextColor ?? this.\u0023\u003DziWrrroE\u003D.get_ClusterProfileTextColor();
      }
      set => this.\u0023\u003DzpojClAU\u003D.ClusterTextColor = new Color?(value);
    }

    public Color ClusterColor
    {
      get
      {
        return this.\u0023\u003DzpojClAU\u003D.ClusterColor ?? this.\u0023\u003DziWrrroE\u003D.get_ClusterProfileClusterColor();
      }
      set => this.\u0023\u003DzpojClAU\u003D.ClusterColor = new Color?(value);
    }

    public Color ClusterMaxColor
    {
      get
      {
        return this.\u0023\u003DzpojClAU\u003D.ClusterMaxColor ?? this.\u0023\u003DziWrrroE\u003D.get_ClusterProfileClusterMaxColor();
      }
      set => this.\u0023\u003DzpojClAU\u003D.ClusterMaxColor = new Color?(value);
    }

    public Color HorizontalVolumeColor
    {
      get => this.\u0023\u003DzpojClAU\u003D.HorizontalVolumeColor ?? Colors.Green;
      set => this.\u0023\u003DzpojClAU\u003D.HorizontalVolumeColor = new Color?(value);
    }

    public Color HorizontalVolumeFontColor
    {
      get
      {
        return this.\u0023\u003DzpojClAU\u003D.HorizontalVolumeFontColor ?? this.\u0023\u003DziWrrroE\u003D.get_BoxVolumeCellFontColor();
      }
      set => this.\u0023\u003DzpojClAU\u003D.HorizontalVolumeFontColor = new Color?(value);
    }

    public Color BuyColor
    {
      get => this.\u0023\u003DzpojClAU\u003D.BuyColor ?? Colors.Green;
      set => this.\u0023\u003DzpojClAU\u003D.BuyColor = new Color?(value);
    }

    public Color SellColor
    {
      get => this.\u0023\u003DzpojClAU\u003D.SellColor ?? Colors.Red;
      set => this.\u0023\u003DzpojClAU\u003D.SellColor = new Color?(value);
    }

    public Color UpColor
    {
      get
      {
        Color? upColor = this.\u0023\u003DzpojClAU\u003D.UpColor;
        if (upColor.HasValue)
          return upColor.GetValueOrDefault();
        return !this.IsDark ? Colors.DarkGreen : Colors.Yellow;
      }
      set => this.\u0023\u003DzpojClAU\u003D.UpColor = new Color?(value);
    }

    public Color DownColor
    {
      get
      {
        Color? downColor = this.\u0023\u003DzpojClAU\u003D.DownColor;
        if (downColor.HasValue)
          return downColor.GetValueOrDefault();
        return !this.IsDark ? Colors.Red : Colors.DeepPink;
      }
      set => this.\u0023\u003DzpojClAU\u003D.DownColor = new Color?(value);
    }
  }
}
