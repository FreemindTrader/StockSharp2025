using Ecng.Collections;
using Ecng.Common;
using MoreLinq;
using StockSharp.Algo.Positions;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace StockSharp.Algo.Strategies.Protective
{
    /// <summary>The strategy of the automatic position protection.</summary>
    /// <remarks>
    /// New trades come in strategy via <see cref="M:StockSharp.Algo.Strategies.Protective.AutoProtectiveStrategy.ProcessNewMyTrade(StockSharp.BusinessEntities.MyTrade)" />. They are automatically protected by <see cref="T:StockSharp.Algo.Strategies.Protective.TakeProfitStopLossStrategy" />. Also, <see cref="T:StockSharp.Algo.Strategies.Protective.AutoProtectiveStrategy" /> turns over stops in case of position flipping.
    /// </remarks>
    public class AutoProtectiveStrategy : Strategy
    {

        private readonly SynchronizedDictionary<Security, ApPositionManager> _securityPositionManager = new SynchronizedDictionary<Security, ApPositionManager>();

        private readonly StrategyParam<Unit> _takeProfitLevel;

        private readonly StrategyParam<Unit> _stopLossLevel;

        private readonly StrategyParam<bool> _isTrailingStopLoss;

        private readonly StrategyParam<bool> _isTrailingTakeProfit;

        private readonly StrategyParam<TimeSpan> _stopLossTimeOut;

        private readonly StrategyParam<TimeSpan> _takeProfitTimeOut;

        private readonly StrategyParam<bool> _useMarketOrders;

        private IMarketRule? _marketRule;

        private Strategy? _myStrategy;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Algo.Strategies.Protective.AutoProtectiveStrategy" />.
        /// </summary>
        public AutoProtectiveStrategy()
        {
            _takeProfitLevel = this.Param( nameof( TakeProfitLevel ), new Unit() );
            _stopLossLevel = this.Param( nameof( StopLossLevel ), new Unit() );
            _isTrailingStopLoss = this.Param( nameof( IsTrailingStopLoss ), false );
            _isTrailingTakeProfit = this.Param( nameof( IsTrailingTakeProfit ), false );
            _takeProfitTimeOut = this.Param( nameof( TakeProfitTimeOut ), new TimeSpan() );
            _stopLossTimeOut = this.Param( nameof( StopLossTimeOut ), new TimeSpan() );
            _useMarketOrders = this.Param( nameof( UseMarketOrders ), false );
        }

        /// <summary>
        /// The protective level for the take profit. The default level is 0, which means the disabled.
        /// </summary>
        public Unit TakeProfitLevel
        {
            get
            {
                return _takeProfitLevel.Value;
            }
            set
            {
                _takeProfitLevel.Value = value;
            }
        }

        /// <summary>
        /// The protective level for the stop loss. The default level is 0, which means the disabled.
        /// </summary>
        public Unit StopLossLevel
        {
            get
            {
                return _stopLossLevel.Value;
            }
            set
            {
                _stopLossLevel.Value = value;
            }
        }

        /// <summary>
        /// Whether to use a trailing technique for <see cref="T:StockSharp.Algo.Strategies.Protective.StopLossStrategy" />. The default is off.
        /// </summary>
        public bool IsTrailingStopLoss
        {
            get
            {
                return _isTrailingStopLoss.Value;
            }
            set
            {
                if ( value && StopLossLevel.Type == UnitTypes.Limit )
                    throw new ArgumentException( LocalizedStrings.Str1225 );
                _isTrailingStopLoss.Value = value;
            }
        }

        /// <summary>
        /// Whether to use a trailing technique for <see cref="T:StockSharp.Algo.Strategies.Protective.TakeProfitStrategy" />. The default is off.
        /// </summary>
        public bool IsTrailingTakeProfit
        {
            get
            {
                return _isTrailingTakeProfit.Value;
            }
            set
            {
                if ( value && TakeProfitLevel.Type == UnitTypes.Limit )
                    throw new ArgumentException( LocalizedStrings.Str1226 );
                _isTrailingTakeProfit.Value = value;
            }
        }

        /// <summary>
        /// Time limit for <see cref="T:StockSharp.Algo.Strategies.Protective.StopLossStrategy" />. If protection has not worked by this time, the position will be closed on the market. The default is off.
        /// </summary>
        public TimeSpan StopLossTimeOut
        {
            get
            {
                return _stopLossTimeOut.Value;
            }
            set
            {
                if ( value < TimeSpan.Zero )
                    throw new ArgumentOutOfRangeException( nameof( value ), value, LocalizedStrings.Str1227 );
                _stopLossTimeOut.Value = value;
            }
        }

        /// <summary>
        /// Time limit for <see cref="T:StockSharp.Algo.Strategies.Protective.TakeProfitStrategy" />. If protection has not worked by this time, the position will be closed on the market. The default is off.
        /// </summary>
        public TimeSpan TakeProfitTimeOut
        {
            get
            {
                return _takeProfitTimeOut.Value;
            }
            set
            {
                if ( value < TimeSpan.Zero )
                    throw new ArgumentOutOfRangeException( nameof( value ), value, LocalizedStrings.Str1227 );
                _takeProfitTimeOut.Value = value;
            }
        }

        /// <summary>Whether to use market orders.</summary>
        public bool UseMarketOrders
        {
            get
            {
                return _useMarketOrders.Value;
            }
            set
            {
                _useMarketOrders.Value = value;
            }
        }

        /// <summary>
        /// To get or set the initial position for the instrument.
        /// </summary>
        /// <param name="security">Security.</param>
        /// <returns>Position.</returns>
        public Decimal this[Security security]
        {
            get
            {
                return GetPositionManager( security ).GetPosition();
            }
            set
            {
                Decimal secValue = this[security];
                if ( secValue == value )
                    return;

                if ( secValue.Sign() != value.Sign() )
                {
                    StopChildStrategies( security );
                    if ( value != Decimal.Zero )
                        ChangePositionTo( value );
                }
                else
                {
                    Decimal diff = value.Abs() - secValue.Abs();
                    if ( diff == Decimal.Zero )
                        return;
                    if ( diff > Decimal.Zero )
                        ChangePositionTo( diff * value.Sign() );
                    else
                        DecreasePositionBy( security, diff.Abs() );
                }

                GetPositionManager( security ).SetPosition( value );
            }
        }

        /// <summary>
        /// The strategy which new trades are automatically passed to <see cref="M:StockSharp.Algo.Strategies.Protective.AutoProtectiveStrategy.ProcessNewMyTrade(StockSharp.BusinessEntities.MyTrade)" />.
        /// </summary>
        public Strategy? MyTradesStrategy
        {
            get
            {
                return _myStrategy;
            }
            set
            {
                if ( value == null )
                    throw new ArgumentNullException( nameof( value ) );

                if ( _myStrategy == value )
                    return;

                if ( _marketRule != null )
                    Rules.Remove( _marketRule );

                _myStrategy = value;

                if ( ProcessState != ProcessStates.Started )
                    return;

                AddNewMyTradeRule();
            }
        }

        private void AddNewMyTradeRule()
        {
            _marketRule = MyTradesStrategy.WhenNewMyTrade().Do( OnNewTradeCorrectProtectiveVolume ).Apply( this );
        }

        /// <inheritdoc />
        protected override void OnStarted()
        {
            base.OnStarted();
            if ( MyTradesStrategy == null )
                return;
            AddNewMyTradeRule();
        }

        /// <summary>
        /// To protect the position that has been updated via <see cref="P:StockSharp.Algo.Strategies.Protective.AutoProtectiveStrategy.Item(StockSharp.BusinessEntities.Security)" />.
        /// </summary>
        /// <param name="position">Position.</param>
        /// <returns>The protective strategy. If <see langword="null" /> will be returned then the position protection is ignored.</returns>
        protected virtual IProtectiveStrategy? Protect( Decimal position )
        {
            return null;
        }

        /// <summary>Protect position.</summary>
        /// <param name="trade">The protected trade.</param>
        /// <param name="volume">Volume to be protected is specified by the value.</param>
        /// <returns>Protective strategy.</returns>
        protected virtual Strategy? Protect( MyTrade trade, Decimal volume )
        {
            TakeProfitStrategy? takeProfit;

            if ( TakeProfitLevel != 0 )
            {
                var profit = new TakeProfitStrategy( trade, TakeProfitLevel );
                profit.UseMarketOrders = UseMarketOrders;
                profit.IsTrailing = IsTrailingTakeProfit;
                profit.TimeOut = TakeProfitTimeOut;
                profit.WaitAllTrades = WaitAllTrades;
                takeProfit = profit;
            }
            else
            {
                takeProfit = null;
            }


            StopLossStrategy? stopLoss;

            if ( StopLossLevel != 0 )
            {
                var stop = new StopLossStrategy( trade, StopLossLevel );
                stop.UseMarketOrders = UseMarketOrders;
                stop.IsTrailing = IsTrailingStopLoss;
                stop.TimeOut = StopLossTimeOut;
                stop.WaitAllTrades = WaitAllTrades;
                stopLoss = stop;
            }
            else
            {
                stopLoss = null;
            }

            Strategy? finalStrategy;
            if ( takeProfit != null && stopLoss != null )
            {
                finalStrategy = new TakeProfitStopLossStrategy( takeProfit, stopLoss );
                finalStrategy.WaitAllTrades = WaitAllTrades;
            }
            else
            {
                if ( takeProfit == null )
                {
                    finalStrategy = stopLoss;
                }
                else
                {
                    finalStrategy = takeProfit;
                }

            }

            if ( finalStrategy != null )
            {
                finalStrategy.DisposeOnStop = true;
                ( ( IProtectiveStrategy )finalStrategy ).ProtectiveVolume = volume;
            }
            return finalStrategy;
        }

        /// <summary>
        /// To sort protective strategies to define the worst and the best ones by market prices (when position is partially closed the worst ones are cancelled firstly).
        /// </summary>
        /// <param name="strategies">Protective strategies in unsorted order.</param>
        /// <returns>Protective strategies in sorted order.</returns>
        protected virtual IEnumerable<IGrouping<Tuple<Sides, Decimal>, IProtectiveStrategy>> Sort(
          IEnumerable<IGrouping<Tuple<Sides, Decimal>, IProtectiveStrategy>> strategies )
        {
            var mySides = strategies.First().Key.Item1;

            return strategies.OrderBy( g => g.Key.Item2 * ( mySides == Sides.Buy ? 1 : -1 ) );
        }

        /// <summary>
        /// To process trade to correct the protective strategies volume.
        /// </summary>
        /// <param name="trade">Trade.</param>
        public void OnNewTradeCorrectProtectiveVolume( MyTrade trade )
        {
            var security = trade.Trade.Security;
            var posMgr = GetPositionManager( security );
            var myPoistion = posMgr.GetPosition();
            posMgr.ProcessMessage( trade.ToMessage() );

            this.AddInfoLog( LocalizedStrings.Str1230Params, security.Id, myPoistion, posMgr.GetPosition() );

            if ( myPoistion == Decimal.Zero )
            {
                SetupProtectiveStrategy( posMgr, trade, trade.Trade.Volume );
            }
            else if ( myPoistion.Sign() != posMgr.GetPosition().Sign() )
            {
                StopChildStrategies( security );

                if ( !( posMgr.GetPosition() != Decimal.Zero ) )
                    return;

                this.AddInfoLog( LocalizedStrings.Str1231 );

                SetupProtectiveStrategy( posMgr, trade, posMgr.GetPosition().Abs() );
            }
            else
            {
                Decimal posDiff = posMgr.GetPosition().Abs() - myPoistion.Abs();
                if ( posDiff == Decimal.Zero )
                    return;

                if ( posDiff > Decimal.Zero )
                {
                    this.AddInfoLog( LocalizedStrings.Str1232Params, posDiff );
                    SetupProtectiveStrategy( posMgr, trade, posDiff );
                }
                else
                {
                    DecreasePositionBy( security, posDiff.Abs() );
                }

            }
        }

        private void StopChildStrategies( Security security )
        {
            foreach ( IEnumerable source in ChildStrategies.Where( s => s.Security == security ).OfType<IProtectiveStrategy>().GroupBy( s => Tuple.Create( s.ProtectiveSide, s.ProtectivePrice ) ).ToArray() )
            {
                MoreEnumerable.ForEach( source.OfType<Strategy>(), s => s.Stop() );
            }
        }

        private void ChangePositionTo( Decimal newPositionValue )
        {
            IProtectiveStrategy? protection = Protect( newPositionValue );

            if ( protection == null )
                return;

            this.AddInfoLog( LocalizedStrings.Str1233Params, newPositionValue );

            ChildStrategies.Add( ( Strategy )protection );
        }

        private void DecreasePositionBy( Security security, Decimal decreaseBy )
        {
            this.AddInfoLog( LocalizedStrings.Str1234Params, decreaseBy );

            var array = ChildStrategies.Where( s => s.Security == security ).OfType<IProtectiveStrategy>().GroupBy( s => Tuple.Create( s.ProtectiveSide, s.ProtectivePrice ) ).ToArray<IGrouping<Tuple<Sides, Decimal>, IProtectiveStrategy>>();

            if ( array.Length == 0 )
            {
                this.AddWarningLog( LocalizedStrings.Str1235 );
            }
            else
            {
                foreach ( var source in Sort( array ) )
                {
                    this.AddInfoLog( LocalizedStrings.Str1236Params, decreaseBy );
                    decreaseBy = GetProtectiveVolumeDiff( source.ToArray(), decreaseBy );
                    if ( decreaseBy <= Decimal.Zero )
                        break;
                }
            }
        }

        private ApPositionManager GetPositionManager( Security security )
        {
            return _securityPositionManager.SafeAdd( security, new Func<Security, ApPositionManager>( CreateApPositionManager ) );
        }

        private void SetupProtectiveStrategy( ApPositionManager posManager, MyTrade myTrade, Decimal price )
        {
            Strategy? strategy = Protect( myTrade, price );
            if ( strategy == null )
            {
                this.AddWarningLog( LocalizedStrings.Str1237 );
            }
            else
            {
                strategy.NewMyTrade += ( t =>
                {
                    var position = posManager.GetPosition();
                    posManager.ProcessMessage( t.ToMessage() );
                    LoggingHelper.AddInfoLog( this, LocalizedStrings.Str1239Params, t.Trade.Security.Id, position, posManager.GetPosition(), t );
                } );
                ChildStrategies.Add( strategy );
            }
        }

        private Decimal GetProtectiveVolumeDiff( IProtectiveStrategy[ ] protectiveStrategies, Decimal volume )
        {
            if ( volume <= Decimal.Zero )
                throw new ArgumentOutOfRangeException();
            Decimal protectiveVolume = ( ( IEnumerable<IProtectiveStrategy> )protectiveStrategies ).First().ProtectiveVolume;
            Decimal volDiff = Math.Max( Decimal.Zero, protectiveVolume - volume );
            if ( volDiff == Decimal.Zero )
            {
                this.AddInfoLog( LocalizedStrings.Str1240 );
                MoreEnumerable.ForEach( protectiveStrategies.Cast<Strategy>(), s => s.Stop() );
            }
            else
            {
                foreach ( IProtectiveStrategy protectiveStrategy in protectiveStrategies )
                    protectiveStrategy.ProtectiveVolume = volDiff;
            }
            return volume - protectiveVolume;
        }

        private ApPositionManager CreateApPositionManager( Security _param1 )
        {
            ApPositionManager uxtiGxGpjHpKye5uFzi = new ApPositionManager();
            uxtiGxGpjHpKye5uFzi.Parent = this;
            return uxtiGxGpjHpKye5uFzi;
        }




        private sealed class ApPositionManager : PositionManager
        {
            private Decimal _position;

            public ApPositionManager()
              : base( false )
            {
            }

            public Decimal GetPosition()
            {
                return _position;
            }

            public void SetPosition( Decimal _param1 )
            {
                _position = _param1;
            }

            public override PositionChangeMessage? ProcessMessage( Message _param1 )
            {
                PositionChangeMessage message = base.ProcessMessage( _param1 );
                if ( message != null )
                {
                    Decimal? posCurrentValue = message.TryGetDecimal( PositionChangeTypes.CurrentValue );
                    if ( posCurrentValue.HasValue )
                        SetPosition( posCurrentValue.Value );
                }
                return message;
            }
        }
    }
}