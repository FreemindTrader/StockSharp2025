// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.ClientSocialComboBox
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System.Windows;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Studio.Controls;

public class ClientSocialComboBox : ComboBoxEdit
{
    public static readonly DependencyProperty SelectedClientSocialProperty = DependencyProperty.Register(nameof(SelectedClientSocial), typeof(ClientSocial), typeof(ClientSocialComboBox), new PropertyMetadata((object)null, new PropertyChangedCallback(ClientSocialComboBox.SelectedClientSocialPropertyCallback)));

    protected override BaseEditSettings CreateEditorSettings()
    {
        return (BaseEditSettings)new ClientSocialComboBoxEditor()
        {
            ShowSearchButton = true
        };
    }

    protected override void OnLoadedInternal()
    {
        base.OnLoadedInternal();
        if (this.EditMode != EditMode.Standalone)
            return;
        this.Settings.ApplyToEdit((IBaseEdit)this, true, (IDefaultEditorViewInfo)EmptyDefaultEditorViewInfo.Instance);
    }

    private static void SelectedClientSocialPropertyCallback(
      DependencyObject sender,
      DependencyPropertyChangedEventArgs args)
    {
        ((ClientSocialComboBox)sender).SelectedClientSocial = (ClientSocial)args.NewValue;
    }

    public ClientSocial SelectedClientSocial
    {
        get => (ClientSocial)this.SelectedItem;
        set => this.SelectedItem = (object)value;
    }
}
