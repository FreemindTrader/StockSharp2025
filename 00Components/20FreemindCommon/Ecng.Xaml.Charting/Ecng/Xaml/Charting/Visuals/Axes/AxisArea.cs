// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.AxisArea
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System.Windows.Controls;

namespace fx.Xaml.Charting
{
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
}
