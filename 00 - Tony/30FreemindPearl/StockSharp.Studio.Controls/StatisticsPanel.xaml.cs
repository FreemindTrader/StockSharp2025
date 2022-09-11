
using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Localization;
using StockSharp.Studio.Core;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml;
using System;
using System.Windows.Markup;

namespace StockSharp.Studio.Controls
{
    [DisplayNameLoc( "Str436" )]
    [DescriptionLoc( "Str3259", false )]
    [VectorIcon( "Clipboard2" )]
    [Doc( "topics/Designer_Statistics.html" )]
    public partial class StatisticsPanel : BaseStudioControl, IComponentConnector
    {

        public StatisticsPanel()
        {
            InitializeComponent();
            IStudioCommandService commandService = CommandService;
            commandService.Register<BindCommand>( this, true, cmd =>
                {
                    if ( !cmd.CheckControl( this ) )
                        return;
                    StatisticsGrid.StatisticManager = cmd.Source.StatisticManager;
                }, null );
            commandService.Register<ResetedCommand>( this, true, cmd => StatisticsGrid.Reset(), null );
            WhenLoaded( () => new RequestBindSource( this ).SyncProcess( this ) );
        }

        public override void Dispose()
        {
            IStudioCommandService commandService = CommandService;
            commandService.UnRegister<BindCommand>( this );
            commandService.UnRegister<ResetedCommand>( this );
            base.Dispose();
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "StatisticsGrid", StatisticsGrid.Save() );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            SettingsStorage storage1 = storage.GetValue<SettingsStorage>( "StatisticsGrid", null );
            if ( storage1 == null )
                return;
            StatisticsGrid.Load( storage1 );
        }


    }
}
