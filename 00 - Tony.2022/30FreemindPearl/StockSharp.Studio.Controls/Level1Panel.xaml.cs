// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.Level1Panel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D457EB08-5750-4F4B-A104-96BE70F84CCF
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Controls.dll

using DevExpress.Xpf.Bars;
using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Alerts;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;

namespace StockSharp.Studio.Controls
{
    [DisplayNameLoc( "Level1" )]
    [DescriptionLoc( "Level1Panel", false )]
    [VectorIcon( "Table" )]
    [Doc( "topics/Terminal_level1.html" )]
    public partial class Level1Panel : BaseSubscriptionStudioControl, IComponentConnector
    {        
        protected override DataType DataType { get; } = DataType.Level1;

        public Level1Panel()
        {
            InitializeComponent();
            Level1Grid.LayoutChanged += new Action( RaiseChangedCommand );
            AlertBtn.SchemaChanged += new Action( RaiseChangedCommand );
            IStudioCommandService commandService = CommandService;
            commandService.Register(
                this,
                false,
                cmd => Level1Grid.Messages.Clear(),
                ( Func<ResetedCommand, bool> )null );
            commandService.Register(
                this,
                false,
                new Action<EntityCommand<Level1ChangeMessage>>( ( Level1Grid.Messages ).AddEntities ),
                null );
        }

        public override void Dispose( CloseReason reason )
        {
            IStudioCommandService commandService = CommandService;
            commandService.UnRegister<ResetedCommand>( this );
            commandService.UnRegister<EntityCommand<Level1ChangeMessage>>( this );
            if ( reason == CloseReason.CloseWindow )
                AlertBtn.Dispose();
            base.Dispose( reason );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "Level1Grid", Level1Grid.Save() );
            storage.SetValue( "AlertBtn", AlertBtn.Save() );
            storage.SetValue( "BarManager", BarManager.SaveDevExpressControl() );
            SaveSubscriptions( storage );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            Level1Grid.Load( storage.GetValue<SettingsStorage>( "Level1Grid", null ) );
            SettingsStorage storage1 = storage.GetValue<SettingsStorage>( "AlertBtn", null );
            if ( storage1 != null )
                AlertBtn.Load( storage1 );
            string settings = storage.GetValue<string>( "BarManager", null );
            if ( settings != null )
                BarManager.LoadDevExpressControl( settings );
            LoadSubscriptions( storage );
        }

        private void Filter_OnClick( object sender, RoutedEventArgs e )
        {
            FilterConfig();
        }        
    }
}
