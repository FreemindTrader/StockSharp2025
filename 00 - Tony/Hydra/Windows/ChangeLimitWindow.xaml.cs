using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using Ecng.Common;
using StockSharp.Localization;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace StockSharp.Hydra.Windows
{
    public partial class ChangeLimitWindow : ThemedWindow, IComponentConnector
    {
        private int _initLimit;
        
        public ChangeLimitWindow()
        {
            InitializeComponent();
            Limit = 1;
        }

        public int Limit
        {
            get
            {
                return LimitCtrl.EditValue.To<int?>().GetValueOrDefault();
            }
            set
            {
                SpinEdit limitCtrl1 = LimitCtrl;
                SpinEdit limitCtrl2 = LimitCtrl;
                Decimal? nullable1 = new Decimal?( _initLimit = value );
                Decimal? nullable2 = nullable1;
                limitCtrl2.MinValue = nullable2;
                var local = ( ValueType )nullable1;
                limitCtrl1.EditValue = local;
                WarningTxt.Text = LocalizedStrings.IncreaseLimit.Put( value );
            }
        }

        private void LimitCtrl_OnEditValueChanged( object sender, EditValueChangedEventArgs e )
        {
            OkBtn.IsEnabled = Limit > _initLimit;
        }        
    }
}
