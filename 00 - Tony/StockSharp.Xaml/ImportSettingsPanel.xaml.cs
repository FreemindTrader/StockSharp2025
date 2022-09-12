using DevExpress.Xpf.Grid;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using MoreLinq;
using StockSharp.Algo.Import;
using StockSharp.Xaml.GridControl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace StockSharp.Xaml
{
    /// <summary>
    /// Interaction logic for ImportSettingsPanel.xaml
    /// </summary>
    public partial class ImportSettingsPanel : UserControl, IPersistable
    {
        //public static RoutedCommand SelectFieldCommand = new RoutedCommand();
        //public static RoutedCommand UnselectFieldCommand = new RoutedCommand();
        //public static RoutedCommand MoveUpFieldCommand = new RoutedCommand();
        //public static RoutedCommand MoveDownFieldCommand = new RoutedCommand();
        private readonly ObservableCollection<FieldMapping> _unselectedFields = new ObservableCollection<FieldMapping>();
        private readonly ObservableCollection<FieldMapping> _selectedFields = new ObservableCollection<FieldMapping>();

        public ImportSettingsPanel( )
        {
            InitializeComponent();

            AllFieldsGrid.ItemsSource = _unselectedFields;
            SelectedFieldsGrid.ItemsSource = _selectedFields;
        }

        public IList<FieldMapping> SelectedFields
        {
            get
            {
                return _selectedFields;
            }
        }

        public IList<FieldMapping> UnSelectedFields
        {
            get
            {
                return _unselectedFields;
            }
        }

        private void SelectField_Executed( object sender, ExecutedRoutedEventArgs e )
        {
            var selectedFields = AllFieldsGrid.SelectedItems.Cast< FieldMapping >( ).ToArray( );

            SelectFields( selectedFields, true );
        }

        public void SelectFields( IEnumerable<FieldMapping> selectedFields, bool autoSetOrders = true )
        {
            selectedFields = ( IEnumerable<FieldMapping> ) selectedFields.ToArray<FieldMapping>();
            int num = _selectedFields.Count > 0 ? _selectedFields.Max( x=>x.Order ?? 0 ) + 1 : 0;

            foreach ( FieldMapping fieldMapping in selectedFields )
            {
                if ( autoSetOrders )
                {
                    fieldMapping.Order = num++;
                }
            }

            _selectedFields.AddRange( selectedFields );
            _unselectedFields.RemoveRange( selectedFields );
        }

        private void SelectField_CanExecute( object sender, CanExecuteRoutedEventArgs e )
        {
            var selectedFields = AllFieldsGrid.SelectedItems.Cast< FieldMapping >( ).ToArray( );

            e.CanExecute = AllFieldsGrid != null && selectedFields.Any();
        }

        private void UnselectField_Executed( object sender, ExecutedRoutedEventArgs e )
        {
            var selectedFields = SelectedFieldsGrid.SelectedItems.Cast< FieldMapping >( ).ToArray( );

            UnselectFields( selectedFields );
        }

        public void UnselectFields( IEnumerable<FieldMapping> items )
        {
            _selectedFields.RemoveRange( items );
            _unselectedFields.AddRange( items );
        }

        private void UnselectField_CanExecute( object sender, CanExecuteRoutedEventArgs e )
        {
            var selectedFields = SelectedFieldsGrid?.SelectedItems.Cast< FieldMapping >( ).ToArray( );

            e.CanExecute = SelectedFieldsGrid != null && selectedFields.Any() && selectedFields.All( x => x.IsRequired );
        }

        private void MoveUp_Executed( object sender, ExecutedRoutedEventArgs e )
        {
            var selectedFields = SelectedFieldsGrid.SelectedItems.Cast< FieldMapping >( ).ToArray( );

            selectedFields.ForEach( x =>
            {
                int index = _selectedFields.IndexOf( x );
                if ( index == 0 )
                {
                    throw new InvalidOperationException( "Wrong item position." );
                }

                var item1                    = _selectedFields[ index - 1 ];
                var item2                    = _selectedFields[ index ];
                _selectedFields[ index - 1 ] = item2;
                _selectedFields[ index ] = item1;
            } );

            SelectedFieldsGrid.SelectedItems.Clear();

            selectedFields.ForEach( x => SelectedFieldsGrid.SelectedItems.Add( x ) );
        }

        private void MoveUp_CanExecute( object sender, CanExecuteRoutedEventArgs e )
        {
            var selectedFields = SelectedFieldsGrid?.SelectedItems.Cast< FieldMapping >( ).ToArray( );

            e.CanExecute = ( SelectedFieldsGrid?.SelectedItem != null ) && selectedFields.Any() && selectedFields.All( x => _selectedFields.IndexOf( x ) > 0 );
        }

        private void MoveDown_Executed( object sender, ExecutedRoutedEventArgs e )
        {
            var selectedFields = SelectedFieldsGrid.SelectedItems.Cast< FieldMapping >( ).ToArray( );

            selectedFields.Reverse()
            .ForEach( x =>
                {
                    int index = _selectedFields.IndexOf( x );
                    if ( index == 0 )
                    {
                        throw new InvalidOperationException( "Wrong item position." );
                    }

                    FieldMapping fieldMapping1 = _selectedFields[index - 1];
                    FieldMapping fieldMapping2 = _selectedFields[index];
                    _selectedFields[ index - 1 ] = fieldMapping2;
                    _selectedFields[ index ] = fieldMapping1;
                } );
        }

        private void MoveDown_CanExecute( object sender, CanExecuteRoutedEventArgs e )
        {
            var selectedFields = SelectedFieldsGrid?.SelectedItems.Cast< FieldMapping >( ).ToArray( );

            e.CanExecute = ( SelectedFieldsGrid?.SelectedItem != null ) && selectedFields.Any() && selectedFields.All( x => ( _selectedFields.IndexOf( x ) < _selectedFields.Count - 1 ) );

        }

        private void AllFieldsGrid_ItemDoubleClick( object arg1, ItemDoubleClickEventArgs e )
        {
            SelectFields(  new FieldMapping[ 1 ]{(FieldMapping) e.Row }, true );
        }

        private void SelectedFieldsGrid_ItemDoubleClick( object arg1, ItemDoubleClickEventArgs e )
        {
            if ( !e.Column.FieldName.CompareIgnoreCase(  "DisplayName" ) )
            {
                return;
            }

            FieldMapping row = (FieldMapping) e.Row;
            if ( row.IsRequired )
            {
                return;
            }

            this.UnselectFields( new FieldMapping[ 1 ] { row } );
        }

        public void Load( SettingsStorage storage )
        {
            this.UnselectFields( ( IEnumerable<FieldMapping> ) ( ( IEnumerable<FieldMapping> ) this.SelectedFields ).ToArray<FieldMapping>( ) );
            this.SelectFields( this.LoadSelectedFields( ( IEnumerable<SettingsStorage> ) storage.GetValue<IEnumerable<SettingsStorage>>( "SelectedFields", null ) ), false );
        }

        private IEnumerable<FieldMapping> LoadSelectedFields( IEnumerable<SettingsStorage> settings )
        {
            List<FieldMapping> fieldMappingList = new List<FieldMapping>();

            foreach ( SettingsStorage field in settings )
            {
                FieldMapping fieldMapping = UnSelectedFields.FirstOrDefault( f => f.Name.CompareIgnoreCase( field.GetValue<string>( "Name" ) ));
                if ( fieldMapping != null )
                {
                    fieldMapping.Load( field );
                    fieldMappingList.Add( fieldMapping );
                }
            }
            
            return ( IEnumerable<FieldMapping> ) fieldMappingList;
        }

        public void Save( SettingsStorage storage )
        {
            storage.SetValue<SettingsStorage[ ]>( "SelectedFields",  SelectedFields.Select( f => f.Save() ).ToArray<SettingsStorage>( ) );
        }
    }
}
