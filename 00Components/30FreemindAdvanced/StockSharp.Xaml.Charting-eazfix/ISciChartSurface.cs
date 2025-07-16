using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using SciChart.Charting.Model.ChartSeries;
using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.ViewportManagers;
using SciChart.Charting.Visuals.Axes;
using SciChart.Charting.Visuals.Events;
using SciChart.Charting.Visuals;
using SciChart.Core.Framework;
using StockSharp.Charting.Visuals;

#nullable disable
namespace StockSharp.Xaml.Charting.Visuals;

public interface ISciChartSurface : ISciChartSurfaceBase, ISuspendable, IInvalidatableElement, IUltrachartController, IDisposable
{
    event EventHandler<AxisAlignmentChangedEventArgs> AxisAlignmentChanged;

    event EventHandler AnnotationsCollectionNewCollectionAssigned;

    event EventHandler YAxesCollectionNewCollectionAssigned;

    event EventHandler XAxesCollectionNewCollectionAssigned;

    IChartModifier ChartModifier
    {
        get;
        set;
    }

    AnnotationCollection Annotations
    {
        get;
    }

    IAxis XAxis
    {
        get;
        set;
    }

    IAxis YAxis
    {
        get;
        set;
    }

    AxisCollection YAxes
    {
        get;
    }

    AxisCollection XAxes
    {
        get;
    }

    IGridLinesPanel GridLinesPanel
    {
        get;
    }

    ObservableCollection<IRenderableSeries> RenderableSeries
    {
        get;
    }

    ObservableCollection<IRenderableSeries> SelectedRenderableSeries
    {
        get;
    }

    IMainGrid RootGrid
    {
        get;
    }

    IViewportManager ViewportManager
    {
        get;
        set;
    }

    IAnnotationCanvas AnnotationOverlaySurface
    {
        get;
    }

    IAnnotationCanvas AnnotationUnderlaySurface
    {
        get;
    }

    Canvas AdornerLayerCanvas
    {
        get;
    }

    int LicenseDaysRemaining
    {
        get;
    }

    ObservableCollection<IChartSeriesViewModel> SeriesSource
    {
        get;
        set;
    }

    void ClearSeries();

    Size OnArrangeUltrachart();

    [Obsolete( "Obsolete. Please use UltrachartSurface.RootGrid.IsPointWithinBounds instead", true )]
    bool IsPointWithinBounds( Point point );

    [Obsolete( "Obsolete. Please use UltrachartSurface.RootGrid.GetBoundsRelativeTo instead", true )]
    Rect GetBoundsRelativeTo( StockSharp.Charting.Visuals.IHitTestable relativeTo );

    [Obsolete( "Obsolete. Please use UltrachartSurface.RootGrid.TranslatePoint instead", true )]
    Point TranslatePoint( Point point, StockSharp.Charting.Visuals.IHitTestable relativeTo );

    [Obsolete( "ISciChartSurface.GetWindowedYRange is obsolete. Use IAxis.GetWindowedYRange instead" )]
    IRange GetWindowedYRange( IAxis yAxis, IRange xRange );

    void OnAxisAlignmentChanged( IAxis axis, AxisAlignment oldValue );

    void OnIsCenterAxisChanged( IAxis axis );

    void DetachDataSeries( IDataSeries dataSeries );

    void AttachDataSeries( IDataSeries dataSeries );

    BitmapSource ExportToBitmapSource();
}
