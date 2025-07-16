// Decompiled with JetBrains decompiler
// Type: #=zx24ajpn1eHsuu_VYWsSDOT7SaPnEBc9oZVOY9qs=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Runtime.CompilerServices;

#nullable disable
public abstract class \u0023\u003Dzx24ajpn1eHsuu_VYWsSDOT7SaPnEBc9oZVOY9qs\u003D : 
  \u0023\u003Dzxn9vS9UX4BfDgK8stUp1bU9TbfoDtGpTtZMbxfI\u003D
{
  private WeakReference \u0023\u003DzFSUsESM\u003D;

  public \u0023\u003Dzx24ajpn1eHsuu_VYWsSDOT7SaPnEBc9oZVOY9qs\u003D(object _param1)
  {
    this.\u0023\u003DzFSUsESM\u003D = _param1 != null ? new WeakReference(_param1) : throw new ArgumentNullException("sender");
  }

  [SpecialName]
  public object \u0023\u003DzruCparrE9ODW()
  {
    return this.\u0023\u003DzFSUsESM\u003D != null ? this.\u0023\u003DzFSUsESM\u003D.Target : (object) null;
  }
}
