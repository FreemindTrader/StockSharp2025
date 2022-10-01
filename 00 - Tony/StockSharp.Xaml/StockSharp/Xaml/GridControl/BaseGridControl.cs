using DevExpress.Data;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Serialization;
using DevExpress.Xpf.Dialogs;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Printing;
using DevExpress.XtraPrinting;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Interop.Dde;
using Ecng.Serialization;
using Ecng.Xaml;
using Microsoft.Win32;
using StockSharp.Localization;
using StockSharp.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace StockSharp.Xaml.GridControl
{
    /// <summary>The grid control.</summary>
    public class BaseGridControl : DevExpress.Xpf.Grid.GridControl, IPersistable
    {
        internal sealed class MyAdorner : Adorner
        {

            private readonly VisualCollection _vCollection;

            private readonly ProgressBarEdit _progressBarEdit;

            private readonly StackPanel _stackPanel;

            public Action MyAdornerAction;

            public MyAdorner( UIElement element ) : base( element )
            {
                this._vCollection = new VisualCollection( ( Visual )this );
                Label myLabel = new Label();
                myLabel.Content = LocalizedStrings.InProgress;
                myLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
                myLabel.Margin = new Thickness( 3.0 );

                var pgEdit = new ProgressBarEdit();
                pgEdit.StyleSettings = ( ( BaseEditStyleSettings )new ProgressBarStyleSettings() );
                pgEdit.Width = 200.0;
                pgEdit.Height = 20.0;
                pgEdit.Minimum = 0.0;
                pgEdit.Maximum = 100.0;
                pgEdit.IsPercent = true;
                pgEdit.DisplayFormatString = "p0";
                pgEdit.ContentDisplayMode = ContentDisplayMode.Value;
                pgEdit.Margin = new Thickness( 3.0 );
                this._progressBarEdit = pgEdit;

                SimpleButton btnCanel = new SimpleButton();
                btnCanel.Content = LocalizedStrings.Cancel;
                btnCanel.HorizontalContentAlignment = HorizontalAlignment.Center;
                btnCanel.Margin = new Thickness( 3.0 );
                btnCanel.Width = 80.0;
                btnCanel.Click += new RoutedEventHandler( this.OnCancelButtonClicked );

                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Vertical;
                SolidColorBrush solidColorBrush = new SolidColorBrush( Colors.Gray );
                solidColorBrush.Opacity = 0.8;
                stackPanel.Background = ( Brush )solidColorBrush;
                this._stackPanel = stackPanel;
                this._stackPanel.Children.Add( myLabel );
                this._stackPanel.Children.Add( ( UIElement )this._progressBarEdit );
                this._stackPanel.Children.Add( btnCanel );
                this._vCollection.Add( ( Visual )this._stackPanel );
            }

            private void OnCancelButtonClicked( object s, RoutedEventArgs e )
            {
                Action myAction = this.MyAdornerAction;
                if ( myAction == null )
                    return;
                myAction();
            }

            protected override int VisualChildrenCount
            {
                get
                {
                    return this._vCollection.Count;
                }
            }



            public void SetProgressEditBarValue( int _param1 )
            {
                ( ( RangeBaseEdit )this._progressBarEdit ).Value = ( ( double )_param1 );
            }

            protected override Visual GetVisualChild( int _param1 )
            {
                return this._vCollection[_param1];
            }

            protected override void OnRender( DrawingContext _param1 )
            {
                SolidColorBrush solidColorBrush1 = new SolidColorBrush( Colors.Gray );
                solidColorBrush1.Opacity = 0.4;
                SolidColorBrush solidColorBrush2 = solidColorBrush1;
                _param1.DrawRectangle( ( Brush )solidColorBrush2, new Pen( ( Brush )Brushes.Gray, 1.0 ), new Rect( new Point( 0.0, 0.0 ), this.DesiredSize ) );
                base.OnRender( _param1 );
            }

            protected override Size ArrangeOverride( Size mySize )
            {
                this._stackPanel.Arrange( new Rect( mySize.Width / 2.0 - this._stackPanel.ActualWidth / 2.0, mySize.Height / 2.0 - this._stackPanel.ActualHeight / 2.0, Math.Min( mySize.Width, this._stackPanel.ActualWidth ), Math.Min( mySize.Height, this._stackPanel.ActualHeight ) ) );
                return mySize;
            }
        }

        /// <summary>Holds columns visibility.</summary>
        public class ColumnVisibilityHolder
        {

            private readonly GridColumnCollection _columns;

            /// <summary>
            /// Initializes a new instance of the <see cref="P:StockSharp.Xaml.GridControl.BaseGridControl.ColumnVisibility" />.
            /// </summary>
            /// <param name="columns">Columns.</param>
            public ColumnVisibilityHolder( GridColumnCollection columns )
            {
                if ( columns == null )
                    throw new ArgumentNullException( nameof( columns ) );
                this._columns = columns;
            }

            /// <summary>Get columns count.</summary>
            public int Count
            {
                get
                {
                    return ( ( Collection<GridColumn> )this._columns ).Count;
                }
            }

            /// <summary>Get or set column visibility by index.</summary>
            public bool this[int index]
            {
                get
                {
                    return _columns[index].Visible;
                }
                set
                {
                    _columns[index].Visible = value;
                }
            }
        }

        /// <summary>DDE export error handler.</summary>
        public event Action<Exception> DdeErrorHandler;

        /// <summary>Layout changed event.</summary>
        public event Action LayoutChanged;

        /// <summary>
        /// Event of double-clicking the mouse on the selected item.
        /// </summary>
        public event Action<object, ItemDoubleClickEventArgs> ItemDoubleClick;




        private static void OnAutoScrollPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            BaseGridControl logicalChild = d.FindLogicalChild<BaseGridControl>();
            logicalChild._autoScroll = ( bool )e.NewValue;
            logicalChild.OnLayoutChange();
        }

        private void OnLayoutChange()
        {
            Action myAction = this.LayoutChanged;
            if ( myAction == null )
                return;
            myAction();
        }


        /// <summary>
        /// <see cref="T:System.Windows.DependencyProperty" /> for <see cref="P:StockSharp.Xaml.GridControl.BaseGridControl.AutoScroll" />.
        ///     </summary>
        public static readonly DependencyProperty AutoScrollProperty = DependencyProperty.Register( nameof( AutoScroll ), typeof( bool ), typeof( BaseGridControl ), new PropertyMetadata( ( object )false, new PropertyChangedCallback( OnAutoScrollPropertyChanged ) ) );

        private readonly XlsDdeClient _xlsDdeClient;

        private readonly BlockingQueue<IList<object>> _blockingQueue = new BlockingQueue<IList<object>>();

        private readonly SyncObject _lock;

        private readonly List<PropertyChangeNotifier> _propertyChangeNotifierList = new List<PropertyChangeNotifier>();

        private readonly HashSet<string> _needNotifiedCol = new HashSet<string>( new string[5]
                                                                                            {
                                                                                                "ActualWidth",
                                                                                                "VisibleIndex",
                                                                                                "GroupIndex",
                                                                                                "Visible",
                                                                                                "SortOrder"
                                                                                            } );


        private readonly SimpleResettableTimer _timer;

        private bool _done;

        private PropertyChangeNotifier _propertyChangeNotifier;

        private readonly BaseGridControl.ColumnVisibilityHolder _colVisibilityHolder;

        private bool _autoScroll;

        private readonly PopupMenu _popupMenu;


        public bool AutoScroll
        {
            get
            {
                return this._autoScroll;
            }
            set
            {
                ( ( DependencyObject )this ).SetValue( BaseGridControl.AutoScrollProperty, ( object )value );
            }
        }

        /// <summary>Popup menu.</summary>
        public PopupMenu PopupMenu
        {
            get
            {
                return this._popupMenu;
            }
        }

        private void IntializeGroupSummary()
        {
            GroupSummary.Clear();
            var groupSummary = GroupSummary;
            var gridSummaryItem = new GridSummaryItem();
            gridSummaryItem.SummaryType = SummaryItemType.Count;
            gridSummaryItem.DisplayFormat = LocalizedStrings.ItemsCountParam;
            groupSummary.Add( gridSummaryItem );
        }

        private void OnTimerElapsed()
        {
            GuiDispatcher.GlobalDispatcher.AddAction( new Action( this.TimeElapsedGuiAction ) );
        }

        private void TimeElapsedGuiAction()
        {
            ( ( DependencyObject )this ).FindVisualChild<ScrollViewer>()?.ScrollToEnd();
        }

        private void BaseGridControl_CopyingToClipboard( object sender, CopyingToClipboardEventArgs e )
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach ( int handle in ( ( DataControlBase )this ).GetSelectedRowHandles() )
            {
                IList<object> cellValue = this.GetCellValueByHandle( handle );
                stringBuilder.Append( StringHelper.Join( cellValue.Cast<string>(), "\t" ) ).AppendLine();
            }

            stringBuilder.ToString().CopyToClipboard<string>();
            ( ( RoutedEventArgs )e ).Handled = true;
        }

        private IList<object> GetCellValueByHandle( int h )
        {
            if ( h == int.MinValue )
            {
                return new List<object>();
            }

            return ( IList<object> )this.GetVisibleGridColumns().Select<GridColumn, string>( x => GetCellValue( h, x )?.ToString() ).Cast<object>().ToList();
        }

        /// <summary>Load settings.</summary>
        /// <param name="storage">Settings storage.</param>
        public virtual void Load( SettingsStorage storage )
        {
            this.AutoScroll = ( bool )storage.GetValue<bool>( nameof( AutoScroll ), this.AutoScroll );
            string layoutString = ( string )storage.GetValue<string>( "Layout", null );
            if ( layoutString != null )
                this.StringToLayout( layoutString );
            this.LoadExpandedState();
            this.IntializeGroupSummary();
        }

        /// <summary>Load group state settings.</summary>
        public void LoadExpandedState()
        {
        }

        private void StringToLayout( string layoutString )
        {
            if ( layoutString == null )
                throw new ArgumentNullException( "settings" );

            using ( MemoryStream memoryStream = new MemoryStream( StringHelper.UTF8( layoutString ) ) )
            {
                DXSerializer.Deserialize( ( DependencyObject )this, ( Stream )memoryStream, "StockSharp", ( DXOptionsLayout )null );
            }

        }

        /// <summary>Save settings.</summary>
        /// <param name="storage">Settings storage.</param>
        public virtual void Save( SettingsStorage storage )
        {
            storage.SetValue<bool>( nameof( AutoScroll ), this.AutoScroll );
            storage.SetValue<string>( "Layout", this.LayoutToString() );
        }

        private string LayoutToString()
        {
            using ( MemoryStream memoryStream = new MemoryStream() )
            {
                DXSerializer.Serialize( ( DependencyObject )this, ( Stream )memoryStream, "StockSharp", ( DXOptionsLayout )null );
                return StringHelper.UTF8( ( byte[ ] )Converter.To<byte[ ]>( ( object )memoryStream ) );
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="sender">
        /// </param>
        /// <param name="e">
        /// </param>
        protected override void OnLoaded( object sender, RoutedEventArgs e )
        {
            base.OnLoaded( sender, e );

            foreach ( GridColumn gridColumn in Columns )
            {
                foreach ( string str in _needNotifiedCol )
                {
                    var propertyChangeNotifier = new PropertyChangeNotifier( gridColumn, str );
                    propertyChangeNotifier.ValueChanged += OnLayoutChange;
                    _propertyChangeNotifierList.Add( propertyChangeNotifier );
                }
            }


            Columns.CollectionChanged += OnColumnsCollectionChanged;
            this.FilterChanged += BaseGridControl_FilterChanged; // ( new RoutedEventHandler( this.BaseGridControl_FilterChanged ) );

            var view = this.View as TableView;
            if ( view == null )
                return;

            view.FormatConditions.CollectionChanged += OnFormatConditionsCollectionChanged;
            this._propertyChangeNotifier = new PropertyChangeNotifier( ( DependencyObject )this.View, "ShowGroupPanel" );
            this._propertyChangeNotifier.ValueChanged += new Action( this.OnLayoutChange );
            this.LoadExpandedState();
        }

        private void OnFormatConditionsCollectionChanged( object _param1, NotifyCollectionChangedEventArgs e )
        {
            this.OnLayoutChange();
        }

        //private void BaseGridControl_FilterChanged( object _param1, RoutedEventArgs e )
        //{
        //    this.OnLayoutChange();
        //}


        private void BaseGridControl_FilterChanged( object sender, RoutedEventArgs e )
        {
            this.OnLayoutChange();
        }

        private void OnColumnsCollectionChanged( object _param1, NotifyCollectionChangedEventArgs e )
        {
            if ( e.NewItems != null || e.OldItems != null )
                this.OnLayoutChange();
            if ( e.OldItems != null )
            {
                foreach ( GridColumn oldItem in e.OldItems )
                {
                    foreach ( var notifier in _propertyChangeNotifierList.Where( x => x.PropertySource.Equals( oldItem ) ).ToArray() )
                    {
                        _propertyChangeNotifierList.Remove( notifier );
                        _propertyChangeNotifier.ValueChanged -= OnLayoutChange;
                        notifier.Dispose();
                    }
                }
            }
            if ( e.NewItems == null )
                return;

            foreach ( GridColumn col in e.NewItems )
            {
                foreach ( string str in _needNotifiedCol )
                {
                    PropertyChangeNotifier propertyChangeNotifier = new PropertyChangeNotifier( col, str );
                    _propertyChangeNotifier.ValueChanged += OnLayoutChange;
                    _propertyChangeNotifierList.Add( propertyChangeNotifier );
                }
            }
        }

        //  private void OnColumnsCollectionChangedProcessOldItems( GridColumn _param1 )
        //  {
        //      foreach ( PropertyChangeNotifier propertyChangeNotifier in this._propertyChangeNotifierList.Where<PropertyChangeNotifier>( new Func<PropertyChangeNotifier, bool>( new BaseGridControl.\u0023\u003Dzk_UQUHQ_B6RAzwbB5oucS8U\u003D()
        //        {
        //  \u0023\u003DzEQIvO\u0024M\u003D = _param1
        //}.\u0023\u003Dz5KPMpg_rJYogAhFX5w\u003D\u003D) ).ToArray<PropertyChangeNotifier>())
        //{
        //          this._propertyChangeNotifierList.Remove( propertyChangeNotifier );
        //          propertyChangeNotifier.ValueChanged -= new Action( this.OnLayoutChange );
        //          propertyChangeNotifier.Dispose();
        //      }
        //  }



        //        private void OnColumnsCollectionChangedProcessNewItems(GridColumn _param1)
        //    {
        //    foreach (string path in this._needNotifiedCol )
        //    {
        //        PropertyChangeNotifier propertyChangeNotifier = new PropertyChangeNotifier( ( DependencyObject )_param1, path );
        //        propertyChangeNotifier.ValueChanged += new Action( this.OnLayoutChange );
        //        this._propertyChangeNotifierList.Add(propertyChangeNotifier );
        //    }
        //}



        //private sealed class LamdaShit003
        //{
        //    public BaseGridControl _grid0354;
        //    public int _rowHandle;

        //    internal string LDM035( GridColumn _param1 )
        //    {
        //        return this._grid0354.GetCellValue( this._rowHandle, _param1 )?.ToString();
        //    }
        //}

        private IEnumerable<GridColumn> GetVisibleGridColumns()
        {
            return this.Columns.Where<GridColumn>( c => c.Visible ).OrderBy<GridColumn, int>( x => x.VisibleIndex );
        }



        //private sealed class LDM039
        //{
        //    public static readonly BaseGridControl.LDM039 _ldm039 = new BaseGridControl.LDM039();
        //    public static Func<GridColumn, bool> _Func_GridColumn_bool;
        //    public static Func<GridColumn, int> _Func_GridColumn_int;
        //    public static Func<PrintableControlLink, CancellationToken, Task> _Func_003d;
        //    public static Func<GridColumn, object> _Func_003e3;

        //    internal bool _LDFunction034( GridColumn _param1 )
        //    {
        //        return ( ( BaseColumn )_param1 ).Visible;
        //    }

        //    internal int _LDFunction035( GridColumn _param1 )
        //    {
        //        return ( ( BaseColumn )_param1 ).VisibleIndex;
        //    }

        //    internal Task _LDFunction036( PrintableControlLink plink, CancellationToken e )
        //    {
        //        using ( MemoryStream stream = new MemoryStream() )
        //        {
        //            var options = new CsvExportOptions();
        //            options.Encoding = Encoding.UTF8;
        //            options.EncodingType = EncodingType.UTF8;
        //            options.TextExportMode = TextExportMode.Value;
        //            options.Separator = "\t";
        //            plink.ExportToCsv( stream, options );

        //            StringHelper.UTF8( stream.To<byte[ ]>() ).CopyToClipboard<string>();
        //        }
        //        return Task.CompletedTask;
        //    }

        //    internal object GetColumnHeader( GridColumn _param1 )
        //    {
        //        return ( ( BaseColumn )_param1 ).Header;
        //    }
        //}


        //private sealed class SomeLambdaClass034
        //{
        //    public ProgressReflector _ProgressReflector;
        //    public BaseGridControl.DxperPrinting _baseGridControl_LDClass035;

        //    internal void Method03431()
        //    {
        //        this._baseGridControl_LDClass035._myAdorner.SetProgressEditBarValue( this._ProgressReflector.Position );
        //    }
        //}

        ////private sealed class DxperPrinting
        //{
        //    public BaseGridControl _grid0354;
        //    public MyAdorner _myAdorner;
        //    public PrintableControlLink _pcLink;
        //    public CancellationTokenSource _cts;
        //    public AdornerLayer _AdornerLayer;
        //    public Func<PrintableControlLink, CancellationToken, Task> _function032;

        //    public Func<Task> _functionTask003;



        //    internal void Stop()
        //    {
        //        ( this._pcLink ).StopPageBuilding();
        //        this._cts.Cancel();
        //    }




        //    internal async Task AsycTaskMethod023()
        //    {
        //        try
        //        {
        //            await this._function032( this._pcLink, this._cts.Token );
        //        }
        //        catch ( OperationCanceledException ex )
        //        {
        //        }
        //        catch ( Exception ex )
        //        {
        //            LoggingHelper.LogError( ex, ( string )null );
        //        }
        //    }
        //}

        //            [StructLayout( LayoutKind.Auto )]
        //            private struct \u0023\u003DzUJV\u0024X4SjHp861Tu4pQ\u003D\u003D : IAsyncStateMachine
        //{


        //    public int \u0023\u003DzmZCvlu0uWGNm;

        //public AsyncTaskMethodBuilder \u0023\u003Dzu1QJGuMAn80M;

        //public BaseGridControl.DxperPrinting _grid0354;

        //            private TaskAwaiter \u0023\u003DzxUTTsn8LiP4f;

        //void IAsyncStateMachine.MoveNext()
        //            {
        //                int zmZcvlu0uWgNm = this.\u0023\u003DzmZCvlu0uWGNm;
        //                BaseGridControl.DxperPrinting z0sZyIb2k8Bgs = this._grid0354;
        //                try
        //                {
        //                    try
        //                    {
        //                        TaskAwaiter awaiter;
        //                        int num;
        //                        if ( zmZcvlu0uWgNm != 0 )
        //                        {
        //                            awaiter = z0sZyIb2k8Bgs._function032( z0sZyIb2k8Bgs._pcLink, z0sZyIb2k8Bgs._cts.Token ).GetAwaiter();
        //                            if ( !awaiter.IsCompleted )
        //                            {
        //                                this.\u0023\u003DzmZCvlu0uWGNm = num = 0;
        //                                this.\u0023\u003DzxUTTsn8LiP4f = awaiter;
        //                                this.\u0023\u003Dzu1QJGuMAn80M.AwaitUnsafeOnCompleted < TaskAwaiter, BaseGridControl.DxperPrinting.\u0023\u003DzUJV\u0024X4SjHp861Tu4pQ\u003D\u003D> (ref awaiter, ref this);
        //                                return;
        //                            }
        //                        }
        //                        else
        //                        {
        //                            awaiter = this.\u0023\u003DzxUTTsn8LiP4f;
        //                            this.\u0023\u003DzxUTTsn8LiP4f = new TaskAwaiter();
        //                            this.\u0023\u003DzmZCvlu0uWGNm = num = -1;
        //                        }
        //                        awaiter.GetResult();
        //                    }
        //                    catch ( OperationCanceledException ex )
        //                    {
        //                    }
        //                    catch ( Exception ex )
        //                    {
        //                        LoggingHelper.LogError( ex, ( string )null );
        //                    }
        //                }
        //                catch ( Exception ex )
        //                {
        //                    this.\u0023\u003DzmZCvlu0uWGNm = -2;
        //                    this.\u0023\u003Dzu1QJGuMAn80M.SetException( ex );
        //                    return;
        //                }
        //                this.\u0023\u003DzmZCvlu0uWGNm = -2;
        //                this.\u0023\u003Dzu1QJGuMAn80M.SetResult();
        //            }

        //            [DebuggerHidden]
        //            void IAsyncStateMachine.SetStateMachine( [Nullable( 1 )] IAsyncStateMachine _param1 )
        //            {
        //                this.\u0023\u003Dzu1QJGuMAn80M.SetStateMachine( _param1 );
        //            }
        //        }
        //    }

        private void CreatePrintableDocument( Func<PrintableControlLink, CancellationToken, Task> myFucntion )
        {
            var pcLink = new PrintableControlLink( ( IPrintableControl )this.View );
            var cts = new CancellationTokenSource();
            var myAdorner = new MyAdorner( ( UIElement )this );
            var myAdornerLayer = AdornerLayer.GetAdornerLayer( ( Visual )this );

            myAdornerLayer.Add( myAdorner );

            myAdorner.MyAdornerAction += () =>
                                                {
                                                    pcLink.StopPageBuilding();
                                                    cts.Cancel();
                                                };

            try
            {
                pcLink.PrintingSystem.ProgressReflector.PositionChanged += ( s, e ) =>
                {
                    var reflector = s as ProgressReflector;
                    if ( reflector == null )
                        return;

                    this.GuiAsync( () => { myAdorner.SetProgressEditBarValue( reflector.Position ); } );
                };
            }
            finally
            {
                pcLink.PrintingSystem.ResetProgressReflector();
            }

            pcLink.CreateDocumentFinished += ( s, e ) =>
            {
                pcLink.PrintingSystem.ProgressReflector.MaximizeRange();
                myAdornerLayer.Remove( myAdorner );
                Task.Run( async () =>
                {
                    try
                    {
                        await myFucntion( pcLink, cts.Token );
                    }
                    catch ( OperationCanceledException ex )
                    {
                    }
                    catch ( Exception ex )
                    {
                        LoggingHelper.LogError( ex, ( string )null );
                    }

                }, cts.Token );
            };
            pcLink.CreateDocument( true );
        }

        //internal void OnCreateDocumentFinished( object _param1, EventArgs e )
        //{
        //    ( ( PrintingSystemBase )( this._pcLink ).PrintingSystem ).ProgressReflector.MaximizeRange();
        //    this._AdornerLayer.Remove( ( Adorner )this._myAdorner );
        //    Task.Run( this._functionTask003 ?? ( this._functionTask003 = new Func<Task>( this.AsycTaskMethod023 ) ), this._cts.Token );
        //}

        //internal void SomeHandler0343( object _param1, EventArgs e )
        //{

        //}
        //private void ProgressReflector_PositionChanged( object sender, EventArgs e )
        //{
        //    BaseGridControl.SomeLambdaClass034 h8Of7DdosFfoedwSo = new BaseGridControl.SomeLambdaClass034();


        //    var reflector = sender as ProgressReflector;
        //    if ( reflector == null )
        //        return;

        //    this.GuiAsync( () => { } );
        //}

        //internal void Method03431()
        //{
        //    this._baseGridControl_LDClass035._myAdorner.SetProgressEditBarValue( this._ProgressReflector.Position );
        //}

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Xaml.GridControl.BaseGridControl" />.
        /// </summary>
        public BaseGridControl()
        {

            /// <summary>
            /// Automatically to scroll control on the last row added. The default is off.
            /// </summary>

            var tbView = new TableView();
            tbView.AllowEditing = false;
            tbView.AllowConditionalFormattingMenu = true;
            tbView.ShowGroupPanel = false;
            tbView.ShowIndicator = false;
            tbView.NavigationStyle = GridViewNavigationStyle.Row;
            View = tbView;


            this.IntializeGroupSummary();
            _colVisibilityHolder = new ColumnVisibilityHolder( Columns );

            _blockingQueue.Close();

            this._timer = new SimpleResettableTimer( TimeSpan.FromMilliseconds( 250.0 ) );
            this._timer.Elapsed += OnTimerElapsed;
            ClipboardCopyMode = ClipboardCopyMode.ExcludeHeader;


            CopyingToClipboard += BaseGridControl_CopyingToClipboard;

            PopupMenu popUp = new PopupMenu();

            var autoScrollCheck = new BarCheckItem();
            autoScrollCheck.Glyph = ( ThemedIconsExtension.GetImage( "UpDown" ) );
            autoScrollCheck.Content = LocalizedStrings.XamlStr17;
            autoScrollCheck.SetBindings( ( DependencyProperty )BarCheckItem.IsCheckedProperty, ( object )this, nameof( AutoScroll ), BindingMode.TwoWay, ( IValueConverter )null, ( object )null );

            popUp.Items.Add( autoScrollCheck );
            popUp.Items.Add( ( IBarItem )new BarItemSeparator() );


            var btnCopy = new BarButtonItem();
            btnCopy.Glyph = ThemedIconsExtension.GetImage( "Copy" );
            btnCopy.Content = LocalizedStrings.Copy;
            btnCopy.Command = ApplicationCommands.Copy;
            popUp.Items.Add( btnCopy );

            popUp.Items.Add( ( IBarItem )new BarItemSeparator() );

            var exportSubItem = new BarSubItem();
            exportSubItem.Glyph = ThemedIconsExtension.GetImage( "Save" );
            exportSubItem.Content = LocalizedStrings.Export;

            popUp.Items.Add( exportSubItem );



            var btnClipBoard = new BarButtonItem();
            btnClipBoard.Glyph = ( ThemedIconsExtension.GetImage( "Txt" ) );
            btnClipBoard.Content = LocalizedStrings.ClipboardCsv;
            btnClipBoard.ItemClick += OnBtnClipBoardClicked;

            exportSubItem.Items.Add( btnClipBoard );

            var btnCbImage = new BarButtonItem();
            btnCbImage.Glyph = ( ThemedIconsExtension.GetImage( "Picture" ) );
            btnCbImage.Content = ( LocalizedStrings.ClipboardImage );
            btnCbImage.ItemClick += ( s, e ) => ( ( FrameworkElement )this ).GetImage().CopyToClipboard<BitmapSource>();

            exportSubItem.Items.Add( ( IBarItem )btnCbImage );

            BarButtonItem btnCsvFile = new BarButtonItem();
            btnCsvFile.Glyph = ( ThemedIconsExtension.GetImage( "Txt" ) );
            btnCsvFile.Content = LocalizedStrings.CsvFile;
            btnCsvFile.ItemClick += BtnCsvFile_ItemClick;
            exportSubItem.Items.Add( ( IBarItem )btnCsvFile );

            var btnExcel = new BarButtonItem();
            btnExcel.Glyph = ThemedIconsExtension.GetImage( "Excel" );
            btnExcel.Content = LocalizedStrings.ExcelFile;
            btnExcel.ItemClick += BtnExcel_ItemClick; //( new ItemClickEventHandler( ( object )this, __methodptr(BtnExcel_ItemClick) ) );
            exportSubItem.Items.Add( ( IBarItem )btnExcel );


            var btnPng = new BarButtonItem();
            btnPng.Glyph = ( ThemedIconsExtension.GetImage( "Picture" ) );
            btnPng.Content = LocalizedStrings.PngFile;
            btnPng.ItemClick += BtnPng_ItemClick; //( new ItemClickEventHandler( ( object )this, __methodptr(BtnPng_ItemClick) ) );
            exportSubItem.Items.Add( ( IBarItem )btnPng );


            var btnShareImage = new BarButtonItem();
            btnShareImage.Glyph = ThemedIconsExtension.GetImage( "Share" );
            btnShareImage.Content = LocalizedStrings.ShareImage + "...";
            btnShareImage.ItemClick += BtnShareImage_ItemClick; //( new ItemClickEventHandler( ( object )this, __methodptr(BtnShareImage_ItemClick) ) );
            exportSubItem.Items.Add( ( IBarItem )btnShareImage );

            var btnCustom = new BarButtonItem();
            btnCustom.Glyph = ( ThemedIconsExtension.GetImage( "Settings" ) );
            btnCustom.Content = LocalizedStrings.CustomExportFormat;
            btnCustom.ItemClick += BtnCustom_ItemClick; //( new ItemClickEventHandler( ( object )this, __methodptr(BtnCustom_ItemClick) ) );
            exportSubItem.Items.Add( ( IBarItem )btnCustom );

            var btnDDE = new BarButtonItem();
            btnDDE.Glyph = ThemedIconsExtension.GetImage( "Table" );
            btnDDE.Content = LocalizedStrings.Dde + "...";

            btnDDE.ItemClick += BtnDDE_ItemClick; //( new ItemClickEventHandler( ( object )this, __methodptr(BtnDDE_ItemClick) ) );
            exportSubItem.Items.Add( ( IBarItem )btnDDE );
            this._popupMenu = popUp;

            BarManager.SetDXContextMenu( ( UIElement )this, ( IPopupControl )popUp );
            ( ( Control )this ).MouseDoubleClick += new MouseButtonEventHandler( this.OnMouseDoubleClicked );
        }

        private void OnMouseDoubleClicked( object _param1, MouseButtonEventArgs e )
        {
            int rowHandle = View.GetRowHandleByMouseEventArgs( e );

            if ( rowHandle == int.MinValue || IsGroupRowHandle( rowHandle ) )
            {
                return;
            }

            var column = ( GridColumn )View.GetColumnByMouseEventArgs( e );

            if ( column == null )
            {
                return;
            }

            object row = GetRow( rowHandle );

            if ( ItemDoubleClick != null )
            {
                ItemDoubleClick( this, new ItemDoubleClickEventArgs( column, row, e ) );
            }
        }

        private void BtnDDE_ItemClick( object sender, ItemClickEventArgs e )
        {
            ( ( Window )new DdeSettingsWindow()
            {
                DdeClient = this._xlsDdeClient,
                StartedAction = new Action( this.OnDDEStartAction ),
                StoppedAction = new Action( this.OnDdeStoppedAction ),
                FlushAction = new Action( this.OnDdeFlushAction )
            } ).ShowModal( ( DependencyObject )this );
        }

        private void OnDDEStartAction()
        {
            _blockingQueue.Open();
            this._done = false;
            ThreadingHelper.Launch( ThreadingHelper.Name( ThreadingHelper.Thread( new Action( this.OnDDEStartGuiAction ) ), "Grid DDE thread" ) );
            IEnumerable itemSource = this.GetItemsSourceAsIEnumerable();
            if ( itemSource == null )
                return;

            foreach ( IList<object> objectList in itemSource )
            {
                _blockingQueue.Enqueue( objectList, false );
            }
        }

        private void OnDdeStoppedAction()
        {
            _blockingQueue.Close();

            lock ( _lock )
            {
                if ( _done )
                {
                    return;
                }

                _lock.Wait( new TimeSpan?() );
            }
        }

        private void OnDdeFlushAction()
        {
            try
            {
                using ( XlsDdeClient xlsDdeClient = new XlsDdeClient( _xlsDdeClient.Settings ) )
                {
                    xlsDdeClient.Start();
                    var objectListList = new List<IList<object>>() { ( IList<object> )GetVisibleGridColumns().Select( x => x.Header ).ToList() };

                    var toBeAdded = ( IEnumerable<IList<object>> )GetItemsSourceAsIEnumerable();
                    if ( toBeAdded == null )
                    {
                        return;
                    }

                    objectListList.AddRange( toBeAdded );

                    xlsDdeClient.Poke( objectListList );
                }
            }
            catch ( Exception ex )
            {
                new MessageBoxBuilder().Error( ex ).Owner( this ).Show();
            }
        }



        private void OnDDEStartGuiAction()
        {
            try
            {
                IList<object> objectList;
                while ( _blockingQueue.TryDequeue( out objectList, true, true ) )
                {
                    this._xlsDdeClient.Poke( ( IList<IList<object>> )new IList<object>[1] { objectList } );
                }

            }
            catch ( Exception ex )
            {
                Action<Exception> err = this.DdeErrorHandler;
                if ( err != null )
                    err( ex );
            }
            lock ( this._lock )
            {
                this._done = true;
                this._lock.Pulse();
            }
        }

        /// <summary>
        /// <see cref="M:DevExpress.Xpf.Grid.DataControlBase.BeginDataUpdate" /> and <see cref="M:DevExpress.Xpf.Grid.DataControlBase.EndDataUpdate" /> auto invoke.
        ///     </summary>
        /// <param name="action">Action.</param>
        public void BeginEndUpdate( Action action )
        {
            if ( action == null )
                throw new ArgumentNullException( nameof( action ) );
            ( ( DataControlBase )this ).BeginDataUpdate();
            try
            {
                action();
            }
            finally
            {
                ( ( DataControlBase )this ).EndDataUpdate();
            }
        }



        private void BtnCustom_ItemClick( object sender, ItemClickEventArgs e )
        {
            PrintHelper.ShowRibbonPrintPreviewDialog( ( ( DependencyObject )this ).GetWindow(), new PrintableControlLink( ( IPrintableControl )this.View ) );
        }

        private void BtnShareImage_ItemClick( object sender, ItemClickEventArgs e )
        {
            var btnName = ( ( FrameworkElement )this ).Name;

            this.CreatePrintableDocument( async ( l, cts ) =>
            {
                string strName = btnName;
                if ( StringHelper.IsEmpty( strName ) )
                    strName = GetType().Name;

                await this.ShareAsync( true, strName + ".png", () =>
                {
                    MemoryStream memStream = new MemoryStream();
                    ImageExportOptions options = new ImageExportOptions();
                    options.ExportMode = ImageExportMode.SingleFilePageByPage;
                    l.ExportToImage( ( Stream )memStream, options );
                    memStream.Position = 0L;
                    return ( Stream )memStream;
                }, cts );

            } );

            //    this.CreatePrintableDocument( new Func<PrintableControlLink, CancellationToken, Task>( new BaseGridControl.SomeLambdaClass338()
            //      {
            //        _grid0354 = this,
            //_SomeLambdaClass338String = __nonvirtual(  )
            //    }.AsyncSomeLambdaClass338 ));
        }

        //private sealed class SomeLambdaClass338
        //{
        //    public string _SomeLambdaClass338String;
        //    public BaseGridControl _grid0354;

        //    internal async Task AsyncSomeLambdaClass338(
        //      PrintableControlLink l,
        //      CancellationToken e )
        //    {
        //        BaseGridControl.SomeLambdaClass339 kyyaBrpMcM1G3Rg6U = new BaseGridControl.SomeLambdaClass339();
        //        kyyaBrpMcM1G3Rg6U._pcl = l;
        //        string str = this._SomeLambdaClass338String;
        //        if ( StringHelper.IsEmpty( str ) )
        //            str = ( ( object )this._grid0354 ).GetType().Name;
        //        await ( ( DependencyObject )this._grid0354 ).ShareAsync( true, str + nameof( 2127265977 ), new Func<Stream>( kyyaBrpMcM1G3Rg6U.SomeLambdaClass339M1 ), e );
        //    }
        //}

        //private sealed class SomeLambdaClass339
        //{
        //    public PrintableControlLink _pcl;

        //    internal Stream SomeLambdaClass339M1()
        //    {
        //        MemoryStream memStream = new MemoryStream();
        //        PrintableControlLink ze3uDzoq = this._pcl;
        //        MemoryStream memoryStream2 = memStream;
        //        ImageExportOptions options = new ImageExportOptions();
        //        options.set_ExportMode( ( ImageExportMode )1 );
        //        ( ( LinkBase )ze3uDzoq ).ExportToImage( ( Stream )memoryStream2, options );
        //        memStream.Position = 0L;
        //        return ( Stream )memStream;
        //    }
        //}



        private void BtnPng_ItemClick( object sender, RoutedEventArgs e )
        {
            var dialog = new DXSaveFileDialog();

            dialog.RestoreDirectory = true;
            dialog.Filter = "Image files (*.png)|*.png|All files (*.*)|*.*";
            dialog.DefaultExt = "png";

            if ( !dialog.ShowModal( this ) )
            {
                return;
            }

            this.CreatePrintableDocument( ( l, cts ) =>
                                                            {
                                                                string fileName = dialog.FileName;
                                                                ImageExportOptions option = new ImageExportOptions();
                                                                option.ExportMode = ImageExportMode.DifferentFiles;
                                                                l.ExportToImage( fileName, option );
                                                                ShowSomeMsgBoxes( fileName );
                                                                return Task.CompletedTask;
                                                            } );

            //BaseGridControl.\u0023\u003DzLVAnrYKQ9bWybKC02MByUbI\u003D q9bWybKc02MbyUbI1 = new BaseGridControl.\u0023\u003DzLVAnrYKQ9bWybKC02MByUbI\u003D();
            //q9bWybKc02MbyUbI1._grid0354 = this;
            //BaseGridControl.\u0023\u003DzLVAnrYKQ9bWybKC02MByUbI\u003D q9bWybKc02MbyUbI2 = q9bWybKc02MbyUbI1;
            //DXSaveFileDialog dialog = new DXSaveFileDialog();
            //( ( DXFileDialog )dialog ).set_RestoreDirectory( true );
            //( ( DXFileDialog )dialog ).set_Filter( nameof( 2127273578 ) );
            //( ( DXFileDialog )dialog ).set_DefaultExt( nameof( 2127273534 ) );
            //q9bWybKc02MbyUbI2._saveFileDialog = dialog;
            //if ( !( ( CommonDialog )q9bWybKc02MbyUbI1._saveFileDialog ).ShowModal( ( DependencyObject )this ) )
            //    return;
            //this.CreatePrintableDocument( new Func<PrintableControlLink, CancellationToken, Task>( q9bWybKc02MbyUbI1.\u0023\u003Dzgz6TI5PWIs7z9Qb6JxCWH4s\u003D) );
        }

        //        private sealed class \u0023\u003DzLVAnrYKQ9bWybKC02MByUbI\u003D
        //    {
        //      public DXSaveFileDialog _saveFileDialog;
        //        public BaseGridControl _grid0354;

        //        internal Task \u0023\u003Dzgz6TI5PWIs7z9Qb6JxCWH4s\u003D(
        //          PrintableControlLink l,
        //          CancellationToken e)
        //      {
        //        PrintableControlLink printableControlLink = l;
        //        string fileName = ( ( DXFileDialog )this._saveFileDialog ).get_FileName();
        //        ImageExportOptions options = new ImageExportOptions();
        //        options.set_ExportMode((ImageExportMode) 2);
        //        ((LinkBase) printableControlLink).ExportToImage( fileName, options);
        //        this._grid0354.ShowSomeMsgBoxes(((DXFileDialog) this._saveFileDialog).get_FileName());
        //        return Task.CompletedTask;
        //      }
        //}


        private void BtnCsvFile_ItemClick( object _param1, RoutedEventArgs e )
        {
            var dialog = new DXSaveFileDialog();

            dialog.RestoreDirectory = true;
            dialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            dialog.DefaultExt = "csv";

            if ( !dialog.ShowModal( this ) )
            {
                return;
            }

            this.CreatePrintableDocument( ( l, cts ) =>
            {
                string fileName = dialog.FileName;
                CsvExportOptions options = new CsvExportOptions();
                options.Encoding = Encoding.UTF8;
                options.EncodingType = EncodingType.UTF8;
                options.TextExportMode = TextExportMode.Value;
                options.Separator = "\t";
                l.ExportToCsv( fileName, options );
                ShowSomeMsgBoxes( dialog.FileName );
                return Task.CompletedTask;
            } );
        }

        private void BtnExcel_ItemClick( object sender, RoutedEventArgs e )
        {
            var dialog = new DXSaveFileDialog();

            dialog.RestoreDirectory = true;
            dialog.Filter = "xlsx files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            dialog.DefaultExt = "xlsx";

            if ( !dialog.ShowModal( this ) )
            {
                return;
            }

            this.CreatePrintableDocument( ( l, cts ) =>
            {

                string fileName = dialog.FileName;
                var option = new XlsxExportOptions();
                option.TextExportMode = TextExportMode.Text;
                l.ExportToXlsx( fileName, option );
                ShowSomeMsgBoxes( fileName );
                return Task.CompletedTask;
            } );
        }

        private void OnBtnClipBoardClicked( object _param1, RoutedEventArgs e )
        {
            this.CreatePrintableDocument( ( l, cts ) =>
            {
                using ( MemoryStream stream = new MemoryStream() )
                {
                    var options = new CsvExportOptions();
                    options.Encoding = Encoding.UTF8;
                    options.EncodingType = EncodingType.UTF8;
                    options.TextExportMode = TextExportMode.Value;
                    options.Separator = "\t";
                    l.ExportToCsv( stream, options );

                    StringHelper.UTF8( stream.To<byte[ ]>() ).CopyToClipboard<string>();
                }
                return Task.CompletedTask;

            } );

            //this.CreatePrintableDocument( BaseGridControl.LDM039._Func_003d ?? ( BaseGridControl.LDM039._Func_003d = new Func<PrintableControlLink, CancellationToken, Task>( BaseGridControl.LDM039._ldm039._LDFunction036 ) ) );
        }

        //internal Task _LDFunction036( PrintableControlLink plink, CancellationToken e )
        //{
        //    using ( MemoryStream stream = new MemoryStream() )
        //    {
        //        var options = new CsvExportOptions();
        //        options.Encoding = Encoding.UTF8;
        //        options.EncodingType = EncodingType.UTF8;
        //        options.TextExportMode = TextExportMode.Value;
        //        options.Separator = "\t";
        //        plink.ExportToCsv( stream, options );

        //        StringHelper.UTF8( stream.To<byte[ ]>() ).CopyToClipboard<string>();
        //    }
        //    return Task.CompletedTask;
        //}


        //        private sealed class \u0023\u003Dz8FIHNhTrdDvPAJPPgZlVHYg\u003D
        //    {
        //      public DXSaveFileDialog _saveFileDialog;
        //        public BaseGridControl _grid0354;

        //        internal Task \u0023\u003DzztyQmdWqBXPfmkqGggF8itU\u003D(
        //          PrintableControlLink l,
        //          CancellationToken cts)
        //      {
        //        PrintableControlLink printableControlLink = l;
        //        string fileName = ( ( DXFileDialog )this._saveFileDialog ).get_FileName();
        //        XlsxExportOptions xlsxExportOptions = new XlsxExportOptions();
        //        ((XlExportOptionsBase) xlsxExportOptions).set_TextExportMode( (TextExportMode) 1);
        //        ((LinkBase) printableControlLink).ExportToXlsx( fileName, xlsxExportOptions);
        //        this._grid0354.ShowSomeMsgBoxes(((DXFileDialog) this._saveFileDialog).get_FileName());
        //        return Task.CompletedTask;
        //      }
        //}

        //private sealed class SomeLambdaClass037
        //{
        //    public DXSaveFileDialog _saveFileDialog;
        //    public BaseGridControl _grid0354;

        //    internal Task SomeInternalTask(
        //      PrintableControlLink l,
        //      CancellationToken cts )
        //    {

        //        string fileName = ( ( DXFileDialog )this._saveFileDialog ).FileName;
        //        CsvExportOptions options = new CsvExportOptions();
        //        options.Encoding = Encoding.UTF8;
        //        options.EncodingType = EncodingType.UTF8;
        //        options.TextExportMode = TextExportMode.Value;
        //        options.Separator = "\t";
        //        l.ExportToCsv( fileName, options );
        //        this._grid0354.ShowSomeMsgBoxes( ( ( DXFileDialog )this._saveFileDialog ).FileName );
        //        return Task.CompletedTask;
        //    }
        //}

        /// <summary>Called when the ItemsSource property changes.</summary>
        /// <param name="oldValue">Old value of the ItemsSource property.</param>
        /// <param name="newValue">New value of the ItemsSource property.</param>
        protected override void OnItemsSourceChanged( object oldValue, object newValue )
        {
            INotifyCollectionChanged collectionChanged1 = oldValue as INotifyCollectionChanged;
            if ( collectionChanged1 != null )
                collectionChanged1.CollectionChanged -= new NotifyCollectionChangedEventHandler( this.OnItemSourceChangedHandler );
            INotifyCollectionChanged collectionChanged2 = newValue as INotifyCollectionChanged;
            if ( collectionChanged2 != null )
                collectionChanged2.CollectionChanged += new NotifyCollectionChangedEventHandler( this.OnItemSourceChangedHandler );
            base.OnItemsSourceChanged( oldValue, newValue );
            this.LoadExpandedState();
        }

        private void OnItemSourceChangedHandler( object sender, NotifyCollectionChangedEventArgs e )
        {
            switch ( e.Action )
            {
                case NotifyCollectionChangedAction.Add:
                {
                    if ( e.NewItems == null )
                        break;

                    if ( this.AutoScroll )
                        this._timer.Reset();

                    if ( _blockingQueue.IsClosed )
                        break;

                    foreach ( IList<object> item in e.NewItems )
                    {
                        _blockingQueue.Enqueue( item, false );
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
                throw new ArgumentOutOfRangeException();
            }
        }

        //      private IEnumerable<IList<object>> SomeIteratorShit003(
        //IEnumerable _param1,
        //int e )
        //      {
        //          return ( IEnumerable<IList<object>> )new BaseGridControl.\u0023\u003Dz9KeQ\u0024p4jvCE_J5pbWA\u003D\u003D( -2 )
        //    {
        //              _grid0354 = this,
        //      \u0023\u003Dz236F1OGbWurB = _param1,
        //      \u0023\u003DzijwA9XI9nVCafCw4ZA\u003D\u003D = e
        //    };
        //      }




        private void ShowSomeMsgBoxes( string fileName )
        {
            if ( StringHelper.IsEmpty( fileName ) || !File.Exists( fileName ) )
                return;

            if ( new MessageBoxBuilder().Owner( ( DependencyObject )this ).Caption( LocalizedStrings.Export ).Text( StringHelper.Put( LocalizedStrings.ExportDoneOpenFile, Path.GetFileName( fileName ) ) ).YesNo().Show() != MessageBoxResult.Yes )
                return;

            fileName.TryOpenLink( ( DependencyObject )this );
        }

        private IEnumerable GetItemsSourceAsIEnumerable()
        {
            return ( IEnumerable )( ( DataControlBase )this ).ItemsSource;
        }

        /// <summary>Get or set column visibility by index.</summary>
        public BaseGridControl.ColumnVisibilityHolder ColumnVisibility
        {
            get
            {
                return this._colVisibilityHolder;
            }
        }
    }
}


























        //private IList<object> \u0023\u003DzTYlNDajxAT3_EEjpOA\u003D\u003D( int _param1)
        //    {
        //    return this.GetCellValueByHandle( this.GetRowHandleByListIndex( _param1 ) );
        //}









































        //private sealed class \u0023\u003Dz9KeQ\u0024p4jvCE_J5pbWA\u003D\u003D : IEnumerable<IList<object>>, IEnumerable, IEnumerator<IList<object>>, IEnumerator, IDisposable
        //{
        //    
        //    private int \u0023\u003DzmZCvlu0uWGNm;
        //    
        //    private IList<object> \u0023\u003DzWvH5n6COaRvj;
        //    
        //    private int \u0023\u003DzvtNL1OGWjIFr14kY\u0024A\u003D\u003D;
        //    
        //    private int \u0023\u003Dza\u0024ljNMk\u003D;
        //    
        //    public int \u0023\u003DzijwA9XI9nVCafCw4ZA\u003D\u003D;
        //    
        //    private IEnumerable \u0023\u003Dz0Y9uvok\u003D;
        //    
        //    public IEnumerable \u0023\u003Dz236F1OGbWurB;
        //    
        //    public BaseGridControl _grid0354;
        //    
        //    private int \u0023\u003Dz_wG_0kRALzEKrdUdZA\u003D\u003D;
        //    
        //    private IEnumerator \u0023\u003DzOMXhHjIpnhb5kQB7Lg\u003D\u003D;

        //    [DebuggerHidden]
        //    public \u0023\u003Dz9KeQ\u0024p4jvCE_J5pbWA\u003D\u003D( int _param1)
        //      {
        //        this.\u0023\u003DzmZCvlu0uWGNm = _param1;
        //        this.\u0023\u003DzvtNL1OGWjIFr14kY\u0024A\u003D\u003D = Environment.CurrentManagedThreadId;
        //    }

        //    [DebuggerHidden]
        //    void IDisposable.\u0023\u003DzkQukXWEgNG41rF31Rw\u003D\u003D()
        //      {
        //        switch ( this.\u0023\u003DzmZCvlu0uWGNm)
        //        {
        //          case -3:
        //          case 1:
        //            try
        //            {
        //            }
        //            finally
        //            {
        //                this.\u0023\u003DzpscbbenJpKdpEDtciA\u003D\u003D();
        //            }
        //            break;
        //        }
        //    }

        //    bool IEnumerator.MoveNext()
        //      {
        //        // ISSUE: fault handler
        //        try
        //        {
        //            int zmZcvlu0uWgNm = this.\u0023\u003DzmZCvlu0uWGNm;
        //            BaseGridControl z0sZyIb2k8Bgs = this._grid0354;
        //            switch ( zmZcvlu0uWgNm )
        //            {
        //                case 0:
        //                this.\u0023\u003DzmZCvlu0uWGNm = -1;
        //                this.\u0023\u003Dz_wG_0kRALzEKrdUdZA\u003D\u003D = this.\u0023\u003Dza\u0024ljNMk\u003D;
        //                this.\u0023\u003DzOMXhHjIpnhb5kQB7Lg\u003D\u003D = this.\u0023\u003Dz0Y9uvok\u003D.GetEnumerator();
        //                this.\u0023\u003DzmZCvlu0uWGNm = -3;
        //                break;
        //                case 1:
        //                this.\u0023\u003DzmZCvlu0uWGNm = -3;
        //                ++this.\u0023\u003Dz_wG_0kRALzEKrdUdZA\u003D\u003D;
        //                break;
        //                default:
        //                return false;
        //            }
        //            if ( this.\u0023\u003DzOMXhHjIpnhb5kQB7Lg\u003D\u003D.MoveNext())
        //          {
        //                object current = this.\u0023\u003DzOMXhHjIpnhb5kQB7Lg\u003D\u003D.Current;
        //                this.\u0023\u003DzWvH5n6COaRvj = z0sZyIb2k8Bgs.\u0023\u003DzTYlNDajxAT3_EEjpOA\u003D\u003D( this.\u0023\u003Dz_wG_0kRALzEKrdUdZA\u003D\u003D);
        //                this.\u0023\u003DzmZCvlu0uWGNm = 1;
        //                return true;
        //            }
        //            this.\u0023\u003DzpscbbenJpKdpEDtciA\u003D\u003D();
        //            this.\u0023\u003DzOMXhHjIpnhb5kQB7Lg\u003D\u003D = ( IEnumerator )null;
        //            return false;
        //        }
        //        __fault
        //        {
        //            this.\u0023\u003DzkQukXWEgNG41rF31Rw\u003D\u003D();
        //        }
        //    }

        //      private void \u0023\u003DzpscbbenJpKdpEDtciA\u003D\u003D()
        //      {
        //    this.\u0023\u003DzmZCvlu0uWGNm = -1;
        //        (this.\u0023\u003DzOMXhHjIpnhb5kQB7Lg\u003D\u003D as IDisposable)?.Dispose();
        //      }

        //      [DebuggerHidden]
        //      IList<object> IEnumerator<IList<object>>.\u0023\u003DzZv\u0024nMF2ox_u8CYUiDickT31jhBOfp5\u0024M2\u0024eObenesSl0UFasZA\u003D\u003D()
        //      {
        //        return this.\u0023\u003DzWvH5n6COaRvj;
        //      }

        //      [DebuggerHidden]
        //      void IEnumerator.\u0023\u003DzPz2CkL3H4EO2s8Al1w\u003D\u003D()
        //      {
        //        throw new NotSupportedException();
        //      }

        //      [DebuggerHidden]
        //      object IEnumerator.\u0023\u003DzUvRtPry9Z3_r4VkfO99sKo4\u003D()
        //      {
        //        return (object) this.\u0023\u003DzWvH5n6COaRvj;
        //      }

        //      [DebuggerHidden]
        //      [return: Nullable(new byte[] {1, 0, 0})]
        //      IEnumerator<IList<object>> IEnumerable<IList<object>>.\u0023\u003DzDsGkXUmbR7uEVj9ybXkIz3YnM3F3_cr4UlQAn6sYYUtMjL7uQQ\u003D\u003D()
        //      {
        //        BaseGridControl.\u0023\u003Dz9KeQ\u0024p4jvCE_J5pbWA\u003D\u003D z9KeQP4jvCeJ5pbWa;
        //        if (this.\u0023\u003DzmZCvlu0uWGNm == -2 && this.\u0023\u003DzvtNL1OGWjIFr14kY\u0024A\u003D\u003D == Environment.CurrentManagedThreadId)
        //        {
        //          this.\u0023\u003DzmZCvlu0uWGNm = 0;
        //          z9KeQP4jvCeJ5pbWa = this;
        //        }
        //        else
        //        {
        //          z9KeQP4jvCeJ5pbWa = new BaseGridControl.\u0023\u003Dz9KeQ\u0024p4jvCE_J5pbWA\u003D\u003D(0);
        //          z9KeQP4jvCeJ5pbWa._grid0354 = this._grid0354;
        //        }
        //        z9KeQP4jvCeJ5pbWa.\u0023\u003Dz0Y9uvok\u003D = this.\u0023\u003Dz236F1OGbWurB;
        //        z9KeQP4jvCeJ5pbWa.\u0023\u003Dza\u0024ljNMk\u003D = this.\u0023\u003DzijwA9XI9nVCafCw4ZA\u003D\u003D;
        //        return (IEnumerator<IList<object>>) z9KeQP4jvCeJ5pbWa;
        //      }

        //      [DebuggerHidden]
        //      [return: Nullable(1)]
        //      IEnumerator IEnumerable.\u0023\u003DzGBBKW4936O1cYMySvZ\u0024sDvw\u003D()
        //      {
        //        return (IEnumerator) this.\u0023\u003DzDsGkXUmbR7uEVj9ybXkIz3YnM3F3_cr4UlQAn6sYYUtMjL7uQQ\u003D\u003D();
        //      }
        //    }










        //private sealed class \u0023\u003Dzk_UQUHQ_B6RAzwbB5oucS8U\u003D
        //{
        //  public GridColumn \u0023\u003DzEQIvO\u0024M\u003D;

        //  internal bool \u0023\u003Dz5KPMpg_rJYogAhFX5w\u003D\u003D(PropertyChangeNotifier _param1)
        //  {
        //    return object.Equals((object) _param1.PropertySource, (object) this.\u0023\u003DzEQIvO\u0024M\u003D);
        //  }
        //}





    

