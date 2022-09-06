using Ecng.Common;
using SciChart.Charting.Visuals.Annotations;
using System;

namespace fx.Charting
{
    public class UltraChartCustomAnnotation : CustomAnnotation
    {
        private readonly string _text;
        private readonly IfxChartElement _element;

        public UltraChartCustomAnnotation( )
        {
        }

        public UltraChartCustomAnnotation( string text, IfxChartElement chartElement )
        {
            if( StringHelper.IsEmpty( text ) )
            {
                throw new ArgumentNullException( "text" );
            }

            if( chartElement == null )
            {
                throw new ArgumentNullException( "element" );
            }

            _element = chartElement;
            _text = text;
        }

        public string Text
        {
            get
            {
                return _text;
            }
        }

        public IfxChartElement Element
        {
            get
            {
                return _element;
            }
        }
    }
}
