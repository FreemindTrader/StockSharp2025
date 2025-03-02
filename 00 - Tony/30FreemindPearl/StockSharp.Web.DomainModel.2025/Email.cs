// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.Email
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class Email : BaseEntity, INameEntity
    {
        public string FromAddress { get; set; }

        public string FromAlias { get; set; }

        public string ToAddress { get; set; }

        public string ToAlias { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public string Html { get; set; }

        public int ErrorCount { get; set; }

        public BaseEntitySet<File> Files { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.FromAddress = ( string ) storage.GetValue<string>( "FromAddress", null );
            this.FromAlias = ( string ) storage.GetValue<string>( "FromAlias", null );
            this.ToAddress = ( string ) storage.GetValue<string>( "ToAddress", null );
            this.ToAlias = ( string ) storage.GetValue<string>( "ToAlias", null );
            this.Name = ( string ) storage.GetValue<string>( "Name", null );
            this.Text = ( string ) storage.GetValue<string>( "Text", null );
            this.Html = ( string ) storage.GetValue<string>( "Html", null );
            this.ErrorCount = ( int ) storage.GetValue<int>( "ErrorCount", 0 );
            this.Files = ( BaseEntitySet<File> ) storage.GetValue<BaseEntitySet<File>>( "Files", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<string>( "FromAddress", this.FromAddress ).Set<string>( "FromAlias", this.FromAlias ).Set<string>( "ToAddress", this.ToAddress ).Set<string>( "ToAlias", this.ToAlias ).Set<string>( "Name", this.Name ).Set<string>( "Text", this.Text ).Set<string>( "Html", this.Html ).Set<int>( "ErrorCount", this.ErrorCount ).Set<BaseEntitySet<File>>( "Files", this.Files );
        }
    }
}
