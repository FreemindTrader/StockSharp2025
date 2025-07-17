// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Rendering.HighSpeedRasterizer.HsPen
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Ecng.Xaml.Charting.Common.Extensions;
using Ecng.Xaml.Charting.Rendering.Common;

namespace Ecng.Xaml.Charting.Rendering.HighSpeedRasterizer
{
    internal class HsPen : IPen2D, IPathColor, IDisposable, IDashSplittingContext
    {
        private readonly float _strokeThickness;
        private readonly bool _isAntialiased;
        private readonly Color _color;
        private readonly int _colorCode;
        private BitmapContext _pen;
        private double[] _strokeDashArray;
        private bool _isTransparent;

        public int StrokeDashArrayIndex
        {
            get; set;
        }

        public double StrokeDashArrayItemPassedLength
        {
            get; set;
        }

        internal BitmapContext Pen
        {
            get
            {
                return this._pen;
            }
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
                return this._colorCode;
            }
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

        public bool HasDashes
        {
            get; private set;
        }

        internal HsPen( Color color, int colorCode, float strokeThickness, bool isAntialiased, double opacity = 1.0, double[ ] strokeDashArray = null )
          : this( color, colorCode, strokeThickness, PenLineCap.Round, isAntialiased, opacity, strokeDashArray )
        {
        }

        internal HsPen( Color color, int colorCode, float strokeThickness, PenLineCap strokeEndLineCap, bool isAntialiased, double opacity = 1.0, double[ ] strokeDashArray = null )
        {
            this._isTransparent = color.A == ( byte ) 0;
            this._color = color;
            this._colorCode = colorCode;
            this._strokeThickness = strokeThickness;
            this._isAntialiased = isAntialiased;
            this._strokeDashArray = strokeDashArray;
            this.HasDashes = this._strokeDashArray != null && this._strokeDashArray.Length >= 2;
            this.CreateLineEndCap( strokeEndLineCap, opacity );
        }

        private void CreateLineEndCap( PenLineCap strokeEndLineCap, double opacity )
        {
            if ( ( double ) this._strokeThickness <= 1.0 )
                return;
            Shape element;
            switch ( strokeEndLineCap )
            {
                case PenLineCap.Round:
                    element = ( Shape ) new Ellipse();
                    this.StrokeEndLineCap = PenLineCap.Round;
                    break;
                default:
                    element = ( Shape ) new Rectangle();
                    this.StrokeEndLineCap = PenLineCap.Square;
                    break;
            }
            element.Width = ( double ) this._strokeThickness;
            element.Height = ( double ) this._strokeThickness;
            element.Opacity = opacity;
            element.Fill = ( Brush ) new SolidColorBrush( Color.FromArgb( ( byte ) ( opacity * ( double ) this._color.A ), this._color.R, this._color.G, this._color.B ) );
            element.Arrange( new Rect( 0.0, 0.0, ( double ) this._strokeThickness, ( double ) this._strokeThickness ) );
            this._pen = element.RenderToBitmap( ( int ) this._strokeThickness, ( int ) this._strokeThickness ).GetBitmapContext();
        }

        public void Dispose()
        {
            BitmapContext pen = this._pen;
            if ( pen.WriteableBitmap == null )
                return;
            pen.WriteableBitmap.Dispatcher.BeginInvokeIfRequired( new Action( pen.Dispose ) );
        }
    }
}
