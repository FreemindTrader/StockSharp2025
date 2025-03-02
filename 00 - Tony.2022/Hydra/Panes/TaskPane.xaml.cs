// Decompiled with JetBrains decompiler
// Type: StockSharp.Hydra.Panes.TaskPane
// Assembly: Hydra, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3BCB1262-CD24-4283-B1AE-24E756F47247
// Assembly location: T:\00-StockSharp\Data\Hydra.dll

using DevExpress.Data;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.TreeList;
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using Ecng.Xaml;
using MoreLinq;
using StockSharp.Algo;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Hydra.Controls;
using StockSharp.Hydra.Core;
using StockSharp.Hydra.Windows;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using StockSharp.Studio.Controls;
using StockSharp.Studio.Core;
using StockSharp.Xaml;
using StockSharp.Xaml.GridControl;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Threading;

namespace StockSharp.Hydra.Panes
{
    public partial class TaskPane : BaseStudioControl, IPane, IStudioControl, IPersistable, IDisposable, INotifyPropertyChanged, IComponentConnector
    {
        public static RoutedUICommand OpenDirectoryCommand = new RoutedUICommand();
        public static RoutedUICommand OpenDataCommand = new RoutedUICommand();
        public static RoutedUICommand ExpandCollapseCommand = new RoutedUICommand();
        public static RoutedUICommand EnableTaskCommand = new RoutedUICommand();
        public static Messages.DataType Custom = Messages.DataType.Create( typeof( TaskPane ), null ).Immutable();
        public static readonly DependencyProperty TaskProperty = DependencyProperty.Register( "Task", typeof( IHydraTask ), typeof( TaskPane ), new PropertyMetadata( null, delegate ( DependencyObject o, DependencyPropertyChangedEventArgs args )
        {
            TaskPane taskPane = ( TaskPane )o;
            IHydraTask hydraTask = ( IHydraTask )args.NewValue;
            IHydraTask task = taskPane._task;
            HydraTaskManager manager = Manager;

            if ( task != null )
            {
                task.PropertyChanged -= taskPane.TaskOnPropertyChanged;
                task.Started -= taskPane.OnStartedTask;
                task.Stopped -= taskPane.OnStoppedTask;
                manager.SecuritiesAdded -= taskPane.OnSecuritiesAdded;
                manager.SecuritiesRemoved -= taskPane.OnSecuritiesRemoved;
            }
            taskPane._task = hydraTask;
            taskPane.DataContext = hydraTask;
            taskPane.SettingsPanel.Task = hydraTask;

            if ( hydraTask == null )
            {
                taskPane._dataTypes.Keys.ForEach(
                                                    delegate ( CheckBox cb )
                                                    {
                                                        cb.IsEnabled = false;
                                                    }
                                                );
                taskPane.CandlesPanel.IsEnabled = false;
                return;
            }

            hydraTask.PropertyChanged += taskPane.TaskOnPropertyChanged;
            hydraTask.Started += taskPane.OnStartedTask;
            hydraTask.Stopped += taskPane.OnStoppedTask;
            manager.SecuritiesAdded += taskPane.OnSecuritiesAdded;
            manager.SecuritiesRemoved += taskPane.OnSecuritiesRemoved;
            taskPane.AddAllSecurity.Content = TraderHelper.AllSecurity.Id;

            taskPane.RefreshEnabledDataTypes( hydraTask );
        } ) );

        
        private readonly HashSet<HydraTaskSecurity> _securitiesToAdd = new HashSet<HydraTaskSecurity>();
        private readonly HydraSecurityTrie _allSecurities = new HydraSecurityTrie();
        private readonly ObservableCollection<TaskVisualSecurity> _filteredSecurities = new ObservableCollection<TaskVisualSecurity>();
        
        private readonly Dictionary<CheckBox, (Messages.DataType dataType, Func<TaskVisualSecurity, bool> getIsEnabled, Action<TaskVisualSecurity, bool> setIsEnabled )> _dataTypes = new Dictionary<CheckBox, ValueTuple<Messages.DataType, Func<TaskVisualSecurity, bool>, Action<TaskVisualSecurity, bool>>>();
        private readonly PairSet<Tuple<HydraTaskSecurity, Messages.DataType>, TaskVisualSecurity> _visualSecurities = new PairSet<Tuple<HydraTaskSecurity, Messages.DataType>, TaskVisualSecurity>();
        private readonly TaskVisualSecurityComparer _taskVisualSecurityComparer = new TaskVisualSecurityComparer();
        private readonly DelayActionHelper _delayActionHelper = new DelayActionHelper() { Interval = TimeSpan.FromSeconds( 2.0 ).TotalMilliseconds };
        private IHydraTask _task;
        private DeferredUIAction _secAddedAction;
        private bool _isInitialized;
        private bool _suspendTimeFrames;
        private bool _initFromSelect;
        private const string _taskIdKey = "TaskId";
        private const string _securitiesKey = "Securities";
        private const string _layoutKey = "Layout";
        private PropertyChangedEventHandler _propertyChanged;
        
        public IHydraTask Task
        {
            get
            {
                return _task;
            }
            set
            {
                SetValue( TaskProperty, value );
                if ( value == null )
                {
                    Title = string.Empty;
                    Icon = null;
                    Key = null;
                    SaveWithLayout = false;
                }
                else
                {
                    Title = Task.Title;
                    Icon = Task.Icon;
                    Key = string.Format( "_{0:N}", Task.Id );
                    SaveWithLayout = true;
                }
            }
        }

        private static HydraTaskManager Manager
        {
            get
            {
                return HydraTaskManager.Instance;
            }
        }

        private void OnStartedTask( IHydraTask task )
        {
            this.GuiAsync( () => EnableControls( false ) );
        }

        private void OnStoppedTask( IHydraTask task )
        {
            this.GuiAsync( () => EnableControls( true ) );
        }

        private void EnableControls( bool isEnabled )
        {
            ControlPanel.IsEnabled = isEnabled;
            SettingsPanel.IsEnabled = isEnabled;
            BeginDateColumn.IsEnabled = isEnabled;
            EndDateColumn.IsEnabled = isEnabled;
        }

        private void TaskOnPropertyChanged( object sender, PropertyChangedEventArgs e )
        {
            string propertyName = e.PropertyName;
            if ( !( propertyName == "Title" ) )
            {
                if ( !( propertyName == "IsEnabled" ) && !( propertyName == "SupportedDataTypes" ) && !( propertyName == "SupportedDepths" ) )
                    return;
                RefreshEnabledDataTypes( Task );
            }
            else
            {
                Title = Task.Title;
                PropertyChangedEventHandler propertyChanged = _propertyChanged;
                if ( propertyChanged == null )
                    return;
                propertyChanged.Invoke( this, e.PropertyName );
            }
        }

        private void RefreshEnabledDataTypes( IHydraTask task )
        {
            ISet<Messages.DataType> set = task.SupportedDataTypes.ToSet();
            bool flag1 = set.Any( dt => dt.IsCandleSource );
            ObservableCollection<SelectableObject> source = new ObservableCollection<SelectableObject>( set.Where( t =>
            {
                if ( t.MessageType == typeof( TimeFrameCandleMessage ) )
                    return t.Arg != null;
                return false;
            } ).Select( s => new SelectableObject( s ) ) );
            bool flag2 = source.Count > 0;
            if ( flag1 )
            {
                if ( !flag2 )
                    source.AddRange( Core.Extensions.GeneratedTimeFrames.Select( s => new SelectableObject( s ) ) );
                source.Add( new SelectableObject( Custom ) );
            }
            TimeFrames.ItemsSource = source;
            CandlesBuildFrom.SetItemsSource( task.CandlesBuildFrom, null, null );
            CandlesPanel.IsEnabled = flag2 | flag1;
            int[ ] array1 = task.SupportedDepths.ToSet().OrderBy( d => d ).ToArray();
            MaxDepthCb.SetItemsSource( array1, null, null );
            MaxDepthCb.IsEnabled = !array1.IsEmpty();
            VolProfileCb.IsEnabled = false;
            foreach ( KeyValuePair<CheckBox, ValueTuple<Messages.DataType, Func<TaskVisualSecurity, bool>, Action<TaskVisualSecurity, bool>>> dataType1 in _dataTypes )
            {
                CheckBox key;
                ValueTuple<Messages.DataType, Func<TaskVisualSecurity, bool>, Action<TaskVisualSecurity, bool>> valueTuple1;
                dataType1.Deconstruct( out key, out valueTuple1 );
                ValueTuple<Messages.DataType, Func<TaskVisualSecurity, bool>, Action<TaskVisualSecurity, bool>> valueTuple2 = valueTuple1;
                CheckBox checkBox = key;
                Messages.DataType dataType2 = valueTuple2.Item1;
                bool[ ] array2 = SelectedRoots2.Select( valueTuple2.Item2 ).ToArray();
                bool flag3 = array2.Any( s => s );
                checkBox.IsEnabled = set.Contains( dataType2 ) | flag3;
                if ( !checkBox.IsEnabled )
                {
                    checkBox.IsThreeState = false;
                    checkBox.IsChecked = new bool?( false );
                }
                else
                {
                    checkBox.Click -= new RoutedEventHandler( CheckBoxClick );
                    try
                    {
                        bool flag4 = array2.Contains( true );
                        bool flag5 = array2.Contains( false );
                        checkBox.IsThreeState = flag4 == flag5 & flag4;
                        checkBox.IsChecked = checkBox.IsThreeState ? new bool?() : new bool?( flag4 );
                    }
                    finally
                    {
                        checkBox.Click += new RoutedEventHandler( CheckBoxClick );
                    }
                }
            }
        }

        private void OnSecuritiesAdded( IHydraTask task, IEnumerable<HydraTaskSecurity> securities )
        {
            if ( task != Task )
                return;
            lock ( _securitiesToAdd )
            {
                if ( _secAddedAction == null )
                    _secAddedAction = new DeferredUIAction( new Action( OnSecuritiesAdded ), TimeSpan.FromMilliseconds( 500.0 ) );
                _securitiesToAdd.AddRange( securities );
            }
            _secAddedAction.Execute();
        }

        private void OnSecuritiesAdded()
        {
            HydraTaskSecurity[ ] securities;
            lock ( _securitiesToAdd )
                securities = _securitiesToAdd.CopyAndClear();
            SecuritiesCtrl.BeginEndUpdate( () =>
            {
                foreach ( TaskVisualSecurity taskVisualSecurity in ToVisual( securities ) )
                {
                    Messages.DataType dataType = taskVisualSecurity.DataType;
                    if ( ( dataType != null ? ( dataType.IsCandles ? 1 : 0 ) : 0 ) != 0 )
                    {
                        IConnectorHydraTask task = Task as IConnectorHydraTask;
                        if ( task != null && task.CandlesFromDate.HasValue )
                            taskVisualSecurity.BeginDate = task.CandlesFromDate;
                    }
                    _filteredSecurities.Add( taskVisualSecurity );
                }
            } );
            _allSecurities.AddRange( securities );
            AddAllSecurity.IsEnabled = _allSecurities.GetAllSecurity() == null;
            TreeListView.ExpandAllNodes();
        }

        private void OnSecuritiesRemoved( IHydraTask task, IEnumerable<HydraTaskSecurity> securities, bool isAll )
        {
            if ( task != Task )
                return;
            if ( !Dispatcher.CheckAccess() )
                this.GuiAsync( () => OnSecuritiesRemoved( task, securities, isAll ) );
            else
                SecuritiesCtrl.BeginEndUpdate( () =>
                {
                    if ( isAll )
                    {
                        _filteredSecurities.Clear();
                        _allSecurities.Clear();
                    }
                    else
                    {
                        securities = securities.ToArray();
                        TaskVisualSecurity[ ] array = ToVisual( securities ).ToArray();
                        _filteredSecurities.RemoveRange( array );
                        foreach ( TaskVisualSecurity taskVisualSecurity in array )
                            taskVisualSecurity.Parent?.Childs.Remove( taskVisualSecurity );
                        _allSecurities.RemoveRange( securities );
                    }
                } );
        }

        private static IEntityRegistry EntityRegistry
        {
            get
            {
                return ServicesRegistry.EntityRegistry;
            }
        }

        private IEnumerable<TaskVisualSecurity> ToVisual(
          IEnumerable<HydraTaskSecurity> securities )
        {
            return securities.SelectMany( new Func<HydraTaskSecurity, IEnumerable<TaskVisualSecurity>>( ToVisual ) );
        }

        private IEnumerable<TaskVisualSecurity> ToVisual( HydraTaskSecurity security )
        {
            TaskVisualSecurity parent = ToVisual( security, null, null );
            yield return parent;
            foreach ( Messages.DataType dataType in security.GetDataTypes() )
                yield return ToVisual( security, parent, dataType );
        }

        private TaskVisualSecurity ToVisual( HydraTaskSecurity security, TaskVisualSecurity parent, Messages.DataType dataType )
        {
            if ( security == null )
                throw new ArgumentNullException( nameof( security ) );
            return _visualSecurities.SafeAdd( Tuple.Create( security, dataType ), key => new TaskVisualSecurity( this, key.Item1, parent, key.Item2 ) );
        }

        public TaskPane()
        {
            InitializeComponent();
            SecuritiesCtrl.ItemsSource = _filteredSecurities;
            CommonBarItemCollection items1 = SecuritiesCtrl.PopupMenu.Items;
            BarButtonItem barButtonItem1 = new BarButtonItem();
            barButtonItem1.Glyph = ThemedIconsExtension.GetImage( "Open" );
            barButtonItem1.Content = LocalizedStrings.Str2916;
            barButtonItem1.Command = OpenDirectoryCommand;
            items1.Add( barButtonItem1 );
            CommonBarItemCollection items2 = SecuritiesCtrl.PopupMenu.Items;
            BarButtonItem barButtonItem2 = new BarButtonItem();
            barButtonItem2.Glyph = ThemedIconsExtension.GetImage( "Search" );
            barButtonItem2.Content = LocalizedStrings.ViewDownloaded;
            barButtonItem2.Command = OpenDataCommand;
            items2.Add( barButtonItem2 );
            CommonBarItemCollection items3 = SecuritiesCtrl.PopupMenu.Items;
            BarButtonItem barButtonItem3 = new BarButtonItem();
            barButtonItem3.Glyph = ThemedIconsExtension.GetImage( "Expand" );
            barButtonItem3.Content = LocalizedStrings.ExpandAll;
            barButtonItem3.Command = ExpandCollapseCommand;
            barButtonItem3.CommandParameter = true;
            items3.Add( barButtonItem3 );
            CommonBarItemCollection items4 = SecuritiesCtrl.PopupMenu.Items;
            BarButtonItem barButtonItem4 = new BarButtonItem();
            barButtonItem4.Glyph = ThemedIconsExtension.GetImage( "Collapse" );
            barButtonItem4.Content = LocalizedStrings.CollapseAll;
            barButtonItem4.Command = ExpandCollapseCommand;
            barButtonItem4.CommandParameter = false;
            items4.Add( barButtonItem4 );
            AddDataType( Ticks, Messages.DataType.Ticks );
            AddDataType( Depths, Messages.DataType.MarketDepth );
            AddDataType( Level1, Messages.DataType.Level1 );
            AddDataType( OrderLog, Messages.DataType.OrderLog );
            AddDataType( Transactions, Messages.DataType.Transactions );
            AddDataType( News, Messages.DataType.News );
            AddDataType( Positions, Messages.DataType.PositionChanges );
        }

        private void AddDataType( CheckBox checkBox, Messages.DataType type )
        {
            if ( checkBox == null )
                throw new ArgumentNullException( nameof( checkBox ) );
            if ( type == null )
                throw new ArgumentNullException( nameof( type ) );
            Func<TaskVisualSecurity, bool> get;
            Action<TaskVisualSecurity, bool> set;
            TaskVisualSecurity.CreateGetSet( type, out get, out set );
            _dataTypes.Add( checkBox, new ValueTuple<Messages.DataType, Func<TaskVisualSecurity, bool>, Action<TaskVisualSecurity, bool>>( type, get, set ) );
        }

        private void TaskPane_OnLoaded( object sender, RoutedEventArgs e )
        {
            if ( _isInitialized )
                return;
            _isInitialized = true;
            _allSecurities.Clear();
            _filteredSecurities.Clear();
            if ( Task == null )
            {
                ControlPanel.IsEnabled = false;
            }
            else
            {
                Mouse.OverrideCursor = Cursors.Wait;
                try
                {
                    _allSecurities.AddRange( Task.Securities );
                    SecuritiesCtrl.BeginEndUpdate( () => _filteredSecurities.AddRange( ToVisual( _allSecurities.RetrieveHydra( string.Empty ) ) ) );
                    TreeListView.ExpandAllNodes();
                    AddAllSecurity.IsEnabled = _allSecurities.GetAllSecurity() == null;
                    ControlPanel.IsEnabled = true;
                }
                finally
                {
                    Mouse.OverrideCursor = null;
                }
            }
        }

        private TaskVisualSecurity SelectedSecurity
        {
            get
            {
                return SelectedSecurities.FirstOrDefault();
            }
        }

        private TaskVisualSecurity[ ] SelectedSecurities
        {
            get
            {
                return SecuritiesCtrl.SelectedItems.Cast<TaskVisualSecurity>().ToArray();
            }
        }

        private TaskVisualSecurity[ ] SelectedRoots
        {
            get
            {
                return SelectedSecurities.Where( s => s.Parent == null ).ToArray();
            }
        }

        private TaskVisualSecurity[ ] SelectedRoots2
        {
            get
            {
                return SelectedSecurities.Select( s => s.Parent ?? s ).Distinct().ToArray();
            }
        }

        private void OpenDirectoryCommand_Executed( object sender, ExecutedRoutedEventArgs e )
        {
            TaskVisualSecurity selectedSecurity = SelectedSecurity;
            IMarketDataDrive drive = Task.Drive;
            if ( !( drive is LocalMarketDataDrive ) )
                return;
            string str = drive.Path;
            if ( !selectedSecurity.TaskSecurity.IsAllSecurity() )
                str = ( ( LocalMarketDataDrive )drive ).GetSecurityPath( selectedSecurity.TaskSecurity.Security.ToSecurityId( null, true, false ) );
            try
            {
                if ( !Directory.Exists( str ) )
                    return;
                str.TryOpenLink( this );
            }
            catch ( Exception ex )
            {
                ex.LogError( null );
                int num = ( int )new MessageBoxBuilder().Text( LocalizedStrings.Str2917Params.Put( ex.Message, str ) ).Warning().Owner( this ).Show();
            }
        }

        private void OpenDirectoryCommand_CanExecute( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = SelectedSecurity != null;
        }

        private void OpenDataCommand_Executed( object sender, ExecutedRoutedEventArgs e )
        {
            TaskVisualSecurity selectedSecurity = SelectedSecurity;
            Security security = selectedSecurity.TaskSecurity.Security;
            if ( security.IsAllSecurity() )
                security = null;
            Messages.DataType dataType = selectedSecurity.DataType;
            IMarketDataDrive drive = Task.Drive;
            StorageFormats storageFormat = Task.StorageFormat;
            DateTime? from = new DateTime?();
            DateTime? to = new DateTime?();
            if ( security != null || !dataType.IsSecurityRequired )
            {
                IMarketDataStorage storage = ServicesRegistry.StorageRegistry.GetStorage( security, dataType, drive, storageFormat );
                from = storage.Dates.FirstOr();
                to = storage.Dates.LastOr();
            }
            string str;
            if ( selectedSecurity.DataType == Messages.DataType.Ticks )
                str = "Ticks";
            else if ( selectedSecurity.DataType == Messages.DataType.Level1 )
                str = "Level1";
            else if ( selectedSecurity.DataType == Messages.DataType.OrderLog )
                str = "OrderLog";
            else if ( selectedSecurity.DataType == Messages.DataType.MarketDepth )
                str = "Depths";
            else if ( selectedSecurity.DataType == Messages.DataType.PositionChanges )
                str = "PositionChanges";
            else if ( selectedSecurity.DataType == Messages.DataType.News )
                str = "News";
            else if ( selectedSecurity.DataType == Messages.DataType.Board )
                str = "Board";
            else if ( selectedSecurity.DataType == Messages.DataType.Transactions )
            {
                str = "Transactions";
            }
            else
            {
                if ( !dataType.IsCandles )
                    throw new ArgumentOutOfRangeException( selectedSecurity.DataType.ToString() );
                str = "Candles";
            }
            IPane pane = MainWindow.CreatePane( str, dataType, security, drive, storageFormat, from, to );
            if ( pane == null )
                throw new ArgumentOutOfRangeException( str );
            MainWindow.Instance.ShowPane( pane );
        }

        private void OpenDataCommand_CanExecute( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute =   ( SelectedSecurity?.DataType ) != null;
        }

        private void AddAllSecurity_OnClick( object sender, RoutedEventArgs e )
        {
            Manager.DeleteAll( Task );
        }

        private void SecuritiesCtrl_OnSelectionChanged( object sender, EventArgs e )
        {
            if ( ControlPanel == null || TimeFrames.ItemsSource == null )
                return;
            TaskVisualSecurity[ ] selectedRoots = SelectedRoots;
            TaskVisualSecurity[ ] selectedSecurities = SelectedSecurities;
            TaskVisualSecurity[ ] selectedRoots2 = SelectedRoots2;
            EditSecurities.IsEnabled = selectedRoots.Any() && selectedRoots.All( s => !s.TaskSecurity.IsAllSecurity() );
            RemoveSecurities.IsEnabled = selectedSecurities.All( s => s.Parent != null ) || selectedSecurities.All( s => !s.TaskSecurity.IsAllSecurity() );
            CandlesButton.IsEnabled = selectedRoots.Any();
            DepthsPanel.IsEnabled = selectedSecurities.Any( s => s.DataType == Messages.DataType.MarketDepth ) || selectedRoots.Any();
            TaskVisualSecurity taskVisualSecurity1;
            if ( selectedSecurities.Length == 1 )
            {
                Messages.DataType dataType = selectedSecurities[0].DataType;
                if ( ( dataType != null ? ( dataType.IsCandles ? 1 : 0 ) : 0 ) != 0 )
                {
                    taskVisualSecurity1 = selectedSecurities[0];
                    goto label_6;
                }
            }
            taskVisualSecurity1 = null;
        label_6:
            TaskVisualSecurity taskVisualSecurity2 = taskVisualSecurity1;
            VolProfileCb.IsEnabled = taskVisualSecurity2 != null;
            _suspendTimeFrames = true;
            try
            {
                foreach ( SelectableObject selectableObject in TimeFrames.ItemsSource.Cast<SelectableObject>() )
                {
                    object obj = selectableObject.Value.Arg;
                    if ( obj is TimeSpan )
                    {
                        TimeSpan tf = ( TimeSpan )obj;
                        bool[ ] array = selectedRoots2.Select( security => _visualSecurities[security].Item1.GetDataTypes().FilterTimeFrames().Contains( tf ) ).ToArray();
                        bool flag1 = array.Contains( true );
                        bool flag2 = array.Contains( false );
                        selectableObject.IsThreeState = flag1 == flag2 & flag1;
                        selectableObject.IsSelected = selectableObject.IsThreeState ? new bool?() : new bool?( flag1 );
                    }
                }
            }
            finally
            {
                _suspendTimeFrames = false;
            }
            foreach ( KeyValuePair<CheckBox, ValueTuple<Messages.DataType, Func<TaskVisualSecurity, bool>, Action<TaskVisualSecurity, bool>>> dataType in _dataTypes )
            {
                CheckBox key;
                ValueTuple<Messages.DataType, Func<TaskVisualSecurity, bool>, Action<TaskVisualSecurity, bool>> valueTuple1;
                dataType.Deconstruct( out key, out valueTuple1 );
                ValueTuple<Messages.DataType, Func<TaskVisualSecurity, bool>, Action<TaskVisualSecurity, bool>> valueTuple2 = valueTuple1;
                CheckBox checkBox = key;
                Func<TaskVisualSecurity, bool> selector = valueTuple2.Item2;
                checkBox.Click -= new RoutedEventHandler( CheckBoxClick );
                try
                {
                    bool[ ] array = selectedRoots2.Select( selector ).ToArray();
                    bool flag1 = array.Contains( true );
                    bool flag2 = array.Contains( false );
                    checkBox.IsThreeState = flag1 == flag2 & flag1;
                    checkBox.IsChecked = checkBox.IsThreeState ? new bool?() : new bool?( flag1 );
                }
                finally
                {
                    checkBox.Click += new RoutedEventHandler( CheckBoxClick );
                }
            }
            Level1Fields?[ ] array1 = selectedSecurities.Where( s => s.Parent != null ).GroupBy( s => s.CandlesBuildFrom ).Select( g => g.Key ).ToArray();
            DateTime?[ ] array2 = selectedSecurities.Where( s => s.Parent != null ).GroupBy( s => s.BeginDate ).Select( g => g.Key ).ToArray();
            DateTime?[ ] array3 = selectedSecurities.Where( s => s.Parent != null ).GroupBy( s => s.EndDate ).Select( g => g.Key ).ToArray();
            int?[ ] array4 = selectedSecurities.Where( s => s.Parent != null ).GroupBy( s => s.MaxDepth ).Select( g => g.Key ).ToArray();
            _initFromSelect = true;
            try
            {
                if ( array1.Length == 1 )
                    CandlesBuildFrom.SetSelected( array1[0] );
                else
                    CandlesBuildFrom.SetSelected( new Level1Fields?() );
                if ( array4.Length == 1 )
                    MaxDepthCb.SetSelected( array4[0] );
                else
                    MaxDepthCb.SetSelected( new int?() );
                BeginDate.EditValue = array2.Length == 1 ? array2[0] : new DateTime?();
                EndDate.EditValue = array3.Length == 1 ? array3[0] : new DateTime?();
                if ( taskVisualSecurity2 == null )
                    return;
                CheckBox volProfileCb = VolProfileCb;
                bool? volumeProfile = taskVisualSecurity2.VolumeProfile;
                bool flag = true;
                bool? nullable = new bool?( volumeProfile.GetValueOrDefault() == flag & volumeProfile.HasValue );
                volProfileCb.IsChecked = nullable;
            }
            finally
            {
                _initFromSelect = false;
            }
        }

        private void SecuritiesCtrl_ItemDoubleClick( object sender, ItemDoubleClickEventArgs e )
        {
            if ( !Equals( e.Column, NameColumn ) )
                return;
            TaskVisualSecurity[ ] selectedSecurities = SelectedSecurities;
            if ( selectedSecurities.Length == 0 )
                return;
            if ( selectedSecurities.Length == 1 && selectedSecurities[0].DataType != null )
                OpenDataCommand.Execute( null, null );
            else
                EditSecurity();
        }

        private void EditSecurity()
        {
            Security[ ] array = SelectedRoots.Select( s => s.TaskSecurity.Security ).Where( s => !s.IsAllSecurity() ).ToArray();
            if ( array.IsEmpty() )
                return;
            new SecurityCreateWindow()
            {
                SecurityStorage = ServicesRegistry.SecurityStorage,
                Securities = array
            }.ShowModal( this );
        }

        private void SecuritiesCtrl_OnFilterChanged( object sender, RoutedEventArgs e )
        {
            TreeListView.ExpandAllNodes();
        }

        private void CheckBoxVolProfileClick( object sender, RoutedEventArgs e )
        {
            bool? isChecked = ( ( ToggleButton )sender ).IsChecked;
            bool flag = true;
            bool isEnabled = ( isChecked.GetValueOrDefault() == flag & isChecked.HasValue ? 1 : 0 ) != 0;
            TaskVisualSecurity[ ] array = SelectedSecurities.Where( s =>
            {
                Messages.DataType dataType = s.DataType;
                if ( dataType == null )
                    return false;
                return dataType.IsCandles;
            } ).ToArray();
            array.ForEach( s => s.VolumeProfile = new bool?( isEnabled ) );
            SaveSecurities( array );
        }

        private void CheckBoxClick( object sender, RoutedEventArgs e )
        {
            CheckBox index = ( CheckBox )sender;
            TaskVisualSecurity[ ] selectedRoots2 = SelectedRoots2;
            bool? isChecked = index.IsChecked;
            bool flag = true;
            bool isEnabled = ( isChecked.GetValueOrDefault() == flag & isChecked.HasValue ? 1 : 0 ) != 0;
            Action<TaskVisualSecurity, bool> set = _dataTypes[index].Item3;
            selectedRoots2.ForEach( s => set( s, isEnabled ) );
            index.IsThreeState = false;
            SaveSecurities( selectedRoots2 );
        }

        private void OnSelectTimeFrame( object sender, RoutedEventArgs e )
        {
            if ( _suspendTimeFrames )
                return;
            DataTypeCheckBox dataTypeCheckBox = ( DataTypeCheckBox )sender;
            dataTypeCheckBox.IsThreeState = false;
            bool? isChecked1 = dataTypeCheckBox.IsChecked;
            bool flag1 = true;
            bool add = ( isChecked1.GetValueOrDefault() == flag1 & isChecked1.HasValue ? 1 : 0 ) != 0;
            bool? isChecked2 = dataTypeCheckBox.IsChecked;
            bool flag2 = true;
            bool remove = ( !( isChecked2.GetValueOrDefault() == flag2 & isChecked2.HasValue ) ? 1 : 0 ) != 0;
            Messages.DataType dataType = dataTypeCheckBox.DataType;
            if ( dataType == Custom )
            {
                dataTypeCheckBox.IsChecked = new bool?();
                CandleCustomSettings candleCustomSettings = new CandleCustomSettings();
                CandleCustomBuildFromSource.Values = Task.SupportedDataTypes.Where( dt => dt.IsCandleSource ).ToArray();
                if ( !new SettingsWindow() { Settings = candleCustomSettings }.ShowModal( this ) )
                    return;
                dataType = candleCustomSettings.Series.ToDataType();
                add = true;
            }
            HashSet<TaskVisualSecurity> changedSecurities = new HashSet<TaskVisualSecurity>();
            SecuritiesCtrl.BeginEndUpdate( () =>
            {
                foreach ( TaskVisualSecurity security in SelectedRoots2 )
                {
                    HydraTaskSecurity hydraTaskSecurity = _visualSecurities[security].Item1;
                    if ( add && !hydraTaskSecurity.Enabled( dataType ) )
                    {
                        AddRemoveDataType( security, true, dataType );
                        changedSecurities.Add( security );
                    }
                    else if ( remove )
                    {
                        AddRemoveDataType( security, false, dataType );
                        changedSecurities.Add( security );
                    }
                }
            } );
            SaveSecurities( changedSecurities );
        }

        private void AddRemoveDataType(
          TaskVisualSecurity security,
          bool isAdd,
          Messages.DataType dataType )
        {
            HydraTaskSecurity taskSecurity = security.TaskSecurity;
            TaskVisualSecurity visual = ToVisual( taskSecurity, security, dataType );
            if ( isAdd )
            {
                taskSecurity.AddDataType( dataType );
                _filteredSecurities.Add( visual );
            }
            else
            {
                taskSecurity.RemoveDataType( dataType );
                _filteredSecurities.Remove( visual );
            }
        }

        private void SaveSecurities( IEnumerable<TaskVisualSecurity> securities )
        {
            Manager.Save( Task, securities.Select( s => s.TaskSecurity ).Distinct().ToArray() );
        }

        private void Candles_OnClick( object sender, RoutedEventArgs e )
        {
            TryShowCandlesPopup();
        }

        private void Candles_OnMouseEnter( object sender, MouseEventArgs e )
        {
            TryShowCandlesPopup();
        }

        private void TryShowCandlesPopup()
        {
            if ( !CandlesButton.IsEnabled )
                return;
            TimeFramesPopup.IsOpen = false;
            TimeFramesPopup.IsOpen = true;
        }

        private void CandlesBuildFrom_OnEditValueChanged( object sender, EditValueChangedEventArgs e )
        {
            if ( SecuritiesCtrl == null || _initFromSelect )
                return;
            Level1Fields? field = CandlesBuildFrom.GetSelected<Level1Fields?>();
            TaskVisualSecurity[ ] array = SelectedSecurities.Where( p =>
            {
                if ( p.Parent != null )
                    return p.DataType.IsCandles;
                return false;
            } ).ToArray();
            array.ForEach( s => s.CandlesBuildFrom = field );
            SaveSecurities( array );
        }

        private void MaxDepthCb_OnEditValueChanged( object sender, EditValueChangedEventArgs e )
        {
            if ( SecuritiesCtrl == null || _initFromSelect )
                return;
            int? depth = MaxDepthCb.GetSelected<int?>();
            TaskVisualSecurity[ ] array = SelectedSecurities.Where( p =>
            {
                if ( p.Parent != null )
                    return p.DataType == Messages.DataType.MarketDepth;
                return false;
            } ).ToArray();
            array.ForEach( s => s.MaxDepth = depth );
            SaveSecurities( array );
        }

        private void BeginDate_OnEditValueChanged( object sender, EditValueChangedEventArgs e )
        {
            if ( SecuritiesCtrl == null || _initFromSelect )
                return;
            TaskVisualSecurity[ ] selectedSecurities = SelectedSecurities;
            DateTime? beginDate = ( DateTime? )BeginDate.EditValue;
            selectedSecurities.SelectMany( s =>
            {
                if ( s.Parent == null )
                    return s.Childs;
                return new TaskVisualSecurity[1] { s };
            } ).Distinct().ForEach( s => s.BeginDate = beginDate );
            SaveSecurities( selectedSecurities );
        }

        private void EndDate_OnEditValueChanged( object sender, EditValueChangedEventArgs e )
        {
            if ( SecuritiesCtrl == null || _initFromSelect )
                return;
            TaskVisualSecurity[ ] selectedSecurities = SelectedSecurities;
            DateTime? endDate = ( DateTime? )EndDate.EditValue;
            selectedSecurities.SelectMany( s =>
            {
                if ( s.Parent == null )
                    return s.Childs;
                return new TaskVisualSecurity[1] { s };
            } ).Distinct().ForEach( s => s.EndDate = endDate );
            SaveSecurities( selectedSecurities );
        }

        private void EditSecurities_OnClick( object sender, RoutedEventArgs e )
        {
            EditSecurity();
        }

        private void AddSecurities_OnClick( object sender, RoutedEventArgs e )
        {
            Windows.SecuritiesWindowEx wnd1 = new Windows.SecuritiesWindowEx() { Task = Task, SecurityProvider = SecurityProvider };
            wnd1.SecuritiesAll.ExcludeAllSecurity();
            ISet<Security> set = _allSecurities.Retrieve( string.Empty ).Where( s => !s.IsAllSecurity() ).ToSet();
            wnd1.SelectedSecurities = set;
            if ( !wnd1.ShowModal( this ) )
                return;
            ISet<Security> newSelected = wnd1.SelectedSecurities.ToSet();
            if ( set.Count == newSelected.Count )
            {
                set.RemoveRange( newSelected );
                if ( set.Count == 0 )
                    return;
            }
            HydraTaskSecurity allSecurity = _allSecurities.GetAllSecurity();
            List<Messages.DataType> list = Task.SupportedDataTypes.ToList();
            if ( list.Count > 0 )
            {
                ISet<Messages.DataType> supported = list.ToSet();
                if ( list.All( dt => dt.MessageType != typeof( TimeFrameCandleMessage ) ) && list.Any( new Func<Messages.DataType, bool>( Messages.DataType.CandleSources.Contains ) ) )
                    list.AddRange( Core.Extensions.GeneratedTimeFrames );
                if ( !list.Contains( Messages.DataType.MarketDepth ) && list.Contains( Messages.DataType.OrderLog ) )
                    list.Add( Messages.DataType.MarketDepth );
                IEnumerable<VisualDataType> visualDataTypes = list.Select( dt => new VisualDataType( dt, !supported.Contains( dt ) ) );
                SelectDataTypesWindow wnd2 = new SelectDataTypesWindow() { DataTypes = visualDataTypes };
                if ( !wnd2.ShowModal( this ) )
                    return;
                list = wnd2.SelectedDataTypes.Select( dt => dt.DataType ).ToList();
            }
            Messages.DataType[ ] arr = list.ToArray();
            if ( allSecurity != null )
            {
                HydraTaskSecurity[ ] array = newSelected.Select( new Func<Security, HydraTaskSecurity>( CreateTaskSecurity ) ).ToArray();
                if ( array.Length == 0 )
                    return;
                Manager.Save( Task, array );
            }
            else
            {
                HydraTaskSecurity[ ] array1 = _allSecurities.RetrieveHydra( string.Empty ).Where( s => !newSelected.Contains( s.Security ) ).ToArray();
                HydraTaskSecurity[ ] array2 = newSelected.Except( _allSecurities.Retrieve( string.Empty ) ).Select( new Func<Security, HydraTaskSecurity>( CreateTaskSecurity ) ).ToArray();
                Manager.Delete( Task, array1 );
                Manager.Save( Task, array2 );
            }
            TreeListView.ExpandAllNodes();
            EntityRegistry.WaitSecuritiesFlush();

            HydraTaskSecurity CreateTaskSecurity( Security security )
            {
                HydraTaskSecurity taskSecurity = Task.ToTaskSecurity( security );
                foreach ( Messages.DataType dataType in arr )
                    taskSecurity.AddDataType( dataType );
                return taskSecurity;
            }
        }

        private void RemoveSecurities_OnClick( object sender, RoutedEventArgs e )
        {
            ISet<HydraTaskSecurity> toRemove = SelectedRoots.Select( s => s.TaskSecurity ).ToSet();
            HashSet<HydraTaskSecurity> toUpdate = new HashSet<HydraTaskSecurity>();
            SecuritiesCtrl.BeginEndUpdate( () =>
            {
                foreach ( TaskVisualSecurity security in SelectedSecurities.Where( s => s.DataType != null ) )
                {
                    AddRemoveDataType( security, false, security.DataType );
                    HydraTaskSecurity taskSecurity = security.Parent.TaskSecurity;
                    toRemove.Remove( taskSecurity );
                    toUpdate.Add( taskSecurity );
                }
            } );
            if ( toRemove.Count > 0 )
                Manager.Delete( Task, toRemove.ToArray() );
            if ( toUpdate.Count > 0 )
                Manager.Save( Task, toUpdate.ToArray() );
            EntityRegistry.WaitSecuritiesFlush();
        }

        public override void Load( SettingsStorage storage )
        {
            if ( storage.ContainsKey( "TaskId" ) )
            {
                Guid taskId = storage.GetValue( "TaskId", new Guid() );
                Task = MainWindow.Instance.Tasks.SingleOrDefault( t => t.Id == taskId );
            }
            SecuritiesCtrl.Load( storage.GetValue<SettingsStorage>( "Securities", null ) );
            DockSite.LoadLayout( storage.GetValue<string>( "Layout", null ) );
            base.Load( storage );
        }

        public override void Save( SettingsStorage storage )
        {
            if ( Task != null )
                storage.SetValue( "TaskId", Task.Id );
            storage.SetValue( "Securities", SecuritiesCtrl.Save() );
            storage.SetValue( "Layout", DockSite.SaveLayout() );
            base.Save( storage );
        }

        bool IPane.IsValid
        {
            get
            {
                return Task != null;
            }
        }

        public override void Dispose( CloseReason reason )
        {
            if ( reason == CloseReason.CloseWindow )
                Task = null;
            base.Dispose( reason );
        }

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
                _propertyChanged += value;
            }
            remove
            {
                _propertyChanged -= value;
            }
        }

        private void TreeListView_OnCustomNodeFilter( object sender, TreeListNodeFilterEventArgs e )
        {
            if ( e.Node.ParentNode == null )
                return;
            e.Visible = !e.Node.ParentNode.IsFiltered;
            e.Handled = true;
        }

        private void TreeListView_OnCustomColumnSort( object sender, TreeListCustomColumnSortEventArgs e )
        {
            if ( !e.Column.FieldName.EqualsIgnoreCase( "Name" ) || e.Node1.ParentNode == null || e.Node2.ParentNode == null )
                return;
            TaskVisualSecurity content1 = ( TaskVisualSecurity )e.Node1.Content;
            TaskVisualSecurity content2 = ( TaskVisualSecurity )e.Node2.Content;
            int num = e.SortOrder == ColumnSortOrder.Ascending ? 1 : -1;
            e.Result = _taskVisualSecurityComparer.Compare( content1, content2 ) * num;
            e.Handled = true;
        }

        private void TreeListView_OnCellValueChanged( object sender, TreeListCellValueChangedEventArgs e )
        {
            SaveSecurities( new TaskVisualSecurity[1]
            {
        (TaskVisualSecurity) e.Row
            } );
        }

        private void TreeListView_OnShowingEditor( object sender, TreeListShowingEditorEventArgs e )
        {
            TreeListShowingEditorEventArgs showingEditorEventArgs = e;
            int num;
            if ( e.Column.IsEnabled )
            {
                TaskVisualSecurity content = e.Node.Content as TaskVisualSecurity;
                num = content == null ? 0 : ( content.DataType == null ? 1 : 0 );
            }
            else
                num = 1;
            showingEditorEventArgs.Cancel = num != 0;
        }

        private void EnableTaskCommand_Executed( object sender, ExecutedRoutedEventArgs e )
        {
            Task.SaveSettings();
        }

        private void EnableTaskCommand_CanExecute( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = Task != null;
        }

        private void ExpandCollapseCommand_Executed( object sender, ExecutedRoutedEventArgs e )
        {
            if ( ( bool )e.Parameter )
                TreeListView.ExpandAllNodes();
            else
                TreeListView.CollapseAllNodes();
        }

        private void ExpandCollapseCommand_CanExecute( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = Task != null;
        }

        private void SettingsPanel_OnChanged()
        {
            _delayActionHelper.Start( () => this.GuiAsync( () =>
            {
                if ( Task == null )
                    return;
                RefreshEnabledDataTypes( Task );
            } ) );
        }

        
        private class SelectableObject : NotifiableObject
        {
            private bool? _isSelected;
            private bool _isThreeState;

            public SelectableObject( Messages.DataType value )
            {
                Messages.DataType dataType = value;
                if ( dataType == null )
                    throw new ArgumentNullException( nameof( value ) );
                Value = dataType;
            }

            public Messages.DataType Value { get; }

            public bool? IsSelected
            {
                get
                {
                    return _isSelected;
                }
                set
                {
                    _isSelected = value;
                    NotifyChanged( nameof( IsSelected ) );
                }
            }

            public bool IsThreeState
            {
                get
                {
                    return _isThreeState;
                }
                set
                {
                    _isThreeState = value;
                    NotifyChanged( nameof( IsThreeState ) );
                }
            }
        }

        private class HydraSecurityTrie : SecurityTrie
        {
            private readonly Dictionary<Security, HydraTaskSecurity> _securities = new Dictionary<Security, HydraTaskSecurity>();

            public void AddRange( IEnumerable<HydraTaskSecurity> securities )
            {
                if ( securities == null )
                    throw new ArgumentNullException( nameof( securities ) );
                foreach ( HydraTaskSecurity security in securities )
                {
                    Add( security.Security );
                    _securities.Add( security.Security, security );
                }
            }

            public HydraTaskSecurity GetAllSecurity()
            {
                return RetrieveHydra( TraderHelper.AllSecurity.Id ).FirstOrDefault();
            }

            public IEnumerable<HydraTaskSecurity> RetrieveHydra( string filter )
            {
                return Retrieve( filter ).Select( s => _securities[s] );
            }

            public void RemoveRange( IEnumerable<HydraTaskSecurity> securities )
            {
                if ( securities == null )
                    throw new ArgumentNullException( nameof( securities ) );
                Security[ ] array = securities.Select( s => s.Security ).ToArray();
                RemoveRange( array );
                foreach ( Security key in array )
                    _securities.Remove( key );
            }

            public override void Clear()
            {
                base.Clear();
                _securities.Clear();
            }
        }

        private sealed class TaskVisualSecurityComparer : IComparer<TaskVisualSecurity>
        {
            private readonly Dictionary<Messages.DataType, long> _koefs = new Dictionary<Messages.DataType, long>() { { Messages.DataType.Ticks, -100L }, { Messages.DataType.Level1, -90L }, { Messages.DataType.MarketDepth, -80L }, { Messages.DataType.OrderLog, -70L }, { Messages.DataType.News, -60L }, { Messages.DataType.PositionChanges, -50L }, { Messages.DataType.Securities, -40L }, { Messages.DataType.Transactions, -30L } };

            public int Compare( TaskVisualSecurity x, TaskVisualSecurity y )
            {
                if (   ( x?.DataType ) == null ||   ( y?.DataType ) == null )
                    return 0;
                long? nullable1 = _koefs.TryGetValue2( x.DataType );
                long? nullable2 = _koefs.TryGetValue2( y.DataType );
                if ( !nullable1.HasValue )
                {
                    object obj = x.DataType.Arg;
                    if ( obj is TimeSpan )
                        nullable1 = new long?( ( ( TimeSpan )obj ).Ticks );
                }
                if ( !nullable2.HasValue )
                {
                    object obj = y.DataType.Arg;
                    if ( obj is TimeSpan )
                        nullable2 = new long?( ( ( TimeSpan )obj ).Ticks );
                }
                return nullable1.Compare( nullable2 );
            }
        }

        internal class TaskVisualSecurity : NotifiableObject
        {
            private bool _isExpanded = true;
            private string _invalidTooltip = string.Empty;
            private readonly TaskPane _pane;
            private bool _isInvalid;

            public string Id { get; }

            public string ParentId { get; }

            public string Name { get; }

            public TaskVisualSecurity Parent { get; }

            public IList<TaskVisualSecurity> Childs { get; } = new List<TaskVisualSecurity>();

            public HydraTaskSecurity TaskSecurity { get; }

            public Messages.DataType DataType { get; }

            public HydraTaskSecurity.TypeInfo Info { get; }

            public bool IsExpanded
            {
                get
                {
                    return _isExpanded;
                }
                set
                {
                    if ( _isExpanded == value )
                        return;
                    _isExpanded = value;
                    NotifyChanged( nameof( IsExpanded ) );
                }
            }

            public DateTime? BeginDate
            {
                get
                {
                    if ( !( DataType == null ) )
                        return TaskSecurity.GetBeginDate( DataType );
                    return new DateTime?();
                }
                set
                {
                    if ( DataType == null )
                        return;
                    TaskSecurity.SetBeginDate( DataType, value );
                    NotifyChanged( nameof( BeginDate ) );
                }
            }

            public DateTime? EndDate
            {
                get
                {
                    if ( !( DataType == null ) )
                        return TaskSecurity.GetEndDate( DataType );
                    return new DateTime?();
                }
                set
                {
                    if ( DataType == null )
                        return;
                    TaskSecurity.SetEndDate( DataType, value );
                    NotifyChanged( nameof( EndDate ) );
                }
            }

            public Level1Fields? CandlesBuildFrom
            {
                get
                {
                    if ( !( DataType == null ) )
                        return TaskSecurity.GetCandlesBuildFrom( DataType );
                    return new Level1Fields?();
                }
                set
                {
                    if ( DataType == null )
                        return;
                    TaskSecurity.SetCandlesBuildFrom( DataType, value );
                    NotifyChanged( nameof( CandlesBuildFrom ) );
                }
            }

            public int? MaxDepth
            {
                get
                {
                    if ( !( DataType == null ) )
                        return TaskSecurity.GetMaxDepth( DataType );
                    return new int?();
                }
                set
                {
                    if ( DataType == null )
                        return;
                    TaskSecurity.SetMaxDepth( DataType, value );
                    NotifyChanged( nameof( MaxDepth ) );
                }
            }

            public bool? VolumeProfile
            {
                get
                {
                    if ( !( DataType == null ) )
                        return TaskSecurity.GetVolumeProfile( DataType );
                    return new bool?();
                }
                set
                {
                    if ( DataType == null )
                        return;
                    TaskSecurity.SetVolumeProfile( DataType, value );
                    NotifyChanged( nameof( VolumeProfile ) );
                }
            }

            public TaskVisualSecurity(
              TaskPane pane,
              HydraTaskSecurity taskSecurity,
              TaskVisualSecurity parent,
              Messages.DataType dataType )
            {
                TaskPane taskPane = pane;
                if ( taskPane == null )
                    throw new ArgumentNullException( nameof( pane ) );
                _pane = taskPane;
                HydraTaskSecurity hydraTaskSecurity = taskSecurity;
                if ( hydraTaskSecurity == null )
                    throw new ArgumentNullException( nameof( taskSecurity ) );
                TaskSecurity = hydraTaskSecurity;
                DataType = dataType;
                Parent = parent;
                parent?.Childs.Add( this );
                string id = taskSecurity.Security.Id;
                if ( dataType == null )
                {
                    Name = Id = id;
                }
                else
                {
                    Id = id + dataType?.ToString();
                    ParentId = id;
                    Name = dataType.ToString();
                    if ( !pane.Task.SupportedDataTypes.Contains( dataType ) )
                        Name = Name + " (" + LocalizedStrings.Str1406 + ")";
                }
                if ( dataType == null )
                {
                    UpdateInvalid();
                    ( ( INotifyPropertyChanged )TaskSecurity.Security ).PropertyChanged += ( s, e ) =>
                    {
                        if ( !( e.PropertyName == "PriceStep" ) && !( e.PropertyName == "VolumeStep" ) && !( e.PropertyName == "Decimals" ) )
                            return;
                        UpdateInvalid();
                    };
                }
                else
                {
                    HydraTaskSecurity.DateTypeInfo dateTypeInfo = taskSecurity.AddDataType( dataType );
                    Info = dateTypeInfo;
                    if ( dateTypeInfo == null )
                        return;
                    dateTypeInfo.PropertyChanged += ( sender, args ) =>
                    {
                        string propertyName = args.PropertyName;
                        if ( !( propertyName == nameof( BeginDate ) ) )
                        {
                            if ( !( propertyName == nameof( EndDate ) ) )
                            {
                                if ( !( propertyName == nameof( CandlesBuildFrom ) ) )
                                    return;
                                NotifyChanged( nameof( CandlesBuildFrom ) );
                            }
                            else
                                NotifyChanged( nameof( EndDate ) );
                        }
                        else
                            NotifyChanged( nameof( BeginDate ) );
                    };
                }
            }

            private void UpdateInvalid()
            {
                List<string> parts = new List<string>();
                if ( !TaskSecurity.GetDataTypes().Any() )
                    parts.Add( LocalizedStrings.XamlStr344 );
                Security security = TaskSecurity.Security;
                Decimal? nullable;
                if ( security.PriceStep.HasValue )
                {
                    nullable = security.PriceStep;
                    Decimal num = new Decimal();
                    if ( !( nullable.GetValueOrDefault() == num & nullable.HasValue ) )
                        goto label_5;
                }
                parts.Add( LocalizedStrings.Str2925 );
            label_5:
                nullable = security.VolumeStep;
                if ( nullable.HasValue )
                {
                    nullable = security.VolumeStep;
                    Decimal num = new Decimal();
                    if ( !( nullable.GetValueOrDefault() == num & nullable.HasValue ) )
                        goto label_8;
                }
                parts.Add( LocalizedStrings.Str2924 );
            label_8:
                if ( !security.Decimals.HasValue )
                    parts.Add( LocalizedStrings.DecimalsNotFilled );
                IsInvalid = parts.Count > 0;
                InvalidTooltip = parts.Join( Environment.NewLine );
            }

            public bool IsInvalid
            {
                get
                {
                    return _isInvalid;
                }
                set
                {
                    if ( _isInvalid == value )
                        return;
                    _isInvalid = value;
                    NotifyChanged( nameof( IsInvalid ) );
                }
            }

            public string InvalidTooltip
            {
                get
                {
                    return _invalidTooltip;
                }
                set
                {
                    if ( _invalidTooltip == value )
                        return;
                    _invalidTooltip = value;
                    NotifyChanged( nameof( InvalidTooltip ) );
                }
            }

            public static void CreateGetSet(
              Messages.DataType type,
              out Func<TaskVisualSecurity, bool> get,
              out Action<TaskVisualSecurity, bool> set )
            {
                get = s => s.TaskSecurity.Enabled( type );
                set = ( s, v ) =>
                {
                    s._pane.AddRemoveDataType( s, v, type );
                    Manager.Save( s._pane.Task, s.TaskSecurity );
                    if ( s.Parent != null )
                        return;
                    s.UpdateInvalid();
                };
            }
        }

        private class CandleCustomBuildFromSource : ItemsSourceBase<Messages.DataType>
        {
            public static new IEnumerable<Messages.DataType> Values = Enumerable.Empty<Messages.DataType>();

            protected override IEnumerable<Messages.DataType> GetValues()
            {
                return Values;
            }
        }

        private class CandleCustomSettings : IPersistable
        {
            [Display( Description = "CandlesType", GroupName = "General", Name = "CandlesType", Order = 1, ResourceType = typeof( LocalizedStrings ) )]
            public CandleSeries Series { get; set; } = new CandleSeries() { CandleType = typeof( TickCandle ), Arg = 1000 };

            [Display( Description = "CandlesBuildSource", GroupName = "General", Name = "Str213", Order = 21, ResourceType = typeof( LocalizedStrings ) )]
            [ItemsSource( typeof( CandleCustomBuildFromSource ) )]
            public Messages.DataType BuildFrom { get; set; }

            void IPersistable.Load( SettingsStorage storage )
            {
                throw new NotSupportedException();
            }

            void IPersistable.Save( SettingsStorage storage )
            {
                throw new NotSupportedException();
            }
        }
    }
}
