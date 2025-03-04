using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using Ecng.Collections;
using Ecng.Common;
using MoreLinq;
using StockSharp.Hydra.Core;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace StockSharp.Hydra.Windows
{
    public partial class SourcesWindow : ThemedWindow, IComponentConnector
    {
        private readonly ObservableCollection<TaskInfo> _tasks = new ObservableCollection<TaskInfo>();
        private MessageAdapterCategories[ ] _lastCategories = Array.Empty<MessageAdapterCategories>();
        private readonly Dictionary<CheckEdit, MessageAdapterCategories> _categories;
        
        public SourcesWindow()
        {
            InitializeComponent();
            TasksCtrl.ItemsSource = _tasks;
            _categories = new Dictionary<CheckEdit, MessageAdapterCategories>()
            {
                {
                    History,
                    MessageAdapterCategories.History
                },
                {
                    RealTime,
                    MessageAdapterCategories.RealTime
                },
                {
                    Stock,
                    MessageAdapterCategories.Stock
                },
                {
                    Currency,
                    MessageAdapterCategories.FX
                },
                {
                    Futures,
                    MessageAdapterCategories.Futures
                },
                {
                    Options,
                    MessageAdapterCategories.Options
                },
                {
                    Bitcoins,
                    MessageAdapterCategories.Crypto
                },
                {
                    Ticks,
                    MessageAdapterCategories.Ticks
                },
                {
                    Candles,
                    MessageAdapterCategories.Candles
                },
                {
                    Depths,
                    MessageAdapterCategories.MarketDepth
                },
                {
                    Level1,
                    MessageAdapterCategories.Level1
                },
                {
                    OrderLog,
                    MessageAdapterCategories.OrderLog
                },
                {
                    News,
                    MessageAdapterCategories.News
                },
                {
                    Transactions,
                    MessageAdapterCategories.Transactions
                },
                {
                    Free,
                    MessageAdapterCategories.Free
                },
                {
                    Paid,
                    MessageAdapterCategories.Paid
                }
            };
        }

        public Type[ ] AvailableTasks
        {
            get
            {
                return _tasks.Select( i => i.Task ).ToArray();
            }
            set
            {
                if ( value == null )
                    throw new ArgumentNullException( nameof( value ) );
                
                if ( LocalizedStrings.ActiveLanguage != "ru" )
                    value = value.OrderBy( t => t.IsCategoryOf( MessageAdapterCategories.Russia ) ).ToArray();
                
                _tasks.Clear();

                foreach ( Type task1 in value )
                {
                    TaskInfo task2 = new TaskInfo( task1 );
                    task2.IsVisible = IsTaskVisible( task2 );
                    task2.Selected += new Action( InfoOnSelected );
                    _tasks.Add( task2 );
                }
            }
        }

        private IEnumerable<TaskInfo> VisibleTasks
        {
            get
            {
                return _tasks.Where( i => i.IsVisible );
            }
        }

        public Type[ ] SelectedTasks
        {
            get
            {
                return VisibleTasks.Where( i => i.IsSelected ).Select( i => i.Task ).ToArray();
            }
        }

        private void OnFilterChanged( object sender, RoutedEventArgs e )
        {
            if ( _categories == null )
                return;
            _lastCategories = _categories.Where( p =>
            {
                bool? isChecked = p.Key.IsChecked;
                bool flag = true;
                return isChecked.GetValueOrDefault() == flag & isChecked.HasValue;
            } ).Select( p => p.Value ).ToArray();
            RefreshTasks();
            TryEnableOk();
        }

        private void RefreshTasks()
        {
            _tasks.ForEach( t => t.IsVisible = IsTaskVisible( t ) );
        }

        private bool IsTaskVisible( TaskInfo task )
        {
            string text = NameLike.Text;
            if ( !_lastCategories.IsEmpty() && !_lastCategories.All( new Func<MessageAdapterCategories, bool>( ( task.Task ).IsCategoryOf ) ) )
                return false;
            if ( !text.IsEmpty() && !task.Name.ContainsIgnoreCase( text ) )
                return task.Description.ContainsIgnoreCase( text );
            return true;
        }

        private void SelectAll_OnClick( object sender, RoutedEventArgs e )
        {
            VisibleTasks.ForEach( i => i.IsSelected = true );
        }

        private void UnSelectAll_OnClick( object sender, RoutedEventArgs e )
        {
            VisibleTasks.ForEach( i => i.IsSelected = false );
        }

        private void InfoOnSelected()
        {
            TryEnableOk();
        }

        private void TryEnableOk()
        {
            OkBtn.IsEnabled = SelectedTasks.Any();
        }

        private void NameLike_OnTextChanged( object sender, RoutedEventArgs e )
        {
            RefreshTasks();
        }        
    }
}
