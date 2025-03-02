// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.Level1Panel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

using DevExpress.Xpf.Bars;
using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Windows;

namespace StockSharp.Studio.Controls
{
    [Display( Description = "Level1Panel", Name = "Level1", ResourceType = typeof( LocalizedStrings ) )]
    [VectorIcon( "Table" )]
    [Doc( "topics/terminal/user_interface/components/level_1.html" )]
    public partial class Level1Panel : BaseSubscriptionStudioControl, IBarManagerControl
    {
        
        BarManager IBarManagerControl.Bar
        {
            get
            {
                return this.BarManager;
            }
        }

        protected override StockSharp.Messages.DataType DataType { get; } = StockSharp.Messages.DataType.Level1;

        public Level1Panel()
        {
            this.InitializeComponent();
            this.TryHideBar();
            this.Level1Grid.LayoutChanged += RaiseChangedCommand;
            this.Register<ResetedCommand>(  this, false, ( Action<ResetedCommand> ) ( cmd => ( ( ICollection<Level1ChangeMessage> ) this.Level1Grid.Messages ).Clear() ), ( Func<ResetedCommand, bool> ) null );
            this.Register<EntityCommand<Level1ChangeMessage>>(  this, false, new Action<EntityCommand<Level1ChangeMessage>>(  this.Level1Grid.Messages.AddEntity<Level1ChangeMessage> ), ( Func<EntityCommand<Level1ChangeMessage>, bool> ) null );
        }

        public override void Dispose( CloseReason reason )
        {
            this.Level1Grid.LayoutChanged -= RaiseChangedCommand;
            base.Dispose( reason );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue<SettingsStorage>( "Level1Grid",  PersistableHelper.Save( ( IPersistable ) this.Level1Grid ) );
            storage.SetValue<string>( "BarManager",  this.BarManager.SaveDevExpressControl() );
            this.SaveSubscriptions( storage );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            PersistableHelper.LoadIfNotNull( ( IPersistable ) this.Level1Grid, storage, "Level1Grid" );
            string settings = (string) storage.GetValue<string>("BarManager",  null);
            if ( settings != null )
                this.BarManager.LoadDevExpressControl( settings );
            this.LoadSubscriptions( storage );
        }

        private void Filter_OnClick( object sender, RoutedEventArgs e )
        {
            this.FilterConfig();
        }

        
    }
}
