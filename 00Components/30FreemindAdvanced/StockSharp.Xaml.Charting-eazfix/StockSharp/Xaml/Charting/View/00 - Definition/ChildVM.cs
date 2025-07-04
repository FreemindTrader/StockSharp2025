using SciChart.Charting;
using SciChart.Charting.Model.ChartData;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;
using StockSharp.Xaml.Charting;

namespace StockSharp.Xaml.Charting;

public sealed class ChildVM : ChartBaseViewModel
{
    private readonly Func< SeriesInfo, Color > _colorFunction;
    private readonly Func< SeriesInfo, string > _valueFunction;
    private string _name;
    private string _value;
    private Color _color;
    private SeriesInfo _seriesInfo;
    private ParentVM _parent;

    public ChildVM( INotifyPropertyChanged mypropertyOwner, Func<SeriesInfo, Color> getColorFunc, Func<SeriesInfo, string> getValueFunc, params string[ ] string_2 ) : this( null, mypropertyOwner, getColorFunc, getValueFunc, string_2 )
    {
    }

    public ChildVM( string name, INotifyPropertyChanged propertyOwner, Func<SeriesInfo, Color> getColor, Func<SeriesInfo, string> getValue, params string[ ] upDownFillColorName ) : base()
    {
        Name = name;
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

    public ParentVM Parent
    {
        get
        {
            return _parent;
        }
        set
        {
            _parent = value;
        }
    }

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

    public static Color GetHigherAlphaColor( Color color_1, Color color_2 )
    {
        if ( color_1.A <= color_2.A )
        {
            return color_2;
        }
        return color_1;
    }

    public void UpdateSeries( SeriesInfo seriesInfo_1 )
    {
        _seriesInfo = seriesInfo_1;
        Color = _colorFunction( _seriesInfo );
        Value = _valueFunction( _seriesInfo );
    }
}
