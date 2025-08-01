using SciChart.Core.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;

#nullable disable
namespace StockSharp.Charting;

public static class ThemeManager
{
    public static readonly DependencyProperty ThemeProperty = DependencyProperty.RegisterAttached("Theme", typeof(string), typeof(ThemeManager), (PropertyMetadata)new FrameworkPropertyMetadata((object)string.Empty, FrameworkPropertyMetadataOptions.Inherits, new PropertyChangedCallback(ThemeManager.OnThemeChanged)));
    public static EventHandler<ThemeAppliedEventArgs> ThemeApplied;
    private static readonly IDictionary<string, ResourceDictionary> _themes = (IDictionary<string, ResourceDictionary>)new Dictionary<string, ResourceDictionary>();
    private static IThemeProvider _themeColorProvider;
    private static readonly Dictionary<string, IThemeProvider> ThemeProviders = new Dictionary<string, IThemeProvider>();
    public static IList<string> _allThemes = (IList<string>)new List<string>()
  {
    "BlackSteel",
    "BrightSpark",
    "Chrome",
    "Electric",
    "ExpressionDark",
    "ExpressionLight",
    "Oscilloscope"
  };

    static ThemeManager()
    {
        ThemeManager.ThemeProvider().ApplyTheme(ThemeManager.GetThemeResourceDictionary("ExpressionDark"));
    }

    public static string GetTheme(DependencyObject _param0)
    {
        return (string)_param0.GetValue(ThemeManager.ThemeProperty);
    }

    public static void SetTheme(DependencyObject _param0, string _param1)
    {
        _param0.SetValue(ThemeManager.ThemeProperty, (object)_param1);
    }

    

    public static IList<string> AllThemes()
    {
        return ThemeManager._allThemes;
    }

    public static IThemeProvider ThemeProvider()
    {
        return ThemeManager._themeColorProvider ?? ( ThemeManager._themeColorProvider = (IThemeProvider)new ThemeColorProvider() );
    }

    public static void AddTheme(string _param0, ResourceDictionary _param1)
    {
        if ( ThemeManager._themes.ContainsKey(_param0) )
            return;
        ThemeManager._themes.Add(_param0, _param1);
        IThemeProvider gmt9VmM5IkNVtVwybkk = (IThemeProvider)new ThemeColorProvider();
        gmt9VmM5IkNVtVwybkk.ApplyTheme(_param1);
        ThemeManager.ThemeProviders.Add(_param0, gmt9VmM5IkNVtVwybkk);
        ThemeManager._allThemes.Add(_param0);
    }

    public static void RemoveTheme(string _param0)
    {
        if ( ThemeManager._themes.ContainsKey(_param0) || !ThemeManager.ThemeProviders.ContainsKey(_param0) )
            return;
        ThemeManager._themes.Remove(_param0);
        ThemeManager.ThemeProviders.Remove(_param0);
        ThemeManager._allThemes.Remove(_param0);
    }

    public static IThemeProvider GetThemeProvider(
      string _param0)
    {
        _param0 = string.IsNullOrEmpty(_param0) ? "ExpressionDark" : _param0;
        IThemeProvider gmt9VmM5IkNVtVwybkk;
        if ( !ThemeManager.ThemeProviders.TryGetValue(_param0, out gmt9VmM5IkNVtVwybkk) )
        {
            gmt9VmM5IkNVtVwybkk = (IThemeProvider)new ThemeColorProvider();
            gmt9VmM5IkNVtVwybkk.ApplyTheme(ThemeManager.GetThemeResourceDictionary(_param0));
            ThemeManager.ThemeProviders.Add(_param0, gmt9VmM5IkNVtVwybkk);
        }
        return gmt9VmM5IkNVtVwybkk;
    }

    private static ResourceDictionary GetThemeResourceDictionary(string theme)
    {
        if ( theme == null )
            return (ResourceDictionary)null;
        if ( ThemeManager._themes.ContainsKey(theme) )
            return ThemeManager._themes[theme];
        ResourceDictionary resourceDictionary = new ResourceDictionary()
        {
            Source = ThemeManager.GetThemeUri(theme)
        };
        ThemeManager._themes.Add(theme, resourceDictionary);
        return resourceDictionary;
    }

    private static Uri GetThemeUri(string _param0)
    {
        return _param0.ToUpper(CultureInfo.InvariantCulture).Contains(";COMPONENT/") ? new Uri(_param0, UriKind.Relative) : new Uri($"/{typeof(SciChartSurface).Assembly.\u0023\u003DzFARAiudukAjJ()};component/Themes/{_param0}.xaml", UriKind.Relative);
    }

    private static void ApplyTheme(this FrameworkElement _param0, string _param1)
    {
        if ( string.IsNullOrEmpty(_param1) )
            return;
        ResourceDictionary resourceDictionary = ThemeManager.GetThemeResourceDictionary(_param1);
        ThemeManager.ThemeProvider().ApplyTheme(resourceDictionary);
        ThemeManager.OnThemeApplied(new ThemeAppliedEventArgs(_param0, _param1));
    }

    private static void OnThemeApplied(
      ThemeAppliedEventArgs _param0)
    {
        EventHandler<ThemeAppliedEventArgs> z8Fi7DbieAz0u = ThemeManager.ThemeApplied;
        if ( z8Fi7DbieAz0u == null )
            return;
        z8Fi7DbieAz0u((object)null, _param0);
    }

    private static void OnThemeChanged(
      DependencyObject _param0,
      DependencyPropertyChangedEventArgs _param1)
    {
        string str = _param1.NewValue as string;
        string oldValue = _param1.OldValue as string;
        FrameworkElement frameworkElement = _param0 as FrameworkElement;
        if ( string.IsNullOrEmpty(str) || frameworkElement == null )
            return;
        if ( str.ToUpper(CultureInfo.InvariantCulture) == "RANDOM" )
        {
            Random random = new Random();
            str = ThemeManager.AllThemes().ElementAt<string>(random.Next(ThemeManager.AllThemes().Count<string>()));
        }
        if ( !( oldValue != str & DependencyPropertyHelper.GetValueSource((DependencyObject)frameworkElement, ThemeManager.ThemeProperty).BaseValueSource != BaseValueSource.Inherited ) )
            return;
        frameworkElement.ApplyTheme(str);
        if ( !( frameworkElement is IInvalidatableElement ks0apzs3KdsLxgXg ) )
            return;
        ks0apzs3KdsLxgXg.InvalidateElement();
    }
}
