// Decompiled with JetBrains decompiler
// Type: FreemindTrader.Controls.WorkAreaControl
// Assembly: Terminal, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C7FF2C7B-469F-4E71-BC76-9E79C0E574D9
// Assembly location: T:\00-StockSharp\Terminal\Terminal.dll

using Ecng.ComponentModel;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo.Strategies;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Studio.Controls;
using StockSharp.Studio.Core;
using StockSharp.Studio.Core.Commands;
using System;
using System.Windows.Markup;

namespace FreemindTrader.Controls
{
    [DisplayNameLoc( "Area" )]
    [DescriptionLoc( "WorkArea", true )]
    [VectorIcon( "CascadeWindows" )]
    [Doc( "topics/Terminal_Workspace.html" )]
    public partial class WorkAreaControl : BaseStudioControl, IStudioCommandScope, IControlsGalleryControl, IComponentConnector
    {
        private readonly Strategy _state = new Strategy();
        private readonly LayoutManager _layoutManager;

        public object State
        {
            get
            {
                return _state;
            }
        }

        public WorkAreaControl()
        {
            if ( !this.IsDesignMode() )
                InitializeCommands();
            IsTitleEditable = true;
            InitializeComponent();
            _layoutManager = new LayoutManager( DockManager, null );
            _layoutManager.Changed += RaiseChangedCommand;
        }

        private void InitializeCommands()
        {
            IStudioCommandService commandService = CommandService;
            commandService.Register( this, false, cmd =>
            {
                _layoutManager.MarkControlChanged( cmd.Control );
                RaiseChangedCommand();
            }, ( Func<ControlChangedCommand, bool> )null );
            commandService.Register( this, true, cmd => RaiseBindCommand( cmd.Control ), ( Func<RequestBindSource, bool> )null );
            commandService.Register( this, true, cmd => cmd.Result = cmd.IsToolWindow ? _layoutManager.OpenToolWindow( cmd.CtrlType, null, true ) : _layoutManager.OpenDocumentWindow( cmd.CtrlType, null, true ), ( Func<OpenWindowCommand, bool> )null );
            commandService.Register( this, true, cmd =>
            {
                try
                {
                    _layoutManager.LoadFromString<JsonSerializer<SettingsStorage>>( cmd.Layout );
                }
                catch ( Exception ex )
                {
                    ex.LogError( null );
                    int num = ( int )new MessageBoxBuilder().Text( LocalizedStrings.LoadCompositionError ).Error().Owner( this ).Show();
                }
            }, ( Func<LoadLayoutCommand, bool> )null );
            commandService.Register( this, true, cmd => cmd.Layout = _layoutManager.SaveToString<JsonSerializer<SettingsStorage>>(), ( Func<StockSharp.Studio.Core.Commands.SaveLayoutCommand, bool> )null );
            commandService.Register( this, true, cmd => cmd.Process( this, false ), ( Func<ReRegisterOrderCommand, bool> )null );
            commandService.Bind( _state, this );
            RaiseBindCommand( null );
        }

        private void RaiseBindCommand( IStudioControl control = null )
        {
            new BindCommand( _state, control )
            {
                IsInteractive = true
            }.SyncProcess( _state );
        }

        public override void Dispose()
        {
            _layoutManager.Dispose();
            IStudioCommandService commandService = CommandService;
            commandService.UnRegister<ControlChangedCommand>( this );
            commandService.UnRegister<RequestBindSource>( this );
            commandService.UnRegister<OpenWindowCommand>( this );
            commandService.UnRegister<LoadLayoutCommand>( this );
            commandService.UnRegister<StockSharp.Studio.Core.Commands.SaveLayoutCommand>( this );
            base.Dispose();
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            _layoutManager.Load( storage.GetValue( "LayoutManager", ( SettingsStorage )null ) );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            SettingsStorage settingsStorage = _layoutManager.Save();
            storage.SetValue( "LayoutManager", settingsStorage );
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


    }
}
