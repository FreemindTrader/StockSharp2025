using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
using MoreLinq;
using StockSharp.Algo;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Candles.Compression;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Hydra.Controls;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Xaml;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Markup;

namespace StockSharp.Hydra.Panes
{
    public partial class Level1Pane : DataPane, IComponentConnector
    {
        private readonly Dictionary<Level1Fields, GridColumn> _columns = new Dictionary<Level1Fields, GridColumn>();
        private const string _fieldsKey = "SelectedLevel1Fields";

        public Level1Pane()
        {
            InitializeComponent();
            if ( this.IsDesignMode() )
                return;
            Init( ExportBtn, MainGrid, SelectSecurityBtn, new Func<SecurityId, DateTime?, DateTime?, bool, IEnumerable>( GetLevel1 ) );
            SetTitlePrefix( LocalizedStrings.Level1 );
            foreach ( GridColumn column in ( Collection<GridColumn> )FindedChanges.Columns )
            {
                Level1Fields key;
                if ( column.FieldName.TryParse( out key, true ) )
                    _columns.Add( key, column );
            }
            Level1FieldsCtrl.SelectedFields = Enumerator.GetValues<Level1Fields>().ExcludeObsolete();
        }

        protected override DataType DataType
        {
            get
            {
                return DataType.Level1;
            }
        }

        private IEnumerable<Level1ChangeMessage> GetLevel1(
          SecurityId securityId,
          DateTime? from,
          DateTime? to,
          bool exporting )
        {
            IEnumerable<Level1ChangeMessage> source = InternalGetLevel1( securityId, from, to );
            TimeZoneInfo tz = TimeZone.TimeZone;
            if ( tz != null )
                source = source.Select( m =>
                {
                    if ( !m.ServerTime.IsDefault() )
                        m.ServerTime = m.ServerTime.Convert( tz );
                    if ( !m.LocalTime.IsDefault() )
                        m.LocalTime = m.LocalTime.Convert( tz );
                    return m;
                } );
            return source;
        }

        private IEnumerable<Level1ChangeMessage> InternalGetLevel1(
          SecurityId securityId,
          DateTime? from,
          DateTime? to )
        {
            Level1Fields[ ] excludedTypes = Enumerator.GetValues<Level1Fields>().Except( Level1FieldsCtrl.SelectedFields ).ToArray();
            IMarketDataStorage<Level1ChangeMessage> level1MessageStorage = StorageRegistry.GetLevel1MessageStorage( securityId, Drive, StorageFormat );
            DateTime? nullable1 = from;
            DateTimeOffset? from1 = nullable1.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable1.GetValueOrDefault() ) : new DateTimeOffset?();
            DateTime? nullable2 = to;
            DateTimeOffset? to1 = nullable2.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable2.GetValueOrDefault() ) : new DateTimeOffset?();
            return level1MessageStorage.Load( from1, to1 ).Select( m =>
            {
                excludedTypes.ForEach( t => m.Changes.Remove( t ) );
                return m;
            } ).Where( m => m.Changes.Any() );
        }

        private bool CheckExportTypes()
        {
            if ( Level1FieldsCtrl.SelectedFields.Any() || CanDirectExport )
                return true;
            int num = ( int )new MessageBoxBuilder().Error().Owner( this ).Text( LocalizedStrings.Str2935 ).Show();
            return false;
        }

        protected override void ExportBtnOnExportStarted()
        {
            if ( !CheckExportTypes() )
                return;
            base.ExportBtnOnExportStarted();
        }

        private void OnDateValueChanged( object sender, EditValueChangedEventArgs e )
        {
            Progress.ClearStatus();
        }

        private void FindClick( object sender, RoutedEventArgs e )
        {
            if ( !ValidateSettings() || !CheckExportTypes() )
                return;
            ObservableCollection<Level1ChangeMessage> observableCollection = new ObservableCollection<Level1ChangeMessage>();
            FindedChanges.ItemsSource = observableCollection;
            Progress.Load( GetAllValues<Level1ChangeMessage>( new DateTime?(), new DateTime?(), false ), new Action<IEnumerable<Level1ChangeMessage>>( ( observableCollection ).AddRange ), 10000, null, null );
        }

        private void Level1FieldsCtrl_EditValueChanged( object sender, EditValueChangedEventArgs e )
        {
            ISet<Level1Fields> set = Level1FieldsCtrl.SelectedFields.ToSet();
            foreach ( KeyValuePair<Level1Fields, GridColumn> column in _columns )
                column.Value.Visible = set.Contains( column.Key );
        }

        private void ShowChartCandles_Click( object sender, RoutedEventArgs e )
        {
            if ( !ValidateSettings() )
                return;
            foreach ( Security selectedSecurity in SelectedSecurities )
            {
                ChartPane chartPane = new ChartPane();
                MainWindow.Instance.ShowPane( chartPane );
                CandleSeries series = new CandleSeries( typeof( TickCandle ), selectedSecurity, 1 );
                chartPane.DrawCandles( series, GetValues<Level1ChangeMessage>( selectedSecurity.ToSecurityId( null, true, false ) ).ToOrderBooks().ToCandles( series, Level1Fields.SpreadMiddle, null ).ToEntities<CandleMessage, Candle>( series.Security, null ), TimeZone.TimeZone );
            }
        }

        private void ShowChartSpread_Click( object sender, RoutedEventArgs e )
        {
            if ( !ValidateSettings() )
                return;
            foreach ( SecurityId selectedSecurityId in SelectedSecurityIds )
            {
                ChartPane chartPane = new ChartPane();
                MainWindow.Instance.ShowPane( chartPane );
                chartPane.DrawLines( selectedSecurityId, GetValues<Level1ChangeMessage>( selectedSecurityId ).ToOrderBooks(), TimeZone.TimeZone );
            }
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            if ( storage.ContainsKey( "SelectedLevel1Fields" ) )
                Level1FieldsCtrl.SelectedFields = storage.GetValue<string>( "SelectedLevel1Fields", null ).SplitByComma( false ).Select( s => s.To<Level1Fields>() ).ToArray();
            FindedChanges.Load( storage.GetValue<SettingsStorage>( "FindedChanges", null ) );
            Level1Fields[ ] array = Level1FieldsCtrl.SelectedFields.ToArray();
            foreach ( KeyValuePair<Level1Fields, GridColumn> column in _columns )
                column.Value.Visible = array.Contains( column.Key );
            TimeZone.TimeZone = storage.GetValue( "TimeZone", TimeZone.TimeZone );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "SelectedLevel1Fields", Level1FieldsCtrl.SelectedFields.Select( f => f.ToString() ).JoinComma() );
            storage.SetValue( "FindedChanges", FindedChanges.Save() );
            storage.SetValue( "TimeZone", TimeZone.TimeZone );
        }

        
    }
}
