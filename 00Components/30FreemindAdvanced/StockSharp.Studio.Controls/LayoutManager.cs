// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.LayoutManager
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using DevExpress.Xpf.Core.Serialization;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.Docking.Base;
using DevExpress.Xpf.Docking.VisualElements;
using DevExpress.Xpf.Layout.Core;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Logging;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Localization;
using StockSharp.Studio.Core;
using StockSharp.Xaml;

#nullable enable
namespace StockSharp.Studio.Controls;

public sealed class LayoutManager : BaseLogReceiver
{
    private readonly
#nullable disable
    DocumentGroup _documentGroup;
    private readonly Dictionary<string, LayoutPanel> _panels = new Dictionary<string, LayoutPanel>();
    private readonly HashSet<IStudioControl> _controls = new HashSet<IStudioControl>();
    private readonly SynchronizedDictionary<IStudioControl, SettingsStorage> _dockingControlSettings = new SynchronizedDictionary<IStudioControl, SettingsStorage>();
    private readonly CachedSynchronizedSet<IStudioControl> _changedControls = new CachedSynchronizedSet<IStudioControl>();
    private bool _supressChanges;
    private bool _isLayoutChanged;
    private string _layout;
    private IStudioControl _loadingCtrl;

    public DockLayoutManager DockingManager { get; }

    public IEnumerable<IStudioControl> DockingControls
    {
        get
        {
            HashSet<IStudioControl> controls = this._controls;
            int index = 0;
            IStudioControl[] items = new IStudioControl[controls.Count];
            foreach (IStudioControl studioControl in controls)
            {
                items[index] = studioControl;
                ++index;
            }

            return (IEnumerable<IStudioControl>)new ReadOnlyCollection<IStudioControl>(items);
        }
    }

    public event Action Changed;

    public event Action LayoutChanged;

    public event Action<IStudioControl> ControlAdded;

    public event Action<IStudioControl> ControlChanged;

    public event Action<IStudioControl> ControlRemoved;

    public LayoutManager(DockLayoutManager dockingManager, DocumentGroup documentGroup = null)
    {
        this._documentGroup = documentGroup;
        this.DockingManager = dockingManager ?? throw new ArgumentNullException(nameof(dockingManager));
        DXSerializer.AddAllowPropertyHandler((DependencyObject)this.DockingManager, (AllowPropertyEventHandler)((s, e) =>
        {
            if (e.DependencyProperty != BaseLayoutItem.CaptionProperty)
                return;
            e.Allow = false;
        }));
        this.DockingManager.DockItemClosing += new DockItemCancelEventHandler(this.OnDockingManagerDockItemClosing);
        this.DockingManager.DockItemClosed += new DockItemClosedEventHandler(this.OnDockingManagerDockItemClosed);
        this.DockingManager.DockItemHidden += new DockItemEventHandler(this.DockingManager_DockItemHidden);
        this.DockingManager.DockItemRestored += new DockItemEventHandler(this.DockingManager_DockItemRestored);
        this.DockingManager.DockItemExpanded += new DockItemExpandedEventHandler(this.DockingManager_DockItemExpanded);
        this.DockingManager.DockItemActivated += new DockItemActivatedEventHandler(this.DockingManager_DockItemActivated);
        this.DockingManager.DockItemEndDocking += new DockItemDockingEventHandler(this.DockingManager_DockItemEndDocking);
        this.DockingManager.LayoutItemSizeChanged += new LayoutItemSizeChangedEventHandler(this.DockingManager_LayoutItemSizeChanged);
    }

    public ContentItem ActivePanel => (ContentItem)this.DockingManager.DockController.ActiveItem;

    public IStudioControl ActiveControl => (IStudioControl)this.ActivePanel?.Content;

    public void Activate(IStudioControl control)
    {
        if (control == null)
            throw new ArgumentNullException(nameof(control));
        LayoutPanel layoutPanel;
        if (!this._panels.TryGetValue(control.Key, out layoutPanel))
            return;
        this.DockingManager.DockController.Activate((BaseLayoutItem)layoutPanel);
    }

    public void Activate(Func<IStudioControl, bool> where)
    {
        IStudioControl control = where != null ? this._controls.FirstOrDefault<IStudioControl>(where) : throw new ArgumentNullException(nameof(where));
        if (control == null)
            return;
        this.Activate(control);
    }

    private (IStudioControl ctrl, bool isNew) OpenWindow(bool isTool, Type controlType)
    {
        if ((object)controlType == null)
            throw new ArgumentNullException(nameof(controlType));
        string key = TypeHelper.Is<IStudioControl>(controlType, true) ? controlType.CreateKey() : throw new ArgumentException(StringHelper.Put(LocalizedStrings.TypeNotImplemented, new object[2]
        {
      (object) controlType.Name,
      (object) "IStudioControl"
        }), nameof(controlType));
        return this.OpenWindow(isTool, key, (Func<IStudioControl>)(() =>
        {
            IStudioControl instance = TypeHelper.CreateInstance<IStudioControl>(controlType, Array.Empty<object>());
            instance.Key = key;
            return instance;
        }));
    }

    public (IStudioControl ctrl, bool isNew) OpenToolWindow(Type controlType)
    {
        return this.OpenWindow(true, controlType);
    }

    public (IStudioControl ctrl, bool isNew) OpenDocumentWindow(Type controlType)
    {
        return this.OpenWindow(false, controlType);
    }

    public (IStudioControl ctrl, bool isNew) OpenDocumentWindow(IStudioControl content)
    {
        return content != null ? this.OpenWindow(false, content.Key, (Func<IStudioControl>)(() => content)) : throw new ArgumentNullException(nameof(content));
    }

    public (IStudioControl ctrl, bool isNew) OpenDocumentWindow<T>(string key, Func<T> getControl) where T : IStudioControl
    {
        return this.OpenWindow(false, key, (Func<IStudioControl>)(() => (IStudioControl)getControl()));
    }

    private (IStudioControl ctrl, bool isNew) OpenWindow(
      bool isTool,
      string key,
      Func<IStudioControl> getControl)
    {
        if (StringHelper.IsEmpty(key))
            throw new ArgumentNullException(nameof(key));
        if (getControl == null)
            throw new ArgumentNullException(nameof(getControl));
        IDockController dockController = this.DockingManager.DockController;
        bool flag = false;
        LayoutPanel layoutPanel;
        if (!this._panels.TryGetValue(key, out layoutPanel))
        {
            flag = true;
            IStudioControl control = getControl();
            layoutPanel = isTool ? dockController.AddPanel(DockType.Right) : (LayoutPanel)dockController.AddDocumentPanel(this._documentGroup);
            layoutPanel.Name = key;
            layoutPanel.Content = (object)control;
            layoutPanel.ShowCloseButton = layoutPanel.AllowClose = true;
            layoutPanel.Visibility = Visibility.Visible;
            LayoutManager.SetHelpSourceUrl(layoutPanel, control);
            LayoutManager.BindProperties(layoutPanel, control);
            this._panels[key] = layoutPanel;
            this._controls.Add(control);
            Action<IStudioControl> controlAdded = this.ControlAdded;
            if (controlAdded != null)
                controlAdded(control);
            if (this._loadingCtrl != control)
                this.MarkControlChanged(control);
        }
        else
        {
            dockController.Restore((BaseLayoutItem)layoutPanel);
            layoutPanel.Visibility = Visibility.Visible;
        }
        dockController.Activate((BaseLayoutItem)layoutPanel);
        return ((IStudioControl)layoutPanel.Content, flag);
    }

    public bool Remove(IStudioControl content)
    {
        if (content == null)
            throw new ArgumentNullException(nameof(content));
        LayoutPanel layoutPanel;
        if (!this._panels.TryGetValue(content.Key, out layoutPanel))
            return false;
        layoutPanel.AllowClose = true;
        layoutPanel.ClosingBehavior = ClosingBehavior.ImmediatelyRemove;
        if (this.DockingManager.ClosedPanels.Contains(layoutPanel))
            this.DockingManager.DockController.Restore((BaseLayoutItem)layoutPanel);
        return this.DockingManager.DockController.Close((BaseLayoutItem)layoutPanel);
    }

    public void Remove(Func<IStudioControl, bool> where)
    {
        IStudioControl content = where != null ? this._controls.FirstOrDefault<IStudioControl>(where) : throw new ArgumentNullException(nameof(where));
        if (content == null)
            return;
        this.Remove(content);
    }

    public void Clear()
    {
        foreach (LayoutPanel layoutPanel in this._panels.Values.Distinct<LayoutPanel>().ToArray<LayoutPanel>())
        {
            layoutPanel.ClosingBehavior = ClosingBehavior.ImmediatelyRemove;
            this.DockingManager.DockController.Close((BaseLayoutItem)layoutPanel);
        }
        this._panels.Clear();
        ((BaseCollection<IStudioControl, ISet<IStudioControl>>)this._changedControls).Clear();
        this._dockingControlSettings.Clear();
    }

    public override void Load(SettingsStorage storage)
    {
        if (storage == null)
            throw new ArgumentNullException(nameof(storage));
        this.Clear();
        foreach (SettingsStorage storage1 in storage.GetValue<SettingsStorage[]>("Controls", (SettingsStorage[])null))
            this.LoadControl(storage1);
        this._layout = storage.GetValue<string>("Layout", (string)null);
        if (StringHelper.IsEmpty(this._layout))
            return;
        this.LoadLayout(this._layout);
    }

    public void Save(SettingsStorage storage, bool force)
    {
        if (storage == null)
            throw new ArgumentNullException(nameof(storage));

        IStudioControl[] items = force ? _dockingControlSettings.SyncGet(d => d.Keys.ToArray()) : _changedControls.SyncGet(c => c.CopyAndClear());

        int num = this._isLayoutChanged ? 1 : 0;
        this._isLayoutChanged = false;
        if (items.Length != 0)
            this.Save((IEnumerable<IStudioControl>)items);
        if (num != 0 && !this.DockingManager.IsDisposing)
            this._layout = this.SaveLayout();

        storage.SetValue<SettingsStorage[]>("Controls", _dockingControlSettings.SyncGet((c => c.Select(p => p.Value).ToArray())));
        storage.SetValue<string>("Layout", this._layout);
    }

    public override void Save(SettingsStorage storage) => this.Save(storage, false);

    public void LoadLayout(string layout)
    {
        if (layout == null)
            throw new ArgumentNullException(nameof(layout));
        try
        {
            LayoutPanel[] array = this.DockingManager.GetItems().OfType<LayoutPanel>().ToArray<LayoutPanel>();
            Dictionary<string, string> dictionary = ((IEnumerable<LayoutPanel>)array).Where<LayoutPanel>((Func<LayoutPanel, bool>)(c => !StringHelper.IsEmpty(c.Name))).ToDictionary<LayoutPanel, string, string>((Func<LayoutPanel, string>)(c => c.Name), (Func<LayoutPanel, string>)(c => (string)c.Caption));
            this._supressChanges = true;
            try
            {
                this.DockingManager.LoadLayout(layout);
            }
            finally
            {
                this._supressChanges = false;
            }
            foreach (LayoutPanel panel in array)
            {
                this._panels[panel.Name] = panel;
                if (panel.Content is IStudioControl content)
                {
                    LayoutManager.BindProperties(panel, content);
                }
                else
                {
                    string str = dictionary.TryGetValue(panel.Name);
                    if (!StringHelper.IsEmpty(str))
                        panel.Caption = (object)str;
                }
            }
        }
        catch (Exception ex)
        {
            this.LogError(ex);
        }
    }

    public string SaveLayout()
    {
        try
        {
            return this.DockingManager.SaveLayout();
        }
        catch (Exception ex)
        {
            LoggingHelper.AddErrorLog((ILogReceiver)this, ex, "error saving dockmanager layout: {0}");
            return this._layout;
        }
    }

    public void LoadControl(SettingsStorage storage)
    {
        if (storage == null)
            throw new ArgumentNullException(nameof(storage));
        try
        {
            string str = LayoutManager.Migrate(storage.GetValue<string>("ControlType", (string)null));
            bool flag = storage.GetValue<bool>("IsToolWindow", true);
            IStudioControl control = TypeHelper.CreateInstance<IStudioControl>(Converter.To<Type>((object)str), Array.Empty<object>());
            ((IPersistable)control).Load(storage);
            this._dockingControlSettings.Add(control, storage);
            this._loadingCtrl = control;
            try
            {
                if (flag)
                    this.OpenWindow(true, control.Key, (Func<IStudioControl>)(() => control));
                else
                    this.OpenDocumentWindow(control);
            }
            finally
            {
                this._loadingCtrl = (IStudioControl)null;
            }
        }
        catch (Exception ex)
        {
            this.LogError(ex);
        }
    }

    public SettingsStorage SaveControl(IStudioControl control)
    {
        return control != null ? this.Save(control) : throw new ArgumentNullException(nameof(control));
    }

    public void MarkControlChanged(IStudioControl control)
    {
        if (!control.SaveWithLayout || !this._controls.Contains(control))
            return;
        ((BaseCollection<IStudioControl, ISet<IStudioControl>>)this._changedControls).Add(control);
        Action changed = this.Changed;
        if (changed != null)
            changed();
        Action<IStudioControl> controlChanged = this.ControlChanged;
        if (controlChanged == null)
            return;
        controlChanged(control);
    }

    private void OnDockingManagerDockItemClosing(object sender, ItemCancelEventArgs e)
    {
        e.Cancel = this.OnDockingManagerItemClosing(e.Item);
    }

    public event Action<LayoutPanel> Hidden;

    private bool OnDockingManagerItemClosing(BaseLayoutItem item)
    {
        switch (item)
        {
            case LayoutPanel layoutPanel:
                if (!(layoutPanel.Content is BaseStudioControl content))
                {
                    item.ClosingBehavior = ClosingBehavior.ImmediatelyRemove;
                    return false;
                }
                CloseAction closeAction1;
                switch (item.ClosingBehavior)
                {
                    case ClosingBehavior.Default:
                        closeAction1 = content.CanClose(CloseReason.CloseWindow);
                        break;
                    case ClosingBehavior.HideToClosedPanelsCollection:
                        closeAction1 = CloseAction.Hide;
                        break;
                    default:
                        closeAction1 = CloseAction.Close;
                        break;
                }
                CloseAction closeAction2 = closeAction1;
                switch (closeAction2)
                {
                    case CloseAction.StayOpen:
                        return true;
                    case CloseAction.Close:
                        item.ClosingBehavior = ClosingBehavior.ImmediatelyRemove;
                        return false;
                    case CloseAction.Hide:
                        item.ClosingBehavior = ClosingBehavior.HideToClosedPanelsCollection;
                        item.Visibility = Visibility.Collapsed;
                        Action<LayoutPanel> hidden = this.Hidden;
                        if (hidden != null)
                            hidden(layoutPanel);
                        return true;
                    default:
                        throw new InvalidOperationException(closeAction2.ToString());
                }
            case LayoutGroup layoutGroup:
                return layoutGroup.Items.Any<BaseLayoutItem>(new Func<BaseLayoutItem, bool>(this.OnDockingManagerItemClosing));
            default:
                return false;
        }
    }

    private void OnDockingManagerDockItemClosed(object sender, DockItemClosedEventArgs e)
    {
        foreach (BaseLayoutItem affectedItem in e.AffectedItems)
        {
            if (affectedItem.ClosingBehavior == ClosingBehavior.HideToClosedPanelsCollection)
                this.LogHiddenPanel(affectedItem);
            else if (affectedItem.ClosingBehavior == ClosingBehavior.ImmediatelyRemove)
                this.OnDockingManagerItemClosed(affectedItem);
        }
        this.OnDockingChanged();
    }

    private void LogHiddenPanel(BaseLayoutItem item)
    {
        string str = item.Name;
        if (item is LayoutPanel layoutPanel && layoutPanel.Content is BaseStudioControl content)
            str = $"{content.Title} ({item.Name}, {((object)content).GetType().Name})";
        this.LogWarning($"Panel {str} was hidden to closed panels", Array.Empty<object>());
    }

    private void OnDockingManagerItemClosed(BaseLayoutItem item)
    {
        LayoutPanel panel = item as LayoutPanel;
        if (panel != null)
        {
            if (!(panel.Content is IStudioControl content))
                return;
            LayoutManager.DisposeControl(content, CloseReason.CloseWindow);
            _panels.RemoveWhere((p => object.Equals(p.Value, panel)));
            this._controls.Remove(content);
            ((BaseCollection<IStudioControl, ISet<IStudioControl>>)this._changedControls).Remove(content);
            this._dockingControlSettings.Remove(content);
            Action<IStudioControl> controlRemoved = this.ControlRemoved;
            if (controlRemoved != null)
                controlRemoved(content);
        }
        if (!(item is LayoutGroup layoutGroup))
            return;
        foreach (BaseLayoutItem baseLayoutItem in (Collection<BaseLayoutItem>)layoutGroup.Items)
            this.OnDockingManagerItemClosed(baseLayoutItem);
    }

    private void DockingManager_DockItemEndDocking(object sender, DockItemDockingEventArgs e)
    {
        this.OnDockingChanged();
    }

    private void DockingManager_DockItemActivated(object sender, DockItemActivatedEventArgs ea)
    {
        this.OnDockingChanged();
    }

    private void DockingManager_DockItemExpanded(object sender, DockItemExpandedEventArgs e)
    {
        this.OnDockingChanged();
    }

    private void DockingManager_DockItemRestored(object sender, ItemEventArgs e)
    {
        this.OnDockingChanged();
    }

    private void DockingManager_DockItemHidden(object sender, ItemEventArgs e)
    {
        this.OnDockingChanged();
    }

    private void DockingManager_LayoutItemSizeChanged(object sender, LayoutItemSizeChangedEventArgs e)
    {
        this.OnDockingChanged();
    }

    private void OnDockingChanged()
    {
        if (this._supressChanges)
            return;
        this._isLayoutChanged = true;
        Action changed = this.Changed;
        if (changed != null)
            changed();
        Action layoutChanged = this.LayoutChanged;
        if (layoutChanged == null)
            return;
        layoutChanged();
    }

    private void Save(IEnumerable<IStudioControl> items)
    {
        foreach (IStudioControl control in items)
        {
            if (control.SaveWithLayout)
                this._dockingControlSettings[control] = this.Save(control);
        }
    }

    private SettingsStorage Save(IStudioControl control)
    {
        bool flag = _panels.TryGetValue(control.Key) is DocumentPanel;
        SettingsStorage settingsStorage = new SettingsStorage().Set<string>("ControlType", TypeHelper.GetTypeName(control.GetType(), false)).Set<bool>("IsToolWindow", !flag);
        ((IPersistable)control).Save(settingsStorage);
        return settingsStorage;
    }

    private static string Migrate(string typeName)
    {
        if (typeName.StartsWith("StockSharp.Designer") && !typeName.Contains("Panels"))
            typeName = typeName.Replace("StockSharp.Designer", "StockSharp.Designer.Panels");
        typeName = typeName.Replace("StockSharp.Hydra.Server.UsersPane,", "StockSharp.Hydra.Panes.UsersPane,");
        typeName = typeName.Replace("StockSharp.Hydra.Server.SubscriptionsPane,", "StockSharp.Hydra.Panes.SubscriptionsPane,");
        typeName = typeName.Replace("StockSharp.Hydra.Server.SecurityMappingPane,", "StockSharp.Hydra.Panes.SecurityMappingPane,");
        typeName = typeName.Replace("StockSharp.Studio.Controls.MyTradesTable", "StockSharp.Studio.Controls.MyTradesPanel");
        return typeName;
    }

    protected override void DisposeManaged()
    {
        foreach (IStudioControl control in this._controls)
            LayoutManager.DisposeControl(control, CloseReason.Shutdown);
        this.DisposeManaged();
    }

    private static void BindProperties(LayoutPanel panel, IStudioControl control)
    {
        panel.SetBindings(BaseLayoutItem.CaptionProperty, (object)control, new PropertyPath("(0)", new object[1]
        {
      (object) typeof (IStudioControl).GetProperty("Title")
        }));
        panel.SetBindings(BaseLayoutItem.AllowRenameProperty, (object)control, new PropertyPath("(0)", new object[1]
        {
      (object) typeof (IStudioControl).GetProperty("IsTitleEditable")
        }), BindingMode.OneWay);
        panel.SetBindings(BaseLayoutItem.CaptionImageProperty, (object)control, new PropertyPath("(0)", new object[1]
        {
      (object) typeof (IStudioControl).GetProperty("Icon")
        }), BindingMode.OneWay);
        LayoutPanel layoutPanel = panel;
        Style style = new Style(typeof(CaptionImage));
        style.Setters.Add((SetterBase)new Setter(FrameworkElement.WidthProperty, (object)16.0));
        style.Setters.Add((SetterBase)new Setter(FrameworkElement.HeightProperty, (object)16.0));
        style.Setters.Add((SetterBase)new Setter(Image.StretchProperty, (object)Stretch.Fill));
        layoutPanel.CaptionImageStyle = style;
    }

    private static void SetHelpSourceUrl(LayoutPanel document, IStudioControl control)
    {
        if (document == null)
            throw new ArgumentNullException(nameof(document));
        string str = control != null ? StringHelper.IsEmpty(control.DocUrl, ((UIElement)control).GetUrl()) : throw new ArgumentNullException(nameof(control));
        if (StringHelper.IsEmpty(str))
            return;
        document.SetUrl(str);
    }

    private static void DisposeControl(IStudioControl control, CloseReason reason)
    {
        if (control is BaseStudioControl baseStudioControl)
            baseStudioControl.Dispose(reason);
        else
            control.Dispose();
    }

    private static class Keys
    {
        public const string ControlType = "ControlType";
        public const string IsToolWindow = "IsToolWindow";
    }
}
