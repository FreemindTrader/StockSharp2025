// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Rendering.HighSpeedRasterizer.HighSpeedRenderSurface
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using StockSharp.Xaml.Charting.Licensing;
using StockSharp.Xaml.Charting.Rendering.Common;
using StockSharp.Xaml.Licensing.Core;

namespace StockSharp.Xaml.Charting.Rendering.HighSpeedRasterizer
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
