using StockSharp.Charting;
using SciChart.Charting.Model.ChartData;
using StockSharp.Xaml.Charting;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;


namespace StockSharp.Xaml.Charting;

/// <summary>
/// Represents an view model for a chart element, which can be used to display series information such as name, value, and color.
/// 
/// It is used in CandleStickVM to represent 
///     1) High
///     2) Low
///     3) Open
///     4) Close 
/// of the candle.
/// 
/// It is also used in the Quotes VM to represent the 
///     1) Ask Line
///     2) Bid Line
///     
/// Every chart compenent need to be drawn separately, so this class is used to represent each element of the chart.
/// A candlestick is componsed of four elements: High, Low, Open, Close.
/// 
/// And each child element of the Candlestick has a name, like High and also it has a color associated with it.
/// 
/// And we need a function to be called (_colorFunction) when we need to draw the high, this function will return the color for the Candlestick High.
/// and we also need to know the value of the high, so we have a function (_valueFunction) that will return the value of the high.
/// </summary>
public sealed class ChartElementViewModel : ChartPropertiesViewModel
{
    private readonly Func<SeriesInfo, Color>  _colorFunction;
    private readonly Func<SeriesInfo, string> _valueFunction;
    private string                            _name;
    private string                            _value;
    private Color                             _color;
    private SeriesInfo                        _seriesInfo;
    private ChartComponentUiDomain           _chartComponent;

    public ChartElementViewModel( INotifyPropertyChanged mypropertyOwner, Func<SeriesInfo, Color> getColorFunc, Func<SeriesInfo, string> getValueFunc, params string[ ] string_2 ) : this( null, mypropertyOwner, getColorFunc, getValueFunc, string_2 )
    {
    }

    public ChartElementViewModel( string name, INotifyPropertyChanged propertyOwner, Func<SeriesInfo, Color> getColor, Func<SeriesInfo, string> getValue, params string[ ] upDownFillColorName ) : base()
    {
        Name           = name;
        _colorFunction = getColor;
        _valueFunction = getValue;

        if ( upDownFillColorName != null && upDownFillColorName.Length == 0 )
        {
            return;
        }

        propertyOwner.PropertyChanged += ( s, e ) =>
        {
            if ( !( upDownFillColorName.Contains( e.PropertyName ) || _seriesInfo == null ) )
            {
                return;
            }

            Color = _colorFunction( _seriesInfo );
        };
    }

    /// <summary>
    /// This is the ChartCompent that contains this Chart Elment.
    /// 
    /// eg. ChartCandleElementViewModel is the ChartCompent and the childrens are the High ChartComponent, Low ChartComponent, Open ChartComponent, Close ChartComponent
    /// </summary>
    public ChartComponentUiDomain ChartComponent
    {
        get
        {
            return _chartComponent;
        }
        set
        {
            _chartComponent = value;
        }
    }


    /// <summary>
    /// This is the Name of the Chart Elment.
    /// 
    /// eg. If this ChartElemnt represent the high of the candlestick, its name will be "High"
    /// </summary>
    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            SetField( ref _name, value, nameof( Name ) );
        }
    }


    /// <summary>
    /// This is the Color of the Chart Elment.
    /// 
    /// eg. If this ChartElemnt represent the high of the candlestick and we want the color of the high to be green, then we will set the color to green.
    /// </summary>
    public Color Color
    {
        get
        {
            return _color;
        }
        set
        {
            SetField( ref _color, value, nameof( Color ) );
        }
    }

    /// <summary>
    /// This is the Value of the Chart Elment.
    /// 
    /// eg. If this ChartElemnt represent the high of the candlestick, then this value will be the high value of the candle.
    /// 
    /// This is kinda strange, Value is returned as string instead of double or decimal.
    /// </summary>
    public string Value
    {
        get
        {
            return _value;
        }
        set
        {
            SetField( ref _value, value, nameof( Value ) );
        }
    }

    public static Color GetHigherAlphaColor( Color colorOne, Color colorTwo )
    {
        if ( colorOne.A <= colorTwo.A )
        {
            return colorTwo;
        }
        return colorOne;
    }

    public void UpdateSeries( SeriesInfo seriesInfo_1 )
    {
        _seriesInfo = seriesInfo_1;
        Color = _colorFunction( _seriesInfo );
        Value = _valueFunction( _seriesInfo );
    }
}
