// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.IndicatorPainters.IndicatorColorProvider
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using DevExpress.Xpf.Core;
using Ecng.Collections;
using Ecng.Common;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Xaml.Charting.IndicatorPainters;

public class IndicatorColorProvider : IIndicatorColorProvider
{
  
  private static readonly IndicatorColorProviderSeed _indicatorColorProviderSeed = new IndicatorColorProviderSeed();
  
  private readonly SynchronizedDictionary<IndicatorColorProviderSeed, int> _indicatorColorProviderSeedMap = new SynchronizedDictionary<IndicatorColorProviderSeed, int>();
  
  private bool _isDarkTheme;
  
  private static readonly Color[] _darkColorMap = new Color[14]
  {
    Colors.Blue,
    Colors.DarkGoldenrod,
    Colors.Purple,
    Colors.DarkRed,
    Colors.DarkGreen,
    Colors.DarkCyan,
    Colors.DarkViolet,
    Colors.DarkRed,
    Colors.SteelBlue,
    Colors.Indigo,
    Colors.Navy,
    Colors.Maroon,
    Colors.Teal,
    Colors.MediumVioletRed
  };
  
  private static readonly Color[] _lightColorMap = new Color[15]
  {
    Colors.Gold,
    Colors.Cyan,
    Colors.Lime,
    Colors.Fuchsia,
    Colors.Yellow,
    Colors.Tomato,
    Colors.DodgerBlue,
    Colors.Orange,
    Colors.DeepPink,
    Colors.White,
    Colors.LimeGreen,
    Colors.Pink,
    Colors.SkyBlue,
    Colors.Red,
    Colors.Violet
  };

  public IndicatorColorProvider()
  {
    this._isDarkTheme = ThemeExtensions.IsCurrDark();
    ThemeManager.ApplicationThemeChanged += new ThemeChangedRoutedEventHandler(this.OnApplicationThemeChanged);
  }

  public Color GetNextColor()
  {
    int num = CollectionHelper.SyncGet<SynchronizedDictionary<IndicatorColorProviderSeed, int>, int>(this._indicatorColorProviderSeedMap, new Func<SynchronizedDictionary<IndicatorColorProviderSeed, int>, int>(new IndicatorColorProvider.\u0023\u003DzxSk_xz9pb7XDulaJDA\u003D\u003D()
    {
      _variableSome3535 = this,
      _IndicatorColorProviderSeed = Scope<IndicatorColorProviderSeed>.Current?.Value ?? IndicatorColorProvider._indicatorColorProviderSeed
    }.SomeSealedClassMethod03845));
    Color[] colorArray = this._isDarkTheme ? IndicatorColorProvider._lightColorMap : IndicatorColorProvider._darkColorMap;
    return colorArray[num % colorArray.Length];
  }

  private void OnApplicationThemeChanged(
    DependencyObject _param1,
    ThemeChangedRoutedEventArgs _param2)
  {
    this._isDarkTheme = ThemeExtensions.IsCurrDark();
  }

  private sealed class \u0023\u003DzxSk_xz9pb7XDulaJDA\u003D\u003D
  {
    public IndicatorColorProvider _variableSome3535;
    public IndicatorColorProviderSeed _IndicatorColorProviderSeed;

    public int SomeSealedClassMethod03845(
      SynchronizedDictionary<IndicatorColorProviderSeed, int> _param1)
    {
      int num;
      this._variableSome3535._indicatorColorProviderSeedMap.TryGetValue(this._IndicatorColorProviderSeed, ref num);
      return (this._variableSome3535._indicatorColorProviderSeedMap[this._IndicatorColorProviderSeed] = num + 1) - 1;
    }
  }
}
