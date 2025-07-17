// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Licensing.RenderSurfaceValidator
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Ecng.Xaml.Charting.Rendering.Common;
using StockSharp.Xaml.Licensing.Core;

namespace Ecng.Xaml.Charting.Licensing
{
    [Obfuscation( ApplyToMembers = true, Exclude = false, Feature = "encryptmethod;encryptstrings;encryptconstants", StripAfterObfuscation = true )]
    internal sealed class RenderSurfaceValidator
    {
        internal static void Validate( object parameter, bool isLicenseValid, int licenseDaysRemaining, Decoder.LicenseType licenseType, string productCode )
        {
            RenderSurfaceBase renderSurfaceBase = parameter as RenderSurfaceBase;
            if ( renderSurfaceBase == null )
            {
                return;
            }

            renderSurfaceBase.IsLicenseValid = true;
        }

        private static TextBlock GetTextBox( string text )
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = text;
            textBlock.TextAlignment = TextAlignment.Center;
            textBlock.HorizontalAlignment = HorizontalAlignment.Center;
            textBlock.FontFamily = new FontFamily( "Verdana" );
            textBlock.FontSize = 16.0;
            textBlock.Foreground = ( Brush ) new SolidColorBrush( Color.FromArgb( byte.MaxValue, byte.MaxValue, ( byte ) 125, ( byte ) 125 ) );
            return textBlock;
        }

        private static TextBlock GetWarningTextBox( string text )
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = text;
            textBlock.TextAlignment = TextAlignment.Center;
            textBlock.HorizontalAlignment = HorizontalAlignment.Right;
            textBlock.VerticalAlignment = VerticalAlignment.Top;
            textBlock.FontFamily = new FontFamily( "Verdana" );
            textBlock.FontSize = 16.0;
            textBlock.Foreground = ( Brush ) new SolidColorBrush( Color.FromArgb( ( byte ) 175, byte.MaxValue, ( byte ) 125, ( byte ) 125 ) );
            return textBlock;
        }
    }
}
