// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyCandleValue
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using Ecng.Serialization;
using StockSharp.Messages;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class StrategyCandleValue : IPersistable
{
    public int Index { get; set; }

    public Decimal Open { get; set; }

    public Decimal High { get; set; }

    public Decimal Low { get; set; }

    public Decimal Close { get; set; }

    public Decimal Volume { get; set; }

    public CandlePriceLevel[] Levels { get; set; }

    public int? Color { get; set; }

    public virtual void Load(SettingsStorage storage)
    {
        this.Index = storage.GetValue<int>("Index", 0);
        this.Open = storage.GetValue<Decimal>("Open", 0M);
        this.High = storage.GetValue<Decimal>("High", 0M);
        this.Low = storage.GetValue<Decimal>("Low", 0M);
        this.Close = storage.GetValue<Decimal>("Close", 0M);
        this.Volume = storage.GetValue<Decimal>("Volume", 0M);
        this.Levels = storage.GetValue<CandlePriceLevel[]>("Levels", (CandlePriceLevel[])null);
        this.Color = storage.GetValue<int?>("Color", new int?());
    }

    public virtual void Save(SettingsStorage storage)
    {
        storage.Set<int>("Index", this.Index).Set<Decimal>("Open", this.Open).Set<Decimal>("High", this.High).Set<Decimal>("Low", this.Low).Set<Decimal>("Close", this.Close).Set<Decimal>("Volume", this.Volume).Set<CandlePriceLevel[]>("Levels", this.Levels).Set<int?>("Color", this.Color);
    }
}
