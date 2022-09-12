using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using DevExpress.Xpf.Core;
using Ecng.Common;
using Ecng.Localization;
using Ecng.Xaml;
using StockSharp.Localization;

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;

using System.Windows.Markup;
using System.Windows.Threading;
using wyDay.Controls;
namespace StockSharp.Xaml
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : DXWindow
    {
        private bool _needUpdate;
        internal AutomaticUpdater _automaticUpdater = new AutomaticUpdater();

        public AboutWindow( )
        {
            InitializeComponent( );

            this._automaticUpdater.wyUpdateLocation = this.UpdaterPath;
            this._automaticUpdater.Translate( );
        }

        public string UpdaterPath
        {
            get
            {
                return Path.GetFileNameWithoutExtension( Assembly.GetEntryAssembly( ).CodeBase ) + ".Updater.exe";
            }
        }

        public string ProductTitle
        {
            get
            {
                string str = this.Product;
                if ( StringHelper.IsEmpty( str ) )
                {
                    str = Path.GetFileNameWithoutExtension( Assembly.GetEntryAssembly( ).CodeBase );
                }

                return LocalizedStrings.About + " " + str;
            }
        }

        public string Version
        {
            get
            {
                return Assembly.GetEntryAssembly( ).GetName( ).Version.ToString( );
            }
        }

        public string Description
        {
            get
            {
                var descriptionAttribute = AttributeHelper.GetAttribute<AssemblyDescriptionAttribute>( ( ICustomAttributeProvider ) Assembly.GetEntryAssembly( ), false );

                if ( descriptionAttribute == null )
                {
                    return null;
                }

                return LocalizationHelper.Translate( descriptionAttribute.Description, new Languages?( ), new Languages?( ) );
            }
        }

        public string Product
        {
            get
            {

                var title = AttributeHelper.GetAttribute<AssemblyTitleAttribute>( ( ICustomAttributeProvider ) Assembly.GetEntryAssembly( ), false );
                return title?.Title;
            }
        }

        public string Copyright
        {
            get
            {
                var copyright = AttributeHelper.GetAttribute<AssemblyCopyrightAttribute>( ( ICustomAttributeProvider ) Assembly.GetEntryAssembly( ), false );
                return copyright?.Copyright;
            }
        }

        public string Company
        {
            get
            {
                var companyAttribute = AttributeHelper.GetAttribute<AssemblyCompanyAttribute>( ( ICustomAttributeProvider ) Assembly.GetEntryAssembly( ), false );

                if ( companyAttribute == null )
                {
                    return null;
                }

                return LocalizationHelper.Translate( companyAttribute.Company, new Languages?( ), new Languages?( ) );
            }
        }

        public string LinkUri
        {
            get
            {
                return string.Format( "https://stocksharp.{0}", LocalizedStrings.Domain );
            }
        }

        public bool ClosingForInstall
        {
            get
            {
                return this._automaticUpdater.ClosingForInstall;
            }
        }

        public void CheckForUpdates( TimeSpan? delay = null )
        {
            if ( delay.HasValue )
            {                
                ThreadingHelper.Interval( ThreadingHelper.Timer( new Action( CheckForUpdateHandler ) ), delay.Value, TimeSpan.Zero );
            }
            else
            {
                this._needUpdate = true;
                this._automaticUpdater.ForceCheckForUpdate( true );
            }
        }

        private void CheckForUpdateHandler( )
        {
            Action toBeDone = delegate ( )
            {
                if (this._needUpdate)
                    return;
                this.CheckForUpdates(new TimeSpan?());
            };

            this.GuiAsync( toBeDone );            
        }

        private void AutomaticUpdater_UpdateAvailable( object sender, EventArgs e )
        {
            Action toBeDone = delegate ( )
            {
                var activeWindow = Ecng.Xaml.XamlHelper.GetActiveOrMainWindow( Application.Current );
                var messageBoxResult = new MessageBoxBuilder( ).Owner( activeWindow ).Caption( this.Product ).Question().Text( LocalizedStrings.DownloadUpdates ).YesNo().Show() ;

                if ( messageBoxResult != MessageBoxResult.Yes )
                {
                    return;
                }

                this._automaticUpdater.InstallNow();
            };

            GuiDispatcher.GlobalDispatcher.AddAction( toBeDone );
        }

        private void AutomaticUpdater_ReadyToBeInstalled( object sender, EventArgs e )
        {
            Action toBeDone = delegate ( )
            {
                var activeWindow = Ecng.Xaml.XamlHelper.GetActiveOrMainWindow( Application.Current );
                var messageBoxResult = new MessageBoxBuilder( ).Owner( activeWindow ).Caption( this.Product ).Question().Text( LocalizedStrings.InstallUpdates ).YesNo().Show() ;

                if ( messageBoxResult != MessageBoxResult.Yes )
                {
                    return;
                }

                Ecng.Xaml.XamlHelper.Restart(Application.Current);
            };

            GuiDispatcher.GlobalDispatcher.AddAction( toBeDone );
        }
    }
}
