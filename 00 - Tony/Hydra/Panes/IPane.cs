// Decompiled with JetBrains decompiler
// Type: StockSharp.Hydra.Panes.IPane
// Assembly: Hydra, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3BCB1262-CD24-4283-B1AE-24E756F47247
// Assembly location: T:\00-StockSharp\Data\Hydra.dll

using Ecng.Serialization;
using StockSharp.Studio.Core;
using System;

namespace StockSharp.Hydra.Panes
{
  public interface IPane : IStudioControl, IPersistable, IDisposable
  {
    bool IsValid { get; }
  }
}
