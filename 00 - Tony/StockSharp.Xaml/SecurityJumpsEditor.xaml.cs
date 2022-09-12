using DevExpress.Xpf.Grid;
using Ecng.Common;
using MoreLinq;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Xaml.GridControl;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    /// <summary>
    /// Interaction logic for SecurityJumpsEditor.xaml
    /// </summary>
    public partial class SecurityJumpsEditor : UserControl
    {
        public static readonly DependencyProperty SecurityProviderProperty = DependencyProperty.Register(nameof (SecurityProvider), typeof (ISecurityProvider), typeof (SecurityJumpsEditor), new PropertyMetadata((PropertyChangedCallback) null));
        private readonly ObservableCollection<SecurityJump> _jumps = new ObservableCollection<SecurityJump>();

        public event Action Changed;

        /// <summary>
        /// The rollover change event.
        /// </summary>
        public event Action<SecurityJump> JumpSelected;
        
        public SecurityJumpsEditor( )
        {
            InitializeComponent( );

            _jumps.CollectionChanged += JumpsOnCollectionChanged;

            JumpsGrid.ItemsSource = _jumps;
        }

        public ISecurityProvider SecurityProvider
        {
            get
            {
                return ( ISecurityProvider ) this.GetValue( SecurityProviderProperty );
            }
            set
            {
                this.SetValue( SecurityProviderProperty,  value );
            }
        }

        public IList<SecurityJump> Jumps
        {
            get
            {
                return ( IList<SecurityJump> ) this._jumps;
            }
        }

        public SecurityJump SelectedJump
        {
            get
            {
                return ( SecurityJump ) JumpsGrid.SelectedItem;
            }
            set
            {
                JumpsGrid.SelectedItem = value;
            }
        }

        public IEnumerable<SecurityJump> SelectedJumps
        {
            get
            {
                return ( IEnumerable<SecurityJump> ) ( JumpsGrid.SelectedItems.Cast<SecurityJump>( ).ToArray<SecurityJump>( ) );
            }
        }

        public string Validate( )
        {
            if ( !_jumps.Any( ) )
                return LocalizedStrings.Str1449;

            if ( _jumps.Any( j => j.Security == null ) )
                return LocalizedStrings.Str1450;

            if ( _jumps.Any( j => j.Security is BasketSecurity ) )
                return LocalizedStrings.Str1451;

            if ( _jumps.Any( j => j.Date.IsDefault( ) ) )
                return LocalizedStrings.Str1452;

            if ( _jumps.GroupBy( j => j.Security ).Any( g => g.Count( ) > 1 ) )
                return LocalizedStrings.Str1453;

            if ( _jumps.GroupBy( j => j.Date ).Any( g => g.Count( ) > 1 ) )
                return ( LocalizedStrings.Str1454 );

            return null;
        }

        private void JumpsGrid_SelectedItemChanged( object sender, DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e )
        {
            JumpSelected?.Invoke( SelectedJump );
        }

        private void JumpsOnCollectionChanged( object sender, NotifyCollectionChangedEventArgs e )
        {
            if ( e.NewItems != null && e.NewItems.Count > 0 )
            {
                e.NewItems.Cast<SecurityJump>( ).ForEach( j => j.PropertyChanged += JumpPropertyChanged );                
            }
                

            if ( e.OldItems != null && e.OldItems.Count > 0 )
            {
                e.OldItems.Cast<SecurityJump>( ).ForEach( j => j.PropertyChanged -= JumpPropertyChanged );
            }                            
        }
        
        private void JumpPropertyChanged( object sender, PropertyChangedEventArgs e )
        {
            
            if ( Changed == null )
                return;

            Changed?.Invoke();
        }
    }
}
