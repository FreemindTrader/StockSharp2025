// Decompiled with JetBrains decompiler
// Type: #=zjIfS4CbXGFDPWmVOPAZGmqCb1u9vHqE9pa_ddXk=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

#nullable disable
internal abstract class \u0023\u003DzjIfS4CbXGFDPWmVOPAZGmqCb1u9vHqE9pa_ddXk\u003D : 
  FrameworkElement,
  \u0023\u003DzFphlrC3tGBVP73muJW4N1sp2o\u0024hCxXn5DXylgtbrM25x,
  IReceiveMouseEvents 
{
  
  private Canvas \u0023\u003DzZ_bY0tT8hlGH;
  
  private IAnnotation \u0023\u003DzJ3FH5kAFy7GH;
  
  private IServiceContainer _serviceContainer;
  
  private string \u0023\u003DzIOF\u0024pM4fvIzk4nVUyHULhOk\u003D;

  protected \u0023\u003DzjIfS4CbXGFDPWmVOPAZGmqCb1u9vHqE9pa_ddXk\u003D(
    IAnnotation _param1)
  {
    this.\u0023\u003DzJ3FH5kAFy7GH = _param1;
    Panel.SetZIndex((UIElement) this, 100);
  }

  public IServiceContainer \u0023\u003Dzu\u0024P3XgkcE7BC()
  {
    return this._serviceContainer;
  }

  public void \u0023\u003DzrEoWi5uPS0Yz(
    IServiceContainer _param1)
  {
    if (this._serviceContainer != null)
      this._serviceContainer.\u0023\u003Dz2VqWonc\u003D<\u0023\u003Dz\u0024xWvhP6es1QcQRB94rX4cNVVgfiVZTLWRhl\u00244Uw\u003D>().\u0023\u003DzfttffOE\u003D((IReceiveMouseEvents ) this);
    this._serviceContainer = _param1;
    if (this._serviceContainer == null)
      return;
    this._serviceContainer.\u0023\u003Dz2VqWonc\u003D<\u0023\u003Dz\u0024xWvhP6es1QcQRB94rX4cNVVgfiVZTLWRhl\u00244Uw\u003D>().\u0023\u003DzZcbqdpE\u003D((\u0023\u003DzQ4iRj1YTApc8D349VbLPOXcxSYN1XwlnLQBsgQeCUZnV) this.\u0023\u003DzJ3FH5kAFy7GH, (IReceiveMouseEvents ) this);
  }

  public Canvas \u0023\u003DzVuf430fCLR2l() => this.\u0023\u003DzZ_bY0tT8hlGH;

  public void \u0023\u003Dzbw4WNWtere7d(Canvas _param1)
  {
    if (this.\u0023\u003DzZ_bY0tT8hlGH != null)
      this.\u0023\u003DzcNW2KR8\u003D();
    this.\u0023\u003DzZ_bY0tT8hlGH = _param1;
    if (this.\u0023\u003DzZ_bY0tT8hlGH == null)
      return;
    this.\u0023\u003DzCGGLJMU\u003D();
  }

  public string MouseEventGroup
  {
    get => this.\u0023\u003DzIOF\u0024pM4fvIzk4nVUyHULhOk\u003D;
    set => this.\u0023\u003DzIOF\u0024pM4fvIzk4nVUyHULhOk\u003D = value;
  }

  [SpecialName]
  public IAnnotation \u0023\u003Dzy2oKVLXXOFmI()
  {
    return this.\u0023\u003DzJ3FH5kAFy7GH;
  }

  public bool \u0023\u003Dzo7mdr1Y1DFNe()
  {
    return this.IsEnabled && this.\u0023\u003DzVuf430fCLR2l() != null;
  }

  public virtual void \u0023\u003DzCGGLJMU\u003D()
  {
    this.\u0023\u003DzZ_bY0tT8hlGH.Children.Add((UIElement) this);
    this.\u0023\u003DzFeNr2Uw\u003D();
  }

  public virtual void \u0023\u003DzcNW2KR8\u003D()
  {
    this.\u0023\u003DzUf222sU\u003D();
    this.\u0023\u003DzVuf430fCLR2l().Children.Remove((UIElement) this);
    this.\u0023\u003DzrEoWi5uPS0Yz((IServiceContainer) null);
  }

  public Point \u0023\u003DzUTaLYgNA\u00243iO25vv\u0024g\u003D\u003D(Point _param1)
  {
    if (!(this.\u0023\u003Dzy2oKVLXXOFmI() is UIElement uiElement))
      return _param1;
    UIElement relativeTo = this.\u0023\u003Dzy2oKVLXXOFmI().ParentSurface.\u0023\u003Dzwc4Gzka23TGB() as UIElement;
    return uiElement.TranslatePoint(_param1, relativeTo);
  }

  public virtual void \u0023\u003Dz5y8F1YNwkhnW(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
  }

  public virtual void \u0023\u003DzsXEfcKpqchyX(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
  }

  public virtual void \u0023\u003Dz11bcnbUrALaA(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
  }

  public virtual void \u0023\u003DzU3pYs4rYVmOS(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
  }

  public virtual void \u0023\u003DzQTINWhMByBmJ(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
  }

  public void \u0023\u003Dz0yya794Z8OaI(
    \u0023\u003DzEJoJjwSelM_K3zbmiw1OAyK9MPuNjG5KW8R74IqEWckf _param1)
  {
    throw new NotImplementedException();
  }

  public void \u0023\u003DzpmQpuKvOtHIk(
    \u0023\u003DzEJoJjwSelM_K3zbmiw1OAyK9MPuNjG5KW8R74IqEWckf _param1)
  {
    throw new NotImplementedException();
  }

  public void \u0023\u003DzsSwjrBzrsGPJ(
    \u0023\u003DzEJoJjwSelM_K3zbmiw1OAyK9MPuNjG5KW8R74IqEWckf _param1)
  {
    throw new NotImplementedException();
  }

  public void \u0023\u003Dz3RBcoKAPKSIX(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
  }

  public new bool IsEnabled
  {
    get => this.\u0023\u003Dzy2oKVLXXOFmI().get_IsSelected();
    set => this.\u0023\u003Dzy2oKVLXXOFmI().set_IsSelected(value);
  }

  public abstract void \u0023\u003DzFeNr2Uw\u003D();

  public abstract void \u0023\u003DzGDdLHa8\u003D();

  public abstract void \u0023\u003DzUf222sU\u003D();
}
