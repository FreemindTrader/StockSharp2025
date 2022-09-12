using DevExpress.Xpf.Core;
using Ecng.Collections;
using StockSharp.Algo.Commissions;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    public partial class CommissionWindow : DXWindow
    {
        public CommissionWindow( )
        {
            InitializeComponent( );
        }

        public IListEx<ICommissionRule> Rules
        {
            get
            {
                return this.Panel.Rules;
            }
        }
    }
}
