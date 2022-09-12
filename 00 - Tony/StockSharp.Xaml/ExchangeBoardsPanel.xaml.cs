using Ecng.Common;
using Ecng.Configuration;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo;
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
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    public partial class ExchangeBoardsPanel : UserControl, IPersistable
    {
        public static readonly DependencyProperty SaveErrorMessageProperty = DependencyProperty.Register( nameof( SaveErrorMessage ), typeof( string ), typeof( ExchangeBoardsPanel ), new PropertyMetadata( ( object )null ) );
        private static readonly Regex _checkBoardRegex = new Regex( "^[a-z0-9]{1,15}$", RegexOptions.IgnoreCase | RegexOptions.Compiled );

        private readonly IExchangeInfoProvider _exchangeInfoProvider;
        private readonly ObservableCollection<ExchangeBoard> _itemSource;

        private DateTime _autoResetMessageTimestamp;
        private string _errorMessage;
        private string _autoResetErrorMessage;
        private bool _updatingUI;
        
        public ExchangeBoardsPanel( )
        {
            if ( !this.IsDesignMode( ) )
            {
                _exchangeInfoProvider = ServicesRegistry.ExchangeInfoProvider;
                _itemSource = new ObservableCollection<ExchangeBoard>( Provider.Boards );
            }
            InitializeComponent( );
            if ( this.IsDesignMode( ) )
            {
                return;
            }

            DataContext = new ExchangeBoardsPanelViewModel( this, Provider );

            ViewModel.DataChanged +=  Vm_DataChanged;
            Provider.BoardAdded += new Action<ExchangeBoard>( ProviderOnBoardAdded );

            GuiDispatcher.GlobalDispatcher.AddPeriodicalAction( new Action( PeriodicalAction ) );
            SetBoardCode( ExchangeBoard.Nasdaq.Code );
        }

        private IExchangeInfoProvider Provider
        {
            get
            {
                return _exchangeInfoProvider;
            }

        }

        public ObservableCollection<ExchangeBoard> Boards
        {
            get
            {
                return _itemSource;
            }
        }

        private ExchangeBoardsPanelViewModel ViewModel
        {
            get
            {
                return ( ExchangeBoardsPanelViewModel )DataContext;
            }
        }

        internal string SaveErrorMessage
        {
            get
            {
                return ( string )GetValue( ExchangeBoardsPanel.SaveErrorMessageProperty );
            }
            private set
            {
                SetValue( ExchangeBoardsPanel.SaveErrorMessageProperty, value );
            }
        }

        internal Exchange SelectedExchange => ExchangesPanel.GetSelectedExchange( );

        public string SelectedBoardCode
        {
            get
            {
                return ViewModel.BoardCode;
            }
        }

        public event Action Changed;

        public void SetBoardCode( string boardCode )
        {
            _updatingUI = true;
            try
            {
                if ( boardCode.IsEmpty( ) )
                {
                    ResetBoardPanel( );
                    ExchangesPanel.SetExchange( null );
                }
                else
                {
                    ViewModel.SetBoard( boardCode );
                    CbBoardCode.SelectedItem = Boards.FirstOrDefault<ExchangeBoard>( b => b.Code == ViewModel.BoardCode );
                    ExchangesPanel.SetExchange( ViewModel.Board == null ? null : ViewModel.Board.Exchange.Name );
                }
            }
            finally
            {
                _updatingUI = false;
            }
        }

        private void CbBoardCode_KeyDown( object sender, KeyEventArgs e )
        {
            ( ( ComboBox )sender ).IsDropDownOpen = true;
        }

        private void CbBoardCode_DropDownClosed( object sender, EventArgs e )
        {
            ComboBox comboBox = ( ComboBox )sender;
            if ( comboBox.IsDropDownOpen || ViewModel == null || _updatingUI )
            {
                return;
            }

            if ( comboBox.SelectedItem != null )
            {
                string selectedValue = comboBox.SelectedValue as string;
                if ( !selectedValue.IsEmpty( ) )
                {
                    SetBoardCode( selectedValue );
                }
            }
            else
            {
                string str = comboBox.Text.Trim( );
                if ( str.IsEmpty( ) )
                {
                    return;
                }

                ExchangesPanel.SetExchange( null );
                SetBoardCode( str );
            }

            Changed?.Invoke( );
        }

        private void ButtonClear_Click( object sender, RoutedEventArgs e )
        {
            ResetBoardPanel( );
            ExchangesPanel.SetExchange( null );
        }

        private void ResetBoardPanel( )
        {
            _updatingUI = true;
            try
            {
                ViewModel.SetBoard( null );
                SetSaveError( null, false );
                CbBoardCode.Text = string.Empty;
                ExchangesPanel.SetExchange( null );
            }
            finally
            {
                _updatingUI = false;
            }
        }

        private void CbBoardCode_Loaded( object sender, RoutedEventArgs e )
        {
            ExchangeBoardsPanel.Class440 class440 = new ExchangeBoardsPanel.Class440( );
            ComboBox comboBox = ( ComboBox )sender;
            if ( ( class440.textBox_0 = comboBox.Template.FindName( "PART_EditableTextBox", comboBox ) as TextBox ) == null )
            {
                return;
            }

            DataObject.AddPastingHandler( class440.textBox_0, new DataObjectPastingEventHandler( ExchangeBoardsPanel.CheckPasteFormat ) );
            class440.textBox_0.CharacterCasing = CharacterCasing.Upper;
            class440.textBox_0.PreviewTextInput += new TextCompositionEventHandler( class440.method_0 );
        }

        private static void CheckPasteFormat( object sender, DataObjectPastingEventArgs e )
        {
            string input = ( ( TextBox )sender ).Text + ( e.DataObject.GetData( typeof( string ) ) as string );
            if ( ExchangeBoardsPanel._checkBoardRegex.IsMatch( input ) )
            {
                return;
            }

            e.CancelCommand( );
        }

        private void ProviderOnBoardAdded( ExchangeBoard exchangeBoard_0 )
        {
            Dispatcher.GuiAsync( new Action( new ExchangeBoardsPanel.Class441( )
            {
                exchangeBoardsPanel_0 = this,
                exchangeBoard_0 = exchangeBoard_0
            }.method_0 ) );
        }

        private void method_6( )
        {
            if ( _updatingUI )
            {
                return;
            }

            ViewModel.SaveBoard( ExchangesPanel.GetSelectedExchange( ) );
        }

        private void Vm_DataChanged( )
        {
            if ( _updatingUI )
            {
                return;
            }

            ViewModel.SaveBoard( ExchangesPanel.GetSelectedExchange( ) );
        }

        private void PeriodicalAction( )
        {
            if ( _autoResetErrorMessage.IsEmpty( ) || DateTime.UtcNow - _autoResetMessageTimestamp <= TimeSpan.FromSeconds( 5.0 ) )
            {
                return;
            }

            _autoResetErrorMessage = null;
            SaveErrorMessage = _errorMessage;
        }

        internal void SetSaveError( string string_2, bool bool_2 = false )
        {
            if ( string_2.IsEmpty( ) )
            {
                SaveErrorMessage = _errorMessage = _autoResetErrorMessage = null;
            }
            else if ( bool_2 )
            {
                if ( _autoResetErrorMessage.IsEmpty( ) )
                {
                    _errorMessage = SaveErrorMessage;
                }

                SaveErrorMessage = _autoResetErrorMessage = string_2;
                _autoResetMessageTimestamp = DateTime.UtcNow;
            }
            else
            {
                _errorMessage = string_2;
                if ( !_autoResetErrorMessage.IsEmpty( ) )
                {
                    return;
                }

                SaveErrorMessage = _errorMessage;
            }
        }

        public void Save( SettingsStorage storage )
        {
            string selectedBoardCode = SelectedBoardCode;
            if ( selectedBoardCode.IsEmpty( ) )
            {
                return;
            }

            storage.SetValue<string>( "SelectedBoardCode", selectedBoardCode );
        }

        public void Load( SettingsStorage storage )
        {
            SetBoardCode( storage.GetValue<string>( "SelectedBoardCode", ExchangeBoard.Nasdaq.Code ) );
        }
               

        private sealed class Class440
        {
            public TextBox textBox_0;

            internal void method_0( object sender, TextCompositionEventArgs e )
            {
                string input = textBox_0.Text + e.Text;
                if ( ExchangeBoardsPanel._checkBoardRegex.IsMatch( input ) )
                {
                    return;
                }

                e.Handled = true;
            }
        }

        private sealed class Class441
        {
            public ExchangeBoardsPanel exchangeBoardsPanel_0;
            public ExchangeBoard exchangeBoard_0;

            internal void method_0( )
            {
                try
                {
                    exchangeBoardsPanel_0._updatingUI = true;
                    exchangeBoardsPanel_0.Boards.Add( exchangeBoard_0 );
                    exchangeBoardsPanel_0.CbBoardCode.SelectedItem = exchangeBoard_0;
                    exchangeBoardsPanel_0.SetBoardCode( exchangeBoard_0.Code );
                }
                finally
                {
                    exchangeBoardsPanel_0._updatingUI = false;
                }
            }
        }
    }
}
