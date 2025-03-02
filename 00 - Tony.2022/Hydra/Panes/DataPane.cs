// Decompiled with JetBrains decompiler
// Type: StockSharp.Hydra.Panes.DataPane
// Assembly: Hydra, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3BCB1262-CD24-4283-B1AE-24E756F47247
// Assembly location: T:\00-StockSharp\Data\Hydra.dll

using DevExpress.Xpf.Dialogs;
using DevExpress.Xpf.Editors;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
using Microsoft.CSharp.RuntimeBinder;
using StockSharp.Algo;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Hydra.Controls;
using StockSharp.Hydra.Core;
using StockSharp.Hydra.Windows;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Studio.Core;
using StockSharp.Studio.Core.Commands;
using StockSharp.Studio.Core.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace StockSharp.Hydra.Panes
{
    public abstract class DataPane : UserControl, IPane, IStudioControl, IPersistable, IDisposable, INotifyPropertyChanged
    {
        private string _titlePrefix;
        private ExportButton _exportBtn;
        private Func<SecurityId, DateTime?, DateTime?, bool, IEnumerable> _getItems;
        private DateEdit _from;
        private DateEdit _to;
        private DrivePanel _drivePanel;
        private SecurityPickerButton _securityPickerButton;
        private PropertyChangedEventHandler _propertyChanged;

        public virtual Security SelectedSecurity
        {
            get
            {
                IEnumerable<Security> selectedSecurities = SelectedSecurities;
                if ( selectedSecurities == null )
                    return null;
                return selectedSecurities.FirstOrDefault();
            }
            set
            {
                if ( _securityPickerButton == null )
                    return;
                _securityPickerButton.SelectedSecurity = value;
            }
        }

        protected bool IsSingleSecurity
        {
            get
            {
                return _securityPickerButton.IsSingleSecurity;
            }
            set
            {
                _securityPickerButton.IsSingleSecurity = value;
            }
        }

        protected virtual IEnumerable<Security> SelectedSecurities
        {
            get
            {
                return _securityPickerButton?.SelectedSecurities;
            }
            private set
            {
                _securityPickerButton.SelectedSecurities = value;
            }
        }

        protected abstract DataType DataType { get; }

        protected static IStorageRegistry StorageRegistry
        {
            get
            {
                return ServicesRegistry.StorageRegistry;
            }
        }

        protected static ISecurityProvider SecurityProvider
        {
            get
            {
                return ServicesRegistry.SecurityProvider;
            }
        }

        protected static IBasketSecurityProcessorProvider ProcessorProvider
        {
            get
            {
                return BaseUserConfig<StudioUserConfig>.Instance.ProcessorProvider;
            }
        }

        protected IEnumerable<SecurityId> SelectedSecurityIds
        {
            get
            {
                return SelectedSecurities.Select( s => s.ToSecurityId( null, true, false ) );
            }
        }

        protected Security GetSecurity( SecurityId securityId )
        {
            return SecurityProvider.LookupById( securityId );
        }

        protected IEnumerable<SecurityId> GetInnerSecurityIds(
          SecurityId securityId,
          out BasketSecurity basket )
        {
            basket = GetSecurity( securityId ).ToBasket( ProcessorProvider );
            return basket.InnerSecurityIds;
        }

        public virtual string Title
        {
            get
            {
                if ( _securityPickerButton?.SelectedSecurity == null )
                    return _titlePrefix;
                return _titlePrefix + " " + _securityPickerButton.Title;
            }
            set
            {
            }
        }

        protected void UpdateTitle()
        {
            PropertyChangedEventHandler propertyChanged = _propertyChanged;
            if ( propertyChanged == null )
                return;
            propertyChanged.Invoke( this, "Title" );
        }

        Uri IStudioControl.Icon
        {
            get
            {
                return null;
            }
        }

        bool IStudioControl.SaveWithLayout
        {
            get
            {
                return true;
            }
        }

        bool IStudioControl.IsTitleEditable
        {
            get
            {
                return false;
            }
        }

        void IStudioControl.FirstTimeInit()
        {
        }

        void IStudioControl.SendCommand( IStudioCommand command )
        {
        }

        public string Key { get; set; }

        public virtual bool InProcess
        {
            get
            {
                return Progress.IsStarted;
            }
        }

        public virtual bool IsValid
        {
            get
            {
                return true;
            }
        }

        public DateTime? From
        {
            get
            {
                DateTime? editValue = ( DateTime? )_from.EditValue;
                ref DateTime? local = ref editValue;
                if ( !local.HasValue )
                    return new DateTime?();
                return new DateTime?( local.GetValueOrDefault().UtcKind() );
            }
            set
            {
                _from.EditValue = value;
            }
        }

        public DateTime? To
        {
            get
            {
                DateTime? editValue = ( DateTime? )_to.EditValue;
                ref DateTime? local = ref editValue;
                if ( !local.HasValue )
                    return new DateTime?();
                return new DateTime?( local.GetValueOrDefault().UtcKind().EndOfDay() );
            }
            set
            {
                _to.EditValue = value;
            }
        }

        public IMarketDataDrive Drive
        {
            get
            {
                return _drivePanel.SelectedDrive;
            }
            set
            {
                _drivePanel.SelectedDrive = value;
            }
        }

        public StorageFormats StorageFormat
        {
            get
            {
                return _drivePanel.StorageFormat;
            }
            set
            {
                _drivePanel.StorageFormat = value;
            }
        }

        private ExportProgress Progress => ( ( dynamic )this ).Progress;

        protected void Init(
            ExportButton exportBtn,
            Grid mainGrid,
            SecurityPickerButton securityPickerButton,
            Func<SecurityId, DateTime?, DateTime?, bool, IEnumerable> getItems )
        {
            if ( mainGrid == null )
            {
                throw new ArgumentNullException( nameof( mainGrid ) );
            }

            if ( getItems == null )
            {
                throw new ArgumentNullException( nameof( getItems ) );
            }


            if ( exportBtn == null )
            {
                throw new ArgumentNullException( nameof( exportBtn ) );
            }

            _getItems = getItems;
            _exportBtn = exportBtn;

            Key = GetType().CreateKey();

            _exportBtn.ExportStarted += new Action( ExportBtnOnExportStarted );

            dynamic ctrl = this;
            _from = ctrl.FromCtrl;
            _to = ctrl.ToCtrl;
            _drivePanel = ctrl.DrivePanel;

            Progress.Init( _exportBtn, mainGrid );

            From = new DateTime?( DateTime.Today - TimeSpan.FromDays( 7.0 ) );
            To = new DateTime?( DateTime.Today + TimeSpan.FromDays( 1.0 ) );

            _securityPickerButton = securityPickerButton;
            Margin = new Thickness( 5.0, 5.0, 5.0, 5.0 );
        }

        protected void SetTitlePrefix( string prefix )
        {
            if ( prefix.IsEmpty() )
                throw new ArgumentNullException( nameof( prefix ) );
            _titlePrefix = prefix;
            UpdateTitle();
            if ( _securityPickerButton == null )
                return;
            _securityPickerButton.SecuritySelected += new Action( SecurityPickerButton_OnSecuritySelected );
        }

        private void SecurityPickerButton_OnSecuritySelected()
        {
            UpdateTitle();
            TryEnabledExport();
        }

        protected virtual bool IsExportEnabled()
        {
            return SelectedSecurity != null;
        }

        protected void TryEnabledExport()
        {
            _exportBtn.IsEnabled = IsExportEnabled();
        }

        protected virtual bool ValidateSettings()
        {
            Security selectedSecurity = SelectedSecurity;
            if ( selectedSecurity != null )
            {
                if ( From.HasValue && To.HasValue && From.Value > To.Value )
                {
                    int num = ( int )new MessageBoxBuilder().Text( LocalizedStrings.Str1014.Put( From.Value ) ).Warning().Owner( this ).Show();
                    return false;
                }
                if ( !IsFromComposite || selectedSecurity.IsBasket() )
                    return true;
                int num1 = ( int )new MessageBoxBuilder().Text( LocalizedStrings.NotCompositeSecurity.Put( selectedSecurity.Id ) ).Warning().Owner( this ).Show();
                return false;
            }
            int num2 = ( int )new MessageBoxBuilder().Text( LocalizedStrings.Str2875 ).Warning().Owner( this ).Show();
            return false;
        }

        protected virtual bool IsFromComposite
        {
            get
            {
                return false;
            }
        }

        protected virtual IEnumerable<Security> FilterSecurities(
          IEnumerable<Security> securities )
        {
            return securities;
        }

        protected IEnumerable<T> GetValues<T>( SecurityId securityId )
        {
            return _getItems( securityId, From, To, false ).Cast<T>();
        }

        protected IEnumerable<T> GetAllValues<T>(
          DateTime? from = null,
          DateTime? to = null,
          bool exporting = false )
        {
            if ( !from.HasValue )
                from = From;
            DateTime? nullable1 = to;
            DateTime? nullable2 = nullable1.HasValue ? nullable1 : To;
            ref DateTime? local = ref nullable2;
            to = local.HasValue ? new DateTime?( local.GetValueOrDefault().EndOfDay() ) : new DateTime?();
            if ( _securityPickerButton == null )
                return _getItems( new SecurityId(), from, to, exporting ).Cast<T>();
            IEnumerable<SecurityId> source = FilterSecurities( SelectedSecurities ).Select( s => s.ToSecurityId( null, true, false ) );
            if ( exporting )
                return source.SelectMany( security => _getItems( security, from, to, exporting ).Cast<T>() );
            return source.Select( security => _getItems( security, from, to, exporting ).Cast<T>() ).ToArray().SelectMany();
        }

        protected virtual bool CanDirectExport
        {
            get
            {
                return true;
            }
        }

        protected virtual void ExportBtnOnExportStarted()
        {
            if ( !ValidateSettings() )
                return;
            DateTime? from = From;
            DateTime? to1 = To;
            ref DateTime? local = ref to1;
            DateTime? to2 = local.HasValue ? new DateTime?( local.GetValueOrDefault().EndOfDay() ) : new DateTime?();
            if ( !GetAllValues<object>( from, to2, true ).Any() )
            {
                Progress.DoesntExist();
            }
            else
            {
                StorageFormats format = StorageFormat;
                IEnumerable<Security> source = FilterSecurities( SelectedSecurities );
                Security[ ] securities = ( source != null ? source.ToArray() : null ) ?? new Security[1];
                string fileFormat = string.Empty;
                bool formatFileName = false;
                object path;
                if ( securities.Length > 1 )
                {
                    path = null;
                    switch ( _exportBtn.ExportType )
                    {
                        case ExportTypes.Excel:
                        case ExportTypes.Xml:
                        case ExportTypes.Txt:
                        case ExportTypes.Json:
                            DXFolderBrowserDialog dlg = new DXFolderBrowserDialog();
                            if ( dlg.ShowModal( this ) )
                            {
                                path = dlg.SelectedPath;
                                formatFileName = true;
                                SecurityFileFormatWindow wnd = new SecurityFileFormatWindow() { FileFormat = Core.Extensions.GetFileFormat( null, DataType, from, to2, _exportBtn.ExportType, format ) };
                                if ( wnd.ShowModal( this ) )
                                {
                                    fileFormat = wnd.FileFormat;
                                    break;
                                }
                                break;
                            }
                            break;
                        case ExportTypes.Sql:
                            DatabaseConnectionWindow wnd1 = new DatabaseConnectionWindow();
                            if ( wnd1.ShowModal( this ) )
                            {
                                path = wnd1.Pair;
                                break;
                            }
                            break;
                        case ExportTypes.StockSharp:
                        case ExportTypes.SaveBuild:
                            SelectDriveWindow wnd2 = new SelectDriveWindow() { SelectedDrive = Drive, SelectedFormat = format };
                            if ( wnd2.ShowModal( this ) )
                            {
                                format = wnd2.SelectedFormat;
                                path = wnd2.SelectedDrive;
                                break;
                            }
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else
                    path = _exportBtn.GetPath( securities[0], null, DataType, From, To, Drive, ref format );
                if ( path == null )
                    return;
                if ( ( !CanDirectExport || format != StorageFormat || !( path is LocalMarketDataDrive ) ? 0 : ( Drive is LocalMarketDataDrive ? 1 : 0 ) ) != 0 )
                {
                    if ( ( ( IMarketDataDrive )path ).Path.ComparePaths( Drive.Path ) )
                    {
                        int num = ( int )new MessageBoxBuilder().Text( LocalizedStrings.Str2876 ).Error().Owner( this ).Show();
                    }
                    else
                        Progress.Start( securities, ( LocalMarketDataDrive )Drive, ( LocalMarketDataDrive )path, from, to2, DataType, format );
                }
                else
                    Progress.Start( securities, DataType, ( secId, f, t ) => _getItems( secId, f, t, true ), int.MaxValue, path, from, to2, formatFileName, fileFormat, format );
            }
        }

        public virtual void Load( SettingsStorage storage )
        {
            Key = storage.GetValue( "Key", Key );
            ISecurityProvider securityProvider = SecurityProvider;
            if ( storage.ContainsKey( "SelectedSecurities" ) )
            {
                SettingsStorage settingsStorage = storage.GetValue<SettingsStorage>( "SelectedSecurities", null );
                List<Security> securityList = new List<Security>();
                foreach ( string id in ( IEnumerable<object> )settingsStorage.Values )
                {
                    Security security = securityProvider.LookupById( id );
                    if ( security != null )
                        securityList.Add( security );
                }
                SelectedSecurities = securityList;
            }
            else if ( storage.ContainsKey( "SelectedSecurity" ) )
                SelectedSecurity = securityProvider.LookupById( storage.GetValue<string>( "SelectedSecurity", null ) );
            From = storage.GetValue( "From", new DateTime?() );
            To = storage.GetValue( "To", new DateTime?() );
            if ( storage.ContainsKey( "Drive" ) )
                Drive = ServicesRegistry.DriveCache.GetDrive( storage.GetValue<string>( "Drive", null ) );
            StorageFormat = storage.GetValue( "StorageFormat", StorageFormats.Binary );
        }

        public virtual void Save( SettingsStorage storage )
        {
            storage.SetValue( "Key", Key );
            if ( _securityPickerButton != null )
            {
                SettingsStorage settingsStorage = new SettingsStorage();
                int num = 0;
                foreach ( Security selectedSecurity in SelectedSecurities )
                {
                    settingsStorage.SetValue( num.To<string>(), selectedSecurity.Id );
                    ++num;
                }
                storage.SetValue( "SelectedSecurities", settingsStorage );
            }
            if ( From.HasValue )
                storage.SetValue( "From", From.Value );
            if ( To.HasValue )
                storage.SetValue( "To", To.Value );
            if ( Drive != null )
                storage.SetValue( "Drive", Drive.Path );
            storage.SetValue( "StorageFormat", StorageFormat );
        }

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
                _propertyChanged += value;
            }
            remove
            {
                _propertyChanged -= value;
            }
        }

        void IDisposable.Dispose()
        {
            Progress.Stop();
        }
    }
}
