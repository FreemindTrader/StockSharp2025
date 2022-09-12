using Ecng.Common;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockSharp.Xaml
{
    public sealed class PortfolioPropertyChangeHandler : BasePosition, IModelUpdate
    {
        private readonly SyncObject _lock = new SyncObject( );
        private readonly Portfolio _portfolio;
        private readonly Action<PortfolioPropertyChangeHandler> _portfolioChangedEventHandler;
        private bool _hasChanges;
        private readonly string _portfolioName;
        private readonly string _name;
        private readonly BasePosition _position;
        private readonly string _depoName;
        private ExchangeBoard _board;
        private Decimal? _commissionTaker;
        private Decimal? _commissionMaker;
        private readonly TPlusLimits? _limitType;
        private PortfolioStates? _pfState;

        public PortfolioPropertyChangeHandler( BasePosition pos, Action<PortfolioPropertyChangeHandler> onPortfolioChanged )
        {            
            if ( pos == null )
            {
                throw new ArgumentNullException( "position" );
            }

            _position = pos;
            
            if ( onPortfolioChanged == null )
            {
                throw new ArgumentNullException( "onChanged" );
            }

            _portfolioChangedEventHandler = onPortfolioChanged;
            _portfolio = pos as Portfolio;

            if ( _portfolio == null )
            {
                var position   = ( StockSharp.BusinessEntities.Position )pos;
                _portfolioName = position.Portfolio.Name;
                _name          = position.Security.Id;
                _depoName      = position.DepoName;
                _limitType     = position.LimitType;
            }
            else
            {
                _portfolioName = _portfolio.Name;
                _name          = LocalizedStrings.Str1543;
            }

            Clone( );
            pos.PropertyChanged += new PropertyChangedEventHandler( OnPortfolioPositionChanged );
        }

        public static explicit operator Portfolio( PortfolioPropertyChangeHandler pfPos )
        {
            return pfPos?._portfolio;
        }

        public string PortfolioName
        {
            get
            {
                return _portfolioName;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public BasePosition Position
        {
            get
            {
                return _position;
            }
        }

        public string DepoName
        {
            get
            {
                return _depoName;
            }
        }

        public ExchangeBoard Board
        {
            get
            {
                return _board;
            }
            set
            {
                if ( ( Equatable<ExchangeBoard> )_board == value )
                {
                    return;
                }

                _board = value;
                NotifyChanged( nameof( Board ) );
            }
        }

        public Decimal? CommissionTaker
        {
            get
            {
                return _commissionTaker;
            }
            set
            {                
                if ( _commissionTaker.GetValueOrDefault( ) == value.GetValueOrDefault( ) & _commissionTaker.HasValue == value.HasValue )
                {
                    return;
                }

                _commissionTaker = value;
                NotifyChanged( nameof( CommissionTaker ) );
            }
        }

        public Decimal? CommissionMaker
        {
            get
            {
                return _commissionMaker;
            }
            set
            {                
                if ( _commissionMaker.GetValueOrDefault( ) == value.GetValueOrDefault( ) & _commissionMaker.HasValue == value.HasValue )
                {
                    return;
                }

                _commissionMaker = value;
                NotifyChanged( nameof( CommissionMaker ) );
            }
        }

        public TPlusLimits? LimitType
        {
            get
            {
                return _limitType;
            }
        }

        public PortfolioStates? State
        {
            get
            {
                return _pfState;
            }
            set
            {                
                
                if ( _pfState.GetValueOrDefault( ) == value.GetValueOrDefault( ) & _pfState.HasValue == value.HasValue )
                {
                    return;
                }

                _pfState = value;
                NotifyChanged( nameof( State ) );
            }
        }

        public void UpdateModel( )
        {
            lock ( _lock )
            {
                if ( !_hasChanges )
                {
                    return;
                }

                _hasChanges = false;
            }

            Clone( );
        }

        private void Clone( )
        {
            Position.CopyTo( ( BasePosition )this );

            LastChangeTime = Position.LastChangeTime;
            LocalTime      = Position.LocalTime;

            if ( _portfolio == null )
            {
                return;
            }

            Board           = _portfolio.Board;
            CommissionMaker = _portfolio.CommissionMaker;
            CommissionTaker = _portfolio.CommissionTaker;
            State           = _portfolio.State;
        }

        private void OnPortfolioPositionChanged( object sender, PropertyChangedEventArgs e )
        {
            bool oldHasChanges;
            lock ( _lock )
            {
                oldHasChanges = _hasChanges;
                _hasChanges = true;
            }

            if ( oldHasChanges )
            {
                return;
            }

            _portfolioChangedEventHandler( this );
        }

        public override string ToString( )
        {
            return Position.ToString( );
        }
    }
    
}
