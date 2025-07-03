// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartPanelOrderSettings
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

#nullable disable
namespace StockSharp.Xaml.Charting;

public class ChartPanelOrderSettings : NotifiableObject, IPersistable
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Security \u0023\u003DzpaXnuR8\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Portfolio \u0023\u003Dz5M\u00246riNP6jrVzScIBQ\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Decimal \u0023\u003DzhqjqeGluToRP = 1M;

  [Display(ResourceType = typeof (LocalizedStrings), Name = "Security", Description = "ChartPanelSecurity", GroupName = "General", Order = 1)]
  public Security Security
  {
    get => this.\u0023\u003DzpaXnuR8\u003D;
    set
    {
      if (this.\u0023\u003DzpaXnuR8\u003D == value)
        return;
      this.\u0023\u003DzpaXnuR8\u003D = value;
      this.NotifyChanged(nameof (Security));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "Portfolio", Description = "ChartPanelPortfolio", GroupName = "General", Order = 2)]
  public Portfolio Portfolio
  {
    get => this.\u0023\u003Dz5M\u00246riNP6jrVzScIBQ\u003D\u003D;
    set
    {
      if (this.\u0023\u003Dz5M\u00246riNP6jrVzScIBQ\u003D\u003D == value)
        return;
      this.\u0023\u003Dz5M\u00246riNP6jrVzScIBQ\u003D\u003D = value;
      this.NotifyChanged(nameof (Portfolio));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "Volume", Description = "OrderVolume", GroupName = "General", Order = 3)]
  public Decimal Volume
  {
    get => this.\u0023\u003DzhqjqeGluToRP;
    set
    {
      this.\u0023\u003DzhqjqeGluToRP = !(value <= 0M) ? value : throw new ArgumentOutOfRangeException(nameof (value), (object) value, LocalizedStrings.InvalidValue);
      this.NotifyChanged(nameof (Volume));
    }
  }

  public void Load(SettingsStorage storage)
  {
    ISecurityProvider securityProvider = ServicesRegistry.TrySecurityProvider;
    if (securityProvider != null)
    {
      string id = storage.GetValue<string>("Security", (string) null);
      if (!StringHelper.IsEmpty(id))
        this.Security = securityProvider.LookupById(id);
    }
    IPortfolioProvider portfolioProvider = ServicesRegistry.TryPortfolioProvider;
    if (portfolioProvider != null)
    {
      string name = storage.GetValue<string>("Portfolio", (string) null);
      if (!StringHelper.IsEmpty(name))
        this.Portfolio = portfolioProvider.LookupByPortfolioName(name);
    }
    Decimal num = storage.GetValue<Decimal>("Volume", this.Volume);
    if (num <= 0M)
      num = 1M;
    this.Volume = num;
  }

  public void Save(SettingsStorage storage)
  {
    storage.SetValue<string>("Security", this.Security?.Id);
    storage.SetValue<string>("Portfolio", this.Portfolio?.Name);
    storage.SetValue<Decimal>("Volume", this.Volume);
  }
}
