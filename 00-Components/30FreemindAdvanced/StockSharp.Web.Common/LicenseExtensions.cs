// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Common.LicenseExtensions
// Assembly: StockSharp.Web.Common, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 02092862-EA5F-4AA7-B6CA-D0C9A4C8AFDF
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Common.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Ecng.Common;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Common;

public static class LicenseExtensions
{
    public static DateTime? GetExpirationDate(this License license)
    {
        return license.GetMaxExpirationFeature()?.ExpiryDate;
    }

    public static LicenseFeatureEx GetMaxExpirationFeature(this License license)
    {
        BaseEntitySet<LicenseFeatureEx> features = TypeHelper.CheckOnNull<License>(license, nameof(license)).Features;
        if (features == null)
            return (LicenseFeatureEx)null;
        LicenseFeatureEx[] items = features.Items;
        return items == null ? (LicenseFeatureEx)null : ((IEnumerable<LicenseFeatureEx>)items).OrderByDescending<LicenseFeatureEx, DateTime>((Func<LicenseFeatureEx, DateTime>)(f => f.ExpiryDate)).FirstOrDefault<LicenseFeatureEx>();
    }

    public static double GetDays(this DateTime end)
    {
        double num = (end - DateTime.UtcNow).TotalDays;
        if (num < 0.0)
            num = 0.0;
        return MathHelper.Round(num, 1);
    }
}
