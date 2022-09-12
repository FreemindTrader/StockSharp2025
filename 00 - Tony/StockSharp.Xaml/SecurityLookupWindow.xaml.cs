using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using Ecng.Common;
using Ecng.ComponentModel;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Xaml.PropertyGrid;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    public partial class SecurityLookupWindow : DXWindow
    {
        public static readonly RoutedCommand OkCommand = new RoutedCommand( );
               
        public SecurityLookupWindow( )
        {
            InitializeComponent( );
            ShowDataSourcePanel = false;
            Init( new Security( ) );
        }

        private void Init( Security sec )
        {
            if ( sec == null )
            {
                throw new ArgumentNullException( "security" );
            }

            var selected = ( ISecurityLookupInfo ) PropGrid.SelectedObject;

            if ( selected != null )
            {
                selected.SecurityInfoChangedEvent -= OnSecurityInfoChangedEvent;
            }

            ISecurityLookupInfo newSec = new SecurityLookupInfo( );
            newSec.CopyFrom( sec );
            PropGrid.SelectedObject = newSec;

            newSec.SecurityInfoChangedEvent += OnSecurityInfoChangedEvent;
        }

        private void OnSecurityInfoChangedEvent( )
        {
            var selected = ( ISecurityLookupInfo )PropGrid.SelectedObject;
            selected.SecurityInfoChangedEvent -= OnSecurityInfoChangedEvent;

            Security newSec = new Security( );
            selected.CopyTo( newSec );

            ISecurityLookupInfo finalSec = selected is SecurityLookupInfo ? new LiteSecurityLookupInfo( ) : ( ISecurityLookupInfo )new SecurityLookupInfo( );
            finalSec.CopyFrom( newSec );
            PropGrid.SelectedObject = finalSec;

            finalSec.SecurityInfoChangedEvent += OnSecurityInfoChangedEvent;
        }

        public bool ShowAllOption
        {
            get
            {
                return AllLabel.Visibility == Visibility.Visible;
            }
            set
            {
                AllLabel.Visibility = All.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public bool ShowDataSourcePanel
        {
            get
            {
                return ConnectionsGroupBox.Visibility == Visibility.Visible;
            }
            set
            {
                ConnectionsGroupBox.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public IList<Tuple<string, object>> DataSourceItemsSource
        {
            get
            {
                return DataSourcePanel.ItemsSource;
            }
        }

        public Tuple<string, object> SelectedDataSource
        {
            get
            {
                return DataSourcePanel.SelectedItem;
            }
            set
            {
                DataSourcePanel.SelectedItem = value;
            }
        }

        public Action Configure
        {
            get
            {
                return DataSourcePanel.Configure;
            }
            set
            {
                DataSourcePanel.Configure = value;
            }
        }

        public Security Criteria
        {
            get
            {
                Security sec = new Security( );
                bool? isChecked = All.IsChecked;

                if ( !( isChecked.GetValueOrDefault( ) & isChecked.HasValue ) )
                {
                    ( ( ISecurityLookupInfo )PropGrid.SelectedObject ).CopyTo( sec );
                }
                    
                return sec;
            }
            set
            {
                if ( value == null )
                    throw new ArgumentNullException( nameof( value ) );
                Init( value );
                if ( ShowAllOption && value.IsLookupAll( ) )
                    All.IsChecked = new bool?( true );
                RefreshPropGrid( );
            }
        }

        private void checkEdit_0_EditValueChanged( object sender, EditValueChangedEventArgs e )
        {
            RefreshPropGrid( );
        }

        private void CanExecuteOkCommand( object sender, CanExecuteRoutedEventArgs e )
        {
            ISecurityLookupInfo selectedObject = ( ISecurityLookupInfo )PropGrid.SelectedObject;
            CanExecuteRoutedEventArgs executeRoutedEventArgs = e;
            int num;
            if ( selectedObject != null )
            {
                bool? isChecked = All.IsChecked;
                if ( isChecked.GetValueOrDefault( ) & isChecked.HasValue || !selectedObject.Code.IsEmpty( ) || selectedObject.Type.HasValue )
                {
                    num = !ShowDataSourcePanel ? 1 : ( SelectedDataSource != null ? 1 : 0 );
                    goto label_4;
                }
            }
            num = 0;
            label_4:
            executeRoutedEventArgs.CanExecute = num != 0;
        }

        private void ExecuteOkCommand( object sender, ExecutedRoutedEventArgs e )
        {
            DialogResult = new bool?( true );
        }

        private void RefreshPropGrid( )
        {
            
            bool? isChecked = All.IsChecked;
            
            PropGrid.IsEnabled = !All.IsChecked.GetValueOrDefault( ) & All.IsChecked.HasValue;
        }

        private void method_5( )
        {
            if ( !ShowDataSourcePanel )
                return;

            if ( !( this.DataSourcePanel.SelectedItem?.Item2 is IMessageAdapter messageAdapter ) )
            {
                All.IsEnabled = true;
            }
            else
            {
                All.IsEnabled = messageAdapter.IsSupportSecuritiesLookupAll;

                if ( All.IsEnabled )
                    return;

                bool? isChecked = All.IsChecked;

                if ( !( isChecked.GetValueOrDefault( ) & isChecked.HasValue ) )
                    return;

                All.IsChecked = new bool?( false );
            }
        }

        

        private interface ISecurityLookupInfo
        {
            event Action SecurityInfoChangedEvent;            
            string Code { get; set; }

            SecurityTypes? Type { get; set; }

            SecurityTypes? UnderlyingSecurityType { get; set; }

            void CopyFrom( Security sec );

            void CopyTo( Security sec );
        }

        private sealed class LiteSecurityLookupInfo : Security, ISecurityLookupInfo
        {
            public event Action SecurityInfoChangedEvent;
            private bool _isLite;            

            [Display( Description = "Str1559", GroupName = "General", Name = "Str1559", Order = 0, ResourceType = typeof( LocalizedStrings ) )]
            public bool IsLite
            {
                get
                {
                    return _isLite;
                }
                set
                {
                    _isLite = value;

                    SecurityInfoChangedEvent?.Invoke( );
                }
            }

            [Browsable( false )]
            public override ExchangeBoard Board
            {
                get
                {
                    return base.Board;
                }
                set
                {
                    base.Board = value;
                }
            }

            [Display( Description = "Str549", GroupName = "General", Name = "Board", Order = 2, ResourceType = typeof( LocalizedStrings ) )]
            public ExchangeBoard Board2
            {
                get
                {
                    return Board;
                }
                set
                {
                    Board = value;
                    Notify( nameof( Board2 ) );
                }
            }

            void ISecurityLookupInfo.CopyFrom( Security sec )
            {
                if ( sec == null )
                    throw new ArgumentNullException( "criteria" );

                sec.CopyTo( this );
            }

            string ISecurityLookupInfo.Code
            {
                get
                {
                    return Code;
                }
                set
                {
                    Code = value;
                }
            }

            SecurityTypes? ISecurityLookupInfo.Type
            {
                get
                {
                    return Type;
                }
                set
                {
                    Type = value;
                }
            }

            SecurityTypes? ISecurityLookupInfo.UnderlyingSecurityType
            {
                get
                {
                    return UnderlyingSecurityType;
                }
                set
                {
                    UnderlyingSecurityType = value;
                }
            }

            void ISecurityLookupInfo.CopyTo( Security sec )
            {
                CopyTo( sec );
            }
        }

        private sealed class SecurityLookupInfo : NotifiableObject, ISecurityLookupInfo
        {
            public event Action SecurityInfoChangedEvent;
            private bool _isExtended;
            private string _code;
            private SecurityTypes? _secType;
            private SecurityTypes? _underlyingSecurityType;            

            [Display( Description = "Str2425", GroupName = "General", Name = "Str2424", Order = 0, ResourceType = typeof( LocalizedStrings ) )]
            public bool IsExtended
            {
                get
                {
                    return _isExtended;
                }
                set
                {
                    _isExtended = value;
                    
                    SecurityInfoChangedEvent?.Invoke( );
                }
            }

            [Display( Description = "Str349Dot", GroupName = "General", Name = "Code", Order = 1, ResourceType = typeof( LocalizedStrings ) )]
            [Required( AllowEmptyStrings = false )]
            public string Code
            {
                get
                {
                    return _code;
                }
                set
                {
                    _code = value;
                }
            }

            [Display( Description = "Str360", GroupName = "General", Name = "Type", Order = 3, ResourceType = typeof( LocalizedStrings ) )]
            public SecurityTypes? Type
            {
                get
                {
                    return _secType;
                }
                set
                {
                    _secType = value;
                }
            }

            [Display( Description = "UnderlyingSecurityTypeDot", GroupName = "Str437", Name = "AssetType", Order = 4, ResourceType = typeof( LocalizedStrings ) )]
            public SecurityTypes? UnderlyingSecurityType
            {
                get
                {
                    return _underlyingSecurityType;
                }
                set
                {
                    _underlyingSecurityType = value;
                }
            }

            void ISecurityLookupInfo.CopyFrom( Security sec )
            {
                if ( sec == null )
                {
                    throw new ArgumentNullException( "criteria" );
                }
                    
                Code                   = sec.Code;
                Type                   = sec.Type;
                UnderlyingSecurityType = sec.UnderlyingSecurityType;
            }

            void ISecurityLookupInfo.CopyTo( Security sec )
            {
                if ( sec == null )
                    throw new ArgumentNullException( "criteria" );

                sec.Code                   = Code;
                sec.Type                   = Type;
                sec.UnderlyingSecurityType = UnderlyingSecurityType;
            }
        }
    }
}
