// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Rendering.HighSpeedRasterizer.HsSprite2D
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Windows.Media.Imaging;
namespace Ecng.Xaml.Charting
{
    internal class HsSprite2D : ISprite2D, IDisposable
    {
        private readonly WriteableBitmap _wbmp;

        public HsSprite2D( WriteableBitmap wbmp )
        {
            this._wbmp = wbmp;
            this.Width = ( float ) this._wbmp.PixelWidth;
            this.Height = ( float ) this._wbmp.PixelHeight;
        }

        public WriteableBitmap WriteableBitmap
        {
            get
            {
                return this._wbmp;
            }
        }

        public void Dispose()
        {
        }

        public float Width
        {
            get; private set;
        }

        public float Height
        {
            get; private set;
        }
    }
}
