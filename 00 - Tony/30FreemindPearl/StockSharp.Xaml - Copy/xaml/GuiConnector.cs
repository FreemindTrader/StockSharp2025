using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.BusinessEntities;
using StockSharp.Logging;
using StockSharp.Messages;
using System;
using System.Collections.Generic;

namespace StockSharp.Xaml
{
    public class GuiConnector<TUnderlyingConnector> : BaseLogReceiver, IConnector, IPersistable, ISecurityProvider, IMarketDataProvider, IDisposable, ILogReceiver, ILogSource, INewsProvider, IPortfolioProvider, IPositionProvider, IMessageSender where TUnderlyingConnector : IConnector
    {        
        public GuiConnector( TUnderlyingConnector connector )
        {
            Connector = connector;
        }

        private TUnderlyingConnector _connector;
       

        public TUnderlyingConnector Connector
        {
            get { return _connector; }
            private set
            {
                if ( value.IsNull() )
                    throw new ArgumentNullException();

                _connector = value;

                Connector.NewPortfolios                     += NewPortfoliosHandler;
                Connector.PortfoliosChanged                 += PortfoliosChangedHandler;
                Connector.NewPositions                      += NewPositionsHandler;
                Connector.PositionsChanged                  += PositionsChangedHandler;
                Connector.NewSecurities                     += NewSecuritiesHandler;
                Connector.SecuritiesChanged                 += SecuritiesChangedHandler;
                Connector.NewTrades                         += NewTradesHandler ;
                Connector.NewMyTrades                       += NewMyTradesHandler ;
                Connector.NewOrders                         += NewOrdersHandler ;
                Connector.OrdersChanged                     += OrdersChangedHandler ;
                Connector.OrdersRegisterFailed              += OrdersRegisterFailedHandler ;
                Connector.OrdersCancelFailed                += OrdersCancelFailedHandler ;
                Connector.NewStopOrders                     += NewStopOrdersHandler ;
                Connector.StopOrdersChanged                 += StopOrdersChangedHandler ;
                Connector.StopOrdersRegisterFailed          += StopOrdersRegisterFailedHandler ;
                Connector.StopOrdersCancelFailed            += StopOrdersCancelFailedHandler ;
                Connector.NewMarketDepths                   += NewMarketDepthsHandler ;
                Connector.MarketDepthsChanged               += MarketDepthsChangedHandler ;
                Connector.NewOrderLogItems                  += NewOrderLogItemsHandler ;
                Connector.NewNews                           += NewNewsHandler ;
                Connector.NewsChanged                       += NewsChangedHandler ;
                Connector.NewMessage                        += NewMessageHandler ;
                Connector.Connected                         += ConnectedHandler ;
                Connector.Disconnected                      += DisconnectedHandler ;
                Connector.ConnectionError                   += ConnectionErrorHandler ;
                Connector.ConnectedEx                       += ConnectedExHandler ;
                Connector.DisconnectedEx                    += DisconnectedExHandler ;
                Connector.ConnectionErrorEx                 += ConnectionErrorExHandler ;
                Connector.Error                             += ErrorHandler ;
                Connector.MarketTimeChanged                 += MarketTimeChangedHandler ;
                Connector.LookupSecuritiesResult            += Connector_LookupSecuritiesResult;
                Connector.LookupSecuritiesResult2           += Connector_LookupSecuritiesResult2;
                Connector.LookupPortfoliosResult            += Connector_LookupPortfoliosResult;
                Connector.LookupPortfoliosResult2           += Connector_LookupPortfoliosResult2;

                Connector.MarketDataSubscriptionSucceeded   += MarketDataSubscriptionSucceededHandler ;
                Connector.MarketDataSubscriptionFailed      += MarketDataSubscriptionFailedHandler ;
                Connector.MarketDataUnSubscriptionSucceeded += MarketDataUnSubscriptionSucceededHandler ;
                Connector.MarketDataUnSubscriptionFailed    += MarketDataUnSubscriptionFailedHandler ;
                Connector.MarketDataSubscriptionFinished    += MarketDataSubscriptionFinishedHandler ;
                Connector.MarketDataUnexpectedCancelled += Connector_MarketDataUnexpectedCancelled;
                Connector.SessionStateChanged               += SessionStateChangedHandler ;
                Connector.ValuesChanged                     += ValuesChangedHandler ;
                Connector.NewPortfolio                      += NewPortfolioHandler ;
                Connector.PortfolioChanged                  += PortfolioChangedHandler ;
                Connector.NewPosition                       += NewPositionHandler ;
                Connector.PositionChanged                   += PositionChangedHandler ;
                Connector.NewSecurity                       += NewSecurityHandler ;
                Connector.SecurityChanged                   += SecurityChangedHandler ;
                Connector.NewTrade                          += NewTradeHandler ;
                Connector.NewMyTrade                        += NewMyTradeHandler ;
                Connector.NewOrder                          += NewOrderHandler ;
                Connector.OrderChanged                      += OrderChangedHandler ;
                Connector.OrderRegisterFailed               += OrderRegisterFailedHandler ;
                Connector.OrderCancelFailed                 += OrderCancelFailedHandler ;
                Connector.NewStopOrder                      += NewStopOrderHandler ;
                Connector.StopOrderChanged                  += StopOrderChangedHandler ;
                Connector.StopOrderRegisterFailed           += StopOrderRegisterFailedHandler ;
                Connector.StopOrderCancelFailed             += StopOrderCancelFailedHandler ;
                Connector.MassOrderCanceled                 += MassOrderCanceledHandler ;
                Connector.MassOrderCancelFailed             += MassOrderCancelFailedHandler ;
                Connector.OrderStatusFailed                 += OrderStatusFailedHandler ;
                Connector.NewMarketDepth                    += NewMarketDepthHandler ;
                Connector.MarketDepthChanged                += MarketDepthChangedHandler ;
                Connector.NewOrderLogItem                   += NewOrderLogItemHandler ;
                Connector.LookupBoardsResult                += Connector_LookupBoardsResult1;
                Connector.LookupBoardsResult2               += Connector_LookupBoardsResult2;
            }
        }

        private void Connector_MarketDataUnexpectedCancelled( Security sec, MarketDataMessage msg, Exception ex )
        {
            AddGuiAction( ( ) => MarketDataUnexpectedCancelled?.Invoke( sec, msg, ex ) );
        }

        private void Connector_LookupBoardsResult2( BoardLookupMessage msg, IEnumerable<ExchangeBoard> b1, IEnumerable<ExchangeBoard> b2, Exception ex )
        {
            AddGuiAction( ( ) => LookupBoardsResult2?.Invoke( msg, b1, b2, ex ) );
        }

        public event Action<BoardLookupMessage, IEnumerable<ExchangeBoard>, Exception> LookupBoardsResult;

        private void Connector_LookupBoardsResult1( BoardLookupMessage msg, IEnumerable<ExchangeBoard> b1, Exception ex )
        {
            AddGuiAction( ( ) => LookupBoardsResult?.Invoke( msg, b1, ex ) );
        }

        private void Connector_LookupPortfoliosResult2( PortfolioLookupMessage msg, IEnumerable<Portfolio> pro1, IEnumerable<Portfolio> pro2, Exception ex )
        {
            
            AddGuiAction( ( ) => LookupPortfoliosResult2?.Invoke( msg, pro1, pro2, ex ) );
        }

        public event Action<PortfolioLookupMessage, IEnumerable<Portfolio>, Exception> LookupPortfoliosResult;        

        private void Connector_LookupPortfoliosResult( PortfolioLookupMessage msg, IEnumerable<Portfolio> pro, Exception ex )
        {
            AddGuiAction( ( ) => LookupPortfoliosResult?.Invoke( msg, pro, ex ) );
        }

        public event Action<SecurityLookupMessage, IEnumerable<Security>, IEnumerable<Security>, Exception> LookupSecuritiesResult2;
        

        private void Connector_LookupSecuritiesResult2( SecurityLookupMessage msg, IEnumerable<Security> sec1, IEnumerable<Security> sec2, Exception ex )
        {
            AddGuiAction( ( ) => LookupSecuritiesResult2?.Invoke( msg, sec1, sec2, ex ) );
        }


        public event Action<SecurityLookupMessage, IEnumerable<Security>, Exception> LookupSecuritiesResult;        

        private void Connector_LookupSecuritiesResult( SecurityLookupMessage msg, IEnumerable<Security> securities, Exception ex )
        {
            AddGuiAction( ( ) => LookupSecuritiesResult?.Invoke( msg, securities, ex ) );
        }

        
        public event Action<Security, MarketDataFinishedMessage> MarketDataSubscriptionFinished;
        
        private void MarketDataSubscriptionFinishedHandler( Security sec, MarketDataFinishedMessage msg )
        {
            AddGuiAction( ( ) => MarketDataSubscriptionFinished?.Invoke( sec, msg ) );
        }

        public event Action<IEnumerable<Portfolio>> NewPortfolios;

        private void NewPortfoliosHandler( IEnumerable<Portfolio> portfolios )
        {
            AddGuiAction( ( ) => NewPortfolios?.Invoke( portfolios ) );
        }

        public event Action<IEnumerable<Portfolio>> PortfoliosChanged;

        private void PortfoliosChangedHandler( IEnumerable<Portfolio> portfolios )
        {
            AddGuiAction( ( ) => PortfoliosChanged?.Invoke( portfolios ) );
        }

        public event Action<IEnumerable<Position>> NewPositions;

        private void NewPositionsHandler( IEnumerable<Position> positions )
        {
            AddGuiAction( ( ) => NewPositions?.Invoke( positions ) );
        }

        public event Action<IEnumerable<Position>> PositionsChanged;

        private void PositionsChangedHandler( IEnumerable<Position> positions )
        {
            AddGuiAction( ( ) => PositionsChanged?.Invoke( positions ) );
        }

        public event Action<IEnumerable<Security>> NewSecurities;

        private void NewSecuritiesHandler( IEnumerable<Security> securities )
        {
            AddGuiAction( ( ) => NewSecurities?.Invoke( securities ) );
        }

        public event Action<IEnumerable<Security>> SecuritiesChanged;

        private void SecuritiesChangedHandler( IEnumerable<Security> securities )
        {
            AddGuiAction( ( ) => SecuritiesChanged?.Invoke( securities ) );
        }

        public event Action<IEnumerable<Trade>> NewTrades;

        private void NewTradesHandler( IEnumerable<Trade> trades )
        {
            AddGuiAction( ( ) => NewTrades?.Invoke( trades ) );
        }

        public event Action<IEnumerable<MyTrade>> NewMyTrades;

        private void NewMyTradesHandler( IEnumerable<MyTrade> trades )
        {
            AddGuiAction( ( ) => NewMyTrades?.Invoke( trades ) );
        }

        public event Action<IEnumerable<Order>> NewOrders;

        private void NewOrdersHandler( IEnumerable<Order> orders )
        {
            AddGuiAction( ( ) => NewOrders?.Invoke( orders ) );
        }

        public event Action<IEnumerable<Order>> OrdersChanged;

        private void OrdersChangedHandler( IEnumerable<Order> orders )
        {
            AddGuiAction( ( ) => OrdersChanged?.Invoke( orders ) );
        }

        public event Action<IEnumerable<OrderFail>> OrdersRegisterFailed;

        private void OrdersRegisterFailedHandler( IEnumerable<OrderFail> fails )
        {
            AddGuiAction( ( ) => OrdersRegisterFailed?.Invoke( fails ) );
        }

        public event Action<IEnumerable<OrderFail>> OrdersCancelFailed;

        private void OrdersCancelFailedHandler( IEnumerable<OrderFail> fails )
        {
            AddGuiAction( ( ) => OrdersCancelFailed?.Invoke( fails ) );
        }

        public event Action<long> MassOrderCanceled;

        private void MassOrderCanceledHandler( long something )
        {
            AddGuiAction( ( ) => MassOrderCanceled?.Invoke( something ) );
        }

        public event Action<long, Exception> MassOrderCancelFailed;

        private void MassOrderCancelFailedHandler( long something, Exception ex )
        {
            AddGuiAction( ( ) => MassOrderCancelFailed?.Invoke( something, ex ) );            
        }

        public event Action<long, Exception> OrderStatusFailed;

        private void OrderStatusFailedHandler( long something, Exception ex )
        {
            AddGuiAction( ( ) => OrderStatusFailed?.Invoke( something, ex ) );            
        }

        public event Action<IEnumerable<Order>> NewStopOrders;

        private void NewStopOrdersHandler( IEnumerable<Order> orders )
        {
            AddGuiAction( ( ) => NewStopOrders?.Invoke( orders ) );
        }

        public event Action<IEnumerable<Order>> StopOrdersChanged;

        private void StopOrdersChangedHandler( IEnumerable<Order> orders )
        {
            AddGuiAction( ( ) => StopOrdersChanged?.Invoke( orders ) );
        }

        public event Action<IEnumerable<OrderFail>> StopOrdersRegisterFailed;

        private void StopOrdersRegisterFailedHandler( IEnumerable<OrderFail> fails )
        {
            AddGuiAction( ( ) => StopOrdersRegisterFailed?.Invoke( fails ) );
        }

        public event Action<IEnumerable<OrderFail>> StopOrdersCancelFailed;

        private void StopOrdersCancelFailedHandler( IEnumerable<OrderFail> fails )
        {
            AddGuiAction( ( ) => StopOrdersCancelFailed?.Invoke( fails ) );
        }

        public event Action<IEnumerable<MarketDepth>> NewMarketDepths;

        private void NewMarketDepthsHandler( IEnumerable<MarketDepth> marketDepths )
        {
            AddGuiAction( ( ) => NewMarketDepths?.Invoke( marketDepths ) );
        }

        public event Action<IEnumerable<MarketDepth>> MarketDepthsChanged;

        private void MarketDepthsChangedHandler( IEnumerable<MarketDepth> marketDepths )
        {
            AddGuiAction( ( ) => MarketDepthsChanged?.Invoke( marketDepths ) );
        }

        public event Action<IEnumerable<OrderLogItem>> NewOrderLogItems;

        private void NewOrderLogItemsHandler( IEnumerable<OrderLogItem> items )
        {
            AddGuiAction( ( ) => NewOrderLogItems?.Invoke( items ) );
        }

        public event Action<StockSharp.BusinessEntities.News> NewNews;

        private void NewNewsHandler( News news )
        {
            AddGuiAction( ( ) => NewNews?.Invoke( news ) );
        }

        public event Action<StockSharp.BusinessEntities.News> NewsChanged;

        private void NewsChangedHandler( News news )
        {
            AddGuiAction( ( ) => NewsChanged?.Invoke( news ) );
        }

        public event Action<Message> NewMessage;

        private void NewMessageHandler( Message message )
        {
            AddGuiAction( ( ) => NewMessage?.Invoke( message ) );
        }

        public event Action Connected;

        private void ConnectedHandler( )
        {
            AddGuiAction( ( ) => Connected?.Invoke() );
        }

        public event Action Disconnected;

        private void DisconnectedHandler( )
        {
            AddGuiAction( ( ) => Disconnected?.Invoke() );
        }

        public event Action<Exception> ConnectionError;

        private void ConnectionErrorHandler( Exception exception )
        {
            AddGuiAction( ( ) => ConnectionError?.Invoke( exception ) );
        }

        public event Action<IMessageAdapter> ConnectedEx;

        private void ConnectedExHandler( IMessageAdapter adapter )
        {
            AddGuiAction( ( ) => ConnectedEx?.Invoke( adapter ) );
        }

        public event Action<IMessageAdapter> DisconnectedEx;

        private void DisconnectedExHandler( IMessageAdapter adapter )
        {
            AddGuiAction( ( ) => DisconnectedEx?.Invoke( adapter ) );
        }

        public event Action<IMessageAdapter, Exception> ConnectionErrorEx;

        private void ConnectionErrorExHandler( IMessageAdapter adapter, Exception ex )
        {
            AddGuiAction( ( ) => ConnectionErrorEx?.Invoke( adapter, ex ) );            
        }

        public event Action<Exception> Error;

        private void ErrorHandler( Exception exception )
        {
            AddGuiAction( ( ) => Error?.Invoke( exception ) );
        }

        public event Action<TimeSpan> MarketTimeChanged;

        private void MarketTimeChangedHandler( TimeSpan diff )
        {
            AddGuiAction( ( ) => MarketTimeChanged?.Invoke( diff ) );
        }

        

        

        public event Action<Security, MarketDataMessage> MarketDataSubscriptionSucceeded;

        private void MarketDataSubscriptionSucceededHandler( Security security, MarketDataMessage marketData )
        {
            AddGuiAction( ( ) => MarketDataSubscriptionSucceeded?.Invoke( security, marketData ) );
        }

        public event Action<Security, MarketDataMessage, Exception> MarketDataSubscriptionFailed;

        private void MarketDataSubscriptionFailedHandler( Security security, MarketDataMessage marketData, Exception error )
        {
            AddGuiAction( ( ) => MarketDataSubscriptionFailed?.Invoke( security, marketData, error ) );
        }

        public event Action<Security, MarketDataMessage> MarketDataUnSubscriptionSucceeded;

        private void MarketDataUnSubscriptionSucceededHandler( Security security, MarketDataMessage marketData )
        {
            AddGuiAction( ( ) => MarketDataUnSubscriptionSucceeded?.Invoke( security, marketData ) );
        }

        public event Action<Security, MarketDataMessage, Exception> MarketDataUnSubscriptionFailed;

        private void MarketDataUnSubscriptionFailedHandler( Security security, MarketDataMessage marketData, Exception error )
        {
            AddGuiAction( ( ) => MarketDataUnSubscriptionFailed?.Invoke( security, marketData, error ) );
        }

        public event Action<ExchangeBoard, SessionStates> SessionStateChanged;

        private void SessionStateChangedHandler( ExchangeBoard board, SessionStates state )
        {
            AddGuiAction( ( ) => SessionStateChanged?.Invoke( board, state ) );
        }

        public event Action<Portfolio> NewPortfolio;

        private void NewPortfolioHandler( Portfolio portfolio )
        {
            AddGuiAction( ( ) => NewPortfolio?.Invoke( portfolio ) );
        }

        public event Action<Portfolio> PortfolioChanged;

        private void PortfolioChangedHandler( Portfolio portfolio )
        {
            AddGuiAction( ( ) => PortfolioChanged?.Invoke( portfolio ) );
        }

        public event Action<Position> NewPosition;

        private void NewPositionHandler( Position position )
        {
            AddGuiAction( ( ) => NewPosition?.Invoke( position ) );
        }

        public event Action<Position> PositionChanged;

        private void PositionChangedHandler( Position position )
        {
            AddGuiAction( ( ) => PositionChanged?.Invoke( position ) );
        }

        public event Action<Security> NewSecurity;

        private void NewSecurityHandler( Security security )
        {
            AddGuiAction( ( ) => NewSecurity?.Invoke( security ) );
        }

        public event Action<Security> SecurityChanged;

        private void SecurityChangedHandler( Security security )
        {
            AddGuiAction( ( ) => SecurityChanged?.Invoke( security ) );
        }

        public event Action<Trade> NewTrade;

        private void NewTradeHandler( Trade trade )
        {
            AddGuiAction( ( ) => NewTrade?.Invoke( trade ) );
        }

        public event Action<MyTrade> NewMyTrade;

        private void NewMyTradeHandler( MyTrade trade )
        {
            AddGuiAction( ( ) => NewMyTrade?.Invoke( trade ) );
        }

        public event Action<Order> NewOrder;

        private void NewOrderHandler( Order order )
        {
            AddGuiAction( ( ) => NewOrder?.Invoke( order ) );
        }

        public event Action<Order> OrderChanged;

        private void OrderChangedHandler( Order order )
        {
            AddGuiAction( ( ) => OrderChanged?.Invoke( order ) );
        }

        public event Action<OrderFail> OrderRegisterFailed;

        private void OrderRegisterFailedHandler( OrderFail fail )
        {
            AddGuiAction( ( ) => OrderRegisterFailed?.Invoke( fail ) );
        }
        public event Action<OrderFail> OrderCancelFailed;

        private void OrderCancelFailedHandler( OrderFail fail )
        {
            AddGuiAction( ( ) => OrderCancelFailed?.Invoke( fail ) );
        }

        public event Action<OrderFail> StopOrderRegisterFailed;

        private void StopOrderRegisterFailedHandler( OrderFail fail )
        {
            AddGuiAction( ( ) => StopOrderRegisterFailed?.Invoke( fail ) );
        }

        public event Action<OrderFail> StopOrderCancelFailed;

        private void StopOrderCancelFailedHandler( OrderFail fail )
        {
            AddGuiAction( ( ) => StopOrderCancelFailed?.Invoke( fail ) );
        }

        public event Action<Order> NewStopOrder;

        private void NewStopOrderHandler( Order order )
        {
            AddGuiAction( ( ) => NewStopOrder?.Invoke( order ) );            
        }

        public event Action<Order> StopOrderChanged;

        private void StopOrderChangedHandler( Order order )
        {
            AddGuiAction( ( ) => StopOrderChanged?.Invoke( order ) );            
        }

        public event Action<MarketDepth> NewMarketDepth;

        private void NewMarketDepthHandler( MarketDepth marketDepth )
        {
            AddGuiAction( ( ) => NewMarketDepth?.Invoke( marketDepth ) );
        }

        public event Action<MarketDepth> MarketDepthChanged;

        private void MarketDepthChangedHandler( MarketDepth marketDepth )
        {
            AddGuiAction( ( ) => MarketDepthChanged?.Invoke( marketDepth ) );
        }

        public event Action<OrderLogItem> NewOrderLogItem;

        private void NewOrderLogItemHandler( OrderLogItem item )
        {
            AddGuiAction( ( ) => NewOrderLogItem?.Invoke( item ) );
        }

        private static void AddGuiAction( Action action )
        {
            GuiDispatcher.GlobalDispatcher.AddAction( action );
        }

        public IdGenerator TransactionIdGenerator
        {
            get
            {
                return Connector.TransactionIdGenerator;
            }
        }

        public SessionStates? GetSessionState( ExchangeBoard board )
        {
            return Connector.GetSessionState( board );
        }

        public IEnumerable<ExchangeBoard> ExchangeBoards
        {
            get
            {
                return Connector.ExchangeBoards;
            }
        }

        public IEnumerable<Security> Securities
        {
            get
            {
                return Connector.Securities;
            }
        }

        public IEnumerable<Order> Orders
        {
            get
            {
                return Connector.Orders;
            }
        }

        public IEnumerable<Order> StopOrders
        {
            get
            {
                return Connector.StopOrders;
            }
        }

        public IEnumerable<OrderFail> OrderRegisterFails
        {
            get
            {
                return Connector.OrderRegisterFails;
            }
        }

        public IEnumerable<OrderFail> OrderCancelFails
        {
            get
            {
                return Connector.OrderCancelFails;
            }
        }

        public IEnumerable<Trade> Trades
        {
            get
            {
                return Connector.Trades;
            }
        }

        public IEnumerable<MyTrade> MyTrades
        {
            get
            {
                return Connector.MyTrades;
            }
        }

        public IEnumerable<Portfolio> Portfolios
        {
            get
            {
                return Connector.Portfolios;
            }
        }

        public IEnumerable<Position> Positions
        {
            get
            {
                return Connector.Positions;
            }
        }

        public IEnumerable<StockSharp.BusinessEntities.News> News
        {
            get
            {
                return Connector.News;
            }
        }

        public ConnectionStates ConnectionState
        {
            get
            {
                return Connector.ConnectionState;
            }
        }

        public IEnumerable<Security> RegisteredSecurities
        {
            get
            {
                return Connector.RegisteredSecurities;
            }
        }

        public IEnumerable<Security> RegisteredMarketDepths
        {
            get
            {
                return Connector.RegisteredMarketDepths;
            }
        }

        public IEnumerable<Security> RegisteredTrades
        {
            get
            {
                return Connector.RegisteredTrades;
            }
        }

        public IEnumerable<Security> RegisteredOrderLogs
        {
            get
            {
                return Connector.RegisteredOrderLogs;
            }
        }

        public IEnumerable<Portfolio> RegisteredPortfolios
        {
            get
            {
                return Connector.RegisteredPortfolios;
            }
        }

        public IMessageAdapter TransactionAdapter
        {
            get
            {
                return Connector.TransactionAdapter;
            }
        }

        public IMessageAdapter MarketDataAdapter
        {
            get
            {
                return Connector.MarketDataAdapter;
            }
        }

        public int Count => throw new NotImplementedException();

        public void Connect( )
        {
            Connector.Connect();
        }

        public void Disconnect( )
        {
            Connector.Disconnect();
        }

        

        public SecurityId GetSecurityId( Security security )
        {
            return Connector.GetSecurityId( security );
        }

        

        public void LookupOrders( Order criteria, IMessageAdapter adapter = null )
        {
            Connector.LookupOrders( criteria, adapter );
        }

        public void LookupOrders( OrderStatusMessage criteria )
        {
            Connector.LookupOrders( criteria );
        }

        public Security LookupSecurity( SecurityId securityId )
        {
            return Connector.LookupSecurity( securityId );
        }

        public Portfolio GetPortfolio( string name )
        {
            return Connector.GetPortfolio( name );
        }

        public Position GetPosition( Portfolio portfolio, Security security, string clientCode = "", string depoName = "" )
        {
            return Connector.GetPosition( portfolio, security, clientCode, depoName );
        }

        public MarketDepth GetMarketDepth( Security security )
        {
            return Connector.GetMarketDepth( security );
        }

        public MarketDepth GetFilteredMarketDepth( Security security )
        {
            return Connector.GetFilteredMarketDepth( security );
        }

        public void SubscribeMarketData( Security security, MarketDataMessage message )
        {
            Connector.SubscribeMarketData( security, message );
        }

        public void UnSubscribeMarketData( Security security, MarketDataMessage message )
        {
            Connector.UnSubscribeMarketData( security, message );
        }

        

        public void UnRegisterMarketDepth( Security security )
        {
            Connector.UnRegisterMarketDepth( security );
        }

        public void RegisterFilteredMarketDepth( Security security )
        {
            Connector.RegisterFilteredMarketDepth( security );
        }

        public void UnRegisterFilteredMarketDepth( Security security )
        {
            Connector.UnRegisterFilteredMarketDepth( security );
        }

        

        public void UnRegisterTrades( Security security )
        {
            Connector.UnRegisterTrades( security );
        }

        public void RegisterPortfolio( Portfolio portfolio )
        {
            Connector.RegisterPortfolio( portfolio );
        }

        public void UnRegisterPortfolio( Portfolio portfolio )
        {
            Connector.UnRegisterPortfolio( portfolio );
        }

        public void RegisterNews( )
        {
            Connector.RegisterNews();
        }

        public void UnRegisterNews( )
        {
            Connector.UnRegisterNews();
        }

        public void SendOutMessage( Message message )
        {
            Connector.SendOutMessage( message );
        }

        public void SendInMessage( Message message )
        {
            Connector.SendInMessage( message );
        }

        public void RequestNewsStory( StockSharp.BusinessEntities.News news )
        {
            Connector.RequestNewsStory( news );
        }

        public void RegisterOrderLog( Security security, DateTimeOffset? from = null, DateTimeOffset? to = null, long? count = null )
        {
            Connector.RegisterOrderLog( security, from, to, count );
        }

        public void UnRegisterOrderLog( Security security )
        {
            Connector.UnRegisterOrderLog( security );
        }

        

        public void UnRegisterSecurity( Security security )
        {
            Connector.UnRegisterSecurity( security );
        }

        public void RegisterOrder( Order order )
        {
            Connector.RegisterOrder( order );
        }

        public void ReRegisterOrder( Order oldOrder, Order newOrder )
        {
            Connector.ReRegisterOrder( oldOrder, newOrder );
        }

        public Order ReRegisterOrder( Order oldOrder, Decimal price, Decimal volume )
        {
            return Connector.ReRegisterOrder( oldOrder, price, volume );
        }

        public void CancelOrder( Order order )
        {
            Connector.CancelOrder( order );
        }

        public void CancelOrders( bool? isStopOrder = null, Portfolio portfolio = null, Sides? direction = null, ExchangeBoard board = null, Security security = null, SecurityTypes? securityType = null, long? transactionId = null )
        {
            Connector.CancelOrders( isStopOrder, portfolio, direction, board, security, securityType, transactionId );
        }

        public event Action<Security, IEnumerable<KeyValuePair<Level1Fields, object>>, DateTimeOffset, DateTimeOffset> ValuesChanged;
        
        public event Action<PortfolioLookupMessage, IEnumerable<Portfolio>, IEnumerable<Portfolio>, Exception> LookupPortfoliosResult2;
        public event Action<BoardLookupMessage, IEnumerable<ExchangeBoard>, IEnumerable<ExchangeBoard>, Exception> LookupBoardsResult2;
        public event Action<Security, MarketDataMessage, Exception> MarketDataUnexpectedCancelled;

        private void ValuesChangedHandler( Security security, IEnumerable<KeyValuePair<Level1Fields, object>> changes, DateTimeOffset serverTime, DateTimeOffset localTime )
        {
            AddGuiAction( ( ) => ValuesChanged?.Invoke( security, changes, serverTime, localTime ) );
        }

        public object GetSecurityValue( Security security, Level1Fields field )
        {
            return Connector.GetSecurityValue( security, field );
        }

        public IEnumerable<Level1Fields> GetLevel1Fields( Security security )
        {
            return Connector.GetLevel1Fields( security );
        }

        int ISecurityProvider.Count => Connector.Count;

        event Action<IEnumerable<Security>> ISecurityProvider.Added
        {
            add { Connector.Added += value; }
            remove { Connector.Added -= value; }
        }

        event Action<IEnumerable<Security>> ISecurityProvider.Removed
        {
            add { Connector.Removed += value; }
            remove { Connector.Removed -= value; }
        }

        event Action ISecurityProvider.Cleared
        {
            add { Connector.Cleared += value; }
            remove { Connector.Cleared -= value; }
        }

        event Action<BoardLookupMessage, IEnumerable<ExchangeBoard>, Exception> IConnector.LookupBoardsResult
        {
            add
            {
                Connector.LookupBoardsResult += value;
            }

            remove
            {                
                Connector.LookupBoardsResult -= value;
            }
        }

        public IEnumerable<Security> Lookup( Security criteria )
        {
            return Connector.Lookup( criteria );
        }

        protected override void DisposeManaged( )
        {
            Connector.NewPortfolios                     -= NewPortfoliosHandler;
            Connector.PortfoliosChanged                 -= PortfoliosChangedHandler;
            Connector.NewPositions                      -= NewPositionsHandler;
            Connector.PositionsChanged                  -= PositionsChangedHandler;
            Connector.NewSecurities                     -= NewSecuritiesHandler;
            Connector.SecuritiesChanged                 -= SecuritiesChangedHandler;
            Connector.NewTrades                         -= NewTradesHandler;
            Connector.NewMyTrades                       -= NewMyTradesHandler;
            Connector.NewOrders                         -= NewOrdersHandler;
            Connector.OrdersChanged                     -= OrdersChangedHandler;
            Connector.OrdersRegisterFailed              -= OrdersRegisterFailedHandler;
            Connector.OrdersCancelFailed                -= OrdersCancelFailedHandler;
            Connector.NewStopOrders                     -= NewStopOrdersHandler;
            Connector.StopOrdersChanged                 -= StopOrdersChangedHandler;
            Connector.StopOrdersRegisterFailed          -= StopOrdersRegisterFailedHandler;
            Connector.StopOrdersCancelFailed            -= StopOrdersCancelFailedHandler;
            Connector.NewMarketDepths                   -= NewMarketDepthsHandler;
            Connector.MarketDepthsChanged               -= MarketDepthsChangedHandler;
            Connector.NewOrderLogItems                  -= NewOrderLogItemsHandler;
            Connector.NewNews                           -= NewNewsHandler;
            Connector.NewsChanged                       -= NewsChangedHandler;
            Connector.NewMessage                        -= NewMessageHandler;
            Connector.Connected                         -= ConnectedHandler;
            Connector.Disconnected                      -= DisconnectedHandler;
            Connector.ConnectionError                   -= ConnectionErrorHandler;
            Connector.ConnectedEx                       -= ConnectedExHandler;
            Connector.DisconnectedEx                    -= DisconnectedExHandler;
            Connector.ConnectionErrorEx                 -= ConnectionErrorExHandler;
            Connector.Error                             -= ErrorHandler;
            Connector.MarketTimeChanged                 -= MarketTimeChangedHandler;
            Connector.LookupSecuritiesResult            -= Connector_LookupSecuritiesResult;
            Connector.LookupSecuritiesResult2           -= Connector_LookupSecuritiesResult2;
            Connector.LookupPortfoliosResult            -= Connector_LookupPortfoliosResult;
            Connector.LookupPortfoliosResult2           -= Connector_LookupPortfoliosResult2;

            Connector.MarketDataSubscriptionSucceeded   -= MarketDataSubscriptionSucceededHandler;
            Connector.MarketDataSubscriptionFailed      -= MarketDataSubscriptionFailedHandler;
            Connector.MarketDataUnSubscriptionSucceeded -= MarketDataUnSubscriptionSucceededHandler;
            Connector.MarketDataUnSubscriptionFailed    -= MarketDataUnSubscriptionFailedHandler;
            Connector.MarketDataSubscriptionFinished    -= MarketDataSubscriptionFinishedHandler;
            Connector.MarketDataUnexpectedCancelled -= Connector_MarketDataUnexpectedCancelled;
            Connector.SessionStateChanged               -= SessionStateChangedHandler;
            Connector.ValuesChanged                     -= ValuesChangedHandler;
            Connector.NewPortfolio                      -= NewPortfolioHandler;
            Connector.PortfolioChanged                  -= PortfolioChangedHandler;
            Connector.NewPosition                       -= NewPositionHandler;
            Connector.PositionChanged                   -= PositionChangedHandler;
            Connector.NewSecurity                       -= NewSecurityHandler;
            Connector.SecurityChanged                   -= SecurityChangedHandler;
            Connector.NewTrade                          -= NewTradeHandler;
            Connector.NewMyTrade                        -= NewMyTradeHandler;
            Connector.NewOrder                          -= NewOrderHandler;
            Connector.OrderChanged                      -= OrderChangedHandler;
            Connector.OrderRegisterFailed               -= OrderRegisterFailedHandler;
            Connector.OrderCancelFailed                 -= OrderCancelFailedHandler;
            Connector.NewStopOrder                      -= NewStopOrderHandler;
            Connector.StopOrderChanged                  -= StopOrderChangedHandler;
            Connector.StopOrderRegisterFailed           -= StopOrderRegisterFailedHandler;
            Connector.StopOrderCancelFailed             -= StopOrderCancelFailedHandler;
            Connector.MassOrderCanceled                 -= MassOrderCanceledHandler;
            Connector.MassOrderCancelFailed             -= MassOrderCancelFailedHandler;
            Connector.OrderStatusFailed                 -= OrderStatusFailedHandler;
            Connector.NewMarketDepth                    -= NewMarketDepthHandler;
            Connector.MarketDepthChanged                -= MarketDepthChangedHandler;
            Connector.NewOrderLogItem                   -= NewOrderLogItemHandler;
            Connector.LookupBoardsResult                -= Connector_LookupBoardsResult1;
            Connector.LookupBoardsResult2               -= Connector_LookupBoardsResult2;

            DisposeManaged();
        }

        
        public void LookupSecurities( Security criteria, IMessageAdapter adapter = null, MessageOfflineModes offlineMode = MessageOfflineModes.None )
        {
            Connector.LookupSecurities( criteria, adapter, offlineMode );
        }

        public void LookupSecurities( Security criteria, IMessageAdapter adapter = null )
        {
            Connector.LookupSecurities( criteria, adapter );
        }

        public void LookupSecurities( SecurityLookupMessage criteria )
        {
            Connector.LookupSecurities( criteria );
        }

        public void LookupPortfolios( Portfolio criteria, IMessageAdapter adapter = null, MessageOfflineModes offlineMode = 0 )
        {
            this.Connector.LookupPortfolios( criteria, adapter, offlineMode );
        }

        public void LookupPortfolios( PortfolioLookupMessage criteria )
        {
            this.Connector.LookupPortfolios( criteria );
        }

        public void RegisterMarketDepth( Security security, DateTimeOffset? from = null, DateTimeOffset? to = null, long? count = null, MarketDataBuildModes buildMode = 0, MarketDataTypes? buildFrom = null, int? maxDepth = null )
        {
            this.Connector.RegisterMarketDepth( security, from, to, count, buildMode, buildFrom, maxDepth );
        }

        public void RegisterTrades( Security security, DateTimeOffset? from = null, DateTimeOffset? to = null, long? count = null, MarketDataBuildModes buildMode = 0, MarketDataTypes? buildFrom = null )
        {
            this.Connector.RegisterTrades( security, from, to, count, buildMode, buildFrom );
        }

        public void RegisterSecurity( Security security, DateTimeOffset? from = null, DateTimeOffset? to = null, long? count = null, MarketDataBuildModes buildMode = 0, MarketDataTypes? buildFrom = null )
        {
            this.Connector.RegisterSecurity( security, from, to, count, buildMode, buildFrom );
        }

        public void LookupBoards( ExchangeBoard criteria, IMessageAdapter adapter = null, MessageOfflineModes offlineMode = MessageOfflineModes.None )
        {
            this.Connector.LookupBoards( criteria, adapter, offlineMode );
        }

        public void LookupBoards( BoardLookupMessage criteria )
        {
            this.Connector.LookupBoards( criteria );
        }

        public void SubscribeBoard( ExchangeBoard board )
        {
            this.Connector.SubscribeBoard( board );
        }

        public void UnSubscribeBoard( ExchangeBoard board )
        {
            this.Connector.UnSubscribeBoard( board );
        }

    }
}
