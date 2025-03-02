// Decompiled with JetBrains decompiler
// Type: StockSharp.Hydra.Core.SecurityLookupSupportTypes
// Assembly: StockSharp.Hydra.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BF4FBD4E-7629-47D5-B0AC-6D48C0A60551
// Assembly location: T:\00-StockSharp\Data\StockSharp.Hydra.Core.dll

namespace StockSharp.Hydra.Core
{
  /// <summary>Типы поиска инструмента.</summary>
  public enum SecurityLookupSupportTypes
  {
    /// <summary>Доступна загрузка всех инструментов.</summary>
    SupportAll,
    /// <summary>По коду инструмента.</summary>
    CodeRequired,
    /// <summary>Не поддерживается.</summary>
    NotSupported,
  }
}
