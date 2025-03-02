// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.DiagramSocket
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class DiagramSocket : BaseEntity, INameEntity, IUserIdEntity
    {
        public string Name { get; set; }

        public string UserId { get; set; }

        public bool? Direction { get; set; }

        public int? MaxLinks { get; set; }

        public DiagramElement Element { get; set; }

        public DiagramSocketType Type { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Name = ( string ) storage.GetValue<string>( "Name", null );
            this.UserId = ( string ) storage.GetValue<string>( "UserId", null );
            this.Direction = ( bool? ) storage.GetValue<bool?>( "Direction", new bool?() );
            this.MaxLinks = ( int? ) storage.GetValue<int?>( "MaxLinks", new int?() );
            this.Element = ( DiagramElement ) storage.GetValue<DiagramElement>( "Element", null );
            this.Type = ( DiagramSocketType ) storage.GetValue<DiagramSocketType>( "Type", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<string>( "Name", this.Name ).Set<string>( "UserId", this.UserId ).Set<bool?>( "Direction", this.Direction ).Set<int?>( "MaxLinks", this.MaxLinks ).Set<DiagramElement>( "Element", this.Element ).Set<DiagramSocketType>( "Type", this.Type );
        }
    }
}
