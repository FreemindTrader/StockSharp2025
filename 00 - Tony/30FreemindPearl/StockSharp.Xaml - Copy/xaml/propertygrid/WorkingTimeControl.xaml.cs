using Ecng.Collections;
using Ecng.ComponentModel;
using Ecng.Xaml;
using StockSharp.Localization;
using StockSharp.Messages;
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
    public partial class WorkingTimeControl : UserControl, IComponentConnector
    {
        public static readonly DependencyProperty WorkingTimeProperty = DependencyProperty.Register( nameof( WorkingTime ), typeof( WorkingTime ), typeof( WorkingTimeControl ), new PropertyMetadata( null, new PropertyChangedCallback( WorkingTimeControl.OnWorkingTimePropertyChanged ) ) );
        private readonly WorkingTimesViewModel _viewModel = new WorkingTimesViewModel( );
        private bool _hasChanges;
        private bool _isProcessingChanges;        
        

        public WorkingTimeControl( )
        {
            InitializeComponent( );
            _viewModel.ErrorEvent += ( new Action<string, bool>( OnErrorEvent ) );
            _viewModel.DataChangedEvent += ( new Action( OnDataChangedEvent ) );
            MainPanel.DataContext = _viewModel;
        }

        private static void OnWorkingTimePropertyChanged(
          DependencyObject d,
          DependencyPropertyChangedEventArgs e )
        {
            ( ( WorkingTimeControl )d ).ProcessPropertyChanged( ( WorkingTime )e.NewValue );
        }

        public WorkingTime WorkingTime
        {
            get
            {
                return ( WorkingTime )GetValue( WorkingTimeControl.WorkingTimeProperty );
            }
            set
            {
                SetValue( WorkingTimeControl.WorkingTimeProperty, value );
            }
        }

        public event Action<string, bool> Error;

        public event Action DataChanged;

        private void OnDataChangedEvent( )
        {
            if ( _hasChanges )
                return;

            _isProcessingChanges = true;

            try
            {
                WorkingTime.Periods = _viewModel.ItemsSource.Select( w => new WorkingTimePeriod( ) { Till = w.Till, Times = w.WorkTimes.ToList( ), SpecialDays = w.DaysOfWeek } ).ToList( );
                WorkingTime.SpecialDays = _viewModel.SpecialDays;
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

        private void ProcessPropertyChanged( WorkingTime wt )
        {
            if ( _isProcessingChanges )
                return;

            _hasChanges = true;

            try
            {
                _viewModel.ItemsSource = wt != null ? new ObservableCollection<WorkingTimeViewModel>( wt.Periods.Select( w => new WorkingTimeViewModel( w ) ) ) : null;
                _viewModel.SpecialDays = wt?.SpecialDays;
                _viewModel.SelectedItem = null;
            }
            finally
            {
                _hasChanges = false;
            }
        }

        

        private sealed class WorkingTimeViewModel : ViewModelBase
        {
            private readonly ObservableCollection<Range<TimeSpan>> _workTimes = new ObservableCollection<Range<TimeSpan>>( );
            private readonly DateTime _till;
            private readonly IDictionary<DayOfWeek, Range<TimeSpan>[ ]> _daysOfWeek;

            public WorkingTimeViewModel( DateTime t )
            {
                _till = t;
                _daysOfWeek = new Dictionary<DayOfWeek, Range<TimeSpan>[ ]>( );
            }

            public WorkingTimeViewModel( WorkingTimePeriod wt )
            {
                _till = wt.Till;
                WorkTimes.AddRange( wt.Times );
                _daysOfWeek = wt.SpecialDays;
            }

            public DateTime Till
            {
                get
                {
                    return _till;
                }
            }

            public ObservableCollection<Range<TimeSpan>> WorkTimes
            {
                get
                {
                    return _workTimes;
                }
            }

            public IDictionary<DayOfWeek, Range<TimeSpan>[ ]> DaysOfWeek
            {
                get
                {
                    return _daysOfWeek;
                }
            }
        }

        private sealed class WorkingTimesViewModel : ViewModelBase
        {
            private ObservableCollection<WorkingTimeViewModel> _itemSource;

            private IDictionary<DateTime, Range<TimeSpan>[ ]> _specialDays;
            private WorkingTimeViewModel _selectedItem;

            private ICommand _commandAddPeriod;
            private ICommand _commandDelPeriod;
            private DateTime _newPeriodTill;

            public event Action<string, bool> ErrorEvent;
            public event Action DataChangedEvent;

            public WorkingTimesViewModel( )
            {
                NewPeriodTill = new DateTime( DateTime.Now.Year + 1, 1, 1 ) - TimeSpan.FromDays( 1.0 );
            }

            public ObservableCollection<WorkingTimeViewModel> ItemsSource
            {
                get
                {
                    return _itemSource;
                }
                set
                {
                    SetField( ref _itemSource, value, ( ) => ItemsSource );
                }
            }

            public WorkingTimeViewModel SelectedItem
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

            public IDictionary<DateTime, Range<TimeSpan>[ ]> SpecialDays
            {
                get
                {
                    return _specialDays;
                }
                set
                {
                    SetField( ref _specialDays, value, ( ) => SpecialDays );
                }
            }

            public DateTime NewPeriodTill
            {
                get
                {
                    return _newPeriodTill;
                }
                set
                {
                    SetField( ref _newPeriodTill, value, ( ) => NewPeriodTill );
                }
            }

            public ICommand CommandAddPeriod
            {
                get
                {
                    return _commandAddPeriod ?? ( _commandAddPeriod = new DelegateCommand( o => CmdAddPeriod(), x => true ) );
                }
            }

            public ICommand CommandDelPeriod
            {
                get
                {
                    return _commandDelPeriod ?? ( _commandDelPeriod = new DelegateCommand( o => CmdDelPeriod(), x => true ) );
                }
            }

            

            private void CmdAddPeriod( )
            {                
                if ( ItemsSource.FirstOrDefault( d => d.Till == NewPeriodTill.Date ) != null )
                {                    
                    ErrorEvent?.Invoke( LocalizedStrings.Str1427, true );
                }
                else
                {
                    int index = ItemsSource.IndexOf( d => d.Till > NewPeriodTill.Date );
                    if ( index < 0 )
                    {
                        ItemsSource.Add( new WorkingTimeViewModel( NewPeriodTill.Date ) );
                    }                        
                    else
                    {
                        ItemsSource.Insert( index, new WorkingTimeViewModel( NewPeriodTill.Date ) );
                    }
                                            
                    DataChangedEvent?.Invoke( );
                }
            }

            private void CmdDelPeriod( )
            {                
                if ( SelectedItem == null )
                    return;

                ItemsSource.RemoveWhere( d => d.Till == SelectedItem.Till );
                
                DataChangedEvent?.Invoke( );
            }           
        }        
    }
}
