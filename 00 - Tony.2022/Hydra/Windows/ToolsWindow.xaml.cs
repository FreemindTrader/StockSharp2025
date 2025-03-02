using DevExpress.Xpf.Core;
using MoreLinq;
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
    public partial class ToolsWindow : ThemedWindow, IComponentConnector
    {
        private readonly ObservableCollection<TaskInfo> _tasks = new ObservableCollection<TaskInfo>();
        
        public ToolsWindow()
        {
            InitializeComponent();
            TasksCtrl.ItemsSource = _tasks;
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
                
                _tasks.Clear();
                
                foreach ( Type task in value )
                {
                    TaskInfo taskInfo = new TaskInfo( task ) { IsVisible = true };
                    taskInfo.Selected += new Action( InfoOnSelected );
                    _tasks.Add( taskInfo );
                }
            }
        }

        public Type[ ] SelectedTasks
        {
            get
            {
                return _tasks.Where( i => i.IsSelected ).Select( i => i.Task ).ToArray();
            }
        }

        private void SelectAll_OnClick( object sender, RoutedEventArgs e )
        {
            _tasks.ForEach( i => i.IsSelected = true );
        }

        private void UnSelectAll_OnClick( object sender, RoutedEventArgs e )
        {
            _tasks.ForEach( i => i.IsSelected = false );
        }

        private void InfoOnSelected()
        {
            TryEnableOk();
        }

        private void TryEnableOk()
        {
            OkBtn.IsEnabled = SelectedTasks.Any();
        }        
    }
}
