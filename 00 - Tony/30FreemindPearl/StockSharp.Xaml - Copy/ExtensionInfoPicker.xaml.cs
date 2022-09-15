
using Ecng.Xaml;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    public partial class ExtensionInfoPicker : Button
    {
        public static readonly DependencyProperty SelectedExtensionInfoProperty = DependencyProperty.Register(nameof (SelectedExtensionInfo), typeof (IDictionary<object, object>), typeof (ExtensionInfoPicker), (PropertyMetadata) new FrameworkPropertyMetadata((object) null, new PropertyChangedCallback(ExtensionInfoPicker.CallBack)));

        private static void CallBack( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            
        }

        public ExtensionInfoPicker()
        {
            InitializeComponent();
        }

        public IDictionary<object, object> SelectedExtensionInfo
        {
            get
            {
                return ( IDictionary<object, object> ) this.GetValue( ExtensionInfoPicker.SelectedExtensionInfoProperty );
            }
            set
            {
                this.SetValue( ExtensionInfoPicker.SelectedExtensionInfoProperty, ( object ) value );
            }
        }

        private void Button_Click( object sender, RoutedEventArgs e )
        {
            XamlHelper.ShowModal( ( Window ) new ExtensionInfoWindow()
            {
                Data = this.SelectedExtensionInfo
            }, ( DependencyObject ) this );
        }
    }
}
