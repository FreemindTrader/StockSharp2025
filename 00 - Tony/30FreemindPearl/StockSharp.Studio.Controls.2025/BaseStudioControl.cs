// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.BaseStudioControl
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

using DevExpress.Mvvm.POCO;
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Logging;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Studio.Controls.Commands;
using StockSharp.Studio.Core;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
                    //if ( ctrl == null || ctrl.IsDesignMode() )
                    //{
                    //    return;
                    //}

                    ctrl.RaisePropertyChanged( nameof( Title ) );
                    ctrl.RaiseChangedCommand();
                } ) );

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(nameof (Icon), typeof (ImageSource), typeof (BaseStudioControl), new PropertyMetadata((PropertyChangedCallback) null));
        private readonly List<Type> _registered = new List<Type>();
        private Action _loadedAction;

        public string Title
        {
            get
            {
                return ( string ) GetValue( TitleProperty );
            }
            set
            {
                SetValue( TitleProperty, value );
            }
        }
        public ImageSource Icon
        {
            get
            {
                return ( ImageSource ) this.GetValue( BaseStudioControl.IconProperty );
            }
            set
            {
                this.SetValue( BaseStudioControl.IconProperty,  value );
            }
        }

        public string Key { get; set; }

        public virtual bool SaveWithLayout { get; protected set; } = true;

        public virtual bool IsTitleEditable
        {
            get
            {
                return false;
            }
        }

        public virtual string DocUrl { get; }

        protected BaseStudioControl()
        {
            Type type = ((object) this).GetType();
            this.Key = type.CreateKey();
            this.Title = Ecng.ComponentModel.Extensions.GetDisplayName( ( ICustomAttributeProvider ) type, ( string ) null );
            Uri iconUrl = type.TryGetIconUrl();
            if ( iconUrl != null )
                this.Icon = iconUrl.Url2Img();
            this.DocUrl = ( ( DocAttribute ) AttributeHelper.GetAttribute<DocAttribute>( ( ICustomAttributeProvider ) type, true ) )?.DocUrl;
            if ( this.IsDesignMode() )
                return;
            LocalizedStrings.ActiveLanguageChanged += new Action( this.OnActiveLanguageChanged );
        }

        protected virtual void OnActiveLanguageChanged()
        {
            this.Title = Ecng.ComponentModel.Extensions.GetDisplayName( ( ICustomAttributeProvider ) (  this ).GetType(), ( string ) null );
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

        protected void Register<TCommand>( object listener, bool guiAsync, Action<TCommand> handler, Func<TCommand, bool> canExecute = null )
          where TCommand : IStudioCommand
        {
            BaseStudioControl.CommandService.Register<TCommand>( listener, guiAsync, handler, canExecute );
            this._registered.Add( typeof( TCommand ) );
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
            return CloseAction.Close;
        }

        public virtual void Dispose( CloseReason reason )
        {
            this.Dispose();
        }

        public void Dispose()
        {
            LocalizedStrings.ActiveLanguageChanged -= new Action( this.OnActiveLanguageChanged );
            IStudioCommandService commandService = BaseStudioControl.CommandService;
            foreach ( Type commandType in ( Type [ ] ) CollectionHelper.CopyAndClear<Type>(  this._registered ) )
                commandService.UnRegister( commandType,  this );
            GC.SuppressFinalize(  this );
        }

        public virtual void FirstTimeInit()
        {
        }

        public virtual void Load( SettingsStorage storage )
        {
            if ( this.IsTitleEditable )
                this.Title = ( string ) storage.GetValue<string>( "Title",  this.Title );
            this.Key = ( string ) storage.GetValue<string>( "Key",  null );
        }

        public virtual void Save( SettingsStorage storage )
        {
            if ( this.IsTitleEditable )
                storage.SetValue<string>( "Title",  this.Title );
            storage.SetValue<string>( "Key",  this.Key );
        }

        protected void RaiseChangedCommand()
        {
            new ControlChangedCommand( ( IStudioControl ) this ).Process(  this, false );
        }

        public virtual void SendCommand( IStudioCommand command )
        {
            command.Process(  this, false );
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged( string name )
        {
            //Ecng.Xaml.GuiDispatcher.GlobalDispatcher.AddAction( ( Action ) ( () =>
            //{
            //    PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            //    if ( propertyChanged == null )
            //        return;
            //    propertyChanged(  this, new PropertyChangedEventArgs( name ) );
            //} ) );
        }
    }
}
