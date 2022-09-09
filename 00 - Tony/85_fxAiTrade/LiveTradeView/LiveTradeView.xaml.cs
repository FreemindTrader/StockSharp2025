using FreemindAITrade.ViewModels;
using StockSharp.BusinessEntities;
using System;
using System.Linq;
using System.Windows.Controls;

namespace FreemindAITrade.View
{
    public partial class LiveTradeView : UserControl
    {
        public LiveTradeView()
        {
            InitializeComponent();
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

        private void QuickOrderPanel_SettingsChanged()
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
