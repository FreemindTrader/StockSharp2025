using DevExpress.Xpf.Core;
using StockSharp.Xaml.PropertyGrid;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    /// <summary>
    /// Interaction logic for ProxyEditorWindow.xaml
    /// </summary>
    public partial class ProxyEditorWindow : DXWindow
    {
        public ProxyEditorWindow( )
        {
            InitializeComponent( );
        }

        public ProxySettings ProxySettings
        {
            get
            {
                return ( ProxySettings ) this.PropGrid.SelectedObject;
            }
            set
            {
                this.PropGrid.SelectedObject = value;
            }
        }
    }
}
