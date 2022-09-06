
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
            this.InitializeComponent();
            IStudioCommandService commandService = BaseStudioControl.CommandService;
            commandService.Register<BindCommand>( ( object )this, true, ( Action<BindCommand> )( cmd =>
                {
                    if ( !cmd.CheckControl( ( IStudioControl )this ) )
                        return;
                    this.StatisticsGrid.StatisticManager = cmd.Source.StatisticManager;
                } ), ( Func<BindCommand, bool> )null );
            commandService.Register<ResetedCommand>( ( object )this, true, ( Action<ResetedCommand> )( cmd => this.StatisticsGrid.Reset() ), ( Func<ResetedCommand, bool> )null );
            this.WhenLoaded( ( Action )( () => new RequestBindSource( ( IStudioControl )this ).SyncProcess( ( object )this ) ) );
        }

        public override void Dispose()
        {
            IStudioCommandService commandService = BaseStudioControl.CommandService;
            commandService.UnRegister<BindCommand>( ( object )this );
            commandService.UnRegister<ResetedCommand>( ( object )this );
            base.Dispose();
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue<SettingsStorage>( "StatisticsGrid", this.StatisticsGrid.Save() );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            SettingsStorage storage1 = storage.GetValue<SettingsStorage>( "StatisticsGrid", ( SettingsStorage )null );
            if ( storage1 == null )
                return;
            this.StatisticsGrid.Load( storage1 );
        }


    }
}
