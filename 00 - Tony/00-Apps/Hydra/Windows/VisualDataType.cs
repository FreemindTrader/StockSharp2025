// Decompiled with JetBrains decompiler
// Type: StockSharp.Hydra.Windows.VisualDataType
// Assembly: Hydra, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3BCB1262-CD24-4283-B1AE-24E756F47247
// Assembly location: T:\00-StockSharp\Data\Hydra.dll

using StockSharp.Localization;
using StockSharp.Messages;
using System;

namespace StockSharp.Hydra.Windows
{
  public class VisualDataType
  {
    private readonly bool _generated;
    public readonly DataType DataType;

    public VisualDataType(DataType dataType, bool generated)
    {
      DataType dataType1 = dataType;
      if (dataType1 == null)
        throw new ArgumentNullException(nameof (dataType));
      DataType = dataType1;
      _generated = generated;
    }

    public override string ToString()
    {
      return DataType.ToString() + (_generated ? " (" + LocalizedStrings.Str1406 + ")" : string.Empty);
    }
  }
}
