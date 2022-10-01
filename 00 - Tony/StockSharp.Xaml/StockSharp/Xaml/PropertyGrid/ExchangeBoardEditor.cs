using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;
using Ecng.Collections;
using Ecng.Xaml;

using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace StockSharp.Xaml.PropertyGrid
{
    public class ExchangeBoardEditor : ComboBoxEditSettings
    {
        public static readonly DependencyProperty ExchangeInfoProviderProperty = DependencyProperty.Register( nameof( ExchangeInfoProvider ), typeof( IExchangeInfoProvider ), typeof( ExchangeBoardEditor ), new PropertyMetadata( ( object )null, new PropertyChangedCallback( ExchangeBoardEditor.OnProviderPropertyChanged ) ) );
        private readonly ThreadSafeObservableCollection<ExchangeBoard> _exchangeBoards;
        private IExchangeInfoProvider _provider;

        public ExchangeBoardEditor( )
        {
            var observableCollectionEx = new UIObservableCollectionEx<ExchangeBoard>( );
            _exchangeBoards = new ThreadSafeObservableCollection<ExchangeBoard>( ( IListEx<ExchangeBoard> )observableCollectionEx );
            DisplayMember = "Code";
            var collectionViewSource = new CollectionViewSource( ) { Source = ( object )observableCollectionEx };
            collectionViewSource.SortDescriptions.Add( new SortDescription( DisplayMember, ListSortDirection.Ascending ) );
            ItemsSource = ( object )collectionViewSource.View;
            AutoComplete = true;
            ImmediatePopup = true;
            IncrementalFiltering = new bool?( true );
            AllowCollectionView = true;
            this.AddClearButton( ( object )null );
        }

        private static void OnProviderPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( ( ExchangeBoardEditor )d ).ChangeProvider( ( IExchangeInfoProvider )e.NewValue );
        }

        public IExchangeInfoProvider ExchangeInfoProvider
        {
            get
            {
                return _provider;
            }
            set
            {
                SetValue( ExchangeBoardEditor.ExchangeInfoProviderProperty, ( object )value );
            }
        }

        private void ChangeProvider( IExchangeInfoProvider newProvider )
        {
            _exchangeBoards.Clear( );
            if ( _provider != null )
            {
                _provider.BoardAdded -= new Action<ExchangeBoard>( _exchangeBoards.Add );
            }

            _provider = newProvider;
            if ( _provider == null )
            {
                return;
            }

            _exchangeBoards.AddRange( _provider.Boards );
            _provider.BoardAdded += new Action<ExchangeBoard>( _exchangeBoards.Add );
        }
    }
}
