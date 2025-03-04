using DevExpress.Xpf.Core;
using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.Algo.Import;
using StockSharp.Algo.Storages;
using StockSharp.Hydra.Controls;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using StockSharp.Studio.Controls;
using StockSharp.Studio.Core;
using StockSharp.Xaml;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Threading;

namespace StockSharp.Hydra.Panes
{
    public partial class ImportPane : BaseStudioControl, IPane, IStudioControl, IPersistable, IDisposable, IComponentConnector
    {
        private readonly BackgroundWorker _worker;
        
        public ImportPane()
        {
            InitializeComponent();
            _worker = new BackgroundWorker()
            {
                WorkerSupportsCancellation = true,
                WorkerReportsProgress = true
            };
            _worker.DoWork += new DoWorkEventHandler( OnDoWork );
            _worker.ProgressChanged += new ProgressChangedEventHandler( OnProgressChanged );
            _worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler( OnRunWorkerCompleted );
            SettingsPanel.Settings.PropertyChanged += new PropertyChangedEventHandler( SettingsOnPropertyChanged );
            UpdateTitle();
        }

        private void SettingsOnPropertyChanged( object sender, PropertyChangedEventArgs e )
        {
            if ( !( e.PropertyName == "DataType" ) )
                return;
            UpdateTitle();
        }

        private void UpdateTitle()
        {
            DataType dataType = SettingsPanel.Settings.DataType;
            if ( dataType == DataType.Securities )
                UpdateTitle( LocalizedStrings.Securities.ToLowerInvariant() );
            else if ( dataType == DataType.Ticks )
                UpdateTitle( LocalizedStrings.Str985.ToLowerInvariant() );
            else if ( dataType == DataType.OrderLog )
                UpdateTitle( LocalizedStrings.Str2858 );
            else if ( dataType == DataType.Transactions )
                UpdateTitle( LocalizedStrings.Transactions );
            else if ( dataType == DataType.MarketDepth )
                UpdateTitle( LocalizedStrings.Str2862 );
            else if ( dataType == DataType.Level1 )
                UpdateTitle( LocalizedStrings.Level1 );
            else if ( dataType == DataType.PositionChanges )
                UpdateTitle( LocalizedStrings.Str972 );
            else if ( dataType == DataType.News )
                UpdateTitle( LocalizedStrings.Str2863 );
            else if ( dataType == DataType.Board )
            {
                UpdateTitle( LocalizedStrings.Board );
            }
            else
            {
                if ( !dataType.IsCandles )
                    throw new ArgumentOutOfRangeException( "type", dataType, LocalizedStrings.Str1655 );
                UpdateTitle( LocalizedStrings.Str2859 );
            }

            void UpdateTitle( string title )
            {
                Title = LocalizedStrings.Str2864 + " '" + title + "'";
            }
        }

        public DataType DataType
        {
            get
            {
                return SettingsPanel.Settings.DataType;
            }
            set
            {
                SettingsPanel.Settings.DataType = value;
                UpdateTitle();
            }
        }

        public override void Load( SettingsStorage storage )
        {
            if ( storage.ContainsKey( "SelectedDrive" ) )
                DrivePanel.SelectedDrive = ServicesRegistry.DriveCache.GetDrive( storage.GetValue<string>( "SelectedDrive", null ) );
            DrivePanel.StorageFormat = storage.GetValue( "StorageFormat", StorageFormats.Binary );
            SettingsStorage panelSettings = storage.GetValue<SettingsStorage>( "SettingsPanel", null );
            if ( panelSettings != null )
                ( ( Action )( () => SettingsPanel.Load( panelSettings ) ) ).DoWithLog();
            if ( storage.ContainsKey( "ImportSettings" ) )
                SettingsPanel.Settings.Load( storage.GetValue<SettingsStorage>( "ImportSettings", null ) );
            base.Load( storage );
        }

        public override void Save( SettingsStorage storage )
        {
            if ( DrivePanel.SelectedDrive != null )
                storage.SetValue( "SelectedDrive", DrivePanel.SelectedDrive.Path );
            storage.SetValue( "StorageFormat", DrivePanel.StorageFormat );
            storage.SetValue( "SettingsPanel", SettingsPanel.Save() );
            storage.SetValue( "ImportSettings", SettingsPanel.Settings.Save() );
            base.Save( storage );
        }

        bool IPane.IsValid
        {
            get
            {
                return DataType != null;
            }
        }

        private void ImportBtn_OnClick( object sender, RoutedEventArgs e )
        {
            if ( _worker.IsBusy )
            {
                SetIsEnabled( false );
                _worker.CancelAsync();
            }
            else
            {
                if ( SettingsPanel.HasErrors() )
                    return;
                SetIsEnabled( false );
                ImportBtnText.Text = LocalizedStrings.Str2890;
                _worker.RunWorkerAsync();
            }
        }

        private void OnProgressChanged( object sender, ProgressChangedEventArgs e )
        {
            ProgressBar.Value = e.ProgressPercentage;
        }

        private void OnRunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e )
        {
            Exception error = e.Error;
            if ( error != null )
                error.LogError( null );
            ImportBtnText.Text = LocalizedStrings.Import;
            SetIsEnabled( true );
            ProgressBar.Value = 0.0;
        }

        private void OnDoWork( object sender, DoWorkEventArgs e )
        {
            IMarketDataDrive drive = null;
            StorageFormats format = StorageFormats.Binary;
            ImportSettings settings = null;
            FieldMapping[ ] fields = null;
            
            this.GuiSync( () =>
            {
                SetIsEnabled( true );
                drive = DrivePanel.SelectedDrive;
                format = DrivePanel.StorageFormat;
                settings = SettingsPanel.Settings;
                fields = SettingsPanel.SelectedFields.ToArray();
            } );

            if ( drive == null )
                drive = ServicesRegistry.DriveCache.DefaultDrive;
            CsvImporter csvImporter = new CsvImporter( settings.DataType, fields, ServicesRegistry.SecurityStorage, ServicesRegistry.ExchangeInfoProvider, drive, format );
            csvImporter.Parent = LogManager.Application;
            CsvImporter importer = csvImporter;
            settings.FillImporter( importer );
            Do.Invariant( () =>
            {
                foreach ( string file in settings.GetFiles() )
                    importer.Import( file, new Action<int>( _worker.ReportProgress ), () => _worker.CancellationPending );
            } );
            _worker.ReportProgress( 100 );
        }

        public override void Dispose()
        {
            _worker.CancelAsync();
            base.Dispose();
        }

        private void SetIsEnabled( bool value )
        {
            ImportBtn.IsEnabled = value;
        }        
    }
}
