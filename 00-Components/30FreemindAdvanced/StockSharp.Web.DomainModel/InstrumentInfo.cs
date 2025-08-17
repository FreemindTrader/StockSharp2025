// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.InstrumentInfo
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using Ecng.Common;
using Ecng.Serialization;
using StockSharp.Messages;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class InstrumentInfo : BaseEntity, INameEntity, IDescriptionEntity
{
    public string Code { get; set; }

    public string Board { get; set; }

    public string Name { get; set; }

    public string ShortName { get; set; }

    public string Isin { get; set; }

    public string Asset { get; set; }

    public SecurityTypes? Type { get; set; }

    public DateTime? IssueDate { get; set; }

    public Decimal? IssueSize { get; set; }

    public DateTime? LastDate { get; set; }

    public int? Decimals { get; set; }

    public Decimal? Multiplier { get; set; }

    public Decimal? PriceStep { get; set; }

    public CurrencyTypes? Currency { get; set; }

    public DateTime? SettleDate { get; set; }

    public long? FinamSecurityId { get; set; }

    public long? FinamMarketId { get; set; }

    public bool? AllowBacktest { get; set; }

    public bool? AllowLive { get; set; }

    public BaseEntitySet<InstrumentDataType> DataTypes { get; set; }

    public BaseEntitySet<DataPermission> Permissions { get; set; }

    string INameEntity.Name
    {
        get => $"{this.Code}@{this.Board}";
        set => this.Code = value;
    }

    string IDescriptionEntity.Description
    {
        get => this.Name;
        set => this.Name = value;
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Code = storage.GetValue<string>("Code", (string)null);
        this.Board = storage.GetValue<string>("Board", (string)null);
        this.Name = storage.GetValue<string>("Name", (string)null);
        this.ShortName = storage.GetValue<string>("ShortName", (string)null);
        this.Isin = storage.GetValue<string>("Isin", (string)null);
        this.Asset = storage.GetValue<string>("Asset", (string)null);
        this.Type = storage.GetValue<SecurityTypes?>("Type", new SecurityTypes?());
        this.IssueDate = storage.GetValue<DateTime?>("IssueDate", new DateTime?());
        this.IssueSize = storage.GetValue<Decimal?>("IssueSize", new Decimal?());
        this.LastDate = storage.GetValue<DateTime?>("LastDate", new DateTime?());
        this.Decimals = storage.GetValue<int?>("Decimals", new int?());
        this.Multiplier = storage.GetValue<Decimal?>("Multiplier", new Decimal?());
        this.PriceStep = storage.GetValue<Decimal?>("PriceStep", new Decimal?());
        this.Currency = storage.GetValue<CurrencyTypes?>("Currency", new CurrencyTypes?());
        this.SettleDate = storage.GetValue<DateTime?>("SettleDate", new DateTime?());
        this.FinamSecurityId = storage.GetValue<long?>("FinamSecurityId", new long?());
        this.FinamMarketId = storage.GetValue<long?>("FinamMarketId", new long?());
        this.AllowBacktest = storage.GetValue<bool?>("AllowBacktest", new bool?());
        this.AllowLive = storage.GetValue<bool?>("AllowLive", new bool?());
        this.DataTypes = storage.GetValue<BaseEntitySet<InstrumentDataType>>("DataTypes", (BaseEntitySet<InstrumentDataType>)null);
        this.Permissions = storage.GetValue<BaseEntitySet<DataPermission>>("Permissions", (BaseEntitySet<DataPermission>)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<string>("Code", this.Code).Set<string>("Board", this.Board).Set<string>("Name", this.Name).Set<string>("ShortName", this.ShortName).Set<string>("Isin", this.Isin).Set<string>("Asset", this.Asset).Set<SecurityTypes?>("Type", this.Type).Set<DateTime?>("IssueDate", this.IssueDate).Set<Decimal?>("IssueSize", this.IssueSize).Set<DateTime?>("LastDate", this.LastDate).Set<int?>("Decimals", this.Decimals).Set<Decimal?>("Multiplier", this.Multiplier).Set<Decimal?>("PriceStep", this.PriceStep).Set<CurrencyTypes?>("Currency", this.Currency).Set<DateTime?>("SettleDate", this.SettleDate).Set<long?>("FinamSecurityId", this.FinamSecurityId).Set<long?>("FinamMarketId", this.FinamMarketId).Set<bool?>("AllowBacktest", this.AllowBacktest).Set<bool?>("AllowLive", this.AllowLive).Set<BaseEntitySet<InstrumentDataType>>("DataTypes", this.DataTypes).Set<BaseEntitySet<DataPermission>>("Permissions", this.Permissions);
    }
}
