using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using Ecng.Xaml;
using StockSharp.BusinessEntities;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    public partial class SecurityPickerWindow : DXWindow    
    {
        public SecurityPickerWindow( )
        {
            InitializeComponent( );

            this.ShowOk = true;
        }

        private void Picker_SecuritySelected( Security sec )
        {
            if ( !this.ShowOk )
                return;

            this.SelectedSecurity = sec;

            OkBtn.IsEnabled = true;
        }

        private void Picker_SecurityDoubleClick( Security sec )
        {
            if ( !this.ShowOk )
                return;

            this.SelectedSecurity = sec;
            this.DialogResult = true;
        }

        public MultiSelectMode SelectionMode
        {
            get
            {
                return this.Picker.SelectionMode;
            }
            set
            {
                this.Picker.SelectionMode = value;
            }
        }

        public Security SelectedSecurity
        {
            get
            {
                return this.Picker.SelectedSecurity;
            }
            set
            {
                this.Picker.SelectedSecurity = value;
            }
        }

        public IList<Security> SelectedSecurities
        {
            get
            {
                return this.Picker.SelectedSecurities;
            }
        }

        public ISet<Security> ExcludeSecurities
        {
            get
            {
                return this.Picker.ExcludeSecurities;
            }
        }

        public ISecurityProvider SecurityProvider
        {
            get
            {
                return this.Picker.SecurityProvider;
            }
            set
            {
                this.Picker.SecurityProvider = value;
            }
        }

        public IMarketDataProvider MarketDataProvider
        {
            get
            {
                return this.Picker.MarketDataProvider;
            }
            set
            {
                this.Picker.MarketDataProvider = value;
            }
        }

        public bool ShowOk
        {
            get
            {
                return OkBtn.GetVisibility( );
            }
            set
            {
                OkBtn.SetVisibility( value );
            }
        }
    }
}
