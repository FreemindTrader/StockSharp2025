using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using Ecng.Collections;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Markup;

namespace StockSharp.Xaml.PropertyGrid
{
    public partial class SecurityEditor : ComboBoxEdit
    {
        public static readonly DependencyProperty SecurityProviderProperty = DependencyProperty.Register( nameof( SecurityProvider ), typeof( ISecurityProvider ), typeof( SecurityEditor ), new PropertyMetadata( ( object )null, new PropertyChangedCallback( SecurityEditor.OnSecurityProviderPropertyChanged ) ) );
        private ThreadSafeObservableCollection<Security> _itemsSource;
        private ISecurityProvider _securityProvider;        

        public SecurityEditor( )
        {
            InitializeComponent( );
            this.EditValueChanged += new EditValueChangedEventHandler( this.SecurityEditor_EditValueChanged );
        }

        private static void OnSecurityProviderPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( ( SecurityEditor )d ).UpdateProvider( ( ISecurityProvider )e.NewValue );
        }

        public ISecurityProvider SecurityProvider
        {
            get
            {
                return _securityProvider;
            }
            set
            {
                this.SetValue( SecurityEditor.SecurityProviderProperty, ( object )value );
            }
        }

        public Security SelectedSecurity
        {
            get
            {
                return ( Security )this.EditValue;
            }
            set
            {
                this.EditValue = ( object )value;
            }
        }

        public event Action SecuritySelected;

        private void UpdateProvider( ISecurityProvider provider )
        {
            if ( _securityProvider == provider )
            {
                return;
            }

            if ( _securityProvider != null )
            {
                _securityProvider.Added -= new Action<IEnumerable<Security>>( AddSecurities );
                _securityProvider.Removed -= new Action<IEnumerable<Security>>( RemoveSecurities );
                _securityProvider.Cleared -= new Action( ClearSecurities );
                this.ItemsSource = ( object )Enumerable.Empty<Security>( );
                _itemsSource = ( ThreadSafeObservableCollection<Security> )null;
            }
            _securityProvider = provider;
            if ( _securityProvider == null )
            {
                return;
            }

            var itemsSource = new ObservableCollectionEx<Security>( );
            _itemsSource = new ThreadSafeObservableCollection<Security>( ( IListEx<Security> )itemsSource );
            _itemsSource.BeforeUpdate += new Action( ( ( LookUpEditBase )this ).BeginDataUpdate );
            _itemsSource.AfterUpdate += new Action( ( ( LookUpEditBase )this ).EndDataUpdate );
            _itemsSource.AddRange( _securityProvider.LookupAll( ) );
            _securityProvider.Added += new Action<IEnumerable<Security>>( AddSecurities );
            _securityProvider.Removed += new Action<IEnumerable<Security>>( RemoveSecurities );
            _securityProvider.Cleared += new Action( ClearSecurities );
            this.DisplayMember = "Id";
            this.ItemsSource = ( object )itemsSource;
        }

        private void AddSecurities( IEnumerable<Security> ienumerable_0 )
        {
            _itemsSource.AddRange( ienumerable_0 );
        }

        private void RemoveSecurities( IEnumerable<Security> ienumerable_0 )
        {
            _itemsSource.RemoveRange( ienumerable_0 );
        }

        private void ClearSecurities( )
        {
            _itemsSource.Clear( );
        }

        private void SearchBtn_Click( object sender, RoutedEventArgs e )
        {
            BaseEdit ownerEdit = BaseEdit.GetOwnerEdit( ( DependencyObject )sender );
            if ( ownerEdit == null || this.IsReadOnly )
            {
                return;
            }

            SecurityPickerWindow wnd = new SecurityPickerWindow( ) { SecurityProvider = _securityProvider ?? ServicesRegistry.TrySecurityProvider, SelectionMode = MultiSelectMode.Row };
            if ( !wnd.ShowModal( ( DependencyObject )ownerEdit ) )
            {
                return;
            }

            ownerEdit.EditValue = ( object )wnd.SelectedSecurity;
        }

        private void CancelBtn_Click( object sender, RoutedEventArgs e )
        {
            BaseEdit ownerEdit = BaseEdit.GetOwnerEdit( ( DependencyObject )sender );
            if ( ownerEdit == null || this.IsReadOnly )
            {
                return;
            }

            ownerEdit.EditValue = null;
        }


        private void SecurityEditor_EditValueChanged( object sender, EditValueChangedEventArgs e )
        {
            SecuritySelected?.Invoke( );
        }
    }
}
