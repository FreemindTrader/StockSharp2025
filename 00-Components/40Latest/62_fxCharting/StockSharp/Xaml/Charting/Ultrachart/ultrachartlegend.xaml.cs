using SciChart.Charting.Common;
using StockSharp.Xaml.Charting.Definitions;
using System;
using System.Windows;
using System.Windows.Controls;

namespace StockSharp.Xaml.Charting
{
    public partial class ultrachartlegend : ItemsControl
    {
        public ultrachartlegend( )
        {
            InitializeComponent( );
        }

        private void OnSizeChanged( object sender, SizeChangedEventArgs e )
        {
            //BUG
            throw new NotImplementedException();

            //var mySender = ( ( ( FrameworkElement )sender ).DataContext as ChartElementViewModel );

            //mySender.Do( vm => vm.Parent.UpdateMinFieldWidth( ( ( FrameworkElement )sender ).ActualWidth ) );
        }        
    }
}
