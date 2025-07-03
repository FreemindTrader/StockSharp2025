using System;
using SciChart.Data.Model;

namespace SciChart.Charting.Visuals.Axes;

//
// Summary:
//     Defines the interface used to pass the set of parameters to SciChart.Charting.Numerics.TickProviders.ITickProvider.
public interface IAxisParams
{
    //
    // Summary:
    //     Gets or sets the VisibleRange of the Axis. In the case of XAxis, this will cause
    //     an align to X-Axis operation to take place
    //
    // Remarks:
    //     Setting the VisibleRange will cause the axis to redraw
    IRange VisibleRange
    {
        get; set;
    }

    //
    // Summary:
    //     Gets or sets the GrowBy Factor. e.g. GrowBy(0.1, 0.2) will increase the axis
    //     extents by 10% (min) and 20% (max) outside of the data range
    IRange<double> GrowBy
    {
        get; set;
    }

    //
    // Summary:
    //     Gets or sets the Minor Delta
    IComparable MinorDelta
    {
        get; set;
    }

    //
    // Summary:
    //     Gets or sets the Major Delta
    IComparable MajorDelta
    {
        get; set;
    }

    //
    // Summary:
    //     Gets the maximum range of the axis, based on the data-range of all series
    IRange GetMaximumRange();
}