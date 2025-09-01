using StockSharp.Charting;
using SciChart.Charting.ChartModifiers;
using SciChart.Charting.Visuals.RenderableSeries;
using MoreLinq;
using System;
using System.Collections.Generic; using fx.Collections;
using System.Linq;
using System.Windows;
using SciChart.Charting.Model.ChartData;
using SciChart.Charting.Utility;
using SciChart.Core.Utility.Mouse;
using Ecng.Collections;
using System.Collections.ObjectModel;
using SciChart.Charting.Visuals;
namespace StockSharp.Xaml.Charting
{
    internal static class ChartDataObjectExHelper
    {
        internal static void RemoveSomething<T>( this IList<T> _param0, Predicate<T> _param1 )
        {
            for ( int index = 0; index < _param0.Count; ++index )
            {
                if ( _param1( _param0[index] ) )
                    _param0.RemoveAt( index-- );
            }
        }
    }

    public sealed class LegendModifierEx : LegendModifier
    {
        private int _count;

        public LegendModifierEx()
        {
            SourceMode = SourceMode.AllSeries;
            ReceiveHandledEvents = true;
            IsEnabled = true;

            SetCurrentValue( SeriesDataProperty, new ChartDataObject() );
        }

        public LegendModifierVM ViewModel { get; set; }

        protected override bool IsSeriesValid( IRenderableSeries visualSeries )
        {
            return true;
        }

        protected override bool IsHitPointValid( HitTestInfo hitTestInfo_0 )
        {
            return true;
        }

        protected override void HandleMasterMouseEvent( Point pt )
        {
            var infoAtMouse = GetSeriesInfoAt( pt );

            var mouseSeries = infoAtMouse.ToDictionary<SeriesInfo, object>( p => p.RenderableSeries );

            UpdateSeriesInfo( SeriesData.SeriesInfo, infoAtMouse );

            //SeriesData.SeriesInfo.RemoveSomething<SeriesInfo>( p => {
            //                                                            SeriesInfo info;
            //                                                            if ( !mouseSeries.TryGetValue( p.RenderableSeries, out info ) )
            //                                                                return true;
            //                                                            return info.GetType( ) != p.GetType( );
            //                                                        } );

            //var old = SeriesData.SeriesInfo.ToDictionary<SeriesInfo, object>( p => p.RenderableSeries );

            //mouseSeries.Values.Where( ( si =>
            //{
            //    if ( LegendModifier.GetIncludeSeries( Modifier.Cursor ) )
            //    {
            //        return !old.ContainsKey( si.RenderableSeries );
            //    }

            //    return false;
            //} ) )

            //ObservableCollection<SeriesInfo> current = SeriesData.SeriesInfo;

            //foreach ( var serie in current )
            //{
            //    
            //}



            Func<SeriesInfo, ChartElementUiDomain> groupByCondition = ( s => ( ( ( FrameworkElement )s.RenderableSeries ).Tag as Tuple<ChartElementUiDomain, ChartElementViewModel[ ]> )?.Item1 );

            Func<IGrouping<ChartElementUiDomain, SeriesInfo>, bool> whereCondition = ( g => g.Key != null );

            var groupDatas = SeriesData.SeriesInfo.GroupBy( groupByCondition ).Where( whereCondition );

            foreach ( var myGroup in groupDatas )
            {
                foreach ( SeriesInfo info in myGroup )
                {
                    var tag = ( ( FrameworkElement )info.RenderableSeries ).Tag as Tuple<ChartElementUiDomain, ChartElementViewModel[ ]>;

                    if ( tag == null )
                    {
                        return;
                    }

                    ChartComponentUiDomain parentVm = null;

                    foreach ( ChartElementViewModel vm in tag.Item2 )
                    {
                        vm.UpdateSeries( info );

                        parentVm = vm.ChartComponent;
                    }

                    if ( parentVm != null )
                    {
                        if ( ViewModel.Elements != null )
                        {
                            var e = ( ObservableCollection<ChartComponentUiDomain> )ViewModel.Elements;
                            //e.Add( parentVm );
                        }


                    }

                    //surface.LegendElements.Add( parentVm );
                }
            }
        }



        public void UpdateSeriesInfo( ICollection<SeriesInfo> SeriesInfo, IEnumerable<SeriesInfo> newSeries )
        {
            if ( newSeries.Count() == 0 )
                return;

            PooledDictionary<object, SeriesInfo> newSInfo = newSeries.ToPooledDictionary( ( Func<SeriesInfo, object> )( si => si.RenderableSeries ) );

            Func<SeriesInfo, bool> filter = si =>
           {
               SeriesInfo seriesInfo;
               if ( !newSInfo.TryGetValue( si.RenderableSeries, out seriesInfo ) )
               {
                   return true;
               }

               return seriesInfo.GetType() != si.GetType();
           };

            SeriesInfo.RemoveWhere( filter );

            PooledDictionary<object, SeriesInfo> oldSInfo = SeriesInfo.ToPooledDictionary( ( Func<SeriesInfo, object> )( si => si.RenderableSeries ) );

            Func<SeriesInfo, bool> newSFilter = si =>
          {
              if ( CursorModifier.GetIncludeSeries( ( DependencyObject )si.RenderableSeries ) )
              {
                  return !oldSInfo.ContainsKey( si.RenderableSeries );
              }

              return false;
          };

            var newSFiltered = newSInfo.Values.Where( newSFilter );

            foreach ( var filtered in newSFiltered )
            {
                SeriesInfo.Add( filtered );
            }

            foreach ( SeriesInfo s in SeriesInfo )
            {
                var other = newSInfo[s.RenderableSeries];

                if ( other.RenderableSeries != s.RenderableSeries )
                {
                    throw new InvalidOperationException( "invalid series" );
                }
                s.SeriesName = other.SeriesName;
                s.YValue = other.YValue;
                s.XValue = other.XValue;
                s.Stroke = other.Stroke;
                s.DataSeriesType = other.DataSeriesType;
                s.Value = other.Value;
                s.XyCoordinate = other.XyCoordinate;
                s.IsHit = other.IsHit;
                s.DataSeriesIndex = other.DataSeriesIndex;

                if ( s is OhlcSeriesInfo && other is OhlcSeriesInfo )
                {
                    var deepCopy = ( OhlcSeriesInfo )s;
                    var deepSrc = ( OhlcSeriesInfo )other;

                    deepCopy.HighValue = deepSrc.HighValue;
                    deepCopy.LowValue = deepSrc.LowValue;
                    deepCopy.OpenValue = deepSrc.OpenValue;
                    deepCopy.CloseValue = deepSrc.CloseValue;

                }


            }
        }


        protected override void HandleSlaveMouseEvent( Point point_0 )
        {
        }

        protected override void ClearAll()
        {
        }

        public override void OnModifierMouseMove( ModifierMouseArgs e )
        {
            HandleMasterMouseEvent( e.MousePoint );

            base.OnModifierMouseMove( e );
        }

        public override void OnParentSurfaceRendered( SciChartRenderedMessage msg )
        {
            base.OnParentSurfaceRendered( msg );

            int count = 0;

            if ( ParentSurface.RenderableSeries != null )
            {
                count = ParentSurface.RenderableSeries.Count;
            }


            if ( _count == count )
            {
                return;
            }

            _count = count;

            HandleMouseEvent( new ModifierMouseArgs( new Point( ModifierSurface.ActualWidth / 2.0, ModifierSurface.ActualHeight / 2.0 ), MouseButtons.None, MouseModifier.None, true, null ) );
        }


    }
}