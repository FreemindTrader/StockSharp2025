// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ProductNugetInfo
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class ProductNugetInfo : IPersistable
{
    public int DownloadsCount { get; set; }

    public DateTime LastTime { get; set; }

    public string LastVersion { get; set; }

    public string[] Versions { get; set; }

    public PackageRepositories Repository { get; set; }

    public virtual void Load(SettingsStorage storage)
    {
        this.DownloadsCount = storage.GetValue<int>("DownloadsCount", 0);
        this.LastTime = storage.GetValue<DateTime>("LastTime", new DateTime());
        this.LastVersion = storage.GetValue<string>("LastVersion", (string)null);
        this.Versions = storage.GetValue<string[]>("Versions", (string[])null);
        this.Repository = storage.GetValue<PackageRepositories>("Repository", PackageRepositories.NuGet);
    }

    public virtual void Save(SettingsStorage storage)
    {
        storage.Set<int>("DownloadsCount", this.DownloadsCount).Set<DateTime>("LastTime", this.LastTime).Set<string>("LastVersion", this.LastVersion).Set<string[]>("Versions", this.Versions).Set<PackageRepositories>("Repository", this.Repository);
    }
}
