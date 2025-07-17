// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Rendering.HighSpeedRasterizer.HighSpeedRenderSurface
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using StockSharp.Xaml.Licensing.Core;

namespace fx.Xaml.Charting
{
    [UltrachartLicenseProvider( typeof( RenderSurfaceLicenseProvider ) )]
    public class HighSpeedRenderSurface : RenderSurfaceBase
    {
        protected override TextureCacheBase CreateTextureCache()
        {
            return ( TextureCacheBase ) new TextureCache();
        }

        public HighSpeedRenderSurface()
        {
            this.RecreateSurface();
        }

        public override IRenderContext2D GetRenderContext()
        {
            return ( IRenderContext2D ) this.RenderWriteableBitmap.GetRenderContext( this.Image, this.TextureCache );
        }
    }
}
