using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;
using Ecng.Xaml;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;

namespace StockSharp.Xaml.PropertyGrid
{
    public partial class ExtensionInfoEditor : ButtonEditSettings
    {
        public ExtensionInfoEditor( )
        {
            InitializeComponent();
        }

        private void EditBtn_Click( object sender, RoutedEventArgs e )
        {
            var ownerEdit = BaseEdit.GetOwnerEdit((DependencyObject) sender);
            if ( ownerEdit == null )
                return;
            XamlHelper.ShowModal( ( Window ) new ExtensionInfoWindow()
            {
                Data = ( IDictionary<object, object> ) ownerEdit.EditValue
            }, ( DependencyObject ) this );
        }
    }
}
