// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.InstrumentInfo
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Common;
using Ecng.Serialization;
using StockSharp.Messages;
using System;

namespace StockSharp.Web.DomainModel
{
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
            get
            {
                return this.Code + "@" + this.Board;
            }
            set
            {
                this.Code = value;
            }
        }

        string IDescriptionEntity.Description
        {
            get
            {
                return this.Name;
            }
            set
            {
                this.Name = value;
            }
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Code = ( string ) storage.GetValue<string>( "Code", null );
            this.Board = ( string ) storage.GetValue<string>( "Board", null );
            this.Name = ( string ) storage.GetValue<string>( "Name", null );
            this.ShortName = ( string ) storage.GetValue<string>( "ShortName", null );
            this.Isin = ( string ) storage.GetValue<string>( "Isin", null );
            this.Asset = ( string ) storage.GetValue<string>( "Asset", null );
            this.Type = ( SecurityTypes? ) storage.GetValue<SecurityTypes?>( "Type", new SecurityTypes?() );
            this.IssueDate = ( DateTime? ) storage.GetValue<DateTime?>( "IssueDate", new DateTime?() );
            this.IssueSize = ( Decimal? ) storage.GetValue<Decimal?>( "IssueSize", new Decimal?() );
            this.LastDate = ( DateTime? ) storage.GetValue<DateTime?>( "LastDate", new DateTime?() );
            this.Decimals = ( int? ) storage.GetValue<int?>( "Decimals", new int?() );
            this.Multiplier = ( Decimal? ) storage.GetValue<Decimal?>( "Multiplier", new Decimal?() );
            this.PriceStep = ( Decimal? ) storage.GetValue<Decimal?>( "PriceStep", new Decimal?() );
            this.Currency = ( CurrencyTypes? ) storage.GetValue<CurrencyTypes?>( "Currency", new CurrencyTypes?() );
            this.SettleDate = ( DateTime? ) storage.GetValue<DateTime?>( "SettleDate", new DateTime?() );
            this.FinamSecurityId = ( long? ) storage.GetValue<long?>( "FinamSecurityId", new long?() );
            this.FinamMarketId = ( long? ) storage.GetValue<long?>( "FinamMarketId", new long?() );
            this.AllowBacktest = ( bool? ) storage.GetValue<bool?>( "AllowBacktest", new bool?() );
            this.AllowLive = ( bool? ) storage.GetValue<bool?>( "AllowLive", new bool?() );
            this.DataTypes = ( BaseEntitySet<InstrumentDataType> ) storage.GetValue<BaseEntitySet<InstrumentDataType>>( "DataTypes", null );
            this.Permissions = ( BaseEntitySet<DataPermission> ) storage.GetValue<BaseEntitySet<DataPermission>>( "Permissions", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<string>( "Code", this.Code ).Set<string>( "Board", this.Board ).Set<string>( "Name", this.Name ).Set<string>( "ShortName", this.ShortName ).Set<string>( "Isin", this.Isin ).Set<string>( "Asset", this.Asset ).Set<SecurityTypes?>( "Type", this.Type ).Set<DateTime?>( "IssueDate", this.IssueDate ).Set<Decimal?>( "IssueSize", this.IssueSize ).Set<DateTime?>( "LastDate", this.LastDate ).Set<int?>( "Decimals", this.Decimals ).Set<Decimal?>( "Multiplier", this.Multiplier ).Set<Decimal?>( "PriceStep", this.PriceStep ).Set<CurrencyTypes?>( "Currency", this.Currency ).Set<DateTime?>( "SettleDate", this.SettleDate ).Set<long?>( "FinamSecurityId", this.FinamSecurityId ).Set<long?>( "FinamMarketId", this.FinamMarketId ).Set<bool?>( "AllowBacktest", this.AllowBacktest ).Set<bool?>( "AllowLive", this.AllowLive ).Set<BaseEntitySet<InstrumentDataType>>( "DataTypes", this.DataTypes ).Set<BaseEntitySet<DataPermission>>( "Permissions", this.Permissions );
        }
    }
}
