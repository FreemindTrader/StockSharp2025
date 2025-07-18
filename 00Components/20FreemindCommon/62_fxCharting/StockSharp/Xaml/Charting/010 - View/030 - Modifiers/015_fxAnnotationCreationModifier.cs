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
/// This modifier is used to define the behaviour for the Annotation. At first, I was thinking about using  this
/// ChartModifier to creat the fibonacci retracment and expansion.  Ultimately, I learnt that I have to use
/// MyTradingAnnotationBase instead of this fxAnnotationCreationModifier
/// </summary>
public class fxAnnotationCreationModifier : ChartModifierBase
{
    public static readonly DependencyProperty YAxisIdProperty = DependencyProperty.Register(
        nameof(YAxisId),
        typeof(string),
        typeof(fxAnnotationCreationModifier),
        new PropertyMetadata((object) "DefaultAxisId"));

    public static readonly DependencyProperty XAxisIdProperty = DependencyProperty.Register(
        nameof(XAxisId),
        typeof(string),
        typeof(fxAnnotationCreationModifier),
        new PropertyMetadata((object) "DefaultAxisId"));

    private Point _draggingStartPoint;

    private AnnotationBaseEx _newAnnotation;

    private Type _annotationType;

    private Style _annotationStyle;

    public event EventHandler AnnotationCreated;


    /// <summary>
    ///
    /// </summary>
    public string YAxisId
    {
        get
        {
            return (string) GetValue(fxAnnotationCreationModifier.YAxisIdProperty);
        }
        set
        {
            SetValue(fxAnnotationCreationModifier.YAxisIdProperty, (object) value);
        }
    }

    /// <summary>
    ///
    /// </summary>
    public string XAxisId
    {
        get
        {
            return (string) GetValue(fxAnnotationCreationModifier.XAxisIdProperty);
        }
        set
        {
            SetValue(fxAnnotationCreationModifier.XAxisIdProperty, (object) value);
        }
    }

    /// <summary>
    ///
    /// </summary>
    public Type AnnotationType
    {
        get => _annotationType;
        set
        {
            _annotationType = !(value != (Type) null) || typeof(IAnnotation).IsAssignableFrom(value)
                ? value
                : throw new ArgumentOutOfRangeException(
                    "value",
                    $"Type {value} does not implement IAnnotation interface.");
        }
    }


    /// <summary>
    ///
    /// </summary>
    public Style AnnotationStyle
    {
        get => _annotationStyle;
        set => _annotationStyle = value;
    }

    /// <summary>
    ///
    /// </summary>
    public IAnnotation Annotation
    {
        get
        {
            return _newAnnotation;
        }
    }


    /// <summary>
    /// Called when the IsEnabled property changes on this SciChart.Charting.ChartModifiers.ChartModifierBase instance 
    /// This annotation is no longer Selectable or Editable.
    /// </summary>
    protected override void OnIsEnabledChanged()
    {
        base.OnIsEnabledChanged();
        _newAnnotation = null;
        if(!IsEnabled || ParentSurface == null)
            return;
        ParentSurface.Annotations
            .ForEach<IAnnotation>(
                annotation =>
                {
                    annotation.IsSelected = false;
                    annotation.IsEditable = false;
                });
    }

    /// <summary>
    ///
    /// </summary>
    protected void OnAnnotationCreated()
    {
        AnnotationCreated?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// When the user is holding the mouse button witout releasing, the dragging process will start, we will update the annotation to the new position.
    /// </summary>
    /// <param name="mouseEventArgs"></param>
    public override void OnModifierMouseMove(ModifierMouseArgs mouseEventArgs)
    {
        if(_annotationType == null || _newAnnotation == null || !_newAnnotation.IsAttached || _newAnnotation.IsSelected)
            return;

        _newAnnotation.UpdatePosition(
            _draggingStartPoint,
            GetPointRelativeTo(mouseEventArgs.MousePoint, (IHitTestable) ModifierSurface));
    }

    private bool IsSubclassOfIsAssignableFrom(Type t)
    {
        return typeof(IAnchorPointAnnotation).IsAssignableFrom(t) ||
            t.IsSubclassOf(typeof(LineAnnotationWithLabelsBase));
    }


    /// <summary>
    /// When the user first click the mouse on the annotation without releasing, this code detect if it is the correct button to 
    /// click, if it is correct, we will start the dragging process.
    /// </summary>
    /// <param name="mouseEvent"></param>
    public override void OnModifierMouseDown(ModifierMouseArgs mouseEvent)
    {
        base.OnModifierMouseDown(mouseEvent);
        if(_annotationType == null ||
            !MatchesExecuteOn(mouseEvent.MouseButtons, ExecuteOn) ||
            !mouseEvent.IsMaster ||
            _newAnnotation != null &&
            !_newAnnotation.IsSelected)
            return;
        mouseEvent.Handled = true;

        if(_newAnnotation != null && _newAnnotation.IsAttached)
            _newAnnotation.IsSelected = false;

        _draggingStartPoint = GetPointRelativeTo(mouseEvent.MousePoint, ModifierSurface);

        if(IsSubclassOfIsAssignableFrom(_annotationType))
            return;

        _newAnnotation = (AnnotationBaseEx) CreateAnnotation(_annotationType, _annotationStyle);
        _newAnnotation.UpdatePosition(_draggingStartPoint, _draggingStartPoint);
    }


    /// <summary>
    /// When the user finish clicking the mouse, we move the annotation to a new position and select it.
    /// </summary>
    /// <param name="mouseEvent"></param>
    public override void OnModifierMouseUp(ModifierMouseArgs mouseEvent)
    {
        if(_annotationType == null || !MatchesExecuteOn(mouseEvent.MouseButtons, ExecuteOn) || !mouseEvent.IsMaster)
            return;

        if(IsSubclassOfIsAssignableFrom(_annotationType) && _newAnnotation == null)
        {
            _newAnnotation = (AnnotationBaseEx) CreateAnnotation(_annotationType, _annotationStyle);
            var point = GetPointRelativeTo(mouseEvent.MousePoint, ModifierSurface);
            _newAnnotation.UpdatePosition(point, point);
        }
        if(_newAnnotation == null)
            return;

        _newAnnotation.IsSelected = true;
        OnAnnotationCreated();

        _newAnnotation.UpdateAdorners();
    }


    /// <summary>
    /// Create a new annotation of the specified type and style and add it to the drawing surface.
    /// </summary>
    /// <param name="annotationType">Type of annotation, like a like, Fib Expansion, Fib Retracement</param>
    /// <param name="annotationStyle"></param>
    /// <returns></returns>
    protected virtual IAnnotation CreateAnnotation(Type annotationType, Style annotationStyle)
    {
        var instance = (AnnotationBaseEx) Activator.CreateInstance(annotationType);
        instance.YAxisId = YAxisId;
        instance.XAxisId = XAxisId;
        if(annotationStyle != null && annotationStyle.TargetType == annotationType)
        {
            Style style = new Style(annotationType) { BasedOn = annotationStyle };
            instance.Style = style;
        }
        ParentSurface.Annotations.Add((IAnnotation) instance);
        return instance;
    }
}