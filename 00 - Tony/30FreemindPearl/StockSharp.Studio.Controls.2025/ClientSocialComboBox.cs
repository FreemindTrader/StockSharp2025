using System;
using System.Windows;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;
using StockSharp.Web.DomainModel;

namespace StockSharp.Studio.Controls
{
    public class ClientSocialComboBox : ComboBoxEdit
    {
        protected override BaseEditSettings CreateEditorSettings()
        {
            return new ClientSocialComboBoxEditor
            {
                ShowSearchButton = true
            };
        }

        protected override void OnLoadedInternal()
        {
            base.OnLoadedInternal();
            if ( base.EditMode == EditMode.Standalone )
            {
                base.Settings.ApplyToEdit( this, true, EmptyDefaultEditorViewInfo.Instance );
            }
        }

        private static void SelectedClientSocialPropertyCallback( DependencyObject sender, DependencyPropertyChangedEventArgs args )
        {
            ClientSocialComboBox clientSocialComboBox = (ClientSocialComboBox)sender;
            ClientSocial selectedClientSocial = (ClientSocial)args.NewValue;
            clientSocialComboBox.SelectedClientSocial = selectedClientSocial;
        }

        public ClientSocial SelectedClientSocial
        {
            get
            {
                return ( ClientSocial ) base.SelectedItem;
            }
            set
            {
                base.SelectedItem = value;
            }
        }

        // Token: 0x0400002D RID: 45
        public static readonly DependencyProperty SelectedClientSocialProperty = DependencyProperty.Register("SelectedClientSocial", typeof(ClientSocial), typeof(ClientSocialComboBox), new PropertyMetadata(null, new PropertyChangedCallback(ClientSocialComboBox.SelectedClientSocialPropertyCallback)));
    }
}