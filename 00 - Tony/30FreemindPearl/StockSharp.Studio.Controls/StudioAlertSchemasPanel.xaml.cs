// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.StudioAlertSchemasPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D457EB08-5750-4F4B-A104-96BE70F84CCF
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Controls.dll

using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Alerts;
using StockSharp.Localization;
using System;
using System.Runtime.InteropServices;
using System.Windows.Markup;

namespace StockSharp.Studio.Controls
{
    [DisplayNameLoc( "Alerts" )]
    [DescriptionLoc( "AlertsSettings", false )]
    [Guid( "5802FEE4-F58F-46A0-B844-C081022C03F6" )]
    [VectorIcon( "Bell" )]
    [Doc( "topics/Designer_notification_Setting.html" )]
    public partial class StudioAlertSchemasPanel : BaseStudioControl, IComponentConnector
    {

        public StudioAlertSchemasPanel()
        {
            this.InitializeComponent();
            this.SchemasPanel.ProcessingService = AlertServicesRegistry.ProcessingService;
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue<SettingsStorage>( "SchemasPanel", this.SchemasPanel.Save() );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.SchemasPanel.Load( storage.GetValue<SettingsStorage>( "SchemasPanel", ( SettingsStorage )null ) );
        }


    }
}
