// Decompiled with JetBrains decompiler
// Type: StockSharp.Terminal.Controls.WorkAreaControl
// Assembly: Terminal, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 33913FF8-0D5D-4EE9-A5BB-58AEFF5B15A5
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\Terminal.dll

using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Docking;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Logging;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo.Strategies;
using StockSharp.Configuration;
using StockSharp.Localization;
using StockSharp.Studio.Controls;
using StockSharp.Studio.Controls.Commands;
using StockSharp.Studio.Core.Commands;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;

namespace StockSharp.Terminal.Controls
{
    [Display( Description = "WorkArea", Name = "Area", ResourceType = typeof( LocalizedStrings ) )]
    [VectorIcon( "Table" )]
    [Doc( "topics/terminal/user_interface/workspace.html" )]
    public partial class WorkAreaControl : BaseStudioControl, IStudioCommandScope, IControlsGalleryControl, IComponentConnector
    {
        private readonly Strategy _state = new Strategy();
        private readonly LayoutManager _layoutManager;
        
        object IControlsGalleryControl.State
        {
            get
            {
                return ( object ) this._state;
            }
        }

        public WorkAreaControl()
        {
            if ( !this.IsDesignMode() )
                this.InitializeCommands();
            this.InitializeComponent();
            this._layoutManager = new LayoutManager( this.DockManager, ( DocumentGroup ) null );
            this._layoutManager.Changed += RaiseChangedCommand;
            if ( this.IsDesignMode() )
                return;
            foreach ( ControlType component in ControlType.GetComponents() )
            {
                BarButtonItem toolControl = component.CreateToolControl( this, (Action<Type>) (type =>
        {
            OpenWindowCommand command = new OpenWindowCommand(type, true);
            command.SyncProcess( this._state);
            ValueTuple<IStudioControl, bool> result = command.Result;
            IStudioControl studioControl = result.Item1;
            bool flag = result.Item2;
            if (!(studioControl != null & flag))
                return;
            studioControl.FirstTimeInit();
        }));
                toolControl.GlyphSize = GlyphSize.Small;
                this.ToolBar.Items.Add( ( IBarItem ) toolControl );
            }
        }

        public override bool IsTitleEditable
        {
            get
            {
                return true;
            }
        }

        private void InitializeCommands()
        {
            this.Register<ControlChangedCommand>( ( object ) this, false, ( Action<ControlChangedCommand> ) ( cmd =>
            {
                this._layoutManager.MarkControlChanged( cmd.Control );
                this.RaiseChangedCommand();
            } ), ( Func<ControlChangedCommand, bool> ) null );
            this.Register<RequestBindSource>( ( object ) this, true, ( Action<RequestBindSource> ) ( cmd => this.RaiseBindCommand( cmd.Control ) ), ( Func<RequestBindSource, bool> ) null );
            this.Register<OpenWindowCommand>( ( object ) this, true, ( Action<OpenWindowCommand> ) ( cmd => cmd.Result = cmd.IsToolWindow ? this._layoutManager.OpenToolWindow( cmd.CtrlType ) : this._layoutManager.OpenDocumentWindow( cmd.CtrlType ) ), ( Func<OpenWindowCommand, bool> ) null );
            this.Register<LoadLayoutCommand>( ( object ) this, true, ( Action<LoadLayoutCommand> ) ( cmd =>
            {
                try
                {
                    this._layoutManager.DeserializeFromString<LayoutManager>( cmd.Layout );
                    foreach ( IStudioControl dockingControl in this._layoutManager.DockingControls )
                        dockingControl.FirstTimeInit();
                }
                catch ( Exception ex )
                {
                    LoggingHelper.LogError( ex, ( string ) null );
                    int num = (int) new MessageBoxBuilder().Text(LocalizedStrings.LoadCompositionError).Error().Owner((DependencyObject) this).Show();
                }
            } ), ( Func<LoadLayoutCommand, bool> ) null );
            this.Register<StockSharp.Studio.Core.Commands.SaveLayoutCommand>( ( object ) this, true, ( Action<StockSharp.Studio.Core.Commands.SaveLayoutCommand> ) ( cmd => cmd.Layout = this._layoutManager.SerializeToString<LayoutManager>( true ) ), ( Func<StockSharp.Studio.Core.Commands.SaveLayoutCommand, bool> ) null );
            this.Register<ReRegisterOrderCommand>( ( object ) this, true, ( Action<ReRegisterOrderCommand> ) ( cmd => cmd.Process( ( object ) this, false ) ), ( Func<ReRegisterOrderCommand, bool> ) null );
            BaseStudioControl.CommandService.Bind( ( object ) this._state, ( IStudioCommandScope ) this );
            this.RaiseBindCommand( ( IStudioControl ) null );
        }

        private void RaiseBindCommand( IStudioControl control = null )
        {
            new BindCommand( ( IStrategyBinder ) new WorkAreaControl.StrategyBinder( this._state ), control )
            {
                IsInteractive = true
            }.SyncProcess( ( object ) this._state );
        }

        public override void Dispose( CloseReason reason )
        {
            ( ( Disposable ) this._layoutManager ).Dispose();
            base.Dispose( reason );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            PersistableHelper.Load( ( IPersistable ) this._layoutManager, storage, "LayoutManager" );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            SettingsStorage settingsStorage = PersistableHelper.Save((IPersistable) this._layoutManager);
            storage.SetValue<SettingsStorage>( "LayoutManager",  settingsStorage );
        }

        bool IStudioCommandScope.UseParentScope
        {
            get
            {
                return false;
            }
        }

        bool IStudioCommandScope.RouteToGlobalScope
        {
            get
            {
                return true;
            }
        }

        
        private class StrategyBinder : IStrategyBinder
        {
            public readonly Strategy _state;

            public StrategyBinder( Strategy state )
            {
                Strategy strategy = state;
                if ( strategy == null )
                    throw new ArgumentNullException( nameof( state ) );
                this._state = strategy;
            }

            string IStrategyBinder.BinderTitle
            {
                get
                {
                    return ( string ) null;
                }
            }

            object IStrategyBinder.Settings
            {
                get
                {
                    return ( object ) null;
                }
            }

            event Action<object> IStrategyBinder.SettingsChanged
            {
                add
                {
                }
                remove
                {
                }
            }

            void IStrategyBinder.Init( Action<Strategy> bind, Action<Strategy> unbind )
            {
                if ( bind == null )
                    throw new ArgumentNullException( nameof( bind ) );
                if ( unbind == null )
                    throw new ArgumentNullException( nameof( unbind ) );
                bind( this._state );
            }
        }
    }
}
