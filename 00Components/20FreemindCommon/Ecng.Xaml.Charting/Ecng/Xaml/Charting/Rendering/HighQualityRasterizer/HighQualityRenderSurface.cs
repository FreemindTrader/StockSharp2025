// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Rendering.HighQualityRasterizer.HighQualityRenderSurface
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using MatterHackers.Agg;
using MatterHackers.Agg.Image;
using StockSharp.Xaml.Licensing.Core;

namespace Ecng.Xaml.Charting
{
    [UltrachartLicenseProvider( typeof( RenderSurfaceLicenseProvider ) )]
    public class HighQualityRenderSurface : RenderSurfaceBase
    {
        private ImageBuffer _imageBuffer;
        private Graphics2D _graphics2D;
        protected internal uint[] _emptyStrideRow;

        public HighQualityRenderSurface()
        {
            this.RecreateSurface();
        }

        public override void RecreateSurface()
        {
            base.RecreateSurface();
            this._emptyStrideRow = new uint[ this.RenderWriteableBitmap.PixelWidth ];
            this._imageBuffer = new ImageBuffer( this.RenderWriteableBitmap.PixelWidth, this.RenderWriteableBitmap.PixelHeight, 32, ( IBlenderByte ) new BlenderBGRA() );
            this._graphics2D = this._imageBuffer.NewGraphics2D();
        }

        protected override TextureCacheBase CreateTextureCache()
        {
            return ( TextureCacheBase ) new TextureCache();
        }

        public override IRenderContext2D GetRenderContext()
        {
            if ( this.IsLicenseValid )
                return ( IRenderContext2D ) new HqRenderContext( this.Image, this.RenderWriteableBitmap, this._emptyStrideRow, this._imageBuffer, this._graphics2D, this.TextureCache );
            return ( IRenderContext2D ) new NullRenderContext();
        }
    }
}
