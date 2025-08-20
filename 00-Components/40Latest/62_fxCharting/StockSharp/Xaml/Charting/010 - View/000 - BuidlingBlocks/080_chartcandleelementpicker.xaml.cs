// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartCandleElementPicker
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using Ecng.Xaml;
using StockSharp.Charting;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;

#nullable disable
namespace StockSharp.Xaml.Charting;

/// <summary>The window for select candle series element.</summary>
/// <summary>ChartCandleElementPicker</summary>
public partial class ChartCandleElementPicker : ThemedWindow
{
    private bool _someInternalBoolean;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Xaml.Charting.ChartCandleElementPicker" />.
    /// </summary>
    public ChartCandleElementPicker() => InitializeComponent();

    /// <summary>Elements.</summary>
    public IEnumerable<IChartCandleElement> Elements
    {
        get => throw new NotSupportedException();
        set
        {
            ElementsCtrl.SetItemsSource(value, p => ( (IChartComponent)p ).GetGeneratedTitle() );
        }
    }

    /// <summary>Selected element.</summary>
    public IChartCandleElement SelectedElement
    {
        get => ElementsCtrl.GetSelected<IChartCandleElement>();
        set => ElementsCtrl.SetSelected<IChartCandleElement>(value);
    }

    private void OnEditValueChanged(object obj, EditValueChangedEventArgs e)
    {
        Ok.IsEnabled = SelectedElement != null;
    }    
}


//using DevExpress.Xpf.Core;
//using Ecng.Common;
//using System;
//using System.Collections;
//using System.Collections.Generic; using fx.Collections;
//using System.Windows.Controls;

//namespace StockSharp.Xaml.Charting
//{
//    public partial class ChartCandleElementViewModelPicker : ThemedWindow
//    {


//        public ChartCandleElementViewModelPicker( )
//        {
//            InitializeComponent( );
//        }

//        public IEnumerable<ChartCandleElement> Elements
//        {
//            get
//            {
//                return ( IEnumerable<ChartCandleElement> )ElementsCtrl.ItemsSource;
//            }
//            set
//            {
//                ElementsCtrl.ItemsSource = value;
//            }
//        }

//        public ChartCandleElement SelectedElement
//        {
//            get
//            {
//                return ( ChartCandleElement )ElementsCtrl.SelectedItem;
//            }
//            set
//            {
//                ElementsCtrl.SelectedItem = value;
//            }
//        }

//        private void ElementsCtrl_SelectionChanged( object sender, SelectionChangedEventArgs e )
//        {
//            Ok.IsEnabled = SelectedElement != null;
//        }


//    }
//}
