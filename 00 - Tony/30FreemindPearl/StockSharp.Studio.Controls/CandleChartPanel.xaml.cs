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
            InitializeComponent();
            CommandService.Register<BindCommand>( this, true, cmd =>
                {
                    if ( !cmd.CheckControl( this ) )
                        return;
                    ChartPanel.IsInteracted = cmd.IsInteractive;
                    cmd.Source.SetChart( ChartPanel );
                }, null );
            ChartPanel.MinimumRange = 200;
            WhenLoaded( () => new RequestBindSource( this ).SyncProcess( this ) );
        }

        public override void Dispose()
        {
            CommandService.UnRegister<BindCommand>( this );
            base.Dispose();
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            ChartPanel.Load( storage.GetValue<SettingsStorage>( "ChartPanel", null ) );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "ChartPanel", ChartPanel.Save() );
        }        
    }
}
