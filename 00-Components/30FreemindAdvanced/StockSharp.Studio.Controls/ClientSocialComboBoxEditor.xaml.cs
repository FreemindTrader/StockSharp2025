// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.ClientSocialComboBoxEditor
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Helpers;
using DevExpress.Xpf.Editors.Settings;
using Ecng.Collections;
using Ecng.ComponentModel;
using Ecng.Xaml;
using StockSharp.Localization;
using StockSharp.Studio.WebApi;
using StockSharp.Web.DomainModel;
using StockSharp.Xaml.PropertyGrid;

#nullable disable
namespace StockSharp.Studio.Controls;

public partial class ClientSocialComboBoxEditor :
  ComboBoxEditSettings,
  ITelegramChannelEditor,
  IComponentConnector
{
    private ComboBoxEdit _edit;
    private readonly ComboButtonItem _searchButton;
    private readonly ComboButtonItem _deleteButton;
    private readonly ObservableCollection<ComboButtonItem> _buttonsSource = new ObservableCollection<ComboButtonItem>();


    static ClientSocialComboBoxEditor()
    {
        EditorSettingsProvider.Default.RegisterUserEditor2(typeof(ClientSocialComboBox), typeof(ClientSocialComboBoxEditor), (CreateEditorMethod2)(optimized => !optimized ? (IBaseEdit)new ClientSocialComboBox() : (IBaseEdit)new InplaceBaseEdit()), (CreateEditorSettingsMethod)(() => (BaseEditSettings)new ClientSocialComboBoxEditor()));
    }

    public ClientSocialComboBoxEditor()
    {
        this.InitializeComponent();
        this.DisplayMember = "Name";
        if (!this.IsDesignMode())
            this.ItemsSource = (object)WebApiHelper.TryClientSocialsSource;
        this._searchButton = new ComboButtonItem("Search", Visibility.Visible, LocalizedStrings.Search, (ICommand)new DelegateCommand((Action<object>)(arg =>
        {
            new ClientSocialListWindow().ShowModal(GetOwner());
            this.ItemsSource = (object)WebApiHelper.TryClientSocialsSource;
        })));
        this._deleteButton = new ComboButtonItem("Cancel", Visibility.Visible, LocalizedStrings.Delete, (ICommand)new DelegateCommand((Action<object>)(arg => this.SelectedClientSocial = (ClientSocial)null), (Func<object, bool>)(arg => this.SelectedClientSocial != null)));
        this.ButtonsSource = (IEnumerable)this._buttonsSource;
        this.ShowSearchButton = true;
        this.ShowDeleteButton = true;

        DependencyObject GetOwner() => (DependencyObject)this._edit ?? (DependencyObject)this;
    }

    private bool IsVisible(ComboButtonItem btn) => this._buttonsSource.Contains(btn);

    private void SetVisible(ComboButtonItem btn, bool value)
    {
        if (value)
            CollectionHelper.TryAdd<ComboButtonItem>((ICollection<ComboButtonItem>)this._buttonsSource, btn);
        else
            this._buttonsSource.Remove(btn);
    }

    public bool ShowSearchButton
    {
        get => this.IsVisible(this._searchButton);
        set => this.SetVisible(this._searchButton, value);
    }

    public bool ShowDeleteButton
    {
        get => this.IsVisible(this._deleteButton);
        set => this.SetVisible(this._deleteButton, value);
    }

    private ClientSocial SelectedClientSocial
    {
        get
        {
            return this._edit != null ? (ClientSocial)this._edit.SelectedItem : ((ClientSocialComboBox)this.Editor).SelectedClientSocial;
        }
        set
        {
            if (this._edit == null)
                ((ClientSocialComboBox)this.Editor).SelectedClientSocial = value;
            else
                this._edit.SelectedItem = (object)value;
        }
    }

    protected override void AssignToEditCore(IBaseEdit edit)
    {
        this._edit = edit as ComboBoxEdit;
        base.AssignToEditCore(edit);
    }


}
