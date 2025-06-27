// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.Domain
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Common;
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class Domain : BaseEntity, INameEntity
    {
        public string Code { get; set; }

        public string Culture { get; set; }

        public CurrencyTypes Currency { get; set; }

        public int YandexCounter { get; set; }

        public string GoogleCounter { get; set; }

        public int VKCounter { get; set; }

        public string Phone { get; set; }

        public string Messenger { get; set; }

        public FileGroup SiteMap { get; set; }

        public string Pluso { get; set; }

        public Topic Error403 { get; set; }

        public Topic Error404 { get; set; }

        public bool YandexChat { get; set; }

        public File Yml { get; set; }

        public Domain PriceSource { get; set; }

        public string GoogleSearch { get; set; }

        string INameEntity.Name
        {
            get
            {
                return this.Code;
            }
            set
            {
                this.Code = value;
            }
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Code = ( string ) storage.GetValue<string>( "Code", null );
            this.Culture = ( string ) storage.GetValue<string>( "Culture", null );
            this.Currency = ( CurrencyTypes ) storage.GetValue<CurrencyTypes>( "Currency", 0 );
            this.YandexCounter = ( int ) storage.GetValue<int>( "YandexCounter", 0 );
            this.GoogleCounter = ( string ) storage.GetValue<string>( "GoogleCounter", null );
            this.VKCounter = ( int ) storage.GetValue<int>( "VKCounter", 0 );
            this.Phone = ( string ) storage.GetValue<string>( "Phone", null );
            this.Messenger = ( string ) storage.GetValue<string>( "Messenger", null );
            this.SiteMap = ( FileGroup ) storage.GetValue<FileGroup>( "SiteMap", null );
            this.Pluso = ( string ) storage.GetValue<string>( "Pluso", null );
            this.Error403 = ( Topic ) storage.GetValue<Topic>( "Error403", null );
            this.Error404 = ( Topic ) storage.GetValue<Topic>( "Error404", null );
            this.YandexChat = ( bool ) storage.GetValue<bool>( "YandexChat", false );
            this.Yml = ( File ) storage.GetValue<File>( "Yml", null );
            this.PriceSource = ( Domain ) storage.GetValue<Domain>( "PriceSource", null );
            this.GoogleSearch = ( string ) storage.GetValue<string>( "GoogleSearch", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<string>( "Code", this.Code ).Set<string>( "Culture", this.Culture ).Set<CurrencyTypes>( "Currency", this.Currency ).Set<int>( "YandexCounter", this.YandexCounter ).Set<string>( "GoogleCounter", this.GoogleCounter ).Set<int>( "VKCounter", this.VKCounter ).Set<string>( "Phone", this.Phone ).Set<string>( "Messenger", this.Messenger ).Set<FileGroup>( "SiteMap", this.SiteMap ).Set<string>( "Pluso", this.Pluso ).Set<Topic>( "Error403", this.Error403 ).Set<Topic>( "Error404", this.Error404 ).Set<bool>( "YandexChat", ( this.YandexChat ) ).Set<File>( "Yml", this.Yml ).Set<Domain>( "PriceSource", this.PriceSource ).Set<string>( "GoogleSearch", this.GoogleSearch );
        }
    }
}
