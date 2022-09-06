using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Docking;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Configuration;
using Ecng.Serialization;
using MoreLinq;
using StockSharp.Algo;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Candles.Compression;
using StockSharp.Algo.Indicators;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml.Charting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using fx.Definitions;
using fx.Common;
using Ecng.ComponentModel;
using fx.Database;
using fx.Database.Common.DataModel;
using fx.Database.ForexDatabarsDataModel;
using fx.Algorithm;

namespace FreemindAITrade.ViewModels
{
    class SecuritiesWindowViewModel : DevExpress.Mvvm.ViewModelBase, ISupportParameter
    {
        IEnumerable< Security > _selectedSecurites = new SynchronizedList< Security >( );
        public IEnumerable<Security> SelectedSecurities
        {
            get
            {
                return _selectedSecurites;
            }            
        }

    }
}
