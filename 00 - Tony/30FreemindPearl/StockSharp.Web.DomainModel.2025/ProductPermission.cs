// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ProductPermission
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public class ProductPermission : IPersistable
    {
        public Client Client { get; set; }

        public bool IsManager { get; set; }

        public DateTime? Till { get; set; }

        public virtual void Load( SettingsStorage storage )
        {
            this.Client = ( Client ) storage.GetValue<Client>( "Client", null );
            this.IsManager = ( bool ) storage.GetValue<bool>( "IsManager", false );
            this.Till = ( DateTime? ) storage.GetValue<DateTime?>( "Till", new DateTime?() );
        }

        public virtual void Save( SettingsStorage storage )
        {
            storage.Set<Client>( "Client", this.Client ).Set<bool>( "IsManager", ( this.IsManager ) ).Set<DateTime?>( "Till", this.Till );
        }
    }
}
