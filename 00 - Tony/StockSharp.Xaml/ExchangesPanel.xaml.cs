using Ecng.Common;
using Ecng.Configuration;
using Ecng.Xaml;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    public partial class ExchangesPanel : UserControl    
    {
        private static readonly Regex _checkCodeRegex = new Regex("^[a-z0-9]{1,15}$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private readonly IExchangeInfoProvider _exchangeInfoProvider;
        private readonly ObservableCollection<Exchange> _exchange;
        private string _selectedExchangeName;

        public ExchangesPanel()
        {
            if ( ! this.IsDesignMode() )
            {
                _exchangeInfoProvider = ( IExchangeInfoProvider ) ConfigManager.GetService<IExchangeInfoProvider>( );
                _exchange = new ObservableCollection<Exchange>( _exchangeInfoProvider.Exchanges );
            }

            InitializeComponent();

            if( this.IsDesignMode( ) )
                return;

            var vm = new ExchangesPanelViewModel(this, _exchangeInfoProvider );
            vm.DataChanged += Vm_DataChanged;

            DataContext = vm;

            Provider.ExchangeAdded += ExchangeInfoProvider_ExchangeAdded;
        }

        private void Vm_DataChanged( )
        {
            ViewModel.SaveExchange( );
        }

        private ExchangesPanelViewModel ViewModel => ( ExchangesPanelViewModel ) DataContext;

        private void ExchangeInfoProvider_ExchangeAdded( Exchange exchange )
        {
            Dispatcher.GuiAsync( ( ) =>
            {
                //using (Dispatcher.DisableProcessing())
                //{
                //var selectedVal = CbExchangeName.SelectedValue as string ?? CbExchangeName.Text.Trim();
                //Exchanges.Clear();

                Exchanges.Add( exchange );

                CbExchangeName.SelectedItem = exchange;
                //if(!SelectedExchangeName.IsEmpty())
                SetExchange( exchange.Name );
                //}
            } );


        }

        private IExchangeInfoProvider Provider 
        {
            get
            {
                return this._exchangeInfoProvider;
            }
            
        }

        public ObservableCollection<Exchange> Exchanges
        {
            get
            {
                return this._exchange;
            }
        }

        public string SelectedExchangeName
        {
            get
            {
                return _selectedExchangeName;
            }
            private set
            {
                if ( _selectedExchangeName == value )
                    return;

                _selectedExchangeName = value;
                SelectedExchangeChanged?.Invoke();
            }
        }

        /// <summary>
        /// The selected exchange change event .
        /// </summary>
        public event Action SelectedExchangeChanged;

        public void SetExchange( string exchangeName )
        {
            if ( exchangeName.IsEmpty(  ) )
            {
                this.ResetEditor();
            }
            else
            {
                ViewModel.SetExchange( exchangeName );
                SelectedExchangeName = ViewModel.ExchangeName;
                CbExchangeName.SelectedItem = Exchanges.FirstOrDefault<Exchange>( e => e.Name == SelectedExchangeName );
            }
        }

        internal Exchange GetSelectedExchange( )
        {
            return this.ViewModel.Exchange;
        }

        private void ResetEditor( )
        {
            ViewModel.SetExchange( null );
            SelectedExchangeName = null;
            CbExchangeName.SelectedItem = null;
            CbExchangeName.Text = string.Empty;
        }

        private void CbExchangeName_KeyDown( object sender, KeyEventArgs e )
        {
            ( ( ComboBox ) sender ).IsDropDownOpen = true;
        }

        private void CbExchangeName_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {

        }

        private void CbExchangeName_DropDownClosed( object sender, EventArgs e )
        {
            var cb = (ComboBox)sender;
            if ( cb.IsDropDownOpen )
                return;

            var curText = cb.SelectedValue as string ?? cb.Text.Trim();

            if ( curText.IsEmpty() )
                return;

            ViewModel.SetExchange( curText );
            SelectedExchangeName = ViewModel.ExchangeName;
        }

        private void CbExchangeName_Loaded( object sender, RoutedEventArgs e )
        {            
            ComboBox cb = (ComboBox) sender;
            var tb = cb.Template.FindName( "PART_EditableTextBox", ( FrameworkElement ) cb ) as TextBox;

            if ( tb == null )
            {
                return;
            }

            DataObject.AddPastingHandler( tb, CheckPasteFormat );
            tb.CharacterCasing = CharacterCasing.Upper;
            tb.PreviewTextInput += ( o, args ) =>
            {
                var newText = tb.Text + args.Text;
                if ( !_checkCodeRegex.IsMatch( newText ) )
                    args.Handled = true;
            };
        }

        private static void CheckPasteFormat( object sender, DataObjectPastingEventArgs e )
        {
            var tb = (TextBox)sender;
            var str = e.DataObject.GetData(typeof(string)) as string;
            var newStr = tb.Text + str;
            if ( !_checkCodeRegex.IsMatch( newStr ) )
                e.CancelCommand();
        }
    }
}
