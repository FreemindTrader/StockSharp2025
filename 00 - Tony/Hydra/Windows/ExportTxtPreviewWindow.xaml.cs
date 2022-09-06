using DevExpress.Xpf.Core;
using Ecng.Common;
using StockSharp.Algo;
using StockSharp.Messages;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace StockSharp.Hydra.Windows
{
    public partial class ExportTxtPreviewWindow : ThemedWindow, IComponentConnector
    {
        private string _defaultTemplate;
        
        public ExportTxtPreviewWindow()
        {
            InitializeComponent();
        }

        public DataType DataType { get; set; }

        public string TxtTemplate
        {
            get
            {
                return TxtTemplateCtrl.Text;
            }
            set
            {
                _defaultTemplate = TxtTemplateCtrl.Text = value;
            }
        }

        public string TxtHeader
        {
            get
            {
                return HeaderCtrl.Text;
            }
            set
            {
                HeaderCtrl.Text = value;
            }
        }

        public IEnumerable Values { get; set; }

        public bool DoNotShowAgain
        {
            get
            {
                bool? isChecked = DoNotShowAgainCheckBox.IsChecked;
                bool flag = true;
                return isChecked.GetValueOrDefault() == flag & isChecked.HasValue;
            }
        }

        private void TxtTemplateCtrl_OnTextChanged( object sender, TextChangedEventArgs e )
        {
            PreviewBtn.IsEnabled = OkBtn.IsEnabled = !TxtTemplate.IsEmpty();
        }

        private void PreviewBtn_OnClick( object sender, RoutedEventArgs e )
        {
            PreviewResult.Text = TxtHeader.IsEmpty() ? string.Empty : TxtHeader + Environment.NewLine;
            IEnumerable source = Values;
            if ( DataType == DataType.MarketDepth )
                source = ( ( IEnumerable<QuoteChangeMessage> )source ).ToTimeQuotes();
            PreviewResult.Text += source.Cast<object>().Take( 10 ).Select( v =>
            {
                try
                {
                    return TxtTemplate.PutEx( v );
                }
                catch ( Exception ex )
                {
                    return "##ERROR:" + ex.Message;
                }
            } ).Join( Environment.NewLine );
        }

        private void ResetTemplate_OnClick( object sender, RoutedEventArgs e )
        {
            TxtTemplate = _defaultTemplate;
        }        
    }
}
