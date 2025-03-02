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
                return ( string )GetValue( TitleProperty );
            }
            set
            {
                SetValue( TitleProperty, value );
            }
        }

        public Uri Icon
        {
            get
            {
                return ( Uri )GetValue( IconProperty );
            }
            set
            {
                SetValue( IconProperty, value );
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
            Type type = GetType();
            Key = type.CreateKey();
            Title = type.GetDisplayName( null );
            Icon = type.GetIconUrl();
            SaveWithLayout = true;
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
            _loadedAction = action;
            Loaded += new RoutedEventHandler( OnLoaded );
        }

        private void OnLoaded( object sender, RoutedEventArgs e )
        {
            Loaded -= new RoutedEventHandler( OnLoaded );
            Action loadedAction = _loadedAction;
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
            Dispose();
        }

        public virtual void Dispose()
        {
        }

        public virtual void FirstTimeInit()
        {
        }

        public virtual void Load( SettingsStorage storage )
        {
            if ( IsTitleEditable )
                Title = storage.GetValue( "Title", Title );
            Key = storage.GetValue<string>( "Key", null );
        }

        public virtual void Save( SettingsStorage storage )
        {
            if ( IsTitleEditable )
                storage.SetValue( "Title", Title );
            storage.SetValue( "Key", Key );
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
                PropertyChangedEventHandler propertyChanged = PropertyChanged;
                if ( propertyChanged == null )
                    return;
                propertyChanged( this, new PropertyChangedEventArgs( name ) );
            } );
        }
    }
}
