using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
using SciChart.Charting.Visuals.Axes;
using SciChart.Charting.Visuals.PointMarkers;
using SciChart.Data.Model;
using StockSharp.Localization;
using System;
using System.Collections.Generic; using fx.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;

namespace StockSharp.Xaml.Charting
{
    public class ChartLineElement : ChartElement<ChartLineElement>, ICloneable, INotifyPropertyChanging, INotifyPropertyChanged, IChartComponent, IDrawableChartElement, IChartElement
    {
        private TASignalSymbol           _signalType;
        private DrawStyles _indicatorDrawStyle;
        private double                   _dataPointWidth = double.NaN;

        private IScichartSurfaceVM _scichartSurfaceVM;
        private CategoryDateTimeAxis     _xAxis;

        private static int               _pointSize = 24;            // This is the size for the star.
        private int                      _pointColor = 0xFF0000;     // This is red color

        private Color                    _lineColor = Colors.DarkBlue;
        private int                      _strokeThickness = 1;
        private bool                     _antiAliasing = true;
        private Color                    _additionalColor;

        private bool                     _showAxisMarker;
        private ControlTemplate          _drawTemplate;
        private IPointMarker             _pointMarker;
        private UIChartBaseViewModel                 _lineViewModel;

        UIChartBaseViewModel IDrawableChartElement.CreateViewModel( IScichartSurfaceVM viewModel )
        {
            _scichartSurfaceVM = viewModel;
            _lineViewModel     = viewModel.Area.XAxisType == ChartAxisType.Numeric ? new ChartLineElementVM<double>( this ) : ( UIChartBaseViewModel )new ChartLineElementVM<DateTime>( this );

            var xAxis = _scichartSurfaceVM.XAxises.FirstOrDefault( );

            if ( xAxis is CategoryDateTimeAxis )
            {
                _xAxis = ( CategoryDateTimeAxis )xAxis;
            }

            xAxis.VisibleRangeChanged += XAxis_VisibleRangeChanged;

            return _lineViewModel;
        }

        private void XAxis_VisibleRangeChanged( object sender, SciChart.Charting.Visuals.Events.VisibleRangeChangedEventArgs e )
        {            
            if ( _xAxis != null )
            {
                _dataPointWidth = _xAxis.CurrentDatapointWidth;

                var currentDatapointWidth = ( int )_dataPointWidth;

                if ( _pointSize != currentDatapointWidth )
                {
                    _pointSize = currentDatapointWidth;

                    if ( _indicatorDrawStyle == DrawStyles.Dot )
                    {
                        PointMarker = CreatePointMarker( );
                    }
                }                
            }
        }

        public ChartLineElement( )
        {
            DrawTemplate    = GetControlTemplate( );
            PointMarker     = CreatePointMarker( );
            AdditionalColor = XamlHelper.ToTransparent( Color, 50 );            
        }


        [Display( Description = "Str2056", Name = "Str1984", Order = 30, ResourceType = typeof( LocalizedStrings ) )]
        public Color Color
        {
            get
            {
                return _lineColor;
            }
            set
            {
                if ( _lineColor == value )
                {
                    return;
                }

                _lineColor   = value;
                DrawTemplate = GetControlTemplate( );
                PointMarker  = CreatePointMarker( );

                RaisePropertyChanged( nameof( Color ) );
            }
        }

        [Display( Description = "Str2057", Name = "Str1986", Order = 40, ResourceType = typeof( LocalizedStrings ) )]
        public Color AdditionalColor
        {
            get
            {
                return _additionalColor;
            }
            set
            {
                _additionalColor = value;
                DrawTemplate     = GetControlTemplate( );
                PointMarker      = CreatePointMarker( );

                RaisePropertyChanged( nameof( AdditionalColor ) );
            }
        }

        [Display( Description = "Str2058", Name = "Str1956", Order = 50, ResourceType = typeof( LocalizedStrings ) )]
        public int StrokeThickness
        {
            get
            {
                return _strokeThickness;
            }
            set
            {
                if ( _strokeThickness == value )
                {
                    return;
                }

                if ( value < 1 )
                {
                    throw new ArgumentOutOfRangeException( nameof( value ), "LocalizedStrings.Str1989" );
                }

                _strokeThickness = value;
                DrawTemplate     = GetControlTemplate( );
                PointMarker      = CreatePointMarker( );

                RaisePropertyChanged( nameof( StrokeThickness ) );
            }
        }

        [Display( Description = "Str2059", Name = "Str1959", Order = 60, ResourceType = typeof( LocalizedStrings ) )]
        public bool AntiAliasing
        {
            get
            {
                return _antiAliasing;
            }
            set
            {
                _antiAliasing = value;
                RaisePropertyChanged( nameof( AntiAliasing ) );
            }
        }

        [Display( Description = "Str1992", Name = "Str1946", Order = 70, ResourceType = typeof( LocalizedStrings ) )]
        public DrawStyles Style
        {
            get
            {
                return _indicatorDrawStyle;
            }
            set
            {
                if ( _indicatorDrawStyle == value )
                {
                    return;
                }

                RaisePropertyValueChanging( nameof( Style ), value );

                _indicatorDrawStyle = value;
                DrawTemplate = GetControlTemplate( );
                PointMarker  = CreatePointMarker( );

                RaisePropertyChanged( nameof( Style ) );
            }
        }

        [Display( Description = "Str1962", Name = "Str1961", Order = 80, ResourceType = typeof( LocalizedStrings ) )]
        public bool ShowAxisMarker
        {
            get
            {
                return _showAxisMarker;
            }
            set
            {
                _showAxisMarker = value;
                RaisePropertyChanged( nameof( ShowAxisMarker ) );
            }
        }

        [Browsable( false )]
        public ControlTemplate DrawTemplate
        {
            get
            {
                return _drawTemplate;
            }
            set
            {
                _drawTemplate = value;
                RaisePropertyChanged( nameof( DrawTemplate ) );
            }
        }

        
        public TASignalSymbol SignalType
        {
            get { return _signalType; }
            set
            {
                if ( _signalType == value )
                    return;
                _signalType = value;
                RaisePropertyChanged( nameof( SignalType ) );
            }
        }
        


        public int PointSize
        {
            get { return _pointSize; }
            set
            {
                if ( _pointSize == value )
                    return;
                _pointSize = value;
                RaisePropertyChanged( nameof( PointSize ) );
            }
        }

        public int PointColor
        {
            get { return _pointColor; }
            set
            {
                if ( _pointColor == value )
                    return;
                _pointColor = value;

                RaisePropertyChanged( nameof( PointColor ) );
            }
        }


        public IPointMarker PointMarker
        {
            get
            {
                return _pointMarker;
            }
            set
            {
                _pointMarker = value;
                RaisePropertyChanged( nameof( PointMarker ) );
            }
        }

        private IPointMarker CreatePointMarker( )
        {
            IPointMarker point = null;

            var dimension = Style == DrawStyles.Dot ? StrokeThickness : Math.Max( 8.0, 2.0 * StrokeThickness );

            if ( Style == DrawStyles.Dot )
            {
                if ( SignalType == TASignalSymbol.Ellipse )
                {
                    point = new EllipsePointMarker( );
                    point.Fill            = Color;
                    point.Width           = dimension;
                    point.Height          = dimension;
                    point.Stroke          = AdditionalColor;
                    point.StrokeThickness = 2;

                }
                else
                {
                    point = FreemindTaXaml.CreateSpritePointMarker( SignalType, PointSize );
                }                
            }

            //
            return point;
        }


        //private IPointMarker CreateSpritePointMarker( TASignalSymbol symbol )
        //{
        //    var sprite = new SpritePointMarker( );

        //    switch ( symbol )
        //    {
        //        case TASignalSymbol.Star:
        //        {
        //            int colorInt = 0x00FF00;
        //            sprite.PointMarkerTemplate = GetSpriteControlTemplate( symbol, PointSize, colorInt );
        //        }
        //        break;

        //        case TASignalSymbol.PositiveDivergence:
        //        {
        //            int positiveDiv = 0x4CAF50;
        //            sprite.PointMarkerTemplate = GetSpriteControlTemplate( symbol, PointSize, positiveDiv );
        //        }
        //        break;
        //        case TASignalSymbol.ImportantPositive:
        //        {
        //            int imptPosDivergence = 0x44C4A1;
        //            int imptPosDivergence2 = 0x3EA69B;

        //            sprite.PointMarkerTemplate = GetSpriteControlTemplate( symbol, PointSize, imptPosDivergence, imptPosDivergence2 );
        //        }
        //        break;

        //        case TASignalSymbol.NegativeDivergence:
        //        {
        //            int negativeDiv = 0xF44336;
        //            sprite.PointMarkerTemplate = GetSpriteControlTemplate( symbol, PointSize, negativeDiv );
        //        }
        //        break;

        //        case TASignalSymbol.ImportantNegative:
        //        {
        //            int imptNegDivergence = 0xBF360C;
        //            int imptNegDivergence2 = 263238;

        //            sprite.PointMarkerTemplate = GetSpriteControlTemplate( symbol, PointSize, imptNegDivergence, imptNegDivergence2 );
        //        }
        //        break;

        //        case TASignalSymbol.ImportantHiddenNegDiv:
        //        {
        //            int imptNegDivergence = 0xFF0000;

        //            sprite.PointMarkerTemplate = GetSpriteControlTemplate( symbol, PointSize, imptNegDivergence );
        //        }
        //        break;

        //        case TASignalSymbol.ImportantHiddenPosDiv:
        //        {
        //            int imptNegDivergence = 0x00FF00;

        //            sprite.PointMarkerTemplate = GetSpriteControlTemplate( symbol, PointSize, imptNegDivergence );
        //        }
        //        break;
        //    }

        //    return sprite;
        //}

        //private ControlTemplate GetSpriteControlTemplate( TASignalSymbol symbol, int width, int colorInt )
        //{
        //    string xaml = "";
        //    switch ( symbol )
        //    {
        //        case TASignalSymbol.Star:
        //        xaml = FreemindTaXaml.GetWaveImportance( width, colorInt );
        //        break;

        //        case TASignalSymbol.PositiveDivergence:
        //        xaml = FreemindTaXaml.GetPositiveDivergence( width, colorInt );
        //        break;

        //        case TASignalSymbol.NegativeDivergence:
        //        xaml = FreemindTaXaml.GetNegativeDivergence( width, colorInt );
        //        break;

        //        case TASignalSymbol.ImportantHiddenNegDiv:
        //        xaml = FreemindTaXaml.GetImptHiddenNegativeDivergence( width, colorInt );
        //        break;

        //        case TASignalSymbol.ImportantHiddenPosDiv:
        //        xaml = FreemindTaXaml.GetImptHiddenPositiveDivergence( width, colorInt );
        //        break;
        //    }

        //    return GetControlTemplateFromString( xaml );
        //}

        //private static ControlTemplate GetControlTemplateFromString( string xaml )
        //{
        //    var sr = new MemoryStream( Encoding.ASCII.GetBytes( xaml ) );
        //    var pc = new ParserContext( );

        //    pc.XmlnsDictionary.Add( "", "http://schemas.microsoft.com/winfx/2006/xaml/presentation" );
        //    pc.XmlnsDictionary.Add( "x", "http://schemas.microsoft.com/winfx/2006/xaml" );

        //    var templ = XamlReader.Load( sr, pc );

        //    return ( ControlTemplate )templ;
        //}

        //private ControlTemplate GetSpriteControlTemplate( TASignalSymbol symbol, int width, int colorInt, int colorInt2 )
        //{
        //    string xaml = "";
        //    switch ( symbol )
        //    {
        //        case TASignalSymbol.Star:
        //        xaml = FreemindTaXaml.GetWaveImportance( width, colorInt );
        //        break;

        //        case TASignalSymbol.PositiveDivergence:
        //        xaml = FreemindTaXaml.GetPositiveDivergence( width, colorInt );
        //        break;

        //        case TASignalSymbol.NegativeDivergence:
        //        xaml = FreemindTaXaml.GetNegativeDivergence( width, colorInt );
        //        break;

        //        case TASignalSymbol.ImportantPositive:
        //        xaml = FreemindTaXaml.GetImportantPositiveDivergence( width, colorInt, colorInt2 );
        //        break;

        //        case TASignalSymbol.ImportantNegative:
        //        xaml = FreemindTaXaml.GetImportantNegativeDivergence( width, colorInt, colorInt2 );
        //        break;

        //        case TASignalSymbol.ImportantHiddenNegDiv:
        //        xaml = FreemindTaXaml.GetImptHiddenNegativeDivergence( width, colorInt );
        //        break;

        //        case TASignalSymbol.ImportantHiddenPosDiv:
        //        xaml = FreemindTaXaml.GetImptHiddenPositiveDivergence( width, colorInt );
        //        break;
        //    }

        //    return GetControlTemplateFromString( xaml );
        //}

        private ControlTemplate GetControlTemplate( )
        {
            ControlTemplate templ = new ControlTemplate( );
            templ.VisualTree = new FrameworkElementFactory( typeof( Ellipse ) );
            double num = Style == DrawStyles.Dot ?   StrokeThickness : Math.Max( 8.0, 2.0 *   StrokeThickness );
            templ.VisualTree.SetValue( Shape.FillProperty, new SolidColorBrush( XamlHelper.ToTransparent( Color, 150 ) ) );
            templ.VisualTree.SetValue( FrameworkElement.WidthProperty, num );
            templ.VisualTree.SetValue( FrameworkElement.HeightProperty, num );
            templ.VisualTree.SetValue( Shape.StrokeProperty, new SolidColorBrush( AdditionalColor ) );
            templ.VisualTree.SetValue( Shape.StrokeThicknessProperty, 2.0 );
            return templ;
        }

        

        bool IDrawableChartElement.StartDrawing( IEnumerableEx<ChartDrawData.IDrawValue> drawValues )
        {
            return _lineViewModel.Draw( drawValues );
        }

        void IDrawableChartElement.StartDrawing( )
        {
            _lineViewModel.Draw(   Enumerable.Empty<ChartDrawData.IDrawValue>( ).ToEx( 0 ) );
        }

        protected override bool OnDraw( ChartDrawData data )
        {
            var drawValues = data.GetLineDrawValues( this );

            if ( drawValues != null && !drawValues.IsEmpty( ) )
            {
                return ( ( IDrawableChartElement )this ).StartDrawing( drawValues );
            }

            return false;
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );

            Color           = storage.GetValue( "Color", 0 ).ToColor();
            AdditionalColor = storage.GetValue( "AdditionalColor", 0 ).ToColor( );
            StrokeThickness = storage.GetValue( "StrokeThickness", 0 );
            AntiAliasing    = storage.GetValue( "AntiAliasing", false );
            Style           = storage.GetValue<DrawStyles>( "Style", 0 );
            ShowAxisMarker  = storage.GetValue( "ShowAxisMarker", false );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "Color", Color.ToInt() );
            storage.SetValue( "AdditionalColor", AdditionalColor.ToInt() );
            storage.SetValue( "StrokeThickness", StrokeThickness );
            storage.SetValue( "AntiAliasing", ( AntiAliasing ) );
            storage.SetValue( "Style", Converter.To<string>( Style ) );
            storage.SetValue( "ShowAxisMarker", ( ShowAxisMarker ) );
        }

        internal override ChartLineElement Clone( ChartLineElement other )
        {
            other = base.Clone( other );

            other.Color           = Color;
            other.AdditionalColor = AdditionalColor;
            other.StrokeThickness = StrokeThickness;
            other.AntiAliasing    = AntiAliasing;
            other.Style           = Style;
            other.ShowAxisMarker  = ShowAxisMarker;

            return other;
        }

        public override bool CheckAxesCompatible( ChartAxisType? xType, ChartAxisType? yType )
        {
            if ( !yType.HasValue )
                return true;

            return yType.GetValueOrDefault( ) == ChartAxisType.Numeric & yType.HasValue;
        }
    }
}
