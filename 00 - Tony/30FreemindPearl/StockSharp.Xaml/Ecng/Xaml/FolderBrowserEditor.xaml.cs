// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.FolderBrowserEditor
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using DevExpress.Xpf.Dialogs;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;
using Ecng.Common;
using Ecng.ComponentModel;
using Microsoft.Win32;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;

namespace Ecng.Xaml
{
  /// <summary>
  /// </summary>
  /// <summary>FolderBrowserEditor</summary>
  public class FolderBrowserEditor : ButtonEditSettings, IFolderBrowserEditor, IComponentConnector
  {
    
    internal ButtonInfo \u0023\u003DzP24SvUhIwSJb;
    
    internal ButtonInfo \u0023\u003Dzu4UOJyl2xAFe;
    
    private bool \u0023\u003DzwPHzQV2Vg5J\u0024;

    /// <summary>
    /// </summary>
    public FolderBrowserEditor()
    {
      base.\u002Ector();
      this.InitializeComponent();
    }

    /// <inheritdoc />
    protected virtual void AssignToEditCore(IBaseEdit edit)
    {
      ButtonEdit buttonEdit = edit as ButtonEdit;
      if (buttonEdit != null)
        ValidationHelper.SetBaseEdit((BaseEditSettings) this, (BaseEdit) buttonEdit);
      base.AssignToEditCore(edit);
    }

    private void \u0023\u003DzNky\u0024VOp0FiGGoAbyTQ\u003D\u003D(
      object _param1,
      RoutedEventArgs _param2)
    {
      BaseEdit ownerEdit = BaseEdit.GetOwnerEdit((DependencyObject) _param1);
      if (ownerEdit == null || ownerEdit.get_IsReadOnly())
        return;
      DXFolderBrowserDialog folderBrowserDialog = new DXFolderBrowserDialog();
      string editValue = (string) ownerEdit.get_EditValue();
      if (!editValue.IsEmpty())
        folderBrowserDialog.set_SelectedPath(editValue);
      if (!((CommonDialog) folderBrowserDialog).ShowModal((DependencyObject) _param1))
        return;
      ownerEdit.set_EditValue((object) folderBrowserDialog.get_SelectedPath());
    }

    private void \u0023\u003DzLO_mz8ZK8hySNqIqZw\u003D\u003D(
      object _param1,
      RoutedEventArgs _param2)
    {
      BaseEdit ownerEdit = BaseEdit.GetOwnerEdit((DependencyObject) _param1);
      if (ownerEdit == null || ownerEdit.get_IsReadOnly())
        return;
      ownerEdit.set_EditValue((object) null);
    }

    /// <summary>InitializeComponent</summary>
    [GeneratedCode("PresentationBuildTasks", "6.0.8.0")]
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this.\u0023\u003DzwPHzQV2Vg5J\u0024)
        return;
      this.\u0023\u003DzwPHzQV2Vg5J\u0024 = true;
      Application.LoadComponent((object) this, new Uri(nameof(2127280429), UriKind.Relative));
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [GeneratedCode("PresentationBuildTasks", "6.0.8.0")]
    [DebuggerNonUserCode]
    void IComponentConnector.\u0023\u003DzwjqCwJRp5nvBkvFFuDtdoCHyTx2y(
      int _param1,
      object _param2)
    {
      if (_param1 != 1)
      {
        if (_param1 == 2)
        {
          this.\u0023\u003Dzu4UOJyl2xAFe = (ButtonInfo) _param2;
          ((CommandButtonInfo) this.\u0023\u003Dzu4UOJyl2xAFe).add_Click(new RoutedEventHandler(this.\u0023\u003DzLO_mz8ZK8hySNqIqZw\u003D\u003D));
        }
        else
          this.\u0023\u003DzwPHzQV2Vg5J\u0024 = true;
      }
      else
      {
        this.\u0023\u003DzP24SvUhIwSJb = (ButtonInfo) _param2;
        ((CommandButtonInfo) this.\u0023\u003DzP24SvUhIwSJb).add_Click(new RoutedEventHandler(this.\u0023\u003DzNky\u0024VOp0FiGGoAbyTQ\u003D\u003D));
      }
    }
  }
}
