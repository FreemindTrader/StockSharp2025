using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Ecng.Xaml.Charting.Common;
using Ecng.Xaml.Charting.Common.Extensions;
using Ecng.Xaml.Charting.Model.DataSeries;
using Ecng.Xaml.Charting.Numerics.CoordinateCalculators;
using Ecng.Xaml.Charting.Numerics.PointResamplers;
using Ecng.Xaml.Charting.Rendering.Common;
using Ecng.Xaml.Charting.Services;
using Ecng.Xaml.Charting.StrategyManager;
using Ecng.Xaml.Charting.Utility;
using Ecng.Xaml.Charting.Visuals;
using Ecng.Xaml.Charting.Visuals.Axes;
using Ecng.Xaml.Charting.Visuals.RenderableSeries;

namespace Ecng.Xaml.Charting
{
    internal class UltrachartRenderer : IUltrachartRenderer
    {
        private readonly UltrachartSurface _ultraChartSurface;

        internal UltrachartRenderer( UltrachartSurface ultraChartSurface )
        {
            this._ultraChartSurface = ultraChartSurface;
        }

        private static void DrawSeries( ISciChartSurface scs, RenderPassInfo rpi, IRenderContext2D renderContext, int seriesIndex )
        {
            ICoordinateCalculator<double> coordinateCalculator;
            ICoordinateCalculator<double> coordinateCalculator1;
            IRenderableSeries renderableSeries = rpi.RenderableSeries[seriesIndex];
            if ( rpi.YCoordinateCalculators.TryGetValue( renderableSeries.YAxisId, out coordinateCalculator1 ) && rpi.XCoordinateCalculators.TryGetValue( renderableSeries.XAxisId, out coordinateCalculator ) )
            {
                RenderPassData renderPassDatum = new RenderPassData(rpi.IndicesRanges[seriesIndex], coordinateCalculator, coordinateCalculator1, rpi.PointSeries[seriesIndex], rpi.TransformationStrategy);
                renderableSeries.OnDraw( renderContext, renderPassDatum );
            }
        }

        private static IPointSeries GetFirstPointSeries( ISciChartSurface scs, RenderPassInfo rpi, Func<IRenderableSeries, bool> pred )
        {
            IRenderableSeries renderableSeries = scs.RenderableSeries.FirstOrDefault<IRenderableSeries>((IRenderableSeries rs) =>
            {
                if (!pred(rs))
                {
                    return false;
                }
                IDataSeries dataSeries = rs.DataSeries;
                if (dataSeries != null)
                {
                    return dataSeries.HasValues;
                }
                return false;
            });
            if ( rpi.PointSeries.IsNullOrEmpty<IPointSeries>() || renderableSeries == null )
            {
                return null;
            }
            IDataSeries[] dataSeriesArray = rpi.DataSeries;
            for ( int i = 0 ; i < ( int ) dataSeriesArray.Length ; i++ )
            {
                if ( rpi.RenderableSeries[ i ].IsVisible && dataSeriesArray[ i ] == renderableSeries.DataSeries )
                {
                    return rpi.PointSeries[ i ];
                }
            }
            return null;
        }

        private bool IsSurfaceValid( ISciChartSurface surface, out RendererErrorCode errorResult )
        {
            bool flag = true;
            string empty = string.Empty;
            if ( surface.RenderSurface == null )
            {
                empty = string.Concat( RendererErrorCodes.BecauseRenderSurfaceIsNull.Value, RendererErrorCodes.ToDisableThisMessage );
                flag = false;
            }
            else if ( surface.XAxes == null || surface.YAxes == null )
            {
                empty = string.Concat( RendererErrorCodes.BecauseXAxesOrYAxesIsNull.Value, RendererErrorCodes.ToDisableThisMessage );
                flag = false;
            }
            else if ( surface.XAxes.IsEmpty<IAxis>() )
            {
                empty = string.Concat( RendererErrorCodes.BecauseThereAreNoXAxes.Value, RendererErrorCodes.ToDisableThisMessage );
                flag = false;
            }
            else if ( surface.YAxes.IsEmpty<IAxis>() )
            {
                empty = string.Concat( RendererErrorCodes.BecauseThereAreNoYAxes.Value, RendererErrorCodes.ToDisableThisMessage );
                flag = false;
            }
            else if ( surface.XAxis is CategoryDateTimeAxis && surface.RenderableSeries.IsNullOrEmpty<IRenderableSeries>() )
            {
                empty = string.Concat( RendererErrorCodes.BecauseUsingCategoryDateTimeAxisAndNoRenderableSeries.Value, RendererErrorCodes.ToDisableThisMessage );
                flag = false;
            }
            errorResult = ( string.IsNullOrWhiteSpace( empty ) ? RendererErrorCodes.Success : new RendererErrorCode( empty ) );
            return flag;
        }

        private bool IsSurfaceValid( ISciChartSurface surface, Size viewportSize, out RendererErrorCode errorResult )
        {
            bool flag = (viewportSize.Width < 2 ? false : viewportSize.Height >= 2);
            bool flag1 = (surface.XAxes.Any<IAxis>((IAxis x) =>
            {
                if (x.HasValidVisibleRange)
                {
                    return false;
                }
                return x.AutoRange == AutoRange.Never;
            }) ? false : !surface.YAxes.Any<IAxis>((IAxis y) =>
            {
                if (y.HasValidVisibleRange)
                {
                    return false;
                }
                return y.AutoRange == AutoRange.Never;
            }));
            bool flag2 = (surface.XAxes.Any<IAxis>((IAxis x) => x.TickProvider == null) ? false : !surface.YAxes.Any<IAxis>((IAxis y) => y.TickProvider == null));
            bool flag3 = !this._ultraChartSurface.RenderableSeries.IsNullOrEmpty<IRenderableSeries>();
            bool flag4 = (!flag3 ? false : this._ultraChartSurface.RenderableSeries.All<IRenderableSeries>((IRenderableSeries x) => x.DataSeries != null));
            bool renderSurface = surface.RenderSurface != null;
            string empty = string.Empty;
            if ( !flag )
            {
                empty = string.Concat( empty, RendererErrorCodes.BecauseViewportSizeIsNotValid, "\r\n\r\n" );
            }
            if ( !flag1 )
            {
                empty = string.Concat( empty, RendererErrorCodes.BecauseVisibleRangeIsNullOrZeroOnOneOrMoreXOrYAxes, "\r\n\r\n" );
            }
            if ( !flag2 )
            {
                empty = string.Concat( empty, RendererErrorCodes.BecauseTickProviderIsNull, "\r\n\r\n" );
            }
            if ( !flag3 )
            {
                empty = string.Concat( empty, RendererErrorCodes.BecauseThereAreNoRenderableSeries, "\r\n\r\n" );
            }
            if ( !flag4 )
            {
                empty = string.Concat( empty, RendererErrorCodes.BecauseThereAreNoDataSeries, "\r\n\r\n" );
            }
            if ( !renderSurface )
            {
                empty = string.Concat( empty, RendererErrorCodes.BecauseRenderSurfaceIsNull, "\r\n\r\n" );
            }
            if ( !string.IsNullOrWhiteSpace( empty ) )
            {
                empty = string.Concat( empty, RendererErrorCodes.ToDisableThisMessage );
            }
            errorResult = ( string.IsNullOrWhiteSpace( empty ) ? RendererErrorCodes.Success : new RendererErrorCode( empty ) );
            return flag & flag1 & flag2;
        }

        internal static void OnDrawAnnotations( UltrachartSurface scs, RenderPassInfo rpi )
        {
            if ( scs.Annotations != null )
            {
                scs.Annotations.RefreshPositions( rpi );
            }
        }

        internal static void OnDrawAxes( ISciChartSurface scs, RenderPassInfo rpi, IRenderContext2D renderContext )
        {
            foreach ( IAxis xAxis in scs.XAxes )
            {
                xAxis.ValidateAxis();
                xAxis.OnDraw( renderContext, null );
            }
            foreach ( IAxis yAxis in scs.YAxes )
            {
                yAxis.ValidateAxis();
                yAxis.OnDraw( renderContext, null );
            }
            renderContext.Layers.Flush();
        }

        internal static void OnDrawSeries( ISciChartSurface scs, RenderPassInfo rpi, IRenderContext2D renderContext )
        {
            if ( rpi.RenderableSeries == null )
            {
                return;
            }
            List<int> nums = new List<int>();
            for ( int i = 0 ; i < ( int ) rpi.RenderableSeries.Length ; i++ )
            {
                IRenderableSeries renderableSeries = rpi.RenderableSeries[i];
                if ( renderableSeries != null )
                {
                    renderableSeries.XAxis = scs.XAxes.GetAxisById( renderableSeries.XAxisId, true );
                    renderableSeries.YAxis = scs.YAxes.GetAxisById( renderableSeries.YAxisId, true );
                    if ( !rpi.RenderableSeries[ i ].IsSelected )
                    {
                        UltrachartRenderer.DrawSeries( scs, rpi, renderContext, i );
                    }
                    else
                    {
                        nums.Add( i );
                    }
                }
            }
            foreach ( int num in nums )
            {
                UltrachartRenderer.DrawSeries( scs, rpi, renderContext, num );
            }
            scs.Services.GetService<IEventAggregator>().Publish<UltrachartRenderedMessage>( new UltrachartRenderedMessage( scs, renderContext ) );
        }

        internal Size OnLayoutUltrachart( ISciChartSurface scs )
        {
            using ( IUpdateSuspender updateSuspender = scs.SuspendUpdates() )
            {
                updateSuspender.ResumeTargetOnDispose = false;
                if ( scs.ViewportManager == null )
                {
                    throw new InvalidOperationException( "UltrachartSurface.ViewportManager is null. Try setting a new DefaultViewportManager()" );
                }
                scs.XAxes.ForEachDo<IAxis>( ( IAxis x ) => this.TryPerformAutorangeOn( x, scs ) );
                scs.YAxes.ForEachDo<IAxis>( ( IAxis x ) => this.TryPerformAutorangeOn( x, scs ) );
            }
            return scs.OnArrangeUltrachart();
        }

        internal static RenderPassInfo PrepareRenderData( ISciChartSurface scs, Size viewportSize )
        {
            IndexRange indexRange;
            IPointSeries pointSeries;
            IDataSeries dataSeries;
            List<IRenderableSeries> list;
            ObservableCollection<IRenderableSeries> renderableSeries = scs.RenderableSeries;
            if ( renderableSeries != null )
            {
                list = renderableSeries.Where<IRenderableSeries>( ( IRenderableSeries rs ) =>
                {
                    if ( scs.XAxes.GetAxisById( rs.XAxisId, false ) == null )
                    {
                        return false;
                    }
                    return scs.YAxes.GetAxisById( rs.YAxisId, false ) != null;
                } ).ToList<IRenderableSeries>();
            }
            else
            {
                list = null;
            }
            List<IRenderableSeries> renderableSeries1 = list;
            int num = (renderableSeries1 != null ? renderableSeries1.Count : 0);
            RenderPassInfo renderPassInfo = new RenderPassInfo()
            {
                ViewportSize = viewportSize,
                Warnings = new List<string>()
            };
            RenderPassInfo item = renderPassInfo;
            if ( Math.Abs( item.ViewportSize.Width ) < 4.94065645841247E-324 || Math.Abs( item.ViewportSize.Height ) < 4.94065645841247E-324 )
            {
                renderPassInfo = new RenderPassInfo();
                return renderPassInfo;
            }
            UltrachartDebugLogger.Instance.WriteLine( "Drawing {0}: Width={1}, Height={2}", new object[ ] { scs.GetType().Name, item.ViewportSize.Width, item.ViewportSize.Height } );
            item.RenderableSeries = new IRenderableSeries[ num ];
            item.PointSeries = new IPointSeries[ num ];
            item.DataSeries = new IDataSeries[ num ];
            item.IndicesRanges = new IndexRange[ num ];
            IPointResamplerFactory service = scs.Services.GetService<IPointResamplerFactory>();
            Guard.NotNull( service, "resamplerFactory" );
            UltrachartRenderer.PrepareXAxes( scs );
            for ( int i = 0 ; i < num ; i++ )
            {
                IRenderableSeries item1 = renderableSeries1[i];
                UltrachartRenderer.ResampleSeries( scs.XAxes, item1, item, service, out dataSeries, out indexRange, out pointSeries );
                item.RenderableSeries[ i ] = renderableSeries1[ i ];
                item.DataSeries[ i ] = dataSeries;
                item.IndicesRanges[ i ] = indexRange;
                item.PointSeries[ i ] = pointSeries;
            }
            UltrachartRenderer.PrepareXAxes( scs, item );
            UltrachartRenderer.PrepareYAxes( scs, item );
            item.XCoordinateCalculators = new Dictionary<string, ICoordinateCalculator<double>>();
            item.YCoordinateCalculators = new Dictionary<string, ICoordinateCalculator<double>>();
            scs.YAxes.ForEachDo<IAxis>( ( IAxis y ) => item.YCoordinateCalculators.Add( y.Id, y.GetCurrentCoordinateCalculator() ) );
            scs.XAxes.ForEachDo<IAxis>( ( IAxis x ) => item.XCoordinateCalculators.Add( x.Id, x.GetCurrentCoordinateCalculator() ) );
            item.TransformationStrategy = scs.Services.GetService<IStrategyManager>().GetTransformationStrategy();
            return item;
        }

        internal static void PrepareXAxes( ISciChartSurface scs )
        {
            foreach ( IAxis xAxis in scs.XAxes )
            {
                using ( IUpdateSuspender updateSuspender = xAxis.SuspendUpdates() )
                {
                    updateSuspender.ResumeTargetOnDispose = false;
                    IRange range = scs.ViewportManager.CalculateNewXAxisRange(xAxis);
                    if ( !range.Equals( xAxis.VisibleRange ) && xAxis.IsValidRange( range ) )
                    {
                        xAxis.VisibleRange = range;
                    }
                }
            }
        }

        internal static void PrepareXAxes( ISciChartSurface scs, RenderPassInfo rpi )
        {
            foreach ( IAxis axi in
                from x in scs.XAxes
                where x != null
                select x )
            {
                IPointSeries firstPointSeries = UltrachartRenderer.GetFirstPointSeries(scs, rpi, (IRenderableSeries x) =>
                {
                    if (x.XAxisId != axi.Id)
                    {
                        return false;
                    }
                    return x.IsVisible;
                });
                axi.OnBeginRenderPass( rpi, firstPointSeries );
            }
        }

        private static void PrepareYAxes( ISciChartSurface scs, RenderPassInfo renderPassInfo )
        {
            foreach ( IAxis yAxis in scs.YAxes )
            {
                using ( IUpdateSuspender updateSuspender = yAxis.SuspendUpdates() )
                {
                    updateSuspender.ResumeTargetOnDispose = false;
                    IRange range = scs.ViewportManager.CalculateNewYAxisRange(yAxis, renderPassInfo);
                    if ( !range.Equals( yAxis.VisibleRange ) && yAxis.IsValidRange( range ) )
                    {
                        yAxis.VisibleRange = range;
                    }
                    IPointSeries firstPointSeries = UltrachartRenderer.GetFirstPointSeries(scs, renderPassInfo, (IRenderableSeries x) =>
                    {
                        if (x.YAxisId != yAxis.Id)
                        {
                            return false;
                        }
                        return x.IsVisible;
                    });
                    yAxis.OnBeginRenderPass( renderPassInfo, firstPointSeries );
                }
            }
        }

        public RendererErrorCode RenderLoop( IRenderContext2D renderContext )
        {
            RendererErrorCode rendererErrorCode;
            if ( !this.IsSurfaceValid( this._ultraChartSurface, out rendererErrorCode ) )
            {
                this._ultraChartSurface.XAxes.ForEachDo<IAxis>( ( IAxis x ) => x.Clear() );
                this._ultraChartSurface.YAxes.ForEachDo<IAxis>( ( IAxis x ) => x.Clear() );
                renderContext.Clear();
                return rendererErrorCode;
            }
            using ( IUpdateSuspender updateSuspender = this._ultraChartSurface.SuspendUpdates() )
            {
                updateSuspender.ResumeTargetOnDispose = false;
                UltrachartDebugLogger.Instance.WriteLine( "Beginning Render Loop ... ", new object[ 0 ] );
                this._ultraChartSurface.RenderableSeries.ForEachDo<IRenderableSeries>( ( IRenderableSeries rs ) => rs.DataSeries.Do<IDataSeries>( ( IDataSeries ds ) => ds.OnBeginRenderPass() ) );
                Size size = this.OnLayoutUltrachart(this._ultraChartSurface);
                if ( this.IsSurfaceValid( this._ultraChartSurface, size, out rendererErrorCode ) )
                {
                    RenderPassInfo renderPassInfo = UltrachartRenderer.PrepareRenderData(this._ultraChartSurface, size);
                    renderContext.Clear();
                    UltrachartRenderer.OnDrawAxes( this._ultraChartSurface, renderPassInfo, renderContext );
                    UltrachartRenderer.OnDrawSeries( this._ultraChartSurface, renderPassInfo, renderContext );
                    UltrachartRenderer.OnDrawAnnotations( this._ultraChartSurface, renderPassInfo );
                    this._ultraChartSurface.OnUltrachartRendered();
                    if ( renderPassInfo.Warnings.Any<string>() )
                    {
                        rendererErrorCode = new RendererErrorCode( string.Concat( new object[ ] { RendererErrorCodes.BecauseOneOrMoreWarningsOccurred, "\r\n - ", string.Join( "\r\n - ", renderPassInfo.Warnings ), "\r\n", RendererErrorCodes.ToDisableThisMessage } ) );
                    }
                }
            }
            return rendererErrorCode;
        }

        private static void ResampleSeries( AxisCollection xAxisCollection, IRenderableSeries renderableSeries, RenderPassInfo renderPassInfo, IPointResamplerFactory resamplerFactory, out IDataSeries dataSeries, out IndexRange pointRange, out IPointSeries resampledSeries )
        {
            pointRange = null;
            resampledSeries = null;
            dataSeries = renderableSeries.DataSeries;
            if ( dataSeries != null && renderableSeries.IsVisible )
            {
                IAxis axisById = xAxisCollection.GetAxisById(renderableSeries.XAxisId, true);
                axisById.AssertDataType( dataSeries.XType );
                IRange visibleRange = axisById.VisibleRange;
                pointRange = dataSeries.GetIndicesRange( visibleRange );
                if ( pointRange.IsDefined )
                {
                    bool displaysDataAsXy = renderableSeries.DisplaysDataAsXy;
                    bool isCategoryAxis = axisById.IsCategoryAxis;
                    if ( isCategoryAxis )
                    {
                        pointRange.Min = Math.Max( pointRange.Min - 1, 0 );
                        pointRange.Max = Math.Min( pointRange.Max + 1, dataSeries.Count - 1 );
                    }
                    pointRange = renderableSeries.GetExtendedXRange( pointRange );
                    pointRange = new IndexRange( Math.Max( 0, pointRange.Min ), Math.Min( dataSeries.Count - 1, pointRange.Max ) );
                    if ( pointRange.IsDefined )
                    {
                        resampledSeries = dataSeries.ToPointSeries( renderableSeries.ResamplingMode, pointRange, ( int ) renderPassInfo.ViewportSize.Width, isCategoryAxis, new bool?( displaysDataAsXy ), visibleRange, resamplerFactory, renderableSeries.PointSeriesArg );
                    }
                }
            }
        }

        internal void TryPerformAutorangeOn( IAxis axis, ISciChartSurface parentSurface )
        {
            bool flag;
            using ( IUpdateSuspender updateSuspender = axis.SuspendUpdates() )
            {
                updateSuspender.ResumeTargetOnDispose = false;
                if ( !axis.HasValidVisibleRange || axis.HasDefaultVisibleRange )
                {
                    flag = ( axis.AutoRange == AutoRange.Once ? true : axis.AutoRange == AutoRange.Always );
                }
                else
                {
                    flag = false;
                }
                if ( flag )
                {
                    IRange range = parentSurface.ViewportManager.CalculateAutoRange(axis);
                    if ( !range.Equals( axis.VisibleRange ) && axis.IsValidRange( range ) )
                    {
                        axis.VisibleRange = range;
                    }
                }
            }
        }
    }
}
