// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.Graphics2D
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using MatterHackers.Agg.Font;
using MatterHackers.Agg.Image;
using MatterHackers.Agg.Transform;
using MatterHackers.Agg.VertexSource;
using MatterHackers.VectorMath;

namespace MatterHackers.Agg
{
    internal abstract class Graphics2D
    {
        protected Stack<Affine> m_AffineTransformStack = new Stack<Affine>();
        private const int cover_full = 255;
        protected IImageByte destImageByte;
        protected IImageFloat destImageFloat;
        protected Stroke StrockedText;
        protected ScanlineRasterizer m_Rasterizer;

        public Graphics2D()
        {
            this.m_AffineTransformStack.Push( Affine.NewIdentity() );
        }

        public Graphics2D( IImageByte destImage, ScanlineRasterizer rasterizer )
          : this()
        {
            this.Initialize( destImage, rasterizer );
        }

        public void Initialize( IImageByte destImage, ScanlineRasterizer rasterizer )
        {
            this.destImageByte = destImage;
            this.destImageFloat = ( IImageFloat ) null;
            this.m_Rasterizer = rasterizer;
        }

        public void Initialize( IImageFloat destImage, ScanlineRasterizer rasterizer )
        {
            this.destImageByte = ( IImageByte ) null;
            this.destImageFloat = destImage;
            this.m_Rasterizer = rasterizer;
        }

        public Affine PopTransform()
        {
            if ( this.m_AffineTransformStack.Count == 1 )
                throw new Exception( "You cannot remove the last transform from the stack." );
            return this.m_AffineTransformStack.Pop();
        }

        public void PushTransform()
        {
            if ( this.m_AffineTransformStack.Count > 1000 )
                throw new Exception( "You seem to be leaking transforms.  You should be poping some of them at some point." );
            this.m_AffineTransformStack.Push( this.m_AffineTransformStack.Peek() );
        }

        public Affine GetTransform()
        {
            return this.m_AffineTransformStack.Peek();
        }

        public void SetTransform( Affine value )
        {
            this.m_AffineTransformStack.Pop();
            this.m_AffineTransformStack.Push( value );
        }

        public ScanlineRasterizer Rasterizer
        {
            get
            {
                return this.m_Rasterizer;
            }
        }

        public abstract IScanlineCache ScanlineCache
        {
            get; set;
        }

        public IImageByte DestImage
        {
            get
            {
                return this.destImageByte;
            }
        }

        public IImageFloat DestImageFloat
        {
            get
            {
                return this.destImageFloat;
            }
        }

        public abstract void Render( IVertexSource vertexSource, int pathIndexToRender, RGBA_Bytes colorBytes );

        public void Render( IImageByte imageSource, Point2D position )
        {
            this.Render( imageSource, ( double ) position.x, ( double ) position.y );
        }

        public void Render( IImageByte imageSource, Vector2 position )
        {
            this.Render( imageSource, position.x, position.y );
        }

        public void Render( IImageByte imageSource, double x, double y )
        {
            this.Render( imageSource, x, y, 0.0, 1.0, 1.0 );
        }

        public abstract void Render( IImageByte imageSource, double x, double y, double angleRadians, double scaleX, double ScaleY );

        public abstract void Render( IImageFloat imageSource, double x, double y, double angleRadians, double scaleX, double ScaleY );

        public void Render( IVertexSource vertexSource, RGBA_Bytes[ ] colorArray, int[ ] pathIdArray, int numPaths )
        {
            for ( int index = 0 ; index < numPaths ; ++index )
                this.Render( vertexSource, pathIdArray[ index ], colorArray[ index ] );
        }

        public void Render( IVertexSource vertexSource, RGBA_Bytes color )
        {
            this.Render( vertexSource, 0, color );
        }

        public void Render( IVertexSource vertexSource, double x, double y, RGBA_Bytes color )
        {
            this.Render( ( IVertexSource ) new VertexSourceApplyTransform( vertexSource, ( ITransform ) Affine.NewTranslation( x, y ) ), 0, color );
        }

        public void Render( IVertexSource vertexSource, Vector2 position, RGBA_Bytes color )
        {
            this.Render( ( IVertexSource ) new VertexSourceApplyTransform( vertexSource, ( ITransform ) Affine.NewTranslation( position.x, position.y ) ), 0, color );
        }

        public abstract void Clear( IColorType color );

        public void DrawString( string Text, double x, double y, double pointSize = 12.0, Justification justification = Justification.Left, Baseline baseline = Baseline.Text, RGBA_Bytes color = default( RGBA_Bytes ), bool drawFromHintedCach = false )
        {
            StringPrinter stringPrinter = new StringPrinter(Text, pointSize, new Vector2(x, y), justification, baseline);
            if ( color.Alpha0To255 == 0 )
                color = RGBA_Bytes.Black;
            if ( drawFromHintedCach )
                stringPrinter.DrawFromHintedCache( this, color );
            else
                this.Render( ( IVertexSource ) stringPrinter, color );
        }

        public void Circle( Vector2 origin, double radius, RGBA_Bytes color )
        {
            this.Circle( origin.x, origin.y, radius, color );
        }

        public void Circle( double x, double y, double radius, RGBA_Bytes color )
        {
            this.Render( ( IVertexSource ) new Ellipse( x, y, radius, radius, 0, false ), color );
        }

        public void Line( Vector2 start, Vector2 end, RGBA_Bytes color )
        {
            this.Line( start.x, start.y, end.x, end.y, color );
        }

        public void Line( double x1, double y1, double x2, double y2, RGBA_Bytes color )
        {
            PathStorage pathStorage = new PathStorage();
            pathStorage.remove_all();
            pathStorage.MoveTo( x1, y1 );
            pathStorage.LineTo( x2, y2 );
            this.Render( ( IVertexSource ) new Stroke( ( IVertexSource ) pathStorage, 1.0 ), color );
        }

        public abstract void SetClippingRect( RectangleDouble rect_d );

        public abstract RectangleDouble GetClippingRect();

        public void Rectangle( double left, double bottom, double right, double top, RGBA_Bytes color, double strokeWidth = 1.0 )
        {
            this.Render( ( IVertexSource ) new Stroke( ( IVertexSource ) new RoundedRect( left + 0.5, bottom + 0.5, right - 0.5, top - 0.5, 0.0 ), strokeWidth ), color );
        }

        public void Rectangle( RectangleDouble rect, RGBA_Bytes color )
        {
            this.Rectangle( rect.Left, rect.Bottom, rect.Right, rect.Top, color, 1.0 );
        }

        public void Rectangle( RectangleInt rect, RGBA_Bytes color )
        {
            this.Rectangle( ( double ) rect.Left, ( double ) rect.Bottom, ( double ) rect.Right, ( double ) rect.Top, color, 1.0 );
        }

        public void FillRectangle( RectangleDouble rect, IColorType fillColor )
        {
            this.FillRectangle( rect.Left, rect.Bottom, rect.Right, rect.Top, fillColor );
        }

        public void FillRectangle( RectangleInt rect, IColorType fillColor )
        {
            this.FillRectangle( ( double ) rect.Left, ( double ) rect.Bottom, ( double ) rect.Right, ( double ) rect.Top, fillColor );
        }

        public void FillRectangle( double left, double bottom, double right, double top, IColorType fillColor )
        {
            if ( right < left || top < bottom )
                throw new ArgumentException();
            this.Render( ( IVertexSource ) new RoundedRect( left, bottom, right, top, 0.0 ), fillColor.GetAsRGBA_Bytes() );
        }
    }
}
