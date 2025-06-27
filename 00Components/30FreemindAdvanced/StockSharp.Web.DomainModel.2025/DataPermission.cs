// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.DataPermission
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;
using StockSharp.Messages;
using System;

namespace StockSharp.Web.DomainModel
{
    public class DataPermission : BaseEntity, IClientEntity, IInstrumentEntity
    {
        public Client Client { get; set; }

        public InstrumentInfo Security { get; set; }

        public UserPermissions Value { get; set; }

        public DateTime Begin { get; set; }

        public DateTime End { get; set; }

        public DataType DataType { get; set; }

        public bool IsDownload { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Client = ( Client ) storage.GetValue<Client>( "Client", null );
            this.Security = ( InstrumentInfo ) storage.GetValue<InstrumentInfo>( "Security", null );
            this.Value = ( UserPermissions ) storage.GetValue<UserPermissions>( "Value", 0 );
            this.Begin = ( DateTime ) storage.GetValue<DateTime>( "Begin", new DateTime() );
            this.End = ( DateTime ) storage.GetValue<DateTime>( "End", new DateTime() );
            this.DataType = ( DataType ) storage.GetValue<DataType>( "DataType", null );
            this.IsDownload = ( bool ) storage.GetValue<bool>( "IsDownload", false );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<Client>( "Client", this.Client ).Set<InstrumentInfo>( "Security", this.Security ).Set<UserPermissions>( "Value", this.Value ).Set<DateTime>( "Begin", this.Begin ).Set<DateTime>( "End", this.End ).Set<DataType>( "DataType", this.DataType ).Set<bool>( "IsDownload", ( this.IsDownload  ) );
        }
    }
}
