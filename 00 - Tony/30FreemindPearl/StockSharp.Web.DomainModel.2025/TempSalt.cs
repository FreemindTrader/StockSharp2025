// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.TempSalt
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class TempSalt : BaseEntity
    {
        public byte [ ] Salt { get; set; }

        public byte [ ] IV { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Salt = ( byte [ ] ) storage.GetValue<byte [ ]>( "Salt", null );
            this.IV = ( byte [ ] ) storage.GetValue<byte [ ]>( "IV", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<byte [ ]>( "Salt", this.Salt ).Set<byte [ ]>( "IV", this.IV );
        }
    }
}
