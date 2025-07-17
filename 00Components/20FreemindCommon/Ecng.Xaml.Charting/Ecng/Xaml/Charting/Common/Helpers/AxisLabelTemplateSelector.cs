using System.Windows;
namespace fx.Xaml.Charting
{
    internal class AxisLabelTemplateSelector : DataTemplateSelector
    {
        private DataTemplate _numericAxisLabelTemplate;

        public DataTemplate NumericAxisLabelTemplate
        {
            get
            {
                return this._numericAxisLabelTemplate;
            }
            set
            {
                this._numericAxisLabelTemplate = value;
                base.UpdateControlTemplate();
            }
        }

        public AxisLabelTemplateSelector()
        {
        }

        public override DataTemplate SelectTemplate( object item, DependencyObject container )
        {
            if ( item as ITickLabelViewModel is NumericTickLabelViewModel )
            {
                return this.NumericAxisLabelTemplate;
            }
            return base.SelectTemplate( item, container );
        }
    }
}
