using Ecng.Common;
using Ecng.Serialization;
using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.Model.DataSeries.Heatmap2DArrayDataSeries;
using SciChart.Charting.Visuals.Axes;
using SciChart.Charting.Visuals.Axes.LabelProviders;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace fx.Charting
{
    /// <summary>
    /// Interaction logic for HeatmapChart.xaml
    /// </summary>
    public partial class HeatmapChart : UserControl
    {
        private readonly ChartViewModel _parentViewModel = new ChartViewModel( );
        public static DependencyProperty ShowColorMapProperty = DependencyProperty.Register( nameof( ShowColorMap ), typeof( bool ), typeof( HeatmapChart ) );
        private string[ ] _xTitles;
        private string[ ] _yTitles;

        public HeatmapChart( )
        {
            InitializeComponent( );

            Ultrachart.DataContext = _parentViewModel;
            YAxis.LabelProvider = new HeatmapLabelProvider( new Func<string[ ]>( ( ) => _yTitles ) );
            XAxis.LabelProvider = new HeatmapLabelProvider( new Func<string[ ]>( ( ) => _xTitles ) );
            ShowColorMap = true;
        }

        static HeatmapChart( )
        {
            LicenseManager.CreateInstance( );
        }

        public string ChartTheme
        {
            get
            {
                return _parentViewModel.SelectedTheme;
            }
            set
            {
                _parentViewModel.SelectedTheme = value;
            }
        }

        public bool ShowColorMap
        {
            get
            {
                return ( bool )GetValue( ShowColorMapProperty );
            }
            set
            {
                SetValue( ShowColorMapProperty, value );
            }
        }

        public void Draw( string[ ] xTitles, string[ ] yTitles, double[ , ] dataArray )
        {
            if ( dataArray == null )
            {
                throw new ArgumentNullException( nameof( dataArray ) );
            }

            string[ ] strArray1 = xTitles;
            if ( strArray1 == null )
            {
                throw new ArgumentNullException( nameof( xTitles ) );
            }

            _xTitles = strArray1;
            string[ ] strArray2 = yTitles;
            if ( strArray2 == null )
            {
                throw new ArgumentNullException( nameof( yTitles ) );
            }

            _yTitles = strArray2;

            double min = 0.0;
            double max = 0.0;
            foreach ( double data in dataArray )
            {
                if ( min > data )
                {
                    min = data;
                }

                if ( max < data )
                {
                    max = data;
                }
            }

            if ( Math.Abs( min - max ) < double.Epsilon )
            {
                max += 1.0;
            }


            //HeatmapSeries.Minimum = min;
            //HeatmapSeries.Maximum = max;
            // HeatmapSeries.DataSeries = ( IDataSeries )new UniformHeatmapDataSeries<int, int, double>( dataArray, i => i, x => x );
        }

        public void Clear( )
        {
            Draw( Array.Empty<string>( ), Array.Empty<string>( ), new double[ 0, 0 ] );
        }

        public void Load( SettingsStorage storage )
        {
            ChartTheme = storage.GetValue( "ChartTheme", ChartTheme );
            ShowColorMap = storage.GetValue( "ShowColorMap", ShowColorMap );
        }

        public void Save( SettingsStorage storage )
        {
            storage.SetValue( "ChartTheme", ChartTheme );
            storage.SetValue( "ShowColorMap", ShowColorMap );
        }

        private sealed class HeatmapLabelProvider : LabelProviderBase
        {
            private readonly Func<string[ ]> func_0;

            public HeatmapLabelProvider( Func<string[ ]> func_1 )
            {

                Func<string[ ]> func = func_1;
                if ( func == null )
                {
                    throw new ArgumentNullException( "getTitles" );
                }

                func_0 = func;
            }

            public override string FormatLabel( IComparable dataValue )
            {
                string[ ] strArray = func_0( );
                if ( strArray == null )
                {
                    return dataValue.ToString( );
                }

                int index =   dataValue .To<int>( );
                if ( index < 0 || index >= strArray.Length )
                {
                    return string.Empty;
                }

                return strArray[ index ];
            }

            public override string FormatCursorLabel( IComparable dataValue )
            {
                return FormatLabel( dataValue );
            }
        }
    }
}
