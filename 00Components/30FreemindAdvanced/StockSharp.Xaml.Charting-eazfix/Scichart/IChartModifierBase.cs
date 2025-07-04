// Decompiled with JetBrains decompiler
// Type: #=zN_ef$eTD0bISWSKkIKedSrfQ$fvhBTfuaBKLOTsYHeMg
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.ComponentModel;

#nullable disable
internal interface IChartModifierBase : 
  INotifyPropertyChanged,
  IReceiveMouseEvents 
{
  IServiceContainer Services { get; set; }

  \u0023\u003DzeOTgfMmJN9ezcFvs39Ju8q\u0024wkROgPo2o_c9nq8U\u003D ModifierSurface { get; }

  string ModifierName { get; }

  bool IsAttached { get; set; }

  object DataContext { get; set; }

  bool ReceiveHandledEvents { get; }

  void OnAttached();

  void OnDetached();
}
