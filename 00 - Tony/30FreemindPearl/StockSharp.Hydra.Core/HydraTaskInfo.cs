// Decompiled with JetBrains decompiler
// Type: StockSharp.Hydra.Core.HydraTaskInfo
// Assembly: StockSharp.Hydra.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BF4FBD4E-7629-47D5-B0AC-6D48C0A60551
// Assembly location: T:\00-StockSharp\Data\StockSharp.Hydra.Core.dll

using Ecng.Common;
using Ecng.Serialization;
using System;

namespace StockSharp.Hydra.Core
{
  /// <summary>Task settings.</summary>
  public class HydraTaskInfo
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Hydra.Core.HydraStorage" />.
    /// </summary>
    /// <param name="id">Task id.</param>
    /// <param name="taskType">Task type.</param>
    /// <param name="settings">Task settings.</param>
    public HydraTaskInfo(Guid id, string taskType, SettingsStorage settings)
    {
      if (id == new Guid())
        throw new ArgumentNullException(nameof (id));
      if (taskType.IsEmpty())
        throw new ArgumentNullException(nameof (taskType));
      this.Id = id;
      this.TaskType = taskType;
      SettingsStorage settingsStorage = settings;
      if (settingsStorage == null)
        throw new ArgumentNullException(nameof (settings));
      this.Settings = settingsStorage;
    }

    /// <summary>Task id.</summary>
    public Guid Id { get; }

    /// <summary>Task type.</summary>
    public string TaskType { get; }

    /// <summary>Task settings.</summary>
    public SettingsStorage Settings { get; }
  }
}
