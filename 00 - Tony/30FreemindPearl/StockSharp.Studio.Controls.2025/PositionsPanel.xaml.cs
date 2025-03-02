// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.PositionsPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Grid;
using Ecng.ComponentModel;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Studio.Core;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml;
using StockSharp.Xaml.GridControl;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;

namespace StockSharp.Studio.Controls
{
    [Display( Description = "PositionsPanel", Name = "Positions", ResourceType = typeof( LocalizedStrings ) )]
    [Guid( "D2E3A2B3-3C34-4973-A973-28D0722B1005" )]
    [VectorIcon( "Money" )]
    public partial class PositionsPanel : BaseStudioControl
    {
        public static readonly RoutedCommand ClosePositionCommand = new RoutedCommand();
        public static readonly RoutedCommand RevertPositionCommand = new RoutedCommand();
        private readonly SubscriptionManager _subscriptionManager;
        
        public PositionsPanel()
        {
            this.InitializeComponent();
            this._subscriptionManager = new SubscriptionManager( ( IStudioControl ) this );
            if ( this.IsDesignMode() )
                return;
            this.PortfolioGrid.LayoutChanged += RaiseChangedCommand;
            this.PortfolioGrid.SelectionChanged += ( GridSelectionChangedEventHandler ) ( ( s, e ) => this.SelectedPosition.SendSelect<StockSharp.BusinessEntities.Position>(  this, this.CanEditPosition ) );
            this.GotFocus += ( RoutedEventHandler ) ( ( s, e ) => this.SelectedPosition.SendSelect<StockSharp.BusinessEntities.Position>(  this, this.CanEditPosition ) );
            this.PortfolioGrid.ItemDoubleClick += ( Action<object, ItemDoubleClickEventArgs> ) ( ( sender, e ) =>
            {
                if ( this.SelectedPosition == null )
                    return;
                new PositionEditCommand( this.SelectedPosition ).Process(  this, false );
            } );
            this.Register<EntityCommand<StockSharp.BusinessEntities.Position>>(  this, false, new Action<EntityCommand<StockSharp.BusinessEntities.Position>>( (  this.PortfolioGrid.Positions ).TryAddEntity<StockSharp.BusinessEntities.Position> ), ( Func<EntityCommand<StockSharp.BusinessEntities.Position>, bool> ) null );
            this.Register<ResetedCommand>(  this, false, ( Action<ResetedCommand> ) ( cmd => ( ( ICollection<StockSharp.BusinessEntities.Position> ) this.PortfolioGrid.Positions ).Clear() ), ( Func<ResetedCommand, bool> ) null );
            this.WhenLoaded( ( Action ) ( () => this._subscriptionManager.CreateSubscription( StockSharp.Messages.DataType.PositionChanges, ( Action<Subscription> ) null ) ) );
            this.PortfolioGrid.PopupMenu.Items.Add( ( IBarItem ) new BarItemSeparator() );
            CommonBarItemCollection items1 = this.PortfolioGrid.PopupMenu.Items;
            BarButtonItem barButtonItem1 = new BarButtonItem();
            barButtonItem1.Glyph = ThemedIconsExtension.GetImage( "Remove2" );
            barButtonItem1.Content =  LocalizedStrings.ClosePosition;
            barButtonItem1.Command = ( ICommand ) PositionsPanel.ClosePositionCommand;
            barButtonItem1.CommandTarget = ( IInputElement ) this;
            items1.Add( ( IBarItem ) barButtonItem1 );
            CommonBarItemCollection items2 = this.PortfolioGrid.PopupMenu.Items;
            BarButtonItem barButtonItem2 = new BarButtonItem();
            barButtonItem2.Glyph = ThemedIconsExtension.GetImage( "Refresh" );
            barButtonItem2.Content =  LocalizedStrings.RevertPosition;
            barButtonItem2.Command = ( ICommand ) PositionsPanel.RevertPositionCommand;
            barButtonItem2.CommandTarget = ( IInputElement ) this;
            items2.Add( ( IBarItem ) barButtonItem2 );
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
            settings.SetValue<SettingsStorage>( "PortfolioGrid",  PersistableHelper.Save( ( IPersistable ) this.PortfolioGrid ) );
        }

        public override void Load( SettingsStorage settings )
        {
            PersistableHelper.LoadIfNotNull( ( IPersistable ) this.PortfolioGrid, settings, "PortfolioGrid" );
        }

        public override void Dispose( CloseReason reason )
        {
            this.PortfolioGrid.LayoutChanged -= RaiseChangedCommand;
            this._subscriptionManager.Dispose();
            base.Dispose( reason );
        }

        private void ExecutedClosePositionCommand( object sender, ExecutedRoutedEventArgs e )
        {
            new StockSharp.Studio.Core.Commands.ClosePositionCommand( this.SelectedPosition ).Process(  this, false );
        }

        private void CanExecuteClosePositionCommand( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = this.SelectedPosition.CanCloseOrRevert();
        }

        private void ExecutedRevertPositionCommand( object sender, ExecutedRoutedEventArgs e )
        {
            new StockSharp.Studio.Core.Commands.RevertPositionCommand( this.SelectedPosition ).Process(  this, false );
        }

        private void CanExecuteRevertPositionCommand( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = this.SelectedPosition.CanCloseOrRevert();
        }

        
    }
}
