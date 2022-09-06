// Decompiled with JetBrains decompiler
// Type: StockSharp.Hydra.Core.IConnectorHydraTask
// Assembly: StockSharp.Hydra.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BF4FBD4E-7629-47D5-B0AC-6D48C0A60551
// Assembly location: T:\00-StockSharp\Data\StockSharp.Hydra.Core.dll

using StockSharp.Messages;
using System;

namespace StockSharp.Hydra.Core
{
  /// <summary>
  /// Интерфейс, описывающий источник, работающий через <see cref="T:StockSharp.Messages.IMessageAdapter" />.
  /// </summary>
  public interface IConnectorHydraTask
  {
    /// <summary>Стартовая дата закачки данных свечей.</summary>
    DateTime? CandlesFromDate { get; }

    /// <summary>Адаптер.</summary>
    IMessageAdapter Adapter { get; }
  }
}
