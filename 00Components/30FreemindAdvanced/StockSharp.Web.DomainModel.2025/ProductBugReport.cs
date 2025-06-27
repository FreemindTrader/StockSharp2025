// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ProductBugReport
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class ProductBugReport : BaseEntity, IClientEntity, IProductEntity, IMessageEntity
    {
        public Product Product { get; set; }

        public string Lang { get; set; }

        public string Framework { get; set; }

        public string Version { get; set; }

        public string SystemInfo { get; set; }

        public Client Client { get; set; }

        public Message Message { get; set; }

        public Error Error { get; set; }

        public int? Priority { get; set; }

        public int? Count { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Product = ( Product ) storage.GetValue<Product>( "Product", null );
            this.Lang = ( string ) storage.GetValue<string>( "Lang", null );
            this.Framework = ( string ) storage.GetValue<string>( "Framework", null );
            this.Version = ( string ) storage.GetValue<string>( "Version", null );
            this.SystemInfo = ( string ) storage.GetValue<string>( "SystemInfo", null );
            this.Client = ( Client ) storage.GetValue<Client>( "Client", null );
            this.Message = ( Message ) storage.GetValue<Message>( "Message", null );
            this.Error = ( Error ) storage.GetValue<Error>( "Error", null );
            this.Priority = ( int? ) storage.GetValue<int?>( "Priority", new int?() );
            this.Count = ( int? ) storage.GetValue<int?>( "Count", new int?() );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<Product>( "Product", this.Product ).Set<string>( "Lang", this.Lang ).Set<string>( "Framework", this.Framework ).Set<string>( "Version", this.Version ).Set<string>( "SystemInfo", this.SystemInfo ).Set<Client>( "Client", this.Client ).Set<Message>( "Message", this.Message ).Set<Error>( "Error", this.Error ).Set<int?>( "Priority", this.Priority ).Set<int?>( "Count", this.Count );
        }
    }
}
