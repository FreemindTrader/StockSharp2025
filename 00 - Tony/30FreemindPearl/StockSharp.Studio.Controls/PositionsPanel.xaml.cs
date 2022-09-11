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
            InitializeComponent();
            _subscriptionManager = new SubscriptionManager( this );
            if ( this.IsDesignMode() )
                return;
            PortfolioGrid.LayoutChanged += RaiseChangedCommand;
            PortfolioGrid.SelectionChanged += ( s, e ) => new SelectCommand<Position>( SelectedPosition, CanEditPosition ).Process( this, false );
            GotFocus += ( s, e ) => new SelectCommand<Position>( SelectedPosition, CanEditPosition ).Process( this, false );
            PortfolioGrid.ItemDoubleClick += ( sender, e ) =>
              {
                  if ( SelectedPosition == null )
                      return;
                  new PositionEditCommand( SelectedPosition ).Process( this, false );
              };
            IStudioCommandService commandService = CommandService;
            commandService.Register( this, false, new Action<EntityCommand<Position>>( ( PortfolioGrid.Positions ).TryAddEntities ), null );
            commandService.Register<ResetedCommand>( this, false, cmd => PortfolioGrid.Positions.Clear(), null );
            WhenLoaded( () => _subscriptionManager.CreateSubscription( DataType.PositionChanges, null ) );
            PortfolioGrid.PopupMenu.Items.Add( new BarItemSeparator() );
            CommonBarItemCollection items1 = PortfolioGrid.PopupMenu.Items;
            BarButtonItem barButtonItem1 = new BarButtonItem();
            barButtonItem1.Glyph = ThemedIconsExtension.GetImage( "Remove2" );
            barButtonItem1.Content = LocalizedStrings.XamlStr173;
            barButtonItem1.Command = ClosePositionCommand;
            barButtonItem1.CommandTarget = this;
            items1.Add( barButtonItem1 );
            CommonBarItemCollection items2 = PortfolioGrid.PopupMenu.Items;
            BarButtonItem barButtonItem2 = new BarButtonItem();
            barButtonItem2.Glyph = ThemedIconsExtension.GetImage( "Refresh" );
            barButtonItem2.Content = LocalizedStrings.RevertPosition;
            barButtonItem2.Command = RevertPositionCommand;
            barButtonItem2.CommandTarget = this;
            items2.Add( barButtonItem2 );
        }

        private bool CanEditPosition { get; set; }

        private Position SelectedPosition
        {
            get
            {
                return PortfolioGrid?.SelectedPosition;
            }
        }

        public override void Save( SettingsStorage settings )
        {
            settings.SetValue( "PortfolioGrid", PortfolioGrid.Save() );
        }

        public override void Load( SettingsStorage settings )
        {
            PortfolioGrid.Load( settings.GetValue<SettingsStorage>( "PortfolioGrid", null ) );
        }

        public override void Dispose()
        {
            IStudioCommandService commandService = CommandService;
            commandService.UnRegister<EntityCommand<Portfolio>>( this );
            commandService.UnRegister<EntityCommand<Position>>( this );
            commandService.UnRegister<ResetedCommand>( this );
            _subscriptionManager.Dispose();
            base.Dispose();
        }

        private void ExecutedClosePositionCommand( object sender, ExecutedRoutedEventArgs e )
        {
            new ClosePositionCommand( SelectedPosition ).Process( this, false );
        }

        private void CanExecuteClosePositionCommand( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = SelectedPosition.CanCloseOrRevert();
        }

        private void ExecutedRevertPositionCommand( object sender, ExecutedRoutedEventArgs e )
        {
            new RevertPositionCommand( SelectedPosition ).Process( this, false );
        }

        private void CanExecuteRevertPositionCommand( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = SelectedPosition.CanCloseOrRevert();
        }


    }
}
