using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using Ecng.Common;
using StockSharp.BusinessEntities;
using StockSharp.Messages;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;

namespace StockSharp.Hydra.Windows
{
    public partial class SecurityFileFormatWindow : ThemedWindow, IComponentConnector
    {        
        public SecurityFileFormatWindow()
        {
            InitializeComponent();
        }

        public string FileFormat
        {
            get
            {
                return FileFormatCtrl.Text;
            }
            set
            {
                FileFormatCtrl.Text = value;
            }
        }

        private void FileFormatCtrl_OnEditValueChanged( object sender, EditValueChangedEventArgs e )
        {
            if ( FileFormat.IsEmpty() )
            {
                OkBtn.IsEnabled = false;
            }
            else
            {
                try
                {
                    PreviewCtrl.Text = FileFormat.PutEx( new
                    {
                        Security = new Security()
                        {
                            Id = "AAPL@NASDAQ",
                            Code = "AAPL",
                            Board = ExchangeBoard.Nasdaq,
                            Type = new SecurityTypes?( SecurityTypes.Stock )
                        },
                        From = new DateTime( 2010, 3, 1 ),
                        To = DateTime.Today
                    } );
                    OkBtn.IsEnabled = true;
                }
                catch
                {
                    OkBtn.IsEnabled = false;
                }
            }
        }        
    }
}
