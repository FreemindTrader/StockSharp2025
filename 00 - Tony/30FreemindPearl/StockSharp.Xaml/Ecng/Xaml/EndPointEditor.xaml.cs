// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.EndPointEditor
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using DevExpress.Xpf.Editors;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Ecng.Xaml
{
    /// <summary>
    /// Editor for <see cref="P:Ecng.Xaml.EndPointEditor.EndPoint" />.
    /// </summary>
    /// <summary>EndPointEditor</summary>
    public partial class EndPointEditor : UserControl, IComponentConnector
    {
        /// <summary>
        /// <see cref="T:System.Windows.DependencyProperty" /> for <see cref="P:Ecng.Xaml.EndPointEditor.EndPoint" />.
        ///     </summary>
        public static readonly DependencyProperty EndPointProperty = DependencyProperty.Register( nameof( EndPoint ), typeof( EndPoint ), typeof( EndPointEditor ), new PropertyMetadata( ( object )null ) );


        /// <summary>
        /// Initializes a new instance of the <see cref="T:Ecng.Xaml.EndPointEditor" />.
        /// </summary>
        public EndPointEditor()
        {
            this.InitializeComponent();
        }

        /// <summary>Address.</summary>
        public EndPoint EndPoint
        {
            get
            {
                return ( EndPoint )this.GetValue( EndPointEditor.EndPointProperty );
            }
            set
            {
                this.SetValue( EndPointEditor.EndPointProperty, ( object )value );
            }
        }
    }
}


