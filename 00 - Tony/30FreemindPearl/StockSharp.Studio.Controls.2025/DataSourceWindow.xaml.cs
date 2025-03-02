// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.DataSourceWindow
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

using DevExpress.Xpf.Core;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace StockSharp.Studio.Controls
{
    public partial class DataSourceWindow : ThemedWindow
    {
        public static readonly RoutedCommand OkCommand = new RoutedCommand();
        
        public IList<Tuple<string, object>> DataSourceItemsSource
        {
            get
            {
                return this.DataSourcePanel.ItemsSource;
            }
        }

        public Tuple<string, object> SelectedDataSource
        {
            get
            {
                return this.DataSourcePanel.SelectedItem;
            }
            set
            {
                this.DataSourcePanel.SelectedItem = value;
            }
        }

        public Action Configure
        {
            get
            {
                return this.DataSourcePanel.Configure;
            }
            set
            {
                this.DataSourcePanel.Configure = value;
            }
        }

        public DataSourceWindow()
        {
            this.InitializeComponent();
        }

        private void OkCommand_OnCanExecute( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = this.SelectedDataSource != null;
        }

        private void OkCommand_OnExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            this.DialogResult = new bool?( true );
            this.Close();
        }        
    }
}
