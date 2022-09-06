
using DevExpress.Xpf.Editors.Settings;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;

namespace StockSharp.Studio.Controls.Editors
{
    public partial class OrderLogBuilderComboEditor : ComboBoxEditSettings, IComponentConnector
    {        
        public OrderLogBuilderComboEditor()
        {
            this.InitializeComponent();
            this.DisplayMember = "Name";
            this.ValueMember = "Type";
            Type defaultBuilder;
            this.ItemsSource = OrderLogBuilderItem.CreateSource( out defaultBuilder );
        }        
    }
}
