using Ecng.Common;
using SciChart.Charting.Visuals.Annotations;
using StockSharp.Charting;
using System;

namespace StockSharp.Xaml.Charting
{
    public class UltraChartCustomAnnotation : CustomAnnotation
    {
        private readonly string _text;
        private readonly IChartElement _element;

        public UltraChartCustomAnnotation( )
        {
        }

        public UltraChartCustomAnnotation( string text, IChartElement chartElement )
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

        public IChartElement Element
        {
            get
            {
                return _element;
            }
        }
    }
}
