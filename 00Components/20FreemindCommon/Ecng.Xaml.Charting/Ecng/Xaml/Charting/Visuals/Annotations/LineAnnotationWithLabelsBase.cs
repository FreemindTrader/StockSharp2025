// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.Annotations.LineAnnotationWithLabelsBase
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Shapes;
using StockSharp.Xaml.Charting.Common.Databinding;
using StockSharp.Xaml.Charting.Common.Extensions;
using StockSharp.Xaml.Charting.Themes;
using StockSharp.Xaml.Charting.Visuals.Axes;

namespace StockSharp.Xaml.Charting.Visuals.Annotations
{
    public abstract class LineAnnotationWithLabelsBase : LineAnnotation
    {
        public static readonly DependencyProperty ShowLabelProperty = DependencyProperty.Register(nameof (ShowLabel), typeof (bool), typeof (LineAnnotationWithLabelsBase), new PropertyMetadata((object) false, new PropertyChangedCallback(LineAnnotationWithLabelsBase.OnShowLabelChanged)));
        protected internal static readonly DependencyProperty DefaultLabelValueProperty = DependencyProperty.Register(nameof (DefaultLabelValue), typeof (IComparable), typeof (LineAnnotationWithLabelsBase), new PropertyMetadata((PropertyChangedCallback) null));
        protected static readonly DependencyProperty DefaultTextFormattingProperty = DependencyProperty.Register(nameof (DefaultTextFormatting), typeof (string), typeof (LineAnnotationWithLabelsBase), new PropertyMetadata((PropertyChangedCallback) null));
        public static readonly DependencyProperty LabelPlacementProperty = DependencyProperty.Register(nameof (LabelPlacement), typeof (LabelPlacement), typeof (LineAnnotationWithLabelsBase), new PropertyMetadata((object) LabelPlacement.Auto));
        public static readonly DependencyProperty LabelValueProperty = DependencyProperty.Register(nameof (LabelValue), typeof (IComparable), typeof (LineAnnotationWithLabelsBase), new PropertyMetadata((object) null));
        public static readonly DependencyProperty LabelTextFormattingProperty = DependencyProperty.Register(nameof (LabelTextFormatting), typeof (string), typeof (LineAnnotationWithLabelsBase), new PropertyMetadata((object) string.Empty, new PropertyChangedCallback(LineAnnotationWithLabelsBase.OnLabelTextFormattingChanged)));
        public static readonly DependencyProperty FormattedLabelProperty = DependencyProperty.Register(nameof (FormattedLabel), typeof (string), typeof (LineAnnotationWithLabelsBase), new PropertyMetadata((object) string.Empty));
        public static readonly DependencyProperty AnnotationLabelsProperty = DependencyProperty.Register(nameof (AnnotationLabels), typeof (ObservableCollection<AnnotationLabel>), typeof (LineAnnotationWithLabelsBase), new PropertyMetadata(new PropertyChangedCallback(LineAnnotationWithLabelsBase.OnAnnotationLabelsChanged)));
        private CategoryIndexToDataValueConverter _xyValueConverter;

        protected LineAnnotationWithLabelsBase()
        {
            this.AnnotationLabels = new ObservableCollection<AnnotationLabel>();
            Binding binding = new Binding(nameof (LabelValue)) { Source = (object) this, Mode = BindingMode.OneWay, Converter = (IValueConverter) new GetAxisFormattedValueConverter(this) };
            this.SetBinding( LineAnnotationWithLabelsBase.FormattedLabelProperty, ( BindingBase ) binding );
        }

        protected IComparable DefaultLabelValue
        {
            get
            {
                return ( IComparable ) this.GetValue( LineAnnotationWithLabelsBase.DefaultLabelValueProperty );
            }
        }

        protected string DefaultTextFormatting
        {
            get
            {
                return ( string ) this.GetValue( LineAnnotationWithLabelsBase.DefaultTextFormattingProperty );
            }
        }

        protected string FormattedLabel
        {
            get
            {
                return ( string ) this.GetValue( LineAnnotationWithLabelsBase.FormattedLabelProperty );
            }
        }

        public ObservableCollection<AnnotationLabel> AnnotationLabels
        {
            get
            {
                return ( ObservableCollection<AnnotationLabel> ) this.GetValue( LineAnnotationWithLabelsBase.AnnotationLabelsProperty );
            }
            set
            {
                this.SetValue( LineAnnotationWithLabelsBase.AnnotationLabelsProperty, ( object ) value );
            }
        }

        public bool ShowLabel
        {
            get
            {
                return ( bool ) this.GetValue( LineAnnotationWithLabelsBase.ShowLabelProperty );
            }
            set
            {
                this.SetValue( LineAnnotationWithLabelsBase.ShowLabelProperty, ( object ) value );
            }
        }

        public LabelPlacement LabelPlacement
        {
            get
            {
                return ( LabelPlacement ) this.GetValue( LineAnnotationWithLabelsBase.LabelPlacementProperty );
            }
            set
            {
                this.SetValue( LineAnnotationWithLabelsBase.LabelPlacementProperty, ( object ) value );
            }
        }

        [TypeConverter( typeof( StringToLabelValueConverter ) )]
        public IComparable LabelValue
        {
            get
            {
                return ( IComparable ) this.GetValue( LineAnnotationWithLabelsBase.LabelValueProperty );
            }
            set
            {
                this.SetValue( LineAnnotationWithLabelsBase.LabelValueProperty, ( object ) value );
            }
        }

        public string LabelTextFormatting
        {
            get
            {
                return ( string ) this.GetValue( LineAnnotationWithLabelsBase.LabelTextFormattingProperty );
            }
            set
            {
                this.SetValue( LineAnnotationWithLabelsBase.LabelTextFormattingProperty, ( object ) value );
            }
        }

        protected void AttachLabels( IEnumerable<AnnotationLabel> labels )
        {
            bool flag = false;
            foreach ( AnnotationLabel label in labels )
            {
                this.Attach( label );
                flag = label.IsAxisLabel;
            }
            if ( !flag )
            {
                return;
            }

            this.Refresh();
        }

        protected void DetachLabels( IEnumerable<AnnotationLabel> labels )
        {
            foreach ( AnnotationLabel label in labels )
            {
                this.Detach( label );
            }
        }

        protected virtual void Attach( AnnotationLabel label )
        {
            if ( this.IsHidden )
            {
                return;
            }

            LabelPlacement labelPlacement = this.GetLabelPlacement(label);
            this.ApplyPlacement( label, labelPlacement );
            label.DataContext = ( object ) this;
            label.ParentAnnotation = this;
            IAxis usedAxis = this.GetUsedAxis();
            if ( label.IsAxisLabel )
            {
                if ( usedAxis == null )
                {
                    return;
                }

                usedAxis.ModifierAxisCanvas.SafeAddChild( ( object ) label, -1 );
            }
            else
            {
                ( this.AnnotationRoot as Grid ).SafeAddChild( ( object ) label, -1 );
            }
        }

        public abstract IAxis GetUsedAxis();

        protected override void OnXAxesCollectionChanged( object sender, NotifyCollectionChangedEventArgs args )
        {
            this.InvalidateAnnotation();
        }

        private void InvalidateAnnotation()
        {
            this.InvalidateAxisLabels();
            this.BindDefaultLabelValue();
        }

        private void InvalidateAxisLabels()
        {
            using ( this.SuspendUpdates() )
            {
                this.AnnotationLabels.Where<AnnotationLabel>( ( Func<AnnotationLabel, bool> ) ( label => label.IsAxisLabel ) ).ForEachDo<AnnotationLabel>( ( Action<AnnotationLabel> ) ( label =>
                  {
                      this.Detach( label );
                      this.InvalidateLabel( label );
                  } ) );
            }
        }

        public void InvalidateLabel( AnnotationLabel annotationLabel )
        {
            this.Attach( annotationLabel );
            this.Refresh();
        }

        private void BindDefaultLabelValue()
        {
            IAxis usedAxis = this.GetUsedAxis();
            this._xyValueConverter = this._xyValueConverter ?? ( this._xyValueConverter = new CategoryIndexToDataValueConverter( this ) );
            Binding binding1 = new Binding(usedAxis == null || !usedAxis.IsXAxis ? "Y1" : "X1") { Source = (object) this, Converter = (IValueConverter) this._xyValueConverter };
            this.SetBinding( LineAnnotationWithLabelsBase.DefaultLabelValueProperty, ( BindingBase ) binding1 );
            if ( usedAxis == null )
            {
                return;
            }

            Binding binding2 = new Binding("CursorTextFormatting") { Source = (object) usedAxis };
            this.SetBinding( LineAnnotationWithLabelsBase.DefaultTextFormattingProperty, ( BindingBase ) binding2 );
        }

        protected override void OnYAxesCollectionChanged( object sender, NotifyCollectionChangedEventArgs args )
        {
            this.InvalidateAnnotation();
        }

        protected override void OnYAxisIdChanged()
        {
            this.InvalidateAnnotation();
        }

        protected override void OnXAxisIdChanged()
        {
            this.InvalidateAnnotation();
        }

        protected override void OnAxisAlignmentChanged( IAxis axis, AxisAlignment oldAlignment )
        {
            base.OnAxisAlignmentChanged( axis, oldAlignment );
            this.InvalidateAnnotation();
        }

        protected override void MakeInvisible()
        {
            base.MakeInvisible();
            this.DetachLabels( this.AnnotationLabels.Where<AnnotationLabel>( ( Func<AnnotationLabel, bool> ) ( label => label.IsAxisLabel ) ) );
        }

        protected override void MakeVisible( AnnotationCoordinates coordinates )
        {
            base.MakeVisible( coordinates );
            IAxis usedAxis = this.GetUsedAxis();
            if ( usedAxis == null || usedAxis.ModifierAxisCanvas == null )
            {
                return;
            }

            this.AttachLabels( this.AnnotationLabels.Where<AnnotationLabel>( ( Func<AnnotationLabel, bool> ) ( label => label.Parent == null ) ) );
        }

        public override void OnApplyTemplate()
        {
            this.DetachLabels( ( IEnumerable<AnnotationLabel> ) this.AnnotationLabels );
            this.AnnotationRoot = ( FrameworkElement ) this.GetAndAssertTemplateChild<Grid>( "PART_LineAnnotationRoot" );
            this.GetAndAssertTemplateChild<Line>( "PART_GhostLine" );
            this.AttachLabels( ( IEnumerable<AnnotationLabel> ) this.AnnotationLabels );
            this.Refresh();
        }

        public override void OnAttached()
        {
            base.OnAttached();
            this.BindDefaultLabelValue();
        }

        protected override void OnAnnotationLoaded( object sender, RoutedEventArgs e )
        {
            base.OnAnnotationLoaded( sender, e );
            this.GetBindingExpression( LineAnnotationWithLabelsBase.DefaultLabelValueProperty )?.UpdateTarget();
        }

        public AnnotationLabel AddLabel()
        {
            AnnotationLabel annotationLabel = new AnnotationLabel();
            Binding binding1 = new Binding("LabelPlacement") { Source = (object) this, Mode = BindingMode.OneWay };
            annotationLabel.SetBinding( AnnotationLabel.LabelPlacementProperty, ( BindingBase ) binding1 );
            Binding binding2 = new Binding("ContextMenu") { Source = (object) this, Mode = BindingMode.OneWay };
            annotationLabel.SetBinding( FrameworkElement.ContextMenuProperty, ( BindingBase ) binding2 );
            this.AnnotationLabels.Add( annotationLabel );
            return annotationLabel;
        }

        protected void TryPlaceAxisLabels( Point offset )
        {
            IAxis axis = this.GetUsedAxis();
            if ( axis == null || axis.ModifierAxisCanvas == null )
            {
                return;
            }

            this.AnnotationLabels.Where<AnnotationLabel>( ( Func<AnnotationLabel, bool> ) ( label =>
          {
              if ( label.IsAxisLabel )
              {
                  return label.ParentAnnotation != null;
              }

              return false;
          } ) ).ForEachDo<AnnotationLabel>( ( Action<AnnotationLabel> ) ( label => this.PlaceAxisLabel( axis, label, offset ) ) );
        }

        protected virtual void PlaceAxisLabel( IAxis axis, AnnotationLabel axisLabel, Point offset )
        {
            if ( axisLabel.Parent == null )
            {
                this.Attach( axisLabel );
                this.Refresh();
            }
            axis.SetHorizontalOffset( ( FrameworkElement ) axisLabel, offset );
            axis.SetVerticalOffset( ( FrameworkElement ) axisLabel, offset );
        }

        protected virtual void Detach( AnnotationLabel label )
        {
            IAnnotationCanvas modifierAxisCanvas;
            label.ParentAnnotation = null;
            Grid annotationRoot = this.AnnotationRoot as Grid;
            IAxis usedAxis = this.GetUsedAxis();
            if ( usedAxis != null )
            {
                modifierAxisCanvas = usedAxis.ModifierAxisCanvas;
            }
            else
            {
                modifierAxisCanvas = null;
            }
            object parent = modifierAxisCanvas;
            if ( parent == null )
            {
                parent = label.Parent as ModifierAxisCanvas;
            }

            annotationRoot.SafeRemoveChild( label );
            ( ( IAnnotationCanvas ) parent ).SafeRemoveChild( label );
        }

        protected virtual void ApplyPlacement( AnnotationLabel label, LabelPlacement placement )
        {
            bool flag1 = placement.IsTop();
            bool flag2 = placement.IsBottom();
            bool flag3 = placement.IsLeft();
            bool flag4 = placement.IsRight();
            if ( flag1 | flag2 )
            {
                label.SetValue( Grid.ColumnProperty, ( object ) 1 );
                label.SetValue( Grid.RowProperty, ( object ) ( flag1 ? 0 : 2 ) );
                label.HorizontalAlignment = HorizontalAlignment.Center;
                label.VerticalAlignment = flag1 ? VerticalAlignment.Bottom : VerticalAlignment.Top;
                if ( flag4 )
                {
                    label.HorizontalAlignment = HorizontalAlignment.Right;
                }

                if ( !flag3 )
                {
                    return;
                }

                label.HorizontalAlignment = HorizontalAlignment.Left;
            }
            else
            {
                if ( flag4 )
                {
                    label.SetValue( Grid.ColumnProperty, ( object ) 2 );
                }

                if ( flag3 )
                {
                    label.SetValue( Grid.ColumnProperty, ( object ) 0 );
                }

                label.SetValue( Grid.RowProperty, ( object ) 1 );
                label.VerticalAlignment = VerticalAlignment.Center;
            }
        }

        internal virtual LabelPlacement ResolveAutoPlacement()
        {
            return LabelPlacement.Top;
        }

        internal LabelPlacement GetLabelPlacement( AnnotationLabel label )
        {
            if ( label.LabelPlacement == LabelPlacement.Auto )
            {
                return this.ResolveAutoPlacement();
            }

            return label.LabelPlacement;
        }

        protected override Cursor GetSelectedCursor()
        {
            return Cursors.SizeNS;
        }

        public override bool IsPointWithinBounds( Point point )
        {
            Grid annotationRoot = this.AnnotationRoot as Grid;
            point = this.ParentSurface.ModifierSurface.TranslatePoint( point, ( IHitTestable ) this );
            return annotationRoot.IsPointWithinBounds( point );
        }

        private static void OnAnnotationLabelsChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ObservableCollection<AnnotationLabel> newValue = e.NewValue as ObservableCollection<AnnotationLabel>;
            ObservableCollection<AnnotationLabel> oldValue = e.OldValue as ObservableCollection<AnnotationLabel>;
            LineAnnotationWithLabelsBase annotationWithLabelsBase = (LineAnnotationWithLabelsBase) d;
            if ( newValue != null )
            {
                newValue.CollectionChanged += new NotifyCollectionChangedEventHandler( annotationWithLabelsBase.OnAnnotationLabelsCollectionChanged );
            }

            if ( oldValue != null )
            {
                oldValue.CollectionChanged -= new NotifyCollectionChangedEventHandler( annotationWithLabelsBase.OnAnnotationLabelsCollectionChanged );
            }

            annotationWithLabelsBase.OnAnnotationLabelsChanged( ( IList ) newValue, ( IList ) oldValue );
            annotationWithLabelsBase.Refresh();
        }

        private void OnAnnotationLabelsChanged( IList newItems, IList oldItems )
        {
            if ( newItems != null )
            {
                this.AttachLabels( newItems.OfType<AnnotationLabel>() );
            }

            if ( oldItems == null )
            {
                return;
            }

            this.DetachLabels( oldItems.OfType<AnnotationLabel>() );
        }

        private void OnAnnotationLabelsCollectionChanged( object sender, NotifyCollectionChangedEventArgs e )
        {
            this.OnAnnotationLabelsChanged( e.NewItems, e.OldItems );
        }

        private static void OnLabelTextFormattingChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( d as LineAnnotationWithLabelsBase )?.GetBindingExpression( LineAnnotationWithLabelsBase.FormattedLabelProperty )?.UpdateTarget();
        }

        private static void OnShowLabelChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            LineAnnotationWithLabelsBase annotationWithLabelsBase = d as LineAnnotationWithLabelsBase;
            if ( annotationWithLabelsBase == null )
            {
                return;
            }

            if ( annotationWithLabelsBase.ShowLabel && annotationWithLabelsBase.AnnotationLabels.Count == 0 )
            {
                AnnotationLabel annotationLabel = annotationWithLabelsBase.AddLabel();
                annotationWithLabelsBase.InvalidateLabel( annotationLabel );
            }
            else
            {
                if ( annotationWithLabelsBase.ShowLabel || annotationWithLabelsBase.AnnotationLabels.Count != 1 )
                {
                    return;
                }

                AnnotationLabel annotationLabel = annotationWithLabelsBase.AnnotationLabels[0];
                annotationWithLabelsBase.AnnotationLabels.Remove( annotationLabel );
            }
        }
    }
}
