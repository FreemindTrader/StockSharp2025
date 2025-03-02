using Ecng.Collections;
using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Algo;
using StockSharp.Algo.Risk;
using StockSharp.Localization;
using System;
using System.Runtime.InteropServices;
using System.Windows.Markup;

namespace StockSharp.Studio.Controls
{
    [DisplayNameLoc( "XamlStr613" )]
    [DescriptionLoc( "RiskSettings", false )]
    [Guid( "CBF49990-CC8B-49BE-8FD8-713CE6F98297" )]
    [VectorIcon( "Dices" )]
    [Doc( "topics/Designer_Risk_Rule.html" )]
    public partial class RiskPanel : BaseStudioControl, IComponentConnector
    {

        public RiskPanel()
        {
            InitializeComponent();
            RiskPanelCtrl.Rules = ServicesRegistry.RiskManager.Rules;
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "RiskPanelCtrl", RiskPanelCtrl.Save() );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            RiskPanelCtrl.Load( storage.GetValue<SettingsStorage>( "RiskPanelCtrl", null ) );
        }

        private void RiskPanelCtrl_OnLayoutChanged()
        {
            RaiseChangedCommand();
        }


    }
}
