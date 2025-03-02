// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.RiskPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Algo;
using StockSharp.Localization;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace StockSharp.Studio.Controls
{
    [Display( Description = "RiskSettings", Name = "Risks", ResourceType = typeof( LocalizedStrings ) )]
    [Guid( "CBF49990-CC8B-49BE-8FD8-713CE6F98297" )]
    [VectorIcon( "Dices" )]
    [Doc( "topics/designer/user_interface/risk_management.html" )]
    public partial class RiskPanel : BaseStudioControl
    {
        public RiskPanel()
        {
            this.InitializeComponent();
            this.RiskPanelCtrl.Rules = ServicesRegistry.RiskManager.Rules;
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue<SettingsStorage>( "RiskPanelCtrl",  PersistableHelper.Save( ( IPersistable ) this.RiskPanelCtrl ) );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            PersistableHelper.LoadIfNotNull( ( IPersistable ) this.RiskPanelCtrl, storage, "RiskPanelCtrl" );
        }

        private void RiskPanelCtrl_OnLayoutChanged()
        {
            this.RaiseChangedCommand();
        }        
    }
}
