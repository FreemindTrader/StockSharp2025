using System;
using StockSharp.Charting;

namespace StockSharp.Xaml.Charting
{
    /// <summary>
    /// Interaction logic for UltrachartBuymakerAnnotation.xaml
    /// </summary>
    public partial class UltrachartSellmarkerAnnotation : UltraChartCustomAnnotation
    {
        public UltrachartSellmarkerAnnotation( string text, IChartElement chartElement ) : base( text, chartElement )
        {
            InitializeComponent( );
        }
    }
}

