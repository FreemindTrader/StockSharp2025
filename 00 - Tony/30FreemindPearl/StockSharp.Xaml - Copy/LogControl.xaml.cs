using DevExpress.Data.Filtering;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Grid;
using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Xaml.GridControl;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    internal sealed class JoinMultiValueConverter : IMultiValueConverter
    {
        public object Convert( object[ ] values, Type targetType, object parameter, CultureInfo culture )
        {
            return ( object ) string.Join( " ", values );
        }

        public object[ ] ConvertBack( object value, Type[ ] targetTypes, object parameter, CultureInfo culture )
        {
            throw new NotSupportedException();
        }
    }

    /// <summary>
    /// https://stackoverflow.com/questions/6786629/wpf-multibinding-in-convertor-fails-dependencyproperty-unsetvalue
    /// https://www.wpf-tutorial.com/data-binding/debugging/
    /// 
    /// Tony: I had some UnsetValue error in the xaml code and this link somehow help me to fix it.
    /// </summary>
    public partial class LogControl : UserControl, IComponentConnector, IPersistable, IDisposable, ILogListener
    {
        public static readonly DependencyProperty AutoScrollProperty              = DependencyProperty.Register(nameof (AutoScroll), typeof (bool), typeof (LogControl), new PropertyMetadata( false, new PropertyChangedCallback(AutoScrollChanged)));
        public static readonly DependencyProperty AutoResizeProperty              = DependencyProperty.Register(nameof (AutoResize), typeof (bool), typeof (LogControl), new PropertyMetadata( false, new PropertyChangedCallback(AutoResizeChanged)));
        public static readonly DependencyProperty MaxItemsCountProperty           = DependencyProperty.Register(nameof (MaxItemsCount), typeof (int), typeof (LogControl), new PropertyMetadata( LogMessageCollection.DefaultMaxItemsCount, new PropertyChangedCallback(MaxItemsCountChanged)));
        public static readonly DependencyProperty ShowSourceNameColumnProperty    = DependencyProperty.Register(nameof (ShowSourceNameColumn), typeof (bool), typeof (LogControl), new PropertyMetadata( true, new PropertyChangedCallback(ShowSourceNameColumnChanged)));
        public static readonly DependencyProperty TimeFormatProperty              = DependencyProperty.Register(nameof (TimeFormat), typeof (string), typeof (LogControl), new PropertyMetadata( "yy/MM/dd HH:mm:ss.fff", new PropertyChangedCallback(TimeFormatChanged)));
        public static readonly DependencyProperty ShowErrorProperty               = DependencyProperty.Register(nameof (ShowError), typeof (bool), typeof (LogControl), new PropertyMetadata( true, new PropertyChangedCallback(ShowChanged)));
        public static readonly DependencyProperty ShowWarningProperty             = DependencyProperty.Register(nameof (ShowWarning), typeof (bool), typeof (LogControl), new PropertyMetadata( true, new PropertyChangedCallback(ShowChanged)));
        public static readonly DependencyProperty ShowInfoProperty                = DependencyProperty.Register(nameof (ShowInfo), typeof (bool), typeof (LogControl), new PropertyMetadata( true, new PropertyChangedCallback(ShowChanged)));
        public static readonly DependencyProperty ShowDebugProperty               = DependencyProperty.Register(nameof (ShowDebug), typeof (bool), typeof (LogControl), new PropertyMetadata( true, new PropertyChangedCallback(ShowChanged)));
        public static readonly DependencyProperty ShowVerboseProperty             = DependencyProperty.Register(nameof (ShowVerbose), typeof (bool), typeof (LogControl), new PropertyMetadata( true, new PropertyChangedCallback(ShowChanged)));
        public static readonly DependencyProperty MessagesProperty                = DependencyProperty.Register(nameof (Messages), typeof (LogMessageCollection), typeof (LogControl), new PropertyMetadata( null, new PropertyChangedCallback(MessagesChanged)));
        public static readonly DependencyProperty ClearCommandProperty            = DependencyProperty.Register(nameof (ClearCommand), typeof (ICommand), typeof (LogControl));
        public static readonly DependencyProperty LogAutoScrollProperty           = DependencyProperty.RegisterAttached("LogAutoScroll", typeof (bool), typeof (LogControl), new PropertyMetadata( false, new PropertyChangedCallback(AutoScrollChanged)));
        public static readonly DependencyProperty LogAutoResizeProperty           = DependencyProperty.Register("LogAutoResize", typeof (bool), typeof (LogControl), new PropertyMetadata( false, new PropertyChangedCallback(AutoResizeChanged)));
        public static readonly DependencyProperty LogMaxItemsCountProperty        = DependencyProperty.RegisterAttached("LogMaxItemsCount", typeof (int), typeof (LogControl), new PropertyMetadata( LogMessageCollection.DefaultMaxItemsCount, new PropertyChangedCallback(MaxItemsCountChanged)));
        public static readonly DependencyProperty LogShowSourceNameColumnProperty = DependencyProperty.RegisterAttached("LogShowSourceNameColumn", typeof (bool), typeof (LogControl), new PropertyMetadata(new PropertyChangedCallback(ShowSourceNameColumnChanged)));
        private string _defaultTimeFormat = "yy/MM/dd HH:mm:ss.fff";
        private bool _showError = true;
        private bool _showWarning = true;
        private bool _showInfo = true;
        private bool _showDebug = true;
        private bool _showVerbose = true;
        private bool _autoScroll;
        private LogMessageCollection _messages;
        private bool _updating;        

        public LogControl( )
        {
            InitializeComponent();
            Messages = new LogMessageCollection { MaxCount = LogMessageCollection.DefaultMaxItemsCount };
            Messages.Clear( );

            ClearCommand = ( ICommand ) new DevExpress.Mvvm.DelegateCommand( ( ) => _messages.Clear(), ( ) => ( _messages.Count > 0 ) );

            var menu = new MenuItem();
            menu.Header = LocalizedStrings.ClearItems;

            XamlHelper.SetBindings( ( DependencyObject ) menu, MenuItem.CommandProperty, this, nameof( ClearCommand ), BindingMode.TwoWay, ( IValueConverter ) null, null );
            ( ( FrameworkElement ) MessageGrid ).ContextMenu.Items.Add( new Separator() );
            ( ( FrameworkElement ) MessageGrid ).ContextMenu.Items.Add( menu );
        }

        private static void AutoScrollChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var logicalChild = d.FindLogicalChild<LogControl>();
            logicalChild._autoScroll = ( bool ) e.NewValue;
            logicalChild.RaiseLayoutChangedEvent();
        }

        public bool AutoScroll
        {
            get
            {
                return _autoScroll;
            }
            set
            {
                SetValue( AutoScrollProperty, value );
            }
        }

        private static void AutoResizeChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var logicalChild = d.FindLogicalChild<LogControl>();

            if ( logicalChild._updating )
            {
                return;
            }

            if ( ( bool ) e.NewValue )
            {
                logicalChild.SetColumnsWidth( true );
            }

            logicalChild.RaiseLayoutChangedEvent();
        }

        public bool AutoResize
        {
            get
            {
                return ( bool ) GetValue( AutoResizeProperty );
            }
            set
            {
                SetValue( AutoResizeProperty, value );
            }
        }

        private static void MaxItemsCountChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            d.FindLogicalChild<LogControl>()._messages.MaxCount = ( ( int ) e.NewValue );
        }

        public int MaxItemsCount
        {
            get
            {
                return _messages.MaxCount;
            }
            set
            {
                SetValue( MaxItemsCountProperty, value );
            }
        }

        private static void ShowSourceNameColumnChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            d.FindLogicalChild<LogControl>().MessageGrid.Columns[ 0 ].Visible = ( bool ) e.NewValue;
        }

        public bool ShowSourceNameColumn
        {
            get
            {
                return ( bool ) GetValue( ShowSourceNameColumnProperty );
            }
            set
            {
                SetValue( ShowSourceNameColumnProperty, value );
            }
        }

        private static void TimeFormatChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var logControl = (LogControl) d;
            string newValue = (string) e.NewValue;

            if ( string.IsNullOrEmpty( newValue ) )
            {
                throw new ArgumentNullException();
            }

            string str = newValue;
            logControl._defaultTimeFormat = str;
        }

        public string TimeFormat
        {
            get
            {
                return _defaultTimeFormat;
            }
            set
            {
                SetValue( TimeFormatProperty, value );
            }
        }

        public bool ShowError
        {
            get
            {
                return _showError;
            }
            set
            {
                SetValue( ShowErrorProperty, value );
            }
        }

        public bool ShowWarning
        {
            get
            {
                return _showWarning;
            }
            set
            {
                SetValue( ShowWarningProperty, value );
            }
        }

        public bool ShowInfo
        {
            get
            {
                return _showInfo;
            }
            set
            {
                SetValue( ShowInfoProperty, value );
            }
        }

        public bool ShowDebug
        {
            get
            {
                return _showDebug;
            }
            set
            {
                SetValue( ShowDebugProperty, value );
            }
        }

        public bool ShowVerbose
        {
            get
            {
                return _showVerbose;
            }
            set
            {
                SetValue( ShowVerboseProperty, value );
            }
        }

        private static void ShowChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var ctrl =  d.FindLogicalChild<LogControl>();
            bool newValue = (bool) e.NewValue;

            if ( e.Property == ShowVerboseProperty )
            {
                ctrl._showVerbose = newValue;
            }
            else if ( e.Property == ShowDebugProperty )
            {
                ctrl._showDebug = newValue;
            }
            else if ( e.Property == ShowErrorProperty )
            {
                ctrl._showError = newValue;
            }
            else if ( e.Property == ShowInfoProperty )
            {
                ctrl._showInfo = newValue;
            }
            else if ( e.Property == ShowWarningProperty )
            {
                ctrl._showWarning = newValue;
            }

            if ( ctrl._updating )
            {
                return;
            }

            ctrl.SetFilter();
            ctrl.RaiseLayoutChangedEvent();
        }

        public LogMessageCollection Messages
        {
            get
            {
                return _messages;
            }
            set
            {
                SetValue( MessagesProperty, value );
            }
        }


        private static void MessagesChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( ( LogControl ) d ).SetMessagesCollection( ( LogMessageCollection ) e.NewValue );
        }

        public ICommand ClearCommand
        {
            get
            {
                return ( ICommand ) GetValue( ClearCommandProperty );
            }
            set
            {
                SetValue( ClearCommandProperty, value );
            }
        }

        public static void SetLogAutoScroll( UIElement element, bool value )
        {
            element.SetValue( LogAutoScrollProperty, value );
        }

        public static bool GetLogAutoScroll( UIElement element )
        {
            return ( bool ) element.GetValue( LogAutoScrollProperty );
        }

        public static void SetLogAutoResize( UIElement element, bool value )
        {
            element.SetValue( LogAutoResizeProperty, value );
        }

        public static bool GetLogAutoResize( UIElement element )
        {
            return ( bool ) element.GetValue( LogAutoResizeProperty );
        }

        public static void SetLogMaxItemsCount( UIElement element, int value )
        {
            element.SetValue( LogMaxItemsCountProperty, value );
        }

        public static int GetLogMaxItemsCount( UIElement element )
        {
            return ( int ) element.GetValue( LogMaxItemsCountProperty );
        }

        public static void SetLogShowSourceNameColumn( UIElement element, bool value )
        {
            element.SetValue( LogShowSourceNameColumnProperty, value );
        }

        public static bool GetLogShowSourceNameColumn( UIElement element )
        {
            return ( bool ) element.GetValue( LogShowSourceNameColumnProperty );
        }

        private void SetMessagesCollection( LogMessageCollection value )
        {
            if ( value == null )
            {
                throw new ArgumentNullException();
            }

            _messages = value;            
            MessageGrid.ItemsSource = _messages.Items;

            SetFilter();            
        }

        private void SetFilter( )
        {
            List<LogLevels> logLevelsList = new List<LogLevels>();
            if ( ShowVerbose )
            {
                logLevelsList.Add( LogLevels.Verbose );
            }

            if ( ShowDebug )
            {
                logLevelsList.Add( LogLevels.Debug );
            }

            if ( ShowInfo )
            {
                logLevelsList.Add( LogLevels.Info );
            }

            if ( ShowWarning )
            {
                logLevelsList.Add( LogLevels.Warning );
            }

            if ( ShowError )
            {
                logLevelsList.Add( LogLevels.Error );
            }

            InOperator inOperator = logLevelsList.Count == 5 ? null : new InOperator("Level", (IEnumerable) logLevelsList);
            CriteriaOperator criteriaOperator = Like.IsEmpty() ?  null : CriteriaOperator.Parse("[Message] LIKE '%" + this.Like + "%'");


            if ( ( logLevelsList.Count == 5 ) & Like.IsEmpty() )
            {
                MessageGrid.FilterCriteria = null;
            }
            else if ( logLevelsList.Count == 5 )
            {
                MessageGrid.FilterCriteria = criteriaOperator;
            }
            else if ( Like.IsEmpty() )
            {
                MessageGrid.FilterCriteria = ( ( CriteriaOperator ) inOperator );
            }
            else
            {
                MessageGrid.FilterCriteria = ( ( CriteriaOperator ) new GroupOperator( GroupOperatorType.And, new CriteriaOperator[ 2 ]
                                                                                                                {
                                                                                                                  criteriaOperator,
                                                                                                                  (CriteriaOperator) inOperator
                                                                                                                } 
                                                                                     ) );
            }
        }

        private void SetColumnsWidth( bool auto )
        {
            TableView view;
            if ( ( view = MessageGrid.View as TableView ) == null )
            {
                return;
            }

            view.AllowBestFit =  auto;
            if ( !auto )
            {
                return;
            }

            view.BestFitColumns();
        }

        void ILogListener.WriteMessages( IEnumerable<LogMessage> messages )
        {
            if ( messages == null )
            {
                throw new ArgumentNullException( "messages" );
            }

            _messages.AddRange( messages );
        }

        public void Load( SettingsStorage storage )
        {
            _updating = true;
            try
            {
                SettingsStorage storage1 = (SettingsStorage) storage.GetValue<SettingsStorage>("MessageGrid", null);
                if ( storage1 != null )
                {
                    MessageGrid.Load( storage1 );
                }

                
                AutoResize           = storage.GetValue( nameof( AutoResize ), false );
                ShowSourceNameColumn = storage.GetValue( nameof( ShowSourceNameColumn ), true );
                MaxItemsCount        = storage.GetValue<int>( nameof( MaxItemsCount ), LogMessageCollection.DefaultMaxItemsCount );
                TimeFormat           = storage.GetValue<string>( nameof( TimeFormat ), "yy/MM/dd HH:mm:ss.fff" );
                ShowInfo             = storage.GetValue<bool>( nameof( ShowInfo ), true );
                ShowError            = storage.GetValue<bool>( nameof( ShowError ), true );
                ShowWarning          = storage.GetValue<bool>( nameof( ShowWarning ), true );
                ShowDebug            = storage.GetValue<bool>( nameof( ShowDebug ), true );
                ShowVerbose          = storage.GetValue<bool>( nameof( ShowVerbose ), true );
                Like                 = storage.GetValue<string>( nameof( Like ), Like );

                string settings      = storage.GetValue<string>("BarManager", null);

                if ( settings == null )
                {
                    return;
                }

                BarManager.LoadDevExpressControl( settings );
            }
            finally
            {
                _updating = false;
            }
        }

        public void Save( SettingsStorage storage )
        {
            storage.SetValue<SettingsStorage>( "MessageGrid", PersistableHelper.Save( ( IPersistable ) MessageGrid ) );            
            storage.SetValue<bool>( nameof( AutoResize ), AutoResize );
            storage.SetValue<bool>( nameof( ShowSourceNameColumn ), ShowSourceNameColumn );
            storage.SetValue<int>( nameof( MaxItemsCount ), MaxItemsCount );
            storage.SetValue<string>( nameof( TimeFormat ), TimeFormat );
            storage.SetValue<bool>( nameof( ShowInfo ), ShowInfo );
            storage.SetValue<bool>( nameof( ShowError ), ShowError );
            storage.SetValue<bool>( nameof( ShowWarning ), ShowWarning );
            storage.SetValue<bool>( nameof( ShowDebug ), ShowDebug );
            storage.SetValue<bool>( nameof( ShowVerbose ), ShowVerbose );
            storage.SetValue<string>( nameof( Like ), Like );
            storage.SetValue<string>( nameof( BarManager ), ( ( DependencyObject ) BarManager ).SaveDevExpressControl( ) );
        }

        void IDisposable.Dispose( )
        {
        }

        public string Like
        {
            get
            {
                return ( string ) LikeCtrl.EditValue;
            }
            set
            {
                LikeCtrl.EditValue = ( value );
            }
        }

        private void LikeCtrl_EditValueChanged( object sender, RoutedEventArgs e )
        {
            if ( _updating )
            {
                return;
            }

            SetFilter( );
            RaiseLayoutChangedEvent( );
        }

        public event Action LayoutChanged;

        private void RaiseLayoutChangedEvent( )
        {
            if ( _updating )
            {
                return;
            }

            Action myEvent = LayoutChanged;
            if ( myEvent == null )
            {
                return;
            }

            myEvent( );
        }

        private void MessageGrid_LayoutChanged( )
        {
            RaiseLayoutChangedEvent( );
        }


        //private void MessagesCollectionChanged( object sender, NotifyCollectionChangedEventArgs e )
        //{
        //    TryScroll();
        //}

        //private void TryScroll( )
        //{
        //    if ( !AutoScroll || _messages == null || ( _messages.Count <= 0 || Visibility != Visibility.Visible ) )
        //    {
        //        return;
        //    }

        //    MessageGrid.FindVisualChild<ScrollViewer>( )?.ScrollToEnd();
        //}

        

        

        

        

        

        
             

        


    }
}
