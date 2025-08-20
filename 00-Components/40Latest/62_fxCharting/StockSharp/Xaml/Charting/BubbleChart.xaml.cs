using Ecng.Serialization;
using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.Numerics;
using SciChart.Charting.Visuals.RenderableSeries;
using SciChart.Data.Numerics;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
namespace StockSharp.Xaml.Charting
{
    public partial class BubbleChart : UserControl, IComponentConnector, IPersistable, IThemeableChart
    {
        private readonly ChartViewModel _viewModel = new ChartViewModel();
        private readonly XyzDataSeries<DateTime, double, double> _xyzDataSeries;

        static BubbleChart( )
        {
            LicenseManager.CreateInstance( );
        }

        public BubbleChart( )
        {
            InitializeComponent( );
            Ultrachart.DataContext = _viewModel;
            XyzDataSeries<DateTime, double, double> xyzDataSeries = new XyzDataSeries<DateTime, double, double>( );
            xyzDataSeries.AcceptsUnsortedData = true;
            _xyzDataSeries = xyzDataSeries;
            
            
            var fast            = new FastBubbleRenderableSeries( );
            fast.ResamplingMode = ResamplingMode.Auto;
            fast.BubbleColor    = Colors.Chocolate;
            fast.ZScaleFactor   = 0.1;
            fast.AutoZRange     = true;
            fast.DataSeries     = _xyzDataSeries;

            Ultrachart.RenderableSeries.Add( fast );
        }

        public string ChartTheme
        {
            get
            {
                return _viewModel.SelectedTheme;
            }
            set
            {
                _viewModel.SelectedTheme = value;
            }
        }

        public void Append( DateTime myTime, Decimal valueOne, Decimal valueTwo )
        {
            _xyzDataSeries.Append( myTime, ( double )valueOne, ( double )valueTwo );
        }

        public void Update( DateTime myTime, Decimal valueOne, Decimal valueTwo )
        {
            _xyzDataSeries.Append( myTime, ( double )valueOne, ( double )valueTwo );
        }

        public void Clear( )
        {
            _xyzDataSeries.Clear( );
        }

        public void Load( SettingsStorage storage )
        {
            ChartTheme = storage.GetValue( "ChartTheme", ChartTheme );
        }

        public void Save( SettingsStorage storage )
        {
            storage.SetValue( "ChartTheme", ChartTheme );
        }
    }
}
