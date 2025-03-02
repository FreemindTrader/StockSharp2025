using SciChart.Charting.Common;
using fx.Charting.Definitions;
using System;
using System.Windows;
using System.Windows.Controls;

namespace fx.Charting
{
    public partial class ultrachartlegend : ItemsControl
    {
        public ultrachartlegend( )
        {
            InitializeComponent( );
        }

        private void OnSizeChanged( object sender, SizeChangedEventArgs e )
        {
            var mySender = ( ( ( FrameworkElement )sender ).DataContext as ChildVM );

            mySender.Do( vm => vm.Parent.UpdateMinFieldWidth( ( ( FrameworkElement )sender ).ActualWidth ) );
        }        
    }
}
