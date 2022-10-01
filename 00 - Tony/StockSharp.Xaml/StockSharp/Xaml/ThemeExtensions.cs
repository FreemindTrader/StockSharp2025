using DevExpress.Xpf.Core;
using Ecng.Common;

namespace StockSharp.Xaml
{
    /// <summary>
    /// Extensions for <see cref="T:DevExpress.Xpf.Core.Theme" />.
    /// </summary>
    public static class ThemeExtensions
    {
        /// <summary>Default theme.</summary>
        public static string DefaultTheme = "VS2017Dark";
        /// <summary>Default dark theme.</summary>
        public static string DefaultDarkTheme = "VS2017Dark";
        /// <summary>Default light theme.</summary>
        public static string DefaultLightTheme = "VS2017Light";

        /// <summary>Invert current theme.</summary>
        public static void Invert()
        {
            if ( ThemeExtensions.IsCurrDark() )
                ApplicationThemeHelper.ApplicationThemeName = ( ThemeExtensions.DefaultLightTheme );
            else
                ApplicationThemeHelper.ApplicationThemeName = ( ThemeExtensions.DefaultDarkTheme );
        }

        /// <summary>
        /// Determines the <see cref="P:DevExpress.Xpf.Core.ApplicationThemeHelper.ApplicationThemeName" /> is dark.
        /// </summary>
        /// <returns>Check result.</returns>
        public static bool IsCurrDark()
        {
            return ApplicationThemeHelper.ApplicationThemeName.IsDark();
        }

        /// <summary>
        /// Initialize <see cref="P:DevExpress.Xpf.Core.ApplicationThemeHelper.ApplicationThemeName" />.
        /// </summary>
        public static void ApplyDefaultTheme()
        {
            ApplicationThemeHelper.ApplicationThemeName = ThemeExtensions.DefaultTheme;
        }

        /// <summary>
        /// Determines the <paramref name="appTheme" /> is dark.
        /// </summary>
        /// <param name="appTheme">Devexpress theme.</param>
        /// <returns>Check result.</returns>
        public static bool IsDark( this string appTheme )
        {
            if ( StringHelper.IsEmpty( appTheme ) )
                return false;

            return  appTheme == "Office2019HighContrast;Touch" ||
                    appTheme == "Office2019Black;Touch" ||
                    appTheme == "Office2019Black" ||
                    appTheme == "Office2016BlackSE" || 
                    appTheme == "Office2016BlackSE;Touch" || 
                    appTheme == "MetropolisDark" || 
                    appTheme == "VS2017Dark"  || 
                    appTheme == "TouchlineDark";
        }
    }
}
