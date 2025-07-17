using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
namespace Ecng.Xaml.Charting
{
    public static class ThemeManager
    {
        public readonly static DependencyProperty ThemeProperty;

        private readonly static IDictionary<string, ResourceDictionary> _themes;

        private static IThemeProvider _themeColorProvider;

        private readonly static Dictionary<string, IThemeProvider> ThemeProviders;

        public static IList<string> _allThemes;

        public static IList<string> AllThemes
        {
            get
            {
                return ThemeManager._allThemes;
            }
        }

        public static IThemeProvider ThemeProvider
        {
            get
            {
                var themeColorProvider = ThemeManager._themeColorProvider;
                if ( themeColorProvider == null )
                {
                    themeColorProvider = new ThemeColorProvider();
                    ThemeManager._themeColorProvider = ( IThemeProvider ) themeColorProvider;
                }
                return themeColorProvider;
            }
        }

        static ThemeManager()
        {
            ThemeManager.ThemeProperty = DependencyProperty.RegisterAttached( "Theme", typeof( string ), typeof( ThemeManager ), new FrameworkPropertyMetadata( string.Empty, FrameworkPropertyMetadataOptions.Inherits, new PropertyChangedCallback( ThemeManager.OnThemeChanged ) ) );
            ThemeManager._themes = new Dictionary<string, ResourceDictionary>();
            ThemeManager.ThemeProviders = new Dictionary<string, IThemeProvider>();
            ThemeManager._allThemes = new List<string>()
            {
                "BlackSteel",
                "BrightSpark",
                "Chrome",
                "Electric",
                "ExpressionDark",
                "ExpressionLight",
                "Oscilloscope"
            };
            ThemeManager.ThemeProvider.ApplyTheme( ThemeManager.GetThemeResourceDictionary( "ExpressionDark" ) );
        }

        public static void AddTheme( string theme, ResourceDictionary dictionary )
        {
            if ( !ThemeManager._themes.ContainsKey( theme ) )
            {
                ThemeManager._themes.Add( theme, dictionary );
                IThemeProvider themeColorProvider = new ThemeColorProvider();
                themeColorProvider.ApplyTheme( dictionary );
                ThemeManager.ThemeProviders.Add( theme, themeColorProvider );
                ThemeManager._allThemes.Add( theme );
            }
        }

        private static void ApplyTheme( this FrameworkElement control, string newTheme )
        {
            if ( !string.IsNullOrEmpty( newTheme ) )
            {
                ResourceDictionary themeResourceDictionary = ThemeManager.GetThemeResourceDictionary(newTheme);
                ThemeManager.ThemeProvider.ApplyTheme( themeResourceDictionary );
                ThemeManager.OnThemeApplied( new ThemeAppliedEventArgs( control, newTheme ) );
            }
        }

        public static string GetTheme( DependencyObject d )
        {
            return ( string ) d.GetValue( ThemeManager.ThemeProperty );
        }

        public static IThemeProvider GetThemeProvider( string theme )
        {
            IThemeProvider themeColorProvider;
            theme = ( string.IsNullOrEmpty( theme ) ? "ExpressionDark" : theme );
            if ( !ThemeManager.ThemeProviders.TryGetValue( theme, out themeColorProvider ) )
            {
                themeColorProvider = new ThemeColorProvider();
                themeColorProvider.ApplyTheme( ThemeManager.GetThemeResourceDictionary( theme ) );
                ThemeManager.ThemeProviders.Add( theme, themeColorProvider );
            }
            return themeColorProvider;
        }

        private static ResourceDictionary GetThemeResourceDictionary( string theme )
        {
            if ( theme == null )
            {
                return null;
            }
            if ( ThemeManager._themes.ContainsKey( theme ) )
            {
                return ThemeManager._themes[ theme ];
            }
            ResourceDictionary resourceDictionaries = new ResourceDictionary()
            {
                Source = ThemeManager.GetThemeUri(theme)
            };
            ThemeManager._themes.Add( theme, resourceDictionaries );
            return resourceDictionaries;
        }

        private static Uri GetThemeUri( string theme )
        {
            if ( theme.ToUpper( CultureInfo.InvariantCulture ).Contains( ";COMPONENT/" ) )
            {
                return new Uri( theme, UriKind.Relative );
            }
            return new Uri( string.Format( "/{0};component/Themes/{1}.xaml", typeof( UltrachartSurface ).Assembly.GetDllName(), theme ), UriKind.Relative );

        }

        private static void OnThemeApplied( ThemeAppliedEventArgs e )
        {
            EventHandler<ThemeAppliedEventArgs> eventHandler = ThemeManager.ThemeApplied;
            if ( eventHandler != null )
            {
                eventHandler( null, e );
            }
        }

        private static void OnThemeChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            string newValue = e.NewValue as string;
            string oldValue = e.OldValue as string;
            FrameworkElement frameworkElement = d as FrameworkElement;

            if ( string.IsNullOrEmpty( newValue ) || frameworkElement == null )
            {
                return;
            }

            if ( newValue.ToUpper( CultureInfo.InvariantCulture ) == "RANDOM" )
            {
                Random random = new Random();
                newValue = ThemeManager.AllThemes.ElementAt<string>( random.Next( ThemeManager.AllThemes.Count<string>() ) );
            }

            if ( ( oldValue != newValue ) & DependencyPropertyHelper.GetValueSource( frameworkElement, ThemeManager.ThemeProperty ).BaseValueSource != BaseValueSource.Inherited )
            {
                frameworkElement.ApplyTheme( newValue );
                IInvalidatableElement invalidatableElement = frameworkElement as IInvalidatableElement;
                if ( invalidatableElement != null )
                {
                    invalidatableElement.InvalidateElement();
                }
            }
        }

        public static void RemoveTheme( string theme )
        {
            if ( !ThemeManager._themes.ContainsKey( theme ) && ThemeManager.ThemeProviders.ContainsKey( theme ) )
            {
                ThemeManager._themes.Remove( theme );
                ThemeManager.ThemeProviders.Remove( theme );
                ThemeManager._allThemes.Remove( theme );
            }
        }

        public static void SetTheme( DependencyObject d, string value )
        {
            d.SetValue( ThemeManager.ThemeProperty, value );
        }

        public static event EventHandler<ThemeAppliedEventArgs> ThemeApplied;
    }
}
