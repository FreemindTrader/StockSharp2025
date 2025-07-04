using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Localization;
using System;
using System.Collections.Generic; using fx.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Media;

namespace fx.Charting
{    
    public partial class CandlestickUI : ChartElement<CandlestickUI>, ICloneable<IChartElement>, ICloneable, INotifyPropertyChanging, INotifyPropertyChanged, IChartComponent, IDrawableChartElement, IChartElement
    {
        public void ShowLessWaves( )
        {
            _viewModel.ShowLessWaves( );
        }

        public void ShowMoreWaves( )
        {
            _viewModel.ShowMoreWaves( );
        }
    }
}

