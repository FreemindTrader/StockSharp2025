// Decompiled with JetBrains decompiler
// Type: System.Windows.Media.Imaging.WriteableBitmapContextExtensions
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

namespace System.Windows.Media.Imaging
{
    internal static class WriteableBitmapContextExtensions
    {
        internal static BitmapContext GetBitmapContext( this WriteableBitmap bmp )
        {
            return new BitmapContext( bmp );
        }

        internal static BitmapContext GetBitmapContext( this WriteableBitmap bmp, ReadWriteMode mode )
        {
            return new BitmapContext( bmp, mode );
        }
    }
}
