using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;
using Ecng.Data;

namespace Ecng.Xaml.Database
{
    /// <summary>Combo box for select database connection.</summary>
    public class DatabaseConnectionComboBox : ComboBoxEdit
    {
        public DatabaseConnectionComboBox()
        {

        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        protected override BaseEditSettings CreateEditorSettings()
        {
            return ( BaseEditSettings )new DatabaseConnectionEditor();
        }

        /// <summary>
        /// </summary>
        protected override void OnLoadedInternal()
        {
            OnLoadedInternal();

            if ( this.EditMode != EditMode.Standalone )
                return;

            ( this.Settings ).ApplyToEdit( ( IBaseEdit )this, true, ( IDefaultEditorViewInfo )EmptyDefaultEditorViewInfo.Instance );
        }

        /// <summary>Selected connection.</summary>
        public DatabaseConnectionPair SelectedConnection
        {
            get
            {
                return ( DatabaseConnectionPair )( ( LookUpEditBase )this ).SelectedItem;
            }
            set
            {
                this.SelectedItem = value;
            }
        }
    }
}
