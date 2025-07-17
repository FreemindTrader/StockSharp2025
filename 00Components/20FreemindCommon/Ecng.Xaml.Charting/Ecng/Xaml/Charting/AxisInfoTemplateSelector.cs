// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.AxisInfoTemplateSelector
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System.Windows;
namespace fx.Xaml.Charting
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
