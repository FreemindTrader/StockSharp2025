using System.Windows;

namespace fx.Xaml.Charting
{
    public class AxisTitleTemplateSelector : DataTemplateSelector
    {
        private DataTemplate _stringTitleTemplate;

        public DataTemplate StringTitleTemplate
        {
            get
            {
                return _stringTitleTemplate;
            }
            set
            {
                _stringTitleTemplate = value;
                base.UpdateControlTemplate();
            }
        }

        public AxisTitleTemplateSelector()
        {
        }

        public override DataTemplate SelectTemplate( object item, DependencyObject container )
        {
            if ( item is string )
            {
                return StringTitleTemplate;
            }
            return base.SelectTemplate( item, container );
        }
    }
}
