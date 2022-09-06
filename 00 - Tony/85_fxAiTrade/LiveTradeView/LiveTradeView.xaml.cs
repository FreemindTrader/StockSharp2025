using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.PropertyGrid;
using Ecng.Backup;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Configuration;
using Ecng.Serialization;
using Ecng.Xaml;
using FreemindAITrade.ViewModels;
using MoreLinq;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Indicators;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Xaml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace FreemindAITrade.View
{    
    public partial class LiveTradeView : UserControl
    {        
        public LiveTradeView( )
        {
            InitializeComponent( );            
        }        

        /* -------------------------------------------------------------------------------------------------------------------------------------------
         * 
         *  Tony Reference : 
         *  https://www.mobilemotion.eu/?p=1806
         * 
         *  For custom Control with custom event, since we can’t use the Binding markup extension on events 
         *  In those cases, we typically create an event handler method in code-behind, and execute a Viewmodel Command from there:
         * 
         * ------------------------------------------------------------------------------------------------------------------------------------------- */

        private void QuickOrderPanel_SettingsChanged( )
        {
            var vm = DataContext as LiveTradeViewModel;
            if ( vm != null )
            {                
                vm.QuickOrderSettingChangedCommand.Execute( null );                
            }            
        }

        private void QuickOrderPanel_RegisterOrder( Order myOrder )
        {
            var vm = DataContext as LiveTradeViewModel;
            if ( vm != null )
            {
                vm.OnQuickPanelRegisterOrder( myOrder );
            }
        }
    }
}
