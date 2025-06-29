// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.IChart3DParameter
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

#nullable disable
namespace StockSharp.Xaml.Charting;

/// <summary>
/// Parameter interface for <see cref="T:StockSharp.Xaml.Charting.Chart3D" />.
/// </summary>
public interface IChart3DParameter
{
  /// <summary>Identifier.</summary>
  string Id { get; }

  /// <summary>Name.</summary>
  string Name { get; }
}
