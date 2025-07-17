// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Visuals.RenderableSeries.IStackedColumnRenderableSeries
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System.Windows.Media;
using System.Xml.Serialization;
using Ecng.Xaml.Charting.Model.DataSeries;
using Ecng.Xaml.Charting.Numerics.CoordinateCalculators;

namespace Ecng.Xaml.Charting.Visuals.RenderableSeries
{
    public interface IStackedColumnRenderableSeries : IStackedRenderableSeries, IRenderableSeries, IRenderableSeriesBase, IDrawable, IXmlSerializable
    {
        IStackedColumnsWrapper Wrapper
        {
            get;
        }

        double Spacing
        {
            get; set;
        }

        SpacingMode SpacingMode
        {
            get; set;
        }

        double DataPointWidth
        {
            get; set;
        }

        bool ShowLabel
        {
            get; set;
        }

        Color LabelColor
        {
            get; set;
        }

        float LabelFontSize
        {
            get; set;
        }

        string LabelTextFormatting
        {
            get; set;
        }

        int GetDatapointWidth( ICoordinateCalculator<double> xCoordinateCalculator, IPointSeries pointSeries, double barsAmount, double widthFraction );

        bool IsValidForDrawing
        {
            get;
        }
    }
}
