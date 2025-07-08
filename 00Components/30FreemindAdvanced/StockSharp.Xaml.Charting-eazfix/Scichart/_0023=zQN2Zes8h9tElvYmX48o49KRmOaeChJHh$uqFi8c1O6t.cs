// Decompiled with JetBrains decompiler
// Type: #=zQN2Zes8h9tElvYmX48o49KRmOaeChJHh$uqFi8c1O6tzvwgXZcckW9tFEg5MxkJ1sg==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Diagnostics;

#nullable disable
internal sealed class \u0023\u003DzQN2Zes8h9tElvYmX48o49KRmOaeChJHh\u0024uqFi8c1O6tzvwgXZcckW9tFEg5MxkJ1sg\u003D\u003D : 
  IDisposable,
  \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024,
  \u0023\u003DzpKvy0OA0_My0Sg27HiUJaX\u0024AyxSGkqEcPv0Ah3hMaVEX
{
  
  private readonly IRenderContext2D \u0023\u003DzVxwXLcXPtvCC;
  
  private \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024 \u0023\u003Dz3A_LTZ7wmTQL;
  
  private readonly bool \u0023\u003DzRvqe\u0024JQD4tCB;
  
  private readonly double \u0023\u003DzND_smWdG_akWj5FwlBOmpAc\u003D;
  
  private readonly bool \u0023\u003DzNDaVMmUigLMz = true;
  
  private readonly double \u0023\u003DztMa63GaK8TjGvEcTHw\u003D\u003D;
  
  private double \u0023\u003Dzlj5LvFYWlD8Q;
  
  private double \u0023\u003DziT6QOXorJxAa;
  
  private double \u0023\u003DzFEDR40ugZMK3;
  
  private double \u0023\u003DzGcDWpYNQwUmC;

  public \u0023\u003DzQN2Zes8h9tElvYmX48o49KRmOaeChJHh\u0024uqFi8c1O6tzvwgXZcckW9tFEg5MxkJ1sg\u003D\u003D(
    IRenderContext2D _param1,
    bool _param2,
    double _param3,
    double _param4)
    : this(_param1, _param2, true, _param3, _param4)
  {
  }

  public \u0023\u003DzQN2Zes8h9tElvYmX48o49KRmOaeChJHh\u0024uqFi8c1O6tzvwgXZcckW9tFEg5MxkJ1sg\u003D\u003D(
    IRenderContext2D _param1,
    bool _param2,
    double _param3)
    : this(_param1, _param2, false, 0.0, _param3)
  {
  }

  protected \u0023\u003DzQN2Zes8h9tElvYmX48o49KRmOaeChJHh\u0024uqFi8c1O6tzvwgXZcckW9tFEg5MxkJ1sg\u003D\u003D(
    IRenderContext2D _param1,
    bool _param2,
    bool _param3,
    double _param4,
    double _param5)
  {
    this.\u0023\u003DzVxwXLcXPtvCC = _param1;
    this.\u0023\u003DzRvqe\u0024JQD4tCB = _param2;
    this.\u0023\u003DzNDaVMmUigLMz = _param3;
    this.\u0023\u003DztMa63GaK8TjGvEcTHw\u003D\u003D = _param4;
    this.\u0023\u003DzND_smWdG_akWj5FwlBOmpAc\u003D = _param5;
  }

  public \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024 \u0023\u003Dz7ZSU06M\u003D(
    IPathColor _param1,
    double _param2,
    double _param3)
  {
    this.\u0023\u003Dzlj5LvFYWlD8Q = _param2;
    this.\u0023\u003DziT6QOXorJxAa = _param3;
    this.\u0023\u003Dz3A_LTZ7wmTQL = this.\u0023\u003DzVxwXLcXPtvCC.\u0023\u003DzD4fw8fY\u003D((IBrush2D) _param1, _param2, _param3, this.\u0023\u003DzND_smWdG_akWj5FwlBOmpAc\u003D);
    return (\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024) this;
  }

  public void \u0023\u003DzBNsE20w\u003D()
  {
    if (this.\u0023\u003DzNDaVMmUigLMz)
    {
      if (this.\u0023\u003DzRvqe\u0024JQD4tCB)
      {
        this.\u0023\u003Dz3A_LTZ7wmTQL.\u0023\u003DzfRDRUq8\u003D(this.\u0023\u003DztMa63GaK8TjGvEcTHw\u003D\u003D, this.\u0023\u003DzGcDWpYNQwUmC);
        this.\u0023\u003Dz3A_LTZ7wmTQL.\u0023\u003DzfRDRUq8\u003D(this.\u0023\u003DztMa63GaK8TjGvEcTHw\u003D\u003D, this.\u0023\u003DziT6QOXorJxAa);
      }
      else
      {
        this.\u0023\u003Dz3A_LTZ7wmTQL.\u0023\u003DzfRDRUq8\u003D(this.\u0023\u003DzFEDR40ugZMK3, this.\u0023\u003DztMa63GaK8TjGvEcTHw\u003D\u003D);
        this.\u0023\u003Dz3A_LTZ7wmTQL.\u0023\u003DzfRDRUq8\u003D(this.\u0023\u003Dzlj5LvFYWlD8Q, this.\u0023\u003DztMa63GaK8TjGvEcTHw\u003D\u003D);
      }
    }
    this.\u0023\u003Dz3A_LTZ7wmTQL.\u0023\u003DzfRDRUq8\u003D(this.\u0023\u003Dzlj5LvFYWlD8Q, this.\u0023\u003DziT6QOXorJxAa);
    this.\u0023\u003Dz3A_LTZ7wmTQL.\u0023\u003DzBNsE20w\u003D();
    this.\u0023\u003Dz3A_LTZ7wmTQL = (\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024) null;
  }

  public \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024 \u0023\u003DzfRDRUq8\u003D(
    double _param1,
    double _param2)
  {
    this.\u0023\u003DzFEDR40ugZMK3 = _param1;
    this.\u0023\u003DzGcDWpYNQwUmC = _param2;
    this.\u0023\u003Dz3A_LTZ7wmTQL.\u0023\u003DzfRDRUq8\u003D(_param1, _param2);
    return (\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024) this;
  }

  void IDisposable.Dispose()
  {
    this.\u0023\u003DzBNsE20w\u003D();
  }
}
