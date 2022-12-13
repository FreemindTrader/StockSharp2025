#region S# License
/******************************************************************************************
NOTICE!!!  This program and source code is owned and licensed by
StockSharp, LLC, www.stocksharp.com
Viewing or use of this code requires your acceptance of the license
agreement found at https://github.com/StockSharp/StockSharp/blob/master/LICENSE
Removal of this comment is a violation of the license agreement.

Project: SampleHistoryTesting.SampleHistoryTestingPublic
File: MainWindow.xaml.cs
Created: 2015, 11, 11, 2:32 PM

Copyright 2010 by StockSharp, LLC
*******************************************************************************************/
#endregion S# License
namespace _01_NewHistoryEmulationGithub
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Collections.Generic;

    using Ecng.Xaml;
    using Ecng.Common;
    using Ecng.Collections;

    using StockSharp.Algo;
    using StockSharp.Algo.Candles;
    using StockSharp.Algo.Commissions;
    using StockSharp.Algo.Storages;
    using StockSharp.Algo.Testing;
    using StockSharp.BusinessEntities;
    using StockSharp.Logging;
    using StockSharp.Messages;
    using StockSharp.Charting;
    using StockSharp.Xaml.Charting;
    using StockSharp.Localization;
    using StockSharp.Configuration;

    public partial class MainWindow
    {
        private void StopBtnClick( object sender, RoutedEventArgs e )
        {
            _connector.Disconnect();
        }

        private void PauseBtnClick( object sender, RoutedEventArgs e )
        {
            _connector.Suspend();            
        }

        private void ClearChart( IChart chart, EquityCurveChart equity, EquityCurveChart position )
        {
            chart.ClearAreas();
            equity.Clear();
            position.Clear();
        }

        private void SetIsEnabled( bool canStart, bool canSuspend, bool canStop )
        {
            this.GuiAsync( () =>
            {
                StopBtn.IsEnabled = canStop;
                StartBtn.IsEnabled = canStart;
                PauseBtn.IsEnabled = canSuspend;
            } );
        }

        private void SetIsChartEnabled( IChart chart, bool started )
        {
            this.GuiAsync( () => chart.IsAutoRange = started );
        }
    }
}