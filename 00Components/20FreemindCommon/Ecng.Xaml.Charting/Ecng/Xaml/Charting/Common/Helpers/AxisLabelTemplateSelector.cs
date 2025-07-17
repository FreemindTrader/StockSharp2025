using System.Windows;
using Ecng.Xaml.Charting.Visuals.Axes;

namespace Ecng.Xaml.Charting.Common.Helpers
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
