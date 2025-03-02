// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.SystemMessage
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public class SystemMessage : Message
    {
        public string Source { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Source = ( string ) storage.GetValue<string>( "Source", null );
            this.ExpiryDate = ( DateTime? ) storage.GetValue<DateTime?>( "ExpiryDate", new DateTime?() );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<string>( "Source", this.Source ).Set<DateTime?>( "ExpiryDate", this.ExpiryDate );
        }
    }
}
