using DevExpress.Xpf.Core;
using System.Diagnostics;
using System.Windows;

namespace StockSharp.Xaml
{
    /// <summary>Theme xaml helper.</summary>
    public class ThemeXamlHelper : DependencyObject
    {
        
        private static readonly DependencyPropertyKey _dpKey = DependencyProperty.RegisterReadOnly( nameof( IsCurrentThemeDark ), typeof( bool ), typeof( ThemeXamlHelper ), new PropertyMetadata( ( object )true ) );
        
        private static readonly ThemeXamlHelper _helper = new ThemeXamlHelper();

        private ThemeXamlHelper()
        {
            this.IsCurrentThemeDark = ThemeExtensions.IsCurrDark();

            ThemeManager.ApplicationThemeChanged += OnApplicationThemeChanged;
        }

        /// <summary>Singleton instance of the class.</summary>
        public static ThemeXamlHelper Instance
        {
            get
            {
                return ThemeXamlHelper._helper;
            }
        }

        /// <summary>Whether current theme is dark.</summary>
        public bool IsCurrentThemeDark
        {
            get
            {
                return ( bool )this.GetValue( ThemeXamlHelper._dpKey.DependencyProperty );
            }
            private set
            {
                this.SetValue( ThemeXamlHelper._dpKey, ( object )value );
            }
        }

        private void OnApplicationThemeChanged( DependencyObject d, ThemeChangedRoutedEventArgs e )
        {
            this.IsCurrentThemeDark = ThemeExtensions.IsCurrDark();
        }
    }
}
