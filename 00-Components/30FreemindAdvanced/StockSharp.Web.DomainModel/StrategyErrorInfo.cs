// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyErrorInfo
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class StrategyErrorInfo : IPersistable
{
    public string Message { get; set; }

    public string ElementId { get; set; }

    public string ElementType { get; set; }

    public string StackTrace { get; set; }

    public virtual void Load(SettingsStorage storage)
    {
        this.Message = storage.GetValue<string>("Message", (string)null);
        this.ElementId = storage.GetValue<string>("ElementId", (string)null);
        this.ElementType = storage.GetValue<string>("ElementType", (string)null);
        this.StackTrace = storage.GetValue<string>("StackTrace", (string)null);
    }

    public virtual void Save(SettingsStorage storage)
    {
        storage.Set<string>("Message", this.Message).Set<string>("ElementId", this.ElementId).Set<string>("ElementType", this.ElementType).Set<string>("StackTrace", this.StackTrace);
    }
}
