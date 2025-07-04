using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using StockSharp.Localization;
using System;
using System.Collections.Generic; using fx.Collections;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;

namespace fx.Charting
{
    public class AnnotationUI : ChartComponent< AnnotationUI >, ICloneable< IChartElement >, INotifyPropertyChanged, IElementWithXYAxes, IDrawableChartElement, ICloneable, INotifyPropertyChanging, IChartElement
    {
        private ChartAnnotationTypes _annotatinType;
        private AnnotationVM _viewModel;

        public AnnotationUI( )
        {
            IsLegend = false;
        }

        [Browsable( false )]
        public ChartAnnotationTypes Type
        {
            get
            {
                return _annotatinType;
            }
            set
            {
                if( _annotatinType == value )
                {
                    return;
                }

                if( _annotatinType != ChartAnnotationTypes.None )
                {
                    throw new InvalidOperationException( LocalizedStrings.AnnotationTypeCantBeChanged );
                }

                _annotatinType = value;
            }
        }

        Color IDrawableChartElement.Color
        {
            get
            {
                return Colors.Transparent;
            }
        }

        UIBaseVM IDrawableChartElement.CreateViewModel( IScichartSurfaceVM viewModel )
        {
            if( Type == ChartAnnotationTypes.None )
            {
                throw new InvalidOperationException( "annotation type is not set" );
            }

            return _viewModel = new AnnotationVM( this );
        }

        bool IDrawableChartElement.StartDrawing( IEnumerableEx< ChartDrawDataEx.IDrawValue > data )
        {
            return _viewModel.Draw( data );
        }

        void IDrawableChartElement.StartDrawing( )
        {
            _viewModel.Draw( Enumerable.Empty< ChartDrawDataEx.IDrawValue >( ).ToEx( 0 ) );
        }

        bool IElementWithXYAxes.CheckAxesCompatible( ChartAxisType? axisType, ChartAxisType? axisType2 )
        {
            if( !axisType2.HasValue )
            {
                return true;
            }

            return axisType2.GetValueOrDefault( ) == ChartAxisType.Numeric & axisType2.HasValue;
        }

        protected override bool OnDraw( ChartDrawDataEx data )
        {
            ChartDrawDataEx.sAnnotation annotationData = data.GetAnnotation( this );
            if( annotationData == null )
            {
                return false;
            }

            return ( ( IDrawableChartElement )this ).StartDrawing(  new ChartDrawDataEx.IDrawValue[ 1 ] { annotationData } .ToEx( 1 ) );
        }

        internal override AnnotationUI Clone( AnnotationUI annotation )
        {
            base.Clone( annotation );
            annotation.Type = annotation.Type;
            return annotation;
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            Type = storage.GetValue( "Type", Type );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "Type", Type );
        }
    }
}

