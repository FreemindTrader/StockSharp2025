using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Xaml;
using MoreLinq;
using StockSharp.Algo;
using StockSharp.Algo.Derivatives;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StockSharp.Xaml
{
    public class OptionDeskModel
    {
        private readonly Dictionary<SecurityId, IBlackScholes> _options = new Dictionary<SecurityId, IBlackScholes>();
        private readonly Dictionary<Decimal, RefPair<Security, Security>> _callPutPair = new Dictionary<Decimal, RefPair<Security, Security>>();
        private readonly ThreadSafeObservableCollection<OptionDeskRow> _itemSource;
        private readonly ObservableCollectionEx<OptionDeskRow> _rows;
        private readonly HashSet<Level1Fields> _optionFields;
        private IMarketDataProvider _provider;
        private Security _security;
        private bool _useBlackModel;

        public OptionDeskModel( )
        {
            _rows = new ObservableCollectionEx<OptionDeskRow>();
            _itemSource = new ThreadSafeObservableCollection<OptionDeskRow>( _rows );

            HashSet<Level1Fields> level1FieldsSet = new HashSet<Level1Fields>();
            level1FieldsSet.Add( Level1Fields.Delta );
            level1FieldsSet.Add( Level1Fields.Gamma);
            level1FieldsSet.Add( Level1Fields.Vega );
            level1FieldsSet.Add( Level1Fields.Theta );
            level1FieldsSet.Add( Level1Fields.Rho );

            _optionFields = level1FieldsSet;
        }

        public ISet<Level1Fields> EvaluateFildes
        {
            get
            {
                return _optionFields;
            }
        }

        public IEnumerable<Security> Options
        {
            get
            {
                return _options.Select( x => x.Value.Option ).ToArray<Security>();
            }
        }

        public IEnumerable<OptionDeskRow> Rows
        {
            get
            {
                return _rows;
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

        public Security UnderlyingAsset
        {
            get
            {
                return _security;
            }
            set
            {
                if ( _security == value )
                    return;
                _security = value;
                Clear();
            }
        }

        public bool UseBlackModel
        {
            get
            {
                return _useBlackModel;
            }
            set
            {
                if ( _useBlackModel == value )
                    return;
                _useBlackModel = value;
                RefreshOptions();
            }
        }

        public void Add( Security security )
        {
            if ( security == null )
                throw new ArgumentNullException( nameof( security ) );

            if ( UnderlyingAsset == null )
                throw new InvalidOperationException( LocalizedStrings.Str1850 );

            SecurityTypes? type = security.Type;

            if ( ( !security.Type.HasValue ) || ( security.Type.GetValueOrDefault() != SecurityTypes.Option ) )
                return;
            
            
            Decimal? strike = security.Strike;
            if ( !strike.HasValue )
                return;

            OptionTypes? optionType = security.OptionType;
            if ( !optionType.HasValue )
                return;

            var refPair = _callPutPair.SafeAdd( strike.Value, x => new RefPair<Security, Security>() );
            
            if ( optionType.HasValue && optionType.GetValueOrDefault() == OptionTypes.Call  )
            {
                refPair.First = security;
            }                
            else
            {
                refPair.Second = security;
            }
                
            RefreshOptionDeskRows();
            RefreshOptions();
        }

        public void Remove( Security security )
        {
            if ( security == null )
                throw new ArgumentNullException( nameof( security ) );

            if ( UnderlyingAsset == null )
                throw new InvalidOperationException( LocalizedStrings.Str1850 );

            if ( ( !security.Type.HasValue ) || ( security.Type.GetValueOrDefault() != SecurityTypes.Option ) )
                return;

            Decimal? strike = security.Strike;
            if ( !strike.HasValue )
                return;

            OptionTypes? optionType = security.OptionType;
            if ( !optionType.HasValue )
                return;

            var refPair = _callPutPair.SafeAdd( strike.Value, x => new RefPair<Security, Security>() );

            if ( optionType.HasValue )
            {
                if (  optionType.GetValueOrDefault() == OptionTypes.Call && refPair.First == security )
                {
                    refPair.First = null;
                }

                if ( optionType.GetValueOrDefault() == OptionTypes.Call && refPair.Second == security )
                {
                    refPair.Second = null;
                }                    
            }
            
            RefreshOptionDeskRows();
            RefreshOptions();
        }

        public void Clear( )
        {
            _itemSource.Clear();
            _options.Clear();
            _callPutPair.Clear();
        }

        public void Refresh( DateTimeOffset? currentTime = null, Decimal? assetPrice = null )
        {            
            if ( MarketDataProvider == null || UnderlyingAsset == null )
            {
                return;
            }
                
            currentTime = new DateTimeOffset?( currentTime ?? TimeHelper.NowWithOffset );

            if ( ! assetPrice.HasValue )
            {
                assetPrice = ( Decimal? ) MarketDataProvider.GetSecurityValue( UnderlyingAsset, Level1Fields.LastTradePrice );
            }

            decimal maxImpliedVolatilityBestBid   = 0;
            decimal maxImpliedVolatilityBestAsk   = 0;
            decimal maxImpliedVolatilityLastTrade = 0;
            decimal maxHistoricalVolatility       = 0;
            decimal maxCallVolume                 = 0;
            decimal maxCallOpenInterest           = 0;
            decimal maxPutVolume                  = 0;
            decimal maxPutOpenInterest            = 0;
            decimal maxPnL                        = 0;

            

            foreach ( OptionDeskRow optionRow in _itemSource )
            {
                if ( optionRow.Call != null )
                {
                    Decimal? impliedVolatilityBestBid;
                    Decimal? impliedVolatilityBestAsk;
                    Decimal? impliedVolatilityLastTrade;
                    Decimal? historicalVolatility;
                    Decimal? openInterest;
                    Decimal? volume;

                    GetOptionDetails( optionRow.Call, currentTime.Value, assetPrice, out impliedVolatilityBestBid, out impliedVolatilityBestAsk, out impliedVolatilityLastTrade, out historicalVolatility, out openInterest, out volume );

                    maxImpliedVolatilityBestBid   = MathHelper.Max( maxImpliedVolatilityBestBid, impliedVolatilityBestBid ?? 0 );
                    maxImpliedVolatilityBestAsk   = MathHelper.Max( maxImpliedVolatilityBestAsk, impliedVolatilityBestAsk ?? 0);
                    maxImpliedVolatilityLastTrade = MathHelper.Max( maxImpliedVolatilityLastTrade, impliedVolatilityLastTrade ?? 0 );
                    maxHistoricalVolatility       = MathHelper.Max( maxHistoricalVolatility, historicalVolatility ?? 0 );
                    maxCallVolume                 = MathHelper.Max( maxCallVolume, volume ?? 0 );
                    maxCallOpenInterest           = MathHelper.Max( maxCallOpenInterest, openInterest ?? 0 );
                }

                if ( optionRow.Put != null )
                {
                    Decimal? impliedVolatilityBestBid;
                    Decimal? impliedVolatilityBestAsk;
                    Decimal? impliedVolatilityLastTrade;
                    Decimal? historicalVolatility;
                    Decimal? openInterest;
                    Decimal? volume;

                    GetOptionDetails( optionRow.Put, currentTime.Value, assetPrice, out impliedVolatilityBestBid, out impliedVolatilityBestAsk, out impliedVolatilityLastTrade, out historicalVolatility, out openInterest, out volume );

                    maxImpliedVolatilityBestBid   = MathHelper.Max( maxImpliedVolatilityBestBid, impliedVolatilityBestBid ?? 0 );
                    maxImpliedVolatilityBestAsk   = MathHelper.Max( maxImpliedVolatilityBestAsk, impliedVolatilityBestAsk ?? 0 );
                    maxImpliedVolatilityLastTrade = MathHelper.Max( maxImpliedVolatilityLastTrade, impliedVolatilityLastTrade ?? 0 );
                    maxHistoricalVolatility       = MathHelper.Max( maxHistoricalVolatility, historicalVolatility ?? 0 );
                    maxPutVolume                  = MathHelper.Max( maxPutVolume, volume ?? 0 );
                    maxPutOpenInterest            = MathHelper.Max( maxPutOpenInterest, openInterest ?? 0 );
                }

                maxPnL = MathHelper.Max( maxPnL, optionRow.PnL ?? 0);                
            }


            if ( maxImpliedVolatilityBestBid   == 0 ) { maxImpliedVolatilityBestBid = 1; }
            if ( maxImpliedVolatilityBestAsk   == 0 ) { maxImpliedVolatilityBestAsk = 1; }
            if ( maxImpliedVolatilityLastTrade == 0 ) { maxImpliedVolatilityLastTrade = 1; }
            if ( maxHistoricalVolatility       == 0 ) { maxHistoricalVolatility = 1; }
            if ( maxCallVolume                 == 0 ) { maxCallVolume = 1; }
            if ( maxCallOpenInterest           == 0 ) { maxCallOpenInterest = 1; }
            if ( maxPutVolume                  == 0 ) { maxPutVolume = 1; }
            if ( maxPutOpenInterest            == 0 ) { maxPutOpenInterest = 1; }
            if ( maxPnL                        == 0 ) { maxPnL = 1; }

            foreach ( OptionDeskRow optionRow in _itemSource )
            {
                optionRow.MaxImpliedVolatilityBestBid   = maxImpliedVolatilityBestBid;
                optionRow.MaxImpliedVolatilityBestAsk   = maxImpliedVolatilityBestAsk;
                optionRow.MaxImpliedVolatilityLastTrade = maxImpliedVolatilityLastTrade;
                optionRow.MaxHistoricalVolatility       = maxHistoricalVolatility;
                optionRow.MaxPnL                        = maxPnL;

                if ( optionRow.Call != null )
                {
                    optionRow.Call.MaxOpenInterest = maxCallOpenInterest;
                    optionRow.Call.MaxVolume = maxCallVolume;
                }

                if ( optionRow.Put != null )
                {
                    optionRow.Put.MaxOpenInterest = maxPutOpenInterest;
                    optionRow.Put.MaxVolume = maxPutVolume;
                }

                optionRow.Notify<OptionDeskRow>( "MaxImpliedVolatilityBestBid" );
                optionRow.Notify<OptionDeskRow>( "MaxImpliedVolatilityBestAsk" );
                optionRow.Notify<OptionDeskRow>( "MaxImpliedVolatilityLastTrade" );
                optionRow.Notify<OptionDeskRow>( "MaxHistoricalVolatility" );
                optionRow.Notify<OptionDeskRow>( "MaxPnL" );
                optionRow.Notify<OptionDeskRow>( "Call" );
                optionRow.Notify<OptionDeskRow>( "Put" );
            }            
        }        

        private void GetOptionDetails( OptionDeskRow.OptionDeskRowSide callOrPut, DateTimeOffset currentTime, Decimal? assetPrice, out Decimal? impliedVolatilityBestBid, out Decimal? impliedVolatilityBestAsk, out Decimal? impliedVolatilityLastTrade, out Decimal? historicalVolatility, out Decimal? openInterest, out Decimal? volume )
        {
            impliedVolatilityBestBid   = null;
            impliedVolatilityBestAsk   = null;
            impliedVolatilityLastTrade = null;
            historicalVolatility       = null;
            openInterest               = null;
            volume                     = null;

            Security optionInfo = callOrPut.Option;
            var optionValues = MarketDataProvider.GetSecurityValues( optionInfo );

            if ( optionValues == null )
                return;

            var optionModel = (IBlackScholes) _options.TryGetValue( optionInfo.ToSecurityId( ) );
            if ( optionModel == null )
                return;

            var calculator             = new OptionCalculator( currentTime, optionValues, optionModel, EvaluateFildes, new Decimal?(), assetPrice);
            callOrPut.BestAskPrice     = calculator.BestAskPrice;
            callOrPut.BestBidPrice     = calculator.BestBidPrice;
            callOrPut.Delta            = calculator.Delta;
            callOrPut.Gamma            = calculator.Gamma;
            callOrPut.Theta            = calculator.Theta;
            callOrPut.Vega             = calculator.Vega;
            callOrPut.Rho              = calculator.Rho;
            openInterest               = callOrPut.OpenInterest = calculator.OpenInterest;
            callOrPut.TheorPrice       = calculator.TheorPrice;
            volume                     = callOrPut.Volume = calculator.Volume;
            impliedVolatilityBestBid   = callOrPut.ImpliedVolatilityBestBid = calculator.ImpliedVolatilityBestBid;
            impliedVolatilityBestAsk   = callOrPut.ImpliedVolatilityBestAsk = calculator.ImpliedVolatilityBestAsk;
            impliedVolatilityLastTrade = callOrPut.ImpliedVolatilityLastTrade = calculator.ImpliedVolatilityLastTrade;
            historicalVolatility       = callOrPut.HistoricalVolatility = calculator.HistoricalVolatility;

            if ( assetPrice.HasValue )
            {                
                OptionTypes? optionType = callOrPut.Option.OptionType;
                Decimal sign = ( optionType.GetValueOrDefault() == OptionTypes.Call && optionType.HasValue ) ? 1 : -1;
                                
                Decimal? inOutMoney = ( assetPrice.HasValue & callOrPut.Option.Strike.HasValue ) ? new Decimal?(assetPrice.Value - callOrPut.Option.Strike.GetValueOrDefault()) : null;                

                if ( !inOutMoney.HasValue )
                {
                    callOrPut.Option.Strike = null;
                    callOrPut.PnL           = null;
                }
                else
                {
                    callOrPut.PnL = new Decimal?( sign * inOutMoney.GetValueOrDefault() );
                }
                
                if ( callOrPut.PnL.HasValue )
                {
                    // Since we are in the money
                    if( callOrPut.PnL.GetValueOrDefault( ) >= 0 )
                        return;
                }                                                
            }
            
            callOrPut.PnL = new Decimal?( new Decimal() );
        }

        private void RefreshOptionDeskRows( )
        {
            _itemSource.Clear();

            var options = _callPutPair.OrderBy( x => x.Key );

            foreach ( var option in options )
            {
                if ( option.Value.First != null || option.Value.Second != null )
                {
                    _itemSource.Add( new OptionDeskRow( UnderlyingAsset, option.Value.First, option.Value.Second ) );
                }
                    
            }            
        }

        private void RefreshOptions( )
        {
            _options.Clear();

            foreach ( OptionDeskRow optionDeskRow in _itemSource )
            {
                if ( optionDeskRow.Call != null )
                {
                    InitOptions( optionDeskRow.Call );
                }
                    
                if ( optionDeskRow.Put != null )
                {
                    InitOptions( optionDeskRow.Put );
                }
                    
            }            
        }

        private void InitOptions( OptionDeskRow.OptionDeskRowSide callOrPut )
        {
            Security option = callOrPut.Option;
            var blackScholes = UseBlackModel ? (BlackScholes) new Black(option, UnderlyingAsset, MarketDataProvider) : new BlackScholes(option, UnderlyingAsset, MarketDataProvider);

            _options.Add( option.ToSecurityId( ), blackScholes );
        }

        private sealed class OptionCalculator
        {
            private readonly DateTimeOffset                    _currentTime;
            private readonly IDictionary<Level1Fields, object> _securityValues;
            private readonly IBlackScholes                     _blackScholesModel;
            private readonly ISet<Level1Fields>                _optionFields;
            private readonly Decimal?                          _deviation;
            private readonly Decimal?                          _assetPrice;

            public OptionCalculator( DateTimeOffset currentTime, IDictionary<Level1Fields, object> optionValues, IBlackScholes optionModel, ISet<Level1Fields> optionFields, Decimal? deviation, Decimal? assetPrice )
            {
                _currentTime = currentTime;
                
                if ( optionValues == null )
                    throw new ArgumentNullException( "changes" );

                _securityValues = optionValues;
                
                if ( optionModel == null )
                    throw new ArgumentNullException( "model" );
                _blackScholesModel = optionModel;
                
                if ( optionFields == null )
                    throw new ArgumentNullException( "evaluates" );

                _optionFields = optionFields;
                _deviation    = deviation;
                _assetPrice   = assetPrice;
            }

            private Decimal? GetFieldValue( Level1Fields level1Field, Level1Fields? optionalField )
            {
                if ( ! _optionFields.Contains( level1Field ) )
                {
                    return ( Decimal? ) CollectionHelper.TryGetValue<Level1Fields, object>( _securityValues, level1Field );
                }
                    
                if ( level1Field != Level1Fields.ImpliedVolatility )
                {
                    switch ( level1Field )
                    {
                        case Level1Fields.Delta:
                            return _blackScholesModel.Delta( _currentTime, _deviation, _assetPrice );

                        case Level1Fields.Gamma:
                            return _blackScholesModel.Gamma( _currentTime, _deviation, _assetPrice );

                        case Level1Fields.Vega:
                            return _blackScholesModel.Vega( _currentTime, _deviation, _assetPrice );

                        case Level1Fields.Theta:
                            return _blackScholesModel.Theta( _currentTime, _deviation, _assetPrice );

                        default:
                            if ( level1Field != Level1Fields.Rho )
                            {
                                throw new InvalidOperationException();
                            }
                                
                            return _blackScholesModel.Rho( _currentTime, _deviation, _assetPrice );
                    }
                }
                else
                {                    
                    if ( optionalField.HasValue )
                    {
                        Level1Fields optional = optionalField.GetValueOrDefault();
                        Decimal? requestedFieldValue;

                        if ( optional != Level1Fields.LastTradePrice )
                        {
                            if ( optional != Level1Fields.BestBidPrice )
                            {
                                if ( optional == Level1Fields.BestAskPrice )
                                {
                                    requestedFieldValue = BestAskPrice;
                                }                                    
                                else
                                {
                                    throw new ArgumentOutOfRangeException( "extraField" );
                                }                                    
                            }
                            else
                            {
                                requestedFieldValue = BestBidPrice;
                            }                                
                        }
                        else
                        {
                            requestedFieldValue = LastTradePrice();
                        }
                            
                        if ( !requestedFieldValue.HasValue )
                            return new Decimal?();

                        return _blackScholesModel.ImpliedVolatility( _currentTime, requestedFieldValue.Value );
                    }                                    
                }

                throw new InvalidOperationException();
            }

            public Decimal? BestAskPrice
            {
                get
                {
                    Decimal? bestAskPrice = GetFieldValue( Level1Fields.BestAskPrice, new Level1Fields?() );

                    if ( bestAskPrice.HasValue )
                    {
                        return bestAskPrice;
                    }
                        
                    return ( ( Quote ) _securityValues.TryGetValue( Level1Fields.BestAsk ) )?.Price;
                }
            }

            public Decimal? BestBidPrice
            {
                get
                {
                    Decimal? bestBidPrice = GetFieldValue( Level1Fields.BestBidPrice, new Level1Fields?());

                    if ( bestBidPrice.HasValue )
                    {
                        return bestBidPrice;
                    }
                        
                    return ( ( Quote ) _securityValues.TryGetValue( Level1Fields.BestBid ) )?.Price;
                }
            }

            public Decimal? Delta
            {
                get
                {
                    return GetFieldValue(  Level1Fields.Delta, new Level1Fields?() );
                }
            }

            public Decimal? Gamma
            {
                get
                {
                    return GetFieldValue( Level1Fields.Gamma, new Level1Fields?() );
                }
            }

            public Decimal? Theta
            {
                get
                {
                    return GetFieldValue( Level1Fields.Theta, new Level1Fields?() );
                }
            }

            public Decimal? Vega
            {
                get
                {
                    return GetFieldValue( Level1Fields.Vega, new Level1Fields?() );
                }
            }

            public Decimal? Rho
            {
                get
                {
                    return GetFieldValue( Level1Fields.Rho, new Level1Fields?() );
                }
            }

            public Decimal? Volume
            {
                get
                {
                    return GetFieldValue( Level1Fields.Volume, new Level1Fields?() );
                }
            }

            public Decimal? OpenInterest
            {
                get
                {
                    return GetFieldValue( Level1Fields.OpenInterest, new Level1Fields?() );
                }
            }

            public Decimal? TheorPrice
            {
                get
                {
                    return GetFieldValue( Level1Fields.TheorPrice, new Level1Fields?() );
                }
            }

            public Decimal? ImpliedVolatilityBestBid
            {
                get
                {
                    return GetFieldValue( Level1Fields.ImpliedVolatility, new Level1Fields?( Level1Fields.BestBidPrice ) );
                }
            }

            public Decimal? ImpliedVolatilityBestAsk
            {
                get
                {
                    return GetFieldValue( Level1Fields.ImpliedVolatility, new Level1Fields?( Level1Fields.BestAskPrice ) );
                }
            }

            public Decimal? ImpliedVolatilityLastTrade
            {
                get
                {
                    return GetFieldValue( Level1Fields.ImpliedVolatility, new Level1Fields?(  Level1Fields.LastTradePrice  ) );
                }
            }

            public Decimal? HistoricalVolatility
            {
                get
                {
                    return GetFieldValue( Level1Fields.HistoricalVolatility, new Level1Fields?() );
                }
            }

            private Decimal? LastTradePrice( )
            {
                Decimal? lastTradePrice = GetFieldValue( Level1Fields.LastTradePrice, new Level1Fields?());

                if ( lastTradePrice.HasValue )
                {
                    return lastTradePrice;
                }
                    
                return ( ( Trade ) _securityValues.TryGetValue( Level1Fields.ClosePrice ) )?.Price;
            }
        }

        

        
    }
}
