// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.Error
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class Error : BaseEntity, IClientEntity, INameEntity, IDescriptionEntity
    {
        public Client Client { get; set; }

        public Payment Payment { get; set; }

        public string Text { get; set; }

        public string Url { get; set; }

        public string Referer { get; set; }

        public int Priority { get; set; }

        public BaseEntitySet<ProductBugReport> ProductBugReports { get; set; }

        string INameEntity.Name
        {
            get
            {
                return this.Text;
            }
            set
            {
                this.Text = value;
            }
        }

        string IDescriptionEntity.Description
        {
            get
            {
                return this.Text;
            }
            set
            {
                this.Text = value;
            }
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Client = ( Client ) storage.GetValue<Client>( "Client", null );
            this.Payment = ( Payment ) storage.GetValue<Payment>( "Payment", null );
            this.Text = ( string ) storage.GetValue<string>( "Text", null );
            this.Url = ( string ) storage.GetValue<string>( "Url", null );
            this.Referer = ( string ) storage.GetValue<string>( "Referer", null );
            this.Priority = ( int ) storage.GetValue<int>( "Priority", 0 );
            this.ProductBugReports = ( BaseEntitySet<ProductBugReport> ) storage.GetValue<BaseEntitySet<ProductBugReport>>( "ProductBugReports", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<Client>( "Client", this.Client ).Set<Payment>( "Payment", this.Payment ).Set<string>( "Text", this.Text ).Set<string>( "Url", this.Url ).Set<string>( "Referer", this.Referer ).Set<int>( "Priority", this.Priority ).Set<BaseEntitySet<ProductBugReport>>( "ProductBugReports", this.ProductBugReports );
        }
    }
}
