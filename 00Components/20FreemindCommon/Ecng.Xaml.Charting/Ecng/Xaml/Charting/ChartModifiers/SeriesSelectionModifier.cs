// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartModifiers.SeriesSelectionModifier
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using StockSharp.Xaml.Charting.Common.Extensions;
using StockSharp.Xaml.Charting.Visuals.RenderableSeries;

namespace StockSharp.Xaml.Charting.ChartModifiers
{
    public class SeriesSelectionModifier : InspectSeriesModifierBase
    {
        public static readonly DependencyProperty SelectedSeriesStyleProperty = DependencyProperty.Register(nameof (SelectedSeriesStyle), typeof (Style), typeof (SeriesSelectionModifier), new PropertyMetadata(new PropertyChangedCallback(SeriesSelectionModifier.OnSelectedSeriesStyleChanged)));
        private bool _isGroupSelection;

        public event EventHandler<EventArgs> SelectionChanged;

        public SeriesSelectionModifier()
        {
            SetCurrentValue( InspectSeriesModifierBase.UseInterpolationProperty, ( object ) true );
            SetCurrentValue( ChartModifierBase.ExecuteOnProperty, ( object ) ExecuteOn.MouseLeftButton );
        }

        public Style SelectedSeriesStyle
        {
            get
            {
                return ( Style ) GetValue( SeriesSelectionModifier.SelectedSeriesStyleProperty );
            }
            set
            {
                SetValue( SeriesSelectionModifier.SelectedSeriesStyleProperty, ( object ) value );
            }
        }

        public override void OnAttached()
        {
            base.OnAttached();
            ApplySelection();
        }

        private void ApplySelection()
        {
            if ( ParentSurface == null )
            {
                return;
            }

            ParentSurface.SelectedRenderableSeries.ForEachDo<IRenderableSeries>( new Action<IRenderableSeries>( TrySetStyle ) );
        }

        protected override void OnSelectedSeriesChanged( IEnumerable<IRenderableSeries> oldSeries, IEnumerable<IRenderableSeries> newSeries )
        {
            base.OnSelectedSeriesChanged( oldSeries, newSeries );
            if ( newSeries == null )
            {
                return;
            }

            ApplySelection();
        }

        protected override void ClearAll()
        {
        }

        public override void OnParentSurfaceRendered( UltrachartRenderedMessage e )
        {
        }

        public override void OnModifierMouseUp( ModifierMouseArgs e )
        {
            base.OnModifierMouseUp( e );
            _isGroupSelection = e.Modifier == MouseModifier.Ctrl;
            HandleMouseEvent( e );
        }

        protected override void HandleSlaveMouseEvent( Point mousePoint )
        {
            HandleMasterMouseEvent( mousePoint );
        }

        protected override void HandleMasterMouseEvent( Point mousePoint )
        {
            bool flag = !ParentSurface.SelectedRenderableSeries.IsNullOrEmpty<IRenderableSeries>();
            SeriesInfo[] array = GetSeriesInfoAt(mousePoint).ToArray<SeriesInfo>();
            if ( ( ( IEnumerable<SeriesInfo> ) array ).Any<SeriesInfo>() )
            {
                IRenderableSeries renderableSeries = ((IEnumerable<SeriesInfo>) array).First<SeriesInfo>().RenderableSeries;
                if ( _isGroupSelection )
                {
                    PerformSelection( renderableSeries );
                }
                else
                {
                    DeselectAllBut( renderableSeries );
                }

                OnSelectionChanged();
            }
            else
            {
                if ( !flag )
                {
                    return;
                }

                DeselectAll();
                OnSelectionChanged();
            }
        }

        protected virtual void DeselectAllBut( IRenderableSeries series )
        {
            bool flag;
            flag = ( !series.IsSelected ? true : ParentSurface.SelectedRenderableSeries.Count > 1 );
            DeselectAll();
            if ( flag )
            {
                PerformSelection( series );
            }
        }

        protected virtual void PerformSelection( IRenderableSeries series )
        {
            series.IsSelected = !series.IsSelected;
            if ( !series.IsSelected )
            {
                return;
            }

            TrySetStyle( series );
        }

        protected virtual void DeselectAll()
        {
            if ( ParentSurface.SelectedRenderableSeries.Count == 0 )
            {
                return;
            }

            for ( int index = ParentSurface.SelectedRenderableSeries.Count - 1 ; index >= 0 ; --index )
            {
                ParentSurface.SelectedRenderableSeries[ index ].IsSelected = false;
            }
        }

        protected virtual void TrySetStyle( IRenderableSeries series )
        {
            if ( SelectedSeriesStyle == null )
            {
                return;
            }

            bool flag = SelectedSeriesStyle.TargetType.IsAssignableFrom(series.GetType());
            if ( !( series.SelectedSeriesStyle == null & flag ) )
            {
                return;
            }

            series.SelectedSeriesStyle = SelectedSeriesStyle;
        }

        private void OnSelectionChanged()
        {
            // ISSUE: reference to a compiler-generated field
            EventHandler<EventArgs> selectionChanged = SelectionChanged;
            if ( selectionChanged == null )
            {
                return;
            }

            selectionChanged( ( object ) this, EventArgs.Empty );
        }

        private static void OnSelectedSeriesStyleChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( ( SeriesSelectionModifier ) d ).ApplySelection();
        }
    }
}
