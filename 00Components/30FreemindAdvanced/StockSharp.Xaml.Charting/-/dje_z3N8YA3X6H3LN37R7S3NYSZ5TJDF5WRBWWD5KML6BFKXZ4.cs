// Decompiled with JetBrains decompiler
// Type: -.dje_z3N8YA3X6H3LN37R7S3NYSZ5TJDF5WRBWWD5KML6BFKXZ44S823J6BQY3A87QVC3HBBYCP4RA_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using Ecng.Xaml;
using StockSharp.BusinessEntities;
using StockSharp.Charting;
using StockSharp.Messages;
using StockSharp.Xaml.PropertyGrid;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Markup;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal sealed class dje_z3N8YA3X6H3LN37R7S3NYSZ5TJDF5WRBWWD5KML6BFKXZ44S823J6BQY3A87QVC3HBBYCP4RA_ejd : 
  ItemsControl,
  IComponentConnector,
  IStyleConnector
{
  
  private readonly Popup \u0023\u003DzKkX3SOgS26uy;
  
  private readonly CandleDataTypeEdit \u0023\u003DzXTZs02hFk44D3c5vJw\u003D\u003D;
  
  internal dje_z3N8YA3X6H3LN37R7S3NYSZ5TJDF5WRBWWD5KML6BFKXZ44S823J6BQY3A87QVC3HBBYCP4RA_ejd \u0023\u003Dzv4BS1WQ\u003D;
  
  private bool \u0023\u003DzQGCmQMjHdLKS;

  public dje_z3N8YA3X6H3LN37R7S3NYSZ5TJDF5WRBWWD5KML6BFKXZ44S823J6BQY3A87QVC3HBBYCP4RA_ejd()
  {
    this.InitializeComponent();
    this.\u0023\u003DzKkX3SOgS26uy = (Popup) this.Resources[(object) XXX.SSS(-539329842)];
    this.\u0023\u003DzXTZs02hFk44D3c5vJw\u003D\u003D = this.\u0023\u003DzKkX3SOgS26uy.FindLogicalChild<CandleDataTypeEdit>();
  }

  private void \u0023\u003DzFEhTVIBjIbf8(object _param1, RoutedEventArgs _param2)
  {
    Hyperlink hyperlink = (Hyperlink) _param1;
    if (!(hyperlink.DataContext is ParentVM dataContext) || !(dataContext.ChartElement is IChartCandleElement) || !dataContext.Pane.GroupChart.IsInteracted)
      return;
    this.\u0023\u003DzKkX3SOgS26uy.IsOpen = false;
    Subscription subscription = dataContext.\u0023\u003DzZ0VU1NABDfD8(dataContext.ChartElement);
    if (subscription == null || !subscription.SecurityId.HasValue)
      return;
    this.\u0023\u003DzKkX3SOgS26uy.DataContext = (object) dataContext;
    this.\u0023\u003DzXTZs02hFk44D3c5vJw\u003D\u003D.DataType = subscription.DataType;
    this.\u0023\u003DzKkX3SOgS26uy.PlacementTarget = (UIElement) hyperlink.Parent;
    this.\u0023\u003DzKkX3SOgS26uy.IsOpen = true;
  }

  private void \u0023\u003DzOdlcCH3I2dRH(object _param1, RoutedEventArgs _param2)
  {
    if (!((_param1 is FrameworkElement frameworkElement ? frameworkElement.DataContext : (object) null) is ParentVM dataContext))
      return;
    this.\u0023\u003DzKkX3SOgS26uy.IsOpen = false;
    Subscription subscription1 = dataContext.\u0023\u003DzZ0VU1NABDfD8(dataContext.ChartElement);
    if (subscription1 == null)
      return;
    DataType dataType = this.\u0023\u003DzXTZs02hFk44D3c5vJw\u003D\u003D.DataType;
    if (dataType != null && dataType.MessageType == subscription1.DataType.MessageType)
    {
      object obj = dataType.Arg;
      if ((obj != null ? (obj.Equals(subscription1.DataType.Arg) ? 1 : 0) : 0) != 0)
        return;
    }
    Subscription subscription2 = new Subscription((ISubscriptionMessage) new MarketDataMessage()
    {
      DataType2 = dataType,
      IsSubscribe = true
    }, (SecurityMessage) subscription1.MarketData);
    dataContext.Pane.ParentViewModel.\u0023\u003Dzld7tWxZuooQ2UzOmtQ\u003D\u003D((IChartElement) dataContext.ChartElement, subscription2);
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  public void InitializeComponent()
  {
    if (this.\u0023\u003DzQGCmQMjHdLKS)
      return;
    this.\u0023\u003DzQGCmQMjHdLKS = true;
    Application.LoadComponent((object) this, new Uri(XXX.SSS(-539329825), UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.\u0023\u003DzuNHLeGEnMjz9FDFZ6wymuXfyw_Iz(int _param1, object _param2)
  {
    if (_param1 != 1)
    {
      if (_param1 == 2)
        ((ButtonBase) _param2).Click += new RoutedEventHandler(this.\u0023\u003DzOdlcCH3I2dRH);
      else
        this.\u0023\u003DzQGCmQMjHdLKS = true;
    }
    else
      this.\u0023\u003Dzv4BS1WQ\u003D = (dje_z3N8YA3X6H3LN37R7S3NYSZ5TJDF5WRBWWD5KML6BFKXZ44S823J6BQY3A87QVC3HBBYCP4RA_ejd) _param2;
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IStyleConnector.\u0023\u003DzFWJS0zuHgjFG6cC2R4YUzRFQ6WaN(int _param1, object _param2)
  {
    if (_param1 != 3)
      return;
    ((Hyperlink) _param2).Click += new RoutedEventHandler(this.\u0023\u003DzFEhTVIBjIbf8);
  }
}
