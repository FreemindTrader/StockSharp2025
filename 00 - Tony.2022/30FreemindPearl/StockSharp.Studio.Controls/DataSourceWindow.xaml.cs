// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.DataSourceWindow
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D457EB08-5750-4F4B-A104-96BE70F84CCF
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Controls.dll

using DevExpress.Xpf.Core;
using StockSharp.Xaml;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace StockSharp.Studio.Controls
{
    public partial class DataSourceWindow : ThemedWindow, IComponentConnector
    {
        public static readonly RoutedCommand OkCommand = new RoutedCommand();

        public IList<Tuple<string, object>> DataSourceItemsSource
        {
            get
            {
                return DataSourcePanel.ItemsSource;
            }
        }

        public Tuple<string, object> SelectedDataSource
        {
            get
            {
                return DataSourcePanel.SelectedItem;
            }
            set
            {
                DataSourcePanel.SelectedItem = value;
            }
        }

        public Action Configure
        {
            get
            {
                return DataSourcePanel.Configure;
            }
            set
            {
                DataSourcePanel.Configure = value;
            }
        }

        public DataSourceWindow()
        {
            InitializeComponent();
        }

        private void OkCommand_OnCanExecute( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = SelectedDataSource != null;
        }

        private void OkCommand_OnExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            DialogResult = new bool?( true );
            Close();
        }        
    }
}
