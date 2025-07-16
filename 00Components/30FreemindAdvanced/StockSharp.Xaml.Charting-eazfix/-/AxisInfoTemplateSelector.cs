using StockSharp.Xaml.Charting.Visuals.RenderableSeries;
using System.Windows;

namespace StockSharp.Xaml.Charting
{
    public class AxisInfoTemplateSelector : DataTemplateSelector
    {
        public static readonly DependencyProperty YAxisDataTemplateProperty = DependencyProperty.Register(nameof (YAxisDataTemplate), typeof (DataTemplate), typeof (AxisInfoTemplateSelector), new PropertyMetadata(new PropertyChangedCallback(DataTemplateSelector.OnDefautlTemplateDependencyPropertyChanged)));
        public static readonly DependencyProperty XAxisDataTemplateProperty = DependencyProperty.Register(nameof (XAxisDataTemplate), typeof (DataTemplate), typeof (AxisInfoTemplateSelector), new PropertyMetadata(new PropertyChangedCallback(DataTemplateSelector.OnDefautlTemplateDependencyPropertyChanged)));

        public DataTemplate YAxisDataTemplate
        {
            get
            {
                return ( DataTemplate ) this.GetValue( AxisInfoTemplateSelector.YAxisDataTemplateProperty );
            }
            set
            {
                this.SetValue( AxisInfoTemplateSelector.YAxisDataTemplateProperty, ( object ) value );
            }
        }

        public DataTemplate XAxisDataTemplate
        {
            get
            {
                return ( DataTemplate ) this.GetValue( AxisInfoTemplateSelector.XAxisDataTemplateProperty );
            }
            set
            {
                this.SetValue( AxisInfoTemplateSelector.XAxisDataTemplateProperty, ( object ) value );
            }
        }

        public override DataTemplate SelectTemplate( object item, DependencyObject container )
        {
            AxisInfo axisInfo = item as AxisInfo;
            DataTemplate dataTemplate = base.SelectTemplate(item, container);
            if ( axisInfo != null )
                dataTemplate = axisInfo.IsXAxis ? this.XAxisDataTemplate : this.YAxisDataTemplate;
            return dataTemplate;
        }
    }
}
