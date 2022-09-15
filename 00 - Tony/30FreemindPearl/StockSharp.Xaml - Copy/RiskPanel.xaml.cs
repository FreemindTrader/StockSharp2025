using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo.Risk;
using StockSharp.Xaml.GridControl;
using StockSharp.Xaml.PropertyGrid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace StockSharp.Xaml
{
    public partial class RiskPanel : UserControl, IPersistable
    {
        private readonly Dictionary<Type, Tuple<string, string>> _names = new Dictionary<Type, Tuple<string, string>>();
        private IList<IRiskRule> _rules = (IList<IRiskRule>) new List<IRiskRule>();
        private readonly ConvertibleObservableCollection<IRiskRule, RuleItem> _rulesItems;
               

        public RiskPanel( )
        {
            InitializeComponent();

            var ruleTypes = new[]
            {
                typeof (RiskCommissionRule),
                typeof (RiskOrderFreqRule),
                typeof (RiskOrderPriceRule),
                typeof (RiskOrderVolumeRule),
                typeof (RiskPnLRule),
                typeof (RiskPositionSizeRule),
                typeof (RiskPositionTimeRule),
                typeof (RiskSlippageRule),
                typeof (RiskTradeFreqRule),
                typeof (RiskTradePriceRule),
                typeof (RiskTradeVolumeRule)
            };

            var toBeAdded = ruleTypes.ToDictionary( t => t, t => Tuple.Create( t.GetDisplayName(), t.GetDescription(  ) ) );

            _names.AddRange( toBeAdded );

            TypeCtrl.ItemsSource = _names;
            TypeCtrl.SelectedIndex = 0;

            var itemsSource = new ObservableCollectionEx<RuleItem>();
            RuleGrid.ItemsSource = itemsSource;

            _rulesItems = new ConvertibleObservableCollection<IRiskRule, RuleItem>( new ThreadSafeObservableCollection<RuleItem>( itemsSource ), CreateItem );
            _rulesItems.AddedRange += OnAddRange;
            _rulesItems.RemovedRange += OnRemoveRange;
        }

        private void OnRemoveRange( IEnumerable<IRiskRule> rules )
        {
            CollectionHelper.RemoveRange<IRiskRule>( _rules, rules );
        }

        private void OnAddRange( IEnumerable<IRiskRule> rules )
        {
            CollectionHelper.AddRange<IRiskRule>( _rules, rules );
        }

        public IList<IRiskRule> Rules
        {
            get
            {
                return _rules;
            }
            set
            {                
                if ( value == null )
                {
                    throw new ArgumentNullException( nameof( value ) );
                }

                _rules = value;
                _rulesItems.Clear();
                _rulesItems.AddRange( value );
            }
        }

        private RuleItem CreateItem( IRiskRule rule )
        {
            if ( rule == null )
            {
                throw new ArgumentNullException( nameof( rule ) );
            }

            Tuple<string, string> tuple = _names[ rule.GetType() ];
            return new RuleItem() { Rule = rule, Name = tuple.Item1, Description = tuple.Item2 };
        }

        private KeyValuePair<Type, Tuple<string, string>>? TypeControlSelectedItem( )
        {
            return ( KeyValuePair<Type, Tuple<string, string>>? ) TypeCtrl.SelectedItem;
        }

        private void AddRule_Click( object sender, RoutedEventArgs e )
        {
            var selectedItem = TypeControlSelectedItem( );

            if ( selectedItem.HasValue )
            {
                var instance = (IRiskRule) selectedItem.Value.Key.CreateInstance<IRiskRule>( );
                _rulesItems.Add( instance );
                _rules.Add( instance );
            }            
        }

        private void RemoveRule_Click( object sender, RoutedEventArgs e )
        {
            _rulesItems.RemoveRange(  RuleGrid.SelectedItems.Cast<RuleItem>().Select( x => x.Rule ).ToArray() );
        }

        private void TypeCtrl_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            ( ( UIElement ) AddRule ).IsEnabled = TypeControlSelectedItem().HasValue;
        }

        

        
        public void Load( SettingsStorage storage )
        {
            RuleGrid.Load( ( SettingsStorage ) storage.GetValue<SettingsStorage>( "RuleGrid", null ) );
        }

        public void Save( SettingsStorage storage )
        {
            storage.SetValue<SettingsStorage>( "RuleGrid",  PersistableHelper.Save( ( IPersistable ) RuleGrid ) );
        }

        public event Action LayoutChanged;

        



        private sealed class RuleItem
        {
            private string _name;
            private string _description;
            private IRiskRule _rule;

            public string Name
            {
                get
                {
                    return _name;
                }
                set
                {
                    _name = value;
                }
            }

            public string Description
            {
                get
                {
                    return _description;
                }
                set
                {
                    _description = value;
                }
            }

            public IRiskRule Rule
            {
                get
                {
                    return _rule;
                }
                set
                {
                    _rule = value;
                }
            }
        }

        

        private void RuleGrid_SelectedItemChanged( object sender, SelectedItemChangedEventArgs e )
        {
            RuleItem selectedItem = (RuleItem) RuleGrid.SelectedItem;
            Settings.SelectedObject = selectedItem?.Rule;
            Description.Text = selectedItem?.Description;
            ( ( UIElement ) RemoveRule ).IsEnabled = selectedItem != null;
        }

        private void RuleGrid_LayoutChanged( )
        {
            Action myEvent = LayoutChanged;
            if ( myEvent == null )
            {
                return;
            }

            myEvent();
        }
    }
}