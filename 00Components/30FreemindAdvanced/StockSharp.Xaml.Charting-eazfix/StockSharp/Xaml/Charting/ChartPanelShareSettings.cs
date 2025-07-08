// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartPanelShareSettings
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Localization;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

#nullable disable
namespace StockSharp.Xaml.Charting;

public sealed class ChartPanelShareSettings : NotifiableObject, IPersistable
{
  
  private string \u0023\u003DzPyzhV2NFLw\u0024q = $"Chart_{DateTime.Now:yyyyMMdd_HHmmssfff}.png";
  
  private TimeSpan \u0023\u003DzDra\u0024AUHGVB33 = TimeSpan.FromMinutes(5.0);
  
  private bool \u0023\u003Dz9amTLwDdTF0z;
  
  private bool \u0023\u003DzU8xSjuFGDDJuii5WcA\u003D\u003D;

  [Display(ResourceType = typeof (LocalizedStrings), Name = "AutoPublish", Description = "AutoPublishDesc", GroupName = "General", Order = 10)]
  public bool IsEnabled
  {
    get => this.\u0023\u003Dz9amTLwDdTF0z;
    set
    {
      if (this.\u0023\u003Dz9amTLwDdTF0z == value)
        return;
      this.\u0023\u003Dz9amTLwDdTF0z = value;
      this.NotifyChanged(nameof (IsEnabled));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "Period", Description = "ChartPublishPeriod", GroupName = "General", Order = 20)]
  [TimeSpanEditor]
  public TimeSpan Period
  {
    get => this.\u0023\u003DzDra\u0024AUHGVB33;
    set
    {
      if (this.\u0023\u003DzDra\u0024AUHGVB33 <= TimeSpan.Zero)
        throw new ArgumentOutOfRangeException(nameof (value), LocalizedStrings.InvalidValue);
      if (this.\u0023\u003DzDra\u0024AUHGVB33 == value)
        return;
      this.\u0023\u003DzDra\u0024AUHGVB33 = value;
      this.NotifyChanged(nameof (Period));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "FileName", Description = "ImageCloudFileName", GroupName = "General", Order = 30)]
  public string FileName
  {
    get => this.\u0023\u003DzPyzhV2NFLw\u0024q;
    set
    {
      if (this.\u0023\u003DzPyzhV2NFLw\u0024q == value)
        return;
      this.Published = false;
      this.\u0023\u003DzPyzhV2NFLw\u0024q = value;
      this.NotifyChanged(nameof (FileName));
    }
  }

  [Browsable(false)]
  public bool Published
  {
    get => this.\u0023\u003DzU8xSjuFGDDJuii5WcA\u003D\u003D;
    set
    {
      if (this.\u0023\u003DzU8xSjuFGDDJuii5WcA\u003D\u003D == value)
        return;
      this.\u0023\u003DzU8xSjuFGDDJuii5WcA\u003D\u003D = value;
      this.NotifyChanged(nameof (Published));
    }
  }

  public void Load(SettingsStorage storage)
  {
    this.Period = storage.GetValue<TimeSpan>("Period", this.Period);
    this.FileName = storage.GetValue<string>("FileName", this.FileName);
    this.Published = storage.GetValue<bool>("Published", this.Published);
    this.IsEnabled = storage.GetValue<bool>("IsEnabled", this.IsEnabled);
  }

  public void Save(SettingsStorage storage)
  {
    storage.SetValue<bool>("IsEnabled", this.IsEnabled);
    storage.SetValue<TimeSpan>("Period", this.Period);
    storage.SetValue<string>("FileName", this.FileName);
    storage.SetValue<bool>("Published", this.Published);
  }
}
