// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.FolderBrowserPicker
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Dialogs;
using Ecng.Common;
using Microsoft.Win32;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

namespace Ecng.Xaml
{
  /// <summary>Визуальный редактор для выбора директории.</summary>
  /// <summary>FolderBrowserPicker</summary>
  public class FolderBrowserPicker : UserControl, IComponentConnector
  {
    /// <summary>
    /// <see cref="T:System.Windows.DependencyProperty" /> для <see cref="P:Ecng.Xaml.FolderBrowserPicker.Folder" />.
    ///     </summary>
    public static readonly DependencyProperty FolderProperty = DependencyProperty.Register(nameof(2127280620), typeof (string), typeof (FolderBrowserPicker), new PropertyMetadata((object) null));
    
    internal SimpleButton \u0023\u003DzbGun6xxyfzWB;
    
    internal TextBox \u0023\u003DzEFicevc\u003D;
    
    private bool \u0023\u003DzwPHzQV2Vg5J\u0024;

    /// <summary>
    /// Создать <see cref="T:Ecng.Xaml.FolderBrowserPicker" />.
    /// </summary>
    public FolderBrowserPicker()
    {
      this.InitializeComponent();
    }

    /// <summary>Директория.</summary>
    public string Folder
    {
      get
      {
        return (string) this.GetValue(FolderBrowserPicker.FolderProperty);
      }
      set
      {
        this.SetValue(FolderBrowserPicker.FolderProperty, (object) value);
      }
    }

    /// <summary>
    /// Событие изменения <see cref="P:Ecng.Xaml.FolderBrowserPicker.Folder" />.
    /// </summary>
    public event Action<string> FolderChanged;

    private void \u0023\u003DzAvv_D0aBYdSm(object _param1, RoutedEventArgs _param2)
    {
      DXFolderBrowserDialog folderBrowserDialog = new DXFolderBrowserDialog();
      if (!this.\u0023\u003DzEFicevc\u003D.Text.IsEmpty())
        folderBrowserDialog.set_SelectedPath(this.\u0023\u003DzEFicevc\u003D.Text);
      if (!((CommonDialog) folderBrowserDialog).ShowModal((DependencyObject) _param1))
        return;
      this.\u0023\u003DzEFicevc\u003D.Text = folderBrowserDialog.get_SelectedPath();
      Action<string> z8bphLv4 = this.\u0023\u003Dz8bphLv4\u003D;
      if (z8bphLv4 == null)
        return;
      z8bphLv4(folderBrowserDialog.get_SelectedPath());
    }

    private void \u0023\u003DzaHLBBtb4rKda(object _param1, TextChangedEventArgs _param2)
    {
      Action<string> z8bphLv4 = this.\u0023\u003Dz8bphLv4\u003D;
      if (z8bphLv4 == null)
        return;
      z8bphLv4(this.\u0023\u003DzEFicevc\u003D.Text);
    }

    /// <summary>InitializeComponent</summary>
    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "6.0.8.0")]
    public void InitializeComponent()
    {
      if (this.\u0023\u003DzwPHzQV2Vg5J\u0024)
        return;
      this.\u0023\u003DzwPHzQV2Vg5J\u0024 = true;
      Application.LoadComponent((object) this, new Uri(nameof(2127280601), UriKind.Relative));
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "6.0.8.0")]
    void IComponentConnector.\u0023\u003DzwjqCwJRp5nvBkvFFuDtdoCHyTx2y(
      int _param1,
      object _param2)
    {
      if (_param1 != 1)
      {
        if (_param1 == 2)
        {
          this.\u0023\u003DzEFicevc\u003D = (TextBox) _param2;
          this.\u0023\u003DzEFicevc\u003D.TextChanged += new TextChangedEventHandler(this.\u0023\u003DzaHLBBtb4rKda);
        }
        else
          this.\u0023\u003DzwPHzQV2Vg5J\u0024 = true;
      }
      else
      {
        this.\u0023\u003DzbGun6xxyfzWB = (SimpleButton) _param2;
        ((ButtonBase) this.\u0023\u003DzbGun6xxyfzWB).Click += new RoutedEventHandler(this.\u0023\u003DzAvv_D0aBYdSm);
      }
    }
  }
}
