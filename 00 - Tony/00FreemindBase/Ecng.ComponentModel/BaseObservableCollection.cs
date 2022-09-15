// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.BaseObservableCollection
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using System;

namespace Ecng.ComponentModel
{
  public abstract class BaseObservableCollection
  {
    private int _maxCount = -1;

    public int MaxCount
    {
      get
      {
        return this._maxCount;
      }
      set
      {
        if (value < -1 || value == 0)
          throw new ArgumentOutOfRangeException();
        this._maxCount = value;
      }
    }

    public abstract int Count { get; }

    public abstract int RemoveRange(int index, int count);

    protected void CheckCount()
    {
      if (this.MaxCount == -1 || (double) this.Count <= 1.5 * (double) this.MaxCount)
        return;
      this.RemoveRange(0, this.Count - this.MaxCount);
    }
  }
}
