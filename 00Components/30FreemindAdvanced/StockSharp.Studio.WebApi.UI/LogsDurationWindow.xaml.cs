// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.LogsDurationWindow
// Assembly: StockSharp.Studio.WebApi.UI, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5447883D-4177-4B33-881A-41D29A366FB7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.WebApi.UI.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using SmartFormat;
using StockSharp.Localization;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Markup;

#nullable disable
namespace StockSharp.Studio.Controls;

public partial class LogsDurationWindow : ThemedWindow, IComponentConnector
{
  internal ComboBoxEdit ComboBoxEdit;
  private bool _contentLoaded;

  public TimeSpan Duration => (TimeSpan) this.ComboBoxEdit.EditValue;

  public LogsDurationWindow()
  {
    this.InitializeComponent();
    CultureInfo ci = LocalizedStrings.CurrentCulture;
    Tuple<string, TimeSpan>[] source = new Tuple<string, TimeSpan>[5]
    {
      Tuple.Create<string, TimeSpan>(LocalizedStrings.AllPeriod, TimeSpan.MaxValue),
      Tuple.Create<string, TimeSpan>(daysstr(1), TimeSpan.FromDays(1.0)),
      Tuple.Create<string, TimeSpan>(daysstr(3), TimeSpan.FromDays(3.0)),
      Tuple.Create<string, TimeSpan>(daysstr(7), TimeSpan.FromDays(7.0)),
      Tuple.Create<string, TimeSpan>(daysstr(30), TimeSpan.FromDays(30.0))
    };
    Tuple<string, TimeSpan> tuple = ((IEnumerable<Tuple<string, TimeSpan>>) source).First<Tuple<string, TimeSpan>>();
    this.ComboBoxEdit.DisplayMember = "Item1";
    this.ComboBoxEdit.ValueMember = "Item2";
    this.ComboBoxEdit.ItemsSource = (object) source;
    this.ComboBoxEdit.SelectedItem = (object) tuple;

    string daysstr(int days)
    {
      return Smart.Format((IFormatProvider) ci, LocalizedStrings.DaysParamSmartPlural, new object[1]
      {
        (object) days
      });
    }
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    Application.LoadComponent((object) this, new Uri("/StockSharp.Studio.WebApi.UI;V5.0.0;component/logsdurationwindow.xaml", UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.Connect(int connectionId, object target)
  {
    if (connectionId == 1)
      this.ComboBoxEdit = (ComboBoxEdit) target;
    else
      this._contentLoaded = true;
  }
}
