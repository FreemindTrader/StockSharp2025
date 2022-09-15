// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.FileBrowserPicker
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using \u002D;
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
using System.Windows.Data;
using System.Windows.Markup;

namespace Ecng.Xaml
{
  /// <summary>Визуальный редактор для выбора файла.</summary>
  /// <summary>FileBrowserPicker</summary>
  public class FileBrowserPicker : UserControl, IComponentConnector
  {
    /// <summary>
    /// <see cref="T:System.Windows.DependencyProperty" /> for <see cref="P:Ecng.Xaml.FileBrowserPicker.DefaultExt" />.
    ///     </summary>
    public static readonly DependencyProperty DefaultExtProperty = DependencyProperty.Register(nameof(2127280280), typeof (string), typeof (FileBrowserPicker), new PropertyMetadata((object) null));
    /// <summary>
    /// <see cref="T:System.Windows.DependencyProperty" /> for <see cref="P:Ecng.Xaml.FileBrowserPicker.Filter" />.
    ///     </summary>
    public static readonly DependencyProperty FilterProperty = DependencyProperty.Register(nameof(2127280265), typeof (string), typeof (FileBrowserPicker), new PropertyMetadata((object) null));
    /// <summary>
    /// <see cref="T:System.Windows.DependencyProperty" /> для <see cref="P:Ecng.Xaml.FileBrowserPicker.File" />.
    ///     </summary>
    public static readonly DependencyProperty FileProperty = DependencyProperty.Register(nameof(2127280262), typeof (string), typeof (FileBrowserPicker), new PropertyMetadata((object) null));
    /// <summary>
    /// <see cref="T:System.Windows.DependencyProperty" /> for <see cref="P:Ecng.Xaml.FileBrowserPicker.IsSaving" />.
    ///     </summary>
    public static readonly DependencyProperty IsSavingProperty = DependencyProperty.Register(nameof(2127280497), typeof (bool), typeof (FileBrowserPicker), new PropertyMetadata((object) false, new PropertyChangedCallback((object) FileBrowserPicker.SomeShit.ShitMethod02, __methodptr(\u0023\u003DzEXll0ASFA9\u0024B9l0ZIKT0PjY\u003D))));
    
    internal SimpleButton \u0023\u003DzdcTukK0\u003D;
    
    internal TextBox \u0023\u003DzyzRNK54\u003D;
    
    private bool \u0023\u003DzwPHzQV2Vg5J\u0024;

    /// <summary>
    /// Создать <see cref="T:Ecng.Xaml.FileBrowserPicker" />.
    /// </summary>
    public FileBrowserPicker()
    {
      this.InitializeComponent();
    }

    /// <summary>
    /// </summary>
    public string DefaultExt
    {
      get
      {
        return (string) this.GetValue(FileBrowserPicker.DefaultExtProperty);
      }
      set
      {
        this.SetValue(FileBrowserPicker.DefaultExtProperty, (object) value);
      }
    }

    /// <summary>
    /// </summary>
    public string Filter
    {
      get
      {
        return (string) this.GetValue(FileBrowserPicker.FilterProperty);
      }
      set
      {
        this.SetValue(FileBrowserPicker.FilterProperty, (object) value);
      }
    }

    /// <summary>Директория.</summary>
    public string File
    {
      get
      {
        return (string) this.GetValue(FileBrowserPicker.FileProperty);
      }
      set
      {
        this.SetValue(FileBrowserPicker.FileProperty, (object) value);
      }
    }

    /// <summary>
    /// </summary>
    public bool IsSaving
    {
      get
      {
        return (bool) this.GetValue(FileBrowserPicker.IsSavingProperty);
      }
      set
      {
        this.SetValue(FileBrowserPicker.IsSavingProperty, (object) value);
      }
    }

    /// <summary>
    /// Событие изменения <see cref="P:Ecng.Xaml.FileBrowserPicker.File" />.
    /// </summary>
    public event Action<string> FileChanged;

    private void \u0023\u003DzztAtwHFWYZu7(object _param1, RoutedEventArgs _param2)
    {
      DXSaveFileDialog dxSaveFileDialog;
      if (!this.IsSaving)
      {
        DXOpenFileDialog dxOpenFileDialog = new DXOpenFileDialog();
        ((DXFileDialog) dxOpenFileDialog).set_CheckFileExists(true);
        dxSaveFileDialog = (DXSaveFileDialog) dxOpenFileDialog;
      }
      else
        dxSaveFileDialog = new DXSaveFileDialog();
      DXFileDialog dxFileDialog = (DXFileDialog) dxSaveFileDialog;
      dxFileDialog.set_RestoreDirectory(true);
      if (!this.Filter.IsEmpty())
        dxFileDialog.set_Filter(this.Filter);
      if (!this.DefaultExt.IsEmpty())
        dxFileDialog.set_DefaultExt(this.DefaultExt);
      if (!this.File.IsEmpty())
        dxFileDialog.set_FileName(this.File);
      if (!((CommonDialog) dxFileDialog).ShowModal((DependencyObject) _param1))
        return;
      this.File = dxFileDialog.get_FileName();
      Action<string> zYmRgzHu = this.\u0023\u003DzYmRGzHU\u003D;
      if (zYmRgzHu == null)
        return;
      zYmRgzHu(dxFileDialog.get_FileName());
    }

    private void \u0023\u003DzAABMeJzP5JVH(object _param1, TextChangedEventArgs _param2)
    {
      Action<string> zYmRgzHu = this.\u0023\u003DzYmRGzHU\u003D;
      if (zYmRgzHu == null)
        return;
      zYmRgzHu(this.\u0023\u003DzyzRNK54\u003D.Text);
    }

    /// <summary>InitializeComponent</summary>
    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "6.0.8.0")]
    public void InitializeComponent()
    {
      if (this.\u0023\u003DzwPHzQV2Vg5J\u0024)
        return;
      this.\u0023\u003DzwPHzQV2Vg5J\u0024 = true;
      Application.LoadComponent((object) this, new Uri(nameof(2127280480), UriKind.Relative));
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
          this.\u0023\u003DzyzRNK54\u003D = (TextBox) _param2;
          this.\u0023\u003DzyzRNK54\u003D.TextChanged += new TextChangedEventHandler(this.\u0023\u003DzAABMeJzP5JVH);
        }
        else
          this.\u0023\u003DzwPHzQV2Vg5J\u0024 = true;
      }
      else
      {
        this.\u0023\u003DzdcTukK0\u003D = (SimpleButton) _param2;
        ((ButtonBase) this.\u0023\u003DzdcTukK0\u003D).Click += new RoutedEventHandler(this.\u0023\u003DzztAtwHFWYZu7);
      }
    }

    [Serializable]
    private sealed class SomeShit
    {
      public static readonly FileBrowserPicker.SomeShit ShitMethod02 = new FileBrowserPicker.SomeShit();

      internal void \u0023\u003DzEXll0ASFA9\u0024B9l0ZIKT0PjY\u003D(
        DependencyObject _param1,
        DependencyPropertyChangedEventArgs _param2)
      {
        ((dje_z7VFZ5P5XG4ZCAE86LAPYNLEGD25A_ejd) BindingOperations.GetBinding((DependencyObject) ((FileBrowserPicker) _param1).\u0023\u003DzyzRNK54\u003D, TextBox.TextProperty).ValidationRules[0]).IsActive = !(bool) ((DependencyPropertyChangedEventArgs) ref _param2).get_NewValue();
      }
    }
  }
}
