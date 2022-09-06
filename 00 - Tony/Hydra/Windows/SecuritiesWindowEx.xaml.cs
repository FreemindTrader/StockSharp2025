using DevExpress.Xpf.Core;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Hydra.Core;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using StockSharp.Xaml;
using StockSharp.Xaml.PropertyGrid;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Threading;

namespace StockSharp.Hydra.Windows
{
    public partial class SecuritiesWindowEx : ThemedWindow, ISecuritiesSelectWindow, IComponentConnector
    {
        public static RoutedCommand SelectSecurityCommand = new RoutedCommand();
        public static RoutedCommand UnselectSecurityCommand = new RoutedCommand();
        private bool _isClosing;
        private IHydraTask _task;
        private bool _downloadClicked;
        
        public IEnumerable<Security> SelectedSecurities
        {
            get
            {
                return SecuritiesSelected.Securities.LookupAll();
            }
            set
            {
                SelectSecurities( value.ToArray() );
            }
        }

        public ISecurityProvider SecurityProvider
        {
            get
            {
                return SecuritiesAll.SecurityProvider;
            }
            set
            {
                SecuritiesAll.SecurityProvider = value;
            }
        }

        public IHydraTask Task
        {
            get
            {
                return _task;
            }
            set
            {
                _task = value;
                DownloadSecurities.Visibility = Visibility.Visible;
            }
        }

        public SecuritiesWindowEx()
        {
            InitializeComponent();
            SecuritiesAll.SecurityDoubleClick += security =>
            {
                if ( security == null )
                    return;
                SelectSecurities( new Security[1] { security } );
            };
            SecuritiesSelected.SecurityDoubleClick += security =>
            {
                if ( security == null )
                    return;
                UnselectSecurities( new Security[1] { security } );
            };
        }

        private void SelectSecurities( Security[ ] securities )
        {
            if ( securities == null )
                throw new ArgumentNullException( nameof( securities ) );
            SecuritiesSelected.Securities.AddRange( securities );
            SecuritiesAll.ExcludeSecurities.AddRange( securities );
        }

        private void UnselectSecurities( Security[ ] securities )
        {
            if ( securities == null )
                throw new ArgumentNullException( nameof( securities ) );
            SecuritiesSelected.Securities.RemoveRange( securities );
            SecuritiesAll.ExcludeSecurities.RemoveRange( securities );
        }

        private void ExecutedSelectSecurity( object sender, ExecutedRoutedEventArgs e )
        {
            SelectSecurities( SecuritiesAll.SelectedSecurities.ToArray() );
        }

        private void CanExecuteSelectSecurity( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = SecuritiesAll.SelectedSecurities.Any();
        }

        private void ExecutedUnselectSecurity( object sender, ExecutedRoutedEventArgs e )
        {
            UnselectSecurities( SecuritiesSelected.SelectedSecurities.ToArray() );
        }

        private void CanExecuteUnselectSecurity( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = SecuritiesSelected.SelectedSecurities.Any();
        }

        private static ISecurityStorage SecurityStorage
        {
            get
            {
                return ServicesRegistry.SecurityStorage;
            }
        }

        private void DownloadSecurities_OnClick( object sender, RoutedEventArgs e )
        {
            _downloadClicked = true;
            if ( Task.SecurityLookupSupportType == SecurityLookupSupportTypes.NotSupported )
            {
                if ( new MessageBoxBuilder().Warning().Text( LocalizedStrings.NotSupportSecurityDownload ).Owner( this ).YesNo().Show() != MessageBoxResult.Yes )
                    return;
                SecurityCreateWindow wnd = new SecurityCreateWindow() { SecurityStorage = SecurityStorage, Security = new Security() };
                if ( !Ecng.Xaml.XamlHelper.ShowModal( wnd, this ) )
                    return;
                SelectSecurities( new Security[1]
                {
          wnd.Security
                } );
            }
            else
            {
                SecurityLookupWindow wnd = new SecurityLookupWindow() { ExchangeInfoProvider = ServicesRegistry.ExchangeInfoProvider, ShowAllOption = Task.SecurityLookupSupportType == SecurityLookupSupportTypes.SupportAll };
                if ( wnd.ShowAllOption )
                    wnd.CriteriaMessage = new SecurityLookupMessage();
                if ( !Ecng.Xaml.XamlHelper.ShowModal( wnd, this ) )
                    return;
                SecurityLookupMessage filter = wnd.CriteriaMessage;
                BusyIndicator.BusyContent = LocalizedStrings.Str2834;
                BusyIndicator.IsBusy = true;
                bool isCancelled = false;
                SimpleButton simpleButton = BusyIndicator.FindVisualChilds<SimpleButton>().FirstOrDefault( b => b.Name == "CancelBtn" );
                if ( simpleButton != null )
                    simpleButton.Click += ( o, args ) =>
                    {
                        isCancelled = true;
                        if ( !( ( string )BusyIndicator.BusyContent != LocalizedStrings.Saving ) )
                            return;
                        BusyIndicator.BusyContent = LocalizedStrings.Cancelling;
                    };
                int count = 0;
                ( ( Action )( () =>
                {
                    try
                    {
                        Task.Refresh( SecurityStorage, filter, s => ++count, () => _isClosing | isCancelled );
                    }
                    catch ( Exception ex )
                    {
                        ex.LogError( null );
                    }
                    try
                    {
                        this.GuiAsync( () => BusyIndicator.BusyContent = LocalizedStrings.Saving );
                        ServicesRegistry.EntityRegistry.WaitSecuritiesFlush();
                        this.GuiAsync( () =>
             {
                 BusyIndicator.IsBusy = false;
                 int num = ( int )new MessageBoxBuilder().Owner( this ).Text( LocalizedStrings.Str3264Params.Put( count ) ).Show();
             } );
                    }
                    catch ( Exception ex )
                    {
                        ex.LogError( null );
                    }
                } ) ).Thread().Launch();
            }
        }

        private void CreateSecurity_OnClick( object sender, RoutedEventArgs e )
        {
            Ecng.Xaml.XamlHelper.ShowModal( new SecurityCreateWindow()
            {
                SecurityStorage = SecurityStorage,
                Security = new Security()
            }, this );
        }

        private void SecuritiesWindowEx_OnLoaded( object sender, RoutedEventArgs e )
        {
            if ( SecurityProvider.Count > 1 )
                return;
            ( ( Action )( () =>
            {
                TimeSpan.FromSeconds( 2.0 ).Sleep();
                this.GuiAsync( () =>
       {
           if ( _downloadClicked || new MessageBoxBuilder().Warning().Text( LocalizedStrings.DownloadSecurities ).Owner( this ).YesNo().Show() != MessageBoxResult.Yes )
               return;
           DownloadSecurities_OnClick( null, null );
       } );
            } ) ).Thread().Launch();
        }

        protected override void OnClosing( CancelEventArgs e )
        {
            _isClosing = true;
            e.Cancel = BusyIndicator.IsBusy;
            base.OnClosing( e );
        }        
    }
}
