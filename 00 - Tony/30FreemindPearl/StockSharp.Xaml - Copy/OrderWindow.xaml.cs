using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace StockSharp.Xaml
{
    public partial class OrderWindow : DXWindow
    {
        private readonly SecurityData Data;
        private Security _security;
        private IMarketDataProvider _marketDataProvider;
        private IPortfolioMessageAdapterProvider _messageAdapter;
        private Order _order;
        private bool _isProcessingChanges;


        public OrderWindow( )
        {
            InitializeComponent( );
            TimeInForceCtrl.SetDataSource<OrderValidityEnum>( false );

            Order = new Order( )
            {
                Type = new OrderTypes?( OrderTypes.Limit )
            };

            DataContext = Data = new SecurityData( );
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

        public IMarketDataProvider MarketDataProvider
        {
            get
            {
                return _marketDataProvider;
            }
            set
            {
                if ( value == _marketDataProvider )
                {
                    return;
                }

                if ( _marketDataProvider != null )
                {
                    _marketDataProvider.ValuesChanged -= MarketDataProviderOnValuesChanged;
                }

                _marketDataProvider = value;

                if ( _marketDataProvider == null )
                {
                    return;
                }

                if ( _security != null )
                {
                    FillDataDefaults( );
                }

                _marketDataProvider.ValuesChanged += MarketDataProviderOnValuesChanged;
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

        public IPortfolioMessageAdapterProvider PortfolioMessageAdapterProvider
        {
            get
            {
                return _messageAdapter;
            }
            set
            {
                _messageAdapter = value;
            }
        }

        public Order Order
        {
            get
            {
                _order.Type          = new OrderTypes?( SelectedOrderType );
                _order.Security      = _security;
                _order.Portfolio     = Portfolio;
                _order.ClientCode    = ClientCodeCtrl.Text;
                _order.Price         = SelectedOrderType == OrderTypes.Market ? Decimal.Zero : PriceCtrl.Value;
                _order.Volume        = VolumeCtrl.Value;
                _order.VisibleVolume = VisibleVolumeCtrl.Value == Decimal.Zero ? new Decimal?( ) : new Decimal?( VisibleVolumeCtrl.Value );
                
                bool? isChecked      = IsBuyCtrl.IsChecked;
                int orderSize        = isChecked.GetValueOrDefault( ) & isChecked.HasValue ? 0 : 1;
                _order.Direction     =  ( Sides )orderSize;
                _order.Comment       = CommentCtrl.Text;
                _order.IsMarketMaker = IsMarketMaker.IsChecked;
                _order.IsMargin      = IsMargin.IsChecked;
                _order.IsManual      = IsManual.IsChecked;

                OrderValidityEnum? selectedValue = ( OrderValidityEnum? )TimeInForceCtrl.SelectedValue;
                if ( selectedValue.HasValue )
                {
                    switch ( selectedValue.GetValueOrDefault( ) )
                    {
                        case OrderWindow.OrderValidityEnum.Gtc:
                            _order.TimeInForce = new TimeInForce?( TimeInForce.PutInQueue );
                            break;
                        case OrderWindow.OrderValidityEnum.MatchOrCancel:
                            _order.TimeInForce = new TimeInForce?( TimeInForce.MatchOrCancel );
                            break;
                        case OrderWindow.OrderValidityEnum.CancelBalance:
                            _order.TimeInForce = new TimeInForce?( TimeInForce.CancelBalance );
                            break;
                        case OrderWindow.OrderValidityEnum.Today:
                            _order.TimeInForce = new TimeInForce?( TimeInForce.PutInQueue );
                            _order.ExpiryDate = new DateTimeOffset?( DateTime.Today.ApplyTimeZone( _security.Board.TimeZone ) );
                            break;
                        case OrderWindow.OrderValidityEnum.Gtd:
                            _order.TimeInForce = new TimeInForce?( TimeInForce.PutInQueue );
                            _order.ExpiryDate = new DateTimeOffset?( ( ( DateTime )ExpiryDate.EditValue ).ApplyTimeZone( _security.Board.TimeZone ) );
                            break;
                        default:
                            throw new ArgumentOutOfRangeException( );
                    }
                }
                OrderTypes? type = _order.Type;
                if ( type.GetValueOrDefault( ) == OrderTypes.Conditional & type.HasValue )
                {
                    if ( SelectedOrderCondition != null )
                    {
                        _order.Condition = SelectedOrderCondition;
                    }
                    else
                    {
                        OrderCondition orderCondition = MessageAdapter.CreateOrderCondition( );
                        Decimal? editValue = ( Decimal? )StopPriceCtrl.EditValue;
                        if ( TakeProfitType.IsSelected )
                        {
                            ( ( ITakeProfitOrderCondition )orderCondition ).ActivationPrice = editValue;
                        }
                        else
                        {
                            ( ( IStopLossOrderCondition )orderCondition ).ActivationPrice = editValue;
                        }
                    }
                }
                return _order;
            }
            set
            {
                Order order = value;
                if ( order == null )
                {
                    throw new ArgumentNullException( nameof( value ) );
                }

                _order                   = order;
                OrderTypes? type         = value.Type;

                SelectedOrderType        = ( type.HasValue ? type.GetValueOrDefault( ) : OrderTypes.Limit );
                Security                 = value.Security;
                Portfolio                = ( value.Portfolio );

                ClientCodeCtrl.Text      = value.ClientCode;
                PriceCtrl.Value          = value.Price == Decimal.Zero ? PriceCtrl.Increment : value.Price;
                VolumeCtrl.Value         = value.Volume == Decimal.Zero ? VolumeCtrl.Increment : value.Volume;
                VisibleVolumeCtrl.Value  = value.VisibleVolume ?? Decimal.Zero;
                IsBuyCtrl.IsChecked      = new bool?( value.Direction == Sides.Buy );
                IsSellCtrl.IsChecked     = new bool?( value.Direction == Sides.Sell );
                CommentCtrl.Text         = value.Comment;
                IsMarketMaker.IsChecked  = value.IsMarketMaker;
                IsMargin.IsChecked       = value.IsMargin;
                IsManual.IsChecked       = value.IsManual;
                TimeInForce? timeInForce = value.TimeInForce;

                if ( timeInForce.HasValue )
                {
                    switch ( timeInForce.GetValueOrDefault( ) )
                    {
                        case TimeInForce.PutInQueue:
                            break;
                        case TimeInForce.MatchOrCancel:
                            TimeInForceCtrl.SelectedValue = OrderValidityEnum.MatchOrCancel;
                            goto label_12;
                        case TimeInForce.CancelBalance:
                            TimeInForceCtrl.SelectedValue = OrderValidityEnum.CancelBalance;
                            goto label_12;
                        default:
                            throw new ArgumentOutOfRangeException( );
                    }
                }
                if ( !value.ExpiryDate.HasValue )
                {
                    TimeInForceCtrl.SelectedValue = OrderValidityEnum.Gtc;
                }
                else
                {
                    DateTimeOffset? expiryDate = value.ExpiryDate;
                    DateTimeOffset dateTimeOffset = DateTimeOffset.Now.Date.ApplyTimeZone( _security.Board.TimeZone );
                    if ( ( expiryDate.HasValue ? ( expiryDate.HasValue ? ( expiryDate.GetValueOrDefault( ) == dateTimeOffset ? 1 : 0 ) : 1 ) : 0 ) != 0 )
                    {
                        TimeInForceCtrl.SelectedValue = OrderValidityEnum.Today;
                    }
                    else
                    {
                        TimeInForceCtrl.SelectedValue = OrderValidityEnum.Gtd;
                        DateEdit dateEdit0            = ExpiryDate;
                        expiryDate                    = value.ExpiryDate;
                        var date                      = ( ValueType )expiryDate.Value.Date;
                        dateEdit0.EditValue           = date;
                    }
                }
                label_12:
                if ( _order.State == OrderStates.None )
                {
                    return;
                }

                SecurityEnabled = false;
                PortfolioEnabled = false;
                OrderTypeEnabled = false;
            }
        }

        private OrderTypes SelectedOrderType
        {
            get
            {
                switch ( TypeCtrl.SelectedIndex )
                {
                    case 0:
                        return OrderTypes.Limit;
                    case 1:
                        return OrderTypes.Market;
                    case 2:
                    case 3:
                        return OrderTypes.Repo;
                    default:
                        throw new ArgumentOutOfRangeException( );
                }
            }

            set
            {
                switch ( value )
                {
                    case OrderTypes.Limit:
                        TypeCtrl.SelectedIndex = 0;
                        break;
                    case OrderTypes.Market:
                        TypeCtrl.SelectedIndex = 1;
                        break;
                    case OrderTypes.Conditional:
                        TypeCtrl.SelectedIndex = 2;
                        break;

                    case OrderTypes.Repo:
                    case OrderTypes.ExtRepo:
                    case OrderTypes.Rps:
                    case OrderTypes.Execute:
                        throw new NotSupportedException( LocalizedStrings.Str2872Params.Put( value ) );
                    default:
                        throw new ArgumentOutOfRangeException( );
                }
            }
        }

        private Security Security
        {
            get { return SecurityCtrl.SelectedSecurity; }
            set { SecurityCtrl.SelectedSecurity = value; }
        }

        private Portfolio Portfolio
        {
            get { return PortfolioCtrl.SelectedPortfolio; }
            set { PortfolioCtrl.SelectedPortfolio = value; }
        }

        public bool SecurityEnabled
        {
            get
            {
                return SecurityCtrl.IsEnabled;
            }
            set
            {
                SecurityCtrl.IsEnabled = value;
            }
        }

        public bool PortfolioEnabled
        {
            get
            {
                return PortfolioCtrl.IsEnabled;
            }
            set
            {
                PortfolioCtrl.IsEnabled = value;
            }
        }

        public bool OrderTypeEnabled
        {
            get
            {
                return TypeCtrl.IsEnabled;
            }
            set
            {
                TypeCtrl.IsEnabled = value;
            }
        }

        private IMessageAdapter MessageAdapter
        {
            get
            {
                if ( PortfolioMessageAdapterProvider == null )
                {
                    return null;
                }

                Portfolio portfolio = Portfolio;
                if ( portfolio == null )
                {
                    return null;
                }

                return PortfolioMessageAdapterProvider.GetAdapter( portfolio );
            }
        }

       
        

        private void FillDataDefaults( )
        {
            Data.BestBidPrice   = GetSecurityValue( Level1Fields.BestBidPrice );
            Data.BestAskPrice   = GetSecurityValue( Level1Fields.BestAskPrice );
            Data.LastTradePrice = GetSecurityValue( Level1Fields.LastTradePrice );
            Data.MinPrice       = GetSecurityValue( Level1Fields.MinPrice );
            Data.MaxPrice       = GetSecurityValue( Level1Fields.MaxPrice );

            UpdateSpreadSize( );
        }

        private void UpdateSpreadSize( )
        {
            if ( Data.BestBidPrice.HasValue && Data.BestAskPrice.HasValue )
            {
                Decimal? spreadSize = Data.BestAskPrice.HasValue & Data.BestBidPrice.HasValue ? new Decimal?( Data.BestAskPrice.Value - Data.BestBidPrice.Value ) : new Decimal?( );
                Data.SpreadSize = spreadSize;
            }
            else
            {
                Data.SpreadSize = new decimal?( );
            }
        }

        private Decimal? GetSecurityValue( Level1Fields lvlFields )
        {
            return ( Decimal? )MarketDataProvider.GetSecurityValue( _security, lvlFields );
        }

        private void VolumeCtrl_ValueChanged( object sender, EditValueChangedEventArgs e )
        {
            UpdateAmount( );
            TryEnableSend( );
        }

        private void PriceCtrl_OnValueChanged( object sender, EditValueChangedEventArgs e )
        {
            UpdateAmount( );
            TryEnableSend( );
        }

        private void TryEnableSend( )
        {
            Send.IsEnabled = ( Security != null ) && ( Portfolio != null ) && ( VolumeCtrl.Value > 0 ) && ( SelectedOrderType == OrderTypes.Market || PriceCtrl.Value > 0 ) && ( !ExpiryDate.IsEnabled || ExpiryDate.EditValue != null );
        }

        private void MarketDataProviderOnValuesChanged(
          Security curSecurity,
          IEnumerable<KeyValuePair<Level1Fields, object>> ienumerable_0,
          DateTimeOffset dateTimeOffset_0,
          DateTimeOffset dateTimeOffset_1 )
        {
            if ( _security != curSecurity )
            {
                return;
            }

            foreach ( KeyValuePair<Level1Fields, object> keyValuePair in ienumerable_0 )
            {
                switch ( keyValuePair.Key )
                {
                    case Level1Fields.MinPrice:
                        Data.MinPrice = new Decimal?( ( Decimal )keyValuePair.Value );
                        continue;
                    case Level1Fields.MaxPrice:
                        Data.MaxPrice = new Decimal?( ( Decimal )keyValuePair.Value );
                        continue;
                    case Level1Fields.LastTradePrice:
                        Data.LastTradePrice = new Decimal?( ( Decimal )keyValuePair.Value );
                        continue;
                    case Level1Fields.BestBidPrice:
                        Data.BestBidPrice = new Decimal?( ( Decimal )keyValuePair.Value );
                        UpdateSpreadSize( );
                        continue;
                    case Level1Fields.BestAskPrice:
                        Data.BestAskPrice = new Decimal?( ( Decimal )keyValuePair.Value );
                        UpdateSpreadSize( );
                        continue;
                    default:
                        continue;
                }
            }
        }

        private void OnMaxPriceClicked( object sender, RoutedEventArgs e )
        {
            Decimal? maxPrice = Data.MaxPrice;
            if ( !maxPrice.HasValue )
            {
                return;
            }

            PriceCtrl.Value = maxPrice.Value;
            SelectedOrderType = ( OrderTypes.Limit );
        }

        private void OnMinPriceClicked( object sender, RoutedEventArgs e )
        {
            Decimal? minPrice = Data.MinPrice;
            if ( !minPrice.HasValue )
            {
                return;
            }

            PriceCtrl.Value = minPrice.Value;
            SelectedOrderType = ( OrderTypes.Limit );
        }

        private void OnLastTradePriceClicked( object sender, RoutedEventArgs e )
        {
            Decimal? lastTradePrice = Data.LastTradePrice;
            if ( !lastTradePrice.HasValue )
            {
                return;
            }

            PriceCtrl.Value = lastTradePrice.Value;
            SelectedOrderType = ( OrderTypes.Limit );
        }

        private void OnBestBidPriceClicked( object sender, RoutedEventArgs e )
        {
            Decimal? bestBidPrice = Data.BestBidPrice;
            if ( !bestBidPrice.HasValue )
            {
                return;
            }

            PriceCtrl.Value = bestBidPrice.Value;
            SelectedOrderType = ( OrderTypes.Limit );
        }

        private void OnBestAskPriceClicked( object sender, RoutedEventArgs e )
        {
            Decimal? bestAskPrice = Data.BestAskPrice;
            if ( !bestAskPrice.HasValue )
            {
                return;
            }

            PriceCtrl.Value = bestAskPrice.Value;
            SelectedOrderType = ( OrderTypes.Limit );
        }

        private void Vol1_OnClick( object sender, RoutedEventArgs e )
        {
            VolumeCtrl.Value = Decimal.One;
        }

        private void Vol10_OnClick( object sender, RoutedEventArgs e )
        {
            VolumeCtrl.Value = new Decimal( 10 );
        }

        private void Vol20_OnClick( object sender, RoutedEventArgs e )
        {
            VolumeCtrl.Value = new Decimal( 20 );
        }

        private void Vol50_OnClick( object sender, RoutedEventArgs e )
        {
            VolumeCtrl.Value = new Decimal( 50 );
        }

        private void Vol100_OnClick( object sender, RoutedEventArgs e )
        {
            VolumeCtrl.Value = new Decimal( 100 );
        }

        private void Vol200_OnClick( object sender, RoutedEventArgs e )
        {
            VolumeCtrl.Value = new Decimal( 200 );
        }

        private void ToVolume_OnClick( object sender, RoutedEventArgs e )
        {
            Decimal num1 = AmountCtrl.Value;
            Decimal num2 = GetPrice( );
            Decimal num3 = VolumeCtrl.Value;
            _isProcessingChanges = true;
            try
            {
                if ( num2 == Decimal.Zero )
                {
                    if ( num3 == Decimal.Zero )
                    {
                        return;
                    }

                    Decimal increment = PriceCtrl.Increment;
                    PriceCtrl.Value = ( num1 / num3 ).Round( increment, increment.GetCachedDecimals( ), new MidpointRounding?( ) );
                }
                else
                {
                    Decimal increment = VolumeCtrl.Increment;
                    VolumeCtrl.Value = ( num1 / num2 ).Round( increment, increment.GetCachedDecimals( ), new MidpointRounding?( ) );
                }
            }
            finally
            {
                _isProcessingChanges = false;
            }
        }

        private Decimal GetPrice( )
        {
            if ( !( PriceCtrl.Value != Decimal.Zero ) )
            {
                return Data.LastTradePrice ?? Decimal.Zero;
            }

            return PriceCtrl.Value;
        }

        private void UpdateAmount( )
        {
            if ( _isProcessingChanges )
            {
                return;
            }

            AmountCtrl.Value = GetPrice( ) * VolumeCtrl.Value;
        }

        private void AmountCtrl_OnValueChanged( object sender, EditValueChangedEventArgs e )
        {
            ToVolume.IsEnabled = AmountCtrl.Value != Decimal.Zero;
        }

        private void TimeInForceCtrl_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            EnumComboBoxHelper.EnumerationMember enumerationMember = e.AddedItems.Cast<EnumComboBoxHelper.EnumerationMember>( ).FirstOrDefault( );
            DateEdit dateEdit0 = ExpiryDate;
            int num;
            if ( enumerationMember != null )
            {
                OrderValidityEnum? nullable = ( OrderValidityEnum? )enumerationMember.Value;
                num = nullable.GetValueOrDefault( ) == OrderWindow.OrderValidityEnum.Gtd & nullable.HasValue ? 1 : 0;
            }
            else
            {
                num = 0;
            }

            dateEdit0.IsEnabled = num != 0;
            TryEnableSend( );
        }

        private void ExpiryDate_ValueChanged( object sender, EditValueChangedEventArgs e )
        {
            TryEnableSend( );
        }

        private void StopPriceCtrl_ValueChanged( object sender, EditValueChangedEventArgs e )
        {
            TryEnableSend( );
        }

        

        public OrderCondition SelectedOrderCondition
        {
            get { return ( OrderCondition )ConditionCtrl.SelectedObject; }
            set
            {
                ConditionCtrl.SelectedObject = value;
            }
        }

        private void ExtendedCtrl_ValueChanged( object sender, EditValueChangedEventArgs e )
        {
            bool? isChecked = ExtendedCtrl.IsChecked;

            if ( isChecked.GetValueOrDefault( ) & isChecked.HasValue )
            {
                SelectedOrderCondition = MessageAdapter.CreateOrderCondition( );
                ConditionCtrl.Visibility = Visibility.Visible;
                TypeCtrl.IsEnabled = false;
            }
            else
            {
                SelectedOrderCondition = null;
                ConditionCtrl.Visibility = Visibility.Collapsed;
                TypeCtrl.IsEnabled = true;
            }
        }



        private enum OrderValidityEnum
        {
            [Display( Name = "GTC", ResourceType = typeof( LocalizedStrings ) )] Gtc,
            [Display( Name = "FOK", ResourceType = typeof( LocalizedStrings ) )] MatchOrCancel,
            [Display( Name = "IOC", ResourceType = typeof( LocalizedStrings ) )] CancelBalance,
            [Display( Name = "Session", ResourceType = typeof( LocalizedStrings ) )] Today,
            [Display( Name = "GTD", ResourceType = typeof( LocalizedStrings ) )] Gtd,
        }

        private sealed class SecurityData : NotifiableObject
        {
            private Decimal? _bestBidPrice;
            private Decimal? _bestAskPrice;
            private Decimal? _spreadSize;
            private Decimal? _lastTradePrice;
            private Decimal? _minPrice;
            private Decimal? _maxPrice;

            public Decimal? BestBidPrice
            {
                get
                {
                    return _bestBidPrice;
                }
                set
                {
                    _bestBidPrice = value;
                    NotifyChanged( nameof( BestBidPrice ) );
                }
            }

            public Decimal? BestAskPrice
            {
                get
                {
                    return _bestAskPrice;
                }
                set
                {
                    _bestAskPrice = value;
                    NotifyChanged( nameof( BestAskPrice ) );
                }
            }

            public Decimal? SpreadSize
            {
                get
                {
                    return _spreadSize;
                }
                set
                {
                    _spreadSize = value;
                    NotifyChanged( nameof( SpreadSize ) );
                }
            }

            public Decimal? LastTradePrice
            {
                get
                {
                    return _lastTradePrice;
                }
                set
                {
                    _lastTradePrice = value;
                    NotifyChanged( nameof( LastTradePrice ) );
                }
            }

            public Decimal? MinPrice
            {
                get
                {
                    return _minPrice;
                }
                set
                {
                    _minPrice = value;
                    NotifyChanged( nameof( MinPrice ) );
                }
            }

            public Decimal? MaxPrice
            {
                get
                {
                    return _maxPrice;
                }
                set
                {
                    _maxPrice = value;
                    NotifyChanged( nameof( MaxPrice ) );
                }
            }
        }
        

        private void SecurityCtrl_EditValueChanged( object sender, EditValueChangedEventArgs e )
        {
            _security = Security;
            TryEnableSend( );
            bool flag = _security == null;
            SpinEdit spinEdit0 = PriceCtrl;
            Decimal? nullable1;
            Decimal num1;
            if ( !flag )
            {
                nullable1 = _security.PriceStep;
                num1 = nullable1 ?? Decimal.One;
            }
            else
            {
                num1 = new Decimal( 1, 0, 0, false, 2 );
            }

            spinEdit0.Increment = num1;
            SpinEdit spinEdit2 = VolumeCtrl;
            Decimal num2;
            if ( !flag )
            {
                nullable1 = _security.VolumeStep;
                num2 = nullable1 ?? Decimal.One;
            }
            else
            {
                num2 = Decimal.One;
            }

            spinEdit2.Increment = num2;
            SecurityData class5790_1 = Data;
            SecurityData class5790_2 = Data;
            SecurityData class5790_3 = Data;
            SecurityData class5790_4 = Data;
            SecurityData class5790_5 = Data;
            SecurityData class5790_6 = Data;
            Decimal? nullable2 = new Decimal?( );
            Decimal? nullable3 = nullable2;
            class5790_6.MaxPrice = nullable3;
            Decimal? nullable4;
            Decimal? nullable5 = nullable4 = nullable2;
            class5790_5.MinPrice = nullable4;
            Decimal? nullable6;
            Decimal? nullable7 = nullable6 = nullable5;
            class5790_4.LastTradePrice = nullable6;
            Decimal? nullable8;
            Decimal? nullable9 = nullable8 = nullable7;
            class5790_3.SpreadSize = nullable8;
            Decimal? nullable10;
            nullable1 = nullable10 = nullable9;
            class5790_2.BestAskPrice = nullable10;
            Decimal? nullable11 = nullable1;
            class5790_1.BestBidPrice = nullable11;
            if ( flag || MarketDataProvider == null )
            {
                return;
            }

            FillDataDefaults( );
        }

        private void PortfolioCtrl_EditValueChanged( object sender, EditValueChangedEventArgs e )
        {
            IMessageAdapter messageAdapter = MessageAdapter;
            if ( messageAdapter != null )
            {
                TakeProfitType.IsEnabled = messageAdapter.IsSupportTakeProfit;
                StopLossType.IsEnabled = messageAdapter.IsSupportStopLoss;
                if ( SelectedOrderType == OrderTypes.Conditional && !TakeProfitType.IsEnabled && !StopLossType.IsEnabled )
                {
                    SelectedOrderType = ( OrderTypes.Limit );
                }
            }
            else
            {
                ComboBoxEditItem comboBoxEditItem0 = TakeProfitType;
                StopLossType.IsEnabled = false;
                comboBoxEditItem0.IsEnabled = false;
                if ( SelectedOrderType == OrderTypes.Conditional )
                {
                    SelectedOrderType = ( OrderTypes.Limit );
                }
            }
            TryEnableSend( );
        }

        private void TypeCtrl_SelectedIndexChanged( object sender, RoutedEventArgs e )
        {
            PriceCtrl.IsEnabled = SelectedOrderType != OrderTypes.Market;
            ExtendedCtrl.IsEnabled = StopPriceCtrl.IsEnabled = SelectedOrderType == OrderTypes.Conditional;
            if ( !ExtendedCtrl.IsEnabled )
            {
                bool? isChecked = ExtendedCtrl.IsChecked;
                if ( isChecked.GetValueOrDefault( ) & isChecked.HasValue )
                {
                    ExtendedCtrl.IsChecked = new bool?( false );
                }
            }
            TryEnableSend( );
        }


        private void TypeCtrl_IndexChanged( object sender, RoutedEventArgs e )
        {
            
        }
    }
}
