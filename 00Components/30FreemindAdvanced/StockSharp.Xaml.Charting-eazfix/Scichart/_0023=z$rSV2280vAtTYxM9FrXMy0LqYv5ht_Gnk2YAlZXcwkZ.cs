// Decompiled with JetBrains decompiler
// Type: #=z$rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors.Helpers;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Xaml;
using StockSharp.Algo.Indicators;
using StockSharp.BusinessEntities;
using StockSharp.Charting;
using StockSharp.Xaml.Charting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;

#nullable enable
internal sealed class \u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6 : 
  DependencyObject
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private 
  #nullable disable
  Action \u0023\u003DzJ36T8eJDWvlL;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Action<ChartArea> \u0023\u003DzrkKCMCLg7L5ZcVefKg\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Action<ChartArea> \u0023\u003DzJ_BmQqM\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Action<ChartArea> \u0023\u003DzfRlPeXe\u0024cYQx;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Action<ChartArea> \u0023\u003DzVRgWIb_qqFUL;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Action<IChartElement> \u0023\u003DzeBeQVx4\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Action<IChartElement, Subscription> \u0023\u003DziZu6zyxyyj5lOwEOlg\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Action<Order> \u0023\u003DzmMdfCUCSnZWZ;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Action<ChartArea> \u0023\u003DzJlQa5yc\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzeopV6tZDn4Ln = DependencyProperty.Register(nameof (SelectedTheme), typeof (string), typeof (\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003Dzw5K\u0024vsW8O9P3 = DependencyProperty.Register(nameof (ShowOverview), typeof (bool), typeof (\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6), new PropertyMetadata((object) false));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzvfWZFdhb7kc9 = DependencyProperty.Register(nameof (ShowLegend), typeof (bool), typeof (\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private static Action \u0023\u003Dz7i2HTHIzKc6jG0gptPUO01M\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzJmdljkCrZ79uM4t8tIUE9U8\u003D = DependencyProperty.Register(nameof (IsInteracted), typeof (bool), typeof (\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6), new PropertyMetadata((object) true, new PropertyChangedCallback(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzygR3f9KFDxTEYisMidH\u0024JS0\u003D)));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzWEDxQqB6jgU9 = DependencyProperty.Register(nameof (AllowAddArea), typeof (bool), typeof (\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6), new PropertyMetadata((object) true, new PropertyChangedCallback(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzyX0jaP1fZfwe), new CoerceValueCallback(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzgQVt7i8Rpksb)));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzX7Gyz7DEzjur = DependencyProperty.Register(nameof (AllowAddAxis), typeof (bool), typeof (\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6), new PropertyMetadata((object) true, new PropertyChangedCallback(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzyX0jaP1fZfwe), new CoerceValueCallback(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzgQVt7i8Rpksb)));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzrMO\u0024AhOl\u0024DHX0XylOA\u003D\u003D = DependencyProperty.Register(nameof (AllowAddCandles), typeof (bool), typeof (\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6), new PropertyMetadata((object) true, new PropertyChangedCallback(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzyX0jaP1fZfwe), new CoerceValueCallback(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzgQVt7i8Rpksb)));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzhEqKjKrXGg88Hv31FXG8nK0\u003D = DependencyProperty.Register(nameof (AllowAddIndicators), typeof (bool), typeof (\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6), new PropertyMetadata((object) true, new PropertyChangedCallback(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzyX0jaP1fZfwe), new CoerceValueCallback(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzgQVt7i8Rpksb)));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003Dz519VfrZuK1ns4onzOw\u003D\u003D = DependencyProperty.Register(nameof (AllowAddOrders), typeof (bool), typeof (\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6), new PropertyMetadata((object) true, new PropertyChangedCallback(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzyX0jaP1fZfwe), new CoerceValueCallback(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzgQVt7i8Rpksb)));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DziBc9dbkgK8SVC\u00247hcrr7JBg\u003D = DependencyProperty.Register(nameof (AllowAddOwnTrades), typeof (bool), typeof (\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6), new PropertyMetadata((object) true, new PropertyChangedCallback(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzyX0jaP1fZfwe), new CoerceValueCallback(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzgQVt7i8Rpksb)));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003Dz76r5\u0024wlS8ZBb = DependencyProperty.Register(nameof (MinimumRange), typeof (int), typeof (\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6), new PropertyMetadata(new PropertyChangedCallback(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dz4t7qXCEHgeaFBnsnsEfGvGsGPrcn)));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private int \u0023\u003Dzw_LOBOuAUrZC;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzvVSpwQoXV\u0024G9i4cjOQ\u003D\u003D = DependencyProperty.Register(nameof (ChartPaneViewModels), typeof (ObservableCollection<ScichartSurfaceMVVM>), typeof (\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly ICommand \u0023\u003DzlI5Q0fMkGWsRwzw1OG_ENm_rPNsf;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003Dz8XahNhP3PI7vvpsncA\u003D\u003D = DependencyProperty.Register(nameof (ShowHiddenAxesCommand), typeof (ICommand), typeof (\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzLNiqY6UHLoB\u0024 = DependencyProperty.Register(nameof (AddAreaCommand), typeof (ICommand), typeof (\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003Dzt5tifD5SNuaQJ9EkVA\u003D\u003D = DependencyProperty.Register(nameof (AddCandlesCommand), typeof (ICommand), typeof (\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzRkLhT7Lt41PT = DependencyProperty.Register(nameof (AddIndicatorCommand), typeof (ICommand), typeof (\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzNIlMuVbIfLDOZB27IQ\u003D\u003D = DependencyProperty.Register(nameof (AddOrdersCommand), typeof (ICommand), typeof (\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzoOjeK6Nq3nq\u0024VNhK5A\u003D\u003D = DependencyProperty.Register(nameof (AddTradesCommand), typeof (ICommand), typeof (\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzUWQ5iiwrKJD_ = DependencyProperty.Register(nameof (UngroupCommand), typeof (ICommand), typeof (\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003Dze4EVknsZpYUH = DependencyProperty.Register(nameof (AddXAxisCommand), typeof (ICommand), typeof (\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzKaPAMV2cOzbF = DependencyProperty.Register(nameof (AddYAxisCommand), typeof (ICommand), typeof (\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003Dz1DarHjvaoDtx = DependencyProperty.Register(nameof (RemoveAxisCommand), typeof (ICommand), typeof (\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003Dzfv12AVpZqluc = DependencyProperty.Register(nameof (IndicatorTypes), typeof (ObservableCollection<IndicatorType>), typeof (\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly DelegateCommand<IScichartSurfaceVM> _closePaneCommand;

  public \u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6()
  {
    this.ChartPaneViewModels = new ObservableCollection<ScichartSurfaceMVVM>();
    this.MinimumRange = 50;
    this.ShowOverview = false;
    this.ShowLegend = true;
    this.IndicatorTypes = new ObservableCollection<IndicatorType>();
    this.AddAreaCommand = (ICommand) new DelegateCommand(new Action<object>(this.\u0023\u003DzLk5v0G8ASW9AV\u0024TMw8K1MAc\u003D), new Func<object, bool>(this.\u0023\u003DzB48yQj1IRDJjkJC1wq8SIIA\u003D));
    this.AddCandlesCommand = (ICommand) new DelegateCommand<ChartArea>(new Action<ChartArea>(this.\u0023\u003DzEzhwUJupC_i96vtElR_c1Lk\u003D), new Func<ChartArea, bool>(this.\u0023\u003Dz900Ul5GeYSVzq0mco4W\u0024wVw\u003D));
    this.AddIndicatorCommand = (ICommand) new DelegateCommand<ChartArea>(new Action<ChartArea>(this.\u0023\u003Dz0n29M_1BxodssYxUpRLzwMs\u003D), new Func<ChartArea, bool>(this.\u0023\u003DzSZmYg75p4YkZfzNPjgt1zes\u003D));
    this.AddOrdersCommand = (ICommand) new DelegateCommand<ChartArea>(new Action<ChartArea>(this.\u0023\u003DzTUCctLMj1IW_YlPWx2npCAQ\u003D), new Func<ChartArea, bool>(this.\u0023\u003Dzq0leWu_wI9CtdDKdOSX6YU0\u003D));
    this.AddTradesCommand = (ICommand) new DelegateCommand<ChartArea>(new Action<ChartArea>(this.\u0023\u003DzzNPHlrzvITA\u0024S6vCm0UpaBg\u003D), new Func<ChartArea, bool>(this.\u0023\u003Dzyr4afQY\u0024tjivCCmKtAW1HpI\u003D));
    this.UngroupCommand = (ICommand) new DelegateCommand<ChartArea>(new Action<ChartArea>(this.\u0023\u003Dz7CMbEUPDck9jB8eBt3cv1t0\u003D), new Func<ChartArea, bool>(this.\u0023\u003DzcXM77DgzL8bzrqeKG97usV4\u003D));
    this.ShowHiddenAxesCommand = (ICommand) new DelegateCommand<ChartArea>(new Action<ChartArea>(this.\u0023\u003DzAhcQ9lSmwA8jEy1mdYi6ePg\u003D), new Func<ChartArea, bool>(this.\u0023\u003DzjUJWvCc8BSpnpUc8DQ5qbx8\u003D));
    this.AddXAxisCommand = (ICommand) new DelegateCommand<ChartArea>(new Action<ChartArea>(this.\u0023\u003DzEzQRvC54NAkZrd_z3oUpUq8\u003D), new Func<ChartArea, bool>(this.\u0023\u003Dzu7XzqRESw\u0024Cj0yMZyfdgHlQ\u003D));
    this.AddYAxisCommand = (ICommand) new DelegateCommand<ChartArea>(new Action<ChartArea>(this.\u0023\u003Dz8avonJRP2fM8CXBcKAguZEc\u003D), new Func<ChartArea, bool>(this.\u0023\u003DzENqocWgP_pJukYrbe4XWVHg\u003D));
    this.RemoveAxisCommand = (ICommand) new DelegateCommand<ChartAxis>(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzDiOIqlf15Sqxg5EJYQ\u003D\u003D ?? (\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzDiOIqlf15Sqxg5EJYQ\u003D\u003D = new Action<ChartAxis>(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzziKq1fCxTiR\u0024IxwE1_to_NU\u003D)), new Func<ChartAxis, bool>(this.\u0023\u003DzQPhCaCOw2SD2t\u0024dJA7\u00247lOE\u003D));
    this.\u0023\u003DzoMQQ88MEiBDX();
    this._closePaneCommand = new DelegateCommand<IScichartSurfaceVM>(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzXj9SS49o1J\u0024OEf0eTw\u003D\u003D ?? (\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzXj9SS49o1J\u0024OEf0eTw\u003D\u003D = new Action<IScichartSurfaceVM>(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dz\u0024yGfI4nRC88_Fx431vwDLXg\u003D)), new Func<IScichartSurfaceVM, bool>(this.\u0023\u003Dz7Vq8a2RMrjOa5jLFAfNRwwA\u003D));
    this.\u0023\u003DzlI5Q0fMkGWsRwzw1OG_ENm_rPNsf = (ICommand) new DelegateCommand<ChartArea>(new Action<ChartArea>(this.\u0023\u003DzWll\u0024XnUAcUtY0YKI0GtTMhA\u003D), new Func<ChartArea, bool>(this.\u0023\u003Dz0lCLFNOjdrmsWu0\u0024ZdVnP0c\u003D));
    if (this.IsDesignMode())
      return;
    this.\u0023\u003Dz5RYhL5E\u003D();
    ThemeManager.ApplicationThemeChanged += new ThemeChangedRoutedEventHandler(this.\u0023\u003Dz_JOkHnKu927R4qx\u0024N\u0024sTKxE\u003D);
  }

  public void \u0023\u003Dz1plbqKAfOGgT(Action _param1)
  {
    Action action = this.\u0023\u003DzJ36T8eJDWvlL;
    Action comparand;
    do
    {
      comparand = action;
      action = Interlocked.CompareExchange<Action>(ref this.\u0023\u003DzJ36T8eJDWvlL, comparand + _param1, comparand);
    }
    while (action != comparand);
  }

  public void \u0023\u003DzBJGq\u0024rZnxcA9(Action _param1)
  {
    Action action = this.\u0023\u003DzJ36T8eJDWvlL;
    Action comparand;
    do
    {
      comparand = action;
      action = Interlocked.CompareExchange<Action>(ref this.\u0023\u003DzJ36T8eJDWvlL, comparand - _param1, comparand);
    }
    while (action != comparand);
  }

  public void \u0023\u003DzVpi0shdst7Eq3GZ4Qg\u003D\u003D(Action<ChartArea> _param1)
  {
    Action<ChartArea> action = this.\u0023\u003DzrkKCMCLg7L5ZcVefKg\u003D\u003D;
    Action<ChartArea> comparand;
    do
    {
      comparand = action;
      action = Interlocked.CompareExchange<Action<ChartArea>>(ref this.\u0023\u003DzrkKCMCLg7L5ZcVefKg\u003D\u003D, comparand + _param1, comparand);
    }
    while (action != comparand);
  }

  public void \u0023\u003DzPasrMn3CLYCfaNBSBw\u003D\u003D(Action<ChartArea> _param1)
  {
    Action<ChartArea> action = this.\u0023\u003DzrkKCMCLg7L5ZcVefKg\u003D\u003D;
    Action<ChartArea> comparand;
    do
    {
      comparand = action;
      action = Interlocked.CompareExchange<Action<ChartArea>>(ref this.\u0023\u003DzrkKCMCLg7L5ZcVefKg\u003D\u003D, comparand - _param1, comparand);
    }
    while (action != comparand);
  }

  public void \u0023\u003DzhyKCJLidU6\u0024W(Action<ChartArea> _param1)
  {
    Action<ChartArea> action = this.\u0023\u003DzJ_BmQqM\u003D;
    Action<ChartArea> comparand;
    do
    {
      comparand = action;
      action = Interlocked.CompareExchange<Action<ChartArea>>(ref this.\u0023\u003DzJ_BmQqM\u003D, comparand + _param1, comparand);
    }
    while (action != comparand);
  }

  public void \u0023\u003DzXebVtC3nhhOK(Action<ChartArea> _param1)
  {
    Action<ChartArea> action = this.\u0023\u003DzJ_BmQqM\u003D;
    Action<ChartArea> comparand;
    do
    {
      comparand = action;
      action = Interlocked.CompareExchange<Action<ChartArea>>(ref this.\u0023\u003DzJ_BmQqM\u003D, comparand - _param1, comparand);
    }
    while (action != comparand);
  }

  public void \u0023\u003DzXNNsPD0jD0Pq1irRnA\u003D\u003D(Action<ChartArea> _param1)
  {
    Action<ChartArea> action = this.\u0023\u003DzfRlPeXe\u0024cYQx;
    Action<ChartArea> comparand;
    do
    {
      comparand = action;
      action = Interlocked.CompareExchange<Action<ChartArea>>(ref this.\u0023\u003DzfRlPeXe\u0024cYQx, comparand + _param1, comparand);
    }
    while (action != comparand);
  }

  public void \u0023\u003DzDVmEWtkWkTPwviOp6w\u003D\u003D(Action<ChartArea> _param1)
  {
    Action<ChartArea> action = this.\u0023\u003DzfRlPeXe\u0024cYQx;
    Action<ChartArea> comparand;
    do
    {
      comparand = action;
      action = Interlocked.CompareExchange<Action<ChartArea>>(ref this.\u0023\u003DzfRlPeXe\u0024cYQx, comparand - _param1, comparand);
    }
    while (action != comparand);
  }

  public void \u0023\u003Dzs2NcPz2K5a8svDuiBw\u003D\u003D(Action<ChartArea> _param1)
  {
    Action<ChartArea> action = this.\u0023\u003DzVRgWIb_qqFUL;
    Action<ChartArea> comparand;
    do
    {
      comparand = action;
      action = Interlocked.CompareExchange<Action<ChartArea>>(ref this.\u0023\u003DzVRgWIb_qqFUL, comparand + _param1, comparand);
    }
    while (action != comparand);
  }

  public void \u0023\u003DzjUn\u0024ppBdqy2Pyk\u002445A\u003D\u003D(Action<ChartArea> _param1)
  {
    Action<ChartArea> action = this.\u0023\u003DzVRgWIb_qqFUL;
    Action<ChartArea> comparand;
    do
    {
      comparand = action;
      action = Interlocked.CompareExchange<Action<ChartArea>>(ref this.\u0023\u003DzVRgWIb_qqFUL, comparand - _param1, comparand);
    }
    while (action != comparand);
  }

  public void \u0023\u003DzPvEital2M7gh(Action<IChartElement> _param1)
  {
    Action<IChartElement> action = this.\u0023\u003DzeBeQVx4\u003D;
    Action<IChartElement> comparand;
    do
    {
      comparand = action;
      action = Interlocked.CompareExchange<Action<IChartElement>>(ref this.\u0023\u003DzeBeQVx4\u003D, comparand + _param1, comparand);
    }
    while (action != comparand);
  }

  public void \u0023\u003Dzfj2KEivrD_Sr(Action<IChartElement> _param1)
  {
    Action<IChartElement> action = this.\u0023\u003DzeBeQVx4\u003D;
    Action<IChartElement> comparand;
    do
    {
      comparand = action;
      action = Interlocked.CompareExchange<Action<IChartElement>>(ref this.\u0023\u003DzeBeQVx4\u003D, comparand - _param1, comparand);
    }
    while (action != comparand);
  }

  public void \u0023\u003DzVZkENKDodIoaG_WJsg\u003D\u003D(
    Action<IChartElement, Subscription> _param1)
  {
    Action<IChartElement, Subscription> action = this.\u0023\u003DziZu6zyxyyj5lOwEOlg\u003D\u003D;
    Action<IChartElement, Subscription> comparand;
    do
    {
      comparand = action;
      action = Interlocked.CompareExchange<Action<IChartElement, Subscription>>(ref this.\u0023\u003DziZu6zyxyyj5lOwEOlg\u003D\u003D, comparand + _param1, comparand);
    }
    while (action != comparand);
  }

  public void \u0023\u003Dztqxgxb_i07gVijmTJw\u003D\u003D(
    Action<IChartElement, Subscription> _param1)
  {
    Action<IChartElement, Subscription> action = this.\u0023\u003DziZu6zyxyyj5lOwEOlg\u003D\u003D;
    Action<IChartElement, Subscription> comparand;
    do
    {
      comparand = action;
      action = Interlocked.CompareExchange<Action<IChartElement, Subscription>>(ref this.\u0023\u003DziZu6zyxyyj5lOwEOlg\u003D\u003D, comparand - _param1, comparand);
    }
    while (action != comparand);
  }

  public void \u0023\u003DzgNd4ReliYq4x(Action<Order> _param1)
  {
    Action<Order> action = this.\u0023\u003DzmMdfCUCSnZWZ;
    Action<Order> comparand;
    do
    {
      comparand = action;
      action = Interlocked.CompareExchange<Action<Order>>(ref this.\u0023\u003DzmMdfCUCSnZWZ, comparand + _param1, comparand);
    }
    while (action != comparand);
  }

  public void \u0023\u003DzIgosDaflfD4r(Action<Order> _param1)
  {
    Action<Order> action = this.\u0023\u003DzmMdfCUCSnZWZ;
    Action<Order> comparand;
    do
    {
      comparand = action;
      action = Interlocked.CompareExchange<Action<Order>>(ref this.\u0023\u003DzmMdfCUCSnZWZ, comparand - _param1, comparand);
    }
    while (action != comparand);
  }

  public void \u0023\u003DzqMcw8k8QHzu3(Action<ChartArea> _param1)
  {
    Action<ChartArea> action = this.\u0023\u003DzJlQa5yc\u003D;
    Action<ChartArea> comparand;
    do
    {
      comparand = action;
      action = Interlocked.CompareExchange<Action<ChartArea>>(ref this.\u0023\u003DzJlQa5yc\u003D, comparand + _param1, comparand);
    }
    while (action != comparand);
  }

  public void \u0023\u003DzMkKdMhc\u0024OzYN(Action<ChartArea> _param1)
  {
    Action<ChartArea> action = this.\u0023\u003DzJlQa5yc\u003D;
    Action<ChartArea> comparand;
    do
    {
      comparand = action;
      action = Interlocked.CompareExchange<Action<ChartArea>>(ref this.\u0023\u003DzJlQa5yc\u003D, comparand - _param1, comparand);
    }
    while (action != comparand);
  }

  public void \u0023\u003Dz\u0024DK5seweHzSZIyjEhw\u003D\u003D(Func<Order, bool> _param1)
  {
    \u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzymgCCsreH3nwJBFwtEPKXQw\u003D ccsreH3nwJbFwtEpkxQw = new \u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzymgCCsreH3nwJBFwtEPKXQw\u003D();
    ccsreH3nwJbFwtEpkxQw.\u0023\u003DzXCEqv64\u003D = this.\u0023\u003DzmMdfCUCSnZWZ;
    if (_param1 == null)
      _param1 = \u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzwCieN8nlFS3aCeRPgg\u003D\u003D ?? (\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzwCieN8nlFS3aCeRPgg\u003D\u003D = new Func<Order, bool>(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzO_BIxNwmDn6VDgqOIx_JE6RwPKwP));
    CollectionHelper.ForEach<Order>((IEnumerable<Order>) CollectionHelper.ToSet<Order>(this.ChartPaneViewModels.SelectMany<ScichartSurfaceMVVM, Order>(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzKeRpc1y1\u0024C\u0024IWjYsyQ\u003D\u003D ?? (\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzKeRpc1y1\u0024C\u0024IWjYsyQ\u003D\u003D = new Func<ScichartSurfaceMVVM, IEnumerable<Order>>(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzrhkYnMPbNPr3HVPmAT1zSUTwkFvh))).Where<Order>(_param1)), new Action<Order>(ccsreH3nwJbFwtEpkxQw.\u0023\u003Dz69cIxDaENs3AtcYRgfOovNI\u003D));
  }

  private void \u0023\u003Dz5RYhL5E\u003D() => this.SelectedTheme = ChartHelper.CurrChartTheme();

  public string SelectedTheme
  {
    get
    {
      return (string) this.GetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzeopV6tZDn4Ln);
    }
    set
    {
      this.SetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzeopV6tZDn4Ln, (object) value);
    }
  }

  public bool ShowOverview
  {
    get
    {
      return (bool) this.GetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003Dzw5K\u0024vsW8O9P3);
    }
    set
    {
      this.SetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003Dzw5K\u0024vsW8O9P3, (object) value);
    }
  }

  public bool ShowLegend
  {
    get
    {
      return (bool) this.GetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzvfWZFdhb7kc9);
    }
    set
    {
      this.SetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzvfWZFdhb7kc9, (object) value);
    }
  }

  internal static void \u0023\u003DztwqF4KBjQLI4w4fkq\u0024UNEzaV82mj(Action _param0)
  {
    Action action1 = \u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003Dz7i2HTHIzKc6jG0gptPUO01M\u003D;
    Action comparand;
    do
    {
      comparand = action1;
      Action action2 = comparand + _param0;
      action1 = Interlocked.CompareExchange<Action>(ref \u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003Dz7i2HTHIzKc6jG0gptPUO01M\u003D, action2, comparand);
    }
    while (action1 != comparand);
  }

  internal static void \u0023\u003DzFKIjoTgcr_ZQ\u0024BFi5BCDqDF5zYZT(Action _param0)
  {
    Action action1 = \u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003Dz7i2HTHIzKc6jG0gptPUO01M\u003D;
    Action comparand;
    do
    {
      comparand = action1;
      Action action2 = comparand - _param0;
      action1 = Interlocked.CompareExchange<Action>(ref \u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003Dz7i2HTHIzKc6jG0gptPUO01M\u003D, action2, comparand);
    }
    while (action1 != comparand);
  }

  private static void \u0023\u003DzygR3f9KFDxTEYisMidH\u0024JS0\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    \u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6 zgziIlo367a8J0vVw6 = (\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6) _param0;
    zgziIlo367a8J0vVw6.CoerceValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzWEDxQqB6jgU9);
    zgziIlo367a8J0vVw6.CoerceValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzX7Gyz7DEzjur);
    zgziIlo367a8J0vVw6.CoerceValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzrMO\u0024AhOl\u0024DHX0XylOA\u003D\u003D);
    zgziIlo367a8J0vVw6.CoerceValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzhEqKjKrXGg88Hv31FXG8nK0\u003D);
    zgziIlo367a8J0vVw6.CoerceValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003Dz519VfrZuK1ns4onzOw\u003D\u003D);
    zgziIlo367a8J0vVw6.CoerceValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DziBc9dbkgK8SVC\u00247hcrr7JBg\u003D);
    ((\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6) _param0).\u0023\u003Dzq2WzUfXQkUHNY5VhTi_dHgzgYlRe();
  }

  private static void \u0023\u003DzyX0jaP1fZfwe(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    ((\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6) _param0).\u0023\u003Dzq2WzUfXQkUHNY5VhTi_dHgzgYlRe();
  }

  private void \u0023\u003Dzq2WzUfXQkUHNY5VhTi_dHgzgYlRe()
  {
    CommandManager.InvalidateRequerySuggested();
    this.ClosePaneCommand.RaiseCanExecuteChanged();
    Action izKc6jG0gptPuO01M = \u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003Dz7i2HTHIzKc6jG0gptPUO01M\u003D;
    if (izKc6jG0gptPuO01M == null)
      return;
    izKc6jG0gptPuO01M();
  }

  public bool IsInteracted
  {
    get
    {
      return (bool) this.GetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzJmdljkCrZ79uM4t8tIUE9U8\u003D);
    }
    set
    {
      this.SetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzJmdljkCrZ79uM4t8tIUE9U8\u003D, (object) value);
    }
  }

  public bool AllowAddArea
  {
    get
    {
      return (bool) this.GetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzWEDxQqB6jgU9);
    }
    set
    {
      this.SetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzWEDxQqB6jgU9, (object) value);
    }
  }

  public bool AllowAddAxis
  {
    get
    {
      return (bool) this.GetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzX7Gyz7DEzjur);
    }
    set
    {
      this.SetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzX7Gyz7DEzjur, (object) value);
    }
  }

  public bool AllowAddCandles
  {
    get
    {
      return (bool) this.GetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzrMO\u0024AhOl\u0024DHX0XylOA\u003D\u003D);
    }
    set
    {
      this.SetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzrMO\u0024AhOl\u0024DHX0XylOA\u003D\u003D, (object) value);
    }
  }

  public bool AllowAddIndicators
  {
    get
    {
      return (bool) this.GetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzhEqKjKrXGg88Hv31FXG8nK0\u003D);
    }
    set
    {
      this.SetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzhEqKjKrXGg88Hv31FXG8nK0\u003D, (object) value);
    }
  }

  public bool AllowAddOrders
  {
    get
    {
      return (bool) this.GetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003Dz519VfrZuK1ns4onzOw\u003D\u003D);
    }
    set
    {
      this.SetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003Dz519VfrZuK1ns4onzOw\u003D\u003D, (object) value);
    }
  }

  public bool AllowAddOwnTrades
  {
    get
    {
      return (bool) this.GetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DziBc9dbkgK8SVC\u00247hcrr7JBg\u003D);
    }
    set
    {
      this.SetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DziBc9dbkgK8SVC\u00247hcrr7JBg\u003D, (object) value);
    }
  }

  private static object \u0023\u003DzgQVt7i8Rpksb(DependencyObject _param0, object _param1)
  {
    return (object) (bool) (!((\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6) _param0).IsInteracted ? 0 : ((bool) _param1 ? 1 : 0));
  }

  public int MinimumRange
  {
    get => this.\u0023\u003Dzw_LOBOuAUrZC;
    set
    {
      this.SetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003Dz76r5\u0024wlS8ZBb, (object) value);
    }
  }

  public ObservableCollection<ScichartSurfaceMVVM> ChartPaneViewModels
  {
    get
    {
      return (ObservableCollection<ScichartSurfaceMVVM>) this.GetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzvVSpwQoXV\u0024G9i4cjOQ\u003D\u003D);
    }
    set
    {
      this.SetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzvVSpwQoXV\u0024G9i4cjOQ\u003D\u003D, (object) value);
    }
  }

  public ICommand \u0023\u003Dzd4glwaTNkyYeey63BQ\u003D\u003D()
  {
    return this.\u0023\u003DzlI5Q0fMkGWsRwzw1OG_ENm_rPNsf;
  }

  public ICommand ShowHiddenAxesCommand
  {
    get
    {
      return (ICommand) this.GetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003Dz8XahNhP3PI7vvpsncA\u003D\u003D);
    }
    set
    {
      this.SetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003Dz8XahNhP3PI7vvpsncA\u003D\u003D, (object) value);
    }
  }

  public ICommand AddAreaCommand
  {
    get
    {
      return (ICommand) this.GetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzLNiqY6UHLoB\u0024);
    }
    set
    {
      this.SetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzLNiqY6UHLoB\u0024, (object) value);
    }
  }

  public ICommand AddCandlesCommand
  {
    get
    {
      return (ICommand) this.GetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003Dzt5tifD5SNuaQJ9EkVA\u003D\u003D);
    }
    set
    {
      this.SetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003Dzt5tifD5SNuaQJ9EkVA\u003D\u003D, (object) value);
    }
  }

  public ICommand AddIndicatorCommand
  {
    get
    {
      return (ICommand) this.GetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzRkLhT7Lt41PT);
    }
    set
    {
      this.SetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzRkLhT7Lt41PT, (object) value);
    }
  }

  public ICommand AddOrdersCommand
  {
    get
    {
      return (ICommand) this.GetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzNIlMuVbIfLDOZB27IQ\u003D\u003D);
    }
    set
    {
      this.SetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzNIlMuVbIfLDOZB27IQ\u003D\u003D, (object) value);
    }
  }

  public ICommand AddTradesCommand
  {
    get
    {
      return (ICommand) this.GetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzoOjeK6Nq3nq\u0024VNhK5A\u003D\u003D);
    }
    set
    {
      this.SetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzoOjeK6Nq3nq\u0024VNhK5A\u003D\u003D, (object) value);
    }
  }

  public ICommand UngroupCommand
  {
    get
    {
      return (ICommand) this.GetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzUWQ5iiwrKJD_);
    }
    set
    {
      this.SetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzUWQ5iiwrKJD_, (object) value);
    }
  }

  internal void \u0023\u003Dzld7tWxZuooQ2UzOmtQ\u003D\u003D(
    IChartElement _param1,
    Subscription _param2)
  {
    Action<IChartElement, Subscription> zu6zyxyyj5lOwEolg = this.\u0023\u003DziZu6zyxyyj5lOwEOlg\u003D\u003D;
    if (zu6zyxyyj5lOwEolg == null)
      return;
    zu6zyxyyj5lOwEolg(_param1, _param2);
  }

  public ICommand AddXAxisCommand
  {
    get
    {
      return (ICommand) this.GetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003Dze4EVknsZpYUH);
    }
    set
    {
      this.SetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003Dze4EVknsZpYUH, (object) value);
    }
  }

  public ICommand AddYAxisCommand
  {
    get
    {
      return (ICommand) this.GetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzKaPAMV2cOzbF);
    }
    set
    {
      this.SetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzKaPAMV2cOzbF, (object) value);
    }
  }

  public ICommand RemoveAxisCommand
  {
    get
    {
      return (ICommand) this.GetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003Dz1DarHjvaoDtx);
    }
    set
    {
      this.SetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003Dz1DarHjvaoDtx, (object) value);
    }
  }

  public ObservableCollection<IndicatorType> IndicatorTypes
  {
    get
    {
      return (ObservableCollection<IndicatorType>) this.GetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003Dzfv12AVpZqluc);
    }
    set
    {
      this.SetValue(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003Dzfv12AVpZqluc, (object) value);
    }
  }

  public DelegateCommand<IScichartSurfaceVM> ClosePaneCommand
  {
    get => this._closePaneCommand;
  }

  public void \u0023\u003DzoMQQ88MEiBDX()
  {
    \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQGlzHFTS415EjH_wseBoYgQlG72ZKsY7iW1Jk3iR.\u0023\u003DzoMQQ88MEiBDX((object) this);
  }

  public void \u0023\u003DzzXq5ccDMuPZc(IChartElement _param1)
  {
    Action<IChartElement> zeBeQvx4 = this.\u0023\u003DzeBeQVx4\u003D;
    if (zeBeQvx4 == null)
      return;
    zeBeQvx4(_param1);
  }

  private ChartArea \u0023\u003DqksRi_8UWWjEPnGiZ9hc7zuNUQDD7yzQz6FXgIUXUoY4\u003D(ChartArea _param1)
  {
    ChartArea chartArea = _param1;
    if (chartArea != null)
      return chartArea;
    ObservableCollection<ScichartSurfaceMVVM> chartPaneViewModels = this.ChartPaneViewModels;
    if (chartPaneViewModels == null)
      return (ChartArea) null;
    return chartPaneViewModels.FirstOrDefault<ScichartSurfaceMVVM>()?.Area;
  }

  private void \u0023\u003DzLk5v0G8ASW9AV\u0024TMw8K1MAc\u003D(object _param1)
  {
    Action zJ36T8eJdWvlL = this.\u0023\u003DzJ36T8eJDWvlL;
    if (zJ36T8eJdWvlL == null)
      return;
    zJ36T8eJdWvlL();
  }

  private bool \u0023\u003DzB48yQj1IRDJjkJC1wq8SIIA\u003D(object _param1) => this.AllowAddArea;

  private void \u0023\u003DzEzhwUJupC_i96vtElR_c1Lk\u003D(ChartArea _param1)
  {
    Action<ChartArea> kcmcLg7L5ZcVefKg = this.\u0023\u003DzrkKCMCLg7L5ZcVefKg\u003D\u003D;
    if (kcmcLg7L5ZcVefKg == null)
      return;
    kcmcLg7L5ZcVefKg(this.\u0023\u003DqksRi_8UWWjEPnGiZ9hc7zuNUQDD7yzQz6FXgIUXUoY4\u003D(_param1));
  }

  private bool \u0023\u003Dz900Ul5GeYSVzq0mco4W\u0024wVw\u003D(ChartArea _param1)
  {
    return this.AllowAddCandles && this.\u0023\u003DqksRi_8UWWjEPnGiZ9hc7zuNUQDD7yzQz6FXgIUXUoY4\u003D(_param1) != null;
  }

  private void \u0023\u003Dz0n29M_1BxodssYxUpRLzwMs\u003D(ChartArea _param1)
  {
    Action<ChartArea> zJBmQqM = this.\u0023\u003DzJ_BmQqM\u003D;
    if (zJBmQqM == null)
      return;
    zJBmQqM(this.\u0023\u003DqksRi_8UWWjEPnGiZ9hc7zuNUQDD7yzQz6FXgIUXUoY4\u003D(_param1));
  }

  private bool \u0023\u003DzSZmYg75p4YkZfzNPjgt1zes\u003D(ChartArea _param1)
  {
    return this.AllowAddIndicators && this.\u0023\u003DqksRi_8UWWjEPnGiZ9hc7zuNUQDD7yzQz6FXgIUXUoY4\u003D(_param1) != null;
  }

  private void \u0023\u003DzTUCctLMj1IW_YlPWx2npCAQ\u003D(ChartArea _param1)
  {
    Action<ChartArea> zfRlPeXeCYqx = this.\u0023\u003DzfRlPeXe\u0024cYQx;
    if (zfRlPeXeCYqx == null)
      return;
    zfRlPeXeCYqx(this.\u0023\u003DqksRi_8UWWjEPnGiZ9hc7zuNUQDD7yzQz6FXgIUXUoY4\u003D(_param1));
  }

  private bool \u0023\u003Dzq0leWu_wI9CtdDKdOSX6YU0\u003D(ChartArea _param1)
  {
    return this.AllowAddOrders && this.\u0023\u003DqksRi_8UWWjEPnGiZ9hc7zuNUQDD7yzQz6FXgIUXUoY4\u003D(_param1) != null;
  }

  private void \u0023\u003DzzNPHlrzvITA\u0024S6vCm0UpaBg\u003D(ChartArea _param1)
  {
    Action<ChartArea> zVrgWibQqFul = this.\u0023\u003DzVRgWIb_qqFUL;
    if (zVrgWibQqFul == null)
      return;
    zVrgWibQqFul(this.\u0023\u003DqksRi_8UWWjEPnGiZ9hc7zuNUQDD7yzQz6FXgIUXUoY4\u003D(_param1));
  }

  private bool \u0023\u003Dzyr4afQY\u0024tjivCCmKtAW1HpI\u003D(ChartArea _param1)
  {
    return this.AllowAddOwnTrades && this.\u0023\u003DqksRi_8UWWjEPnGiZ9hc7zuNUQDD7yzQz6FXgIUXUoY4\u003D(_param1) != null;
  }

  private void \u0023\u003Dz7CMbEUPDck9jB8eBt3cv1t0\u003D(ChartArea _param1)
  {
    Action<ChartArea> zJlQa5yc = this.\u0023\u003DzJlQa5yc\u003D;
    if (zJlQa5yc == null)
      return;
    zJlQa5yc(this.\u0023\u003DqksRi_8UWWjEPnGiZ9hc7zuNUQDD7yzQz6FXgIUXUoY4\u003D(_param1));
  }

  private bool \u0023\u003DzcXM77DgzL8bzrqeKG97usV4\u003D(ChartArea _param1)
  {
    return this.\u0023\u003DqksRi_8UWWjEPnGiZ9hc7zuNUQDD7yzQz6FXgIUXUoY4\u003D(_param1) != null;
  }

  private void \u0023\u003DzAhcQ9lSmwA8jEy1mdYi6ePg\u003D(ChartArea _param1)
  {
    if (Equatable<ChartArea>.op_Inequality((Equatable<ChartArea>) _param1, (ChartArea) null))
      _param1.ChartSurfaceViewModel.ShowHiddenAxesCommand.TryExecute((object) null);
    else
      CollectionHelper.ForEach<ScichartSurfaceMVVM>((IEnumerable<ScichartSurfaceMVVM>) this.ChartPaneViewModels, \u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz4K9Ew\u00245ncgrgb99V4w\u003D\u003D ?? (\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz4K9Ew\u00245ncgrgb99V4w\u003D\u003D = new Action<ScichartSurfaceMVVM>(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dz0sbMZcoPgn7DQ3i\u0024I2emR\u00244\u003D)));
  }

  private bool \u0023\u003DzjUJWvCc8BSpnpUc8DQ5qbx8\u003D(ChartArea _param1) => this.IsInteracted;

  private void \u0023\u003DzEzQRvC54NAkZrd_z3oUpUq8\u003D(ChartArea _param1)
  {
    _param1 = this.\u0023\u003DqksRi_8UWWjEPnGiZ9hc7zuNUQDD7yzQz6FXgIUXUoY4\u003D(_param1);
    if (_param1 == null)
      return;
    // ISSUE: explicit non-virtual call
    ((ICollection<IChartAxis>) __nonvirtual (_param1.XAxises)).Add((IChartAxis) new ChartAxis()
    {
      AxisType = ChartAxisType.CategoryDateTime,
      TimeZone = ((IEnumerable<IChartAxis>) _param1.XAxises).LastOrDefault<IChartAxis>()?.TimeZone
    });
  }

  private bool \u0023\u003Dzu7XzqRESw\u0024Cj0yMZyfdgHlQ\u003D(ChartArea _param1)
  {
    return this.AllowAddAxis;
  }

  private void \u0023\u003Dz8avonJRP2fM8CXBcKAguZEc\u003D(ChartArea _param1)
  {
    ChartArea chartArea = this.\u0023\u003DqksRi_8UWWjEPnGiZ9hc7zuNUQDD7yzQz6FXgIUXUoY4\u003D(_param1);
    if (chartArea == null)
      return;
    // ISSUE: explicit non-virtual call
    ((ICollection<IChartAxis>) __nonvirtual (chartArea.YAxises)).Add((IChartAxis) new ChartAxis()
    {
      AxisType = ChartAxisType.Numeric
    });
  }

  private bool \u0023\u003DzENqocWgP_pJukYrbe4XWVHg\u003D(ChartArea _param1)
  {
    return this.AllowAddAxis && this.\u0023\u003DqksRi_8UWWjEPnGiZ9hc7zuNUQDD7yzQz6FXgIUXUoY4\u003D(_param1) != null;
  }

  private bool \u0023\u003DzQPhCaCOw2SD2t\u0024dJA7\u00247lOE\u003D(ChartAxis _param1)
  {
    return this.IsInteracted && _param1?.ChartArea != null && !CompareHelper.IsDefault<ChartAxis>(_param1) && this.AllowAddAxis;
  }

  private bool \u0023\u003Dz7Vq8a2RMrjOa5jLFAfNRwwA\u003D(
    IScichartSurfaceVM _param1)
  {
    return this.AllowAddArea;
  }

  private void \u0023\u003DzWll\u0024XnUAcUtY0YKI0GtTMhA\u003D(ChartArea _param1)
  {
    this.\u0023\u003Dz\u0024DK5seweHzSZIyjEhw\u003D\u003D((Func<Order, bool>) null);
  }

  private bool \u0023\u003Dz0lCLFNOjdrmsWu0\u0024ZdVnP0c\u003D(ChartArea _param1)
  {
    return this.IsInteracted;
  }

  private void \u0023\u003Dz_JOkHnKu927R4qx\u0024N\u0024sTKxE\u003D(
    DependencyObject _param1,
    ThemeChangedRoutedEventArgs _param2)
  {
    this.\u0023\u003Dz5RYhL5E\u003D();
  }

  [Serializable]
  private sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly \u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new \u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003Dz7qOdpi4\u003D();
    public static Action<ScichartSurfaceMVVM> \u0023\u003Dz4K9Ew\u00245ncgrgb99V4w\u003D\u003D;
    public static Action<ChartAxis> \u0023\u003DzDiOIqlf15Sqxg5EJYQ\u003D\u003D;
    public static Action<IScichartSurfaceVM> \u0023\u003DzXj9SS49o1J\u0024OEf0eTw\u003D\u003D;
    public static Func<Order, bool> \u0023\u003DzwCieN8nlFS3aCeRPgg\u003D\u003D;
    public static Func<Order, bool> \u0023\u003DzU9srAoETJDIIA3EbGw\u003D\u003D;
    public static Func<ScichartSurfaceMVVM, 
    #nullable enable
    IEnumerable<
    #nullable disable
    Order>> \u0023\u003DzKeRpc1y1\u0024C\u0024IWjYsyQ\u003D\u003D;

    internal void \u0023\u003Dz0sbMZcoPgn7DQ3i\u0024I2emR\u00244\u003D(
      ScichartSurfaceMVVM _param1)
    {
      _param1.Area.ChartSurfaceViewModel.ShowHiddenAxesCommand.TryExecute((object) null);
    }

    internal void \u0023\u003DzziKq1fCxTiR\u0024IxwE1_to_NU\u003D(ChartAxis _param1)
    {
      IChartArea chartArea = _param1.ChartArea;
      if (((ICollection<IChartAxis>) chartArea.XAxises).Contains((IChartAxis) _param1))
        ((ICollection<IChartAxis>) chartArea.XAxises).Remove((IChartAxis) _param1);
      if (!((ICollection<IChartAxis>) chartArea.YAxises).Contains((IChartAxis) _param1))
        return;
      ((ICollection<IChartAxis>) chartArea.YAxises).Remove((IChartAxis) _param1);
    }

    internal void \u0023\u003Dz\u0024yGfI4nRC88_Fx431vwDLXg\u003D(
      IScichartSurfaceVM _param1)
    {
      \u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzNCeF9LrrNtIjst\u0024ouVg4RRo\u003D lrrNtIjstOuVg4Rro = new \u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003DzNCeF9LrrNtIjst\u0024ouVg4RRo\u003D();
      lrrNtIjstOuVg4Rro.\u0023\u003DzsWV8_ck\u003D = _param1;
      IChart chart = ((ScichartSurfaceMVVM) lrrNtIjstOuVg4Rro.\u0023\u003DzsWV8_ck\u003D).Chart;
      IChartArea area = chart.Areas.FirstOrDefault<IChartArea>(new Func<IChartArea, bool>(lrrNtIjstOuVg4Rro.\u0023\u003DzHDJpZroCOKM644oB\u0024A\u003D\u003D));
      if (area == null)
        return;
      chart.RemoveArea(area);
    }

    internal bool \u0023\u003DzO_BIxNwmDn6VDgqOIx_JE6RwPKwP(Order _param1) => true;

    internal 
    #nullable enable
    IEnumerable<
    #nullable disable
    Order> \u0023\u003DzrhkYnMPbNPr3HVPmAT1zSUTwkFvh(
      ScichartSurfaceMVVM _param1)
    {
      return _param1.\u0023\u003DzQ\u0024gUWeEbsN2c(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzU9srAoETJDIIA3EbGw\u003D\u003D ?? (\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzU9srAoETJDIIA3EbGw\u003D\u003D = new Func<Order, bool>(\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dz6ysbq7QSBMgTXxXd1pDg18rT_80t)));
    }

    internal bool \u0023\u003Dz6ysbq7QSBMgTXxXd1pDg18rT_80t(Order _param1)
    {
      return _param1.State == 1 || _param1.State == 4;
    }

    internal void \u0023\u003Dz4t7qXCEHgeaFBnsnsEfGvGsGPrcn(
      DependencyObject _param1,
      DependencyPropertyChangedEventArgs _param2)
    {
      ((\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy0LqYv5ht_Gnk2YAlZXcwkZGziIlo367a8J0vVW6) _param1).\u0023\u003Dzw_LOBOuAUrZC = (int) _param2.NewValue;
    }
  }

  private sealed class \u0023\u003DzNCeF9LrrNtIjst\u0024ouVg4RRo\u003D
  {
    public IScichartSurfaceVM \u0023\u003DzsWV8_ck\u003D;

    internal bool \u0023\u003DzHDJpZroCOKM644oB\u0024A\u003D\u003D(IChartArea _param1)
    {
      return ((ChartArea) _param1).ChartSurfaceViewModel == this.\u0023\u003DzsWV8_ck\u003D;
    }
  }

  private sealed class \u0023\u003DzymgCCsreH3nwJBFwtEPKXQw\u003D
  {
    public Action<Order> \u0023\u003DzXCEqv64\u003D;

    internal void \u0023\u003Dz69cIxDaENs3AtcYRgfOovNI\u003D(Order _param1)
    {
      Action<Order> zXcEqv64 = this.\u0023\u003DzXCEqv64\u003D;
      if (zXcEqv64 == null)
        return;
      zXcEqv64(_param1);
    }
  }
}
