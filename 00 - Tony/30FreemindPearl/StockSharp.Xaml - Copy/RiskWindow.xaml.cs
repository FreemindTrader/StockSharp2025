using DevExpress.Xpf.Core;
using StockSharp.Algo.Risk;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    public partial class RiskWindow : DXWindow
    {
        public RiskWindow()
        {
            InitializeComponent();
        }

        public IList<IRiskRule> Rules
        {
            get
            {
                return Panel.Rules;
            }
        }
    }
}
