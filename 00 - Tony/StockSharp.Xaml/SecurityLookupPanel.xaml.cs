using DevExpress.Xpf.LayoutControl;
using Ecng.Common;
using Ecng.Serialization;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Xaml.PropertyGrid;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    /// <summary>
    /// Interaction logic for SecurityLookupPanel.xaml
    /// </summary>
    public partial class SecurityLookupPanel : UserControl, IPersistable
    {
        public static RoutedCommand SearchSecurityCommand = new RoutedCommand();
       
        public SecurityLookupPanel()
        {
            InitializeComponent();

            Filter = new Security();
        }

        /// <summary>
		/// The filter for instrument search.
		/// </summary>
		private Security Filter
        {
            get { return ( Security ) SecurityFilterEditor.SelectedObject; }
            set
            {
                if ( value == null )
                    throw new ArgumentNullException( nameof( value ) );

                SecurityFilterEditor.SelectedObject = value;
            }
        }

        /// <summary>
		/// The start of instrument search event.
		/// </summary>
		public event Action<Security> Lookup;

        private void ExecutedSearchSecurityCommand( object sender, ExecutedRoutedEventArgs e )
        {
            Lookup?.Invoke( Filter );
        }

        private void CanExecutedSearchSecurityCommand( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = Filter != null;// && !SecurityCodeLike.Text.IsEmpty();
        }


        /// <summary>
		/// Load settings.
		/// </summary>
		/// <param name="storage">Settings storage.</param>
		public void Load( SettingsStorage storage )
        {
            SecurityCodeLike.Text = storage.GetValue<string>( nameof( SecurityCodeLike ) );
            Filter = storage.GetValue<Security>( nameof( Filter ) );
        }

        /// <summary>
        /// Save settings.
        /// </summary>
        /// <param name="storage">Settings storage.</param>
        public void Save( SettingsStorage storage )
        {
            storage.SetValue( nameof( SecurityCodeLike ), SecurityCodeLike.Text );
            storage.SetValue( nameof( Filter ), Filter.Clone() );
        }
        private void SecurityCodeLike_PreviewKeyUp( object sender, KeyEventArgs e )
        {
            if ( e.Key != Key.Enter )
                return;

            Filter.Code = SecurityCodeLike.Text.Trim();

            if ( Filter.IsLookupAll() )
                Filter.Code = string.Empty;
            

            Lookup?.Invoke( Filter );
        }

        private void ClearFilter( object sender, RoutedEventArgs e )
        {
            Filter = new Security();
        }
    }
}
