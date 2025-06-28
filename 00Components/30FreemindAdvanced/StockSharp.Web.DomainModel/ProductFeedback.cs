// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ProductFeedback
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class ProductFeedback : ProductBugReport
{
    public int Rate { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Rate = storage.GetValue<int>("Rate", 0);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<int>("Rate", this.Rate);
    }
}
