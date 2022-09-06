using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.Native;

namespace FreemindTrader
{
    /// <summary>
    /// Interaction logic for SupportResistanceView.xaml
    /// </summary>
    public partial class SupportResistanceView : UserControl
    {
        SupportResistanceViewModel _vm;
         
        public SupportResistanceView( )
        {
            InitializeComponent( );

            _vm = ( SupportResistanceViewModel )DataContext;

        }

        

        double dataRowHeightCore;
        double DataRowHeight
        {
            get
            {
                if ( dataRowHeightCore == 0d )
                {
                    FrameworkElement fe = View.GetRowElementByRowHandle( 0 );
                    dataRowHeightCore = fe != null ? fe.ActualHeight : 21d;
                }
                return dataRowHeightCore;
            }
        }

        DataPresenter dataPresenterCore;
        DataPresenter ViewDataPresenter
        {
            get
            {
                if ( dataPresenterCore == null )
                    dataPresenterCore = ( View.ScrollContentPresenter as ScrollContentPresenter ).Content as DataPresenter;
                return dataPresenterCore;
            }
        }

        private void _supportResistanceLevels_CurrentItemChanged( object sender, CurrentItemChangedEventArgs e )
        {
            int scrollToIndex = _vm.LastRowIndex;
            int rowHandle = _supportResistanceLevels.GetRowHandleByListIndex( scrollToIndex );

            if ( !View.IsRowSelected( scrollToIndex ) )
            {
                ScrollInfo scrollInfo = ViewDataPresenter.ScrollInfoCore;
                scrollInfo.SetVerticalOffsetForce( scrollToIndex - View.ScrollContentPresenter.ActualHeight / DataRowHeight / 2d );
                View.MoveFocusedRow( _supportResistanceLevels.GetRowVisibleIndexByHandle( rowHandle ) );
            }
        }

        private void View_FocusedRowChanged( object sender, FocusedRowChangedEventArgs e )
        {
            //var rowHandle =  View.FocusedRowHandle;
            
        }
    }
}
