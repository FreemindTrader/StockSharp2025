// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.SecretEdit
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using DevExpress.Xpf.Editors;
using Ecng.Common;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Ecng.Xaml
{
  /// <summary>
  /// </summary>
  /// <summary>SecretEdit</summary>
  public class SecretEdit : UserControl, IComponentConnector
  {
    /// <summary>
    /// <see cref="T:System.Windows.DependencyProperty" /> для <see cref="P:Ecng.Xaml.SecretEdit.Secret" />.
    ///     </summary>
    public static readonly DependencyProperty SecretProperty = DependencyProperty.Register(nameof(2127279155), typeof (SecureString), typeof (SecretEdit), new PropertyMetadata((object) null, new PropertyChangedCallback((object) SecretEdit.SomeShit.ShitMethod02, __methodptr(\u0023\u003DzBoIYm_iuKE6y41tAfnVXzRI\u003D))));
    
    private bool \u0023\u003DzOG9csn\u0024iE\u00244E;
    
    internal PasswordBoxEdit \u0023\u003Dz6uGL2N305JQS;
    
    private bool \u0023\u003DzwPHzQV2Vg5J\u0024;

    /// <summary>
    /// </summary>
    public SecretEdit()
    {
      this.InitializeComponent();
    }

    /// <summary>Секрет.</summary>
    public SecureString Secret
    {
      get
      {
        return (SecureString) this.GetValue(SecretEdit.SecretProperty);
      }
      set
      {
        this.SetValue(SecretEdit.SecretProperty, (object) value);
      }
    }

    private void \u0023\u003DzqkUegdsMMdHjLHK5kQ\u003D\u003D(
      object _param1,
      EditValueChangedEventArgs _param2)
    {
      if (this.\u0023\u003Dz6uGL2N305JQS.get_Password() == nameof(2127279136))
        return;
      try
      {
        this.\u0023\u003DzOG9csn\u0024iE\u00244E = true;
        this.Secret = this.\u0023\u003Dz6uGL2N305JQS.get_Password().Secure();
      }
      finally
      {
        this.\u0023\u003DzOG9csn\u0024iE\u00244E = false;
      }
    }

    private void \u0023\u003DzaoIF6eQ\u003D(SecureString _param1)
    {
      if (this.\u0023\u003DzOG9csn\u0024iE\u00244E)
        return;
      this.\u0023\u003Dz6uGL2N305JQS.set_Password(!_param1.IsEmpty() ? nameof(2127279136) : (string) null);
    }

    /// <summary>InitializeComponent</summary>
    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "6.0.8.0")]
    public void InitializeComponent()
    {
      if (this.\u0023\u003DzwPHzQV2Vg5J\u0024)
        return;
      this.\u0023\u003DzwPHzQV2Vg5J\u0024 = true;
      Application.LoadComponent((object) this, new Uri(nameof(2127279121), UriKind.Relative));
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [GeneratedCode("PresentationBuildTasks", "6.0.8.0")]
    [DebuggerNonUserCode]
    void IComponentConnector.\u0023\u003DzwjqCwJRp5nvBkvFFuDtdoCHyTx2y(
      int _param1,
      object _param2)
    {
      if (_param1 == 1)
      {
        this.\u0023\u003Dz6uGL2N305JQS = (PasswordBoxEdit) _param2;
        // ISSUE: method pointer
        ((BaseEdit) this.\u0023\u003Dz6uGL2N305JQS).add_EditValueChanged(new EditValueChangedEventHandler((object) this, __methodptr(\u0023\u003DzqkUegdsMMdHjLHK5kQ\u003D\u003D)));
      }
      else
        this.\u0023\u003DzwPHzQV2Vg5J\u0024 = true;
    }

    [Serializable]
    private sealed class SomeShit
    {
      public static readonly SecretEdit.SomeShit ShitMethod02 = new SecretEdit.SomeShit();

      internal void \u0023\u003DzBoIYm_iuKE6y41tAfnVXzRI\u003D(
        DependencyObject _param1,
        DependencyPropertyChangedEventArgs _param2)
      {
        ((SecretEdit) _param1).\u0023\u003DzaoIF6eQ\u003D((SecureString) ((DependencyPropertyChangedEventArgs) ref _param2).get_NewValue());
      }
    }
  }
}
