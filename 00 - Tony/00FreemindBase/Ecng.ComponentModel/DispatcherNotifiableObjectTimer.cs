// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.DispatcherNotifiableObjectTimer
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using System;
using System.Threading.Tasks;

namespace Ecng.ComponentModel
{
  internal class DispatcherNotifiableObjectTimer
  {
    private static readonly Lazy<DispatcherNotifiableObjectTimer> _instance = new Lazy<DispatcherNotifiableObjectTimer>(true);
    private readonly TimeSpan _minInterval = TimeSpan.FromMilliseconds(100.0);
    private TimeSpan _interval = TimeSpan.FromMilliseconds(1000.0);

    public event Action Tick;

    public TimeSpan Interval
    {
      get
      {
        return this._interval;
      }
      set
      {
        if (value > this._interval)
          return;
        if (value < this._minInterval)
          value = this._minInterval;
        this._interval = value;
      }
    }

    public DispatcherNotifiableObjectTimer()
    {
      Task.Run(new Func<Task>(this.TimerTask));
    }

    private async Task TimerTask()
    {
      while (true)
      {
        Action tick;
        do
        {
          await Task.Delay(this._interval);
          tick = this.Tick;
        }
        while (tick == null);
        tick();
      }
    }

    public static DispatcherNotifiableObjectTimer Instance
    {
      get
      {
        return DispatcherNotifiableObjectTimer._instance.Value;
      }
    }
  }
}
