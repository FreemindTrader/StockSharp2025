// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.DiagramSocketType
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class DiagramSocketType : BaseEntity, INameEntity
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public string Color { get; set; }

        public BaseEntitySet<DiagramSocket> Sockets { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Name = ( string ) storage.GetValue<string>( "Name", null );
            this.Type = ( string ) storage.GetValue<string>( "Type", null );
            this.Color = ( string ) storage.GetValue<string>( "Color", null );
            this.Sockets = ( BaseEntitySet<DiagramSocket> ) storage.GetValue<BaseEntitySet<DiagramSocket>>( "Sockets", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<string>( "Name", this.Name ).Set<string>( "Type", this.Type ).Set<string>( "Color", this.Color ).Set<BaseEntitySet<DiagramSocket>>( "Sockets", this.Sockets );
        }
    }
}
