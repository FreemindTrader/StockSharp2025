// Decompiled with JetBrains decompiler
// Type: #=zuPRmIFUVJkGxyCE55JH19ZE5sEUdF5DXPLZ7U6Rxl0An
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
internal sealed class UpdateSuspender : 
  IUpdateSuspender,
  IDisposable
{
  
  private readonly object \u0023\u003Dz3xPfSd0\u003D;
  
  private readonly ISuspendable \u0023\u003DzCede7bY\u003D;
  
  private static readonly IDictionary<ISuspendable, UpdateSuspender.SomeStruct> \u0023\u003Dz6y_gICEiE1z2 = (IDictionary<ISuspendable, UpdateSuspender.SomeStruct>) new Dictionary<ISuspendable, UpdateSuspender.SomeStruct>();
  
  private static readonly object myLock = new object();
  
  private bool \u0023\u003Dz9lEOT9K2_723;

  internal UpdateSuspender(
    ISuspendable _param1,
    object _param2)
    : this(_param1)
  {
    this.\u0023\u003Dz3xPfSd0\u003D = _param2;
  }

  internal UpdateSuspender(
    ISuspendable _param1)
  {
    this.\u0023\u003DzCede7bY\u003D = _param1;
    lock (UpdateSuspender.myLock)
    {
      if (!UpdateSuspender.\u0023\u003Dz6y_gICEiE1z2.ContainsKey(this.\u0023\u003DzCede7bY\u003D))
        UpdateSuspender.\u0023\u003Dz6y_gICEiE1z2.Add(this.\u0023\u003DzCede7bY\u003D, new UpdateSuspender.SomeStruct(0, 0));
      this.Inc(this.\u0023\u003DzCede7bY\u003D);
      this.ResumeTargetOnDispose=true;
    }
  }

  [SpecialName]
  public bool IsSuspended
  {
    return UpdateSuspender.\u0023\u003DzY5RcByYV3P6y(this.\u0023\u003DzCede7bY\u003D);
  }

  [SpecialName]
  public bool ResumeTargetOnDispose => this.\u0023\u003Dz9lEOT9K2_723;

  [SpecialName]
  public void ResumeTargetOnDispose=bool _param1)
  {
    if (this.\u0023\u003Dz9lEOT9K2_723 == _param1)
      return;
    this.\u0023\u003Dz9lEOT9K2_723 = _param1;
    lock (UpdateSuspender.myLock)
    {
      UpdateSuspender.SomeStruct z2xTuPx8 = UpdateSuspender.\u0023\u003Dz6y_gICEiE1z2[this.\u0023\u003DzCede7bY\u003D];
      UpdateSuspender.\u0023\u003Dz6y_gICEiE1z2[this.\u0023\u003DzCede7bY\u003D] = new UpdateSuspender.SomeStruct(z2xTuPx8.GetReadOnly1(), z2xTuPx8.GetReadOnly2() + (_param1 ? 1 : -1));
    }
  }

  public void Dispose()
  {
    lock (UpdateSuspender.myLock)
    {
      this.\u0023\u003DzCede7bY\u003D.DecrementSuspend();
      if (this.Dec(this.\u0023\u003DzCede7bY\u003D) != 0)
        return;
      this.\u0023\u003Dz9lEOT9K2_723 = UpdateSuspender.\u0023\u003Dz6y_gICEiE1z2[this.\u0023\u003DzCede7bY\u003D].GetReadOnly2() > 0;
      UpdateSuspender.\u0023\u003Dz6y_gICEiE1z2.Remove(this.\u0023\u003DzCede7bY\u003D);
      this.\u0023\u003DzCede7bY\u003D.ResumeUpdates((IUpdateSuspender) this);
    }
  }

  [SpecialName]
  public object Tag => this.\u0023\u003Dz3xPfSd0\u003D;

  internal static bool \u0023\u003DzY5RcByYV3P6y(
    ISuspendable _param0)
  {
    lock (UpdateSuspender.myLock)
      return UpdateSuspender.\u0023\u003Dz6y_gICEiE1z2.ContainsKey(_param0) && UpdateSuspender.\u0023\u003Dz6y_gICEiE1z2[_param0].GetReadOnly1() != 0;
  }

  private void Inc(
    ISuspendable _param1)
  {
    lock (UpdateSuspender.myLock)
    {
      UpdateSuspender.SomeStruct z2xTuPx8 = UpdateSuspender.\u0023\u003Dz6y_gICEiE1z2[_param1];
      UpdateSuspender.\u0023\u003Dz6y_gICEiE1z2[_param1] = new UpdateSuspender.SomeStruct(z2xTuPx8.GetReadOnly1() + 1, z2xTuPx8.GetReadOnly2());
    }
  }

  private int Dec(
    ISuspendable _param1)
  {
    lock (UpdateSuspender.myLock)
    {
      UpdateSuspender.SomeStruct z2xTuPx8 = UpdateSuspender.\u0023\u003Dz6y_gICEiE1z2[_param1];
      UpdateSuspender.\u0023\u003Dz6y_gICEiE1z2[_param1] = new UpdateSuspender.SomeStruct(z2xTuPx8.GetReadOnly1() - 1, z2xTuPx8.GetReadOnly2());
      return z2xTuPx8.GetReadOnly1() - 1;
    }
  }

  private struct SomeStruct(int _param1, int _param2)
  {
    
    private readonly int _intReadOnly1 = _param1;
    
    private readonly int _intReadOnly2 = _param2;

    public readonly int GetReadOnly1()
    {
      return this._intReadOnly1;
    }

    public readonly int GetReadOnly2()
    {
      return this._intReadOnly2;
    }
  }
}
