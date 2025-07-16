using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Charting;
using SciChart.Charting.ChartModifiers;
using MoreLinq;

using StockSharp.Localization;
using System;
using System.Collections.Generic; 
using fx.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using Ecng.Drawing;
using StockSharp.Charting;

namespace StockSharp.Xaml.Charting
{
    public partial class EquityCurveChartEx : UserControl, IComponentConnector, IPersistable, IThemeableChart
    {
        private bool _noGapMode = true;
        private readonly ScichartSurfaceMVVM _paneViewModel;
        private readonly ChartModifierBase[ ] _modifiers;
        private bool _enableZoom;

        public EquityCurveChartEx( )
        {
            InitializeComponent( );

            _paneViewModel = ( ScichartSurfaceMVVM )Chart.DataContext;
            _paneViewModel.ShowLegend = true;
            _paneViewModel.Area.XAxises.First( ).AutoRange = true;
            _paneViewModel.Area.YAxises.First( ).AutoRange = true;

            var ZoomModifiers             = new ChartModifierBase[ 4 ];

            var zoomPanModifier           = new ZoomPanModifier( );
            zoomPanModifier.XyDirection   = XyDirection.XDirection;
            zoomPanModifier.ClipModeX     = ClipMode.None;

            var wheelZoomModifier         = new MouseWheelZoomModifier( );
            wheelZoomModifier.XyDirection = XyDirection.XDirection;

            var zoomExtentsModifier       = new ZoomExtentsModifier( );
            zoomExtentsModifier.ExecuteOn = ExecuteOn.MouseDoubleClick;

            var yaxisDragModifier         = new YAxisDragModifier( );
            yaxisDragModifier.AxisId      = "Y";

            ZoomModifiers[ 0 ]            = zoomPanModifier;
            ZoomModifiers[ 1 ]            = wheelZoomModifier;
            ZoomModifiers[ 2 ]            = zoomExtentsModifier;
            ZoomModifiers[ 3 ]            = yaxisDragModifier;

            _modifiers                    = ZoomModifiers;

            _paneViewModel.ChartModifier.ChildModifiers.AddRange( _modifiers );
            

            var EnableModifier                = new ChartModifierBase[ 1 ];
            var rolloverModifier              = new RolloverModifier( );
            rolloverModifier.ShowAxisLabels   = false;
            rolloverModifier.UseInterpolation = false;

            EnableModifier[ 0 ]               = rolloverModifier;

            _paneViewModel.ChartModifier.ChildModifiers.AddRange( EnableModifier );

            Chart.PreviewMouseWheel += new MouseWheelEventHandler( simplechart_0_PreviewMouseWheel );
            Chart.PreviewMouseDoubleClick += new MouseButtonEventHandler( simplechart_0_PreviewMouseDoubleClick );

            foreach ( ChartAxis yAxis in Chart.Area.YAxises )
            {
                yAxis.TextFormatting = "0.##";
            }

            EnableZoom( false );
        }

        static EquityCurveChartEx( )
        {
            LicenseManager.CreateInstance( );
        }

        public string ChartTheme
        {
            get
            {
                return _paneViewModel.SelectedTheme;
            }
            set
            {
                _paneViewModel.SelectedTheme = value;
            }
        }

        public bool NoGapMode
        {
            get
            {
                return _noGapMode;
            }
            set
            {
                if ( _noGapMode == value )
                    return;
                _noGapMode = value;
                Chart.Area.XAxisType = value ? ChartAxisType.DateTime : ChartAxisType.CategoryDateTime;
            }
        }

        public void Draw( ChartDrawData data )
        {
            Chart.Draw( data );
        }

        public IEnumerable<ChartBandElement> Elements
        {
            get
            {
                return _paneViewModel.Area.Elements.Cast<ChartBandElement>( ).ToArray( );
            }
        }

        public ChartBandElement CreateCurve( string title, Color color, DrawStyles style, Guid id = default( Guid ) )
        {
            return CreateCurve( title, color, color, style, id );
        }

        public ChartBandElement CreateCurve( string title, Color color, Color secondColor, DrawStyles style, Guid id = default( Guid ) )
        {
            if ( title == null )
            {
                throw new ArgumentNullException( nameof( title ) );
            }
                
            if ( style == DrawStyles.Band || style == DrawStyles.Area )
            {
                style = DrawStyles.BandOneValue;
            }
                
            var band = new ChartBandElement( );

            band.FullTitle             = title;
            band.Style                 = DrawStyles.BandOneValue;
            band.Line1.ShowAxisMarker  = true;
            band.Line1.Color           = color;
            band.Line1.AdditionalColor = style != DrawStyles.BandOneValue ? Colors.Transparent : color.ToTransparent( 50 );
            band.Line2.Color           = secondColor;
            band.Line2.AdditionalColor = style != DrawStyles.BandOneValue ? Colors.Transparent : secondColor.ToTransparent( 50 );
            

            if ( style != DrawStyles.BandOneValue )
            {
                band.Line2.IsVisible = false;
                band.AddExtraName( "Line2" );
                band.AddName( band.Line1, "LocalizedStrings.Str1898" );
            }
            else
            {
                band.AddName( band.Line1, "LocalizedStrings.Str1898" + " 1" );
                band.AddName( band.Line2, "LocalizedStrings.Str1898" + " 2" );
            }

            if ( id != new Guid( ) )
            {
                band.Id = id;
            }
                
            _paneViewModel.Area.Elements.Add( band );

            return band;
        }

        [Obsolete( "Use CreateCurve() methods which return ChartBandElement." )]
        public ICollection<LineData<DateTime>> CreateCurve( string title, Color color, Color secondColor, LineChartStyles style = LineChartStyles.Line, Guid id = default( Guid ) )
        {
            if ( title == null )
                throw new ArgumentNullException( nameof( title ) );
            return new EquityCurveList( this, CreateCurve( title, color, secondColor, GetDrawStylesByLineStyle( style ), id ) );
        }

        [Obsolete( "Use CreateCurve() methods which returns ChartBandElement." )]
        public ICollection<LineData<DateTime>> CreateCurve( string title, Color color, LineChartStyles style = LineChartStyles.Line, Guid id = default( Guid ) )
        {
            return CreateCurve( title, color, color, style, id );
        }

        public void RemoveCurve( ChartBandElement elem )
        {
            if ( elem == null )
                throw new ArgumentNullException( nameof( elem ) );
            _paneViewModel.Area.Elements.Remove( elem );
        }

        [Obsolete( "Use RemoveCurve(ChartBandElement) instead." )]
        public void RemoveCurve( ICollection<LineData<DateTime>> items )
        {
            if ( items == null )
                throw new ArgumentNullException( nameof( items ) );
            _paneViewModel.Area.Elements.Remove( ( ( EquityCurveList )items ).Element );
        }

        public void Clear( )
        {
            _paneViewModel.Area.Elements.Clear( );
        }

        public void Reset( )
        {
            _paneViewModel.Reset( _paneViewModel.Area.Elements.ToArray( ) );
        }

        public void Reset( IEnumerable<ICollection<LineData<DateTime>>> items )
        {
            _paneViewModel.Reset( items.Cast<EquityCurveList>( ).Select( a => a.Element ) );
        }

        public void Reset( IEnumerable<ChartBandElement> elements )
        {
            _paneViewModel.Reset( elements );
        }

        private void EnableZoom( bool isEnable )
        {
            foreach ( ChartAxis xAxis in Chart.Area.XAxises )
            {
                xAxis.AutoRange = !isEnable;
            }

            foreach ( ChartAxis yAxis in Chart.Area.YAxises )
            {
                yAxis.AutoRange = !isEnable;
            }

            if ( _modifiers != null )
            {
                foreach ( ChartModifierBase modifier in _modifiers )
                {
                    modifier.IsEnabled = isEnable;
                }
            }
            
        }

        private static DrawStyles GetDrawStylesByLineStyle( LineChartStyles lineStyles )
        {
            switch ( lineStyles )
            {
                case LineChartStyles.Area:
                    return DrawStyles.BandOneValue;
                case LineChartStyles.Line:
                    return DrawStyles.Line;
                case LineChartStyles.DashedLine:
                    return DrawStyles.DashedLine;
                default:
                    throw new ArgumentException( "style" );
            }
        }

        public void Load( SettingsStorage storage )
        {
            Clear( );

            foreach ( IChartElement chartElement in ( storage.GetValue<SettingsStorage[ ]>( "elements", null ) ).Select( s => s.Load<ChartBandElement>( ) ) )
            {
                _paneViewModel.Area.Elements.Add( chartElement );
            }
                
        }

        public void Save( SettingsStorage storage )
        {
            storage.SetValue( "elements", Elements.Select( e => e.Save( ) ).ToArray( ) );
        }


        private void simplechart_0_PreviewMouseWheel( object sender, MouseWheelEventArgs e )
        {
            if ( _enableZoom || e.Delta <= 0 )
            {
                return;
            }
                
            _enableZoom = true;
            EnableZoom( true );
        }

        private void simplechart_0_PreviewMouseDoubleClick( object sender, MouseButtonEventArgs e )
        {
            if ( !_enableZoom )
            {
                return;
            }
                
            _enableZoom = false;
            EnableZoom( false );
        }

        

        private sealed class EquityCurveList : BaseList<LineData<DateTime>>
        {
            private readonly EquityCurveChartEx _equityCurveChart;
            private readonly ChartBandElement _element;

            public EquityCurveList( EquityCurveChartEx equityCurveChart_1, ChartBandElement BandsUI_1 )
            {
                _equityCurveChart = equityCurveChart_1;
                _element = BandsUI_1;
            }

            public ChartBandElement Element
            {
                get
                {
                    return _element;
                }
            }

            protected override void OnAdded( LineData<DateTime> lineData )
            {
                base.OnAdded( lineData );
                ChartDrawData data = new ChartDrawData( );
                data.Group( lineData.X ).Add( Element, Decimal.ToDouble( lineData.Y ), 0.0 );
                _equityCurveChart.Chart.Draw( data );
            }

            protected override bool OnRemoving( LineData<DateTime> lineData_0 )
            {
                throw new InvalidOperationException( LocalizedStrings.RemoveNotSupported );
            }

            protected override void OnCleared( )
            {
                _equityCurveChart.Chart.Reset( new ChartBandElement[ 1 ] { Element } );

                base.OnCleared( );
            }
        }        
    }
}
