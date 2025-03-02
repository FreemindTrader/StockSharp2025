using DevExpress.Xpf.Core;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Data;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.Algo.Export;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Hydra.Core;
using StockSharp.Hydra.Windows;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using StockSharp.Studio.Core.Configuration;
using StockSharp.Xaml;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Windows.Threading;

namespace StockSharp.Hydra.Controls
{
    public partial class ExportProgress : StatusBar, IComponentConnector
    {
        private BackgroundWorker _worker;
        private ExportButton _button;
        private Grid _mainGrid;
        private string _fileName;
        private int _totalCount;

        public ExportProgress()
        {
            InitializeComponent();
        }

        public bool IsStarted
        {
            get
            {
                return _worker != null;
            }
        }

        public void Init( ExportButton button, Grid mainGrid )
        {
            ExportButton exportButton = button;
            if ( exportButton == null )
                throw new ArgumentNullException( nameof( button ) );
            _button = exportButton;
            Grid grid = mainGrid;
            if ( grid == null )
                throw new ArgumentNullException( nameof( mainGrid ) );
            _mainGrid = grid;
        }

        public void Start(
          Security[ ] securities,
          DataType dataType,
          Func<SecurityId, DateTime?, DateTime?, IEnumerable> getValues,
          int valuesCount,
          object path,
          DateTime? from,
          DateTime? to,
          bool formatFileName,
          string fileFormat,
          StorageFormats format )
        {
            if ( dataType == null )
                throw new ArgumentNullException( nameof( dataType ) );
            if ( getValues == null )
                throw new ArgumentNullException( nameof( getValues ) );
            if ( securities == null )
                securities = new Security[1];
            int currProgress = 5;
            int valuesPerPercent = ( valuesCount / ( 100 - currProgress ) ).Max( 1 );
            int valuesProcessed = 0;
            int prevValuesProcessed = 0;
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = ( TimeSpan.FromMilliseconds( 500.0 ) );
            DispatcherTimer timer = dispatcherTimer;
            timer.Tick += ( sender, args ) =>
            {
                int count = valuesProcessed;
                if ( prevValuesProcessed >= count )
                    return;
                UpdateCount( count );
                prevValuesProcessed = count;
            };
            IStorageRegistry storageRegistry = ServicesRegistry.StorageRegistry;
            Dictionary<BaseExporter, IEnumerable> exporters = new Dictionary<BaseExporter, IEnumerable>();
            foreach ( Security security in securities )
            {
                object obj = path;
                if ( formatFileName )
                    obj = Path.Combine( ( string )obj, security.GetFileName( null, fileFormat, dataType, from, to, _button.ExportType, format ) );
                BaseExporter key;
                switch ( _button.ExportType )
                {
                    case ExportTypes.Excel:
                        key = new ExcelExporter( ServicesRegistry.ExcelProvider, dataType, new Func<int, bool>( IsCancelled ), ( string )obj, () => GuiDispatcher.GlobalDispatcher.AddSyncAction( new Action( TooManyValues ) ) );
                        break;
                    case ExportTypes.Xml:
                        key = new XmlExporter( dataType, new Func<int, bool>( IsCancelled ), ( string )obj );
                        break;
                    case ExportTypes.Txt:
                        HydraCommonSettings settings = MainWindow.Settings;
                        TemplateTxtRegistry templateTxtRegistry = settings.TemplateTxtRegistry;
                        string header = string.Empty;
                        string str = templateTxtRegistry.GetTemplate( dataType, security == null );
                        if ( !templateTxtRegistry.DoNotShowAgain && exporters.Count == 0 )
                        {
                            ExportTxtPreviewWindow wnd = new ExportTxtPreviewWindow() { DataType = dataType, Values = getValues( security != null ? security.ToSecurityId( null, true, false ) : new SecurityId(), from, to ), TxtTemplate = str };
                            if ( !wnd.ShowModal( this ) )
                                return;
                            str = wnd.TxtTemplate;
                            header = wnd.TxtHeader;
                            templateTxtRegistry.SetTemplate( dataType, security == null, str );
                            templateTxtRegistry.DoNotShowAgain = wnd.DoNotShowAgain;
                            BaseUserConfig<StudioUserConfig>.Instance.SetSettings( settings );
                        }
                        key = new TextExporter( dataType, new Func<int, bool>( IsCancelled ), ( string )obj, str, header );
                        break;

                    //case ExportTypes.Sql:
                        //key = ( BaseExporter )new DatabaseExporter( security?.PriceStep, security?.VolumeStep, dataType, new Func<int, bool>( IsCancelled ), ( DatabaseConnectionPair )obj )
                        //{
                        //    CheckUnique = false
                        //};
                      //  break;
                    case ExportTypes.Json:
                        key = new JsonExporter( dataType, new Func<int, bool>( IsCancelled ), ( string )obj );
                        break;
                    case ExportTypes.StockSharp:
                        IMarketDataDrive drive = ( IMarketDataDrive )obj;
                        key = new StockSharpExporter( dataType, new Func<int, bool>( IsCancelled ), storageRegistry, drive, format );
                        break;
                    case ExportTypes.SaveBuild:
                        key = new StockSharpExporter( dataType, new Func<int, bool>( IsCancelled ), storageRegistry, ( IMarketDataDrive )obj, format );
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                exporters.Add( key, getValues( security != null ? security.ToSecurityId( null, true, false ) : new SecurityId(), from, to ) );
            }
            InitWorker( valuesCount, path as string ?? ( path as LocalMarketDataDrive )?.Path );
            _worker.DoWork += ( s, e ) =>
            {
                _worker.ReportProgress( currProgress );
                timer.Start();
                try
                {
                    foreach ( KeyValuePair<BaseExporter, IEnumerable> keyValuePair in exporters )
                        keyValuePair.Key.Export( keyValuePair.Value );
                }
                finally
                {
                    timer.Stop();
                }
                _worker.ReportProgress( 100 );
                Thread.Sleep( 500 );
                _worker.ReportProgress( 0 );
            };
            _worker.RunWorkerAsync();

            bool IsCancelled( int count )
            {
                int num = _worker.CancellationPending ? 1 : 0;
                if ( num != 0 )
                    return num != 0;
                valuesProcessed += count;
                if ( valuesProcessed / valuesPerPercent <= currProgress )
                    return num != 0;
                currProgress = valuesProcessed / valuesPerPercent;
                _worker.ReportProgress( currProgress );
                return num != 0;
            }
        }

        public void Start(
          Security[ ] securities,
          LocalMarketDataDrive sourceDrive,
          LocalMarketDataDrive destDrive,
          DateTime? from,
          DateTime? to,
          DataType dataType,
          StorageFormats format )
        {
            if ( securities.IsDefault() )
                throw new ArgumentNullException( nameof( securities ) );
            if ( sourceDrive == null )
                throw new ArgumentNullException( nameof( sourceDrive ) );
            if ( destDrive == null )
                throw new ArgumentNullException( nameof( destDrive ) );
            if ( dataType == null )
                throw new ArgumentNullException( nameof( dataType ) );
            InitWorker( securities.Length, destDrive.Path );
            _worker.DoWork += ( s, e ) =>
            {
                try
                {
                    foreach ( Security security in securities )
                    {
                        IMarketDataStorage storage = ServicesRegistry.StorageRegistry.GetStorage( security, dataType, sourceDrive, format );
                        DateTime[ ] array1 = storage.Dates.ToArray();
                        if ( array1.Length != 0 )
                        {
                            DateTime? nullable = from;
                            DateTime from1 = nullable ?? array1.First();
                            nullable = to;
                            DateTime to1 = nullable ?? array1.Last();
                            TimeSpan interval = TimeSpan.FromDays( 1.0 );
                            IEnumerable<DateTime> second = from1.Range( to1, interval );
                            DateTime[ ] array2 = storage.Dates.Intersect( second ).ToArray();
                            string fileName = LocalMarketDataDrive.GetFileName( dataType, new StorageFormats?( format ), true );
                            _worker.ReportProgress( 0 );
                            int currentValuesCount = 0;
                            Directory.CreateDirectory( destDrive.Path );
                            SecurityId securityId = security.ToSecurityId( null, true, false );
                            string securityPath1 = sourceDrive.GetSecurityPath( securityId );
                            string securityPath2 = destDrive.GetSecurityPath( securityId );
                            foreach ( DateTime date in array2 )
                            {
                                string dirName = LocalMarketDataDrive.GetDirName( date );
                                string str1 = Path.Combine( securityPath1, dirName, fileName );
                                if ( File.Exists( str1 ) )
                                {
                                    string str2 = Path.Combine( securityPath2, dirName );
                                    string destFileName = Path.Combine( str2, fileName );
                                    Directory.CreateDirectory( str2 );
                                    File.Copy( str1, destFileName, true );
                                }
                            }
                            currentValuesCount++;
                            _worker.ReportProgress( ( int )Math.Round( currentValuesCount * new Decimal( 100 ) / securities.Length ) );
                            this.GuiAsync( () => UpdateCount( currentValuesCount ) );
                        }
                    }
                }
                finally
                {
                    _worker.ReportProgress( 100 );
                    Thread.Sleep( 500 );
                    _worker.ReportProgress( 0 );
                }
            };
            _worker.RunWorkerAsync();
        }

        public void Stop()
        {
            _worker?.CancelAsync();
        }

        private void InitWorker( int totalCount, string fileName )
        {
            ClearStatus();
            _totalCount = totalCount;
            StopBtn.Visibility = Visibility.Visible;
            OpenFilePanel.Visibility = Visibility.Collapsed;
            _fileName = fileName;
            _mainGrid.IsEnabled = false;
            _worker = new BackgroundWorker()
            {
                WorkerSupportsCancellation = true,
                WorkerReportsProgress = true
            };
            _worker.RunWorkerCompleted += ( s, e ) =>
            {
                if ( e.Error != null )
                {
                    e.Error.LogError( null );
                    StopBtn.Visibility = Visibility.Collapsed;
                    ProgressBar.Value = 0.0;
                }
                _mainGrid.IsEnabled = true;
                ProgressBarPanel.Visibility = Visibility.Collapsed;
                if ( _fileName != null && ( e.Error == null || File.Exists( _fileName ) || Directory.Exists( _fileName ) ) )
                {
                    OpenFilePanel.Visibility = Visibility.Visible;
                    OpenFileTitle.Text = _fileName.IsDirectory() ? LocalizedStrings.Str2916 : LocalizedStrings.XamlStr416;
                }
                _worker = null;
            };
            _worker.ProgressChanged += ( s, e ) =>
            {
                ProgressBarPanel.Visibility = Visibility.Visible;
                ProgressBar.Value = e.ProgressPercentage;
            };
        }

        public void ClearStatus()
        {
            StatusText.Text = string.Empty;
            ProgressText.Text = string.Empty;
            StatusTextPanel.Visibility = Visibility.Collapsed;
            ProgressBarPanel.Visibility = Visibility.Collapsed;
        }

        private void TooManyValues()
        {
            UpdateStatus( LocalizedStrings.Str2911 );
        }

        private void UpdateCount( int count )
        {
            UpdateCount( count, LocalizedStrings.Str2912Params );
        }

        private void UpdateCount( int count, string format )
        {
            ProgressText.Text = format.Put( count, _totalCount );
            StatusTextPanel.Visibility = Visibility.Collapsed;
            ProgressBarPanel.Visibility = Visibility.Visible;
        }

        public void DoesntExist()
        {
            UpdateStatus( LocalizedStrings.Str2913 );
        }

        private void UpdateStatus( string status )
        {
            StatusText.Text = status;
            StatusTextPanel.Visibility = Visibility.Visible;
            ProgressBarPanel.Visibility = Visibility.Collapsed;
        }

        public void Load<T>( IEnumerable<T> source, Action<IEnumerable<T>> addValues, int maxValueCount, Action<T[ ]> itemsLoaded = null, string statusTextFormat = null )
        {
            InitWorker( maxValueCount, null );
            UpdateCount( 0, statusTextFormat ?? LocalizedStrings.Str2912Params );
            _worker.DoWork += ( sender, args ) =>
            {
                int count = 0;
                TimeSpan timeOut = TimeSpan.FromMilliseconds( 100.0 );
                foreach ( IEnumerable<T> source1 in source.Batch( 50, x => x, () => _worker.CancellationPending ) )
                {
                    if ( !_worker.CancellationPending )
                    {
                        T[ ] values = source1.ToArray();
                        count += values.Length;
                        this.GuiSync( () =>
               {
                   addValues( values );
                   UpdateCount( count, statusTextFormat ?? LocalizedStrings.Str2912Params );
                   Action<T[ ]> action = itemsLoaded;
                   if ( action == null )
                       return;
                   action( values );
               } );
                        if ( count > maxValueCount )
                        {
                            if ( !this.GuiSync( ( () =>
                      {
                          ChangeLimitWindow wnd = new ChangeLimitWindow() { Limit = maxValueCount };
                          if ( wnd.ShowModal( this ) )
                          {
                              _totalCount = maxValueCount = wnd.Limit;
                              UpdateCount( count, statusTextFormat ?? LocalizedStrings.Str2912Params );
                              return true;
                          }
                          TooManyValues();
                          return false;
                      } ) ) ) 
                                break;
                        }
                        timeOut.Sleep();
                    }
                    else
                        break;
                }
                if ( count != 0 )
                    return;
                this.GuiSync( new Action( DoesntExist ) );
            };
            _worker.RunWorkerAsync();
        }

        private void StopBtn_Click( object sender, RoutedEventArgs e )
        {
            Stop();
        }

        private void OpenFileBtn_OnClick( object sender, RoutedEventArgs e )
        {
            _fileName.TryOpenLink( this );
        }


    }
}
