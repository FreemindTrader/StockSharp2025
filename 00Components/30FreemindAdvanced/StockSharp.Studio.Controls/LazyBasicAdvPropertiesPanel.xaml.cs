// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.LazyBasicAdvPropertiesPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using DevExpress.Xpf.PropertyGrid;
using Ecng.Collections;
using Ecng.Serialization;
using StockSharp.Xaml.PropertyGrid;

#nullable disable
namespace StockSharp.Studio.Controls;

public partial class LazyBasicAdvPropertiesPanel : ContentControl, IPersistable, IComponentConnector
{
    public static readonly DependencyProperty SelectedObjectProperty = DependencyProperty.Register(nameof(SelectedObject), typeof(object), typeof(LazyBasicAdvPropertiesPanel), new PropertyMetadata((object)null, (PropertyChangedCallback)((s, e) =>
    {
        if (!(s is LazyBasicAdvPropertiesPanel advPropertiesPanel2))
            return;
        advPropertiesPanel2.Underlying.SelectedObject = e.NewValue;
    })));
    private bool _isReadOnly;
    private bool _showGridLines;
    private bool _postImmediately;
    private SettingsStorage _delayedStorage;

    public LazyBasicAdvPropertiesPanel() => this.InitializeComponent();

    public object SelectedObject
    {
        get => this.GetValue(LazyBasicAdvPropertiesPanel.SelectedObjectProperty);
        set => this.SetValue(LazyBasicAdvPropertiesPanel.SelectedObjectProperty, value);
    }

    public bool IsReadOnly
    {
        get => this._isReadOnly;
        set
        {
            this._isReadOnly = value;
            if (this.Content == null)
                return;
            this.Underlying.IsReadOnly = value;
        }
    }

    public bool ShowGridLines
    {
        get => this._showGridLines;
        set
        {
            this._showGridLines = value;
            if (this.Content == null)
                return;
            this.Underlying.ShowGridLines = value;
        }
    }

    public bool PostImmediately
    {
        get => this._postImmediately;
        set
        {
            this._postImmediately = value;
            if (this.Content == null)
                return;
            this.Underlying.PostImmediately = value;
        }
    }

    public event CustomExpandEventHandler CustomExpand;

    private void OnUnderlyingCustomExpand(object sender, CustomExpandEventArgs args)
    {
        CustomExpandEventHandler customExpand = this.CustomExpand;
        if (customExpand == null)
            return;
        customExpand(sender, args);
    }

    private BasicAdvPropertiesPanel Underlying
    {
        get
        {
            if (this.Content == null)
            {
                object obj;
                this.Content = (object)(BasicAdvPropertiesPanel)(obj = (object)create());
            }
            BasicAdvPropertiesPanel content = (BasicAdvPropertiesPanel)this.Content;
            if (this._delayedStorage != null)
            {
                PersistableHelper.ForceLoad<BasicAdvPropertiesPanel>(content, this._delayedStorage);
                this._delayedStorage = (SettingsStorage)null;
            }
            return content;

            BasicAdvPropertiesPanel create()
            {
                BasicAdvPropertiesPanel advPropertiesPanel = new BasicAdvPropertiesPanel();
                advPropertiesPanel.ShowGridLines = this.ShowGridLines;
                advPropertiesPanel.IsReadOnly = this.IsReadOnly;
                advPropertiesPanel.CustomExpand += new CustomExpandEventHandler(this.OnUnderlyingCustomExpand);
                return advPropertiesPanel;
            }
        }
    }

    void IPersistable.Load(SettingsStorage storage)
    {
        if (this.Content == null)
        {
            SettingsStorage settingsStorage = new SettingsStorage();
            foreach (KeyValuePair<string, object> keyValuePair in (SynchronizedDictionary<string, object>)storage)
                ((SynchronizedDictionary<string, object>)settingsStorage).Add(keyValuePair);
            this._delayedStorage = settingsStorage;
        }
        else
            PersistableHelper.ForceLoad<BasicAdvPropertiesPanel>(this.Underlying, storage);
    }

    void IPersistable.Save(SettingsStorage storage)
    {
        if (this._delayedStorage != null)
            CollectionHelper.AddRange<KeyValuePair<string, object>>((ICollection<KeyValuePair<string, object>>)storage, (IEnumerable<KeyValuePair<string, object>>)this._delayedStorage);
        else
            ((IPersistable)this.Underlying).Save(storage);
    }


}
