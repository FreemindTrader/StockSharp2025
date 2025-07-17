// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Rendering.HighQualityRasterizer.HqPen
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Windows.Media;
using Ecng.Xaml.Charting.Rendering.Common;

namespace Ecng.Xaml.Charting.Rendering.HighQualityRasterizer
{
    internal class HqPen : IPen2D, IPathColor, IDisposable, IDashSplittingContext
    {
        private float _strokeThickness;
        private Color _color;
        private bool _isAntialiased;
        private readonly double[] _strokeDashArray;
        private bool _isTransparent;

        public int StrokeDashArrayIndex
        {
            get; set;
        }

        public double StrokeDashArrayItemPassedLength
        {
            get; set;
        }

        public double[ ] StrokeDashArray
        {
            get
            {
                return this._strokeDashArray;
            }
        }

        public float StrokeThickness
        {
            get
            {
                return this._strokeThickness;
            }
        }

        public bool Antialiased
        {
            get
            {
                return this._isAntialiased;
            }
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

        public bool HasDashes
        {
            get; private set;
        }

        public bool IsTransparent
        {
            get
            {
                return this._isTransparent;
            }
        }

        public PenLineCap StrokeEndLineCap
        {
            get; private set;
        }

        internal HqPen( Color color, float strokeThickness, bool isAntialiased, double opacity = 1.0, double[ ] strokeDashArray = null )
          : this( color, strokeThickness, PenLineCap.Round, isAntialiased, opacity, strokeDashArray )
        {
        }

        internal HqPen( Color color, float strokeThickness, PenLineCap strokeEndLineCap, bool isAntialiased, double opacity = 1.0, double[ ] strokeDashArray = null )
        {
            this._isTransparent = color.A == ( byte ) 0;
            this._strokeThickness = strokeThickness;
            this._color = Color.FromArgb( ( byte ) ( ( double ) color.A * opacity ), color.R, color.G, color.B );
            this._isAntialiased = isAntialiased;
            this._strokeDashArray = strokeDashArray;
            this.HasDashes = this._strokeDashArray != null && this._strokeDashArray.Length >= 2;
            this.StrokeEndLineCap = strokeEndLineCap;
        }

        public void Dispose()
        {
        }
    }
}
