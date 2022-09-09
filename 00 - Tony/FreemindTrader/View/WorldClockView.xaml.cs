using System;
using System.Linq;
using System.Windows.Controls;

namespace FreemindTrader
{
    /// <summary>
    /// Interaction logic for WorldClockControl.xaml
    /// </summary>
    public partial class WorldClockView : UserControl
    {
        public WorldClockView()
        {
            InitializeComponent();

            DataContext = new WorldClockViewModel();
        }
    }
}
