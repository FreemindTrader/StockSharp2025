// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.PublishProject
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class PublishProject : INameEntity, IPersistable
{
    public string Name { get; set; }

    public string PackageId { get; set; }

    public string ReleaseNotes { get; set; }

    public string Current { get; set; }

    public string Planned { get; set; }

    public bool IsPrivate { get; set; }

    public bool IsSources { get; set; }

    public string[] ReferencedProjects { get; set; }

    public string[] ReferencedByProjects { get; set; }

    public string CsprojPath { get; set; }

    public string Commit { get; set; }

    public bool IsChanged { get; set; }

    public virtual void Load(SettingsStorage storage)
    {
        this.Name = storage.GetValue<string>("Name", (string)null);
        this.PackageId = storage.GetValue<string>("PackageId", (string)null);
        this.ReleaseNotes = storage.GetValue<string>("ReleaseNotes", (string)null);
        this.Current = storage.GetValue<string>("Current", (string)null);
        this.Planned = storage.GetValue<string>("Planned", (string)null);
        this.IsPrivate = storage.GetValue<bool>("IsPrivate", false);
        this.IsSources = storage.GetValue<bool>("IsSources", false);
        this.ReferencedProjects = storage.GetValue<string[]>("ReferencedProjects", (string[])null);
        this.ReferencedByProjects = storage.GetValue<string[]>("ReferencedByProjects", (string[])null);
        this.CsprojPath = storage.GetValue<string>("CsprojPath", (string)null);
        this.Commit = storage.GetValue<string>("Commit", (string)null);
        this.IsChanged = storage.GetValue<bool>("IsChanged", false);
    }

    public virtual void Save(SettingsStorage storage)
    {
        storage.Set<string>("Name", this.Name).Set<string>("PackageId", this.PackageId).Set<string>("ReleaseNotes", this.ReleaseNotes).Set<string>("Current", this.Current).Set<string>("Planned", this.Planned).Set<bool>("IsPrivate", this.IsPrivate).Set<bool>("IsSources", this.IsSources).Set<string[]>("ReferencedProjects", this.ReferencedProjects).Set<string[]>("ReferencedByProjects", this.ReferencedByProjects).Set<string>("CsprojPath", this.CsprojPath).Set<string>("Commit", this.Commit).Set<bool>("IsChanged", this.IsChanged);
    }
}
