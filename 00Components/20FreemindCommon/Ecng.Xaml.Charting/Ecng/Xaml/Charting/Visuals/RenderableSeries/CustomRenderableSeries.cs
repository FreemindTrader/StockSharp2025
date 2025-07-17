// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Visuals.RenderableSeries.CustomRenderableSeries
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using Ecng.Xaml.Charting.Licensing;
using Ecng.Xaml.Charting.Rendering.Common;
using StockSharp.Xaml.Licensing.Core;

namespace Ecng.Xaml.Charting.Visuals.RenderableSeries
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
