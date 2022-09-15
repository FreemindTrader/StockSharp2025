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
    public partial class SpecialDaysControl : UserControl, IComponentConnector
    {
        public static readonly DependencyProperty SpecialDaysProperty = DependencyProperty.Register( nameof( SpecialDays ), typeof( IDictionary<DateTime, Range<TimeSpan>[ ]> ), typeof( SpecialDaysControl ), new PropertyMetadata( null, new PropertyChangedCallback( SpecialDaysControl.OnSpecialDaysChanged ) ) );
        private readonly SpecialDaysViewModel _specialDaysViewModel = new SpecialDaysViewModel( );
        private bool _isProcessingChanges;
        private bool _hasChanged;        

        public SpecialDaysControl( )
        {
            InitializeComponent( );

            _specialDaysViewModel.ErrorEvent += OnErrorEvent;
            _specialDaysViewModel.DataChangedEvent += OnDataChanged;

            MainPanel.DataContext = _specialDaysViewModel;
        }

        private static void OnSpecialDaysChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( ( SpecialDaysControl )d ).ProcessSpecialDaysChanged( ( IDictionary<DateTime, Range<TimeSpan>[ ]> )e.NewValue );
        }

        public IDictionary<DateTime, Range<TimeSpan>[ ]> SpecialDays
        {
            get => ( IDictionary<DateTime, Range<TimeSpan>[ ]> )GetValue( SpecialDaysControl.SpecialDaysProperty );
            set => SetValue( SpecialDaysControl.SpecialDaysProperty, value );
        }

        public event Action<string, bool> Error;

        public event Action DataChanged;

        private void OnDataChanged( )
        {
            if ( _isProcessingChanges )
                return;

            _hasChanged = true;

            try
            {
                SpecialDays.Clear( );

                foreach ( var day in _specialDaysViewModel.ItemsSource )
                {
                    SpecialDays.Add( day.DateTime, day.Ranges.ToArray( ) );
                }
                    
            }
            finally
            {
                _hasChanged = false;
            }

            DataChanged?.Invoke( );            
        }

        private void OnErrorEvent( string string_0, bool bool_3 )
        {            
            Error?.Invoke( string_0, bool_3 );
        }

        private void ProcessSpecialDaysChanged( IDictionary<DateTime, Range<TimeSpan>[ ]> changed )
        {
            if ( _hasChanged )
                return;

            _isProcessingChanges = true;

            try
            {
                _specialDaysViewModel.ItemsSource = changed != null ? new ObservableCollection<SpecialDayViewModel>( changed.Select( d => new SpecialDayViewModel( d ) ) ) : null;
                _specialDaysViewModel.SelectedItem = null;
            }
            finally
            {
                _isProcessingChanges = false;
            }
        }

        

        private sealed class SpecialDayViewModel : ViewModelBase
        {
            private readonly ObservableCollection<Range<TimeSpan>> _itemSource = new ObservableCollection<Range<TimeSpan>>( );
            private bool _isSelected;
            private readonly DateTime _specialDate;

            public SpecialDayViewModel( KeyValuePair<DateTime, Range<TimeSpan>[ ]> input ) : this( input.Key )
            {
                Ranges.AddRange( input.Value );
            }

            public SpecialDayViewModel( DateTime specialDate )
            {
                _specialDate = specialDate;
            }

            public DateTime DateTime
            {
                get
                {
                    return _specialDate;
                }
            }

            public ObservableCollection<Range<TimeSpan>> Ranges
            {
                get
                {
                    return _itemSource;
                }
            }

            public bool IsSelected
            {
                get => _isSelected;
                set => SetField( ref _isSelected, value, ( ) => IsSelected );
            }
        }

        private sealed class SpecialDaysViewModel : ViewModelBase
        {
            private ObservableCollection<SpecialDayViewModel> _specialDays;
            private SpecialDayViewModel _selectedItem;
            private DateTime _newSpecialDay;
            private ICommand _cmdAddSpecialDay;
            private ICommand _cmdDelSpecialDay;

            public event Action<string, bool> ErrorEvent;
            public event Action DataChangedEvent;

            public SpecialDaysViewModel( )
            {
                NewSpecialDay = new DateTime( DateTime.Now.Year, 1, 1 );
            }

            public ObservableCollection<SpecialDayViewModel> ItemsSource
            {
                get => _specialDays;
                set => SetField( ref _specialDays, value, ( ) => ItemsSource );
            }

            public SpecialDayViewModel SelectedItem
            {
                get => _selectedItem;
                set => SetField( ref _selectedItem, value, ( ) => SelectedItem );
            }

            public DateTime NewSpecialDay
            {
                get => _newSpecialDay;
                set => SetField( ref _newSpecialDay, value, ( ) => NewSpecialDay );
            }

            public ICommand CommandAddSpecialDay
            {
                get
                {
                    return _cmdAddSpecialDay ?? ( _cmdAddSpecialDay = new DelegateCommand( o => CmdAddSpecialDay(), x => true ) );
                }
            }

            public ICommand CommandDelSpecialDay
            {
                get
                {
                    return _cmdDelSpecialDay ?? ( _cmdDelSpecialDay = new DelegateCommand( o => CmdDelSpecialDay(), x => true ) );
                }
            }            

            private void CmdAddSpecialDay( )
            {                
                if ( ItemsSource == null )
                    return;

                var specialDate = NewSpecialDay.Date;

                if ( ItemsSource.FirstOrDefault( d => d.DateTime == specialDate ) != null )
                {                    
                    ErrorEvent?.Invoke( LocalizedStrings.Str1432, true );
                }
                else
                {
                    int index = ItemsSource.IndexOf( vm => vm.DateTime > specialDate );

                    if ( index < 0 )
                    {
                        ItemsSource.Add( new SpecialDayViewModel( specialDate ) );
                    }                        
                    else
                    {
                        ItemsSource.Insert( index, new SpecialDayViewModel( specialDate ) );
                    }
                                            
                    DataChangedEvent?.Invoke( );
                }
            }

            private void CmdDelSpecialDay( )
            {
                if ( ItemsSource == null )
                    return;

                int selectedDate = ItemsSource.IndexOf( d => d.IsSelected );

                if ( selectedDate < 0 )
                    return;

                ItemsSource.RemoveWhere( d => d.IsSelected );

                if ( ItemsSource.Any( ) )
                {
                    ItemsSource[ selectedDate == ItemsSource.Count ? selectedDate - 1 : selectedDate ].IsSelected = true;
                }
                                    
                DataChangedEvent?.Invoke( );
            }                      
        }        
    }
}
