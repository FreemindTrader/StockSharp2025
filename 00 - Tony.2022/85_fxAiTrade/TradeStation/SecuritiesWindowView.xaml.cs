using DevExpress.Xpf.Core;
using Ecng.Collections;
using Ecng.Common;
using StockSharp.Algo;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using StockSharp.Studio.Controls;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml.PropertyGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace FreemindAITrade.View
{
    public enum TaskStates
    {
        /// <summary>
        /// Остановлен.
        /// </summary>
        Stopped,
        /// <summary>
        /// Останавливается.
        /// </summary>
        Stopping,
        /// <summary>
        /// Запускается.
        /// </summary>
        Starting,
        /// <summary>
        /// Запущен.
        /// </summary>
        Started,
    }

    public enum SecurityLookupSupportTypes
    {
        /// <summary>
        /// Доступна загрузка всех инструментов.
        /// </summary>
        SupportAll,
        /// <summary>
        /// По коду инструмента.
        /// </summary>
        CodeRequired,
        /// <summary>
        /// Не поддерживается.
        /// </summary>
        NotSupported,
    }

    public interface IHydraTask : ILogReceiver, ILogSource, IDisposable, ICloneable<IHydraTask>, ICloneable
    {
        /// <summary>
        /// Адрес иконки, для визуального обозначения.
        /// </summary>
        Uri Icon
        {
            get;
        }


        /// <summary>
        /// Запустить.
        /// </summary>
        void Start();

        /// <summary>
        /// Остановить.
        /// </summary>
        void Stop();

        /// <summary>
        /// Поддерживаемые типы данных.
        /// </summary>
        IEnumerable<DataType> SupportedDataTypes
        {
            get;
        }

        /// <summary>
        /// Поддерживаемые источники данных построения свечей.
        /// </summary>
        IEnumerable<Level1Fields> CandlesBuildFrom
        {
            get;
        }

        /// <summary>
        /// Тип поиска инструмента.
        /// </summary>
        SecurityLookupSupportTypes SecurityLookupSupportType
        {
            get;
        }

        /// <summary>
        /// Событие о загрузке маркет-данных.
        /// </summary>
        event Action<Security, DataType, DateTimeOffset, int, IEnumerable<Message>> DataLoaded;

        /// <summary>
        /// Событие запуска.
        /// </summary>
        event Action<IHydraTask> Started;

        /// <summary>
        /// Событие остановки.
        /// </summary>
        event Action<IHydraTask> Stopped;

        /// <summary>
        /// Текущее состояние задачи.
        /// </summary>
        TaskStates State
        {
            get;
        }
    }
    /// <summary>
    /// Interaction logic for SecuritiesWindowsMVVM.xaml
    /// </summary>
    public partial class SecuritiesWindowView : ThemedWindow, ISecuritiesSelectWindow
    {
        public static RoutedCommand SelectSecurityCommand = new RoutedCommand();
        public static RoutedCommand UnselectSecurityCommand = new RoutedCommand();

        private IHydraTask _task;

        public bool IsLookup { get; set; }

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

        public bool AllowDuplicates { get; set; }

        public IHydraTask Task
        {
            get
            {
                return _task;
            }
            set
            {
                _task = value;
                LookupPanel.Visibility = Visibility.Visible;
            }
        }

        public SecuritiesWindowView()
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

        private void SecuritiesWindowEx_OnLoaded( object sender, RoutedEventArgs e )
        {
            if ( IsLookup )
            {
                SecuritiesAll.Title = LocalizedStrings.Str3255;
                SecuritiesSelected.Title = LocalizedStrings.Str3256;
                StudioServicesRegistry.CommandService.Register<LookupSecuritiesResultCommand>( this, false, cmd => SecuritiesAll.Securities.AddRange( cmd.Securities ), null );
            }
            else
                LookupPanel.Visibility = Visibility.Visible;
        }

        protected override void OnClosed( EventArgs e )
        {
            StudioServicesRegistry.CommandService.UnRegister<LookupSecuritiesResultCommand>( this );
            base.OnClosed( e );
        }

        private void SelectSecurities( Security[ ] securities )
        {
            if ( securities.Length == 0 )
                return;

            SecuritiesSelected.Securities.AddRange( securities );
            if ( !AllowDuplicates )
                SecuritiesAll.ExcludeSecurities.AddRange( securities );
            EnableOk();
        }

        private void UnselectSecurities( Security[ ] securities )
        {
            SecuritiesSelected.Securities.RemoveRange( securities );
            if ( !AllowDuplicates )
                SecuritiesAll.ExcludeSecurities.RemoveRange( securities );
            EnableOk();
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

        private void EnableOk()
        {
            Ok.IsEnabled = true;
        }

        private void LookupPanel_OnLookup( Security filter )
        {
            SecuritiesAll.Securities.Clear();
            new LookupSecuritiesCommand( filter ).Process( this, false );
        }

        private static ISecurityStorage SecurityStorage
        {
            get
            {
                return ServicesRegistry.SecurityStorage;
            }
        }

        //private void DownloadSecurities_OnClick( object sender, RoutedEventArgs e )
        //{
        //    ISecurityDownloader downloader = Task as ISecurityDownloader;

        //    if ( downloader == null || Task.SecurityLookupSupportType == SecurityLookupSupportTypes.NotSupported )
        //    {
        //        if ( new MessageBoxBuilder( ).Warning( ).Text( LocalizedStrings.NotSupportSecurityDownload ).Owner( this ).YesNo( ).Show( ) != MessageBoxResult.Yes )
        //            return;

        //        SecurityCreateWindow wnd = new SecurityCreateWindow( )
        //        {
        //            SecurityStorage = SecurityStorage,
        //            Security = new Security( )
        //        };

        //        if ( !Ecng.Xaml.XamlHelper.ShowModal( wnd, this ) )
        //            return;
        //        SelectSecurities( new Security[ 1 ] { wnd.Security } );
        //    }
        //    else
        //    {
        //        SecurityLookupWindow wnd = new SecurityLookupWindow( )
        //        {
        //            ShowAllOption = Task.SecurityLookupSupportType == SecurityLookupSupportTypes.SupportAll
        //        };

        //        if ( wnd.ShowAllOption )
        //            wnd.Criteria = new Security( );
        //        
        //        if ( !Ecng.Xaml.XamlHelper.ShowModal( wnd, this ) )
        //            return;
        //        Security filter = wnd.Criteria;
        //        BusyIndicator.BusyContent = LocalizedStrings.Str2834;
        //        BusyIndicator.IsBusy = true;
        //        bool isCancelled = false;
        //        Button button = BusyIndicator.FindVisualChilds<Button>( ).FirstOrDefault( b => b.Name == "CancelBtn" );
        //        if ( button != null )
        //            button.Click += ( o, args ) =>
        //            {
        //                isCancelled = true;
        //                if ( !( ( string ) BusyIndicator.BusyContent != LocalizedStrings.Saving ) )
        //                    return;
        //                BusyIndicator.BusyContent = LocalizedStrings.Cancelling;
        //            };
        //        int count = 0;
        //        ( ( Action ) ( ( ) =>
        //        {
        //            try
        //            {
        //                downloader.Refresh( SecurityStorage, filter, s => ++count, ( ) => _isClosing | isCancelled );
        //            }
        //            catch ( Exception ex )
        //            {
        //                ex.LogError( null );
        //            }
        //            try
        //            {
        //                this.GuiAsync( ( ) => BusyIndicator.BusyContent = LocalizedStrings.Saving );
        //                ServicesRegistry.EntityRegistry.Securities.WaitFlush( ); 
        //                this.GuiAsync( ( ) =>
        //                {
        //                    BusyIndicator.IsBusy = false;
        //                    int num = ( int )new MessageBoxBuilder( ).Owner( this ).Text( LocalizedStrings.Str3264Params.Put( ( object )count ) ).Show( );
        //                } );
        //            }
        //            catch ( Exception ex )
        //            {
        //                ex.LogError( null );
        //            }
        //        } ) ).Thread( ).Launch( );
        //    }
        //}

        //private void CreateSecurity_OnClick( object sender, RoutedEventArgs e )
        //{
        //    Ecng.Xaml.XamlHelper.ShowModal( new SecurityCreateWindow( )
        //    {
        //        SecurityStorage = SecurityStorage,
        //        Security = new Security( )
        //    }, this );
        //}

        //protected override void OnClosing( CancelEventArgs e )
        //{
        //    _isClosing = true;
        //    e.Cancel = BusyIndicator.IsBusy;
        //    base.OnClosing( e );
        //}

    }
}
