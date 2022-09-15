// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.LoadingAnimation
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Ecng.Xaml
{
  /// <summary>
  /// </summary>
  /// <summary>LoadingAnimation</summary>
  public class LoadingAnimation : UserControl, IComponentConnector
  {
    /// <summary>
    /// </summary>
    public static readonly DependencyProperty IsBusyProperty = DependencyProperty.Register(nameof(2127277652), typeof (bool), typeof (LoadingAnimation), new PropertyMetadata((object) false));
    
    internal LoadingAnimation \u0023\u003DzFRPJlsE\u003D;
    
    internal Border \u0023\u003Dzjfe_TFs\u003D;
    
    internal Border \u0023\u003Dzis0xCdo\u003D;
    
    internal Border \u0023\u003DzYdZTqD4\u003D;
    
    internal Border \u0023\u003Dz7QiEDEI\u003D;
    
    internal Border \u0023\u003DzXNgQW54\u003D;
    
    internal TextBlock \u0023\u003DzysRX7UdNV_WR;
    
    private bool \u0023\u003DzwPHzQV2Vg5J\u0024;

    /// <summary>
    /// </summary>
    public LoadingAnimation()
    {
      this.InitializeComponent();
    }

    /// <summary>
    /// </summary>
    public bool IsBusy
    {
      get
      {
        return (bool) this.GetValue(LoadingAnimation.IsBusyProperty);
      }
      set
      {
        this.SetValue(LoadingAnimation.IsBusyProperty, (object) value);
      }
    }

    /// <summary>
    /// </summary>
    public string AnimationText
    {
      get
      {
        return this.\u0023\u003DzysRX7UdNV_WR.Text;
      }
      set
      {
        this.\u0023\u003DzysRX7UdNV_WR.Text = value;
      }
    }

    /// <summary>InitializeComponent</summary>
    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "6.0.8.0")]
    public void InitializeComponent()
    {
      if (this.\u0023\u003DzwPHzQV2Vg5J\u0024)
        return;
      this.\u0023\u003DzwPHzQV2Vg5J\u0024 = true;
      Application.LoadComponent((object) this, new Uri(nameof(2127279897), UriKind.Relative));
    }

    [GeneratedCode("PresentationBuildTasks", "6.0.8.0")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DebuggerNonUserCode]
    void IComponentConnector.\u0023\u003DzwjqCwJRp5nvBkvFFuDtdoCHyTx2y(
      int _param1,
      object _param2)
    {
      switch (_param1)
      {
        case 1:
          this.\u0023\u003DzFRPJlsE\u003D = (LoadingAnimation) _param2;
          break;
        case 2:
          this.\u0023\u003Dzjfe_TFs\u003D = (Border) _param2;
          break;
        case 3:
          this.\u0023\u003Dzis0xCdo\u003D = (Border) _param2;
          break;
        case 4:
          this.\u0023\u003DzYdZTqD4\u003D = (Border) _param2;
          break;
        case 5:
          this.\u0023\u003Dz7QiEDEI\u003D = (Border) _param2;
          break;
        case 6:
          this.\u0023\u003DzXNgQW54\u003D = (Border) _param2;
          break;
        case 7:
          this.\u0023\u003DzysRX7UdNV_WR = (TextBlock) _param2;
          break;
        default:
          this.\u0023\u003DzwPHzQV2Vg5J\u0024 = true;
          break;
      }
    }
  }
}
