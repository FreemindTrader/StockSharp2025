using Ecng.Collections;
using Ecng.Common;
using Ecng.Interop;
using Ecng.Localization;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Localization;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    public sealed class PlatformToColorConverter : IMultiValueConverter
    {
        public object Convert( object[ ] values, Type targetType, object parameter, CultureInfo culture )
        {
            var feature = values[0].WpfCast<TargetPlatformFeature>();
            var platform = values[1].WpfCast<bool?>();

            if ( feature == null || platform == null )
            {
                return Binding.DoNothing;
            }

            return feature.Platform == Platforms.AnyCPU || ( !platform.Value && feature.Platform == Platforms.x86 ) || ( platform.Value && feature.Platform == Platforms.x64 )
                ? Brushes.Black
                : Brushes.Transparent;
        }

        public object[ ] ConvertBack( object value, Type[ ] targetTypes, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }


    public partial class TargetPlatformWindow : Window, IComponentConnector
    {

        public static readonly DependencyProperty AppNameProperty = DependencyProperty.Register(nameof (AppName), typeof (string), typeof (TargetPlatformWindow), new PropertyMetadata((object) TypeHelper.ApplicationName ));
        public static readonly DependencyProperty AppIconProperty = DependencyProperty.Register(nameof (AppIcon), typeof (string), typeof (TargetPlatformWindow));
        public static readonly DependencyProperty AutoStartProperty = DependencyProperty.Register(nameof (AutoStart), typeof (bool), typeof (TargetPlatformWindow));
        private readonly ObservableCollection<TargetPlatformFeature> _features = new ObservableCollection<TargetPlatformFeature>();
        private readonly ListCollectionView _featuresView;
        
        public TargetPlatformWindow( )
        {
            this.InitializeComponent();
            this.Title = TypeHelper.ApplicationNameWithVersion;
            var app = (BaseApplication)Application.Current;

            var features = new[]
            {
                new TargetPlatformFeature("Quik",               Languages.Russian, Platforms.AnyCPU),
                new TargetPlatformFeature("SmartCOM",           Languages.Russian, Platforms.AnyCPU),
                new TargetPlatformFeature("Plaza2",             Languages.Russian, Platforms.AnyCPU),
                new TargetPlatformFeature("TWIME",              Languages.Russian, Platforms.AnyCPU),
                new TargetPlatformFeature("Transaq",            Languages.Russian, Platforms.AnyCPU),
                new TargetPlatformFeature("Micex",              Languages.Russian, Platforms.AnyCPU),
                new TargetPlatformFeature("SpbEx",              Languages.Russian, Platforms.AnyCPU),
                new TargetPlatformFeature("AlfaDirect",         Languages.Russian, Platforms.x86),
                new TargetPlatformFeature("OpenECry",           Languages.English, Platforms.AnyCPU),
                new TargetPlatformFeature("InteractiveBrokers", Languages.English, Platforms.AnyCPU),
                new TargetPlatformFeature("E*Trade",            Languages.English, Platforms.AnyCPU),
                new TargetPlatformFeature("Blackwood/Fusion",   Languages.English, Platforms.AnyCPU),
                new TargetPlatformFeature("Lmax",               Languages.English, Platforms.AnyCPU),
                new TargetPlatformFeature("IQFeed",             Languages.English, Platforms.AnyCPU),
                new TargetPlatformFeature("BarChart",           Languages.English, Platforms.AnyCPU),
                new TargetPlatformFeature("Oanda",              Languages.English, Platforms.AnyCPU),
                new TargetPlatformFeature("Fxcm",               Languages.English, Platforms.x64   ),
                new TargetPlatformFeature("Rithmic",            Languages.English, Platforms.AnyCPU),
                new TargetPlatformFeature("Sterling",           Languages.English, Platforms.AnyCPU),
                new TargetPlatformFeature("CQG",                Languages.English, Platforms.AnyCPU),
                new TargetPlatformFeature("FIX/FAST",           Languages.English, Platforms.AnyCPU),
                new TargetPlatformFeature("Itch",               Languages.English, Platforms.AnyCPU),
                new TargetPlatformFeature("Binance",            Languages.English, Platforms.AnyCPU),
                new TargetPlatformFeature("Bitfinex",           Languages.English, Platforms.AnyCPU),
                new TargetPlatformFeature("Bithumb",            Languages.English, Platforms.AnyCPU),
                new TargetPlatformFeature("Bitmex",             Languages.English, Platforms.AnyCPU),
                new TargetPlatformFeature("BitStamp",           Languages.English, Platforms.AnyCPU),
                new TargetPlatformFeature("Bittrex",            Languages.English, Platforms.AnyCPU),
                new TargetPlatformFeature("Btce",               Languages.English, Platforms.AnyCPU),
                new TargetPlatformFeature("Cex",                Languages.English, Platforms.AnyCPU),
                new TargetPlatformFeature("Coinbase",           Languages.English, Platforms.AnyCPU),
                new TargetPlatformFeature("Coincheck",          Languages.English, Platforms.AnyCPU),
                new TargetPlatformFeature("CoinExchange",       Languages.English, Platforms.AnyCPU),
                new TargetPlatformFeature("Cryptopia",          Languages.English, Platforms.AnyCPU),
                new TargetPlatformFeature("Deribit",            Languages.English, Platforms.AnyCPU),
                new TargetPlatformFeature("Exmo",               Languages.English, Platforms.AnyCPU),
                new TargetPlatformFeature("Gdax",               Languages.English, Platforms.AnyCPU),
                new TargetPlatformFeature("HitBtc",             Languages.English, Platforms.AnyCPU),
                new TargetPlatformFeature("Huobi",              Languages.English, Platforms.AnyCPU),
                new TargetPlatformFeature("Kraken",             Languages.English, Platforms.AnyCPU),
                new TargetPlatformFeature("Kucoin",             Languages.English, Platforms.AnyCPU),
                new TargetPlatformFeature("Liqui",              Languages.English, Platforms.AnyCPU),
                new TargetPlatformFeature("LiveCoin",           Languages.English, Platforms.AnyCPU),
                new TargetPlatformFeature("Okcoin",             Languages.English, Platforms.AnyCPU),
                new TargetPlatformFeature("Okex",               Languages.English, Platforms.AnyCPU),
                new TargetPlatformFeature("Poloniex",           Languages.English, Platforms.AnyCPU),
                new TargetPlatformFeature("Zaif",               Languages.English, Platforms.AnyCPU),
                new TargetPlatformFeature("Bitbank",            Languages.English, Platforms.AnyCPU),
                new TargetPlatformFeature("Quoinex",            Languages.English, Platforms.AnyCPU),
                new TargetPlatformFeature("Yobit",              Languages.English, Platforms.AnyCPU),
                new TargetPlatformFeature("Rss",                Languages.English, Platforms.AnyCPU),
                new TargetPlatformFeature("QuantFeed",          Languages.English, Platforms.AnyCPU)
            };

            _features.AddRange( features );
            _features.AddRange( app.ExtendedFeatures );

            _featuresView = ( ListCollectionView ) CollectionViewSource.GetDefaultView( _features );
            _featuresView.CustomSort = new LanguageSorter( SelectedLanguage );

            AppIcon = app.AppIcon;

            if ( !Environment.Is64BitOperatingSystem )
            {
                PlatformCheckBox.IsEnabled = false;
                PlatformCheckBox.IsChecked = false;
            }
            else
            {
                SelectedPlatform = Platforms.x64;
                UpdatePlatformCheckBox();
            }


            SelectedLanguage = LocalizedStrings.ActiveLanguage;
            UpdateLangButtons();

            var configFile = BaseApplication.PlatformConfigurationFile;

            if ( configFile.IsEmptyOrWhiteSpace() || !File.Exists( configFile ) )
            {
                return;
            }

            //var settings = new XmlSerializer<AppStartSettings>().Deserialize(configFile);

            //SelectedPlatform = PlatformCheckBox.IsEnabled ? settings.Platform : Platforms.x86;
            //AutoStart = settings.AutoStart;
            //LocalizedStrings.ActiveLanguage = SelectedLanguage = settings.Language;

            SelectedPlatform = Platforms.x64;
            AutoStart = true;
            LocalizedStrings.ActiveLanguage = SelectedLanguage = Languages.English;

            UpdateLangButtons();
            this.UpdatePlatformCheckBox();
        }

        public string AppName
        {
            get
            {
                return ( string ) this.GetValue( TargetPlatformWindow.AppNameProperty );
            }
            set
            {
                this.SetValue( TargetPlatformWindow.AppNameProperty, ( object ) value );
            }
        }

        public string AppIcon
        {
            get
            {
                return ( string ) this.GetValue( TargetPlatformWindow.AppIconProperty );
            }
            private set
            {
                this.SetValue( TargetPlatformWindow.AppIconProperty, ( object ) value );
            }
        }

        public bool AutoStart
        {
            get
            {
                return ( bool ) this.GetValue( TargetPlatformWindow.AutoStartProperty );
            }
            private set
            {
                this.SetValue( TargetPlatformWindow.AutoStartProperty, ( object ) value );
            }
        }

        public ObservableCollection<TargetPlatformFeature> Features
        {
            get
            {
                return this._features;
            }
        }

        public Platforms SelectedPlatform { get; private set; }


        private Languages _selectedLanguage;

        public Languages SelectedLanguage
        {
            get { return _selectedLanguage; }
            private set
            {
                _selectedLanguage = value;

                HintLabel.Text = LocalizedStrings.GetString( LocalizedStrings.XamlStr178Key, value );
                AutoCheckBox.Content = LocalizedStrings.GetString( LocalizedStrings.XamlStr176Key, value );
                SelectPlatformLabel.Text = LocalizedStrings.GetString( LocalizedStrings.SelectAppModeKey, value );
            }
        }

        private void Save( )
        {
            if ( BaseApplication.PlatformConfigurationFile.IsEmptyOrWhiteSpace() )
            {
                return;
            }

            new XmlSerializer<AppStartSettings>().Serialize( new AppStartSettings
            {
                Platform = SelectedPlatform,
                AutoStart = AutoStart,
                Language = SelectedLanguage
            }, BaseApplication.PlatformConfigurationFile );
        }




        private void UpdateLangButtons( )
        {
            if ( SelectedLanguage == Languages.English )
            {
                EnglishLang.IsChecked = true;
                RussianLang.IsChecked = false;
            }
            else
            {
                EnglishLang.IsChecked = false;
                RussianLang.IsChecked = true;
            }
        }

        private void UpdatePlatformCheckBox( )
        {
            PlatformCheckBox.IsChecked = SelectedPlatform == Platforms.x64;
        }

        private void OnLangClick( object sender, RoutedEventArgs e )
        {
            if ( ReferenceEquals( sender, RussianLang ) )
            {
                EnglishLang.IsChecked = !RussianLang.IsChecked;
            }
            else
            {
                RussianLang.IsChecked = !EnglishLang.IsChecked;
            }

            SelectedLanguage = EnglishLang.IsChecked == true ? Languages.English : Languages.Russian;

            _featuresView.CustomSort = new LanguageSorter( SelectedLanguage );
            _featuresView.Refresh();
        }


        private void PlatformCheckBox_OnChecked( object sender, RoutedEventArgs e )
        {
            SelectedPlatform = PlatformCheckBox.IsChecked == true ? Platforms.x64 : Platforms.x86;
            _featuresView.Refresh();
        }

        private void OkButton_OnClick( object sender, RoutedEventArgs e )
        {
            //if (!AutoStart)
            //	SelectedPlatform = Platforms.x64;

            LocalizedStrings.ActiveLanguage = SelectedLanguage;

            Save();

            DialogResult = true;
            //Close();
        }



        private sealed class AppStartSettings : IPersistable
        {            
            public bool AutoStart { get; set; }
            public Platforms Platform { get; set; }
            public Languages Language { get; set; }

            void IPersistable.Load( SettingsStorage settings )
            {
                this.AutoStart = ( bool ) settings.GetValue<bool>( "AutoStart" );
                this.Platform = ( ( Platforms ) settings.GetValue<Platforms>( "Platform" ) );
                this.Language = ( ( Languages ) settings.GetValue<Languages>( "Language" ) );
            }

            void IPersistable.Save( SettingsStorage settings )
            {
                settings.SetValue<bool>( "AutoStart", AutoStart );
                settings.SetValue<Platforms>( "Platform", Platform );
                settings.SetValue<Languages>( "Language", Language );
            }
        }

        private sealed class LanguageSorter : IComparer
        {
            private readonly Languages _language;

            public LanguageSorter( Languages language )
            {
                this._language = language;
            }

            public int Compare( object x, object y )
            {
                var xFeature = (TargetPlatformFeature)x;
                var yFeature = (TargetPlatformFeature)y;

                var xKey = xFeature.PreferLanguage == _language ? -1 : (int)xFeature.PreferLanguage;
                var yKey = yFeature.PreferLanguage == _language ? -1 : (int)yFeature.PreferLanguage;

                if ( xKey == yKey )
                {
                    return string.Compare( xFeature.ToString(), yFeature.ToString(), StringComparison.Ordinal );
                }

                return xKey.CompareTo( yKey );
            }
        }


    }
}
