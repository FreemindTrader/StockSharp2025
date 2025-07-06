using Ecng.Common;
using Ecng.Serialization;
using StockSharp.Algo.Indicators;
using StockSharp.Localization;
using StockSharp.Xaml.Charting.IndicatorPainters;
using System;
using System.Collections.Generic; 
using fx.Collections;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using StockSharp.Charting;

namespace StockSharp.Xaml.Charting
{
    
    public sealed class IndicatorUI : ChartElement< IndicatorUI >
    {
        private DefaultPainter _defaultPainter;
        private IChartIndicatorPainter _chartIndicatorPainter;

        public IndicatorUI( )
        {
            _defaultPainter = new DefaultPainter( );

            _defaultPainter.OnAttached( this );
        }

        public IndicatorUI( int fifoCapacity )
        {
            _defaultPainter = new DefaultPainter( fifoCapacity );
            FifoCapacity    = fifoCapacity;

            _defaultPainter.OnAttached( this );
        }

        public override string ToString( )
        {
            return FullTitle;
        }

        [Browsable( false )]
        [Obsolete( "Use FullTitle property instead." )]
        public string Title
        {
            get
            {
                return FullTitle;
            }
            set
            {
                FullTitle = value;
            }
        }

        public IChartIndicatorPainter IndicatorPainter
        {
            get
            {
                return _chartIndicatorPainter ?? _defaultPainter;
            }
            set
            {
                if( _chartIndicatorPainter == value )
                {
                    return;
                }
                ChartArea chartArea = ChartArea;
                chartArea?.ChartSurfaceViewModel.OnChartAreaElementsRemoving( IndicatorPainter.Element );
                IndicatorPainter.OnDetached( );
                
                if( value?.GetType( ) != typeof( DefaultPainter ) )
                {
                    _chartIndicatorPainter = value;
                }
                else
                {
                    _chartIndicatorPainter = null;
                    _defaultPainter = ( DefaultPainter )value;
                }
                
                IndicatorPainter.OnAttached( this );
                chartArea?.ChartSurfaceViewModel.OnChartAreaElementsAdded( this );
                RaisePropertyChanged( nameof( IndicatorPainter ) );
            }
        }

        [Browsable( false )]
        public Color Color
        {
            get
            {
                return _defaultPainter.Line.Color;
            }
            set
            {
                _defaultPainter.Line.Color = value;
            }
        }

        [Browsable( false )]
        public Color AdditionalColor
        {
            get
            {
                return _defaultPainter.Line.AdditionalColor;
            }
            set
            {
                _defaultPainter.Line.AdditionalColor = value;
            }
        }

        [Browsable( false )]
        public int StrokeThickness
        {
            get
            {
                return _defaultPainter.Line.StrokeThickness;
            }
            set
            {
                _defaultPainter.Line.StrokeThickness = value;
            }
        }

        [Browsable( false )]
        public bool AntiAliasing
        {
            get
            {
                return _defaultPainter.Line.AntiAliasing;
            }
            set
            {
                _defaultPainter.Line.AntiAliasing = value;
            }
        }

        [Browsable( false )]
        public ChartIndicatorDrawStyles DrawStyle
        {
            get
            {
                return _defaultPainter.Line.Style;
            }
            set
            {
                _defaultPainter.Line.Style = value;
            }
        }

        [Browsable( false )]
        public bool ShowAxisMarker
        {
            get
            {
                return _defaultPainter.Line.ShowAxisMarker;
            }
            set
            {
                _defaultPainter.Line.ShowAxisMarker = value;
            }
        }

        [Browsable( false )]
        public ControlTemplate DrawTemplate
        {
            get
            {
                return _defaultPainter.Line.DrawTemplate;
            }
            set
            {
                _defaultPainter.Line.DrawTemplate = value;
            }
        }

        protected override bool OnDraw( ChartDrawData data )
        {
            return IndicatorPainter.Draw( data );
        }

        protected override void OnReset( )
        {
            base.OnReset( );
            IndicatorPainter?.Reset( );
        }

        internal void CreateIndicatorPainter( IList< IndicatorType > indicatorTypeList, IIndicator indicator )
        {
            if( _chartIndicatorPainter != null || indicatorTypeList == null || indicatorTypeList.Count <= 0 )
            {
                return;
            }

            IndicatorType indicatorType = indicatorTypeList.FirstOrDefault( t => t.Indicator == indicator.GetType( ) );

            IChartIndicatorPainter myPainter;
            if( indicatorType == null )
            {
                myPainter = null;
            }
            else
            {
                myPainter = (StockSharp.Xaml.Charting.IChartIndicatorPainter ) indicatorType.CreatePainter();
                
            }

            if( !( myPainter?.GetType( ) != typeof( DefaultPainter ) ) )
            {
                return;
            }

            IndicatorPainter = myPainter;
        }

        protected override IndicatorUI CreateClone( )
        {
            IndicatorUI clone = base.CreateClone( );
            clone.IndicatorPainter = ( IChartIndicatorPainter )IndicatorPainter.Clone( );
            return clone;
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            SettingsStorage settingsStorage = storage.GetValue( "IndicatorPainter", ( SettingsStorage )null );
            if( settingsStorage == null )
            {
                return;
            }
            Type type = Type.GetType( settingsStorage.GetValue( "type", ( string )null ), false );
            if( type == null )
            {
                return;
            }
            IndicatorPainter.OnDetached( );
            IChartIndicatorPainter instance = type.CreateInstance< IChartIndicatorPainter >( );
            if( instance.GetType( ) == typeof( DefaultPainter ) )
            {
                _defaultPainter = ( DefaultPainter )instance;
                _chartIndicatorPainter = null;
            }
            else
            {
                _chartIndicatorPainter = instance;
            }
            IndicatorPainter.OnAttached( this );
            SettingsStorage storage1 = settingsStorage.GetValue( "settings", ( SettingsStorage )null );
            try
            {
                instance.Load( storage1 );
            }
            catch
            {
            }
        }

        public override void Save( SettingsStorage storage )
        {
            storage.SetValue( "IndicatorPainter", IndicatorPainter.SaveEntire( false ) );
            base.Save( storage );
        }
    }
}
