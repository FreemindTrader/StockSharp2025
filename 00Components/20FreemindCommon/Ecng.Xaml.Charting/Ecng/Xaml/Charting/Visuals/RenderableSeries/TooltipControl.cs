// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.RenderableSeries.TooltipControl
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Windows;

namespace StockSharp.Xaml.Charting.Visuals.RenderableSeries
{
    public class TooltipControl : TemplatableControl
    {
        public static readonly DependencyProperty SelectorProperty = DependencyProperty.Register(nameof (Selector), typeof (IDataTemplateSelector), typeof (TooltipControl), new PropertyMetadata((object) null, new PropertyChangedCallback(TooltipControl.OnSelectorDependencyPropertyChanged)));
        public static readonly DependencyProperty SelectorContextProperty = DependencyProperty.Register(nameof (SelectorContext), typeof (object), typeof (TooltipControl), new PropertyMetadata(new PropertyChangedCallback(TooltipControl.OnSelectorContextDependencyPropertyChanged)));

        public IDataTemplateSelector Selector
        {
            get
            {
                return ( IDataTemplateSelector ) this.GetValue( TooltipControl.SelectorProperty );
            }
            set
            {
                this.SetValue( TooltipControl.SelectorProperty, ( object ) value );
            }
        }

        public object SelectorContext
        {
            get
            {
                return this.GetValue( TooltipControl.SelectorContextProperty );
            }
            set
            {
                this.SetValue( TooltipControl.SelectorContextProperty, value );
            }
        }

        public TooltipControl()
        {
            this.DefaultStyleKey = ( object ) typeof( TooltipControl );
        }

        private void UpdateContentTemplate( object context )
        {
            if ( this.Selector == null )
                return;
            this.ContentTemplate = this.Selector.SelectTemplate( context, ( DependencyObject ) this );
        }

        private static void OnSelectorDependencyPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            TooltipControl tooltipControl = d as TooltipControl;
            if ( tooltipControl == null )
                return;
            IDataTemplateSelector newValue = e.NewValue as IDataTemplateSelector;
            if ( newValue != null )
                newValue.DataTemplateChanged += new EventHandler( tooltipControl.UpdateContentTemplate );
            IDataTemplateSelector oldValue = e.OldValue as IDataTemplateSelector;
            if ( oldValue != null )
                oldValue.DataTemplateChanged -= new EventHandler( tooltipControl.UpdateContentTemplate );
            tooltipControl.UpdateContentTemplate( tooltipControl.SelectorContext );
        }

        private void UpdateContentTemplate( object sender, EventArgs args )
        {
            this.UpdateContentTemplate( this.SelectorContext );
        }

        private static void OnSelectorContextDependencyPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            TooltipControl tooltipControl = d as TooltipControl;
            tooltipControl?.UpdateContentTemplate( tooltipControl.SelectorContext );
        }
    }
}
