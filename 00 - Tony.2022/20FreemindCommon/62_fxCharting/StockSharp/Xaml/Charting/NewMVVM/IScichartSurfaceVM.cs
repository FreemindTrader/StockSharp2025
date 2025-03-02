using SciChart.Charting.Model;
using SciChart.Charting.Visuals.Annotations;
using SciChart.Charting.Visuals.RenderableSeries;
using StockSharp.BusinessEntities;
using System;
using System.Collections.Generic; using fx.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace fx.Charting
{
    public interface IScichartSurfaceVM
    {
        double Height { get; set; }
        int MinimumRange { get; set; }
        int RightMarginBars { get; set; }        

        ChartViewModel ChartViewModel { get; }
        ChartExViewModel ChartExViewModel { get; }
        ObservableCollection<ParentVM> LegendElements { get; }
        ChartArea Area { get; }

        AxisCollection XAxises { get;  }

        void RemoveAnnotation( IElementWithXYAxes elementXY, object objAnnoPair );

        void RemoveAnnotation( IfxChartElement annotation );

        void AddAxisMakerAnnotation( IElementWithXYAxes elementXY, IAnnotation axisMakerAnnotation, object axisMarker );

        void InvokeMoveOrderEvent( Order order, Decimal newOrderPrice );

        void InvokeCancelOrderEvent( Order order );

        IAnnotation GetAxisMakerAnnotation( IElementWithXYAxes elementXY, object objAnnoPair );

        void AddRenderableSeriesToChartSurface( IElementWithXYAxes elementXY, IRenderableSeries renderableSeries );

        void SetupAnnotation( IfxChartElement annotation, ChartDrawDataEx.sAnnotation data );

        void Remove( IElementWithXYAxes elementXY );

        VisbleRangeDp GetVisibleRangeDp( string axisId );

        bool IsAutoScroll { get; }

        bool IsAutoRange { get; }

        IChart Chart { get;  }

        void Release( );

        void Dispose( );

        void Draw( ChartDrawDataEx data );

        void UpdateQuote( DateTime offerTime, double bid, double ask );

        void Reset( IEnumerable< IfxChartElement > chartElements );

        void InitPropertiesEventHandlers( );

        bool OnChartAreaElementsRemoving( IfxChartElement element );

        void OnChartAreaElementsAdded( IfxChartElement anyChartUI );

        ICommand ShowHiddenAxesCommand { get;  }

        ChartExViewModel GroupChartEx { get;  }
    }
}
