// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.SecuritiesWindowEx
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using DevExpress.Xpf.Core;
using Ecng.Collections;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml.PropertyGrid;

#nullable disable
namespace StockSharp.Studio.Controls;

public partial class SecuritiesWindowEx : ThemedWindow, ISecuritiesSelectWindow, IComponentConnector
{
    public static readonly RoutedCommand SelectSecurityCommand = new RoutedCommand();
    public static readonly RoutedCommand UnselectSecurityCommand = new RoutedCommand();

    public bool IsLookup { get; set; }

    public IEnumerable<Security> SelectedSecurities
    {
        get => this.SecuritiesSelected.Securities.LookupAll();
        set => this.SelectSecurities(value.ToArray<Security>());
    }

    public ISecurityProvider SecurityProvider
    {
        get => this.SecuritiesAll.SecurityProvider;
        set => this.SecuritiesAll.SecurityProvider = value;
    }

    public bool AllowDuplicates { get; set; }

    public SecuritiesWindowEx()
    {
        this.InitializeComponent();
        this.SecuritiesAll.SecurityDoubleClick += (Action<Security>)(security =>
        {
            if (security == null)
                return;
            this.SelectSecurities(new Security[1] { security });
        });
        this.SecuritiesSelected.SecurityDoubleClick += (Action<Security>)(security =>
        {
            if (security == null)
                return;
            this.UnselectSecurities(new Security[1] { security });
        });
    }

    private void SecuritiesWindowEx_OnLoaded(object sender, RoutedEventArgs e)
    {
        if (this.IsLookup)
        {
            this.SecuritiesAll.Title = LocalizedStrings.Found;
            this.SecuritiesSelected.Title = LocalizedStrings.Selected;
        }
        else
            this.LookupPanel.Visibility = Visibility.Collapsed;
    }

    private void SelectSecurities(Security[] securities)
    {
        this.SecuritiesSelected.Securities.AddRange((IEnumerable<Security>)securities);
        if (!this.AllowDuplicates)
            CollectionHelper.AddRange<Security>((ICollection<Security>)this.SecuritiesAll.ExcludeSecurities, (IEnumerable<Security>)securities);
        this.EnableOk();
    }

    private void UnselectSecurities(Security[] securities)
    {
        this.SecuritiesSelected.Securities.RemoveRange((IEnumerable<Security>)securities);
        if (!this.AllowDuplicates)
            CollectionHelper.RemoveRange<Security>((ICollection<Security>)this.SecuritiesAll.ExcludeSecurities, (IEnumerable<Security>)securities);
        this.EnableOk();
    }

    private void ExecutedSelectSecurity(object sender, ExecutedRoutedEventArgs e)
    {
        IList<Security> selectedSecurities = this.SecuritiesAll.SelectedSecurities;
        int index = 0;
        Security[] securities = new Security[selectedSecurities.Count];
        foreach (Security security in (IEnumerable<Security>)selectedSecurities)
        {
            securities[index] = security;
            ++index;
        }
        this.SelectSecurities(securities);
    }

    private void CanExecuteSelectSecurity(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = this.SecuritiesAll.SelectedSecurities.Any<Security>();
    }

    private void ExecutedUnselectSecurity(object sender, ExecutedRoutedEventArgs e)
    {
        IList<Security> selectedSecurities = this.SecuritiesSelected.SelectedSecurities;
        int index = 0;
        Security[] securities = new Security[selectedSecurities.Count];
        foreach (Security security in (IEnumerable<Security>)selectedSecurities)
        {
            securities[index] = security;
            ++index;
        }
        this.UnselectSecurities(securities);
    }

    private void CanExecuteUnselectSecurity(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = this.SecuritiesSelected.SelectedSecurities.Any<Security>();
    }

    private void EnableOk() => this.Ok.IsEnabled = true;

    private void LookupPanel_OnLookup(Security filter)
    {
        this.SecuritiesAll.Securities.Clear();
        new LookupSecuritiesCommand(filter.ToLookupMessage()).Process((object)this);
    }


}
