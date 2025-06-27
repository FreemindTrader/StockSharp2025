// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Common.LicenseExtensions
// Assembly: StockSharp.Web.Common, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E38A38B-3071-40E9-9B31-80D08347A76B
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Common.dll

using Ecng.Common;
using StockSharp.Web.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockSharp.Web.Common
{
    public static class LicenseExtensions
    {
        public static DateTime? GetExpirationDate( this License license )
        {
            return license.GetMaxExpirationFeature()?.ExpiryDate;
        }

        public static LicenseFeatureEx GetMaxExpirationFeature( this License license )
        {
            BaseEntitySet<LicenseFeatureEx> features = ((License) TypeHelper.CheckOnNull<License>( license, nameof (license))).Features;
            if ( features == null )
                return ( LicenseFeatureEx ) null;
            LicenseFeatureEx[] items = features.Items;
            if ( items == null )
                return ( LicenseFeatureEx ) null;
            return ( ( IEnumerable<LicenseFeatureEx> ) items ).OrderByDescending<LicenseFeatureEx, DateTime>( ( Func<LicenseFeatureEx, DateTime> ) ( f => f.ExpiryDate ) ).FirstOrDefault<LicenseFeatureEx>();
        }

        public static double GetDays( this DateTime end )
        {
            double num = (end - DateTime.UtcNow).TotalDays;
            if ( num < 0.0 )
                num = 0.0;
            return MathHelper.Round( num, 1 );
        }
    }
}
