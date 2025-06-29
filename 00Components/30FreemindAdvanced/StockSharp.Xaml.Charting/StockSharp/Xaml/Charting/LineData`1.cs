// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.LineData`1
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Runtime.Serialization;

#nullable disable
namespace StockSharp.Xaml.Charting;

/// <summary>Line data.</summary>
/// <typeparam name="TKey">Key type.</typeparam>
[DataContract]
[Serializable]
public class LineData<TKey>
{
  /// <summary>The X value.</summary>
  [DataMember]
  public TKey X { get; set; }

  /// <summary>The Y value.</summary>
  [DataMember]
  public Decimal Y { get; set; }
}
