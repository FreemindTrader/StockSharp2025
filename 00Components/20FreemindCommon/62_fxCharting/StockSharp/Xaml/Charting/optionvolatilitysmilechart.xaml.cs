using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using SciChart.Charting;
using SciChart.Charting.ChartModifiers;
using MathNet.Numerics;
using MoreLinq;
using Ecng.Drawing;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic; using fx.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;


namespace StockSharp.Xaml.Charting
{
    public partial class OptionVolatilitySmileChartDx : UserControl, IComponentConnector, IPersistable, IThemeableChart
    {
        private readonly PooledList<OptionVolatilitySmileList> _smileChartList = new PooledList<OptionVolatilitySmileList>( );
        public static readonly DependencyProperty SmileStepProperty = DependencyProperty.Register( nameof( SmileStep ), typeof( double ), typeof( OptionVolatilitySmileChartDx ), new PropertyMetadata(   10.0, new PropertyChangedCallback( OnSmileStepPropertyChanged ) ) );
        private readonly ScichartSurfaceMVVM _chartPaneViewModel;
        private readonly ChartModifierBase[ ] _modifiers;
        private bool bool_0;


        static OptionVolatilitySmileChartDx( )
        {
            LicenseManager.CreateInstance( );
        }

        public OptionVolatilitySmileChartDx( )
        {
            InitializeComponent( );
            _chartPaneViewModel                 = ( ScichartSurfaceMVVM )Chart.DataContext;
            _chartPaneViewModel.ShowLegend      = true;
            var xAxis                           = _chartPaneViewModel.Area.XAxises.First( );
            var yAxis                           = _chartPaneViewModel.Area.YAxises.First( );
            xAxis.AutoRange                     = true;
            yAxis.AutoRange                     = true;
            yAxis.TextFormatting                = "0.##";
            ChartModifierBase[ ] modifiersArray = new ChartModifierBase[ 4 ];

            var zoomPanModifier                 = new ZoomPanModifier( );
            zoomPanModifier.XyDirection         = XyDirection.XDirection;
            zoomPanModifier.ClipModeX           = ClipMode.None;


            var wheelZoomModifier               = new MouseWheelZoomModifier( );
            wheelZoomModifier.XyDirection       = XyDirection.XDirection;

            var zoomExtentsModifier             = new ZoomExtentsModifier( );
            zoomExtentsModifier.ExecuteOn       = ExecuteOn.MouseDoubleClick;

            var yaxisDragModifier               = new YAxisDragModifier( );
            yaxisDragModifier.AxisId            = "Y";



            modifiersArray[ 0 ]                 = zoomPanModifier;
            modifiersArray[ 1 ]                 = wheelZoomModifier;
            modifiersArray[ 2 ]                 = zoomExtentsModifier;
            modifiersArray[ 3 ]                 = yaxisDragModifier;
            _modifiers                          = modifiersArray;


            var childModifiers                  = _chartPaneViewModel.ChartModifier.ChildModifiers;

            childModifiers.AddRange( _modifiers );

            var modifiersArray2                 = new ChartModifierBase[ 1 ];
            var rolloverModifier                = new RolloverModifier( );
            rolloverModifier.ShowAxisLabels     = false;
            rolloverModifier.UseInterpolation   = false;
            modifiersArray2[ 0 ]                = rolloverModifier;

            CollectionHelper.AddRange( childModifiers, modifiersArray2 );
            Chart.PreviewMouseWheel += new MouseWheelEventHandler( simplechart_0_PreviewMouseWheel );
            Chart.PreviewMouseDoubleClick += new MouseButtonEventHandler( simplechart_0_PreviewMouseDoubleClick );
            Initialize( false );
        }

        public string ChartTheme
        {
            get
            {
                return _chartPaneViewModel.SelectedTheme;
            }
            set
            {
                _chartPaneViewModel.SelectedTheme = value;
            }
        }

        public double SmileStep
        {
            get
            {
                return ( double )GetValue( SmileStepProperty );
            }
            set
            {
                SetValue( SmileStepProperty, value );
            }
        }

        public IEnumerable<KeyValuePair<ICollection<LineData<double>>, VolatilitySmileUI>> Elements
        {
            get
            {
                return _smileChartList.Select( l => new KeyValuePair<ICollection<LineData<double>>, VolatilitySmileUI>( l, l.Element ) ).ToArray( );
            }
        }

        public ICollection<LineData<double>> CreateSmile( string title, Color color, DrawStyles style = DrawStyles.Line, Guid id = default( Guid ) )
        {
            return CreateSmile( title, color, color, style, id );
        }

        public ICollection<LineData<double>> CreateSmile( string title, Color color, Color secondColor, DrawStyles style = DrawStyles.Line, Guid id = default( Guid ) )
        {
            if ( title == null )
            {
                throw new ArgumentNullException( nameof( title ) );
            }

            VolatilitySmileUI element = new VolatilitySmileUI( );
            element.FullTitle = title;
            element.Values.Color = color;
            element.Values.AdditionalColor = secondColor;
            element.Values.Style = DrawStyles.Dot;
            element.Values.StrokeThickness = 8;
            element.Values.ShowAxisMarker = false;
            element.Smile.Color = color;
            element.Smile.AdditionalColor = secondColor;
            element.Smile.Style = style;
            element.Smile.ShowAxisMarker = false;

            if ( !CompareHelper.IsDefault( id ) )
            {
                element.Id = id;
            }

            OptionVolatilitySmileList list = new OptionVolatilitySmileList( this, element );
            _smileChartList.Add( list );
            return list;
        }

        public void RemoveSmile( ICollection<LineData<double>> items )
        {
            OptionVolatilitySmileList class171;
            if ( ( class171 = items as OptionVolatilitySmileList ) == null )
            {
                throw new ArgumentNullException( nameof( items ) );
            }

            if ( !_smileChartList.Remove( class171 ) )
            {
                return;
            }

            class171.Dispose( );
        }

        public void Clear( )
        {
            foreach ( ICollection<LineData<double>> items in _smileChartList.ToArray( ) )
            {
                RemoveSmile( items );
            }
        }

        public void Reset( IEnumerable<ICollection<LineData<double>>> items )
        {
            foreach ( OptionVolatilitySmileList smile in items.Cast<OptionVolatilitySmileList>( ) )
            {
                smile.Clear( );
            }
        }

        private static void OnSmileStepPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( ( OptionVolatilitySmileChartDx )d )._smileChartList.ForEach( l => l.PopulateLineDataList( ) );
        }

        private void Initialize( bool isEnable )
        {
            foreach ( ChartAxis xAxis in Chart.Area.XAxises )
            {
                xAxis.AutoRange = !isEnable;
            }

            foreach ( ChartAxis yAxis in Chart.Area.YAxises )
            {
                yAxis.AutoRange = !isEnable;
            }

            foreach ( ChartModifierBase modifier in _modifiers )
            {
                modifier.IsEnabled = isEnable;
            }
        }

        public void Load( SettingsStorage storage )
        {
            ChartTheme = storage.GetValue( "ChartTheme", ChartTheme );
            Clear( );

            var elementList = ( storage.GetValue<SettingsStorage[ ]>( "elements", null ) ).Select( s =>
            {
                var smileList = new OptionVolatilitySmileList( this );
                smileList.Load( s );
                return smileList;
            } );

            foreach ( var element in elementList )
            {
                _smileChartList.Add( element );
            }
        }



        private void simplechart_0_PreviewMouseWheel( object sender, MouseWheelEventArgs e )
        {
            if ( bool_0 || e.Delta <= 0 )
            {
                return;
            }

            bool_0 = true;
            Initialize( true );
        }

        private void simplechart_0_PreviewMouseDoubleClick( object sender, MouseButtonEventArgs e )
        {
            if ( !bool_0 )
            {
                return;
            }

            bool_0 = false;
            Initialize( false );
        }



        public void Save( SettingsStorage storage )
        {
            throw new NotImplementedException( );
        }

        private sealed class OptionVolatilitySmileList : BaseList<LineData<double>>, IDisposable, IPersistable
        {
            private readonly OptionVolatilitySmileChartDx _chart;
            private readonly PooledList<LineData<double>> _lineDataList = new PooledList<LineData<double>>( );
            private double[ ] _polyPoints;
            private readonly VolatilitySmileUI _chartVolatilitySmileElement;

            public OptionVolatilitySmileList( OptionVolatilitySmileChartDx chart, VolatilitySmileUI element )
            {

                _chart = chart;
                _chartVolatilitySmileElement = element;
                GetSimpleChart( ).Area.Elements.Add( Element );
            }

            public OptionVolatilitySmileList( OptionVolatilitySmileChartDx optionVolatilitySmileChart_1 )
              : this( optionVolatilitySmileChart_1, new VolatilitySmileUI( ) )
            {
            }

            private SimpleChartDx GetSimpleChart( )
            {
                return _chart.Chart;
            }

            public VolatilitySmileUI Element
            {
                get
                {
                    return _chartVolatilitySmileElement;
                }
            }

            protected override void OnAdded( LineData<double> lineData_0 )
            {
                PopulateLineDataList( );
                OnAdded( lineData_0 );
            }

            protected override void OnRemoved( LineData<double> lineData_0 )
            {
                PopulateLineDataList( );
                OnRemoved( lineData_0 );
            }

            protected override void OnCleared( )
            {
                PopulateLineDataList( );
                OnCleared( );
            }

            private void GetDataAndDraw( )
            {
                //GetSimpleChart( ).Reset( new VolatilitySmileUI[ 1 ] { Element } );
                //ChartDrawData data = new ChartDrawData( );

                //using ( IEnumerator<LineData<double>> enumerator = GetEnumerator( ) )
                //{
                //    while ( enumerator.MoveNext( ) )
                //    {
                //        LineData<double> current = enumerator.Current;
                //        data.Group( current.X ).Add( Element.Values, Decimal.ToDouble( current.Y ), double.NaN );
                //    }
                //}

                //foreach ( LineData<double> lineData in _lineDataList )
                //{
                //    data.Group( lineData.X ).Add( Element.Smile, Decimal.ToDouble( lineData.Y ), double.NaN );
                //}

                //GetSimpleChart( ).Draw( data );
            }

            public void PopulateLineDataList( )
            {
                _lineDataList.Clear( );

                if ( Count < 3 )
                {
                    return;
                }

                double min = GetMinimum( );
                _polyPoints = Fit.Polynomial( this.Select( l => l.X ).ToArray( ), this.Select( l => Decimal.ToDouble( l.Y ) ).ToArray( ), 2 );

                if ( _polyPoints.Length != 3 )
                {
                    return;
                }

                double smileStep = _chart.SmileStep;
                double num2 = Math.Min( Math.Max( 0.0001, double.IsNaN( smileStep ) ? 10.0 : smileStep ), min / 2.0 );
                double num3 = this[ Count - 1 ].X + num2;
                double prop1st = this[ 0 ].X;

                while ( prop1st < num3 )
                {
                    _lineDataList.Add( new LineData<double>( )
                    {
                        X = prop1st,
                        Y = GetPolynomialValue( prop1st ).ToDecimal( ) ?? Decimal.Zero
                    } );
                    prop1st += num2;
                }
                GetDataAndDraw( );
            }

            private double GetPolynomialValue( double x )
            {
                return _polyPoints[ 2 ] * x * x + _polyPoints[ 1 ] * x + _polyPoints[ 0 ];
            }

            private double GetMinimum( )
            {
                double min = double.MaxValue;

                for ( int index = 1; index < Count; ++index )
                {
                    double diff = this[ index ].X - this[ index - 1 ].X;

                    if ( diff > 0.0 )
                    {
                        min = Math.Min( min, diff );
                    }
                    else
                    {
                        //throw new InvalidOperationException( StringHelper.Put( LocalizedStrings.Str2014Params, new object[ 2 ] { this[ index ].X, this[ index - 1 ].X } ) );
                    }
                }
                return min;
            }

            public void Dispose( )
            {
                GetSimpleChart( ).Area.Elements.Remove( Element );
            }

            public void Load( SettingsStorage settings )
            {
                Element.Load( settings.GetValue<SettingsStorage>( "Element", null ) );
            }

            public void Save( SettingsStorage settings )
            {
                settings.SetValue( "Element", PersistableHelper.Save( Element ) );
            }


        }

    }
}
