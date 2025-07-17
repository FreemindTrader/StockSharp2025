using System;
using System.Windows;
using System.Windows.Media;
namespace fx.Xaml.Charting
{
    public class SeriesInfo : BindableObject, ICloneable
    {
        private readonly IRenderableSeries _rSeries;

        private string _seriesName;

        private IComparable _yValue;

        private IComparable _xValue;

        private Color _seriesColor;

        private fx.Xaml.Charting.DataSeriesType _dataSeriesType;

        private double _yValueDouble;

        private Point _xyCoordinate;

        private bool _isHit;

        private int _dataSeriesIndex;

        public int DataSeriesIndex
        {
            get
            {
                return _dataSeriesIndex;
            }
            set
            {
                SetField<int>( ref _dataSeriesIndex, value, "DataSeriesIndex" );
            }
        }

        public fx.Xaml.Charting.DataSeriesType DataSeriesType
        {
            get
            {
                return _dataSeriesType;
            }
            set
            {
                SetField<fx.Xaml.Charting.DataSeriesType>( ref _dataSeriesType, value, "DataSeriesType" );
            }
        }

        public string FormattedXValue
        {
            get
            {
                return GetXCursorFormattedValue( XValue );
            }
        }

        public string FormattedYValue
        {
            get
            {
                return GetYCursorFormattedValue( YValue );
            }
        }

        public bool IsHit
        {
            get
            {
                return _isHit;
            }
            set
            {
                SetField<bool>( ref _isHit, value, "IsHit" );
            }
        }

        public bool IsVisible
        {
            get
            {
                return RenderableSeries.IsVisible;
            }
            set
            {
                RenderableSeries.IsVisible = value;
                base.OnPropertyChanged( "IsVisible" );
            }
        }

        public IRenderableSeries RenderableSeries
        {
            get
            {
                return _rSeries;
            }
        }

        public Color SeriesColor
        {
            get
            {
                return _seriesColor;
            }
            set
            {
                SetField<Color>( ref _seriesColor, value, "SeriesColor" );
            }
        }

        public virtual object SeriesInfoKey
        {
            get
            {
                return RenderableSeries;
            }
        }

        public string SeriesName
        {
            get
            {
                return _seriesName;
            }
            set
            {
                SetField<string>( ref _seriesName, value, "SeriesName" );
            }
        }

        public double Value
        {
            get
            {
                return _yValueDouble;
            }
            set
            {
                SetField<double>( ref _yValueDouble, value, "Value" );
            }
        }

        public IComparable XValue
        {
            get
            {
                return _xValue;
            }
            set
            {
                if ( SetField<IComparable>( ref _xValue, value, "XValue" ) )
                {
                    base.OnPropertyChanged( "FormattedXValue" );
                }
            }
        }

        public Point XyCoordinate
        {
            get
            {
                return _xyCoordinate;
            }
            set
            {
                SetField<Point>( ref _xyCoordinate, value, "XyCoordinate" );
            }
        }

        public IComparable YValue
        {
            get
            {
                return _yValue;
            }
            set
            {
                if ( SetField<IComparable>( ref _yValue, value, "YValue" ) )
                {
                    base.OnPropertyChanged( "FormattedYValue" );
                }
            }
        }

        public SeriesInfo( IRenderableSeries rSeries )
        {
            string seriesName;
            _rSeries = rSeries;
            if ( rSeries.DataSeries != null )
            {
                seriesName = rSeries.DataSeries.SeriesName;
            }
            else
            {
                seriesName = null;
            }
            SeriesName = seriesName;
            SeriesColor = rSeries.SeriesColor;
        }

        public SeriesInfo( IRenderableSeries rSeries, HitTestInfo hitTestInfo ) : this( rSeries )
        {
            DataSeriesType = hitTestInfo.DataSeriesType;
            DataSeriesIndex = hitTestInfo.DataSeriesIndex;
            IsHit = hitTestInfo.IsHit;
            XValue = hitTestInfo.XValue;
            YValue = hitTestInfo.YValue;
            Value = hitTestInfo.YValue.ToDouble();
            XyCoordinate = hitTestInfo.HitTestPoint;
        }

        public virtual object Clone()
        {
            return base.MemberwiseClone();
        }

        public virtual void CopyFrom( SeriesInfo other )
        {
            if ( other.RenderableSeries != _rSeries )
            {
                throw new InvalidOperationException( "invalid series" );
            }
            SeriesName = other.SeriesName;
            YValue = other.YValue;
            XValue = other.XValue;
            SeriesColor = other.SeriesColor;
            DataSeriesType = other.DataSeriesType;
            Value = other.Value;
            XyCoordinate = other.XyCoordinate;
            IsHit = other.IsHit;
            DataSeriesIndex = other.DataSeriesIndex;
        }

        protected string GetXCursorFormattedValue( IComparable value )
        {
            string empty = null;

            IAxis xAxis = RenderableSeries.XAxis;

            if ( xAxis != null )
            {
                empty = xAxis.FormatCursorText( value );
            }

            if ( empty == null )
            {
                empty = string.Empty;
            }

            return empty;
        }

        protected string GetYCursorFormattedValue( IComparable value )
        {
            string empty = null;

            IAxis yAxis = RenderableSeries.YAxis;

            if ( yAxis != null )
            {
                empty = yAxis.FormatCursorText( value );
            }

            if ( empty == null )
            {
                empty = string.Empty;
            }

            return empty;
        }
    }
}
