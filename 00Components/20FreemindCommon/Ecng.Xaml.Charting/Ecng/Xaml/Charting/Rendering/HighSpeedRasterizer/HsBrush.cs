// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Rendering.HighSpeedRasterizer.HsBrush
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Windows.Media;
using StockSharp.Xaml.Charting.Rendering.Common;

namespace StockSharp.Xaml.Charting.Rendering.HighSpeedRasterizer
{
    internal struct HsBrush : IBrush2D, IPathColor, IDisposable
    {
        private Color _color;
        private int _colorCode;
        private bool _alphaBlend;
        private bool _isTransparent;

        internal HsBrush( Color color, int colorCode, bool alphaBlend )
        {
            this = new HsBrush();
            this._isTransparent = color.A == ( byte ) 0;
            this._color = color;
            this._colorCode = colorCode;
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
                return this._colorCode;
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
