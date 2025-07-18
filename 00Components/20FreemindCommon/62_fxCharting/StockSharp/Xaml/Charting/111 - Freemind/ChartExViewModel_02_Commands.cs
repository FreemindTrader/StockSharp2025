using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;
using Ecng.Common;
using Ecng.Collections;
using Ecng.ComponentModel;
using Ecng.Configuration;
using Ecng.Serialization;
using MoreLinq;
using SciChart.Charting.Visuals.TradeChart;
using StockSharp.Algo;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Indicators;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Xaml.Charting;
using System;
using System.Collections.Generic; 
using fx.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DevExpress.Mvvm.UI;
using StockSharp.Xaml.Charting.Definitions;
using System.Windows;
using DevExpress.Xpf.Grid;
using SciChart.Charting.Visuals;
using fx.Common;
using StockSharp.Xaml;
using StockSharp.Messages;
using StockSharp.Charting;


#pragma warning disable 067

namespace StockSharp.Xaml.Charting
{
    public partial class ChartExViewModel 
    {
        

        

        public void RemoveIndicatorArea()
        {
            if ( _indicatorAreas.Count > 0 )
            {
                foreach ( ChartArea area in _indicatorAreas )
                {
                    RemoveArea( area );
                }
            }
        }

        
        public bool CanAddArea( )
        {            
            return AllowAddArea;
        }

        [Command]
        public void AddIndicator( ChartArea chartArea )
        {
            AddIndicatorEvent?.Invoke( chartArea );
        }

        public bool CanAddIndicator( ChartArea chartArea )
        {
            return AllowAddIndicators;
        }

        [Command]
        public void AddIndicatorFifo( ChartArea chartArea )
        {
            var myEvent = new AddIndicatorFifoEventArgs( chartArea );
            AddIndicatorFifoEvent?.Invoke( this, myEvent );
        }

        public bool CanAddIndicatorFifo( ChartArea chartArea )
        {
            return AllowAddIndicators;
        }

        [ Command]
        public void AddCandles( ChartArea chartArea )
        {
            AddCandlesEvent?.Invoke( chartArea );
        }

        public bool CanAddCandles( ChartArea chartArea )
        {
            return AllowAddCandles;
        }

        [Command]
        public void AddOrders( ChartArea chartArea )
        {
            AddOrdersEvent?.Invoke( chartArea );
        }


        public bool CanAddOrders( ChartArea chartArea )
        {
            return AllowAddOrders;
        }

        [Command]
        public void AddTrades( ChartArea chartArea )
        {
            AddTradesEvent?.Invoke( chartArea );
        }

        public bool CanAddTrades( ChartArea chartArea )
        {
            return AllowAddOwnTrades;
        }

        [Command]
        public void AddXAxis( ChartArea a )
        {
            var area = GetRealChartArea( a );
            if ( area == null )
                return;

            area.XAxises.Add
            (
                new ChartAxis( )
                {
                    AxisType = ChartAxisType.CategoryDateTime,
                    TimeZone = ( a.XAxises ).LastOrDefault( )?.TimeZone
                }
            );
        }

        public bool CanAddXAxis( ChartArea chartArea )
        {
            return IsInteracted;
        }

        [Command]
        public void AddYAxis( ChartArea a )
        {
            ChartArea area = GetRealChartArea( a );
            if ( area == null )
                return;

            area.YAxises.Add
            (
                new ChartAxis( )
                {
                    AxisType = ChartAxisType.Numeric
                }
            );
        }

        public bool CanAddYAxis( ChartArea chartArea )
        {
            return IsInteracted;
        }

        [Command]
        public void ShowHiddenAxes( ChartArea area )
        {
            if ( area != null )
            {
                area.ViewModel.ShowHiddenAxesCommand.Execute( null );
            }
            else
            {
                Ecng.Collections.CollectionHelper.ForEach( ScichartSurfaceViewModels, p => p.Area.ViewModel.ShowHiddenAxesCommand.Execute( null ) );
                
            }

        }

        public bool CanShowHiddenAxes( ChartArea _param1 )
        {
            return IsInteracted;
        }


        private ChartArea GetRealChartArea( ChartArea area )
        {
            if ( area != null )
                return area;

            ObservableCollection<ScichartSurfaceMVVM> vm = ScichartSurfaceViewModels;
            if ( vm == null )
                return null;

            return vm.FirstOrDefault( )?.Area;
        }

        [Command]
        public void CancelActiveOrders( ChartArea _param1 )
        {
            OnExecuteCancelActiveOrders( null );
        }

        public bool CanCancelActiveOrders( ChartArea _param1 )
        {
            return IsInteracted;
        }

        public void OnExecuteCancelActiveOrders( Func<Order, bool> cancelOrdersFunc )
        {
            if ( cancelOrdersFunc == null )
            {
                cancelOrdersFunc = ( p => true );
            }

            IEnumerable<Order> selectActiverOrders( ScichartSurfaceMVVM s )
            {
                return s.GetActiveOrders( o =>
                {
                    if ( o.State != OrderStates.Active )
                        return o.State == OrderStates.Pending;
                    return true;
                } );
            }

            var some = ScichartSurfaceViewModels.SelectMany( selectActiverOrders).Where( cancelOrdersFunc);
            var ordersSet = Enumerable.ToHashSet( some);

            foreach ( var order in ordersSet )
            {
                var cancelOrder = CancelActiveOrderEvent;
                if ( cancelOrder == null )
                    return;
                cancelOrder( order );
            }
        }

        



        public static readonly DependencyProperty RemoveAxisCommandProperty = DependencyProperty.Register( nameof( RemoveAxisCommand ),       typeof( DelegateCommand<ChartAxis> ), typeof( ChartExViewModel ) );

        public DelegateCommand<ChartAxis> RemoveAxisCommand
        {
            get;
            private set;
        }

        private void ExecuteRemoveAxisCommand( ChartAxis axis )
        {
            IChartArea area = axis.ChartArea;

            if ( area.XAxises.Contains( axis ) )
            {
                area.XAxises.Remove( axis );
            }

            if ( !area.YAxises.Contains( axis ) )
            {
                return;
            }

            area.YAxises.Remove( axis );
        }

        private bool CanExecuteRemoveAxisCommand( ChartAxis a )
        {
            //BUG:
            if ( IsInteracted && ( a?.ChartArea != null ) /*&& !a.IsDefault*/ )
            {
                return AllowAddAxis;
            }

            return false;
        }
        public DelegateCommand<IChildPane> ClosePaneCommand
        {
            get;
            private set;
        }

        private void ExecuteClosePaneCommand( IChildPane pane )
        {
            var sciMvvm = ( ( IDrawingSurfaceVM )pane );

            var areas = sciMvvm.Chart.ChartAreas;
            var chartArea = areas.FirstOrDefault( a => a.ViewModel == pane );

            if ( chartArea == null )
            {
                return;
            }

            areas.Remove( chartArea );
        }

        private bool CanExecuteClosePaneCommand( IChildPane pane )
        {
            return IsInteracted;
        }

        
    }
}
