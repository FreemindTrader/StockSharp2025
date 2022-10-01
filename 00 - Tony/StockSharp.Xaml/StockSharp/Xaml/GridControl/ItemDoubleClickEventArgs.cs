using DevExpress.Xpf.Grid;
using System;
using System.Windows.Input;

namespace StockSharp.Xaml.GridControl
{
    public class ItemDoubleClickEventArgs : EventArgs
    {
        private readonly GridColumn _gridColumn;
        private readonly object _row;
        private readonly MouseButtonEventArgs _buttonArgs;

        public ItemDoubleClickEventArgs( GridColumn column, object row, MouseButtonEventArgs buttonArgs )
        {
            GridColumn gridColumn = column;
            if ( gridColumn == null )
                throw new ArgumentNullException( nameof( column ) );
            this._gridColumn = gridColumn;
            object obj = row;
            if ( obj == null )
                throw new ArgumentNullException( nameof( row ) );
            this._row = obj;
            MouseButtonEventArgs mouseButtonEventArgs = buttonArgs;
            if ( mouseButtonEventArgs == null )
                throw new ArgumentNullException( nameof( buttonArgs ) );
            this._buttonArgs = mouseButtonEventArgs;
        }

        public GridColumn Column
        {
            get
            {
                return this._gridColumn;
            }
        }

        public object Row
        {
            get
            {
                return this._row;
            }
        }

        public MouseButtonEventArgs ButtonArgs
        {
            get
            {
                return this._buttonArgs;
            }
        }
    }
}
