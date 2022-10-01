using DevExpress.Xpf.Editors.Settings;
using Ecng.Collections;
using Ecng.Xaml;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using System;
using System.Windows;

namespace StockSharp.Xaml.PropertyGrid
{
    public class ExchangeEditor : ComboBoxEditSettings
    {
        public static readonly DependencyProperty ExchangeInfoProviderProperty = DependencyProperty.Register(nameof (ExchangeInfoProvider), typeof (IExchangeInfoProvider), typeof (ExchangeEditor), new PropertyMetadata((object) null, new PropertyChangedCallback(ExchangeEditor.OnPropertyChangedCallback)));
        private readonly ThreadSafeObservableCollection<Exchange> _exchanges;
        private IExchangeInfoProvider _provider;

        public ExchangeEditor( )
        {            
            var observableCollectionEx = new UIObservableCollectionEx<Exchange>();
            _exchanges                 = new ThreadSafeObservableCollection<Exchange>( ( IListEx<Exchange> ) observableCollectionEx );

            DisplayMember              = ( "Name" );
            ItemsSource                = ( ( object ) observableCollectionEx );
            AutoComplete               = true;
            ImmediatePopup             = true;
            IncrementalFiltering       = true;
            AllowCollectionView        = true;
        }

        private static void OnPropertyChangedCallback( DependencyObject d, DependencyPropertyChangedEventArgs eventArgs )
        {
            var exEditor = ( ExchangeEditor ) d;            

            exEditor.ChangeProvider( ( IExchangeInfoProvider ) eventArgs.NewValue );
        }

        private void ChangeProvider( IExchangeInfoProvider provider )
        {
            _exchanges.Clear( );
            if( _provider != null )
                _provider.ExchangeAdded -= _exchanges.Add;

            _provider = provider;
            if ( _provider == null )
                return;
            _exchanges.AddRange( _provider.Exchanges );

            _provider.ExchangeAdded += _exchanges.Add;
        }

        public IExchangeInfoProvider ExchangeInfoProvider
        {
            get
            {
                return _provider;
            }
            set
            {
                SetValue( ExchangeInfoProviderProperty, value );
            }
        }

        
    }
}
