// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.PublishDetails
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class PublishDetails : IPersistable
{
    public TimeSpan Timeout { get; set; }

    public PublishProject[] Projects { get; set; }

    public bool IsInteract { get; set; }

    public bool IsSources { get; set; }

    public bool IsChangesOnly { get; set; }

    public string PreReleaseSuffix { get; set; }

    public bool IsUnitTests { get; set; }

    public bool IsPush { get; set; }

    public virtual void Load(SettingsStorage storage)
    {
        this.Timeout = storage.GetValue<TimeSpan>("Timeout", new TimeSpan());
        this.Projects = storage.GetValue<PublishProject[]>("Projects", (PublishProject[])null);
        this.IsInteract = storage.GetValue<bool>("IsInteract", false);
        this.IsSources = storage.GetValue<bool>("IsSources", false);
        this.IsChangesOnly = storage.GetValue<bool>("IsChangesOnly", false);
        this.PreReleaseSuffix = storage.GetValue<string>("PreReleaseSuffix", (string)null);
        this.IsUnitTests = storage.GetValue<bool>("IsUnitTests", false);
        this.IsPush = storage.GetValue<bool>("IsPush", false);
    }

    public virtual void Save(SettingsStorage storage)
    {
        storage.Set<TimeSpan>("Timeout", this.Timeout).Set<PublishProject[]>("Projects", this.Projects).Set<bool>("IsInteract", this.IsInteract).Set<bool>("IsSources", this.IsSources).Set<bool>("IsChangesOnly", this.IsChangesOnly).Set<string>("PreReleaseSuffix", this.PreReleaseSuffix).Set<bool>("IsUnitTests", this.IsUnitTests).Set<bool>("IsPush", this.IsPush);
    }
}
