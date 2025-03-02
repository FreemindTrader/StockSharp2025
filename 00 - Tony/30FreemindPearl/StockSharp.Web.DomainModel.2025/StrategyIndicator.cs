// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyIndicator
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class StrategyIndicator : IUserIdEntity, INameEntity, IPersistable
    {
        public string Name { get; set; }

        public string IndicatorName { get; set; }

        public string TypeName { get; set; }

        public string UserId { get; set; }

        public StrategyIndicatorParam [ ] Params { get; set; }

        public virtual void Load( SettingsStorage storage )
        {
            this.Name = ( string ) storage.GetValue<string>( "Name", null );
            this.IndicatorName = ( string ) storage.GetValue<string>( "IndicatorName", null );
            this.TypeName = ( string ) storage.GetValue<string>( "TypeName", null );
            this.UserId = ( string ) storage.GetValue<string>( "UserId", null );
            this.Params = ( StrategyIndicatorParam [ ] ) storage.GetValue<StrategyIndicatorParam [ ]>( "Params", null );
        }

        public virtual void Save( SettingsStorage storage )
        {
            storage.Set<string>( "Name", this.Name ).Set<string>( "IndicatorName", this.IndicatorName ).Set<string>( "TypeName", this.TypeName ).Set<string>( "UserId", this.UserId ).Set<StrategyIndicatorParam [ ]>( "Params", this.Params );
        }
    }
}
