// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ProductNugetInfo
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public class ProductNugetInfo : IPersistable
    {
        public int DownloadsCount { get; set; }

        public DateTime LastTime { get; set; }

        public string LastVersion { get; set; }

        public string [ ] Versions { get; set; }

        public PackageRepositories Repository { get; set; }

        public virtual void Load( SettingsStorage storage )
        {
            this.DownloadsCount = ( int ) storage.GetValue<int>( "DownloadsCount", 0 );
            this.LastTime = ( DateTime ) storage.GetValue<DateTime>( "LastTime", new DateTime() );
            this.LastVersion = ( string ) storage.GetValue<string>( "LastVersion", null );
            this.Versions = ( string [ ] ) storage.GetValue<string [ ]>( "Versions", null );
            this.Repository = ( PackageRepositories ) storage.GetValue<PackageRepositories>( "Repository", 0 );
        }

        public virtual void Save( SettingsStorage storage )
        {
            storage.Set<int>( "DownloadsCount", this.DownloadsCount ).Set<DateTime>( "LastTime", this.LastTime ).Set<string>( "LastVersion", this.LastVersion ).Set<string [ ]>( "Versions", this.Versions ).Set<PackageRepositories>( "Repository", this.Repository );
        }
    }
}
