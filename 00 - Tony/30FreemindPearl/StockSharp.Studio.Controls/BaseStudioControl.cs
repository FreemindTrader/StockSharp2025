using Ecng.ComponentModel;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Logging;
using StockSharp.Studio.Core;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace StockSharp.Studio.Controls
{
    public abstract class BaseStudioControl : UserControl, IStudioControl, IPersistable, IDisposable, INotifyPropertyChanged
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            nameof( Title ),
            typeof( string ),
            typeof( BaseStudioControl ),
            new PropertyMetadata(
                string.Empty,
                ( o, args ) =>
                {
                    var ctrl = o as BaseStudioControl;
                    if ( ctrl == null || ctrl.IsDesignMode() )
                    {
                        return;
                    }

                    ctrl.RaisePropertyChanged( nameof( Title ) );
                    ctrl.RaiseChangedCommand();
                } ) );


        public static readonly DependencyProperty StudioContainerProperty = DependencyProperty.RegisterAttached( "StudioContainer", typeof( IStudioContainer ), typeof( BaseStudioControl ), new FrameworkPropertyMetadata( null, FrameworkPropertyMetadataOptions.Inherits ) );
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register( nameof( Icon ), typeof( Uri ), typeof( BaseStudioControl ), new PropertyMetadata( null ) );
        private Action _loadedAction;

        public string Title
        {
            get
            {
                return ( string )this.GetValue( TitleProperty );
            }
            set
            {
                this.SetValue( TitleProperty, value );
            }
        }

        public Uri Icon
        {
            get
            {
                return ( Uri )this.GetValue( IconProperty );
            }
            set
            {
                this.SetValue( IconProperty, value );
            }
        }

        public string Key { get; set; }

        public bool SaveWithLayout { get; protected set; }

        public bool IsTitleEditable { get; protected set; }

        public static void SetStudioContainer( UIElement element, IStudioContainer container )
        {
            element.SetValue( StudioContainerProperty, container );
        }

        public static IStudioContainer GetStudioContainer( UIElement element )
        {
            return ( IStudioContainer )element.GetValue( StudioContainerProperty );
        }

        protected BaseStudioControl()
        {
            Type type = this.GetType();
            this.Key = type.CreateKey();
            this.Title = type.GetDisplayName( null );
            this.Icon = type.GetIconUrl();
            this.SaveWithLayout = true;
        }

        protected static IStudioCommandService CommandService
        {
            get
            {
                return StudioServicesRegistry.CommandService;
            }
        }

        protected static ISecurityProvider SecurityProvider
        {
            get
            {
                return ServicesRegistry.SecurityProvider;
            }
        }

        protected static Connector Connector
        {
            get
            {
                return ServicesRegistry.Connector;
            }
        }

        protected static IMarketDataProvider MarketDataProvider
        {
            get
            {
                return ServicesRegistry.MarketDataProvider;
            }
        }

        protected static PortfolioDataSource PortfolioDataSource
        {
            get
            {
                return StudioServicesRegistry.PortfolioDataSource;
            }
        }

        protected static LogManager LogManager
        {
            get
            {
                return ServicesRegistry.LogManager;
            }
        }

        protected static IExchangeInfoProvider ExchangeInfoProvider
        {
            get
            {
                return ServicesRegistry.ExchangeInfoProvider;
            }
        }

        protected static IPortfolioMessageAdapterProvider PortfolioMessageAdapterProvider
        {
            get
            {
                return ServicesRegistry.PortfolioAdapterProvider;
            }
        }

        protected void WhenLoaded( Action action )
        {
            this._loadedAction = action;
            this.Loaded += new RoutedEventHandler( this.OnLoaded );
        }

        private void OnLoaded( object sender, RoutedEventArgs e )
        {
            this.Loaded -= new RoutedEventHandler( this.OnLoaded );
            Action loadedAction = this._loadedAction;
            if ( loadedAction == null )
                return;
            loadedAction();
        }

        public virtual CloseAction CanClose( CloseReason reason )
        {
            return GetStudioContainer( this )?.GetClosingBehavior( this, reason ) ?? CloseAction.Close;
        }

        public virtual void Dispose( CloseReason reason )
        {
            this.Dispose();
        }

        public virtual void Dispose()
        {
        }

        public virtual void FirstTimeInit()
        {
        }

        public virtual void Load( SettingsStorage storage )
        {
            if ( this.IsTitleEditable )
                this.Title = storage.GetValue<string>( "Title", this.Title );
            this.Key = storage.GetValue<string>( "Key", null );
        }

        public virtual void Save( SettingsStorage storage )
        {
            if ( this.IsTitleEditable )
                storage.SetValue<string>( "Title", this.Title );
            storage.SetValue<string>( "Key", this.Key );
        }

        protected void RaiseChangedCommand()
        {
            new ControlChangedCommand( this ).Process( this, false );
        }

        public virtual void SendCommand( IStudioCommand command )
        {
            command.Process( this, false );
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged( string name )
        {
            GuiDispatcher.GlobalDispatcher.AddAction( () =>
            {
                PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
                if ( propertyChanged == null )
                    return;
                propertyChanged( this, new PropertyChangedEventArgs( name ) );
            } );
        }
    }
}
