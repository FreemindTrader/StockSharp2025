// Decompiled with JetBrains decompiler
// Type: #=z8HlC6EDl$btRSPRwAzbJh76xifd94OJRDA==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;

#nullable disable
public sealed class \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh76xifd94OJRDA\u003D\u003D : 
  IServiceContainer
{
  private readonly IDictionary<Type, object> \u0023\u003Dz0AYLrTERpgx0 = (IDictionary<Type, object>) new Dictionary<Type, object>();

  public bool \u0023\u003DzY9a0Q6M\u003D<T>()
  {
    return this.\u0023\u003Dz0AYLrTERpgx0.ContainsKey(typeof (T));
  }

  public void \u0023\u003Dz7wSH25w\u003D<T>(T _param1)
  {
    this.\u0023\u003Dz0AYLrTERpgx0[typeof (T)] = (object) _param1;
  }

  public void \u0023\u003DzwC3KXAMeFY2h<T>() => this.\u0023\u003Dz0AYLrTERpgx0.Remove(typeof (T));

  public T GetService<T>()
  {
    Type key = typeof (T);
    if (!this.\u0023\u003DzY9a0Q6M\u003D<T>())
      throw new Exception($"The service instance of type {key} has not been registered with the container");
    return (T) this.\u0023\u003Dz0AYLrTERpgx0[key];
  }
}
