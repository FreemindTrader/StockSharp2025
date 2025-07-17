// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.AnnotationCreationModifier
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Windows;
namespace Ecng.Xaml.Charting
{
    public class AnnotationCreationModifier : ChartModifierBase
    {
        public static readonly DependencyProperty YAxisIdProperty = DependencyProperty.Register(nameof (YAxisId), typeof (string), typeof (AnnotationCreationModifier), new PropertyMetadata((object) "DefaultAxisId"));
        public static readonly DependencyProperty XAxisIdProperty = DependencyProperty.Register(nameof (XAxisId), typeof (string), typeof (AnnotationCreationModifier), new PropertyMetadata((object) "DefaultAxisId"));
        private Point _draggingStartPoint;
        private AnnotationBase _newAnnotation;
        private Type _newAnnotationType;
        private Style _newAnnotationStyle;

        public event EventHandler AnnotationCreated;

        public string YAxisId
        {
            get
            {
                return ( string ) this.GetValue( AnnotationCreationModifier.YAxisIdProperty );
            }
            set
            {
                this.SetValue( AnnotationCreationModifier.YAxisIdProperty, ( object ) value );
            }
        }

        public string XAxisId
        {
            get
            {
                return ( string ) this.GetValue( AnnotationCreationModifier.XAxisIdProperty );
            }
            set
            {
                this.SetValue( AnnotationCreationModifier.XAxisIdProperty, ( object ) value );
            }
        }

        public Type AnnotationType
        {
            get
            {
                return this._newAnnotationType;
            }
            set
            {
                if ( value != ( Type ) null && !typeof( IAnnotation ).IsAssignableFrom( value ) )
                {
                    throw new ArgumentOutOfRangeException( nameof( value ), string.Format( "Type {0} does not implement IAnnotation interface.", ( object ) value ) );
                }

                this._newAnnotationType = value;
            }
        }

        public Style AnnotationStyle
        {
            get
            {
                return this._newAnnotationStyle;
            }
            set
            {
                this._newAnnotationStyle = value;
            }
        }

        public IAnnotation Annotation
        {
            get
            {
                return ( IAnnotation ) this._newAnnotation;
            }
        }

        protected override void OnIsEnabledChanged()
        {
            base.OnIsEnabledChanged();
            this._newAnnotation = ( AnnotationBase ) null;
            if ( !this.IsEnabled || this.ParentSurface == null )
            {
                return;
            }

            this.ParentSurface.Annotations.ForEachDo<IAnnotation>( ( Action<IAnnotation> ) ( annotation =>
          {
              annotation.IsSelected = false;
              annotation.IsEditable = false;
          } ) );
        }

        protected void OnAnnotationCreated()
        {
            // ISSUE: reference to a compiler-generated field
            EventHandler annotationCreated = this.AnnotationCreated;
            if ( annotationCreated == null )
            {
                return;
            }

            annotationCreated( ( object ) this, EventArgs.Empty );
        }

        public override void OnModifierMouseMove( ModifierMouseArgs mouseEventArgs )
        {
            if ( this._newAnnotationType == ( Type ) null || this._newAnnotation == null || ( !this._newAnnotation.IsAttached || this._newAnnotation.IsSelected ) )
            {
                return;
            }

            this._newAnnotation.UpdatePosition( this._draggingStartPoint, this.GetPointRelativeTo( mouseEventArgs.MousePoint, ( IHitTestable ) this.ModifierSurface ) );
        }

        private bool IsSinglePointAnnotation( Type annType )
        {
            if ( !typeof( IAnchorPointAnnotation ).IsAssignableFrom( annType ) )
            {
                return annType.IsSubclassOf( typeof( LineAnnotationWithLabelsBase ) );
            }

            return true;
        }

        public override void OnModifierMouseDown( ModifierMouseArgs mouseButtonEventArgs )
        {
            base.OnModifierMouseDown( mouseButtonEventArgs );
            if ( this._newAnnotationType == ( Type ) null || !this.MatchesExecuteOn( mouseButtonEventArgs.MouseButtons, this.ExecuteOn ) || !mouseButtonEventArgs.IsMaster || this._newAnnotation != null && !this._newAnnotation.IsSelected )
            {
                return;
            }

            mouseButtonEventArgs.Handled = true;
            if ( this._newAnnotation != null && this._newAnnotation.IsAttached )
            {
                this._newAnnotation.IsSelected = false;
            }

            this._draggingStartPoint = this.GetPointRelativeTo( mouseButtonEventArgs.MousePoint, ( IHitTestable ) this.ModifierSurface );
            if ( this.IsSinglePointAnnotation( this._newAnnotationType ) )
            {
                return;
            }

            this._newAnnotation = this.CreateAnnotation( this._newAnnotationType, this._newAnnotationStyle );
            this._newAnnotation.UpdatePosition( this._draggingStartPoint, this._draggingStartPoint );
        }

        public override void OnModifierMouseUp( ModifierMouseArgs mouseButtonEventArgs )
        {
            if ( this._newAnnotationType == ( Type ) null || !this.MatchesExecuteOn( mouseButtonEventArgs.MouseButtons, this.ExecuteOn ) || !mouseButtonEventArgs.IsMaster )
            {
                return;
            }

            if ( this.IsSinglePointAnnotation( this._newAnnotationType ) && this._newAnnotation == null )
            {
                this._newAnnotation = this.CreateAnnotation( this._newAnnotationType, this._newAnnotationStyle );
                Point pointRelativeTo = this.GetPointRelativeTo(mouseButtonEventArgs.MousePoint, (IHitTestable) this.ModifierSurface);
                this._newAnnotation.UpdatePosition( pointRelativeTo, pointRelativeTo );
            }
            if ( this._newAnnotation == null )
            {
                return;
            }

            AnnotationBase newAnnotation = this._newAnnotation;
            this._newAnnotation.IsSelected = true;
            this.OnAnnotationCreated();
            newAnnotation.UpdateAdorners();
        }

        protected virtual AnnotationBase CreateAnnotation( Type annotationType, Style annotationStyle )
        {
            AnnotationBase instance = (AnnotationBase) Activator.CreateInstance(annotationType);
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
    }
}
