using DevExpress.Data.Filtering;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Xaml;
using MoreLinq;
using StockSharp.Algo;
using StockSharp.Community;
using StockSharp.Localization;
using StockSharp.Xaml.GridControl;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    public partial class PermissionCredentialsWindow : DXWindow, IComponentConnector
    {
        public static readonly RoutedCommand OkCommand = new RoutedCommand();
        public static readonly RoutedCommand AddCommand = new RoutedCommand();
        public static readonly RoutedCommand RemoveCommand = new RoutedCommand();
        public static readonly RoutedCommand SelectAllCommand = new RoutedCommand();
        public static readonly RoutedCommand UnSelectAllCommand = new RoutedCommand();
        private bool _hasChanges;
        private readonly ObservableCollectionEx<PermissionCredentialsWindow.PermissionItem> _permissions;
        private readonly string _counterText;
        private readonly ObservableCollection<PermissionCredentials> _creditials = new ObservableCollection<PermissionCredentials>();               

        public PermissionCredentialsWindow( )
        {
            InitializeComponent();
            CredentialsGrid.ItemsSource = _creditials;
            _permissions = new ObservableCollectionEx<PermissionCredentialsWindow.PermissionItem>();

            PermissionsGrid.ItemsSource = _permissions;

            _creditials.CollectionChanged += new NotifyCollectionChangedEventHandler( OnCredentialCollectionChanged );
            _counterText = Counter.Text;

            UpdateCounter();
        }

        public IList<PermissionCredentials> Credentials
        {
            get
            {
                return  _creditials;
            }
        }

        private void OnCredentialCollectionChanged( object sender, NotifyCollectionChangedEventArgs e )
        {
            
            var newItems = e.NewItems != null ? e.NewItems.Cast<ServerCredentials>().Skip( e.NewStartingIndex ) : null;            
            var oldItems = e.OldItems != null ? e.OldItems.Cast<ServerCredentials>().Skip( e.OldStartingIndex ) : null;

            switch ( e.Action )
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        foreach ( var item in newItems )
                        {
                            item.PropertyChanged += PermissionPropertyChanged;
                        }
                    }
                    UpdateCounter( );
                    break;

                case NotifyCollectionChangedAction.Remove:
                    {
                        foreach ( var item in oldItems )
                        {
                            item.PropertyChanged -= PermissionPropertyChanged;
                        }
                    }
                    UpdateCounter();
                    break;

                case NotifyCollectionChangedAction.Replace:
                    if ( newItems != null )
                    {
                        foreach ( var item in newItems )
                        {
                            item.PropertyChanged += PermissionPropertyChanged;
                        }
                    }

                    if ( oldItems != null )
                    {
                        foreach ( var item in oldItems )
                        {
                            item.PropertyChanged -= PermissionPropertyChanged;
                        }                        
                    }
                    UpdateCounter();
                    break;

                case NotifyCollectionChangedAction.Move:
                    UpdateCounter();
                    break;

                case NotifyCollectionChangedAction.Reset:
                    if ( oldItems != null )
                    {
                        foreach ( var item in oldItems )
                        {
                            item.PropertyChanged -= PermissionPropertyChanged;
                        }
                    }
                    UpdateCounter();
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void PermissionPropertyChanged( object sender, PropertyChangedEventArgs e )
        {
            HasChanges();
        }

        private void CanExecuteOkCommand( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = _hasChanges;
        }


        


        private void ExecuteOkCommand( object sender, ExecutedRoutedEventArgs e )
        {
            if ( Credentials.Any( p => p.Email.IsEmpty() ? true : p.Password.IsEmpty() ) )
            {                
                new MessageBoxBuilder().Owner( this ).Caption( this.Title ).Warning().Text( LocalizedStrings.LoginAndPasswordMustBeSpecified ).Button( MessageBoxButton.OK ).Show();
            }
            else
            {
                DialogResult = true;
            }
        }

        private void CanExecuteAddCommand( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = true;
        }

        private void ExecuteAddCommand( object sender, ExecutedRoutedEventArgs e )
        {
            _creditials.Add( new PermissionCredentials() );
            HasChanges();
        }

        private void CanExecuteRemoveCommand( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = CredentialsGrid?.SelectedItem != null;
        }

        private void ExecuteRemoveCommand( object sender, ExecutedRoutedEventArgs e )
        {
            _creditials.RemoveRange(  CredentialsGrid.SelectedItems.Cast<PermissionCredentials>().ToArray() );
            HasChanges();
        }

        private void OnGridCellValueChanged( object sender, CellValueChangedEventArgs e )
        {
            HasChanges();
        }

        private void HasChanges( )
        {
            _hasChanges = true;
        }

        private void CredentialsGrid_SelectedItemChanged( object sender, SelectedItemChangedEventArgs e )
        {
            _permissions.Clear();
            var credential = ( PermissionCredentials ) CredentialsGrid.SelectedItem;

            if ( credential == null )
            {
                return;
            }

            _permissions.AddRange( ( Enumerator.GetValues<UserPermissions>() ).Select( x => new PermissionItem( credential, x, ( ) => _hasChanges = true ) ) );
        }
                

        private void OnCredentialsFilterCtrlTextChanged( object sender, TextChangedEventArgs e )
        {
            CredentialsGrid.BeginDataUpdate();
            try
            {
                string str = CredentialsFilterCtrl.Text?.Trim();
                CredentialsGrid.FilterCriteria = ( str.IsEmpty(  ) ? null : CriteriaOperator.Parse( string.Format( "Contains([{0}], '{1}')", "Name", str ) ) );
            }
            finally
            {
                CredentialsGrid.EndDataUpdate();
                UpdateCounter();
            }
        }

        private void UpdateCounter( )
        {
            GridSummaryItem summary = CredentialsGrid.TotalSummary.OfType<GridSummaryItem>().FirstOrDefault();
            if ( summary == null )
            {
                Counter.Text = _counterText.Put( new object[  ]{ 0, Credentials.Count } );
            }
            else
            {
                Counter.Text = _counterText.Put( new object[  ] { CredentialsGrid.GetTotalSummaryValue( summary ), Credentials.Count } );
            }
        }

        private void CanExecuteSelectAllCommand( object sender, CanExecuteRoutedEventArgs e )
        {                                    
            e.CanExecute = ( _permissions  != null ) && ( _permissions.Count > 0 );
        }
        
        private void ExecuteSelectAllCommand( object sender, ExecutedRoutedEventArgs e )
        {
            foreach ( PermissionItem permissionItem in _permissions )
            {
                permissionItem.Value = true;
            }            
        }

        private void CanExecuteUnSelectAllCommand( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = ( _permissions != null ) && ( _permissions.Count > 0 );
        }

        private void ExecuteUnSelectAllCommand( object sender, ExecutedRoutedEventArgs e )
        {
            foreach ( PermissionItem permissionItem in _permissions )
            {
                permissionItem.Value = false;
            }            
        }                   

        private sealed class PermissionItem : NotifiableObject
        {
            private readonly PermissionCredentials _credentials;
            private readonly UserPermissions _permissions;
            private readonly Action _action;
            private readonly string _name;

            public PermissionItem( PermissionCredentials credentials, UserPermissions permissions, Action actionToBeTaken )
            {

                PermissionCredentials permissionCredentials = credentials;
                if ( permissionCredentials == null )
                {
                    throw new ArgumentNullException( "credentials" );
                }

                _credentials = permissionCredentials;
                _permissions = permissions;
                var action   = actionToBeTaken;

                if ( action == null )
                {
                    throw new ArgumentNullException( "changed" );
                }

                _action = action;
                _name = Ecng.ComponentModel.Extensions.GetDisplayName( ( object ) permissions );
            }

            public string Name
            {
                get
                {
                    return _name;
                }
            }

            public bool Value
            {
                get
                {
                    return _credentials.Permissions.ContainsKey( _permissions );
                }
                set
                {
                    if ( value )
                    {
                        CollectionHelper.SafeAdd<UserPermissions, SynchronizedDictionary<Tuple<string, string, object, DateTime?>, bool>>( _credentials.Permissions, _permissions );
                    }
                    else
                    {
                        _credentials.Permissions.Remove( _permissions );
                    }

                    _action();
                    NotifyChanged( nameof( Value ) );
                }
            }
        }
    }
}
