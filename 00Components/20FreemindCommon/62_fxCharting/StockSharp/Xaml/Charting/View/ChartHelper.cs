using DevExpress.Xpf.Core;
using Ecng.Common;
using Ecng.Xaml;
using SciChart.Charting.Themes;
using SciChart.Charting;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Strategies;
using StockSharp.Localization;
using System;
using System.Collections.Generic; using fx.Collections;
using System.IO;
using System.Drawing;
using System.Windows.Media;
using StockSharp.Configuration;

using StockSharp.Algo.Indicators;
using System.Linq;
using fx.Charting.IndicatorPainters;
using System.ComponentModel;
using Ecng.Collections;
using StockSharp.Xaml;

namespace fx.Charting
{
    public static class ChartHelper
    {
        public static int GetFifoCapcity( TimeSpan period )
        {
            if ( period == TimeSpan.FromMinutes( 1 ) )
            {
                return 10000;
            }
            else if ( period == TimeSpan.FromMinutes( 5 ) )
            {
                return 5000;
            }
            else if ( period == TimeSpan.FromMinutes( 15 ) )
            {
                return 3000;
            }
            else if ( period > TimeSpan.FromDays( 1 ) )
            {
                return 1000;
            }

            return 2000;
        }

        static ChartHelper( )
        {
            // Tony: Can't find RootSection in new version

            //StockSharpSection rootSection = StockSharp.Configuration.Extensions.RootSection;
            //_customIndicators = ( rootSection != null ? rootSection.CustomIndicators.SafeAdd( new Func<IndicatorElement, IndicatorType>( elem => new IndicatorType( elem.Type.To<Type>( ), elem.Painter.To<Type>( ) ) ) )  : null ) ?? Array.Empty<IndicatorType>( );
        }

        private static readonly IndicatorType[ ] _customIndicators = Array.Empty<IndicatorType>();
        private static IndicatorType[ ] _indicatorTypes;

        public static void Draw( this IChart chart, CandlestickUI element, Candle candle )
        {
            if( element == null )
            {
                throw new ArgumentNullException( nameof( element ) );
            }
            if( candle == null )
            {
                throw new ArgumentNullException( nameof( candle ) );
            }
            ChartDrawDataEx data = new ChartDrawDataEx( );
            data.Group( candle.OpenTime ).Add( element, candle );
            chart.Draw( data );
        }

        //[Obsolete( "Use the Draw method instead." )]
        //public static void Draw( this IChart chart,
        //                         DateTimeOffset time,
        //                         IChartElement element,
        //                         object value )
        //{
        //    if( chart == null )
        //    {
        //        throw new ArgumentNullException( nameof( chart ) );
        //    }
        //    chart.Draw( time,
        //                  new PooledDictionary<IChartElement, object>( )
        //    {
        //        {
        //            element,
        //            value
        //        }
        //    } );
        //}

        //[Obsolete( "Use the Draw method instead." )]
        //public static void Draw( this IChart chart,
        //                         DateTimeOffset time,
        //                         IDictionary< IChartElement, object > values )
        //{
        //    if( chart == null )
        //    {
        //        throw new ArgumentNullException( nameof( chart ) );
        //    }
        //    chart.Draw( new RefPair<DateTimeOffset, IDictionary<IChartElement, object>>[ 1 ]
        //    {
        //        RefTuple.Create( time, values )
        //    } );
        //}

        //[Obsolete( "Use the Draw method instead." )]
        //public static void Draw( this IChart chart, IEnumerable< RefPair< DateTimeOffset, IDictionary< IChartElement, object > > > values )
        //{
        //    chart.Draw( new ChartDrawDataEx( values ) );
        //}

        public static Stream SaveToImage( this IChart chart )
        {
            if( chart == null )
            {
                throw new ArgumentNullException( nameof( chart ) );
            }
            if( !( chart is Chart elem ) )
            {
                throw new ArgumentException( "LocalizedStrings.Str1970Params.Put( chart.GetType() ), nameof( chart )" );
            }
            MemoryStream memoryStream = new MemoryStream( );
            elem.GetImage( ).SaveImage( memoryStream );
            memoryStream.Position = 0L;
            return memoryStream;
        }

        public static ChartArea GetArea( this Chart chart, int index )
        {
            if( chart == null )
            {
                throw new ArgumentNullException( nameof( chart ) );
            }
            if( index < 0 )
            {
                throw new ArgumentOutOfRangeException( nameof( index ) );
            }
            while( chart.ChartAreas.Count < index + 1 )
            {
                ChartArea chartArea = new ChartArea( )
                {
                    Title = LocalizedStrings.Panel + " " +   index,
                    XAxisType = chart.XAxisType
                };

                var viewModel = new ScichartSurfaceMVVM( chartArea );
                chartArea.ChartSurfaceViewModel = viewModel;

                chart.ChartAreas.Add( chartArea );
            }
            return chart.ChartAreas[ index ];
        }

        public static IChart GetChart( this Strategy strategy )
        {
            if( strategy == null )
            {
                throw new ArgumentNullException( nameof( strategy ) );
            }
            return strategy.Environment.GetValue( "Chart", ( IChart )null );
        }

        public static void SetChart( this Strategy strategy, IChart chart )
        {
            if( strategy == null )
            {
                throw new ArgumentNullException( nameof( strategy ) );
            }
            strategy.Environment.SetValue( "Chart", chart );
        }

        internal static string Title( this CandleSeries candleSeries_0 )
        {
            if( candleSeries_0 != null )
            {
                return string.Format( "{0} ({1})", candleSeries_0.Security.Id, candleSeries_0.Arg );
            }
            return string.Empty;
        }

        public static string ToChartTheme( this string appTheme )
        {
            return !appTheme.IsDark( ) ? "Chrome" : "ExpressionDark";
        }

        public static void UpdateTheme( this IThemeableChart chart )
        {
            if( chart == null )
            {
                throw new ArgumentNullException( nameof( chart ) );
            }

            var sciChartName = ApplicationThemeHelper.ApplicationThemeName.ToChartTheme( );

            if ( sciChartName == "Chrome" )
            {
                IThemeProvider tcp = SciChart.Charting.ThemeManager.GetThemeProvider( "Chrome" );

                System.Windows.Media.Brush brush = new SolidColorBrush( Colors.White );

                tcp.SciChartBackground = brush;

            }
            else if ( sciChartName == "ExpressionDark" )
            {
                IThemeProvider tcp = SciChart.Charting.ThemeManager.GetThemeProvider( "ExpressionDark" );

                System.Windows.Media.Brush brush = new SolidColorBrush( Colors.Black );

                tcp.SciChartBackground = brush;
            }

            chart.ChartTheme = sciChartName;
        }

        public static bool IsVolumeProfileChart( this ChartCandleDrawStyles style )
        {
            if( style != ChartCandleDrawStyles.BoxVolume )
            {
                return style == ChartCandleDrawStyles.ClusterProfile;
            }
            return true;
        }

        public static IEnumerable<IndicatorType> GetIndicatorTypes( )
        {
            if ( _indicatorTypes == null )
            {
                var ns = typeof( IIndicator ).Namespace;

                var rendererTypes = typeof( Chart ).Assembly.GetTypes( )
                                                            .Where( t => !t.IsAbstract && 
                                                                    typeof( BaseChartIndicatorPainter ).IsAssignableFrom( t ) && 
                                                                    t.GetAttribute<IndicatorAttribute>( ) != null 
                                                                  )
                                                            .ToDictionary( t => t.GetAttribute<IndicatorAttribute>( ).Type );

                _indicatorTypes = typeof( IIndicator ).Assembly.GetTypes( )
                                                               .Where( t => t.Namespace == ns && 
                                                                       !t.IsAbstract && 
                                                                       typeof( IIndicator ).IsAssignableFrom( t ) && 
                                                                       t.GetConstructor( Type.EmptyTypes ) != null && 
                                                                       t.GetAttribute<BrowsableAttribute>( )?.Browsable != false 
                                                                     )
                                                               .Select( t => new IndicatorType( t /*, rendererTypes.TryGetValue( t )*/ ) )
                                                               .Concat( _customIndicators )
                                                               .OrderBy( t => t.Name )
                                                               .ToArray( );
            }

            return _indicatorTypes;
        }

        /// <summary>
        /// Fill <see cref="IChart.IndicatorTypes"/> using <see cref="GetIndicatorTypes"/>.
        /// </summary>
        /// <param name="chart">Chart.</param>
        public static void FillIndicators( this IChart chart )
        {
            if ( chart == null )
                throw new ArgumentNullException( nameof( chart ) );

            chart.IndicatorTypes.Clear( );
            chart.IndicatorTypes.AddRange( GetIndicatorTypes( ) );
        }
    }
}
