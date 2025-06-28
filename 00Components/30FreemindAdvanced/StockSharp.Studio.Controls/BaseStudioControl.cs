// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.BaseStudioControl
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
using StockSharp.Studio.Controls.Commands;
using StockSharp.Studio.Core;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml;

#nullable disable
namespace StockSharp.Studio.Controls;

public abstract class BaseStudioControl :
  UserControl,
  IStudioControl,
  IPersistable,
  IDisposable,
  INotifyPropertyChanged
{
    private Action _loadedAction;
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof(Title), typeof(string), typeof(BaseStudioControl), new PropertyMetadata((object)string.Empty, (PropertyChangedCallback)((o, args) =>
    {
        if (!(o is BaseStudioControl baseStudioControl2) || baseStudioControl2.IsDesignMode())
            return;
        baseStudioControl2.RaisePropertyChanged(nameof(Title));
        baseStudioControl2.RaiseChangedCommand();
    })));
    public static readonly DependencyProperty IconProperty = DependencyProperty.Register(nameof(Icon), typeof(ImageSource), typeof(BaseStudioControl), new PropertyMetadata((PropertyChangedCallback)null));
    private readonly List<Type> _registered = new List<Type>();

    public string Title
    {
        get => (string)this.GetValue(BaseStudioControl.TitleProperty);
        set => this.SetValue(BaseStudioControl.TitleProperty, (object)value);
    }

    public ImageSource Icon
    {
        get => (ImageSource)this.GetValue(BaseStudioControl.IconProperty);
        set => this.SetValue(BaseStudioControl.IconProperty, (object)value);
    }

    public string Key { get; set; }

    public virtual bool SaveWithLayout { get; protected set; } = true;

    public virtual bool IsTitleEditable => false;

    public virtual string DocUrl { get; }

    protected BaseStudioControl()
    {
        Type type = ((object)this).GetType();
        this.Key = type.CreateKey();
        this.Title = Ecng.ComponentModel.Extensions.GetDisplayName((ICustomAttributeProvider)type, (string)null);
        Uri iconUrl = StockSharp.Messages.Extensions.TryGetIconUrl(type);
        if ((object)iconUrl != null)
            this.Icon = iconUrl.Url2Img();
        this.DocUrl = AttributeHelper.GetAttribute<DocAttribute>((ICustomAttributeProvider)type, true)?.DocUrl;
        if (this.IsDesignMode())
            return;
        LocalizedStrings.ActiveLanguageChanged += new Action(this.OnActiveLanguageChanged);
    }

    protected virtual void OnActiveLanguageChanged()
    {
        this.Title = Ecng.ComponentModel.Extensions.GetDisplayName((ICustomAttributeProvider)((object)this).GetType(), (string)null);
    }

    protected static IStudioCommandService CommandService => StudioServicesRegistry.CommandService;

    protected static ISecurityProvider SecurityProvider => ServicesRegistry.SecurityProvider;

    protected static Connector Connector => ServicesRegistry.Connector;

    protected static IMarketDataProvider MarketDataProvider => ServicesRegistry.MarketDataProvider;

    protected static PortfolioDataSource PortfolioDataSource
    {
        get => StudioServicesRegistry.PortfolioDataSource;
    }

    protected static LogManager LogManager => ServicesRegistry.LogManager;

    protected static IExchangeInfoProvider ExchangeInfoProvider
    {
        get => ServicesRegistry.ExchangeInfoProvider;
    }

    protected static IPortfolioMessageAdapterProvider PortfolioMessageAdapterProvider
    {
        get => ServicesRegistry.PortfolioAdapterProvider;
    }

    protected void Register<TCommand>(
      object listener,
      bool guiAsync,
      Action<TCommand> handler,
      Func<TCommand, bool> canExecute = null)
      where TCommand : IStudioCommand
    {
        BaseStudioControl.CommandService.Register<TCommand>(listener, guiAsync, handler, canExecute);
        this._registered.Add(typeof(TCommand));
    }

    protected void WhenLoaded(Action action)
    {
        this._loadedAction = action;
        this.Loaded += new RoutedEventHandler(this.OnLoaded);
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        this.Loaded -= new RoutedEventHandler(this.OnLoaded);
        Action loadedAction = this._loadedAction;
        if (loadedAction == null)
            return;
        loadedAction();
    }

    public virtual CloseAction CanClose(CloseReason reason) => CloseAction.Close;

    public virtual void Dispose(CloseReason reason) => this.Dispose();

    public void Dispose()
    {
        LocalizedStrings.ActiveLanguageChanged -= new Action(this.OnActiveLanguageChanged);
        IStudioCommandService commandService = BaseStudioControl.CommandService;
        foreach (Type commandType in CollectionHelper.CopyAndClear<Type>((ICollection<Type>)this._registered))
            commandService.UnRegister(commandType, (object)this);
        GC.SuppressFinalize((object)this);
    }

    public virtual void FirstTimeInit()
    {
    }

    public virtual void Load(SettingsStorage storage)
    {
        if (this.IsTitleEditable)
            this.Title = storage.GetValue<string>("Title", this.Title);
        this.Key = storage.GetValue<string>("Key", (string)null);
    }

    public virtual void Save(SettingsStorage storage)
    {
        if (this.IsTitleEditable)
            storage.SetValue<string>("Title", this.Title);
        storage.SetValue<string>("Key", this.Key);
    }

    protected void RaiseChangedCommand()
    {
        new ControlChangedCommand((IStudioControl)this).Process((object)this);
    }

    public virtual void SendCommand(IStudioCommand command) => command.Process((object)this);

    public event PropertyChangedEventHandler PropertyChanged;

    protected void RaisePropertyChanged(string name)
    {
        GuiDispatcher.GlobalDispatcher.AddAction((Action)(() =>
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if (propertyChanged == null)
                return;
            propertyChanged((object)this, new PropertyChangedEventArgs(name));
        }));
    }
}
