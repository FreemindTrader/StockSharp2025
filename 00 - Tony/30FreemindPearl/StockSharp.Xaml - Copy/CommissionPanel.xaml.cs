using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Xaml;
using StockSharp.Algo.Commissions;
using StockSharp.Xaml.GridControl;
using StockSharp.Xaml.PropertyGrid;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    public partial class CommissionPanel : UserControl
    {
        private sealed class RuleItem
        {
            private string _name;
            private string _description;
            private ICommissionRule _rule;

            public string Name
            {
                get
                {
                    return this._name;
                }
                set
                {
                    this._name = value;
                }
            }

            public string Description
            {
                get
                {
                    return this._description;
                }
                set
                {
                    this._description = value;
                }
            }

            public ICommissionRule Rule
            {
                get
                {
                    return this._rule;
                }
                set
                {
                    this._rule = value;
                }
            }
        }

        private readonly Dictionary<Type, Tuple<string, string>> _names = new Dictionary<Type, Tuple<string, string>>();
        private readonly ConvertibleObservableCollection<ICommissionRule, RuleItem> _rules;

        public CommissionPanel( )
        {
            InitializeComponent( );

            var itemsSource = new ObservableCollectionEx<RuleItem>();
            RuleGrid.ItemsSource = itemsSource;

            _rules = new ConvertibleObservableCollection<ICommissionRule, RuleItem>( new ThreadSafeObservableCollection<RuleItem>( itemsSource ), CreateItem );

            var ruleTypes = new[]
            {
                typeof(CommissionPerOrderCountRule),
                typeof(CommissionPerOrderRule),
                typeof(CommissionPerOrderVolumeRule),
                typeof(CommissionPerTradeCountRule),
                typeof(CommissionPerTradePriceRule),
                typeof(CommissionPerTradeRule),
                typeof(CommissionPerTradeVolumeRule),
                typeof(CommissionSecurityIdRule),
                typeof(CommissionSecurityTypeRule),
                typeof(CommissionTurnOverRule),
                typeof(CommissionBoardCodeRule)
            };

            _names.AddRange( ruleTypes.ToDictionary( t => t, t => Tuple.Create( t.GetDisplayName( ), t.GetDescription( ) ) ) );

            TypeCtrl.ItemsSource = _names;
            TypeCtrl.SelectedIndex = 0;
        }

        /// <summary>
		/// The list of commission rules added to the table.
		/// </summary>
		public IListEx<ICommissionRule> Rules => _rules;

        private CommissionPanel.RuleItem CreateItem( ICommissionRule rule )
        {
            if ( rule == null )
                throw new ArgumentNullException( "rule" );

            var tuple = _names[rule.GetType()];

            return new CommissionPanel.RuleItem( ) { Rule = rule, Name = tuple.Item1, Description = tuple.Item2 };
        }

        private KeyValuePair<Type, Tuple<string, string>>? SelectedType => ( KeyValuePair<Type, Tuple<string, string>>? ) TypeCtrl.SelectedItem;

        private void AddRule_Click( object sender, RoutedEventArgs e )
        {
            var rule = SelectedType.Value.Key.CreateInstance<ICommissionRule>();

            this._rules.Add( rule );
        }

        private void RemoveRule_Click( object sender, RoutedEventArgs e )
        {
            _rules.RemoveRange( RuleGrid.SelectedItems.Cast<RuleItem>( ).Select( r => r.Rule ).ToArray( ) );
        }

        private void RuleGrid_SelectedItemChanged( object sender, SelectedItemChangedEventArgs e )
        {
            var item = ( RuleItem ) RuleGrid.SelectedItem;

            Settings.SelectedObject = item == null ? null : item.Rule;
            Description.Text = item == null ? null : item.Description;
            RemoveRule.IsEnabled = item != null;            
        }

        private void TypeCtrl_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            AddRule.IsEnabled = SelectedType.HasValue ;
        }
    }
}
