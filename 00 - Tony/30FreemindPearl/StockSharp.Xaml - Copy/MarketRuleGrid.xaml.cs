using Ecng.Collections;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.Xaml.GridControl;
using System;

namespace StockSharp.Xaml
{
    public partial class MarketRuleGrid : BaseGridControl
    {
        private readonly ConvertibleObservableCollection<IMarketRule, MarketRuleGrid.SimpleMarketRule> _marketRules;

        private INotifyList<IMarketRule> _marketRuleNotifyList;

        private sealed class SimpleMarketRule
        {
            private readonly bool _isSuspended;
            private readonly bool _isActive;
            private readonly string _name;
            private readonly IMarketRule _marketRule;

            public SimpleMarketRule( IMarketRule rule )
            {
                IMarketRule imarketRule = rule;
                if ( imarketRule == null )
                {
                    throw new ArgumentNullException( "rule" );
                }

                _marketRule = imarketRule;
                _name = rule.Name;
                _isSuspended = rule.IsSuspended;
                _isActive = rule.IsActive;
            }

            public bool IsSuspended
            {
                get
                {
                    return _isSuspended;
                }
            }

            public bool IsActive
            {
                get
                {
                    return _isActive;
                }
            }

            public string Name
            {
                get
                {
                    return _name;
                }
            }

            public IMarketRule Rule
            {
                get
                {
                    return _marketRule;
                }
            }
        }
        public MarketRuleGrid( )
        {
            InitializeComponent();

            var rules = new ObservableCollectionEx< SimpleMarketRule >();
            ItemsSource = rules;
            _marketRules = new ConvertibleObservableCollection<IMarketRule, MarketRuleGrid.SimpleMarketRule>( new ThreadSafeObservableCollection<MarketRuleGrid.SimpleMarketRule>( rules ), r =>
            {
                if ( r == null )
                {
                    throw new ArgumentNullException( "rule" );
                }

                return new MarketRuleGrid.SimpleMarketRule( r );
            } );
        }

        public INotifyList<IMarketRule> Rules
        {
            get
            {
                return _marketRuleNotifyList;
            }
            set
            {
                if ( _marketRuleNotifyList == value )
                {
                    return;
                }

                if ( _marketRuleNotifyList != null )
                {
                    _marketRuleNotifyList.Added -= _marketRuleNotifyList_Added;
                    _marketRuleNotifyList.Removed -= _marketRuleNotifyList_Removed;
                    _marketRuleNotifyList.Cleared -= _marketRuleNotifyList_Cleared;
                }

                _marketRuleNotifyList = value;
                _marketRules.Clear();

                if ( _marketRuleNotifyList == null )
                {
                    return;
                }

                _marketRules.AddRange( _marketRuleNotifyList );
                _marketRuleNotifyList.Added += _marketRuleNotifyList_Added;
                _marketRuleNotifyList.Removed += _marketRuleNotifyList_Removed; 
                _marketRuleNotifyList.Cleared += _marketRuleNotifyList_Cleared; 
            }
        }

        private void _marketRuleNotifyList_Cleared( )
        {
            _marketRules.Clear();
        }

        private void _marketRuleNotifyList_Removed( IMarketRule rule )
        {
            _marketRules.Remove( rule );
        }

        private void _marketRuleNotifyList_Added( IMarketRule rule )
        {
            _marketRules.Add( rule );
        }
    }
}
