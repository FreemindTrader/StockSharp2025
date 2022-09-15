using DevExpress.Mvvm;
using Ecng.Common;
using Ecng.Collections;
using Ecng.ComponentModel;
using StockSharp.Localization;
using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    public partial class RangeListEditor : UserControl, IComponentConnector
    {
        public static readonly DependencyProperty RangesProperty = DependencyProperty.Register( nameof( Ranges ), typeof( ObservableCollection<Range<TimeSpan>> ), typeof( RangeListEditor ), new PropertyMetadata( null, new PropertyChangedCallback( RangeListEditor.OnRangesPropertyChanged ) ) );
        private readonly RangeListViewModel _viewModel = new RangeListViewModel( );
        private bool _isProcessingChanges;
        private bool _hasChanged;
                       

        public RangeListEditor( )
        {
            InitializeComponent( );

            _viewModel.TimeErrorEvent += ( s, e ) => Error?.Invoke( s, e );
            _viewModel.DataChangedEvent += RangeListEditor_DataChanged;
            
            MainPanel.DataContext = _viewModel;
        }

        private void RangeListEditor_DataChanged( )
        {
            if ( _isProcessingChanges )
            {
                return;
            }

            _hasChanged = true;
            try
            {
                Ranges.Clear( );

                foreach ( RangeViewModel vm in _viewModel.ItemsSource )
                {
                    Ranges.Add( vm.Range );
                }
            }
            finally
            {
                _hasChanged = false;
            }

            DataChanged?.Invoke( );            
        }

        

        

        private static void OnRangesPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( ( RangeListEditor )d ).ProcessChanges( ( ObservableCollection<Range<TimeSpan>> )e.NewValue );
        }

        public ObservableCollection<Range<TimeSpan>> Ranges
        {
            get
            {
                return ( ObservableCollection<Range<TimeSpan>> )GetValue( RangeListEditor.RangesProperty );
            }
            set
            {
                SetValue( RangeListEditor.RangesProperty, value );
            }
        }

        public event Action<string, bool> Error;

        public event Action DataChanged;

        

        

        private void ProcessChanges( ObservableCollection<Range<TimeSpan>> source )
        {
            if ( _hasChanged )
            {
                return;
            }

            _isProcessingChanges = true;

            try
            {
                _viewModel.ItemsSource = source != null ? new ObservableCollection<RangeViewModel>( source.Select( r => new RangeViewModel( r ) ) ) : null;
                _viewModel.SelectedItem = null;
            }
            finally
            {
                _isProcessingChanges = false;
            }
        }

        

        private sealed class RangeViewModel : Ecng.Xaml.ViewModelBase
        {
            private bool _isSelected;
            private readonly Range<TimeSpan> _timeRange;

            public RangeViewModel( Range<TimeSpan> range )
            {
                _timeRange = range;
            }

            public RangeViewModel( TimeSpan from, TimeSpan to ) : this( new Range<TimeSpan>( from, to ) )
            {
            }

            public Range<TimeSpan> Range
            {
                get
                {
                    return _timeRange;
                }
            }

            public bool IsSelected
            {
                get
                {
                    return _isSelected;
                }
                set
                {
                    SetField( ref _isSelected, value, ( ) => IsSelected );
                }
            }
        }

        private sealed class RangeListViewModel : Ecng.Xaml.ViewModelBase
        {
            private ObservableCollection<RangeViewModel> _itemsSource;
            private RangeViewModel _selectedItem;
            private ICommand _commandAddTimes;
            private ICommand _commandDelTimes;
            private TimeSpan _newTimeFrom;
            private TimeSpan _newTimeTill;
            public  event Action<string, bool> TimeErrorEvent;
            public event Action DataChangedEvent;

            public RangeListViewModel( )
            {
                NewTimeFrom = TimeSpan.Zero;
                NewTimeTill = TimeHelper.LessOneDay;
            }

            public ObservableCollection<RangeViewModel> ItemsSource
            {
                get
                {
                    return _itemsSource;
                }
                set
                {
                    SetField( ref _itemsSource, value, ( ) => ItemsSource );
                }
            }

            public RangeViewModel SelectedItem
            {
                get
                {
                    return _selectedItem;
                }
                set
                {
                    SetField( ref _selectedItem, value, ( ) => SelectedItem );
                }
            }

            public TimeSpan NewTimeFrom
            {
                get
                {
                    return _newTimeFrom;
                }
                set
                {
                    SetField( ref _newTimeFrom, value, ( ) => NewTimeFrom );
                }
            }

            public TimeSpan NewTimeTill
            {
                get
                {
                    return _newTimeTill;
                }
                set
                {
                    SetField( ref _newTimeTill, value, ( ) => NewTimeTill );
                }
            }

            public ICommand CommandAddTimes
            {
                get
                {
                    return _commandAddTimes ?? ( _commandAddTimes = new DelegateCommand(  () => CmdAddTimes( ) , true ) );
                }
            }

            public ICommand CommandDelTimes
            {
                get
                {
                    return _commandDelTimes ?? ( _commandDelTimes = new DelegateCommand( () => CmdDelTimes(), true  ) );
                }
            }

            //public void method_0( Action<string, bool> action_2 )
            //{
            //    Action<string, bool> action = action_0;
            //    Action<string, bool> comparand;
            //    do
            //    {
            //        comparand = action;
            //        action = Interlocked.CompareExchange<Action<string, bool>>( ref action_0, comparand + action_2, comparand );
            //    }
            //    while ( action != comparand );
            //}

            //public void method_1( Action<string, bool> action_2 )
            //{
            //    Action<string, bool> action = action_0;
            //    Action<string, bool> comparand;
            //    do
            //    {
            //        comparand = action;
            //        action = Interlocked.CompareExchange<Action<string, bool>>( ref action_0, comparand - action_2, comparand );
            //    }
            //    while ( action != comparand );
            //}

            //public void method_2( Action action_2 )
            //{
            //    Action action = action_1;
            //    Action comparand;
            //    do
            //    {
            //        comparand = action;
            //        action = Interlocked.CompareExchange<Action>( ref action_1, comparand + action_2, comparand );
            //    }
            //    while ( action != comparand );
            //}

            //public void method_3( Action action_2 )
            //{
            //    Action action = action_1;
            //    Action comparand;
            //    do
            //    {
            //        comparand = action;
            //        action = Interlocked.CompareExchange<Action>( ref action_1, comparand - action_2, comparand );
            //    }
            //    while ( action != comparand );
            //}

            private void CmdAddTimes( )
            {                
                if ( ItemsSource == null )
                {
                    return;
                }

                var from = NewTimeFrom;
                var till = NewTimeTill;

                if ( !( till > from ) )
                {
                    TimeErrorEvent?.Invoke( LocalizedStrings.Str1428, true );                    
                }
                else
                {
                    int index = ItemsSource.IndexOf( r => r.Range.Min > till );

                    if ( index == 0 )
                    {
                        ItemsSource.Insert( 0, new RangeViewModel( from, till ) );
                    }
                    else if ( index > 0 && ItemsSource[ index - 1 ].Range.Max < from )
                    {
                        ItemsSource.Insert( index, new RangeViewModel( from, till ) );
                    }
                    else if ( index < 0 && ( !ItemsSource.Any( ) || ItemsSource.Last( ).Range.Max < from ) )
                    {
                        ItemsSource.Add( new RangeViewModel( from, till ) );
                    }
                    else
                    {
                        TimeErrorEvent?.Invoke( LocalizedStrings.Str1428, true );
                    }

                    DataChangedEvent?.Invoke( );
                }
            }

            private void CmdDelTimes( )
            {
                if ( ItemsSource == null )
                {
                    return;
                }

                int selectedItems = ItemsSource.IndexOf( r => r.IsSelected );
                if ( selectedItems < 0 )
                {
                    return;
                }

                ItemsSource.RemoveWhere( r => r.IsSelected );
                if ( ItemsSource.Any( ) )
                {
                    ItemsSource[ selectedItems == ItemsSource.Count ? selectedItems - 1 : selectedItems ].IsSelected = true;
                }

                DataChangedEvent?.Invoke( );
            }

            private void method_6( object o )
            {
                CmdAddTimes( );
            }

            private void method_7( object object_0 )
            {
                CmdDelTimes( );
            }                   
        }        
    }
}
