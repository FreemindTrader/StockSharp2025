using DevExpress.Xpf.Core;
using DevExpress.Xpf.Dialogs;
using Ecng.Common;
using Ecng.Xaml;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Hydra.Core;
using StockSharp.Hydra.Windows;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace StockSharp.Hydra.Controls
{
    public partial class ExportButton : DropDownButton, IComponentConnector
    {        
        public ExportButton()
        {
            InitializeComponent();
        }

        public void SetTypeEnabled( ExportTypes type, bool isEnabled )
        {
            ( ( UIElement )ExportTypeCtrl.Items[( int )type] ).IsEnabled = isEnabled;
        }

        public ExportTypes ExportType { get; private set; }

        public event Action ExportStarted;

        private void ExportTypeCtrlSelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            ExportType = ( ExportTypes )ExportTypeCtrl.SelectedIndex;
            ExportTypeCtrl.SelectionChanged -= new SelectionChangedEventHandler( ExportTypeCtrlSelectionChanged );
            try
            {
                ExportTypeCtrl.SelectedIndex = -1;
                Action exportStarted = ExportStarted;
                if ( exportStarted == null )
                    return;
                exportStarted();
            }
            finally
            {
                ExportTypeCtrl.SelectionChanged += new SelectionChangedEventHandler( ExportTypeCtrlSelectionChanged );
            }
        }

        public object GetPath(
          Security security,
          string fileNamePrefix,
          DataType dataType,
          DateTime? from,
          DateTime? to,
          IMarketDataDrive drive,
          ref StorageFormats format )
        {
            string fileName = security.GetFileName( fileNamePrefix, null, dataType, from, to, ExportType, format );
            DXSaveFileDialog dxSaveFileDialog = new DXSaveFileDialog();
            dxSaveFileDialog.FileName = fileName;
            dxSaveFileDialog.RestoreDirectory = true;
            DXSaveFileDialog dlg = dxSaveFileDialog;
            switch ( ExportType )
            {
                case ExportTypes.Excel:
                    dlg.Filter = "xlsx files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                    break;
                case ExportTypes.Xml:
                    dlg.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
                    break;
                case ExportTypes.Txt:
                    dlg.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
                    break;
                case ExportTypes.Sql:
                    DatabaseConnectionWindow wnd1 = new DatabaseConnectionWindow();
                    if ( !wnd1.ShowModal( this ) )
                        return null;
                    return wnd1.Pair;
                case ExportTypes.Json:
                    dlg.Filter = "json files (*.json)|*.json|All files (*.*)|*.*";
                    break;
                case ExportTypes.StockSharp:
                case ExportTypes.SaveBuild:
                    SelectDriveWindow wnd2 = new SelectDriveWindow() { SelectedDrive = drive, SelectedFormat = format };
                    if ( !wnd2.ShowModal( this ) )
                        return null;
                    format = wnd2.SelectedFormat;
                    return wnd2.SelectedDrive;
                default:
                    int num = ( int )new MessageBoxBuilder().Error().Owner( this ).Text( LocalizedStrings.Str2910Params.Put( ExportType ) ).Show();
                    return null;
            }
            if ( !dlg.ShowModal( this ) )
                return null;
            return dlg.FileName;
        }        
    }
}
