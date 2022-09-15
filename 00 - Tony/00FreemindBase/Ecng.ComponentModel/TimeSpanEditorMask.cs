// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.TimeSpanEditorMask
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using System;

namespace Ecng.ComponentModel
{
  [Flags]
  public enum TimeSpanEditorMask
  {
    Days = 1,
    Hours = 2,
    Minutes = 4,
    Seconds = 8,
    Milliseconds = 16, // 0x00000010
    Microseconds = 32, // 0x00000020
  }
}
