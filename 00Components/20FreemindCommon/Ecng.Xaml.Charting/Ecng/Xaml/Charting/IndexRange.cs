// fx.Xaml.Charting.IndexRange
using System;
using System.ComponentModel;
using fx.Xaml.Charting;
public class IndexRange : Range<int>
{
    private double _dMin;

    private double _dMax;

    public override bool IsDefined
    {
        get
        {
            if ( base.IsDefined )
            {
                return base.Min <= base.Max;
            }
            return false;
        }
    }

    public override int Diff => base.Max - base.Min;

    public override bool IsZero => Diff == 0;

    public IndexRange()
    {
        Init( base.Min, base.Max );
    }

    public IndexRange( int min, int max )
        : base( min, max )
    {
        Init( base.Min, base.Max );
    }

    private void Init( int min, int max )
    {
        ( ( INotifyPropertyChanged ) this ).PropertyChanged += OnPropertyChanged;
        _dMin = ( double ) min;
        _dMax = ( double ) max;
    }

    private void OnPropertyChanged( object sender, PropertyChangedEventArgs args )
    {
        if ( !( args.PropertyName != "Min" ) || !( args.PropertyName != "Max" ) )
        {
            if ( ( int ) _dMin.RoundOff() != base.Min )
            {
                _dMin = ( double ) base.Min;
            }
            if ( ( int ) _dMax.RoundOff() != base.Max )
            {
                _dMax = ( double ) base.Max;
            }
        }
    }

    public override object Clone()
    {
        return new IndexRange( base.Min, base.Max )
        {
            _dMin = _dMin,
            _dMax = _dMax
        };
    }

    public override DoubleRange AsDoubleRange()
    {
        return new DoubleRange( _dMin, _dMax );
    }

    public override IRange<int> SetMinMax( double min, double max )
    {
        int num = (int)min.RoundOff();
        int num2 = (int)max.RoundOff();
        SetMinMaxInternal( num, num2 );
        if ( num == base.Min )
        {
            _dMin = min;
        }
        if ( num2 == base.Max )
        {
            _dMax = max;
        }
        return this;
    }

    public override IRange<int> SetMinMax( double min, double max, IRange<int> maxRange )
    {
        base.Min = ( int ) Math.Max( min, ( double ) maxRange.Min );
        base.Max = ( int ) Math.Min( max, ( double ) maxRange.Max );
        return this;
    }

    public override IRange<int> GrowBy( double minFraction, double maxFraction )
    {
        int diff = Diff;
        bool flag = diff == 0;
        _dMax += maxFraction * ( flag ? _dMax : ( ( double ) diff ) );
        _dMin -= minFraction * ( flag ? _dMin : ( ( double ) diff ) );
        if ( _dMax < _dMin )
        {
            NumberUtil.Swap( ref _dMin, ref _dMax );
        }
        int value = (int)_dMax.RoundOff();
        int value2 = (int)_dMin.RoundOff();
        if ( value < value2 )
        {
            NumberUtil.Swap( ref value2, ref value );
        }
        double dMin = _dMin;
        double dMax = _dMax;
        base.Min = value2;
        base.Max = value;
        _dMin = dMin;
        _dMax = dMax;
        return new IndexRange( value2, value )
        {
            _dMin = _dMin,
            _dMax = _dMax
        };
    }

    public override string ToString()
    {
        return $"min={base.Min}, max={base.Max}, dmin={_dMin:F3}, dmax={_dMax:F3}";
    }

    public override IRange<int> ClipTo( IRange<int> maximumRange )
    {
        int max = base.Max;
        int min = base.Min;
        int num = (base.Max > maximumRange.Max) ? maximumRange.Max : base.Max;
        int num2 = (base.Min < maximumRange.Min) ? maximumRange.Min : base.Min;
        if ( num2 > maximumRange.Max )
        {
            num2 = maximumRange.Min;
        }
        if ( num < min )
        {
            num = maximumRange.Max;
        }
        if ( num2 > num )
        {
            num2 = maximumRange.Min;
            num = maximumRange.Max;
        }
        SetMinMaxInternal( num2, num );
        return this;
    }
}
