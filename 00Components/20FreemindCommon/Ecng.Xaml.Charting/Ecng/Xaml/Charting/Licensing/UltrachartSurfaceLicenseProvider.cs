// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Licensing.UltrachartSurfaceLicenseProvider
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using StockSharp.Xaml.Charting.Visuals;
using StockSharp.Xaml.Licensing.Core;

namespace StockSharp.Xaml.Charting.Licensing
{
    [Obfuscation( ApplyToMembers = true, Exclude = false, Feature = "encryptmethod;encryptstrings;encryptconstants", StripAfterObfuscation = true )]
    internal sealed class UltrachartSurfaceLicenseProvider : Credentials, IUltrachartLicenseProvider
    {
        public void Validate( object parameter )
        {
            UltrachartSurface ultrachartSurface = parameter as UltrachartSurface;
            if ( ultrachartSurface == null )
            {
                return;
            }

            ultrachartSurface.LicenseDaysRemaining = this.LicenseDaysRemaining;
            Grid rootGrid = ultrachartSurface.RootGrid as Grid;
            if ( rootGrid == null )
            {
                return;
            }

            UIElement element = rootGrid.Children.Cast<UIElement>().FirstOrDefault<UIElement>((Func<UIElement, bool>) (x => x is Button));
            if ( element == null )
            {
                return;
            }

            rootGrid.Children.Remove( element );
        }
    }
}
