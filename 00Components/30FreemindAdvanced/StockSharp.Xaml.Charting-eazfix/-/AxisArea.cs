using System.Windows.Controls;

#nullable disable
namespace StockSharp.Charting;

public class AxisArea : ItemsControl
{
    internal void SafeRemoveItem( object item )
    {
        try
        {
            if ( item == null || this.Items == null || !this.Items.Contains( item ) )
                return;
            this.Items.Remove( item );
        }
        catch
        {
        }
    }
}