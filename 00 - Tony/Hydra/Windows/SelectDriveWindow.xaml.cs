using DevExpress.Xpf.Core;
using StockSharp.Algo.Storages;
using StockSharp.Hydra.Controls;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;

namespace StockSharp.Hydra.Windows
{
    public partial class SelectDriveWindow : ThemedWindow, IComponentConnector
    {
        public SelectDriveWindow()
        {
            InitializeComponent();
            DrivePanel_OnSelectedDriveChanged();
        }

        public IMarketDataDrive SelectedDrive
        {
            get
            {
                return DrivePanel.SelectedDrive;
            }
            set
            {
                DrivePanel.SelectedDrive = value;
            }
        }

        public StorageFormats SelectedFormat
        {
            get
            {
                return DrivePanel.StorageFormat;
            }
            set
            {
                DrivePanel.StorageFormat = value;
            }
        }

        private void DrivePanel_OnSelectedDriveChanged()
        {
            OkBtn.IsEnabled = SelectedDrive != null;
        }        
    }
}
