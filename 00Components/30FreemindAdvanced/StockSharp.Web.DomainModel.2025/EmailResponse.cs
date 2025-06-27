using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class EmailResponse : BaseResponse
    {
        public EmailResponse()
          : base( SubscriptionTypes.Email )
        {
        }

        public bool IsCancelled { get; set; }

        public EmailBulk Bulk { get; set; }

        public Client [ ] Clients { get; set; }

        public string Html { get; set; }

        public string Text { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.IsCancelled = ( bool ) storage.GetValue<bool>( "IsCancelled", false );
            this.Bulk = ( EmailBulk ) storage.GetValue<EmailBulk>( "Bulk", null );
            this.Clients = ( Client [ ] ) storage.GetValue<Client [ ]>( "Clients", null );
            this.Html = ( string ) storage.GetValue<string>( "Html", null );
            this.Text = ( string ) storage.GetValue<string>( "Text", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );

            throw new System.Exception();
            //storage.Set<bool>( "IsCancelled", this.IsCancelled.Set<EmailBulk>( "Bulk", this.Bulk ).Set<Client [ ]>( "Clients", this.Clients ).Set<string>( "Html", this.Html ).Set<string>( "Text", this.Text );
        }
    }
}
