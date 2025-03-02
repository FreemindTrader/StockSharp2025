// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.DataType
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class DataType : BaseEntity, INameEntity
    {
        public string Name { get; set; }

        public DataTypes Type { get; set; }

        public long Value { get; set; }

        public BaseEntitySet<InstrumentDataType> InstrumentDataTypes { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Name = ( string ) storage.GetValue<string>( "Name", null );
            this.Type = ( DataTypes ) storage.GetValue<DataTypes>( "Type", 0 );
            this.Value = ( long ) storage.GetValue<long>( "Value", 0L );
            this.InstrumentDataTypes = ( BaseEntitySet<InstrumentDataType> ) storage.GetValue<BaseEntitySet<InstrumentDataType>>( "InstrumentDataTypes", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<string>( "Name", this.Name ).Set<DataTypes>( "Type", this.Type ).Set<long>( "Value", this.Value ).Set<BaseEntitySet<InstrumentDataType>>( "InstrumentDataTypes", this.InstrumentDataTypes );
        }
    }
}
