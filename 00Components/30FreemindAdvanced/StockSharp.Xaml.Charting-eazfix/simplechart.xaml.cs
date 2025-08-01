// Decompiled with JetBrains decompiler
// Type: -.SimpleChart
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Algo.Indicators;
using StockSharp.Charting;
using StockSharp.Xaml.Charting;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

#nullable disable
namespace StockSharp.Charting;

public sealed class SimpleChart : 
  UserControl,
  INotifyPropertyChanged,
  IChart,
  IPersistable,
  IComponentConnector,
  IChartBuilder,
  IThemeableChart,
  INotifyPropertyChangedEx
{
  
  private readonly ChartArea _chartArea;
  
  private PropertyChangedEventHandler PropertyChangedEvent;
  
  private readonly IChartArea[] _iChartAreaArray;
  
  public SciChartSurface _drawingSurface;
  
  private bool _someInternalBoolean;

  public SimpleChart()
  {
    this.InitializeComponent();
    SimpleChart.SetupScichartSurface(this._drawingSurface);
    this._chartArea = new ChartArea()
    {
      XAxisType = ChartAxisType.Numeric,
      SimpleChart = (IChart) this
    };
    this.DataContext = (object) this.\u0023\u003DzigsRD8\u0024hw_SZ().ViewModel;
    this._iChartAreaArray = new IChartArea[1]
    {
      (IChartArea) this.\u0023\u003DzigsRD8\u0024hw_SZ()
    };
  }

  public ChartArea \u0023\u003DzigsRD8\u0024hw_SZ()
  {
    return this._chartArea;
  }

  void INotifyPropertyChanged.\u0023\u003DzEv9LVlkJvUjy\u0024TeZl6vNKqTVYPst(
    PropertyChangedEventHandler _param1)
  {
    this.PropertyChangedEvent += _param1;
  }

  void INotifyPropertyChanged.PropertyChanged(
    PropertyChangedEventHandler _param1)
  {
    this.PropertyChangedEvent -= _param1;
  }

  void INotifyPropertyChangedEx.\u0023\u003DzU3me7F908hm9DJ_XUjSH2F42LnoOQEKRZA\u003D\u003D(
    string _param1)
  {
    PropertyChangedEventHandler ziApqnpw = this.PropertyChangedEvent;
    if (ziApqnpw == null)
      return;
    DelegateHelper.Invoke(ziApqnpw, (object) this, _param1);
  }

  public IChartDrawData CreateData() => (IChartDrawData) new ChartDrawData();

  IChartArea IChartBuilder.\u0023\u003Dz4yHFX3JAJsm9BPq4w3u8GIHygsg\u0024tWn2Mw\u003D\u003D()
  {
    return (IChartArea) new ChartArea();
  }

  IChartAxis IChartBuilder.\u0023\u003DzxcicpF2tMw4DL_KJ07IK\u0024LKQGKCAfpbeMQ\u003D\u003D()
  {
    return (IChartAxis) new ChartAxis();
  }

  IChartCandleElement IChartBuilder.\u0023\u003DzYoYHumUnjlVgjBX2zf7ToXqAZUA5QCxC4GAJAcq7Rloq()
  {
    return (IChartCandleElement) new ChartCandleElement();
  }

  IChartIndicatorElement IChartBuilder.\u0023\u003Dz8gQZ90Hb\u0024GzupnClXKB4vITU41cM4uy8obJTkf4\u003D()
  {
    return (IChartIndicatorElement) new ChartIndicatorElement();
  }

  IChartActiveOrdersElement IChartBuilder.\u0023\u003DziF4l0Hs7eMhZS9EVq3OjBP1kmARyIJLcm1Tmk9tmKm78()
  {
    return (IChartActiveOrdersElement) new ChartActiveOrdersElement();
  }

  IChartAnnotationElement IChartBuilder.\u0023\u003Dzfv3ZzZhRMeYp94XgaQhLbNGQg3murI3ZPw\u003D\u003D()
  {
    return (IChartAnnotationElement) new ChartAnnotation();
  }

  IChartBandElement IChartBuilder.\u0023\u003DzIbSt8PyQFiIIo8atmglmqCnw9xXu9aTa\u00240wA9TM\u003D()
  {
    return (IChartBandElement) new ChartBandElement();
  }

  IChartLineElement IChartBuilder.\u0023\u003DzHDFPVa9LbPGTYc1s490ovEplkRlm2VZsZA\u003D\u003D()
  {
    return (IChartLineElement) new ChartLineElement();
  }

  IChartLineElement IChartBuilder.\u0023\u003Dzu1zxTVWmOO4d19mUGQfnjk5DjBWdkU8PuAD2OD0\u003D()
  {
    return (IChartLineElement) new ChartBubbleElement();
  }

  IChartOrderElement IChartBuilder.\u0023\u003DzJ3cLm4EvTpiQhKZNnV2msJjRfJrYtcRxn50oyFs\u003D()
  {
    return (IChartOrderElement) new ChartOrderElement();
  }

  IChartTradeElement IChartBuilder.\u0023\u003Dz5e9jiAiELhW72AuY5KaxUu1gqdh6qI9N6bUSqz0\u003D()
  {
    return (IChartTradeElement) new ChartTradeElement();
  }

  public void Draw(IChartDrawData _param1)
  {
    this.\u0023\u003DzigsRD8\u0024hw_SZ().ViewModel.Draw((ChartDrawData) _param1);
  }

  public void Reset(IEnumerable<IChartElement> _param1)
  {
    this.\u0023\u003DzigsRD8\u0024hw_SZ().ViewModel.Reset(_param1);
  }

  IList<IndicatorType> IChart.\u0023\u003DzsYGC0kqvnZauoBk\u0024NilgO_TQ8VINJ8nwedZpIJc\u003D()
  {
    return (IList<IndicatorType>) null;
  }

  bool IChart.\u0023\u003Dz94Zl8NPKaP0QMDSUK4EjiXxSy_Vszw63eHkNpXgbzeRoRke8RA_HC3k\u003D() => false;

  void IChart.\u0023\u003DzQRsC5Z9DmUEpDlxEijPY_L7\u0024VlaWKfCmrXKYqCcuXM_QadGUPOVIgP4\u003D(
    bool _param1)
  {
    throw new NotSupportedException();
  }

  bool IChart.\u0023\u003DzMfWBifyvxwsTSiJ_V2OucXgYx9wkgxtR9aXCvP4\u003D() => false;

  void IChart.\u0023\u003DzA3qBTKnD4s5HotUnpb2inhZurISqsYGE9qFLVAg\u003D(bool _param1)
  {
    throw new NotSupportedException();
  }

  bool IChart.\u0023\u003DzHLRZi\u0024WD1kIM0OXeYiLGgeLv2f\u0024mU_RRcy9CDhc\u003D() => false;

  void IChart.\u0023\u003DzMdiTb7SZMRysyeTUlyHM_zVbLrC2fvJcdedrxfA\u003D(bool _param1)
  {
    throw new NotSupportedException();
  }

  bool IChart.\u0023\u003Dza_NWqZdzWFvd2Nz2Tn282UjN83JVBpOKWkEXZxI\u003D() => false;

  void IChart.\u0023\u003DzqnZm6ALxeaLeh72w3GYmEb3bsropmFYkNxoinV8\u003D(bool _param1)
  {
    throw new NotSupportedException();
  }

  bool IChart.\u0023\u003Dzp6kQZ2CP9bOMp6XR0uyEgdH1HN\u0024mHbdaa\u0024C\u00245X8\u003D() => false;

  void IChart.\u0023\u003Dze0VmSWO\u0024pQ\u0024IhYV3sB2B4\u0024cUsWN132R5a2E1pIA\u003D(bool _param1)
  {
    throw new NotSupportedException();
  }

  bool IChart.\u0023\u003DztVNLi5Q7R2SQDNrxE6dVqOSWWKLLo3w1Zukh6Jw\u003D() => false;

  void IChart.\u0023\u003DzTfkbtyOkK\u0024ENBGMqXQ833vyaLthJQ4gQgNX9pSc\u003D(bool _param1)
  {
    throw new NotSupportedException();
  }

  bool IChart.\u0023\u003DzAoMUchAfYYpzi8AVpxyI4dvkNIj8\u0024DLXrVTX5NbthWUD() => false;

  void IChart.\u0023\u003DzwHP2e\u00246LOzVrH_yavPhYPVlbm6fseU0QDA9XS0PBl9um(bool _param1)
  {
    throw new NotSupportedException();
  }

  bool IChart.\u0023\u003Dzyil0IyleMuesZyWJiN5GUOPD204jJXjj6FqdQgs\u003D() => false;

  void IChart.\u0023\u003Dzi2\u0024hDR3nUyrYBKoVWRsx0ORFY\u00240USEHhexxuYKQ\u003D(bool _param1)
  {
    throw new NotSupportedException();
  }

  bool IChart.\u0023\u003DzXWAZFbxkM6UEgP2XE63OLkLHSq5x0fKt7Y1YLi5iksuW() => false;

  void IChart.\u0023\u003DzIki3ih7depQljq72Td_K3EUrjBrpebMtuf2E_jl49UxT(bool _param1)
  {
    throw new NotSupportedException();
  }

  bool IChart.\u0023\u003Dz0Xa6ZdIa5OQoJ29mZqnlXlcgZkH7ydjBin200iE8vZI7() => false;

  void IChart.\u0023\u003Dz70Pu5itMlF2fYjLz2TgzI6w7nUzViOaVKSuDckUIvaQo(bool _param1)
  {
    throw new NotSupportedException();
  }

  bool IChart.\u0023\u003DzvWkYsUwx8HePoQPrDRvGCQcFXIbKbUtZZeyp5lw\u003D() => false;

  void IChart.\u0023\u003Dzt\u0024RT9qGMHQGb5Fq0FuWuH1VNFIhh\u00242KCxark_lE\u003D(bool _param1)
  {
    throw new NotSupportedException();
  }

  TimeZoneInfo IChart.\u0023\u003DzUVp8\u0024aa9THlHHjIsyhL3stz6nb5aV6V\u0024hw\u003D\u003D()
  {
    return TimeZoneInfo.Local;
  }

  void IChart.\u0023\u003Dzr0oUfp0aPeK9tfBYvcR1qwtcqt9xYVSPLQ\u003D\u003D(TimeZoneInfo _param1)
  {
    throw new NotSupportedException();
  }

  string IThemeableChart.\u0023\u003Dz6jeuV4M9k7fwa6pr9Q\u0024z6OwLX\u0024\u0024HU32A8PUXns6KUwjeynOZXA\u003D\u003D()
  {
    return (string) null;
  }

  void IThemeableChart.\u0023\u003Dzck77nOUo8KLRSmZQfrvq4KPX0BPvJ4nfpEiQK1DwT_Ud5i0ONg\u003D\u003D(
    string _param1)
  {
    throw new NotSupportedException();
  }

  IEnumerable<IChartArea> IChart.\u0023\u003Dz\u0024SuXAUM\u00247U3SPRUgeWHrabmhrgCuIZf2_A\u003D\u003D()
  {
    return (IEnumerable<IChartArea>) this._iChartAreaArray;
  }

  void IChart.\u0023\u003DzX8fadRjkEu91Ug0fAVlnGKpI4nayEoQ7Ujl1uIA\u003D(Action<IChartArea> _param1)
  {
  }

  void IChart.\u0023\u003DzqnZm6ALxeaLeh72w3GYmERiJ\u0024z2Qh1LtU2sqX1w\u003D(
    Action<IChartArea> _param1)
  {
  }

  void IChart.\u0023\u003DzEIleL0SDY6KIID2ZeiMzwE\u0024e_3iFEBkL\u0024QMKEWc\u003D(
    Action<IChartArea> _param1)
  {
  }

  void IChart.\u0023\u003Dz9zRwiuNt2UUJvmiTqC5SQh6d1l2FoWZav5rjmDM\u003D(Action<IChartArea> _param1)
  {
  }

  void IChart.\u0023\u003Dz7M49swl6bHWcd6f_bvvyABeLjDkoz_sHHA\u003D\u003D(IChartArea _param1)
  {
    throw new NotSupportedException();
  }

  void IChart.\u0023\u003DzV0Lnuq5ri78uiL61Nc4Wa1Hk3SeSXkyxkQ\u003D\u003D(IChartArea _param1)
  {
    throw new NotSupportedException();
  }

  void IChart.\u0023\u003DzXkzhcE5OV7i5Dkkhg0FXOAw3MH5Un\u0024QDLg\u003D\u003D(
    IChartArea _param1,
    IChartElement _param2)
  {
    throw new NotSupportedException();
  }

  void IChart.\u0023\u003DzAlbSc4kYxfr6C\u0024qGte3HybbQ506KTL7HTw\u003D\u003D(
    IChartArea _param1,
    IChartElement _param2)
  {
    throw new NotSupportedException();
  }

  void IPersistable.\u0023\u003DzItXqQE1ZgvMrPpx5KDy0sy0\u003D(SettingsStorage _param1)
  {
    throw new NotSupportedException();
  }

  void IPersistable.\u0023\u003DzzmKgquG4SUyltuFrUAVXrAM\u003D(SettingsStorage _param1)
  {
    throw new NotSupportedException();
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  public void InitializeComponent()
  {
    if (this._someInternalBoolean)
      return;
    this._someInternalBoolean = true;
    Application.LoadComponent((object) this, new Uri("/StockSharp.Xaml.Charting;V5.0.0;component/simplechart.xaml", UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  public Delegate \u0023\u003DzciIj4U627yBM(Type _param1, string _param2)
  {
    return Delegate.CreateDelegate(_param1, (object) this, _param2);
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.\u0023\u003DzuNHLeGEnMjz9FDFZ6wymuXfyw_Iz(int _param1, object _param2)
  {
    if (_param1 == 1)
      this._drawingSurface = (SciChartSurface) _param2;
    else
      this._someInternalBoolean = true;
  }
}
