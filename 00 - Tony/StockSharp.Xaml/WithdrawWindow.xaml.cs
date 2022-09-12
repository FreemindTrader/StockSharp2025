using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using StockSharp.Messages;
using StockSharp.Xaml.PropertyGrid;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    public partial class WithdrawWindow : DXWindow
    {
        public WithdrawWindow()
        {
            InitializeComponent();

            WithdrawInfo = new WithdrawInfo();
            Volume = new Decimal?( 1 );
        }

        public Decimal? Volume
        {
            get
            {
                return ( Decimal ?  ) VolumeCtrl.EditValue;
            }
            set
            {
                VolumeCtrl.EditValue = value;
            }
        }

        public Decimal VolumeStep
        {
            get
            {
                return VolumeCtrl.Increment;
            }
            set
            {
                VolumeCtrl.Increment =  value;
            }
        }

        public WithdrawInfo WithdrawInfo
        {
            get
            {
                return ( WithdrawInfo ) PropertyGrid.SelectedObject;
            }
            set
            {
                if ( value == null )
                {
                    throw new ArgumentNullException( nameof( value ) );
                }

                PropertyGrid.SelectedObject = value;
            }
        }

        private void spinEdit_0_EditValueChanged( object sender, EditValueChangedEventArgs e )
        {
            TryEnableOkayButton();
        }

        private void TryEnableOkayButton( )
        {
            OkBtn.IsEnabled = ( Volume.HasValue && Volume.Value > 0 );            
        }
    }
}
