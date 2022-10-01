using DevExpress.Xpf.Core;
using StockSharp.Messages;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;

namespace StockSharp.Xaml.PropertyGrid
{
    public partial class WorkingTimeEditor : DropDownButton
    {
        public static readonly DependencyProperty WorkingTimeProperty = DependencyProperty.Register( nameof( WorkingTime ), typeof( WorkingTime ), typeof( WorkingTimeEditor ), new PropertyMetadata( ( PropertyChangedCallback )null ) );       

        public WorkingTimeEditor( )
        {
            this.InitializeComponent( );
        }

        public WorkingTime WorkingTime
        {
            get
            {
                return ( WorkingTime )this.GetValue( WorkingTimeEditor.WorkingTimeProperty );
            }
            set
            {
                this.SetValue( WorkingTimeEditor.WorkingTimeProperty, ( object )value );
            }
        }

        
    }
}
