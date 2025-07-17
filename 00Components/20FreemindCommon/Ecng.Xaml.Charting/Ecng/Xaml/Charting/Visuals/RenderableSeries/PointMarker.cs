// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Visuals.RenderableSeries.PointMarker
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Ecng.Xaml.Charting.Visuals.RenderableSeries
{
    public class PointMarker : TemplatableControl
    {
        public static readonly DependencyProperty DeferredContentProperty = DependencyProperty.Register(nameof (DeferredContent), typeof (DataTemplate), typeof (PointMarker), new PropertyMetadata(new PropertyChangedCallback(PointMarker.OnDeferredContentChanged)));

        public DataTemplate DeferredContent
        {
            get
            {
                return ( DataTemplate ) this.GetValue( PointMarker.DeferredContentProperty );
            }
            set
            {
                this.SetValue( PointMarker.DeferredContentProperty, ( object ) value );
            }
        }

        public override void OnApplyTemplate()
        {
            if ( this.DeferredContent == null )
                return;
            this.Content = ( object ) this.DeferredContent.LoadContent();
        }

        public static PointMarker CreateFromTemplate( ControlTemplate template, object dataContext = null )
        {
            PointMarker pointMarker1 = (PointMarker) null;
            if ( template != null )
            {
                PointMarker pointMarker2 = new PointMarker();
                pointMarker2.Template = template;
                pointMarker1 = pointMarker2;
                if ( dataContext != null )
                    pointMarker1.DataContext = dataContext;
                pointMarker1.ApplyTemplate();
            }
            return pointMarker1;
        }

        private static void OnDeferredContentChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            PointMarker pointMarker = d as PointMarker;
            if ( pointMarker == null || pointMarker.DeferredContent == null )
                return;
            FrameworkElement frameworkElement = pointMarker.DeferredContent.LoadContent() as FrameworkElement;
            if ( frameworkElement == null )
                return;
            Binding binding = new Binding("DataContext") { Source = (object) pointMarker };
            frameworkElement.SetBinding( FrameworkElement.DataContextProperty, ( BindingBase ) binding );
            pointMarker.Content = ( object ) frameworkElement;
        }
    }
}
