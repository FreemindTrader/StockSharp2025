// Decompiled with JetBrains decompiler
// Type: #=z3uoqT9PJZU9sx1O75XaUu$W0gdZzFkccR0pTuu84OEtC
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Input;
using System.Windows.Media;

#nullable disable
public sealed class \u0023\u003Dz3uoqT9PJZU9sx1O75XaUu\u0024W0gdZzFkccR0pTuu84OEtC : IDisposable
{
  
  private bool \u0023\u003Dzwt21uMVBOpMf;
  
  private readonly IPublishMouseEvents \u0023\u003DzwhoPcC4\u003D;
  
  private MouseEventHandler \u0023\u003DzMv6aNgkbDCcS;

  public \u0023\u003Dz3uoqT9PJZU9sx1O75XaUu\u0024W0gdZzFkccR0pTuu84OEtC(
    IPublishMouseEvents _param1)
  {
    this.\u0023\u003DzwhoPcC4\u003D = _param1;
    _param1.add_MouseMove(new MouseEventHandler(this.\u0023\u003DztQrG6UkAcwQR));
  }

  public void \u0023\u003Dz5v7amar0n_e8(MouseEventHandler _param1)
  {
    MouseEventHandler mouseEventHandler = this.\u0023\u003DzMv6aNgkbDCcS;
    MouseEventHandler comparand;
    do
    {
      comparand = mouseEventHandler;
      mouseEventHandler = Interlocked.CompareExchange<MouseEventHandler>(ref this.\u0023\u003DzMv6aNgkbDCcS, comparand + _param1, comparand);
    }
    while (mouseEventHandler != comparand);
  }

  public void \u0023\u003DzOzRhiTCfaCeW(MouseEventHandler _param1)
  {
    MouseEventHandler mouseEventHandler = this.\u0023\u003DzMv6aNgkbDCcS;
    MouseEventHandler comparand;
    do
    {
      comparand = mouseEventHandler;
      mouseEventHandler = Interlocked.CompareExchange<MouseEventHandler>(ref this.\u0023\u003DzMv6aNgkbDCcS, comparand - _param1, comparand);
    }
    while (mouseEventHandler != comparand);
  }

  public void Dispose()
  {
    this.\u0023\u003DzwhoPcC4\u003D.remove_MouseMove(new MouseEventHandler(this.\u0023\u003DztQrG6UkAcwQR));
    this.\u0023\u003DzMv6aNgkbDCcS = (MouseEventHandler) null;
  }

  private void \u0023\u003DztQrG6UkAcwQR(object _param1, MouseEventArgs _param2)
  {
    if (this.\u0023\u003Dzwt21uMVBOpMf)
      return;
    this.\u0023\u003DzOcPDGUuqguUQ(_param2);
    this.\u0023\u003Dzwt21uMVBOpMf = true;
    CompositionTarget.Rendering += new EventHandler(this.\u0023\u003Dzq1hBQp\u0024VOhGm);
  }

  private void \u0023\u003Dzq1hBQp\u0024VOhGm(object _param1, EventArgs _param2)
  {
    this.\u0023\u003Dzwt21uMVBOpMf = false;
    CompositionTarget.Rendering -= new EventHandler(this.\u0023\u003Dzq1hBQp\u0024VOhGm);
  }

  public void \u0023\u003DzOcPDGUuqguUQ(MouseEventArgs _param1)
  {
    MouseEventHandler zMv6aNgkbDccS = this.\u0023\u003DzMv6aNgkbDCcS;
    if (zMv6aNgkbDccS == null)
      return;
    zMv6aNgkbDccS((object) this.\u0023\u003DzwhoPcC4\u003D, _param1);
  }
}
