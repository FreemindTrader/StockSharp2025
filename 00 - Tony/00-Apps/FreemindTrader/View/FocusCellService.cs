using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Grid;
using System;
using System.Linq;
using System.Windows;

namespace FreemindTrader
{
    public interface IFocusCellService
    {
        void FocusCell();
    }

    public class FocusCellService : ServiceBase, IFocusCellService
    {
        public TableView View
        {
            get
            {
                return ( TableView )GetValue( ViewProperty );
            }
            set
            {
                SetValue( ViewProperty, value );
            }
        }

        public static readonly DependencyProperty ViewProperty = DependencyProperty.Register( "View", typeof( TableView ), typeof( FocusCellService ), new PropertyMetadata( null ) );

        public string ColumnName
        {
            get
            {
                return ( string )GetValue( ColumnNameProperty );
            }
            set
            {
                SetValue( ColumnNameProperty, value );
            }
        }

        public static readonly DependencyProperty ColumnNameProperty = DependencyProperty.Register( "ColumnName", typeof( string ), typeof( FocusCellService ), new PropertyMetadata( "Name" ) );

        public int RowHandle
        {
            get
            {
                return ( int )GetValue( RowHandleProperty );
            }
            set
            {
                SetValue( RowHandleProperty, value );
            }
        }

        public static readonly DependencyProperty RowHandleProperty = DependencyProperty.Register( "RowHandle", typeof( int ), typeof( FocusCellService ), new PropertyMetadata( 0 ) );

        void IFocusCellService.FocusCell()
        {
            if ( View == null )
            {
                return;
            }

            if ( View.Grid == null )
            {
                return;
            }

            View.FocusedRowHandle = RowHandle;
            View.Grid.CurrentColumn = View.Grid.Columns[ColumnName];
            View.ShowEditor();
        }
    }
}
