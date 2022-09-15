using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.ConditionalFormatting;
using DevExpress.Xpf.Core.ConditionalFormatting.Themes;
using DevExpress.Xpf.Grid;
using StockSharp.Xaml.GridControl;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
namespace StockSharp.Xaml
{
    public partial class OptionDesk : BaseGridControl
    {
        private OptionDeskModel _optionModel;

        public OptionDesk( )
        {
            InitializeComponent();
            ThemeManager.ApplicationThemeChanged += new ThemeChangedRoutedEventHandler( OnApplicationThemeChanged );
            ChangeSkin( ApplicationThemeHelper.ApplicationThemeName );
        }

        public OptionDeskModel Model
        {
            get
            {
                return _optionModel;
            }
            set
            {
                if ( _optionModel == value )
                    return;

                _optionModel = value;

                ItemsSource = value?.Rows;
            }
        }

        private void ChangeSkin( string themeName )
        {
            Brush callBackgroundBrush;
            Brush callProgressBrush;
            Brush putBackgroundBrush;
            Brush putProgressBrush;
            Brush volatilityBackgroundBrush;
            Brush volatilityProgressBrush;
            if ( themeName.IsDark( ) )
            {
                callBackgroundBrush       = ( Brush ) Resources[ "CallBackgroundDark" ];
                callProgressBrush         = ( Brush ) Resources[ "CallProgressDark" ];
                putBackgroundBrush        = ( Brush ) Resources[ "PutBackgroundDark" ];
                putProgressBrush          = ( Brush ) Resources[ "PutProgressDark" ];
                volatilityBackgroundBrush = ( Brush ) Resources[ "VolatilityBackgroundDark" ];
                volatilityProgressBrush   = ( Brush ) Resources[ "VolatilityProgressDark" ];
            }
            else
            {
                callBackgroundBrush       = ( Brush ) Resources[ "CallBackgroundLight" ];
                callProgressBrush         = ( Brush ) Resources[ "CallProgressLight" ];
                putBackgroundBrush        = ( Brush ) Resources[ "PutBackgroundLight" ];
                putProgressBrush          = ( Brush ) Resources[ "PutProgressLight" ];
                volatilityBackgroundBrush = ( Brush ) Resources[ "VolatilityBackgroundLight" ];
                volatilityProgressBrush   = ( Brush ) Resources[ "VolatilityProgressLight" ];
            }
            SetOptionThemeResources( themeName, callBackgroundBrush, callProgressBrush, putBackgroundBrush, putProgressBrush, volatilityBackgroundBrush, volatilityProgressBrush );
            SetOptionThemeResources( ( string ) null, callBackgroundBrush, callProgressBrush, putBackgroundBrush, putProgressBrush, volatilityBackgroundBrush, volatilityProgressBrush );
            Resources[ GetConditionalFormatting( themeName ) ] = GetFormatInfoCollection( callBackgroundBrush, putBackgroundBrush, volatilityBackgroundBrush );
        }

        private void SetOptionThemeResources( string themeName, Brush callBackgroundBrush, Brush callProgressBrush, Brush putBackgroundBrush, Brush putProgressBrush, Brush volatilityBackgroundBrush, Brush volatilityProgressBrush )
        {
            Resources[ GetOptionThemeKey( OptionEnum.Call, themeName ) ]               = callBackgroundBrush;
            Resources[ GetOptionThemeKey( OptionEnum.CallProgress, themeName ) ]       = callProgressBrush;
            Resources[ GetOptionThemeKey( OptionEnum.Put, themeName ) ]                = putBackgroundBrush;
            Resources[ GetOptionThemeKey( OptionEnum.PutProgress, themeName ) ]        = putProgressBrush;
            Resources[ GetOptionThemeKey( OptionEnum.Volatility, themeName ) ]         = volatilityBackgroundBrush;
            Resources[ GetOptionThemeKey( OptionEnum.VolatilityProgress, themeName ) ] = volatilityProgressBrush;
        }

        private static object GetOptionThemeKey( OptionEnum optionEnum, string themeName )
        {
            OptionThemeKey otKey = new OptionThemeKey();
            otKey.ResourceKey = optionEnum;
            otKey.ThemeName = themeName;
            return otKey;
        }

        private static object GetConditionalFormatting( string themeName )
        {
            ConditionalFormattingThemeKeyExtension themeKeyExtension = new ConditionalFormattingThemeKeyExtension();
            themeKeyExtension.ResourceKey = ConditionalFormattingThemeKeys.PredefinedFormats;
            themeKeyExtension.ThemeName = themeName;
            return themeKeyExtension;
        }

        private static object GetFormatInfoCollection( Brush callBackgroundBrush, Brush putBackgroundBrush, Brush volatilityBackgroundBrush )
        {
            FormatInfoCollection formatInfoCollection = new FormatInfoCollection();
            formatInfoCollection.Add( new FormatInfo( )
            {
                FormatName = "Call",
                Format = new Format( )
                {
                    Background = callBackgroundBrush
                }
            } );
            formatInfoCollection.Add( new FormatInfo( )
            {
                FormatName = "Put",
                Format = new Format( )
                {
                    Background = putBackgroundBrush
                }
            } );
            formatInfoCollection.Add( new FormatInfo( )
            {
                FormatName = "Strike",
                Format = new Format( )
                {
                    Background = volatilityBackgroundBrush
                }
            } );
            return formatInfoCollection;
        }

        private void OnApplicationThemeChanged( DependencyObject dependencyObject_0, ThemeChangedRoutedEventArgs themeChangedRoutedEventArgs_0 )
        {
            ChangeSkin( ApplicationThemeHelper.ApplicationThemeName );
        }
    }
}
