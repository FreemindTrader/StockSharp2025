// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.DataSourceWindow
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Markup;
using DevExpress.Xpf.Core;

#nullable disable
namespace StockSharp.Studio.Controls;

public partial class DataSourceWindow : ThemedWindow, IComponentConnector
{
    public static readonly RoutedCommand OkCommand = new RoutedCommand();

    public IList<Tuple<string, object>> DataSourceItemsSource => this.DataSourcePanel.ItemsSource;

    public Tuple<string, object> SelectedDataSource
    {
        get => this.DataSourcePanel.SelectedItem;
        set => this.DataSourcePanel.SelectedItem = value;
    }

    public Action Configure
    {
        get => this.DataSourcePanel.Configure;
        set => this.DataSourcePanel.Configure = value;
    }

    public DataSourceWindow() => this.InitializeComponent();

    private void OkCommand_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = this.SelectedDataSource != null;
    }

    private void OkCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        this.DialogResult = new bool?(true);
        this.Close();
    }


}
