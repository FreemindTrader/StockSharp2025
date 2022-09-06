// Decompiled with JetBrains decompiler
// Type: StockSharp.Hydra.Properties.Resources
// Assembly: Hydra, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3BCB1262-CD24-4283-B1AE-24E756F47247
// Assembly location: T:\00-StockSharp\Data\Hydra.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace StockSharp.Hydra.Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (StockSharp.Hydra.Properties.Resources.resourceMan == null)
          StockSharp.Hydra.Properties.Resources.resourceMan = new ResourceManager("StockSharp.Hydra.Properties.Resources", typeof (StockSharp.Hydra.Properties.Resources).Assembly);
        return StockSharp.Hydra.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get
      {
        return StockSharp.Hydra.Properties.Resources.resourceCulture;
      }
      set
      {
        StockSharp.Hydra.Properties.Resources.resourceCulture = value;
      }
    }

    internal static string DailyHighestVolumeStrategy
    {
      get
      {
        return StockSharp.Hydra.Properties.Resources.ResourceManager.GetString(nameof (DailyHighestVolumeStrategy), StockSharp.Hydra.Properties.Resources.resourceCulture);
      }
    }

    internal static string NewAnalyticsStrategy
    {
      get
      {
        return StockSharp.Hydra.Properties.Resources.ResourceManager.GetString(nameof (NewAnalyticsStrategy), StockSharp.Hydra.Properties.Resources.resourceCulture);
      }
    }

    internal static string PriceVolumeDistributionStrategy
    {
      get
      {
        return StockSharp.Hydra.Properties.Resources.ResourceManager.GetString(nameof (PriceVolumeDistributionStrategy), StockSharp.Hydra.Properties.Resources.resourceCulture);
      }
    }
  }
}
