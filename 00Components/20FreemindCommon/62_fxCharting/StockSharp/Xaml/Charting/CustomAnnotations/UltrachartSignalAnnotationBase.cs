using Ecng.Collections;
using SciChart.Charting.Visuals.Annotations;
using System;
using System.Collections.Generic; using fx.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Ecng.Common;

namespace fx.Charting.CustomAnnotations
{
    internal class UltrachartSignalAnnotationBase : CustomAnnotation
    {
        public static readonly DependencyProperty UseAltIconProperty = DependencyProperty.Register(nameof(UseAltIcon), typeof (bool), typeof (UltrachartSignalAnnotationBase));

        public UltrachartSignalAnnotationBase( )
        {
        }

        public UltrachartSignalAnnotationBase( string text, IChartElement element, bool isError )
        {
            if ( StringHelper.IsEmpty( text ) )
            {
                throw new ArgumentNullException( nameof( text ) );
            }
                
            IChartElement chartElement = element;
            if ( chartElement == null )
                throw new ArgumentNullException( nameof( chartElement ) );
            Element = chartElement;
            Text = text;
            IsError = isError;
        }

        public bool UseAltIcon
        {
            get
            {
                return ( bool ) GetValue( UseAltIconProperty );
            }
            set
            {
                SetValue( UseAltIconProperty, value );
            }
        }

        public bool IsError { get; }

        public string Text { get; }

        public IChartElement Element { get; }
    }
}
