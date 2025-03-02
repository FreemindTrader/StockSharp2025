using DevExpress.Xpf.Editors;
using Ecng.Common;
using Ecng.Serialization;
using StockSharp.Algo;
using StockSharp.Algo.Storages;
using StockSharp.Xaml;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace StockSharp.Hydra.Controls
{
    public partial class DrivePanel : UserControl, IPersistable, IComponentConnector
    {
        private readonly bool _initializing;
        
        public DrivePanel()
        {
            _initializing = true;
            InitializeComponent();
            SelectedDrive = ServicesRegistry.DriveCache.Drives.FirstOrDefault();
            _initializing = false;
        }

        public IMarketDataDrive SelectedDrive
        {
            get
            {
                return DriveCtrl.SelectedDrive;
            }
            set
            {
                DriveCtrl.SelectedDrive = value;
            }
        }

        public StorageFormats StorageFormat
        {
            get
            {
                return FormatCtrl.SelectedFormat;
            }
            set
            {
                FormatCtrl.SelectedFormat = value;
            }
        }

        public event Action SelectedDriveChanged;

        public event Action StorageFormatChanged;

        public event Action<IMarketDataDrive, bool> DriveChanged;

        private void DriveCtrl_OnChanged( IMarketDataDrive drive, bool isNew )
        {
            Action<IMarketDataDrive, bool> driveChanged = DriveChanged;
            if ( driveChanged == null )
                return;
            driveChanged( drive, isNew );
        }

        private void DriveCtrl_OnSelectionChanged( object sender, EditValueChangedEventArgs e )
        {
            if ( _initializing )
                return;
            Action selectedDriveChanged = SelectedDriveChanged;
            if ( selectedDriveChanged == null )
                return;
            selectedDriveChanged();
        }

        private void FormatCtrl_OnEditValueChanged( object sender, EditValueChangedEventArgs e )
        {
            if ( _initializing )
                return;
            Action storageFormatChanged = StorageFormatChanged;
            if ( storageFormatChanged == null )
                return;
            storageFormatChanged();
        }

        public void Load( SettingsStorage storage )
        {
            if ( storage.ContainsKey( "SelectedDrive" ) )
                SelectedDrive = ServicesRegistry.DriveCache.GetDrive( storage.GetValue<string>( "SelectedDrive", null ) );
            StorageFormat = storage.GetValue( "StorageFormat", StorageFormats.Binary );
        }

        public void Save( SettingsStorage storage )
        {
            if ( SelectedDrive != null )
                storage.SetValue( "SelectedDrive", SelectedDrive.Path );
            storage.SetValue( "StorageFormat", StorageFormat.To<string>() );
        }        
    }
}
