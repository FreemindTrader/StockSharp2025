using System;
using System.ComponentModel;
using Ecng.Common;
using SciChart.Data.Numerics.GenericMath;

namespace SciChart.Data.Model;

internal abstract class Range<T> :
  BindableObject,
  INotifyPropertyChanged,
  ICloneable,
  IRange<T>,
  IRange
  where T : IComparable
{

    private T dgr;

    private T dgs;

    private static IMath<T> _mathHelper = MathHelper.GetMath<T>();

    protected Range()
    {
    }

    protected Range(
      T _param1,
      T _param2 )
    {
        this.Min = _param1;
        this.Max = _param2;
    }

    //
    // Summary:
    //     Gets whether this Range is defined
    public virtual bool IsDefined
    {
        get
        {
            if ( Max.IsDefined() || 1 == 0 )
            {
                return Min.IsDefined();
            }

            return false;
        }
    }

    //
    // Summary:
    //     Gets or sets the Min value of this range
    IComparable IRange.Min
    {
        get
        {
            return Min;
        }
        set
        {
            Min = ( T ) value;
        }
    }

    //
    // Summary:
    //     Gets or sets the Max value of this range
    IComparable IRange.Max
    {
        get
        {
            return Max;
        }
        set
        {
            Max = ( T ) value;
        }
    }

    //
    // Summary:
    //     Gets the difference (Max - Min) of this range
    IComparable IRange.Diff => Diff;

    //
    // Summary:
    //     Gets whether the range is Zero, where Max equals Min
    public abstract bool IsZero
    {
        get;
    }

    //
    // Summary:
    //     Gets or sets the Min value of this range
    public T Min
    {
        get
        {
            return dgr;
        }
        set
        {
            T val = dgr;
            this.dgr = value;

            if ( !dgr.Equals( value ) && 0 == 0 )
            {
                dgr = value;
                if ( dgt || 1 == 0 )
                {
                    OnPropertyChanged( ptn.dth( 5054 ), val, value );
                }
            }
        }
    }

    public T Min
    {
        get => this.dgr;
        set
        {
            T val = this.dgr;
            this.dgr = value;
            this.OnPropertyChanged(nameof( Min ), ( object ) zWanvsEc, ( object ) value);
        }
    }

    public T Max
    {
        get => this.dgs;
        set
        {
            T zjV9csI = this.dgs;
            this.dgs = value;
            this.OnPropertyChanged(nameof( Max ), ( object ) zjV9csI, ( object ) value);
        }
    }

    public abstract T Diff
    {
        get;
    }

    public abstract object Clone();

    public abstract IRange<T> GrowBy(
      double _param1,
      double _param2 );

    public abstract IRange<T> \u0023\u003DzJIqIiUw\u003D(
      IRange<T> _param1);

  public abstract DoubleRange AsDoubleRange();

    public abstract IRange<T> SetMinMax(
      double _param1,
      double _param2 );

    public abstract IRange<T> SetMinMax(
      double _param1,
      double _param2,
      IRange<T> _param3 );

    protected void SetMinMaxInternal(
      T _param1,
      T _param2 )
    {
        if ( this.Max.CompareTo( ( object ) _param1 ) < 0 )
        {
            this.Max = _param2;
            this.Min = _param1;
        }
        else
        {
            this.Min = _param1;
            this.Max = _param2;
        }
    }

    public IRange \u0023\u003DzJIqIiUw\u003D(
      IRange _param1)
  {
    return (IRange) this.\u0023\u003DzJIqIiUw\u003D((IRange<T>) _param1);
    }

    public IRange \u0023\u003DzJIqIiUw\u003D(
      IRange _param1,
    \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQBfb77y0xl0\u00246w\u003D\u003D _param2)
  {
    IRange abyLt9clZggmJsWhw = (IRange) null;
    switch (_param2)
    {
      case \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQBfb77y0xl0\u00246w\u003D\u003D.MinMax:
        abyLt9clZggmJsWhw = _param1;
        break;
      case \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQBfb77y0xl0\u00246w\u003D\u003D.Max:
        T zH9Hnkng1 = \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNXCE4EvjzL\u0024mX84Druo\u003D.\u0023\u003DzAGURk2c\u003D<T>();
        abyLt9clZggmJsWhw = \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDlB0n65RlWCGw\u003D\u003D.\u0023\u003Dzo7udA0u6sNJJ( _param1, (IComparable) zH9Hnkng1, _param1.Max);
        break;
      case \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQBfb77y0xl0\u00246w\u003D\u003D.Min:
        T zH9Hnkng2 = \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNXCE4EvjzL\u0024mX84Druo\u003D.\u0023\u003Dz9S54PDM\u003D<T>();
        abyLt9clZggmJsWhw = \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDlB0n65RlWCGw\u003D\u003D.\u0023\u003Dzo7udA0u6sNJJ( _param1, _param1.Min, (IComparable) zH9Hnkng2 );
        break;
    }
    return (IRange) this.\u0023\u003DzJIqIiUw\u003D((IRange<T>) abyLt9clZggmJsWhw);
}

public IRange \u0023\u003DzeiifnZI\u003D(
  IRange _param1)
  {
    return (IRange) this.\u0023\u003DzeiifnZI\u003D((IRange<T>) _param1);
}

public bool \u0023\u003DzU0feMzXFLecQ( IComparable _param1 )
{
    return this.Min.CompareTo( ( object ) _param1 ) <= 0 && this.Max.CompareTo( ( object ) _param1 ) >= 0;
}

public IRange<T> \u0023\u003DzeiifnZI\u003D(
  IRange<T> _param1)
  {
    return (IRange<T>) \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDlB0n65RlWCGw\u003D\u003D.\u0023\u003DzLc65\u0024pc\u003D((IComparable) Range<T>._mathHelper.Min(this.Min, _param1.Min), (IComparable) Range<T>._mathHelper.Max(this.Max, _param1.Max));
}

IRange IRange.\u0023\u003DznUYKC7Ax8Zwair3Ru5V4H3L844WUagxCAVomufc\u003D(
  double _param1,
  double _param2)
  {
    return ( IRange ) this.SetMinMax( _param1, _param2 );
}

IRange IRange.\u0023\u003Dz3RRntx4pzkd854dIVpLK6aPvdl8ZapW2OeSTMYm_K6Gu(
  double _param1,
  double _param2,
  IRange _param3)
  {
    return ( IRange ) this.SetMinMax( _param1, _param2, ( IRange<T> ) _param3 );
}

IRange IRange.\u0023\u003DzpTBWTwmpvpgHkLhFsQhfVp2o1afiKe2D_7xBFPY\u003D(
  double _param1,
  double _param2)
  {
    return ( IRange ) this.GrowBy( _param1, _param2 );
}

public override string ToString() => $"{this.GetType()} {{Min={this.Min}, Max={this.Max}}}";

public override int GetHashCode()
{
    T zH9Hnkng = this.Min;
    int num = zH9Hnkng.GetHashCode() * 397;
    zH9Hnkng = this.Max;
    int hashCode = zH9Hnkng.GetHashCode();
    return num ^ hashCode;
}

public override bool Equals( object _param1 )
{
    return _param1 is IRange<T> && this.\u0023\u003DzhxbsSqM\u003D( ( IRange<T> ) _param1 );
}

public bool \u0023\u003DzhxbsSqM\u003D(
  IRange<T> _param1)
  {
    if (_param1 == null)
      return false;
    if (this == _param1)
      return true;
T zH9Hnkng = _param1.Min;
    if (!zH9Hnkng.Equals((object) this.Min))
      return false;
zH9Hnkng = _param1.Max;
    return zH9Hnkng.Equals((object) this.Max);
}

internal void \u0023\u003DzIECuo1rstuxex\u0024WBruVMWlw\u003D()
  {
    \u0023\u003DzITX8mZ2jbGEtwuB21HaSb94StZu7BSE7Sw\u003D\u003D.\u0023\u003DzlTskcr4\u003D((IComparable) this.Min, "Min").\u0023\u003DziXfpgk1YpfgIxrtqTA\u003D\u003D((IComparable) this.Max, "Max");
}
}
