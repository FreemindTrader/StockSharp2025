// Decompiled with JetBrains decompiler
// Type: StockSharp.Hydra.Core.TaskStates
// Assembly: StockSharp.Hydra.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BF4FBD4E-7629-47D5-B0AC-6D48C0A60551
// Assembly location: T:\00-StockSharp\Data\StockSharp.Hydra.Core.dll

namespace StockSharp.Hydra.Core
{
  /// <summary>Состояния задачи.</summary>
  public enum TaskStates
  {
    /// <summary>Остановлен.</summary>
    Stopped,
    /// <summary>Останавливается.</summary>
    Stopping,
    /// <summary>Запускается.</summary>
    Starting,
    /// <summary>Запущен.</summary>
    Started,
  }
}
