using DevExpress.Data.Extensions;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using Ecng.Common;
using Ecng.ComponentModel;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Xaml.PropertyGrid;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    public partial class OrderConditionalWindow : DXWindow
    {
        private sealed class MessageAdapter
        {
            private readonly IMessageAdapter _adapter;
            private readonly string _name;
            private readonly string _description;

            public MessageAdapter( IMessageAdapter adapter )
            {

                if ( adapter == null )
                {
                    throw new ArgumentNullException( "adapter" );
                }

                _adapter     = adapter;
                _name        = Ecng.ComponentModel.Extensions.GetDisplayName( ( ICustomAttributeProvider ) ( ( object ) adapter ).GetType(), ( string ) null );
                _description = Ecng.ComponentModel.Extensions.GetDescription( ( ICustomAttributeProvider ) ( ( object ) adapter ).GetType(), ( string ) null );
            }

            public IMessageAdapter Adapter
            {
                get
                {
                    return _adapter;
                }
            }

            public string Name
            {
                get
                {
                    return _name;
                }
            }

            public string Description
            {
                get
                {
                    return _description;
                }
            }
        }

        public static readonly DependencyProperty ConditionalEditorModeProperty = DependencyProperty.Register(nameof (OnlyEdit), typeof (bool), typeof (OrderConditionalWindow), (PropertyMetadata) new UIPropertyMetadata((object) false));

        private IMarketDataProvider _provider;
        private IPortfolioMessageAdapterProvider _messageAdapter;
        private IEnumerable<IMessageAdapter> _adapters;
        private Order _order;

        public OrderConditionalWindow( )
        {
            InitializeComponent();

            Order = new Order { Type = OrderTypes.Conditional };
        }

        public bool OnlyEdit
        {
            get
            {
                return ( bool ) GetValue( ConditionalEditorModeProperty );
            }
            set
            {
                SetValue( ConditionalEditorModeProperty, value );
            }
        }        

        public Order Order
        {
            get
            {
                _order.Security  = SecurityCtrl.SelectedSecurity;
                _order.Portfolio = PortfolioCtrl.SelectedPortfolio;
                _order.Price     = PriceCtrl.Value;
                _order.Volume    = VolumeCtrl.Value;
                _order.Direction = IsBuyCtrl.IsChecked == true ? Sides.Buy : Sides.Sell;
                _order.Condition = ( OrderCondition ) ConditionCtrl.SelectedObject;

                return _order;
            }
            set
            {
                if ( value == null )
                    throw new ArgumentNullException( nameof( value ) );

                if ( value.Type != OrderTypes.Conditional )
                    throw new NotSupportedException( LocalizedStrings.Str2872Params.Put( value.Type ) );

                _order = value;

                SecurityCtrl.SelectedSecurity   = value.Security;
                PortfolioCtrl.SelectedPortfolio = value.Portfolio;
                VolumeCtrl.Value                = value.Volume;
                PriceCtrl.Value                 = value.Price;
                IsBuyCtrl.IsChecked             = ( value.Direction == Sides.Buy );
                ConditionCtrl.SelectedObject    = value.Condition;

                if ( value.Condition != null )
                {
                    return;
                }
                
                CreateCondition();
            }
        }

        private void CreateCondition( )
        {
            ConditionCtrl.SelectedObject = ( Adapter?.CreateOrderCondition() );
        }

        public IPortfolioMessageAdapterProvider MessageAdapterProvider
        {
            get
            {
                return _messageAdapter;
            }
            set
            {
                _messageAdapter = value;
                if ( value == null )
                    return;

                Adapters = value.PortfolioAdapters.Select( x => x.Value ).Distinct().ToArray();
            }
        }

        public IEnumerable<IMessageAdapter> Adapters
        {
            get
            {
                return _adapters;
            }
            set
            {
                
                if ( value == null )
                {
                    throw new ArgumentNullException( nameof( value ) );
                }

                _adapters = value;
                AdapterType.ItemsSource = value.Select( x => new MessageAdapter( x )  ).ToArray();
            }
        }

        public IMessageAdapter Adapter
        {
            get
            {
                return ( ( MessageAdapter ) AdapterType.SelectedItem )?.Adapter;
            }
            set
            {                
                if ( value != null )
                {
                    if ( AdapterType.ItemsSource == null )
                    {
                        AdapterType.ItemsSource = ( new MessageAdapter[ ]
                        {
                            new MessageAdapter( value )
                        } );

                        AdapterType.SelectedIndex =  0;
                    }
                    else
                    {
                        var itemSource = ( MessageAdapter[ ] )AdapterType.ItemsSource;

                        var myList = itemSource.ToList( );

                        int index = myList.FindIndex( x => x.Adapter == value );

                        AdapterType.SelectedIndex = index;
                    }
                }
                else
                {
                    AdapterType.SelectedItem = null;
                }

                CreateCondition();
            }
        }

        private Security Security
        {
            get { return SecurityCtrl.SelectedSecurity; }
            set { SecurityCtrl.SelectedSecurity = value; }
        }

        public PortfolioDataSource Portfolios
        {
            get
            {
                return PortfolioCtrl.Portfolios;
            }
            set
            {
                PortfolioCtrl.Portfolios = value;
            }
        }

        public ISecurityProvider SecurityProvider
        {
            get
            {
                return SecurityCtrl.SecurityProvider;
            }
            set
            {
                SecurityCtrl.SecurityProvider = value;
            }
        }

        public IMarketDataProvider MarketDataProvider
        {
            get
            {
                return _provider;
            }
            set
            {
                _provider = value;
            }
        }

        private void SecurityCtrl_EditValueChanged( object sender, EditValueChangedEventArgs e )
        {
            TryEnableSend();
            bool noSelected = ( SecurityCtrl.SelectedSecurity == null );
            PriceCtrl.Increment = ( noSelected ? new Decimal( 1, 0, 0, false, ( byte ) 2 ) : SecurityCtrl.SelectedSecurity.PriceStep ?? Decimal.One );
            VolumeCtrl.Increment = ( noSelected ? Decimal.One : SecurityCtrl.SelectedSecurity.VolumeStep ?? Decimal.One );
        }

        private void TryEnableSend( )
        {
            Send.IsEnabled = OnlyEdit
                ? ( Adapter != null && ConditionCtrl.SelectedObject != null )
                : ( SecurityCtrl.SelectedSecurity != null && PortfolioCtrl.SelectedPortfolio != null && VolumeCtrl.Value > 0 && ConditionCtrl.SelectedObject != null );
            
        }

        
        private void PortfolioCtrl_EditValueChanged( object sender, EditValueChangedEventArgs e )
        {
            if ( Adapter == null && MessageAdapterProvider != null )
            {
                Portfolio selectedPortfolio = PortfolioCtrl.SelectedPortfolio;
                if ( selectedPortfolio != null )
                    Adapter = TraderHelper.GetAdapter( MessageAdapterProvider, selectedPortfolio );
            }

            TryEnableSend();
        }

        private void ConditionCtrl_SelectionChanged( object sender, RoutedEventArgs e )
        {
            CreateCondition();
            TryEnableSend();

        }
    }
}
