using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using Ecng.Collections;
using Ecng.Reflection;
using Ecng.Serialization;
using Ecng.Xaml;
using Ookii.Dialogs.Wpf;
using StockSharp.Localization;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace StockSharp.Xaml.Code
{
    public partial class CodeReferencesWindow : DXWindow, IPersistable
    {
        public static readonly RoutedCommand OkCommand = new RoutedCommand();
        public static readonly RoutedCommand AddCommand = new RoutedCommand();
        public static readonly RoutedCommand RemoveCommand = new RoutedCommand();
        private readonly ObservableCollection<CodeReference> _references = new ObservableCollection<CodeReference>();

        private bool _isReady;

        public CodeReferencesWindow( )
        {
            InitializeComponent();

            ReferencesListView.ItemsSource = _references;
        }

        public IList<CodeReference> References
        {
            get
            {
                return ( IList<CodeReference> ) _references;
            }
        }

        private void CanOk( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = _isReady;
        }

        private void ExecuteOk( object sender, ExecutedRoutedEventArgs e )
        {
            ( ( Window ) this ).Close();
        }

        private void CanAdd( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = true;
        }

        private void ExecuteAdd( object sender, ExecutedRoutedEventArgs e )
        {
            VistaOpenFileDialog dialog = new VistaOpenFileDialog();
            dialog.CheckFileExists = true;
            dialog.Multiselect = false;
            dialog.Filter = LocalizedStrings.Str1422;

            if ( !dialog.ShowModal( this ) )
            {
                return;
            }

            Assembly assembly = ReflectionHelper.VerifyAssembly( dialog.FileName );
            if ( assembly == ( Assembly ) null )
            {
                new MessageBoxBuilder().Text( LocalizedStrings.Str1423 ).Warning().Owner( this ).Show();
            }
            else
            {
                _references.Add( new CodeReference()
                {
                    Name = assembly.GetName().Name,
                    Location = assembly.Location
                } );
                _isReady = true;
            }
        }

        private void CanRemove( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = ( ( DataControlBase ) ReferencesListView )?.SelectedItem != null;
        }

        private void ExecuteRemove( object sender, ExecutedRoutedEventArgs e )
        {
            _references.RemoveRange(  ReferencesListView.SelectedItems.Cast<CodeReference>().ToArray() );
            _isReady = true;
        }

        public void Load( SettingsStorage storage )
        {
            ( ( Window ) this ).LoadWindowSettings( storage );
            ( ( DependencyObject ) BarManager ).LoadDevExpressControl( ( string ) storage.GetValue<string>( "BarManager", null ) );
            ReferencesListView.Load( ( SettingsStorage ) storage.GetValue<SettingsStorage>( "ReferencesListView", null ) );
        }

        public void Save( SettingsStorage storage )
        {
            ( ( Window ) this ).SaveWindowSettings( storage );
            storage.SetValue<string>( "BarManager", ( ( DependencyObject ) BarManager ).SaveDevExpressControl() );
            storage.SetValue<SettingsStorage>( "ReferencesListView", PersistableHelper.Save( ( IPersistable ) ReferencesListView ) );
        }

    }
}
