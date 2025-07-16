// Decompiled with JetBrains decompiler
// Type: #=zExPUKZPbT0fb9dlf_qOoa7Fo_o9lZIelo$_m4wTHwP6Ifze3$A==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

#nullable disable
public interface ISuspendable
{
  bool IsSuspended { get; }

  bool IsSuspended;

  IUpdateSuspender SuspendUpdates();

  void ResumeUpdates(
    IUpdateSuspender _param1);

  void DecrementSuspend();
}
