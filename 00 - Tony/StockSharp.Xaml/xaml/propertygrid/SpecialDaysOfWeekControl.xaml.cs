using Ecng.Collections;
using Ecng.ComponentModel;
using Ecng.Xaml;
using StockSharp.Localization;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace StockSharp.Xaml.PropertyGrid
{
    public partial class SpecialDaysOfWeekControl : UserControl, IComponentConnector
    {
        public static readonly DependencyProperty SpecialDaysOfWeekProperty = DependencyProperty.Register( nameof( SpecialDaysOfWeek ), typeof( IDictionary<DayOfWeek, Range<TimeSpan>[ ]> ), typeof( SpecialDaysOfWeekControl ), new PropertyMetadata( null, new PropertyChangedCallback( SpecialDaysOfWeekControl.OnSpecialDaysOfWeekPropertyChanged ) ) );
        private readonly SpecialDaysOfWeekViewModel _viewModel = new SpecialDaysOfWeekViewModel( );
        private bool _hasChanges;
        private bool _isProcessingChanges;               

        public SpecialDaysOfWeekControl( )
        {
            InitializeComponent( );

            _viewModel.ErrorEvent       += OnErrorEvent;
            _viewModel.DataChangedEvent += OnDataChangedEvent;
            MainPanel.DataContext        = _viewModel;
        }

        private static void OnSpecialDaysOfWeekPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( ( SpecialDaysOfWeekControl )d ).ProcessSpecialDaysOfWeekPropertyChanged( ( IDictionary<DayOfWeek, Range<TimeSpan>[ ]> )e.NewValue );
        }

        public IDictionary<DayOfWeek, Range<TimeSpan>[ ]> SpecialDaysOfWeek
        {
            get
            {
                return ( IDictionary<DayOfWeek, Range<TimeSpan>[ ]> )GetValue( SpecialDaysOfWeekControl.SpecialDaysOfWeekProperty );
            }
            set
            {
                SetValue( SpecialDaysOfWeekControl.SpecialDaysOfWeekProperty, value );
            }
        }

        public event Action<string, bool> Error;

        public event Action DataChanged;

        private void OnDataChangedEvent( )
        {
            if ( _hasChanges )
            {
                return;
            }

            _isProcessingChanges = true;

            try
            {
                SpecialDaysOfWeek.Clear( );
                foreach ( SpecialDayOfWeekViewModel class605 in _viewModel.ItemsSource )
                {
                    SpecialDaysOfWeek.Add( class605.Day, class605.Ranges.ToArray( ) );
                }
            }
            finally
            {
                _isProcessingChanges = false;
            }
            

            DataChanged?.Invoke( );
        }

        private void OnErrorEvent( string string_0, bool bool_3 )
        {            
            Error?.Invoke( string_0, bool_3 );
        }

        private void ProcessSpecialDaysOfWeekPropertyChanged( IDictionary<DayOfWeek, Range<TimeSpan>[ ]> d )
        {
            if ( _isProcessingChanges )
            {
                return;
            }

            _hasChanges = true;

            try
            {
                _viewModel.ItemsSource = d != null ? new ObservableCollection<SpecialDayOfWeekViewModel>( d.Select( p => new SpecialDayOfWeekViewModel( p ) ) ) : null;
                _viewModel.SelectedItem = null;
            }
            finally
            {
                _hasChanges = false;
            }
        }

        

        private sealed class SpecialDaysOfWeekViewModel : ViewModelBase
        {
            private ObservableCollection<SpecialDayOfWeekViewModel> _itemSource;
            private SpecialDayOfWeekViewModel _selectedItem;
            private DayOfWeek? _newDayOfWeek;
            private ICommand _commandAddDayOfWeek;
            private ICommand _commandDelDayOfWeek;
            public event Action<string, bool> ErrorEvent;
            public event Action DataChangedEvent;

            public SpecialDaysOfWeekViewModel( )
            {
                NewDayOfWeek = new DayOfWeek?( DayOfWeek.Monday );
            }

            public ObservableCollection<SpecialDayOfWeekViewModel> ItemsSource
            {
                get
                {
                    return _itemSource;
                }
                set
                {
                    this.SetField( ref _itemSource, value, ( ) => ItemsSource );
                }
            }

            public SpecialDayOfWeekViewModel SelectedItem
            {
                get
                {
                    return _selectedItem;
                }
                set
                {
                    this.SetField( ref _selectedItem, value, ( ) => SelectedItem );
                }
            }

            public DayOfWeek? NewDayOfWeek
            {
                get
                {
                    return _newDayOfWeek;
                }
                set
                {
                    this.SetField( ref _newDayOfWeek, value, ( ) => NewDayOfWeek );
                }
            }

            public ICommand CommandAddDayOfWeek
            {
                get
                {
                    return _commandAddDayOfWeek ?? ( _commandAddDayOfWeek = new DelegateCommand( o => CmdAddDayOfWeek(), x => true ) );
                }
            }

            public ICommand CommandDelDayOfWeek
            {
                get
                {
                    return _commandDelDayOfWeek ?? ( _commandDelDayOfWeek = new DelegateCommand( o => CmdDelDayOfWeek(), x => true ) );
                }
            }

            

            private void CmdAddDayOfWeek( )
            {
                if ( ItemsSource == null || !NewDayOfWeek.HasValue )
                {
                    return;
                }

                if ( ItemsSource.FirstOrDefault( d => d.Day == NewDayOfWeek.GetValueOrDefault( ) & NewDayOfWeek.HasValue ) != null )
                {
                    ErrorEvent?.Invoke( LocalizedStrings.Str1432, true );
                }
                else
                {
                    int index = ItemsSource.IndexOf( d => d.Day > NewDayOfWeek.GetValueOrDefault( ) & NewDayOfWeek.HasValue );

                    if ( index < 0 )
                    {
                        ItemsSource.Add( new SpecialDayOfWeekViewModel( NewDayOfWeek.Value ) );
                    }
                    else
                    {
                        ItemsSource.Insert( index, new SpecialDayOfWeekViewModel( NewDayOfWeek.Value ) );
                    }

                    DataChangedEvent?.Invoke( );
                }
            }

            private void CmdDelDayOfWeek( )
            {
                if ( ItemsSource == null )
                {
                    return;
                }

                int selectedDate = ItemsSource.IndexOf( d => d.IsSelected );

                if ( selectedDate < 0 )
                {
                    return;
                }

                ItemsSource.RemoveWhere( d => d.IsSelected );

                if ( ItemsSource.Any( ) )
                {
                    ItemsSource[ selectedDate == ItemsSource.Count ? selectedDate - 1 : selectedDate ].IsSelected = true;
                }
                
                DataChangedEvent?.Invoke( );
            }
            
        }
        
        private sealed class SpecialDayOfWeekViewModel : ViewModelBase
        {
            private readonly ObservableCollection<Range<TimeSpan>> _range = new ObservableCollection<Range<TimeSpan>>( );
            private bool _isSelected;
            private readonly DayOfWeek _day;

            public SpecialDayOfWeekViewModel( KeyValuePair<DayOfWeek, Range<TimeSpan>[ ]> input ) : this( input.Key )
            {
                Ranges.AddRange( input.Value );
            }

            public SpecialDayOfWeekViewModel( DayOfWeek dayOfWeek_1 )
            {
                _day = dayOfWeek_1;
            }

            public DayOfWeek Day
            {
                get
                {
                    return _day;
                }
            }

            public ObservableCollection<Range<TimeSpan>> Ranges
            {
                get
                {
                    return _range;
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
                    this.SetField( ref _isSelected, value, ( ) => IsSelected );
                }
            }
        }
    }
}
