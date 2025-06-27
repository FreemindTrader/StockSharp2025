// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyParam
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class StrategyParam : BaseEntity, INameEntity, IStrategyEntity, IUserIdEntity
    {
        public string Name { get; set; }

        public string UserId { get; set; }

        public Strategy Strategy { get; set; }

        public string Value { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string Step { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Name = ( string ) storage.GetValue<string>( "Name", null );
            this.UserId = ( string ) storage.GetValue<string>( "UserId", null );
            this.Strategy = ( Strategy ) storage.GetValue<Strategy>( "Strategy", null );
            this.Value = ( string ) storage.GetValue<string>( "Value", null );
            this.From = ( string ) storage.GetValue<string>( "From", null );
            this.To = ( string ) storage.GetValue<string>( "To", null );
            this.Step = ( string ) storage.GetValue<string>( "Step", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<string>( "Name", this.Name ).Set<string>( "UserId", this.UserId ).Set<Strategy>( "Strategy", this.Strategy ).Set<string>( "Value", this.Value ).Set<string>( "From", this.From ).Set<string>( "To", this.To ).Set<string>( "Step", this.Step );
        }
    }
}
