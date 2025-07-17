// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.IStackedMountainRenderableSeries
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System.Xml.Serialization;
namespace Ecng.Xaml.Charting
{
    public interface IStackedMountainRenderableSeries : IStackedRenderableSeries, IRenderableSeries, IRenderableSeriesBase, IDrawable, IXmlSerializable
    {
        IStackedMountainsWrapper Wrapper
        {
            get;
        }

        bool IsDigitalLine
        {
            get; set;
        }

        void DrawMountain( IRenderContext2D renderContext, bool isPreviousSeriesDigital );
    }
}
