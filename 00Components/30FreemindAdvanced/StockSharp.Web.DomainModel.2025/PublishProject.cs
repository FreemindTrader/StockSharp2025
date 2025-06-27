// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.PublishProject
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class PublishProject : INameEntity, IPersistable
    {
        public string Name { get; set; }

        public string PackageId { get; set; }

        public string ReleaseNotes { get; set; }

        public string Current { get; set; }

        public string Planned { get; set; }

        public bool IsPrivate { get; set; }

        public bool IsSources { get; set; }

        public string [ ] ReferencedProjects { get; set; }

        public string [ ] ReferencedByProjects { get; set; }

        public string CsprojPath { get; set; }

        public string Commit { get; set; }

        public bool IsChanged { get; set; }

        public virtual void Load( SettingsStorage storage )
        {
            this.Name = ( string ) storage.GetValue<string>( "Name", null );
            this.PackageId = ( string ) storage.GetValue<string>( "PackageId", null );
            this.ReleaseNotes = ( string ) storage.GetValue<string>( "ReleaseNotes", null );
            this.Current = ( string ) storage.GetValue<string>( "Current", null );
            this.Planned = ( string ) storage.GetValue<string>( "Planned", null );
            this.IsPrivate = ( bool ) storage.GetValue<bool>( "IsPrivate", false );
            this.IsSources = ( bool ) storage.GetValue<bool>( "IsSources", false );
            this.ReferencedProjects = ( string [ ] ) storage.GetValue<string [ ]>( "ReferencedProjects", null );
            this.ReferencedByProjects = ( string [ ] ) storage.GetValue<string [ ]>( "ReferencedByProjects", null );
            this.CsprojPath = ( string ) storage.GetValue<string>( "CsprojPath", null );
            this.Commit = ( string ) storage.GetValue<string>( "Commit", null );
            this.IsChanged = ( bool ) storage.GetValue<bool>( "IsChanged", false );
        }

        public virtual void Save( SettingsStorage storage )
        {
            storage.Set<string>( "Name", this.Name ).Set<string>( "PackageId", this.PackageId ).Set<string>( "ReleaseNotes", this.ReleaseNotes ).Set<string>( "Current", this.Current ).Set<string>( "Planned", this.Planned ).Set<bool>( "IsPrivate", ( this.IsPrivate ) ).Set<bool>( "IsSources", ( this.IsSources ) ).Set<string [ ]>( "ReferencedProjects", this.ReferencedProjects ).Set<string [ ]>( "ReferencedByProjects", this.ReferencedByProjects ).Set<string>( "CsprojPath", this.CsprojPath ).Set<string>( "Commit", this.Commit ).Set<bool>( "IsChanged", ( this.IsChanged ) );
        }
    }
}
