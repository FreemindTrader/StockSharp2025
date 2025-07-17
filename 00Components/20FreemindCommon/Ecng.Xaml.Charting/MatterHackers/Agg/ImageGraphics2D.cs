// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.ImageGraphics2D
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: B:\00 - Programming\StockSharp\References\fx.Xaml.Charting.dll

using System;
using MatterHackers.Agg.Image;
using MatterHackers.Agg.RasterizerScanline;
using MatterHackers.Agg.Transform;
using MatterHackers.Agg.VertexSource;

namespace MatterHackers.Agg
{
    internal class ImageGraphics2D : Graphics2D
    {
        private PathStorage drawImageRectPath = new PathStorage();
        private span_allocator destImageSpanAllocatorCache = new span_allocator();
        private ScanlineCachePacked8 drawImageScanlineCache = new ScanlineCachePacked8();
        private ScanlineRenderer scanlineRenderer = new ScanlineRenderer();
        private const int cover_full = 255;
        protected IScanlineCache m_ScanlineCache;

        public ImageGraphics2D()
        {
        }

        public ImageGraphics2D( IImageByte destImage, ScanlineRasterizer rasterizer, IScanlineCache scanlineCache )
          : base( destImage, rasterizer )
        {
            this.m_ScanlineCache = scanlineCache;
        }

        public override IScanlineCache ScanlineCache
        {
            get
            {
                return this.m_ScanlineCache;
            }
            set
            {
                this.m_ScanlineCache = value;
            }
        }

        public override void SetClippingRect( RectangleDouble clippingRect )
        {
            this.Rasterizer.SetVectorClipBox( clippingRect );
        }

        public override RectangleDouble GetClippingRect()
        {
            return this.Rasterizer.GetVectorClipBox();
        }

        public override void Render( IVertexSource vertexSource, int pathIndexToRender, RGBA_Bytes colorBytes )
        {
            this.m_Rasterizer.reset();
            Affine transform = this.GetTransform();
            if ( !transform.is_identity() )
                vertexSource = ( IVertexSource ) new VertexSourceApplyTransform( vertexSource, ( ITransform ) transform );
            this.m_Rasterizer.add_path( vertexSource, pathIndexToRender );
            if ( this.destImageByte != null )
            {
                this.scanlineRenderer.render_scanlines_aa_solid( this.destImageByte, ( IRasterizer ) this.m_Rasterizer, this.m_ScanlineCache, colorBytes );
                this.DestImage.MarkImageChanged();
            }
            else
            {
                this.scanlineRenderer.RenderSolid( this.destImageFloat, ( IRasterizer ) this.m_Rasterizer, this.m_ScanlineCache, colorBytes.GetAsRGBA_Floats() );
                this.destImageFloat.MarkImageChanged();
            }
        }

        private void DrawImageGetDestBounds( IImageByte sourceImage, double DestX, double DestY, double HotspotOffsetX, double HotspotOffsetY, double ScaleX, double ScaleY, double AngleRad, out Affine destRectTransform )
        {
            destRectTransform = Affine.NewIdentity();
            if ( HotspotOffsetX != 0.0 || HotspotOffsetY != 0.0 )
                destRectTransform *= Affine.NewTranslation( -HotspotOffsetX, -HotspotOffsetY );
            if ( ScaleX != 1.0 || ScaleY != 1.0 )
                destRectTransform *= Affine.NewScaling( ScaleX, ScaleY );
            if ( AngleRad != 0.0 )
                destRectTransform *= Affine.NewRotation( AngleRad );
            if ( DestX != 0.0 || DestY != 0.0 )
                destRectTransform *= Affine.NewTranslation( DestX, DestY );
            int width = sourceImage.Width;
            int height = sourceImage.Height;
            this.drawImageRectPath.remove_all();
            this.drawImageRectPath.MoveTo( 0.0, 0.0 );
            this.drawImageRectPath.LineTo( ( double ) width, 0.0 );
            this.drawImageRectPath.LineTo( ( double ) width, ( double ) height );
            this.drawImageRectPath.LineTo( 0.0, ( double ) height );
            this.drawImageRectPath.ClosePolygon();
        }

        private void DrawImage( IImageByte sourceImage, ISpanGenerator spanImageFilter, Affine destRectTransform )
        {
            if ( this.destImageByte.OriginOffset.x != 0.0 || this.destImageByte.OriginOffset.y != 0.0 )
                destRectTransform *= Affine.NewTranslation( -this.destImageByte.OriginOffset.x, -this.destImageByte.OriginOffset.y );
            this.Rasterizer.add_path( ( IVertexSource ) new VertexSourceApplyTransform( ( IVertexSource ) this.drawImageRectPath, ( ITransform ) destRectTransform ) );
            this.scanlineRenderer.GenerateAndRender( ( IRasterizer ) this.Rasterizer, ( IScanlineCache ) this.drawImageScanlineCache, ( IImageByte ) new ImageClippingProxy( this.destImageByte ), this.destImageSpanAllocatorCache, spanImageFilter );
        }

        public override void Render( IImageByte source, double destX, double destY, double angleRadians, double inScaleX, double inScaleY )
        {
            RectangleInt bounds1 = source.GetBounds();
            RectangleInt bounds2 = this.destImageByte.GetBounds();
            bounds1.Offset( ( int ) destX, ( int ) destY );
            if ( !RectangleInt.DoIntersect( bounds1, bounds2 ) )
            {
                if ( inScaleX != 1.0 || inScaleY != 1.0 || angleRadians != 0.0 )
                    throw new NotImplementedException();
            }
            else
            {
                double ScaleX = inScaleX;
                double ScaleY = inScaleY;
                Affine transform = this.GetTransform();
                if ( !transform.is_identity() )
                {
                    if ( ScaleX != 1.0 || ScaleY != 1.0 || angleRadians != 0.0 )
                        throw new NotImplementedException();
                    transform.transform( ref destX, ref destY );
                }
                int num1 = ScaleX != 1.0 ? 1 : (ScaleY != 1.0 ? 1 : 0);
                bool flag = true;
                if ( Math.Abs( angleRadians ) < 0.00174532930056254 )
                {
                    flag = false;
                    angleRadians = 0.0;
                }
                double x = source.OriginOffset.x;
                double y = source.OriginOffset.y;
                if ( ScaleX <= 0.5 )
                    ;
                int num2 = flag ? 1 : 0;
                if ( ( ( num1 | num2 ) != 0 || destX != ( double ) ( int ) destX ? 1 : ( destY != ( double ) ( int ) destY ? 1 : 0 ) ) != 0 )
                {
                    Affine destRectTransform;
                    this.DrawImageGetDestBounds( source, destX, destY, x, y, ScaleX, ScaleY, angleRadians, out destRectTransform );
                    Affine affine = new Affine(destRectTransform);
                    affine.invert();
                    span_interpolator_linear interpolatorLinear = new span_interpolator_linear((ITransform) affine);
                    span_image_filter spanImageFilter = (span_image_filter) new span_image_filter_rgba_bilinear_clip((IImageBufferAccessor) new ImageBufferAccessorClip(source, RGBA_Floats.rgba_pre(0.0f, 0.0f, 0.0f, 0.0f).GetAsRGBA_Bytes()), (IColorType) RGBA_Floats.rgba_pre(0.0f, 0.0f, 0.0f, 0.0f), (ISpanInterpolator) interpolatorLinear);
                    this.DrawImage( source, ( ISpanGenerator ) spanImageFilter, destRectTransform );
                }
                else
                {
                    Affine destRectTransform;
                    this.DrawImageGetDestBounds( source, destX, destY, x, y, ScaleX, ScaleY, angleRadians, out destRectTransform );
                    Affine affine = new Affine(destRectTransform);
                    affine.invert();
                    span_interpolator_linear interpolatorLinear = new span_interpolator_linear((ITransform) affine);
                    ImageBufferAccessorClip bufferAccessorClip = new ImageBufferAccessorClip(source, RGBA_Floats.rgba_pre(0.0f, 0.0f, 0.0f, 0.0f).GetAsRGBA_Bytes());
                    span_image_filter spanImageFilter;
                    switch ( source.BitDepth )
                    {
                        case 8:
                            spanImageFilter = ( span_image_filter ) new span_image_filter_gray_nn_stepXby1( ( IImageBufferAccessor ) bufferAccessorClip, ( ISpanInterpolator ) interpolatorLinear );
                            break;
                        case 24:
                            spanImageFilter = ( span_image_filter ) new span_image_filter_rgb_nn_stepXby1( ( IImageBufferAccessor ) bufferAccessorClip, ( ISpanInterpolator ) interpolatorLinear );
                            break;
                        case 32:
                            spanImageFilter = ( span_image_filter ) new span_image_filter_rgba_nn_stepXby1( ( IImageBufferAccessor ) bufferAccessorClip, ( ISpanInterpolator ) interpolatorLinear );
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                    this.DrawImage( source, ( ISpanGenerator ) spanImageFilter, destRectTransform );
                    this.DestImage.MarkImageChanged();
                }
            }
        }

        public override void Render( IImageFloat source, double x, double y, double angleDegrees, double inScaleX, double inScaleY )
        {
            throw new NotImplementedException();
        }

        public override void Clear( IColorType iColor )
        {
            RectangleDouble clippingRect = this.GetClippingRect();
            RectangleInt rectangleInt = new RectangleInt((int) clippingRect.Left, (int) clippingRect.Bottom, (int) clippingRect.Right, (int) clippingRect.Top);
            if ( this.DestImage != null )
            {
                RGBA_Bytes asRgbaBytes = iColor.GetAsRGBA_Bytes();
                int width = this.DestImage.Width;
                int height = this.DestImage.Height;
                byte[] buffer = this.DestImage.GetBuffer();
                switch ( this.DestImage.BitDepth )
                {
                    case 8:
                        int red0To255 = iColor.Red0To255;
                        for ( int bottom = rectangleInt.Bottom ; bottom < rectangleInt.Top ; ++bottom )
                        {
                            int bufferOffsetXy = this.DestImage.GetBufferOffsetXY((int) clippingRect.Left, bottom);
                            int betweenPixelsInclusive = this.DestImage.GetBytesBetweenPixelsInclusive();
                            for ( int index = 0 ; index < rectangleInt.Width ; ++index )
                            {
                                buffer[ bufferOffsetXy ] = asRgbaBytes.blue;
                                bufferOffsetXy += betweenPixelsInclusive;
                            }
                        }
                        break;
                    case 24:
                        for ( int bottom = rectangleInt.Bottom ; bottom < rectangleInt.Top ; ++bottom )
                        {
                            int bufferOffsetXy = this.DestImage.GetBufferOffsetXY((int) clippingRect.Left, bottom);
                            int betweenPixelsInclusive = this.DestImage.GetBytesBetweenPixelsInclusive();
                            for ( int index = 0 ; index < rectangleInt.Width ; ++index )
                            {
                                buffer[ bufferOffsetXy ] = asRgbaBytes.blue;
                                buffer[ bufferOffsetXy + 1 ] = asRgbaBytes.green;
                                buffer[ bufferOffsetXy + 2 ] = asRgbaBytes.red;
                                bufferOffsetXy += betweenPixelsInclusive;
                            }
                        }
                        break;
                    case 32:
                        for ( int bottom = rectangleInt.Bottom ; bottom < rectangleInt.Top ; ++bottom )
                        {
                            int bufferOffsetXy = this.DestImage.GetBufferOffsetXY((int) clippingRect.Left, bottom);
                            int betweenPixelsInclusive = this.DestImage.GetBytesBetweenPixelsInclusive();
                            for ( int index = 0 ; index < rectangleInt.Width ; ++index )
                            {
                                buffer[ bufferOffsetXy ] = asRgbaBytes.blue;
                                buffer[ bufferOffsetXy + 1 ] = asRgbaBytes.green;
                                buffer[ bufferOffsetXy + 2 ] = asRgbaBytes.red;
                                buffer[ bufferOffsetXy + 3 ] = asRgbaBytes.alpha;
                                bufferOffsetXy += betweenPixelsInclusive;
                            }
                        }
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
            else
            {
                if ( this.DestImageFloat == null )
                    throw new Exception( "You have to have either a byte or float DestImage." );
                RGBA_Floats asRgbaFloats = iColor.GetAsRGBA_Floats();
                int width = this.DestImageFloat.Width;
                int height = this.DestImageFloat.Height;
                float[] buffer = this.DestImageFloat.GetBuffer();
                if ( this.DestImageFloat.BitDepth != 128 )
                    throw new NotImplementedException();
                for ( int y = 0 ; y < height ; ++y )
                {
                    int bufferOffsetXy = this.DestImageFloat.GetBufferOffsetXY(rectangleInt.Left, y);
                    int betweenPixelsInclusive = this.DestImageFloat.GetFloatsBetweenPixelsInclusive();
                    for ( int index = 0 ; index < rectangleInt.Width ; ++index )
                    {
                        buffer[ bufferOffsetXy ] = asRgbaFloats.blue;
                        buffer[ bufferOffsetXy + 1 ] = asRgbaFloats.green;
                        buffer[ bufferOffsetXy + 2 ] = asRgbaFloats.red;
                        buffer[ bufferOffsetXy + 3 ] = asRgbaFloats.alpha;
                        bufferOffsetXy += betweenPixelsInclusive;
                    }
                }
            }
        }
    }
}
