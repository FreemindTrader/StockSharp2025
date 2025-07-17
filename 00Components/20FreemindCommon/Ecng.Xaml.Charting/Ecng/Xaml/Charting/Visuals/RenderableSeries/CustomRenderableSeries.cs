// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.CustomRenderableSeries
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using StockSharp.Xaml.Licensing.Core;

namespace fx.Xaml.Charting
{
    [UltrachartLicenseProvider( typeof( RenderableSeriesUltrachartLicenseProvider ) )]
    public class CustomRenderableSeries : BaseRenderableSeries
    {
        protected override sealed void InternalDraw( IRenderContext2D renderContext, IRenderPassData renderPassData )
        {
            this.Draw( renderContext, renderPassData );
        }

        protected virtual void Draw( IRenderContext2D renderContext, IRenderPassData renderPassData )
        {
        }
    }
}
