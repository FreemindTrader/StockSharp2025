using StockSharp.Charting;
using StockSharp.Xaml.Charting.Visuals.Annotations;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

#nullable disable
namespace StockSharp.Charting;

[TemplatePart( Name = "PART_InputTextArea", Type = typeof( TextBox ) )]
public class AnnotationLabel : Control
{
    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof (Text), typeof (string), typeof (AnnotationLabel), new PropertyMetadata((object) string.Empty));
    public static readonly DependencyProperty LabelPlacementProperty = DependencyProperty.Register(nameof (LabelPlacement), typeof (LabelPlacement), typeof (AnnotationLabel), new PropertyMetadata((object) LabelPlacement.Auto, new PropertyChangedCallback(AnnotationLabel.OnLabelPlacementChanged)));
    public static readonly DependencyProperty LabelStyleProperty = DependencyProperty.Register(nameof (LabelStyle), typeof (Style), typeof (AnnotationLabel), new PropertyMetadata((object) null, new PropertyChangedCallback(AnnotationLabel.OnLabelPlacementChanged)));
    public static readonly DependencyProperty AxisLabelStyleProperty = DependencyProperty.Register(nameof (AxisLabelStyle), typeof (Style), typeof (AnnotationLabel), new PropertyMetadata((object) null, new PropertyChangedCallback(AnnotationLabel.OnLabelPlacementChanged)));
    public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(nameof (CornerRadius), typeof (CornerRadius), typeof (AnnotationLabel), new PropertyMetadata((object) new CornerRadius()));
    public static readonly DependencyProperty RotationAngleProperty = DependencyProperty.Register(nameof (RotationAngle), typeof (double), typeof (AnnotationLabel), new PropertyMetadata((object) 0.0));
    public static readonly DependencyProperty CanEditTextProperty = DependencyProperty.Register(nameof (CanEditText), typeof (bool), typeof (AnnotationLabel), new PropertyMetadata((object) false));
    [Obsolete("We're sorry! AnnotationLabel.TextFormatting is obsolete. Please use a value converter or set StringFormat on a binding.")]
    public static readonly DependencyProperty TextFormattingProperty = DependencyProperty.Register(nameof (TextFormatting), typeof (string), typeof (AnnotationLabel), new PropertyMetadata((object) string.Empty));
    private LineAnnotationWithLabelsBase _parentAnnotation;
    private TextBox _inputTextArea;

    public AnnotationLabel()
    {
        this.DefaultStyleKey = ( object ) typeof( AnnotationLabel );
        this.MouseLeftButtonDown += ( MouseButtonEventHandler ) ( ( s, e ) =>
        {
            this.TryFocusInputTextArea();
            this.ParentAnnotation.TrySelectAnnotation();
        } );
    }

    public bool IsAxisLabel
    {
        get
        {
            if ( this.LabelPlacement == LabelPlacement.Axis )
                return true;
            if ( this.ParentAnnotation != null )
                return this.ParentAnnotation.GetLabelPlacement( this ) == LabelPlacement.Axis;
            return false;
        }
    }

    public bool CanEditText
    {
        get
        {
            return ( bool ) this.GetValue( AnnotationLabel.CanEditTextProperty );
        }
        set
        {
            this.SetValue( AnnotationLabel.CanEditTextProperty, ( object ) value );
        }
    }

    public double RotationAngle
    {
        get
        {
            return ( double ) this.GetValue( AnnotationLabel.RotationAngleProperty );
        }
        set
        {
            this.SetValue( AnnotationLabel.RotationAngleProperty, ( object ) value );
        }
    }

    public LineAnnotationWithLabelsBase ParentAnnotation
    {
        get
        {
            return this._parentAnnotation;
        }
        set
        {
            if ( this._parentAnnotation != null )
                this._parentAnnotation.Unselected -= new EventHandler( this.OnParentAnnotationUnselected );
            this._parentAnnotation = value;
            if ( this._parentAnnotation == null )
                return;
            this._parentAnnotation.Unselected += new EventHandler( this.OnParentAnnotationUnselected );
            this.ApplyStyle();
        }
    }

    public string Text
    {
        get
        {
            return ( string ) this.GetValue( AnnotationLabel.TextProperty );
        }
        set
        {
            this.SetValue( AnnotationLabel.TextProperty, ( object ) value );
        }
    }

    public LabelPlacement LabelPlacement
    {
        get
        {
            return ( LabelPlacement ) this.GetValue( AnnotationLabel.LabelPlacementProperty );
        }
        set
        {
            this.SetValue( AnnotationLabel.LabelPlacementProperty, ( object ) value );
        }
    }

    [Obsolete( "We're sorry! AnnotationLabel.TextFormatting is obsolete. Please use a value converter or set StringFormat on a binding.", true )]
    public string TextFormatting
    {
        get
        {
            throw new Exception( "We're sorry! AnnotationLabel.TextFormatting is obsolete. Please use a value converter or set StringFormat on a binding." );
        }
        set
        {
            throw new Exception( "We're sorry! AnnotationLabel.TextFormatting is obsolete. Please use a value converter or set StringFormat on a binding." );
        }
    }

    public Style LabelStyle
    {
        get
        {
            return ( Style ) this.GetValue( AnnotationLabel.LabelStyleProperty );
        }
        set
        {
            this.SetValue( AnnotationLabel.LabelStyleProperty, ( object ) value );
        }
    }

    public Style AxisLabelStyle
    {
        get
        {
            return ( Style ) this.GetValue( AnnotationLabel.AxisLabelStyleProperty );
        }
        set
        {
            this.SetValue( AnnotationLabel.AxisLabelStyleProperty, ( object ) value );
        }
    }

    public CornerRadius CornerRadius
    {
        get
        {
            return ( CornerRadius ) this.GetValue( AnnotationLabel.CornerRadiusProperty );
        }
        set
        {
            this.SetValue( AnnotationLabel.CornerRadiusProperty, ( object ) value );
        }
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        if ( this._inputTextArea != null )
            this._inputTextArea.ClearValue( TextBox.TextProperty );
        this._inputTextArea = this.GetAndAssertTemplateChild<TextBox>( "PART_InputTextArea" );
    }

    protected T GetAndAssertTemplateChild<T>( string childName ) where T : class
    {
        T templateChild = this.GetTemplateChild(childName) as T;
        if ( ( object ) templateChild == null )
            throw new InvalidOperationException( string.Format( "Unable to Apply the Control Template. {0} is missing or of the wrong type", ( object ) childName ) );
        return templateChild;
    }

    private void TryFocusInputTextArea()
    {
        if ( !this.CanEditText || !this.ParentAnnotation.CanEditText || !this.ParentAnnotation.IsSelected )
            return;
        this._inputTextArea.IsEnabled = true;
        this._inputTextArea.Focus();
    }

    private void RemoveFocusInputTextArea()
    {
        if ( this._inputTextArea == null )
            return;
        this._inputTextArea.IsEnabled = false;
    }

    private void ApplyStyle()
    {
        this.Style = this.IsAxisLabel ? this.AxisLabelStyle : this.LabelStyle;
    }

    private static void OnLabelPlacementChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
    {
        AnnotationLabel annotationLabel = d as AnnotationLabel;
        if ( annotationLabel == null || annotationLabel.ParentAnnotation == null )
            return;
        annotationLabel.ApplyStyle();
        annotationLabel.ParentAnnotation.InvalidateLabel( annotationLabel );
    }

    private void OnParentAnnotationUnselected( object sender, EventArgs e )
    {
        this.RemoveFocusInputTextArea();
    }
}