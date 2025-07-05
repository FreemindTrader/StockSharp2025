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

        void RemoveAnnotation( IChartComponent elementXY, object objAnnoPair );

        void RemoveAnnotation( IChartElement annotation );

        void AddAxisMakerAnnotation( IChartComponent elementXY, IAnnotation axisMakerAnnotation, object axisMarker );

        void InvokeMoveOrderEvent( Order order, Decimal newOrderPrice );

        void InvokeCancelOrderEvent( Order order );

        IAnnotation GetAxisMakerAnnotation( IChartComponent elementXY, object objAnnoPair );

        void AddRenderableSeriesToChartSurface( IChartComponent elementXY, IRenderableSeries renderableSeries );

        void SetupAnnotation( IChartElement annotation, ChartDrawData.sAnnotation data );

        void Remove( IChartComponent elementXY );

        VisbleRangeDp GetVisibleRangeDp( string axisId );

        bool IsAutoScroll { get; }

        bool IsAutoRange { get; }

        IChart Chart { get;  }

        void Release( );

        void Dispose( );

        void Draw( ChartDrawData data );

        void UpdateQuote( DateTime offerTime, double bid, double ask );

        void Reset( IEnumerable< IChartElement > chartElements );

        void InitPropertiesEventHandlers( );

        bool OnChartAreaElementsRemoving( IChartElement element );

        void OnChartAreaElementsAdded( IChartElement anyChartUI );

        ICommand ShowHiddenAxesCommand { get;  }

        ChartExViewModel GroupChartEx { get;  }
    }
}
