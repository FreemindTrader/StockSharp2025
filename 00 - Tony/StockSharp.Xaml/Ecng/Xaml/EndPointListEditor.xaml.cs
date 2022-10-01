// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.EndPointListEditor
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using DevExpress.Xpf.Editors;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Ecng.Common;

using System.Globalization;

using System.Windows.Data;
namespace Ecng.Xaml
{
    /// <summary>
    /// </summary>
    /// <summary>EndPointListEditor</summary>
    public partial class EndPointListEditor : UserControl
    {
        public static readonly DependencyProperty EndPointsProperty = DependencyProperty.Register( nameof( EndPoints ), typeof( IEnumerable<EndPoint> ), typeof( EndPointListEditor ), new PropertyMetadata( null ) );

        public EndPointListEditor()
        {
            InitializeComponent();
        }

        public IEnumerable<EndPoint> EndPoints
        {
            get
            {
                return ( IEnumerable<EndPoint> )this.GetValue( EndPointListEditor.EndPointsProperty );
            }
            set
            {
                this.SetValue( EndPointListEditor.EndPointsProperty, ( object )value );
            }
        }
    }

    public class EndPointListConverter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            IEnumerable<EndPoint> source = value as IEnumerable<EndPoint>;
            if ( source == null )
            {
                return ( object )null;
            }

            return ( object )source.Select<EndPoint, string>( ( Func<EndPoint, string> )( e => e.To<string>() ) ).Join( "," );
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            return value.To<string>().SplitByComma(false).Select(  s => s.To<EndPoint>() ).ToArray<EndPoint>();
        }
    }
}