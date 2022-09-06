// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.PositionsPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D457EB08-5750-4F4B-A104-96BE70F84CCF
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Controls.dll

using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Grid;
using Ecng.ComponentModel;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Studio.Core;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml;
using StockSharp.Xaml.GridControl;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace StockSharp.Studio.Controls
{
    [DisplayNameLoc( "Str972" )]
    [DescriptionLoc( "Str3254", false )]
    [Guid( "D2E3A2B3-3C34-4973-A973-28D0722B1005" )]
    [VectorIcon( "Money" )]
    public partial class PositionsPanel : BaseStudioControl, IComponentConnector
    {
        public static RoutedCommand ClosePositionCommand = new RoutedCommand();
        public static RoutedCommand RevertPositionCommand = new RoutedCommand();
        private readonly SubscriptionManager _subscriptionManager;
        
        public PositionsPanel()
        {
            this.InitializeComponent();
            this._subscriptionManager = new SubscriptionManager( ( IStudioControl )this );
            if ( this.IsDesignMode() )
                return;
            this.PortfolioGrid.LayoutChanged += RaiseChangedCommand;
            this.PortfolioGrid.SelectionChanged += ( GridSelectionChangedEventHandler )( ( s, e ) => new SelectCommand<StockSharp.BusinessEntities.Position>( this.SelectedPosition, this.CanEditPosition ).Process( ( object )this, false ) );
            this.GotFocus += ( RoutedEventHandler )( ( s, e ) => new SelectCommand<StockSharp.BusinessEntities.Position>( this.SelectedPosition, this.CanEditPosition ).Process( ( object )this, false ) );
            this.PortfolioGrid.ItemDoubleClick += ( Action<object, ItemDoubleClickEventArgs> )( ( sender, e ) =>
              {
                  if ( this.SelectedPosition == null )
                      return;
                  new PositionEditCommand( this.SelectedPosition ).Process( ( object )this, false );
              } );
            IStudioCommandService commandService = BaseStudioControl.CommandService;
            commandService.Register<EntityCommand<StockSharp.BusinessEntities.Position>>( ( object )this, false, new Action<EntityCommand<StockSharp.BusinessEntities.Position>>( ( PortfolioGrid.Positions ).TryAddEntities<StockSharp.BusinessEntities.Position> ), ( Func<EntityCommand<StockSharp.BusinessEntities.Position>, bool> )null );
            commandService.Register<ResetedCommand>( ( object )this, false, ( Action<ResetedCommand> )( cmd => this.PortfolioGrid.Positions.Clear() ), ( Func<ResetedCommand, bool> )null );
            this.WhenLoaded( ( Action )( () => this._subscriptionManager.CreateSubscription( DataType.PositionChanges, ( Action<Subscription> )null ) ) );
            this.PortfolioGrid.PopupMenu.Items.Add( ( IBarItem )new BarItemSeparator() );
            CommonBarItemCollection items1 = this.PortfolioGrid.PopupMenu.Items;
            BarButtonItem barButtonItem1 = new BarButtonItem();
            barButtonItem1.Glyph = ThemedIconsExtension.GetImage( "Remove2" );
            barButtonItem1.Content = ( object )LocalizedStrings.XamlStr173;
            barButtonItem1.Command = ( ICommand )PositionsPanel.ClosePositionCommand;
            barButtonItem1.CommandTarget = ( IInputElement )this;
            items1.Add( ( IBarItem )barButtonItem1 );
            CommonBarItemCollection items2 = this.PortfolioGrid.PopupMenu.Items;
            BarButtonItem barButtonItem2 = new BarButtonItem();
            barButtonItem2.Glyph = ThemedIconsExtension.GetImage( "Refresh" );
            barButtonItem2.Content = ( object )LocalizedStrings.RevertPosition;
            barButtonItem2.Command = ( ICommand )PositionsPanel.RevertPositionCommand;
            barButtonItem2.CommandTarget = ( IInputElement )this;
            items2.Add( ( IBarItem )barButtonItem2 );
        }

        private bool CanEditPosition { get; set; }

        private StockSharp.BusinessEntities.Position SelectedPosition
        {
            get
            {
                return this.PortfolioGrid?.SelectedPosition;
            }
        }

        public override void Save( SettingsStorage settings )
        {
            settings.SetValue<SettingsStorage>( "PortfolioGrid", this.PortfolioGrid.Save() );
        }

        public override void Load( SettingsStorage settings )
        {
            this.PortfolioGrid.Load( settings.GetValue<SettingsStorage>( "PortfolioGrid", ( SettingsStorage )null ) );
        }

        public override void Dispose()
        {
            IStudioCommandService commandService = BaseStudioControl.CommandService;
            commandService.UnRegister<EntityCommand<Portfolio>>( ( object )this );
            commandService.UnRegister<EntityCommand<StockSharp.BusinessEntities.Position>>( ( object )this );
            commandService.UnRegister<ResetedCommand>( ( object )this );
            this._subscriptionManager.Dispose();
            base.Dispose();
        }

        private void ExecutedClosePositionCommand( object sender, ExecutedRoutedEventArgs e )
        {
            new StockSharp.Studio.Core.Commands.ClosePositionCommand( this.SelectedPosition ).Process( ( object )this, false );
        }

        private void CanExecuteClosePositionCommand( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = this.SelectedPosition.CanCloseOrRevert();
        }

        private void ExecutedRevertPositionCommand( object sender, ExecutedRoutedEventArgs e )
        {
            new StockSharp.Studio.Core.Commands.RevertPositionCommand( this.SelectedPosition ).Process( ( object )this, false );
        }

        private void CanExecuteRevertPositionCommand( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = this.SelectedPosition.CanCloseOrRevert();
        }


    }
}
