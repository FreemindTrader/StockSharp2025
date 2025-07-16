using System;
using SciChart.Core.Framework;

namespace StockSharp.Xaml.Charting.Visuals;

public interface IUltrachartController : ISuspendable, IInvalidatableElement
{
    void ZoomExtents();

    void AnimateZoomExtents(TimeSpan duration);

    void ZoomExtentsY();

    void AnimateZoomExtentsY(TimeSpan duration);

    void ZoomExtentsX();

    void AnimateZoomExtentsX(TimeSpan duration);
}