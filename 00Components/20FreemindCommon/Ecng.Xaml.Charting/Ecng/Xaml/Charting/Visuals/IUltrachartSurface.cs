// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.ISciChartSurface
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Ecng.Xaml.Charting;

public interface ISciChartSurface : IUltrachartSurfaceBase, ISuspendable, IInvalidatableElement, IUltrachartController, IDisposable
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
    Rect GetBoundsRelativeTo( IHitTestable relativeTo );

    [Obsolete( "Obsolete. Please use UltrachartSurface.RootGrid.TranslatePoint instead", true )]
    Point TranslatePoint( Point point, IHitTestable relativeTo );

    [Obsolete( "ISciChartSurface.GetWindowedYRange is obsolete. Use IAxis.GetWindowedYRange instead" )]
    IRange GetWindowedYRange( IAxis yAxis, IRange xRange );

    void OnAxisAlignmentChanged( IAxis axis, AxisAlignment oldValue );

    void OnIsCenterAxisChanged( IAxis axis );

    void DetachDataSeries( IDataSeries dataSeries );

    void AttachDataSeries( IDataSeries dataSeries );

    BitmapSource ExportToBitmapSource();
}
