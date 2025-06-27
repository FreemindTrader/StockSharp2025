// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.Session
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class Session : BaseEntity, IClientEntity, IProductEntity
    {
        public Product Product { get; set; }

        public Client Client { get; set; }

        public bool Logout { get; set; }

        public string Version { get; set; }

        public int? Count { get; set; }

        public int? AverageUpTimeMinutes { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Product = ( Product ) storage.GetValue<Product>( "Product", null );
            this.Client = ( Client ) storage.GetValue<Client>( "Client", null );
            this.Logout = ( bool ) storage.GetValue<bool>( "Logout", false );
            this.Version = ( string ) storage.GetValue<string>( "Version", null );
            this.Count = ( int? ) storage.GetValue<int?>( "Count", new int?() );
            this.AverageUpTimeMinutes = ( int? ) storage.GetValue<int?>( "AverageUpTimeMinutes", new int?() );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<Product>( "Product", this.Product ).Set<Client>( "Client", this.Client ).Set<bool>( "Logout", ( this.Logout  ) ).Set<string>( "Version", this.Version ).Set<int?>( "Count", this.Count ).Set<int?>( "AverageUpTimeMinutes", this.AverageUpTimeMinutes );
        }
    }
}
