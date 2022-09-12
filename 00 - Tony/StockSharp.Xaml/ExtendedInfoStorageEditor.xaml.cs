using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Helpers;
using DevExpress.Xpf.Editors.Settings;
using Ecng.Collections;
using Ecng.Configuration;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.Algo.Storages;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;

namespace StockSharp.Xaml.PropertyGrid
{
    public partial class ExtendedInfoStorageEditor : ComboBoxEditSettings, IComponentConnector
    {
        private readonly ObservableCollection<IExtendedInfoStorageItem> _storageItem = new ObservableCollection<IExtendedInfoStorageItem>( );
        private ComboBoxEdit _internalComboBox;                

        static ExtendedInfoStorageEditor( )
        {
            EditorSettingsProvider.Default.RegisterUserEditor2( typeof( ExtendedInfoStorageComboBox ), 
                                                                typeof( ExtendedInfoStorageEditor ), 
                                                                new CreateEditorMethod2( optimized =>
                                                                                                {
                                                                                                    if ( !optimized )
                                                                                                    {
                                                                                                        return ( IBaseEdit )new ExtendedInfoStorageComboBox( );
                                                                                                    }

                                                                                                    return ( IBaseEdit )new InplaceBaseEdit( );
                                                                                                } 
                                                                                        ), 
                                                                new CreateEditorSettingsMethod( ( ) => new ExtendedInfoStorageEditor( ) ) 
                                                              );
        }

        public ExtendedInfoStorageEditor( )
        {
            this.InitializeComponent( );
            if ( !this.IsDesignMode( ) )
            {
                IExtendedInfoStorage extendedInfoStorage = ServicesRegistry.ExtendedInfoStorage;
                this._storageItem.AddRange<IExtendedInfoStorageItem>( extendedInfoStorage.Storages );
                extendedInfoStorage.Created += new Action<IExtendedInfoStorageItem>( this.Service_Created );
                extendedInfoStorage.Deleted += new Action<IExtendedInfoStorageItem>( this.Service_Deleted );
            }
            this.ItemsSource = ( object )this._storageItem;
        }

        private void Service_Created( IExtendedInfoStorageItem storageItem )
        {
            this.GuiAsync( ( ) => _storageItem.Add( storageItem ) );
        }

        private void Service_Deleted( IExtendedInfoStorageItem storageItem )
        {
            this.GuiAsync( ( ) => _storageItem.Remove( storageItem ) );
        }

        protected override void AssignToEditCore( IBaseEdit edit )
        {
            this._internalComboBox = edit as ComboBoxEdit;
            base.AssignToEditCore( edit );
        }

        private void CancelBtn_Click( object sender, RoutedEventArgs e )
        {
            if ( this._internalComboBox == null )
            {
                return;
            }

            this._internalComboBox.EditValue = null;
        }
       
    }
}
