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
    public partial class HistogramChart : UserControl, IComponentConnector, IPersistable, IThemeableChart
    {
        private readonly ChartViewModel _viewModel = new ChartViewModel();
        private readonly XyDataSeries<double, double> _xyDataSeries;

        static HistogramChart( )
        {
            LicenseManager.CreateInstance( );
        }

        public HistogramChart( )
        {
            InitializeComponent( );
            Ultrachart.DataContext = _viewModel;

            var xyDataSeries                 = new XyDataSeries<double, double>( );
            xyDataSeries.AcceptsUnsortedData = true;
            _xyDataSeries                    = xyDataSeries;
            
            var fastCol                      = new FastColumnRenderableSeries( );
            fastCol.ResamplingMode           = ResamplingMode.None;
            fastCol.DataPointWidth           = 1.0;
            fastCol.Stroke                   = Colors.Chocolate;
            fastCol.DataSeries               = _xyDataSeries;
            Ultrachart.RenderableSeries.Add( fastCol );
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

        public void Append( Decimal valueOne, Decimal valueTwo )
        {
            _xyDataSeries.Append( ( double )valueOne, ( double )valueTwo );
        }

        public void Update( Decimal valueOne, Decimal valueTwo )
        {
            _xyDataSeries.Append( ( double )valueOne, ( double )valueTwo );
        }

        public void Clear( )
        {
            _xyDataSeries.Clear( );
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
