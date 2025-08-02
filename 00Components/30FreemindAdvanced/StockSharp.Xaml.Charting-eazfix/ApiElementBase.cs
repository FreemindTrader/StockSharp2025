using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;

#nullable disable
public abstract class ApiElementBase : ContentControl, INotifyPropertyChanged
{

    private ISciChartSurface _parentSurface;

    private IServiceContainer _serviceContainer;

    private bool _isAttached;

    public event PropertyChangedEventHandler PropertyChanged;

    public virtual ISciChartSurface ParentSurface
    {
        get => this._parentSurface;
        set
        {
            this._parentSurface = value;
            this.OnPropertyChanged( nameof( ParentSurface ) );
        }
    }

    public IEnumerable<IAxis> XAxes
    {
        get
        {
            return this.ParentSurface == null || this.ParentSurface.get_XAxes() == null ? Enumerable.Empty<IAxis>() : ( IEnumerable<IAxis> ) this.ParentSurface.get_XAxes();
        }
    }

    public IEnumerable<IAxis> YAxes
    {
        get
        {
            return this.ParentSurface == null || this.ParentSurface.get_YAxes() == null ? Enumerable.Empty<IAxis>() : ( IEnumerable<IAxis> ) this.ParentSurface.get_YAxes();
        }
    }

    public virtual IAxis YAxis
    {
        get
        {
            ISciChartSurface parentSurface = this.ParentSurface;
            IAxis yaxis = (IAxis) null;
            if ( parentSurface != null )
            {
                yaxis = parentSurface.YAxis;
                if ( yaxis == null && !parentSurface.get_YAxes().\u0023\u003DzCCMM80zDpO6N<IAxis>())
          yaxis = parentSurface.get_YAxes().FirstOrDefault<IAxis>( ApiElementBase.SomeClass34343383.\u0023\u003DzcFhfCcBLh7a_pKoCqw\u003D\u003D ?? ( ApiElementBase.SomeClass34343383.\u0023\u003DzcFhfCcBLh7a_pKoCqw\u003D\u003D = new Func<IAxis, bool>( ApiElementBase.SomeClass34343383.SomeMethond0343.\u0023\u003Dz40hF903Z7W1MLFKRgtpttIg\u003D) ));
            }
            return yaxis;
        }
    }

    public virtual IAxis XAxis
    {
        get
        {
            ISciChartSurface parentSurface = this.ParentSurface;
            IAxis xaxis = (IAxis) null;
            if ( parentSurface != null )
            {
                xaxis = parentSurface.XAxis;
                if ( xaxis == null && !parentSurface.get_XAxes().\u0023\u003DzCCMM80zDpO6N<IAxis>())
          xaxis = parentSurface.get_XAxes().FirstOrDefault<IAxis>( ApiElementBase.SomeClass34343383.\u0023\u003Dz74JLo6CIZR_cYr0qvA\u003D\u003D ?? ( ApiElementBase.SomeClass34343383.\u0023\u003Dz74JLo6CIZR_cYr0qvA\u003D\u003D = new Func<IAxis, bool>( ApiElementBase.SomeClass34343383.SomeMethond0343.\u0023\u003DzQY8WNqpkffjul30yEBWakgc\u003D) ));
            }
            return xaxis;
        }
    }

    public virtual IServiceContainer Services
    {
        get => this._serviceContainer;
        set => this._serviceContainer = value;
    }

    public IChartModifierSurface ModifierSurface
    {
        get
        {
            return this.ParentSurface == null ? ( IChartModifierSurface ) null : this.ParentSurface.\u0023\u003DzBgWxEdRxHdEh();
        }
    }

    public virtual bool IsAttached
    {
        get => this._isAttached;
        set => this._isAttached = value;
    }

    protected \u0023\u003DzlvwXE9mBO1uItIXfGGLJcGAvOm_MyInBFl6FOhs\u003D \u0023\u003Dzwc4Gzka23TGB()
    {
        return this.ParentSurface == null ? (\u0023\u003DzlvwXE9mBO1uItIXfGGLJcGAvOm_MyInBFl6FOhs\u003D) null : this.ParentSurface.\u0023\u003Dzwc4Gzka23TGB();
    }

    public abstract void OnAttached();

    public abstract void OnDetached();

    public IAxis GetYAxis( string _param1 )
    {
        return this.ParentSurface == null || this.ParentSurface.get_YAxes() == null ? ( IAxis ) null : this.ParentSurface.get_YAxes().\u0023\u003Dz\u0024YoxjvGBoa2C( _param1, false );
    }

    public IAxis GetXAxis( string _param1 )
    {
        return this.ParentSurface == null || this.ParentSurface.get_XAxes() == null ? ( IAxis ) null : this.ParentSurface.get_XAxes().\u0023\u003Dz\u0024YoxjvGBoa2C( _param1, false );
    }

    protected virtual void OnInvalidateParentSurface()
    {
        if ( this.Services == null )
            return;
        this.Services.GetService <\u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u_IIwaqHR9EOQ\u003D\u003D> ().\u0023\u003DzosHqOAc\u003D<\u0023\u003DzFphlrC3tGBVP73muJW4N1gQtGNrHdTMuWgCKGfUfH93B > ( new \u0023\u003DzFphlrC3tGBVP73muJW4N1gQtGNrHdTMuWgCKGfUfH93B( ( object ) this ));
    }

    protected T \u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<T>(string _param1) where T : class
    {
    return this.GetTemplateChild( _param1) is T templateChild ? templateChild : throw new InvalidOperationException($"Unable to Apply the Control Template. {_param1} is missing or of the wrong type");
    }

    protected void OnPropertyChanged(string _param1)
  {
    PropertyChangedEventHandler zUapFgog = this.\u0023\u003DzUApFgog\u003D;
    if (zUapFgog == null)
      return;
    zUapFgog( (object) this, new PropertyChangedEventArgs( _param1));
  }

[Serializable]
private sealed class SomeClass34343383
{
    public static readonly ApiElementBase.SomeClass34343383 SomeMethond0343 = new ApiElementBase.SomeClass34343383();
    public static Func<IAxis, bool> \u0023\u003DzcFhfCcBLh7a_pKoCqw\u003D\u003D;
    public static Func<IAxis, bool> \u0023\u003Dz74JLo6CIZR_cYr0qvA\u003D\u003D;

    public bool \u0023\u003Dz40hF903Z7W1MLFKRgtpttIg\u003D(
      IAxis _param1)
    {
      return _param1.get_IsPrimaryAxis();
    }

public bool \u0023\u003DzQY8WNqpkffjul30yEBWakgc\u003D(
      IAxis _param1)
    {
      return _param1.get_IsPrimaryAxis();
    }
  }
}
