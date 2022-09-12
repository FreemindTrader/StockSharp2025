
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Configuration;
using Ecng.Xaml;
using Ookii.Dialogs.Wpf;
using StockSharp.Algo.Storages;
using StockSharp.Localization;
using StockSharp.Xaml.GridControl;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    public partial class ExtendedInfoStorageWindow : DXWindow
    {
        private readonly IExtendedInfoStorage _storage;

        private readonly ObservableCollection< Tuple<string, Type> > _stringToTypeCollection = new ObservableCollection<Tuple<string, Type>>();

        public ExtendedInfoStorageWindow()
        {
            InitializeComponent();

            Type[] myTypes = new Type[]
            {
                typeof( int ),
                typeof( string ),
                typeof( bool )
            };

            DataContext = myTypes;

            FieldsGrid.ItemsSource = _stringToTypeCollection;

            if( this.IsDesignMode( ) )
                return;

            _storage = ( IExtendedInfoStorage ) ConfigManager.GetService< IExtendedInfoStorage >( );

            SelectedStorage = _storage.Storages.FirstOrDefault();

        }

        public IEnumerable<IExtendedInfoStorageItem> Storages
        {
            get
            {
                return this.StoragesCtrl.Storages;
            }
            set
            {
                this.StoragesCtrl.Storages = value;
            }
        }

        public IExtendedInfoStorageItem SelectedStorage
        {
            get
            {
                return this.StoragesCtrl.SelectedStorage;
            }
            set
            {
                this.StoragesCtrl.SelectedStorage = value;
            }
        }

        private void CreateFromSample_Click( object sender, RoutedEventArgs e )
        {
            var dialogWindow = new VistaOpenFileDialog();
            dialogWindow.Filter = "CSV (.csv)|*.csv";
            dialogWindow.CheckFileExists =  true ;
            dialogWindow.RestoreDirectory =  true;

            if ( !dialogWindow.ShowModal( this ) )
            {
                return;
            }
            
            string withoutExtension = Path.GetFileNameWithoutExtension( dialogWindow.FileName );

            if ( this._storage.Get( withoutExtension ) != null )
            {
                new MessageBoxBuilder( ).Text( LocalizedStrings.StorageAlreadyExist.Put( withoutExtension ) ).Error( ).Owner( this ).Show( );                
            }
            else
            {
                string fileName = File.ReadLines( dialogWindow.FileName ).FirstOrDefault();

                if ( fileName == null )
                    return;

                string comma = ",";

                if ( !fileName.Contains( comma ) )
                    comma = ";";

                string[] strArray = fileName.Split( comma, true );

                using ( var enumerator = strArray.GroupBy( x => x.ToLowerInvariant() ).Where( x => x.Count() > 1 ).GetEnumerator() )
                {
                    if ( enumerator.MoveNext() )
                    {
                        new MessageBoxBuilder().Text( LocalizedStrings.Str1705Params.Put( enumerator.Current.Key ) ).Error().Owner( ( Window ) this ).Show();
                        
                        return;
                    }
                }

                this.SelectedStorage = this._storage.Create( withoutExtension, strArray.Select( x => Tuple.Create( x, typeof(string) ) ).ToArray() );
            }
        }

        private void DeleteStorage_Click( object sender, RoutedEventArgs e )
        {
            if ( new MessageBoxBuilder().Text( LocalizedStrings.RemoveDriveQuestion ).Warning().YesNo().Owner( this ).Show() == MessageBoxResult.No )
                return;

            this._storage.Delete( this.SelectedStorage );
            this._stringToTypeCollection.Clear();
        }

        private void Ok_Click( object sender, RoutedEventArgs e )
        {
            DialogResult = true;
        }

        private void StoragesCtrl_EditValueChanged( object sender, EditValueChangedEventArgs e )
        {
            Ok.IsEnabled = DeleteStorage.IsEnabled = this.SelectedStorage != null;
            this._stringToTypeCollection.Clear();
            if ( this.SelectedStorage == null )
                return;

            _stringToTypeCollection.AddRange( SelectedStorage.Fields.Select( x => new Tuple< string, Type >( x.Item1, x.Item2 ) ) );
            
        }

        private void FieldsGrid_SelectedItemChanged( object sender, SelectedItemChangedEventArgs e )
        {

        }
    }
}
