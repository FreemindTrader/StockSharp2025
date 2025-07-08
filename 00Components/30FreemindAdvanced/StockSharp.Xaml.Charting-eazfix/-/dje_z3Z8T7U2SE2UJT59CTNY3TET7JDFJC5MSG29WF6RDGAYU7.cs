// Decompiled with JetBrains decompiler
// Type: -.dje_z3Z8T7U2SE2UJT59CTNY3TET7JDFJC5MSG29WF6RDGAYU7XFY5TNXTAC5YX2796KWCRF36XMM_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

#nullable disable
namespace \u002D;

internal sealed class dje_z3Z8T7U2SE2UJT59CTNY3TET7JDFJC5MSG29WF6RDGAYU7XFY5TNXTAC5YX2796KWCRF36XMM_ejd : 
  dje_zN29EKU2DQXHTNKQ8X5JHMLEL2WYLBY8R3YWYA24CH8SZCCZ_ejd,
  IComponentConnector
{
  
  private readonly ControlTemplate \u0023\u003Dzh\u0024ZLVlTJ\u0024Noh;
  
  public static readonly DependencyProperty \u0023\u003DztwQ4ieQ9dTof = DependencyProperty.Register(nameof (InPlaceTooltip), typeof (bool), typeof (dje_z3Z8T7U2SE2UJT59CTNY3TET7JDFJC5MSG29WF6RDGAYU7XFY5TNXTAC5YX2796KWCRF36XMM_ejd), new PropertyMetadata((object) true, new PropertyChangedCallback(dje_z3Z8T7U2SE2UJT59CTNY3TET7JDFJC5MSG29WF6RDGAYU7XFY5TNXTAC5YX2796KWCRF36XMM_ejd.\u0023\u003Dzuc47VriCXw3z)));
  
  private bool \u0023\u003DzQGCmQMjHdLKS;

  public dje_z3Z8T7U2SE2UJT59CTNY3TET7JDFJC5MSG29WF6RDGAYU7XFY5TNXTAC5YX2796KWCRF36XMM_ejd()
  {
    this.InitializeComponent();
    this.\u0023\u003Dzh\u0024ZLVlTJ\u0024Noh = this.TooltipLabelTemplate;
    this.UseInterpolation = false;
  }

  private static void \u0023\u003Dzuc47VriCXw3z(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    ((dje_z3Z8T7U2SE2UJT59CTNY3TET7JDFJC5MSG29WF6RDGAYU7XFY5TNXTAC5YX2796KWCRF36XMM_ejd) _param0).\u0023\u003DzvSVAzF\u0024\u0024QePr((bool) _param1.NewValue);
  }

  public bool InPlaceTooltip
  {
    get
    {
      return (bool) this.GetValue(dje_z3Z8T7U2SE2UJT59CTNY3TET7JDFJC5MSG29WF6RDGAYU7XFY5TNXTAC5YX2796KWCRF36XMM_ejd.\u0023\u003DztwQ4ieQ9dTof);
    }
    set
    {
      this.SetValue(dje_z3Z8T7U2SE2UJT59CTNY3TET7JDFJC5MSG29WF6RDGAYU7XFY5TNXTAC5YX2796KWCRF36XMM_ejd.\u0023\u003DztwQ4ieQ9dTof, (object) value);
    }
  }

  private void \u0023\u003DzvSVAzF\u0024\u0024QePr(bool _param1)
  {
    this.TooltipLabelTemplate = _param1 ? this.\u0023\u003Dzh\u0024ZLVlTJ\u0024Noh : (ControlTemplate) null;
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  public void InitializeComponent()
  {
    if (this.\u0023\u003DzQGCmQMjHdLKS)
      return;
    this.\u0023\u003DzQGCmQMjHdLKS = true;
    Application.LoadComponent((object) this, new Uri("/StockSharp.Xaml.Charting;V5.0.0;component/ultrachart/ultrachartcursormodifier.xaml", UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.\u0023\u003DzuNHLeGEnMjz9FDFZ6wymuXfyw_Iz(int _param1, object _param2)
  {
    this.\u0023\u003DzQGCmQMjHdLKS = true;
  }
}
