using DevExpress.Mvvm.Native;
using SciChart.Charting.ChartModifiers;
using SciChart.Charting.Visuals.Annotations;
using SciChart.Core.Framework;
using SciChart.Core.Utility.Mouse;
using System;
using System.Diagnostics;
using System.Windows;

#nullable disable
namespace StockSharp.Xaml.Charting;


/// <summary>
/// This modifier is used to define the behaviour for the Annotation. At first, I was thinking about using 
/// this ChartModifier to creat the fibonacci retracment and expansion.
/// 
/// Ultimately, I learnt that I have to use MyTradingAnnotationBase instead of this fxAnnotationCreationModifier
/// </summary>
public class fxAnnotationCreationModifier : ChartModifierBase
{
    public static readonly DependencyProperty YAxisIdProperty = DependencyProperty.Register(nameof(YAxisId), typeof(string), typeof(fxAnnotationCreationModifier), new PropertyMetadata((object) "DefaultAxisId"));

    public static readonly DependencyProperty XAxisIdProperty = DependencyProperty.Register(nameof(XAxisId), typeof(string), typeof(fxAnnotationCreationModifier), new PropertyMetadata((object) "DefaultAxisId"));

    private Point _pointRelativeToModifier;

    private AnnotationBaseEx _annotation;

    private Type _annotationType;

    private Style _annotationStyle;

    public event EventHandler AnnotationCreated;
   

    public string YAxisId
    {
        get
        {
            return ( string ) this.GetValue( fxAnnotationCreationModifier.YAxisIdProperty );
        }
        set
        {
            this.SetValue( fxAnnotationCreationModifier.YAxisIdProperty, ( object ) value );
        }
    }

    public string XAxisId
    {
        get
        {
            return ( string ) this.GetValue( fxAnnotationCreationModifier.XAxisIdProperty );
        }
        set
        {
            this.SetValue( fxAnnotationCreationModifier.XAxisIdProperty, ( object ) value );
        }
    }

    public Type AnnotationType
    {
        get => this._annotationType;
        set
        {
            this._annotationType = !( value != ( Type ) null ) || typeof( IAnnotation ).IsAssignableFrom( value )
                ? value
                : throw new ArgumentOutOfRangeException(
                    "value",
                    $"Type {value} does not implement IAnnotation interface." );
        }
    }

    public Style AnnotationStyle
    {
        get => this._annotationStyle;
        set => this._annotationStyle = value;
    }

    public IAnnotation Annotation
    {
        get
        {
            return _annotation;
        }
    }


    /// <summary>
    /// Called when the IsEnabled property changes on this SciChart.Charting.ChartModifiers.ChartModifierBase instance
    /// 
    /// This annotation is no longer Selectable or Editable.
    /// </summary>
    protected override void OnIsEnabledChanged()
    {
        base.OnIsEnabledChanged();
        _annotation = null;
        if ( !this.IsEnabled || this.ParentSurface == null )
            return;
        this.ParentSurface.Annotations.ForEach<IAnnotation>(
                                                                 annotation =>
                                                                 {
                                                                     annotation.IsSelected = false;
                                                                     annotation.IsEditable = false;
                                                                 }
                                                              );
    }

    protected void OnAnnotationCreated()
    {
        AnnotationCreated?.Invoke( this, EventArgs.Empty );
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mouseEventArgs"></param>
    public override void OnModifierMouseMove( ModifierMouseArgs mouseEventArgs )
    {
        if ( this._annotationType == ( Type ) null || this._annotation == null || !this._annotation.IsAttached || this._annotation.IsSelected )
            return;
        this._annotation.UpdatePosition(
            this._pointRelativeToModifier,
            this.GetPointRelativeTo( mouseEventArgs.MousePoint, ( IHitTestable ) this.ModifierSurface ) );
    }

    private bool IsSubclassOfIsAssignableFrom( Type t )
    {
        return typeof( IAnchorPointAnnotation ).IsAssignableFrom( t ) || t.IsSubclassOf( typeof( LineAnnotationWithLabelsBase ) );
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="mouseEvent"></param>
    public override void OnModifierMouseDown( ModifierMouseArgs mouseEvent )
    {
        base.OnModifierMouseDown( mouseEvent );
        if ( this._annotationType == ( Type ) null || !this.MatchesExecuteOn( mouseEvent.MouseButtons, this.ExecuteOn ) || !mouseEvent.IsMaster || this._annotation != null && !this._annotation.IsSelected )
            return;
        mouseEvent.Handled=( true );
        if ( this._annotation != null && this._annotation.IsAttached )
            this._annotation.IsSelected = false;
        this._pointRelativeToModifier = this.GetPointRelativeTo( mouseEvent.MousePoint, ( IHitTestable ) this.ModifierSurface );
        if ( this.IsSubclassOfIsAssignableFrom( this._annotationType ) )
            return;
        this._annotation = ( AnnotationBaseEx ) this.CreateAnnotation( this._annotationType, this._annotationStyle );
        this._annotation.UpdatePosition( this._pointRelativeToModifier, this._pointRelativeToModifier );
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="mouseEvent"></param>
    public override void OnModifierMouseUp( ModifierMouseArgs mouseEvent )
    {
        if ( this._annotationType == ( Type ) null || !this.MatchesExecuteOn( mouseEvent.MouseButtons, this.ExecuteOn ) || !mouseEvent.IsMaster )
            return;
        if ( this.IsSubclassOfIsAssignableFrom( this._annotationType ) && this._annotation == null )
        {
            this._annotation = ( AnnotationBaseEx ) this.CreateAnnotation( this._annotationType, this._annotationStyle );
            Point point = this.GetPointRelativeTo( mouseEvent.MousePoint, ( IHitTestable ) this.ModifierSurface );
            this._annotation.UpdatePosition( point, point );
        }
        if ( this._annotation == null )
            return;
        
        this._annotation.IsSelected = true;
        this.OnAnnotationCreated();
        
        _annotation.UpdateAdorners();
    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="annotationType"></param>
    /// <param name="annotationStyle"></param>
    /// <returns></returns>
    protected virtual IAnnotation CreateAnnotation( Type annotationType, Style annotationStyle )
    {
        var instance = (AnnotationBaseEx) Activator.CreateInstance(annotationType);
        instance.YAxisId = this.YAxisId;
        instance.XAxisId = this.XAxisId;
        if ( annotationStyle != null && annotationStyle.TargetType == annotationType )
        {
            Style style = new Style(annotationType) { BasedOn = annotationStyle };
            instance.Style = style;
        }
        this.ParentSurface.Annotations.Add( ( IAnnotation ) instance );
        return instance;
    }

//    [Serializable]
//    private new sealed class SomeClass34343383
//    {
//        public static readonly fxAnnotationCreationModifier.SomeClass34343383 SomeMethond0343 = new fxAnnotationCreationModifier.SomeClass34343383();
//        public static Action<IAnnotation> \u0023\u003DzmsiBBf5ih\u0024wjnD8X9g\u003D\u003D;

//    internal void \u0023\u003DzLdGxEgyAELbBj1m4vj3jYKw\u003D(
//      IAnnotation _param1)
//    {
//      _param1.set_IsSelected(false);
//      _param1.set_IsEditable(false);
//    }
//}
}


//using SciChart.Charting.ChartModifiers;
//using SciChart.Charting.Visuals;
//using SciChart.Charting.Visuals.Annotations;
//using SciChart.Core.Extensions;
//using SciChart.Core.Framework;
//using SciChart.Core.Utility.Mouse;
//using StockSharp.Xaml.Charting.HewFibonacci;
//using System;
//using System.Collections.Generic; using fx.Collections;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;

//namespace StockSharp.Xaml.Charting;

//public class fxAnnotationCreationModifier : ChartModifierBase
//{
//    public static readonly DependencyProperty YAxisIdProperty = DependencyProperty.Register(
//        nameof(YAxisId),
//        typeof(string),
//        typeof(fxAnnotationCreationModifier),
//        new PropertyMetadata("DefaultAxisId"));
//    public static readonly DependencyProperty XAxisIdProperty = DependencyProperty.Register(
//        nameof(XAxisId),
//        typeof(string),
//        typeof(fxAnnotationCreationModifier),
//        new PropertyMetadata("DefaultAxisId"));
//    public static readonly DependencyProperty AnnotationTypeProperty = DependencyProperty.Register(
//        nameof(AnnotationType),
//        typeof(Type),
//        typeof(fxAnnotationCreationModifier),
//        new PropertyMetadata(null),
//        new ValidateValueCallback(OnAnnotationTypeChanged));
//    private Point _pointRelativeToModifier;
//    private AnnotationBase _annotation;

//    public event EventHandler<AnnotationCreationArgs> AnnotationCreated;

//    public string YAxisId
//    {
//        get
//        {
//            return (string) GetValue(YAxisIdProperty);
//        }
//        set
//        {
//            SetValue(YAxisIdProperty, value);
//        }
//    }

//    public string XAxisId
//    {
//        get
//        {
//            return (string) GetValue(XAxisIdProperty);
//        }
//        set
//        {
//            SetValue(XAxisIdProperty, value);
//        }
//    }

//    public Type AnnotationType
//    {
//        get
//        {
//            return (Type) GetValue(AnnotationTypeProperty);
//        }
//        set
//        {
//            SetValue(AnnotationTypeProperty, value);
//        }
//    }

//    public Style AnnotationStyle
//    {
//        get;
//        set;
//    }

//    public IAnnotation Annotation
//    {
//        get
//        {
//            return _annotation;
//        }
//        protected set
//        {
//            _annotation = (AnnotationBase) value;
//        }
//    }

//    protected override void OnIsEnabledChanged()
//    {
//        base.OnIsEnabledChanged();
//        _annotation = null;
//        if(!IsEnabled)
//        {
//            return;
//        }
//        ISciChartSurface parentSurface = ParentSurface;
//        if(parentSurface == null)
//        {
//            return;
//        }
//        parentSurface.Annotations
//            .ForEachDo(
//                annotation =>
//                {
//                    annotation.IsSelected = false;
//                    annotation.IsEditable = false;
//                });
//    }

//    protected void OnAnnotationCreated()
//    {
//        AnnotationCreated?.Invoke(this, new AnnotationCreationArgs(_annotation));

//        Annotation = null;
//    }

//    public override void OnModifierMouseMove(ModifierMouseArgs mouseEventArgs)
//    {
//        if(AnnotationType == null || _annotation == null || (!_annotation.IsAttached || _annotation.IsSelected))
//        {
//            return;
//        }
//        _annotation.UpdatePosition(
//            _pointRelativeToModifier,
//            GetPointRelativeTo(mouseEventArgs.MousePoint, ModifierSurface));
//    }

//    public override void OnModifierMouseUp(ModifierMouseArgs mouseButtonEventArgs)
//    {
//        if(AnnotationType == null ||
//            !MatchesExecuteOn(mouseButtonEventArgs.MouseButtons, ExecuteOn) ||
//            !mouseButtonEventArgs.IsMaster)
//        {
//            return;
//        }

//        if(_annotation != null && !_annotation.IsSelected)
//        {
//            _annotation.IsSelected = true;

//            if(_annotation is IfxFibonacciAnnotation)
//            {
//                return;
//            }

//            OnAnnotationCreated();
//        } else
//        {
//            if(_annotation != null && _annotation.IsAttached)
//            {
//                _annotation.IsSelected = false;
//            }

//            _pointRelativeToModifier = GetPointRelativeTo(mouseButtonEventArgs.MousePoint, ModifierSurface);
//            _annotation = CreateAnnotation(AnnotationType, AnnotationStyle);
//            _annotation.UpdatePosition(_pointRelativeToModifier, _pointRelativeToModifier);

//            if(!typeof(IAnchorPointAnnotation).IsAssignableFrom(AnnotationType) &&
//                !typeof(LineAnnotationWithLabelsBase).IsAssignableFrom(AnnotationType))
//            {
//                return;
//            }

//            _annotation.IsSelected = true;

//            OnAnnotationCreated();
//        }
//    }

//    protected virtual AnnotationBase CreateAnnotation(Type annotationType, Style annotationStyle)
//    {
//        AnnotationBase instance = (AnnotationBase)Activator.CreateInstance(annotationType);

//        instance.YAxisId = YAxisId;
//        instance.XAxisId = XAxisId;

//        if(annotationStyle != null && annotationStyle.TargetType == annotationType)
//        {
//            Style style = new Style(annotationType) { BasedOn = annotationStyle };
//            instance.Style = style;
//        }

//        ParentSurface.Annotations.Add(instance);
//        return instance;
//    }

//    private static bool OnAnnotationTypeChanged(object object_0)
//    {
//        if(object_0 != null && !typeof(IAnnotation).IsAssignableFrom((Type) object_0))
//        {
//            throw new ArgumentOutOfRangeException(
//                "value",
//                string.Format("Type {0} does not implement IAnnotation interface.", object_0));
//        }
//        return true;
//    }
//}
