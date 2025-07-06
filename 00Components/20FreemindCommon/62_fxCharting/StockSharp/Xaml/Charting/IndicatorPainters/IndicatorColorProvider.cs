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
    private static readonly IndicatorColorProviderSeed _indicatorColorProviderSeed = new IndicatorColorProviderSeed();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly SynchronizedDictionary<IndicatorColorProviderSeed, int> _indicatorColorProviderSeedMap = new SynchronizedDictionary<IndicatorColorProviderSeed, int>();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private bool _isDarkTheme;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
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

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
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

    /// <summary>Create instance.</summary>
    public IndicatorColorProvider()
    {
        this._isDarkTheme = StockSharp.Xaml.ThemeExtensions.IsCurrDark();
        ThemeManager.ApplicationThemeChanged += new ThemeChangedRoutedEventHandler( this.OnApplicationThemeChanged );
    }

    /// <inheritdoc />
    public Color GetNextColor()
    {
        int num = CollectionHelper.SyncGet(this._indicatorColorProviderSeedMap, p=>
        {
            var seed =Scope<IndicatorColorProviderSeed>.Current?.Value ?? IndicatorColorProvider._indicatorColorProviderSeed;
            
            _indicatorColorProviderSeedMap.TryGetValue( seed, out int num );
            return ( _indicatorColorProviderSeedMap[ seed ] = num + 1 ) - 1;
        });

        Color[] colorArray = this._isDarkTheme ? IndicatorColorProvider._lightColorMap : IndicatorColorProvider._darkColorMap;
        return colorArray[ num % colorArray.Length ];
    }

    private void OnApplicationThemeChanged( DependencyObject _param1, ThemeChangedRoutedEventArgs _param2 )
    {
        this._isDarkTheme = StockSharp.Xaml.ThemeExtensions.IsCurrDark();
    }    
}
