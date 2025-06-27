// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.InstrumentDataType
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public class InstrumentDataType : BaseEntity, IInstrumentEntity
    {
        public InstrumentInfo Security { get; set; }

        public DataType DataType { get; set; }

        public DateTime Begin { get; set; }

        public DateTime End { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Security = ( InstrumentInfo ) storage.GetValue<InstrumentInfo>( "Security", null );
            this.DataType = ( DataType ) storage.GetValue<DataType>( "DataType", null );
            this.Begin = ( DateTime ) storage.GetValue<DateTime>( "Begin", new DateTime() );
            this.End = ( DateTime ) storage.GetValue<DateTime>( "End", new DateTime() );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<InstrumentInfo>( "Security", this.Security ).Set<DataType>( "DataType", this.DataType ).Set<DateTime>( "Begin", this.Begin ).Set<DateTime>( "End", this.End );
        }
    }
}
