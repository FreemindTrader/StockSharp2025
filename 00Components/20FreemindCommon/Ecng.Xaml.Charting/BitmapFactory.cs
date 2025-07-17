// Decompiled with JetBrains decompiler
// Type: System.Windows.Media.Imaging.BitmapFactory
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

namespace System.Windows.Media.Imaging
{
    internal static class BitmapFactory
    {
        internal static WriteableBitmap New( int pixelWidth, int pixelHeight )
        {
            if ( pixelHeight < 1 )
                pixelHeight = 1;
            if ( pixelWidth < 1 )
                pixelWidth = 1;
            return new WriteableBitmap( pixelWidth, pixelHeight, 96.0, 96.0, PixelFormats.Pbgra32, ( BitmapPalette ) null );
        }
    }
}
