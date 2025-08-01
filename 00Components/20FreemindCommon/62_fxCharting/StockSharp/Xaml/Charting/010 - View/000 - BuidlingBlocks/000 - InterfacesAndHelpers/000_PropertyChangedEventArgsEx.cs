// Decompiled with JetBrains decompiler
// Type: #=zMv9TAT1PEEnC0UeBhCNwDCPI15WlokXZO9yDJcU=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.ComponentModel;
using System.Diagnostics;

#nullable disable
public sealed class PropertyChangedEventArgsEx :  PropertyChangedEventArgs
{
  
  private object _objectOne;
  
  private object _objectTwo;

  public PropertyChangedEventArgsEx(
    string _param1,
    object _param2,
    object _param3)
    : base(_param1)
  {
    this.SetObjectOne(_param2);
    this.SetObjectTwo(_param3);
  }

  public object GetObjectOne() => this._objectOne;

  protected void SetObjectOne(object _param1)
  {
    this._objectOne = _param1;
  }

  public object GetObjectTwo()
  {
    return this._objectTwo;
  }

  protected void SetObjectTwo(object _param1)
  {
    this._objectTwo = _param1;
  }
}
