using DevExpress.Data;
using DevExpress.Xpf.Core.Serialization;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Printing;
using DevExpress.XtraPrinting;
using Ecng.Backup;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Configuration;
using Ecng.Interop;
using Ecng.Interop.Dde;
using Ecng.Localization;
using Ecng.Serialization;
using Ecng.Xaml;
using Ecng.Xaml.DevExp;
using Ookii.Dialogs.Wpf;
using StockSharp.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace StockSharp.Xaml.GridControl
{
    public class BaseGridControl : DevExpress.Xpf.Grid.GridControl, IPersistable
    {
        public static readonly DependencyProperty AutoScrollProperty = DependencyProperty.Register( nameof( AutoScroll ), typeof( bool ), typeof( BaseGridControl ), new PropertyMetadata( ( object )false, new PropertyChangedCallback( BaseGridControl.smethod_0 ) ) );
        private readonly XlsDdeClient xlsDdeClient_0 = new XlsDdeClient( new DdeSettings( ) );
        private readonly BlockingQueue<IList<object>> _blockingQueue = new BlockingQueue<IList<object>>( );
        private readonly SyncObject syncObject_0 = new SyncObject( );
        private readonly List<PropertyChangeNotifier> list_0 = new List<PropertyChangeNotifier>( );
        private readonly HashSet<string> hashSet_0 = new HashSet<string>( ( IEnumerable<string> )new string[ 5 ] { "ActualWidth", "VisibleIndex", "GroupIndex", "Visible", "SortOrder" } );
        private readonly SimpleResettableTimer simpleResettableTimer_0;
        private bool bool_0;
        private PropertyChangeNotifier propertyChangeNotifier_0;
        private readonly BaseGridControl.ColumnVisibilityHolder columnVisibilityHolder_0;
        private IBackupService ibackupService_0;
        private bool bool_1;

        public BaseGridControl( )
        {
            TableView tableView = new TableView( );
            tableView.AllowEditing = false;
            tableView.AllowConditionalFormattingMenu = true;
            tableView.ShowGroupPanel = false;
            tableView.ShowIndicator = false;
            tableView.NavigationStyle = GridViewNavigationStyle.Row;
            this.View = ( DataViewBase )tableView;
            this.CreateGroupSummary( );
            this.columnVisibilityHolder_0 = new BaseGridControl.ColumnVisibilityHolder( this.Columns );
            this._blockingQueue.Close( );
            this.simpleResettableTimer_0 = new SimpleResettableTimer( TimeSpan.FromMilliseconds( 250.0 ) );
            this.simpleResettableTimer_0.Elapsed += new Action( this.method_24 );
            this.ClipboardCopyMode = ClipboardCopyMode.ExcludeHeader;
            this.CopyingToClipboard += new CopyingToClipboardEventHandler( this.BaseGridControl_CopyingToClipboard );
            this.ContextMenu = new ContextMenu( );
            MenuItem menuItem1 = new MenuItem( );
            menuItem1.Header = ( object )LocalizedStrings.XamlStr17;
            menuItem1.IsCheckable = true;
            MenuItem menuItem2 = menuItem1;
            menuItem2.SetBindings( MenuItem.IsCheckedProperty, ( object )this, nameof( AutoScroll ), BindingMode.TwoWay, ( IValueConverter )null, ( object )null );
            this.ContextMenu.Items.Add( ( object )menuItem2 );
            MenuItem menuItem3 = new MenuItem( );
            menuItem3.Header = ( object )LocalizedStrings.Export;
            MenuItem menuItem4 = menuItem3;
            this.ContextMenu.Items.Add( ( object )menuItem4 );
            MenuItem menuItem5 = new MenuItem( );
            menuItem5.Header = ( object )LocalizedStrings.ClipboardCsv;
            MenuItem menuItem6 = menuItem5;
            menuItem6.Click += new RoutedEventHandler( this.method_2 );
            menuItem4.Items.Add( ( object )menuItem6 );
            MenuItem menuItem7 = new MenuItem( );
            menuItem7.Header = ( object )LocalizedStrings.ClipboardImage;
            MenuItem menuItem8 = menuItem7;
            menuItem8.Click += new RoutedEventHandler( this.method_3 );
            menuItem4.Items.Add( ( object )menuItem8 );
            MenuItem menuItem9 = new MenuItem( );
            menuItem9.Header = ( object )LocalizedStrings.CsvFile;
            MenuItem menuItem10 = menuItem9;
            menuItem10.Click += new RoutedEventHandler( this.method_4 );
            menuItem4.Items.Add( ( object )menuItem10 );
            MenuItem menuItem11 = new MenuItem( );
            menuItem11.Header = ( object )LocalizedStrings.ExcelFile;
            MenuItem menuItem12 = menuItem11;
            menuItem12.Click += new RoutedEventHandler( this.method_5 );
            menuItem4.Items.Add( ( object )menuItem12 );
            MenuItem menuItem13 = new MenuItem( );
            menuItem13.Header = ( object )LocalizedStrings.PngFile;
            MenuItem menuItem14 = menuItem13;
            menuItem14.Click += new RoutedEventHandler( this.method_6 );
            menuItem4.Items.Add( ( object )menuItem14 );
            MenuItem menuItem15 = new MenuItem( );
            menuItem15.Header = ( object )( LocalizedStrings.ShareImage + "..." );
            MenuItem menuItem16 = menuItem15;
            menuItem16.Click += new RoutedEventHandler( this.method_7 );
            menuItem4.Items.Add( ( object )menuItem16 );
            MenuItem menuItem17 = new MenuItem( );
            menuItem17.Header = ( object )LocalizedStrings.CustomExportFormat;
            MenuItem menuItem18 = menuItem17;
            menuItem18.Click += new RoutedEventHandler( this.method_8 );
            menuItem4.Items.Add( ( object )menuItem18 );
            MenuItem menuItem19 = new MenuItem( );
            menuItem19.Header = ( object )"DDE...".Translate( new Languages?( ), new Languages?( ) );
            MenuItem menuItem20 = menuItem19;
            menuItem20.Click += new RoutedEventHandler( this.method_9 );
            menuItem4.Items.Add( ( object )menuItem20 );
            this.MouseDoubleClick += new MouseButtonEventHandler( this.BaseGridControl_MouseDoubleClick );
        }

        private ICollection<GridColumn> GetGridColumns( )
        {
            return ( ICollection<GridColumn> )this.Columns.Where<GridColumn>( BaseGridControl.Class461.func_0 ?? ( BaseGridControl.Class461.func_0 = new Func<GridColumn, bool>( BaseGridControl.Class461.class461_0.GetGridColumns ) ) ).OrderBy<GridColumn, int>( BaseGridControl.Class461.func_1 ?? ( BaseGridControl.Class461.func_1 = new Func<GridColumn, int>( BaseGridControl.Class461.class461_0.GetItemSource ) ) ).ToArray<GridColumn>( );
        }

        private IEnumerable GetItemSource( )
        {
            return ( IEnumerable )this.ItemsSource;
        }

        public BaseGridControl.ColumnVisibilityHolder ColumnVisibility
        {
            get
            {
                return this.columnVisibilityHolder_0;
            }
        }

        public event Action<Exception> DdeErrorHandler;

        public event Action LayoutChanged;

        public event Action<object, ItemDoubleClickEventArgs> ItemDoubleClick;

        public IBackupService BackupService
        {
            get
            {
                return this.ibackupService_0;
            }
            set
            {
                this.ibackupService_0 = value;
            }
        }

        private static void smethod_0(
          DependencyObject dependencyObject_0,
          DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs_0 )
        {
            BaseGridControl logicalChild = dependencyObject_0.FindLogicalChild<BaseGridControl>( );
            logicalChild.bool_1 = ( bool )dependencyPropertyChangedEventArgs_0.NewValue;
            logicalChild.method_18( );
        }

        public bool AutoScroll
        {
            get
            {
                return this.bool_1;
            }
            set
            {
                this.SetValue( BaseGridControl.AutoScrollProperty, ( object )value );
            }
        }

        private void BaseGridControl_MouseDoubleClick( object sender, MouseButtonEventArgs e )
        {
            int byMouseEventArgs1 = this.View.GetRowHandleByMouseEventArgs( ( MouseEventArgs )e );
            if ( byMouseEventArgs1 == int.MinValue || this.IsGroupRowHandle( byMouseEventArgs1 ) )
            {
                return;
            }

            GridColumn byMouseEventArgs2 = ( GridColumn )this.View.GetColumnByMouseEventArgs( ( MouseEventArgs )e );
            if ( byMouseEventArgs2 == null )
            {
                return;
            }

            object row = this.GetRow( byMouseEventArgs1 );
            Action<object, ItemDoubleClickEventArgs> action2 = this.ItemDoubleClick;
            if ( action2 == null )
            {
                return;
            }

            action2( ( object )this, new ItemDoubleClickEventArgs( byMouseEventArgs2, row, e ) );
        }

        private void BaseGridControl_CopyingToClipboard( object sender, CopyingToClipboardEventArgs e )
        {
            StringBuilder stringBuilder = new StringBuilder( );
            foreach ( int selectedRowHandle in this.GetSelectedRowHandles( ) )
            {
                IList<object> source = this.method_12( selectedRowHandle );
                stringBuilder.Append( source.Cast<string>( ).Join( "\t" ) ).AppendLine( );
            }
            stringBuilder.ToString( ).CopyToClipboard<string>( );
            e.Handled = true;
        }

        private void method_2( object sender, RoutedEventArgs e )
        {
            this.method_20( BaseGridControl.Class461.action_0 ?? ( BaseGridControl.Class461.action_0 = new Action<PrintableControlLink>( BaseGridControl.Class461.class461_0.method_2 ) ) );
        }

        private void method_3( object sender, RoutedEventArgs e )
        {
            this.GetImage( ).CopyToClipboard<BitmapSource>( );
        }

        private void method_4( object sender, RoutedEventArgs e )
        {
            BaseGridControl.Class457 class457_1 = new BaseGridControl.Class457( );
            class457_1.baseGridControl_0 = this;
            BaseGridControl.Class457 class457_2 = class457_1;
            VistaSaveFileDialog vistaSaveFileDialog = new VistaSaveFileDialog( );
            vistaSaveFileDialog.RestoreDirectory = true;
            vistaSaveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            vistaSaveFileDialog.DefaultExt = "csv";
            class457_2.vistaSaveFileDialog_0 = vistaSaveFileDialog;
            if ( !class457_1.vistaSaveFileDialog_0.ShowModal( ( DependencyObject )this ) )
            {
                return;
            }

            this.method_20( new Action<PrintableControlLink>( class457_1.GetGridColumns ) );
        }

        private void method_5( object sender, RoutedEventArgs e )
        {
            BaseGridControl.Class459 class459_1 = new BaseGridControl.Class459( );
            class459_1.baseGridControl_0 = this;
            BaseGridControl.Class459 class459_2 = class459_1;
            VistaSaveFileDialog vistaSaveFileDialog = new VistaSaveFileDialog( );
            vistaSaveFileDialog.RestoreDirectory = true;
            vistaSaveFileDialog.Filter = "xlsx files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            vistaSaveFileDialog.DefaultExt = "xlsx";
            class459_2.vistaSaveFileDialog_0 = vistaSaveFileDialog;
            if ( !class459_1.vistaSaveFileDialog_0.ShowModal( ( DependencyObject )this ) )
            {
                return;
            }

            this.method_20( new Action<PrintableControlLink>( class459_1.GetGridColumns ) );
        }

        private void method_6( object sender, RoutedEventArgs e )
        {
            BaseGridControl.Class462 class462_1 = new BaseGridControl.Class462( );
            class462_1.baseGridControl_0 = this;
            BaseGridControl.Class462 class462_2 = class462_1;
            VistaSaveFileDialog vistaSaveFileDialog = new VistaSaveFileDialog( );
            vistaSaveFileDialog.RestoreDirectory = true;
            vistaSaveFileDialog.Filter = "Image files (*.png)|*.png|All files (*.*)|*.*";
            vistaSaveFileDialog.DefaultExt = "png";
            class462_2.vistaSaveFileDialog_0 = vistaSaveFileDialog;
            if ( !class462_1.vistaSaveFileDialog_0.ShowModal( ( DependencyObject )this ) )
            {
                return;
            }

            this.method_20( new Action<PrintableControlLink>( class462_1.GetGridColumns ) );
        }

        private void method_7( object sender, RoutedEventArgs e )
        {
            BaseGridControl.Class460 class460 = new BaseGridControl.Class460( );
            class460.baseGridControl_0 = this;
            class460.ibackupService_0 = this.BackupService ?? ConfigManager.TryGetService<IBackupService>( );
            if ( class460.ibackupService_0 == null )
            {
                return;
            }

            this.method_20( new Action<PrintableControlLink>( class460.GetGridColumns ) );
        }

        private void method_8( object sender, RoutedEventArgs e )
        {
            PrintHelper.ShowRibbonPrintPreviewDialog( this.GetWindow( ), ( DevExpress.Xpf.Printing.LinkBase )new PrintableControlLink( ( IPrintableControl )this.View ) );
        }

        private void method_9( object sender, RoutedEventArgs e )
        {
            new DdeSettingsWindow( )
            {
                DdeClient = this.xlsDdeClient_0,
                StartedAction = new Action( this.method_26 ),
                StoppedAction = new Action( this.method_28 ),
                FlushAction = new Action( this.method_29 )
            }.ShowModal( ( DependencyObject )this );
        }

        private void OnItemsSourceChangedEventHandler( object sender, NotifyCollectionChangedEventArgs e )
        {
            switch ( e.Action )
            {
                case NotifyCollectionChangedAction.Add:
                    if ( e.NewItems == null )
                    {
                        break;
                    }

                    if ( this.AutoScroll )
                    {
                        this.simpleResettableTimer_0.Reset( );
                    }

                    if ( this._blockingQueue.IsClosed )
                    {
                        break;
                    }

                    var newItems = e.NewItems;

                    foreach ( object newItem in newItems )
                    {
                        int listIndex = newItems.IndexOf( newItem );

                        if ( listIndex >= e.NewStartingIndex )
                        {
                            _blockingQueue.Enqueue( GetColumnCellValueByRowIndex( listIndex ) );
                        }
                    }

                    break;
                case NotifyCollectionChangedAction.Remove:
                    break;
                case NotifyCollectionChangedAction.Replace:
                    break;
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    break;
                default:
                    throw new ArgumentOutOfRangeException( );
            }
        }

        private IList<object> GetColumnCellValueByRowIndex( int listIndex )
        {
            var rowHandle = GetRowHandleByListIndex( listIndex );

            var gridColumnList = GetGridColumns( );

            var output = gridColumnList.Select( x => ( GetCellValue( rowHandle, x ) ) ).Cast<object>( ).ToList<object>( );

            return output;
        }

        private IList<object> method_11( int int_0 )
        {
            return this.method_12( this.GetRowHandleByListIndex( int_0 ) );
        }

        private IList<object> method_12( int int_0 )
        {
            BaseGridControl.Class456 class456 = new BaseGridControl.Class456( );
            class456.baseGridControl_0 = this;
            class456.int_0 = int_0;
            if ( class456.int_0 == int.MinValue )
            {
                return ( IList<object> )new List<object>( );
            }

            return ( IList<object> )this.GetGridColumns( ).Select<GridColumn, string>( new Func<GridColumn, string>( class456.GetGridColumns ) ).Cast<object>( ).ToList<object>( );
        }        

        protected override void OnItemsSourceChanged( object oldValue, object newValue )
        {
            var oldNotify = oldValue as INotifyCollectionChanged;

            if ( oldNotify != null )
            {
                oldNotify.CollectionChanged -= new NotifyCollectionChangedEventHandler( OnItemsSourceChangedEventHandler );
            }

            var newNotify = newValue as INotifyCollectionChanged;

            if ( newNotify != null )
            {
                newNotify.CollectionChanged += new NotifyCollectionChangedEventHandler( OnItemsSourceChangedEventHandler );
            }

            base.OnItemsSourceChanged( oldValue, newValue );
            this.LoadExpandedState( );
        }

        protected override void OnLoaded( object sender, RoutedEventArgs e )
        {
            base.OnLoaded( sender, e );
            foreach ( GridColumn column in ( Collection<GridColumn> )this.Columns )
            {
                this.method_14( column );
            }

            this.Columns.CollectionChanged += new NotifyCollectionChangedEventHandler( this.method_16 );
            this.FilterChanged += new RoutedEventHandler( this.BaseGridControl_FilterChanged );
            if ( !( this.View is TableView view ) )
            {
                return;
            }

            view.FormatConditions.CollectionChanged += new NotifyCollectionChangedEventHandler( this.method_17 );
            this.propertyChangeNotifier_0 = new PropertyChangeNotifier( ( DependencyObject )this.View, "ShowGroupPanel" );
            this.propertyChangeNotifier_0.ValueChanged += new Action( this.method_18 );
            this.LoadExpandedState( );
        }

        private void method_14( GridColumn gridColumn_0 )
        {
            foreach ( string path in this.hashSet_0 )
            {
                PropertyChangeNotifier propertyChangeNotifier = new PropertyChangeNotifier( ( DependencyObject )gridColumn_0, path );
                propertyChangeNotifier.ValueChanged += new Action( this.method_18 );
                this.list_0.Add( propertyChangeNotifier );
            }
        }

        private void method_15( GridColumn gridColumn_0 )
        {
            foreach ( PropertyChangeNotifier propertyChangeNotifier in this.list_0.Where<PropertyChangeNotifier>( new Func<PropertyChangeNotifier, bool>( new BaseGridControl.Class454( ) { gridColumn_0 = gridColumn_0 }.GetGridColumns ) ).ToArray<PropertyChangeNotifier>( ) )
            {
                this.list_0.Remove( propertyChangeNotifier );
                propertyChangeNotifier.ValueChanged -= new Action( this.method_18 );
                propertyChangeNotifier.Dispose( );
            }
        }

        private void method_16( object sender, NotifyCollectionChangedEventArgs e )
        {
            if ( e.NewItems != null || e.OldItems != null )
            {
                this.method_18( );
            }

            if ( e.OldItems != null )
            {
                foreach ( GridColumn oldItem in ( IEnumerable )e.OldItems )
                {
                    this.method_15( oldItem );
                }
            }
            if ( e.NewItems == null )
            {
                return;
            }

            foreach ( GridColumn newItem in ( IEnumerable )e.NewItems )
            {
                this.method_14( newItem );
            }
        }

        private void BaseGridControl_FilterChanged( object sender, RoutedEventArgs e )
        {
            this.method_18( );
        }

        private void method_17( object sender, NotifyCollectionChangedEventArgs e )
        {
            this.method_18( );
        }

        private void method_18( )
        {
            Action action1 = this.LayoutChanged;
            if ( action1 == null )
            {
                return;
            }

            action1( );
        }

        private void CreateGroupSummary( )
        {
            this.GroupSummary.Clear( );
            var groupSummary = this.GroupSummary;
            GridSummaryItem item = new GridSummaryItem( );
            item.SummaryType = SummaryItemType.Count;
            item.DisplayFormat = LocalizedStrings.ItemsCountParam;
            groupSummary.Add( item );
        }

        private void method_20( Action<PrintableControlLink> action_3 )
        {
            BaseGridControl.Class458 class458 = new BaseGridControl.Class458( );
            class458.action_0 = action_3;
            class458.class376_0 = new class376( ( UIElement )this );
            class458.adornerLayer_0 = AdornerLayer.GetAdornerLayer( ( Visual )this );
            class458.adornerLayer_0.Add( ( Adorner )class458.class376_0 );
            class458.printableControlLink_0 = new PrintableControlLink( ( IPrintableControl )this.View );
            class458.bool_0 = false;
            class458.class376_0.GetGridColumns( new Action( class458.GetItemSource ) );
            try
            {
                class458.printableControlLink_0.PrintingSystem.ProgressReflector.PositionChanged += new EventHandler( class458.GetGridColumns );
            }
            finally
            {
                class458.printableControlLink_0.PrintingSystem.ResetProgressReflector( );
            }
            class458.printableControlLink_0.CreateDocument( true );
            class458.printableControlLink_0.CreateDocumentFinished += new EventHandler<EventArgs>( class458.method_2 );
        }

        private void method_21( string string_0 )
        {
            if ( string_0.IsEmpty( ) || !File.Exists( string_0 ) )
            {
                return;
            }

            if ( new MessageBoxBuilder( ).Owner( ( DependencyObject )this ).Caption( LocalizedStrings.Export ).Text( LocalizedStrings.ExportDoneOpenFile.Put( ( object )Path.GetFileName( string_0 ) ) ).YesNo( ).Show( ) != MessageBoxResult.Yes )
            {
                return;
            }

            string_0.TryOpenLink( ( DependencyObject )this );
        }

        public virtual void Load( SettingsStorage storage )
        {
            this.AutoScroll  = storage.GetValue<bool>( "AutoScroll", this.AutoScroll );
            string layoutXml = storage.GetValue<string>( "Layout", ( string )null );

            if ( layoutXml != null )
            {
                this.DxperDeserialize( layoutXml );
            }

            this.LoadExpandedState( );
            this.CreateGroupSummary( );
        }

        public virtual void Save( SettingsStorage storage )
        {
            storage.SetValue<bool>( "AutoScroll", this.AutoScroll );
            storage.SetValue<string>( "Layout", this.DxperSerialize( ) );
        }

        public void LoadExpandedState( )
        {
        }

        private string DxperSerialize( )
        {
            using ( MemoryStream memoryStream = new MemoryStream( ) )
            {
                DXSerializer.Serialize( ( DependencyObject )this, ( Stream )memoryStream, "StockSharp", ( DXOptionsLayout )null );
                return Encoding.UTF8.GetString( memoryStream.ToArray( ) );
            }
        }

        private void DxperDeserialize( string layoutXaml )
        {
            if ( layoutXaml == null )
            {
                throw new ArgumentNullException( "settings" );
            }

            using ( MemoryStream memoryStream = new MemoryStream( Encoding.UTF8.GetBytes( layoutXaml ) ) )
            {
                DXSerializer.Deserialize( ( DependencyObject )this, ( Stream )memoryStream, "StockSharp", ( DXOptionsLayout )null );
            }
        }

        private void method_24( )
        {
            GuiDispatcher.GlobalDispatcher.AddAction( new Action( this.method_25 ) );
        }

        private void method_25( )
        {
            this.FindVisualChild<ScrollViewer>( )?.ScrollToEnd( );
        }

        private void method_26( )
        {
            this._blockingQueue.Open( );
            this.bool_0 = false;
            new Action( this.method_27 ).Thread( ).Name( "Grid DDE thread" ).Launch( );
            IEnumerable itemSource = this.GetItemSource( );
            if ( itemSource == null )
            {
                return;
            }

            foreach ( IList<object> objectList in itemSource )
            {
                _blockingQueue.Enqueue( objectList, false );
            }
        }

        private void method_27( )
        {
            try
            {
                IList<object> objectList;

                while ( this._blockingQueue.TryDequeue( out objectList, true, true ) )
                {
                    this.xlsDdeClient_0.Poke( ( IList<IList<object>> )new IList<object>[ 1 ] { objectList } );
                }
            }
            catch ( Exception ex )
            {
                Action<Exception> action0 = this.DdeErrorHandler;
                if ( action0 != null )
                {
                    action0( ex );
                }
            }
            lock ( this.syncObject_0 )
            {
                this.bool_0 = true;
                this.syncObject_0.Pulse( );
            }
        }

        private void method_28( )
        {
            this._blockingQueue.Close( );
            lock ( this.syncObject_0 )
            {
                if ( this.bool_0 )
                {
                    return;
                }

                this.syncObject_0.Wait( new TimeSpan?( ) );
            }
        }

        private void method_29( )
        {
            try
            {
                using ( XlsDdeClient xlsDdeClient = new XlsDdeClient( this.xlsDdeClient_0.Settings ) )
                {
                    xlsDdeClient.Start( );
                    List<IList<object>> objectListList = new List<IList<object>>( ) { ( IList<object> )this.GetGridColumns( ).Select<GridColumn, object>( BaseGridControl.Class461.func_2 ?? ( BaseGridControl.Class461.func_2 = new Func<GridColumn, object>( BaseGridControl.Class461.class461_0.method_4 ) ) ).ToList<object>( ) };

                    var toBeAdded = ( IEnumerable<IList<object>> )GetItemSource( );
                    if ( toBeAdded == null )
                    {
                        return;
                    }

                    objectListList.AddRange( toBeAdded );
                    xlsDdeClient.Poke( ( IList<IList<object>> )objectListList );
                }
            }
            catch ( Exception ex )
            {
                int num = ( int )new MessageBoxBuilder( ).Error( ex ).Owner( ( DependencyObject )this ).Show( );
            }
        }

        private sealed class Class454
        {
            public GridColumn gridColumn_0;

            internal bool GetGridColumns( PropertyChangeNotifier propertyChangeNotifier_0 )
            {
                return object.Equals( ( object )propertyChangeNotifier_0.PropertySource, ( object )this.gridColumn_0 );
            }
        }

        
        private sealed class Class456
        {
            public BaseGridControl baseGridControl_0;
            public int int_0;

            internal string GetGridColumns( GridColumn gridColumn_0 )
            {
                return this.baseGridControl_0.GetCellValue( this.int_0, gridColumn_0 )?.ToString( );
            }
        }

        private sealed class Class457
        {
            public VistaSaveFileDialog vistaSaveFileDialog_0;
            public BaseGridControl baseGridControl_0;

            internal void GetGridColumns( PrintableControlLink printableControlLink_0 )
            {
                PrintableControlLink printableControlLink = printableControlLink_0;
                string fileName = this.vistaSaveFileDialog_0.FileName;
                CsvExportOptions options = new CsvExportOptions( );
                options.Encoding = Encoding.UTF8;
                options.EncodingType = EncodingType.UTF8;
                options.TextExportMode = TextExportMode.Value;
                options.Separator = "\t";
                printableControlLink.ExportToCsv( fileName, options );
                this.baseGridControl_0.method_21( this.vistaSaveFileDialog_0.FileName );
            }
        }

        private sealed class Class458
        {
            public class376 class376_0;
            public bool bool_0;
            public PrintableControlLink printableControlLink_0;
            public AdornerLayer adornerLayer_0;
            public Action<PrintableControlLink> action_0;

            internal void GetGridColumns( object sender, EventArgs e )
            {
                if ( !( sender is ProgressReflector progressReflector ) )
                {
                    return;
                }

                this.class376_0.method_2( progressReflector.Position );
            }

            internal void GetItemSource( )
            {
                this.bool_0 = true;
                this.printableControlLink_0.StopPageBuilding( );
            }

            internal void method_2( object sender, EventArgs e )
            {
                this.printableControlLink_0.PrintingSystem.ProgressReflector.MaximizeRange( );
                this.adornerLayer_0.Remove( ( Adorner )this.class376_0 );
                if ( this.bool_0 )
                {
                    return;
                }

                this.action_0( this.printableControlLink_0 );
            }
        }

        private sealed class Class459
        {
            public VistaSaveFileDialog vistaSaveFileDialog_0;
            public BaseGridControl baseGridControl_0;

            internal void GetGridColumns( PrintableControlLink printableControlLink_0 )
            {
                PrintableControlLink printableControlLink = printableControlLink_0;
                string fileName = this.vistaSaveFileDialog_0.FileName;
                XlsxExportOptions options = new XlsxExportOptions( );
                options.TextExportMode = TextExportMode.Text;
                printableControlLink.ExportToXlsx( fileName, options );
                this.baseGridControl_0.method_21( this.vistaSaveFileDialog_0.FileName );
            }
        }

        private sealed class Class460
        {
            public BaseGridControl baseGridControl_0;
            public IBackupService ibackupService_0;

            internal void GetGridColumns( PrintableControlLink printableControlLink_0 )
            {
                using ( MemoryStream memoryStream = new MemoryStream( ) )
                {
                    printableControlLink_0.ExportToImage( ( Stream )memoryStream, new ImageExportOptions( )
                    {
                        ExportMode = ImageExportMode.SingleFilePageByPage
                    } );
                    memoryStream.Position = 0L;
                    
                    string name = ( this.baseGridControl_0.Name );
                    if ( name.IsEmpty( ) )
                    {
                        name = this.baseGridControl_0.GetType( ).Name;
                    }

                    BackupEntry entry = new BackupEntry( ) { Name = name + ".png" };
                    using ( new Scope<Window>( this.baseGridControl_0.GetWindow( ) ) )
                    {
                        if ( this.ibackupService_0.Upload( entry, ( Stream )memoryStream, BaseGridControl.Class461.action_1 ?? ( BaseGridControl.Class461.action_1 = new Action<int>( BaseGridControl.Class461.class461_0.method_3 ) ) ).IsCancellationRequested || !this.ibackupService_0.CanPublish )
                        {
                            return;
                        }

                        string str = this.ibackupService_0.Publish( entry );
                        if ( str.IsEmpty( ) )
                        {
                            return;
                        }

                        str.CopyToClipboard<string>( );
                        str.OpenLink( false );
                    }
                }
            }
        }

        [Serializable]
        private sealed class Class461
        {
            public static readonly BaseGridControl.Class461 class461_0 = new BaseGridControl.Class461( );
            public static Func<GridColumn, bool> func_0;
            public static Func<GridColumn, int> func_1;
            public static Action<PrintableControlLink> action_0;
            public static Action<int> action_1;
            public static Func<GridColumn, object> func_2;

            internal bool GetGridColumns( GridColumn gridColumn_0 )
            {
                return gridColumn_0.Visible;
            }

            internal int GetItemSource( GridColumn gridColumn_0 )
            {
                return gridColumn_0.VisibleIndex;
            }

            internal void method_2( PrintableControlLink printableControlLink_0 )
            {
                using ( MemoryStream memoryStream1 = new MemoryStream( ) )
                {
                    PrintableControlLink printableControlLink = printableControlLink_0;
                    MemoryStream memoryStream2 = memoryStream1;
                    CsvExportOptions options = new CsvExportOptions( );
                    options.Encoding = Encoding.UTF8;
                    options.EncodingType = EncodingType.UTF8;
                    options.TextExportMode = TextExportMode.Value;
                    options.Separator = "\t";
                    printableControlLink.ExportToCsv( ( Stream )memoryStream2, options );
                    Encoding.UTF8.GetString( memoryStream1.ToArray( ) ).CopyToClipboard<string>( );
                }
            }

            internal void method_3( int int_0 )
            {
            }

            internal object method_4( GridColumn gridColumn_0 )
            {
                return gridColumn_0.Header;
            }
        }

        private sealed class Class462
        {
            public VistaSaveFileDialog vistaSaveFileDialog_0;
            public BaseGridControl baseGridControl_0;

            internal void GetGridColumns( PrintableControlLink printableControlLink_0 )
            {
                printableControlLink_0.ExportToImage( this.vistaSaveFileDialog_0.FileName, new ImageExportOptions( )
                {
                    ExportMode = ImageExportMode.DifferentFiles
                } );
                this.baseGridControl_0.method_21( this.vistaSaveFileDialog_0.FileName );
            }
        }

        public class ColumnVisibilityHolder
        {
            private readonly GridColumnCollection gridColumnCollection_0;

            public ColumnVisibilityHolder( GridColumnCollection columns )
            {
                GridColumnCollection columnCollection = columns;
                if ( columnCollection == null )
                {
                    throw new ArgumentNullException( nameof( columns ) );
                }

                this.gridColumnCollection_0 = columnCollection;
            }

            public int Count
            {
                get
                {
                    return this.gridColumnCollection_0.Count;
                }
            }

            public bool this[ int index ]
            {
                get
                {
                    return this.gridColumnCollection_0[ index ].Visible;
                }
                set
                {
                    this.gridColumnCollection_0[ index ].Visible = value;
                }
            }
        }
    }

    internal sealed class class376 : Adorner
    {
        private readonly VisualCollection visualCollection_0;
        private readonly ProgressBarEdit progressBarEdit_0;
        private readonly StackPanel stackPanel_0;
        private Action action_0;

        public class376( UIElement uielement_0 )
          : base( uielement_0 )
        {
            this.visualCollection_0 = new VisualCollection( ( Visual )this );
            Label label1 = new Label( );
            label1.Content = ( object )LocalizedStrings.InProgress;
            label1.HorizontalContentAlignment = HorizontalAlignment.Center;
            label1.Margin = new Thickness( 3.0 );
            Label label2 = label1;
            ProgressBarEdit pgEdit = new ProgressBarEdit( );
            pgEdit.StyleSettings = ( BaseEditStyleSettings )new ProgressBarStyleSettings( );
            pgEdit.Width = 200.0;
            pgEdit.Height = 20.0;
            pgEdit.Minimum = 0.0;
            pgEdit.Maximum = 100.0;
            pgEdit.IsPercent = true;
            pgEdit.DisplayFormatString = "p0";
            pgEdit.ContentDisplayMode = ContentDisplayMode.Value;
            pgEdit.Margin = new Thickness( 3.0 );
            this.progressBarEdit_0 = pgEdit;
            Button button1 = new Button( );
            button1.Content = ( object )LocalizedStrings.Cancel;
            button1.HorizontalContentAlignment = HorizontalAlignment.Center;
            button1.Margin = new Thickness( 3.0 );
            button1.Width = 80.0;
            Button button2 = button1;
            button2.Click += new RoutedEventHandler( this.method_3 );
            StackPanel stackPanel = new StackPanel( );
            stackPanel.Orientation = Orientation.Vertical;
            SolidColorBrush solidColorBrush = new SolidColorBrush( Colors.Gray );
            solidColorBrush.Opacity = 0.8;
            stackPanel.Background = ( Brush )solidColorBrush;
            this.stackPanel_0 = stackPanel;
            this.stackPanel_0.Children.Add( ( UIElement )label2 );
            this.stackPanel_0.Children.Add( ( UIElement )this.progressBarEdit_0 );
            this.stackPanel_0.Children.Add( ( UIElement )button2 );
            this.visualCollection_0.Add( ( Visual )this.stackPanel_0 );
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return this.visualCollection_0.Count;
            }
        }

        public void GetGridColumns( Action action_1 )
        {
            Action action = this.action_0;
            Action comparand;
            do
            {
                comparand = action;
                action = Interlocked.CompareExchange<Action>( ref this.action_0, comparand + action_1, comparand );
            }
            while ( action != comparand );
        }

        public void GetItemSource( Action action_1 )
        {
            Action action = this.action_0;
            Action comparand;
            do
            {
                comparand = action;
                action = Interlocked.CompareExchange<Action>( ref this.action_0, comparand - action_1, comparand );
            }
            while ( action != comparand );
        }

        public void method_2( int int_0 )
        {
            this.progressBarEdit_0.Value = ( double )int_0;
        }

        protected override Visual GetVisualChild( int index )
        {
            return this.visualCollection_0[ index ];
        }

        protected override void OnRender( DrawingContext drawingContext )
        {
            SolidColorBrush solidColorBrush1 = new SolidColorBrush( Colors.Gray );
            solidColorBrush1.Opacity = 0.4;
            SolidColorBrush solidColorBrush2 = solidColorBrush1;
            drawingContext.DrawRectangle( ( Brush )solidColorBrush2, new Pen( ( Brush )Brushes.Gray, 1.0 ), new Rect( new Point( 0.0, 0.0 ), this.DesiredSize ) );
            base.OnRender( drawingContext );
        }

        protected override Size ArrangeOverride( Size arrangeBounds )
        {
            this.stackPanel_0.Arrange( new Rect( arrangeBounds.Width / 2.0 - this.stackPanel_0.ActualWidth / 2.0, arrangeBounds.Height / 2.0 - this.stackPanel_0.ActualHeight / 2.0, Math.Min( arrangeBounds.Width, this.stackPanel_0.ActualWidth ), Math.Min( arrangeBounds.Height, this.stackPanel_0.ActualHeight ) ) );
            return arrangeBounds;
        }

        private void method_3( object sender, RoutedEventArgs e )
        {
            Action action0 = this.action_0;
            if ( action0 == null )
            {
                return;
            }

            action0( );
        }
    }

}
