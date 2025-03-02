// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.ClientSocialComboBoxEditor
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

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
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace StockSharp.Studio.Controls
{
    public partial class ClientSocialComboBoxEditor : ComboBoxEditSettings, ITelegramChannelEditor
    {
        private readonly ObservableCollection<ComboButtonItem> _buttonsSource = new ObservableCollection<ComboButtonItem>();
        private ComboBoxEdit _edit;
        private readonly ComboButtonItem _searchButton;
        private readonly ComboButtonItem _deleteButton;        

        static ClientSocialComboBoxEditor()
        {
            EditorSettingsProvider.Default.RegisterUserEditor2( typeof( ClientSocialComboBox ), typeof( ClientSocialComboBoxEditor ), ( CreateEditorMethod2 ) ( optimized =>
            {
                if ( !optimized )
                    return ( IBaseEdit ) new ClientSocialComboBox();
                return ( IBaseEdit ) new InplaceBaseEdit();
            } ), ( CreateEditorSettingsMethod ) ( () => ( BaseEditSettings ) new ClientSocialComboBoxEditor() ) );
        }

        public ClientSocialComboBoxEditor()
        {
            this.InitializeComponent();
            this.DisplayMember = "Name";
            if ( !this.IsDesignMode() )
                this.ItemsSource =  WebApiHelper.TryClientSocialsSource;
            this._searchButton = new ComboButtonItem( "Search", Visibility.Visible, LocalizedStrings.Search, ( ICommand ) new DelegateCommand( ( Action<object> ) ( arg =>
            {
                new ClientSocialListWindow().ShowModal( GetOwner() );
                this.ItemsSource =  WebApiHelper.TryClientSocialsSource;
            } ), ( Func<object, bool> ) null ) );
            this._deleteButton = new ComboButtonItem( "Cancel", Visibility.Visible, LocalizedStrings.Delete, ( ICommand ) new DelegateCommand( ( Action<object> ) ( arg => this.SelectedClientSocial = ( ClientSocial ) null ), ( Func<object, bool> ) ( arg => this.SelectedClientSocial != null ) ) );
            this.ButtonsSource = ( IEnumerable ) this._buttonsSource;
            this.ShowSearchButton = true;
            this.ShowDeleteButton = true;

            DependencyObject GetOwner()
            {
                return ( DependencyObject ) this._edit ?? ( DependencyObject ) this;
            }
        }

        private bool IsVisible( ComboButtonItem btn )
        {
            return this._buttonsSource.Contains( btn );
        }

        private void SetVisible( ComboButtonItem btn, bool value )
        {
            if ( value )
                CollectionHelper.TryAdd<ComboButtonItem>(  this._buttonsSource,  btn );
            else
                this._buttonsSource.Remove( btn );
        }

        public bool ShowSearchButton
        {
            get
            {
                return this.IsVisible( this._searchButton );
            }
            set
            {
                this.SetVisible( this._searchButton, value );
            }
        }

        public bool ShowDeleteButton
        {
            get
            {
                return this.IsVisible( this._deleteButton );
            }
            set
            {
                this.SetVisible( this._deleteButton, value );
            }
        }

        private ClientSocial SelectedClientSocial
        {
            get
            {
                if ( this._edit != null )
                    return ( ClientSocial ) this._edit.SelectedItem;
                return ( ( ClientSocialComboBox ) this.Editor ).SelectedClientSocial;
            }
            set
            {
                if ( this._edit == null )
                    ( ( ClientSocialComboBox ) this.Editor ).SelectedClientSocial = value;
                else
                    this._edit.SelectedItem =  value;
            }
        }

        protected override void AssignToEditCore( IBaseEdit edit )
        {
            this._edit = edit as ComboBoxEdit;
            base.AssignToEditCore( edit );
        }

        
    }
}
