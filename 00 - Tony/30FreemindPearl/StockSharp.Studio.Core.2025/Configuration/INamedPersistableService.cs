// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Configuration.INamedPersistableService
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 287A45E4-79AD-43B4-B98B-3CD330C45DE5
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Core.dll

using StockSharp.Studio.Core.Services;

namespace StockSharp.Studio.Core.Configuration
{
    public interface INamedPersistableService : IPersistableService
    {
        string Name { get; }
    }
}
