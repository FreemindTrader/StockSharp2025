using DevExpress.Mvvm.Native;
using SciChart.Charting.ChartModifiers;
using SciChart.Charting.Visuals.Annotations;
using SciChart.Core.Framework;
using StockSharp.Xaml.Charting.Visuals.Annotations;
using System;
using System.Diagnostics;
using System.Windows;

#nullable disable
namespace StockSharp.Xaml.Charting;

public class fxAnnotationCreationModifier : ChartModifierBase
{
    public static readonly DependencyProperty YAxisIdProperty = DependencyProperty.Register(nameof(YAxisId), typeof(string), typeof(fxAnnotationCreationModifier), new PropertyMetadata((object) "DefaultAxisId"));

    public static readonly DependencyProperty XAxisIdProperty = DependencyProperty.Register(nameof(XAxisId), typeof(string), typeof(fxAnnotationCreationModifier), new PropertyMetadata((object) "DefaultAxisId"));

    private Point _pointRelativeToModifier;

    private AnnotationBase _annotation;

    private Type _annotationType;

    private Style _annotationStyle;

    public event EventHandler AnnotationCreated;

    public void \u0023\u003DzyBjJfv2nzVhf( EventHandler _param1 );

    public void \u0023\u003DzzZ2ZUVx2hG0P( EventHandler _param1 );

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

    public override void OnModifierMouseMove( ModifierMouseArgs mouseEventArgs )
    {
        if ( this._annotationType == ( Type ) null || this._annotation == null || !this._annotation.IsAttached || this._annotation.IsSelected )
            return;
        this._annotation.UpdatePosition(
            this._pointRelativeToModifier,
            this.GetPointRelativeTo( mouseEventArgs.MousePoint(), ( IHitTestable ) this.ModifierSurface ) );
    }

    private bool IsSubclassOfIsAssignableFrom( Type t )
    {
        return typeof( IAnchorPointAnnotation ).IsAssignableFrom( t ) || t.IsSubclassOf( typeof( LineAnnotationWithLabelsBase ) );
    }

    public override void OnModifierMouseDown( ModifierMouseArgs _param1 )
    {
        base.OnModifierMouseDown( _param1 );
        if ( this._annotationType == ( Type ) null || !this.MatchesExecuteOn( _param1.MouseButtons(), this.ExecuteOn ) || !_param1.IsMaster() || this._annotation != null && !this._annotation.IsSelected )
            return;
        _param1.Handled( true );
        if ( this._annotation != null && this._annotation.IsAttached )
            this._annotation.IsSelected = false;
        this._pointRelativeToModifier = this.GetPointRelativeTo( _param1.MousePoint(), ( IHitTestable ) this.ModifierSurface );
        if ( this.IsSubclassOfIsAssignableFrom( this._annotationType ) )
            return;
        this._annotation = this.CreateAnnotation( this._annotationType, this._annotationStyle );
        this._annotation.UpdatePosition( this._pointRelativeToModifier, this._pointRelativeToModifier );
    }

    public override void OnModifierMouseUp( ModifierMouseArgs _param1 )
    {
        if ( this._annotationType == ( Type ) null || !this.MatchesExecuteOn( _param1.MouseButtons(), this.ExecuteOn ) || !_param1.IsMaster() )
            return;
        if ( this.IsSubclassOfIsAssignableFrom( this._annotationType ) && this._annotation == null )
        {
            this._annotation = this.CreateAnnotation( this._annotationType, this._annotationStyle );
            Point point = this.GetPointRelativeTo( _param1.MousePoint(), ( IHitTestable ) this.ModifierSurface );
            this._annotation.UpdatePosition( point, point );
        }
        if ( this._annotation == null )
            return;
        AnnotationBase zJu3oQ4Zae0S = this._annotation;
        this._annotation.IsSelected = true;
        this.OnAnnotationCreated();
        zJu3oQ4Zae0S.UpdateAdorners();
    }

    protected virtual AnnotationBase CreateAnnotation( Type _param1, Style _param2 )
    {
        AnnotationBase instance = (AnnotationBase) Activator.CreateInstance(_param1);
        instance.YAxisId = this.YAxisId;
        instance.XAxisId = this.XAxisId;
        if ( _param2 != null && _param2.TargetType == _param1 )
        {
            Style style = new Style(_param1) { BasedOn = _param2 };
            instance.Style = style;
        }
        this.ParentSurface.get_Annotations().Add( ( IAnnotation ) instance );
        return instance;
    }

    [Serializable]
    private new sealed class SomeClass34343383
    {
        public static readonly fxAnnotationCreationModifier.SomeClass34343383 SomeMethond0343 = new fxAnnotationCreationModifier.SomeClass34343383();
        public static Action<IAnnotation> \u0023\u003DzmsiBBf5ih\u0024wjnD8X9g\u003D\u003D;

    public void \u0023\u003DzLdGxEgyAELbBj1m4vj3jYKw\u003D(
      IAnnotation _param1)
    {
      _param1.set_IsSelected(false);
      _param1.set_IsEditable(false);
    }
}
}
