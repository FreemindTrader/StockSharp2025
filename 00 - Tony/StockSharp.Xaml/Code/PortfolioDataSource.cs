using Ecng.Collections;
using Ecng.Xaml;
using StockSharp.BusinessEntities;
using System;
using System.Collections.Generic;

namespace StockSharp.Xaml
{
    public class PortfolioDataSource : IDisposable
    {
        private readonly SynchronizedSet<PortfolioPropertyChangeHandler> _portfolioPositionSet;
        private readonly IPortfolioProvider _provider;
        private readonly object _pfPeriodicalActionHandler;
        private readonly UIObservableCollectionEx<PortfolioPropertyChangeHandler> _itemSource;
        private readonly ConvertibleObservableCollection<Portfolio, PortfolioPropertyChangeHandler> _portfolioPositionsCollection;

        public PortfolioDataSource( ) : this( null )
        {
        }

        public PortfolioDataSource( IPortfolioProvider provider )
        {
            _provider                     = provider;
            _itemSource                   = new UIObservableCollectionEx<PortfolioPropertyChangeHandler>( );
            _portfolioPositionSet         = new SynchronizedSet<PortfolioPropertyChangeHandler>( );
            _portfolioPositionsCollection = new ConvertibleObservableCollection<Portfolio, PortfolioPropertyChangeHandler>( new ThreadSafeObservableCollection<PortfolioPropertyChangeHandler>( Items ), p => new PortfolioPropertyChangeHandler( p, new Action<PortfolioPropertyChangeHandler>( ( _portfolioPositionSet ).Add ) ) );
            _pfPeriodicalActionHandler    = GuiDispatcher.GlobalDispatcher.AddPeriodicalAction( new Action( pfPeriodicalAction ) );

            if ( _provider == null )
            {
                return;
            }

            GetPortfolioCollection( ).AddRange( _provider.Portfolios );
            _provider.NewPortfolio += new Action<Portfolio>( OnNewPortfolio );
        }

        internal int ItemsourceCount( )
        {
            return Items.Count;
        }

        public UIObservableCollectionEx<PortfolioPropertyChangeHandler> Items
        {
            get
            {
                return _itemSource;
            }
            
        }

        internal ConvertibleObservableCollection<Portfolio, PortfolioPropertyChangeHandler> GetPortfolioCollection( )
        {
            return _portfolioPositionsCollection;
        }

        public IEnumerable<Portfolio> Portfolios
        {
            get
            {
                return GetPortfolioCollection( ).Items;
            }
        }

        public void Add( Portfolio portfolio )
        {
            GetPortfolioCollection( ).Add( portfolio );
        }

        public void AddRange( IEnumerable<Portfolio> portfolios )
        {
            GetPortfolioCollection( ).AddRange( portfolios );
        }

        private void pfPeriodicalAction( )
        {
            if ( _portfolioPositionSet.Count == 0 )
            {
                return;
            }

            foreach ( var pos in _portfolioPositionSet.SyncGet( s => s.CopyAndClear<PortfolioPropertyChangeHandler>( ) ) )
            {
                pos.UpdateModel( );
            }
        }

        private void OnNewPortfolio( Portfolio pf )
        {
            GetPortfolioCollection( ).Add( pf );
        }

        public void Dispose( )
        {
            GuiDispatcher.GlobalDispatcher.RemovePeriodicalAction( _pfPeriodicalActionHandler );
            if ( _provider == null )
            {
                return;
            }

            _provider.NewPortfolio -= new Action<Portfolio>( OnNewPortfolio );
        }              
    }
}
