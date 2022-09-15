using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;
using StockSharp.Algo.Storages;
using StockSharp.Xaml.PropertyGrid;
using System.Collections.Generic;

namespace StockSharp.Xaml
{
    public class ExtendedInfoStorageComboBox : ComboBoxEdit
    {
        public ExtendedInfoStorageComboBox( )
        {
            
        }

        protected override BaseEditSettings CreateEditorSettings( )
        {
            return ( BaseEditSettings ) new ExtendedInfoStorageEditor();
        }

        protected override void OnLoadedInternal( )
        {
            base.OnLoadedInternal();

            if ( ( ( BaseEdit ) this ).EditMode != EditMode.Standalone )
                return;
            ( ( BaseEditSettings ) this.Settings ).ApplyToEdit( ( IBaseEdit ) this, true, ( IDefaultEditorViewInfo ) EmptyDefaultEditorViewInfo.Instance );
        }

        public IEnumerable<IExtendedInfoStorageItem> Storages
        {
            get
            {
                return ( IEnumerable<IExtendedInfoStorageItem> ) ( ( LookUpEditBase ) this ).ItemsSource;
            }
            set
            {
                ItemsSource = (  value );
            }
        }

        public IExtendedInfoStorageItem SelectedStorage
        {
            get
            {
                return ( IExtendedInfoStorageItem ) ( ( BaseEdit ) this ).EditValue;
            }
            set
            {
                EditValue = value;
            }
        }
    }
}
