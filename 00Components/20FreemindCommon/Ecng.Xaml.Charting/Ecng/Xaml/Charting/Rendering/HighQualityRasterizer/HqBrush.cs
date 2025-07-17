// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Rendering.HighQualityRasterizer.HqBrush
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Windows.Media;
using Ecng.Xaml.Charting.Rendering.Common;

namespace Ecng.Xaml.Charting.Rendering.HighQualityRasterizer
{
    internal struct HqBrush : IBrush2D, IPathColor, IDisposable
    {
        private Color _color;
        private bool _alphaBlend;
        private bool _isTransparent;

        internal HqBrush( Color color, bool alphaBlend, double opacity = 1.0 )
        {
            this = new HqBrush();
            this._isTransparent = color.A == ( byte ) 0;
            this._color = Color.FromArgb( ( byte ) ( ( double ) color.A * opacity ), color.R, color.G, color.B );
            this._alphaBlend = alphaBlend;
        }

        public Color Color
        {
            get
            {
                return this._color;
            }
        }

        public int ColorCode
        {
            get
            {
                return -1;
            }
        }

        public bool AlphaBlend
        {
            get
            {
                return this._alphaBlend;
            }
        }

        public bool IsTransparent
        {
            get
            {
                return this._isTransparent;
            }
        }

        public void Dispose()
        {
        }
    }
}
