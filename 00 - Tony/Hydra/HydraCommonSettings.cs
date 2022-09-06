// Decompiled with JetBrains decompiler
// Type: StockSharp.Hydra.HydraCommonSettings
// Assembly: Hydra, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3BCB1262-CD24-4283-B1AE-24E756F47247
// Assembly location: T:\00-StockSharp\Data\Hydra.dll

using Ecng.Common;
using Ecng.Serialization;
using StockSharp.Algo.Export;
using StockSharp.Localization;
using StockSharp.Studio.Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace StockSharp.Hydra
{
  internal class HydraCommonSettings : StudioCommonSettings
  {
    private TemplateTxtRegistry _templateTxtRegistry = new TemplateTxtRegistry();
    private TimeSpan? _stopTime;
    private int _emailErrorCount;

    [Display(Description = "Str2218", GroupName = "General", Name = "Str2217", Order = 10, ResourceType = typeof (LocalizedStrings))]
    public bool AutoStart { get; set; }

    [Display(Description = "Str2220", GroupName = "General", Name = "Str2219", Order = 12, ResourceType = typeof (LocalizedStrings))]
    public bool MinimizeToTray { get; set; }

    [Display(Description = "Str2223Dot", GroupName = "General", Name = "Str2223", Order = 13, ResourceType = typeof (LocalizedStrings))]
    public TimeSpan? StopTime
    {
      get
      {
        return _stopTime;
      }
      set
      {
        if (value.HasValue && (value.Value < TimeSpan.Zero || value.Value.TotalHours > 24.0))
          throw new ArgumentOutOfRangeException(nameof (value));
        _stopTime = value;
      }
    }

    [Display(Description = "Str2226", GroupName = "General", Name = "Str2225", Order = 13, ResourceType = typeof (LocalizedStrings))]
    public int EmailErrorCount
    {
      get
      {
        return _emailErrorCount;
      }
      set
      {
        if (value < 0)
          throw new ArgumentOutOfRangeException();
        _emailErrorCount = value;
      }
    }

    [Display(Description = "Str2228", GroupName = "General", Name = "Str2227", Order = 14, ResourceType = typeof (LocalizedStrings))]
    public string EmailErrorAddress { get; set; }

    [Display(Description = "TemplateDot", GroupName = "CSV", Name = "Template", Order = 30, ResourceType = typeof (LocalizedStrings))]
    public TemplateTxtRegistry TemplateTxtRegistry
    {
      get
      {
        return _templateTxtRegistry;
      }
      set
      {
        TemplateTxtRegistry templateTxtRegistry = value;
        if (templateTxtRegistry == null)
          throw new ArgumentNullException(nameof (value));
        _templateTxtRegistry = templateTxtRegistry;
      }
    }

    public override void Load(SettingsStorage storage)
    {
      base.Load(storage);
      AutoStart = storage.GetValue( "AutoStart", AutoStart);
      MinimizeToTray = storage.GetValue( "MinimizeToTray", MinimizeToTray);
      if (storage.ContainsKey("AutoStop"))
      {
        StopTime = storage.GetValue( "AutoStop", false) ? new TimeSpan?(storage.GetValue( "StopTime", 0L).To<TimeSpan>()) : new TimeSpan?();
      }
      else
      {
        long? nullable = storage.GetValue( "StopTime", new long?());
        ref long? local = ref nullable;
        StopTime = local.HasValue ? local.GetValueOrDefault().To<TimeSpan?>() : new TimeSpan?();
      }
      EmailErrorCount = storage.GetValue( "EmailErrorCount", EmailErrorCount);
      EmailErrorAddress = storage.GetValue( "EmailErrorAddress", EmailErrorAddress);
      TemplateTxtRegistry.ForceLoad( storage.ContainsKey("TemplateTxtRegistry") ? storage.GetValue<SettingsStorage>("TemplateTxtRegistry", null ) : storage);
    }

    public override void Save(SettingsStorage storage)
    {
      base.Save(storage);
      storage.SetValue( "AutoStart", AutoStart);
      storage.SetValue( "MinimizeToTray", MinimizeToTray);
      SettingsStorage settingsStorage = storage;
      TimeSpan? stopTime = StopTime;
      ref TimeSpan? local = ref stopTime;
      long? nullable = local.HasValue ? new long?(local.GetValueOrDefault().To<long>()) : new long?();
      settingsStorage.SetValue( "StopTime", nullable);
      storage.SetValue( "EmailErrorCount", EmailErrorCount);
      storage.SetValue( "EmailErrorAddress", EmailErrorAddress);
      storage.SetValue( "TemplateTxtRegistry", TemplateTxtRegistry.Save());
    }
  }
}
