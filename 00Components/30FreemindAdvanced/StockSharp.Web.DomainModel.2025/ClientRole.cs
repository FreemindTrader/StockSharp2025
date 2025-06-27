// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ClientRole
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public class ClientRole : BaseEntity
    {
        public Client Role { get; set; }

        public Product OneApp { get; set; }

        public DateTime? Till { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Role = ( Client ) storage.GetValue<Client>( "Role", null );
            this.OneApp = ( Product ) storage.GetValue<Product>( "OneApp", null );
            this.Till = ( DateTime? ) storage.GetValue<DateTime?>( "Till", new DateTime?() );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<Client>( "Role", this.Role ).Set<Product>( "OneApp", this.OneApp ).Set<DateTime?>( "Till", this.Till );
        }
    }
}
