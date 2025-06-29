// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.IndicatorPainters.IndicatorColorProvider
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using DevExpress.Xpf.Core;
using Ecng.Collections;
using Ecng.Common;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Xaml.Charting.IndicatorPainters;

/// <inheritdoc />
public class IndicatorColorProvider : IIndicatorColorProvider
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private static readonly IndicatorColorProviderSeed \u0023\u003Dz4Q3sa\u0024gsAL0q = new IndicatorColorProviderSeed();
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly SynchronizedDictionary<IndicatorColorProviderSeed, int> \u0023\u003Dzy4fXhVU\u003D = new SynchronizedDictionary<IndicatorColorProviderSeed, int>();
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003Dz9IwAea1R8vuk;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private static readonly Color[] \u0023\u003DzpJ1HExkWqXSv = new Color[14]
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
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private static readonly Color[] \u0023\u003DzwVHfLaDc921YiCH_fw\u003D\u003D = new Color[15]
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

  /// <summary>Create instance.</summary>
  public IndicatorColorProvider()
  {
    this.\u0023\u003Dz9IwAea1R8vuk = ThemeExtensions.IsCurrDark();
    ThemeManager.ApplicationThemeChanged += new ThemeChangedRoutedEventHandler(this.\u0023\u003DzAEKOL3_JGd58TKQC0w\u003D\u003D);
  }

  /// <inheritdoc />
  public Color GetNextColor()
  {
    int num = CollectionHelper.SyncGet<SynchronizedDictionary<IndicatorColorProviderSeed, int>, int>(this.\u0023\u003Dzy4fXhVU\u003D, new Func<SynchronizedDictionary<IndicatorColorProviderSeed, int>, int>(new IndicatorColorProvider.\u0023\u003DzxSk_xz9pb7XDulaJDA\u003D\u003D()
    {
      \u0023\u003DzRRvwDu67s9Rm = this,
      \u0023\u003Dzi2Vlbjk\u003D = Scope<IndicatorColorProviderSeed>.Current?.Value ?? IndicatorColorProvider.\u0023\u003Dz4Q3sa\u0024gsAL0q
    }.\u0023\u003Dz2tz5mWTr\u0024r523Symdw\u003D\u003D));
    Color[] colorArray = this.\u0023\u003Dz9IwAea1R8vuk ? IndicatorColorProvider.\u0023\u003DzwVHfLaDc921YiCH_fw\u003D\u003D : IndicatorColorProvider.\u0023\u003DzpJ1HExkWqXSv;
    return colorArray[num % colorArray.Length];
  }

  private void \u0023\u003DzAEKOL3_JGd58TKQC0w\u003D\u003D(
    DependencyObject _param1,
    ThemeChangedRoutedEventArgs _param2)
  {
    this.\u0023\u003Dz9IwAea1R8vuk = ThemeExtensions.IsCurrDark();
  }

  private sealed class \u0023\u003DzxSk_xz9pb7XDulaJDA\u003D\u003D
  {
    public IndicatorColorProvider \u0023\u003DzRRvwDu67s9Rm;
    public IndicatorColorProviderSeed \u0023\u003Dzi2Vlbjk\u003D;

    internal int \u0023\u003Dz2tz5mWTr\u0024r523Symdw\u003D\u003D(
      SynchronizedDictionary<IndicatorColorProviderSeed, int> _param1)
    {
      int num;
      this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003Dzy4fXhVU\u003D.TryGetValue(this.\u0023\u003Dzi2Vlbjk\u003D, ref num);
      return (this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003Dzy4fXhVU\u003D[this.\u0023\u003Dzi2Vlbjk\u003D] = num + 1) - 1;
    }
  }
}
