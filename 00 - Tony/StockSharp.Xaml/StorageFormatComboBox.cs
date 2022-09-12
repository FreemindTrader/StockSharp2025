using Ecng.Xaml;
using StockSharp.Algo.Storages;
using System.Windows.Controls;

namespace StockSharp.Xaml
{
    public class StorageFormatComboBox : EnumComboBox
    {
        public StorageFormatComboBox( )
        {            
            EnumType = ( typeof( StorageFormats ) );
            SelectedFormat = StorageFormats.Binary;
        }

        public StorageFormats SelectedFormat
        {
            get
            {
                return ( StorageFormats? ) this.GetSelectedValue<StorageFormats>( ) ??  0;
            }
            set
            {
                this.SetSelectedValue<StorageFormats>( new StorageFormats?( value ) );
            }
        }
    }
}
