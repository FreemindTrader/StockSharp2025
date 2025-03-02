// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.PublishDetails
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public class PublishDetails : IPersistable
    {
        public TimeSpan Timeout { get; set; }

        public PublishProject [ ] Projects { get; set; }

        public bool IsInteract { get; set; }

        public bool IsSources { get; set; }

        public bool IsChangesOnly { get; set; }

        public string PreReleaseSuffix { get; set; }

        public bool IsUnitTests { get; set; }

        public bool IsPush { get; set; }

        public virtual void Load( SettingsStorage storage )
        {
            this.Timeout = ( TimeSpan ) storage.GetValue<TimeSpan>( "Timeout", new TimeSpan() );
            this.Projects = ( PublishProject [ ] ) storage.GetValue<PublishProject [ ]>( "Projects", null );
            this.IsInteract = ( bool ) storage.GetValue<bool>( "IsInteract", false );
            this.IsSources = ( bool ) storage.GetValue<bool>( "IsSources", false );
            this.IsChangesOnly = ( bool ) storage.GetValue<bool>( "IsChangesOnly", false );
            this.PreReleaseSuffix = ( string ) storage.GetValue<string>( "PreReleaseSuffix", null );
            this.IsUnitTests = ( bool ) storage.GetValue<bool>( "IsUnitTests", false );
            this.IsPush = ( bool ) storage.GetValue<bool>( "IsPush", false );
        }

        public virtual void Save( SettingsStorage storage )
        {
            storage.Set<TimeSpan>( "Timeout", this.Timeout ).Set<PublishProject [ ]>( "Projects", this.Projects ).Set<bool>( "IsInteract", ( this.IsInteract ) ).Set<bool>( "IsSources", ( this.IsSources ) ).Set<bool>( "IsChangesOnly", ( this.IsChangesOnly  ) ).Set<string>( "PreReleaseSuffix", this.PreReleaseSuffix ).Set<bool>( "IsUnitTests", ( this.IsUnitTests ) ).Set<bool>( "IsPush", ( this.IsPush ) );
        }
    }
}
