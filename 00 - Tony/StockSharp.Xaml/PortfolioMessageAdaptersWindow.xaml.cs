using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Xaml.GridControl;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    public partial class PortfolioMessageAdaptersWindow : DXWindow, IPersistable
    {
        public static readonly RoutedCommand RemoveCommand = new RoutedCommand();
        public static readonly RoutedCommand AddCommand = new RoutedCommand();
        private readonly ObservableCollection<PortfolioMessageAdaptersItem> _items = new ObservableCollection<PortfolioMessageAdaptersItem>();
        private BasketMessageAdapter _adapter;
        private PortfolioDataSource _portfolio;        

        public PortfolioMessageAdaptersWindow( )
        {
            InitializeComponent();
            PortfoliosGrid.ItemsSource = _items;
        }

        public BasketMessageAdapter Adapter
        {
            get
            {
                return _adapter;
            }
            set
            {
                BasketMessageAdapter basketMessageAdapter = value;
                if ( basketMessageAdapter == null )
                {
                    throw new ArgumentNullException( nameof( value ) );
                }

                _adapter = basketMessageAdapter;
            }
        }

        
        public PortfolioDataSource Portfolios
        {
            get
            {
                return _portfolio;
            }
            set
            {
                _portfolio = value;
            }
        }

        private IPortfolioMessageAdapterProvider PortfolioMessageAdapterProvider
        {
            get
            {
                return Adapter.AdapterProvider;
            }
            
        }

        private IEnumerable<PortfolioMessageAdaptersItem> SelectedItems
        {            
            get
            {
                IEnumerable<PortfolioMessageAdaptersItem> output;
                if ( PortfoliosGrid == null )
                {
                    output = null;
                }
                else
                {
                    output = PortfoliosGrid.SelectedItems.Cast<PortfolioMessageAdaptersItem>();

                    if ( output != null )
                    {
                        return output;
                    }
                }

                return Enumerable.Empty<PortfolioMessageAdaptersItem>();
            }            
        }

        private void ExecuteRemoveCommand( object sender, ExecutedRoutedEventArgs e )
        {
            _items.RemoveRange<PortfolioMessageAdaptersItem>( SelectedItems.ToArray<PortfolioMessageAdaptersItem>() );
        }

        private void CanExecuteRemoveCommand( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = SelectedItems.Any<PortfolioMessageAdaptersItem>();
        }

        private void ExecuteAddCommand( object sender, ExecutedRoutedEventArgs e )
        {
            _items.Add( new PortfolioMessageAdaptersItem() );
        }

        private void CanExecuteAddCommand( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = true;
        }

        private void OnButtonOkClicked( object sender, RoutedEventArgs e )
        {
            HashSet<string> stringSet = new HashSet<string>((IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase);
            foreach ( PortfolioMessageAdaptersItem item in ( Collection<PortfolioMessageAdaptersItem> ) _items )
            {
                if ( item.Adapter != null )
                {
                    if ( item.PortfolioName != null )
                    {
                        if ( !stringSet.Add( item.PortfolioName ) )
                        {
                            PortfolioMessageAdaptersWindow.ShowMessageBox( LocalizedStrings.Str1542Params.Put( item.PortfolioName ) );
                            return;
                        }
                    }
                    else
                    {
                        PortfolioMessageAdaptersWindow.ShowMessageBox( LocalizedStrings.Str3009.Put( ( object ) item ) );
                        return;
                    }
                }
                else
                {
                    PortfolioMessageAdaptersWindow.ShowMessageBox( LocalizedStrings.ConnectionNotSpecifiedParams.Put( ( object ) item ) );
                    return;
                }
            }
            

            foreach ( KeyValuePair<string, IMessageAdapter> adapter in PortfolioMessageAdapterProvider.PortfolioAdapters )
            {
                PortfolioMessageAdapterProvider.RemoveAssociation( adapter.Key );

            }

            foreach ( var item in _items )
            {
                PortfolioMessageAdapterProvider.SetAdapter( item.PortfolioName, item.Adapter );
            } 
            
            DialogResult = true;
        }

        private static void ShowMessageBox( string message )
        {
            int num = (int) new MessageBoxBuilder().Caption(LocalizedStrings.SecuritiesAndConnections ).Warning().Text(message).Show();
        }

        public void Load( SettingsStorage storage )
        {
            ( ( Window ) this ).LoadWindowSettings( storage );
            ( ( DependencyObject ) BarManager ).LoadDevExpressControl( ( string ) storage.GetValue<string>( "BarManager", null ) );
            SettingsStorage storage1 = (SettingsStorage) storage.GetValue<SettingsStorage>("PortfoliosGrid", null);
            if ( storage1 == null )
            {
                return;
            }

            PortfoliosGrid.Load( storage1 );
        }

        public void Save( SettingsStorage storage )
        {
            ( ( Window ) this ).SaveWindowSettings( storage );
            storage.SetValue<string>( "BarManager", ( ( DependencyObject ) BarManager ).SaveDevExpressControl() );
            storage.SetValue<SettingsStorage>( "PortfoliosGrid",  PersistableHelper.Save( ( IPersistable ) PortfoliosGrid ) );
        }

        

        private sealed class PortfolioMessageAdaptersItem
        {
            private string _portfolioName;
            private IMessageAdapter _adapter;

            public PortfolioMessageAdaptersItem( )
            {
            }

            public PortfolioMessageAdaptersItem( string portfolioName, IMessageAdapter adapter )
            {
                PortfolioName = portfolioName;
                Adapter = adapter;
            }

            public string PortfolioName
            {
                get
                {
                    return _portfolioName;
                }
                set
                {
                    _portfolioName = value;
                }
            }

            public IMessageAdapter Adapter
            {
                get
                {
                    return _adapter;
                }
                set
                {
                    _adapter = value;
                }
            }

            public override string ToString( )
            {
                return string.Format( "{0} <-> {1}", ( object ) PortfolioName, ( object ) Adapter );
            }
        }        

        private void OnPortfolioMessageAdaptersWindowLoaded( object sender, RoutedEventArgs e )
        {
            ( ( FrameworkElement ) this ).DataContext = ( object ) this;
            _items.Clear();

            _items.AddRange<PortfolioMessageAdaptersItem>(   PortfolioMessageAdapterProvider.PortfolioAdapters.Select( r => new PortfolioMessageAdaptersItem( r.Key, r.Value ) ) );
        }
    }


    internal sealed class SessionHolderToStringConverter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            if ( value is string )
            {
                return value;
            }

            IMessageAdapter messageAdapter = (IMessageAdapter) value;
            string displayName = messageAdapter.GetType().GetDisplayName((string) null);
            string str = messageAdapter.ToString();
            if ( str.IsEmpty() )
            {
                return ( object ) displayName;
            }

            return ( object ) StringHelper.Put( "{0} ({1})", new object[ 2 ] { ( object ) displayName, ( object ) str } );
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
