using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Charting;
using StockSharp.Localization;
using StockSharp.Studio.Core;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml.Charting;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;

namespace StockSharp.Studio.Controls
{
    [DisplayNameLoc( "Str3200" )]
    [DescriptionLoc( "Str3201", false )]
    [VectorIcon( "CandleStick" )]
    [Doc( "topics/Designer_Chart.html" )]
    public partial class CandleChartPanel : BaseStudioControl, IComponentConnector
    {                
        public CandleChartPanel()
        {
            this.InitializeComponent();
            BaseStudioControl.CommandService.Register<BindCommand>( ( object )this, true, ( Action<BindCommand> )( cmd =>
                {
                    if ( !cmd.CheckControl( ( IStudioControl )this ) )
                        return;
                    this.ChartPanel.IsInteracted = cmd.IsInteractive;
                    cmd.Source.SetChart( ( IChart )this.ChartPanel );
                } ), ( Func<BindCommand, bool> )null );
            this.ChartPanel.MinimumRange = 200;
            this.WhenLoaded( ( Action )( () => new RequestBindSource( ( IStudioControl )this ).SyncProcess( ( object )this ) ) );
        }

        public override void Dispose()
        {
            BaseStudioControl.CommandService.UnRegister<BindCommand>( ( object )this );
            base.Dispose();
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.ChartPanel.Load( storage.GetValue<SettingsStorage>( "ChartPanel", ( SettingsStorage )null ) );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue<SettingsStorage>( "ChartPanel", this.ChartPanel.Save() );
        }        
    }
}
