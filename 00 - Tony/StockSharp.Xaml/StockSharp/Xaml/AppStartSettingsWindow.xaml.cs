// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.AppStartSettingsWindow
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using DevExpress.Xpf.Core;
using Ecng.Common;
using StockSharp.Configuration;
using StockSharp.Licensing;
using StockSharp.Localization;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    /// <summary>
    /// The component to select a platform for the application.
    /// </summary>
    /// <summary>AppStartSettingsWindow</summary>
    public partial class AppStartSettingsWindow : ThemedWindow
    {
        /// <summary>
        /// <see cref="T:System.Windows.DependencyProperty" /> for <see cref="P:StockSharp.Xaml.AppStartSettingsWindow.AppName" />.
        ///     </summary>
        public static readonly DependencyProperty AppNameProperty = DependencyProperty.Register( nameof( AppName ), typeof( string ), typeof( AppStartSettingsWindow ), new PropertyMetadata( ( object )Paths.AppName ) );
        /// <summary>
        /// <see cref="T:System.Windows.DependencyProperty" /> for <see cref="P:StockSharp.Xaml.AppStartSettingsWindow.AppIcon" />.
        ///     </summary>
        public static readonly DependencyProperty AppIconProperty = DependencyProperty.Register( nameof( AppIcon ), typeof( string ), typeof( AppStartSettingsWindow ) );
        /// <summary>
        /// <see cref="T:System.Windows.DependencyProperty" /> for <see cref="P:StockSharp.Xaml.AppStartSettingsWindow.AutoStart" />.
        ///     </summary>
        public static readonly DependencyProperty AutoStartProperty = DependencyProperty.Register( nameof( AutoStart ), typeof( bool ), typeof( AppStartSettingsWindow ) );
        /// <summary>
        /// <see cref="T:System.Windows.DependencyProperty" /> for <see cref="P:StockSharp.Xaml.AppStartSettingsWindow.Online" />.
        ///     </summary>
        public static readonly DependencyProperty OnlineProperty = DependencyProperty.Register( nameof( Online ), typeof( bool ), typeof( AppStartSettingsWindow ), new PropertyMetadata( ( object )true ) );

        private string _selectedLanguage;

        //    internal StackPanel LangPanel;

        //internal ToggleButton RussianLang;

        //internal ToggleButton EnglishLang;

        //internal TextBlock SelectPlatformLabel;

        //internal CheckBox AutoCheckBox;

        //internal CheckBox OnlineMode;

        //internal SimpleButton OkButton;

        //private bool \u0023\u003DzwPHzQV2Vg5J\u0024;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Xaml.AppStartSettingsWindow" />.
        /// </summary>
        public AppStartSettingsWindow()
        {
            this.InitializeComponent();

            ( ( Window )this ).Title = ( string )Paths.AppName;
            this.AppIcon = ( ( BaseApplication )Application.Current ).AppIcon;
            if ( StringHelper.IsEmpty( LicenseHelper.ValidateLicense( nameof( 2127278577 ), ( string )null, ( ( object )this ).GetType() ) ) )
                this.OnlineMode.Visibility = Visibility.Visible;
            this.ChangeUILanguage( LocalizedStrings.get_ActiveLanguage() );
            this.ToggleLanguageButtons();
            AppStartSettings appStartSettings = AppStartSettings.TryLoad();
            if ( appStartSettings == null )
                return;
            this.AutoStart = appStartSettings.get_AutoStart();
            string language;
            this.ChangeUILanguage( language = appStartSettings.get_Language() );
            LocalizedStrings.set_ActiveLanguage( language );
            this.Online = appStartSettings.get_Online();
            this.ToggleLanguageButtons();
        }

        /// <summary>The application name.</summary>
        public string AppName
        {
            get
            {
                return ( string )( ( DependencyObject )this ).GetValue( AppStartSettingsWindow.AppNameProperty );
            }
            set
            {
                ( ( DependencyObject )this ).SetValue( AppStartSettingsWindow.AppNameProperty, ( object )value );
            }
        }

        /// <summary>The application icon.</summary>
        public string AppIcon
        {
            get
            {
                return ( string )( ( DependencyObject )this ).GetValue( AppStartSettingsWindow.AppIconProperty );
            }
            private set
            {
                ( ( DependencyObject )this ).SetValue( AppStartSettingsWindow.AppIconProperty, ( object )value );
            }
        }

        /// <summary>
        /// <see cref="P:StockSharp.Configuration.AppStartSettings.AutoStart" />.
        ///     </summary>
        public bool AutoStart
        {
            get
            {
                return ( bool )( ( DependencyObject )this ).GetValue( AppStartSettingsWindow.AutoStartProperty );
            }
            private set
            {
                ( ( DependencyObject )this ).SetValue( AppStartSettingsWindow.AutoStartProperty, ( object )value );
            }
        }

        /// <summary>
        /// <see cref="P:StockSharp.Configuration.AppStartSettings.Online" />.
        ///     </summary>
        public bool Online
        {
            get
            {
                return ( bool )( ( DependencyObject )this ).GetValue( AppStartSettingsWindow.OnlineProperty );
            }
            private set
            {
                ( ( DependencyObject )this ).SetValue( AppStartSettingsWindow.OnlineProperty, ( object )value );
            }
        }

        /// <summary>The selected culture.</summary>
        public string SelectedLanguage
        {
            get
            {
                return this._selectedLanguage;
            }
        }

        private void ChangeUILanguage( string _param1 )
        {
            this._selectedLanguage = _param1;
            this.AutoCheckBox.Content = ( object )LocalizedStrings.GetString( nameof( 2127278504 ), _param1 );
            this.SelectPlatformLabel.Text = LocalizedStrings.GetString( nameof( 2127278489 ), _param1 );
        }

        private void SaveAppSettings()
        {
            AppStartSettings appStartSettings = new AppStartSettings();
            appStartSettings.AutoStart = ( this.AutoStart );
            appStartSettings.Language = ( this.SelectedLanguage );
            appStartSettings.Online = ( this.Online );
            appStartSettings.TrySave();
        }

        private void ToggleLanguageButtons()
        {
            if ( this.SelectedLanguage == nameof( 2127278224 ) )
            {
                this.EnglishLang.IsChecked = new bool?( true );
                this.RussianLang.IsChecked = new bool?( false );
            }
            else
            {
                this.EnglishLang.IsChecked = new bool?( false );
                this.RussianLang.IsChecked = new bool?( true );
            }
        }

        private void OnLanguageBtnClicked( object _param1, RoutedEventArgs _param2 )
        {
            bool? isChecked;
            if ( _param1 == this.RussianLang )
            {
                ToggleButton zPaPvLyXp8Zby = this.EnglishLang;
                isChecked = this.RussianLang.IsChecked;
                bool? nullable = isChecked.HasValue ? new bool?( !isChecked.GetValueOrDefault() ) : new bool?();
                zPaPvLyXp8Zby.IsChecked = nullable;
            }
            else
            {
                ToggleButton fyrbXpShf8UzZ3Wjia = this.RussianLang;
                isChecked = this.EnglishLang.IsChecked;
                bool? nullable = isChecked.HasValue ? new bool?( !isChecked.GetValueOrDefault() ) : new bool?();
                fyrbXpShf8UzZ3Wjia.IsChecked = nullable;
            }
            isChecked = this.EnglishLang.IsChecked;
            this.ChangeUILanguage( isChecked.GetValueOrDefault() & isChecked.HasValue ? nameof( 2127278224 ) : nameof( 2127278477 ) );
        }

        private void OnOkayBtnClicked( object _param1, RoutedEventArgs _param2 )
        {
            LocalizedStrings.ActiveLanguage = ( this.SelectedLanguage );
            this.SaveAppSettings();
            ( ( Window )this ).DialogResult = new bool?( true );
        }

        ///// <summary>InitializeComponent</summary>
        //[GeneratedCode( "PresentationBuildTasks", "6.0.8.0" )]
        //[DebuggerNonUserCode]
        //public void InitializeComponent()
        //{
        //    if ( this.\u0023\u003DzwPHzQV2Vg5J\u0024)
        //    return;
        //    this.\u0023\u003DzwPHzQV2Vg5J\u0024 = true;
        //    Application.LoadComponent( ( object )this, new Uri(nameof( 2127278470 ), UriKind.Relative ) );
        //}

        //[EditorBrowsable( EditorBrowsableState.Never )]
        //[DebuggerNonUserCode]
        //[GeneratedCode( "PresentationBuildTasks", "6.0.8.0" )]
        //void IComponentConnector.\u0023\u003DzwjqCwJRp5nvBkvFFuDtdoCHyTx2y(
        //  int _param1,
        //  object _param2)
        //{
        //    switch ( _param1 )
        //    {
        //        case 1:
        //        this.LangPanel = ( StackPanel )_param2;
        //        break;
        //        case 2:
        //        this.RussianLang = ( ToggleButton )_param2;
        //        this.RussianLang.Click += new RoutedEventHandler( this.OnLanguageBtnClicked );
        //        break;
        //        case 3:
        //        this.EnglishLang = ( ToggleButton )_param2;
        //        this.EnglishLang.Click += new RoutedEventHandler( this.OnLanguageBtnClicked );
        //        break;
        //        case 4:
        //        this.SelectPlatformLabel = ( TextBlock )_param2;
        //        break;
        //        case 5:
        //        this.AutoCheckBox = ( CheckBox )_param2;
        //        break;
        //        case 6:
        //        this.OnlineMode = ( CheckBox )_param2;
        //        break;
        //        case 7:
        //        this.OkButton = ( SimpleButton )_param2;
        //        ( ( ButtonBase )this.OkButton ).Click += new RoutedEventHandler( this.OnOkayBtnClicked );
        //        break;
        //        default:
        //        this.\u0023\u003DzwPHzQV2Vg5J\u0024 = true;
        //        break;
        //    }
        //}
    }
}
