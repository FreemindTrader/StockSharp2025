// Decompiled with JetBrains decompiler
// Type: #=zro0Io1hfSw7LlH634iIk6E8I_CiUqBSaCicrMV4=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Diagnostics;
using System.Windows.Threading;

#nullable enable
internal sealed class \u0023\u003Dzro0Io1hfSw7LlH634iIk6E8I_CiUqBSaCicrMV4\u003D : IDisposable
{
  
  private readonly 
  #nullable disable
  Action dpoChangedEventArgs;
  
  private int \u0023\u003DzMRT_jLU\u003D = 100;
  
  private DispatcherTimer \u0023\u003DzO6iOtf4\u003D;
  
  private DispatcherPriority \u0023\u003Dz87uDtww\u003D = (DispatcherPriority) 4;

  private \u0023\u003Dzro0Io1hfSw7LlH634iIk6E8I_CiUqBSaCicrMV4\u003D(Action _param1)
  {
    this.dpoChangedEventArgs = _param1;
  }

  public static \u0023\u003Dzro0Io1hfSw7LlH634iIk6E8I_CiUqBSaCicrMV4\u003D \u0023\u003DzEbmjWGc\u003D(
    Action _param0)
  {
    return new \u0023\u003Dzro0Io1hfSw7LlH634iIk6E8I_CiUqBSaCicrMV4\u003D(_param0);
  }

  public \u0023\u003Dzro0Io1hfSw7LlH634iIk6E8I_CiUqBSaCicrMV4\u003D \u0023\u003Dz5PVA6zg\u003D(
    DispatcherPriority _param1)
  {
    this.\u0023\u003Dz87uDtww\u003D = _param1;
    return this;
  }

  public \u0023\u003Dzro0Io1hfSw7LlH634iIk6E8I_CiUqBSaCicrMV4\u003D \u0023\u003DzjuNK2y0\u003D(
    int _param1)
  {
    this.\u0023\u003DzMRT_jLU\u003D = _param1;
    return this;
  }

  public void Dispose()
  {
    if (this.\u0023\u003DzO6iOtf4\u003D == null)
      return;
    this.\u0023\u003DzO6iOtf4\u003D.Stop();
    this.\u0023\u003DzO6iOtf4\u003D = (DispatcherTimer) null;
  }

  public \u0023\u003Dzro0Io1hfSw7LlH634iIk6E8I_CiUqBSaCicrMV4\u003D \u0023\u003Dz0g8_jDM\u003D()
  {
    if (this.\u0023\u003DzMRT_jLU\u003D <= 0)
    {
      this.dpoChangedEventArgs();
      return this;
    }
    this.\u0023\u003DzO6iOtf4\u003D = new DispatcherTimer(this.\u0023\u003Dz87uDtww\u003D);
    this.\u0023\u003DzO6iOtf4\u003D.Interval = TimeSpan.FromMilliseconds((double) this.\u0023\u003DzMRT_jLU\u003D);
    this.\u0023\u003DzO6iOtf4\u003D.Tick += new EventHandler(this.\u0023\u003DzSfpwmICHOeDiktpI9w\u003D\u003D);
    this.\u0023\u003DzO6iOtf4\u003D.Start();
    return this;
  }

  private void \u0023\u003DzSfpwmICHOeDiktpI9w\u003D\u003D(
  #nullable enable
  object? _param1, EventArgs _param2)
  {
    this.dpoChangedEventArgs();
    this.\u0023\u003DzO6iOtf4\u003D.Stop();
  }
}
