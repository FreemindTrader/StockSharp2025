// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.DynamicMenuGroup
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class DynamicMenuGroup : BaseEntity, INameEntity
    {
        public string Name { get; set; }

        public string Style { get; set; }

        public BaseEntitySet<DynamicMenu> Menus { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Name = ( string ) storage.GetValue<string>( "Name", null );
            this.Style = ( string ) storage.GetValue<string>( "Style", null );
            this.Menus = ( BaseEntitySet<DynamicMenu> ) storage.GetValue<BaseEntitySet<DynamicMenu>>( "Menus", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<string>( "Name", this.Name ).Set<string>( "Style", this.Style ).Set<BaseEntitySet<DynamicMenu>>( "Menus", this.Menus );
        }
    }
}
