using SciChart.Charting.ChartModifiers;
using SciChart.Charting.Visuals;
using SciChart.Charting.Visuals.Annotations;
using SciChart.Core.Extensions;
using SciChart.Core.Framework;
using SciChart.Core.Utility.Mouse;
using fx.Charting.HewFibonacci;
using System;
using System.Collections.Generic; using fx.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace fx.Charting
{
    public class fxAnnotationCreationModifier : ChartModifierBase
    {
        public static readonly DependencyProperty YAxisIdProperty        = DependencyProperty.Register( nameof( YAxisId ), typeof( string ), typeof( fxAnnotationCreationModifier ), new PropertyMetadata( "DefaultAxisId" ) );
        public static readonly DependencyProperty XAxisIdProperty        = DependencyProperty.Register( nameof( XAxisId ), typeof( string ), typeof( fxAnnotationCreationModifier ), new PropertyMetadata( "DefaultAxisId" ) );
        public static readonly DependencyProperty AnnotationTypeProperty = DependencyProperty.Register( nameof( AnnotationType ), typeof( Type ), typeof( fxAnnotationCreationModifier ), new PropertyMetadata( null ), new ValidateValueCallback( OnAnnotationTypeChanged ) );
        private Point _pointRelativeToModifier;
        private AnnotationBase _annotation;

        public event EventHandler< AnnotationCreationArgs > AnnotationCreated;

        public string YAxisId
        {
            get
            {
                return ( string ) GetValue( YAxisIdProperty );
            }
            set
            {
                SetValue( YAxisIdProperty, value );
            }
        }

        public string XAxisId
        {
            get
            {
                return ( string ) GetValue( XAxisIdProperty );
            }
            set
            {
                SetValue( XAxisIdProperty, value );
            }
        }

        public Type AnnotationType
        {
            get
            {
                return ( Type ) GetValue( AnnotationTypeProperty );
            }
            set
            {
                SetValue( AnnotationTypeProperty, value );
            }
        }

        public Style AnnotationStyle
        {
            get;
            set;
        }

        public IAnnotation Annotation
        {
            get
            {
                return _annotation;
            }
            protected set
            {
                _annotation = ( AnnotationBase ) value;
            }
        }

        protected override void OnIsEnabledChanged( )
        {
            base.OnIsEnabledChanged( );
            _annotation = null;
            if ( !IsEnabled )
            {
                return;
            }
            ISciChartSurface parentSurface = ParentSurface;
            if ( parentSurface == null )
            {
                return;
            }
            parentSurface.Annotations.ForEachDo(
                                                                 annotation =>
                                                                 {
                                                                     annotation.IsSelected = false;
                                                                     annotation.IsEditable = false;
                                                                 }
                                                              );
        }

        protected void OnAnnotationCreated( )
        {
            AnnotationCreated?.Invoke( this, new AnnotationCreationArgs( _annotation ) );

            Annotation = null;
        }

        public override void OnModifierMouseMove( ModifierMouseArgs mouseEventArgs )
        {
            if ( AnnotationType == null || _annotation == null || ( !_annotation.IsAttached || _annotation.IsSelected ) )
            {
                return;
            }
            _annotation.UpdatePosition( _pointRelativeToModifier, GetPointRelativeTo( mouseEventArgs.MousePoint, ModifierSurface ) );
        }

        public override void OnModifierMouseUp( ModifierMouseArgs mouseButtonEventArgs )
        {
            if ( AnnotationType == null || !MatchesExecuteOn( mouseButtonEventArgs.MouseButtons, ExecuteOn ) || !mouseButtonEventArgs.IsMaster )
            {
                return;
            }

            if ( _annotation != null && !_annotation.IsSelected )
            {
                _annotation.IsSelected = true;

                if ( _annotation is IfxFibonacciAnnotation )
                {
                    return;
                }

                OnAnnotationCreated( );
            }
            else
            {
                if ( _annotation != null && _annotation.IsAttached )
                {
                    _annotation.IsSelected = false;
                }

                _pointRelativeToModifier = GetPointRelativeTo( mouseButtonEventArgs.MousePoint, ModifierSurface );
                _annotation = CreateAnnotation( AnnotationType, AnnotationStyle );
                _annotation.UpdatePosition( _pointRelativeToModifier, _pointRelativeToModifier );

                if ( !typeof( IAnchorPointAnnotation ).IsAssignableFrom( AnnotationType ) && !typeof( LineAnnotationWithLabelsBase ).IsAssignableFrom( AnnotationType ) )
                {
                    return;
                }

                _annotation.IsSelected = true;

                OnAnnotationCreated( );
            }
        }

        protected virtual AnnotationBase CreateAnnotation( Type annotationType, Style annotationStyle )
        {
            AnnotationBase instance = ( AnnotationBase )Activator.CreateInstance( annotationType );

            instance.YAxisId = YAxisId;
            instance.XAxisId = XAxisId;

            if ( annotationStyle != null && annotationStyle.TargetType == annotationType )
            {
                Style style = new Style( annotationType ) { BasedOn = annotationStyle };
                instance.Style = style;
            }

            ParentSurface.Annotations.Add( instance );
            return instance;
        }

        private static bool OnAnnotationTypeChanged( object object_0 )
        {
            if ( object_0 != null && !typeof( IAnnotation ).IsAssignableFrom( ( Type ) object_0 ) )
            {
                throw new ArgumentOutOfRangeException( "value", string.Format( "Type {0} does not implement IAnnotation interface.", object_0 ) );
            }
            return true;
        }
    }
}
